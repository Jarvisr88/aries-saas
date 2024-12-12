namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NamedElementStartEntryContext : BamlEntryContext
    {
        public NamedElementStartEntryContext();
        public NamedElementStartEntryContext(byte type);
        public override void Read(BamlStreamReader reader);
        public override void Write(BamlStreamWriter writer);

        private string RuntimeName { get; set; }

        private short TypeId { get; set; }
    }
}

