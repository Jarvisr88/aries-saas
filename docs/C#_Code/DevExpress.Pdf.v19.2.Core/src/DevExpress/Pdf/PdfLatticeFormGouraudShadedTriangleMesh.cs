namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLatticeFormGouraudShadedTriangleMesh : PdfGouraudShadedTriangleMesh
    {
        internal const int Type = 5;
        private const string verticesPerRowDictionaryKey = "VerticesPerRow";
        private readonly int verticesPerRow;

        internal PdfLatticeFormGouraudShadedTriangleMesh(PdfReaderStream stream) : base(stream)
        {
            int? integer = stream.Dictionary.GetInteger("VerticesPerRow");
            if ((integer == null) || (integer.Value < 2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.verticesPerRow = integer.Value;
            using (PdfIntegerStreamReader reader = base.CreateIntegerStreamReader())
            {
                int num = reader.BytesPerVertex * this.verticesPerRow;
                int length = base.Data.Length;
                int num3 = length / num;
                if ((num3 == 1) || ((length % num) > 0))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (num3 != 0)
                {
                    IList<PdfTriangle> triangles = base.Triangles;
                    PdfVertex[] vertexArray = new PdfVertex[this.verticesPerRow];
                    int index = 0;
                    while (true)
                    {
                        if (index >= this.verticesPerRow)
                        {
                            int num5 = 1;
                            while (num5 < num3)
                            {
                                PdfVertex vertex = vertexArray[0];
                                PdfVertex vertex2 = reader.ReadVertex();
                                vertexArray[0] = vertex2;
                                int num6 = 1;
                                while (true)
                                {
                                    if (num6 >= this.verticesPerRow)
                                    {
                                        num5++;
                                        break;
                                    }
                                    PdfVertex vertex3 = vertexArray[num6];
                                    PdfVertex vertex4 = reader.ReadVertex();
                                    vertexArray[num6] = vertex4;
                                    triangles.Add(new PdfTriangle(vertex, vertex3, vertex2));
                                    triangles.Add(new PdfTriangle(vertex3, vertex2, vertex4));
                                    vertex2 = vertex4;
                                    vertex = vertex3;
                                    num6++;
                                }
                            }
                            break;
                        }
                        vertexArray[index] = reader.ReadVertex();
                        index++;
                    }
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("VerticesPerRow", this.verticesPerRow);
            return dictionary;
        }

        protected override int ShadingType =>
            5;
    }
}

