namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.IO;

    public class PdfIntegerStreamReader : PdfDisposableObject
    {
        private readonly int bitsPerFlag;
        private readonly int bitsPerCoordinate;
        private readonly int bitsPerComponent;
        private readonly PdfDecodeRange decodeX;
        private readonly PdfDecodeRange decodeY;
        private readonly PdfDecodeRange[] decodeC;
        private readonly PdfBitReader bitReader;
        private readonly Stream stream;

        public PdfIntegerStreamReader(int bitsPerFlag, int bitsPerCoordinate, int bitsPerComponent, PdfDecodeRange decodeX, PdfDecodeRange decodeY, PdfDecodeRange[] decodeC, byte[] data)
        {
            this.bitsPerFlag = bitsPerFlag;
            this.bitsPerCoordinate = bitsPerCoordinate;
            this.bitsPerComponent = bitsPerComponent;
            this.decodeX = decodeX;
            this.decodeY = decodeY;
            this.decodeC = decodeC;
            this.stream = new MemoryStream(data);
            this.bitReader = new PdfBitReader(this.stream);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.stream.Dispose();
            }
        }

        public bool IgnoreExtendedBits() => 
            this.bitReader.IgnoreExtendedBits();

        public PdfColor ReadColor()
        {
            int length = this.decodeC.Length;
            double[] components = new double[length];
            for (int i = 0; i < length; i++)
            {
                components[i] = this.decodeC[i].DecodeValue((double) this.bitReader.GetInteger(this.bitsPerComponent));
            }
            return new PdfColor(components);
        }

        public int ReadEdgeFlag() => 
            this.bitReader.GetInteger(this.bitsPerFlag);

        public PdfPoint ReadPoint()
        {
            double unsignedInteger = this.bitReader.GetUnsignedInteger(this.bitsPerCoordinate);
            double num2 = this.bitReader.GetUnsignedInteger(this.bitsPerCoordinate);
            return new PdfPoint(this.decodeX.DecodeValue(unsignedInteger), this.decodeY.DecodeValue(num2));
        }

        public PdfVertex ReadVertex()
        {
            PdfVertex vertex = new PdfVertex(this.ReadPoint(), this.ReadColor());
            this.bitReader.IgnoreExtendedBits();
            return vertex;
        }

        public int BytesPerVertex
        {
            get
            {
                int num = (this.bitsPerFlag + (this.bitsPerCoordinate * 2)) + (this.bitsPerComponent * this.decodeC.Length);
                int num2 = num / 8;
                if ((num % 8) > 0)
                {
                    num2++;
                }
                return num2;
            }
        }
    }
}

