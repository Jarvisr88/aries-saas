namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ShapeBracket : ShapeBase
    {
        private int tipLength;
        protected int fFillet;
        protected int fTailLength;

        public ShapeBracket()
        {
            this.tipLength = 20;
        }

        protected ShapeBracket(ShapeBracket source)
        {
            this.tipLength = 20;
            this.tipLength = source.TipLength;
        }

        protected override RectangleF AdjustClientRectangle(RectangleF clientBounds, float lineWidth) => 
            DeflateLineWidth(clientBounds, lineWidth);

        protected override ShapeBase CloneShape() => 
            new ShapeBracket(this);

        protected internal override ShapeCommandCollection CreateCommands(RectangleF bounds, int angle) => 
            ShapeHelper.CreateBraceCommands(bounds, this.fTailLength, this.TipLength, this.fFillet);

        [Description("Gets or sets the length of a brace's tip."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeBracket.TipLength"), DefaultValue(20), XtraSerializableProperty]
        public int TipLength
        {
            get => 
                this.tipLength;
            set => 
                this.tipLength = ShapeHelper.ValidatePercentageValue(value, "TipLength");
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Bracket;

        protected internal override bool SupportsFillColor =>
            false;
    }
}

