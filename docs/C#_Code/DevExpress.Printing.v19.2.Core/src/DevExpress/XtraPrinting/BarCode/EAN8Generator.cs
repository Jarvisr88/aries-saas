namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class EAN8Generator : EAN13Generator
    {
        public EAN8Generator();
        protected EAN8Generator(EAN8Generator source);
        protected override BarCodeGeneratorBase CloneGenerator();
        protected override void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        protected override string FormatText(string text);
        protected override int[] GetGuardBarsBounds();
        protected override float GetLeftSpacing(IBarCodeData data, IGraphicsBase gr);
        protected override int GetMiddleIndex();
        protected override char[] PrepareText(string text);

        [Description("For internal use. Gets the bar code symbology for the current generator object.")]
        public override BarCodeSymbology SymbologyCode { get; }
    }
}

