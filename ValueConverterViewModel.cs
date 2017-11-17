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
using DPINT_Wk1_Strategies.Converters;

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

        private NumberConverterFactory _numberConverterFactory;

        public ObservableCollection<string> ConverterNames
        {
            get { return new ObservableCollection<string>(_numberConverterFactory.ConverterNames); }
        }

        public ICommand ConvertCommand { get; set; }

        public ValueConverterViewModel()
        {
            _numberConverterFactory = new NumberConverterFactory();

            FromText = "0";
            ToText = "0";
            FromConverterName = "Numerical";
            ToConverterName = "Numerical";

            ConvertCommand = new RelayCommand(ConvertNumbers);
        }

        private void ConvertNumbers()
        {
            try
            {
                int number = 0;
                number = _numberConverterFactory.GetConverter(FromConverterName).ToNumerical(FromText);
                ToText = _numberConverterFactory.GetConverter(ToConverterName).ToLocalString(number);
            }
            catch (FormatException ex)
            {
                ToText = "Error";
            }
        }
    }
}
