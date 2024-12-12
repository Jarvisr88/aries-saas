namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(LabelBrickExporter))]
    public class LabelBrick : TextBrick, ILabelBrick, ITextBrick, IVisualBrick, IBaseBrick, IBrick
    {
        public LabelBrick() : this(NullBrickOwner.Instance)
        {
        }

        public LabelBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        private LabelBrick(LabelBrick labelBrick) : base(labelBrick)
        {
            this.Angle = labelBrick.Angle;
        }

        public override object Clone() => 
            new LabelBrick(this);

        [Description("Gets or sets the rotation angle of the text displayed within the brick."), XtraSerializableProperty, DefaultValue((float) 0f)]
        public float Angle
        {
            get => 
                base.GetValue<float>(BrickAttachedProperties.Angle, 0f);
            set => 
                base.SetAttachedValue<float>(BrickAttachedProperties.Angle, value, 0f);
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool CanShrinkAndGrow { get; set; }

        [Description("Gets a value indicating whether text within a label brick is drawn vertically.")]
        public bool IsVerticalTextMode =>
            Math.Abs(Math.Sin((3.1415926535897931 * this.RealAngle) / 180.0)) == 1.0;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Label";

        private float RealAngle =>
            -this.Angle;
    }
}

