namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class UPCAGenerator : EAN13Generator
    {
        protected static StringFormat sfLeft;

        static UPCAGenerator();
        public UPCAGenerator();
        protected UPCAGenerator(UPCAGenerator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        protected override string FormatText(string text);
        protected override float GetLeftSpacing(IBarCodeData data, IGraphicsBase gr);
        protected override float GetRightSpacing(IBarCodeData data, IGraphicsBase gr);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

