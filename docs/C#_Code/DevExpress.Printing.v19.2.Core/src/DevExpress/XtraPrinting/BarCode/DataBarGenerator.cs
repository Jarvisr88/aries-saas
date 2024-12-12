namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using DevExpress.XtraReports.Design;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class DataBarGenerator : BarCodeGeneratorBase
    {
        private const string defaultFNC1Subst = "#";
        private const char fnc1Char = '\x00e8';
        private const int fixedNumberDigits = 14;
        private static string validCharCharSet;
        private DataBarType type;
        private int segmentPairsInRow;
        private string fnc1Subst;
        private DataBarPatternProcessor dataBarPatternProcessor;

        static DataBarGenerator();
        public DataBarGenerator();
        private DataBarGenerator(DataBarGenerator source);
        protected override double CalcAutoModuleX(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected override float CalcBarCodeHeight(ArrayList pattern, double module);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        private void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data, float heightBarCode);
        protected override string FormatText(string text);
        protected override Hashtable GetPatternTable();
        protected override string GetValidCharSet();
        protected override bool IsValidTextFormat(string text);
        protected override void JustifyBarcodeBounds(IBarCodeData data, ref float barCodeWidth, ref float barCodeHeight, ref RectangleF barBounds);
        protected override ArrayList MakeBarCodePattern(string text);
        protected override string MakeDisplayText(string text);
        protected override char[] PrepareText(string text);

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }

        [Description("Gets or sets the type of a GS1 DataBar bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataBarGenerator.Type"), DefaultValue(0), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, NotifyParentProperty(true)]
        public DataBarType Type { get; set; }

        [Description("Gets or sets the number of data segments per row in the Expanded Stacked type of a GS1 DataBar bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataBarGenerator.SegmentsInRow"), TypeConverter(typeof(DataBarExpandedWidthConverter)), DefaultValue(20), RefreshProperties(RefreshProperties.All), NotifyParentProperty(true), XtraSerializableProperty]
        public int SegmentsInRow { get; set; }

        [Description("Specifies the symbol (or set of symbols) in the bar code's text that will be replaced with the FNC1 functional character when the bars are drawn."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.DataBarGenerator.FNC1Substitute"), DefaultValue("#"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(DataBarExpandedFNC1Converter)), NotifyParentProperty(true), XtraSerializableProperty]
        public string FNC1Substitute { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }

        protected DataBarPatternProcessor PatternProcessor { get; }
    }
}

