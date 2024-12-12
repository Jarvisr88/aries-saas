namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DeferrableAttributeEntryContext : ExtendedEntryContext
    {
        public DeferrableAttributeEntryContext();
        protected override void ReadData(BamlStreamReader reader);
        protected override void WriteContent(BamlStreamWriter writer);

        public string Value { get; set; }

        public short Id { get; internal set; }
    }
}

