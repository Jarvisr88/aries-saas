namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class Pdf3dAngularDimensionMeasurement : Pdf3dMeasurement
    {
        private readonly Pdf3dGeometryMeasurementDataContainer geometryDataContainer;
        private readonly IList<object> horizontalTextDirection;
        private readonly bool isDegrees;
        private readonly IList<object> anchor1LeaderLineDirection;
        private readonly IList<object> anchor2LeaderLineDirection;
        private readonly string anchor1Name;

        public Pdf3dAngularDimensionMeasurement(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.geometryDataContainer = new Pdf3dGeometryMeasurementDataContainer(dictionary);
            this.horizontalTextDirection = dictionary.GetArray("TX");
            bool? boolean = dictionary.GetBoolean("DR");
            this.isDegrees = (boolean != null) ? boolean.GetValueOrDefault() : true;
            this.anchor1LeaderLineDirection = dictionary.GetArray("D1");
            this.anchor2LeaderLineDirection = dictionary.GetArray("D2");
            this.anchor1Name = dictionary.GetString("N1");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            this.geometryDataContainer.FillDictionary(dictionary);
            dictionary.AddIfPresent("TX", this.horizontalTextDirection);
            dictionary.Add("DR", this.isDegrees, true);
            dictionary.AddIfPresent("D1", this.anchor1LeaderLineDirection);
            dictionary.AddIfPresent("D2", this.anchor2LeaderLineDirection);
            dictionary.AddIfPresent("N1", this.anchor1Name);
            return dictionary;
        }

        public override Pdf3dMeasurementType Type =>
            Pdf3dMeasurementType.AngularDimension;
    }
}

