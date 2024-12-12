namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public interface IPageData
    {
        bool Landscape { get; }

        Rectangle Bounds { get; }

        DevExpress.XtraPrinting.Native.MarginsF MarginsF { get; }
    }
}

