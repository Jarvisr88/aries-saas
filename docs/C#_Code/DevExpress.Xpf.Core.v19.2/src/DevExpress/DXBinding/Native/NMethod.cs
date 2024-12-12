namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class NMethod : NIdentBase
    {
        public NMethod(string name, NIdentBase next, NArgs args = null) : base(name, next)
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

