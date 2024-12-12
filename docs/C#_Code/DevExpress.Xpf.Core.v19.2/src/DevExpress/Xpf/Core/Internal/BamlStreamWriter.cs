namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.IO;

    internal class BamlStreamWriter : BinaryWriter
    {
        private readonly int byte1;
        private readonly int byte2;
        private readonly int byte3;
        private readonly int byte4;

        public BamlStreamWriter(Stream output);
        private int CalcEncodedSize(int size);
        private int CalcEntrySize(int entrySize);
        public void WriteEntrySize(int size);

        public long Position { get; set; }
    }
}

