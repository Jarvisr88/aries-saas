namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public interface IXlsChunk
    {
        int GetMaxDataSize();
        void Write(BinaryWriter writer);

        byte[] Data { get; set; }
    }
}

