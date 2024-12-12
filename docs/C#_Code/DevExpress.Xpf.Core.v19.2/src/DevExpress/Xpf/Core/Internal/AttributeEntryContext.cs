namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class AttributeEntryContext : ExtendedEntryContext
    {
        public AttributeEntryContext();
        protected override void ReadData(BamlStreamReader reader);
        protected override void WriteContent(BamlStreamWriter writer);

        public short Id { get; set; }

        public short OwnerType { get; private set; }

        private byte Usage { get; set; }

        public string Name { get; set; }
    }
}

