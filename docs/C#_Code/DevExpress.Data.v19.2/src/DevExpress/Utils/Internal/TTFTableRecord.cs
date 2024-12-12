namespace DevExpress.Utils.Internal
{
    using System;
    using System.IO;

    internal class TTFTableRecord
    {
        private string tag;
        private uint offset;
        private uint length;

        public void Read(BigEndianStreamReader reader)
        {
            this.tag = reader.ReadFixedString(4);
            reader.Stream.Seek(4L, SeekOrigin.Current);
            this.offset = reader.ReadUInt32();
            this.length = reader.ReadUInt32();
        }

        public override string ToString()
        {
            string[] textArray1 = new string[] { this.Tag, ":", this.Length.ToString(), " bytes at ", this.Offset.ToString() };
            return string.Concat(textArray1);
        }

        public string Tag =>
            this.tag;

        public uint Offset =>
            this.offset;

        public uint Length =>
            this.length;
    }
}

