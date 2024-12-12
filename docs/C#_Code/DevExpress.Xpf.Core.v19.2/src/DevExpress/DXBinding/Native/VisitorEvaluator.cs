namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    internal class VisitorEvaluator : VisitorEvaluatorBase
    {
        private readonly IDictionary<Operand, object> operandValues;
        private readonly bool executeAssign;
        private object backParam;
        private object parameterParam;
        private object senderParam;
        private object argsParam;
        private Dictionary<Operand, object> assigns;

        protected VisitorEvaluator(IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler, bool fullPack, bool executeAssign) : base(typeResolver, errorHandler, fullPack)
        {
            this.executeAssign = executeAssign;
            this.operandValues = operandValues;
            this.assigns = new Dictionary<Operand, object>();
        }

        protected override object Assign(NAssign n, object value) => 
            this.executeAssign ? this.Assign_Execute(n, value) : this.Assign_NotExecute(n, value);

        private object Assign_Execute(NAssign n, object value)
        {
            this.Visit(n.Left);
            if (!base.IsLastMember)
            {
                base.errorHandler.Report("Assign Expression is invalid.", true);
                return null;
            }
            object lastMember = base.LastMember;
            object lastMemberOwner = base.LastMemberOwner;
            if (lastMember is DependencyProperty)
            {
                ((DependencyObject) base.LastMemberOwner).SetCurrentValue((DependencyProperty) lastMember, value);
                return null;
            }
            if (lastMember is PropertyInfo)
            {
                ((PropertyInfo) lastMember).SetValue(base.LastMemberOwner, value, null);
                return null;
            }
            if (lastMember is FieldInfo)
            {
                ((FieldInfo) lastMember).SetValue(base.LastMemberOwner, value);
                return null;
            }
            base.errorHandler.Report("Assign Expression is invalid.", true);
            return null;
        }

        private object Assign_NotExecute(NAssign n, object value)
        {
            NIdentBase base2;
            if (!(n.Left is NIdentBase))
            {
                throw new InvalidOperationException();
            }
            Operand key = VisitorOperand.ReduceIdent((NIdentBase) n.Left, x => base.typeResolver(x), out base2, true);
            this.assigns.Add(key, value);
            return null;
        }

        protected override object GetOperandValue(Operand operand, NRelative.NKind? relativeSource)
        {
            if (relativeSource == null)
            {
                if (operand != null)
                {
                    if (this.operandValues.ContainsKey(operand))
                    {
                        return this.operandValues[operand];
                    }
                    base.errorHandler.SetError();
                }
                return null;
            }
            switch (relativeSource.Value)
            {
                case NRelative.NKind.Value:
                    return this.backParam;

                case NRelative.NKind.Parameter:
                    return this.parameterParam;

                case NRelative.NKind.Sender:
                    return this.senderParam;

                case NRelative.NKind.Args:
                    return this.argsParam;
            }
            throw new NotImplementedException();
        }

        public static IEnumerable<object> Resolve(NRoot expr, IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler) => 
            new VisitorEvaluator(operandValues, typeResolver, errorHandler, true, false).RootVisit(expr);

        public static IEnumerable<object> ResolveBack(NRoot expr, object backParam, List<Operand> operands, IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            VisitorEvaluator me = new VisitorEvaluator(operandValues, typeResolver, errorHandler, true, false) {
                backParam = backParam
            };
            IEnumerable<object> source = me.RootVisit(expr);
            return (((source.Count<object>() != 1) || (source.First<object>() == null)) ? ((IEnumerable<object>) operands.Select<Operand, object>(delegate (Operand x) {
                object obj2;
                me.assigns.TryGetValue(x, out obj2);
                return obj2;
            }).ToArray<object>()) : source);
        }

        public static bool ResolveCanExecute(NRoot expr, object parameterParam, IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            VisitorEvaluator evaluator = new VisitorEvaluator(operandValues, typeResolver, errorHandler, true, false) {
                parameterParam = parameterParam
            };
            return Equals(evaluator.RootVisit(expr).FirstOrDefault<object>(), true);
        }

        public static void ResolveEvent(NRoot expr, object sender, object args, IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            new VisitorEvaluator(operandValues, typeResolver, errorHandler, false, true) { 
                senderParam = sender,
                argsParam = args
            }.RootVisit(expr);
        }

        public static void ResolveExecute(NRoot expr, object parameterParam, IDictionary<Operand, object> operandValues, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            new VisitorEvaluator(operandValues, typeResolver, errorHandler, false, true) { parameterParam = parameterParam }.RootVisit(expr);
        }
    }
}

