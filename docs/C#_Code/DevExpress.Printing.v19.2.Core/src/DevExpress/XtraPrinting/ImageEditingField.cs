namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ImageEditingField : EditingField
    {
        internal ImageEditingField() : this(null)
        {
        }

        public ImageEditingField(VisualBrick brick) : base(brick)
        {
            this.EditorName = string.Empty;
            if (this.ImageBrick != null)
            {
                this.StoreInitialImageBrickState(this.ImageBrick);
            }
        }

        protected override void AfterLoadBrick()
        {
            base.AfterLoadBrick();
            this.StoreInitialImageBrickState(this.ImageBrick);
        }

        internal override void AssignEditValueToBrick(VisualBrick brick)
        {
            DevExpress.XtraPrinting.ImageBrick brick2 = brick as DevExpress.XtraPrinting.ImageBrick;
            if (brick2 != null)
            {
                brick2.ImageSource = this.ImageBrick.ImageSource;
                brick2.SizeMode = this.ImageBrick.SizeMode;
                brick2.ImageAlignment = this.ImageBrick.ImageAlignment;
            }
        }

        private void StoreInitialImageBrickState(DevExpress.XtraPrinting.ImageBrick imageBrick)
        {
            this.InitialImageSource = imageBrick.ImageSource;
            this.InitialImageSizeMode = imageBrick.SizeMode;
            this.InitialImageAlignment = imageBrick.ImageAlignment;
        }

        [DefaultValue(""), XtraSerializableProperty]
        public string EditorName { get; set; }

        private DevExpress.XtraPrinting.ImageBrick ImageBrick =>
            (DevExpress.XtraPrinting.ImageBrick) base.Brick;

        public DevExpress.XtraPrinting.Drawing.ImageSource InitialImageSource { get; private set; }

        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource
        {
            get => 
                this.ImageBrick.ImageSource;
            set
            {
                if (!ReferenceEquals(this.ImageBrick.ImageSource, value))
                {
                    this.ImageBrick.ImageSource = value;
                    base.RaiseEditValueChanged(EventArgs.Empty);
                }
            }
        }

        public DevExpress.XtraPrinting.ImageSizeMode ImageSizeMode
        {
            get => 
                this.ImageBrick.SizeMode;
            set
            {
                if (this.ImageBrick.SizeMode != value)
                {
                    this.ImageBrick.SizeMode = value;
                    base.RaiseEditValueChanged(EventArgs.Empty);
                }
            }
        }

        public DevExpress.XtraPrinting.ImageAlignment ImageAlignment
        {
            get => 
                this.ImageBrick.ImageAlignment;
            set
            {
                if (this.ImageBrick.ImageAlignment != value)
                {
                    this.ImageBrick.ImageAlignment = value;
                    base.RaiseEditValueChanged(EventArgs.Empty);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override object EditValue
        {
            get => 
                this.ImageSource;
            set => 
                this.ImageSource = (value is SvgImage) ? new DevExpress.XtraPrinting.Drawing.ImageSource((SvgImage) value) : ((value is Image) ? new DevExpress.XtraPrinting.Drawing.ImageSource((Image) value) : (value as DevExpress.XtraPrinting.Drawing.ImageSource));
        }

        internal DevExpress.XtraPrinting.ImageSizeMode InitialImageSizeMode { get; private set; }

        internal DevExpress.XtraPrinting.ImageAlignment InitialImageAlignment { get; private set; }
    }
}

