using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class perRSignElement : IndexElement
    {
        private Sign sign;

        public perRSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "perRSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("perRSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "perRSign == " + sign.GetString() + "?";
        }
    }
}
