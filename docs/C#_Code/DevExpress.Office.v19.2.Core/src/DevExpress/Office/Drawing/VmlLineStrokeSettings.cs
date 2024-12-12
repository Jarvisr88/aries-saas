namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class VmlLineStrokeSettings : ISupportsCopyFrom<VmlLineStrokeSettings>, ICloneable<VmlLineStrokeSettings>
    {
        private readonly IDocumentModel documentModel;

        public VmlLineStrokeSettings(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "worksheet");
            this.documentModel = documentModel;
            this.Color = DXColor.Black;
            this.Color2 = DXColor.White;
            this.JoinStyle = VmlStrokeJoinStyle.Round;
            this.FillType = VmlFillType.Solid;
            this.Opacity = 1f;
            this.StrokeWeight = documentModel.UnitConverter.PointsToModelUnits(1);
        }

        public VmlLineStrokeSettings Clone()
        {
            VmlLineStrokeSettings settings = new VmlLineStrokeSettings(this.documentModel);
            settings.CopyFrom(this);
            return settings;
        }

        private void CopyEmbeddedImage(OfficeImage source)
        {
            this.EmbeddedImage = source?.Clone(this.documentModel);
        }

        public void CopyFrom(VmlLineStrokeSettings source)
        {
            this.Color = source.Color;
            this.Opacity = source.Opacity;
            this.Color2 = source.Color2;
            this.JoinStyle = source.JoinStyle;
            this.Title = source.Title;
            this.FillType = source.FillType;
            this.DashStyle = source.DashStyle;
            this.LineStyle = source.LineStyle;
            this.CopyEmbeddedImage(source.EmbeddedImage);
            this.Stroked = source.Stroked;
            this.StrokeWeight = source.StrokeWeight;
            this.StartArrowType = source.StartArrowType;
            this.StartArrowLength = source.StartArrowLength;
            this.StartArrowWidth = source.StartArrowWidth;
            this.EndArrowType = source.EndArrowType;
            this.EndArrowLength = source.EndArrowLength;
            this.EndArrowWidth = source.EndArrowWidth;
        }

        public bool? Stroked { get; set; }

        public int StrokeWeight { get; set; }

        public System.Drawing.Color Color { get; set; }

        public float Opacity { get; set; }

        public System.Drawing.Color Color2 { get; set; }

        public VmlStrokeJoinStyle JoinStyle { get; set; }

        public VmlFillType FillType { get; set; }

        public string Title { get; set; }

        public VmlDashStyle DashStyle { get; set; }

        public VmlLineStyle LineStyle { get; set; }

        public OfficeImage EmbeddedImage { get; set; }

        public VMLStrokeArrowType StartArrowType { get; set; }

        public VMLStrokeArrowLength StartArrowLength { get; set; }

        public VMLStrokeArrowWidth StartArrowWidth { get; set; }

        public VMLStrokeArrowType EndArrowType { get; set; }

        public VMLStrokeArrowLength EndArrowLength { get; set; }

        public VMLStrokeArrowWidth EndArrowWidth { get; set; }

        public IDocumentModel DocumentModel =>
            this.documentModel;
    }
}

