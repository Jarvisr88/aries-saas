namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public abstract class BarCode2DGenerator : BarCodeGeneratorBase
    {
        private string text;
        private byte[] binaryData;
        private RectangleF textBarcodeBounds;

        public BarCode2DGenerator();
        public BarCode2DGenerator(BarCode2DGenerator source);
        protected override RectangleF AlignBarcodeBounds(RectangleF barcodeBounds, float width, float height, TextAlignment align);
        protected override RectangleF AlignTextBounds(IBarCodeData data, RectangleF barBounds, RectangleF textBounds);
        protected abstract bool BinaryCompactionMode();
        protected override double CalcAutoModuleX(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected override double CalcAutoModuleY(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected override float CalcBarCodeHeight(ArrayList pattern, double module);
        protected override float CalcBarCodeWidth(ArrayList pattern, double module);
        protected override void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        protected override Hashtable GetPatternTable();
        protected override void JustifyBarcodeBounds(IBarCodeData data, ref float barCodeWidth, ref float barCodeHeight, ref RectangleF barBounds);
        protected override ArrayList MakeBarCodePattern(string text);
        protected override char[] PrepareText(string text);
        protected virtual string ProcessText(string text);
        protected virtual void RefreshPatternProcessor();
        protected abstract bool TextCompactionMode();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void Update(string text, byte[] binaryData);

        protected string Text { get; set; }

        protected byte[] BinaryData { get; set; }

        protected object Data { get; }

        protected abstract IPatternProcessor PatternProcessor { get; }

        protected abstract bool IsSquareBarcode { get; }

        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool CalcCheckSum { get; set; }
    }
}

