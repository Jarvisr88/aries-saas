namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum PdfJpegImageQuality
    {
        Lowest = 10,
        Low = 0x19,
        Medium = 50,
        High = 0x4b,
        Highest = 100
    }
}

