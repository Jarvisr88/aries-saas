namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfOpenPrepressInterface : PdfObject
    {
        private const string dictionaryType = "OPI";
        private const string version13DictionaryKey = "1.3";
        private const string version20DictionaryKey = "2.0";
        private const string versionDictionaryKey = "Version";
        private const string fileSpecificationDictionaryKey = "F";
        private const string sizeDictionaryKey = "Size";
        private const string cropRectDictionaryKey = "CropRect";
        private const string overprintDictionaryKey = "Overprint";
        private const string cropFixedDictionaryKey = "CropFixed";
        private const string positionDictionaryKey = "Position";
        private const string resolutionDictionaryKey = "Resolution";
        private const string tintDictionaryKey = "Tint";
        private const string imageTypeDictionaryKey = "ImageType";
        private const string transparencyDictionaryKey = "Transparency";
        private const string inksDictionaryKey = "Inks";
        private const string includedImageQualityDictionaryKey = "IncludedImageQuality";
        private const string monochromeInkName = "monochrome";
        private readonly double version;
        private readonly PdfFileSpecification fileSpecification;
        private readonly double width;
        private readonly double height;
        private readonly PdfRectangle cropRect;
        private readonly bool overprint;
        private readonly PdfRectangle cropFixed;
        private readonly PdfParallelogram position;
        private readonly double horizontalResolution;
        private readonly double verticalResolution;
        private readonly double tint;
        private readonly int samplesPerPixel;
        private readonly int bitsPerSample;
        private readonly bool transparency;
        private readonly string inksName;
        private readonly Dictionary<string, double> inks;
        private readonly PdfIncludedImageQuality includedImageQuality;

        private PdfOpenPrepressInterface(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.tint = 1.0;
            this.transparency = true;
            this.includedImageQuality = PdfIncludedImageQuality.High;
            double? number = dictionary.GetNumber("Version");
            if (number == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.version = number.Value;
            bool flag = this.version == 1.3;
            if (!flag && (this.version != 2.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "F", true);
            IList<object> array = dictionary.GetArray("Size");
            if (array == null)
            {
                if (flag)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.width = PdfDocumentReader.ConvertToDouble(array[0]);
                this.height = PdfDocumentReader.ConvertToDouble(array[1]);
            }
            this.cropRect = dictionary.GetRectangle("CropRect");
            if (ReferenceEquals(this.cropRect, null) & flag)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            bool? boolean = dictionary.GetBoolean("Overprint");
            this.overprint = (boolean != null) ? boolean.GetValueOrDefault() : false;
            if (flag)
            {
                this.cropFixed = dictionary.GetRectangle("CropFixed");
                IList<object> list2 = dictionary.GetArray("Position");
                if (list2 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.position = new PdfParallelogram(list2);
                IList<object> list3 = dictionary.GetArray("Resolution");
                if (list3 != null)
                {
                    if (list3.Count != 2)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.horizontalResolution = PdfDocumentReader.ConvertToDouble(list3[0]);
                    this.verticalResolution = PdfDocumentReader.ConvertToDouble(list3[1]);
                }
                double? nullable3 = dictionary.GetNumber("Tint");
                if (nullable3 != null)
                {
                    this.tint = nullable3.Value;
                    if ((this.tint < 0.0) || (this.tint > 1.0))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                IList<object> list4 = dictionary.GetArray("ImageType");
                if (list4 != null)
                {
                    if (list4.Count != 2)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    object obj2 = list4[0];
                    object obj3 = list4[1];
                    if (!(obj2 is int) || !(obj3 is int))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.samplesPerPixel = (int) obj2;
                    this.bitsPerSample = (int) obj3;
                }
                boolean = dictionary.GetBoolean("Transparency");
                this.transparency = (boolean != null) ? boolean.GetValueOrDefault() : true;
            }
            else
            {
                object obj4;
                if (dictionary.TryGetValue("Inks", out obj4))
                {
                    IList<object> list5 = obj4 as IList<object>;
                    if (list5 == null)
                    {
                        PdfName name = obj4 as PdfName;
                        if (name == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.inksName = name.Name;
                    }
                    else
                    {
                        int count = list5.Count;
                        if (count == 0)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        count--;
                        if ((count % 2) != 0)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        count /= 2;
                        PdfName name2 = list5[0] as PdfName;
                        if ((name2 == null) || (name2.Name != "monochrome"))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.inks = new Dictionary<string, double>(count);
                        int num2 = 0;
                        int num3 = 1;
                        while (num2 < count)
                        {
                            byte[] buffer = list5[num3++] as byte[];
                            if (buffer == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.inks.Add(PdfDocumentReader.ConvertToString(buffer), PdfDocumentReader.ConvertToDouble(list5[num3++]));
                            num2++;
                        }
                    }
                }
                this.includedImageQuality = PdfEnumToValueConverter.Parse<PdfIncludedImageQuality>(dictionary.GetInteger("IncludedImageQuality"), 2);
            }
        }

        internal static PdfOpenPrepressInterface Create(PdfReaderDictionary dictionary)
        {
            object obj2;
            if ((dictionary == null) || (dictionary.Count < 1))
            {
                return null;
            }
            if (!dictionary.TryGetValue("1.3", out obj2) && !dictionary.TryGetValue("2.0", out obj2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary2 = obj2 as PdfReaderDictionary;
            if (dictionary2 != null)
            {
                return new PdfOpenPrepressInterface(dictionary2);
            }
            Func<PdfReaderDictionary, PdfOpenPrepressInterface> create = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<PdfReaderDictionary, PdfOpenPrepressInterface> local1 = <>c.<>9__17_0;
                create = <>c.<>9__17_0 = d => new PdfOpenPrepressInterface(d);
            }
            return dictionary.Objects.GetObject<PdfOpenPrepressInterface>(obj2, create);
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Version", this.version);
            dictionary.Add("F", this.fileSpecification);
            dictionary.Add("Overprint", this.overprint, false);
            if (this.version != 2.0)
            {
                object[] objArray1 = new object[] { (int) this.width, (int) this.height };
                dictionary.Add("Size", objArray1);
                object[] objArray2 = new object[] { (int) this.cropRect.Left, (int) this.cropRect.Bottom, (int) this.cropRect.Right, (int) this.cropRect.Top };
                dictionary.Add("CropRect", objArray2);
                dictionary.Add("CropFixed", this.cropFixed);
                dictionary.Add("Position", this.position.ToWriteableObject());
                if ((this.horizontalResolution != 0.0) && (this.verticalResolution != 0.0))
                {
                    double[] numArray2 = new double[] { this.horizontalResolution, this.verticalResolution };
                    dictionary.Add("Resolution", numArray2);
                }
                dictionary.Add("Tint", this.tint, 1.0);
                if ((this.samplesPerPixel != 0) || (this.bitsPerSample != 0))
                {
                    object[] objArray3 = new object[] { this.samplesPerPixel, this.bitsPerSample };
                    dictionary.Add("ImageType", objArray3);
                }
                dictionary.Add("Transparency", this.transparency, true);
            }
            else
            {
                if ((this.width != 0.0) || (this.height != 0.0))
                {
                    double[] numArray1 = new double[] { this.width, this.height };
                    dictionary.Add("Size", numArray1);
                }
                if (this.cropRect != null)
                {
                    dictionary.Add("CropRect", this.cropRect);
                }
                if (!string.IsNullOrEmpty(this.inksName))
                {
                    dictionary.AddName("Inks", this.inksName);
                }
                else if (this.inks != null)
                {
                    IList<object> list = new List<object> {
                        new PdfName("monochrome")
                    };
                    foreach (KeyValuePair<string, double> pair in this.inks)
                    {
                        list.Add(pair.Key);
                        list.Add(pair.Value);
                    }
                    dictionary.Add("Inks", list);
                }
                dictionary.Add("IncludedImageQuality", PdfEnumToValueConverter.Convert<PdfIncludedImageQuality>(this.includedImageQuality));
            }
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.Add((this.version == 1.3) ? "1.3" : "2.0", dictionary);
            return dictionary2;
        }

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public double Width =>
            this.width;

        public double Height =>
            this.height;

        public PdfRectangle CropRect =>
            this.cropRect;

        public bool Overprint =>
            this.overprint;

        public PdfRectangle CropFixed =>
            this.cropFixed;

        public PdfParallelogram Position =>
            this.position;

        public double HorizontalResolution =>
            this.horizontalResolution;

        public double VerticalResolution =>
            this.verticalResolution;

        public double Tint =>
            this.tint;

        public int SamplesPerPixel =>
            this.samplesPerPixel;

        public int BitsPerSample =>
            this.bitsPerSample;

        public bool Transparency =>
            this.transparency;

        public string InksName =>
            this.inksName;

        public IDictionary<string, double> Inks =>
            this.inks;

        public PdfIncludedImageQuality IncludedImageQuality =>
            this.includedImageQuality;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOpenPrepressInterface.<>c <>9 = new PdfOpenPrepressInterface.<>c();
            public static Func<PdfReaderDictionary, PdfOpenPrepressInterface> <>9__17_0;

            internal PdfOpenPrepressInterface <Create>b__17_0(PdfReaderDictionary d) => 
                new PdfOpenPrepressInterface(d);
        }
    }
}

