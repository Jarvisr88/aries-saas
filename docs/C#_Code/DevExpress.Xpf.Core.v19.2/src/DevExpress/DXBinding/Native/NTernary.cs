namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NTernary : NBase
    {
        public NTernary(NKind kind, NBase first, NBase second, NBase third)
        {
            this.Kind = kind;
            this.First = first;
            this.Second = second;
            this.Third = third;
        }

        public NBase First { get; set; }

        public NBase Second { get; set; }

        public NBase Third { get; set; }

        public NKind Kind { get; set; }

        public enum NKind
        {
            Condition
        }
    }
}

