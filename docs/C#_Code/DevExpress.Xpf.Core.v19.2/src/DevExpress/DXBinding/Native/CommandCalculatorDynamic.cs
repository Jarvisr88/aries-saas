namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommandCalculatorDynamic : CalculatorDynamicBase, ICommandCalculator
    {
        private readonly NRoot executeExpr;
        private readonly NRoot canExecuteExpr;
        private readonly bool fallbackCanExecute;

        public CommandCalculatorDynamic(CommandTreeInfo treeInfo, bool fallbackCanExecute) : this(treeInfo.ExecuteExpr, treeInfo.CanExecuteExpr, fallbackCanExecute, treeInfo.ErrorHandler)
        {
        }

        internal CommandCalculatorDynamic(NRoot executeExpr, NRoot canExecuteExpr, bool fallbackCanExecute, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.executeExpr = executeExpr;
            this.canExecuteExpr = canExecuteExpr;
            this.fallbackCanExecute = fallbackCanExecute;
        }

        public bool CanExecute(object[] opValues, object parameter) => 
            VisitorEvaluator.ResolveCanExecute(this.canExecuteExpr, parameter, base.GetOperandsValues(opValues), x => base.GetResolvedType(x), base.ErrorHandler);

        public void Execute(object[] opValues, object parameter)
        {
            VisitorEvaluator.ResolveExecute(this.executeExpr, parameter, base.GetOperandsValues(opValues), x => base.GetResolvedType(x), base.ErrorHandler);
        }

        internal override IEnumerable<Operand> GetOperands()
        {
            NRoot[] exprs = new NRoot[] { this.executeExpr };
            NRoot[] rootArray2 = new NRoot[] { this.canExecuteExpr };
            return VisitorOperand.Resolve(exprs, null, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, false).Union<Operand>(VisitorOperand.Resolve(rootArray2, null, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, true));
        }

        internal override IEnumerable<VisitorType.TypeInfo> GetTypeInfos() => 
            VisitorType.Resolve(this.executeExpr, this.canExecuteExpr, false);
    }
}

