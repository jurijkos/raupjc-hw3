using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListLib
{
    internal class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> genericList;
        private int lastIndex;

        public GenericListEnumerator(GenericList<X> genericList)
        {
            this.genericList = genericList;
            lastIndex = -1;
        }

        public X Current => genericList.GetElement(lastIndex);

        object IEnumerator.Current => genericList.GetElement(lastIndex);

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if ((lastIndex + 1) < genericList.Count)
            {
                lastIndex++;
                return true;
            }
            return false;

        }

        public void Reset()
        {
            
        }
    }
}
