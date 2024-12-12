namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class Pdf3dLinearDimensionMeasurement : Pdf3dMeasurement
    {
        private readonly Pdf3dGeometryMeasurementDataContainer geometryDataContainer;
        private readonly string anchor1Name;
        private readonly string units;

        public Pdf3dLinearDimensionMeasurement(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.geometryDataContainer = new Pdf3dGeometryMeasurementDataContainer(dictionary);
            this.anchor1Name = dictionary.GetString("N1");
            this.units = dictionary.GetString("U");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            this.geometryDataContainer.FillDictionary(dictionary);
            dictionary.AddIfPresent("N1", this.anchor1Name);
            dictionary.AddIfPresent("U", this.units);
            return dictionary;
        }

        public override Pdf3dMeasurementType Type =>
            Pdf3dMeasurementType.LinearDimension;
    }
}

