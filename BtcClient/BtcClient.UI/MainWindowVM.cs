using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using andri.BtcClient;

namespace BtcClient.UI
{
    public class MainWindowVM : BaseVM 
    {
        public MainWindowVM()
        {
            this.BidVM = new TickerVM() { Label = "Bid (vente)", Price = 0.0 };
            this.AskVM = new TickerVM() { Label = "Ask (achat)", Price = 0.0 };
            this.LastVM = new TickerVM() { Label = "Last", Price = 0.0 };
            this.Predict3deg = new TrendPredictionControlVM(3, 15);
            this.Predict5deg = new TrendPredictionControlVM(5, 15);
            this.Predict6deg = new TrendPredictionControlVM(6, 15);
            this.Predict7deg = new TrendPredictionControlVM(7, 15);
            this.RefreshCommand = new BaseCommand();
            this.RefreshCommand.OnExecute += RefreshCommand_OnExecute;
        }

        void RefreshCommand_OnExecute(object sender, EventArgs e)
        {
            refresh();
        }

        private async void refresh()
        {
            this.BidVM.Price = await MtGoxHttp.QuoteTask(MtGoxHttp.QuoteType.Bid, 1.0, "BTC", "USD");
            this.AskVM.Price = await MtGoxHttp.QuoteTask(MtGoxHttp.QuoteType.Ask, 1.0, "BTC", "USD");
            this.LastVM.Price = await MtGoxHttp.QuoteLastTask("BTC", "USD");

            var data = await BitcoinCharts.HistorySampleTask("mtgoxUSD", DateTime.UtcNow.Subtract(new TimeSpan(3, 0, 0)));
            var coords = BitcoinCharts.AsTimeSeries(data);

            this.Predict3deg.RefreshProperties(coords);
            this.Predict5deg.RefreshProperties(coords);
            this.Predict6deg.RefreshProperties(coords);
            this.Predict7deg.RefreshProperties(coords);
        }

        public TickerVM BidVM
        {
            get;
            set;
        }
        public TickerVM AskVM
        {
            get;
            set;
        }
        public TickerVM LastVM
        {
            get;
            set;
        }

        public BaseCommand RefreshCommand { get; set; }

        public TrendPredictionControlVM Predict3deg { get; set; }

        public TrendPredictionControlVM Predict7deg { get; set; }

        public TrendPredictionControlVM Predict5deg { get; set; }

        public TrendPredictionControlVM Predict6deg { get; set; }
    }
}
