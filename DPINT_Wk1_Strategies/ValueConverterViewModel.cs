using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPINT_Wk1_Strategies
{
    public class ValueConverterViewModel : ViewModelBase
    {
        private string _fromText;
        public string FromText
        {
            get { return _fromText; }
            set
            {
                _fromText = value;
                RaisePropertyChanged("FromText");
                this.ConvertNumbers();
            }
        }

        private string _toText;
        public string ToText
        {
            get { return _toText; }
            set
            {
                _toText = value;
                RaisePropertyChanged("ToText");
            }
        }

        private string _fromConverterName;
        public string FromConverterName
        {
            get { return _fromConverterName; }
            set
            {
                _fromConverterName = value;
                RaisePropertyChanged("FromConverterName");
                this.ConvertNumbers();
            }
        }

        private string _toConverterName;
        public string ToConverterName
        {
            get { return _toConverterName; }
            set
            {
                _toConverterName = value;
                RaisePropertyChanged("ToConverterName");
                this.ConvertNumbers();
            }
        }

        public ObservableCollection<string> ConverterNames { get; set; }
        public ICommand ConvertCommand { get; set; }

        public ValueConverterViewModel()
        {
            ConverterNames = new ObservableCollection<string>();
            ConverterNames.Add("Numerical");
            ConverterNames.Add("Binary");
            ConverterNames.Add("Hexadecimal");
            ConverterNames.Add("Roman");

            FromText = "0";
            ToText = "0";
            FromConverterName = "Numerical";
            ToConverterName = "Numerical";

            ConvertCommand = new RelayCommand(ConvertNumbers);
        }

        private void ConvertNumbers()
        {
            try {
                int fromNumber = 0;
                switch (FromConverterName)
                {
                    case "Numerical":
                        fromNumber = Int32.Parse(FromText);
                        break;
                    case "Binary":
                        fromNumber = Convert.ToInt32(FromText, 2);
                        break;
                    case "Hexadecimal":
                        fromNumber = Convert.ToInt32(FromText, 16);
                        break;
                    case "Roman":
                        fromNumber = ConvertRomanToNumber(FromText.ToUpper());
                        break;
                    default:
                        break;
                }

                switch (ToConverterName)
                {
                    case "Numerical":
                        ToText = fromNumber.ToString();
                        break;
                    case "Binary":
                        ToText = Convert.ToString(fromNumber, 2);
                        break;
                    case "Hexadecimal":
                        ToText = Convert.ToString(fromNumber, 16);
                        break;
                    case "Roman":
                        ToText = ToRoman(fromNumber);
                        break;
                    default:
                        break;
                }
            }
            catch(FormatException ex)
            {
                ToText = "Error";
            }
        }

        private static Dictionary<char, int> _romanMap = new Dictionary<char, int>
        {
           {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
        };

        public static int ConvertRomanToNumber(string text)
        {
            int totalValue = 0, prevValue = 0;
            foreach (var c in text)
            {
                if (!_romanMap.ContainsKey(c))
                    return 0;
                var crtValue = _romanMap[c];
                totalValue += crtValue;
                if (prevValue != 0 && prevValue < crtValue)
                {
                    if (prevValue == 1 && (crtValue == 5 || crtValue == 10)
                        || prevValue == 10 && (crtValue == 50 || crtValue == 100)
                        || prevValue == 100 && (crtValue == 500 || crtValue == 1000))
                        totalValue -= 2 * prevValue;
                    else
                        return 0;
                }
                prevValue = crtValue;
            }
            return totalValue;
        }

        public static string ToRoman(int number)
        {
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);

            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
}
