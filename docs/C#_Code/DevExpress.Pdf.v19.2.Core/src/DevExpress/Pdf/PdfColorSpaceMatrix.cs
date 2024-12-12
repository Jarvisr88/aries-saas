namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfColorSpaceMatrix
    {
        private readonly double xa;
        private readonly double ya;
        private readonly double za;
        private readonly double xb;
        private readonly double yb;
        private readonly double zb;
        private readonly double xc;
        private readonly double yc;
        private readonly double zc;

        internal PdfColorSpaceMatrix()
        {
            this.xa = 1.0;
            this.yb = 1.0;
            this.zc = 1.0;
        }

        internal PdfColorSpaceMatrix(IList<object> array)
        {
            this.xa = 1.0;
            this.yb = 1.0;
            this.zc = 1.0;
            if (array.Count != 9)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.xa = PdfDocumentReader.ConvertToDouble(array[0]);
            this.ya = PdfDocumentReader.ConvertToDouble(array[1]);
            this.za = PdfDocumentReader.ConvertToDouble(array[2]);
            this.xb = PdfDocumentReader.ConvertToDouble(array[3]);
            this.yb = PdfDocumentReader.ConvertToDouble(array[4]);
            this.zb = PdfDocumentReader.ConvertToDouble(array[5]);
            this.xc = PdfDocumentReader.ConvertToDouble(array[6]);
            this.yc = PdfDocumentReader.ConvertToDouble(array[7]);
            this.zc = PdfDocumentReader.ConvertToDouble(array[8]);
        }

        internal double[] ToArray()
        {
            double[] numArray1 = new double[9];
            numArray1[0] = this.xa;
            numArray1[1] = this.ya;
            numArray1[2] = this.za;
            numArray1[3] = this.xb;
            numArray1[4] = this.yb;
            numArray1[5] = this.zb;
            numArray1[6] = this.xc;
            numArray1[7] = this.yc;
            numArray1[8] = this.zc;
            return numArray1;
        }

        public double Xa =>
            this.xa;

        public double Ya =>
            this.ya;

        public double Za =>
            this.za;

        public double Xb =>
            this.xb;

        public double Yb =>
            this.yb;

        public double Zb =>
            this.zb;

        public double Xc =>
            this.xc;

        public double Yc =>
            this.yc;

        public double Zc =>
            this.zc;

        public bool IsIdentity =>
            (this.xa == 1.0) && ((this.ya == 0.0) && ((this.za == 0.0) && ((this.xb == 0.0) && ((this.yb == 1.0) && ((this.zb == 0.0) && ((this.xc == 0.0) && ((this.yc == 0.0) && (this.zc == 1.0))))))));
    }
}

