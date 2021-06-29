using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataAcquisition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPortConnection serialPortTestConnection;
        private bool isDeviceConnected = false;
        private string expectedDeviceName = "Serial No. 0101";

        // Setting variables
        private string expectedComPort;
        private int expectedBaudRate;
        private int expectedDataBit;
        private StopBits expectedStopBit;
        private Parity expectedParity;

        // Available com ports
        public List<string> ComPorts { get; set; }
        public List<int> BaudRates { get; set; }
        public List<int> DataBits { get; set; }
        public List<int> StopBits { get; set; }
        public List<string> ParityBits { get; set; }

        private string fileName;

        // Store roll and pitch
        public List<double> roll = new List<double>();
        public List<double> pitch = new List<double>();
        public int testCounter;

        public MainWindow()
        {
            InitializeComponent();

            ComPorts = GetPorts();

            FillComboBoxes();

            btnCalibration.IsEnabled = false;
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = false;

            DataContext = this;

            testCounter = 0;
        }

        private void OpenPort(object sender, RoutedEventArgs e)
        {
            //// Testing write to file methods without MOSIS
            //if(!isDeviceConnected)
            //{
            //    WritePortOpen("Port opened at: " + DateTime.Now.ToString("h:mm:ss tt"));

            //    btnWriteS.IsEnabled = true;
            //    btnWriteA.IsEnabled = true;

            //    isDeviceConnected = true;
            //}
            //else
            //{
            //    isDeviceConnected = false;
            //}

            if (cBoxComPort.SelectedItem != null && cBoxBaudRate.SelectedItem != null && cBoxDataBits.SelectedItem != null && cBoxStopBits.SelectedItem != null && cBoxParityBits.SelectedItem != null)
            {
                expectedComPort = cBoxComPort.Text;
                expectedBaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                expectedDataBit = Convert.ToInt32(cBoxDataBits.Text);
                expectedStopBit = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                expectedParity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);

                if (!isDeviceConnected)
                {
                    WritePortOpen("Open port button pressed at: " + DateTime.Now.ToString("h:mm:ss tt"));

                    btnOpenPort.Content = "OPENING...";

                    Thread dataReading = new Thread(() =>
                    {
                        serialPortTestConnection = new SerialPortConnection(expectedComPort, expectedBaudRate, expectedDataBit, expectedStopBit, expectedParity, "S", this);
                    });
                    dataReading.Start();

                    btnCalibration.IsEnabled = true;
                }
                else
                {
                    serialPortTestConnection.TerminateConnection();
                    Thread.Sleep(100);
                    MarkDeviceAsDisconnected();
                    btnCalibration.IsEnabled = false;
                    btnStart.IsEnabled = false;
                    btnStop.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select from all combo boxes.", "ComboBox null", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WritePortOpen(string text)
        {
            DateTime dateTime = DateTime.Now;
            fileName = dateTime.ToString("ddMMyy-Hmm") + ".txt";

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (FileStream fs = File.Create(fileName))
                {
                }
                using (StreamWriter file = new StreamWriter(fileName, true))
                {
                    file.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Could not create file.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        internal void MarkDeviceAsConnected()
        {
            this.Dispatcher.Invoke(() =>
            {
                isDeviceConnected = true;
                btnOpenPort.Content = "CLOSE PORT";

                // Display time port opened in TexBox
                tBoxDataOut.AppendText("Port opened at: " + DateTime.Now.ToString("HH:mm:ss.fff\n"));
            });
        }

        internal void MarkDeviceAsDisconnected()
        {
            this.Dispatcher.Invoke(() =>
            {
                isDeviceConnected = false;
                btnOpenPort.Content = "OPEN PORT";
                tBoxDataOut.AppendText("\nPort closed at: " + DateTime.Now.ToString("h:mm:ss tt"));
            });
        }

        private List<string> GetPorts()
        {
            return SerialPort.GetPortNames().ToList();
        }

        private void WriteS(object sender, RoutedEventArgs e)
        {
            //TestSave(tBoxDataOut.Text);
            serialPortTestConnection.WriteS();
            tBoxDataOut.Text += "Averages: " + Math.Round(roll.Average() / 60, 2) + "deg, " + Math.Round(pitch.Average() / 60, 2) + "deg\n";
            tBoxDataOut.Text += "Inclinimoter data ended at: " + DateTime.Now.ToString("h:mm:ss tt\n");

            roll.Clear();
            pitch.Clear();
        }

        private void TestSave(string text)
        {
            DateTime dateTime = DateTime.Now;
            string testFileName = dateTime.ToString("ddMMyy-Hmmss") + ".txt";

            try
            {
                if (File.Exists(testFileName))
                {
                    File.Delete(testFileName);
                }
                using (FileStream fs = File.Create(testFileName))
                {
                }
                using (StreamWriter file = new StreamWriter(testFileName, true))
                {
                    file.WriteLine(testCounter);
                    file.WriteLine(text);
                    file.WriteLine("Averages: " + Math.Round(roll.Average() / 60, 2) + "deg, " + Math.Round(pitch.Average() / 60, 2) + "deg\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Could not create file.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void WriteA(object sender, RoutedEventArgs e)
        {
            // Comment next line out to test without MOSIS
            serialPortTestConnection.WriteC();
            tBoxDataOut.Text += "Inclinimoter data started at: " + DateTime.Now.ToString("h:mm:ss tt\n");
            //WriteCtoFile("Inclinimoter data started at: " + DateTime.Now.ToString("h:mm:ss tt"));
        }

        private void WriteCtoFile(string text)
        {
            try
            {             
                using (StreamWriter file = new StreamWriter(fileName, true))
                {
                    file.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Could not create file.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FillComboBoxes()
        {
            BaudRates = new List<int>
            {
                300,
                1200,
                2400,
                4800,
                9600,
                19200,
                115200
            };

            DataBits = new List<int>
            {
                7,
                8
            };

            StopBits = new List<int>
            {
                1,
                2
            };

            ParityBits = new List<string>
            {
                "None",
                "Even",
                "Odd"
            };
        }

        private void SaveDataToFile(object sender, RoutedEventArgs e)
        {
            try
            {               
                using (StreamWriter file = new StreamWriter(fileName, true))
                {
                    file.WriteLine(tBoxDataOut.Text);
                    if(roll.Count != 0 && pitch.Count != 0)
                    {
                        file.WriteLine("Averages: " + Math.Round(roll.Average() / 60, 2) + "deg, " + Math.Round(pitch.Average() / 60, 2) + "deg\n");
                    }
                    file.WriteLine("File saved at: " + DateTime.Now.ToString("h:mm:ss tt"));
                }

                MessageBox.Show("File saved as: " + fileName, "File saved successfully.", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Could not create file.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Calibration(object sender, RoutedEventArgs e)
        {
            tBoxDataOut.AppendText("Calibrating... Please do not shut down the program whilst calibration is in progress.\n");
            tBoxDataOut.AppendText("Calibration run started at: " + DateTime.Now.ToString("HH:mm:ss.fff\n"));
            tBoxDataOut.ScrollToEnd();

            serialPortTestConnection.StartCalibration("C");
        }

        private void StartRecording(object sender, RoutedEventArgs e)
        {
            tBoxDataOut.AppendText("\nInclinimoter data started at: " + DateTime.Now.ToString("HH:mm:ss.fff\n"));
            // Start recording inclinometer data
            serialPortTestConnection.WriteC();
        }

        private void StopRecording(object sender, RoutedEventArgs e)
        {
            serialPortTestConnection.StopRecording();

            tBoxDataOut.AppendText("\nInclinimoter data stopped at: " + DateTime.Now.ToString("HH:mm:ss.fff\n"));
            tBoxDataOut.ScrollToEnd();
            tBoxDataOut.AppendText("Averages: " + Math.Round(roll.Average() / 60, 2) + "deg, " + Math.Round(pitch.Average() / 60, 2) + "deg\n");

            roll.Clear();
            pitch.Clear();
        }
    }
}
