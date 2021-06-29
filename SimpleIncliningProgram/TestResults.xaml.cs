using System.Windows;

namespace SimpleIncliningProgram
{
    /// <summary>
    /// Interaction logic for TestResults.xaml
    /// </summary>
    public partial class TestResults : Window
    {
        private MainWindow mw;
        public TestResults(MainWindow mainWindow)
        {
            this.mw = mainWindow;
            InitializeComponent();
            tBoxStartTime.Text = mw.testData.StartTime.ToString();
            tBoxEndTime.Text = mw.testData.EndTime.ToString();
            minRollMean.Text = string.Format("{0}", mw.testData.MinRollMean);
            minPitchMean.Text = string.Format("{0}", mw.testData.MinPitchMean); 
            MinRollSD.Text = string.Format("{0}", mw.testData.MinRollStdDev); 
            MinPitchSD.Text = string.Format("{0}", mw.testData.MinPitchStdDev);
            angRollMean.Text = string.Format("{0}", mw.testData.AngRollMean); 
            angPitchMean.Text = string.Format("{0}", mw.testData.AngPitchMean); 
            angRollSD.Text = string.Format("{0}", mw.testData.AngRollStdDev); 
            angPitchSD.Text = string.Format("{0}", mw.testData.AngPitchStdDev); 
        }
    }
}
