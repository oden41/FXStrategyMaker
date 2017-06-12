using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX.Element.Function
{
    [Serializable]
    class OrElement: LogicElement
    {
        public OrElement()
        {
            ChildrenNodes = new BaseElement[2];
            SymbolString = "OR";
        }

        public override string ToSql()
        {
            return "(" + ChildrenNodes[0].ToSql() + " OR " + ChildrenNodes[1].ToSql() + ")";
        }

        public override string ToString()
        {
            return "(" + ChildrenNodes[0].ToString() + " OR " + ChildrenNodes[1].ToString() + ")";
        }
    }
}
