namespace IBManagePositions
{
    partial class frmAddFuture
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
            this.tbFutureSymbol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFutureExchange = new System.Windows.Forms.TextBox();
            this.btnFuturesSearch = new System.Windows.Forms.Button();
            this.btnFuturesAdd = new System.Windows.Forms.Button();
            this.lbFutureName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFutureSymbol
            // 
            this.tbFutureSymbol.Location = new System.Drawing.Point(129, 23);
            this.tbFutureSymbol.Name = "tbFutureSymbol";
            this.tbFutureSymbol.Size = new System.Drawing.Size(65, 20);
            this.tbFutureSymbol.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Symbol:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Exchange:";
            // 
            // tbFutureExchange
            // 
            this.tbFutureExchange.Location = new System.Drawing.Point(129, 57);
            this.tbFutureExchange.Name = "tbFutureExchange";
            this.tbFutureExchange.Size = new System.Drawing.Size(65, 20);
            this.tbFutureExchange.TabIndex = 3;
            // 
            // btnFuturesSearch
            // 
            this.btnFuturesSearch.Location = new System.Drawing.Point(107, 94);
            this.btnFuturesSearch.Name = "btnFuturesSearch";
            this.btnFuturesSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFuturesSearch.TabIndex = 4;
            this.btnFuturesSearch.Text = "Search";
            this.btnFuturesSearch.UseVisualStyleBackColor = true;
            this.btnFuturesSearch.Click += new System.EventHandler(this.btnFuturesSearch_Click);
            // 
            // btnFuturesAdd
            // 
            this.btnFuturesAdd.Location = new System.Drawing.Point(107, 160);
            this.btnFuturesAdd.Name = "btnFuturesAdd";
            this.btnFuturesAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFuturesAdd.TabIndex = 6;
            this.btnFuturesAdd.Text = "Add";
            this.btnFuturesAdd.UseVisualStyleBackColor = true;
            this.btnFuturesAdd.Click += new System.EventHandler(this.btnFuturesAdd_Click);
            // 
            // lbFutureName
            // 
            this.lbFutureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFutureName.Location = new System.Drawing.Point(12, 127);
            this.lbFutureName.Name = "lbFutureName";
            this.lbFutureName.Size = new System.Drawing.Size(260, 23);
            this.lbFutureName.TabIndex = 7;
            this.lbFutureName.Text = "        ";
            this.lbFutureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddFuture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 197);
            this.Controls.Add(this.lbFutureName);
            this.Controls.Add(this.btnFuturesAdd);
            this.Controls.Add(this.btnFuturesSearch);
            this.Controls.Add(this.tbFutureExchange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFutureSymbol);
            this.Name = "frmAddFuture";
            this.Text = "Futures";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFutureSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFutureExchange;
        private System.Windows.Forms.Button btnFuturesSearch;
        private System.Windows.Forms.Button btnFuturesAdd;
        private System.Windows.Forms.Label lbFutureName;
    }
}