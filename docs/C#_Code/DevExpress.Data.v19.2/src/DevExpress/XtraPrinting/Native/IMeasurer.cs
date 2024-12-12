namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public interface IMeasurer
    {
        SizeF MeasureString(string text, Font font, GraphicsUnit pageUnit);
        SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit pageUnit);
        SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit);
        SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit pageUnit);
    }
}

