using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class PivotSignElement : IndexElement
    {
        private Sign sign;

        public PivotSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "PivotSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("PivotSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "PivotSign == " + sign.GetString() + "?";
        }
    }
}
