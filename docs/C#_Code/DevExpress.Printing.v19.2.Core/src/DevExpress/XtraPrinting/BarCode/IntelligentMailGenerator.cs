namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class IntelligentMailGenerator : BarCode2DGenerator
    {
        private readonly IntelligentMailProcessor processor;
        private const short HightToWidthRatio = 8;

        public IntelligentMailGenerator();
        private IntelligentMailGenerator(IntelligentMailGenerator source);
        protected override RectangleF AlignBarcodeBounds(RectangleF barcodeBounds, float width, float height, TextAlignment align);
        protected override bool BinaryCompactionMode();
        protected override float CalcBarCodeHeight(ArrayList pattern, double module);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        private static int GetPatternLengthTable(char patternSymbol);
        private static int GetPatternOffsetTable(char patternSymbol);
        protected override Hashtable GetPatternTable();
        protected override float GetPatternWidth(char pattern);
        protected override string GetValidCharSet();
        protected override bool IsValidText(string text);
        private static bool IsValidTextLength(string text);
        private static bool IsValidValue(string text);
        protected override char[] PrepareText(string text);
        protected override bool TextCompactionMode();
        public override void Update(string text, byte[] binaryData);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        protected override IPatternProcessor PatternProcessor { get; }

        protected override bool IsSquareBarcode { get; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

