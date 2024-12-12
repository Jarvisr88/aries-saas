namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.ComponentModel;

    [BrickExporter(typeof(ZipCodeBrickExporter))]
    public class ZipCodeBrick : TextBrickBase
    {
        private int segmentWidth;

        public ZipCodeBrick() : this(NullBrickOwner.Instance)
        {
        }

        public ZipCodeBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override object TextValue
        {
            get => 
                base.TextValue;
            set => 
                base.TextValue = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string TextValueFormatString
        {
            get => 
                base.TextValueFormatString;
            set => 
                base.TextValueFormatString = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string XlsxFormatString
        {
            get => 
                base.XlsxFormatString;
            set => 
                base.XlsxFormatString = value;
        }

        [Description("Gets or sets the width of the lines that the numbers in a zip code brick are drawn with."), XtraSerializableProperty, DefaultValue(0)]
        public int SegmentWidth
        {
            get => 
                this.segmentWidth;
            set => 
                this.segmentWidth = value;
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "ZipCode";
    }
}

