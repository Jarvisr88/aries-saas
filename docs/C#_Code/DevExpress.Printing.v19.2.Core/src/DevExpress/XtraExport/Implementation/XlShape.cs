namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class XlShape : XlDrawingObject, IXlHyperlinkOwner
    {
        private readonly XlDrawingFill fill = new XlDrawingFill();
        private readonly XlOutline outline = new XlOutline();
        private readonly XlGraphicFrame frame = new XlGraphicFrame();
        private readonly XlPictureHyperlink hyperlinkClick = new XlPictureHyperlink();

        protected XlShape(XlGeometryPreset geometryPreset)
        {
            this.GeometryPreset = geometryPreset;
        }

        public static XlShape Line(XlColor color, XlOutlineDashing dashing = 0)
        {
            if (color.IsAutoOrEmpty)
            {
                throw new ArgumentException("Line color is auto or empty!");
            }
            return new XlShape(XlGeometryPreset.Line) { Outline = { 
                Color = color,
                Dashing = dashing
            } };
        }

        public static XlShape Rectangle(XlColor fillColor, XlColor outlineColor, XlOutlineDashing outlineDashing = 0)
        {
            if (fillColor.ColorType == XlColorType.Auto)
            {
                throw new ArgumentException("Fill color is auto!");
            }
            if (outlineColor.ColorType == XlColorType.Auto)
            {
                throw new ArgumentException("Outline color is auto!");
            }
            if ((fillColor.ColorType == XlColorType.Empty) && (outlineColor.ColorType == XlColorType.Empty))
            {
                throw new ArgumentException("Fill and outline colors are empty!");
            }
            XlShape shape = new XlShape(XlGeometryPreset.Rect) {
                Outline = { 
                    Color = outlineColor,
                    Dashing = outlineDashing
                }
            };
            if (fillColor.ColorType != XlColorType.Empty)
            {
                shape.Fill.FillType = XlDrawingFillType.Solid;
                shape.Fill.Color = fillColor;
            }
            return shape;
        }

        protected internal int Id { get; set; }

        public XlGeometryPreset GeometryPreset { get; private set; }

        public XlDrawingFill Fill =>
            this.fill;

        public XlOutline Outline =>
            this.outline;

        public XlGraphicFrame Frame =>
            this.frame;

        public XlPictureHyperlink HyperlinkClick =>
            this.hyperlinkClick;

        internal override XlDrawingObjectType DrawingObjectType =>
            XlDrawingObjectType.Shape;

        internal bool IsConnector =>
            this.GeometryPreset == XlGeometryPreset.Line;
    }
}

