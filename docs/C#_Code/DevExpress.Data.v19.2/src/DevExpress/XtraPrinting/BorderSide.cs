namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting.Design;
    using System;
    using System.ComponentModel;

    [Serializable, TypeConverter(typeof(BordersConverter)), Flags, ResourceFinder(typeof(ResFinder))]
    public enum BorderSide
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,
        All = 15
    }
}

