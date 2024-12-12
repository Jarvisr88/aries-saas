namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public interface IXlsContent
    {
        int GetSize();
        void Read(XlReader reader, int size);
        void Write(BinaryWriter writer);

        FutureRecordHeaderBase RecordHeader { get; }
    }
}

