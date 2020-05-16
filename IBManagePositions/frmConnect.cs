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
    public partial class frmConnect : Form
    {
        private int port;
        private int clientId;

        public frmConnect ()
        {
            InitializeComponent ();

            DataGroupName = "IB";
            if (Properties.Settings.Default.IfUsingIBGateway)
            {
                rbIBGateway.Checked = true;
            }
            else
            {
                rbTWS.Checked = true;
            }

            if (Properties.Settings.Default.IfUsingInteractiveBrokers)
            {
                rbDataIB.Checked = true;
            }
            else if (Properties.Settings.Default.IfUsingTDI)
            {
                rbDataTDI.Checked = true;
            }
            else if (Properties.Settings.Default.IfUsingCore)
            {
                rbDataCore.Checked = true;
            }
            else
            {
                rbDataTest.Checked = true;
            }
            tbClientId.Text = Properties.Settings.Default.ClientID.ToString ();
        }

        public int Port 
        { 
            get
            {
                return port;
            }
        }

        public int ClientId
        {
            get
            {
                return clientId;
            }
        }

        public string DataGroupName { get; set; }

        private void btnConnect_Click (object sender, EventArgs e)
        {
            port = 7496;
            if (!rbTWS.Checked)
            {
                port = 4001;
            }

            try
            {
                clientId = int.Parse (tbClientId.Text);
            }
            catch (Exception)
            {
                MessageBox.Show ("Invalid client id specified");
                btnConnect.DialogResult = DialogResult.Cancel;
            }

        }

        private void rbData_CheckedChanged (object sender, EventArgs e)
        {
            if (rbDataTDI.Checked)
            {
                DataGroupName = "TDI";
            }
            else if (rbDataIB.Checked)
            {
                DataGroupName = "IB";
            }
            else if (rbDataCore.Checked)
            {
                DataGroupName = "COR";
            }
            else
            {
                DataGroupName = "TST";
            }
        }

        private void frmConnect_FormClosing (object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IfUsingIBGateway = rbIBGateway.Checked;
            Properties.Settings.Default.IfUsingInteractiveBrokers = rbDataIB.Checked;
            Properties.Settings.Default.IfUsingTDI = rbDataTDI.Checked;
            Properties.Settings.Default.IfUsingCore = rbDataCore.Checked;
            int ClientID = 5;
            if (int.TryParse (tbClientId.Text, out ClientID))
            {
                Properties.Settings.Default.ClientID = ClientID;
            }
            Properties.Settings.Default.Save ();
        }
    }
}
