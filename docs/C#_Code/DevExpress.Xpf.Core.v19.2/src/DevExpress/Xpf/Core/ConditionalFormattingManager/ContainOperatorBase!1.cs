namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ContainOperatorBase<T> where T: struct, IComparable
    {
        private readonly string name;
        private CriteriaOperator prototype;

        public ContainOperatorBase(string name, T rule, Func<OperandProperty, OperandValue[], CriteriaOperator> factory, Func<CriteriaOperator, object[]> extractor)
        {
            this.name = name;
            this.Factory = factory;
            this.Extractor = extractor;
            Func<int, UniversalValue<T>> selector = <>c<T>.<>9__16_0;
            if (<>c<T>.<>9__16_0 == null)
            {
                Func<int, UniversalValue<T>> local1 = <>c<T>.<>9__16_0;
                selector = <>c<T>.<>9__16_0 = x => new UniversalValue<T>();
            }
            this.prototype = this.Factory(new UniversalProperty<T>(), Enumerable.Range(0, 2).Select<int, UniversalValue<T>>(selector).ToArray<UniversalValue<T>>());
            this.Rule = rule;
        }

        public bool Match(CriteriaOperator op) => 
            this.prototype.Equals(op);

        public override string ToString() => 
            this.name;

        public Func<OperandProperty, OperandValue[], CriteriaOperator> Factory { get; private set; }

        public Func<CriteriaOperator, object[]> Extractor { get; private set; }

        public T Rule { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainOperatorBase<T>.<>c <>9;
            public static Func<int, ContainOperatorBase<T>.UniversalValue> <>9__16_0;

            static <>c()
            {
                ContainOperatorBase<T>.<>c.<>9 = new ContainOperatorBase<T>.<>c();
            }

            internal ContainOperatorBase<T>.UniversalValue <.ctor>b__16_0(int x) => 
                new ContainOperatorBase<T>.UniversalValue();
        }

        private class UniversalProperty : OperandProperty
        {
            public override bool Equals(object obj) => 
                obj is OperandProperty;

            public override int GetHashCode() => 
                base.GetHashCode();
        }

        private class UniversalValue : OperandValue
        {
            public override bool Equals(object obj) => 
                obj is OperandValue;

            public override int GetHashCode() => 
                base.GetHashCode();
        }
    }
}

