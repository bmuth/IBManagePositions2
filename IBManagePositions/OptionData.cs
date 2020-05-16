using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBManagePositions
{
    class OptionInfo
    {
        public double Strike { get; set; }
        public string LocalSymbol { get; set; }
        public string Multiplier { get; set; }
        public int conId { get; set; }

        public OptionInfo (double strike, string local_symbol, string multiplier, int con_id)
        {
            Strike = strike;
            LocalSymbol = local_symbol;
            Multiplier = multiplier;
            conId = con_id;
        }
    }

    enum OpenCloseType
    {
        Pending,
        Open,
        Closed
    };

    class OptionData
    {

        private int? id;
        
        public List<OptionInfo> m_OptionChain;

        public int? Id
        {
            get
            {
                return id;
            }
        }

        public int? conId { get; set; }
        public bool bIfUpdatingMarketData { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }
        public OpenCloseType OpenCloseStatus { get; set; }
        public string IfCall { get; set; }
        public string IfSell { get; set; }
        public double? Strike { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string DaysLeft { get; set; }
        public double? OpenPrice { get; set; }
        public double? ClosePrice { get; set; }
        public double? Premium { get; set; }
        public int NoContracts { get; set; }
        public double? Commissions { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Guid? Linkage { get; set; }
        public double? Delta { get; set; }
        public double? Theta { get; set; }
        public double? LastPrice {get; set; }
        public double? CurrBid { get; set; }
        public double? CurrAsk { get; set; }
        public double? iv { get; set; }
        public double? ProbITM { get; set; }
        public double? TotalProfitLoss { get; set; }
        public double? DailyProfitLoss { get; set; }
        public DateTime? ProfitLossTimestamp {get; set; }
        public double? YesterdayProfitLoss { get; set; }
        public DateTime? YesterdayProfitLossTimestamp { get; set; }
        public double? PercentProfit { get; set; }
        public double? UnderlyingPrice { get; set; }
        public string LocalSymbol { get; set; }
        public DateTime? LastEmail { get; set; }

        public OptionData ()
        {
            id = null;
            bIfUpdatingMarketData = false;
            Ticker = "";
            OpenCloseStatus = OpenCloseType.Pending;
            IfCall = "Call";
            IfSell = "Sell";
        }

        public OptionData (int? ID, 
                           string ticker, 
                           string company,
                           bool? if_closed, 
                           bool if_call, 
                           bool if_sell, 
                           decimal? strike, 
                           DateTime? expiry_date, 
                           decimal? open_price, 
                           decimal? close_price,  
                           decimal? premium, 
                           int no_contracts, 
                           decimal? commissions, 
                           DateTime? open_date, 
                           DateTime? closed_date, 
                           decimal? profit_loss,
                           DateTime? profit_loss_timestamp,
                           decimal? yesterday_profit_loss,
                           DateTime? yesterday_profit_loss_timestamp,
                           Guid? linkage,
                           DateTime? last_email)
        {
            m_OptionChain = new List<OptionInfo> ();

            bIfUpdatingMarketData = false;
            id = ID;
            Ticker = ticker;
            Company = company;
            if (if_closed == null)
            {
                OpenCloseStatus = OpenCloseType.Pending;
            }
            else if ((bool) if_closed)
            {
                OpenCloseStatus = OpenCloseType.Closed;
            }
            else
            {
                OpenCloseStatus = OpenCloseType.Open;
            }
            IfCall = "Put";
            if (if_call)
            {
                IfCall = "Call";
            }
            IfSell = "Buy";
            if (if_sell)
            {
                IfSell = "Sell";
            }
            Strike = (double?) strike;
            ExpiryDate = expiry_date;
            OpenPrice = (double?) open_price;
            ClosePrice = (double?) close_price;
            Premium = (double?) premium;
            NoContracts = no_contracts;
            Commissions = (double?) commissions;
            OpenDate = open_date;
            ClosedDate = closed_date;
            TotalProfitLoss = (double?) profit_loss;
            ProfitLossTimestamp = profit_loss_timestamp;
            YesterdayProfitLoss = (double?) yesterday_profit_loss;
            YesterdayProfitLossTimestamp = yesterday_profit_loss_timestamp;
            Linkage = linkage;
            LastEmail = last_email;
        }
    }
}
