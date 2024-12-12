namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.IO;

    internal class BamlStreamReader : BinaryReader
    {
        public BamlStreamReader(Stream input);
        public int ReadEncodedInt();

        public long Position { get; }

        public bool CanRead { get; }
    }
}

