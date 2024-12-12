namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public class EventCalculatorDynamic : CalculatorDynamicBase, IEventCalculator
    {
        private readonly NRoot expr;

        public EventCalculatorDynamic(EventTreeInfo treeInfo) : this(treeInfo.Expr, treeInfo.ErrorHandler)
        {
        }

        internal EventCalculatorDynamic(NRoot expr, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.expr = expr;
        }

        public void Event(object[] opValues, object sender, object args)
        {
            VisitorEvaluator.ResolveEvent(this.expr, sender, args, base.GetOperandsValues(opValues), x => base.GetResolvedType(x), base.ErrorHandler);
        }

        internal override IEnumerable<Operand> GetOperands()
        {
            NRoot[] exprs = new NRoot[] { this.expr };
            return VisitorOperand.Resolve(exprs, null, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, false);
        }

        internal override IEnumerable<VisitorType.TypeInfo> GetTypeInfos() => 
            VisitorType.Resolve(this.expr, null, true);
    }
}

