namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(ProgressBarBrickExporter))]
    public class ProgressBarBrick : VisualBrick, IProgressBarBrick, IVisualBrick, IBaseBrick, IBrick
    {
        private int position;
        private object textValue;
        private Color? fillColor;

        public ProgressBarBrick()
        {
        }

        public ProgressBarBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        public ProgressBarBrick(int position)
        {
            this.position = position;
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

        [Description("Gets or sets the current position of the progress bar brick."), XtraSerializableProperty, DefaultValue(0)]
        public int Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        protected internal int ValidPosition =>
            Math.Max(0, Math.Min(this.position, 100));

        [Description("Gets or sets the color of progress bars displayed in the current brick.")]
        public Color ForeColor
        {
            get => 
                base.Style.ForeColor;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeForeColor(base.Style, value);
        }

        [Description("Gets or sets the color to fill a progress bar brick's content."), XtraSerializableProperty, DefaultValue((string) null)]
        public Color? FillColor
        {
            get => 
                this.fillColor;
            set => 
                this.fillColor = value;
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "ProgressBar";
    }
}

