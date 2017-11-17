using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk1_Strategies.Converters
{
    public class NumberConverterFactory
    {
        public IEnumerable<string> ConverterNames
        {
            get { return _converters.Keys; }
        }

        private Dictionary<string, INumberConverter> _converters;

        public NumberConverterFactory()
        {
            _converters = new Dictionary<string, INumberConverter>();

            _converters["Numerical"] = new NumericalNumberConverter();
            _converters["Binary"] = new BinaryNumberConverter(); ;
            _converters["Hexadecimal"] = new HexadecimalNumberConverter();
            _converters["Roman"] = new RomanNumberConverter();
        }

        public INumberConverter GetConverter(string converterName)
        {
            if (!String.IsNullOrWhiteSpace(converterName))
                return _converters[converterName];
            return _converters["Numerical"];
        }
    }
}
