using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Function
{
    [Serializable]
    public class AndElement : LogicElement
    {
        public AndElement()
        {
            ChildrenNodes = new BaseElement[2];
            SymbolString = "AND";
        }

        public override string ToSql()
        {
            return "(" + ChildrenNodes[0].ToSql() + " AND " + ChildrenNodes[1].ToSql() + ")";
        }

        public override string ToString()
        {
            return "(" + ChildrenNodes[0].ToString() + " AND " + ChildrenNodes[1].ToString() + ")";
        }
    }
}
