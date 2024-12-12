namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfCoonsPatchMesh : PdfMeshShading
    {
        internal const int Type = 6;
        private readonly IList<PdfCoonsPatch> patches;

        internal PdfCoonsPatchMesh(PdfReaderStream stream) : base(stream)
        {
            this.patches = new List<PdfCoonsPatch>();
            if (base.Data.Length != 0)
            {
                using (PdfIntegerStreamReader reader = base.CreateIntegerStreamReader())
                {
                    PdfCoonsPatch item = null;
                    do
                    {
                        int num = reader.ReadEdgeFlag();
                        if (num > 3)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        else
                        {
                            VertexData data;
                            PdfBezierCurve curve;
                            if (num == 0)
                            {
                                PdfPoint point2 = reader.ReadPoint();
                                data = new VertexData(reader);
                                curve = new PdfBezierCurve(new PdfVertex(reader.ReadPoint(), reader.ReadColor()), point2, reader.ReadPoint(), new PdfVertex(reader.ReadPoint(), reader.ReadColor()));
                            }
                            else
                            {
                                if (item == null)
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                data = new VertexData(reader);
                                curve = (num == 1) ? item.Top : ((num == 2) ? item.Right : item.Bottom);
                            }
                            PdfColor color = reader.ReadColor();
                            PdfColor color2 = reader.ReadColor();
                            item = new PdfCoonsPatch(curve, new PdfBezierCurve(curve.Vertex2, data.Point5, data.Point6, new PdfVertex(data.Point7, color)), new PdfBezierCurve(new PdfVertex(data.Point7, color), data.Point8, data.Point9, new PdfVertex(data.Point10, color2)), new PdfBezierCurve(new PdfVertex(data.Point10, color2), data.Point11, data.Point12, curve.Vertex1));
                            this.patches.Add(item);
                        }
                    }
                    while (reader.IgnoreExtendedBits());
                }
            }
        }

        public IList<PdfCoonsPatch> Patches =>
            this.patches;

        protected override int ShadingType =>
            6;

        protected override bool HasBitsPerFlag =>
            true;

        [StructLayout(LayoutKind.Sequential)]
        private struct VertexData
        {
            private readonly PdfPoint point5;
            private readonly PdfPoint point6;
            private readonly PdfPoint point7;
            private readonly PdfPoint point8;
            private readonly PdfPoint point9;
            private readonly PdfPoint point10;
            private readonly PdfPoint point11;
            private readonly PdfPoint point12;
            public PdfPoint Point5 =>
                this.point5;
            public PdfPoint Point6 =>
                this.point6;
            public PdfPoint Point7 =>
                this.point7;
            public PdfPoint Point8 =>
                this.point8;
            public PdfPoint Point9 =>
                this.point9;
            public PdfPoint Point10 =>
                this.point10;
            public PdfPoint Point11 =>
                this.point11;
            public PdfPoint Point12 =>
                this.point12;
            public VertexData(PdfIntegerStreamReader streamReader)
            {
                this.point5 = streamReader.ReadPoint();
                this.point6 = streamReader.ReadPoint();
                this.point7 = streamReader.ReadPoint();
                this.point8 = streamReader.ReadPoint();
                this.point9 = streamReader.ReadPoint();
                this.point10 = streamReader.ReadPoint();
                this.point11 = streamReader.ReadPoint();
                this.point12 = streamReader.ReadPoint();
            }
        }
    }
}

