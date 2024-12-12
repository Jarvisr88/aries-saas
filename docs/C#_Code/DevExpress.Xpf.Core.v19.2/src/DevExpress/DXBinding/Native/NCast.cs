namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NCast : NUnaryBase
    {
        public NCast(NKind kind, NBase value, NType type) : base(value)
        {
            this.Kind = kind;
            this.Type = type;
        }

        public NKind Kind { get; set; }

        public NType Type { get; set; }

        public enum NKind
        {
            Cast,
            Is,
            As
        }
    }
}

