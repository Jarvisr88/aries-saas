namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class IntelligentMailPackageGenerator : EAN128Generator
    {
        private const int allowedLengthShort = 0x16;
        private const int allowedLengthShortWithoutCheckDigit = 0x15;
        private const int allowedLengthLong = 0x1a;
        private const int allowedLengthLongWithoutCheckDigit = 0x19;
        private static readonly float quietZone;
        private static readonly float lineWidth;
        private static readonly float smallClearance;
        private static readonly float bigClearance;
        private static readonly float textHeight;
        private static readonly float barHeight;
        private static readonly float barCodeHeight;

        static IntelligentMailPackageGenerator();
        public IntelligentMailPackageGenerator();
        protected IntelligentMailPackageGenerator(IntelligentMailPackageGenerator source);
        protected override double CalcAutoModuleX(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected override float CalcBarCodeHeight(ArrayList pattern, double module);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        private int CalculateCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        protected override string FormatText(string text);
        private static Font GetFont();
        private string GetHumanReadableText(string text);
        private int[] GetNumbersWithoutZIP(string text);
        private static StringFormat GetStringFormat();
        private int GetSum(bool even, int[] numbers);
        private string GetTextWithoutZIP(string text);
        private string GetZIP(string text);
        private static bool IsValidAIWithLongLength(int ai, int lengthWithoutZIP);
        private static bool IsValidAIWithShortLength(int ai, int lengthWithoutZIP);
        protected override bool IsValidText(string text);
        protected override bool IsValidTextFormat(string text);
        private static bool IsValidZIP(string zip);
        protected override void JustifyBarcodeBounds(IBarCodeData data, ref float barCodeWidth, ref float barCodeHeight, ref RectangleF barBounds);
        protected override string MakeDisplayText(string text);
        private float VerticalAlignHeight(float clientHeight, TextAlignment alignment);

        [Description("This property is not in effect for the IntelligentMailPackageGenerator class.")]
        public override BarCodeSymbology SymbologyCode { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override bool HumanReadableText { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override Code128Charset CharacterSet { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IntelligentMailPackageGenerator.<>c <>9;
            public static Func<GS1Helper.ElementResult, bool> <>9__33_0;
            public static Converter<char, int> <>9__35_0;

            static <>c();
            internal int <GetNumbersWithoutZIP>b__35_0(char c);
            internal bool <GetTextWithoutZIP>b__33_0(GS1Helper.ElementResult element);
        }
    }
}

