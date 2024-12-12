namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NExprIdent : NIdentBase
    {
        public NExprIdent(NBase expr, NIdentBase next) : base("expr", next)
        {
            this.Expr = expr;
        }

        public NBase Expr { get; set; }
    }
}

