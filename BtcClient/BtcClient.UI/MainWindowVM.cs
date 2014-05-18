using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using andri.BtcClient;

using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace BtcClient.UI
{
    public class MainWindowVM : BaseVM
    {
        public MainWindowVM(MtGoxStream.MtGoxLiveTickerProvider liveProvider)
        {
            this.BidVM = new TickerVM() { Label = "Bid (vente)", Price = 0.0 };
            this.AskVM = new TickerVM() { Label = "Ask (achat)", Price = 0.0 };
            this.LastVM = new TickerVM() { Label = "Last", Price = 0.0 };
            this.Predict3deg = new TrendPredictionControlVM(3, 15);
            this.Predict5deg = new TrendPredictionControlVM(5, 15);
            this.Predict6deg = new TrendPredictionControlVM(6, 15);
            this.Predict7deg = new TrendPredictionControlVM(7, 15);

            liveProvider.Data.Subscribe(list => this.BidVM.Price = list.Last().Bid);
            liveProvider.Data.Subscribe(list => this.AskVM.Price = list.Last().Ask);
            liveProvider.Data.Subscribe(list => this.LastVM.Price = list.Last().Last);
            Thread.Sleep(10000);
            liveProvider.Data.Subscribe(list =>
            {
                var lastValues = useLastPrice ? (from o in list where o.Now > new DateTime(2002, 1, 1) select Tuple.Create(o.Now, o.Last)).ToList() : (from o in list where o.Vwap > 0 && o.Now > new DateTime(2002, 1, 1) select Tuple.Create(o.Now, o.Vwap)).ToList();
                if (lastValues.Count >= 10)
                {
                    this.Predict3deg.RefreshProperties(lastValues);
                    this.Predict5deg.RefreshProperties(lastValues);
                    this.Predict6deg.RefreshProperties(lastValues);
                    this.Predict7deg.RefreshProperties(lastValues);
                }
            });
        }

        private bool useLastPrice = true;

        public bool UseStrategy_LastPrice
        {
            get { return useLastPrice; }
            set
            {
                useLastPrice = value;
                OnPropertyChanged("UseStrategy_Vwap");
                OnPropertyChanged("UseStrategy_LastPrice");
            }
        }

        public bool UseStrategy_Vwap
        {
            get { return !useLastPrice; }
            set
            {
                useLastPrice = !value;
                OnPropertyChanged("UseStrategy_Vwap");
                OnPropertyChanged("UseStrategy_LastPrice");
            }
        }

        public TickerVM BidVM { get; set; }
        public TickerVM AskVM { get; set; }
        public TickerVM LastVM { get; set; }

        public TrendPredictionControlVM Predict3deg { get; set; }
        public TrendPredictionControlVM Predict7deg { get; set; }
        public TrendPredictionControlVM Predict5deg { get; set; }
        public TrendPredictionControlVM Predict6deg { get; set; }
    }
}
