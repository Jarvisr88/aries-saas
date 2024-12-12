namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class Pdf3dNode : PdfObject
    {
        private readonly string name;
        private readonly double? opacity;
        private readonly bool? visible;
        private readonly IList<object> matrix;

        public Pdf3dNode(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.name = dictionary.GetTextString("N");
            this.opacity = dictionary.GetNumber("O");
            this.visible = dictionary.GetBoolean("V");
            this.matrix = dictionary.GetArray("M");
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DNode");
            dictionary.AddIfPresent("N", this.name);
            dictionary.AddIfPresent("O", this.opacity);
            dictionary.AddIfPresent("V", this.visible);
            dictionary.AddIfPresent("M", this.matrix);
            return dictionary;
        }

        public string Name =>
            this.name;

        public double? Opacity =>
            this.opacity;

        public bool? Visible =>
            this.visible;

        public IList<object> Matrix =>
            this.matrix;
    }
}

