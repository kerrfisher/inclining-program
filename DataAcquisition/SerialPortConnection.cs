using DataAcquisition.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace DataAcquisition
{
    public class SerialPortConnection
    {
        private SerialPort serialPort;
        private static Timer serialPortTimer;

        private string expectedComPort;
        private int expectedBaudRate;
        private int expectedDataBit;
        private StopBits expectedStopBit;
        private Parity expectedParity;
        private MainWindow mainWindow;

        private bool isCorrectConnection = false;
        private string expectedDeviceName = "Serial No. 0101";

        // letter to write to port
        private string letter;

        private bool isFirstReading = false;

        // List of heel and pitch for calibration run
        public List<Motions> calibrationMotions = new List<Motions>();

        public SerialPortConnection(string expectedComPort, int expectedBaudRate, int expectedDataBit, StopBits expectedStopBit, Parity expectedParity, string toWrite, MainWindow mw)
        {
            this.expectedComPort = expectedComPort;
            this.expectedBaudRate = expectedBaudRate;
            this.expectedDataBit = expectedDataBit;
            this.expectedStopBit = expectedStopBit;
            this.expectedParity = expectedParity;
            this.letter = toWrite;
            this.mainWindow = mw;

            string comPort = FindCorrectPort();

            if (comPort == null || comPort == string.Empty)
            {
                mainWindow.MarkDeviceAsDisconnected();
                return;
            }

            serialPort = new SerialPort(this.expectedComPort);
            serialPort.BaudRate = this.expectedBaudRate;
            serialPort.DataBits = this.expectedDataBit;
            serialPort.StopBits = this.expectedStopBit;
            serialPort.Parity = this.expectedParity;
            serialPort.Handshake = Handshake.None;

            serialPort.Open();
            mainWindow.MarkDeviceAsConnected();

            // Write "S" to retrieve serial number
            serialPort.WriteLine(letter);

            // Read serial number from port
            serialPortTimer = new Timer(Open, null, 1000, Timeout.Infinite);
        }

        private string FindCorrectPort()
        {
            string port = expectedComPort;
            SerialPort connectionToCheck = new SerialPort(port);
            bool canConnectToPort = TryEstablishConnection(connectionToCheck);
            Thread.Sleep(1000);
            if (canConnectToPort)
            {
                return port;
            }

            return string.Empty;
        }

        private bool TryEstablishConnection(SerialPort sp)
        {
            sp.BaudRate = expectedBaudRate;
            sp.Parity = expectedParity;
            sp.StopBits = expectedStopBit;
            sp.DataBits = expectedDataBit;
            sp.Handshake = Handshake.None;
            sp.Encoding = ASCIIEncoding.ASCII;

            try
            {
                sp.Open();
                sp.Close();
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                sp.Dispose();
                return false;
            }
            catch (IOException ex)
            {
                sp.Dispose();
                return false;
            }
        }

        private void Callback(object state)
        {
            string toWrite = letter;
            serialPortTimer.Dispose();
            string data = serialPort.ReadExisting();

            try
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Append data to TextBox in MainWindow
                    mainWindow.tBoxDataOut.AppendText(data);
                    // Scroll to end of view
                    mainWindow.tBoxDataOut.ScrollToEnd();

                    // Split data into heel and pitch
                    string[] dataIn = data.Split(' ');

                    if (dataIn.Length == 2)
                    {
                        // Add data to list of heel and pitch in mainWindow
                        mainWindow.roll.Add(Convert.ToDouble(dataIn[0]));
                        mainWindow.pitch.Add(Convert.ToDouble(dataIn[1]));
                    }
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nUnknown command.");
            }

            serialPort.WriteLine(toWrite);
            serialPortTimer = new Timer(Callback, null, 500, Timeout.Infinite);
        }

        // Calibration should run for 5 minutes
        private void Calibration(object state)
        {
            // Read port buffer/stream
            string data = serialPort.ReadExisting();

            // Limit equaling 5 minutes
            int calibrationLimit = 600;

            try
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Only continue if calibration motions count is less than the limit
                    if (calibrationMotions.Count() < calibrationLimit)
                    {
                        // Append buffer/stream contents to TextBox in MainWindow
                        mainWindow.tBoxDataOut.AppendText(data.Replace("\r", "") + "\t" + DateTime.Now.ToString("HH:mm:ss.fff\n"));
                        // Scroll to end of view
                        mainWindow.tBoxDataOut.ScrollToEnd();

                        // Split data and add to motions list
                        string[] dataIn = data.Split(' ');
                        if (dataIn.Length == 2)
                        {
                            calibrationMotions.Add(new Motions(Convert.ToDouble(dataIn[0]), Convert.ToDouble(dataIn[1])));
                        }
                    }
                    else
                    {
                        // When calibration is finished print average etc.
                        mainWindow.tBoxDataOut.AppendText("Averages: " + Math.Round(calibrationMotions.Average(x => x.Heel) / 60, 2) + "deg, " + Math.Round(calibrationMotions.Average(x => x.Pitch) / 60, 2) + "deg\n");
                        mainWindow.tBoxDataOut.AppendText("Calibration run ended at: " + DateTime.Now.ToString("HH:mm:ss.fff\n"));
                        // Dispose of the timer, so as to stop recording
                        serialPortTimer.Dispose();

                        // Enable the start and stop buttons
                        mainWindow.btnStart.IsEnabled = true;
                        mainWindow.btnStop.IsEnabled = true;

                        // Disable the calibration button
                        mainWindow.btnCalibration.IsEnabled = false;
                    }
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nUnknown command.");
            }

            // Repeat
            serialPort.WriteLine(letter);
            serialPortTimer = new Timer(Calibration, null, 500, Timeout.Infinite);
        }

        // Calibration should run for 5 minutes
        private void Open(object state)
        {
            // Dispose of last timer
            serialPortTimer.Dispose();

            // Read port buffer/stream
            string data = serialPort.ReadExisting();

            try
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Append buffer/stream contents to TextBox in MainWindow
                    mainWindow.tBoxDataOut.AppendText(data);
                    // Scroll to end of view
                    mainWindow.tBoxDataOut.ScrollToEnd();
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nUnknown command.");
            }
        }

        public void StartCalibration(string letter)
        {
            // Assign letter "S"
            this.letter = letter;

            // Dispose of last timer
            serialPortTimer.Dispose();

            // Write "S" to retrieve serial number
            serialPort.WriteLine(letter);

            // Start new timer with different callback
            serialPortTimer = new Timer(Calibration, null, 500, Timeout.Infinite);
        }

        internal void TerminateConnection()
        {
            serialPortTimer.Dispose();
            serialPort.Close();
            serialPort.Dispose();
        }

        public void StopRecording()
        {
            serialPortTimer.Dispose();
        }

        internal void WriteC()
        {
            letter = "C";
            serialPortTimer = new Timer(Callback, null, 500, Timeout.Infinite);
        }

        internal void WriteS()
        {
            this.letter = "S";
            isFirstReading = true;
            serialPortTimer = new Timer(Callback, null, 500, Timeout.Infinite);
        }
    }
}