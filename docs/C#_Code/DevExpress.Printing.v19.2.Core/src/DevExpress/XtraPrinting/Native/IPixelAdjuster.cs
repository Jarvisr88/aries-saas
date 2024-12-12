namespace DevExpress.XtraPrinting.Native
{
    using System.Drawing;

    public interface IPixelAdjuster
    {
        RectangleF AdjustRect(RectangleF bounds);
        SizeF AdjustSize(SizeF size);
        SizeF GetDevicePointSize();
    }
}

