using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class MACDTrendElement : IndexElement
    {
        private Trend trend;

        public MACDTrendElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            trend = (Trend)n;
            SymbolString = "MACDTrend == " + trend.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("MACDTrend = {0} ", (int)trend);
        }

        public override string ToString()
        {
            return "MACDTrend == " + trend.GetString() + "?";
        }
    }
}
