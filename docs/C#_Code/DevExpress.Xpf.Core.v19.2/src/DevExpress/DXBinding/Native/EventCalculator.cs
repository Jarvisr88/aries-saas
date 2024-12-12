namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;

    public class EventCalculator : CalculatorBase, IEventCalculator
    {
        private readonly NRoot expr;
        private Type senderType;
        private Type argsType;
        private Func<object[], object> eventFunc;

        public EventCalculator(EventTreeInfo treeInfo) : this(treeInfo.Expr, treeInfo.ErrorHandler)
        {
        }

        internal EventCalculator(NRoot expr, IErrorHandler errorHandler) : base(errorHandler)
        {
            this.expr = expr;
            base.InitTypeInfos(VisitorType.Resolve(expr, null, true));
        }

        private object[] CollectParameterValues(object[] opValues, object sender, object args)
        {
            List<object> list = new List<object>(opValues ?? new object[0]) {
                sender,
                args
            };
            return list.ToArray();
        }

        public void Event(object[] opValues, object sender, object args)
        {
            if (!base.ErrorHandler.HasError)
            {
                object[] input = this.CollectParameterValues(opValues, sender, args);
                this.ResolveCore(opValues, ((this.eventFunc == null) || (this.senderType == null)) || (this.argsType == null), delegate {
                    if (this.senderType == null)
                    {
                        this.senderType = sender?.GetType();
                    }
                    if (this.argsType == null)
                    {
                        this.argsType = args?.GetType();
                    }
                    this.eventFunc = this.VisitorExpression.ResolveEvent(this.expr, this.senderType, this.argsType);
                }, () => this.eventFunc(input));
            }
        }

        public override void Init(ITypeResolver typeResolver)
        {
            base.Init(typeResolver);
            NRoot[] exprs = new NRoot[] { this.expr };
            base.InitOperands(VisitorOperand.Resolve(exprs, null, new Func<NType, Type>(this.GetResolvedType), base.ErrorHandler, true));
        }
    }
}

