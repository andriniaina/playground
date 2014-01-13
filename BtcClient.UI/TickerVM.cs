using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BtcClient.UI
{
    public class TickerVM : BaseVM 
    {
        private System.Windows.Media.Color color;
        public System.Windows.Media.Color Color
        {
            get
            {
                return color;
            }
            private set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        private string label;
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnPropertyChanged("Label");
            }
        }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                this.Color = (value > price) ? (Color)ColorConverter.ConvertFromString("Green") : (Color)ColorConverter.ConvertFromString("Red");

                price = value;
                OnPropertyChanged("Price");
            }
        }
    }
}
