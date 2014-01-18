using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andri.BtcClient.Data
{
    public partial class BitcoinChartHistory
    {
        public override bool Equals(object obj)
        {
            var o = obj as BitcoinChartHistory;
            if (o == null) return false;

            return
                o.Amount==this.Amount
                && o.Market==this.Market
                && o.Now==this.Now
                && o.Price==this.Price;
        }
        public override int GetHashCode()
        {
            return
                this.Amount.GetHashCode()
                ^ this.Market.GetHashCode()
                ^ this.Now.GetHashCode()
                ^ this.Price.GetHashCode();
        }
    }
}
