namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IBaseBrick : IBrick
    {
        string Hint { get; set; }

        RectangleF Rect { get; set; }

        string Url { get; set; }

        object Value { get; set; }
    }
}

