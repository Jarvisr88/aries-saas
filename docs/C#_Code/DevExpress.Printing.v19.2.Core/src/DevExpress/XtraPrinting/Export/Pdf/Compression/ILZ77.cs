namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public interface ILZ77
    {
        bool Next(LZ77ResultValue resultValue);
        void Reset();
        void Reset(byte[] input);
    }
}

