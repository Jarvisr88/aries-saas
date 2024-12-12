namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(TrackBarBrickExporter))]
    public class TrackBarBrick : VisualBrick, ITrackBarBrick, IVisualBrick, IBaseBrick, IBrick
    {
        private int position;
        private int min;
        private int max;
        private object textValue;

        public TrackBarBrick()
        {
        }

        public TrackBarBrick(int position, int min, int max)
        {
            this.position = position;
            this.min = min;
            this.max = max;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get => 
                base.Text;
            set => 
                base.Text = value;
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

        [XtraSerializableProperty, DefaultValue((string) null), EditorBrowsable(EditorBrowsableState.Always)]
        public override object TextValue
        {
            get => 
                this.textValue;
            set => 
                this.textValue = value;
        }

        [Description(""), XtraSerializableProperty, DefaultValue(0)]
        public int Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        public int Minimum =>
            this.min;

        public int Maximum =>
            this.max;

        [Description("")]
        public Color ForeColor
        {
            get => 
                base.Style.ForeColor;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeForeColor(base.Style, value);
        }

        [Description("")]
        public override string BrickType =>
            "TrackBar";
    }
}

