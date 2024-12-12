namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public class BindingCalculator : CalculatorBase, IBindingCalculator
    {
        private readonly NRoot expr;
        private readonly NRoot backExpr;
        private readonly object fallbackValue;
        private Func<object[], object> calc;

        public BindingCalculator(BindingTreeInfo treeInfo, object fallbackValue) : this(treeInfo.Expr, treeInfo.BackExpr, fallbackValue, treeInfo.ErrorHandler)
        {
        }

        internal BindingCalculator(NRoot expr, NRoot backExpr, object fallbackValue, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.expr = expr;
            this.backExpr = backExpr;
            this.fallbackValue = fallbackValue;
            base.InitTypeInfos(VisitorType.Resolve(expr, backExpr, true));
        }

        private object[] CollectParameterValues()
        {
            List<object> list = new List<object>();
            foreach (VisitorExpression.OperandInfo info in base.OperandInfos.Values)
            {
                if (info.Parameter != null)
                {
                    list.Add(info.OperandValue);
                }
            }
            return list.ToArray();
        }

        public override void Init(ITypeResolver typeResolver)
        {
            base.Init(typeResolver);
            NRoot[] exprs = new NRoot[] { this.expr };
            base.InitOperands(VisitorOperand.Resolve(exprs, this.backExpr, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, true));
        }

        public void InitBack(Type backExprType)
        {
            if (this.backExpr != null)
            {
                base.VisitorExpression.ResolveBack(this.backExpr, backExprType);
            }
        }

        public object Resolve(object[] opValues)
        {
            if (base.ErrorHandler.HasError)
            {
                return this.fallbackValue;
            }
            object res = this.fallbackValue;
            base.ResolveCore(opValues, ReferenceEquals(this.calc, null), delegate {
                this.calc = base.VisitorExpression.Resolve(this.expr);
            }, delegate {
                res = (this.calc != null) ? this.calc(this.CollectParameterValues()) : this.fallbackValue;
            });
            return res;
        }
    }
}

