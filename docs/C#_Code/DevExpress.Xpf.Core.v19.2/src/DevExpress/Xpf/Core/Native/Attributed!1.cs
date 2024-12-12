namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;

    public class Attributed<T>
    {
        public readonly string PropertyName;
        public readonly T Value;

        public Attributed(string propertyName, T value);
        public Attributed<T> Update(Func<T, T> update);

        public OperandProperty Property { get; }
    }
}

