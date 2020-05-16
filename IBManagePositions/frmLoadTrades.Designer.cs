namespace IBManagePositions
{
    partial class frmLoadTrades
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
            this.cbOpenTradesOnly = new System.Windows.Forms.CheckBox();
            this.dtpLoadTradeStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpLoadTradeEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLoadTradeStartsWith = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadOk = new System.Windows.Forms.Button();
            this.cbUseDateRange = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // cbOpenTradesOnly
            // 
            this.cbOpenTradesOnly.AutoSize = true;
            this.cbOpenTradesOnly.Checked = true;
            this.cbOpenTradesOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenTradesOnly.Location = new System.Drawing.Point(83, 24);
            this.cbOpenTradesOnly.Name = "cbOpenTradesOnly";
            this.cbOpenTradesOnly.Size = new System.Drawing.Size(112, 17);
            this.cbOpenTradesOnly.TabIndex = 0;
            this.cbOpenTradesOnly.Text = "Open Trades Only";
            this.cbOpenTradesOnly.UseVisualStyleBackColor = true;
            this.cbOpenTradesOnly.CheckedChanged += new System.EventHandler(this.cbOpenTradesOnly_CheckedChanged);
            // 
            // dtpLoadTradeStartDate
            // 
            this.dtpLoadTradeStartDate.Enabled = false;
            this.dtpLoadTradeStartDate.Location = new System.Drawing.Point(83, 125);
            this.dtpLoadTradeStartDate.Name = "dtpLoadTradeStartDate";
            this.dtpLoadTradeStartDate.Size = new System.Drawing.Size(147, 20);
            this.dtpLoadTradeStartDate.TabIndex = 1;
            // 
            // dtpLoadTradeEndDate
            // 
            this.dtpLoadTradeEndDate.Enabled = false;
            this.dtpLoadTradeEndDate.Location = new System.Drawing.Point(83, 151);
            this.dtpLoadTradeEndDate.Name = "dtpLoadTradeEndDate";
            this.dtpLoadTradeEndDate.Size = new System.Drawing.Size(147, 20);
            this.dtpLoadTradeEndDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Date:";
            // 
            // tbLoadTradeStartsWith
            // 
            this.tbLoadTradeStartsWith.Enabled = false;
            this.tbLoadTradeStartsWith.Location = new System.Drawing.Point(83, 54);
            this.tbLoadTradeStartsWith.Name = "tbLoadTradeStartsWith";
            this.tbLoadTradeStartsWith.Size = new System.Drawing.Size(43, 20);
            this.tbLoadTradeStartsWith.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Starts with:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnLoadOk
            // 
            this.btnLoadOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLoadOk.Location = new System.Drawing.Point(83, 207);
            this.btnLoadOk.Name = "btnLoadOk";
            this.btnLoadOk.Size = new System.Drawing.Size(75, 23);
            this.btnLoadOk.TabIndex = 7;
            this.btnLoadOk.Text = "Ok";
            this.btnLoadOk.UseVisualStyleBackColor = true;
            // 
            // cbUseDateRange
            // 
            this.cbUseDateRange.AutoSize = true;
            this.cbUseDateRange.Location = new System.Drawing.Point(83, 100);
            this.cbUseDateRange.Name = "cbUseDateRange";
            this.cbUseDateRange.Size = new System.Drawing.Size(106, 17);
            this.cbUseDateRange.TabIndex = 8;
            this.cbUseDateRange.Text = "Use Date Range";
            this.cbUseDateRange.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 101);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // frmLoadTrades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 241);
            this.Controls.Add(this.cbUseDateRange);
            this.Controls.Add(this.btnLoadOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLoadTradeStartsWith);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpLoadTradeEndDate);
            this.Controls.Add(this.dtpLoadTradeStartDate);
            this.Controls.Add(this.cbOpenTradesOnly);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmLoadTrades";
            this.Text = "Load Trades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbOpenTradesOnly;
        private System.Windows.Forms.DateTimePicker dtpLoadTradeStartDate;
        private System.Windows.Forms.DateTimePicker dtpLoadTradeEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLoadTradeStartsWith;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadOk;
        private System.Windows.Forms.CheckBox cbUseDateRange;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}