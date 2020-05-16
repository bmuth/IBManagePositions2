using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBManagePositions
{
    class FutureDetails
    {
        public string LocalSymbol { get; set; }
        public string LongName { get; set; }
        public DateTime Expiry { get; set; }
        public int ConId { get; set; }
        public string Exchange { get; set; }


        public FutureDetails (string local_symbol, string longname, DateTime expiry, int conid, string exchange)
        {
            LocalSymbol = local_symbol;
            LongName = longname;
            ConId = conid;
            Expiry = expiry;
            Exchange = exchange;
        }
    }
}
