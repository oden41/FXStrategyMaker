﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Terminal
{
    [Serializable]
    class PsychologicalSignElement : IndexElement
    {
        private Sign sign;

        public PsychologicalSignElement(Random rand)
        {
            var n = 2  * rand.Next(2) - 1;
            sign = (Sign)n;
            SymbolString = "PsychologicalSign == " + sign.GetString() + "?";
        }

        public override string ToSql()
        {
            return string.Format("PsychologicalSign = {0} ", (int)sign);
        }

        public override string ToString()
        {
            return "PsychologicalSign == " + sign.GetString() + "?";
        }
    }
}
