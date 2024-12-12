namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class NRoot
    {
        public NRoot()
        {
            this.Exprs = new List<NBase>();
        }

        public NBase Expr =>
            this.Exprs.First<NBase>();

        public List<NBase> Exprs { get; set; }
    }
}

