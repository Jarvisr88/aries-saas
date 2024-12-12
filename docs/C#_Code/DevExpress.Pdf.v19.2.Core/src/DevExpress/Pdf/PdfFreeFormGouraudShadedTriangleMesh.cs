namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFreeFormGouraudShadedTriangleMesh : PdfGouraudShadedTriangleMesh
    {
        internal const int Type = 4;

        internal PdfFreeFormGouraudShadedTriangleMesh(PdfReaderStream stream) : base(stream)
        {
            using (PdfIntegerStreamReader reader = base.CreateIntegerStreamReader())
            {
                int num = base.Data.Length / reader.BytesPerVertex;
                if ((num > 0) && (num < 3))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                IList<PdfTriangle> triangles = base.Triangles;
                PdfTriangle triangle = null;
                int num2 = num;
                while (num2 > 0)
                {
                    PdfTriangle triangle2;
                    int num3 = reader.ReadEdgeFlag();
                    switch (num3)
                    {
                        case 0:
                            if (num2 < 3)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            reader.ReadEdgeFlag();
                            reader.ReadEdgeFlag();
                            triangle2 = new PdfTriangle(reader.ReadVertex(), reader.ReadVertex(), reader.ReadVertex());
                            num2 -= 3;
                            break;

                        case 1:
                            if (triangle == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            triangle2 = new PdfTriangle(triangle.Vertex2, triangle.Vertex3, reader.ReadVertex());
                            num2--;
                            break;

                        case 2:
                            if (triangle == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            triangle2 = new PdfTriangle(triangle.Vertex1, triangle.Vertex3, reader.ReadVertex());
                            num2--;
                            break;

                        default:
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                            triangle2 = null;
                            break;
                    }
                    triangles.Add(triangle2);
                    triangle = triangle2;
                }
            }
        }

        internal PdfFreeFormGouraudShadedTriangleMesh(PdfObjectList<PdfCustomFunction> functions, int bitsPerFlag, int bitsPerCoordinate, int bitsPerComponent, PdfDecodeRange decodeX, PdfDecodeRange decodeY, PdfDecodeRange[] decodeC, IList<PdfTriangle> triangles) : base(functions, bitsPerFlag, bitsPerCoordinate, bitsPerComponent, decodeX, decodeY, decodeC, triangles)
        {
        }

        protected override int ShadingType =>
            4;

        protected override bool HasBitsPerFlag =>
            true;
    }
}

