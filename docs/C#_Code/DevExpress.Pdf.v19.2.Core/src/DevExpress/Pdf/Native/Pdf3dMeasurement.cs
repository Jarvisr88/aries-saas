namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class Pdf3dMeasurement : PdfObject
    {
        private readonly string name;
        private readonly Pdf3dMeasurementDataContainer dataContainer;
        private PdfAnnotation annotation;
        private PdfReaderDictionary dictionary;
        private PdfPage page;

        protected Pdf3dMeasurement(PdfPage page, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.name = dictionary.GetString("TRL");
            this.dataContainer = new Pdf3dMeasurementDataContainer(dictionary);
            this.dictionary = dictionary;
            this.page = page;
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DMeasure");
            dictionary.AddEnumName<Pdf3dMeasurementType>("Subtype", this.Type);
            dictionary.AddIfPresent("TRL", this.name);
            this.dataContainer.FillDictionary(dictionary);
            dictionary.Add("S", this.Annotation);
            return dictionary;
        }

        public static Pdf3dMeasurement Parse(PdfPage page, PdfReaderDictionary dictionary)
        {
            switch (dictionary.GetEnumName<Pdf3dMeasurementType>("Subtype"))
            {
                case Pdf3dMeasurementType.LinearDimension:
                    return new Pdf3dLinearDimensionMeasurement(page, dictionary);

                case Pdf3dMeasurementType.PerpendicularDimension:
                    return new Pdf3dPerpendicularDimensionMeasurement(page, dictionary);

                case Pdf3dMeasurementType.AngularDimension:
                    return new Pdf3dAngularDimensionMeasurement(page, dictionary);

                case Pdf3dMeasurementType.RadialDimension:
                    return new Pdf3dRadialDimensionMeasurement(page, dictionary);

                case Pdf3dMeasurementType.Comment:
                    return new Pdf3dCommentMarkup(page, dictionary);
            }
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        public abstract Pdf3dMeasurementType Type { get; }

        protected Pdf3dMeasurementDataContainer DataContainer =>
            this.dataContainer;

        private PdfAnnotation Annotation
        {
            get
            {
                if (this.dictionary != null)
                {
                    this.annotation = this.dictionary.GetObject<PdfAnnotation>("S", value => PdfAnnotation.Parse(this.page, value));
                    this.dictionary = null;
                    this.page = null;
                }
                return this.annotation;
            }
        }
    }
}

