namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;

    public abstract class FilletShapeBase : ClosedShapeBase
    {
        private int fillet;

        protected FilletShapeBase()
        {
        }

        protected FilletShapeBase(FilletShapeBase source)
        {
            this.fillet = source.Fillet;
        }

        protected override int GetFilletValueInPercents() => 
            this.fillet;

        protected override ILinesAdjuster GetLinesAdjuster() => 
            ShortestLineLinesAdjuster.Instance;

        [Description("Gets or sets a value specifying how shape corners are rounded (in percent)."), DefaultValue(0), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Shape.FilletShapeBase.Fillet"), XtraSerializableProperty]
        public int Fillet
        {
            get => 
                this.fillet;
            set => 
                this.fillet = ShapeHelper.ValidatePercentageValue(value, "Fillet");
        }
    }
}

