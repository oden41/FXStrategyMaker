﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class TwoMAElement : IndexElement
    {
        private Sign sign;

        public TwoMAElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "TwoMA == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("TwoMA = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "TwoMA == " + sign.GetString() + "?";
        }
    }
}
