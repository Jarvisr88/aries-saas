namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfTensorProductPatchMesh : PdfMeshShading
    {
        internal const int Type = 7;
        private readonly IList<PdfTensorProductPatch> patches;

        internal PdfTensorProductPatchMesh(PdfReaderStream stream) : base(stream)
        {
            this.patches = new List<PdfTensorProductPatch>();
            if (base.Data.Length != 0)
            {
                using (PdfIntegerStreamReader reader = base.CreateIntegerStreamReader())
                {
                    PdfTensorProductPatch item = null;
                    do
                    {
                        PdfPoint point;
                        PdfPoint point2;
                        PdfPoint point3;
                        PdfPoint point4;
                        VertexData data;
                        PdfColor color;
                        PdfColor color2;
                        int num = reader.ReadEdgeFlag();
                        if (num > 3)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        if (num == 0)
                        {
                            point = reader.ReadPoint();
                            point2 = reader.ReadPoint();
                            point3 = reader.ReadPoint();
                            point4 = reader.ReadPoint();
                            data = new VertexData(reader);
                            color = reader.ReadColor();
                            color2 = reader.ReadColor();
                        }
                        else
                        {
                            if (item == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            PdfPoint[,] pointArray2 = item.ControlPoints;
                            PdfColor[] colorArray = item.Colors;
                            if (num == 1)
                            {
                                point = pointArray2[0, 3];
                                point2 = pointArray2[1, 3];
                                point3 = pointArray2[2, 3];
                                point4 = pointArray2[3, 3];
                                color = colorArray[1];
                                color2 = colorArray[2];
                            }
                            else if (num != 2)
                            {
                                point = pointArray2[3, 0];
                                point2 = pointArray2[2, 0];
                                point3 = pointArray2[1, 0];
                                point4 = pointArray2[0, 0];
                                color = colorArray[3];
                                color2 = colorArray[0];
                            }
                            else
                            {
                                point = pointArray2[3, 3];
                                point2 = pointArray2[3, 2];
                                point3 = pointArray2[3, 1];
                                point4 = pointArray2[3, 0];
                                color = colorArray[2];
                                color2 = colorArray[3];
                            }
                            data = new VertexData(reader);
                        }
                        PdfPoint[] pointArray1 = new PdfPoint[,] { { point, point2, point3, point4 }, { data.P10, data.P11, data.P12, data.P13 }, { data.P20, data.P21, data.P22, data.P23 }, { data.P30, data.P31, data.P32, data.P33 } };
                        PdfPoint[,] controlPoints = pointArray1;
                        PdfColor color3 = reader.ReadColor();
                        PdfColor color4 = reader.ReadColor();
                        PdfColor[] colors = new PdfColor[] { color, color2, color3, color4 };
                        item = new PdfTensorProductPatch(controlPoints, colors);
                        this.patches.Add(item);
                    }
                    while (reader.IgnoreExtendedBits());
                }
            }
        }

        public IList<PdfTensorProductPatch> Patches =>
            this.patches;

        protected override int ShadingType =>
            7;

        protected override bool HasBitsPerFlag =>
            true;

        [StructLayout(LayoutKind.Sequential)]
        private struct VertexData
        {
            private readonly PdfPoint p13;
            private readonly PdfPoint p23;
            private readonly PdfPoint p33;
            private readonly PdfPoint p32;
            private readonly PdfPoint p31;
            private readonly PdfPoint p30;
            private readonly PdfPoint p20;
            private readonly PdfPoint p10;
            private readonly PdfPoint p11;
            private readonly PdfPoint p12;
            private readonly PdfPoint p22;
            private readonly PdfPoint p21;
            public PdfPoint P13 =>
                this.p13;
            public PdfPoint P23 =>
                this.p23;
            public PdfPoint P33 =>
                this.p33;
            public PdfPoint P32 =>
                this.p32;
            public PdfPoint P31 =>
                this.p31;
            public PdfPoint P30 =>
                this.p30;
            public PdfPoint P20 =>
                this.p20;
            public PdfPoint P10 =>
                this.p10;
            public PdfPoint P11 =>
                this.p11;
            public PdfPoint P12 =>
                this.p12;
            public PdfPoint P22 =>
                this.p22;
            public PdfPoint P21 =>
                this.p21;
            public VertexData(PdfIntegerStreamReader streamReader)
            {
                this.p13 = streamReader.ReadPoint();
                this.p23 = streamReader.ReadPoint();
                this.p33 = streamReader.ReadPoint();
                this.p32 = streamReader.ReadPoint();
                this.p31 = streamReader.ReadPoint();
                this.p30 = streamReader.ReadPoint();
                this.p20 = streamReader.ReadPoint();
                this.p10 = streamReader.ReadPoint();
                this.p11 = streamReader.ReadPoint();
                this.p12 = streamReader.ReadPoint();
                this.p22 = streamReader.ReadPoint();
                this.p21 = streamReader.ReadPoint();
            }
        }
    }
}

