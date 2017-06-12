using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class DeviationSignElement : IndexElement
    {
        private Sign sign;

        public DeviationSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "DeviationSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("DeviationSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "DeviationSign == " + sign.GetString() + "?";
        }
    }
}
