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

namespace IBManagePositions
{
    public partial class frmOpenTrade : Form
    {
        const int opcolCALLPUT = 3;
        const int opcolBUYSELL = 5;

        int m_OrderId;

        delegate void SpreadDelegate ();

        bool bSettingWidthsToBeSaved = false;
        List<LegData> m_Legs;
        //AxTWSLib.AxTws m_axTws;
        frmPos m_Pos;
        LogCtl m_Log;

        private System.Collections.Specialized.StringCollection ColumnWidths;

        /***********************************************
         * 
         * Constructor
         * 
         * ********************************************/

        internal frmOpenTrade (frmPos pos, LogCtl log, /*AxTWSLib.AxTws axtws,*/ List<LegData> Legs)
        {
            InitializeComponent ();

            //m_axTws = axtws;
 /*           m_Legs = Legs;
            m_Pos = pos;
            m_Log = log;

            dgvOpenTrade.AutoGenerateColumns = false;
            dgvOpenTrade.DataSource = m_Legs;

            dgvOpenTrade.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dgvOpenTrade.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgvOpenTrade.CellBorderStyle = DataGridViewCellBorderStyle.None;

            m_axTws.tickPrice += m_axTws_tickPrice; 
            m_axTws.tickSize += m_axTws_tickSize; 
            m_axTws.tickOptionComputation += m_axTws_tickOptionComputation;
            m_axTws.tickGeneric +=m_axTws_tickGeneric;
            m_axTws.tickString +=m_axTws_tickString; 
            m_axTws.tickEFP += m_axTws_tickEFP;
            m_axTws.tickSnapshotEnd +=m_axTws_tickSnapshotEnd;


            FetchOpenTradeMarketData ();*/
        }

        /*********************************************
         * 
         * Formatting Call/Put and Buy/Sell
         * 
         * ******************************************/

        private void dgvOpenTrade_CellFormatting (object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == opcolCALLPUT)
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
            if (e.ColumnIndex == opcolBUYSELL)
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

        private void frmOpenTrade_FormClosing (object sender, FormClosingEventArgs e)
        {
 /*           for (int i = 0; i < m_Legs.Count; i++)
            {
                m_axTws.cancelMktData (Utils.ibOPENTRADE | i);
            }  
            
            m_axTws.tickPrice -= m_axTws_tickPrice;
            m_axTws.tickSize -= m_axTws_tickSize;
            m_axTws.tickOptionComputation -= m_axTws_tickOptionComputation;
            m_axTws.tickGeneric -= m_axTws_tickGeneric;
            m_axTws.tickString -= m_axTws_tickString;
            m_axTws.tickEFP -= m_axTws_tickEFP;
            m_axTws.tickSnapshotEnd -= m_axTws_tickSnapshotEnd;

            if (bSettingWidthsToBeSaved)
            {
                Properties.Settings.Default.OpenTradeColumnWidths = ColumnWidths;
                Properties.Settings.Default.Save ();
            }
*/

        }

        /*******************************************************
         * 
         * Column Width changes
         * 
         * ****************************************************/

        private void dgvOpenTrade_ColumnWidthChanged (object sender, DataGridViewColumnEventArgs e)
        {
            if (bSettingWidthsToBeSaved)
            {
                ColumnWidths = new System.Collections.Specialized.StringCollection ();

                for (int i = 0; i < dgvOpenTrade.Columns.Count; i++)
                {
                    ColumnWidths.Add (dgvOpenTrade.Columns[i].Width.ToString ());
                }
            }
        }

        /******************************************************
         * 
         * Form shown
         * 
         * ***************************************************/

        private void frmOpenTrade_Shown (object sender, EventArgs e)
        {
            AdjustWidths ();
            bSettingWidthsToBeSaved = true; // can save now

            int height = 0;
            foreach (DataGridViewRow row in dgvOpenTrade.Rows)
            {
                height += row.Height;
            }

            int width = 0;
            foreach (DataGridViewColumn col in dgvOpenTrade.Columns)
            {
                if (col.Visible)
                {
                    width += col.Width;
                }
            }
//            width += dgvOpenTrade.RowHeadersWidth;

            dgvOpenTrade.ClientSize = new Size (width, height);

        }

        /******************************************************
         * 
         * AdjustWidths
         * 
         * ***************************************************/

        private void AdjustWidths ()
        {
            ColumnWidths = Properties.Settings.Default.OpenTradeColumnWidths;

            for (int i = 0; i < dgvOpenTrade.Columns.Count; i++)
            {
                int col_width;
                if (i < ColumnWidths.Count)
                {
                    if (int.TryParse (ColumnWidths[i], out col_width))
                    {
                        dgvOpenTrade.Columns[i].Width = col_width;
                    }
                }
            }
        }

        /************************************************
         * 
         * Prevent row selection
         * 
         * **********************************************/

        private void dgvOpenTrade_SelectionChanged (object sender, EventArgs e)
        {
            dgvOpenTrade.ClearSelection ();
        }

        /*********************************************************
         * 
         * Fetch market data for this trade
         * 
         * *******************************************************/

        private void FetchOpenTradeMarketData ()
        {
/*            m_axTws.reqMarketDataType (m_Pos.cbTradeFrozenData.Checked ? 2 : 1);

            TWSLib.IContract contract = m_axTws.createContract ();

            for(int index = 0; index < m_Legs.Count; index++)
            {
                LegData leg = m_Legs[index];

                contract.symbol = "";
                contract.secType = "OPT";
                contract.exchange = "SMART";
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
                    m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE reqMktDataEx (snapshot) {0} {1} {3} {4} {5} {6:N2} conid: {2}",
                                                                index.ToString (),
                                                                contract.localSymbol,
                                                                contract.conId,
                                                                leg.Ticker,
                                                                leg.DisplayCall (),
                                                                leg.IfSell ? "Sell" : "Buy",
                                                                leg.Strike));

                    m_axTws.reqMktDataEx (Utils.ibOPENTRADE | index, contract, "", 1, null);
                }
                else
                {
                    m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE reqMktDataEx (active) {0} {1} {3} {4} {5} {6:N2} conid: {2}",
                                                                index.ToString (),
                                                                contract.localSymbol,
                                                                contract.conId,
                                                                leg.Ticker,
                                                                leg.DisplayCall (),
                                                                leg.IfSell ? "Sell" : "Buy",
                                                                leg.Strike));

                    m_axTws.reqMktDataEx (Utils.ibOPENTRADE | index, contract, "106", 0, null);
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
            int index = e.id & 0xFFFF;

            double? price = null;
            //     1.79769e+308;
            // 1.79769313486232E+308
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickOptionComputation for {0} ticktype: {1} {2} value: {3} for ID {4}", m_Legs[index].Ticker, e.tickType, TickType.Display (e.tickType), e.optPrice, index));
            if (e.optPrice < double.MaxValue)
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
                    m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE CurrBid {0:F2}", leg.CurrBid));
                    UpdateSpread ();
                    //LegUpdated (m_Legs[index], legcolBID);
                    //m_Legs[index].ComputeProfitFigures ();
                    //LegUpdated (m_Legs[index], legcolTOTALPROFIT);
                    //LegUpdated (m_Legs[index], legcolPERCENTPROFIT);
                    ////dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colBID]);
                    //m_Legs[index].EstimateCommissions ();
                    break;
                case TickType.ASK_OPTION:
                    leg.CurrAsk = price;
                    m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE CurrAsk {0:F2}", leg.CurrAsk));
                    UpdateSpread ();
                    //LegUpdated (m_Legs[index], legcolASK);
                    //m_Legs[index].ComputeProfitFigures ();
                    //LegUpdated (m_Legs[index], legcolTOTALPROFIT);
                    //LegUpdated (m_Legs[index], legcolPERCENTPROFIT);
                    ////dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colASK]);
                    //m_Legs[index].EstimateCommissions ();
                    break;
                case TickType.LAST_OPTION:
                    if (price < double.MaxValue)
                    {
                        leg.LastPrice = price;
                        m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE Last {0:F2}", leg.LastPrice));
                        UpdateSpread ();
                        //LegUpdated (m_Legs[index], legcolLAST);
                        //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colLAST]);
                    }
                    break;
                case TickType.MODEL_OPTION:
                    m_Log.Log (ErrorLevel.logINF, "OPENTRADE axTws_tickOptionComputation case 13");
                    break;

                default:
                    m_Log.Log (ErrorLevel.logSEV, "OPENTRADE Bug in axTws_tickOptionComputation");
                    break;
            }
            //if (e.undPrice < double.MaxValue)
            //{
            //    leg.UnderlyingPrice = e.undPrice;
            //    LegUpdated (m_Legs[index], legcolUNDPRICE);
            //    //dgvPositions.InvalidateCell (dgvPositions.Rows[index].Cells[colUNDPRICE]);
            //}
            //m_Log.Log (ErrorLevel.logINF, string.Format ("axTws_tickOptionComputation for {0} Underlying price {1}.", m_Legs[index].Ticker, e.undPrice.ToString ()));
            //if (e.impliedVol < double.MaxValue)
            //{
            //    if (leg.iv != e.impliedVol)
            //    {
            //        leg.iv = e.impliedVol;
            //        m_Log.Log (ErrorLevel.logINF, string.Format ("axTws_tickOptionComputation for {0} iv={1:F4}.", m_Legs[index].Ticker, e.impliedVol));
            //        m_Legs[index].ComputeProbITM ();
            //        LegUpdated (m_Legs[index], legcolPROBITM);
            //    }
            //}
            //else
            //{
            //    m_Log.Log (ErrorLevel.logERR, string.Format ("axTws_tickOptionComputation for {0} Bad IV. {1}.", m_Legs[index].Ticker, e.impliedVol.ToString ()));
            //}

        }

        /*********************************************************************
         * 
         * UpdateSpread
         * 
         * ******************************************************************/

        private void UpdateSpread ()
        {
            if (lbOpAsk.InvokeRequired)
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
                    m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE UpdateSpread {3} B: {0} A: {1} L: {2}", leg.CurrBid == null ? "null" : ((double) leg.CurrBid).ToString ("F2"),
                                                                                         leg.CurrAsk == null ? "null" : ((double) leg.CurrAsk).ToString ("F2"),
                                                                                         leg.LastPrice == null ? "null" : ((double) leg.LastPrice).ToString ("F2"), i++));
                    if (!leg.IfSell)
                    {
                        Bid -= leg.CurrBid;
                        Ask -= leg.CurrAsk;
                        Nat -= (leg.CurrBid + leg.CurrAsk) / 2.0;
                        Last -= leg.LastPrice;
                    }
                    else
                    {
                        Bid += leg.CurrBid;
                        Ask += leg.CurrAsk;
                        Nat += (leg.CurrBid + leg.CurrAsk) / 2.0;
                        Last += leg.LastPrice;
                    }
                }
                if (Ask != null)
                {
                    lbOpAsk.Text = ((double) Ask).ToString ("F2");
                    lbOpAsk.Invalidate ();
                }
                if (Bid != null)
                {
                    lbOpBid.Text = ((double) Bid).ToString ("F2");
                    lbOpBid.Invalidate ();
                }
                if (Nat != null)
                {
                    lbOpNat.Text = ((double) Nat).ToString ("F2");
                    lbOpNat.Invalidate ();

                    if (string.IsNullOrEmpty (tbOpMyPrice.Text))
                    {
                        tbOpMyPrice.Text = lbOpNat.Text;
                    }
                }
                if (Last != null)
                {
                    lbOpLast.Text = ((double) Last).ToString ("F2");
                    lbOpLast.Invalidate ();
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
            int index = e.id & 0xFFFF;
            LegData leg = m_Legs[index];
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickPrice for {0} tickType:{1} {2} value: {3}", leg.Ticker, e.tickType, TickType.Display (e.tickType), e.price, e.id & 0xFFFF));
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
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickSize for {0} tickType:{1} {2} size: {3}", m_Legs[e.id & 0xFFFF].Ticker, e.tickType, TickType.Display (e.tickType), e.size, e.id & 0xFFFF));
        }
        void m_axTws_tickString (object sender, AxTWSLib._DTwsEvents_tickStringEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickString called for {0} tickType:{1} {2} [{3}] ", m_Legs[e.id & 0xFFFF].Ticker, e.tickType, TickType.Display (e.tickType), e.value, e.id & 0xFFFF));
        }
        void m_axTws_tickGeneric (object sender, AxTWSLib._DTwsEvents_tickGenericEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickGeneric for {0} tickType: {1} {2} value: {3}", m_Legs[e.id & 0xFFFF], e.tickType, TickType.Display (e.tickType), e.value, e.id & 0xFFFF));
        }

        void m_axTws_tickEFP (object sender, AxTWSLib._DTwsEvents_tickEFPEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {3} axTws_tickEFP for {0}", m_Legs[e.tickerId & 0xFFFF]));
        }

        void m_axTws_tickSnapshotEnd (object sender, AxTWSLib._DTwsEvents_tickSnapshotEndEvent e)
        {
            m_Log.Log (ErrorLevel.logINF, string.Format ("OPENTRADE {1} axTws_tickSnapshotEnd for {0}", m_Legs[e.reqId & 0xFFFF].Ticker, e.reqId & 0xFFFF));
        }

        /********************************************************************
         * 
         * Place the open order
         * 
         * *****************************************************************/

        private void btnOpPlaceOrder_Click (object sender, EventArgs e)
        {
 /*           if (string.IsNullOrEmpty (tbOpMyPrice.Text))
            {
                MessageBox.Show ("Need to enter a price.");
                return;
            }

            double price;
            if (!double.TryParse (tbOpMyPrice.Text, out price))
            {
                MessageBox.Show ("Enter a valid price.");
                return;
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
                        comboleg.action = "SELL"; 
                    }
                    else
                    {
                        comboleg.action = "BUY";
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

 /*               TWSLib.IOrder order = m_axTws.createOrder ();

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
                    order.action = "SELL";
                }
                else
                {
                    order.action = "BUY";
                }
                order.totalQuantity = l.NoContracts;
                order.orderType = "LMT";
                order.lmtPrice = price;

                m_axTws.placeOrderEx (m_OrderId, contract, order);
            }
            foreach (LegData leg in m_Legs)
            {
                leg.OpenOrderId = m_OrderId;
            }*/
        }
    }
}
