using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be.Timvw.Framework.ComponentModel;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;
using System.Net;

namespace IBManagePositions
{
    class TradeT
    {
        public string Name {get; set; }
        public int Value {get; set; }
        public TradeT (string name, int value)
        {
            Name = name;
            Value = value;
        }

        private static readonly List<TradeT> TradeChoice = new List<TradeT>
        {
            {
                new TradeT ("Short Put", 1)
            },
            {
                new TradeT ("Short Call", 2)
            },
            {
                new TradeT ("Short Call Vertical", 3)
            },
            {
                new TradeT ("Short Put Vertical", 4)
            },
            {
                new TradeT ("Long Call Vertical", 5)
            },           
            {
                new TradeT ("Long Put Vertical", 6)
            },           
            {
                new TradeT ("Short Strangle", 7)
            },           
            {
                new TradeT ("Short Straddle", 14)
            },           
            {
                new TradeT ("Calendar", 15)
            },           
            {
                new TradeT ("Diagonal", 16)
            },           
            //{
            //    new TradeT ("Short Call Butterfly", 8)
            //},           
            {
                new TradeT ("Long Call Butterfly", 9)
            },           
            //{
            //    new TradeT ("Short Put Butterfly", 10)
            //},           
            {
                new TradeT ("Long Put Butterfly", 11)
            },           
            {
                new TradeT ("Jade Lizard", 12)
            },           
            {
                new TradeT ("Reverse Jade Lizard", 13)
            },  
            {
                new TradeT ("Iron Condor", 17)
            },
             {
                new TradeT ("Covered Call", 18)
            },
            {
                new TradeT ("Covered Put", 19)
            },
           {
                new TradeT ("Custom", 99)
            }
        };

        public static List<TradeT> Choices ()
        {
            return TradeChoice;
        }
    };
    class TradeData
    {
        public int? Id { get; set;}

        public int TradeType { get; set; }
        public bool bIfUpdatingMarketData { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string Company { get; set; }
        public OpenCloseValues OpenCloseStatus { get; set; }
        public double? Premium { get; set; }
        public double? Commissions { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public double? TotalDelta { get; set; }
        public double? BetaWeightedDelta { get; set; }
        public double? TotalTheta { get; set; }
        public double? Vega { get; set; }
        public double? ThetaVegaRatio { get; set; }
        public double? TotalProfitLoss { get; set; }
        public DateTime? TotalProfitLossTimestamp { get; set; }
        public double? DailyProfitLoss { get; set; }
        public DateTime? DailyProfitLossTimestamp { get; set; }
        public double? YesterdayProfitLoss { get; set; }
        public DateTime? YesterdayProfitLossTimestamp { get; set; }
        public double? ProfitThreshold { get; set; }
        public double? PriceThreshold { get; set; }
        public bool PriceThresholdAboveBelow { get; set; }
        public double? UnderlyingPrice { get; set; }
        public int EmailNotifications { get; set; }
        public string Notes { get; set; }
        public DateTime? LastEmail { get; set; }

        //public SortableBindingList<LegData> m_Legs;
        public SortableBindingList<LegData> m_Legs;

        public TradeData ()
        {
            Id = null;
            bIfUpdatingMarketData = false;
            Ticker = "";
            OpenCloseStatus = OpenCloseValues.Pending;
            TradeType = 99;
            m_Legs = new SortableBindingList<LegData> ();
        }

        public TradeData (int? TradeId, 
                        string ticker, 
                        int trade_type,
                        string exchange,
                        string company,
                        OpenCloseValues if_closed, 
                        decimal? premium,
                        decimal? commissions,
                        DateTime? open_date, 
                        DateTime? closed_date, 
                        double? delta,
                        double? theta,
                        decimal? profit_loss,
                        DateTime? profit_loss_timestamp,
                        decimal? daily_profit_loss,
                        DateTime? daily_profit_loss_timestamp,
                        decimal? yesterday_profit_loss,
                        DateTime? yesterday_profit_loss_timestamp,
                        decimal? profit_threshold,
                        decimal? price_threshold,
                        bool price_above_below,
                        DateTime? last_email,
                        int email_notifications,
                        string notes)
        {
            bIfUpdatingMarketData = false;
            Id = TradeId;
            Ticker = ticker;
            Exchange = exchange;
            TradeType = trade_type;
            Company = company;
            OpenCloseStatus = if_closed;
            Premium = (double?) premium;
            Commissions = (double?) commissions;
            OpenDate = open_date;
            ClosedDate = closed_date;
            TotalProfitLoss = (double?) profit_loss;
            TotalProfitLossTimestamp = profit_loss_timestamp;
            DailyProfitLoss = (double?) daily_profit_loss;
            DailyProfitLossTimestamp = daily_profit_loss_timestamp;
            YesterdayProfitLoss = (double?) yesterday_profit_loss;
            YesterdayProfitLossTimestamp = yesterday_profit_loss_timestamp;
            LastEmail = last_email;
            EmailNotifications = email_notifications;
            ProfitThreshold = (double?) profit_threshold;
            PriceThreshold = (double?) price_threshold;
            PriceThresholdAboveBelow = price_above_below;
            Notes = notes;
            //m_Legs = new SortableBindingList<LegData> ();
            m_Legs = new SortableBindingList<LegData> ();
        }

        /******************************************************
         * 
         * A leg column has changed, so handle it
         * 
         * ***************************************************/

        internal void LegStatusChangeHandler (LegData leg, int col)
        {
            Program.frmMain.LegStatusChanged (this, leg, col);
        }

        /******************************************************
         * 
         * Email notification
         * 
         * ***************************************************/

        internal void EmailBrian (string note, TimeSpan urgency)
        {
            if (LastEmail == null || (DateTime) LastEmail < DateTime.Now - urgency)
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    var rec = (from r in dc.Trades
                               where r.TradeId == Id
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
                NetworkCredential basicCredential = new NetworkCredential ("bmuth@telus.net", "blackie0");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = basicCredential;

                try
                {
                    smtp.Send (msg);
                }
                catch (SmtpException se)
                {
                    MessageBox.Show (se.Message, "SMTP email failed", MessageBoxButtons.OK);
                }
            }
        }

        /******************************************************************
         * 
         * Compute Premium
         * 
         * ***************************************************************/

        internal void ComputePremium ()
        {
            var min_date = (from r in this.m_Legs select r.OpenDate).Min ();

            this.Premium = 0;

            foreach (var l in this.m_Legs)
            {
                if (l.OpenDate == min_date)
                {
                    double? premium;

                    //if (l.OpenCloseStatus == OpenCloseValues.Close)
                    //{
                    //    premium = (l.ClosePrice - l.OpenPrice) * l.Multiplier * l.NoContracts;
                    //    if (l.IfSell)
                    //    {
                    //        premium = -premium;
                    //    }
                    //}
                    //else
                    {
                        premium = -l.OpenPrice * l.Multiplier * l.NoContracts;

                        if (l.IfSell)
                        {
                            premium = -premium;
                        }
                    }
                    this.Premium += premium;
                }
            }
        }

 
        /************************************************************
          * 
          * Compute Theta and Vega
          * 
          * *********************************************************/

        internal void ComputeTheta ()
        {
            this.TotalTheta = 0;

            foreach (var l in this.m_Legs)
            {
                if (l.OpenCloseStatus != OpenCloseValues.Close)
                {
                    this.TotalTheta += l.TotalTheta;
                }
            }
        }

        /************************************************************
          * 
          * Compute Theta and Vega
          * 
          * *********************************************************/

        //internal void ComputeVega ()
        //{
        //    this.Vega = 0;

        //    foreach (var l in this.m_Legs)
        //    {
        //        if (l.OpenCloseStatus != OpenCloseValues.Close)
        //        {
        //            this.Vega += l.Vega;
        //        }
        //    }
        //}

        /************************************************************
        * 
        * Compute Delta
        * 
        * *********************************************************/

        internal void ComputeDelta ()
        {
            this.TotalDelta = 0;

            foreach (var l in this.m_Legs)
            {
                if (l.OpenCloseStatus != OpenCloseValues.Close)
                {
                    this.TotalDelta += l.TotalDelta;
                }
            }
        }

        /************************************************************
       * 
       * Compute Theta Vega Ratio for the trade... not sure what this means
       * 
       * *********************************************************/

        //internal void ComputeThetaVegaRatio ()
        //{
        //    double? theta = 0;
        //    double? vega = 0;
 
        //    foreach (var l in this.m_Legs)
        //    {
        //        if (l.OpenCloseStatus != OpenCloseValues.Close)
        //        {
        //            theta += l.Theta;
        //            vega += l.Vega;
        //        }
        //    }
        //    this.ThetaVegaRatio = theta / vega;
        //}

        internal void Persist (string DataGroupName)
        {
            if (this.Id != null)
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    dc.UpdateTrade (Id, TradeType, Ticker, (int) OpenCloseStatus, Premium, Commissions, OpenDate, ClosedDate, TotalDelta, TotalTheta, (decimal?) TotalProfitLoss, (decimal?) ProfitThreshold, EmailNotifications, Notes, LastEmail);
                }
            }
            else
            {
                using (dbOptionsDataContext dc = new dbOptionsDataContext ())
                {
                    InsertTradeResult tr =
                        dc.InsertTrade (Id,
                                        DataGroupName,
                                        TradeType,
                                        Ticker,
                                        (int) OpenCloseStatus,
                                        Premium,
                                        Commissions,
                                        OpenDate,
                                        ClosedDate,
                                        TotalDelta,
                                        TotalTheta,
                                        (decimal?) TotalProfitLoss,
                                        (decimal?) ProfitThreshold,
                                        EmailNotifications,
                                        Notes,
                                        LastEmail).Single ();
                    Id = tr.TradeId;
                }
            }
        }
    }

    class ShortTradeData
    {
        public int? Id { get; set; }
        public string Ticker { get; set; }
        public string TradeGroup { get; set; }

        public ShortTradeData (int id, string ticker, string tradegroup)
        {
            Id = id;
            Ticker = ticker;
            TradeGroup = tradegroup;
        }
    }
}

