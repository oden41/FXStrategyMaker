using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class ParabolicSignElement : IndexElement
    {
        private Sign sign;

        public ParabolicSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "ParabolicSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("ParabolicSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "ParabolicSign == " + sign.GetString() + "?";
        }
    }
}
