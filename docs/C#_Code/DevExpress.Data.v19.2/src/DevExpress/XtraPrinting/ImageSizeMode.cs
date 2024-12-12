namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ImageSizeMode
    {
        Normal = 0,
        StretchImage = 1,
        AutoSize = 2,
        [EditorBrowsable(EditorBrowsableState.Never)]
        CenterImage = 3,
        ZoomImage = 4,
        Squeeze = 5,
        Tile = 6
    }
}

