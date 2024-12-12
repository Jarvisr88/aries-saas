namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PdfWriterDictionary : PdfDictionary, IPdfWritableObject
    {
        private readonly PdfObjectCollection objects;

        public PdfWriterDictionary(PdfObjectCollection objects)
        {
            this.objects = objects;
        }

        public void Add(string key, PdfObject value)
        {
            this.Add(key, value, null);
        }

        public void Add(string key, PdfStream value)
        {
            this.AddReference(key, value);
        }

        public void Add(string key, PdfWriterStream value)
        {
            this.AddReference(key, value);
        }

        public void Add(string key, PdfColor color)
        {
            if (color != null)
            {
                base.Add(key, color.ToWritableObject());
            }
        }

        public void Add(string key, PdfRectangle value)
        {
            if (value != null)
            {
                base.Add(key, value.ToWritableObject());
            }
        }

        public void Add(string key, IEnumerable<double> value)
        {
            if (value != null)
            {
                base.Add(key, new PdfWritableDoubleArray(value));
            }
        }

        public void Add(string key, PdfObject value, PdfObject defaultValue)
        {
            if ((value != null) && !value.Equals(defaultValue))
            {
                base.Add(key, this.objects.AddObject(value));
            }
        }

        public void Add(string key, PdfRectangle value, PdfRectangle defaultValue)
        {
            if ((value != null) && !value.Equals(defaultValue))
            {
                base.Add(key, value.ToWritableObject());
            }
        }

        public void Add(string key, object value, object defaultValue)
        {
            if (((value == null) && (defaultValue != null)) || ((value != null) && !value.Equals(defaultValue)))
            {
                base.Add(key, value);
            }
        }

        public void AddASCIIString(string key, string value)
        {
            if (value != null)
            {
                base.Add(key, EncodingHelpers.AnsiEncoding.GetBytes(value));
            }
        }

        public void AddEnumerable<T>(string key, IEnumerable<T> value, Func<T, object> converter)
        {
            if (value != null)
            {
                base.Add(key, new PdfWritableConvertibleArray<T>(value, converter));
            }
        }

        public void AddEnumName<T>(string key, T value) where T: struct
        {
            this.AddEnumName<T>(key, value, true);
        }

        public void AddEnumName<T>(string key, T value, bool useDefaultValue) where T: struct
        {
            this.AddName(key, PdfEnumToStringConverter.Convert<T>(value, useDefaultValue));
        }

        public void AddIfPresent(string key, object value)
        {
            if (value != null)
            {
                base.Add(key, value);
            }
        }

        public void AddIntent(string key, PdfOptionalContentIntent value)
        {
            if (value != PdfOptionalContentIntent.All)
            {
                this.AddEnumName<PdfOptionalContentIntent>(key, value);
            }
            else
            {
                PdfOptionalContentIntent[] intentArray1 = new PdfOptionalContentIntent[] { PdfOptionalContentIntent.View, PdfOptionalContentIntent.Design };
                Func<PdfOptionalContentIntent, object> converter = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<PdfOptionalContentIntent, object> local1 = <>c.<>9__19_0;
                    converter = <>c.<>9__19_0 = v => new PdfName(PdfEnumToStringConverter.Convert<PdfOptionalContentIntent>(v, false));
                }
                this.AddList<PdfOptionalContentIntent>(key, intentArray1, converter);
            }
        }

        public void AddLanguage(CultureInfo culture)
        {
            if (!ReferenceEquals(culture, CultureInfo.InvariantCulture))
            {
                base.Add("Lang", PdfDocumentStream.ConvertToArray(culture.Name));
            }
        }

        public void AddList<T>(string key, IList<T> value) where T: PdfObject
        {
            if ((value != null) && (value.Count > 0))
            {
                this.AddRequiredList<T>(key, value);
            }
        }

        public void AddList<T>(string key, IList<T> value, Func<T, object> converter)
        {
            if ((value != null) && (value.Count > 0))
            {
                base.Add(key, new PdfWritableConvertibleArray<T>(value, converter));
            }
        }

        public void AddName(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                base.Add(key, new PdfName(value));
            }
        }

        public void AddName(string key, string value, string defaultValue)
        {
            if (value != defaultValue)
            {
                this.AddName(key, value);
            }
        }

        public void AddNotNullOrEmptyString(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                base.Add(key, value);
            }
        }

        public void AddNullable<T>(string key, T? value) where T: struct
        {
            if (value != null)
            {
                base.Add(key, value.Value);
            }
        }

        public void AddPDFDocEncodedString(string key, string value)
        {
            if (value != null)
            {
                base.Add(key, PdfDocEncoding.GetBytes(value));
            }
        }

        private void AddReference(string key, PdfStream value)
        {
            if ((value != null) && (this.objects != null))
            {
                base.Add(key, this.objects.AddStream(value));
            }
        }

        public void AddRequiredList<T>(string key, IList<T> value) where T: PdfObject
        {
            base.Add(key, new PdfWritableObjectArray((IEnumerable<PdfObject>) value, this.objects));
        }

        public void AddStream(string key, string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                base.Add(key, this.objects.AddStream(Encoding.UTF8.GetBytes(data)));
            }
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            stream.WriteOpenDictionary();
            foreach (KeyValuePair<string, object> pair in this)
            {
                if (pair.Value is PdfStream)
                {
                    throw new InvalidOperationException();
                }
                ((IPdfWritableObject) new PdfName(pair.Key)).Write(stream, number);
                stream.WriteSpace();
                stream.WriteObject(pair.Value, number);
                stream.WriteSpace();
            }
            stream.WriteCloseDictionary();
        }

        public PdfObjectCollection Objects =>
            this.objects;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfWriterDictionary.<>c <>9 = new PdfWriterDictionary.<>c();
            public static Func<PdfOptionalContentIntent, object> <>9__19_0;

            internal object <AddIntent>b__19_0(PdfOptionalContentIntent v) => 
                new PdfName(PdfEnumToStringConverter.Convert<PdfOptionalContentIntent>(v, false));
        }
    }
}

