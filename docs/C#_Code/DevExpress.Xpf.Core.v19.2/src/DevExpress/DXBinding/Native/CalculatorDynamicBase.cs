namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class CalculatorDynamicBase
    {
        internal CalculatorDynamicBase(IErrorHandler errorHandler)
        {
            this.ErrorHandler = errorHandler;
            this.Operands = new List<Operand>();
        }

        internal abstract IEnumerable<Operand> GetOperands();
        internal IDictionary<Operand, object> GetOperandsValues(object[] values)
        {
            List<object> _values = (values != null) ? values.ToList<object>() : new List<object>();
            IEnumerable<Operand> operands = this.Operands;
            if (<>c.<>9__21_0 == null)
            {
                IEnumerable<Operand> local1 = this.Operands;
                operands = (IEnumerable<Operand>) (<>c.<>9__21_0 = (x, i) => new { 
                    x = x,
                    i = i
                });
            }
            var keySelector = <>c.<>9__21_1;
            if (<>c.<>9__21_1 == null)
            {
                var local2 = <>c.<>9__21_1;
                keySelector = <>c.<>9__21_1 = x => x.x;
            }
            return ((IEnumerable<Operand>) <>c.<>9__21_0).Select(((Func<Operand, int, <>f__AnonymousType8<Operand, int>>) operands)).ToDictionary(keySelector, x => _values.ElementAtOrDefault<object>(x.i));
        }

        internal Type GetResolvedType(NType n) => 
            this.TypeInfos[new VisitorType.TypeInfo(n)];

        internal abstract IEnumerable<VisitorType.TypeInfo> GetTypeInfos();
        public virtual void Init(ITypeResolver typeResolver)
        {
            this.TypeResolver = typeResolver;
            this.TypeInfos = new Dictionary<VisitorType.TypeInfo, Type>();
            foreach (VisitorType.TypeInfo info in this.GetTypeInfos())
            {
                this.TypeInfos.Add(info, VisitorType.ResolveType(info, this.TypeResolver, this.ErrorHandler));
            }
            this.Operands = this.GetOperands().ToList<Operand>();
        }

        public IEnumerable<Operand> Operands { get; private set; }

        internal Dictionary<VisitorType.TypeInfo, Type> TypeInfos { get; private set; }

        internal IErrorHandler ErrorHandler { get; private set; }

        internal ITypeResolver TypeResolver { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalculatorDynamicBase.<>c <>9 = new CalculatorDynamicBase.<>c();
            public static Func<Operand, int, <>f__AnonymousType8<Operand, int>> <>9__21_0;
            public static Func<<>f__AnonymousType8<Operand, int>, Operand> <>9__21_1;

            internal <>f__AnonymousType8<Operand, int> <GetOperandsValues>b__21_0(Operand x, int i) => 
                new { 
                    x = x,
                    i = i
                };

            internal Operand <GetOperandsValues>b__21_1(<>f__AnonymousType8<Operand, int> x) => 
                x.x;
        }
    }
}

