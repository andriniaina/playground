using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BtcClient.UI
{
    /// <summary>
    /// Interaction logic for TrendPredictionControl.xaml
    /// </summary>
    public partial class TrendPredictionControl : UserControl
    {
        public TrendPredictionControl()
        {
            InitializeComponent();
        }
        public TrendPredictionControl(int degree, int futureDuration) : this()
        {
        }
    }
}
