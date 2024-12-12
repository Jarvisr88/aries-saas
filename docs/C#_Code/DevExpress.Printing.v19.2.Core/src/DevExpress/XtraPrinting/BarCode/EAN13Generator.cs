namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EAN13Generator : BarCodeGeneratorBase
    {
        private static Hashtable parityTable;
        private static string validCharSet;
        internal static Hashtable charPattern;
        protected static StringFormat sfCenter;
        protected static StringFormat sfRight;
        private const int spacing = 10;

        static EAN13Generator();
        public EAN13Generator();
        protected EAN13Generator(EAN13Generator source);
        protected static char CalcCheckDigit(string text);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        protected override void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        protected override string FormatText(string text);
        protected internal static string FormatText(string text, int fixedWidth);
        protected virtual int[] GetGuardBarsBounds();
        protected override float GetLeftSpacing(IBarCodeData data, IGraphicsBase gr);
        protected virtual int GetMiddleIndex();
        protected virtual string GetParityString(char numberSystem);
        protected override Hashtable GetPatternTable();
        protected float GetSpacing(IGraphicsBase graphics);
        protected override string GetValidCharSet();
        protected static float MeasureCharWidth(char ch, IBarCodeData data, StringFormat sf, IGraphicsBase gr);
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        internal bool ConvertSpacingUnits { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

