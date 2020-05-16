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

namespace IBManagePositions
{
    public partial class frmCloseTrade : Form
    {
        const int clscolTICKER = 1;
        const int clscolCALLPUT = 3;
        const int clscolBUYSELL = 5;

        int m_OrderId;

        delegate void SpreadDelegate ();

        bool bSettingWidthsToBeSaved = false;
        //SortableBindingList<LegData> m_Legs;
        List<LegData> m_Legs;
//        AxTWSLib.AxTws m_axTws;
        frmPos m_Pos;
        LogCtl m_Log;

        private System.Collections.Specialized.StringCollection ColumnWidths;
        private bool m_bInvalidPositions = false;

        /***********************************************
         * 
         * Constructor
         * 
         * ********************************************/

//        internal frmCloseTrade (frmPos pos, LogCtl log, AxTWSLib.AxTws axtws, List<LegData> Legs)
        internal frmCloseTrade (frmPos pos, LogCtl log, /*AxTWSLib.AxTws axtws,*/ List<LegData> Legs)
        {
            InitializeComponent ();

//            m_axTws = axtws;
            //m_Legs = new SortableBindingList<LegData> (Legs);
            m_Legs = Legs;
            m_Pos = pos;
            m_Log = log;

            lbMissingRemotePosition.Visible = false;

            dgvCloseTrade.AutoGenerateColumns = false;
            dgvCloseTrade.DataSource = m_Legs;

            dgvCloseTrade.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvCloseTrade.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvCloseTrade.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvCloseTrade.DefaultCellStyle.SelectionBackColor = Color.FromArgb (252, 150, 29);
            dgvCloseTrade.DefaultCellStyle.SelectionForeColor = Color.Black;

            CheckForValidPositions ();

            FetchBidAskMarketData ();
        }

        /***********************************************************
         * 
         * Check for valid IB positions
         * 
         * ********************************************************/

        private async Task CheckForValidPositions ()
        {
            List<LegData> ib = await frmPos.FetchIBPositions ();
 
            foreach (DataGridViewRow r in dgvCloseTrade.Rows)
            {
                bool bFound = false;

                LegData leg = (LegData) r.DataBoundItem;
                foreach (var l in ib)
                {
                    if (leg.LocalSymbol == l.LocalSymbol)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    r.DefaultCellStyle.BackColor = Utils.colRED;
                    lbMissingRemotePosition.Visible = true;
                    m_bInvalidPositions = true;
                }
            }
        }

        /*********************************************
         * 
         * Formatting Call/Put and Buy/Sell
         * 
         * ******************************************/

        private void dgvCloseTrade_CellFormatting (object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == clscolCALLPUT)
            {
                if (e.Value is bool)
                {
                    if ((bool) e.Value)
                    {
                        e.Value = "Call";
                    }
                    else
                    {
                        e.Value = "Put";
                    }
                    e.FormattingApplied = true;
                }
            }
            if (e.ColumnIndex == clscolBUYSELL)
            {
                if (e.Value is bool)
                {
                    if ((bool) e.Value)
                    {
                        e.Value = "Sell";
                    }
                    else
                    {
                        e.Value = "Buy";
                    }
                    e.FormattingApplied = true;
                }
            }
        }

        /*****************************************************
         * 
         * Form Closing
         * 
         * ***************************************************/

        private void frmCloseTrade_FormClosing (object sender, FormClosingEventArgs e)
        {
            StopBidAskMarketData ();

            if (bSettingWidthsToBeSaved)
            {
                Properties.Settings.Default.CloseTradeColumnWidths = ColumnWidths;
                Properties.Settings.Default.Save ();
            }


        }

        /*******************************************************
         * 
         * Column Width changes
         * 
         * ****************************************************/

        private void dgvCloseTrade_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                ColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvCloseTrade.Columns.Count; i++)
                {
                    ColumnWidths.Add (dgvCloseTrade.Columns[i].Width.ToString ());
                }
            }
        }

        /******************************************************
         * 
         * Form shown
         * 
         * ***************************************************/

        private void frmCloseTrade_Shown (object sender, EventArgs e)
        {
            AdjustWidths ();
            bSettingWidthsToBeSaved = true; // can save now

            AdjustRows ();
        }

        /********************************************************
         * 
         * Adjust Rows
         * 
         * *****************************************************/

        private void AdjustRows ()
        {
            int height = 0;
            foreach (DataGridViewRow row in dgvCloseTrade.Rows)
            {
                height += row.Height;
            }

            int width = 0;
            foreach (DataGridViewColumn col in dgvCloseTrade.Columns)
            {
                if (col.Visible)
                {
                    width += col.Width;
                }
            }
//            width += dgvCloseTrade.RowHeadersWidth;

            dgvCloseTrade.ClientSize = new Size (width, height);
            dgvCloseTrade.ClearSelection ();
        }

        /******************************************************
         * 
         * AdjustWidths
         * 
         * ***************************************************/

        private void AdjustWidths ()
        {
            ColumnWidths = Properties.Settings.Default.CloseTradeColumnWidths;

            for (int i = 0; i < dgvCloseTrade.Columns.Count; i++)
            {
                int col_width;
                if (i < ColumnWidths.Count)
                {
                    if (int.TryParse (ColumnWidths[i], out col_width))
                    {
                        dgvCloseTrade.Columns[i].Width = col_width;
                    }
                }
            }
        }

        /************************************************
         * 
         * Prevent row selection
         * 
         * **********************************************/

        //private void dgvCloseTrade_SelectionChanged (object sender, EventArgs e)
        //{
        //    //dgvCloseTrade.ClearSelection ();
        //}

        /*********************************************************
         * 
         * Fetch market data for this trade
         * 
         * *******************************************************/

        private void FetchBidAskMarketData ()
        {
/*            m_Log.Log (ErrorLevel.logINF, "CLOSETRADE FetchBidAskMarketData() called");

            lbClAsk.Text = "";
            lbClBid.Text = "";
            lbClNat.Text = "";
            tbClMyPrice.Text = "";

            m_axTws.tickPrice += m_axTws_tickPrice;
            m_axTws.tickSize += m_axTws_tickSize;
            m_axTws.tickOptionComputation += m_axTws_tickOptionComputation;
            m_axTws.tickGeneric += m_axTws_tickGeneric;
            m_axTws.tickString += m_axTws_tickString;
            m_axTws.tickEFP += m_axTws_tickEFP;
            m_axTws.tickSnapshotEnd += m_axTws_tickSnapshotEnd;

            m_axTws.reqMarketDataType (m_Pos.cbTradeFrozenData.Checked ? 2 : 1);

            TWSLib.IContract contract = m_axTws.createContract ();

            for(int index = 0; index < m_Legs.Count; index++)
            {
                LegData leg = m_Legs[index];

                contract.symbol = "";
                if (leg.Equity == EquityType.FutOpt)
                {
                    contract.secType = "FOP";
                }
                else if (leg.Equity == EquityType.Future)
                {
                    contract.secType = "FUT";
                }
                else if (leg.Equity == EquityType.Option)
                {
                    contract.secType = "OPT";
                }
                else if (leg.Equity == EquityType.Stock)
                {
                    contract.secType = "STK";
                }

                //contract.exchange = "SMART";
                contract.exchange = leg.Exchange;

                //if (leg.ConId == null)
                //{
                //    MessageBox.Show ("One of the legs has a missing conId. Something is not right.");
                //    return;
                //}
                //contract.conId = (int) leg.ConId;
                if (string.IsNullOrEmpty (leg.LocalSymbol))
                {
                    MessageBox.Show ("One of the legs has a missing localsymbol. Something is not right.");
                    return;
                }
                contract.localSymbol = leg.LocalSymbol;

                leg.bIfUpdatingMarketData = true;

                if (m_Pos.cbTradeSnapshot.Checked)
                {
                    m_Log.Log (ErrorLevel.logDEB, string.Format ("CLOSETRADE reqMktDataEx (snapshot) index: {0} equitytype:{6} localSymbol:[{1}] Ticker:{2} C/P:{3} S/B:{4} strike:{5:N2}",
                                            index.ToString (),
                                            contract.localSymbol,
                                            leg.Ticker,
                                            leg.DisplayCall (),
                                            leg.IfSell ? "Sell" : "Buy",
                                            leg.Strike,
                                            Utils.EquityType2String (leg.Equity)));



                    m_axTws.reqMktDataEx (Utils.ibCLOSETRADE | index, contract, "", 1, null);
                }
                else
                {
                    m_Log.Log (ErrorLevel.logDEB, string.Format ("CLOSETRADE reqMktDataEx (active) index: {0} equitytype:{6} localSymbol:[{1}] Ticker:{2} C/P:{3} S/B:{4} strike:{5:N2}",
                                            index.ToString (),
                                            contract.localSymbol,
                                            leg.Ticker,
                                            leg.DisplayCall (),
                                            leg.IfSell ? "Sell" : "Buy",
                                            leg.Strike,
                                            Utils.EquityType2String (leg.Equity)));


                    m_axTws.reqMktDataEx (Utils.ibCLOSETRADE | index, contract, "106", 0, null);
                }
            }*/
        }

        /********************************************************************************
         * 
         * Option computation update
         * 
         * *****************************************************************************/
/*
        void m_axTws_tickOptionComputation (object sender, AxTWSLib._DTwsEvents_tickOptionComputationEvent e)
        {
            if ((e.id & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }

            int index = e.id & 0xFFFF;

            double? price = null;
            //     1.79769e+308;
            // 1.79769313486232E+308
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickOptionComputation for {0} ticktype: {1} {2} value: {3} for ID {4}", m_Legs[index].Ticker, e.tickType, TickType.Display (e.tickType), e.optPrice, index));
            if (e.optPrice < double.MaxValue && e.optPrice != -1) // if volume is 0, I notice that the BID and ASK might be -1
            {
                price = e.optPrice;
            }
            else
            {
                m_Log.Log (ErrorLevel.logERR, "axTws_tickOptionComputation price set to nil");
            }
            LegData leg = m_Legs[index];

            switch (e.tickType)
            {
                case TickType.BID_OPTION:
                    leg.CurrBid = price;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE CurrBid {0:F2}", leg.CurrBid));
                    UpdateSpread ();
                    break;

                case TickType.ASK_OPTION:
                    leg.CurrAsk = price;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE CurrAsk {0:F2}", leg.CurrAsk));
                    UpdateSpread ();
                    break;

                case TickType.LAST_OPTION:
                    if (price < double.MaxValue)
                    {
                        leg.LastPrice = price;
                        m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE Last {0:F2}", leg.LastPrice));
                        UpdateSpread ();
                        //LegUpdated (m_Legs[index], legcolLAST);
                        //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colLAST]);
                    }
                    break;

                case TickType.MODEL_OPTION:
                    leg.ModelOption = price;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE axTws_tickOptionComputation case 13 ModelOption: {0:F2}", leg.ModelOption));
                    UpdateSpread ();
                    break;

                default:
                    m_Log.Log (ErrorLevel.logSEV, "CLOSETRADE Bug in axTws_tickOptionComputation");
                    break;
            }

}

        /*********************************************************************
         * 
         * UpdateSpread
         * 
         * ******************************************************************/

        private void UpdateSpread ()
        {
            m_Log.Log (ErrorLevel.logINF, "CLOSETRADE UpdateSpread() called");

            if (lbClAsk.InvokeRequired)
            {
                SpreadDelegate d = new SpreadDelegate (UpdateSpread);
                this.Invoke (d, new object[] { });
            }
            else
            {
                double? Bid, Ask, Nat, Last;
                Bid = 0;
                Ask = 0;
                Nat = 0;
                Last = 0;

                m_Log.Log (ErrorLevel.logINF, "UpdateSpread");
                int i = 0;
                foreach (LegData leg in m_Legs)
                {
                    m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE UpdateSpread {3} B: {0} A: {1} L: {2}", leg.CurrBid == null ? "null" : ((double) leg.CurrBid).ToString ("F2"),
                                                                                         leg.CurrAsk == null ? "null" : ((double) leg.CurrAsk).ToString ("F2"),
                                                                                         leg.LastPrice == null ? "null" : ((double) leg.LastPrice).ToString ("F2"), i++));
                    if (!leg.IfSell)
                    {
                        Bid -= leg.CurrBid;
                        Ask -= leg.CurrAsk;
                        Nat -= (leg.CurrBid + leg.CurrAsk) / 2.0;
                        Last -= leg.LastPrice;
                        if (Nat == null || Nat == -1)
                        {
                            Nat = -leg.ModelOption;
                        }
                    }
                    else
                    {
                        Bid += leg.CurrBid;
                        Ask += leg.CurrAsk;
                        Nat += (leg.CurrBid + leg.CurrAsk) / 2.0;
                        Last += leg.LastPrice;
                        if (Nat == null || Nat == -1)
                        {
                            Nat = -leg.ModelOption;
                        }
                    }
                }
                if (Ask != null)
                {
                    lbClAsk.Text = ((double) Ask).ToString ("F2");
                    lbClAsk.Invalidate ();
                }
                if (Bid != null)
                {
                    lbClBid.Text = ((double) Bid).ToString ("F2");
                    lbClBid.Invalidate ();
                }
                if (Nat != null)
                {
                    lbClNat.Text = ((double) Nat).ToString ("F2");
                    lbClNat.Invalidate ();

                    if (string.IsNullOrEmpty (tbClMyPrice.Text))
                    {
                        tbClMyPrice.Text = lbClNat.Text;
                    }
                }
                if (Last != null)
                {
                    lbClLast.Text = ((double) Last).ToString ("F2");
                    lbClLast.Invalidate ();
                }
            }
        }

        /*******************************************************************
         * 
         * tickPrice
         * 
         * ****************************************************************/
/*
        void m_axTws_tickPrice (object sender, AxTWSLib._DTwsEvents_tickPriceEvent e)
        {
            if ((e.id & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            int index = e.id & 0xFFFF;
            LegData leg = m_Legs[index];
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickPrice for {0} tickType:{1} {2} value: {3}", leg.Ticker, e.tickType, TickType.Display (e.tickType), e.price, e.id & 0xFFFF));
            switch (e.tickType)
            {
                case 1: // bid
                    leg.CurrBid = e.price;
                    UpdateSpread ();
                    break;

                case 2: // ask
                    leg.CurrAsk = e.price;
                    UpdateSpread ();
                    break;

                case 4:
                //case 9: // close
                    leg.LastPrice = e.price;
                    UpdateSpread ();
                    break;

                default:
                    break;

            }
        }
        void m_axTws_tickSize (object sender, AxTWSLib._DTwsEvents_tickSizeEvent e)
        {
            if ((e.id & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickSize for {0} tickType:{1} {2} size: {3}", m_Legs[e.id & 0xFFFF].Ticker, e.tickType, TickType.Display (e.tickType), e.size, e.id & 0xFFFF));
        }
        void m_axTws_tickString (object sender, AxTWSLib._DTwsEvents_tickStringEvent e)
        {
            if ((e.id & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickString called for {0} tickType:{1} {2} [{3}] ", m_Legs[e.id & 0xFFFF].Ticker, e.tickType, TickType.Display (e.tickType), e.value, e.id & 0xFFFF));
        }
        void m_axTws_tickGeneric (object sender, AxTWSLib._DTwsEvents_tickGenericEvent e)
        {
            if ((e.id & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickGeneric for {0} tickType: {1} {2} value: {3}", m_Legs[e.id & 0xFFFF], e.tickType, TickType.Display (e.tickType), e.value, e.id & 0xFFFF));
        }

        void m_axTws_tickEFP (object sender, AxTWSLib._DTwsEvents_tickEFPEvent e)
        {
            if ((e.tickerId & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {3} axTws_tickEFP for {0}", m_Legs[e.tickerId & 0xFFFF]));
        }

        void m_axTws_tickSnapshotEnd (object sender, AxTWSLib._DTwsEvents_tickSnapshotEndEvent e)
        {
            if ((e.reqId & 0xFFFF0000) != Utils.ibCLOSETRADE)
            {
                return;
            }
            m_Log.Log (ErrorLevel.logINF, string.Format ("CLOSETRADE {1} axTws_tickSnapshotEnd for {0}", m_Legs[e.reqId & 0xFFFF].Ticker, e.reqId & 0xFFFF));
        }

        /********************************************************************
         * 
         * Place the close order
         * 
         * *****************************************************************/

        private void btnClPlaceOrder_Click (object sender, EventArgs e)
        {
 /*           if (string.IsNullOrEmpty (tbClMyPrice.Text))
            {
                MessageBox.Show ("Need to enter a price.");
                return;
            }

            double price;
            if (!double.TryParse (tbClMyPrice.Text, out price))
            {
                MessageBox.Show ("Enter a valid price.");
                return;
            }

            if (m_bInvalidPositions)
            {
                DialogResult rc = MessageBox.Show ("Some invalid positions have been detected. Are you sure you want to close?", "Invalid Positions", MessageBoxButtons.YesNo);
                if (rc == DialogResult.No)
                {
                    return;
                }
            }
            if (m_Legs.Count > 1)
            {
                TWSLib.IOrder order = m_axTws.createOrder ();

                TWSLib.IComboLegList ComboLegList = m_axTws.createComboLegList ();

                var nums = (from LegData leg in m_Legs
                            select (leg.NoContracts)).ToArray ();
                int gcd = Utils.GCD (nums);

                foreach (LegData leg in m_Legs)
                {
                    TWSLib.IComboLeg comboleg = ComboLegList.Add () as TWSLib.IComboLeg;
                    comboleg.conId = (int) leg.ConId;
                    comboleg.ratio = leg.NoContracts / gcd;
                    if (leg.IfSell)
                    {
                        comboleg.action = "BUY"; // buy back the sold option
                    }
                    else
                    {
                        comboleg.action = "SELL";
                    }
                    comboleg.openClose = 0;
                    comboleg.shortSaleSlot = 0;
                    comboleg.designatedLocation = "";
                }

                TWSLib.IContract contract = m_axTws.createContract ();

                contract.symbol = "USD";
                contract.secType = "BAG";
                contract.currency = "USD";
                contract.exchange = "SMART";
                contract.comboLegs = ComboLegList;

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    m_OrderId = dc.FetchOrderID ();
                }

                order.action = "BUY";
                order.totalQuantity = gcd;
                order.orderType = "LMT";
                order.lmtPrice = price;
                order.allOrNone = 1;

                m_axTws.placeOrderEx (m_OrderId, contract, order);
            }
            else 
            {
                /* only a single leg, so create a single order
                 * ------------------------------------------- */
/*
                TWSLib.IOrder order = m_axTws.createOrder ();

                TWSLib.IComboLegList ComboLegList = m_axTws.createComboLegList ();

                var nums = (from LegData leg in m_Legs
                            select (leg.NoContracts)).ToArray ();
                int gcd = Utils.GCD (nums);

                LegData l = m_Legs[0];

                TWSLib.IContract contract = m_axTws.createContract ();

                contract.symbol = "";
                contract.conId = (int) l.ConId;
                contract.secType = "OPT";
                contract.currency = "USD";
                contract.exchange = "SMART";

                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    m_OrderId = dc.FetchOrderID ();
                }

                if (l.IfSell)
                {
                    order.action = "BUY";
                }
                else
                {
                    order.action = "SELL";
                }
                order.totalQuantity = l.NoContracts;
                order.orderType = "LMT";
                order.lmtPrice = price;

                m_axTws.placeOrderEx (m_OrderId, contract, order);

                l.CloseOrderId = m_OrderId;
            }
            foreach (LegData leg in m_Legs)
            {
                leg.CloseOrderId = m_OrderId;
            }
*/
        }

        /**********************************************************
         * 
         * user has deleted a row
         * 
         * *******************************************************/

        private void dgvCloseTrade_UserDeletedRow (object sender, DataGridViewRowEventArgs e)
        {
            AdjustRows ();

            FetchBidAskMarketData ();
        }

        /*********************************************************
         * 
         * User is about to delete a row
         * 
         * ******************************************************/

        private void dgvCloseTrade_UserDeletingRow (object sender, DataGridViewRowCancelEventArgs e)
        {
            StopBidAskMarketData ();
        }

        private void StopBidAskMarketData ()
        {
 /*           m_Log.Log (ErrorLevel.logINF, "StopBidAskMarketData() called");
            for (int i = 0; i < m_Legs.Count; i++)
            {
                m_axTws.cancelMktData (Utils.ibCLOSETRADE | i);
            }

            m_axTws.tickPrice -= m_axTws_tickPrice;
            m_axTws.tickSize -= m_axTws_tickSize;
            m_axTws.tickOptionComputation -= m_axTws_tickOptionComputation;
            m_axTws.tickGeneric -= m_axTws_tickGeneric;
            m_axTws.tickString -= m_axTws_tickString;
            m_axTws.tickEFP -= m_axTws_tickEFP;
            m_axTws.tickSnapshotEnd -= m_axTws_tickSnapshotEnd;
 */       }
    }
}
