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
using andri.BtcClient;
namespace BtcClient.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            var data = BitcoinCharts.HistorySampleSynchronized("mtgoxUSD", DateTime.UtcNow.Subtract(new TimeSpan(3, 0, 0)));
            var tickerProvider =MtGoxStream.LiveTickerFactory(
                new MtGoxStream.PubnubWrapper(new PubNubMessaging.Core.Pubnub("", MtGoxStream.PUBNUB_KEY)),
                MtGoxStream.PUBNUB_CHANNELS.ticker_BTCUSD,
                "BTCUSD",
                1000);
            foreach (var d in data)
            {
                tickerProvider.PushTick(new Tick(d.Now, d.Price, 0, 0, 0));
            }

            this.DataContext = new MainWindowVM(tickerProvider);;

            InitializeComponent();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
