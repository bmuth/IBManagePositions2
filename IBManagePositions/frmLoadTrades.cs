using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBManagePositions
{
    public partial class frmLoadTrades : Form
    {
        public bool bOpenOnly
        {
            get
            {
                return cbOpenTradesOnly.Checked;
            }
        }

        public DateTime dtStartDate
        {
            get
            {
                return dtpLoadTradeStartDate.Value;
            }
        }

        public bool bUseDateRange
        {
            get
            {
                return cbUseDateRange.Checked;
            }
        }

        public DateTime dtEndDate
        {
            get
            {
                return dtpLoadTradeEndDate.Value;
            }
        }

        public string szStartsWith
        {
            get
            {
                return tbLoadTradeStartsWith.Text;
            }
        }

        public frmLoadTrades ()
        {
            InitializeComponent ();
            DateTime now = DateTime.Now;
            dtpLoadTradeEndDate.Value = now;
            dtpLoadTradeStartDate.Value = now - new TimeSpan (45, 0, 0, 0);
        }
 
        private void cbOpenTradesOnly_CheckedChanged (object sender, EventArgs e)
        {
            if (cbOpenTradesOnly.Checked)
            {
                dtpLoadTradeStartDate.Enabled = false;
                dtpLoadTradeEndDate.Enabled = false;
                tbLoadTradeStartsWith.Enabled = false;
                cbUseDateRange.Enabled = false;
            }
            else
            {
                dtpLoadTradeStartDate.Enabled = true;
                dtpLoadTradeEndDate.Enabled = true;
                tbLoadTradeStartsWith.Enabled = true;
                cbUseDateRange.Enabled = true;
            }
        }

        private void label3_Click (object sender, EventArgs e)
        {

        }

 
    }
}
