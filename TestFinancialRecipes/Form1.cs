using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFinancialRecipes
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent ();
        }

        private void btnPut_Click (object sender, EventArgs e)
        {
            double Stock = 0;
            double Strike = 0;
            double rate = 0;
            int DaysLeft = 0;
            double option = 0;
            bool bIfCall = rbSideCall.Checked;

            if (!double.TryParse (tbStockPrice.Text, out Stock))
            {
                MessageBox.Show ("Bad stock price");
                return;
            }
            if (!double.TryParse (tbStrike.Text, out Strike))
            {
                MessageBox.Show ("Bad strike price");
                return;
            }
            try
            {
                rate = Utils.PercentageParse (tbInterestRate.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show (string.Format ("Bad interest rate. {0}", ex.Message));
                return;
            }
            TimeSpan ToExpiry = dtpExpiration.Value - DateTime.Today;
            DaysLeft = ToExpiry.Days;
            if (!double.TryParse (tbOptionPrice.Text, out option))
            {
                MessageBox.Show ("Bad option  price");
                return;
            }

            double iv;
            double delta = 0;
            double gamma = 0;
            double theta = 0;
            double vega = 0;
            double rho = 0;

            if (bIfCall)
            {
                iv = FinancialRecipes.FR.OptionPriceImpliedVolatilityCallBlackScholes (Stock, Strike, rate, DaysLeft, option);
                FinancialRecipes.FR.OptionPricePartialsCallBlackScholes (Stock, Strike, rate, iv, DaysLeft, out delta, out gamma, out theta, out vega, out rho);
            }
            else
            {
                iv = FinancialRecipes.FR.OptionPriceImpliedVolatilityPutBlackScholes (Stock, Strike, rate, DaysLeft, option);
                FinancialRecipes.FR.OptionPricePartialsPutBlackScholes (Stock, Strike, rate, iv, DaysLeft, out delta, out gamma, out theta, out vega, out rho);
            }

            lbIV.Text = string.Format ("{0:N3}%", iv * 100.0);
            lbDelta.Text = delta.ToString ("N4");
            lbGamma.Text = gamma.ToString ("N4");
            lbTheta.Text = theta.ToString ("N4");
            lbVega.Text = vega.ToString ("N4");
            lbRho.Text = rho.ToString ("N4");
        }
    }
}
