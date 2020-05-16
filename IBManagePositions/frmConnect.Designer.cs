namespace IBManagePositions
{
    partial class frmConnect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbClientId = new System.Windows.Forms.TextBox();
            this.rbIBGateway = new System.Windows.Forms.RadioButton();
            this.rbTWS = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDataCore = new System.Windows.Forms.RadioButton();
            this.rbDataTest = new System.Windows.Forms.RadioButton();
            this.rbDataTDI = new System.Windows.Forms.RadioButton();
            this.rbDataIB = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConnect.Location = new System.Drawing.Point(87, 248);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Client Id";
            // 
            // tbClientId
            // 
            this.tbClientId.Location = new System.Drawing.Point(135, 67);
            this.tbClientId.Name = "tbClientId";
            this.tbClientId.Size = new System.Drawing.Size(34, 20);
            this.tbClientId.TabIndex = 7;
            this.tbClientId.Text = "5";
            // 
            // rbIBGateway
            // 
            this.rbIBGateway.Checked = true;
            this.rbIBGateway.Location = new System.Drawing.Point(53, 44);
            this.rbIBGateway.Name = "rbIBGateway";
            this.rbIBGateway.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbIBGateway.Size = new System.Drawing.Size(116, 17);
            this.rbIBGateway.TabIndex = 6;
            this.rbIBGateway.TabStop = true;
            this.rbIBGateway.Text = "IB Gateway";
            this.rbIBGateway.UseVisualStyleBackColor = true;
            // 
            // rbTWS
            // 
            this.rbTWS.AutoSize = true;
            this.rbTWS.Location = new System.Drawing.Point(53, 21);
            this.rbTWS.Name = "rbTWS";
            this.rbTWS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbTWS.Size = new System.Drawing.Size(116, 17);
            this.rbTWS.TabIndex = 5;
            this.rbTWS.Text = "Trader Workstation";
            this.rbTWS.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDataCore);
            this.groupBox1.Controls.Add(this.rbDataTest);
            this.groupBox1.Controls.Add(this.rbDataTDI);
            this.groupBox1.Controls.Add(this.rbDataIB);
            this.groupBox1.Location = new System.Drawing.Point(23, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 118);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // rbDataCore
            // 
            this.rbDataCore.AutoSize = true;
            this.rbDataCore.Location = new System.Drawing.Point(45, 69);
            this.rbDataCore.Name = "rbDataCore";
            this.rbDataCore.Size = new System.Drawing.Size(47, 17);
            this.rbDataCore.TabIndex = 3;
            this.rbDataCore.Text = "Core";
            this.rbDataCore.UseVisualStyleBackColor = true;
            this.rbDataCore.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // rbDataTest
            // 
            this.rbDataTest.AutoSize = true;
            this.rbDataTest.Location = new System.Drawing.Point(45, 94);
            this.rbDataTest.Name = "rbDataTest";
            this.rbDataTest.Size = new System.Drawing.Size(46, 17);
            this.rbDataTest.TabIndex = 2;
            this.rbDataTest.Text = "Test";
            this.rbDataTest.UseVisualStyleBackColor = true;
            this.rbDataTest.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // rbDataTDI
            // 
            this.rbDataTDI.AutoSize = true;
            this.rbDataTDI.Location = new System.Drawing.Point(45, 44);
            this.rbDataTDI.Name = "rbDataTDI";
            this.rbDataTDI.Size = new System.Drawing.Size(117, 17);
            this.rbDataTDI.TabIndex = 1;
            this.rbDataTDI.Text = "TD Direct Investing";
            this.rbDataTDI.UseVisualStyleBackColor = true;
            this.rbDataTDI.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // rbDataIB
            // 
            this.rbDataIB.AutoSize = true;
            this.rbDataIB.Checked = true;
            this.rbDataIB.Location = new System.Drawing.Point(45, 19);
            this.rbDataIB.Name = "rbDataIB";
            this.rbDataIB.Size = new System.Drawing.Size(114, 17);
            this.rbDataIB.TabIndex = 0;
            this.rbDataIB.TabStop = true;
            this.rbDataIB.Text = "Interactive Brokers";
            this.rbDataIB.UseVisualStyleBackColor = true;
            this.rbDataIB.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // frmConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 283);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbClientId);
            this.Controls.Add(this.rbIBGateway);
            this.Controls.Add(this.rbTWS);
            this.Name = "frmConnect";
            this.Text = "Connect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConnect_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbClientId;
        private System.Windows.Forms.RadioButton rbIBGateway;
        private System.Windows.Forms.RadioButton rbTWS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDataTDI;
        private System.Windows.Forms.RadioButton rbDataIB;
        private System.Windows.Forms.RadioButton rbDataTest;
        private System.Windows.Forms.RadioButton rbDataCore;
    }
}