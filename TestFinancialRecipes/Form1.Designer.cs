namespace TestFinancialRecipes
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbStockPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.rbSideCall = new System.Windows.Forms.RadioButton();
            this.rbSidePut = new System.Windows.Forms.RadioButton();
            this.tbStrike = new System.Windows.Forms.TextBox();
            this.tbInterestRate = new System.Windows.Forms.TextBox();
            this.tbOptionPrice = new System.Windows.Forms.TextBox();
            this.btnPut = new System.Windows.Forms.Button();
            this.Inputs = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbIV = new System.Windows.Forms.Label();
            this.lbDelta = new System.Windows.Forms.Label();
            this.lbGamma = new System.Windows.Forms.Label();
            this.lbTheta = new System.Windows.Forms.Label();
            this.lbVega = new System.Windows.Forms.Label();
            this.lbRho = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stock Price:";
            // 
            // tbStockPrice
            // 
            this.tbStockPrice.Location = new System.Drawing.Point(119, 30);
            this.tbStockPrice.Name = "tbStockPrice";
            this.tbStockPrice.Size = new System.Drawing.Size(49, 20);
            this.tbStockPrice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Strike:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Interest Rate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Expiration:";
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Location = new System.Drawing.Point(119, 110);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(140, 20);
            this.dtpExpiration.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Option Price:";
            // 
            // rbSideCall
            // 
            this.rbSideCall.AutoSize = true;
            this.rbSideCall.Checked = true;
            this.rbSideCall.Location = new System.Drawing.Point(119, 168);
            this.rbSideCall.Name = "rbSideCall";
            this.rbSideCall.Size = new System.Drawing.Size(42, 17);
            this.rbSideCall.TabIndex = 6;
            this.rbSideCall.TabStop = true;
            this.rbSideCall.Text = "Call";
            this.rbSideCall.UseVisualStyleBackColor = true;
            // 
            // rbSidePut
            // 
            this.rbSidePut.AutoSize = true;
            this.rbSidePut.Location = new System.Drawing.Point(119, 191);
            this.rbSidePut.Name = "rbSidePut";
            this.rbSidePut.Size = new System.Drawing.Size(41, 17);
            this.rbSidePut.TabIndex = 7;
            this.rbSidePut.Text = "Put";
            this.rbSidePut.UseVisualStyleBackColor = true;
            // 
            // tbStrike
            // 
            this.tbStrike.Location = new System.Drawing.Point(119, 56);
            this.tbStrike.Name = "tbStrike";
            this.tbStrike.Size = new System.Drawing.Size(49, 20);
            this.tbStrike.TabIndex = 2;
            // 
            // tbInterestRate
            // 
            this.tbInterestRate.Location = new System.Drawing.Point(119, 83);
            this.tbInterestRate.Name = "tbInterestRate";
            this.tbInterestRate.Size = new System.Drawing.Size(49, 20);
            this.tbInterestRate.TabIndex = 3;
            // 
            // tbOptionPrice
            // 
            this.tbOptionPrice.Location = new System.Drawing.Point(119, 137);
            this.tbOptionPrice.Name = "tbOptionPrice";
            this.tbOptionPrice.Size = new System.Drawing.Size(49, 20);
            this.tbOptionPrice.TabIndex = 5;
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(104, 241);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(75, 23);
            this.btnPut.TabIndex = 8;
            this.btnPut.Text = "Compute";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // Inputs
            // 
            this.Inputs.Location = new System.Drawing.Point(13, 13);
            this.Inputs.Name = "Inputs";
            this.Inputs.Size = new System.Drawing.Size(259, 211);
            this.Inputs.TabIndex = 13;
            this.Inputs.TabStop = false;
            this.Inputs.Text = "Inputs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "IV:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "delta:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(89, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "gamma:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 360);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "theta:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(99, 386);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "vega:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(108, 412);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "rho:";
            // 
            // lbIV
            // 
            this.lbIV.AutoSize = true;
            this.lbIV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIV.Location = new System.Drawing.Point(137, 281);
            this.lbIV.Name = "lbIV";
            this.lbIV.Size = new System.Drawing.Size(47, 15);
            this.lbIV.TabIndex = 20;
            this.lbIV.Text = "30.2%";
            // 
            // lbDelta
            // 
            this.lbDelta.AutoSize = true;
            this.lbDelta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDelta.Location = new System.Drawing.Point(137, 306);
            this.lbDelta.Name = "lbDelta";
            this.lbDelta.Size = new System.Drawing.Size(47, 15);
            this.lbDelta.TabIndex = 21;
            this.lbDelta.Text = "30.2%";
            // 
            // lbGamma
            // 
            this.lbGamma.AutoSize = true;
            this.lbGamma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGamma.Location = new System.Drawing.Point(137, 332);
            this.lbGamma.Name = "lbGamma";
            this.lbGamma.Size = new System.Drawing.Size(47, 15);
            this.lbGamma.TabIndex = 22;
            this.lbGamma.Text = "30.2%";
            // 
            // lbTheta
            // 
            this.lbTheta.AutoSize = true;
            this.lbTheta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTheta.Location = new System.Drawing.Point(137, 358);
            this.lbTheta.Name = "lbTheta";
            this.lbTheta.Size = new System.Drawing.Size(47, 15);
            this.lbTheta.TabIndex = 23;
            this.lbTheta.Text = "30.2%";
            // 
            // lbVega
            // 
            this.lbVega.AutoSize = true;
            this.lbVega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVega.Location = new System.Drawing.Point(137, 384);
            this.lbVega.Name = "lbVega";
            this.lbVega.Size = new System.Drawing.Size(47, 15);
            this.lbVega.TabIndex = 24;
            this.lbVega.Text = "30.2%";
            // 
            // lbRho
            // 
            this.lbRho.AutoSize = true;
            this.lbRho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRho.Location = new System.Drawing.Point(137, 410);
            this.lbRho.Name = "lbRho";
            this.lbRho.Size = new System.Drawing.Size(47, 15);
            this.lbRho.TabIndex = 25;
            this.lbRho.Text = "30.2%";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 447);
            this.Controls.Add(this.lbRho);
            this.Controls.Add(this.lbVega);
            this.Controls.Add(this.lbTheta);
            this.Controls.Add(this.lbGamma);
            this.Controls.Add(this.lbDelta);
            this.Controls.Add(this.lbIV);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPut);
            this.Controls.Add(this.tbOptionPrice);
            this.Controls.Add(this.tbInterestRate);
            this.Controls.Add(this.tbStrike);
            this.Controls.Add(this.rbSidePut);
            this.Controls.Add(this.rbSideCall);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbStockPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Inputs);
            this.Name = "Form1";
            this.Text = "IV Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStockPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbSideCall;
        private System.Windows.Forms.RadioButton rbSidePut;
        private System.Windows.Forms.TextBox tbStrike;
        private System.Windows.Forms.TextBox tbInterestRate;
        private System.Windows.Forms.TextBox tbOptionPrice;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.GroupBox Inputs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbIV;
        private System.Windows.Forms.Label lbDelta;
        private System.Windows.Forms.Label lbGamma;
        private System.Windows.Forms.Label lbTheta;
        private System.Windows.Forms.Label lbVega;
        private System.Windows.Forms.Label lbRho;
    }
}

