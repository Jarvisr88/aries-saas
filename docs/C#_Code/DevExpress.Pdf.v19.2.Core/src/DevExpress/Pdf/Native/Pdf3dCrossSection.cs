namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class Pdf3dCrossSection : PdfObject
    {
        private readonly IList<object> cuttingPlaneCenterOfRotation;
        private readonly IList<object> cuttingPlaneOrientation;
        private readonly double cuttingPlaneOpacity;
        private readonly IList<object> cuttingPlaneColor;
        private readonly bool intersectionVisibility;
        private readonly IList<object> intersectionColor;

        public Pdf3dCrossSection(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.cuttingPlaneCenterOfRotation = dictionary.GetArray("C");
            this.cuttingPlaneOrientation = dictionary.GetArray("O");
            double? number = dictionary.GetNumber("PO");
            this.cuttingPlaneOpacity = (number != null) ? number.GetValueOrDefault() : 0.5;
            this.cuttingPlaneColor = dictionary.GetArray("PC");
            bool? boolean = dictionary.GetBoolean("IV");
            this.intersectionVisibility = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.intersectionColor = dictionary.GetArray("IC");
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DCrossSection");
            dictionary.AddIfPresent("C", this.cuttingPlaneCenterOfRotation);
            dictionary.AddIfPresent("O", this.cuttingPlaneOrientation);
            dictionary.Add("PO", this.cuttingPlaneOpacity, 0.5);
            dictionary.AddIfPresent("PC", this.cuttingPlaneColor);
            dictionary.Add("IV", this.intersectionVisibility, false);
            dictionary.AddIfPresent("IC", this.intersectionColor);
            return dictionary;
        }

        public IList<object> CuttingPlaneCenterOfRotation =>
            this.cuttingPlaneCenterOfRotation;

        public IList<object> CuttingPlaneOrientation =>
            this.cuttingPlaneOrientation;

        public double CuttingPlaneOpacity =>
            this.cuttingPlaneOpacity;

        public IList<object> CuttingPlaneColor =>
            this.cuttingPlaneColor;

        public bool IntersectionVisibility =>
            this.intersectionVisibility;

        public IList<object> IntersectionColor =>
            this.intersectionColor;
    }
}

