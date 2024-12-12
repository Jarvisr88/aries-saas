namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class Pdf3dRadialDimensionMeasurement : Pdf3dMeasurement
    {
        private readonly Pdf3dGeometryMeasurementDataContainer geometryDataContainer;
        private readonly IList<object> horizontalTextDirection;
        private readonly IList<object> arcStart;
        private readonly IList<object> arcEnd;
        private readonly double extensionLineLength;
        private readonly bool isRadius;
        private readonly bool showCircle;
        private readonly string units;

        public Pdf3dRadialDimensionMeasurement(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.geometryDataContainer = new Pdf3dGeometryMeasurementDataContainer(dictionary);
            this.horizontalTextDirection = dictionary.GetArray("TX");
            this.arcStart = dictionary.GetArray("A3");
            this.arcEnd = dictionary.GetArray("A4");
            double? number = dictionary.GetNumber("EL");
            this.extensionLineLength = (number != null) ? number.GetValueOrDefault() : 60.0;
            bool? boolean = dictionary.GetBoolean("R");
            this.isRadius = (boolean != null) ? boolean.GetValueOrDefault() : true;
            boolean = dictionary.GetBoolean("SC");
            this.showCircle = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.units = dictionary.GetString("U");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            this.geometryDataContainer.FillDictionary(dictionary);
            dictionary.AddIfPresent("TX", this.horizontalTextDirection);
            dictionary.AddIfPresent("A3", this.arcStart);
            dictionary.AddIfPresent("A4", this.arcEnd);
            dictionary.Add("EL", this.extensionLineLength, 60.0);
            dictionary.Add("R", this.isRadius, true);
            dictionary.Add("SC", this.showCircle, false);
            dictionary.AddIfPresent("U", this.units);
            return dictionary;
        }

        public override Pdf3dMeasurementType Type =>
            Pdf3dMeasurementType.RadialDimension;
    }
}

