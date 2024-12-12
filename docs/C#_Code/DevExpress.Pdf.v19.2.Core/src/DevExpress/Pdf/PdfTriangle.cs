namespace DevExpress.Pdf
{
    using System;

    public class PdfTriangle
    {
        private readonly PdfVertex vertex1;
        private readonly PdfVertex vertex2;
        private readonly PdfVertex vertex3;

        internal PdfTriangle(PdfVertex vertex1, PdfVertex vertex2, PdfVertex vertex3)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.vertex3 = vertex3;
        }

        public PdfVertex Vertex1 =>
            this.vertex1;

        public PdfVertex Vertex2 =>
            this.vertex2;

        public PdfVertex Vertex3 =>
            this.vertex3;
    }
}

