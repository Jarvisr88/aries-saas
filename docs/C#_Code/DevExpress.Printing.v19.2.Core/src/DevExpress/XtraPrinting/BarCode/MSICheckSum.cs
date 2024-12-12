namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum MSICheckSum
    {
        public const MSICheckSum None = MSICheckSum.None;,
        public const MSICheckSum Modulo10 = MSICheckSum.Modulo10;,
        public const MSICheckSum DoubleModulo10 = MSICheckSum.DoubleModulo10;
    }
}

