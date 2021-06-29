using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleIncliningProgram
{
    public class SerialPortConnection
    {
        public SerialPort serialPort;
        private static Timer serialPortTimer;
        private MainWindow mainWindow;

        private List<string> portNames = new List<string>();
        private bool isCorrectConnection = false;
        private string expectedDeviceName;
        private int testLimit;

        public List<PitchRollData> calibrationData = new List<PitchRollData>();
        public string calibrationDataOutput = "";

        public double rollMean;
        public double pitchMean;
        public double rollSD;
        public double pitchSD;

        // setting cport
        public string expectedComPort; 

        public SerialPortConnection(string deviceName, string cPort, int limit, MainWindow mw)
        {
            mainWindow = mw;
            expectedDeviceName = deviceName;
            expectedComPort = cPort;
            testLimit = limit;

            string comPort = FindCorrectPort();

            if (comPort == null || comPort == string.Empty)
            {
                mainWindow.ShowNoCorrectPortMessage();
                mainWindow.MarkDeviceAsDisconnected();
                return;
            }

            Thread.Sleep(1000);

            serialPort = new SerialPort(comPort);
            serialPort.BaudRate = 2400;
            serialPort.Parity = Parity.Even;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 7;
            serialPort.Handshake = Handshake.None;

            serialPort.Open();
            mainWindow.MarkDeviceAsConnected();

            
            serialPortTimer = new Timer(Callback, null, 500, Timeout.Infinite);

        }

        public void TerminateConnection()
        {
            serialPort.Close();
            serialPort.Dispose();
            serialPortTimer.Dispose();
            SetMean();
            SetStdDev();
            mainWindow.FinishedRecording();
        }

        public void SetStdDev()
        {
            List<Double> rollList = new List<double>();
            List<Double> pitchList = new List<double>();

            foreach (PitchRollData pdr in calibrationData)
            {
                rollList.Add(pdr.Roll);
                pitchList.Add(pdr.Pitch);
            }
            rollSD = StdDev(rollList);
            pitchSD = StdDev(pitchList);
        }

        public void SetMean()
        {
            foreach (PitchRollData pdr in calibrationData)
            {
                rollMean += pdr.Roll;
                pitchMean += pdr.Pitch;
            }

            rollMean /= calibrationData.Count;
            pitchMean /= calibrationData.Count;
        }

        /* Return the standard deviation of an array of doubles. 
         */
        public static double StdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }

        private void Callback(Object state)
        {
            serialPortTimer.Dispose();
            string data = serialPort.ReadExisting();

            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                mainWindow.tBoxDataOut.AppendText(data);
                mainWindow.tBoxDataOut.ScrollToEnd();

                string[] dataIn = data.Split(' ');
                if(dataIn.Length == 2)
                {
                    double x = Convert.ToDouble(dataIn[0]);
                    double y = Convert.ToDouble(dataIn[1]);

                    if(calibrationData.Count() < testLimit)
                    {
                        PitchRollData prd = new PitchRollData();
                        prd.Roll = x;
                        prd.Pitch = y;
                        calibrationData.Add(prd);
                    }
                    else
                    {
                        foreach(PitchRollData pitchRoll in calibrationData)
                        {
                            calibrationDataOutput += pitchRoll.Roll + "," + pitchRoll.Pitch + "\n";
                        }                        
                    }
                }
            }));

            serialPort.WriteLine("C");
            if(calibrationData.Count < testLimit-1)
            {
                serialPortTimer = new Timer(Callback, null, 500, Timeout.Infinite);
            }
            else
            {
                TerminateConnection();
                mainWindow.MarkDeviceAsDisconnected();
            }
        }

        public string FindCorrectPort()
        {
            GetAllComPorts();

            foreach (string port in portNames)
            {
                SerialPort connectionToCheck = new SerialPort(port);
                bool canConnectToPort = TryEstablishConnection(connectionToCheck);
                Thread.Sleep(1000);
                if (canConnectToPort)
                {
                    if (CheckIfCorrectConnection(connectionToCheck))
                    {
                        if (!isCorrectConnection)
                        {
                            return string.Empty;
                        }
                        return port;
                    }
                }
            }
            return string.Empty;
        }

        private bool TryEstablishConnection(SerialPort sp)
        {
            sp.BaudRate = 2400;
            sp.Parity = Parity.Even;
            sp.StopBits = StopBits.One;
            sp.DataBits = 7;
            sp.Handshake = Handshake.None;
            sp.Encoding = ASCIIEncoding.ASCII;

            try
            {
                sp.Open();
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

        private bool CheckIfCorrectConnection(SerialPort connectionToCheck)
        {
            try
            {
                connectionToCheck.DataReceived += new SerialDataReceivedEventHandler(DataReceivedTestHandler);
                connectionToCheck.WriteTimeout = 1000;
                connectionToCheck.WriteLine("S");
                Thread.Sleep(1500);
            }
            catch (TimeoutException ex)
            {
                connectionToCheck.DataReceived -= DataReceivedTestHandler;
                connectionToCheck.Dispose();
                connectionToCheck.Close();
                return false;
            }

            return true;
        }

        private void DataReceivedTestHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            sp.Close();
            if(indata.Contains(this.expectedDeviceName) && !isCorrectConnection)
            {
                isCorrectConnection = true;
            }
            else
            {
                isCorrectConnection = false;
            }
        }
        

        private void GetAllComPorts()
        {            
            portNames.Add(expectedComPort);
        }
        
    }

    
}