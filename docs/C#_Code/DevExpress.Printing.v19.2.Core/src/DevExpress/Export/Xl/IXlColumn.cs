namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlColumn : IDisposable
    {
        void ApplyFormatting(XlCellFormatting formatting);

        int ColumnIndex { get; }

        bool IsHidden { get; set; }

        bool IsCollapsed { get; set; }

        int WidthInPixels { get; set; }

        float WidthInCharacters { get; set; }

        XlCellFormatting Formatting { get; set; }
    }
}

