namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NUnary : NUnaryBase
    {
        public NUnary(NKind kind, NBase value) : base(value)
        {
            this.Kind = kind;
        }

        public NKind Kind { get; set; }

        public enum NKind
        {
            Plus,
            Minus,
            Not,
            NotBitwise
        }
    }
}

