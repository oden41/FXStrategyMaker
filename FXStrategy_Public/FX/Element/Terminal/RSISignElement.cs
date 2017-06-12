using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class RSISignElement : IndexElement
    {
        private Sign sign;

        public RSISignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "RSISign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("RSISign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "RSISign == " + sign.GetString() + "?";
        }
    }
}
