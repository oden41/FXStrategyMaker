using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.Element.Terminal
{
    [Serializable]
    public abstract class IndexElement : BaseElement
    {
        public IndexElement()
        {
            IsTerminal = true;
            ChildrenNodes = new BaseElement[0];
        }

        public override int ChildrenCount
        {
            get
            {
                return 0;    
            }
        }
    }
}
