namespace DevExpress.Utils
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class CompressedXmlStreamInfo
    {
        public System.IO.MemoryStream MemoryStream { get; set; }

        public ChunkedMemoryStream Stream { get; set; }

        [CLSCompliant(false)]
        public Crc32Stream CrcStream { get; set; }

        public ByteCountStream UncompressedSizeStream { get; set; }

        public System.IO.Compression.DeflateStream DeflateStream { get; set; }

        public System.IO.StreamWriter StreamWriter { get; set; }

        public XmlWriter Writer { get; set; }
    }
}

