using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Other way of suppressing columns is to set AutoCreateColumns to false
 *
 *         [Browsable(false)]
 * 
 */

namespace IBManagePositions
{
    public class OptionInfo
     {

        public OptionInfo Copy ()
        {
            return (OptionInfo) this.MemberwiseClone ();
        }

        public int ConId { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public DateTime Expiry { get; set; }
        public double Strike { get; set; }
        public string Ticker { get; set; }
        public string LocalSymbol { get; set; }
        public string Multiplier { get; set; }
        public string SecId { get; set; }
        public string SecIdType { get; set; }
        public string SecType { get; set; }
        public bool IfCall { get; set; }
        public bool IfSell { get; set; }
        public string TradingClass { get; set; }
        public double? Bid { get; set; }
        public double? Ask {get; set; }
        public double? Last { get; set; }
        public double? Price { get; set; }  // set by MODEL_OPTION or derived from Bid, Ask, Last
        public double? Delta { get; set; }
        public double Gamma { get; set; }
        public double Theta { get; set; }
        public double Vega { get; set; }
        public double? ImpliedVolatility { get; set; }
        public double UndPrice { get; set; }
        public double? ProbITM {get; set; }
        public int NoContracts { get; set; }
        public double BuyingPowerReduction { get; set; }

        public OptionInfo (int conid, string currency, string exchange, string expiry, double strike, string symbol, string localsymbol,
            string multiplier, string secid, string secidtype, string sectype, string right, string tradingclass)
        {
            ConId = conid;
            Currency = currency;
            Exchange = exchange;

            Expiry = DateTime.ParseExact (expiry, "yyyyMMdd", CultureInfo.InvariantCulture);

            Strike = strike;
            Ticker = symbol;
            LocalSymbol = localsymbol;
            Multiplier = multiplier;
            SecId = secid;
            SecIdType = secidtype;
            SecType = sectype;
            IfCall = right == "C" ? true : false;
            TradingClass = tradingclass;
        }
    }
}
