namespace DevExpress.Xpf.Core.Internal
{
    using System;

    internal class SingleByteEntryContext : BamlEntryContext
    {
        public SingleByteEntryContext(byte type);
        public override void Read(BamlStreamReader reader);
        public override void Write(BamlStreamWriter writer);
    }
}

