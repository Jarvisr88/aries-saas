namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Csv;
    using DevExpress.SpreadsheetSource.Xls;
    using DevExpress.SpreadsheetSource.Xlsx;
    using DevExpress.Utils;
    using DevExpress.Utils.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public static class SpreadsheetSourceFactory
    {
        private const SpreadsheetDocumentFormat undefinedFormat = ~SpreadsheetDocumentFormat.Xls;
        private const int maxSignatureLength = 8;
        private static readonly byte[] cfbSignature = new byte[] { 0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1a, 0xe1 };
        private static readonly byte[] zipSignature = new byte[] { 80, 0x4b };
        private const string workbookStreamName = "Workbook";
        private const string bookStreamName = "Book";
        private const string ctWorkbook = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml";
        private const string ctTemplate = "application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml";
        private const string ctMacroEnabledWorkbook = "application/vnd.ms-excel.sheet.macroEnabled.main+xml";
        private const string ctMacroEnabledTemplate = "application/vnd.ms-excel.template.macroEnabled.main+xml";

        private static bool CheckFileStreamExtension(Stream stream, string extension)
        {
            FileStream stream2 = stream as FileStream;
            if (stream2 == null)
            {
                return false;
            }
            string strB = Path.GetExtension(stream2.Name);
            return (string.Compare(extension, strB, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        private static bool CheckSignature(byte[] buffer, byte[] signature)
        {
            for (int i = 0; i < signature.Length; i++)
            {
                if (buffer[i] != signature[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static ISpreadsheetSource CreateSource(string fileName) => 
            CreateSource(fileName, DetectFormat(fileName), new SpreadsheetSourceOptions());

        public static ISpreadsheetSource CreateSource(Stream stream, SpreadsheetDocumentFormat documentFormat) => 
            CreateSource(stream, documentFormat, new SpreadsheetSourceOptions());

        public static ISpreadsheetSource CreateSource(string fileName, ISpreadsheetSourceOptions options) => 
            CreateSource(fileName, DetectFormat(fileName), options);

        public static ISpreadsheetSource CreateSource(string fileName, SpreadsheetDocumentFormat documentFormat) => 
            CreateSource(fileName, documentFormat, new SpreadsheetSourceOptions());

        public static ISpreadsheetSource CreateSource(Stream stream, SpreadsheetDocumentFormat documentFormat, ISpreadsheetSourceOptions options)
        {
            switch (documentFormat)
            {
                case SpreadsheetDocumentFormat.Xls:
                    return new XlsSpreadsheetSource(stream, options);

                case SpreadsheetDocumentFormat.Xlsx:
                case SpreadsheetDocumentFormat.Xlsm:
                    return new XlsxSpreadsheetSource(stream, options);

                case SpreadsheetDocumentFormat.Csv:
                    return new CsvSpreadsheetSource(stream, CsvSpreadsheetSourceOptions.ConvertToCsvOptions(options));
            }
            return null;
        }

        public static ISpreadsheetSource CreateSource(string fileName, SpreadsheetDocumentFormat documentFormat, ISpreadsheetSourceOptions options)
        {
            switch (documentFormat)
            {
                case SpreadsheetDocumentFormat.Xls:
                    return new XlsSpreadsheetSource(fileName, options);

                case SpreadsheetDocumentFormat.Xlsx:
                case SpreadsheetDocumentFormat.Xlsm:
                    return new XlsxSpreadsheetSource(fileName, options);

                case SpreadsheetDocumentFormat.Csv:
                    return new CsvSpreadsheetSource(fileName, CsvSpreadsheetSourceOptions.ConvertToCsvOptions(options));
            }
            return null;
        }

        private static SpreadsheetDocumentFormat DetectCompoundFileBinaryContent(Stream stream)
        {
            using (PackageFileReader reader = new PackageFileReader(stream, true))
            {
                BinaryReader cachedPackageFileReader = reader.GetCachedPackageFileReader("Workbook");
                if (cachedPackageFileReader == null)
                {
                    cachedPackageFileReader = reader.GetCachedPackageFileReader("Book");
                    if (cachedPackageFileReader == null)
                    {
                        return ~SpreadsheetDocumentFormat.Xls;
                    }
                }
                return DetectContentType(cachedPackageFileReader);
            }
        }

        private static SpreadsheetDocumentFormat DetectContentType(PackageFile packageFile)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                packageFile.Stream.CopyTo(stream);
                SpreadsheetDocumentFormat format = DetectContentTypeByOverrides(stream);
                if (format == ~SpreadsheetDocumentFormat.Xls)
                {
                    format = DetectContentTypeByDefaults(stream);
                }
                return format;
            }
        }

        private static SpreadsheetDocumentFormat DetectContentType(BinaryReader reader)
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                short num = reader.ReadInt16();
                if ((num == 10) || (num == 0x60))
                {
                    return SpreadsheetDocumentFormat.Xls;
                }
                short num2 = reader.ReadInt16();
                if ((num2 < 0) || (num2 > 0x2020))
                {
                    return ~SpreadsheetDocumentFormat.Xls;
                }
                if (num2 > 0)
                {
                    reader.BaseStream.Seek((long) num2, SeekOrigin.Current);
                }
            }
            return SpreadsheetDocumentFormat.Xls;
        }

        private static SpreadsheetDocumentFormat DetectContentTypeByDefaults(Stream stream)
        {
            stream.Position = 0L;
            XmlReader reader = XmlReader.Create(stream);
            if (reader != null)
            {
                while (reader.ReadToFollowing("Default"))
                {
                    string attribute = reader.GetAttribute("Extension");
                    string str2 = reader.GetAttribute("ContentType");
                    if (attribute == "xml")
                    {
                        if ((str2 == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml") || (str2 == "application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml"))
                        {
                            return SpreadsheetDocumentFormat.Xlsx;
                        }
                        if ((str2 == "application/vnd.ms-excel.sheet.macroEnabled.main+xml") || (str2 == "application/vnd.ms-excel.template.macroEnabled.main+xml"))
                        {
                            return SpreadsheetDocumentFormat.Xlsm;
                        }
                    }
                }
            }
            return ~SpreadsheetDocumentFormat.Xls;
        }

        private static SpreadsheetDocumentFormat DetectContentTypeByOverrides(Stream stream)
        {
            stream.Position = 0L;
            XmlReader reader = XmlReader.Create(stream);
            if (reader != null)
            {
                while (reader.ReadToFollowing("Override"))
                {
                    string attribute = reader.GetAttribute("PartName");
                    if (attribute == "/xl/workbook.xml")
                    {
                        string str2 = reader.GetAttribute("ContentType");
                        if ((str2 == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml") || (str2 == "application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml"))
                        {
                            return SpreadsheetDocumentFormat.Xlsx;
                        }
                        if ((str2 == "application/vnd.ms-excel.sheet.macroEnabled.main+xml") || (str2 == "application/vnd.ms-excel.template.macroEnabled.main+xml"))
                        {
                            return SpreadsheetDocumentFormat.Xlsm;
                        }
                    }
                }
            }
            return ~SpreadsheetDocumentFormat.Xls;
        }

        internal static SpreadsheetDocumentFormat DetectFormat(Stream stream)
        {
            SpreadsheetDocumentFormat format = DetectFormatCore(stream);
            if (format == ~SpreadsheetDocumentFormat.Xls)
            {
                throw new ArgumentException("Unknown document format.");
            }
            return format;
        }

        internal static SpreadsheetDocumentFormat DetectFormat(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                SpreadsheetDocumentFormat format = DetectFormatCore(stream);
                if (format != ~SpreadsheetDocumentFormat.Xls)
                {
                    return format;
                }
            }
            string extension = Path.GetExtension(fileName);
            if (StringExtensions.CompareInvariantCultureIgnoreCase(extension, ".xls") == 0)
            {
                return SpreadsheetDocumentFormat.Xls;
            }
            if (StringExtensions.CompareInvariantCultureIgnoreCase(extension, ".xlsx") == 0)
            {
                return SpreadsheetDocumentFormat.Xlsx;
            }
            if (StringExtensions.CompareInvariantCultureIgnoreCase(extension, ".xlsm") == 0)
            {
                return SpreadsheetDocumentFormat.Xlsm;
            }
            if (StringExtensions.CompareInvariantCultureIgnoreCase(extension, ".csv") != 0)
            {
                throw new ArgumentException("Unknown extension. Can't detect document format.");
            }
            return SpreadsheetDocumentFormat.Csv;
        }

        private static SpreadsheetDocumentFormat DetectFormatCore(Stream stream)
        {
            if ((stream != null) && ((stream.Length > 0L) && stream.CanSeek))
            {
                long position = stream.Position;
                try
                {
                    byte[] signatureBuffer = GetSignatureBuffer(stream);
                    if (!CheckSignature(signatureBuffer, cfbSignature))
                    {
                        SpreadsheetDocumentFormat csv;
                        if (!CheckSignature(signatureBuffer, zipSignature))
                        {
                            if (!CheckFileStreamExtension(stream, ".csv"))
                            {
                                goto TR_0000;
                            }
                            else
                            {
                                csv = SpreadsheetDocumentFormat.Csv;
                            }
                        }
                        else
                        {
                            csv = DetectZippedFileContent(stream);
                        }
                        return csv;
                    }
                    else
                    {
                        return DetectCompoundFileBinaryContent(stream);
                    }
                }
                catch
                {
                }
                finally
                {
                    stream.Seek(position, SeekOrigin.Begin);
                }
            }
        TR_0000:
            return ~SpreadsheetDocumentFormat.Xls;
        }

        private static SpreadsheetDocumentFormat DetectZippedFileContent(Stream stream)
        {
            SpreadsheetDocumentFormat format;
            long position = stream.Position;
            InternalZipFileCollection files = InternalZipArchive.Open(stream);
            stream.Seek(position, SeekOrigin.Begin);
            using (List<InternalZipFile>.Enumerator enumerator = files.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        InternalZipFile current = enumerator.Current;
                        if (!current.FileName.Contains("[Content_Types].xml"))
                        {
                            continue;
                        }
                        format = DetectContentType(new PackageFile(current.FileName.Replace('\\', '/'), current.FileDataStream, (int) current.UncompressedSize));
                    }
                    else
                    {
                        return ~SpreadsheetDocumentFormat.Xls;
                    }
                    break;
                }
            }
            return format;
        }

        private static byte[] GetSignatureBuffer(Stream stream)
        {
            byte[] buffer2;
            long position = stream.Position;
            try
            {
                byte[] buffer = new byte[8];
                stream.Read(buffer, 0, buffer.Length);
                buffer2 = buffer;
            }
            finally
            {
                stream.Seek(position, SeekOrigin.Begin);
            }
            return buffer2;
        }
    }
}

