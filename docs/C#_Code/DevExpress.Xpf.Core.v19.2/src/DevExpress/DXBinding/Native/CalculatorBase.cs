namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class CalculatorBase
    {
        public CalculatorBase(IErrorHandler errorHandler)
        {
            this.ErrorHandler = errorHandler;
            this.TypeInfos = new Dictionary<VisitorType.TypeInfo, Type>();
            this.OperandInfos = new Dictionary<Operand, DevExpress.DXBinding.Native.VisitorExpression.OperandInfo>();
        }

        private void CalculateCore(Action calculate)
        {
            if (!this.ErrorHandler.HasError)
            {
                try
                {
                    calculate();
                }
                catch (Exception exception)
                {
                    this.ErrorHandler.Report(exception.Message, true);
                }
            }
        }

        private bool CheckOperandsCount(object[] opValues)
        {
            if ((this.Operands.Count<Operand>() != 0) || (opValues != null))
            {
                if (opValues == null)
                {
                    return false;
                }
                if (this.Operands.Count<Operand>() != opValues.Count<object>())
                {
                    throw new InvalidOperationException();
                }
            }
            return true;
        }

        private void ClearOperandInfos()
        {
            foreach (DevExpress.DXBinding.Native.VisitorExpression.OperandInfo info in this.OperandInfos.Values)
            {
                info.Clear();
            }
        }

        private void ClearOperandParameters()
        {
            foreach (DevExpress.DXBinding.Native.VisitorExpression.OperandInfo info in this.OperandInfos.Values)
            {
                info.ClearParameter();
            }
        }

        internal Type GetResolvedType(NType n) => 
            this.TypeInfos[new VisitorType.TypeInfo(n)];

        public virtual void Init(ITypeResolver typeResolver)
        {
            this.TypeResolver = typeResolver;
            foreach (VisitorType.TypeInfo info in this.TypeInfos.Keys.ToList<VisitorType.TypeInfo>())
            {
                this.TypeInfos[info] = VisitorType.ResolveType(info, typeResolver, this.ErrorHandler);
            }
        }

        private void InitOperandInfos(object[] opValues)
        {
            if (opValues != null)
            {
                int num = 0;
                List<object> list = opValues.ToList<object>();
                foreach (Operand operand in this.Operands)
                {
                    this.OperandInfos[operand].Init(list[num]);
                    num++;
                }
            }
        }

        internal void InitOperands(IEnumerable<Operand> operands)
        {
            foreach (Operand operand in operands)
            {
                this.OperandInfos.Add(operand, new DevExpress.DXBinding.Native.VisitorExpression.OperandInfo());
            }
            this.VisitorExpression = new DevExpress.DXBinding.Native.VisitorExpression(this.OperandInfos, new Func<NType, Type>(this.GetResolvedType), this.ErrorHandler);
        }

        internal void InitTypeInfos(IEnumerable<VisitorType.TypeInfo> typeInfos)
        {
            foreach (VisitorType.TypeInfo info in typeInfos)
            {
                this.TypeInfos.Add(info, null);
            }
        }

        private bool NeedToRecompile(object[] opValues)
        {
            if (opValues == null)
            {
                return false;
            }
            Func<DevExpress.DXBinding.Native.VisitorExpression.OperandInfo, Type> selector = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<DevExpress.DXBinding.Native.VisitorExpression.OperandInfo, Type> local1 = <>c.<>9__29_0;
                selector = <>c.<>9__29_0 = x => x.OperandType;
            }
            IEnumerable<Type> first = this.OperandInfos.Values.Select<DevExpress.DXBinding.Native.VisitorExpression.OperandInfo, Type>(selector);
            Func<object, Type> func2 = <>c.<>9__29_1;
            if (<>c.<>9__29_1 == null)
            {
                Func<object, Type> local2 = <>c.<>9__29_1;
                func2 = <>c.<>9__29_1 = x => x?.GetType();
            }
            return !first.SequenceEqual<Type>(opValues.Select<object, Type>(func2));
        }

        internal void ResolveCore(object[] opValues, bool forceRecompile, Action recompile, Action calculate)
        {
            if (this.CheckOperandsCount(opValues))
            {
                bool flag = this.NeedToRecompile(opValues);
                this.InitOperandInfos(opValues);
                if (forceRecompile | flag)
                {
                    this.ClearOperandParameters();
                    recompile();
                }
                this.CalculateCore(calculate);
                this.ClearOperandInfos();
            }
        }

        public IEnumerable<Operand> Operands =>
            this.OperandInfos.Keys;

        internal DevExpress.DXBinding.Native.VisitorExpression VisitorExpression { get; private set; }

        internal Dictionary<VisitorType.TypeInfo, Type> TypeInfos { get; private set; }

        internal IErrorHandler ErrorHandler { get; private set; }

        internal ITypeResolver TypeResolver { get; private set; }

        internal Dictionary<Operand, DevExpress.DXBinding.Native.VisitorExpression.OperandInfo> OperandInfos { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalculatorBase.<>c <>9 = new CalculatorBase.<>c();
            public static Func<VisitorExpression.OperandInfo, Type> <>9__29_0;
            public static Func<object, Type> <>9__29_1;

            internal Type <NeedToRecompile>b__29_0(VisitorExpression.OperandInfo x) => 
                x.OperandType;

            internal Type <NeedToRecompile>b__29_1(object x) => 
                x?.GetType();
        }
    }
}

