namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class DataMatrixBinaryPatternProcessor : DataMatrixPatternProcessor
    {
        protected override bool EncodeData(object data, byte[] encodeBuf, out int encodeBufSize);
        public override string GetValidCharSet();
    }
}

