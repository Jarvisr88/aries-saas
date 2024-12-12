namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BarCode.Native;
    using DevExpress.XtraReports.Design;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class Code128Generator : BarCodeGeneratorBase
    {
        protected static ArrayList charSetA;
        private static ArrayList charSetB;
        private static ArrayList charSetC;
        protected const char FNC1 = 'f';
        internal static string fnc1Char;
        private static string validCharsetA;
        private static string validCharsetB;
        private static Hashtable charPattern;
        private const int stopSymbolIndex = 0x6a;
        private const int shiftToCharsetC = 0x63;
        private const int shiftToCharsetB = 100;
        private const int shiftToCharsetA = 0x65;
        private Code128Charset startCharSet;
        private Code128Charset fStartCharSet;
        private bool addLeadingZero;

        static Code128Generator();
        public Code128Generator();
        protected Code128Generator(Code128Generator source);
        private static void AppendFromString(ArrayList array, string text);
        private static Code128Charset AutoDetectCharset(char[] text, int from, ref int sequenceLength);
        private static char CalcCheckDigit(ArrayList text);
        protected override BarCodeGeneratorBase CloneGenerator();
        private static void FillCharPattern();
        private static void FillCharsetA();
        private static void FillCharsetB();
        private static void FillCharsetC();
        private static int FindSwitchToCharsetC(char[] text, int from, int sequenceLength);
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        private char GetStartSymbolIndex();
        private static char GetSwitchCharTo(Code128Charset cs);
        protected override string GetValidCharSet();
        protected virtual void InsertControlCharsIndexes(ArrayList text);
        private static bool IsDigit(char ch);
        protected override bool IsValidTextFormat(string text);
        protected override ArrayList MakeBarCodePattern(string text);
        protected override char[] PrepareText(string text);
        private char[] Text2Indexes(char[] text);
        private static char[] Text2Indexes(char[] text, int from, int count, Code128Charset cs);
        private char[] Text2IndexesAuto(char[] text);
        private static char[] Text2IndexesC(char[] text, int from, int count);
        private static char[] Text2IndexesInternal(char[] text, int from, int count, ArrayList baseCharSet, ArrayList alternativeCharset);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [Description("Adds a zero sign prefix to the number that is being encoded."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.Code128Generator.AddLeadingZero"), TypeConverter(typeof(Code128LeadZeroConverter)), DefaultValue(false), XtraSerializableProperty]
        public virtual bool AddLeadingZero { get; set; }

        [Description("Gets or sets the character set type for the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.Code128Generator.CharacterSet"), DefaultValue(0x67), NotifyParentProperty(true), XtraSerializableProperty]
        public virtual Code128Charset CharacterSet { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

