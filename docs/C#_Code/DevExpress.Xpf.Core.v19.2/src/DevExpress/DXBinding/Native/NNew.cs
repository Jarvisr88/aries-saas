namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class NNew : NIdentBase
    {
        public NNew(NType type, NIdentBase next, NArgs args = null) : base(type.Name, next)
        {
            this.Type = type;
            NArgs args1 = args;
            if (args == null)
            {
                NArgs local1 = args;
                args1 = new NArgs();
            }
            this.Args = args1;
        }

        public NType Type { get; set; }

        public NArgs Args { get; set; }
    }
}

