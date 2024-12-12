namespace DevExpress.XtraPrinting.BarCode
{
    using DevExpress.Printing;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(BarCodeGeneratorTypeConverter)), Editor("DevExpress.XtraPrinting.BarCode.Design.BarCodeGeneratorEditor, DevExpress.XtraEditors.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public abstract class BarCodeGeneratorBase : StorableObjectBase, ICloneable
    {
        protected static string charSetDigits;
        protected static string charSetUpperCase;
        protected static string charSetLowerCase;
        protected static string charSetAll;
        private bool calcCheckSum;
        private IBarCodeData barCodeData;
        private string finalText;
        private string displayText;
        private ArrayList pattern;

        static BarCodeGeneratorBase();
        protected BarCodeGeneratorBase();
        protected BarCodeGeneratorBase(BarCodeGeneratorBase source);
        private static void AdjustBounds(ref RectangleF textBounds, ref RectangleF barBounds, float textHeight, StringFormat sf);
        protected virtual RectangleF AlignBarcodeBounds(RectangleF barcodeBounds, float width, float height, TextAlignment align);
        protected virtual RectangleF AlignTextBounds(IBarCodeData data, RectangleF barBounds, RectangleF textBounds);
        protected static RectangleF AlignVerticalBarcodeBound(RectangleF barcodeBounds, float height, TextAlignment align);
        protected virtual double CalcAutoModuleX(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected virtual double CalcAutoModuleY(IBarCodeData data, RectangleF clientBounds, IGraphicsBase gr);
        protected virtual float CalcBarCodeHeight(ArrayList pattern, double module);
        protected virtual float CalcBarCodeWidth(ArrayList pattern, double module);
        private static RectangleF CalcClientBounds(RectangleF bounds, IBarCodeData data, GraphicsUnit pageUnit);
        internal bool CalculateDrawingViewInfo(BarCodeDrawingViewInfo viewInfo, IGraphicsBase gr);
        private bool CalculateDrawingViewInfoCore(BarCodeDrawingViewInfo viewInfo, IGraphicsBase gr);
        internal static int Char2Int(char ch);
        protected abstract BarCodeGeneratorBase CloneGenerator();
        protected float CorrectBarcodeHeight(float barCodeHeight, float brickHeight);
        protected float CorrectBarcodeWidth(float barCodeWidth, float brickWidth);
        protected virtual void DrawBarCode(IGraphicsBase gr, RectangleF barBounds, RectangleF textBounds, IBarCodeData data, float xModule, float yModule);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DrawContent(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        private void DrawContentCore(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        private static void DrawErrorString(IGraphicsBase gr, RectangleF bounds, IBarCodeData data, string errorString);
        protected virtual void DrawText(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);
        protected virtual string FormatText(string text);
        private static string GenerateComplement(string chars, int upperBound);
        private static float GetAdditionalHeight(GraphicsUnit graphicsUnit);
        public string GetDisplayText(IBarCodeData data);
        public string GetFinalText(IBarCodeData data);
        protected virtual float GetLeftSpacing(IBarCodeData data, IGraphicsBase gr);
        protected virtual int GetMaxDataLength();
        private static PaddingInfo GetOrientedPaddingSide(BarCodeOrientation orientation, PaddingInfo padding);
        protected abstract Hashtable GetPatternTable();
        protected virtual float GetPatternWidth(char pattern);
        protected virtual float GetRightSpacing(IBarCodeData data, IGraphicsBase measurer);
        private static float GetTransformRotateAngle(IBarCodeData data);
        private static SizeF GetTransformTranslateOffset(RectangleF bounds, IBarCodeData data);
        protected abstract string GetValidCharSet();
        protected virtual ArrayList GetWidthPattern(string pattern);
        protected virtual bool IsCorrectSettings();
        private static bool IsHorzOrientation(BarCodeOrientation orientation);
        protected virtual bool IsValidPattern(ArrayList pattern);
        protected virtual bool IsValidText(string text);
        protected virtual bool IsValidTextFormat(string text);
        protected virtual void JustifyBarcodeBounds(IBarCodeData data, ref float barCodeWidth, ref float barCodeHeight, ref RectangleF barBounds);
        protected virtual ArrayList MakeBarCodePattern(string text);
        protected virtual string MakeDisplayText(string text);
        protected static float MeasureTextHeight(string text, float width, IBarCodeData data, IGraphicsBase gr);
        protected static SizeF MeasureTextSize(string text, float width, IBarCodeData data, IGraphicsBase gr);
        protected abstract char[] PrepareText(string text);
        internal virtual void Scale(double scaleFactor);
        object ICloneable.Clone();
        public BarCodeError Validate(IGraphicsBase gr, RectangleF bounds, IBarCodeData data);

        [Description("Gets or sets whether to calculate a checksum for the bar code."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase.CalcCheckSum"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), NotifyParentProperty(true), XtraSerializableProperty]
        public virtual bool CalcCheckSum { get; set; }

        [Browsable(false), Description("Gets the name of the bar code type, which is represented by a current class."), XtraSerializableProperty(-1)]
        public string Name { get; }

        protected virtual float YRatio { get; }

        internal bool ForceEnoughSpace { get; set; }

        internal bool DoNotClip { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public abstract BarCodeSymbology SymbologyCode { get; }

        protected IBarCodeData BarCodeData { get; set; }

        protected string FinalText { get; }

        protected internal string DisplayText { get; }

        protected ArrayList Pattern { get; }
    }
}

