namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;

    public class ShapeBrace : ShapeBracket
    {
        public ShapeBrace()
        {
            base.fFillet = 50;
            base.fTailLength = 20;
        }

        private ShapeBrace(ShapeBrace source) : base(source)
        {
            base.fFillet = source.Fillet;
            base.fTailLength = source.TailLength;
        }

        protected override ShapeBase CloneShape() => 
            new ShapeBrace(this);

        [Description("Gets or sets a value which specifies how brace corners are rounded."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeBrace.Fillet"), DefaultValue(50), XtraSerializableProperty]
        public int Fillet
        {
            get => 
                base.fFillet;
            set => 
                base.fFillet = ShapeHelper.ValidatePercentageValue(value, "Fillet");
        }

        [Description("Gets or sets the length of a brace's tail."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.ShapeBrace.TailLength"), DefaultValue(20), XtraSerializableProperty]
        public int TailLength
        {
            get => 
                base.fTailLength;
            set => 
                base.fTailLength = ShapeHelper.ValidatePercentageValue(value, "TailLength");
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Brace;
    }
}

