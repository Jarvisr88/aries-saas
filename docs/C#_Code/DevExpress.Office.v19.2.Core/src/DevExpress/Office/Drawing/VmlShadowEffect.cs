namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class VmlShadowEffect : ISupportsCopyFrom<VmlShadowEffect>, ICloneable<VmlShadowEffect>
    {
        private IDocumentModel documentModel;
        private bool isDefault = true;

        public VmlShadowEffect(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.Color = DXColor.Gray;
            this.Color2 = DXColor.LightGray;
            this.Matrix = new VmlMatrix();
            int x = documentModel.UnitConverter.PointsToModelUnits(2);
            this.Offset = new VmlCoordUnit(x, x);
            this.Offset2 = new VmlCoordUnit(-x, -x);
            this.Obscured = false;
            this.Opacity = 1f;
            this.ShadowType = VmlShadowType.Single;
        }

        public VmlShadowEffect Clone()
        {
            VmlShadowEffect effect = new VmlShadowEffect(this.documentModel);
            effect.CopyFrom(this);
            return effect;
        }

        public void CopyFrom(VmlShadowEffect source)
        {
            this.Color = source.Color;
            this.Color2 = source.Color2;
            this.Matrix.CopyFrom(source.Matrix);
            this.Obscured = source.Obscured;
            this.Offset.CopyFrom(source.Offset);
            this.Offset2.CopyFrom(source.Offset2);
            this.OriginX = source.OriginX;
            this.OriginY = source.OriginY;
            this.ShadowType = source.ShadowType;
            this.On = source.On;
            this.Opacity = source.Opacity;
            this.isDefault = source.isDefault;
        }

        public System.Drawing.Color Color { get; set; }

        public System.Drawing.Color Color2 { get; set; }

        public VmlMatrix Matrix { get; private set; }

        public VmlCoordUnit Offset { get; private set; }

        public VmlCoordUnit Offset2 { get; private set; }

        public float OriginX { get; set; }

        public float OriginY { get; set; }

        public VmlShadowType ShadowType { get; set; }

        public float Opacity { get; set; }

        public bool Obscured { get; set; }

        public bool On { get; set; }

        public bool IsDefault
        {
            get => 
                this.isDefault;
            set => 
                this.isDefault = value;
        }
    }
}

