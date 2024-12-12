namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class Pdf3dPerpendicularDimensionMeasurement : Pdf3dMeasurement
    {
        private readonly Pdf3dGeometryMeasurementDataContainer geometryDataContainer;
        private readonly IList<object> leaderLinesDirection;
        private readonly string anchor1Name;
        private readonly string units;

        public Pdf3dPerpendicularDimensionMeasurement(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.geometryDataContainer = new Pdf3dGeometryMeasurementDataContainer(dictionary);
            this.leaderLinesDirection = dictionary.GetArray("D1");
            this.anchor1Name = dictionary.GetString("N1");
            this.units = dictionary.GetString("U");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            this.geometryDataContainer.FillDictionary(dictionary);
            dictionary.AddIfPresent("D1", this.leaderLinesDirection);
            dictionary.AddIfPresent("N1", this.anchor1Name);
            dictionary.AddIfPresent("U", this.units);
            return dictionary;
        }

        public override Pdf3dMeasurementType Type =>
            Pdf3dMeasurementType.PerpendicularDimension;
    }
}

