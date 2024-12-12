namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public class CommandCalculator : CalculatorBase, ICommandCalculator
    {
        private readonly NRoot executeExpr;
        private readonly NRoot canExecuteExpr;
        private readonly bool fallbackCanExecute;
        private Type parameterType;
        private Func<object[], object> execute;
        private Func<object[], object> canExecute;

        public CommandCalculator(CommandTreeInfo treeInfo, bool fallbackCanExecute) : this(treeInfo.ExecuteExpr, treeInfo.CanExecuteExpr, fallbackCanExecute, treeInfo.ErrorHandler)
        {
        }

        internal CommandCalculator(NRoot executeExpr, NRoot canExecuteExpr, bool fallbackCanExecute, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.executeExpr = executeExpr;
            this.canExecuteExpr = canExecuteExpr;
            this.fallbackCanExecute = fallbackCanExecute;
            base.InitTypeInfos(VisitorType.Resolve(executeExpr, canExecuteExpr, true));
        }

        public bool CanExecute(object[] opValues, object parameter)
        {
            bool res = this.fallbackCanExecute;
            object[] input = this.CollectParameterValues(opValues, parameter);
            this.Resolve(opValues, parameter, delegate {
                res = (bool) this.canExecute(input);
            });
            return res;
        }

        private object[] CollectParameterValues(object[] opValues, object parameter)
        {
            List<object> list = new List<object>(opValues ?? new object[0]) {
                parameter
            };
            return list.ToArray();
        }

        public void Execute(object[] opValues, object parameter)
        {
            object[] input = this.CollectParameterValues(opValues, parameter);
            this.Resolve(opValues, parameter, () => this.execute(input));
        }

        public override void Init(ITypeResolver typeResolver)
        {
            base.Init(typeResolver);
            NRoot[] exprs = new NRoot[] { this.executeExpr, this.canExecuteExpr };
            base.InitOperands(VisitorOperand.Resolve(exprs, null, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, true));
        }

        private void Resolve(object[] opValues, object parameter, Action calculate)
        {
            if (!base.ErrorHandler.HasError)
            {
                Type actualParameterType = (parameter != null) ? parameter.GetType() : typeof(object);
                this.ResolveCore(opValues, ((this.execute == null) || (this.canExecute == null)) || (this.parameterType != actualParameterType), delegate {
                    this.parameterType = actualParameterType;
                    this.VisitorExpression.ResolveExecute(this.executeExpr, this.canExecuteExpr, this.parameterType, out this.execute, out this.canExecute);
                }, calculate);
            }
        }
    }
}

