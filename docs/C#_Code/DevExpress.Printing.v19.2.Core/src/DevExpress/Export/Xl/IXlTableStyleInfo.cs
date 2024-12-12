namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlTableStyleInfo
    {
        string Name { get; set; }

        bool ShowColumnStripes { get; set; }

        bool ShowRowStripes { get; set; }

        bool ShowFirstColumn { get; set; }

        bool ShowLastColumn { get; set; }
    }
}

