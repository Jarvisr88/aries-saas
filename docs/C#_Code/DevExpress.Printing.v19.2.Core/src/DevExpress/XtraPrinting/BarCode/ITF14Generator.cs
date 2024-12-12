namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class ITF14Generator : Interleaved2of5Generator
    {
        private const int quietZone = 10;

        public ITF14Generator();
        protected ITF14Generator(ITF14Generator source);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        protected override string FormatText(string text);
        protected override string MakeDisplayText(string text);
        protected override char[] PrepareText(string text);

        [Description(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.ITF14Generator.WideNarrowRatio")]
        public override float WideNarrowRatio { get; set; }

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

