namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class NIndex : NIdentBase
    {
        public NIndex(NIdentBase next, NArgs args = null) : base("Indexer", next)
        {
            NArgs args1 = args;
            if (args == null)
            {
                NArgs local1 = args;
                args1 = new NArgs();
            }
            this.Args = args1;
        }

        public NArgs Args { get; set; }
    }
}

