namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NAssign : NBase
    {
        public NAssign(NBase left, NBase expr)
        {
            this.Left = left;
            this.Expr = expr;
        }

        public NBase Left { get; set; }

        public NBase Expr { get; set; }
    }
}

