namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class Pdf3dGeometryMeasurementDataContainer
    {
        private readonly IList<object> annotationPlane;
        private readonly IList<object> anchor2;
        private readonly string anchor2ModelName;
        private readonly IList<object> textUpDirection;
        private readonly double? measurementValue;
        private readonly int precision;

        public Pdf3dGeometryMeasurementDataContainer(PdfReaderDictionary dictionary)
        {
            this.annotationPlane = dictionary.GetArray("AP");
            this.anchor2 = dictionary.GetArray("A2");
            this.anchor2ModelName = dictionary.GetString("N2");
            this.textUpDirection = dictionary.GetArray("TY");
            this.measurementValue = dictionary.GetNumber("V");
            int? integer = dictionary.GetInteger("P");
            this.precision = (integer != null) ? integer.GetValueOrDefault() : 3;
        }

        public void FillDictionary(PdfWriterDictionary dictionary)
        {
            dictionary.AddIfPresent("AP", this.annotationPlane);
            dictionary.AddIfPresent("A2", this.anchor2);
            dictionary.AddIfPresent("N2", this.anchor2ModelName);
            dictionary.AddIfPresent("TY", this.textUpDirection);
            dictionary.AddIfPresent("V", this.measurementValue);
            dictionary.Add("P", this.precision, 3);
        }

        public IList<object> AnnotationPlane =>
            this.annotationPlane;

        public IList<object> Anchor2 =>
            this.anchor2;

        public string Anchor2ModelName =>
            this.anchor2ModelName;

        public IList<object> TextUpDirection =>
            this.textUpDirection;

        public double? MeasurementValue =>
            this.measurementValue;

        public int Precision =>
            this.precision;
    }
}

