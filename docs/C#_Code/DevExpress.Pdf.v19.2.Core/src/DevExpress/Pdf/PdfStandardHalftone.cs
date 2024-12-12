namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfStandardHalftone : PdfHalftone
    {
        internal const int Number = 1;
        private const string frequencyDictionaryKey = "Frequency";
        private const string angleDictionaryKey = "Angle";
        private const string spotFunctionDictionaryKey = "SpotFunction";
        private const string accurateScreensDictionaryKey = "AccurateScreens";
        private const string transferFunctionDictionaryKey = "TransferFunction";
        private readonly double frequency;
        private readonly double angle;
        private readonly PdfFunction spotFunction;
        private readonly bool accurateScreens;
        private readonly PdfFunction transferFunction;

        internal PdfStandardHalftone(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj3;
            PdfObjectCollection objects = dictionary.Objects;
            double? number = dictionary.GetNumber("Frequency");
            double? nullable2 = dictionary.GetNumber("Angle");
            object obj2 = null;
            if ((number == null) || ((nullable2 == null) || !dictionary.TryGetValue("SpotFunction", out obj2)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfName name = obj2 as PdfName;
            this.spotFunction = (name == null) ? PdfFunction.Parse(objects, obj2, false) : new PdfPredefinedSpotFunction(name.Name);
            this.frequency = number.Value;
            this.angle = nullable2.Value;
            if (this.frequency <= 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            bool? boolean = dictionary.GetBoolean("AccurateScreens");
            this.accurateScreens = (boolean != null) ? boolean.GetValueOrDefault() : false;
            if (dictionary.TryGetValue("TransferFunction", out obj3))
            {
                this.transferFunction = PdfFunction.Parse(objects, obj3, false);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("HalftoneType", 1);
            dictionary.Add("Frequency", this.frequency);
            dictionary.Add("Angle", this.angle);
            dictionary.Add("SpotFunction", this.spotFunction.Write(objects));
            dictionary.Add("AccurateScreens", this.accurateScreens, false);
            if (this.transferFunction != null)
            {
                dictionary.Add("TransferFunction", this.transferFunction.Write(objects));
            }
            return dictionary;
        }

        protected internal override bool IsSame(PdfHalftone halftone)
        {
            PdfStandardHalftone halftone2 = halftone as PdfStandardHalftone;
            return ((halftone2 != null) && (base.IsSame(halftone) && ((this.frequency == halftone2.frequency) && ((this.angle == halftone2.angle) && (this.spotFunction.IsSame(halftone2.spotFunction) && ((this.accurateScreens == halftone2.accurateScreens) && ((this.transferFunction == null) ? ReferenceEquals(halftone2.transferFunction, null) : this.transferFunction.IsSame(halftone2.transferFunction))))))));
        }

        public double Frequency =>
            this.frequency;

        public double Angle =>
            this.angle;

        public PdfFunction SpotFunction =>
            this.spotFunction;

        public bool AccurateScreens =>
            this.accurateScreens;

        public PdfFunction TransferFunction =>
            this.transferFunction;
    }
}

