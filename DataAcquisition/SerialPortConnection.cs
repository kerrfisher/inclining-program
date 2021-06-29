using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
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

            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (mainWindow.testCounter == 0)
                {
                    mainWindow.tBoxDataOut.Text += "Calibrating...\n";
                }
                else
                {
                    serialPortTimer.Change(500,0);
                }

                mainWindow.testCounter++;
            }));
            

            serialPortTimer = new Timer(Callback, null, 5000, Timeout.Infinite);
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
                    if (isFirstReading)
                    {
                        isFirstReading = false;
                    }
                    else
                    {
                        mainWindow.tBoxDataOut.AppendText(data);
                    }

                    mainWindow.tBoxDataOut.ScrollToEnd();               

                    string[] dataIn = data.Split(' ');

                    if(dataIn.Length == 2)
                    {
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

        internal void TerminateConnection()
        {
            serialPortTimer.Dispose();
            serialPort.Close();
            serialPort.Dispose();
        }

        internal void WriteA()
        {
            this.letter = "C";
            isFirstReading = true;
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