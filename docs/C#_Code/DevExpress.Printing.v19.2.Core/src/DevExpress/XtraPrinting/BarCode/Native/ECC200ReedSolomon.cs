namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Runtime.InteropServices;

    internal class ECC200ReedSolomon
    {
        private int gfPoly;
        private int symSize;
        private int logmod;
        private int rlen;
        private int[] log;
        private int[] alog;
        private int[] rspoly;

        public ECC200ReedSolomon(int poly, int nsym, int index);
        public void Encode(byte[] data, int len, out byte[] res);
        public void InitCode(int nsym, int index);
        public void InitGf(int poly);
    }
}

