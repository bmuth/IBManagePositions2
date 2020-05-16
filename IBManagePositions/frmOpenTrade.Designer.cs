namespace IBManagePositions
{
    partial class frmOpenTrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOpenTrade = new System.Windows.Forms.DataGridView();
            this.tbOpMyPrice = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpPlaceOrder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbOpAsk = new System.Windows.Forms.Label();
            this.lbOpNat = new System.Windows.Forms.Label();
            this.lbOpBid = new System.Windows.Forms.Label();
            this.lbOpLast = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfCall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strike = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfSell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contracts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenTrade)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOpenTrade
            // 
            this.dgvOpenTrade.AllowUserToAddRows = false;
            this.dgvOpenTrade.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvOpenTrade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOpenTrade.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvOpenTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenTrade.ColumnHeadersVisible = false;
            this.dgvOpenTrade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.dgvOpenTrade.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOpenTrade.Location = new System.Drawing.Point(23, 31);
            this.dgvOpenTrade.MultiSelect = false;
            this.dgvOpenTrade.Name = "dgvOpenTrade";
            this.dgvOpenTrade.ReadOnly = true;
            this.dgvOpenTrade.RowHeadersVisible = false;
            this.dgvOpenTrade.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOpenTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOpenTrade.Size = new System.Drawing.Size(305, 84);
            this.dgvOpenTrade.TabIndex = 1;
            this.dgvOpenTrade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOpenTrade_CellFormatting);
            this.dgvOpenTrade.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvOpenTrade_ColumnWidthChanged);
            this.dgvOpenTrade.SelectionChanged += new System.EventHandler(this.dgvOpenTrade_SelectionChanged);
            // 
            // tbOpMyPrice
            // 
            this.tbOpMyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOpMyPrice.Location = new System.Drawing.Point(378, 120);
            this.tbOpMyPrice.Name = "tbOpMyPrice";
            this.tbOpMyPrice.Size = new System.Drawing.Size(68, 22);
            this.tbOpMyPrice.TabIndex = 10;
            this.tbOpMyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(143, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpPlaceOrder
            // 
            this.btnOpPlaceOrder.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpPlaceOrder.Location = new System.Drawing.Point(253, 169);
            this.btnOpPlaceOrder.Name = "btnOpPlaceOrder";
            this.btnOpPlaceOrder.Size = new System.Drawing.Size(75, 23);
            this.btnOpPlaceOrder.TabIndex = 12;
            this.btnOpPlaceOrder.Text = "Place Order";
            this.btnOpPlaceOrder.UseVisualStyleBackColor = true;
            this.btnOpPlaceOrder.Click += new System.EventHandler(this.btnOpPlaceOrder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ask";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Nat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Bid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Last";
            // 
            // lbOpAsk
            // 
            this.lbOpAsk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpAsk.Location = new System.Drawing.Point(375, 40);
            this.lbOpAsk.Name = "lbOpAsk";
            this.lbOpAsk.Size = new System.Drawing.Size(48, 16);
            this.lbOpAsk.TabIndex = 17;
            this.lbOpAsk.Text = "0";
            this.lbOpAsk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbOpNat
            // 
            this.lbOpNat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpNat.Location = new System.Drawing.Point(375, 66);
            this.lbOpNat.Name = "lbOpNat";
            this.lbOpNat.Size = new System.Drawing.Size(48, 16);
            this.lbOpNat.TabIndex = 18;
            this.lbOpNat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbOpBid
            // 
            this.lbOpBid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpBid.Location = new System.Drawing.Point(375, 92);
            this.lbOpBid.Name = "lbOpBid";
            this.lbOpBid.Size = new System.Drawing.Size(48, 16);
            this.lbOpBid.TabIndex = 19;
            this.lbOpBid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbOpLast
            // 
            this.lbOpLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOpLast.Location = new System.Drawing.Point(455, 69);
            this.lbOpLast.Name = "lbOpLast";
            this.lbOpLast.Size = new System.Drawing.Size(46, 16);
            this.lbOpLast.TabIndex = 20;
            this.lbOpLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Ticker";
            this.dataGridViewTextBoxColumn2.HeaderText = "Ticker";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ExpiryDate";
            this.dataGridViewTextBoxColumn3.HeaderText = "Expires";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "IfCall";
            this.dataGridViewTextBoxColumn4.HeaderText = "(C/P)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
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
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "NoContracts";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "# contracts";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "IfSell";
            this.dataGridViewTextBoxColumn7.HeaderText = "(B/S)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 50;
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
            // frmOpenTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 204);
            this.Controls.Add(this.lbOpLast);
            this.Controls.Add(this.lbOpBid);
            this.Controls.Add(this.lbOpNat);
            this.Controls.Add(this.lbOpAsk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpPlaceOrder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbOpMyPrice);
            this.Controls.Add(this.dgvOpenTrade);
            this.Name = "frmOpenTrade";
            this.Text = "Open Trade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOpenTrade_FormClosing);
            this.Shown += new System.EventHandler(this.frmOpenTrade_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenTrade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOpenTrade;
        private System.Windows.Forms.TextBox tbOpMyPrice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOpPlaceOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbOpAsk;
        private System.Windows.Forms.Label lbOpNat;
        private System.Windows.Forms.Label lbOpBid;
        private System.Windows.Forms.Label lbOpLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expires;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfCall;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strike;
        private System.Windows.Forms.DataGridViewTextBoxColumn IfSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contracts;
    }
}