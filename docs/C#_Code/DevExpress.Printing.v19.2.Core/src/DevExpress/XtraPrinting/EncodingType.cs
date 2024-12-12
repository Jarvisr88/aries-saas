namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum EncodingType
    {
        Default,
        ASCII,
        Unicode,
        BigEndianUnicode,
        UTF7,
        UTF8,
        UTF32
    }
}

