using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace SimpleIncliningProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SerialPortConnection serialPortTestConnection;
        private bool isDeviceConnected = false;
        private string expectedDeviceName = "Serial No. 0101";
        private DateTime startTime;
        private DateTime endTime;

        // setting variables
        public string expectedComPort;
        public int testLimit;

        // displaying test data in different WPF
        public TestData testData = new TestData();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            expectedComPort = tBoxComPort.Text;
            testLimit = int.Parse(tBoxTestLimit.Text);
            startTime = ReturnDate();

            if (!isDeviceConnected)
            {
                string expectedDeviceName = this.expectedDeviceName;

                Thread dataReading = new Thread(() =>
                {
                    serialPortTestConnection = new SerialPortConnection(expectedDeviceName, expectedComPort, testLimit, this);
                });
                dataReading.Name = "Data Gettings";
                dataReading.Start();
            }
            else
            {
                serialPortTestConnection.TerminateConnection();
                Thread.Sleep(100);
                MarkDeviceAsDisconnected();
            }
        }

        public void FinishedRecording()
        {
            this.Dispatcher.Invoke(() =>
            {
                statusLbl.Content = "STATUS: FINISHED";
                statusLbl.Background = Brushes.Green;
            });            
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {            
            endTime = ReturnDate();
            SetTestData();

            // saving to file
            string fileName = @"C:\Users\KerrFisher\Documents\GitHub\mosis\ComPort\SimpleIncliningProgram\test results\" + startTime.ToString("ddMMyyyy_HHmm") + ".txt";
            CreateFile(fileName);
            SaveData(fileName);

            // show results on new page
            TestResults testResults = new TestResults(this);
            testResults.Show();
        }

        public void SaveData(string fName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fName, true))
            {
                file.WriteLine(startTime.ToString() + ", " + endTime.ToString());
                foreach (PitchRollData prd in serialPortTestConnection.calibrationData)
                {
                    file.WriteLine(prd.Roll + "," + prd.Pitch);
                }
                file.WriteLine(string.Format("Minutes of degrees:\nRoll avg: {0}    Pitch avg: {1}", serialPortTestConnection.rollMean, serialPortTestConnection.pitchMean));
                file.WriteLine(string.Format("Roll std dev: {0}\nPitch std dev: {1}", serialPortTestConnection.rollSD, serialPortTestConnection.pitchSD));
                file.WriteLine(string.Format("Angles in degrees:\nRoll avg: {0}    Pitch avg: {1}", (serialPortTestConnection.rollMean / 60), (serialPortTestConnection.pitchMean / 60)));
                file.WriteLine(string.Format("Roll std dev: {0}\nPitch std dev: {1}", (serialPortTestConnection.rollSD / 60), (serialPortTestConnection.pitchSD / 60)));
            }
        }

        public void CreateFile(string fileName)
        {            
            try
            {
                if(File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (FileStream fs = File.Create(fileName))
                {
                    
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Could not create file!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }            
        }

        public void SetTestData()
        {
            testData.StartTime = startTime;
            testData.EndTime = endTime;
            testData.MinRollMean = serialPortTestConnection.rollMean;
            testData.MinPitchMean = serialPortTestConnection.pitchMean;
            testData.MinRollStdDev = serialPortTestConnection.rollSD;
            testData.MinPitchStdDev = serialPortTestConnection.pitchSD;
            testData.AngRollMean = serialPortTestConnection.rollMean / 60;
            testData.AngPitchMean = serialPortTestConnection.pitchMean / 60;
            testData.AngRollStdDev = serialPortTestConnection.rollSD / 60;
            testData.AngPitchStdDev = serialPortTestConnection.pitchSD / 60;
        }        

        public DateTime ReturnDate()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime;
        }

        public void ShowNoCorrectPortMessage()
        {
            this.Dispatcher.Invoke(() => {
                MessageBox.Show("Can't connect to port.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public void MarkDeviceAsConnected()
        {
            this.Dispatcher.Invoke(() =>
            {
                isDeviceConnected = true;
                btnOpen.Content = "Close";

                statusLbl.Content = "STATUS: RECORDING";
                statusLbl.Background = Brushes.Yellow;
            });
        }

        public void MarkDeviceAsDisconnected()
        {
            this.Dispatcher.Invoke(() =>
            {
                isDeviceConnected = false;
                btnOpen.Content = "Open";
            });
        }

    }
}
