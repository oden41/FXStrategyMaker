using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class MATrendElement : IndexElement
    {
        private Trend trend;

        public MATrendElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            trend = (Trend)n;
            SymbolString = "MATrend == " + trend.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("MATrend = {0} ", (int)trend);
        }

        public override string ToString()
        {
            return "MATrend == " + trend.GetString() + "?";
        }
    }
}
