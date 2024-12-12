namespace DevExpress.DXBinding.Native
{
    using System;

    public class CommandTreeInfo : TreeInfoBase
    {
        public CommandTreeInfo(string executeExpr, string canExecuteExpr, IErrorHandler errorHandler) : base(infoArray1, errorHandler)
        {
            TreeInfoBase.ExprInfo[] infoArray1 = new TreeInfoBase.ExprInfo[] { new TreeInfoBase.ExprInfo(executeExpr, null, ParserMode.CommandExecute), new TreeInfoBase.ExprInfo(canExecuteExpr, "true", ParserMode.CommandCanExecute) };
        }

        public string ExecuteExprString =>
            base.GetExprString(0);

        public string CanExecuteExprString =>
            base.GetExprString(1);

        internal NRoot ExecuteExpr =>
            base.GetExpr(0);

        internal NRoot CanExecuteExpr =>
            base.GetExpr(1);
    }
}

