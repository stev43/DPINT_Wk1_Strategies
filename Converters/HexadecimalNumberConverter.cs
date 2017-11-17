using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk1_Strategies.Converters
{
    class HexadecimalNumberConverter : INumberConverter
    {
        public string ToLocalString(int value)
        {
            return Convert.ToString(value, 16);
        }

        public int ToNumerical(string text)
        {
            return Convert.ToInt32(text, 16);
        }
    }
}
