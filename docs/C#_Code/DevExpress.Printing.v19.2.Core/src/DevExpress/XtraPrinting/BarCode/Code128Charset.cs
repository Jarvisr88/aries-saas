namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum Code128Charset
    {
        public const Code128Charset CharsetA = Code128Charset.CharsetA;,
        public const Code128Charset CharsetB = Code128Charset.CharsetB;,
        public const Code128Charset CharsetC = Code128Charset.CharsetC;,
        public const Code128Charset CharsetAuto = Code128Charset.CharsetAuto;
    }
}

