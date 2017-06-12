using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class ParabolicTrendElement : IndexElement
    {
        private Trend trend;

        public ParabolicTrendElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            trend = (Trend)n;
            SymbolString = "ParabolicTrend == " + trend.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("ParabolicTrend = {0} ", (int)trend);
        }

        public override string ToString()
        {
            return "ParabolicTrend == " + trend.GetString() + "?";
        }
    }
}
