using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWI.Controls;
using System.Net.Mail;

namespace IBManagePositions
{
    public enum OpenCloseValues
    {
        Open = 0,
        Close = 1,
        Pending = 2
    };

    public enum EquityType
    {
        Option,
        Stock,
        Future,
        FutOpt,
        Index
    }

    class CallPutT
    {
        public string Name {get; set; }
        public bool? Value {get; set; }
        public CallPutT (string name, bool? value)
        {
            Name = name;
            Value = value;
        }

        private static readonly List<CallPutT> CallPutChoice = new List<CallPutT>
        {
            {
                new CallPutT ("Put", false)
            },
            {
                new CallPutT ("Call", true)
            },
            {
                new CallPutT ("", null)
            }
        };

        public static List<CallPutT> Choices ()
        {
            return CallPutChoice;
        }
    };

    class BuySellT
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public BuySellT (string name, bool value)
        {
            Name = name;
            Value = value;
        }

        private static readonly List<BuySellT> BuySellChoice = new List<BuySellT>
        {
            {
                new BuySellT ("Buy", false)
            },
            {
                new BuySellT ("Sell", true)
            }
        };

        public static List<BuySellT> Choices ()
        {
            return BuySellChoice;
        }
    };

    class ConIdLocalSymbolMultiplier
    {
        public int ConId { get; set; }
        public string LocalSymbol { get; set; }
        public int Multiplier { get; set; }

        public ConIdLocalSymbolMultiplier (int conid, string local_symbol, int multiplier)
        {
            ConId = conid;
            LocalSymbol = local_symbol;
            Multiplier = multiplier;
        }
    };

    class LegData
    {
        public delegate void StatusChangeHandler (LegData leg, int col);

        public event StatusChangeHandler StatusChanged;

        public int? Id {get; set; }
        public bool bIfUpdatingMarketData { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public EquityType Equity { get; set; }
        public int Multiplier { get; set; }
        public string LocalSymbol { get; set; }
        public int? ConId { get; set; }
        public OpenCloseValues OpenCloseStatus { get; set; }
        public bool? IfCall { get; set; }
        public bool IfSell { get; set; }
        public double? Strike { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? DaysLeft { get; set; }
        public double? UnderlyingPrice { get; set; }
        public double? OpenPrice { get; set; }
        public double? ClosePrice { get; set; }
        public int NoContracts { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
//        public double? Delta { get; set; }
//        public double? Theta { get; set; }
        public double? TotalDelta { get; set; }
        public double? TotalTheta { get; set; }
        //public double? Gamma { get; set; }
        //public double? Vega { get; set; }
        public double? MyDelta { get; set; }
        public double? MyTheta { get; set; }
        public double? MyGamma { get; set; }
        public double? MyVega { get; set; }
//        public double? ThetaVegaRatio { get; set; }
        public double? LastPrice { get; set; }
        public double? CurrBid { get; set; }
        public double? CurrAsk { get; set; }
        public double? ModelOption { get; set; }
        public double? Premium { get; set; }
        public double? Commissions { get; set; }
        public double? iv { get; set; }
        public double? ProbITM { get; set; }
        public double? TotalProfitLoss { get; set; }
        public double? DailyProfitLoss { get; set; }
        public DateTime? DailyProfitLossTimestamp { get; set; }
        public double? PercentProfit { get; set; }
        public DateTime? ProfitLossTimestamp { get; set; }
        public double? YesterdayProfitLoss { get; set; }
        public DateTime? YesterdayProfitLossTimestamp { get; set; }
        public double? ProfitThreshold { get; set; }
        public DateTime? LastEmail { get; set; }
        public int EmailNotifications { get; set; }
        public int? Trade_id { get; set; }
        public int? OpenOrderId { get; set; }
        public int? CloseOrderId { get; set; }
        public double? Beta { get; set; }
        public double? Correlation { get; set; }
        public double? Intercept { get; set; }
        public double? TotalBetaWeightedDelta { get; set; }

        public LegData Clone ()
        {
            LegData ld = (LegData) this.MemberwiseClone ();
            ld.Id = null;
            return ld;
        }

        public LegData ()
        {
            Id = null;
            bIfUpdatingMarketData = false;
            Ticker = "";
            OpenCloseStatus = OpenCloseValues.Open;
            IfCall = false;
            IfSell = true;
            Strike = null;
            YesterdayProfitLoss = 0.0;  // added Aug 4, 2014
            TotalProfitLoss = 0.0;      // added Aug 4, 2014
            OpenDate = DateTime.Today;  // added Mar 21, 2015
        }

        /******************************************************************
         * 
         * DisplayCall - utility function for displaying value of IfCall
         * 
         * ***************************************************************/

        public string DisplayCall ()
        {
            if (IfCall == null)
            {
                return "";
            }
            else if (IfCall == true)
            {
                return "Call";
            }
            else
            {
                return "Put";
            }
        }

        public LegData (int? ID,
                        string ticker,
                        EquityType equity_type,
                        string exchange,
                        int multiplier,
                        string local_symbol,
                        int? con_id,
                        OpenCloseValues if_closed,
                        bool? if_call,
                        bool if_sell,
                        decimal? strike,
                        DateTime? expiry_date,
                        decimal? und_price,
                        decimal? open_price,
                        decimal? close_price,
                        int no_contracts,
                        double? totaldelta,
                        double? totaltheta,
                        double? gamma,
                        double? vega,
                        double? mydelta,
                        double? mytheta,
                        double? mygamma,
                        double? myvega,
                        DateTime? open_date,
                        DateTime? closed_date,
                        decimal? profit_loss,
                        DateTime? profit_loss_timestamp,
                        decimal? daily_profit_loss,
                        DateTime? daily_profit_loss_timestamp,
                        decimal? yesterday_profit_loss,
                        DateTime? yesterday_profit_loss_timestamp,
                        decimal? profit_threshold,
                        DateTime? last_email,
                        int email_notifications,
                        int? trade_id)
        {
            bIfUpdatingMarketData = false;
            Id = ID;
            Ticker = ticker;
            Equity = equity_type;
            Exchange = exchange;
            Multiplier = multiplier;
            LocalSymbol = local_symbol;
            ConId = con_id;
            OpenCloseStatus = if_closed;
            IfCall = if_call;
            IfSell = if_sell;
            Strike = (double?) strike;
            ExpiryDate = expiry_date;
            UnderlyingPrice = (double?) und_price;
            OpenPrice = (double?) open_price;
            ClosePrice = (double?) close_price;
            NoContracts = no_contracts;
            TotalDelta = totaldelta;
            TotalTheta = totaltheta;
//            Gamma = gamma;
//            Vega = vega;
            MyDelta = mydelta;
            MyTheta = mytheta;
            MyGamma = mygamma;
            MyVega = myvega;
            OpenDate = open_date;
            ClosedDate = closed_date;
            TotalProfitLoss = (double?) profit_loss;
            DailyProfitLoss = (double?) daily_profit_loss;
            DailyProfitLossTimestamp = daily_profit_loss_timestamp;
            ProfitLossTimestamp = profit_loss_timestamp;
            YesterdayProfitLoss = (double?) yesterday_profit_loss;
            YesterdayProfitLossTimestamp = yesterday_profit_loss_timestamp;
            ProfitThreshold = (double?) profit_threshold;
            LastEmail = last_email;
            EmailNotifications = email_notifications;
            Trade_id = trade_id;
            ModelOption = null;
        }

        public void SignalTrade (int field)
        {
            if (StatusChanged != null)
            {
                StatusChanged (this, field);
            }
        }

        /***************************************************************
         * 
         * FetchConIdLocalSymbolMultiplier
         * 
         * ************************************************************/

        public struct ContractResult
        {
            public int conid;
            public string localsymbol;
            public int multiplier;
            public ContractResult (int c, string l, int m)
            {
                conid = c;
                localsymbol = l;
                multiplier = m;
            }

        };

        public Task<ConIdLocalSymbolMultiplier> FetchConIdLocalSymbolMultiplier (int reqid)
        {
            var tcs = new TaskCompletionSource<ConIdLocalSymbolMultiplier> ();
 /*           TWSLib.IContract contract = Utils.axTws.createContract ();

            List<ContractResult> cr = new List<ContractResult> ();

            if (this.Equity == EquityType.Option)
            {
                contract.symbol = Utils.Massage (this.Ticker);
                contract.secType = "OPT";
                contract.expiry = ((DateTime) this.ExpiryDate).ToString ("yyyyMMdd");
                contract.strike = (double) this.Strike;
                contract.right = (bool) this.IfCall ? "C" : "P";
                contract.multiplier = "100"; // this ensures we skip the minis if they exist
                //contract.exchange = "SMART";
                contract.exchange = this.Exchange;
                contract.primaryExchange = "";
                contract.currency = "USD";
                contract.localSymbol = "";
                contract.includeExpired = 0;
            }
            else if (this.Equity == EquityType.Stock)
            {
                contract.symbol = Utils.Massage (this.Ticker);
                contract.secType = "STK";
                //contract.expiry = ((DateTime) this.ExpiryDate).ToString ("yyyyMMdd");
                //contract.strike = (double) this.Strike;
                //contract.right = this.IfCall ? "C" : "P";
                contract.multiplier = "";
//                contract.exchange = "SMART";
                contract.exchange = this.Exchange;
                contract.primaryExchange = "";
                contract.currency = "USD";
                contract.localSymbol = "";
                contract.includeExpired = 0;
            }
            else if (this.Equity == EquityType.Future)
            {
                contract.secType = "FUT";
                contract.exchange = this.Exchange;
                contract.currency = "USD";
                contract.expiry = ((DateTime) this.ExpiryDate).ToString ("yyyyMMdd");
                contract.includeExpired = 0;
                contract.symbol = Utils.Massage (this.Ticker);
            }
            else if (this.Equity == EquityType.FutOpt)
            {
                contract.secType = "FOP";
                contract.exchange = this.Exchange;
                contract.currency = "USD";
                contract.includeExpired = 0;
                //contract.symbol = this.Ticker.Substring (0, 2);
                contract.symbol = this.Ticker;
                contract.expiry = ((DateTime) this.ExpiryDate).ToString ("yyyyMMdd");
                contract.strike = (double) this.Strike;
                contract.right = (bool) this.IfCall ? "C" : "P";

            }
            else if (this.Equity == EquityType.Index)
            {
                contract.secType = "IND";
                contract.exchange = this.Exchange;
                contract.currency = "USD";
                contract.includeExpired = 0;
                contract.symbol = this.Ticker;
            }
            Utils.Log.Log (ErrorLevel.logINF, string.Format ("FetchConIdLocalSymbolMultiplier attempting to find a contract for ticker:{0} symbol: {7} secType:{1} exchange: {2} currency:{3} strike:{4} expiry: {5} right:{6}",
                            this.Ticker, contract.secType, contract.exchange, contract.currency, contract.strike, contract.expiry, contract.right, contract.symbol));

            var errhandler = default (AxTWSLib._DTwsEvents_errMsgEventHandler);
            var handler = default (AxTWSLib._DTwsEvents_contractDetailsExEventHandler);
            var endhandler = default (AxTWSLib._DTwsEvents_contractDetailsEndEventHandler);

            errhandler = new AxTWSLib._DTwsEvents_errMsgEventHandler ((s, e) =>
            {
                tcs.TrySetException (new Exception (e.errorMsg));
                Utils.axTws.contractDetailsEx -= handler;
                Utils.axTws.errMsg -= errhandler;
                Utils.axTws.contractDetailsEnd -= endhandler;
            });

            endhandler = new AxTWSLib._DTwsEvents_contractDetailsEndEventHandler ((s, e) =>
            {
                /* There may be multiple options. Which one do we choose?
                 * ------------------------------------------------------ */
/*

                try
                {
                    if (cr.Count >= 1)
                    {
                        ContractResult c = cr[0];
                        tcs.TrySetResult (new ConIdLocalSymbolMultiplier (c.conid, c.localsymbol, c.multiplier));
                    }
                    else
                    {
                        Utils.Log.Log (ErrorLevel.logERR, string.Format ("FetchConIdLocalSymbolMultiplier: failed to find a matching contract for {0}", this.Ticker));
                    }
                    if (cr.Count > 1)
                    {
                        Utils.Log.Log (ErrorLevel.logERR, "FetchConIdLocalSymbolMultiplier: Multiple contracts satisfy the provided criteria.");
                    }
                }
                finally
                {
                    Utils.axTws.contractDetailsEx -= handler;
                    Utils.axTws.errMsg -= errhandler;
                }

                Utils.axTws.contractDetailsEx -= handler;
                Utils.axTws.errMsg -= errhandler;
                Utils.axTws.contractDetailsEnd -= endhandler;
            });

            handler = new AxTWSLib._DTwsEvents_contractDetailsExEventHandler ((s, e) =>
                {
                    TWSLib.IContractDetails c = e.contractDetails;
                    TWSLib.IContract con = (TWSLib.IContract) c.summary;
                    if ((reqid | Utils.ibLEG2) == e.reqId)
                    {
                        int multiplier = 1;
                        if (!string.IsNullOrEmpty (con.multiplier))
                        {
                            multiplier = int.Parse (con.multiplier);
                        }
                        Utils.Log.Log (ErrorLevel.logINF, string.Format ("FetchConIdLocalSymbolMultiplier: conId for {0} set to [{1}], {2}", this.Ticker, con.localSymbol, con.conId));

                        cr.Add (new ContractResult (con.conId, con.localSymbol, multiplier));
                    }
                });
            Utils.axTws.contractDetailsEx += handler;
            Utils.axTws.errMsg += errhandler;
            Utils.axTws.contractDetailsEnd += endhandler;

            Utils.axTws.reqContractDetailsEx (reqid | Utils.ibLEG2, contract);
*/
            return tcs.Task;
        }

        /***********************************************************************
         * 
         * Compute Profit Loss and Percent Profit 
         * 
         * ********************************************************************/

        public void ComputeProfitFigures ()
        {
            if (ClosePrice != null)
            {
                TotalProfitLoss = (ClosePrice - OpenPrice) * Multiplier * NoContracts;
                if (IfSell)
                {
                    TotalProfitLoss = -TotalProfitLoss;
                }
                PercentProfit = null;
                return;
            }

            if (this.Premium == null) return;
            
            if (Equity == EquityType.Stock || Equity == EquityType.Future || Equity == EquityType.Index)
            {
                double premium = (double) this.Premium;
                if (this.OpenPrice == null) return;
                double open_price = (double) this.OpenPrice;
                double commissions = 0.0;
                if (this.Commissions != null)
                {
                    //commissions = (double) this.Commissions; 
                    commissions = 3.00;
                }

                if (this.IfSell)
                {
                    this.TotalProfitLoss = (open_price - UnderlyingPrice) * this.NoContracts * Multiplier - commissions * 2.0;
                    this.PercentProfit = ((open_price - UnderlyingPrice) * this.NoContracts * Multiplier - commissions * 2.0) * 100 / (open_price * this.NoContracts * 100 - commissions * 2.0);
                }
                else
                {
                    this.TotalProfitLoss = (-open_price + UnderlyingPrice) * this.NoContracts * Multiplier - commissions * 2.0;
                    this.PercentProfit = ((open_price - UnderlyingPrice) * this.NoContracts * Multiplier - commissions * 2.0) * 100 / (open_price * this.NoContracts * 100 - commissions * 2.0);
                }
                this.ProfitLossTimestamp = DateTime.Now;
            }
            else
            {
                double ask = 0;
                double bid = 0;

                if (CurrAsk == null && CurrBid == null && LastPrice == null) return;

                if ((LastPrice != null) && ((CurrAsk == null) || (CurrBid == null)))
                {
                    ask = (double) LastPrice;
                    bid = (double) LastPrice;
                }
                else
                {
                    if (CurrAsk != null)
                    {
                        ask = (double) this.CurrAsk;
                    }
                    if (this.CurrBid != null)
                    {
                        bid = (double) this.CurrBid;
                    }
                }

                double premium = (double) this.Premium;
                if (this.OpenPrice == null) return;
                double open_price = (double) this.OpenPrice;
                double commissions = 0.0;
                if (this.Commissions != null)
                {
                    commissions = (double) this.Commissions;
                }
                if (this.IfSell)
                {
                    this.TotalProfitLoss = (open_price - (ask + bid) / 2.0) * this.NoContracts * Multiplier - commissions * 2.0;
                    this.PercentProfit = ((open_price - (ask + bid) / 2.0) * this.NoContracts * Multiplier - commissions * 2.0) * 100 / (open_price * this.NoContracts * 100 - commissions * 2.0);
                }
                else
                {
                    this.TotalProfitLoss = (-open_price + (ask + bid) / 2.0) * this.NoContracts * Multiplier - commissions * 2.0;
                    this.PercentProfit = ((open_price - (ask + bid) / 2.0) * this.NoContracts * Multiplier - commissions * 2.0) * 100 / (open_price * this.NoContracts * 100 - commissions * 2.0);
                }
                this.ProfitLossTimestamp = DateTime.Now;
            }

            /* Only compute daily profit loss on a trading day
             * ----------------------------------------------- */

            if (Utils.bIfTodayTradingDay)
            {
                this.DailyProfitLoss = this.TotalProfitLoss - this.YesterdayProfitLoss;
                this.DailyProfitLossTimestamp = DateTime.Now;
            }

            if ((EmailNotifications & Utils.enPROFITABLE) == Utils.enPROFITABLE)
            {
                if (TotalProfitLoss > ProfitThreshold)
                {
                    string note = string.Format ("Option {0} {1} {2} strike {3} is now profitable!!! {4:C2}", Ticker, DisplayCall (), IfSell ? "Sell" : "Buy", Strike, TotalProfitLoss);
                    EmailBrian (note, new TimeSpan (4, 0, 0));
                }
            }
        }

        /**********************************************************************
         * 
         * EstimateCommissions
         * 
         * *******************************************************************/
        public void EstimateCommissions ()
        {
            //OptionData option = m_Options[index];
            //if (option.Commissions == null)
            //{
            //    return;
            //}
            //if (option.conId == null || option.CurrAsk == null || option.CurrBid == null)
            //{
            //    return;
            //}

            //TWSLib.IContract con = axTws.createContract ();
            //TWSLib.IOrder order = axTws.createOrder ();

            //if (false)
            //{
            //    TWSLib.IComboLegList cl = axTws.createComboLegList ();
            //    TWSLib.IComboLeg leg = (TWSLib.IComboLeg) cl.Add ();
            //    leg.conId = (int) option.conId;
            //    leg.ratio = 1;
            //    leg.action = option.IfSell.ToUpper (CultureInfo.InvariantCulture);
            //    leg.exchange = "SMART";
            //    leg.openClose = 0;
            //    leg.shortSaleSlot = 0;
            //    leg.designatedLocation = "";

            //    con.symbol = option.Ticker;
            //    con.secType = "BAG";
            //    con.exchange = "SMART";
            //    con.currency = "USD";
            //    con.comboLegs = cl;

            //    order.whatIf = 1;
            //    order.action = "BUY";
            //    order.totalQuantity = 1;
            //    order.orderType = "MKT";
            //}
            //else
            //{
            //    order.whatIf = 1;
            //    order.action = option.IfSell.ToUpper ();
            //    order.totalQuantity = option.NoContracts;
            //    order.orderType = "MKT";
            //    order.lmtPrice = ((double) option.CurrBid + (double) option.CurrAsk) / 2;
            //    //order.goodTillDate = DateTime.Now.ToString ("yyyyMMdd");

            //    con.localSymbol = option.LocalSymbol;
            //    con.secType = "OPT";
            //    con.exchange = "SMART";
            //    con.currency = "USD";
            //    m_Log.Log (ErrorLevel.logINF, string.Format ("attempting to place whatif order for {0}", con.localSymbol));
            //}


            //int orderId;
            //using (dbOptionsDataContext dc = new dbOptionsDataContext ())
            //{
            //    orderId = dc.FetchOrderID ();
            //}

            //axTws.placeOrderEx (orderId, con, order);
        }

        /***************************************************************************
         * 
         * Compute Probability of ITM for the particular displayed option
         * 
         * ************************************************************************/

        public void ComputeProbITM ()
        {
            if  (Equity != EquityType.Option)
            {
                return;
            }
            if (iv == null || Strike == null || UnderlyingPrice == null || ExpiryDate == null)
            {
                frmPos.m_Log.Log (ErrorLevel.logERR, string.Format ("ComputeProbITM unable to compute Prob ITM for {0}", Ticker));
                return;

            }
            double vol = (double) iv / Math.Sqrt (252);
            double K = (double) Strike;
            double S = (double) UnderlyingPrice;

            int DaysToExpire = (int) Utils.ComputeDaysToExpire (ExpiryDate);

            frmPos.m_Log.Log (ErrorLevel.logDEB, string.Format ("ComputeProbITM {0}: iv: {1:F6} strike: {2:N} underlying: {3:F4} days to expiry {4} side {5}", Ticker, vol, K, S, DaysToExpire, DisplayCall ()));

            double variance = vol * vol;
            double d2 = Math.Log (S / K, Math.E);
            d2 += -variance / 2 * DaysToExpire;
            d2 /= vol;
            d2 /= Math.Sqrt (DaysToExpire);

            if ((bool) !IfCall)
            {
                ProbITM = Phi.phi (-d2) * 100;
            }
            else
            {
                ProbITM = Phi.phi (d2) * 100;
            }

            if ((EmailNotifications & Utils.enNEAR_ITM) == Utils.enNEAR_ITM)
            {
                if (ProbITM > 30.0 && ProbITM < 50.0)
                {
                    string note = string.Format ("Option {0} {1} {2} strike {3} is ITM {4:F2}", Ticker, DisplayCall (), IfSell ? "Sell" : "Buy", Strike, ProbITM);
                    EmailBrian (note, new TimeSpan (3, 0, 0));
                }
            }
            if ((EmailNotifications & Utils.enITM) == Utils.enITM)
            {
                if (ProbITM >= 50.0)
                {
                    string note = string.Format ("Option {0} {1} {2} strike {3} is ITM {4:F2}", Ticker, DisplayCall (), IfSell ? "Sell" : "Buy", Strike, ProbITM);
                    EmailBrian (note, new TimeSpan (1, 0, 0));
                }
            }
        }

        private void EmailBrian (string note, TimeSpan urgency)
        {
            //OptionData od = m_Options[index];

            //TimeSpan ts = new TimeSpan (0);
            //switch (n)
            //{
            //    case EmailNotify.ITM:
            //        note = string.Format ("Option {0} {1} {2} strike {3} is ITM {4:F2}", od.Ticker, od.IfCall, od.IfSell, od.Strike, od.ProbITM);
            //        ts = new TimeSpan (1, 0, 0); // 1 hour
            //        break;
            //    case EmailNotify.NEAR_ITM:
            //        note = string.Format ("Option {0} {1} {2} strike {3} is approaching ITM {4:F2}", od.Ticker, od.IfCall, od.IfSell, od.Strike, od.ProbITM);
            //        ts = new TimeSpan (3, 0, 0);
            //        break;
            //    case EmailNotify.PROFIT50:
            //        note = string.Format ("Option {0} {1} {2} strike {3} is > 50% profitable {4:F2}", od.Ticker, od.IfCall, od.IfSell, od.Strike, od.PercentProfit);
            //        ts = new TimeSpan (8, 0, 0);
            //        break;
            //    case EmailNotify.PROFIT75:
            //        note = string.Format ("Option {0} {1} {2} strike {3} is > 75% profitable {4:F2}", od.Ticker, od.IfCall, od.IfSell, od.Strike, od.PercentProfit);
            //        ts = new TimeSpan (4, 0, 0);
            //        break;
            //    default:
            //        m_Log.Log (ErrorLevel.logERR, "Bug in NotifyBrian");
            //        break;
            //}

            if (LastEmail == null || (DateTime) LastEmail >= DateTime.Now + urgency)
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var rec = (from r in dc.Legs
                               where r.Id == Id
                               select r).Single ();
                    LastEmail = rec.LastEmail = DateTime.Now;
                    dc.SubmitChanges ();
                }
                MailMessage msg = new MailMessage ();
                msg.To.Add ("bmuth@telus.net");
                msg.From = new MailAddress ("IBManagePositions@telus.net");
                msg.Subject = note;
                msg.Body = note;
                SmtpClient smtp = new SmtpClient ("smtp.telus.net");
                smtp.Send (msg);
            }
        }

        /*******************************************************
         * 
         * Update Premium
         * 
         * ****************************************************/

        public void UpdatePremium ()
        {
            if (OpenPrice == null) return;

            Premium = NoContracts * OpenPrice * Multiplier;
            if (!IfSell)
            {
                Premium = -Premium;
            }
        }    
        
        /*******************************************************
         * 
         * Update Commissions
         * 
         * ****************************************************/

        public void UpdateCommissions ()
        {
            if (Commissions == null)
            {
                Commissions = NoContracts * Multiplier * 0.005;
                Commissions = Math.Max ((double) Commissions, 1.0);
                if (Premium != null)
                {
                    Commissions = Math.Min ((double) Commissions, 0.005 * (double) Premium);
                }
            }
        }

        internal void UpdateUnderlyingPrice (double price)
        {
            this.UnderlyingPrice = price;
            if (this.Equity == EquityType.Stock || this.Equity == EquityType.Future || this.Equity == EquityType.Index)
            {
                ComputeProfitFigures ();
            }
        }

        internal void HookInSignalTrade (TradeData trade)
        {
            if (StatusChanged == null)
            {
                StatusChanged += new LegData.StatusChangeHandler (trade.LegStatusChangeHandler);
            }
        }

        /**************************************************************************
         * 
         * ComputeMyGreeks
         * 
         * ***********************************************************************/

        internal bool ComputeMyGreeks ()
        {
            double iv;
            double delta = 0;
            double gamma = 0;
            double theta = 0;
            double vega = 0;
            double rho = 0;

            if (this.UnderlyingPrice == null)
            {
                return false;
            }
            double stockprice = (double) this.UnderlyingPrice;
            if (this.Strike == null)
            {
                return false;
            }
            double strike = (double) this.Strike;
            if (this.LastPrice == null)
            {
                return false;
            }
            double lastprice = (double) this.LastPrice;
            if (this.ExpiryDate == null)
            {
                return false;
            }
            int days_left = (int) Utils.ComputeDaysToExpire ((DateTime) this.ExpiryDate);

            try
            {
                if ((bool) this.IfCall)
                {
                    iv = FinancialRecipes.FR.OptionPriceImpliedVolatilityCallBlackScholes (stockprice, strike, 0.0 /*rate*/, days_left, lastprice);
                    FinancialRecipes.FR.OptionPricePartialsCallBlackScholes (stockprice, strike, 0.0 /*rate*/, iv, days_left, out delta, out gamma, out theta, out vega, out rho);
                }
                else
                {
                    iv = FinancialRecipes.FR.OptionPriceImpliedVolatilityPutBlackScholes (stockprice, strike, 0.0 /*rate*/, days_left, lastprice);
                    FinancialRecipes.FR.OptionPricePartialsPutBlackScholes (stockprice, strike, 0.0 /*rate*/, iv, days_left, out delta, out gamma, out theta, out vega, out rho);
                }

                this.MyDelta = delta;
                this.MyTheta = theta;
                this.MyGamma = gamma;
                this.MyVega = vega;

                if (this.IfSell)
                {
                    this.MyDelta = -delta;
                    this.MyTheta = -theta;
                }

                //                this.ThetaVegaRatio = this.MyTheta / this.MyVega;
                this.TotalTheta = this.NoContracts * this.MyTheta * this.Multiplier;
                this.TotalDelta = this.NoContracts * this.MyDelta * this.Multiplier;

                return true;
            }
            catch (Exception e)
            {
                 Utils.Log.Log (ErrorLevel.logERR, string.Format ("ComputeMyGreeks for {0} {1}", this.LocalSymbol, e.Message));
                return false;
            }
        }
    }
}
