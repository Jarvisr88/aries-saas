namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal abstract class NIdentBase : NBase
    {
        public NIdentBase(string name, NIdentBase next)
        {
            this.Name = name;
            this.Next = next;
        }

        public IEnumerable<NIdentBase> Unfold()
        {
            List<NIdentBase> list = new List<NIdentBase>();
            for (NIdentBase base2 = this; base2 != null; base2 = base2.Next)
            {
                list.Add(base2);
            }
            return list;
        }

        public string Name { get; set; }

        public NIdentBase Next { get; set; }
    }
}

