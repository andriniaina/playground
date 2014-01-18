using andri.BtcClient;
using andri.BtcClient.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSharp.Charting;

namespace BtcClient.UI
{
    public class TrendPredictionControlVM : BaseVM
    {
        public BaseCommand ShowGraph { get; set; }

        private int degree;
        public int Degree
        {
            get { return degree; }
            set
            {
                degree = value;
                OnPropertyChanged("Degree");
            }
        }

        private string advice;
        private IEnumerable<Tuple<DateTime, double>> data;
        private int samplingRate = 15;
        public string Advice
        {
            get { return advice; }
            set
            {
                advice = value;
                OnPropertyChanged("Advice");
            }
        }

        public TrendPredictionControlVM()
        {
            this.ShowGraph = new BaseCommand();
            this.ShowGraph.OnExecute += ShowGraph_OnExecute;
        }

        public TrendPredictionControlVM(int degree, int samplingRate)
            : this()
        {
            this.degree = degree;
            this.samplingRate = samplingRate;
        }

        public void RefreshProperties(IEnumerable<Tuple<DateTime, double>> data)
        {
            this.data = data;
            this.Advice = DataAnalysis.PredictTrend2(samplingRate, this.Degree, data).ToString();
        }

        void ShowGraph_OnExecute(object sender, EventArgs e)
        {
            DataAnalysis.ShowChart(30.0, this.degree, data);
        }
    }
}