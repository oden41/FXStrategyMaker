using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class FourWeeksRuleElement : IndexElement
    {
        private Sign sign;

        public FourWeeksRuleElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "FourWeeksRule == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("FourWeeksRule = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "FourWeeksRule == " + sign.GetString() + "?";
        }
    }
}
