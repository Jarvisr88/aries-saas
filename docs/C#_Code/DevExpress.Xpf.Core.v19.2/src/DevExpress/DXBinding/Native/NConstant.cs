namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NConstant : NBase
    {
        public NConstant(NKind kind, object value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        public object Value { get; set; }

        public NKind Kind { get; set; }

        public enum NKind
        {
            Integer,
            Float,
            String,
            Boolean,
            Null
        }
    }
}

