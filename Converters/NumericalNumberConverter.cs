using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk1_Strategies.Converters
{
    class NumericalNumberConverter : INumberConverter
    {
        public string ToLocalString(int value)
        {
            return value.ToString();
        }

        public int ToNumerical(string text)
        {
            return Int32.Parse(text);
        }
    }
}
