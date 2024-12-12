namespace DevExpress.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class UnboundSourceValueNeededEventArgs : EventArgs
    {
        private int _RowIndex;
        private readonly string _Name;
        private readonly object _Tag;
        private readonly int _PropertyIndex;

        private UnboundSourceValueNeededEventArgs(string name, object tag, int propertyIndex);
        internal void Init(int rowIndex);
        public abstract void SetValue<T>(T value);
        protected internal virtual void UnInit();

        public int RowIndex { get; }

        public string PropertyName { get; }

        public object Tag { get; }

        public int PropertyIndex { get; }

        public abstract object Value { get; set; }

        internal sealed class Typed<TypeOfValue> : UnboundSourceValueNeededEventArgs
        {
            private TypeOfValue _Value;

            public Typed(string name, object tag, int propertyIndex);
            public TypeOfValue GetValue();
            public override void SetValue<T>(T value);
            protected internal override void UnInit();

            public override object Value { get; set; }

            private static class ConvHolder<T>
            {
                public static readonly Func<T, TypeOfValue> Convertor;

                static ConvHolder();
                private static Func<T, TypeOfValue> CreateConvertor();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly UnboundSourceValueNeededEventArgs.Typed<TypeOfValue>.ConvHolder<T>.<>c <>9;
                    public static Func<T, T> <>9__1_0;
                    public static Func<T, TypeOfValue> <>9__1_1;

                    static <>c();
                    internal T <CreateConvertor>b__1_0(T x);
                    internal TypeOfValue <CreateConvertor>b__1_1(T x);
                }
            }
        }

        internal sealed class Untyped : UnboundSourceValueNeededEventArgs
        {
            private object _Value;

            public Untyped(string name, object tag, int propertyIndex);
            public override void SetValue<T>(T value);
            protected internal override void UnInit();

            public override object Value { get; set; }
        }
    }
}

