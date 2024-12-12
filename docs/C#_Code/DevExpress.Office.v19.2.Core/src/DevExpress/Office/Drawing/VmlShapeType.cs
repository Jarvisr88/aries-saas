namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class VmlShapeType : ISupportsCopyFrom<VmlShapeType>
    {
        public const string DefaultID = "_x0000_t202";
        public const int DefaultCoordSize = 0x5460;
        private IDocumentModel documentModel;

        public VmlShapeType(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.Initialize();
        }

        private void CopyFill(VmlShapeFillProperties source)
        {
            this.Fill = source?.Clone();
        }

        private void CopyFormulas(VmlSingleFormulasCollection source)
        {
            this.Formulas = source?.Clone();
        }

        public void CopyFrom(VmlShapeType source)
        {
            Guard.ArgumentNotNull(source, "source");
            Array.Copy(source.AdjustValues, this.AdjustValues, 8);
            this.Spt = source.Spt;
            this.BlackAndWhiteMode = source.BlackAndWhiteMode;
            this.CoordOrigin.CopyFrom(source.CoordOrigin);
            this.CoordSize.CopyFrom(source.CoordSize);
            this.CopyFill(source.Fill);
            this.CopyStroke(source.Stroke);
            this.StrokeColor = source.StrokeColor;
            this.Stroked = source.Stroked;
            this.FillColor = source.FillColor;
            this.Filled = source.Filled;
            this.Path = source.Path;
            this.CopyFormulas(source.Formulas);
            this.CopyShapeProtections(source.ShapeProtections);
            this.CopyShadowEffect(source.ShadowEffect);
            this.CopyShapePath(source.ShapePath);
        }

        private void CopyShadowEffect(VmlShadowEffect source)
        {
            this.ShadowEffect = source?.Clone();
        }

        private void CopyShapePath(VmlShapePath source)
        {
            this.ShapePath = source?.Clone();
        }

        private void CopyShapeProtections(VmlShapeProtections source)
        {
            this.ShapeProtections = source?.Clone();
        }

        private void CopyStroke(VmlLineStrokeSettings source)
        {
            this.Stroke = source?.Clone();
        }

        public static VmlShapeType CreateCommentShapeType(IDocumentModel documentModel)
        {
            VmlShapeType type = new VmlShapeType(documentModel) {
                Id = "_x0000_t202",
                CoordSize = { 
                    X = 0x5460,
                    Y = 0x5460
                },
                Spt = 202f,
                Path = "m,l,21600r21600,l21600,xe",
                Stroke = new VmlLineStrokeSettings(documentModel)
            };
            type.Stroke.JoinStyle = VmlStrokeJoinStyle.Miter;
            type.ShapePath = new VmlShapePath();
            type.ShapePath.GradientShapeOk = true;
            type.ShapePath.ConnectType = VmlConnectType.Rect;
            return type;
        }

        public bool GetFilled()
        {
            bool? filledCore = this.GetFilledCore();
            return ((filledCore != null) ? filledCore.GetValueOrDefault() : true);
        }

        protected bool? GetFilledCore() => 
            ((this.ShapePath == null) || this.ShapePath.FillOk) ? (((this.Fill == null) || (this.Fill.Filled == null)) ? this.Filled : new bool?(this.Fill.Filled.Value)) : false;

        public string GetPath() => 
            ((this.ShapePath == null) || string.IsNullOrEmpty(this.ShapePath.Path)) ? this.Path : this.ShapePath.Path;

        public bool GetShadowed() => 
            ((this.ShapePath == null) || this.ShapePath.ShadowOk) ? ((this.ShadowEffect != null) && this.ShadowEffect.On) : false;

        public bool GetStroked()
        {
            bool? strokedCore = this.GetStrokedCore();
            return ((strokedCore != null) ? strokedCore.GetValueOrDefault() : true);
        }

        public bool? GetStrokedCore() => 
            ((this.ShapePath == null) || this.ShapePath.StrokeOk) ? (((this.Stroke == null) || (this.Stroke.Stroked == null)) ? this.Stroked : new bool?(this.Stroke.Stroked.Value)) : false;

        private void Initialize()
        {
            this.AdjustValues = new int?[8];
            this.CoordSize = new VmlCoordUnit(0x5460, 0x5460);
            this.CoordOrigin = new VmlCoordUnit();
        }

        public VmlCoordUnit CoordSize { get; private set; }

        public VmlCoordUnit CoordOrigin { get; private set; }

        public string Id { get; set; }

        public float Spt { get; set; }

        public string Path { get; set; }

        public VmlLineStrokeSettings Stroke { get; set; }

        public VmlShapeFillProperties Fill { get; set; }

        public VmlShapePath ShapePath { get; set; }

        public VmlSingleFormulasCollection Formulas { get; set; }

        public VmlShapeProtections ShapeProtections { get; set; }

        public VmlShadowEffect ShadowEffect { get; set; }

        public int?[] AdjustValues { get; set; }

        public bool? Filled { get; set; }

        public bool? Stroked { get; set; }

        public bool? PreferRelative { get; set; }

        public VmlBlackAndWhiteMode BlackAndWhiteMode { get; set; }

        public Color FillColor { get; set; }

        public Color StrokeColor { get; set; }

        public int StrokeWeight { get; set; }
    }
}

