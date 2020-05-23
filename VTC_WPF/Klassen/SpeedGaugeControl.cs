using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    public class SpeedGaugeControl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }


        public SpeedGaugeControl()
        {
            Value = 0;
            Angle = -85;
        }

        int _angle;
        public int Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
                NotifyPropertyChanged("Angle");
            }
        }

        int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value > 0 && value < 170)
                {
                    _value = value;
                    Angle = value - 85;
                    NotifyPropertyChanged("Value");
                }
            }
        }



    }
}
