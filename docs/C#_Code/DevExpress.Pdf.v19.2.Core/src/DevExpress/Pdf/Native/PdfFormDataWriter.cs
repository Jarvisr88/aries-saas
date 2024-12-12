namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Linq;

    public abstract class PdfFormDataWriter
    {
        protected PdfFormDataWriter(PdfFormData formData)
        {
            this.<FormData>k__BackingField = formData;
        }

        public static void Save(PdfFormData formData, Stream stream, PdfFormDataFormat format)
        {
            switch (format)
            {
                case PdfFormDataFormat.Fdf:
                    FdfDocumentWriter.Write(stream, formData);
                    return;

                case PdfFormDataFormat.Xml:
                    new PdfFormDataXMLWriter(formData).Write(stream);
                    return;

                case PdfFormDataFormat.Xfdf:
                    new PdfFormDataXFDFWriter(formData).Write(stream);
                    return;

                case PdfFormDataFormat.Txt:
                    new PdfFormDataTXTWriter(formData).Write(stream);
                    return;
            }
        }

        public abstract void Write(Stream stream);
        protected void WriteFields(Action<string, object> fieldWriter)
        {
            Queue<KeyValuePair<string, PdfFormData>> queue = new Queue<KeyValuePair<string, PdfFormData>>();
            IDictionary<string, PdfFormData> kids = this.FormData.Kids;
            if (kids != null)
            {
                foreach (PdfFormData data in kids.Values)
                {
                    queue.Enqueue(new KeyValuePair<string, PdfFormData>(data.Name, data));
                }
            }
            while (queue.Count > 0)
            {
                KeyValuePair<string, PdfFormData> pair = queue.Dequeue();
                if (pair.Value.Kids == null)
                {
                    if (pair.Value.Value == null)
                    {
                        continue;
                    }
                    fieldWriter(pair.Key, pair.Value.Value);
                    continue;
                }
                foreach (PdfFormData data2 in pair.Value.Kids.Values)
                {
                    queue.Enqueue(new KeyValuePair<string, PdfFormData>(pair.Key + "." + data2.Name, data2));
                }
            }
        }

        protected PdfFormData FormData { get; }

        private class PdfFormDataTXTWriter : PdfFormDataWriter
        {
            private const char tabulationCharacter = '\t';
            private const char quotaCharacter = '"';

            public PdfFormDataTXTWriter(PdfFormData formData) : base(formData)
            {
            }

            public override void Write(Stream stream)
            {
                string names = "";
                string values = "";
                base.WriteFields(delegate (string name, object value) {
                    names = names + name + "\t";
                    IEnumerable<string> enumerable = value as IEnumerable<string>;
                    if (enumerable == null)
                    {
                        string str3 = ((string) value).Replace("\r\n", "\n").Replace("\"", "\"\"");
                        if (str3.Contains("\n"))
                        {
                            str3 = "\"" + str3 + "\"";
                        }
                        values = values + str3 + "\t";
                    }
                    else
                    {
                        string str = '"'.ToString();
                        foreach (string str2 in enumerable)
                        {
                            str = str + str2 + "\n";
                        }
                        char[] trimChars = new char[] { '\n' };
                        str = str.TrimEnd(trimChars) + "\"";
                        values = values + str + "\t";
                    }
                });
                names = names.Substring(0, Math.Max(0, names.Length - 1));
                values = values.Substring(0, Math.Max(0, values.Length - 1));
                Encoding encoding = Encoding.UTF8;
                byte[] bytes = encoding.GetBytes(names + "\r\n");
                stream.Write(bytes, 0, bytes.Length);
                byte[] buffer = encoding.GetBytes(values + "\r\n");
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        private class PdfFormDataXFDFWriter : PdfFormDataWriter.PdfFormDataXMLWriter
        {
            private static readonly XNamespace xfdfNamespace = "http://ns.adobe.com/xfdf/";

            public PdfFormDataXFDFWriter(PdfFormData formData) : base(formData)
            {
            }

            public override void Write(Stream stream)
            {
                XDocument document = new XDocument();
                XElement content = new XElement(xfdfNamespace + "xfdf");
                content.Add(base.WriteXmlFields(xfdfNamespace, false));
                document.Add(content);
                document.Save(stream);
            }
        }

        private class PdfFormDataXMLWriter : PdfFormDataWriter
        {
            private const string nameAttributeName = "name";

            public PdfFormDataXMLWriter(PdfFormData formData) : base(formData)
            {
            }

            public override void Write(Stream stream)
            {
                object[] content = new object[] { this.WriteXmlFields("", true) };
                new XDocument(content).Save(stream);
            }

            protected XElement WriteXmlFields(XNamespace ns, bool writeOriginal)
            {
                XNamespace original = "http://ns.adobe.com/xfdf-transition/";
                XElement fields = new XElement(ns + "fields");
                base.WriteFields(delegate (string name, object value) {
                    List<string> list = value as List<string>;
                    XElement content = new XElement(ns + "field");
                    if (writeOriginal)
                    {
                        content.SetAttributeValue(original + "original", name);
                    }
                    content.SetAttributeValue("name", name);
                    if (list == null)
                    {
                        if (writeOriginal)
                        {
                            content.Add(value);
                        }
                        else
                        {
                            content.Add(new XElement(ns + "value", value));
                        }
                    }
                    else
                    {
                        foreach (string str in list)
                        {
                            content.Add(new XElement(ns + "value", str));
                        }
                    }
                    fields.Add(content);
                });
                return fields;
            }
        }
    }
}

