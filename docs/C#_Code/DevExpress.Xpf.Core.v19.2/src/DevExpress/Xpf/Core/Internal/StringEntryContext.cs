namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class StringEntryContext : ExtendedEntryContext
    {
        public StringEntryContext();
        protected override void ReadData(BamlStreamReader reader);
        protected override void WriteContent(BamlStreamWriter writer);

        public short Id { get; set; }

        public string Value { get; set; }
    }
}

