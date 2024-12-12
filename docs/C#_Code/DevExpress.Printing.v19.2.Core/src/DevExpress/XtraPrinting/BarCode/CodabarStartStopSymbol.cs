namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum CodabarStartStopSymbol
    {
        public const CodabarStartStopSymbol None = CodabarStartStopSymbol.None;,
        public const CodabarStartStopSymbol A = CodabarStartStopSymbol.A;,
        public const CodabarStartStopSymbol B = CodabarStartStopSymbol.B;,
        public const CodabarStartStopSymbol C = CodabarStartStopSymbol.C;,
        public const CodabarStartStopSymbol D = CodabarStartStopSymbol.D;
    }
}

