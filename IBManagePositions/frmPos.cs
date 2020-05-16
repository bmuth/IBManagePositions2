using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWI.Controls;
using Be.Timvw.Framework.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Net.Mail;
using PopupControl;
using System.IO;
using BMuth.StatisticsUtilities;
using IBApi;

namespace IBManagePositions
{
    public partial class frmPos : Form
    {
        const int trdcolTICKERTYPE = 1;
        const int trdcolTICKER = 2;
        const int trdcolUNDPRICE = 4;
        const int trdcolOPENCLOSE = 5;
        const int trdcolPREMIUM = 6;
        const int trdcolDELTA = 8;
        const int trdcolTHETA = 9;
        const int trdcolVEGA = 10;
        const int trdcolTHETAVEGARATIO = 11;
        const int trdcolTOTALPROFIT = 12;
        const int trdcolDAILYPROFIT = 13;
        const int trdcolOPENDATE = 14;
        const int trdcolCLOSEDATE = 15;

        const int legcolID = 0;
        const int legcolTICKER = 1;
        const int legcolEQUITYTYPE = 2;
        const int legcolEXCHANGE = 3;
        const int legcolUNDPRICE = 4;
        const int legcolOPENCLOSE = 5;
        const int legcolCALLPUT = 6;
        const int legcolBUYSELL = 7;
        const int legcolSTRIKE = 8;
        const int legcolNOCONTRACTS = 9;
        const int legcolEXPIRES = 10;
        const int legcolDAYSLEFT = 11;
        const int legcolOPENPRICE = 12;
        const int legcolCLOSEPRICE = 13;
        const int legcolLAST = 14;
        const int legcolBID = 15;
        const int legcolASK = 16;
        const int legcolPREMIUM = 17;
        const int legcolDELTA = 19;
        const int legcolTHETA = 20;
        const int legcolGAMMA = 21;
        const int legcolVEGA = 22;
        const int legcolTHETAVEGARATIO = 23;
        const int legcolPROBITM = 24;
        const int legcolTOTALPROFIT = 25;
        const int legcolDAILYPROFIT = 26;
        const int legcolPERCENTPROFIT = 27;
        const int legcolMYDELTA = 28;
        const int legcolMYTHETA = 29;
        const int legcolMYGAMMA = 30;
        const int legcolMYVEGA = 31;
        const int legcolOPENDATE = 32;
        const int legcolCLOSEDATE = 33;

        const int ibcolID = 0;
        const int ibcolTICKER = 1;
        const int ibcolEQUITYTYPE = 2;
        const int ibcolCALLPUT = 3;
        const int ibcolSTRIKE = 4;
        const int ibcolBUYSELL = 5;
        const int ibcolNOCONTRACTS = 6;

        const int betacolID = 0;
        const int betacolTICKER = 1;
        const int betacolEQUITYTYPE = 2;
        const int betacolUNDPRICE = 3;
        const int betacolCALLPUT = 4;
        const int betacolSTRIKE = 5;
        const int betacolBUYSELL = 6;
        const int betacolEXPIRY = 7;
        const int betacolUNITS = 8;
        const int betacolLAST = 9;


        //private Color colRED = Color.FromArgb (228, 71, 23);
        //private Color colYELLOW = Color.FromArgb (248, 236, 47);

        delegate void UpdateMsgDelegate (ErrorLevel level, string text);
        delegate void DataViewDelegate (int index);

        private bool m_bIfConnected = false;
        public static LogCtl m_Log;
        private bool bSettingWidthsToBeSaved = false;
        private bool m_bNotesDirty = false;
        private bool m_bTradeProfitThresholdDirty = false;
        private bool m_bLegProfitThresholdDirty;
        private bool m_bTradePriceThresholdDirty;
        private bool m_bTradeEmailNotificationsDirty = false;
        private bool m_bLegEmailNotificationsDirty = false;
        private DateTime m_RememberDateExpires;
        private System.Collections.Specialized.StringCollection LegColumnWidths;
        private System.Collections.Specialized.StringCollection TradeColumnWidths;
        private System.Collections.Specialized.StringCollection BetaColumnWidths;
        private System.Collections.Specialized.StringCollection IBColumnWidths;
        private System.Collections.Specialized.StringCollection LocalColumnWidths;

        private SortableBindingList<TradeData> m_Trades = new SortableBindingList<TradeData> ();
        private List<LegData> m_ActiveLegs;
        private SortableBindingList<LegData> m_IBLegs;
        private SortableBindingList<LegData> m_LocalLegs;
        private GridToolTip m_CustomProfitToolTip;
        private bool m_bLegRowDirtyFlag;
        private System.Drawing.Size m_szForm;
        private Popup m_ProfitToolTip;
        private Random m_random = new Random ();
        private EWrapperImpl ib;


        internal static string m_DataGroupName { get; set; }

        public frmPos ()
        {

            /* Common log file to begin with
             * -----------------------------
             * once we know the group, we  will reassign the log filename */

            m_Log = new LogCtl ("IBManagePositions2");
            m_Log.LogFilename = Properties.Settings.Default.LogDir + "IBManagePositions2.log";

            ib = new EWrapperImpl (m_Log);

            InitializeComponent ();

            Utils.ib = ib;
            Utils.Log = m_Log;

            m_RememberDateExpires = Properties.Settings.Default.RememberDateExpiry;
            if (m_RememberDateExpires == null)
            {
                m_RememberDateExpires = Utils.ComputeNextExpiryDate (DateTime.Now);
            }

            AdjustWidths ();

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLeg.Columns[legcolEQUITYTYPE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (EquityType));
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvIBPositions.Columns[ibcolEQUITYTYPE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (EquityType));
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLocalPositions.Columns[ibcolEQUITYTYPE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (EquityType));
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBeta.Columns[betacolEQUITYTYPE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (EquityType));
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLeg.Columns[legcolOPENCLOSE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (OpenCloseValues));
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLeg.Columns[legcolCALLPUT];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = CallPutT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBeta.Columns[betacolCALLPUT];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = CallPutT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvIBPositions.Columns[ibcolCALLPUT];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = CallPutT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLocalPositions.Columns[ibcolCALLPUT];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = CallPutT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLeg.Columns[legcolBUYSELL];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = BuySellT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvIBPositions.Columns[ibcolBUYSELL];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = BuySellT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBeta.Columns[betacolBUYSELL];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = BuySellT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvLocalPositions.Columns[ibcolBUYSELL];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = BuySellT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvTrade.Columns[trdcolTICKERTYPE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = TradeT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }
            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvTrade.Columns[trdcolOPENCLOSE];
                oc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                oc.DataSource = Enum.GetValues (typeof (OpenCloseValues)); 
            }

            {
                DataGridViewTextBoxColumn oc = (DataGridViewTextBoxColumn) dgvTrade.Columns[trdcolCLOSEDATE];
                oc.DefaultCellStyle.NullValue = string.Empty;
            }
            {
               DataGridViewTextBoxColumn oc = (DataGridViewTextBoxColumn) dgvLeg.Columns[legcolCLOSEPRICE];
               oc.DefaultCellStyle.NullValue = string.Empty;
            }
            {
                CalendarColumn oc = (CalendarColumn) dgvLeg.Columns[legcolCLOSEDATE];
                oc.DefaultCellStyle.NullValue = string.Empty;
            }
            {
                DataGridViewTextBoxColumn oc = (DataGridViewTextBoxColumn) dgvLeg.Columns[legcolSTRIKE];
                oc.DefaultCellStyle.NullValue = string.Empty;
            }

            //Font font = new Font (new FontFamily ("Calibri"), 9.0F, FontStyle.Regular, GraphicsUnit.Point);

            dgvLeg.Columns[legcolDELTA].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeg.Columns[legcolTHETA].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeg.Columns[legcolGAMMA].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeg.Columns[legcolVEGA].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeg.Columns[legcolTHETAVEGARATIO].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgvLeg.Columns[legcolDELTA].HeaderText = "Total δ";
            ////dgvLeg.Columns[legcolTHETA].HeaderCell.Style.Font = font;
            //dgvLeg.Columns[legcolTHETA].HeaderText = "Total θ";
            ////dgvLeg.Columns[legcolGAMMA].HeaderCell.Style.Font = font;
            //dgvLeg.Columns[legcolGAMMA].HeaderText = "γ";
            ////dgvLeg.Columns[legcolVEGA].HeaderCell.Style.Font = font;
            //dgvLeg.Columns[legcolVEGA].HeaderText = "ν";
            ////dgvLeg.Columns[legcolTHETAVEGARATIO].HeaderCell.Style.Font = font;
            //dgvLeg.Columns[legcolTHETAVEGARATIO].HeaderText = "θ/ν Ratio";
            
            dgvLeg.RowsDefaultCellStyle.BackColor = Color.FromArgb (235, 241, 222);
            dgvLeg.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb (216, 228, 188);
            dgvLeg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLeg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb (155, 187, 89);

            dgvTrade.RowsDefaultCellStyle.BackColor = Color.FromArgb (219, 228, 241);
            dgvTrade.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb (184, 204, 227);
            dgvTrade.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTrade.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb (79, 129, 188);


            /* Associate a context menu for premium column
             * ------------------------------------------- */

            dgvTrade.Columns[trdcolPREMIUM].ContextMenuStrip = cmsTradePremium;

            m_CustomProfitToolTip = new GridToolTip ();
            m_ProfitToolTip = new Popup (m_CustomProfitToolTip);
            m_ProfitToolTip.AutoClose = false;
            m_ProfitToolTip.FocusOnOpen = false;
            m_ProfitToolTip.ShowingAnimation = m_ProfitToolTip.HidingAnimation = PopupAnimations.Blend;
        }

        /*****************************************************
         * 
         * Form Closing
         * 
         * ***************************************************/

        private void frmPos_FormClosing (object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.RememberDateExpiry = m_RememberDateExpires;

            if (bSettingWidthsToBeSaved)
            {
//                Properties.Settings.Default.PositionColumnWidths = PositionColumnWidths;
                Properties.Settings.Default.LegColumnWidths = LegColumnWidths;
                Properties.Settings.Default.TradeColumnWidths = TradeColumnWidths;
                Properties.Settings.Default.BetaColumnWidths = BetaColumnWidths;
                Properties.Settings.Default.IBColumnWidths = IBColumnWidths;
                Properties.Settings.Default.LocalColumnWidths = LocalColumnWidths;
                Properties.Settings.Default.FormHeight = m_szForm.Height;
                Properties.Settings.Default.FormWidth = m_szForm.Width;
                Properties.Settings.Default.SplitDistance = splitContainer1.SplitterDistance;
                Properties.Settings.Default.Save ();
            }

            ib.ClientSocket.eDisconnect ();
            m_Log.Dispose ();

        }

 
        /*****************************************************
         * 
         * Adjust Widths
         * 
         * ***************************************************/

        private void AdjustWidths ()
        {
            LegColumnWidths = Properties.Settings.Default.LegColumnWidths;

            for (int i = 0; i < dgvLeg.Columns.Count; i++)
            {
                int col_width;
                if (i < LegColumnWidths.Count)
                {
                    if (int.TryParse (LegColumnWidths[i], out col_width))
                    {
                        dgvLeg.Columns[i].Width = col_width;
                    }
                }
            }

            TradeColumnWidths = Properties.Settings.Default.TradeColumnWidths;

            for (int i = 0; i < dgvTrade.Columns.Count; i++)
            {
                int col_width;
                if (i < TradeColumnWidths.Count)
                {
                    if (int.TryParse (TradeColumnWidths[i], out col_width))
                    {
                        dgvTrade.Columns[i].Width = col_width;
                    }
                }
            }

            BetaColumnWidths = Properties.Settings.Default.BetaColumnWidths;

            for (int i = 0; i < dgvBeta.Columns.Count; i++)
            {
                int col_width;
                if (i < BetaColumnWidths.Count)
                {
                    if (int.TryParse (BetaColumnWidths[i], out col_width))
                    {
                        dgvBeta.Columns[i].Width = col_width;
                    }
                }
            }

            IBColumnWidths = Properties.Settings.Default.IBColumnWidths;

            for (int i = 0; i < dgvIBPositions.Columns.Count; i++)
            {
                int col_width;
                if (i < IBColumnWidths.Count)
                {
                    if (int.TryParse (IBColumnWidths[i], out col_width))
                    {
                        dgvIBPositions.Columns[i].Width = col_width;
                    }
                }
            }

            LocalColumnWidths = Properties.Settings.Default.LocalColumnWidths;

            for (int i = 0; i < dgvLocalPositions.Columns.Count; i++)
            {
                int col_width;
                if (i < LocalColumnWidths.Count)
                {
                    if (int.TryParse (LocalColumnWidths[i], out col_width))
                    {
                        dgvLocalPositions.Columns[i].Width = col_width;
                    }
                }
            }
        }

        /*****************************************************
        * 
        * Form Load
        * 
        * ***************************************************/
        private void frmPos_Load (object sender, EventArgs e)
        {
            using (frmConnect Con = new frmConnect ())
            {
                while (true)
                {
                    if (Con.ShowDialog (this) == DialogResult.OK)
                    {
                        string host = "127.0.0.1";
                        try
                        {
                            ib.ClientSocket.eConnect (host, Con.Port, Con.ClientId);
                            m_bIfConnected = true;
                            m_DataGroupName = Con.DataGroupName;

                            /* Now reassign the log filename
                             * ----------------------------- */

                            string dir = Properties.Settings.Default.LogDir + m_DataGroupName;

                            if (Directory.Exists (dir))
                            {
                                m_Log.LogFilename = dir + "\\IBManagePositions2.log";
                            }
                            return;
                        }
                        catch (Exception ex)
                        {
                            AddMessage (ErrorLevel.logERR, string.Format ("Please check your connection attributes. {0}", ex.Message));
                            MessageBox.Show (ex.Message);
                        }
                    }
                }
            }
        }

        /*****************************************************
        * 
        * AddMessage
        * 
        * ***************************************************/
        public void AddMessage (ErrorLevel level, string text)
        {
            if (this.tbMsg.InvokeRequired)
            {
                UpdateMsgDelegate d = new UpdateMsgDelegate (AddMessage);
                this.Invoke (d, new object[] { level, text });
            }
            else
            {
                m_Log.Log (level, text);
                tbMsg.Text += (text + "\r\n");
                tbMsg.Select (tbMsg.Text.Length, 0);
                tbMsg.ScrollToCaret ();
            }
        }

        /************************************************************
          * 
          * frmPos_Shown
          * 
          * *********************************************************/

        private void frmPos_Shown (object sender, EventArgs e)
        {
            if (!m_bIfConnected)
            {
                this.Close ();
            }

            StartIBPump ();

            using (Font font = new Font ("Tahoma", 6.75f, GraphicsUnit.Point))
            {
                foreach (Control c in tabControlPos.Controls)
                {
                    UpdateTheme (c, font);
                }
            }

            AdjustWidths ();
            bSettingWidthsToBeSaved = true; // can save now
            splitContainer1.SuspendLayout ();
            m_szForm.Height = Properties.Settings.Default.FormHeight;
            m_szForm.Width = Properties.Settings.Default.FormWidth;
            this.Size = m_szForm;
            splitContainer1.SplitterDistance = Properties.Settings.Default.SplitDistance;
            splitContainer1.ResumeLayout ();
            LoadTradeGrid ();
        }

        /********************************************************
         * 
         * Hook up the IB Pump
         * 
         * ******************************************************/
        private void StartIBPump ()
        {
            AutoResetEvent ev = new AutoResetEvent (false);

            void NextValidIdHandler (int x)
            {
                ev.Set ();
            }

            ib.evError1 += Ib_evError1;
            ib.evError3 += Ib_evError3;

             ib.evNextValidId += NextValidIdHandler;

            /* Spin up the EReader
             * ------------------- */

            var reader = new EReader (ib.ClientSocket, ib.Signal);
            reader.Start ();

            /* Once the messages are in the queue, an additional thread can be created to fetch them
             * ------------------------------------------------------------------------------------- */

            new Thread (() =>
            {
                while (ib.ClientSocket.IsConnected ())
                {
                    ib.Signal.waitForSignal ();
                    reader.processMsgs ();
                }
            }
                       )
            { IsBackground = true }.Start ();

            //! [ereader]
            /*************************************************************************************************************************************************/
            /* One (although primitive) way of knowing if we can proceed is by monitoring the order's nextValidId reception which comes down automatically after connecting. */
            /*************************************************************************************************************************************************/

            ev.WaitOne ();
            ib.evNextValidId -= NextValidIdHandler;

            m_Log.Log (ErrorLevel.logDEB, string.Format ("NextOrderId={0}", ib.NextOrderId));
        }


        /**********************************************************
         * 
         * UpdateTheme
         * 
         * *******************************************************/

        private void UpdateTheme (Control c, System.Drawing.Font font)
        {
            if (c.GetType () == typeof (Button))
            {
                c.Font = font;
                //c.BackColor = Color.FromArgb (79, 129, 188);
            }
            else if (c.GetType () == typeof (CheckBox))
            {
                c.Font = Font;
            }
            else if (c.GetType () == typeof (Label))
            {
                if (((string) c.Tag) != "lma")
                {
                    c.Font = font;
                }
            }

            foreach (Control control in c.Controls)
            {
                UpdateTheme (control, font);
            }
        }

         /***********************************************************
         * 
         * hlProbabilityITM
         * 
         * ********************************************************/

        private void hlLegProbabilityITM (TradeData trade, LegData leg)
        {
            if (trade == null)
            {
                return;
            }

            foreach (DataGridViewRow r in dgvLeg.Rows)
            {
                if (!r.IsNewRow)
                {
                    if (leg == trade.m_Legs[r.Index])
                    {
                        DataGridViewCell cell = r.Cells[legcolPROBITM];

                        if (leg.ProbITM > 30.0 && leg.ProbITM < 50.0)
                        {
                            cell.Style.BackColor = Utils.colYELLOW;
                            cell.Style.ForeColor = Color.Black;
                            //NotifyBrian (EmailNotify.NEAR_ITM, index);
                        }
                        else if (leg.ProbITM >= 50.0)
                        {
                            cell.Style.BackColor = Utils.colRED;
                            cell.Style.ForeColor = Color.White;
                            //NotifyBrian (EmailNotify.ITM, index);
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Empty;
                            cell.Style.ForeColor = Color.Black;
                        }
                        dgvLeg.InvalidateCell (r.Cells[legcolPROBITM]);
                    }
                }
            }
        }


        /*************************************************************************
         * 
         * Call back OptionComputation
         * 
         * **********************************************************************/

        private void Ib_evTickOptionComputation (int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        // private void axTws_tickOptionComputation (object sender, AxTWSLib._DTwsEvents_tickOptionComputationEvent e)
        {
            if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
            {
                LegOptionComputation (tickerId, field, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
            }
            else if ((tickerId & 0xFFFF0000) == Utils.ibCLOSETRADE)
            {
//                m_frmCloseTrade.TradeCloseOptionComputation (e);
            }
            else if ((tickerId & 0xFFFF0000) == 0)
            {
//                OldOptionComputation (e);
            }
        }

        /**************************************************************************
         * 
         * LegOptionComputation
         * 
         * ***********************************************************************/

        private void LegOptionComputation (int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        //  private void LegOptionComputation (AxTWSLib._DTwsEvents_tickOptionComputationEvent e)
        {
            int index = tickerId & 0xFFFF;

            double? price = null;
            //     1.79769e+308;
            // 1.79769313486232E+308
            m_Log.Log (ErrorLevel.logDEB, string.Format ("LegOptionComputation: for {0} ticktype: {1} {3} value: {2}", m_ActiveLegs[index].Ticker, field, optPrice, TickType.Display (field)));
            if (optPrice < double.MaxValue)
            {
                price = optPrice;
            }
            else
            {
                m_Log.Log (ErrorLevel.logDEB, "LegOptionComputation: price set to nil");
            }
            LegData option = m_ActiveLegs[index];

            /* Changed to compute our own total delta
               -------------------------------------- */

            //if (e.delta < double.MaxValue)
            //{
            //    if (option.IfSell)
            //    {
            //        option.Delta = -e.delta;
            //        option.TotalDelta = option.NoContracts * (-e.delta) * 100.0;
            //    }
            //    else
            //    {
            //        option.Delta = e.delta;
            //        option.TotalDelta = option.NoContracts * (e.delta) * 100.0;

            //    }
            //    LegChangedSoUpdateGUI (option, legcolDELTA);
            //}
            //else
            //{
            //    m_Log.Log (ErrorLevel.logERR, string.Format ("LegOptionComputation: for {0} Bad delta. {1}.", m_ActiveLegs[index].Ticker, e.delta.ToString ()));
            //}

            /* Changed to compute our own total theta
               --------------------------------------- */

            //if (e.theta < double.MaxValue)
            //{
            //    if (option.IfSell)
            //    {
            //        option.Theta = -e.theta;
            //        option.TotalTheta = 100 * option.NoContracts * (-e.theta);
            //    }
            //    else
            //    {
            //        option.Theta = e.theta;
            //        option.TotalTheta = 100 * option.NoContracts * (e.theta);
            //    }
            //    LegChangedSoUpdateGUI (option, legcolTHETA);
            //    //  dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colTHETA]);

            //    //option.ThetaVegaRatio = option.Theta / option.Vega;
            //    //LegChangedSoUpdateGUI (option, legcolTHETAVEGARATIO);
            //}
            //else
            //{
            //    m_Log.Log (ErrorLevel.logERR, string.Format ("LegOptionComputation: for {0} Bad theta. {1}.", m_ActiveLegs[index].Ticker, e.theta.ToString ()));
            //}

            /* Don't really care about gamma and vega 
               -------------------------------------- removed */

            //if (e.gamma < double.MaxValue)
            //{
            //    option.Gamma = e.gamma;
            //    LegChangedSoUpdateGUI (option, legcolGAMMA);
            //}
            //if (e.vega < double.MaxValue)
            //{
            //    option.Vega = e.vega;
            //    LegChangedSoUpdateGUI (option, legcolVEGA);
            //    //option.ThetaVegaRatio = option.Theta / option.Vega;
            //}

            switch (field)
            {
                case TickType.BID_OPTION:
                    if (price != null)
                    {
                        if (option.CurrBid != price)
                        {
                            option.CurrBid = price;
                            LegChangedSoUpdateGUI (option, legcolBID);
                            option.ComputeProfitFigures ();
                            LegChangedSoUpdateGUI (option, legcolTOTALPROFIT);
                            LegChangedSoUpdateGUI (option, legcolPERCENTPROFIT);
                            //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colBID]);
                            option.EstimateCommissions ();
                        }
                    }
                    break;
                case TickType.ASK_OPTION:
                    if (price != null)
                    {
                        if (option.CurrAsk != price)
                        {
                            option.CurrAsk = price;
                            LegChangedSoUpdateGUI (option, legcolASK);
                            option.ComputeProfitFigures ();
                            LegChangedSoUpdateGUI (option, legcolTOTALPROFIT);
                            LegChangedSoUpdateGUI (option, legcolPERCENTPROFIT);
                            //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colASK]);
                            option.EstimateCommissions ();
                        }
                    }
                    break;
                case TickType.LAST_OPTION:
                    if (price != null)
                    {
                        if (option.LastPrice != price)
                        {
                            option.LastPrice = price;
                            LegChangedSoUpdateGUI (option, legcolLAST);
                            option.ComputeProfitFigures ();
                            LegChangedSoUpdateGUI (option, legcolTOTALPROFIT);
                            LegChangedSoUpdateGUI (option, legcolPERCENTPROFIT);
                            //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colLAST]);
                        }
                    }
                    break;
                case TickType.MODEL_OPTION:
                    m_Log.Log (ErrorLevel.logDEB, "LegOptionComputation: case 13");
                    break;

                default:
                    m_Log.Log (ErrorLevel.logSEV, "Bug in LegOptionComputation:");
                    break;
            }
            if (undPrice < double.MaxValue)
            {
                option.UpdateUnderlyingPrice (undPrice);
                LegChangedSoUpdateGUI (option, legcolUNDPRICE);
                //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colUNDPRICE]);
            }
            m_Log.Log (ErrorLevel.logDEB, string.Format ("LegOptionComputation: for {0} Underlying price {1}.", option.Ticker, undPrice.ToString ()));
            if (impliedVolatility < double.MaxValue)
            {
                if (option.iv != impliedVolatility)
                {
                    option.iv = impliedVolatility;
                    m_Log.Log (ErrorLevel.logDEB, string.Format ("LegOptionComputation: for {0} iv={1:F4}.", option.Ticker, impliedVolatility));
                    option.ComputeProbITM ();
                    LegChangedSoUpdateGUI (option, legcolPROBITM);
                }
            }
            //else
            //{
            //    m_Log.Log (ErrorLevel.logERR, string.Format ("LegOptionComputation: for {0} Bad IV. {1}.", m_ActiveLegs[index].Ticker, e.impliedVol.ToString ()));
            //}
        }

        /**********************************************************
         * 
         * Some leg data has been updated
         * 
         * Note that this may be a leg that is not currently displayed
         * 
         * *******************************************************/

        internal delegate void LegChangedSoUpdateGUIHandler (LegData leg, int col);
        internal void LegChangedSoUpdateGUI (LegData leg, int col)
        {

            /* Make sure we are on the form thread
             * ----------------------------------- */

            if (this.InvokeRequired)
            {
                LegChangedSoUpdateGUIHandler d = new LegChangedSoUpdateGUIHandler (LegChangedSoUpdateGUI);
                this.Invoke (d, new object[] { leg, col });
            }


            /* Signal a change
             * --------------- */

            leg.SignalTrade (col);    // the parent trade should pick this up.

            /* Now see if any cells need to be invalidated
             * ------------------------------------------- */

            TradeData trade = null;
            if (dgvTrade.CurrentRow != null)
            {
                if (!dgvTrade.CurrentRow.IsNewRow)
                {
                    trade = m_Trades[dgvTrade.CurrentRow.Index];
                }
            }

            if (trade == null || !trade.m_Legs.Contains (leg))
            {
                // Nope, this isn't currently displayed, so bail
                return;
            }
 
            switch (col)
            {
//                case legcolUNDPRICE:
                case legcolLAST:
                case legcolBID:
                case legcolASK:
                case legcolDELTA:
                case legcolTHETA:
                case legcolTHETAVEGARATIO:
                case legcolGAMMA:
                case legcolVEGA:
                case legcolMYDELTA:
                case legcolMYTHETA:
                case legcolMYGAMMA:
                case legcolMYVEGA:
                case legcolPERCENTPROFIT:

                    /* invalidate the cell
                     * ------------------- */

                    for (int i = 0; i < dgvLeg.Rows.Count; i++)
                    {
                        DataGridViewRow r = dgvLeg.Rows[i];
                        if (trade != null && r.Index < trade.m_Legs.Count && trade.m_Legs[r.Index] == leg)
                        {
                            dgvLeg.InvalidateCell (r.Cells[col]);
                        }
                    }
                    break;

                case legcolUNDPRICE:

                    /* invalidate the cell
                     * ------------------- */

                    for (int i = 0; i < dgvLeg.Rows.Count; i++)
                    {
                        DataGridViewRow r = dgvLeg.Rows[i];
                        if (trade != null && r.Index < trade.m_Legs.Count && trade.m_Legs[r.Index] == leg)
                        {
                            dgvLeg.InvalidateCell (r.Cells[col]);
                            if (leg.Equity == EquityType.Stock || leg.Equity == EquityType.FutOpt)
                            {
                                leg.ComputeProfitFigures ();
                                hlLegProfitLoss (r, leg);
                                dgvLeg.InvalidateCell (r.Cells[legcolTOTALPROFIT]);
                                dgvLeg.InvalidateCell (r.Cells[legcolDAILYPROFIT]);
                            }
                        }
                    }
                    break;

                case legcolPROBITM:
                    hlLegProbabilityITM (trade, leg);
                    break;

                case legcolTOTALPROFIT:
                    for (int i = 0; i < dgvLeg.Rows.Count; i++)
                    {
                        DataGridViewRow r = dgvLeg.Rows[i];
                        if (trade != null && r.Index < trade.m_Legs.Count && trade.m_Legs[r.Index] == leg)
                        {
                            hlLegProfitLoss (r, leg);
                            dgvLeg.InvalidateCell (r.Cells[col]);
                        }
                    }
                    break;

                case legcolDAILYPROFIT:
                    for (int i = 0; i < dgvLeg.Rows.Count; i++)
                    {
                        DataGridViewRow r = dgvLeg.Rows[i];
                        if (trade != null && r.Index < trade.m_Legs.Count && trade.m_Legs[r.Index] == leg)
                        {
                            hlLegProfitLoss (r, leg);
                            dgvLeg.InvalidateCell (r.Cells[col]);
                        }
                    }
                    break;
                default:
                    m_Log.Log (ErrorLevel.logSEV, string.Format ("LegUpdated: didn't handle {0} column update", col));
                    break;
            }
        }

        private void Ib_evTickGeneric (int tickerId, int field, double value)
//      private void axTws_tickGeneric (object sender, AxTWSLib._DTwsEvents_tickGenericEvent e)
        {
            if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickGeneric for {0} tickType: {1} value: {2}", m_ActiveLegs[tickerId & 0xFFFF], field, value));
            }
            else if ((tickerId & Utils.ibCLOSETRADE) == Utils.ibCLOSETRADE)
            {
//                if (m_frmCloseTrade != null)
//                {
////                    m_frmCloseTrade.tickGeneric (e);
//                }
            }
            else
            {
//                m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickGeneric for {0} tickType: {1} value: {2}", m_Options[e.id].Ticker, e.tickType, e.value));
            }
        }

        private void Ib_evTickEFP (int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        //private void axTws_tickEFP (object sender, AxTWSLib._DTwsEvents_tickEFPEvent e)
        {
            if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickEFP for {0}", m_ActiveLegs[tickerId & 0xFFFF].Ticker));
            }
            else
            {
//                m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickEFP for {0}", m_Options[e.tickerId].Ticker));
            }
        }

        private void Ib_evTickSize (int tickerId, int field, int size)
        //private void axTws_tickSize (object sender, AxTWSLib._DTwsEvents_tickSizeEvent e)
        {
            m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickSize tickerId: {0:X} tickType:{1} {3} size: {2}", tickerId, field, size, TickType.Display (field)));
            if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickSize for {0} tickType:{1} {3} size: {2}", m_ActiveLegs[tickerId & 0xFFFF].Ticker, field, size, TickType.Display (field)));
            }
            else if ((tickerId & Utils.ibCLOSETRADE) == Utils.ibCLOSETRADE)
            {
//                if (m_frmCloseTrade != null)
//                {
////                    m_frmCloseTrade.tickSize (e);
//                }
            }
            else
            {
                //                m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickSize for {0} tickType:{1} size: {2}", m_Options[e.id].Ticker, field, size));
            }
        }

 /*       private void axTws_tickSnapshotEnd (object sender, AxTWSLib._DTwsEvents_tickSnapshotEndEvent e)
        {
            if ((e.reqId & 0xFFFF0000) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickSnapshotEnd for {0}", m_ActiveLegs[e.reqId & 0xFFFF].Ticker));
                m_ActiveLegs[e.reqId & 0xFFFF].bIfUpdatingMarketData = false;
            }
            else if ((e.reqId & Utils.ibCLOSETRADE) == Utils.ibCLOSETRADE)
            {
//                if (m_frmCloseTrade != null)
//                {
////                    m_frmCloseTrade.tickSnapshotEnd (e);
//                }
            }
            else
            {
                //m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickSnapshotEnd for {0}", m_Options[e.reqId].Ticker));
                //hlStandardView (e.reqId);
            }
        }
*/

        private void Ib_evTickPrice (int tickerId, int field, double price, TickAttrib attribs)
  //      private void axTws_tickPrice (object sender, AxTWSLib._DTwsEvents_tickPriceEvent e)
        {
            int index = tickerId & 0xFFFF;

            if (price < double.MaxValue)
            {
                if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
                {
                    m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickPrice for {0} tickType:{1} {3} value: {2}", m_ActiveLegs[index].Ticker, field, price, TickType.Display (field)));

                    LegData leg = m_ActiveLegs[index];

                    /* originally I would only update the stock price if this was the LAST price.
                     * Note sure why. I've removed this distinction */

                    //if (e.tickType == TickType.LAST) // last price
                    //{
                    //    if (leg.Equity == EquityType.Stock)
                    //    {
                    //        leg.UpdateUnderlyingPrice (e.price);
                    //        LegChangedSoUpdateGUI (leg, legcolUNDPRICE);
                    //        leg.SignalTrade (legcolUNDPRICE);

                    //        leg.ComputeProfitFigures ();
                    //        LegChangedSoUpdateGUI (leg, legcolTOTALPROFIT);
                    //        LegChangedSoUpdateGUI (leg, legcolDAILYPROFIT);
                    //        leg.SignalTrade (legcolTOTALPROFIT);
                    //        leg.SignalTrade (legcolDAILYPROFIT);
                    //    }
                    //}

                    /* Ignore ASK and BID prices
                     * -------------------------
                     * price is zero in this case for after hours data */

                    /* Use the CLOSE price only if there is no LAST price.  */
                      

                    if (field == TickType.CLOSE || field == TickType.LAST)
                    {
                        if (field == TickType.CLOSE)
                        {
                            if (leg.LastPrice != null)
                            {
                                return;
                            }
                            leg.LastPrice = price; // set last price to the closing price
                            LegChangedSoUpdateGUI (leg, legcolLAST);
                        }
                    
                    //if (e.tickType == TickType.LAST)
                    //{
                        if (leg.Equity == EquityType.Stock || leg.Equity == EquityType.Future || leg.Equity == EquityType.Index) // it's a future, index or stock
                        {
                            leg.UpdateUnderlyingPrice (price);
                            LegChangedSoUpdateGUI (leg, legcolUNDPRICE);//this will update profit loss columns as well
                            LegChangedSoUpdateGUI (leg, legcolTOTALPROFIT);
                            LegChangedSoUpdateGUI (leg, legcolPERCENTPROFIT);
                        }

                        if (leg.Equity == EquityType.Option || leg.Equity == EquityType.FutOpt)
                        {
//                            if (leg.LastPrice != e.price)
                            {
                                if (price > 0) // just to be sure
                                {
                                    leg.LastPrice = price;
                                    if (leg.ComputeMyGreeks ())
                                    {
                                        LegChangedSoUpdateGUI (leg, legcolMYDELTA);
                                        LegChangedSoUpdateGUI (leg, legcolMYTHETA);
                                        LegChangedSoUpdateGUI (leg, legcolMYGAMMA);
                                        LegChangedSoUpdateGUI (leg, legcolMYVEGA);
                                        LegChangedSoUpdateGUI (leg, legcolDELTA);
                                        LegChangedSoUpdateGUI (leg, legcolTHETA);
                                        //LegChangedSoUpdateGUI (leg, legcolTHETAVEGARATIO);

                                    }
                                    LegChangedSoUpdateGUI (leg, legcolLAST);
                                    leg.ComputeProfitFigures ();
                                    LegChangedSoUpdateGUI (leg, legcolTOTALPROFIT);
                                    LegChangedSoUpdateGUI (leg, legcolPERCENTPROFIT);
                                }
                            }
                        }
                    }

                }
                else if ((tickerId & Utils.ibCLOSETRADE) == Utils.ibCLOSETRADE)
                {
                    //                if (m_frmCloseTrade != null)
                    //                {
                    ////                    m_frmCloseTrade.tickPrice (e);
                    //                }
                }
                else
                {
                    //                m_Log.Log (ErrorLevel.logDEB, string.Format ("axTws_tickPrice for {0} tickType:{1} value: {2}", m_Options[e.id].Ticker, e.tickType, e.price));
                }
            }
        }
        private void Ib_evTickString (int tickerId, int field, string value)
        // private void axTws_tickString (object sender, AxTWSLib._DTwsEvents_tickStringEvent e)
        {
            m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickString tickerId {0:X} tickType:{1} {3} [{2}] ", tickerId, field, value, TickType.Display (field)));
            if ((tickerId & 0xFFFF0000) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickString called for {0} tickType:{1} {3} [{2}] ", m_ActiveLegs[tickerId & 0xFFFF].Ticker, field, value, TickType.Display (field)));
            }
            else if ((tickerId & Utils.ibCLOSETRADE) == Utils.ibCLOSETRADE)
            {
                //                if (m_frmCloseTrade != null)
                //                {
                ////                    m_frmCloseTrade.tickString (e);
                //                }
            }
            else
            {
                //                m_Log.Log (ErrorLevel.logDEB, string.Format ("Ib_evTickString called for {0} tickType:{1} [{2}] ", m_Options[e.id].Ticker, e.tickType, e.value));
            }
        }

        /**************************************************
         * 
         * This probably will never get called
         * 
         * ***********************************************/

    private void Ib_evError1 (string s)
    {
        NotifyError (-999, -999, s);
    }
    
    private void Ib_evError3 (int id, int errorCode, string errorMsg)
    //private void axTws_errMsg (object sender, AxTWSLib._DTwsEvents_errMsgEvent e)
    {
        NotifyError (id, errorCode, errorMsg);
    }

    /**********************************************************************
     * 
     * NotifyError
     * 
     * *******************************************************************/

    public void NotifyError (int id, int errorCode, string errorMsg)
        {
            if (id == -1 && errorCode == 502)
            {
 //               m_bConnectFailure = true; // connection failure
            }
            AddMessage (ErrorLevel.logERR, string.Format ("Error. Id: {0:X} Code: {1} Msg: {2}", id, errorCode, errorMsg));
        }

        /**********************************************************************
          * 
          * contractDetails event (not used. Was in original designer frmPos)
          * 
          * *******************************************************************/
     
     
        //private void axTws_contractDetails (object sender, AxTWSLib._DTwsEvents_contractDetailsEvent e)
        //{
        //    //m_Log.Log (ErrorLevel.logDEB, string.Format ("Local Symbol: {0}, Expires: {1}, Strike: {2} Multiplier: {3} {4}", e.localSymbol, e.expiry, e.strike.ToString ("0000.00"), e.multiplier, e.conId.ToString ()));
        //    //if (e.multiplier != "100")
        //    //{
        //    //    m_Log.Log (ErrorLevel.logERR, "Skipped, due to wrong multiplier");
        //    //    return;
        //    //}
        //    //m_Options[m_CurrentOption].m_OptionChain.Add (new OptionInfo (e.strike, e.localSymbol, e.multiplier, e.conId));
        //}

        /**********************************************************************
            * 
            * contractDetailsEnd event
            * - finished collecting this one option of the option chain
            * 
            * *******************************************************************/
        private void Ib_evContractDetailsEnd (int ReqId)
        //      private void axTws_contractDetailsEnd (object sender, AxTWSLib._DTwsEvents_contractDetailsEndEvent e) 
        {
            if ((ReqId & Utils.ibLEG) == Utils.ibLEG)
            {
                m_Log.Log (ErrorLevel.logINF, string.Format ("Ib_evContractDetailsEnd: {0}", m_ActiveLegs[ReqId & 0xFFFF].Ticker));
            }
            else
            {
                //m_Log.Log (ErrorLevel.logINF, string.Format ("axTws_contractDetailsEnd: {0}", m_Options[e.reqId].Ticker));
                //FinishedCollectingOptionChains (e.reqId);
            }
        }

        /*************************************************************************
         * 
         * axTws_contractDetailsEx event
         * 
         * **********************************************************************/
        private void Ib_evContractDetails (int ReqId, ContractDetails cd)
//        private void axTws_contractDetailsEx (object sender, AxTWSLib._DTwsEvents_contractDetailsExEvent e)
        {
            if ((ReqId & Utils.ibLEG) == Utils.ibLEG)
            {
                //TWSLib.IContractDetails c = e.contractDetails;
                //TWSLib.IContract con = (TWSLib.IContract) c.summary;

                Contract c = cd.Contract;

                if (c.Multiplier == "10") // skip the minis
                {
                    return;
                }
                m_Log.Log (ErrorLevel.logINF, string.Format ("contractDetailsEx: {0}, market name: {1} ordertypes {2}", cd.LongName, cd.MarketName, cd.OrderTypes));

                int reqId = ReqId & 0xFFFF;

                LegData leg = m_ActiveLegs[reqId];
                leg.LocalSymbol = c.LocalSymbol;
                leg.ConId = c.ConId;
                m_Log.Log (ErrorLevel.logINF, string.Format ("Setting ConId in Ib_evContractDetails: {0} {1} strike: {2} exp: {3} assigned localsym: [{4}] conId set to {5}", leg.Ticker, leg.DisplayCall (), leg.Strike.ToString (), leg.ExpiryDate.ToString (), c.LocalSymbol, c.ConId));
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    //dc.UpdateLocalSymbolLeg (m_ActiveLegs[reqId].Id, m_ActiveLegs[reqId].LocalSymbol);
                    m_Log.Log (ErrorLevel.logINF, string.Format ("UpdateConIdLeg called for {0}", leg.LocalSymbol));
                    dc.UpdateConIdLeg (leg.Id, leg.ConId, leg.LocalSymbol, leg.Multiplier);
                }
                FetchLegMarketData (reqId);
            }
            }

                /*****************************************************
                 * 
                 * axTws_orderStatus
                 * 
                 * **************************************************/
        /*
                private void axTws_orderStatus (object sender, AxTWSLib._DTwsEvents_orderStatusEvent e)
                {
                    m_Log.Log (ErrorLevel.logINF, string.Format ("orderStatus: clientId {0} orderId {1} status {2} permid {3} why held {4} avg. Fill Price {5:F3}", e.clientId, e.id, e.status, e.permId.ToString (), e.whyHeld, e.avgFillPrice));
                }

                /****************************************************
                 * 
                 * axTws_openOrderEnd
                 * 
                 * *************************************************/
        /*
                private void axTws_openOrderEnd (object sender, EventArgs e)
                {
                    m_Log.Log (ErrorLevel.logINF, "orderEnd");

                }


                /*****************************************************
                 * 
                 * Reconnect button pressed
                 * 
                 * ***************************************************/

        private void btnMsgConnect_Click (object sender, EventArgs e)
        {
/*            m_bConnectFailure = false;

            if (m_bIfConnected)
            {
                axTws.disconnect ();
                m_bIfConnected = false;
            }

            using (frmConnect Con = new frmConnect ())
            {
                if (Con.ShowDialog (this) == DialogResult.OK)
                {
                    string host = "127.0.0.1";
                    try
                    {
                        axTws.connect (host, Con.Port, Con.ClientId);
                        m_DataGroupName = Con.DataGroupName;
                        m_bIfConnected = true;
                    }
                    catch (Exception ex)
                    {
                        AddMessage (ErrorLevel.logERR, string.Format ("Please check your connection attributes. {0}", ex.Message));
                        m_bConnectFailure = true;
                    }
                }
            }

            LoadTradeGrid ();
            tabControlPos.SelectedIndex = 0;*/
        }

        /**************************************************************
          * 
          * dgvLeg Column Width changed
          * 
          * ***********************************************************/

        private void dgvLeg_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                LegColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvLeg.Columns.Count; i++)
                {
                    LegColumnWidths.Add (dgvLeg.Columns[i].Width.ToString ());
                }
            }
        }

        /***************************************************************
         * 
         * dgvIBPosition Column Width changed
         * 
         * ************************************************************/

        private void dgvIBPositions_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                IBColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvIBPositions.Columns.Count; i++)
                {
                    IBColumnWidths.Add (dgvIBPositions.Columns[i].Width.ToString ());
                }
            }
        }

        /***************************************************************
         * 
         * LocalPositions Column Width changed
         * 
         * ************************************************************/
        private void dgvLocalPositions_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                LocalColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvLocalPositions.Columns.Count; i++)
                {
                    LocalColumnWidths.Add (dgvLocalPositions.Columns[i].Width.ToString ());
                }
            }
        }




        /*************************************************************
         * 
         * Show or Hide Import Positions form
         * 
         * **********************************************************/

        private void cbImportPositions_CheckedChanged (object sender, EventArgs e)
        {
            //if (!cbImportPositions.Checked)
            //{
            //    m_frmImportPositions.Hide ();
            //    m_frmImportPositions.Dispose ();
            //    m_frmImportPositions = null;
            //}
            //else
            //{
            //    m_frmImportPositions = new frmImportPosition ();
            //    m_frmImportPositions.Show ();
            //}

        }

        /****************************************************************
         * 
         * Accept a drop from the Import Positions form
         * 
         * **************************************************************/

        private void dgvLeg_DragDrop (object sender, DragEventArgs e)
        {
            //if (e.Effect == DragDropEffects.Copy)
            //{
            //    if (e.Data.GetDataPresent ("myFormat"))
            //    {
            //        List<int?> PositionIdList = (List<int?>) e.Data.GetData ("myFormat");

            //        using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            //        {
            //            foreach (var id in PositionIdList)
            //            {
            //                var option = (from r in dc.Positions
            //                              //join p in dc.Stocks on r.Ticker equals p.Ticker
            //                              where r.Id == id
            //                              select new LegData (null,
            //                                                  r.Ticker,
            //                                                  null,
            //                                                  null, 
            //                                                  r.IfClosed,
            //                                                  r.IfCall,
            //                                                  r.IfSell,
            //                                                  r.Strike,
            //                                                  r.Expiry,
            //                                                  r.OpenPrice,
            //                                                  r.ClosePrice,
            //                                                  r.NoContracts,
            //                                                  r.OpenDate,
            //                                                  r.ClosedDate,
            //                                                  r.ProfitLoss,
            //                                                  r.ProfitLossTimeStamp,
            //                                                  r.TodayProfitLoss,
            //                                                  r.TodayProfitLossTimeStamp,
            //                                                  r.YesterdayProfitLoss,
            //                                                  r.YesterdayProfitLossTimeStamp,
            //                                                  null,
            //                                                  null,
            //                                                  0,
            //                                                  null)
            //                             ).Single ();


            //                TradeData trade = m_Trades[dgvTrade.CurrentRow.Index];
            //                trade.m_Legs.Add (option);

            //                LegData.StatusChangeHandler sd = new LegData.StatusChangeHandler (trade.LegStatusChangeHandler); 
            //                option.StatusChanged += sd;

            //                option.UpdatePremiumCommissions ();
            //                option.UpdateField (legcolPREMIUM);
            //            }

            //            dgvLeg.AutoGenerateColumns = false;
            //            dgvLeg.DataSource = m_Trades[dgvTrade.CurrentRow.Index].m_Legs;
            //        }
            //    }
            //}

        }

        private void dgvLeg_DragOver (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

 
        /*********************************************************************
         * 
         * Update trade
         * 
         * ******************************************************************/

        private void btnUpdateTrade_Click (object sender, EventArgs e)
        {
             UpdateTrade ();
        }

        private void UpdateTrade ()
        {
            TradeData trade = null;

            if (dgvTrade.CurrentRow == null)
            {
                return;
            }
            if (!dgvTrade.CurrentRow.IsNewRow)
            {
                trade = (TradeData) dgvTrade.CurrentRow.DataBoundItem;
            }
            else
            {
                DialogResult dr = MessageBox.Show ("A current trade needs to be selected.");
                return;
            }

            /* First count the trades associated with the legs. Should be 0 or 1
             * -----------------------------------------------------------------
             * by the time this routine is called, the trade has already been updated in the
             * database, and hence already has a tradeid assigned */

            HashSet<int> tradeids = new HashSet<int> ();

            foreach (LegData r in trade.m_Legs)
            {
                int? trid = r.Trade_id;
                if (trid != null)
                {
                    tradeids.Add ((int) trid);
                }
            }

            if (tradeids.Count > 1)
            {
                DialogResult dr = MessageBox.Show (string.Format ("These legs belong to multiple trades. Ok to assign to {0}?", trade.Ticker), "Legs with multiple trades", MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }

            /* Trade selected, but update company...
             * ------------------------------------- */

            //using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            //{
            //    var stock = (from s in dc.Stocks
            //                 where s.Ticker == trade.Ticker
            //                 select new { s.LastTrade, s.Company }
            //                ).SingleOrDefault ();

            //    if (stock == null)
            //    {
            //        MessageBox.Show (string.Format ("The ticker {0} does not exist in the database. Please fix.", trade.Ticker));
            //        return;
            //    }
            //    trade.Company = stock.Company;
            //    trade.UnderlyingPrice = (double?) stock.LastTrade;
            //}

            /* Now update the legs
             * ------------------- */

            trade.TotalDelta = 0;
            trade.TotalTheta = 0;
            trade.TotalProfitLoss = 0;
            trade.DailyProfitLoss = 0;

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {

                /* Don't really need to use UpsertTrade. Could have used
                 * just UpdateTrade since the td.Id is never null at this point.
                 * In fact, I don't even think this needs to be updated at all...
                 */

                //UpsertTradeResult tr =
                //    dc.UpsertTrade (td.Id,
                //                    td.TradeType,
                //                    td.Ticker,
                //                    (int) td.OpenCloseStatus,
                //                    td.Premium,
                //                    td.Commissions,
                //                    td.OpenDate,
                //                    td.ClosedDate,
                //                    td.TotalDelta,
                //                    td.TotalTheta,
                //                    (decimal?) td.TotalProfitLoss,
                //                    (decimal?) td.ProfitThreshold,
                //                    td.EmailNotifications,
                //                    td.Notes,
                //                    td.LastEmail).Single ();


                foreach (LegData ld in trade.m_Legs)
                {
                    ld.Trade_id = trade.Id;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("UpsertLeg(2) on [{0}]", ld.LocalSymbol));
                    UpsertLegResult lr = dc.UpsertLeg (ld.Id, ld.ConId, ld.LocalSymbol, ld.Ticker, Utils.EquityType2String (ld.Equity), ld.Exchange, ld.Multiplier, (int) ld.OpenCloseStatus, ld.IfCall, ld.IfSell, ld.Strike, ld.ExpiryDate, ld.OpenPrice, ld.ClosePrice, ld.NoContracts, ld.Commissions, ld.OpenDate, ld.ClosedDate, ld.LastEmail, ld.Trade_id).Single ();
                    ld.Id = lr.Id;
                    ld.HookInSignalTrade (trade);
                }

                UpdateTradeTotalAndDailyProfit (trade);
                /* Update the daily profit and daily */
                dc.UpdateTradeProfitLoss (trade.Id, (decimal?) trade.TotalProfitLoss, DateTime.Now, (decimal?) trade.DailyProfitLoss, DateTime.Now);
            }

//            dgvTrade.InvalidateRow (dgvTrade.CurrentRow.Index);
//            //dgvLeg.Invalidate ();
        }

        /***************************************************************
        * 
        * Load Trade Form
        * 
        * ************************************************************/
        private void btnLoadTrades_Click (object sender, EventArgs e)
        {
            using (frmLoadTrades f = new frmLoadTrades ())
            {
                DialogResult res = f.ShowDialog (this);
                if (res == DialogResult.OK)
                {
                    LoadTradeGrid (f.bOpenOnly, f.bUseDateRange, f.dtStartDate, f.dtEndDate, f.szStartsWith);
                }
            }
        }


        /***************************************************************
         * 
         * LoadTradeGrid
         * 
         * ************************************************************/

        private void LoadTradeGrid ()
        {
            LoadTradeGrid (true, false, DateTime.Now, DateTime.Now, "");
        }

        private void LoadTradeGrid (bool bOpenOnly, bool bUseDateRange, DateTime TradeStartDate, DateTime TradeEndDate, string TickerStartsWith)
        {
            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                if (bOpenOnly)
                {
                    var trades = (from t in dc.Trades
                                  join s in dc.Stocks on t.Ticker equals s.Ticker
                                  where (t.IfClosed != (int) OpenCloseValues.Close) && (t.TradeGroup == m_DataGroupName)
                                  select new TradeData (t.TradeId,
                                     t.Ticker,
                                     t.TradeType,
                                     s.Exchange,
                                     s.Company,
                                     (OpenCloseValues) t.IfClosed,
                                     t.Premium,
                                     t.Commissions,
                                     t.OpenDate,
                                     t.ClosedDate,
                                     t.Delta,
                                     t.Theta,
                                     t.TotalProfitLoss, t.TotalProfitLossTimeStamp,
                                     t.TodayProfitLoss, t.TodayProfitLossTimeStamp,
                                     t.YesterdayProfitLoss, t.YesterdayProfitLossTimeStamp,
                                     t.ProfitThreshold,
                                     t.PriceThreshold,
                                     t.PriceThresholdAboveBelow,
                                     t.LastEmail,
                                     t.EmailNotifications,
                                     t.Notes)
                                     ).ToList ();
                    m_Trades = new SortableBindingList<TradeData> (trades);
                }
                else if (TickerStartsWith.Length > 0)
                {
                    if (bUseDateRange)
                    {
                        var trades = (from t in dc.Trades
                                      join s in dc.Stocks on t.Ticker equals s.Ticker
                                      where (t.TradeGroup == m_DataGroupName && t.Ticker.StartsWith (TickerStartsWith)
                                             && t.OpenDate >= TradeStartDate && t.OpenDate <= TradeEndDate)
                                      select new TradeData (t.TradeId,
                                         t.Ticker,
                                         t.TradeType,
                                         s.Exchange,
                                         s.Company,
                                         (OpenCloseValues) t.IfClosed,
                                         t.Premium,
                                         t.Commissions,
                                         t.OpenDate,
                                         t.ClosedDate,
                                         t.Delta,
                                         t.Theta,
                                         t.TotalProfitLoss, t.TotalProfitLossTimeStamp,
                                         t.TodayProfitLoss, t.TodayProfitLossTimeStamp,
                                         t.YesterdayProfitLoss, t.YesterdayProfitLossTimeStamp,
                                         t.ProfitThreshold,
                                         t.PriceThreshold,
                                         t.PriceThresholdAboveBelow,
                                         t.LastEmail,
                                         t.EmailNotifications,
                                         t.Notes)
                                         );
                        m_Trades = new SortableBindingList<TradeData> (trades);
                    }
                    else
                    {
                        var trades = (from t in dc.Trades
                                      join s in dc.Stocks on t.Ticker equals s.Ticker
                                      where (t.TradeGroup == m_DataGroupName && t.Ticker.StartsWith (TickerStartsWith))
                                      select new TradeData (t.TradeId,
                                         t.Ticker,
                                         t.TradeType,
                                         s.Exchange,
                                         s.Company,
                                         (OpenCloseValues) t.IfClosed,
                                         t.Premium,
                                         t.Commissions,
                                         t.OpenDate,
                                         t.ClosedDate,
                                         t.Delta,
                                         t.Theta,
                                         t.TotalProfitLoss, t.TotalProfitLossTimeStamp,
                                         t.TodayProfitLoss, t.TodayProfitLossTimeStamp,
                                         t.YesterdayProfitLoss, t.YesterdayProfitLossTimeStamp,
                                         t.ProfitThreshold,
                                         t.PriceThreshold,
                                         t.PriceThresholdAboveBelow,
                                         t.LastEmail,
                                         t.EmailNotifications,
                                         t.Notes)
                                         );
                        m_Trades = new SortableBindingList<TradeData> (trades);
                    }
                }
                else
                {
                    if (bUseDateRange)
                    {
                        var trades = (from t in dc.Trades
                                      join s in dc.Stocks on t.Ticker equals s.Ticker
                                      where (t.TradeGroup == m_DataGroupName 
                                             && t.OpenDate >= TradeStartDate && t.OpenDate <= TradeEndDate)
                                      select new TradeData (t.TradeId,
                                         t.Ticker,
                                         t.TradeType,
                                         s.Exchange,
                                         s.Company,
                                         (OpenCloseValues) t.IfClosed,
                                         t.Premium,
                                         t.Commissions,
                                         t.OpenDate,
                                         t.ClosedDate,
                                         t.Delta,
                                         t.Theta,
                                         t.TotalProfitLoss, t.TotalProfitLossTimeStamp,
                                         t.TodayProfitLoss, t.TodayProfitLossTimeStamp,
                                         t.YesterdayProfitLoss, t.YesterdayProfitLossTimeStamp,
                                         t.ProfitThreshold,
                                         t.PriceThreshold,
                                         t.PriceThresholdAboveBelow,
                                         t.LastEmail,
                                         t.EmailNotifications,
                                         t.Notes)
                                         );
                        m_Trades = new SortableBindingList<TradeData> (trades);
                    }
                    else
                    {
                        var trades = (from t in dc.Trades
                                      join s in dc.Stocks on t.Ticker equals s.Ticker
                                      where (t.TradeGroup == m_DataGroupName)
                                      select new TradeData (t.TradeId,
                                         t.Ticker,
                                         t.TradeType,
                                         s.Exchange,
                                         s.Company,
                                         (OpenCloseValues) t.IfClosed,
                                         t.Premium,
                                         t.Commissions,
                                         t.OpenDate,
                                         t.ClosedDate,
                                         t.Delta,
                                         t.Theta,
                                         t.TotalProfitLoss, t.TotalProfitLossTimeStamp,
                                         t.TodayProfitLoss, t.TodayProfitLossTimeStamp,
                                         t.YesterdayProfitLoss, t.YesterdayProfitLossTimeStamp,
                                         t.ProfitThreshold,
                                         t.PriceThreshold,
                                         t.PriceThresholdAboveBelow,
                                         t.LastEmail,
                                         t.EmailNotifications,
                                         t.Notes)
                                         );
                        m_Trades = new SortableBindingList<TradeData> (trades);
                    }
                }
                
                foreach (var t in m_Trades)
                {

                    /* Update underlying price
                     * ----------------------- */

                    var stock = (from s in dc.Stocks
                                    where s.Ticker == t.Ticker
                                    select s.LastTrade
                                ).SingleOrDefault ();
                    t.UnderlyingPrice = (double?) stock;

                    t.m_Legs.Clear ();

                    var legs = (from l in dc.Legs
                                where l.Trade_Id == t.Id
                                select new LegData (l.Id,
                                                    l.Ticker,
                                                    Utils.String2EquityType (l.EquityType),
                                                    l.Exchange,
                                                    l.Multiplier,
                                                    l.LocalSymbol,
                                                    l.ConId,
                                                    (OpenCloseValues) l.IfClosed,
                                                    l.IfCall,
                                                    l.IfSell,
                                                    l.Strike,
                                                    l.Expiry,
                                                    l.UndPrice,
                                                    l.OpenPrice,
                                                    l.ClosePrice,
                                                    l.NoContracts,
                                                    l.TotalDelta,
                                                    l.TotalTheta,
                                                    l.Gamma,
                                                    l.Vega,
                                                    l.MyDelta,
                                                    l.MyTheta,
                                                    l.MyGamma,
                                                    l.MyVega,
                                                    l.OpenDate,
                                                    l.ClosedDate,
                                                    l.ProfitLoss,
                                                    l.ProfitLossTimeStamp,
                                                    l.TodayProfitLoss,
                                                    l.TodayProfitLossTimeStamp,
                                                    l.YesterdayProfitLoss,
                                                    l.YesterdayProfitLossTimeStamp,
                                                    l.ProfitThreshold,
                                                    l.LastEmail,
                                                    l.EmailNotifications,
                                                    l.Trade_Id)
                                );
                    foreach (var leg in legs)
                    {

                        /* Fill in some missing fields
                         * --------------------------- */

                        if (leg.ExpiryDate != null)
                        {
                            leg.DaysLeft = Utils.ComputeDaysToExpire (leg.ExpiryDate);
                        }

                        if (leg.UnderlyingPrice == null && leg.OpenCloseStatus != OpenCloseValues.Close)
                        {
                            var stock2 = (from s in dc.Stocks
                                            where s.Ticker == t.Ticker
                                            select s.LastTrade
                                        ).SingleOrDefault ();

                            leg.UnderlyingPrice = (double?) stock2;
                        }
                        // display yesterday's figures for now
                        //leg.ComputeProfitFigures ();
                        leg.UpdatePremium ();
                        leg.UpdateCommissions ();

                        /* May need to update the database, moving current ProfitLoss to YesterdayProfitLoss
                         * --------------------------------------------------------------------------------- */
 
                        DateTime now = DateTime.Now;
                        if (Utils.bIfTodayTradingDay)
                        {
                            if (leg.ProfitLossTimestamp != null)
                            {
                                if (((DateTime) leg.ProfitLossTimestamp).Day != now.Day)
                                {
                                    leg.YesterdayProfitLoss = leg.TotalProfitLoss;
                                    leg.YesterdayProfitLossTimestamp = leg.ProfitLossTimestamp;
                                    dc.UpdateLegYesterdayProfitLoss (leg.Id, (decimal?) leg.YesterdayProfitLoss, leg.YesterdayProfitLossTimestamp);
                                }
                            }
                        }
 
                        leg.StatusChanged += new LegData.StatusChangeHandler (t.LegStatusChangeHandler);
                        t.m_Legs.Add (leg);

                        leg.SignalTrade (legcolPREMIUM);
                    }

                    /* May need to update the database, moving current ProfitLoss to YesterdayProfitLoss
                      * --------------------------------------------------------------------------------- */
                     
                    if (t.TotalProfitLossTimestamp != null)
                    {
                        if (((DateTime) t.TotalProfitLossTimestamp).Day != DateTime.Now.Day)
                        {
                            t.YesterdayProfitLoss = t.TotalProfitLoss;
                            t.YesterdayProfitLossTimestamp = t.TotalProfitLossTimestamp;
                            dc.UpdateTradeYesterdayProfitLoss (t.Id, (decimal?) t.YesterdayProfitLoss, t.YesterdayProfitLossTimestamp);
                        }
                    }
                }

                /* May need to update the database, moving current ProfitLoss to YesterdayProfitLoss
                 * --------------------------------------------------------------------------------- */

                foreach (var trade in m_Trades)
                {
                    DateTime now = DateTime.Now;
                    if (trade.TotalProfitLossTimestamp != null)
                    {
                        if (((DateTime) trade.TotalProfitLossTimestamp).Day != now.Day)
                        {
                            trade.YesterdayProfitLoss = trade.TotalProfitLoss;
                            trade.YesterdayProfitLossTimestamp = trade.TotalProfitLossTimestamp;
                            dc.UpdateLegYesterdayProfitLoss (trade.Id, (decimal?) trade.YesterdayProfitLoss, trade.YesterdayProfitLossTimestamp);
                        }
                    }
                }

                dgvTrade.AutoGenerateColumns = false;
                dgvTrade.DataSource = m_Trades;

               /* Highlighting...
                * --------------- */
                
                foreach (DataGridViewRow r in dgvTrade.Rows)
                {
                    hlTradeProfitLoss (r);
                }
            }

            /* Update overall delta
             * -------------------- */

            UpdateOverallDelta ();
            UpdateOverallTheta ();
        }

        /***************************************************************
         * 
         * Highlight Trade Profit/Loss
         * 
         * ***********************************************************/

        private void hlTradeProfitLoss (DataGridViewRow r)
        {
            TradeData trade = r.DataBoundItem as TradeData;

            if (trade != null)
            {

                if (trade.TotalProfitLoss != null)
                {
                    if (trade.TotalProfitLoss < 0.0)
                    {
                        r.Cells[trdcolTOTALPROFIT].Style.BackColor = Utils.colPLRED;
                    }
                    else
                    {
                        r.Cells[trdcolTOTALPROFIT].Style.BackColor = Utils.colPLGREEN;
                    }
                }

                if (trade.DailyProfitLoss != null)
                {
                    if (trade.DailyProfitLoss < 0.0)
                    {
                        r.Cells[trdcolDAILYPROFIT].Style.BackColor = Utils.colPLRED;
                    }
                    else
                    {
                        r.Cells[trdcolDAILYPROFIT].Style.BackColor = Utils.colPLGREEN;
                    }
                }
            }
        }

        private void dgvTrade_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                TradeColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvTrade.Columns.Count; i++)
                {
                    TradeColumnWidths.Add (dgvTrade.Columns[i].Width.ToString ());
                }
            }
        }

        /******************************************************************
         * 
         * When current row in Trades has changed
         * 
         * ***************************************************************/
        private void dgvTrade_RowEnter (object sender, DataGridViewCellEventArgs e)
        {
            tbNotes.Text = m_Trades[e.RowIndex].Notes;

            double? pt = m_Trades[e.RowIndex].ProfitThreshold;
            if (pt == null)
            {
                tbTradeProfitThreshold.Text = "";
            }
            else
            {
                tbTradeProfitThreshold.Text = ((double) m_Trades[e.RowIndex].ProfitThreshold).ToString ("C2");
            }

            pt = m_Trades[e.RowIndex].PriceThreshold;
            if (pt == null)
            {
                tbTradePriceLevel.Text = "";
            }
            else
            {
                tbTradePriceLevel.Text = ((double) m_Trades[e.RowIndex].PriceThreshold).ToString ("C2");
            }
            lbEnTradeAboveBelow.SelectedIndex = m_Trades[e.RowIndex].PriceThresholdAboveBelow ? 0 : 1;

            ShowTradeEmailNotifications (m_Trades[e.RowIndex].EmailNotifications);
            m_bTradeProfitThresholdDirty = false;
            m_bTradePriceThresholdDirty = false;
            m_bTradeEmailNotificationsDirty = false;
            m_bNotesDirty = false;

            LoadLegsGrid (m_Trades[e.RowIndex]);
            if (m_Trades[e.RowIndex].m_Legs.Count > 0)
            {
                ShowLegEmailNotifications (m_Trades[e.RowIndex].m_Legs[0].EmailNotifications);
            }
        }

        /******************************************************************
         * 
         * If notes has changed, remember
         * 
         * ***************************************************************/

        private void tbNotes_TextChanged (object sender, EventArgs e)
        {
            m_bNotesDirty = true;
        }

        /******************************************************************
         * 
         * If Notes has changed
         * 
         * ***************************************************************/

        private void tbNotes_Leave (object sender, EventArgs e)
        {
            if (m_bNotesDirty)
            {
                if (dgvTrade.CurrentRow.IsNewRow)
                {
                    return;
                }

                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
                tr.Notes = tbNotes.Text;

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var trade = (from t in dc.Trades
                                 where t.TradeId == tr.Id
                                 select t).Single ();
                    trade.Notes = tr.Notes;
                    dc.SubmitChanges ();
                }
            }
        }

        /******************************************************************
        * 
        * If profit threshold has changed, remember
        * 
        * ***************************************************************/

        private void tbTradeProfitThreshold_TextChanged (object sender, EventArgs e)
        {
            m_bTradeProfitThresholdDirty = true;
        }

        private void tbTradeProfitThreshold_Leave (object sender, EventArgs e)
        {
            if (dgvTrade.CurrentRow.IsNewRow)
            {
                return;
            }

            if (m_bTradeProfitThresholdDirty)
            {
                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];

                double pt;
                string text = tbTradeProfitThreshold.Text.TrimStart (new char[] { ' ', '$' });
                if (double.TryParse (text, out pt))
                {
                    tr.ProfitThreshold = pt;
                }
                else
                {
                    tr.ProfitThreshold = null;
                }

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var trade = (from t in dc.Trades
                                    where t.TradeId == tr.Id
                                    select t).Single ();
                    trade.ProfitThreshold = (decimal?) tr.ProfitThreshold;
                    dc.SubmitChanges ();
                }
            }
        }

        /******************************************************************
        * 
        * If price rises above or below a price threshold, remember
        * 
        * ***************************************************************/
        private void tbEnTradePriceLevel_TextChanged (object sender, EventArgs e)
        {
            m_bTradePriceThresholdDirty = true;

        }

        private void tbEnTradePriceLevel_Leave (object sender, EventArgs e)
        {
            if (dgvTrade.CurrentRow.IsNewRow)
            {
                return;
            }

            if (m_bTradePriceThresholdDirty)
            {
                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];

                double pt;
                string text = tbTradePriceLevel.Text.TrimStart (new char[] { ' ', '$' });
                if (double.TryParse (text, out pt))
                {
                    tr.PriceThreshold = pt;
                }
                else
                {
                    tr.PriceThreshold = null;
                }

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var trade = (from t in dc.Trades
                                    where t.TradeId == tr.Id
                                    select t).Single ();
                    trade.PriceThreshold = (decimal?) tr.PriceThreshold;
                    trade.PriceThresholdAboveBelow = tr.PriceThresholdAboveBelow;
                    dc.SubmitChanges ();
                }
            }
        }

        /*************************************************************
         * 
         * User has changed Above/Below price threshold
         * 
         * **********************************************************/

        //private void lbEnTradeAboveBelow_SelectedIndexChanged (object sender, EventArgs e)
        //{
        //    if (dgvTrade.CurrentRow != null)
        //    {
        //        TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
        //        tr.PriceThresholdAboveBelow = (lbEnTradeAboveBelow.SelectedIndex == 0);
        //        using (dbOptionsDataContext dc = new dbOptionsDataContext ())
        //        {
        //            var trade = (from t in dc.Trades
        //                         where t.TradeId == tr.Id
        //                         select t).Single ();
        //            trade.PriceThresholdAboveBelow = tr.PriceThresholdAboveBelow;
        //            dc.SubmitChanges ();
        //        }
        //    }
        //}

        private void lbEnTradeAboveBelow_Enter (object sender, EventArgs e)
        {
            if (dgvTrade.CurrentRow != null)
            {
                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
                lbEnTradeAboveBelow.SelectedIndex = tr.PriceThresholdAboveBelow ? 0 : 1;
            }
        }

        private void lbEnTradeAboveBelow_Leave (object sender, EventArgs e)
        {
            if (dgvTrade.CurrentRow != null)
            {
                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
                tr.PriceThresholdAboveBelow = (lbEnTradeAboveBelow.SelectedIndex == 0);
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var trade = (from t in dc.Trades
                                 where t.TradeId == tr.Id
                                 select t).Single ();
                    trade.PriceThresholdAboveBelow = tr.PriceThresholdAboveBelow;
                    dc.SubmitChanges ();
                }
            }

        }


 
        /*******************************************************
         * 
         * ShowEmailNotifications
         * 
         * ****************************************************/

        void ShowTradeEmailNotifications (int notifications)
        {
            //cbEnITM.Checked = false;
            //if ((notifications & enITM) == enITM)
            //{
            //    cbEnITM.Checked = true;
            //}
            //cbEnNEAR_ITM.Checked = false;
            //if ((notifications & enNEAR_ITM) == enNEAR_ITM)
            //{
            //    cbEnNEAR_ITM.Checked = true;
            //}
            cbEnTradePROFITABLE.Checked = false;
            if ((notifications & Utils.enPROFITABLE) == Utils.enPROFITABLE)
            {
                cbEnTradePROFITABLE.Checked = true;
            }
            cbEnTradePRICE.Checked = false;
            if ((notifications & Utils.enPRICE) == Utils.enPRICE)
            {
                cbEnTradePRICE.Checked = true;
            }
            cbEnTradeLOSSES2PREMIUM.Checked = false;
            if ((notifications & Utils.enLOSSES2PREMIUM) == Utils.enLOSSES2PREMIUM)
            {
                cbEnTradeLOSSES2PREMIUM.Checked = true;
            }
        }

        void ShowLegEmailNotifications (int notifications)
        {
            cbEnLegITM.Checked = false;
            if ((notifications & Utils.enITM) == Utils.enITM)
            {
                cbEnLegITM.Checked = true;
            }
            cbEnLegNEAR_ITM.Checked = false;
            if ((notifications & Utils.enNEAR_ITM) == Utils.enNEAR_ITM)
            {
                cbEnLegNEAR_ITM.Checked = true;
            }
            cbEnLegPROFITABLE.Checked = false;
            if ((notifications & Utils.enPROFITABLE) == Utils.enPROFITABLE)
            {
                cbEnLegPROFITABLE.Checked = true;
            }
        }

        /*********************************************
        * 
        * Checkbox notification changed (clicked)
        * 
        * ******************************************/

        private void cbEnTradePROFITABLE_Click (object sender, EventArgs e)
        {
            m_bTradeEmailNotificationsDirty = true;
        }

        private void cbEnTradePRICE_Click (object sender, EventArgs e)
        {
            m_bTradeEmailNotificationsDirty = true;
        }

        private void cbEnTradeLOSSES2PREMIUM_Click (object sender, EventArgs e)
        {
            m_bTradeEmailNotificationsDirty = true;
        }


        /*********************************************
         * 
         * Checkbox notification changed
         * 
         * ******************************************/

        private void cbEn_Clicked (object sender, EventArgs e)
        {
            m_bLegEmailNotificationsDirty = true;
        }

        /*********************************************
         * 
         * Leaving leg checkbox area
         * 
         * ******************************************/

        private void gbLegNotifications_Leave (object sender, EventArgs e)
        {
            if (m_bLegEmailNotificationsDirty)
            {
                UpdateLegEmailNotifications ();
            }
        }

        /**********************************************
         * 
         * Leaving trade profit checkbox area
         * 
         * *******************************************/

        private void RecordTradeCheckBoxes (object sender, EventArgs e)
        {
            if (dgvTrade.CurrentRow.IsNewRow)
            {
                return;
            }

            if (m_bTradeEmailNotificationsDirty)
            {
                int n = 0;

                if (cbEnTradePROFITABLE.Checked)
                {
                    n |= Utils.enPROFITABLE;
                }
                if (cbEnTradePRICE.Checked)
                {
                    n |= Utils.enPRICE;
                }
                if (cbEnTradeLOSSES2PREMIUM.Checked)
                {
                    n |= Utils.enLOSSES2PREMIUM;
                }

                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
                tr.EmailNotifications = n;

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var trade = (from t in dc.Trades
                                 where t.TradeId == tr.Id
                                 select t).Single ();
                    trade.EmailNotifications = n;
                    dc.SubmitChanges ();
                }
            }
        }
 

         /**********************************************
         * 
         * Leaving leg checkbox area
         * 
         * *******************************************/
       private void UpdateLegEmailNotifications ()
        {
           if (dgvLeg.CurrentRow == null)
           {
               return;
           }
           if (dgvTrade.CurrentRow == null)
           {
               return;
           }
           if (dgvLeg.CurrentRow.IsNewRow)
           {
               return;
           }

           //if (m_bLegEmailNotificationsDirty)
            {
                int n = 0;
                if (cbEnLegITM.Checked)
                {
                    n |= Utils.enITM;
                }
                if (cbEnLegNEAR_ITM.Checked)
                {
                    n |= Utils.enNEAR_ITM;
                }
                if (cbEnLegPROFITABLE.Checked)
                {
                    n |= Utils.enPROFITABLE;
                }

               if (dgvTrade.CurrentRow.IsNewRow)
               {
                   return;
               }
               TradeData trade = m_Trades[dgvTrade.CurrentRow.Index];
               trade.m_Legs[dgvLeg.CurrentRow.Index].EmailNotifications = n;

                m_Trades[dgvTrade.CurrentRow.Index].EmailNotifications = n;
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var leg = (from t in dc.Legs
                               where t.Id == trade.m_Legs[dgvLeg.CurrentRow.Index].Id
                                 select t).Single ();
                    leg.EmailNotifications = n;
                    dc.SubmitChanges ();
                }
            }
        }

        /*******************************************
         * 
         * Leg ProfitThreshold has changed
         * 
         * ****************************************/

       private void tbLegProfitThreshold_TextChanged (object sender, EventArgs e)
       {
            m_bLegProfitThresholdDirty = true;
       }

       /*******************************************
         * 
         * leaving Leg ProfitThreshold
         * 
         * ****************************************/

       private void tbLegProfitThreshold_Leave (object sender, EventArgs e)
       {
            if (dgvLeg.CurrentRow.IsNewRow)
            {
                return;
            }

            if (m_bLegProfitThresholdDirty)
            {
                if (dgvTrade.CurrentRow.IsNewRow)
                {
                    return;
                }

                TradeData tr = m_Trades[dgvTrade.CurrentRow.Index];
                LegData l = tr.m_Legs[dgvLeg.CurrentRow.Index];

                double pt;
                if (double.TryParse (tbLegProfitThreshold.Text, out pt))
                {
                    l.ProfitThreshold = pt;

                    using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                    {
                        var leg = (from t in dc.Legs
                                   where t.Id == l.Id
                                   select t).Single ();
                        leg.ProfitThreshold = (decimal?) pt;
                        dc.SubmitChanges ();
                    }
                }
            }
       }


        /**************************************************************
         * 
         * Save to DB
         * 
         * ***********************************************************/

        private void btnTradeSave_Click (object sender, EventArgs e)
        {
            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                foreach (TradeData td in m_Trades)
                {
                    dc.UpsertTrade (td.Id,
                                    m_DataGroupName,
                                    td.TradeType,
                                    td.Ticker,
                                    (int) td.OpenCloseStatus,
                                    td.Premium,
                                    td.Commissions,
                                    td.OpenDate,
                                    td.ClosedDate,
                                    td.TotalDelta,
                                    td.TotalTheta,
                                    (decimal?) td.TotalProfitLoss,
                                    //(decimal?) td.DailyProfitLoss,
                                    (decimal?) td.ProfitThreshold,
                                    td.EmailNotifications,
                                    td.Notes,
                                    td.LastEmail);
                }
            }
        }

        /********************************************************
         * 
         * Explicitly reload trade grid
         * 
         * *****************************************************/

        private void btnTradeFromDB_Click (object sender, EventArgs e)
        {
            LoadTradeGrid ();
        }

        /******************************************************
         * 
         * Delete selected trades
         * 
         * ***************************************************/

        private void btnTradeDelete_Click (object sender, EventArgs e)
        {
            if (cbActivateSelected.Checked || cbActivateAll.Checked)
            {
                MessageBox.Show ("Cannot delete a trade while actively collecting data");
                return;
            }

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                foreach (DataGridViewRow row in dgvTrade.SelectedRows)
                {
                    DialogResult rc = MessageBox.Show (string.Format ("Confirm deletion of trade involving {0} is OK", m_Trades[row.Index].Ticker), "Deleting trade", MessageBoxButtons.OKCancel);
                    if (rc != DialogResult.OK)
                    {
                        continue;
                    }
                    var legs = (from l in dc.Legs
                                where l.Trade_Id == m_Trades[row.Index].Id
                                select l    
                                ).ToList ();
                    foreach (var leg in legs)
                    {
                        dc.Legs.DeleteOnSubmit (leg);
                    }
                    var trade = (from t in dc.Trades
                                 where t.TradeId == m_Trades[row.Index].Id
                                 select t
                                ).Single ();
                    dc.Trades.DeleteOnSubmit (trade);
                }
                dc.SubmitChanges ();
            }
            LoadTradeGrid ();
        }

        /**********************************************************
         * 
         * Load Legs Grid
         * 
         * *******************************************************/

        void LoadLegsGrid (TradeData trade)
        {
            dgvLeg.AutoGenerateColumns = false;
            
            dgvLeg.DataSource = trade.m_Legs;

            foreach (DataGridViewRow r in dgvLeg.Rows)
            {
                if (!r.IsNewRow)
                {
                    LegData leg = trade.m_Legs[r.Index];

                    if (cbOpenPosOnly.Checked)
                    {
                        if (leg.OpenCloseStatus != OpenCloseValues.Open)
                        {
                            if (dgvLeg.CurrentRow == r)
                            {
                                dgvLeg.CurrentCell = dgvLeg[dgvLeg.CurrentCell.ColumnIndex, r.Index + 1];
                            }
                            r.Visible = false;
                            continue;
                        }
                    }

                    r.Visible = true;

                    /* Use appropriate highlight
                     * ------------------------- */

                    hlLegProbabilityITM (trade, leg);

                    hlLegProfitLoss (r, leg);
                }
            }
        }

        /*****************************************************
         * 
         * Highlight appropriate Leg ProfitLoss column
         * 
         * **************************************************/

        private void hlLegProfitLoss (DataGridViewRow row, LegData leg)
        {
            if (leg.TotalProfitLoss != null)
            {
                if (leg.TotalProfitLoss < 0.0)
                {
                    row.Cells[legcolTOTALPROFIT].Style.BackColor = Utils.colPLRED;
                }
                else
                {
                    row.Cells[legcolTOTALPROFIT].Style.BackColor = Utils.colPLGREEN;
                }
                dgvLeg.InvalidateCell (row.Cells[legcolTOTALPROFIT]);

            }

            if (leg.DailyProfitLoss != null)
            {
                if (leg.DailyProfitLoss < 0.0)
                {
                    row.Cells[legcolDAILYPROFIT].Style.BackColor = Utils.colPLRED;
                }
                else
                {
                    row.Cells[legcolDAILYPROFIT].Style.BackColor = Utils.colPLGREEN;
                }
                dgvLeg.InvalidateCell (row.Cells[legcolDAILYPROFIT]);
            }

        }
        /**************************************************************
         * 
         * User is attempting to delete a leg row
         * 
         * ***********************************************************/

        private void dgvLeg_UserDeletingRow (object sender, DataGridViewRowCancelEventArgs e)
        {
            if (cbActivateSelected.Checked || cbActivateAll.Checked)
            {
                MessageBox.Show ("Cannot delete a leg while actively collecting data");
                e.Cancel = true;
                return;
            }

            TradeData trade = null;

            if (!dgvTrade.CurrentRow.IsNewRow)
            {
                trade = m_Trades[dgvTrade.CurrentRow.Index];
            }

            int index = e.Row.Index;
            LegData leg = trade.m_Legs[index];
            DialogResult rc = MessageBox.Show (string.Format ("Are you sure you want to delete leg {0} {1} {2} {3} contracts at strike {4}?", leg.Ticker, leg.DisplayCall (), leg.IfSell ? "Sell" : "Buy", leg.NoContracts, leg.Strike), "Confirm Leg Deletion", MessageBoxButtons.YesNo);
            if (rc != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            if (trade.m_Legs[index].Id == null)
            {
                // Nothing to delete. Probably was editing this line and changed my mind
                return;
            }
            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                var dataleg = (from l in dc.Legs
                           where l.Id == trade.m_Legs[index].Id
                           select l).Single ();
                dc.Legs.DeleteOnSubmit (dataleg);
                dc.SubmitChanges ();

                //var leg = (from r in trade.m_Legs
                //           where r.Id == dataleg.Id
                //           select r).Single ();
            }
        }

        /**************************************************************
         * 
         * User is attempting to delete a trade row
         * 
         * ***********************************************************/

        private void dgvTrade_UserDeletingRow (object sender, DataGridViewRowCancelEventArgs e)
        {
            if (cbActivateSelected.Checked || cbActivateAll.Checked)
            {
                MessageBox.Show ("Cannot delete a trade while actively collecting data");
                return;
            }

            int index = e.Row.Index;
            DialogResult rc = MessageBox.Show (string.Format ("Are you sure you want to delete trade {0} and all legs?", m_Trades[index].Ticker), "Confirm Trade Deletion", MessageBoxButtons.YesNo);
            if (rc != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                var legs = (from l in dc.Legs
                            where l.Trade_Id == m_Trades[index].Id
                            select l
                            ).ToList ();
                foreach (var leg in legs)
                {
                    dc.Legs.DeleteOnSubmit (leg);
                }
                var trade = (from t in dc.Trades
                             where t.TradeId == m_Trades[index].Id
                             select t
                            ).Single ();
                dc.Trades.DeleteOnSubmit (trade);
                dc.SubmitChanges ();
            }
        }

        /**************************************************************
          * 
          * User has deleted a trade row
          * 
          * ***********************************************************/

        private void dgvTrade_UserDeletedRow (object sender, DataGridViewRowEventArgs e)
        {
            LoadTradeGrid ();
        }

        /*****************************************************
         * 
         * Activate selected trades
         * 
         * **************************************************/
        private void cbActivateSelected_CheckedChanged (object sender, EventArgs e)
        {
            if (cbActivateAll.Checked)
            {
                cbActivateAll.Checked = false;
                cbActivateAll_CheckedChanged (null, null); // turn off other button
            }

            if (cbActivateSelected.Checked)
            {
                cbActivateSelected.ForeColor = Color.White;

                ib.evContractDetails += Ib_evContractDetails;
                ib.evContractDetailsEnd += Ib_evContractDetailsEnd;

                ib.evTickEFP += Ib_evTickEFP;
                ib.evTickGeneric += Ib_evTickGeneric;
                ib.evTickOptionComputation += Ib_evTickOptionComputation;
                ib.evTickPrice += Ib_evTickPrice;
                ib.evTickSize += Ib_evTickSize;
                //axTws.tickSnapshotEnd += axTws_tickSnapshotEnd;
                ib.evTickString += Ib_evTickString;

                timerLeg.Enabled = true;

                m_ActiveLegs = new List<LegData> ();

                foreach (DataGridViewRow r in dgvTrade.SelectedRows)
                {
                    ActivateTrade (m_Trades[r.Index]);
                }
            }
            else
            {
                cbActivateSelected.ForeColor = Color.Black;

                ib.evContractDetails -= Ib_evContractDetails;
                ib.evContractDetailsEnd -= Ib_evContractDetailsEnd;

                ib.evTickEFP -= Ib_evTickEFP;
                ib.evTickGeneric -= Ib_evTickGeneric;
                ib.evTickOptionComputation -= Ib_evTickOptionComputation;
                ib.evTickPrice -= Ib_evTickPrice;
                ib.evTickSize -= Ib_evTickSize;
                //axTws.tickSnapshotEnd -= axTws_tickSnapshotEnd;
                ib.evTickString -= Ib_evTickString;

                timerLeg.Enabled = false;

                SavePriceProfitGreeks ();

                for (int i = 0; i < m_ActiveLegs.Count; i++)
                {
                    if (m_ActiveLegs[i].bIfUpdatingMarketData)
                    {
                        ib.ClientSocket.cancelMktData (i | Utils.ibLEG);
                        //axTws.cancelMktData (i | Utils.ibLEG);
                        m_ActiveLegs[i].bIfUpdatingMarketData = false;
                    }
                }
                m_ActiveLegs = null;
            }
        }


        /*****************************************************
       * 
       * Activate all trades
       * 
       * **************************************************/

        private void cbActivateAll_CheckedChanged (object sender, EventArgs e)
        {
            if (cbActivateSelected.Checked)
            {
                cbActivateSelected.Checked = false;
                cbActivateSelected_CheckedChanged (null, null); //turn off other button
            }

            if (cbActivateAll.Checked)
            {
                //cbActivateAll.BackColor = Utils.colDKBLUE;
                cbActivateAll.ForeColor = Color.White;

                ib.evContractDetails += Ib_evContractDetails;
                ib.evContractDetailsEnd += Ib_evContractDetailsEnd;

                ib.evTickEFP += Ib_evTickEFP;
                ib.evTickGeneric += Ib_evTickGeneric;
                ib.evTickOptionComputation += Ib_evTickOptionComputation;
                ib.evTickPrice += Ib_evTickPrice;
                ib.evTickSize += Ib_evTickSize;
                //axTws.tickSnapshotEnd += axTws_tickSnapshotEnd;
                ib.evTickString += Ib_evTickString;

                timerLeg.Enabled = true;

                timerLeg.Enabled = true;

                m_ActiveLegs = new List<LegData> ();

                foreach (var t in m_Trades)
                {
                    ActivateTrade (t);
                }
            }
            else
            {
                //cbActivateAll.BackColor = Color.Transparent;
                cbActivateAll.ForeColor = Color.Black;

                ib.evContractDetails -= Ib_evContractDetails;
                ib.evContractDetailsEnd -= Ib_evContractDetailsEnd;

                ib.evTickEFP -= Ib_evTickEFP;
                ib.evTickGeneric -= Ib_evTickGeneric;
                ib.evTickOptionComputation -= Ib_evTickOptionComputation;
                ib.evTickPrice -= Ib_evTickPrice;
                ib.evTickSize -= Ib_evTickSize;
                //axTws.tickSnapshotEnd -= axTws_tickSnapshotEnd;
                ib.evTickString -= Ib_evTickString;

                timerLeg.Enabled = false;

                SavePriceProfitGreeks ();

                for (int i = 0; i < m_ActiveLegs.Count; i++)
                {
                    if (m_ActiveLegs[i].bIfUpdatingMarketData)
                    {
                        ib.ClientSocket.cancelMktData (i | Utils.ibLEG);
//                        axTws.cancelMktData (i | Utils.ibLEG);
                        m_ActiveLegs[i].bIfUpdatingMarketData = false;
                    }
                }
                m_ActiveLegs = null;
            }
        }
 
        /*****************************************************
        * 
        * Activate a specific trade trades
        * 
        * **************************************************/

        private void ActivateTrade (TradeData trade)
        {
            foreach (var leg in trade.m_Legs)
            {
                if (string.IsNullOrEmpty (leg.LocalSymbol))
                {
                    Contract contract = new Contract ();
                    //TWSLib.IContract contract = axTws.createContract ();

                    if (leg.Equity == EquityType.Option)
                    {
                        if (leg.Strike == null)
                        {
                            continue;
                        }

                        contract.Symbol = Utils.Massage (leg.Ticker);
                        contract.SecType = "OPT";
                        contract.LastTradeDateOrContractMonth = ((DateTime) leg.ExpiryDate).ToString ("yyyyMMdd");
                        contract.Strike = (double) leg.Strike;
                        contract.Right = (bool) leg.IfCall ? "C" : "P";
                        contract.Multiplier = "";
                        contract.Exchange = "SMART";
                        contract.PrimaryExch = "";
                        contract.Currency = "USD";
                        contract.LocalSymbol = "";
                        contract.IncludeExpired = false;
                    }
                    else if (leg.Equity == EquityType.Stock)
                    {
                        contract.SecType = "STK";
                        contract.Exchange = "SMART";
                        contract.PrimaryExch = "";
                        contract.Currency = "USD";
                        contract.LocalSymbol = "";
                        contract.IncludeExpired = false;
                    }
                    else if (leg.Equity == EquityType.Future)
                    {
                        contract.SecType = "FUT";
                        contract.Exchange = leg.Exchange;
                        contract.Currency = "USD";
                        contract.LocalSymbol = leg.LocalSymbol;
                        contract.IncludeExpired = false;
                    }
                    else if (leg.Equity == EquityType.Index)
                    {
                        contract.SecType = "IND";
                        contract.Exchange = leg.Exchange;
                        contract.Currency = "USD";
                        contract.LocalSymbol = leg.LocalSymbol;
                        contract.IncludeExpired = false;
                    }
                    else if (leg.Equity == EquityType.FutOpt)
                    {
                        contract.SecType = "FOP";
                        contract.Exchange = leg.Exchange;
                        contract.Currency = "USD";
                        contract.LocalSymbol = leg.LocalSymbol;
                        //contract.expiry = ((DateTime) leg.ExpiryDate).ToString ("yyyyMM");
                        //contract.strike = (double) leg.Strike;
                        //contract.right = (bool) leg.IfCall ? "C" : "P";
                        contract.IncludeExpired = false;
                    }

                    m_Log.Log (ErrorLevel.logDEB, string.Format ("Getting option for {0}, {1} strike: {2}", leg.Ticker, leg.DisplayCall (), leg.Strike));
                    m_ActiveLegs.Add (leg);

                    ib.ClientSocket.reqContractDetails ((m_ActiveLegs.Count - 1) | Utils.ibLEG, contract);
                    //axTws.reqContractDetailsEx ((m_ActiveLegs.Count - 1) | Utils.ibLEG, contract);
                }
                else
                {
                    m_ActiveLegs.Add (leg);
                    if (leg.OpenCloseStatus != OpenCloseValues.Close)
                    {
                        FetchLegMarketData (m_ActiveLegs.Count - 1);
                    }
                }
            }
        }

        /********************************************************************
         * 
         * FetchLegMarketData
         * 
         * *****************************************************************/

        private void FetchLegMarketData (int index)
        {
            ib.ClientSocket.reqMarketDataType (cbTradeFrozenData.Checked ? 2 : 1);
            //axTws.reqMarketDataType (cbTradeFrozenData.Checked ? 2 : 1);

            Contract contract = new Contract ();
            //TWSLib.IContract contract = axTws.createContract ();

            LegData leg = m_ActiveLegs[index];

            contract.Symbol = "";
            if (leg.Equity == EquityType.Stock)
            {
                contract.SecType = "STK";
                contract.Symbol = leg.Ticker;
                contract.Currency = "USD";
                contract.Exchange = leg.Exchange;
                //contract.conId = (int) leg.ConId;
                contract.LocalSymbol = leg.LocalSymbol;
            }
            else if (leg.Equity == EquityType.Option)
            {
                contract.SecType = "OPT";
                contract.Exchange = "SMART";
                //contract.conId = (int) leg.ConId;
                contract.LocalSymbol = leg.LocalSymbol;
            }
            else if (leg.Equity == EquityType.Future)
            {
                contract.SecType = "FUT";
                contract.Exchange = leg.Exchange;
                contract.Currency = "USD";
                contract.LocalSymbol = leg.LocalSymbol;
            }
            else if (leg.Equity == EquityType.FutOpt)
            {
                contract.SecType = "FOP";
                contract.Exchange = leg.Exchange;
                contract.Currency = "USD";
                contract.LocalSymbol = leg.LocalSymbol;
            }
            else if (leg.Equity == EquityType.Index)
            {
                contract.SecType = "IND";
                contract.Exchange = leg.Exchange;
                contract.Currency = "USD";
                contract.LocalSymbol = leg.LocalSymbol;
            }
            else
            {
                throw new Exception (string.Format ("One of the legs [{0}] has a bad EquityType.", leg.Ticker));
            }

            m_Log.Log (ErrorLevel.logDEB, string.Format ("reqMktDataEx index: {0} equitytype:{6} localSymbol:[{1}] Ticker:{2} C/P:{3} S/B:{4} strike:{5:N2}", 
                                                        index.ToString (), 
                                                        contract.LocalSymbol, 
                                                        leg.Ticker, 
                                                        leg.DisplayCall (), 
                                                        leg.IfSell ? "Sell" : "Buy", 
                                                        leg.Strike,
                                                        Utils.EquityType2String (leg.Equity)));

            leg.bIfUpdatingMarketData = true;

            if (cbTradeSnapshot.Checked)
            {
                ib.ClientSocket.reqMktData (Utils.ibLEG | index, contract, string.Empty, true, false, null);
                //axTws.reqMktDataEx (Utils.ibLEG | index, contract, "", 1, null);
            }
            else
            {
                ib.ClientSocket.reqMktData (Utils.ibLEG | index, contract, string.Empty, false, false, null);
                //axTws.reqMktDataEx (Utils.ibLEG | index, contract, "106", 0, null);
            }
        }

        /**************************************************************
          * 
          * Timer fired, so save last ProfitLoss and timestamp
          * 
          * ***********************************************************/

        private void timerLeg_Tick (object sender, EventArgs e)
        {
            SavePriceProfitGreeks ();
        }

        private void SavePriceProfitGreeks ()
        {
            UpdateOverallDelta ();
            UpdateOverallTheta ();

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                foreach (var trade in m_Trades)
                {
                    if (trade.OpenCloseStatus != OpenCloseValues.Close)
                    {
                        dc.UpdateTradeProfitLoss (trade.Id, (decimal?) trade.TotalProfitLoss, trade.TotalProfitLossTimestamp, (decimal?) trade.DailyProfitLoss, trade.DailyProfitLossTimestamp);
                    }

                    if (trade.UnderlyingPrice != null)
                    {
                        dc.ExecuteCommand ("UPDATE Stock SET LastTrade = {0:F2} WHERE Ticker = {1}", (double) trade.UnderlyingPrice, trade.Ticker);
                    }
                    foreach (var leg in trade.m_Legs)
                    {
                        if (leg.OpenCloseStatus == OpenCloseValues.Open)
                        {
                            if (leg.ProfitLossTimestamp != null) // then the profit/loss has been computed
                            {
//                                dc.UpdateLegGreeks (leg.Id, leg.Delta, leg.Theta, leg.Gamma, leg.Vega, leg.MyDelta, leg.MyTheta, leg.MyGamma, leg.MyVega);
                                dc.UpdateLegGreeks (leg.Id, leg.TotalDelta, leg.TotalTheta, null, null, leg.MyDelta, leg.MyTheta, leg.MyGamma, leg.MyVega);
                                dc.UpdateLegUndPrice (leg.Id, (decimal?) leg.UnderlyingPrice);
                                dc.UpdateLegProfitLoss (leg.Id, (decimal?) leg.TotalProfitLoss, leg.ProfitLossTimestamp, (decimal?) leg.DailyProfitLoss, leg.DailyProfitLossTimestamp);
                            }
                        }
                    }
                }
            }
        }

        /*************************************************************
         * 
         * UpdateOverallDelta
         * 
         * **********************************************************/

        private void UpdateOverallDelta ()
        {
            double? delta = 0;
            foreach (var trade in m_Trades)
            {
                foreach (var leg in trade.m_Legs)
                {
                    if (leg.OpenCloseStatus != OpenCloseValues.Close)
                    {
                        delta += leg.TotalDelta;
                    }
                }
            }

            if (delta == null)
            {
                lbOverallDelta.Text = "---";
            }
            else
            {
                lbOverallDelta.Text = ((double) delta).ToString ("F2");
            }
        }

        /*************************************************************
         * 
         * UpdateOverallTheta
         * 
         * **********************************************************/

        private void UpdateOverallTheta ()
        {
            double? theta = 0;
            foreach (var trade in m_Trades)
            {
                foreach (var leg in trade.m_Legs)
                {
                    if (leg.OpenCloseStatus != OpenCloseValues.Close)
                    {
                        theta += leg.TotalTheta;
                    }
                }
            }

            if (theta == null)
            {
                lbOverallTheta.Text = "---";
            }
            else
            {
                lbOverallTheta.Text = ((double) theta).ToString ("F2");
            }
        }

        /**************************************************************
         * 
         * One of the legs of the specified trade has changed
         * 
         * One of the legs of this trade has signaled a change
         * 
         * Improvements can be made here to check to see whether the value
         * has changed before invalidating the cell.
         * 
         * ***********************************************************/

        internal void LegStatusChanged (TradeData trade, LegData leg, int col)
        {
            //m_Log.Log (ErrorLevel.logDEB, string.Format ("LegStatusChanged for trade {0}, leg {1} {2} {3}, column {4}", trade.Ticker, leg.Ticker, leg.IfCall ? "Call" : "Put", leg.Strike, col));
            switch (col)
            {
                case legcolUNDPRICE:
                    if (trade.UnderlyingPrice == leg.UnderlyingPrice)
                    {
                        break;
                    }

                    trade.UnderlyingPrice = leg.UnderlyingPrice;
                    foreach (DataGridViewRow r in dgvTrade.Rows)
                    {
                        if (trade == (TradeData) r.DataBoundItem)
                        {
                            dgvTrade.InvalidateCell (r.Cells[trdcolUNDPRICE]);


                            /* Check price threshold crossed
                            * ------------------------------ */

                            if ((trade.EmailNotifications & Utils.enPRICE) == Utils.enPRICE)
                            {
                                if ( (trade.PriceThresholdAboveBelow && (trade.UnderlyingPrice > trade.PriceThreshold))
                                     ||
                                     (!trade.PriceThresholdAboveBelow && (trade.UnderlyingPrice < trade.PriceThreshold))
                                   )
                                { 
                                    string note = string.Format ("Trade {0} price is now {1} {2:C2}. Currently trading at {3:C2}", trade.Ticker, trade.PriceThresholdAboveBelow ? "above" : "below", trade.PriceThreshold, trade.UnderlyingPrice);

                                    trade.EmailBrian (note, new TimeSpan (1, 0, 0));
                                    //trade.EmailBrian (note, new TimeSpan (0, 5, 0));
                                }
                            }
                            break;
                        }
                    }
                    break;

                case legcolPREMIUM:
                    {
                        trade.ComputePremium ();

                        foreach (DataGridViewRow r in dgvTrade.Rows)
                        {
                            if (trade == (TradeData) r.DataBoundItem)
                            {
                                dgvTrade.InvalidateCell (r.Cells[trdcolPREMIUM]);
                                break;
                            }
                        } 
                    }
                    break;

                case legcolDELTA:
                    {
                        trade.ComputeDelta ();

                        foreach (DataGridViewRow r in dgvTrade.Rows)
                        {
                            if (trade == (TradeData) r.DataBoundItem)
                            {
                                dgvTrade.InvalidateCell (r.Cells[trdcolDELTA]);
                                break;
                            }
                        }
                    }
                    break;

                case legcolTHETA:
                    {
                        trade.ComputeTheta ();
                        //trade.ComputeThetaVegaRatio ();

                        for (int row = 0; row < dgvTrade.Rows.Count; row++)
                        {
                            TradeData tr = (TradeData) dgvTrade.Rows[row].DataBoundItem;
                            if (trade == tr)
                            {
                                dgvTrade.InvalidateCell (trdcolTHETA, row);
//                                dgvTrade.InvalidateCell (trdcolTHETAVEGARATIO, row);
                                break;
                            }
                        }


                    }
                    break;

                //case legcolVEGA:
                //    {
                //        trade.ComputeVega ();
                //        trade.ComputeThetaVegaRatio ();

                //        for (int row = 0; row < dgvTrade.Rows.Count; row++)
                //        {
                //            TradeData tr = (TradeData) dgvTrade.Rows[row].DataBoundItem;
                //            if (trade == tr)
                //            {
                //                dgvTrade.InvalidateCell (trdcolVEGA, row);
                //                dgvTrade.InvalidateCell (trdcolTHETAVEGARATIO, row);
                //                break;
                //            }
                //        }
                //    }
                //    break;

                case legcolTOTALPROFIT:
                    {
                        UpdateTradeTotalAndDailyProfit (trade);

                        /* Update Total Daily Profits
                         * --------------------------*/

                        double total = 0;
                        double daily = 0;
                        foreach (TradeData tr in m_Trades)
                        {
                            if (tr.TotalProfitLoss != null)
                            {
                                total += (double) tr.TotalProfitLoss;
                            }
                            if (tr.DailyProfitLoss != null)
                            {
                                daily += (double) tr.DailyProfitLoss;
                            }
                        }
                        Color c = tbTotalProfitLoss.BackColor;
                        if (total > 0)
                        {
                            if (c != Utils.colPLGREEN)
                            {
                                tbTotalProfitLoss.BackColor = Utils.colPLGREEN;
                            }
                        }
                        else
                        {
                            if (c != Utils.colPLRED)
                            {
                                tbTotalProfitLoss.BackColor = Utils.colPLRED;
                            }
                        }
                        c = tbDailyProfitLoss.BackColor;
                        if (daily > 0)
                        {
                            if (c != Utils.colPLGREEN)
                            {
                                tbDailyProfitLoss.BackColor = Utils.colPLGREEN;
                            }
                        }
                        else
                        {
                            if (c != Utils.colPLRED)
                            {
                                tbDailyProfitLoss.BackColor = Utils.colPLRED;
                            }
                        } 
                        tbTotalProfitLoss.Text = total.ToString ("C0");
                        tbDailyProfitLoss.Text = daily.ToString ("C0");
                    }

                    break;
                default:
                    break;
            }
        }

        /*******************************************************************
         * 
         * UpdateTradeTotalAndDailyProfit
         * 
         * ****************************************************************/

        private void UpdateTradeTotalAndDailyProfit (TradeData trade)
        {
            trade.TotalProfitLoss = 0;
            trade.DailyProfitLoss = 0;

            foreach (var l in trade.m_Legs)
            {
                //if (l.TotalProfitLoss == null)
                //{
                //    l.ComputeProfitFigures (); // really shouldn't be necessary
                //}
                trade.TotalProfitLoss += l.TotalProfitLoss;
                if (l.OpenCloseStatus != OpenCloseValues.Close)
                {
                    trade.DailyProfitLoss += l.DailyProfitLoss;
                }
                trade.TotalProfitLossTimestamp = DateTime.Now;
            }

            for (int i = 0; i < dgvTrade.Rows.Count; i++)
            {
                DataGridViewRow r = dgvTrade.Rows[i];
                if (!r.IsNewRow)
                {
                    if (trade == m_Trades[r.Index])
                    {

                        /* Use appropriate highlight
                         * ------------------------- */

                        if (trade.TotalProfitLoss != null)
                        {
                            if (trade.TotalProfitLoss < 0.0)
                            {
                                r.Cells[trdcolTOTALPROFIT].Style.BackColor = Utils.colPLRED;
                            }
                            else
                            {
                                r.Cells[trdcolTOTALPROFIT].Style.BackColor = Utils.colPLGREEN;
                            }
                        }

                        if (trade.DailyProfitLoss != null)
                        {
                            if (trade.DailyProfitLoss < 0.0)
                            {
                                r.Cells[trdcolDAILYPROFIT].Style.BackColor = Utils.colPLRED;
                            }
                            else
                            {
                                r.Cells[trdcolDAILYPROFIT].Style.BackColor = Utils.colPLGREEN;
                            }
                        }
                        dgvTrade.InvalidateCell (r.Cells[trdcolTOTALPROFIT]);
                        dgvTrade.InvalidateCell (r.Cells[trdcolDAILYPROFIT]);

                        /* Check profit threshold
                         * ---------------------- */

                        if ((trade.EmailNotifications & Utils.enPROFITABLE) == Utils.enPROFITABLE)
                        {
                            if (trade.TotalProfitLoss > trade.ProfitThreshold)
                            {
                                string opendate = "";
                                if (trade.OpenDate != null)
                                {
                                    opendate = ((DateTime) trade.OpenDate).ToLongDateString ();
                                }
                                string note = string.Format ("Trade {0} opened {1} is now profitable!!! {2:C2}", trade.Ticker, opendate, trade.TotalProfitLoss);

                                trade.EmailBrian (note, new TimeSpan (1, 0, 0));
                                //trade.EmailBrian (note, new TimeSpan (0, 5, 0));
                            }
                        }

                        /* Check if losses twice premium
                         * ----------------------------- */

                        if ((trade.EmailNotifications * Utils.enLOSSES2PREMIUM) == Utils.enLOSSES2PREMIUM)
                        {
                            if (-trade.TotalProfitLoss > 2 * trade.Premium)
                            {
                                string note = string.Format ("Trade {0} now has losses {1:N2} that are twice the premium.", trade.Ticker, trade.TotalProfitLoss);

                                trade.EmailBrian (note, new TimeSpan (2, 0, 0));

                            }
                        }
                        break;
                    }
                }
            }
        }

        /*************************************************************
         * 
         * Cell value in the Leg grid is changing
         * 
         * Do we need turn off data acquisition?
         * 
         * ***********************************************************/
        
        private void dgvLeg_CellValidating (object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            LegData ld = (LegData) dgvLeg.Rows[e.RowIndex].DataBoundItem;
            if (ld == null)
            {
                return;
            }

            if (e.ColumnIndex == legcolSTRIKE)
            {
                if (ld.Equity == EquityType.Stock || ld.Equity == EquityType.Future || ld.Equity == EquityType.Index)
                {
                    string v = e.FormattedValue as string;
                    if (v != "")
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    string v = e.FormattedValue as string;
                    if (v == "")
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (e.ColumnIndex == legcolCALLPUT)
            {
                if (ld.Equity == EquityType.Stock || ld.Equity == EquityType.Future)
                {
                    string v = e.FormattedValue as string;
                    if (v != "")
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    string v = e.FormattedValue as string;
                    if (v == "")
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (e.ColumnIndex == legcolOPENPRICE || e.ColumnIndex == legcolCLOSEPRICE)
            {
                double price;
                string value = e.FormattedValue as string;
                value = value.Replace ("$", "");
                if (string.IsNullOrWhiteSpace (value))
                {
                    return;
                }
                if (!double.TryParse (value, out price))
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (e.ColumnIndex == legcolNOCONTRACTS)
            {
                m_OldNoContracts = ld.NoContracts;
            }

            //if (!ld.bIfUpdatingMarketData)
            //{
            //    return;
            //}
            //if (ld.ConId == null)
            //{
            //    return;
            //}

            //if ( (e.ColumnIndex == legcolBUYSELL) ||
            //     (e.ColumnIndex == legcolSTRIKE) ||
            //     (e.ColumnIndex == legcolEXPIRES) ||
            //     (e.ColumnIndex == legcolEQUITYTYPE) ||
            //     (e.ColumnIndex == legcolTICKER))
            //{
            //    MessageBox.Show ("Data acquisition must be turned off before modifying this information.");
            //    e.Cancel = true;
            //}
        }

        /*************************************************************
         * 
         * Cell value in the Leg grid has changed
         * 
         * ***********************************************************/

        private async void dgvLeg_CellValueChanged (object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            LegData leg = (LegData) dgvLeg.Rows[e.RowIndex].DataBoundItem;
            if (leg == null)
            {
                //MessageBox.Show ("Problem in dgvLeg_CellValueChanged. The leg item is undefined. This is unexpected.");
                return;
            }

            if (e.ColumnIndex == legcolEQUITYTYPE)
            {
                leg.ConId = null;
                leg.LocalSymbol = null;

                if (leg.Equity == EquityType.Stock || leg.Equity == EquityType.Future || leg.Equity == EquityType.Index)
                {
                    leg.IfCall = null;
                    leg.Strike = null;
                    leg.ExpiryDate = null;
                    leg.DaysLeft = null;
                    dgvLeg.InvalidateCell (legcolCALLPUT, e.RowIndex);
                    dgvLeg.InvalidateCell (legcolSTRIKE, e.RowIndex);
                    dgvLeg.InvalidateCell (legcolEXPIRES, e.RowIndex);
                    dgvLeg.InvalidateCell (legcolDAYSLEFT, e.RowIndex);

                    if (leg.Equity == EquityType.Future)
                    {
                        using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                        {
                            var st = (from s in dc.Stocks
                                      where s.Ticker == leg.Ticker
                                      select new { s.Ticker, s.FutureExpiry, s.Exchange }).Single ();
                            leg.ExpiryDate = st.FutureExpiry;
                            leg.Exchange = st.Exchange;
                        }
                    }
                }
                else
                {
                    leg.IfCall = false;
                    leg.ExpiryDate = m_RememberDateExpires;
                    leg.DaysLeft = Utils.ComputeDaysToExpire (leg.ExpiryDate);
                }
            }

            m_bLegRowDirtyFlag = true;

            if (e.ColumnIndex == legcolBID || e.ColumnIndex == legcolASK)
            {
                if (dgvLeg.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }
                if (dgvTrade.CurrentRow.IsNewRow)
                {
                    MessageBox.Show ("The current trade line is not yet defined.");
                    return;
                }
                leg.ComputeProfitFigures ();
            }

            if (e.ColumnIndex == legcolCLOSEPRICE)
            {
                LegClosePrice (e.RowIndex);
            }

            if (e.ColumnIndex == legcolEXPIRES)
            {
                m_RememberDateExpires = (DateTime) dgvLeg.Rows[e.RowIndex].Cells[legcolEXPIRES].Value;

                dgvLeg.Rows[e.RowIndex].Cells[legcolDAYSLEFT].Value = Utils.ComputeDaysToExpire (m_RememberDateExpires);
                dgvLeg.InvalidateCell (legcolDAYSLEFT, e.RowIndex);
                leg.ConId = null;
                leg.LocalSymbol = null;
            }

            if (e.ColumnIndex == legcolCALLPUT)
            {
                leg.ConId = null;
                leg.LocalSymbol = null;
            }

            if (e.ColumnIndex == legcolBUYSELL)
            {
                leg.ConId = null;
                leg.LocalSymbol = null;
            }

            if (e.ColumnIndex == legcolSTRIKE)
            {
                leg.ConId = null;
                leg.LocalSymbol = null;
            }

            if (e.ColumnIndex == legcolNOCONTRACTS)
            {
                if (m_OldNoContracts > leg.NoContracts)
                {
                    DialogResult dr = MessageBox.Show ("Do you want to split the position?", "Split?", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        SplitLeg (leg);
                        return;
                    }
                }
            }

            /* Now try to update premium and profit numbers
             * --------------------------------------------
             * This is also done when row is validated, but it might be useful here */
 
            /* See if this a valid stock/option
             * -------------------------------- */

            if (string.IsNullOrEmpty (leg.LocalSymbol) || leg.ConId == null || leg.Multiplier == 0)
            {
                try
                {
                    ConIdLocalSymbolMultiplier cm = await leg.FetchConIdLocalSymbolMultiplier (e.RowIndex);
                    leg.ConId = cm.ConId;
                    leg.LocalSymbol = cm.LocalSymbol;
                    leg.Multiplier = cm.Multiplier;
                }
                catch (Exception )
                {
                    // do nothing. Will be re-excuted when row is validated
                    return;
                }
            }

            /* Update the premium
             * ------------------
             * Need to update the premium before computing profit figures */

            leg.UpdatePremium (); // need multipler for this
            dgvLeg.InvalidateCell (legcolPREMIUM, e.RowIndex);
            leg.SignalTrade (legcolPREMIUM);

            /* Update Profit /Loss
             * ------------------- */

            leg.ComputeProfitFigures ();
            LegChangedSoUpdateGUI (leg, legcolTOTALPROFIT);
            LegChangedSoUpdateGUI (leg, legcolDAILYPROFIT);
            leg.SignalTrade (legcolTOTALPROFIT);
            leg.SignalTrade (legcolPERCENTPROFIT);
        }

        /**************************************************************
         * 
         * Split the leg into two sub-legs
         * 
         * ***********************************************************/
        
        private void SplitLeg (LegData leg)
        {
            int oNo = m_OldNoContracts;
            int nNo = leg.NoContracts;

            LegData newleg = leg.Clone ();
            newleg.NoContracts = oNo - nNo;
            newleg.YesterdayProfitLoss *= newleg.NoContracts / oNo;
            newleg.DailyProfitLoss *= newleg.NoContracts / oNo;
            newleg.Premium *= newleg.NoContracts / oNo;

            leg.Premium *= leg.NoContracts / oNo;
            leg.YesterdayProfitLoss *= leg.NoContracts / oNo;
            leg.DailyProfitLoss *= leg.NoContracts / oNo;

            TradeData trade = (TradeData) dgvTrade.CurrentRow.DataBoundItem;
            trade.m_Legs.Add (newleg);

            UpdateTrade ();
            LoadLegsGrid (trade);
        }

        /***************************************************************
         * 
         * Close Current Leg
         * 
         * ************************************************************/

        private void LegClosePrice (int index)
        {
            decimal ClosingPrice;
            double fClosingPrice = -1;

            DataGridViewRow row = dgvLeg.Rows[index];

            if (row.Cells[legcolCLOSEPRICE].Value != null)
            {
                if (!decimal.TryParse (row.Cells[legcolCLOSEPRICE].Value.ToString (), out ClosingPrice))
                {
                    MessageBox.Show ("Need to specify a valid Closing Price first");
                    return;
                }
                fClosingPrice = (double) ClosingPrice;
            }

            if (row.IsNewRow)
            {
                MessageBox.Show ("A valid leg needs to be selected.");
                return;
            }

            LegData leg = (LegData) row.DataBoundItem;

            if (fClosingPrice == -1)
            {
                leg.ClosePrice = null;
                leg.OpenCloseStatus = OpenCloseValues.Open;
                leg.ClosedDate = null;
            }
            else
            {
                leg.ClosePrice = fClosingPrice;
                leg.ClosedDate = DateTime.Now;
                leg.OpenCloseStatus = OpenCloseValues.Close;
            }

            leg.ComputeProfitFigures ();
            hlLegProfitLoss (row, leg);
            leg.SignalTrade (legcolTOTALPROFIT);
            leg.SignalTrade (legcolPERCENTPROFIT);
            leg.SignalTrade (legcolCLOSEDATE);

            dgvLeg.InvalidateCell (legcolTOTALPROFIT, index);
            dgvLeg.InvalidateCell (legcolPERCENTPROFIT, index);
            dgvLeg.InvalidateCell (legcolCLOSEDATE, index);
            dgvLeg.InvalidateCell (legcolOPENCLOSE, index);

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                m_Log.Log (ErrorLevel.logINF, string.Format ("UpsertLeg on {0}", leg.LocalSymbol));
                dc.UpsertLeg (leg.Id, 
                              leg.ConId,
                              leg.LocalSymbol,
                              leg.Ticker,
                              Utils.EquityType2String (leg.Equity), 
                              leg.Exchange,
                              leg.Multiplier, 
                              (int) leg.OpenCloseStatus, 
                              leg.IfCall,
                              leg.IfSell, 
                              leg.Strike, 
                              leg.ExpiryDate, 
                              leg.OpenPrice,
                              leg.ClosePrice,
                              leg.NoContracts, 
                              leg.Commissions, 
                              leg.OpenDate, 
                              leg.ClosedDate, 
                              leg.LastEmail, 
                              leg.Trade_id);
                dc.UpdateLegProfitLoss (leg.Id, (decimal?) leg.TotalProfitLoss, DateTime.Now, (decimal?) leg.DailyProfitLoss, DateTime.Now);
            }

        }

 
        /***************************************************************
         * 
         * If there is a new trade, fill in the open date
         * 
         * ************************************************************/

        private void dgvTrade_RowValidated (object sender, DataGridViewCellEventArgs e)
        {
            TradeData trade = (TradeData) dgvTrade.Rows[e.RowIndex].DataBoundItem;

            if (trade != null)
            {
                if (trade.OpenDate == null)
                {
                    trade.OpenDate = DateTime.Now;
                    dgvTrade.InvalidateCell (trdcolOPENDATE, e.RowIndex);

                    trade.Persist (m_DataGroupName);
                }
            }
        }

        /***********************************************************************
         * 
         * Trade cell value validation
         * 
         * ********************************************************************/

        private void dgvTrade_CellValidating (object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            //if (e.ColumnIndex == trdcolOPENCLOSE)
            //{
            //    if (dgvTrade.CurrentRow.IsNewRow)
            //    {
            //        return;
            //    }

            //    if ((string) dgvTrade.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue != "Close")
            //    {
            //        return;
            //    }

            //    TradeData t = m_Trades[dgvTrade.Rows[e.RowIndex].Index];

            //    DialogResult rc = MessageBox.Show (string.Format ("Confirm that trade {0} is being closed.", t.Ticker), "Close Confirmation", MessageBoxButtons.OKCancel);
            //    if (rc == DialogResult.OK)
            //    {
            //        t.ClosedDate = DateTime.Now;
            //        dgvTrade.InvalidateCell (trdcolCLOSEDATE, e.RowIndex);

            //        return;
            //    }
            //    e.Cancel = true;
            //}
        }

        /*******************************************************************************************
         * 
         * Cell validated, so update database trade 
         * 
         * ****************************************************************************************/

        private void dgvTrade_CellValueChanged (object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvTrade.CurrentRow.IsNewRow)
            {
                return;
            }

            TradeData t = (TradeData) dgvTrade.Rows[e.RowIndex].DataBoundItem;
            
            /* Update Company, if ticker has changed
             * ------------------------------------- */

            if (e.ColumnIndex == trdcolTICKER && t != null)
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var stock = (from s in dc.Stocks
                                 where s.Ticker == t.Ticker
                                 select new { s.LastTrade, s.Company }
                                ).SingleOrDefault ();

                    if (stock == null)
                    {
                        MessageBox.Show (string.Format ("The ticker {0} does not exist in the database. Please fix.", t.Ticker));
                        return;
                    }
                    t.Company = stock.Company;
                    t.UnderlyingPrice = (double?) stock.LastTrade;
                }

            }

            if (e.ColumnIndex == trdcolOPENCLOSE)
            {
                if (!dgvTrade.CurrentRow.IsNewRow)
                {
                    if (t.OpenCloseStatus == OpenCloseValues.Close)
                    {
                        t.ClosedDate = DateTime.Now;
                        dgvTrade.InvalidateCell (trdcolCLOSEDATE, e.RowIndex);
                    }
                }
            }

            /* Should only update if one second has passed since the last time
             * --------------------------------------------------------------- */
            
            //if (e.ColumnIndex == trdcolOPENCLOSE)
            {

                //using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                //{
                //    dc.UpsertTrade (t.Id,
                //                     m_DataGroupName,
                //                     t.TradeType,
                //                     t.Ticker,
                //                     (int) t.OpenCloseStatus,
                //                     t.Premium,
                //                     t.Commissions,
                //                     t.OpenDate,
                //                     t.ClosedDate,
                //                     t.TotalDelta,
                //                     t.TotalTheta,
                //                     (decimal?) t.TotalProfitLoss,
                //                     (decimal?) t.ProfitThreshold,
                //                     t.EmailNotifications,
                //                     t.Notes,
                //                     t.LastEmail);
                //}

                t.Persist (m_DataGroupName);

            }
        }

        /***************************************************************
         * 
         * Entered into a new leg row
         * 
         * ************************************************************/

        private void dgvLeg_RowEnter (object sender, DataGridViewCellEventArgs e)
        {
            m_Log.Log (ErrorLevel.logDEB, string.Format ("dgvLeg_RowEnter rowindex {0}", e.RowIndex));
            m_OldNoContracts = 0;

            m_bLegRowDirtyFlag = false;

            if (!gbLegNotifications.Visible)
            {
                return;
            }

            m_bLegProfitThresholdDirty = false;

            LegData ld = dgvLeg.Rows[e.RowIndex].DataBoundItem as LegData;

            if (ld == null)
            {
                tbLegProfitThreshold.Text = "";
            }
            else
            {
                if (ld.ProfitThreshold != null)
                {
                    tbLegProfitThreshold.Text = ((double) ld.ProfitThreshold).ToString ("C2");
                }
                ShowLegEmailNotifications (ld.EmailNotifications);
            }
        }

        /***********************************************************
         * 
         * When dgvLeg has focus
         * 
         * ********************************************************/

        private void dgvLeg_Enter (object sender, EventArgs e)
        {
            gbLegNotifications.Visible = true;
        }

        /***********************************************************
        * 
        * When dgvTrade has focus
        * 
        * ********************************************************/
        private void dgvTrade_Enter (object sender, EventArgs e)
        {
            gbLegNotifications.Visible = false;
        }

        /***********************************************************
         * 
         * Validating the row
         * 
         * ********************************************************/

        private async void dgvLeg_RowValidating (object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dgvLeg.Rows[e.RowIndex];
            if (row == null)
            {
                return;
            }
            //if (row.IsNewRow)
            //{
            //    return;
            //}

            LegData ld = (LegData) row.DataBoundItem;

            if (ld == null)
            {
                return;
            }

            if (string.IsNullOrEmpty (ld.Ticker))
            {
                MessageBox.Show ("Invalid ticker specified.");
                e.Cancel = true;
                return;
            }

            if (ld.Strike != null && ld.Strike <= 0)
            {
                MessageBox.Show ("Illegal strike specified.");
                e.Cancel = true;
                return;
            }

            if ((ld.Equity == EquityType.Option || ld.Equity == EquityType.FutOpt) && ld.ExpiryDate == null)
            {
                MessageBox.Show ("Missing expiry date for this option/future.");
                e.Cancel = true;
                return;
            }

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                if (string.IsNullOrEmpty (ld.Exchange))
                {
                    var leg = (from s in dc.Stocks
                               where s.Ticker == row.Cells[legcolTICKER].Value.ToString ()
                               select new { s.Ticker, s.LastTrade, s.Exchange }
                              ).SingleOrDefault ();

                    if (leg == null)
                    {
                        MessageBox.Show (string.Format ("ERROR!! Ticker {0} is not in the database.", row.Cells[legcolTICKER].Value.ToString ()));
                        e.Cancel = true;
                        return;
                    }
                    ld.Exchange = leg.Exchange;
                }
            }

            if (ld.Equity == EquityType.Option || ld.Equity == EquityType.FutOpt)
            {
                if (ld.IfCall == null)
                {
                    MessageBox.Show ("Options/Future Options must be either a call or a put");
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (ld.IfCall != null)
                {
                    MessageBox.Show ("Stocks/Futures cannot be either a call or a put");
                    e.Cancel = true;
                    return;
                }
            }



            /* See if this a valid stock/option
             * -------------------------------- */

//            if (ld.ConId == null || ld.Multiplier == 0)
            if (string.IsNullOrEmpty (ld.LocalSymbol) || ld.ConId == null || ld.Multiplier == 0)
            {
                try
                {
                    ConIdLocalSymbolMultiplier cm = await ld.FetchConIdLocalSymbolMultiplier (e.RowIndex);
                    ld.ConId = cm.ConId;
                    ld.LocalSymbol = cm.LocalSymbol;
                    ld.Multiplier = cm.Multiplier;
                }
                catch (Exception ex)
                {
                    MessageBox.Show (ex.Message);
                    e.Cancel = true;
                }
            }

            /* Update the premium
             * ------------------
             * Need to update the premium before computing profit figures */

            ld.UpdatePremium (); // need multipler for this
            dgvLeg.InvalidateCell (legcolPREMIUM, e.RowIndex);
            ld.SignalTrade (legcolPREMIUM);       
            
            /* Update Profit /Loss
             * ------------------- 
             * do we have yesterday's profit? */



            ld.ComputeProfitFigures ();
            LegChangedSoUpdateGUI (ld, legcolTOTALPROFIT);
            LegChangedSoUpdateGUI (ld, legcolDAILYPROFIT);
            ld.SignalTrade (legcolTOTALPROFIT);
            ld.SignalTrade (legcolPERCENTPROFIT);


        }

        /********************************************************************
         * 
         * RowValidated
         * 
         * *****************************************************************/

        private void dgvLeg_RowValidated (object sender, DataGridViewCellEventArgs e)
        {
            if (m_bLegRowDirtyFlag)
            {
                UpdateTrade ();
            }
        }

        /******************************************************************
         * 
         * Assign default values to the leg when needed
         * 
         * ***************************************************************/

        private void dgvLeg_DefaultValuesNeeded (object sender, DataGridViewRowEventArgs e)
        {
            DateTime dt = m_RememberDateExpires;
            e.Row.Cells[legcolEXPIRES].Value = m_RememberDateExpires; // this changes m_RememberDateExpires which we don't intend
            e.Row.Cells[legcolDAYSLEFT].Value = Utils.ComputeDaysToExpire (dt);
            m_RememberDateExpires = dt; // reset it.

            if (dgvTrade.CurrentRow != null)
            {
                e.Row.Cells[legcolTICKER].Value = dgvTrade.CurrentRow.Cells[trdcolTICKER].Value;
            }
        }

         /**********************************************************************
         * 
         * Close Trade
         * 
         * *******************************************************************/

        private void btnCloseTrade_Click (object sender, EventArgs e)
        {
            //if (cbActivateAll.Checked || cbActivateSelected.Checked)
            //{
            //    MessageBox.Show ("Turn off data collection before closing trade.");
            //    return;
            //}
            
            List<LegData> Legs = new List<LegData> ();

            if (dgvTrade.CurrentRow.IsNewRow)
            {
                MessageBox.Show ("Need to update the trade first.");
                return;
            }

            TradeData trade = m_Trades[dgvTrade.CurrentRow.Index];

            if (dgvLeg.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvLeg.SelectedRows)
                {
                    LegData leg = (LegData) row.DataBoundItem;
                    if (leg != null)
                    {
                        Legs.Add ((LegData) row.DataBoundItem);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvLeg.Rows)
                {
                    LegData leg = (LegData) row.DataBoundItem;
                    if (leg != null)
                    {
                        if (leg.OpenCloseStatus != OpenCloseValues.Close)
                        {
                            Legs.Add ((LegData) row.DataBoundItem);
                        }
                    }
                }
            }

            if (Legs.Count <= 0)
            {
                MessageBox.Show ("Nothing to close!");
                return;
            }

 /*           using (frmCloseTrade frmCloseTrade = new frmCloseTrade (this, m_Log, axTws, Legs))
            {
                DialogResult rc = frmCloseTrade.ShowDialog (this);
                if (rc == DialogResult.OK)
                {
                    /* Persist the leg order id's
                     * -------------------------- */

 /*                   using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                    {
                        foreach (LegData leg in Legs)
                        {
                            dc.UpdateLegOrderId (leg.Id, leg.OpenOrderId, leg.CloseOrderId);
                        }
                    }
                }
            }*/
        }

        /*************************************************************************
         * 
         * Commissions
         * 
         * **********************************************************************/

        private void btnCommissions_Click (object sender, EventArgs e)
        {
            //axTws.reqAllOpenOrders ();
        }

        /*************************************************************************
         * 
         * OpenOrderEx event
         * 
         * **********************************************************************/
/*
        private void axTws_openOrderEx (object sender, AxTWSLib._DTwsEvents_openOrderExEvent e)
        {
            TWSLib.IContract con = e.contract;
            TWSLib.IOrderState ord = e.orderState;

            double? commission = null;
            double? mincommission = null;
            double? maxcommission = null;

            if (ord.commission < double.MaxValue)
            {
                commission = ord.commission;
            }
            if (ord.minCommission < double.MaxValue)
            {
                mincommission = ord.minCommission;
            }
            if (ord.maxCommission < double.MaxValue)
            {
                maxcommission = ord.maxCommission;
            }

            m_Log.Log (ErrorLevel.logINF, string.Format ("symbol: {0} localsymbol {1} status: {2} commission {3} min commission: {4}, max commission: {5}", 
                con.symbol, 
                con.localSymbol,
                ord.status, 
                commission == null ? "INVALID" : ord.commission.ToString ("F3"),
                mincommission == null ? "INVALID" : ord.minCommission.ToString ("F3"),
                maxcommission == null ? "INVALID" : ord.maxCommission.ToString ("F3")));
            
            if (ord.status != "PreSubmitted")
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    dc.UpsertOrder (e.orderId,
                                    DateTime.Now,
                                    commission,
                                    mincommission,
                                    maxcommission,
                                    ord.status);
                }
            }
        }

        /**********************************************************************
         * 
         * Executions button
         * 
         * *******************************************************************/

        private void btnExecutions_Click (object sender, EventArgs e)
        {
 /*           if (dgvLeg.SelectedRows.Count <= 0)
            {
                MessageBox.Show ("Select one or more legs to pull in execution");
                return;
            }

            if (dgvTrade.CurrentRow.IsNewRow)
            {
                MessageBox.Show ("Need to update the trade first.");
                return;
            }

            TradeData trade = m_Trades[dgvTrade.CurrentRow.Index];    
           
            foreach (DataGridViewRow row in dgvLeg.SelectedRows)
            {
                LegData leg = trade.m_Legs[row.Index];

                TWSLib.IExecutionFilter f = axTws.createExecutionFilter ();
                f.secType = "OPT";
                f.symbol = leg.Ticker;
                f.side = leg.IfSell ? "SELL" : "BUY";

                m_Log.Log (ErrorLevel.logINF, string.Format ("calling reqExecutionsEx. reqId {0}", row.Index));
                axTws.reqExecutionsEx (row.Index, f);
            } */     
        }

        /***************************************************************************
         * 
         * exeDetailsEx 
         * 
         * ************************************************************************/
/*
        private void axTws_execDetailsEx (object sender, AxTWSLib._DTwsEvents_execDetailsExEvent e)
        {
            TWSLib.IContract con = e.contract;
            TWSLib.IExecution x = e.execution;

            m_Log.Log (ErrorLevel.logINF, string.Format ("symbol: {0} localsymbol {1}", con.symbol, con.localSymbol));
           // m_Log.Log (ErrorLevel.logINF, string.Format ("commission: {0:F3", ord.commission));

        }

        private void axTws_execDetailsEnd (object sender, AxTWSLib._DTwsEvents_execDetailsEndEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("execDetailsEnd called. reqId {0}", e.reqId));
        }

        /*********************************************************************
         * 
         * Best Strangle
         * 
         * ******************************************************************/

        private void btnBestStrangle_Click (object sender, EventArgs e)
        {
 /*           string ticker = dgvTrade.CurrentRow.Cells[trdcolTICKER].Value.ToString ();

            using (frmBestStrangle f = new frmBestStrangle (ticker, this, m_Log, axTws))
            {
                DialogResult res = f.ShowDialog (this);
                if (res == DialogResult.OK)
                {
                    TradeData trade = (TradeData) dgvTrade.CurrentRow.DataBoundItem;
                    if (trade != null)
                    {
                        foreach (OptionInfo opt in f.OptionList)
                        {
                            trade.m_Legs.Add (new LegData (null,
                                                           opt.Ticker,
                                                           EquityType.Option,
                                                           trade.Exchange,
                                                           100, 
                                                           opt.LocalSymbol, 
                                                           opt.ConId, 
                                                           OpenCloseValues.Pending, 
                                                           opt.IfCall, 
                                                           opt.IfSell, 
                                                           (decimal) opt.Strike, 
                                                           opt.Expiry,
                                                           null,
                                                           null, 
                                                           null, 
                                                           opt.NoContracts,
                                                           null, null, null, null, // greeks
                                                           null, null, null, null, // my greeks
                                                           null, null, null, null, null, null, null, null, null, null, 
                                                           0,
                                                           null));
                        }
                    }
                }
            }*/
        }

        /*************************************************************
         * 
         * Open Trade
         * 
         * **********************************************************/

        private void btnOpenTrade_Click (object sender, EventArgs e)
        {
            if (cbActivateAll.Checked || cbActivateSelected.Checked)
            {
                MessageBox.Show ("Turn off data collection before closing trade.");
                return;
            }
            List<LegData> Legs = new List<LegData> ();

            if (dgvTrade.CurrentRow.IsNewRow)
            {
                MessageBox.Show ("Need to update the trade first.");
                return;
            }

            TradeData trade = m_Trades[dgvTrade.CurrentRow.Index];

            if (dgvLeg.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvLeg.SelectedRows)
                {
                    LegData leg = (LegData) row.DataBoundItem;
                    if (leg != null)
                    {
                        Legs.Add ((LegData) row.DataBoundItem);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvLeg.Rows)
                {
                    LegData leg = (LegData) row.DataBoundItem;
                    if (leg != null)
                    {
                        Legs.Add ((LegData) row.DataBoundItem);
                    }
                }
            }

            if (Legs.Count <= 0)
            {
                MessageBox.Show ("Nothing to open!");
                return;
            }

/*            using (frmOpenTrade frmOpenTrade = new frmOpenTrade (this, m_Log, axTws, Legs))
            {
                DialogResult rc = frmOpenTrade.ShowDialog (this);
                if (rc == DialogResult.OK)
                {
                    /* Persist the leg order id's
                     * -------------------------- */

 /*                   using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                    {
                        foreach (LegData leg in Legs)
                        {
                            dc.UpdateLegOrderId (leg.Id, leg.OpenOrderId, leg.CloseOrderId);
                        }
                    }
                }
            }*/
        }

        /**************************************************************
         * 
         * Reset ConId for selected legs
         * 
         * ***********************************************************/

        private async void btnResetConId_Click (object sender, EventArgs e)
        {
            if (dgvLeg.SelectedRows.Count == 0)
            {
                MessageBox.Show ("Must select a leg to reset.");
                return;
            }

            List<Task<ConIdLocalSymbolMultiplier>> tasks = new List<Task<ConIdLocalSymbolMultiplier>> ();
            int ctr = 0;
            foreach (DataGridViewRow row in dgvLeg.SelectedRows)
            {
                LegData leg = (LegData) row.DataBoundItem;
                if (leg != null)
                {
                    tasks.Add (leg.FetchConIdLocalSymbolMultiplier (ctr++));
                }
            }
            ConIdLocalSymbolMultiplier[] conids = await Task.WhenAll (tasks);

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                int ctr2 = 0;
                foreach (DataGridViewRow row in dgvLeg.SelectedRows)
                {
                    LegData leg = (LegData) row.DataBoundItem;
                    leg.ConId = conids[ctr2].ConId;
                    leg.LocalSymbol = conids[ctr2].LocalSymbol;
                    leg.Multiplier = conids[ctr2++].Multiplier;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("ResetConid... resetting {0} {1} strike: {2} exp: {3} ConId now: {4} multiplier: {5} [{6}]", leg.Ticker, leg.DisplayCall (), leg.Strike.ToString (), leg.ExpiryDate.ToString (), leg.ConId, leg.Multiplier, leg.LocalSymbol));
                    dc.UpdateConIdLeg (leg.Id, leg.ConId, leg.LocalSymbol, leg.Multiplier);
                }
            }

            MessageBox.Show ("ConId's reset for selected legs.");
        }

        /***********************************************************
         * 
         * recomputeToolStripMenuItem clicked
         * 
         * ********************************************************/

        private void recomputeToolStripMenuItem_Click (object sender, EventArgs e)
        {
            TradeData t = (TradeData) dgvTrade.Rows[m_MouseLocation.RowIndex].DataBoundItem;

            t.ComputePremium ();

            dgvTrade.InvalidateCell (m_MouseLocation.ColumnIndex, m_MouseLocation.RowIndex);

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                dc.UpdateTrade (t.Id, t.TradeType, t.Ticker, (int) t.OpenCloseStatus, t.Premium, t.Commissions, t.OpenDate, t.ClosedDate, t.TotalDelta, t.TotalTheta, (decimal?) t.TotalProfitLoss, (decimal?) t.ProfitThreshold, t.EmailNotifications, t.Notes, t.LastEmail);
            }
        }

        /**********************************************************
         * 
         * track the mouse when it enters a trade cell
         * 
         * *******************************************************/

        private DataGridViewCellMouseEventArgs m_MouseLocation;
        private int m_OldNoContracts;

        private void dgvTrade_CellMouseDown (object sender, DataGridViewCellMouseEventArgs e)
        {
            m_MouseLocation = e;
        }

 
        /**************************************************************
         * 
         * Size of form has changed
         * 
         * ***********************************************************/

        private void frmPos_SizeChanged (object sender, EventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                m_szForm = this.Size;
            }
        }

        /***************************************************************
         * 
         * Update Profit figures 
         * 
         * ************************************************************/

        private void btnUpdateProfit_Click (object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvLeg.Rows)
            {
                LegData leg = (LegData) row.DataBoundItem;
                if (leg != null)
                {
                    leg.ComputeProfitFigures ();
                    LegChangedSoUpdateGUI (leg, legcolTOTALPROFIT);
                    leg.SignalTrade (legcolTOTALPROFIT);
                }
            }
        }

        /************************************************************
         * 
         * Show open positions only
         * 
         * *********************************************************/

        private void cbOpenPosOnly_Click (object sender, EventArgs e)
        {
            TradeData trade = (TradeData) dgvTrade.CurrentRow.DataBoundItem;
            if (trade != null)
            {
                LoadLegsGrid (trade);
            }
        }

        /**************************************************************
         * 
         * Tab selection has changed
         * 
         * ***********************************************************/

        private void tabControlPos_SelectedIndexChanged (object sender, EventArgs e)
        {

            //if (tabControlPos.TabPages[tabControlPos.SelectedIndex].Name == "tabReconcile")
            //{
            //    LoadLocalPositions ();
            //    await LoadIBPositions ();
            //}
        }

        /*******************************************************
         * 
         * Load Local Positions
         * 
         * ****************************************************/

        private void LoadLocalPositions (bool bIfIB, bool bIfCore, bool bIfTDI, bool bIfTest)
        {
            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                var trades = (from t in dc.Trades
                              join s in dc.Stocks on t.Ticker equals s.Ticker
//                              where (t.IfClosed != (int) OpenCloseValues.Close) && (t.TradeGroup == m_DataGroupName)
                                where (t.IfClosed != (int) OpenCloseValues.Close) 
                                select new ShortTradeData (t.TradeId,
                                                           t.Ticker,
                                                           t.TradeGroup)).ToList ();
                
                /* Filter out unwanted trades
                 * -------------------------- */

                if (!bIfTest)
                {
                    trades.RemoveAll (t => t.TradeGroup == "TST");
                }
                if (!bIfIB)
                {
                    trades.RemoveAll (t => t.TradeGroup == "IB ");
                }
                if (!bIfCore)
                {
                    trades.RemoveAll (t => t.TradeGroup == "COR");
                }
                if (!bIfTDI)
                {
                    trades.RemoveAll (t => t.TradeGroup == "TDI");
                }

                /* Got the trades, now get the legs
                 * -------------------------------- */

                m_LocalLegs = new SortableBindingList<LegData> ();

                foreach (var t in trades)
                {
                    var legs = (from l in dc.Legs
                                where l.Trade_Id == t.Id
                                select new LegData (l.Id,
                                                    l.Ticker,
                                                    Utils.String2EquityType (l.EquityType),
                                                    l.Exchange,
                                                    l.Multiplier,
                                                    l.LocalSymbol,
                                                    l.ConId,
                                                    (OpenCloseValues) l.IfClosed,
                                                    l.IfCall,
                                                    l.IfSell,
                                                    l.Strike,
                                                    l.Expiry,
                                                    l.UndPrice,
                                                    l.OpenPrice,
                                                    l.ClosePrice,
                                                    l.NoContracts,
                                                    l.TotalDelta,
                                                    l.TotalTheta,
                                                    l.Gamma,
                                                    l.Vega,
                                                    l.MyDelta,
                                                    l.MyTheta,
                                                    l.MyGamma,
                                                    l.MyVega,
                                                    l.OpenDate,
                                                    l.ClosedDate,
                                                    l.ProfitLoss,
                                                    l.ProfitLossTimeStamp,
                                                    l.TodayProfitLoss,
                                                    l.TodayProfitLossTimeStamp,
                                                    l.YesterdayProfitLoss,
                                                    l.YesterdayProfitLossTimeStamp,
                                                    l.ProfitThreshold,
                                                    l.LastEmail,
                                                    l.EmailNotifications,
                                                    l.Trade_Id)
                                );
                    foreach (var l in legs)
                    {
                        if (l.OpenCloseStatus != OpenCloseValues.Close)
                        {
                            m_LocalLegs.Add (l);
                        }
                    }
                }
            }
        }

        /**************************************************************
         * 
         * ComputeTotalWeightedDelta
         * 
         * ************************************************************/
        private void ComputeTotalWeightedDelta ()
        {
            double? rd = 0;
            double? wd = 0;

            for (int row = 0; row < dgvBeta.Rows.Count - 1; row++)
            {
                TradeData tr = (TradeData) dgvBeta.Rows[row].DataBoundItem;
                if (tr.BetaWeightedDelta != null)
                {
                    wd += tr.BetaWeightedDelta;
                }
                if (tr.TotalDelta != null)
                {
                    rd += tr.TotalDelta;
                }
            }
            TradeData total = (TradeData) dgvBeta.Rows[dgvBeta.Rows.Count - 1].DataBoundItem;
            total.BetaWeightedDelta = wd;
            total.TotalDelta = rd;
            dgvBeta.InvalidateRow (dgvBeta.Rows.Count - 1);
        }

        private void dgvBeta_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                BetaColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvBeta.Columns.Count; i++)
                {
                    BetaColumnWidths.Add (dgvBeta.Columns[i].Width.ToString ());
                }
            }
        }

        /************************************************************************************
         * 
         * Reconcile
         * 
         * *********************************************************************************/

        private void btnReconcile_Click (object sender, EventArgs e)
        {
            while (SuccessfulRemovalLocalLeg ())
            {}

            /* Remove remote legs that are zero
             * -------------------------------- */

            for (int j = 0; j < m_IBLegs.Count; )
            {
                var remote = m_IBLegs[j];
                if (remote.NoContracts == 0)
                {
                    dgvIBPositions.Rows.RemoveAt (j);
                }
                else
                {
                    j++;
                }
            }
        }

        private bool SuccessfulRemovalLocalLeg ()
        {
            for (int i = 0; i < m_LocalLegs.Count; i++)
            {
                var local = m_LocalLegs[i];

                for (int j = 0; j < m_IBLegs.Count; j++)
                {
                    var remote = m_IBLegs[j];
                    if (local.LocalSymbol == remote.LocalSymbol)
                    {
                        if (local.IfSell == remote.IfSell)
                        {
                            remote.NoContracts -= local.NoContracts;
                        }
                        else
                        {
                            remote.NoContracts += local.NoContracts;
                        }

                        dgvLocalPositions.Rows.RemoveAt (i);
                        //{
                        //    List<LegData> ld = new List<LegData> ();
                        //    for (int k = 0; k < m_IBLegs.Count; k++)
                        //    {
                        //        if (m_IBLegs[k].LocalSymbol == remote.LocalSymbol)
                        //        {
                        //            ld.Add (m_IBLegs[k]);
                        //        }

                        //    }
                        //}
                        return true;
                    }
                }
            }
            return false;
        }

        /***********************************************************************************
         * 
         * Fetch IB Positions
         * 
         * ********************************************************************************/

        private async void btnIBPositions_Click (object sender, EventArgs e)
        {
            LoadLocalPositions (chkRecInteractiveBrokers.Checked, chkRecCore.Checked, false, chkRecTest.Checked);

            m_LocalLegs = new SortableBindingList<LegData> (m_LocalLegs.OrderBy (leg => leg.LocalSymbol).ToList ());

            dgvLocalPositions.AutoGenerateColumns = false;
            dgvLocalPositions.DataSource = m_LocalLegs;

            await LoadIBPositions ();
        }

        /**********************************************************************************
         * 
         * LoadIBPositions
         * 
         * *******************************************************************************/

        private async Task LoadIBPositions ()
        {
            try
            {
                m_IBLegs = new SortableBindingList<LegData> (await FetchIBPositions ());
                dgvIBPositions.AutoGenerateColumns = false;
                dgvIBPositions.DataSource = m_IBLegs;
            }
            catch (Exception ex)
            {
                MessageBox.Show (string.Format ("Failed to Fetch IB positions. {0}", ex.Message));
            }
        }

        /*******************************************************************************
         * 
         * Fetch a list of all the current IB Positions
         * 
         * ****************************************************************************/

        internal static Task<List<LegData>> FetchIBPositions ()
        {
            List<LegData> legs = new List<LegData> ();
            var tcs = new TaskCompletionSource<List<LegData>> ();
/*
            m_Log.Log (ErrorLevel.logINF, "FetchIBPositions called");

            var errhandler = default (AxTWSLib._DTwsEvents_errMsgEventHandler);
            var timehandler = default (AxTWSLib._DTwsEvents_updateAccountTimeEventHandler);
            var valuehandler = default (AxTWSLib._DTwsEvents_updateAccountValueEventHandler);
            var porthandler = default (AxTWSLib._DTwsEvents_updatePortfolioExEventHandler);
            var endhandler = default (AxTWSLib._DTwsEvents_accountDownloadEndEventHandler);

            errhandler = new AxTWSLib._DTwsEvents_errMsgEventHandler ((s, e) =>
            {
                tcs.TrySetException (new Exception (e.errorMsg));
                Utils.axTws.updateAccountTime -= timehandler;
                Utils.axTws.updateAccountValue -= valuehandler;
                Utils.axTws.updatePortfolioEx -= porthandler;
                Utils.axTws.accountDownloadEnd -= endhandler;
                Utils.axTws.errMsg -= errhandler;

                Utils.axTws.reqAccountUpdates (0, "xx");
            });

            valuehandler = new AxTWSLib._DTwsEvents_updateAccountValueEventHandler ((s, e) =>
            {

            });

            timehandler = new AxTWSLib._DTwsEvents_updateAccountTimeEventHandler ((s, e) =>
            { 
            });

            endhandler = new AxTWSLib._DTwsEvents_accountDownloadEndEventHandler ((s, e) =>
            {
                legs = legs.OrderBy (leg => leg.LocalSymbol).ToList ();

                tcs.SetResult (legs);

                Utils.axTws.updateAccountTime -= timehandler;
                Utils.axTws.updateAccountValue -= valuehandler;
                Utils.axTws.updatePortfolioEx -= porthandler;
                Utils.axTws.accountDownloadEnd -= endhandler;
                Utils.axTws.errMsg -= errhandler;

                Utils.axTws.reqAccountUpdates (0, "xx");
            });

/*            porthandler = new AxTWSLib._DTwsEvents_updatePortfolioExEventHandler ((s, e) =>
            {
                TWSLib.IContract c = e.contract;

                EquityType eqtype = EquityType.Option; // assign it something...

                switch (c.secType)
                {
                    case "OPT":
                        eqtype = EquityType.Option;
                        break;

                    case "FOP":
                        eqtype = EquityType.FutOpt;
                        break;

                    case "FUT":
                        eqtype = EquityType.Future;
                        break;

                    case "STK":
                        eqtype = EquityType.Stock;
                        break;

                    case "IND":
                        eqtype = EquityType.Index;
                        break;
                }

                DateTime? expiry = null;
                CultureInfo enUS = new CultureInfo ("en-US");
                {
                    DateTime exp;
                    if (DateTime.TryParseExact (c.expiry, "yyyyMMdd", enUS, DateTimeStyles.None, out exp))
                    {
                        expiry = exp;
                    }
                }
                OpenCloseValues open_close = e.position == 0 ? OpenCloseValues.Close : OpenCloseValues.Open;
                bool? IfCall = null;
                if (c.right == "C")
                {
                    IfCall = true;
                }
                else if (c.right == "P")
                {
                    IfCall = false;
                }
                int multiplier;
                if (int.TryParse (c.multiplier,out  multiplier))
                {
                    multiplier = 0;
                }

                LegData l = new LegData (null,
                                         c.symbol,
                                         eqtype,
                                         c.exchange,
                                         multiplier,
                                         c.localSymbol,
                                         c.conId,
                                         open_close,
                                         IfCall,
                                         e.position > 0 ? false : true,
                                         (decimal) c.strike,
                                         expiry,
                                         null, null, null,
                                         Math.Abs (e.position),
                                         null, null, null, null, null, null, null, null, null, null, null, null, null,
                                         null, null, null, null, null, 0, null
                                         );
                legs.Add (l);
            });

            Utils.axTws.updateAccountTime += timehandler;
            Utils.axTws.updateAccountValue += valuehandler;
            Utils.axTws.updatePortfolioEx += porthandler;
            Utils.axTws.errMsg += errhandler;
            Utils.axTws.accountDownloadEnd += endhandler;

            Utils.axTws.reqAccountUpdates (1, "xx");
*/            return tcs.Task;
        }

        /***************************************************************************
         * 
         * If row is sorted, this event is called
         * 
         * ************************************************************************/

        private void dgvTrade_RowPostPaint (object sender, DataGridViewRowPostPaintEventArgs e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("dgvTrade_RowPostPaint called row: {0}.", e.RowIndex));
            hlTradeProfitLoss (dgvTrade.Rows[e.RowIndex]);
        }

        /***************************************************************************
         * 
         * Add Future entries to Stocks in the database
         * 
         * ************************************************************************/

        private void btnFutures_Click (object sender, EventArgs e)
        {
            using (frmAddFuture frm = new frmAddFuture (m_Log))
            {
                frm.ShowDialog (this);
            }
        }

        /*************************************************************************
         * 
         * If Loading Positions for Beta Weighting
         * 
         * **********************************************************************/

        class PriceHistory : IComparable
        {
            public DateTime pricedate;
            public double? price;
            public PriceHistory (DateTime dt, decimal? p)
            {
                pricedate = dt;
                price = (double?) p;
            }
            public int CompareTo (object obj)
            {
                if (obj == null) throw new ArgumentNullException ();
                PriceHistory other = (PriceHistory) obj;
                if (other == null) throw new ArgumentNullException ();

                if (this.price < other.price) return -1;
                if (this.price > other.price) return 1;
                return 0;
            }
        };

        private async void btnBetaLoadPositions_Click (object sender, EventArgs e)
        {
            LoadLocalPositions (chkBetaIfInteractiveBrokers.Checked, chkBetaIfCore.Checked, chkBetaIfTDI.Checked, chkBetaIfTest.Checked);

            /* Fetch most recent price
             * ----------------------- */

            bool bIfPricesFetched = false;
            try
            {
                bIfPricesFetched = await Utils.FetchLastPrice (m_Log, m_LocalLegs);
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
                return;
            }

            /* Fetch the underlying price
             * -------------------------- */

            foreach (var leg in m_LocalLegs)
            {
                if ((leg.Equity == EquityType.Option || leg.Equity== EquityType.FutOpt) && leg.UnderlyingPrice == null)
                {
                    leg.UnderlyingPrice = await Utils.FetchUnderlyingPrice (m_random.Next (0xFFFF), leg.Ticker);
                    leg.ComputeMyGreeks ();
                }
                if (leg.Equity == EquityType.Stock)
                {
                    leg.MyDelta = leg.IfSell ? -1.0 : 1.0;
                    leg.TotalDelta = leg.NoContracts * leg.MyDelta * leg.Multiplier;
                }
                if (leg.Equity == EquityType.Future)
                {
                    leg.MyDelta = leg.IfSell ? -1.0 : 1.0;
                    leg.TotalDelta = leg.NoContracts * leg.MyDelta * leg.Multiplier;
                }
            }

            /* Fetch historical prices for SPY
             * ------------------------------- */

            Dictionary<DateTime, double?> spy = new Dictionary<DateTime,double?> ();
            int NoDays = int.Parse (tbBetaInterval.Text);
            
            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                DateTime AWhileBack = DateTime.Now - new TimeSpan (NoDays, 0, 0, 0);

                var spyl = (from p in dc.PriceHistories
                          where p.Ticker == "SPY" && p.PriceDate > AWhileBack && p.ClosingPrice != null
                          orderby p.PriceDate ascending
                          select new PriceHistory (p.PriceDate, p.ClosingPrice)).ToList ();

                foreach (var s in spyl)
                {
                    spy.Add (s.pricedate, s.price);
                }

                /* Fetch historical prices for each leg
                 * ------------------------------------
                 * so that we can compute beta and correlation */

                foreach (var leg in m_LocalLegs)
                {
                    List<PriceHistory> t;

                    /* Get an estimate of the beta for futures
                     * ---------------------------------------
                     * by substituting the appropriate ETF */

                    string ticker;
                    switch (leg.Ticker)
                    {
                        case "ES":
                            ticker = "SPY";
                            break;

                        case "ZB":
                            ticker = "TLT";
                            break;

                        case "GC":
                            ticker = "GLD";
                            break;

                        case "CL":
                            ticker = "XOP";
                            break;

                        default:
                            ticker = leg.Ticker;
                            break;
                    }

                    t = (from p in dc.PriceHistories
                         where p.Ticker == ticker && p.PriceDate > AWhileBack && p.ClosingPrice != null
                         orderby p.PriceDate ascending
                         select new PriceHistory (p.PriceDate, p.ClosingPrice)).ToList ();

                    double SpyPrice = (double) spyl.Last ().price;

                    double beta, correlation, intercept;
                    ComputeBeta (spy, t, out beta, out intercept, out correlation);
                    leg.Beta = beta;
                    leg.Correlation = correlation;
                    leg.Intercept = intercept;

                    //DisplayBeta (spy, t, NoDays, beta, intercept);

                    //leg.TotalDelta = leg.MyDelta * leg.NoContracts * leg.Multiplier;
                    leg.TotalBetaWeightedDelta = leg.TotalDelta * (leg.UnderlyingPrice / SpyPrice) * leg.Beta;
                }

            }

            dgvBeta.AutoGenerateColumns = false;
            dgvBeta.DataSource = m_LocalLegs;

            double TotalBetaWeightedDelta = 0;
            double TotalTheta = 0;
            foreach (var leg in m_LocalLegs)
            {
                if (leg.TotalBetaWeightedDelta != null)
                {
                    TotalBetaWeightedDelta += (double) leg.TotalBetaWeightedDelta;
                }
                if (leg.TotalTheta != null)
                {
                    TotalTheta += (double) leg.TotalTheta;
                }
            }
            lbBetaTotalTheta.Text = TotalTheta.ToString ("F2");
            lbBetaWeightedDelta.Text = TotalBetaWeightedDelta.ToString ("F2");
 
            //if (tabControlPos.TabPages[tabControlPos.SelectedIndex].Name == "tabBeta")
            //{
            //    using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            //    {
            //        foreach (var trade in m_Trades)
            //        {
            //            /* Special cases... convert futures to ETF equivalents
            //               --------------------------------------------------- */

            //            string ticker = trade.Ticker;
            //            if (ticker == "ES")
            //            {
            //                ticker = "SPY";
            //            }
            //            else if (ticker == "ZB")
            //            {
            //                ticker = "TLT";
            //            }
            //            else if (ticker == "NQ")
            //            {
            //                ticker = "QQQ";
            //            }
            //            else if (ticker == "TF")
            //            {
            //                ticker = "IWM";
            //            }
            //            var beta = (from s in dc.Stocks
            //                        where s.Ticker == ticker
            //                        select s.Beta).SingleOrDefault ();

            //            double tradeprice = 0.0;
            //            try
            //            {
            //                if (trade.UnderlyingPrice == null)
            //                {
            //                    tradeprice = await Utils.FetchUnderlyingPrice (m_random.Next (0xFFFF), trade.Ticker);
            //                }
            //                else
            //                {
            //                    tradeprice = (double) trade.UnderlyingPrice;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show (string.Format ("Failed to obtain price for {0}. {1}", trade.Ticker, ex.Message));
            //                tradeprice = 0;
            //            }

            //            if (beta == null)
            //            {
            //                beta = 1.0;
            //                m_Log.Log (ErrorLevel.logINF, string.Format ("Missing beta in {0}. Default to 1.0", trade.Ticker));
            //            }
            //            trade.BetaWeightedDelta = trade.TotalDelta * (tradeprice / spy) * beta;
            //        }
            //    }

            //    List<TradeData> copyTrades = m_Trades.ToList ();
            //    TradeData total = new TradeData ();
            //    total.Ticker = "Total";
            //    copyTrades.Add (total);

            //    dgvBeta.AutoGenerateColumns = false;
            //    dgvBeta.DataSource = copyTrades;
            //    ComputeTotalWeightedDelta ();

            //    DataGridViewCellStyle style = new DataGridViewCellStyle ();
            //    style.Font = new Font ("Tahoma", 6.75F, FontStyle.Bold, GraphicsUnit.Point);
            //    dgvBeta.Rows[dgvBeta.Rows.Count - 1].DefaultCellStyle = style;
            //}

        }

        /*********************************************************************************
         * 
         * Display Beta graph
         * 
         * ******************************************************************************/

        private void DisplayBeta (Dictionary<DateTime, double?> spy, List<PriceHistory> t, int NoDays, double beta, double intercept)
        {
            if (t.Count == 0)
            {
                return;
            }

            chartBeta.ChartAreas.Clear ();
            chartBeta.Series.Clear ();

            chartBeta.ChartAreas.Add ("area");
            double x1 = chartBeta.ChartAreas["area"].AxisX.Minimum = Math.Round ((double) spy.Values.Min () - 1.0);
            double x2 = chartBeta.ChartAreas["area"].AxisX.Maximum = Math.Round ((double) spy.Values.Max () + 1.0);
            chartBeta.ChartAreas["area"].AxisY.Minimum = Math.Round ((double) t.Min ().price - 1.0);
            chartBeta.ChartAreas["area"].AxisY.Maximum = Math.Round ((double) t.Max ().price + 1.0);
            chartBeta.ChartAreas["area"].AxisX.LabelStyle.Format = "{0:N1}";
            chartBeta.ChartAreas["area"].AxisY.LabelStyle.Format = "{0:N1}";

            chartBeta.Series.Add ("plot");

            chartBeta.Series["plot"].Color = Color.BlueViolet;
            chartBeta.Series["plot"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chartBeta.Series["plot"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            chartBeta.Series["plot"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;

            chartBeta.Series.Add ("line");

            chartBeta.Series["line"].Color = Color.OrangeRed;
            chartBeta.Series["line"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartBeta.Series["line"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            chartBeta.Series["line"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;

            double y1 = beta * x1 + intercept;
            double y2 = beta * x2 + intercept;

            chartBeta.Series["line"].Points.AddXY (x1, y1);
            chartBeta.Series["line"].Points.AddXY (x2, y2);

            System.Windows.Forms.DataVisualization.Charting.Series s = chartBeta.Series[0];
            s.XValueMember = "x";
            s.YValueMembers = "y";

            chartBeta.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //chartBeta.Series[0].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            chartBeta.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            foreach (var r in t)
            {
                if (spy.ContainsKey (r.pricedate))
                {
                    chartBeta.Series["plot"].Points.AddXY ((double) spy[r.pricedate], (double) r.price);
                }
            }
        }


        /**********************************************************************************
         * 
         * Compute beta
         * 
         * *******************************************************************************/
               
        private void ComputeBeta (Dictionary<DateTime, double?> spy, List<PriceHistory> t, out double beta, out double intercept, out double correlation)
        {
            List<BMuth.StatisticsUtilities.Point> pts = new List<BMuth.StatisticsUtilities.Point> ();
            foreach (var r in t)
            {
                if (spy.ContainsKey (r.pricedate))
                {
                    BMuth.StatisticsUtilities.Point pt = new BMuth.StatisticsUtilities.Point ((double) spy[r.pricedate], (double) r.price);
                    pts.Add (pt);
                }
            }
            
            Tuple<double, double> results = Statistics.LinearRegression (pts);
            correlation = Statistics.Correlation (pts);
            beta = results.Item1;
            intercept = results.Item2;
        }

        /**********************************************************************************
         * 
         * Beta row has changed
         * 
         * *******************************************************************************/

        private void dgvBeta_RowEnter (object sender, DataGridViewCellEventArgs e)
        {
            LegData leg = dgvBeta.Rows[e.RowIndex].DataBoundItem as LegData;

            if (leg != null)
            {
                if (leg.Beta == null || leg.Intercept == null)
                {
                    return;
                }
                Dictionary<DateTime, double?> spy = new Dictionary<DateTime, double?> ();
                int NoDays = int.Parse (tbBetaInterval.Text);

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    DateTime AWhileBack = DateTime.Now - new TimeSpan (NoDays, 0, 0, 0);

                    var spyl = (from p in dc.PriceHistories
                                where p.Ticker == "SPY" && p.PriceDate > AWhileBack && p.ClosingPrice != null
                                orderby p.PriceDate ascending
                                select new PriceHistory (p.PriceDate, p.ClosingPrice)).ToList ();

                    foreach (var s in spyl)
                    {
                        spy.Add (s.pricedate, s.price);
                    }

                    /* Fetch historical prices for leg
                     * -------------------------------
                     * so that we can compute beta */


                    List<PriceHistory> t;

                    t = (from p in dc.PriceHistories
                             where p.Ticker == leg.Ticker && p.PriceDate > AWhileBack && p.ClosingPrice != null
                             orderby p.PriceDate ascending
                             select new PriceHistory (p.PriceDate, p.ClosingPrice)).ToList ();

                    DisplayBeta (spy, t, NoDays, (double) leg.Beta, (double) leg.Intercept);
                }
            }
        }
    }
}
