namespace ComPort
{
    partial class ComPortControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chBoxRtsEnable = new System.Windows.Forms.CheckBox();
            this.chBoxDtrEnable = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxParityBits = new System.Windows.Forms.ComboBox();
            this.cBoxStopBits = new System.Windows.Forms.ComboBox();
            this.cBoxDataBits = new System.Windows.Forms.ComboBox();
            this.cBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.cBoxComPort = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gBStatus = new System.Windows.Forms.GroupBox();
            this.lblStatusCom = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSendData = new System.Windows.Forms.Button();
            this.tBoxDataOut = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCheckCon = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDataOutLength = new System.Windows.Forms.Label();
            this.tBoxDataIn = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gBStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chBoxRtsEnable);
            this.groupBox1.Controls.Add(this.chBoxDtrEnable);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cBoxParityBits);
            this.groupBox1.Controls.Add(this.cBoxStopBits);
            this.groupBox1.Controls.Add(this.cBoxDataBits);
            this.groupBox1.Controls.Add(this.cBoxBaudRate);
            this.groupBox1.Controls.Add(this.cBoxComPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Com Port Control";
            // 
            // chBoxRtsEnable
            // 
            this.chBoxRtsEnable.AutoSize = true;
            this.chBoxRtsEnable.Location = new System.Drawing.Point(162, 174);
            this.chBoxRtsEnable.Name = "chBoxRtsEnable";
            this.chBoxRtsEnable.Size = new System.Drawing.Size(93, 17);
            this.chBoxRtsEnable.TabIndex = 10;
            this.chBoxRtsEnable.Text = "RTS ENABLE";
            this.chBoxRtsEnable.UseVisualStyleBackColor = true;
            this.chBoxRtsEnable.CheckedChanged += new System.EventHandler(this.ChBoxRtsEnable_CheckedChanged);
            // 
            // chBoxDtrEnable
            // 
            this.chBoxDtrEnable.AutoSize = true;
            this.chBoxDtrEnable.Location = new System.Drawing.Point(39, 174);
            this.chBoxDtrEnable.Name = "chBoxDtrEnable";
            this.chBoxDtrEnable.Size = new System.Drawing.Size(94, 17);
            this.chBoxDtrEnable.TabIndex = 4;
            this.chBoxDtrEnable.Text = "DTR ENABLE";
            this.chBoxDtrEnable.UseVisualStyleBackColor = true;
            this.chBoxDtrEnable.CheckedChanged += new System.EventHandler(this.ChBoxDtrEnable_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "PARITY BITS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "STOP BITS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "DATA BITS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "BAUD RATE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "COM PORT";
            // 
            // cBoxParityBits
            // 
            this.cBoxParityBits.FormattingEnabled = true;
            this.cBoxParityBits.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cBoxParityBits.Location = new System.Drawing.Point(144, 136);
            this.cBoxParityBits.Name = "cBoxParityBits";
            this.cBoxParityBits.Size = new System.Drawing.Size(121, 21);
            this.cBoxParityBits.TabIndex = 4;
            this.cBoxParityBits.Text = "Even";
            // 
            // cBoxStopBits
            // 
            this.cBoxStopBits.FormattingEnabled = true;
            this.cBoxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cBoxStopBits.Location = new System.Drawing.Point(144, 109);
            this.cBoxStopBits.Name = "cBoxStopBits";
            this.cBoxStopBits.Size = new System.Drawing.Size(121, 21);
            this.cBoxStopBits.TabIndex = 3;
            this.cBoxStopBits.Text = "1";
            // 
            // cBoxDataBits
            // 
            this.cBoxDataBits.FormattingEnabled = true;
            this.cBoxDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cBoxDataBits.Location = new System.Drawing.Point(144, 82);
            this.cBoxDataBits.Name = "cBoxDataBits";
            this.cBoxDataBits.Size = new System.Drawing.Size(121, 21);
            this.cBoxDataBits.TabIndex = 2;
            this.cBoxDataBits.Text = "7";
            // 
            // cBoxBaudRate
            // 
            this.cBoxBaudRate.FormattingEnabled = true;
            this.cBoxBaudRate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "115200"});
            this.cBoxBaudRate.Location = new System.Drawing.Point(144, 55);
            this.cBoxBaudRate.Name = "cBoxBaudRate";
            this.cBoxBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cBoxBaudRate.TabIndex = 1;
            this.cBoxBaudRate.Text = "2400";
            // 
            // cBoxComPort
            // 
            this.cBoxComPort.FormattingEnabled = true;
            this.cBoxComPort.Location = new System.Drawing.Point(144, 28);
            this.cBoxComPort.Name = "cBoxComPort";
            this.cBoxComPort.Size = new System.Drawing.Size(121, 21);
            this.cBoxComPort.TabIndex = 0;
            this.cBoxComPort.Text = "COM5";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gBStatus);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Location = new System.Drawing.Point(12, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 163);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gBStatus
            // 
            this.gBStatus.Controls.Add(this.lblStatusCom);
            this.gBStatus.Location = new System.Drawing.Point(121, 19);
            this.gBStatus.Name = "gBStatus";
            this.gBStatus.Size = new System.Drawing.Size(157, 100);
            this.gBStatus.TabIndex = 3;
            this.gBStatus.TabStop = false;
            this.gBStatus.Text = "COM PORT STATUS";
            // 
            // lblStatusCom
            // 
            this.lblStatusCom.AutoSize = true;
            this.lblStatusCom.Font = new System.Drawing.Font("Britannic Bold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusCom.Location = new System.Drawing.Point(52, 41);
            this.lblStatusCom.Name = "lblStatusCom";
            this.lblStatusCom.Size = new System.Drawing.Size(48, 25);
            this.lblStatusCom.TabIndex = 0;
            this.lblStatusCom.Text = "OFF";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 125);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(257, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(21, 72);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 47);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(21, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(89, 47);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // btnSendData
            // 
            this.btnSendData.Enabled = false;
            this.btnSendData.Location = new System.Drawing.Point(121, 19);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(82, 71);
            this.btnSendData.TabIndex = 2;
            this.btnSendData.Text = "Start Recording";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.BtnSendData_Click);
            // 
            // tBoxDataOut
            // 
            this.tBoxDataOut.Location = new System.Drawing.Point(6, 19);
            this.tBoxDataOut.Multiline = true;
            this.tBoxDataOut.Name = "tBoxDataOut";
            this.tBoxDataOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxDataOut.Size = new System.Drawing.Size(350, 168);
            this.tBoxDataOut.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.tBoxDataIn);
            this.groupBox3.Location = new System.Drawing.Point(319, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 375);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transmitter Control";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSaveData);
            this.groupBox4.Controls.Add(this.btnCheckCon);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.btnSendData);
            this.groupBox4.Location = new System.Drawing.Point(6, 193);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(323, 176);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // btnCheckCon
            // 
            this.btnCheckCon.Location = new System.Drawing.Point(20, 19);
            this.btnCheckCon.Name = "btnCheckCon";
            this.btnCheckCon.Size = new System.Drawing.Size(82, 71);
            this.btnCheckCon.TabIndex = 8;
            this.btnCheckCon.Text = "Check Connection";
            this.btnCheckCon.UseVisualStyleBackColor = true;
            this.btnCheckCon.Click += new System.EventHandler(this.BtnCheckCon_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.lblDataOutLength);
            this.groupBox7.Location = new System.Drawing.Point(6, 105);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(303, 43);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Data OUT Length :";
            // 
            // lblDataOutLength
            // 
            this.lblDataOutLength.AutoSize = true;
            this.lblDataOutLength.Location = new System.Drawing.Point(189, 18);
            this.lblDataOutLength.Name = "lblDataOutLength";
            this.lblDataOutLength.Size = new System.Drawing.Size(19, 13);
            this.lblDataOutLength.TabIndex = 6;
            this.lblDataOutLength.Text = "00";
            // 
            // tBoxDataIn
            // 
            this.tBoxDataIn.Location = new System.Drawing.Point(6, 19);
            this.tBoxDataIn.Multiline = true;
            this.tBoxDataIn.Name = "tBoxDataIn";
            this.tBoxDataIn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxDataIn.Size = new System.Drawing.Size(323, 172);
            this.tBoxDataIn.TabIndex = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tBoxDataOut);
            this.groupBox9.Location = new System.Drawing.Point(660, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(362, 201);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Reciever Control";
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(223, 19);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(82, 71);
            this.btnSaveData.TabIndex = 9;
            this.btnSaveData.Text = "Save Data to File";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // ComPortControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 405);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ComPortControl";
            this.Text = "C# COM PORT SERIAL";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gBStatus.ResumeLayout(false);
            this.gBStatus.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBoxParityBits;
        private System.Windows.Forms.ComboBox cBoxStopBits;
        private System.Windows.Forms.ComboBox cBoxDataBits;
        private System.Windows.Forms.ComboBox cBoxBaudRate;
        private System.Windows.Forms.ComboBox cBoxComPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.TextBox tBoxDataOut;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.CheckBox chBoxRtsEnable;
        private System.Windows.Forms.CheckBox chBoxDtrEnable;
        private System.Windows.Forms.GroupBox gBStatus;
        private System.Windows.Forms.Label lblStatusCom;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDataOutLength;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox tBoxDataIn;
        private System.Windows.Forms.Button btnCheckCon;
        private System.Windows.Forms.Button btnSaveData;
    }
}

