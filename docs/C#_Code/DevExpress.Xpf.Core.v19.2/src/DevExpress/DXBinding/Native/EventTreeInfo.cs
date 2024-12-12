namespace DevExpress.DXBinding.Native
{
    using System;

    public class EventTreeInfo : TreeInfoBase
    {
        public EventTreeInfo(string expr, IErrorHandler errorHandler) : base(infoArray1, errorHandler)
        {
            TreeInfoBase.ExprInfo[] infoArray1 = new TreeInfoBase.ExprInfo[] { new TreeInfoBase.ExprInfo(expr, null, ParserMode.Event) };
        }

        public string ExprString =>
            base.GetExprString(0);

        internal NRoot Expr =>
            base.GetExpr(0);
    }
}

