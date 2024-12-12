namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PdfFormData
    {
        private static readonly char[] fieldNameDelimiter = new char[] { '.' };
        private readonly ValueWrapper valueWrapper;
        private bool allowAddNewKids;
        private string name;
        private Dictionary<string, PdfFormData> kids;

        public PdfFormData()
        {
            this.allowAddNewKids = true;
            this.valueWrapper = new DetachedValueWrapper();
        }

        internal PdfFormData(bool allowAddNewKids)
        {
            this.allowAddNewKids = allowAddNewKids;
        }

        public PdfFormData(Stream stream) : this(stream, Detect(stream))
        {
        }

        public PdfFormData(string fileName) : this()
        {
            using (Stream stream = File.OpenRead(fileName))
            {
                PdfFormDataReader.Load(this, stream, Detect(stream));
            }
        }

        internal PdfFormData(PdfInteractiveFormField formField, IPdfExportFontProvider fontSearch)
        {
            this.valueWrapper = new FormFieldValueWrapper(formField, fontSearch);
            this.name = formField.Name;
            this.allowAddNewKids = false;
        }

        public PdfFormData(Stream stream, PdfFormDataFormat format) : this()
        {
            PdfFormDataReader.Load(this, stream, format);
        }

        public PdfFormData(string fileName, PdfFormDataFormat format) : this()
        {
            using (Stream stream = File.OpenRead(fileName))
            {
                PdfFormDataReader.Load(this, stream, format);
            }
        }

        internal void AddKid(string name, PdfFormData kid)
        {
            this.kids ??= new Dictionary<string, PdfFormData>();
            this.kids[name] = kid;
        }

        internal void Apply(PdfFormData data)
        {
            if ((data == null) || (data.Name != this.Name))
            {
                throw new ArgumentNullException("data");
            }
            IEnumerable<PdfFormData> enumerable = data.Value as IEnumerable<PdfFormData>;
            if (enumerable == null)
            {
                this.Value = data.Value;
            }
            else if (this.kids != null)
            {
                foreach (PdfFormData data2 in enumerable)
                {
                    PdfFormData data3;
                    if (this.kids.TryGetValue(data2.Name, out data3))
                    {
                        data3.Apply(data2);
                    }
                }
            }
        }

        private static PdfFormDataFormat Detect(Stream stream)
        {
            string str = PdfDocumentStream.ReadString(stream).Replace("\x00ef\x00bb\x00bf", "");
            stream.Position = 0L;
            return (!str.StartsWith("%FDF-") ? (!str.StartsWith("<?xml") ? PdfFormDataFormat.Txt : PdfFormDataFormat.Xml) : PdfFormDataFormat.Fdf);
        }

        public IList<string> GetFieldNames()
        {
            IList<string> list = new List<string>();
            if (this.kids != null)
            {
                foreach (PdfFormData data in this.kids.Values)
                {
                    if (data.kids == null)
                    {
                        list.Add(data.name);
                        continue;
                    }
                    foreach (string str in data.GetFieldNames())
                    {
                        list.Add(data.name + "." + str);
                    }
                }
            }
            return list;
        }

        internal void Reset()
        {
            if (this.valueWrapper == null)
            {
                ValueWrapper valueWrapper = this.valueWrapper;
            }
            else
            {
                this.valueWrapper.ResetValue();
            }
            if (this.kids != null)
            {
                foreach (PdfFormData data in this.kids.Values)
                {
                    data.Reset();
                }
            }
        }

        public void Save(Stream stream, PdfFormDataFormat format)
        {
            PdfFormDataWriter.Save(this, stream, format);
        }

        public void Save(string fileName, PdfFormDataFormat format)
        {
            using (Stream stream = File.Create(fileName))
            {
                this.Save(stream, format);
            }
        }

        internal static char[] FieldNameDelimiter =>
            fieldNameDelimiter;

        internal bool AllowAddNewKids
        {
            get => 
                this.allowAddNewKids;
            set => 
                this.allowAddNewKids = value;
        }

        public string Name =>
            this.name;

        public object Value
        {
            get
            {
                if (this.kids != null)
                {
                    return this.kids.Values;
                }
                if (this.valueWrapper != null)
                {
                    return this.valueWrapper.Value;
                }
                ValueWrapper valueWrapper = this.valueWrapper;
                return null;
            }
            set
            {
                if (this.valueWrapper != null)
                {
                    this.valueWrapper.Value = value;
                }
            }
        }

        public PdfFormData this[string name]
        {
            get
            {
                PdfFormData data;
                this.kids ??= new Dictionary<string, PdfFormData>();
                if (this.kids.TryGetValue(name, out data))
                {
                    return data;
                }
                string[] strArray = name.Split(fieldNameDelimiter, 2);
                if (strArray.Length > 1)
                {
                    return this[strArray[0]][strArray[1]];
                }
                PdfFormData data2 = new PdfFormData();
                this[name] = data2;
                data2.name = name;
                return data2;
            }
            set
            {
                if (!this.allowAddNewKids)
                {
                    throw new KeyNotFoundException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgFormDataNotFound));
                }
                this.kids ??= new Dictionary<string, PdfFormData>();
                if (value == null)
                {
                    this.kids.Remove(name);
                }
                else
                {
                    this.kids[name] = value;
                    value.name = name;
                }
            }
        }

        internal IDictionary<string, PdfFormData> Kids =>
            this.kids;

        internal bool IsPasswordFormField
        {
            get
            {
                if (this.valueWrapper != null)
                {
                    return this.valueWrapper.IsPasswordFormField;
                }
                ValueWrapper valueWrapper = this.valueWrapper;
                return false;
            }
        }

        private class DetachedValueWrapper : PdfFormData.ValueWrapper
        {
            public override void ResetValue()
            {
            }

            public override object Value { get; set; }

            public override bool IsPasswordFormField =>
                false;
        }

        private class FormFieldValueWrapper : PdfFormData.ValueWrapper
        {
            private readonly PdfInteractiveFormField formField;
            private readonly IPdfExportFontProvider fontSearch;

            public FormFieldValueWrapper(PdfInteractiveFormField formField, IPdfExportFontProvider fontSearch)
            {
                this.formField = formField;
                this.fontSearch = fontSearch;
            }

            public override void ResetValue()
            {
                this.Value = this.formField.DefaultValue;
            }

            public override object Value
            {
                get => 
                    this.formField.Value;
                set => 
                    this.formField.SetExportValue(value, this.fontSearch);
            }

            public override bool IsPasswordFormField =>
                this.formField.Flags.HasFlag(PdfInteractiveFormFieldFlags.Password);
        }

        private abstract class ValueWrapper
        {
            protected ValueWrapper()
            {
            }

            public abstract void ResetValue();

            public abstract object Value { get; set; }

            public abstract bool IsPasswordFormField { get; }
        }
    }
}

