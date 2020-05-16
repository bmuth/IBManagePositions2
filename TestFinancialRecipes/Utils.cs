using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFinancialRecipes
{
    class Utils
    {
        internal static double PercentageParse (string percent)
        {
            if (percent.IndexOf ('%') != -1)
            {
                percent = percent.Trim (new char[] { '%' });
                return double.Parse (percent) / 100.0;
            }
            else
            {
                return double.Parse (percent);
            }
        }
    }
}
