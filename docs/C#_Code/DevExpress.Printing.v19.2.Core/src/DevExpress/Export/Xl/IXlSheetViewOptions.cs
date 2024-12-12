namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlSheetViewOptions
    {
        bool ShowFormulas { get; set; }

        bool ShowGridLines { get; set; }

        bool ShowRowColumnHeaders { get; set; }

        bool ShowZeroValues { get; set; }

        bool ShowOutlineSymbols { get; set; }

        bool RightToLeft { get; set; }
    }
}

