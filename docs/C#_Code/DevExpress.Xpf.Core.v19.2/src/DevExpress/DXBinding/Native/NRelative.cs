namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NRelative : NIdentBase
    {
        public NRelative(string name, NIdentBase next, NKind kind) : base(name, next)
        {
            this.Kind = kind;
        }

        public NKind Kind { get; set; }

        public string ElementName { get; set; }

        public string ResourceName { get; set; }

        public string ReferenceName { get; set; }

        public NType AncestorType { get; set; }

        public int? AncestorLevel { get; set; }

        public enum NKind
        {
            Context,
            Self,
            Parent,
            Element,
            Resource,
            Reference,
            Ancestor,
            Value,
            Parameter,
            Sender,
            Args
        }
    }
}

