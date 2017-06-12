using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class MACDSignElement : IndexElement
    {
        private Sign sign;

        public MACDSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "MACDSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("MACDSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "MACDSign == " + sign.GetString() + "?";
        }
    }
}
