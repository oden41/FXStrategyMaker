using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class RCISignElement : IndexElement
    {
        private Sign sign;

        public RCISignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "RCISign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("RCISign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "RCISign == " + sign.GetString() + "?";
        }
    }
}
