namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NBinary : NBase
    {
        public NBinary(NKind kind, NBase left, NBase right)
        {
            this.Kind = kind;
            this.Left = left;
            this.Right = right;
        }

        public NBase Left { get; set; }

        public NBase Right { get; set; }

        public NKind Kind { get; set; }

        public enum NKind
        {
            Mul,
            Div,
            Mod,
            Plus,
            Minus,
            ShiftLeft,
            ShiftRight,
            Less,
            Greater,
            LessOrEqual,
            GreaterOrEqual,
            And,
            Or,
            Xor,
            AndAlso,
            OrElse,
            Equal,
            NotEqual,
            Coalesce
        }
    }
}

