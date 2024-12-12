namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public interface ISupportPartialReading
    {
        void ReadData(XlReader reader);

        bool Complete { get; }
    }
}

