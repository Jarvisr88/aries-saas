namespace DevExpress.Utils
{
    using DevExpress.Utils.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class OptionsLayoutBase : BaseOptions
    {
        [ThreadStatic]
        private static OptionsLayoutBase fullLayout;
        internal SizeF? storedLayoutScaleFactor;
        internal SizeF? currentLayoutScaleFactor;
        private string layoutVersion = "";
        private int assignScaleFactorLocker;

        public override void Assign(BaseOptions source)
        {
            base.Assign(source);
            OptionsLayoutBase base2 = source as OptionsLayoutBase;
            if (base2 != null)
            {
                this.LayoutVersion = base2.LayoutVersion;
            }
        }

        internal void AssignLayoutScaleInfo(OptionsLayoutBase options)
        {
            this.assignScaleFactorLocker++;
            if (options != null)
            {
                this.storedLayoutScaleFactor = options.storedLayoutScaleFactor;
                this.currentLayoutScaleFactor = options.currentLayoutScaleFactor;
            }
        }

        internal void ClearLayoutScaleInfo()
        {
            int assignScaleFactorLocker = this.assignScaleFactorLocker;
            this.assignScaleFactorLocker = assignScaleFactorLocker - 1;
            if (assignScaleFactorLocker <= 0)
            {
                this.storedLayoutScaleFactor = null;
                this.currentLayoutScaleFactor = null;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool ShouldSerializeCore(IComponent owner) => 
            this.ShouldSerialize(owner);

        [Description("Returns an OptionsLayoutBase object whose settings indicate that the full layout of the control should be stored to and restored from storage (a stream, xml file or system registry)."), NotifyParentProperty(true), AutoFormatDisable]
        public static OptionsLayoutBase FullLayout
        {
            get
            {
                fullLayout ??= new OptionsLayoutBase();
                return fullLayout;
            }
        }

        [Browsable(false), Description(""), Category("Scale"), DefaultValue((string) null)]
        public SizeF? StoredLayoutScaleFactor =>
            this.storedLayoutScaleFactor;

        [Browsable(false), Description(""), Category("Scale"), DefaultValue((string) null)]
        public SizeF? CurrentLayoutScaleFactor =>
            this.currentLayoutScaleFactor;

        [Browsable(false), Description(""), Category("Scale"), DefaultValue((string) null)]
        public SizeF? LayoutScaleFactor
        {
            get
            {
                if ((this.currentLayoutScaleFactor != null) && (this.storedLayoutScaleFactor != null))
                {
                    SizeF? storedLayoutScaleFactor = this.StoredLayoutScaleFactor;
                    SizeF ef = storedLayoutScaleFactor.Value;
                    if ((ef.Width <= 0f) || (ef.Height <= 0f))
                    {
                        return null;
                    }
                    SizeF ef2 = new SizeF(this.currentLayoutScaleFactor.Value.Width / ef.Width, this.currentLayoutScaleFactor.Value.Height / ef.Height);
                    if ((ef2.Width <= 0f) || (ef2.Height <= 0f))
                    {
                        return null;
                    }
                    if ((ef2.Width != 1f) || (ef2.Height != 1f))
                    {
                        return new SizeF?(ef2);
                    }
                }
                return null;
            }
        }

        [Description("Gets or sets the version of the layout."), Category("Version"), DefaultValue(""), NotifyParentProperty(true), AutoFormatDisable]
        public string LayoutVersion
        {
            get => 
                this.layoutVersion;
            set => 
                this.layoutVersion = value;
        }
    }
}

