using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andri.BtcClient.Data
{
    public partial class BitcoinChartHistory
    {
        public BitcoinChartHistory() { }
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }

        public DateTime Now { get; set; }
        public string Market { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}
