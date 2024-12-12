namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    public static class PdfFormDataReader
    {
        private const char tabulationCharacter = '\t';
        private const char quotaCharacter = '"';
        private const string originalAttributeName = "{http://ns.adobe.com/xfdf-transition/}original";
        private const string nameAttributeName = "name";
        private static readonly XNamespace xfdfNamespace = "http://ns.adobe.com/xfdf/";

        public static void Load(PdfFormData formData, Stream stream, PdfFormDataFormat format)
        {
            try
            {
                switch (format)
                {
                    case PdfFormDataFormat.Fdf:
                        FdfDocumentReader.Read(stream, formData);
                        break;

                    case PdfFormDataFormat.Xml:
                    case PdfFormDataFormat.Xfdf:
                        ReadXml(stream, formData);
                        break;

                    case PdfFormDataFormat.Txt:
                        ReadTxt(stream, formData);
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectFormDataFile));
            }
        }

        private static void ReadTxt(Stream stream, PdfFormData formData)
        {
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadLine();
            char[] trimChars = new char[] { '\r', '\n' };
            string str2 = reader.ReadToEnd().TrimEnd(trimChars);
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
            {
                char[] separator = new char[] { '\t' };
                string[] strArray = str.Split(separator);
                char[] chArray3 = new char[] { '\t' };
                string[] strArray2 = str2.Split(chArray3);
                int length = strArray2.Length;
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = (i < length) ? strArray2[i] : "";
                    if (!str3.StartsWith('"'.ToString()))
                    {
                        formData[strArray[i]].Value = str3;
                    }
                    else
                    {
                        char[] chArray4 = new char[] { '\n' };
                        string[] collection = str3.Substring(1, str3.Length - 2).Replace("\"\"", "\"").Split(chArray4);
                        formData[strArray[i]].Value = (collection.Length != 1) ? ((object) new List<string>(collection)) : ((object) collection[0]);
                    }
                }
            }
        }

        private static void ReadXml(Stream stream, PdfFormData formData)
        {
            XDocument document;
            using (XmlReader reader = SafeXml.CreateReader(stream, DtdProcessing.Prohibit, null))
            {
                document = XDocument.Load(reader);
            }
            XElement root = document.Root;
            if (string.Equals(root.Name.LocalName, "xfdf", StringComparison.OrdinalIgnoreCase))
            {
                root = root.Element(xfdfNamespace + "fields");
            }
            if (string.Equals(root.Name.LocalName, "fields", StringComparison.OrdinalIgnoreCase))
            {
                foreach (XElement element2 in root.Elements())
                {
                    XAttribute attribute = element2.Attribute("{http://ns.adobe.com/xfdf-transition/}original") ?? element2.Attribute("name");
                    string str = (attribute == null) ? element2.Name.LocalName : attribute.Value;
                    List<string> list = new List<string>();
                    foreach (XElement element3 in element2.Elements())
                    {
                        list.Add(element3.Value);
                    }
                    int count = list.Count;
                    formData[str].Value = (count == 0) ? element2.Value : ((count == 1) ? ((object) list[0]) : ((object) list));
                }
            }
        }
    }
}

