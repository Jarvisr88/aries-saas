namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class EmfPlusTransformMatrix
    {
        public EmfPlusTransformMatrix(MetaReader reader)
        {
            this.Elements = reader.ReadSingleArray(6);
        }

        public float[] Elements { get; set; }

        public float M11 =>
            this.Elements[0];

        public float M12 =>
            this.Elements[1];

        public float M21 =>
            this.Elements[2];

        public float M22 =>
            this.Elements[3];

        public float Dx =>
            this.Elements[4];

        public float Dy =>
            this.Elements[5];
    }
}

