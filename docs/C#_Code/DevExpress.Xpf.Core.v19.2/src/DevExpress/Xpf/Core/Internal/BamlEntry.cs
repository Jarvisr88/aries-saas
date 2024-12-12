namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class BamlEntry
    {
        private readonly BamlEntryContext context;

        internal BamlEntry(BamlEntryContext context);
        public BamlEntry(byte type);
        public void Read(BamlStreamReader reader);
        public void Write(BamlStreamWriter writer);

        public byte Type { get; }

        public int Position { get; set; }

        public BamlEntryContext Context { get; }

        public BamlEntry DefferableContent { get; set; }
    }
}

