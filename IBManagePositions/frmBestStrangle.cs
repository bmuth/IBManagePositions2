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
using IBApi;
using System.Threading;
using System.Globalization;
using Be.Timvw.Framework.ComponentModel;

namespace IBManagePositions
{


    public partial class frmBestStrangle : Form
    {
        const int colbsCALLPUT = 3;
        const int colbsSTRIKE = 4;
        const int colbsBUYSELL = 5;
        const int colbsNOCONTRACTS = 9;

        bool bSettingWidthsToBeSaved = false;
//        AxTWSLib.AxTws m_axTws;
        frmPos m_Pos;
        LogCtl m_Log;
        string m_ticker;
        delegate void OptionsCollectedDelegate ();

        private System.Collections.Specialized.StringCollection ColumnWidths;
        private double m_dTargetITM;
        private int m_iTargetMargin;
        private List<OptionInfo> m_Options;
        private BestStrangleQueue m_q;
        private List<OptionInfo> m_Best;
        private int m_OrderId;

        internal frmBestStrangle (string ticker, frmPos pos, LogCtl log/*, AxTWSLib.AxTws axtws*/)
        {
            InitializeComponent ();

 //           m_axTws = axtws;
            m_Pos = pos;
            m_Log = log;
            m_ticker = ticker;

            dgvBestStrangle.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvBestStrangle.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvBestStrangle.CellBorderStyle = DataGridViewCellBorderStyle.None;

            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBestStrangle.Columns[colbsCALLPUT];
                oc.DataSource = CallPutT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }
            {
                DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBestStrangle.Columns[colbsBUYSELL];
                oc.DataSource = BuySellT.Choices ();
                oc.DisplayMember = "Name";
                oc.ValueMember = "Value";
            }

        }

        /*****************************************************
         * 
         * contractDetailsEnd event
         *  
         * **************************************************/
 /*       
        void m_axTws_contractDetailsEnd (object sender, AxTWSLib._DTwsEvents_contractDetailsEndEvent e)
        {
            m_axTws.contractDetailsEx -= m_axTws_contractDetailsEx;
            m_axTws.contractDetailsEnd -= m_axTws_contractDetailsEnd;
            
            OptionsCollected ();
        }
*/
        private void OptionsCollected ()
        {
 /*           if (this.btnBsCancel.InvokeRequired)
            {
                OptionsCollectedDelegate d = new OptionsCollectedDelegate (OptionsCollected);
                this.Invoke (d, new object[] { });
            }
            else
            {
                m_axTws.tickPrice += m_axTws_tickPrice;
                m_axTws.tickSize += m_axTws_tickSize;
                m_axTws.tickOptionComputation += m_axTws_tickOptionComputation;
                m_axTws.tickGeneric += m_axTws_tickGeneric;
                m_axTws.tickString += m_axTws_tickString;
                m_axTws.tickEFP += m_axTws_tickEFP;
                m_axTws.tickSnapshotEnd += m_axTws_tickSnapshotEnd; 

                /*  Now collect the option market data (bid, ask)
                 *  --------------------------------------------- */

 /*               m_q = new BestStrangleQueue (m_Log, m_axTws, m_Options);
                m_q.Done += OptionsMarketDataCollectionDone;
            }*/
        }

        /***********************************************************
         * 
         * property OptionList
         * 
         * ********************************************************/

        public List<OptionInfo> OptionList 
        {
            get
            {
                return m_Best;
            }
        }

        /*******************************************************************
         * 
         * OptionsMarketDataCollectionDone
         * 
         * ready to search to find the best option for a strangle
         * 
         * ****************************************************************/

        void OptionsMarketDataCollectionDone ()
        {
/*            if (this.btnBsCancel.InvokeRequired)
            {
                OptionsCollectedDelegate d = new OptionsCollectedDelegate (OptionsCollected);
                this.Invoke (d, new object[] { });
            }
            else
            {
                m_axTws.tickPrice -= m_axTws_tickPrice;
                m_axTws.tickSize -= m_axTws_tickSize;
                m_axTws.tickOptionComputation -= m_axTws_tickOptionComputation;
                m_axTws.tickGeneric -= m_axTws_tickGeneric;
                m_axTws.tickString -= m_axTws_tickString;
                m_axTws.tickEFP -= m_axTws_tickEFP;
                m_axTws.tickSnapshotEnd -= m_axTws_tickSnapshotEnd;

                double targetITM;

                if (!double.TryParse (tbBsTargetITM.Text, out targetITM))
                {
                    MessageBox.Show ("Need to specify a valid target ITM.");
                    return;
                }

                /* Scan through the options, and set the price if it hasn't already been set
                 * ------------------------------------------------------------------------- */
/*
                foreach (var opt in m_Options)
                {
                    if (opt.Price == null)
                    {
                        if (opt.Ask != null && opt.Bid != null)
                        {
                            opt.Price = (opt.Ask + opt.Bid) / 2.0;
                        }
                    }

                    ComputeProbITM (opt);
                }

                /* Scan the call option chain for the best option
                 * ---------------------------------------------- */

 /*               double closestCall = double.MaxValue;
                OptionInfo BestCall = null;
                double closestPut = double.MaxValue;
                OptionInfo BestPut = null;

                foreach (var opt in m_Options)
                {
                    if (opt.IfCall)
                    {
                        if (opt.ProbITM != null)
                        {
                            double diff = targetITM - (double) opt.ProbITM;
                            if (diff > 0 && diff < closestCall)
                            {
                                closestCall = diff;
                                BestCall = opt;
                            }
                        }
                    }
                    else
                    {
                        if (opt.ProbITM != null)
                        {
                            double diff = targetITM - (double) opt.ProbITM;
                            if (diff > 0 && diff < closestPut)
                            {
                                closestPut = diff;
                                BestPut = opt;
                            }
                        }
                    }
                }

                if (BestCall == null)
                {
                    MessageBox.Show ("Unable to compute best call. Probably missing iv data.");
                    return;
                }
                BestCall = BestCall.Copy ();

                if (BestPut == null)
                {
                    MessageBox.Show ("Unable to compute best put. Probably missing iv data.");
                    return;
                }

                BestPut = BestPut.Copy ();

                BestCall.IfSell = true;
                BestPut.IfSell = true;

                BestCall.BuyingPowerReduction = ComputeBuyingPowerReduction ((double) BestCall.UndPrice, (double) BestCall.Strike, (double) BestCall.Price);
                BestPut.BuyingPowerReduction = ComputeBuyingPowerReduction ((double) BestPut.UndPrice, (double) BestPut.Strike, (double) BestPut.Price);

                double bpr = Math.Max (BestCall.BuyingPowerReduction, BestCall.BuyingPowerReduction);
                BestCall.NoContracts = (int) Math.Round (m_iTargetMargin / bpr);
                BestPut.NoContracts = BestCall.NoContracts;

                m_Best = new List<OptionInfo> ();
                m_Best.Add (BestCall);
                m_Best.Add (BestPut);

                {
                    DataGridViewComboBoxColumn oc = (DataGridViewComboBoxColumn) dgvBestStrangle.Columns[colbsSTRIKE];
                    List<double> str = (from opt in m_Options
                               where opt.IfCall == true
                               orderby opt.Strike
                               select opt.Strike).ToList ();
                    oc.DataSource = str;
                }

                dgvBestStrangle.AutoGenerateColumns = false;
                dgvBestStrangle.DataSource = m_Best;

                int height = 0;
                foreach (DataGridViewRow row in dgvBestStrangle.Rows)
                {
                    height += row.Height;
                }

                int width = 0;
                foreach (DataGridViewColumn col in dgvBestStrangle.Columns)
                {
                    if (col.Visible)
                    {
                        width += col.Width;
                    }
                }
            }*/
        }

        /*****************************************************************
         * 
         * ComputeBuyingPowerReduction
         * 
         * **************************************************************/

        private double ComputeBuyingPowerReduction (double stockprice, double strike, double credit)
        {
            double bpr1 = (.2 * stockprice - Math.Abs (stockprice - strike) + credit) * 100.0;
            double bpr2 = (.1 * stockprice + credit) * 100.0;

            return Math.Max (bpr1, bpr2);
        }

        /***************************************************************
         * 
         * ComputeProbITM for given option
         * 
         * ************************************************************/

        private void ComputeProbITM (OptionInfo opt)
        {
             if (opt.ImpliedVolatility == null)
            {
                m_Log.Log (ErrorLevel.logERR, string.Format ("BESTSTRANGLE ComputeProbITM unable to compute Prob ITM for {0}, strike {1}", opt.Ticker, opt.Strike.ToString ()));
                return;

            }
            double vol = (double) opt.ImpliedVolatility / Math.Sqrt (365);
            double K = opt.Strike;
            double S = opt.UndPrice;

            /* If the underlying price is 0 
             * ----------------------------
             * then fetch the last price from the database. This sometimes happens off-hours for some
             * reason.... the underlying price is not returned */

            if (S == 0.0)
            {
                m_Log.Log (ErrorLevel.logERR, string.Format ("BESTSTRANGLE ComputeProbITM underlying strike price is undefined. {0}, strike {1}", opt.Ticker, opt.Strike.ToString ()));
                    return;
            }

//            DateTime Expires = DateTime.ParseExact (opt.Expiry, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime Expires = opt.Expiry;
            int DaysToExpire = (Expires - DateTime.Now).Days;

            double variance = vol * vol;
            double d2 = Math.Log (S / K, Math.E);
            d2 += -variance / 2 * DaysToExpire;
            d2 /= vol;
            d2 /= Math.Sqrt (DaysToExpire);

            if (!opt.IfCall)
            {
                opt.ProbITM = Phi.phi (-d2);
            }
            else
            {
                opt.ProbITM = Phi.phi (d2);
            }
        }

        /*****************************************************
         * 
         * contractDetailsEx event - for collecting option chain
         * 
         * **************************************************/
 /*       void m_axTws_contractDetailsEx (object sender, AxTWSLib._DTwsEvents_contractDetailsExEvent e)
        {
            IContract d = (IContract) e.contractDetails.summary;
            m_Log.Log (ErrorLevel.logINF, string.Format ("contractDetailsEx: Local Symbol: {0}, Expires: {1}, Strike: {2} Multiplier: {3} {4}", d.localSymbol, d.expiry, d.strike.ToString ("0000.00"), d.multiplier, d.conId.ToString ()));

            if (d.multiplier != "100")
            {
                m_Log.Log (ErrorLevel.logERR, "Skipped, due to wrong multiplier");
                return;
            }
            
            m_Options.Add (new OptionInfo (d.conId, d.currency, d.exchange, d.expiry, d.strike, d.symbol, d.localSymbol, d.multiplier, d.secId, d.secIdType, d.secType, d.right, d.tradingClass));
        }

        /********************************************************************
         * 
         * FetchOptionChain
         * 
         * *****************************************************************/
/*
        private void FetchOptionChain ()
        {
            m_axTws.contractDetailsEx += m_axTws_contractDetailsEx;
            m_axTws.contractDetailsEnd += m_axTws_contractDetailsEnd;

            m_Options = new List<OptionInfo> ();

            IContract contract = m_axTws.createContract ();

            contract.symbol = Utils.Massage (m_ticker);
            contract.secType = "OPT";

            DateTime expires = dtpBsExpires.Value;

            contract.expiry = expires.ToString ("yyyyMMdd");
            contract.strike = 0.0;
            //contract.right = ""; // will accept either
            contract.multiplier = "";
            contract.exchange = "SMART";
            contract.primaryExchange = "";
            contract.currency = "USD";
            contract.localSymbol = "";
            contract.includeExpired = 0;

            m_Log.Log (ErrorLevel.logINF, string.Format ("obtaining option chain for {0}", m_ticker));
            m_axTws.reqContractDetailsEx (0, contract);
        }

        void m_axTws_tickSnapshotEnd (object sender, AxTWSLib._DTwsEvents_tickSnapshotEndEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE snapshot end for {0}", e.reqId));
            if (m_q != null)
            {
                m_q.DecrementOutstandingCall ();
            }
        }

        void m_axTws_tickEFP (object sender, AxTWSLib._DTwsEvents_tickEFPEvent e)
        {
            throw new NotImplementedException ();
        }

        void m_axTws_tickString (object sender, AxTWSLib._DTwsEvents_tickStringEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE axTws_tickString called for {0:x} tickType:{1} {2} [{3}] ", e.id, e.tickType, TickType.Display (e.tickType), e.value));
        }

        void m_axTws_tickGeneric (object sender, AxTWSLib._DTwsEvents_tickGenericEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE axTws_tickGeneric for {0:x} tickType: {1} {2} value: {3}", e.id, e.tickType, TickType.Display (e.tickType), e.value));
        }

        /****************************************************************
         * 
         * m_axTws_tickOptionComputation
         * 
         * *************************************************************/

/*        void m_axTws_tickOptionComputation (object sender, AxTWSLib._DTwsEvents_tickOptionComputationEvent e)
        {
            double? price = null;
            //     1.79769e+308;
            // 1.79769313486232E+308
            m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE axTws_tickOptionComputation for {0:x} ticktype: {1} {2} value: {3} optPrice {3:F5} undPrice {4:F5}", e.id, e.tickType, TickType.Display (e.tickType), e.optPrice, e.undPrice));
            if (e.optPrice < double.MaxValue)
            {
                price = e.optPrice;
            }
            else
            {
                m_Log.Log (ErrorLevel.logERR, "BESTSTRANGLE axTws_tickOptionComputation price set to nil");
            }
            int index = e.id & 0xFFFF;

            OptionInfo option = m_Options[index];
 
            if (e.delta < double.MaxValue)
            {
                option.Delta = e.delta;
            }
            else
            {
                m_Log.Log (ErrorLevel.logERR, string.Format ("BESTSTRANGLE axTws_tickOptionComputation for {0:x} Bad delta. {1}.", e.id, e.delta.ToString ()));
            }
            option.Gamma = e.gamma;
            option.Theta = e.theta;
            option.Vega = e.vega;
            switch (e.tickType)
            {
                case TickType.BID_OPTION:
                    option.Bid = price;
                    break;
                case TickType.ASK_OPTION:
                    option.Ask = price;
                    break;
                case TickType.LAST_OPTION:
                    option.Last = price;
                    break;
                case TickType.MODEL_OPTION:
                    option.UndPrice = e.undPrice;
                    option.Price = e.optPrice;
                    m_Log.Log (ErrorLevel.logINF, "BESTSTRANGLE axTws_tickOptionComputation case 13");
                    break;

                default:
                    m_Log.Log (ErrorLevel.logSEV, "BESTSTRANGLE. Unknown tick type. Investigate.");
                    break;
            }
            if (e.undPrice < double.MaxValue)
            {
                option.UndPrice = e.undPrice;
            }
            if (e.impliedVol < double.MaxValue)
            {
                option.ImpliedVolatility = e.impliedVol;
            }
        }

        void m_axTws_tickSize (object sender, AxTWSLib._DTwsEvents_tickSizeEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE axTws_tickSize for {0:x} tickType:{1} {2} size: {3}", e.id, e.tickType, TickType.Display (e.tickType), e.size));
        }

        void m_axTws_tickPrice (object sender, AxTWSLib._DTwsEvents_tickPriceEvent e)
        {
            int index = e.id & 0xFFFF;

            switch (e.tickType)
            {
                case TickType.ASK:
                    m_Options[index].Ask = e.price;
                    break;
                case TickType.BID:
                    m_Options[index].Bid = e.price;
                    break;
                case TickType.HIGH:
                    //m_OptionInfo[index].Bid = e.price;
                    break;
                case TickType.LOW:
                    break;
                case TickType.CLOSE:
                    break;

                default:
                    m_Log.Log (ErrorLevel.logERR, string.Format ("BESTSTRANGLE axTws_tickPrice for {0:x} tickType:{1} {2} value: {3} remains unassigned", e.id, e.tickType, TickType.Display (e.tickType), e.price));
                    break;
            }
 
        }

        /*****************************************************
          * 
          * Form Closing
          * 
          * ***************************************************/
        private void frmBestStrangle_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (m_q != null)
            {
                m_q.Dispose ();
                m_q = null;
            }

            //m_axTws.contractDetailsEx -= m_axTws_contractDetailsEx;
            //m_axTws.contractDetailsEnd -= m_axTws_contractDetailsEnd;

            if (bSettingWidthsToBeSaved)
            {
                Properties.Settings.Default.BestStrangleColumnWidths = ColumnWidths;
                Properties.Settings.Default.BestStrangleTargetITM = m_dTargetITM;
                Properties.Settings.Default.BestStrangleTargetMargin = m_iTargetMargin;
                Properties.Settings.Default.BestStrangleDaysToExpire = int.Parse (tbBsDaysToExpire.Text);
                Properties.Settings.Default.BestStrangleIncludeWeeklies = cbBsIncludeWeeklies.Checked;

                Properties.Settings.Default.Save ();
            }
        }

        /*******************************************************
         * 
         * Column Width changes
         * 
         * ****************************************************/
        private void dgvBestStrangle_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                ColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvBestStrangle.Columns.Count; i++)
                {
                    ColumnWidths.Add (dgvBestStrangle.Columns[i].Width.ToString ());
                }
            }
        }

        /******************************************************
         * 
         * Form shown
         * 
         * ***************************************************/
        private void frmBestStrangle_Shown (object sender, EventArgs e)
        {
            tbBsDaysToExpire.Text = Properties.Settings.Default.BestStrangleDaysToExpire.ToString ();
            cbBsIncludeWeeklies.Checked = Properties.Settings.Default.BestStrangleIncludeWeeklies;
            m_dTargetITM = Properties.Settings.Default.BestStrangleTargetITM;
            m_iTargetMargin = Properties.Settings.Default.BestStrangleTargetMargin;
            tbBsTargetMargin.Text = m_iTargetMargin.ToString ();
            tbBsTargetITM.Text = m_dTargetITM.ToString ("F3");

            tbBsDaysToExpire_TextChanged (null, null);

            AdjustWidths ();
            bSettingWidthsToBeSaved = true; // can save now
        }

        /******************************************************
          * 
          * AdjustWidths
          * 
          * ***************************************************/

        private void AdjustWidths ()
        {
            ColumnWidths = Properties.Settings.Default.BestStrangleColumnWidths;

            for (int i = 0; i < dgvBestStrangle.Columns.Count; i++)
            {
                int col_width;
                if (i < ColumnWidths.Count)
                {
                    if (int.TryParse (ColumnWidths[i], out col_width))
                    {
                        dgvBestStrangle.Columns[i].Width = col_width;
                    }
                }
            }
        }

        /******************************************************
         * 
         * DaysToExpire changes
         * 
         * ***************************************************/

        private void tbBsDaysToExpire_TextChanged (object sender, EventArgs e)
        {
            int days;
            
            if (!int.TryParse (tbBsDaysToExpire.Text, out days))
            {
                MessageBox.Show ("Invalid days to expire value.");
                return;
            }

            if (cbBsIncludeWeeklies.Checked)
            {
                DateTime x = DateTime.Now + new TimeSpan (days, 0, 0, 0);
                if (DayOfWeek.Friday - x.DayOfWeek <= 3)
                {
                    x += new TimeSpan (DayOfWeek.Friday - x.DayOfWeek, 0, 0, 0);
                }
                else
                {
                    x -= new TimeSpan (7 - (DayOfWeek.Friday - x.DayOfWeek), 0, 0, 0);
                }
                dtpBsExpires.Value = x;
            }
            else
            {
                DateTime[] dt = new DateTime[3];
                DateTime prev = DateTime.Now;
                for (int i = 0; i < 3; i++)
                {
                    dt[i] = Utils.ComputeNextExpiryDate (prev);
                    prev = dt[i] + new TimeSpan (14, 0, 0, 0);
                }
                int MinNoDays = int.MaxValue;
                int index = 0;
                for (int i = 0; i < 3; i++)
                {
                    int NoDays = Math.Abs ((dt[i] - DateTime.Now).Days - days);
                    if (NoDays < MinNoDays)
                    {
                        MinNoDays = NoDays;
                        index = i;
                    }
                }

                dtpBsExpires.Value = dt[index];
            }
        }

        /*****************************************
         * 
         * Expiry date has changed
         * 
         * **************************************/

        private void dtpBsExpires_ValueChanged (object sender, EventArgs e)
        {
 //           FetchOptionChain ();
        }

        /*************************************************************
         * 
         * tbBsTargetMargin_Validating
         * 
         * **********************************************************/

        private void tbBsTargetMargin_Validating (object sender, CancelEventArgs e)
        {
            if (!int.TryParse (tbBsTargetMargin.Text, out m_iTargetMargin))
            {
                e.Cancel = true;
                tbBsTargetMargin.Select (0, tbBsTargetMargin.Text.Length);
            }
        }

        /************************************************************
         * 
         * tbBsTargetMargin_Validated
         * 
         * **********************************************************/

        private void tbBsTargetMargin_Validated (object sender, EventArgs e)
        {
            double bpr = 0;

            foreach (DataGridViewRow r in dgvBestStrangle.Rows)
            {
                OptionInfo opt = (OptionInfo) r.DataBoundItem;
                if (opt.BuyingPowerReduction > bpr)
                {
                    bpr = opt.BuyingPowerReduction;
                }
            }
            int no_contracts = (int) Math.Round (m_iTargetMargin / bpr);
            foreach (DataGridViewRow r in dgvBestStrangle.Rows)
            {
                OptionInfo opt = (OptionInfo) r.DataBoundItem;
                opt.NoContracts = no_contracts;
                dgvBestStrangle.InvalidateCell (r.Cells[colbsNOCONTRACTS]);
            }
        }

        /************************************************************
         * 
         * Cell value has changed
         * 
         * *********************************************************/

        private void dgvBestStrangle_CellEndEdit (object sender, DataGridViewCellEventArgs e)
        {
            OptionInfo opt = (OptionInfo) dgvBestStrangle.Rows[e.RowIndex].DataBoundItem;
            if (e.ColumnIndex == colbsSTRIKE)
            {
                foreach (var o in m_Options)
                {
                    if ((opt.IfCall == o.IfCall) && opt.Strike == o.Strike)
                    {
                        opt.Price = o.Price;
                        opt.Delta = o.Delta;
                        opt.BuyingPowerReduction = ComputeBuyingPowerReduction ((double) o.UndPrice, (double) o.Strike, (double) o.Price);
                        ComputeProbITM (opt);
                        dgvBestStrangle.InvalidateRow (e.RowIndex);
                        break;
                    }
                }
            }

        }

        /*****************************************************************
         * 
         * Estimate Commissions
         * 
         ****************************************************************/

        private void btnbsEstimateCommissions_Click (object sender, EventArgs e)
        {
/*            m_axTws.openOrderEx += m_axTws_openOrderEx;

            TWSLib.IOrder order = m_axTws.createOrder ();

            TWSLib.IComboLegList leglist = m_axTws.createComboLegList ();

            var nums = (from DataGridViewRow r in dgvBestStrangle.Rows
                          select ((OptionInfo) r.DataBoundItem).NoContracts).ToArray ();

            int gcd = Utils.GCD (nums);

            foreach (DataGridViewRow r in dgvBestStrangle.Rows)
            {
                OptionInfo opt = (OptionInfo) r.DataBoundItem;

                TWSLib.IComboLeg leg = (TWSLib.IComboLeg) leglist.Add ();
                leg.conId = opt.ConId;
                leg.ratio = opt.NoContracts / gcd;
                leg.action = "SELL";
                leg.exchange = "SMART";
                leg.openClose = 0;
                leg.shortSaleSlot = 0;
                leg.designatedLocation = "";
            }

            TWSLib.IContract con = m_axTws.createContract ();
            con.symbol = "USD";
            con.secType = "BAG";
            con.exchange = "SMART";
            con.currency = "USD";
            con.comboLegs = leglist;

            using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            {
                m_OrderId = dc.FetchOrderID ();
                order.orderId = m_OrderId;
            }

            order.action = "BUY";
            order.totalQuantity = gcd;
            order.orderType = "MKT";
            order.whatIf = 1;

            m_axTws.placeOrderEx (order.orderId, con, order);*/
        }

        /***********************************************************
         * 
         * openOrderEx event
         * 
         * ********************************************************/
/*
        void m_axTws_openOrderEx (object sender, AxTWSLib._DTwsEvents_openOrderExEvent e)
        {
            if (e.orderId == m_OrderId)
            {
                TWSLib.IContract con = e.contract;
                TWSLib.IOrderState ord = e.orderState;

                m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE symbol: {0} localsymbol {1}", con.symbol, con.localSymbol));
                m_Log.Log (ErrorLevel.logINF, string.Format ("BESTSTRANGLE min commission: {0:F3}, max commission: {0:F3}", ord.minCommission, ord.maxCommission));

                lbbsMinCommission.Text = string.Format ("{0:C2}", ord.minCommission);
                lbbsMaxCommission.Text = string.Format ("{0:C2}", ord.maxCommission);
                double m;
                if (double.TryParse (ord.initMargin, out m))
                {
                    lbbsInitMargin.Text = string.Format ("{0:C0}", m);
                }
                if (double.TryParse (ord.maintMargin, out m))
                {
                    lbbsMaintMargin.Text = string.Format ("{0:C0}", m);
                }

                m_axTws.openOrderEx -= m_axTws_openOrderEx;
            }
        }
*/
        /************************************************************
         * 
         * Accept button pressed
         * 
         * *********************************************************/
        
        private void btnBsAccept_Click (object sender, EventArgs e)
        {

        }
    }

    /***************************************************************
     * 
     * BestStrangleQueue class
     * 
     * ************************************************************/

    class BestStrangleQueue : IDisposable
    {
        public delegate void DoneHandler ();

        public event DoneHandler Done;

        private Queue<int> m_q;
        private Thread m_thr;
        private LogCtl m_Log;
        private AutoResetEvent m_Event;
        private int m_OutstandingCalls;
        private object locker = new object ();
        private bool m_bAbort;
//        private AxTWSLib.AxTws m_axTws;
        private List<OptionInfo> m_Options;

        /****************************
         * 
         * Constructor 
         * 
         * *************************/

        public BestStrangleQueue (LogCtl log,/* AxTWSLib.AxTws axtws,*/ List<OptionInfo> options)
        {
            m_bAbort = false;
            m_OutstandingCalls = 0;
            m_Log = log;
//            m_axTws = axtws;
            m_Options = options;
            m_Event = new AutoResetEvent (true); // make sure set to go 
            m_q = new Queue<int> ();

            for (int index = 0; index < m_Options.Count; index++)
            {
                m_q.Enqueue (index);
            }
            m_thr = new Thread (new ThreadStart (this.FetchOptionData));
            m_thr.Start ();
        }

        /******************************************************
         * 
         * DecrementOutstandingCall
         * 
         * ***************************************************/

        public void DecrementOutstandingCall ()
        {
            lock (locker)
            {
                m_OutstandingCalls--;
            }
            m_Event.Set ();
            if (m_OutstandingCalls == 0)
            {
                if (Done != null)
                {
                    Done ();
                }
            }
        }

        /************************************************************
         * 
         * worker thread fetching option data
         * 
         * *********************************************************/
        private void FetchOptionData ()
        {
 /*           while (!m_bAbort)
            {
                while (m_Event.WaitOne (500))
                {
                    while (m_q.Count > 0 && m_OutstandingCalls < 75)
                    {
                        int index;
                        lock (m_q)
                        {
                            index = m_q.Dequeue ();
                        }

                        IContract contract = m_axTws.createContract ();

                        contract.symbol = "";
                        contract.secType = "OPT";
                        contract.exchange = "SMART";
                        contract.localSymbol = m_Options[index].LocalSymbol;

                        m_axTws.reqMktDataEx (index | Utils.ibBESTSTRANGLE, contract, "", 1, null);

                        lock (locker)
                        {
                            m_OutstandingCalls++;
                        }
                    }
                }
            }*/
        }

        /*******************************************************
         * 
         * Internal routine for disposing our resource here
         * 
         * *****************************************************/

        private void Dispose (bool IfDisposing)
        {
            /* Dispose unmanaged resources here
             * -------------------------------- */

            if (IfDisposing)
            {
            }

            /* Dispose managed resources here
             * ------------------------------ */

            if (m_thr.IsAlive)
            {
                Stop ();
            }

            m_Log.Log (ErrorLevel.logINF, "BestStrangle.Dispose called.");
        }

        /*******************************************************
         * 
         * Dispose ()
         * 
         * *****************************************************/

        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
            m_Log.Log (ErrorLevel.logINF, "BestStrangle.Dispose called.");
        }

        /*******************************************************
         * 
         * Stop the thread
         * 
         * ****************************************************/

        private void Stop ()
        {
            m_Log.Log (ErrorLevel.logINF, "BESTSTRANGLE stopping thread in BestStrangleQueue");
            m_bAbort = true;
            m_Event.Set ();

            if (!m_thr.Join (5000))
            {
                m_Log.Log (ErrorLevel.logERR, "BESTSTRANGLE failed to stop thread in a timely manner. Will abort thread");
                m_thr.Abort ();
            }
        }
    }
}
