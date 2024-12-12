namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfGouraudShadedTriangleMesh : PdfMeshShading
    {
        private readonly IList<PdfTriangle> triangles;

        protected PdfGouraudShadedTriangleMesh(PdfReaderStream stream) : base(stream)
        {
            this.triangles = new List<PdfTriangle>();
        }

        protected PdfGouraudShadedTriangleMesh(PdfObjectList<PdfCustomFunction> functions, int bitsPerFlag, int bitsPerCoordinate, int bitsPerComponent, PdfDecodeRange decodeX, PdfDecodeRange decodeY, PdfDecodeRange[] decodeC, IList<PdfTriangle> triangles) : base(functions, bitsPerFlag, bitsPerCoordinate, bitsPerComponent, decodeX, decodeY, decodeC)
        {
            this.triangles = new List<PdfTriangle>();
            this.triangles = triangles;
        }

        protected override byte[] GetData() => 
            base.GetData() ?? PdfGouraudShadedTriangleMeshWriter.Write(this);

        public IList<PdfTriangle> Triangles =>
            this.triangles;
    }
}

