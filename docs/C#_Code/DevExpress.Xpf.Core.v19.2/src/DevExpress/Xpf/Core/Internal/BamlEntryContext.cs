namespace DevExpress.Xpf.Core.Internal
{
    using System;

    internal abstract class BamlEntryContext
    {
        private readonly byte type;

        protected BamlEntryContext(byte type);
        public abstract void Read(BamlStreamReader reader);
        public abstract void Write(BamlStreamWriter writer);

        public byte Type { get; }
    }
}

