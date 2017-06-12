using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class ROCSignElement : IndexElement
    {
        private Sign sign;

        public ROCSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "ROCSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("ROCSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "ROCSign == " + sign.GetString() + "?";
        }
    }
}
