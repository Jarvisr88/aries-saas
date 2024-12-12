namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public abstract class UPCEGeneratorBase : UPCAGenerator
    {
        protected UPCEGeneratorBase();
        protected UPCEGeneratorBase(UPCEGeneratorBase source);
        internal static string ConvertUPCA2UPCE(string text);
        protected override void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        protected override string FormatText(string text);
        protected override int[] GetGuardBarsBounds();
        protected override float GetLeftSpacing(IBarCodeData data, IGraphicsBase gr);
        protected override int GetMiddleIndex();
        protected abstract char GetNumberSystemDigit();
        protected override bool IsValidTextFormat(string text);
        protected override char[] PrepareText(string text);

        [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }
    }
}

