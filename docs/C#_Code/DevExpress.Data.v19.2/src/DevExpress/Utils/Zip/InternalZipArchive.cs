namespace DevExpress.Utils.Zip
{
    using DevExpress.Utils;
    using DevExpress.Utils.Zip.Internal;
    using System;
    using System.IO;
    using System.Text;

    public class InternalZipArchive : InternalZipArchiveCore
    {
        public InternalZipArchive(Stream stream) : base(stream)
        {
        }

        public InternalZipArchive(string zipFileName) : base(zipFileName)
        {
        }

        public static bool IsZipFileSignature(int value) => 
            value == 0x4034b50;

        public static InternalZipFileCollection Open(Stream stream) => 
            Open(stream, DXEncoding.Default);

        public static InternalZipFileCollection Open(Stream stream, Encoding fileNameEncoding)
        {
            InternalZipFileParser parser = new InternalZipFileParser();
            parser.Parse(stream, fileNameEncoding);
            return parser.Records;
        }
    }
}

