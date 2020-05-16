namespace IBManagePositions
{
    partial class frmCloseTrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCloseTrade = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfCall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strike = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfSell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contracts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbClAsk = new System.Windows.Forms.Label();
            this.lbClNat = new System.Windows.Forms.Label();
            this.lbClBid = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClPlaceOrder = new System.Windows.Forms.Button();
            this.tbClMyPrice = new System.Windows.Forms.TextBox();
            this.lbClLast = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbMissingRemotePosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloseTrade)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCloseTrade
            // 
            this.dgvCloseTrade.AllowUserToAddRows = false;
            this.dgvCloseTrade.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvCloseTrade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCloseTrade.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCloseTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCloseTrade.ColumnHeadersVisible = false;
            this.dgvCloseTrade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Ticker,
            this.Expires,
            this.IfCall,
            this.Strike,
            this.IfSell,
            this.Contracts});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCloseTrade.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCloseTrade.Location = new System.Drawing.Point(12, 35);
            this.dgvCloseTrade.MultiSelect = false;
            this.dgvCloseTrade.Name = "dgvCloseTrade";
            this.dgvCloseTrade.ReadOnly = true;
            this.dgvCloseTrade.RowHeadersVisible = false;
            this.dgvCloseTrade.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCloseTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCloseTrade.Size = new System.Drawing.Size(305, 84);
            this.dgvCloseTrade.TabIndex = 0;
            this.dgvCloseTrade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCloseTrade_CellFormatting);
            this.dgvCloseTrade.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvCloseTrade_ColumnWidthChanged);
            this.dgvCloseTrade.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvCloseTrade_UserDeletedRow);
            this.dgvCloseTrade.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvCloseTrade_UserDeletingRow);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Ticker
            // 
            this.Ticker.DataPropertyName = "Ticker";
            this.Ticker.HeaderText = "Ticker";
            this.Ticker.Name = "Ticker";
            this.Ticker.ReadOnly = true;
            this.Ticker.Width = 50;
            // 
            // Expires
            // 
            this.Expires.DataPropertyName = "ExpiryDate";
            this.Expires.HeaderText = "Expires";
            this.Expires.Name = "Expires";
            this.Expires.ReadOnly = true;
            this.Expires.Width = 50;
            // 
            // IfCall
            // 
            this.IfCall.DataPropertyName = "IfCall";
            this.IfCall.HeaderText = "(C/P)";
            this.IfCall.Name = "IfCall";
            this.IfCall.ReadOnly = true;
            this.IfCall.Width = 50;
            // 
            // Strike
            // 
            this.Strike.DataPropertyName = "Strike";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.NullValue = null;
            this.Strike.DefaultCellStyle = dataGridViewCellStyle1;
            this.Strike.HeaderText = "Strike";
            this.Strike.Name = "Strike";
            this.Strike.ReadOnly = true;
            this.Strike.Width = 50;
            // 
            // IfSell
            // 
            this.IfSell.DataPropertyName = "IfSell";
            this.IfSell.HeaderText = "(B/S)";
            this.IfSell.Name = "IfSell";
            this.IfSell.ReadOnly = true;
            this.IfSell.Width = 50;
            // 
            // Contracts
            // 
            this.Contracts.DataPropertyName = "NoContracts";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.Contracts.DefaultCellStyle = dataGridViewCellStyle2;
            this.Contracts.HeaderText = "# contracts";
            this.Contracts.Name = "Contracts";
            this.Contracts.ReadOnly = true;
            this.Contracts.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ask";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nat";
            // 
            // lbClAsk
            // 
            this.lbClAsk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClAsk.Location = new System.Drawing.Point(356, 38);
            this.lbClAsk.Name = "lbClAsk";
            this.lbClAsk.Size = new System.Drawing.Size(51, 16);
            this.lbClAsk.TabIndex = 4;
            this.lbClAsk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbClNat
            // 
            this.lbClNat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClNat.Location = new System.Drawing.Point(353, 63);
            this.lbClNat.Name = "lbClNat";
            this.lbClNat.Size = new System.Drawing.Size(54, 16);
            this.lbClNat.TabIndex = 5;
            this.lbClNat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbClBid
            // 
            this.lbClBid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClBid.Location = new System.Drawing.Point(353, 87);
            this.lbClBid.Name = "lbClBid";
            this.lbClBid.Size = new System.Drawing.Size(54, 16);
            this.lbClBid.TabIndex = 6;
            this.lbClBid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(150, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnClPlaceOrder
            // 
            this.btnClPlaceOrder.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClPlaceOrder.Location = new System.Drawing.Point(254, 157);
            this.btnClPlaceOrder.Name = "btnClPlaceOrder";
            this.btnClPlaceOrder.Size = new System.Drawing.Size(75, 23);
            this.btnClPlaceOrder.TabIndex = 8;
            this.btnClPlaceOrder.Text = "Place Order";
            this.btnClPlaceOrder.UseVisualStyleBackColor = true;
            this.btnClPlaceOrder.Click += new System.EventHandler(this.btnClPlaceOrder_Click);
            // 
            // tbClMyPrice
            // 
            this.tbClMyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClMyPrice.Location = new System.Drawing.Point(352, 114);
            this.tbClMyPrice.Name = "tbClMyPrice";
            this.tbClMyPrice.Size = new System.Drawing.Size(68, 22);
            this.tbClMyPrice.TabIndex = 9;
            this.tbClMyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbClLast
            // 
            this.lbClLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClLast.Location = new System.Drawing.Point(434, 63);
            this.lbClLast.Name = "lbClLast";
            this.lbClLast.Size = new System.Drawing.Size(46, 16);
            this.lbClLast.TabIndex = 11;
            this.lbClLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(408, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Last";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Ticker";
            this.dataGridViewTextBoxColumn2.HeaderText = "Ticker";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "bIfCall";
            this.dataGridViewTextBoxColumn3.HeaderText = "(C/P)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "bIfSell";
            this.dataGridViewTextBoxColumn4.HeaderText = "(B/S)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Strike";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Strike";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ExpiryDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "Expires";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "NoContracts";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn7.HeaderText = "# contracts";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // lbMissingRemotePosition
            // 
            this.lbMissingRemotePosition.AutoSize = true;
            this.lbMissingRemotePosition.Location = new System.Drawing.Point(15, 132);
            this.lbMissingRemotePosition.Name = "lbMissingRemotePosition";
            this.lbMissingRemotePosition.Size = new System.Drawing.Size(291, 13);
            this.lbMissingRemotePosition.TabIndex = 12;
            this.lbMissingRemotePosition.Text = "WARNING: There is a position not found with the brokerage";
            this.lbMissingRemotePosition.Visible = false;
            // 
            // frmCloseTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 192);
            this.Controls.Add(this.lbMissingRemotePosition);
            this.Controls.Add(this.lbClLast);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbClMyPrice);
            this.Controls.Add(this.btnClPlaceOrder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbClBid);
            this.Controls.Add(this.lbClNat);
            this.Controls.Add(this.lbClAsk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCloseTrade);
            this.Name = "frmCloseTrade";
            this.Text = "Close Trade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCloseTrade_FormClosing);
            this.Shown += new System.EventHandler(this.frmCloseTrade_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloseTrade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCloseTrade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbClAsk;
        private System.Windows.Forms.Label lbClNat;
        private System.Windows.Forms.Label lbClBid;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClPlaceOrder;
        private System.Windows.Forms.TextBox tbClMyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Label lbClLast;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expires;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfCall;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strike;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contracts;
        private System.Windows.Forms.Label lbMissingRemotePosition;
    }
}