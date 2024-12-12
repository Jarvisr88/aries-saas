namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BindingCalculatorDynamic : CalculatorDynamicBase, IBindingCalculator
    {
        private readonly NRoot expr;
        private readonly NRoot backExpr;
        private readonly object fallbackValue;

        public BindingCalculatorDynamic(BindingTreeInfo treeInfo, object fallbackValue) : this(treeInfo.Expr, treeInfo.BackExpr, fallbackValue, treeInfo.ErrorHandler)
        {
        }

        internal BindingCalculatorDynamic(NRoot expr, NRoot backExpr, object fallbackValue, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.expr = expr;
            this.backExpr = backExpr;
            this.fallbackValue = fallbackValue;
        }

        internal override IEnumerable<Operand> GetOperands()
        {
            NRoot[] exprs = new NRoot[] { this.expr };
            return VisitorOperand.Resolve(exprs, this.backExpr, x => base.GetResolvedType(x), base.ErrorHandler, true).ToList<Operand>();
        }

        internal override IEnumerable<VisitorType.TypeInfo> GetTypeInfos() => 
            VisitorType.Resolve(this.expr, this.backExpr, true);

        public object Resolve(object[] values) => 
            VisitorEvaluator.Resolve(this.expr, base.GetOperandsValues(values), x => base.GetResolvedType(x), base.ErrorHandler).FirstOrDefault<object>();

        public IEnumerable<object> ResolveBack(object[] values, object backParam) => 
            VisitorEvaluator.ResolveBack(this.backExpr, backParam, base.Operands.ToList<Operand>(), base.GetOperandsValues(values), x => base.GetResolvedType(x), base.ErrorHandler);
    }
}

