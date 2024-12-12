namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public interface IOffsetPositionWriter
    {
        void WriteData(BinaryWriter writer);

        long StartPosition { get; set; }

        int OffsetPositionThingsCounter { get; set; }
    }
}

