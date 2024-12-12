namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ExtendedEntryContext : BamlEntryContext
    {
        public ExtendedEntryContext(byte type);
        private void CalcSize(BamlStreamWriter writer, out int size, out long position);
        public override void Read(BamlStreamReader reader);
        protected virtual void ReadData(BamlStreamReader reader);
        private void ReadEntrySize(BamlStreamReader reader);
        public override void Write(BamlStreamWriter writer);
        protected virtual void WriteContent(BamlStreamWriter writer);
        private void WriteSize(BamlStreamWriter writer);

        public int Size { get; protected set; }

        public byte[] Data { get; private set; }

        public int EncodedSize { get; private set; }
    }
}

