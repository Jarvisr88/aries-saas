namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ImageViewMode
    {
        Clip,
        Stretch,
        Zoom
    }
}

