using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.Element
{
    [Serializable]
    public class Cache<T>
    {
        private bool isCached;
        private T cachedValue;

        public bool IsCached { get { return isCached; } }
        public bool IsNotCached { get { return !isCached; } }
        public T Value
        {
            get
            {
                return cachedValue;
            }
            set
            {
                if (IsNotCached)
                {
                    cachedValue = value;
                    isCached = true;
                }
            }
        }

        public void Clear()
        {
            isCached = false;
            cachedValue = default(T);
        }
    }
}
