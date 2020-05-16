using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWI.Controls;
using System.Drawing;
using Be.Timvw.Framework.ComponentModel;
using IBApi;

namespace IBManagePositions
{
    class Holidays
    {
        public static List<DateTime> MarketHolidays = new List<DateTime> 
                                                            {   
                                                                new DateTime (2014, 4, 18),
                                                                new DateTime (2014, 5, 26),
                                                                new DateTime (2014, 7, 4),
                                                                new DateTime (2014, 9, 1),
                                                                new DateTime (2014, 11, 27),
                                                                new DateTime (2014, 12, 25),
                                                                new DateTime (2015, 1, 1),
                                                                new DateTime (2015, 1, 19),
                                                                new DateTime (2015, 2, 16),
                                                                new DateTime (2015, 4, 3),
                                                                new DateTime (2015, 5, 25),
                                                                new DateTime (2015, 7, 3),
                                                                new DateTime (2015, 9, 7),
                                                                new DateTime (2015, 11, 26),
                                                                new DateTime (2015, 12, 25),
                                                                new DateTime (2016, 1, 1),
                                                                new DateTime (2016, 1, 18),
                                                                new DateTime (2016, 3, 25),
                                                                new DateTime (2016, 7, 4),
                                                                new DateTime (2016, 9, 5),
                                                                new DateTime (2016, 11, 24),
                                                                new DateTime (2016, 5, 26),
                                                                new DateTime (2016, 12, 26),
                                                                new DateTime (2017, 1, 2),
                                                                new DateTime (2017, 1, 16),
                                                                new DateTime (2017, 2, 20),
                                                                new DateTime (2017, 4, 14),
                                                                new DateTime (2017, 5, 29),
                                                                new DateTime (2017, 7, 4),
                                                                new DateTime (2017, 9, 4),
                                                                new DateTime (2017, 11, 23),
                                                                new DateTime (2017, 12, 25),
                                                                new DateTime (2018, 1, 1),
                                                                new DateTime (2018, 1, 15),
                                                                new DateTime (2018, 2, 19),
                                                                new DateTime (2018, 3, 30),
                                                                new DateTime (2018, 5, 28),
                                                                new DateTime (2018, 7, 4),
                                                                new DateTime (2018, 9, 3),
                                                                new DateTime (2018, 11, 22),
                                                                new DateTime (2018, 12, 25)
                                                                
                                                            };                                                            
    }
    class Utils
    {
        public const int enNEAR_ITM = 1;
        public const int enITM = 2;
        public const int enPROFITABLE = 4;
        public const int enPRICE = 8;
        public const int enLOSSES2PREMIUM = 16;

        public static Color colRED = Color.FromArgb (228, 71, 23);
        public static Color colYELLOW = Color.FromArgb (248, 236, 47);
        public static Color colDKBLUE = Color.FromArgb (112, 109, 241);
        public static Color colPLRED = Color.FromArgb (200, 27, 35);
        public static Color colPLGREEN = Color.FromArgb (51, 156, 56);

        public const int ibLEG          = 0x00010000;
        public const int ibCLOSETRADE   = 0x00020000;
        public const int ibBESTSTRANGLE = 0x00040000;
        public const int ibOPENTRADE    = 0x00080000;
        public const int ibLEG2         = 0x00100000;
        public const int ibPRICE        = 0x00200000;
        public const int ibBETAPRICE    = 0x00400000;


        public static EWrapperImpl ib;
        public static LogCtl Log;

        public static int GCD (int[] numbers)
        {
            return numbers.Aggregate (GCD);
        }

        public static EquityType String2EquityType (string eq)
        {
            switch (eq)
            {
                case "OPT":
                    return EquityType.Option;

                case "STK":
                    return EquityType.Stock;

                case "FUT":
                    return EquityType.Future;

                case "FOP":
                    return EquityType.FutOpt;
                
                case "IND":
                    return EquityType.Index;

                default:
                    throw new Exception ("Invalid equity type specified.");
            }
        }

        public static string EquityType2String (EquityType eq)
        {
            if (eq == EquityType.Option)
            {
                return "OPT";
            }
            else if (eq == EquityType.Stock)
            {
                return "STK";
            }
            else if (eq == EquityType.Future)
            {
                return "FUT";
            }
            else if (eq == EquityType.FutOpt)
            {
                return "FOP";
            }
            else if (eq == EquityType.Index)
            {
                return "IND";
            }

            throw new Exception ("Invalid exception type. Internal error");
        }

        static int GCD (int a, int b)
        {
            return b == 0 ? a : GCD (b, a % b);
        }

        /************************************************************
          * 
          * Massage
          * 
          * *********************************************************/

        public static string Massage (string ticker)
        {
            return ticker.Replace ('.', ' ');
        }

        public static bool bIfTodayTradingDay
        {
            get
            {
                DayOfWeek d = DateTime.Now.DayOfWeek;
                if (d == DayOfWeek.Saturday || d == DayOfWeek.Sunday)
                {
                    return false;
                }
                if (Holidays.MarketHolidays.Contains (DateTime.Today))
                {
                    return false;
                }
                return true;
            }
        }

        /************************************************************
          * 
          * Compute Days to Expire (both trading days and all days)
          * 
          * ********************************************************/

        public static int? ComputeDaysToExpire (DateTime? expiry)
        {
            if (expiry == null)
            {
                return null;
            }
            TimeSpan full_days = (DateTime) expiry - DateTime.Now;
            return (int) (Math.Ceiling (full_days.TotalDays)) + 1;
        }

        public static string ComputeDaysToExpire (DateTime Expires, out int TradingDays)
        {
            TradingDays = 1;
            TimeSpan one_day = new TimeSpan (1, 0, 0, 0);

            DateTime d = DateTime.Now;
            while (d < Expires)
            {
                if (d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                {
                    TradingDays++;
                }
                d += one_day;
            }

            TimeSpan full_days = Expires - DateTime.Now;

            string display_days = TradingDays.ToString () + " (" + (full_days.Days + 1).ToString () + ")";

            return display_days;
        }

        /*******************************************************************
         * 
         * Compute Next Expiry Date
         * 
         * ****************************************************************/

        static internal DateTime ComputeNextExpiryDate (DateTime dt)
        {
            DateTime d = new DateTime (dt.Year, dt.Month, 1);
            if (d.DayOfWeek == DayOfWeek.Saturday)
            {
                d += new TimeSpan (7, 0, 0, 0);
            }
            d -= new TimeSpan ((int) d.DayOfWeek, 0, 0, 0);

            d += new TimeSpan (5 + 14, 0, 0, 0);

            if (Holidays.MarketHolidays.Contains (d))
            {
                d -= new TimeSpan (1, 0, 0, 0);
            }

            if (d < dt)
            {
                return ComputeNextExpiryDate (dt += new TimeSpan (14, 0, 0, 0));
            }
            return d;
        }

        /*****************************************************
         * 
         * Is this a trading day?
         * 
         * **************************************************/

        public static bool IfTradingDay (DateTime n)
        {
            DayOfWeek d = n.DayOfWeek;
            if (d == DayOfWeek.Saturday || d == DayOfWeek.Sunday)
            {
                return false;
            }
            if (Holidays.MarketHolidays.Contains (n))
            {
                return false;
            }
            return true;
        }

        /*******************************************************
          * 
          * Is the market open?
          * 
          * ****************************************************/

        public static bool IfTradingNow ()
        {
            DateTime today = DateTime.Today;
            if (IfTradingDay (today))
            {
                if (today + new TimeSpan (6, 30, 0) <= DateTime.Now && today + new TimeSpan (12, 59, 58) >= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        private class PriceData
        {
            public double? Open;
            public double? Close;
            public double? Bid;
            public double? Ask;
            public double? Last;
            public double? OptPrice;
            public double? Delta;

            public PriceData ()
            {
                Open = null;
                Close = null;
                Bid = null;
                Ask = null;
                Last = null;
                OptPrice = null;
                Delta = null;
            }
        }

        /***********************************************************************
        * 
        * Fetch Prices on LegData list
        * 
        * ********************************************************************/

        public static Task<bool> FetchLastPrice (LogCtl log, SortableBindingList<LegData> leglist)
        {
            /*           int Cntr = leglist.Count;

                       log.Log (ErrorLevel.logINF, "FetchLastPrice: Calling FetchLastPrice");

                       PriceData[] prices = new PriceData[leglist.Count];
                       for (int i = 0; i < leglist.Count; i++)
                       {
                           prices[i] = new PriceData ();
                       }

                       System.Timers.Timer timer = new System.Timers.Timer ();

                       timer.AutoReset = false;
                       timer.Interval = 5000;
           //            timer.Interval = 30000;

                       var tcs = new TaskCompletionSource<bool> ();

                       var errhandler = default (AxTWSLib._DTwsEvents_errMsgEventHandler);
                       var endhandler = default (AxTWSLib._DTwsEvents_tickSnapshotEndEventHandler);
                       var pricehandler = default (AxTWSLib._DTwsEvents_tickPriceEventHandler);
                       var optioncomputehandler = default (AxTWSLib._DTwsEvents_tickOptionComputationEventHandler);
                       var generichandler = default (AxTWSLib._DTwsEvents_tickGenericEventHandler);
                       var sizehander = default (AxTWSLib._DTwsEvents_tickSizeEventHandler);
                       var timerhandler = default (System.Timers.ElapsedEventHandler);

                       errhandler = new AxTWSLib._DTwsEvents_errMsgEventHandler ((s, e) =>
                       {
                           if (e.id != -1)
                           {
                               if ((e.id & 0xFFFF0000) == Utils.ibBETAPRICE)
                               {
                                   e.id &= 0xFFFF;
                                   log.Log (ErrorLevel.logERR, string.Format ("FetchLastPrice: error {0} {1}", leglist[e.id].Ticker, e.errorMsg));

                                   axTws.errMsg -= errhandler;
                                   axTws.tickGeneric -= generichandler;
                                   axTws.tickPrice -= pricehandler;
                                   axTws.tickOptionComputation -= optioncomputehandler;
                                   axTws.tickSnapshotEnd -= endhandler;
                                   axTws.tickSize -= sizehander;
                                   timer.Elapsed -= timerhandler;
                                   timer.Stop ();

                                   for (int leg_no = 0; leg_no < leglist.Count; leg_no++)
                                   {
                                       axTws.cancelMktData (Utils.ibBETAPRICE | leg_no);
                                   }
                                   tcs.SetException (new Exception (e.errorMsg));
                               }
                           }
                           else
                           {
                               log.Log (ErrorLevel.logERR, string.Format ("FetchLastPrice: error {0}", e.errorMsg));
                           }
                       });

                       pricehandler = new AxTWSLib._DTwsEvents_tickPriceEventHandler ((s, e) =>
                       {
                           if ((e.id & 0xFFFF0000) != Utils.ibBETAPRICE)
                           {
                               return;
                           }
                           e.id &= 0xFFFF;
                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: axTws_tickPrice for {0} tickType:{1} {2} value: {3}", leglist[e.id].Ticker, e.tickType, TickType.Display (e.tickType), e.price));

                           int leg_no = e.id;
                           PriceData p = prices[leg_no];

                           switch (e.tickType)
                           {
                               case TickType.CLOSE:
                                   p.Close = (float) e.price;
                                   break;

                               case TickType.BID:
                                   p.Bid = (float) e.price;
                                   break;

                               case TickType.LAST:
                                   p.Last = (float) e.price;
                                   break;

                               case TickType.ASK:
                                   p.Ask = (float) e.price;
                                   break;

                               default:
                                   break;
                           }
                       });

                       sizehander = new AxTWSLib._DTwsEvents_tickSizeEventHandler ((s, e) =>
                       {
                           if ((e.id & 0xFFFF0000) != Utils.ibBETAPRICE)
                           {
                               return;
                           }
                           e.id &= 0xFFFF;
                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: axTws_tickSize for {0} tickType: {1} {2} value: {3}", leglist[e.id].Ticker, e.tickType, TickType.Display (e.tickType), e.size));
                           //switch (e.tickType)
                           //{
                           //    case TickType.OPTION_PUT_OPEN_INTEREST:
                           //        {
                           //            e.id &= 0xFFFF;
                           //            int s_no = e.id >> 8;
                           //            int opt_no = e.id & 0xFF;
                           //            StockAnal st = m_SelectedStocks[s_no];
                           //            OptionInfo option = st.OptionChain[opt_no];
                           //            if (option.Right == "P")
                           //            {
                           //                option.OpenInterest = e.size;
                           //            }
                           //        }
                           //        break;
                           //    case TickType.OPTION_CALL_OPEN_INTEREST:
                           //        {
                           //            e.id &= 0xFFFF;
                           //            int s_no = e.id >> 8;
                           //            int opt_no = e.id & 0xFF;
                           //            StockAnal st = m_SelectedStocks[s_no];
                           //            OptionInfo option = st.OptionChain[opt_no];
                           //            if (option.Right == "C")
                           //            {
                           //                option.OpenInterest = e.size;
                           //            }
                           //        }
                           //        break;

                           //    default:
                           //        break;
                           //}
                       });

                       generichandler = new AxTWSLib._DTwsEvents_tickGenericEventHandler ((s, e) =>
                       {
                           if ((e.id & 0xFFFF0000) != Utils.ibBETAPRICE)
                           {
                               return;
                           }
                           e.id &= 0xFFFF;
                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: axTws_tickGeneric for {0} tickType: {1} {2} value: {3}", leglist[e.id].Ticker, e.tickType, TickType.Display (e.tickType), e.value));
                       });

                       optioncomputehandler = new AxTWSLib._DTwsEvents_tickOptionComputationEventHandler ((s, e) =>
                       {
                           if ((e.id & 0xFFFF0000) != Utils.ibBETAPRICE)
                           {
                               return;
                           }
                           e.id &= 0xFFFF;

                           string szUndPrice = "Invalid";
                           if (e.undPrice < double.MaxValue)
                           {
                               szUndPrice = e.undPrice.ToString ("N3");
                           }
                           string szOptPrice = "Invalid";
                           if (e.optPrice < double.MaxValue)
                           {
                               szOptPrice = e.optPrice.ToString ("N3");
                           }

                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: axTws_tickOptionComputation for {0} ticktype: {1} {2} optPrice {3} undPrice {4} delta {5:F5}", leglist[e.id].Ticker, e.tickType, TickType.Display (e.tickType), szOptPrice, szUndPrice, e.delta));

                           if (e.optPrice == double.MaxValue)
                           {
                               return; // ignore it
                           }

                           PriceData p = prices[e.id];

                           switch (e.tickType)
                           {
                               case TickType.MODEL_OPTION:
                                   p.OptPrice = (float) e.optPrice;
                                   break;

                               case TickType.LAST_OPTION:
                                   p.Last = (float) e.optPrice;
                                   break;

                               default:
                                   return;

                           }


                           p.Delta = e.delta;
                       });

                       /* This won't be called since we aren't taking a snapshot
                        * ------------------------------------------------------ */

            /*           endhandler = new AxTWSLib._DTwsEvents_tickSnapshotEndEventHandler ((s, e) =>
                       {
                           if ((e.reqId & 0xFFFF0000) != Utils.ibBETAPRICE)
                           {
                               return;
                           }
                           e.reqId &= 0xFFFF;
                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: axTws_tickSnapshotEnd for {0}", leglist[e.reqId].Ticker));

                           if (--Cntr <= 0)
                           {
                               axTws.errMsg -= errhandler;
                               axTws.tickGeneric -= generichandler;
                               axTws.tickPrice -= pricehandler;
                               axTws.tickOptionComputation -= optioncomputehandler;
                               axTws.tickSnapshotEnd -= endhandler;
                               axTws.tickSize -= sizehander;
                               timer.Elapsed -= timerhandler;
                               timer.Stop ();

                               tcs.SetResult (true);
                           }
                       });

                       timerhandler = new System.Timers.ElapsedEventHandler ((s, e) =>
                       {
                           log.Log (ErrorLevel.logINF, string.Format ("FetchLastPrice: timer.ElapsedEventHandler. CUTTING SHORT"));

                           axTws.errMsg -= errhandler;
                           axTws.tickGeneric -= generichandler;
                           axTws.tickPrice -= pricehandler;
                           axTws.tickOptionComputation -= optioncomputehandler;
                           axTws.tickSnapshotEnd -= endhandler;
                           axTws.tickSize -= sizehander;
                           timer.Elapsed -= timerhandler;

                           for (int no = 0; no < leglist.Count; no++)
                           {
                               axTws.cancelMktData (Utils.ibBETAPRICE | no);
                           }

                           for (int i = 0; i < leglist.Count; i++)
                           {
                               var p = prices[i];
                               var l = leglist[i];

                               if (p.Last != null)
                               {
                                   l.LastPrice = p.Last;
                               }
                               else if (p.Close != null)
                               {
                                   l.LastPrice = p.Close;
                               }
                               else if (p.Ask >= 0.0 && p.Bid >= 0.0)
                               {
                                   l.LastPrice = (p.Ask + p.Bid) / 2.0;
                               }
                               else if (p.OptPrice != null)
                               {
                                   l.LastPrice = p.OptPrice;
                               }
                           }
                           tcs.SetResult (true);
                       });

                       axTws.errMsg += errhandler;
                       axTws.tickGeneric += generichandler;
                       axTws.tickPrice += pricehandler;
                       axTws.tickOptionComputation += optioncomputehandler;
                       axTws.tickSnapshotEnd += endhandler;
                       timer.Elapsed += timerhandler;
                       axTws.tickSize += sizehander;

                       timer.Start ();

                       /* Don't exceed 100
                        * ---------------- */
            /*
                        if (Cntr > 96)
                        {
                            log.Log (ErrorLevel.logERR, string.Format ("FetchLastPrice: No. in option chain {0} reduced to 96", Cntr));
                            Cntr = 96;
                        }

                        for (int index = 0; index < Cntr; index++)
                        //for (int index = 0; index < 2; index++ )
                        {
                            var leg = leglist[index];

                            IContract contract = axTws.createContract ();

                            contract.symbol = "";

                            if (leg.Equity == EquityType.Stock)
                            {
                                contract.secType = "STK";
                                contract.symbol = leg.Ticker;
                                contract.currency = "USD";
                                contract.exchange = leg.Exchange;
                                //contract.conId = (int) leg.ConId;
                                contract.localSymbol = leg.LocalSymbol;
                            }
                            else if (leg.Equity == EquityType.Option)
                            {
                                contract.secType = "OPT";
                                contract.exchange = "SMART";
                                //contract.conId = (int) leg.ConId;
                                contract.localSymbol = leg.LocalSymbol;
                            }
                            else if (leg.Equity == EquityType.Future)
                            {
                                contract.secType = "FUT";
                                contract.exchange = leg.Exchange;
                                contract.currency = "USD";
                                contract.localSymbol = leg.LocalSymbol;
                            }
                            else if (leg.Equity == EquityType.FutOpt)
                            {
                                contract.secType = "FOP";
                                contract.exchange = leg.Exchange;
                                contract.currency = "USD";
                                contract.localSymbol = leg.LocalSymbol;
                            }
                            else if (leg.Equity == EquityType.Index)
                            {
                                contract.secType = "IND";
                                contract.exchange = leg.Exchange;
                                contract.currency = "USD";
                                contract.localSymbol = leg.LocalSymbol;
                            }
                            else
                            {
                                throw new Exception (string.Format ("One of the legs [{0}] has a bad EquityType.", leg.Ticker));
                            }

                            //axTws.reqMktDataEx (Utils.ibDATA | index, contract, "", 1, null);
                            axTws.reqMktDataEx (Utils.ibBETAPRICE | index, contract, "", 0, null);
                            //axTws.reqMktDataEx (Constants.ANALYZE_OPTIONS_MARKET_DATA | ((stock_no << 8) + option_no), contract, "100, 101, 104, 106", 0, null);
                        }
                        return tcs.Task;*/
            return null; /*remove later*/
        }

        /*********************************************************************
          * 
          * FetchUnderlyingPrice
          * 
          * ******************************************************************/

        static internal Task<double> FetchUnderlyingPrice (int reqid, string ticker)
        {
            double price = 0.0;
            double close = 0.0;
            var tcs = new TaskCompletionSource<double> ();

            /* If the market is closed, get the price from somewhere else
             * ---------------------------------------------------------- */

            if (!Utils.IfTradingNow ())
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var stockprice = (from s in dc.Stocks
                                      where s.Ticker == ticker
                                      select s.LastTrade
                                ).SingleOrDefault ();

                    if (stockprice == null)
                    {
                        tcs.SetException (new Exception (string.Format ("Failed to fetch underlying price for {0}", ticker)));
                        return tcs.Task;
                    }
                    tcs.SetResult ((double) stockprice);
                    return tcs.Task;
                }
            }

            //int reqid = m_random.Next (0xFFFF);
            /*
                        var errhandler = default (AxTWSLib._DTwsEvents_errMsgEventHandler);
                        var endhandler = default (AxTWSLib._DTwsEvents_tickSnapshotEndEventHandler);
                        var pricehandler = default (AxTWSLib._DTwsEvents_tickPriceEventHandler);

                        errhandler = new AxTWSLib._DTwsEvents_errMsgEventHandler ((s, e) =>
                        {
                            if (e.id == -1)
                            {
                                Log.Log (ErrorLevel.logERR, string.Format ("FetchPriceUnderlying: error {0}", e.errorMsg));
                                return;
                            }

                            if ((e.id & 0xFFFF0000) != Utils.ibPRICE)
                            {
                                return;
                            }
                            e.id &= 0xFFFF;

                            Log.Log (ErrorLevel.logERR, string.Format ("FetchPriceUnderlying: error {0} ", e.errorMsg));

                            axTws.errMsg -= errhandler;
                            axTws.tickPrice -= pricehandler;
                            axTws.tickSnapshotEnd -= endhandler;

                            axTws.cancelMktData (reqid);

                            tcs.SetException (new Exception (e.errorMsg));
                        });

                        pricehandler = new AxTWSLib._DTwsEvents_tickPriceEventHandler ((s, e) =>
                        {
                            if ((e.id & 0xFFFF0000) != Utils.ibPRICE)
                            {
                                return;
                            }
                            e.id &= 0xFFFF;
                            if (reqid != e.id)
                            {
                                return;
                            }

                            Log.Log (ErrorLevel.logERR, string.Format ("FetchPriceUnderlying: axTws_tickPrice for {0} tickType:{1} {2} value: {3}", ticker, e.tickType, TickType.Display (e.tickType), e.price));


                            switch (e.tickType)
                            {
                                case TickType.LAST:
                                    price = e.price;
                                    break;

                                case TickType.CLOSE:
                                    close = e.price;
                                    break;

                                case TickType.BID:
                                    //opt.Bid = e.price;
                                    break;

                                case TickType.ASK:
                                    //opt.Ask = e.price;
                                    break;

                                default:
                                    break;
                            }
                        });

                        endhandler = new AxTWSLib._DTwsEvents_tickSnapshotEndEventHandler ((s, e) =>
                        {
                            if ((e.reqId & 0xFFFF0000) != Utils.ibPRICE)
                            {
                                return;
                            }
                            e.reqId &= 0xFFFF;
                            if (reqid != e.reqId)
                            {
                                return;
                            }

                            Log.Log (ErrorLevel.logINF, string.Format ("FetchOneOptionChain: axTws_tickSnapshotEnd for {0}. price={1:N3} close={2:N3}", ticker, price, close));

                            axTws.errMsg -= errhandler;
                            axTws.tickPrice -= pricehandler;
                            axTws.tickSnapshotEnd -= endhandler;

                            if (price != 0.0)
                            {
                                tcs.SetResult (price);
                            }
                            else
                            {
                                tcs.SetResult (close);
                            }
                        });


                        axTws.errMsg += errhandler;
                        axTws.tickPrice += pricehandler;
                        axTws.tickSnapshotEnd += endhandler;

                        TWSLib.IContract contract = axTws.createContract ();

                        contract.symbol = ticker;
                        contract.currency = "USD";
                        contract.secType = "STK";
                        contract.exchange = "SMART";
                        contract.localSymbol = ticker;
                        contract.includeExpired = 0;

                        axTws.reqMktDataEx (Utils.ibPRICE | reqid, contract, "", 1, null);
                        return tcs.Task;*/
            return null; /* remove */
        }        

    }
}
