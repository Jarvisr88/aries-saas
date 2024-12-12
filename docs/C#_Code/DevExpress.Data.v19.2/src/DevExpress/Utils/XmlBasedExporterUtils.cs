namespace DevExpress.Utils
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Xml;

    public class XmlBasedExporterUtils
    {
        private readonly StringBuilder preProcessVariableValueStringBuilder = new StringBuilder();
        private readonly FastCharacterMultiReplacement xmlCharsReplacement;
        private readonly Dictionary<char, string> xmlCharsReplacementTable = CreateVariableValueReplacementTable();
        private readonly Dictionary<char, string> xmlCharsNoCrLfReplacementTable = CreateVariableValueNoCrLfReplacementTable();
        private readonly Dictionary<char, string> xmlCharsNoCrLfTabReplacementTable = CreateVariableValueNoTabCrLfReplacementTable();
        [ThreadStatic]
        private static XmlBasedExporterUtils instance;

        private XmlBasedExporterUtils()
        {
            this.xmlCharsReplacement = new FastCharacterMultiReplacement(this.preProcessVariableValueStringBuilder);
            this.xmlCharsReplacementTable = CreateVariableValueReplacementTable();
            this.xmlCharsNoCrLfReplacementTable = CreateVariableValueNoCrLfReplacementTable();
        }

        public CompressedXmlStreamInfo BeginCreateCompressedXmlContent(XmlWriterSettings xmlSettings)
        {
            CompressedXmlStreamInfo info = new CompressedXmlStreamInfo {
                Stream = new ChunkedMemoryStream()
            };
            info.DeflateStream = new DeflateStream(info.Stream, CompressionMode.Compress);
            info.CrcStream = new Crc32Stream(info.DeflateStream);
            info.UncompressedSizeStream = new ByteCountStream(info.CrcStream);
            info.StreamWriter = new StreamWriter(info.UncompressedSizeStream, Encoding.UTF8);
            info.Writer = XmlWriter.Create(info.StreamWriter, xmlSettings);
            return info;
        }

        public CompressedXmlStreamInfo BeginCreateUncompressedXmlContent(XmlWriterSettings xmlSettings)
        {
            CompressedXmlStreamInfo info = new CompressedXmlStreamInfo {
                MemoryStream = new MemoryStream()
            };
            info.StreamWriter = new StreamWriter(info.MemoryStream, Encoding.UTF8);
            info.Writer = XmlWriter.Create(info.StreamWriter, xmlSettings);
            return info;
        }

        public CompressedStream CreateCompressedXmlContent(Action<XmlWriter> action, XmlWriterSettings xmlSettings)
        {
            CompressedXmlStreamInfo info = this.BeginCreateCompressedXmlContent(xmlSettings);
            action(info.Writer);
            return this.EndCreateCompressedXmlContent(info);
        }

        public XmlWriterSettings CreateDefaultXmlWriterSettings() => 
            new XmlWriterSettings { 
                Indent = false,
                Encoding = DXEncoding.UTF8NoByteOrderMarks,
                CheckCharacters = true,
                OmitXmlDeclaration = false
            };

        public Stream CreateUncompressedXmlContent(Action<XmlWriter> action, XmlWriterSettings xmlSettings)
        {
            CompressedXmlStreamInfo info = this.BeginCreateUncompressedXmlContent(xmlSettings);
            action(info.Writer);
            return this.EndCreateUncompressedXmlContent(info);
        }

        private static Dictionary<char, string> CreateVariableValueNoCrLfReplacementTable()
        {
            Dictionary<char, string> dictionary = new Dictionary<char, string>();
            for (char ch = '\0'; ch <= '\x001f'; ch = (char) (ch + '\x0001'))
            {
                if ((ch != '\n') && (ch != '\r'))
                {
                    dictionary.Add(ch, $"_x{(int) ch:x4}_");
                }
            }
            dictionary.Add(0xfffe, $"_x{0xfffe:x4}_");
            dictionary.Add(0xffff, $"_x{0xffff:x4}_");
            return dictionary;
        }

        private static Dictionary<char, string> CreateVariableValueNoTabCrLfReplacementTable()
        {
            Dictionary<char, string> dictionary = new Dictionary<char, string>();
            for (char ch = '\0'; ch <= '\x001f'; ch = (char) (ch + '\x0001'))
            {
                if ((ch != '\n') && ((ch != '\r') && (ch != '\t')))
                {
                    dictionary.Add(ch, $"_x{(int) ch:x4}_");
                }
            }
            dictionary.Add(0xfffe, $"_x{0xfffe:x4}_");
            dictionary.Add(0xffff, $"_x{0xffff:x4}_");
            return dictionary;
        }

        private static Dictionary<char, string> CreateVariableValueReplacementTable()
        {
            Dictionary<char, string> dictionary = new Dictionary<char, string>();
            for (char ch = '\0'; ch <= '\x001f'; ch = (char) (ch + '\x0001'))
            {
                dictionary.Add(ch, $"_x{(int) ch:x4}_");
            }
            dictionary.Add(0xfffe, $"_x{0xfffe:x4}_");
            dictionary.Add(0xffff, $"_x{0xffff:x4}_");
            return dictionary;
        }

        private void Dispose(IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public string EncodeXmlChars(string value) => 
            this.xmlCharsReplacement.PerformReplacements(value, this.xmlCharsReplacementTable);

        public string EncodeXmlCharsNoCrLf(string value) => 
            this.xmlCharsReplacement.PerformReplacements(value, this.xmlCharsNoCrLfReplacementTable);

        public string EncodeXmlCharsXML1_0(string value) => 
            this.xmlCharsReplacement.PerformReplacements(value, this.xmlCharsNoCrLfTabReplacementTable);

        public CompressedStream EndCreateCompressedXmlContent(CompressedXmlStreamInfo info)
        {
            if (info.Writer != null)
            {
                info.Writer.Flush();
                this.Dispose(info.Writer);
            }
            if (info.StreamWriter != null)
            {
                info.StreamWriter.Flush();
                this.Dispose(info.StreamWriter);
            }
            if (info.DeflateStream != null)
            {
                this.Dispose(info.DeflateStream);
            }
            info.Stream.Seek(0L, SeekOrigin.Begin);
            return new CompressedStream { 
                UncompressedSize = info.UncompressedSizeStream.WriteCheckSum,
                Crc32 = info.CrcStream.WriteCheckSum,
                Stream = info.Stream
            };
        }

        public Stream EndCreateUncompressedXmlContent(CompressedXmlStreamInfo info)
        {
            Stream stream;
            try
            {
                if (info.Writer != null)
                {
                    info.Writer.Flush();
                }
                stream = new MemoryStream(info.MemoryStream.GetBuffer(), 0, (int) info.Stream.Length);
            }
            finally
            {
                this.Dispose(info.Writer);
                this.Dispose(info.StreamWriter);
                this.Dispose(info.MemoryStream);
            }
            return stream;
        }

        public static XmlBasedExporterUtils Instance
        {
            get
            {
                instance ??= new XmlBasedExporterUtils();
                return instance;
            }
        }
    }
}

