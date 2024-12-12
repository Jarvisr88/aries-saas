namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BindingTreeInfo : TreeInfoBase
    {
        public BindingTreeInfo(string expr, string backExpr, IErrorHandler errorHandler) : base(infoArray1, errorHandler)
        {
            TreeInfoBase.ExprInfo[] infoArray1 = new TreeInfoBase.ExprInfo[] { new TreeInfoBase.ExprInfo(expr, "@DataContext", ParserMode.BindingExpr), new TreeInfoBase.ExprInfo(backExpr, null, ParserMode.BindingBackExpr) };
        }

        public bool IsEmptyBackExpr() => 
            ReferenceEquals(this.BackExpr, null);

        public bool IsSimpleExpr()
        {
            NIdentBase base2;
            if (this.Expr.Exprs.Count<NBase>() != 1)
            {
                return false;
            }
            if (!(this.Expr.Expr is NIdentBase))
            {
                return false;
            }
            Func<NType, Type> typeResolver = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<NType, Type> local1 = <>c.<>9__9_0;
                typeResolver = <>c.<>9__9_0 = x => typeof(object);
            }
            return ((VisitorOperand.ReduceIdent((NIdentBase) this.Expr.Expr, typeResolver, out base2, true) != null) && ReferenceEquals(base2, null));
        }

        public string ExprString =>
            base.GetExprString(0);

        public string BackExprString =>
            base.GetExprString(1);

        internal NRoot Expr =>
            base.GetExpr(0);

        internal NRoot BackExpr =>
            base.GetExpr(1);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingTreeInfo.<>c <>9 = new BindingTreeInfo.<>c();
            public static Func<NType, Type> <>9__9_0;

            internal Type <IsSimpleExpr>b__9_0(NType x) => 
                typeof(object);
        }
    }
}

