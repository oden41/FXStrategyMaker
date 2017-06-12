using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class BollingerSignElement : IndexElement
    {
        private Sign sign;

        public BollingerSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "BollingerSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("BollingerSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "BollingerSign == " + sign.GetString() + "?";
        }
    }
}
