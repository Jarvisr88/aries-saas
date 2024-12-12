namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class Pdf3dRenderMode : PdfObject
    {
        private readonly Pdf3dRenderModeType type;
        private readonly IList<object> auxiliaryColor;
        private readonly object faceColor;
        private readonly double opacity;
        private readonly double creaseValue;

        public Pdf3dRenderMode(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.type = dictionary.GetEnumName<Pdf3dRenderModeType>("Subtype");
            this.auxiliaryColor = dictionary.GetArray("AC");
            if (dictionary.TryGetValue("FC", out this.faceColor))
            {
                this.faceColor = dictionary.Objects.TryResolve(this.faceColor, null);
            }
            double? number = dictionary.GetNumber("O");
            this.opacity = (number != null) ? number.GetValueOrDefault() : 0.5;
            number = dictionary.GetNumber("CV");
            this.creaseValue = (number != null) ? number.GetValueOrDefault() : 45.0;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DRenderMode");
            dictionary.AddEnumName<Pdf3dRenderModeType>("Subtype", this.type);
            dictionary.AddIfPresent("AC", this.auxiliaryColor);
            dictionary.AddIfPresent("FC", this.faceColor);
            dictionary.Add("O", this.opacity, 0.5);
            dictionary.Add("CV", this.creaseValue, 45.0);
            return dictionary;
        }

        public Pdf3dRenderModeType Type =>
            this.type;

        public IList<object> AuxiliaryColor =>
            this.auxiliaryColor;

        public object FaceColor =>
            this.faceColor;

        public double Opacity =>
            this.opacity;

        public double CreaseValue =>
            this.creaseValue;
    }
}

