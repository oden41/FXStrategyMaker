using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class DMITrendElement : IndexElement
    {
        private Trend trend;

        public DMITrendElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            trend = (Trend)n;
            SymbolString = "DMITrend == " + trend.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("DMITrend = {0} ", (int)trend);
        }

        public override string ToString()
        {
            return "DMITrend == " + trend.GetString() + "?";
        }
    }
}
