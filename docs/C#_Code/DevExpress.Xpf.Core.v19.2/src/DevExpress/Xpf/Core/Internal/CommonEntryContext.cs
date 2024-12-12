namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class CommonEntryContext : BamlEntryContext
    {
        public CommonEntryContext(byte type);
        private int GetSize();
        public override void Read(BamlStreamReader reader);
        public override void Write(BamlStreamWriter writer);

        public byte[] Data { get; private set; }
    }
}

