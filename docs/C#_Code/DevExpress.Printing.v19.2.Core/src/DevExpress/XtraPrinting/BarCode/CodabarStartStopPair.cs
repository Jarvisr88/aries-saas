namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum CodabarStartStopPair
    {
        public const CodabarStartStopPair None = CodabarStartStopPair.None;,
        public const CodabarStartStopPair AT = CodabarStartStopPair.AT;,
        public const CodabarStartStopPair BN = CodabarStartStopPair.BN;,
        public const CodabarStartStopPair CStar = CodabarStartStopPair.CStar;,
        public const CodabarStartStopPair DE = CodabarStartStopPair.DE;
    }
}

