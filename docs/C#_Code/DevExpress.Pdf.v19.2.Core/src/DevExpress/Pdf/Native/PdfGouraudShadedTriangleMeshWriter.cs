namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.IO;

    public class PdfGouraudShadedTriangleMeshWriter : PdfDisposableObject
    {
        private readonly PdfGouraudShadedTriangleMesh mesh;
        private readonly int bitsPerFlag;
        private readonly int bitsPerCoordinate;
        private readonly int bitsPerComponent;
        private readonly double minX;
        private readonly double xFactor;
        private readonly double minY;
        private readonly double yFactor;
        private readonly double[] minC;
        private readonly double[] cFactor;
        private readonly byte[] data;
        private readonly MemoryStream stream;
        private readonly BinaryWriter writer;
        private byte writingByte;
        private int currentBitOffset;

        private PdfGouraudShadedTriangleMeshWriter(PdfGouraudShadedTriangleMesh mesh)
        {
            this.mesh = mesh;
            this.bitsPerFlag = mesh.BitsPerFlag;
            this.bitsPerCoordinate = mesh.BitsPerCoordinate;
            this.bitsPerComponent = mesh.BitsPerComponent;
            uint num = (uint) (-1 >> ((0x20 - this.bitsPerCoordinate) & 0x1f));
            PdfDecodeRange decodeX = mesh.DecodeX;
            this.minX = decodeX.Min;
            this.xFactor = ((double) num) / (decodeX.Max - this.minX);
            PdfDecodeRange decodeY = mesh.DecodeY;
            this.minY = decodeY.Min;
            this.yFactor = ((double) num) / (decodeY.Max - this.minY);
            PdfDecodeRange[] decodeC = mesh.DecodeC;
            int length = decodeC.Length;
            this.minC = new double[length];
            this.cFactor = new double[length];
            int num3 = (1 << (this.bitsPerComponent & 0x1f)) - 1;
            for (int i = 0; i < length; i++)
            {
                PdfRange range3 = decodeC[i];
                double num7 = this.minC[i];
                this.minC[i] = num7;
                this.cFactor[i] = ((double) num3) / (range3.Max - num7);
            }
            int num4 = (this.bitsPerFlag + (this.bitsPerCoordinate * 2)) + (this.bitsPerComponent * length);
            int num5 = num4 / 8;
            if ((num4 % 8) > 0)
            {
                num5++;
            }
            this.data = new byte[(num5 * mesh.Triangles.Count) * 3];
            this.stream = new MemoryStream(this.data);
            this.writer = new BinaryWriter(this.stream);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.writer.Dispose();
                this.stream.Dispose();
            }
        }

        private byte[] Write()
        {
            foreach (PdfTriangle triangle in this.mesh.Triangles)
            {
                this.Write(triangle.Vertex1);
                this.Write(triangle.Vertex2);
                this.Write(triangle.Vertex3);
            }
            if (this.currentBitOffset != 0)
            {
                this.WriteByte();
            }
            this.writer.Flush();
            return this.data;
        }

        public static byte[] Write(PdfGouraudShadedTriangleMesh mesh)
        {
            using (PdfGouraudShadedTriangleMeshWriter writer = new PdfGouraudShadedTriangleMeshWriter(mesh))
            {
                return writer.Write();
            }
        }

        private void Write(PdfVertex value)
        {
            this.WriteBits(0, this.bitsPerFlag);
            PdfPoint point = value.Point;
            this.WriteBits((uint) ((point.X - this.minX) * this.xFactor), this.bitsPerCoordinate);
            this.WriteBits((uint) ((point.Y - this.minY) * this.yFactor), this.bitsPerCoordinate);
            double[] components = value.Color.Components;
            int length = components.Length;
            for (int i = 0; i < length; i++)
            {
                this.WriteBits((uint) ((components[i] - this.minC[i]) * this.cFactor[i]), this.bitsPerComponent);
            }
            if (this.currentBitOffset != 0)
            {
                this.WriteByte();
            }
        }

        private void WriteBits(uint value, int bitsCount)
        {
            int num = bitsCount;
            int num2 = 8 - this.currentBitOffset;
            while (num > num2)
            {
                int num3 = num - num2;
                this.writingByte = (byte) (this.writingByte | ((byte) ((value >> (num3 & 0x1f)) & (-1 >> (num3 & 0x1f)))));
                this.currentBitOffset += num2;
                this.WriteByte();
                num -= num2;
                num2 = 8 - this.currentBitOffset;
            }
            this.writingByte = (byte) (this.writingByte | ((byte) ((value & ((1 << (num & 0x1f)) - 1)) << ((num2 - num) & 0x3f))));
            this.currentBitOffset += num;
        }

        private void WriteByte()
        {
            this.writer.Write(this.writingByte);
            this.writingByte = 0;
            this.currentBitOffset = 0;
        }
    }
}

