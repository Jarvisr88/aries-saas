namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IBarCodeData
    {
        double Module { get; }

        bool AutoModule { get; }

        bool ShowText { get; }

        string Text { get; }

        TextAlignment Alignment { get; }

        BarCodeOrientation Orientation { get; }

        BrickStyle Style { get; }
    }
}

