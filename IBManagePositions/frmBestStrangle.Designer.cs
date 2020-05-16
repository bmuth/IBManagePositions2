namespace IBManagePositions
{
    partial class frmBestStrangle
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBsDaysToExpire = new System.Windows.Forms.TextBox();
            this.cbBsIncludeWeeklies = new System.Windows.Forms.CheckBox();
            this.dtpBsExpires = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBsTargetITM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBsTargetMargin = new System.Windows.Forms.TextBox();
            this.btnBsCancel = new System.Windows.Forms.Button();
            this.dgvBestStrangle = new System.Windows.Forms.DataGridView();
            this.Ticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UndPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfCall = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Strike = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IfBuy = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Delta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbITM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoContracts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BPR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBsAccept = new System.Windows.Forms.Button();
            this.btnbsEstimateCommissions = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbbsMaxCommission = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbbsInitMargin = new System.Windows.Forms.Label();
            this.lbbsMaintMargin = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbbsMinCommission = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestStrangle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Days to expire";
            // 
            // tbBsDaysToExpire
            // 
            this.tbBsDaysToExpire.Location = new System.Drawing.Point(95, 7);
            this.tbBsDaysToExpire.Name = "tbBsDaysToExpire";
            this.tbBsDaysToExpire.Size = new System.Drawing.Size(42, 20);
            this.tbBsDaysToExpire.TabIndex = 1;
            this.tbBsDaysToExpire.Leave += new System.EventHandler(this.tbBsDaysToExpire_TextChanged);
            // 
            // cbBsIncludeWeeklies
            // 
            this.cbBsIncludeWeeklies.AutoSize = true;
            this.cbBsIncludeWeeklies.Location = new System.Drawing.Point(16, 40);
            this.cbBsIncludeWeeklies.Name = "cbBsIncludeWeeklies";
            this.cbBsIncludeWeeklies.Size = new System.Drawing.Size(104, 17);
            this.cbBsIncludeWeeklies.TabIndex = 3;
            this.cbBsIncludeWeeklies.Text = "include weeklies";
            this.cbBsIncludeWeeklies.UseVisualStyleBackColor = true;
            this.cbBsIncludeWeeklies.CheckedChanged += new System.EventHandler(this.tbBsDaysToExpire_TextChanged);
            // 
            // dtpBsExpires
            // 
            this.dtpBsExpires.Location = new System.Drawing.Point(170, 7);
            this.dtpBsExpires.Name = "dtpBsExpires";
            this.dtpBsExpires.Size = new System.Drawing.Size(137, 20);
            this.dtpBsExpires.TabIndex = 2;
            this.dtpBsExpires.ValueChanged += new System.EventHandler(this.dtpBsExpires_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "target % ITM";
            // 
            // tbBsTargetITM
            // 
            this.tbBsTargetITM.Location = new System.Drawing.Point(199, 38);
            this.tbBsTargetITM.Name = "tbBsTargetITM";
            this.tbBsTargetITM.Size = new System.Drawing.Size(42, 20);
            this.tbBsTargetITM.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "target margin";
            // 
            // tbBsTargetMargin
            // 
            this.tbBsTargetMargin.Location = new System.Drawing.Point(329, 38);
            this.tbBsTargetMargin.Name = "tbBsTargetMargin";
            this.tbBsTargetMargin.Size = new System.Drawing.Size(87, 20);
            this.tbBsTargetMargin.TabIndex = 5;
            this.tbBsTargetMargin.Validating += new System.ComponentModel.CancelEventHandler(this.tbBsTargetMargin_Validating);
            this.tbBsTargetMargin.Validated += new System.EventHandler(this.tbBsTargetMargin_Validated);
            // 
            // btnBsCancel
            // 
            this.btnBsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBsCancel.Location = new System.Drawing.Point(329, 213);
            this.btnBsCancel.Name = "btnBsCancel";
            this.btnBsCancel.Size = new System.Drawing.Size(75, 23);
            this.btnBsCancel.TabIndex = 7;
            this.btnBsCancel.Text = "Cancel";
            this.btnBsCancel.UseVisualStyleBackColor = true;
            // 
            // dgvBestStrangle
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBestStrangle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvBestStrangle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBestStrangle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ticker,
            this.UndPrice,
            this.Expires,
            this.IfCall,
            this.Strike,
            this.IfBuy,
            this.Delta,
            this.Price,
            this.ProbITM,
            this.NoContracts,
            this.BPR});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBestStrangle.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvBestStrangle.Location = new System.Drawing.Point(16, 74);
            this.dgvBestStrangle.Name = "dgvBestStrangle";
            this.dgvBestStrangle.Size = new System.Drawing.Size(594, 113);
            this.dgvBestStrangle.TabIndex = 21;
            this.dgvBestStrangle.TabStop = false;
            this.dgvBestStrangle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBestStrangle_CellEndEdit);
            this.dgvBestStrangle.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvBestStrangle_ColumnWidthChanged);
            // 
            // Ticker
            // 
            this.Ticker.DataPropertyName = "Ticker";
            this.Ticker.HeaderText = "Ticker";
            this.Ticker.Name = "Ticker";
            this.Ticker.ReadOnly = true;
            // 
            // UndPrice
            // 
            this.UndPrice.DataPropertyName = "UndPrice";
            this.UndPrice.HeaderText = "Und Price";
            this.UndPrice.Name = "UndPrice";
            this.UndPrice.ReadOnly = true;
            // 
            // Expires
            // 
            this.Expires.DataPropertyName = "Expiry";
            this.Expires.HeaderText = "Expires";
            this.Expires.Name = "Expires";
            // 
            // IfCall
            // 
            this.IfCall.DataPropertyName = "IfCall";
            this.IfCall.HeaderText = "(C/P)";
            this.IfCall.Name = "IfCall";
            this.IfCall.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IfCall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Strike
            // 
            this.Strike.DataPropertyName = "Strike";
            this.Strike.HeaderText = "Strike";
            this.Strike.Name = "Strike";
            this.Strike.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Strike.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IfBuy
            // 
            this.IfBuy.DataPropertyName = "IfSell";
            this.IfBuy.HeaderText = "(B/S)";
            this.IfBuy.Name = "IfBuy";
            this.IfBuy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IfBuy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Delta
            // 
            this.Delta.DataPropertyName = "Delta";
            dataGridViewCellStyle13.Format = "N3";
            dataGridViewCellStyle13.NullValue = null;
            this.Delta.DefaultCellStyle = dataGridViewCellStyle13;
            this.Delta.HeaderText = "Delta";
            this.Delta.Name = "Delta";
            this.Delta.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle14.Format = "C2";
            dataGridViewCellStyle14.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle14;
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // ProbITM
            // 
            this.ProbITM.DataPropertyName = "ProbITM";
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.ProbITM.DefaultCellStyle = dataGridViewCellStyle15;
            this.ProbITM.HeaderText = "% ITM";
            this.ProbITM.Name = "ProbITM";
            this.ProbITM.ReadOnly = true;
            // 
            // NoContracts
            // 
            this.NoContracts.DataPropertyName = "NoContracts";
            this.NoContracts.HeaderText = "# contr.";
            this.NoContracts.Name = "NoContracts";
            // 
            // BPR
            // 
            this.BPR.DataPropertyName = "BuyingPowerReduction";
            dataGridViewCellStyle16.Format = "C0";
            dataGridViewCellStyle16.NullValue = null;
            this.BPR.DefaultCellStyle = dataGridViewCellStyle16;
            this.BPR.HeaderText = "BPR";
            this.BPR.Name = "BPR";
            this.BPR.ReadOnly = true;
            // 
            // btnBsAccept
            // 
            this.btnBsAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBsAccept.Location = new System.Drawing.Point(199, 213);
            this.btnBsAccept.Name = "btnBsAccept";
            this.btnBsAccept.Size = new System.Drawing.Size(75, 23);
            this.btnBsAccept.TabIndex = 6;
            this.btnBsAccept.Text = "Accept";
            this.btnBsAccept.UseVisualStyleBackColor = true;
            this.btnBsAccept.Click += new System.EventHandler(this.btnBsAccept_Click);
            // 
            // btnbsEstimateCommissions
            // 
            this.btnbsEstimateCommissions.Location = new System.Drawing.Point(619, 213);
            this.btnbsEstimateCommissions.Name = "btnbsEstimateCommissions";
            this.btnbsEstimateCommissions.Size = new System.Drawing.Size(149, 23);
            this.btnbsEstimateCommissions.TabIndex = 22;
            this.btnbsEstimateCommissions.Text = "Estimate Commissions";
            this.btnbsEstimateCommissions.UseVisualStyleBackColor = true;
            this.btnbsEstimateCommissions.Click += new System.EventHandler(this.btnbsEstimateCommissions_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(616, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Max. Commissions:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(653, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Init Margin:";
            // 
            // lbbsMaxCommission
            // 
            this.lbbsMaxCommission.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbsMaxCommission.Location = new System.Drawing.Point(714, 101);
            this.lbbsMaxCommission.Name = "lbbsMaxCommission";
            this.lbbsMaxCommission.Size = new System.Drawing.Size(65, 20);
            this.lbbsMaxCommission.TabIndex = 25;
            this.lbbsMaxCommission.Text = "$0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(638, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Maint. Margin:";
            // 
            // lbbsInitMargin
            // 
            this.lbbsInitMargin.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbsInitMargin.Location = new System.Drawing.Point(714, 132);
            this.lbbsInitMargin.Name = "lbbsInitMargin";
            this.lbbsInitMargin.Size = new System.Drawing.Size(65, 20);
            this.lbbsInitMargin.TabIndex = 27;
            this.lbbsInitMargin.Text = "$0";
            // 
            // lbbsMaintMargin
            // 
            this.lbbsMaintMargin.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbsMaintMargin.Location = new System.Drawing.Point(714, 162);
            this.lbbsMaintMargin.Name = "lbbsMaintMargin";
            this.lbbsMaintMargin.Size = new System.Drawing.Size(65, 20);
            this.lbbsMaintMargin.TabIndex = 28;
            this.lbbsMaintMargin.Text = "$0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(624, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Min. Commission:";
            // 
            // lbbsMinCommission
            // 
            this.lbbsMinCommission.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbsMinCommission.Location = new System.Drawing.Point(714, 72);
            this.lbbsMinCommission.Name = "lbbsMinCommission";
            this.lbbsMinCommission.Size = new System.Drawing.Size(65, 20);
            this.lbbsMinCommission.TabIndex = 30;
            this.lbbsMinCommission.Text = "$0";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Ticker";
            this.dataGridViewTextBoxColumn1.HeaderText = "Ticker";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Expiry";
            this.dataGridViewTextBoxColumn2.HeaderText = "Expires";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IfCall";
            this.dataGridViewTextBoxColumn3.HeaderText = "(C/P)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Strike";
            dataGridViewCellStyle18.Format = "N3";
            dataGridViewCellStyle18.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn4.HeaderText = "Strike";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IfSell";
            dataGridViewCellStyle19.Format = "N3";
            dataGridViewCellStyle19.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn5.HeaderText = "(B/S)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Delta";
            dataGridViewCellStyle20.Format = "C2";
            dataGridViewCellStyle20.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn6.HeaderText = "Delta";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ProbITM";
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn7.HeaderText = "% ITM";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NoContracts";
            dataGridViewCellStyle22.Format = "C0";
            dataGridViewCellStyle22.NullValue = null;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn8.HeaderText = "# contracts";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // frmBestStrangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 248);
            this.Controls.Add(this.lbbsMinCommission);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbbsMaintMargin);
            this.Controls.Add(this.lbbsInitMargin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbbsMaxCommission);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnbsEstimateCommissions);
            this.Controls.Add(this.btnBsAccept);
            this.Controls.Add(this.dgvBestStrangle);
            this.Controls.Add(this.btnBsCancel);
            this.Controls.Add(this.tbBsTargetMargin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBsTargetITM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpBsExpires);
            this.Controls.Add(this.cbBsIncludeWeeklies);
            this.Controls.Add(this.tbBsDaysToExpire);
            this.Controls.Add(this.label1);
            this.Name = "frmBestStrangle";
            this.Text = "frmBestStrangle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBestStrangle_FormClosing);
            this.Shown += new System.EventHandler(this.frmBestStrangle_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestStrangle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBsDaysToExpire;
        private System.Windows.Forms.CheckBox cbBsIncludeWeeklies;
        private System.Windows.Forms.DateTimePicker dtpBsExpires;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBsTargetITM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBsTargetMargin;
        private System.Windows.Forms.Button btnBsCancel;
        private System.Windows.Forms.DataGridView dgvBestStrangle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Button btnBsAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticker;
        private System.Windows.Forms.DataGridViewTextBoxColumn UndPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expires;
        private System.Windows.Forms.DataGridViewComboBoxColumn IfCall;
        private System.Windows.Forms.DataGridViewComboBoxColumn Strike;
        private System.Windows.Forms.DataGridViewComboBoxColumn IfBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Delta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbITM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoContracts;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPR;
        private System.Windows.Forms.Button btnbsEstimateCommissions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbbsMaxCommission;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbbsInitMargin;
        private System.Windows.Forms.Label lbbsMaintMargin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbbsMinCommission;

    }
}