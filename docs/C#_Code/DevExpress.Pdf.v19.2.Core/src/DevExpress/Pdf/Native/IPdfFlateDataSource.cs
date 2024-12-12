namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfFlateDataSource : IDisposable
    {
        void FillBuffer(byte[] buffer);
    }
}

