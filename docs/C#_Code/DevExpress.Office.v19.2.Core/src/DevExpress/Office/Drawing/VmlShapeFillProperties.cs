namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class VmlShapeFillProperties : ISupportsCopyFrom<VmlShapeFillProperties>, ICloneable<VmlShapeFillProperties>
    {
        private readonly IDocumentModel documentModel;
        private readonly VmlIntermediateColors colors;
        private readonly VmlFocusPosition focusPosition;
        private readonly VmlFocusSize focusSize;

        public VmlShapeFillProperties(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.Color = DXColor.White;
            this.Color2 = DXColor.White;
            this.Opacity = 1f;
            this.Opacity2 = 1f;
            this.Focus = 0f;
            this.Recolor = false;
            this.Rotate = false;
            this.Method = VmlFillMethod.Sigma;
            this.Type = VmlFillType.Solid;
            this.focusPosition = new VmlFocusPosition();
            this.colors = new VmlIntermediateColors();
            this.focusSize = new VmlFocusSize();
        }

        public VmlShapeFillProperties Clone()
        {
            VmlShapeFillProperties properties = new VmlShapeFillProperties(this.documentModel);
            properties.CopyFrom(this);
            return properties;
        }

        private void CopyEmbeddedImage(OfficeImage source)
        {
            this.EmbeddedImage = source?.Clone(this.documentModel);
        }

        public void CopyFrom(VmlShapeFillProperties source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.Angle = source.Angle;
            this.Color = source.Color;
            this.Color2 = source.Color2;
            this.Opacity = source.Opacity;
            this.Opacity2 = source.Opacity2;
            this.Recolor = source.Recolor;
            this.Rotate = source.Rotate;
            this.Method = source.Method;
            this.Type = source.Type;
            this.Title = source.Title;
            this.Focus = source.Focus;
            this.Filled = source.Filled;
            this.OriginX = source.OriginX;
            this.OriginY = source.OriginY;
            this.SizeX = source.SizeX;
            this.SizeY = source.SizeY;
            this.PositionX = source.PositionX;
            this.PositionY = source.PositionY;
            this.Aspect = source.Aspect;
            this.FocusPosition.CopyFrom(source.FocusPosition);
            this.FocusSize.CopyFrom(source.FocusSize);
            this.Colors.CopyFrom(source.Colors);
            this.CopyEmbeddedImage(source.EmbeddedImage);
        }

        public bool? Filled { get; set; }

        public float Angle { get; set; }

        public System.Drawing.Color Color { get; set; }

        public System.Drawing.Color Color2 { get; set; }

        public float Opacity { get; set; }

        public float Opacity2 { get; set; }

        public bool Recolor { get; set; }

        public bool Rotate { get; set; }

        public VmlFillMethod Method { get; set; }

        public VmlFillType Type { get; set; }

        public float Focus { get; set; }

        public VmlFocusPosition FocusPosition =>
            this.focusPosition;

        public VmlFocusSize FocusSize =>
            this.focusSize;

        public VmlIntermediateColors Colors =>
            this.colors;

        public string Title { get; set; }

        public float OriginX { get; set; }

        public float OriginY { get; set; }

        public float SizeX { get; set; }

        public float SizeY { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public VmlImageAspect Aspect { get; set; }

        public OfficeImage EmbeddedImage { get; set; }
    }
}

