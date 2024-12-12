namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Xml;

    public class XamlWriter : IDisposable
    {
        private const string defaultFormatString = "{0}";
        private const string argbColorFormatString = "#{0:X8}";
        private XmlWriter writer;
        private bool disposed;

        public XamlWriter(Stream output)
        {
            Guard.ArgumentNotNull(output, "output");
            XmlWriterSettings settings1 = new XmlWriterSettings();
            settings1.OmitXmlDeclaration = true;
            settings1.Indent = true;
            XmlWriterSettings settings = settings1;
            this.writer = XmlWriter.Create(output, settings);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.writer.Close();
                    this.writer = null;
                }
                this.disposed = true;
            }
        }

        public void Flush()
        {
            this.writer.Flush();
        }

        private static string JoinItemsToString<T>(IEnumerable<T> items) where T: IConvertible
        {
            Func<T, string> selector = <>c__31<T>.<>9__31_0;
            if (<>c__31<T>.<>9__31_0 == null)
            {
                Func<T, string> local1 = <>c__31<T>.<>9__31_0;
                selector = <>c__31<T>.<>9__31_0 = value => value.ToString(CultureInfo.InvariantCulture);
            }
            IEnumerable<string> values = items.Select<T, string>(selector);
            return string.Join(",", values);
        }

        private static string RemoveInvalidXmlCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            Func<char, bool> predicate = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<char, bool> local1 = <>c.<>9__32_0;
                predicate = <>c.<>9__32_0 = ch => XmlConvert.IsXmlChar(ch);
            }
            return string.Concat<char>(text.Where<char>(predicate));
        }

        public void WriteAttribute(string attribute, Color value)
        {
            this.WriteAttributeCore(attribute, "#{0:X8}", value.ToArgb());
        }

        public void WriteAttribute(string attribute, RectangleF value)
        {
            float[] values = new float[] { value.X, value.Y, value.Width, value.Height };
            this.WriteAttribute(attribute, values);
        }

        public void WriteAttribute(string attribute, params float[] values)
        {
            Guard.ArgumentNotNull(values, "values");
            string str = JoinItemsToString<float>(values);
            this.WriteAttribute(attribute, str);
        }

        public void WriteAttribute(string attribute, float value)
        {
            this.WriteAttributeCore(attribute, "{0}", value);
        }

        public void WriteAttribute(string attribute, string value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.WriteAttributeCore(attribute, "{0}", value);
        }

        public void WriteAttribute(string prefix, string attribute, Color value)
        {
            object[] args = new object[] { value.ToArgb() };
            string str = string.Format(CultureInfo.InvariantCulture, "#{0:X8}", args);
            this.WriteAttribute(prefix, attribute, str);
        }

        public void WriteAttribute(string prefix, string attribute, RectangleF value)
        {
            float[] values = new float[] { value.X, value.Y, value.Width, value.Height };
            this.WriteAttribute(prefix, attribute, values);
        }

        public void WriteAttribute(string prefix, string attribute, params float[] values)
        {
            string str = JoinItemsToString<float>(values);
            this.WriteAttribute(prefix, attribute, str);
        }

        public void WriteAttribute(string prefix, string attribute, string value)
        {
            this.writer.WriteAttributeString(prefix, attribute, null, value);
        }

        private void WriteAttributeCore(string attribute, string format, object value)
        {
            object[] args = new object[] { value };
            string text = string.Format(CultureInfo.InvariantCulture, format, args);
            if ((attribute == XamlAttribute.Text) && ((text.Length > 0) && (text[0] == '{')))
            {
                text = "{}" + text;
            }
            string str2 = RemoveInvalidXmlCharacters(text);
            this.writer.WriteAttributeString(attribute, str2);
        }

        public void WriteEndElement()
        {
            this.writer.WriteEndElement();
        }

        public void WriteNamespace(string prefix, string value)
        {
            this.writer.WriteAttributeString("xmlns", prefix, null, value);
        }

        public void WriteRaw(string data)
        {
            this.writer.WriteRaw(data);
        }

        public void WriteSetter(string attribute, Color value)
        {
            this.WriteSetterCore(attribute, "#{0:X8}", value.ToArgb());
        }

        public void WriteSetter(string attribute, float value)
        {
            this.WriteSetterCore(attribute, "{0}", value);
        }

        public void WriteSetter(string attribute, string value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.WriteSetterCore(attribute, "{0}", value);
        }

        public void WriteSetter(string attribute, Thickness value)
        {
            float[] values = new float[] { (float) value.Left, (float) value.Top, (float) value.Right, (float) value.Bottom };
            this.WriteSetter(attribute, values);
        }

        public void WriteSetter(string attribute, params float[] values)
        {
            Guard.ArgumentNotNull(values, "values");
            string str = JoinItemsToString<float>(values);
            this.WriteSetterCore(attribute, "{0}", str);
        }

        private void WriteSetterCore(string attribute, string format, object value)
        {
            this.WriteStartElement(XamlTag.Setter);
            this.WriteAttribute(XamlAttribute.Property, attribute);
            this.WriteAttributeCore(XamlAttribute.Value, format, value);
            this.WriteEndElement();
        }

        public void WriteStartElement(string tag)
        {
            this.writer.WriteStartElement(tag);
        }

        public void WriteStartElement(string tag, string ns)
        {
            this.writer.WriteStartElement(tag, ns);
        }

        public void WriteStartElement(string prefix, string tag, string ns)
        {
            this.writer.WriteStartElement(prefix, tag, ns);
        }

        public void WriteValue(string value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.writer.WriteValue(value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XamlWriter.<>c <>9 = new XamlWriter.<>c();
            public static Func<char, bool> <>9__32_0;

            internal bool <RemoveInvalidXmlCharacters>b__32_0(char ch) => 
                XmlConvert.IsXmlChar(ch);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__31<T> where T: IConvertible
        {
            public static readonly XamlWriter.<>c__31<T> <>9;
            public static Func<T, string> <>9__31_0;

            static <>c__31()
            {
                XamlWriter.<>c__31<T>.<>9 = new XamlWriter.<>c__31<T>();
            }

            internal string <JoinItemsToString>b__31_0(T value) => 
                value.ToString(CultureInfo.InvariantCulture);
        }
    }
}

