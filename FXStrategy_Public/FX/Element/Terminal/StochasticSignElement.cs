using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class StochasticSignElement : IndexElement
    {
        private Sign sign;

        public StochasticSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "StochasticSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("StochasticSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "StochasticSign == " + sign.GetString() + "?";
        }
    }
}
