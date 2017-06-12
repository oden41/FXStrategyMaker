using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.Element.Function
{
    [Serializable]
    public abstract class LogicElement : BaseElement
    {
        public LogicElement()
        {
            IsTerminal = false;
        }

        public override int ChildrenCount
        {
            get
            {
                return 2;
            }
        }
    }
}
