namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class DXBindingException : DXBindingExceptionBase<DXBindingException, DXBindingExtension>
    {
        public DXBindingException(DXBindingExtension owner, string message, Exception innerException) : base(owner, message, innerException)
        {
            this.Expr = owner.Expr;
            this.BackExpr = owner.BackExpr;
        }

        protected override string Report(string message) => 
            ErrorHelper.ReportBindingError(message, this.Expr, this.BackExpr);

        public string Expr { get; private set; }

        public string BackExpr { get; private set; }
    }
}

