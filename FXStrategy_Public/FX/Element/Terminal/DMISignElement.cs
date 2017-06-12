using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class DMISignElement : IndexElement
    {
        private Sign sign;

        public DMISignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "DMISign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("DMISign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "DMISign == " + sign.GetString() + "?";
        }
    }
}
