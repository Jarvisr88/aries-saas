namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum PageInfo : byte
    {
        None = 0,
        Number = 1,
        NumberOfTotal = 2,
        RomLowNumber = 4,
        RomHiNumber = 8,
        DateTime = 0x10,
        UserName = 0x20,
        Total = 0x40
    }
}

