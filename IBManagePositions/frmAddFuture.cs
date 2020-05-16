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
using System.Globalization;

namespace IBManagePositions
{
    public partial class frmAddFuture : Form
    {
        LogCtl m_Log;
        FutureDetails m_fd = null;

        public frmAddFuture (LogCtl log)
        {

            m_Log = log;
            InitializeComponent ();
        }

        private async void btnFuturesSearch_Click (object sender, EventArgs e)
        {
            List<FutureDetails> fds = null;
            
            try
            {
                 fds = await FetchFutureContracts ();
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }

            if (fds == null)
            {
                return;
            }

            if (fds.Count != 1)
            {
                MessageBox.Show (string.Format ("Located {0} future contracts",fds.Count));
                return;
            }
            m_fd = fds[0];
            lbFutureName.Text = string.Format ("{0} {1}", m_fd.LongName, m_fd.Expiry.ToString ("MMMdd"));
        }

        private Task<List<FutureDetails>> FetchFutureContracts ()
        {
            var tcs = new TaskCompletionSource<List<FutureDetails>> ();
            List<FutureDetails> cd= new List<FutureDetails> ();
             
/*            var errhandler = default (AxTWSLib._DTwsEvents_errMsgEventHandler);
            var contracthandler = default (AxTWSLib._DTwsEvents_contractDetailsExEventHandler);
            var endhandler = default (AxTWSLib._DTwsEvents_contractDetailsEndEventHandler);

            errhandler = new AxTWSLib._DTwsEvents_errMsgEventHandler ((s, e) =>
            {
                if (e.id != -1)
                {
                    m_Log.Log (ErrorLevel.logERR, string.Format ("GetOptionStrikes: {0}", e.errorMsg));

                    Utils.axTws.errMsg -= errhandler;
                    Utils.axTws.contractDetailsEx -= contracthandler;
                    Utils.axTws.contractDetailsEnd -= endhandler;

                    tcs.TrySetException (new Exception (e.errorMsg));
                }
                else
                {
                    m_Log.Log (ErrorLevel.logERR, string.Format ("GetOptionStrikes: error {0:x} {1}", e.id, e.errorMsg));
                }
            });

            contracthandler = new AxTWSLib._DTwsEvents_contractDetailsExEventHandler ((s, e) =>
            {
                TWSLib.IContractDetails c = e.contractDetails;
                TWSLib.IContract d = (TWSLib.IContract) c.summary;

                m_Log.Log (ErrorLevel.logINF, string.Format ("FetchContracts: {0} localsym {1} mult: {2}, strike {3:N2} right {4}", d.symbol, d.localSymbol, d.multiplier, d.strike, d.right));
                //if (d.strike >= strike_start && d.strike <= strike_stop)
                {
                    string format = "yyyyMMdd";
                    DateTime Expiry = DateTime.ParseExact (d.expiry, format, CultureInfo.InvariantCulture);


                    cd.Add (new FutureDetails (d.localSymbol, c.longName, Expiry, d.conId, d.exchange));
                }
            });

            endhandler = new AxTWSLib._DTwsEvents_contractDetailsEndEventHandler ((s, e) =>
            {
                try
                {
                    tcs.TrySetResult (cd);
                }
                finally
                {
                    Utils.axTws.contractDetailsEx -= contracthandler;
                    Utils.axTws.errMsg -= errhandler;
                    Utils.axTws.contractDetailsEnd -= endhandler;
                }
            });

            Utils.axTws.errMsg += errhandler;
            Utils.axTws.contractDetailsEx += contracthandler;
            Utils.axTws.contractDetailsEnd += endhandler;


            IContract contract = Utils.axTws.createContract ();

            /* Unable to get strikes of expired contracts, so use the next contract instead
             * ---------------------------------------------------------------------------- */
/*
            //contract.symbol = tbTicker.Text;
            contract.localSymbol = tbFutureSymbol.Text;
            contract.secType = "FUT";
            contract.exchange = tbFutureExchange.Text;
            contract.primaryExchange = "";
            contract.currency = "USD";
            //contract.right = bIfCall ? "C" : "P";
            //contract.expiry = expiry.ToString ("yyyyMMdd");
            contract.strike = 0.0;
            //contract.multiplier = "50";
            contract.includeExpired = 1;

            Utils.axTws.reqContractDetailsEx (1, contract);
*/
            return tcs.Task;
        }

        private void btnFuturesAdd_Click (object sender, EventArgs e)
        {
            if (m_fd != null)
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var f = (from st in dc.Stocks
                            where st.Ticker == m_fd.LocalSymbol
                            select st).Count ();
                    if (f > 0)
                    {
                        MessageBox.Show ("Already added to the database.");
                        return;
                    }


                    Stock s = new Stock ();
                    s.Ticker = m_fd.LocalSymbol;
                    s.Company = string.Format ("{0} {1}", m_fd.LongName, m_fd.Expiry.ToString ("MMMdd"));
                    s.Exchange = m_fd.Exchange;
                    s.SecType = "FUT";
                    s.FutureExpiry = m_fd.Expiry;
                    dc.Stocks.InsertOnSubmit (s);
                    try
                    {
                        dc.SubmitChanges ();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show (ex.Message);
                    }
                }
            }
        }
    }
}
