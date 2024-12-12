namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class PdfCompositeFontDescriptor : PdfFontDescriptor
    {
        private const string styleDictionaryKey = "Style";
        private const string fontDescriptorsDictionaryKey = "FD";
        internal const string PanoseDictionaryKey = "Panose";
        private readonly PdfFontFamilyClass fontFamilyClass;
        private readonly PdfPanose panose;
        private readonly CultureInfo languageCulture;
        private readonly IDictionary<string, PdfFontDescriptor> fontDescriptors;

        internal PdfCompositeFontDescriptor(IPdfFontDescriptorBuilder fontDescriptorBuilder) : base(fontDescriptorBuilder)
        {
            this.languageCulture = CultureInfo.InvariantCulture;
        }

        internal PdfCompositeFontDescriptor(PdfFontDescriptorData fontDescriptorData) : base(fontDescriptorData)
        {
            this.languageCulture = CultureInfo.InvariantCulture;
        }

        internal PdfCompositeFontDescriptor(PdfType0Font font, PdfReaderDictionary dictionary) : base(font, dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Style");
            if (dictionary2 != null)
            {
                byte[] bytes = dictionary2.GetBytes("Panose");
                if (bytes.Length == 12)
                {
                    using (PdfBinaryStream stream = new PdfBinaryStream(bytes))
                    {
                        this.fontFamilyClass = (PdfFontFamilyClass) stream.ReadShort();
                        this.panose = new PdfPanose(stream);
                    }
                }
            }
            this.languageCulture = PdfReaderDictionary.ConvertToCultureInfo(dictionary.GetName("Lang"));
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("FD");
            if (dictionary3 != null)
            {
                this.fontDescriptors = new Dictionary<string, PdfFontDescriptor>(dictionary3.Count);
                PdfObjectCollection objects = dictionary.Objects;
                foreach (KeyValuePair<string, object> pair in dictionary3)
                {
                    PdfReaderDictionary dictionary4 = objects.TryResolve(pair.Value, null) as PdfReaderDictionary;
                    if (dictionary4 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.fontDescriptors.Add(pair.Key, new PdfFontDescriptor(font, dictionary4));
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            if (this.fontFamilyClass == PdfFontFamilyClass.NoClassification)
            {
                PdfPanose panose = this.panose;
                if (panose.IsDefault)
                {
                    goto TR_000F;
                }
            }
            using (PdfBinaryStream stream = new PdfBinaryStream())
            {
                stream.WriteShort((short) this.fontFamilyClass);
                this.panose.Write(stream);
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                dictionary2.Add("Panose", stream.Data);
                dictionary.Add("Style", dictionary2);
            }
        TR_000F:
            if (!ReferenceEquals(this.languageCulture, CultureInfo.InvariantCulture))
            {
                dictionary.AddName("Lang", this.languageCulture.Name);
            }
            if (this.fontDescriptors != null)
            {
                PdfWriterDictionary dictionary3 = new PdfWriterDictionary(objects);
                foreach (KeyValuePair<string, PdfFontDescriptor> pair in this.fontDescriptors)
                {
                    dictionary3.Add(pair.Key, pair.Value);
                }
                dictionary.Add("FD", dictionary3);
            }
            return dictionary;
        }

        public PdfFontFamilyClass FontFamilyClass =>
            this.fontFamilyClass;

        public PdfPanose Panose =>
            this.panose;

        public CultureInfo LanguageCulture =>
            this.languageCulture;

        public IDictionary<string, PdfFontDescriptor> FontDescriptors =>
            this.fontDescriptors;
    }
}

