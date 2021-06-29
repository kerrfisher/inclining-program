using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace ComPort
{
    public partial class ComPortControl : Form
    {
        string dataOUT;
        string dataIN;
        string saveData;
        bool isRecording = false;

        public ComPortControl()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);

            btnOpen.Enabled = true;
            btnClose.Enabled = false;

            chBoxDtrEnable.Checked = false;
            serialPort1.DtrEnable = false;
            chBoxRtsEnable.Checked = false;
            serialPort1.RtsEnable = false;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);
                serialPort1.Handshake = Handshake.None;
                serialPort1.Encoding = ASCIIEncoding.ASCII;
                serialPort1.Open();
                //serialPort1.Write("S");
                progressBar1.Value = 100;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblStatusCom.Text = "ON";
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
                tBoxDataOut.Text += saveData;
            }
        }

        private void BtnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = "C";
                tBoxDataIn.Enabled = true;
                serialPort1.Write(dataOUT);
                isRecording = true;
            }
        }

        private void ChBoxDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxDtrEnable.Checked)
            {
                serialPort1.DtrEnable = true;
                MessageBox.Show("DTR Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { serialPort1.DtrEnable = false; }
        }

        private void ChBoxRtsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxRtsEnable.Checked)
            {
                serialPort1.RtsEnable = true;
                MessageBox.Show("RTS Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { serialPort1.RtsEnable = true; }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //dataIN = serialPort1.ReadExisting();
            dataIN = serialPort1.ReadLine();
            this.BeginInvoke(new EventHandler(ShowData));
            //this.BeginInvoke(new EventHandler(UpdateRollAndPitch));
        }

        private void ShowData(object sender, EventArgs e)
        {
            if (tBoxDataIn.Enabled)
            {
                string[] dataIn = dataIN.Split(' ');
                tBoxDataIn.AppendText(dataIn[0] + "\n");
                //tBoxDataIn.Text += "\n" + dataIN;
            }
        }

        private void UpdateRollAndPitch(object sender, EventArgs e)
        {
            if (isRecording)
            {
                saveData += "\n" + dataIN;
            }
        }

        private void BtnCheckCon_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {                
                dataOUT = "S";
                tBoxDataIn.Enabled = true;
                serialPort1.Write(dataOUT);

                btnSendData.Enabled = true;
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (var item in tBoxDataOut.Lines)
            {
                str += item;
            }
            Debug.WriteLine(str);
        }
    }
}
