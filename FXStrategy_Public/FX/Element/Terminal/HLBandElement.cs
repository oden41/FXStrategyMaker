using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class HLBandElement : IndexElement
    {
        private Sign sign;

        public HLBandElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "HLBand == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("HLBand = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "HLBand == " + sign.GetString() + "?";
        }
    }
}
