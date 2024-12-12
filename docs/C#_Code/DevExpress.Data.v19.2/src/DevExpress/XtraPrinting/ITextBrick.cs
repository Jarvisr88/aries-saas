namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public interface ITextBrick : IVisualBrick, IBaseBrick, IBrick
    {
        System.Drawing.Font Font { get; set; }

        Color ForeColor { get; set; }

        DevExpress.Utils.HorzAlignment HorzAlignment { get; set; }

        BrickStringFormat StringFormat { get; set; }

        DevExpress.Utils.VertAlignment VertAlignment { get; set; }

        DefaultBoolean XlsExportNativeFormat { get; set; }
    }
}

