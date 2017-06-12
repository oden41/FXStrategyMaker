using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class MASignElement : IndexElement
    {
        private Sign sign;

        public MASignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "MASign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("MASign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "MASign == " + sign.GetString() + "?";
        }
    }
}
