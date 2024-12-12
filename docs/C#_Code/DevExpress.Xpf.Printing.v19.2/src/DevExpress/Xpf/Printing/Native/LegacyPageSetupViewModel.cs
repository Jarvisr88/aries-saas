namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class LegacyPageSetupViewModel
    {
        private readonly Margins minMargins;
        private float leftMargin;
        private float topMargin;
        private float rightMargin;
        private float bottomMargin;
        private float width;
        private float height;
        private readonly string displayFormat;
        private readonly string mask;
        private float customWidth = 1f;
        private float customHeight = 1f;

        public LegacyPageSetupViewModel()
        {
            this.displayFormat = this.GetUsersIsMetric() ? ("{0} " + PreviewLocalizer.GetString(PreviewStringId.Margin_Millimeter)) : "{0} ''";
            this.mask = this.GetUsersIsMetric() ? "d" : "f";
            this.minMargins = new Margins(0, 0, 0, 0);
            this.AvailablePaperKinds = this.GetPaperKinds();
        }

        public MarginsF GetMargins(float dpi) => 
            new MarginsFloat(GraphicsUnitConverter.Convert(this.leftMargin, (float) 1f, dpi), GraphicsUnitConverter.Convert(this.rightMargin, (float) 1f, dpi), GraphicsUnitConverter.Convert(this.topMargin, (float) 1f, dpi), GraphicsUnitConverter.Convert(this.bottomMargin, (float) 1f, dpi));

        protected virtual IEnumerable<System.Drawing.Printing.PaperKind> GetPaperKinds() => 
            Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)).Cast<System.Drawing.Printing.PaperKind>().ToArray<System.Drawing.Printing.PaperKind>();

        public SizeF GetSizeF(float dpi) => 
            new SizeF(GraphicsUnitConverter.Convert(this.width, (float) 1f, dpi), GraphicsUnitConverter.Convert(this.height, (float) 1f, dpi));

        internal virtual bool GetUsersIsMetric() => 
            RegionInfo.CurrentRegion.IsMetric;

        private float InchesToMarginsUnit(float value) => 
            GraphicsUnitConverter.Convert(value, (float) 1f, this.MarginsUnit);

        private float MarginsUnitToInches(float value) => 
            GraphicsUnitConverter.Convert(value, this.MarginsUnit, (float) 1f);

        protected void OnPaperKindChanged()
        {
            if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
            {
                this.width = this.customWidth;
                this.height = this.customHeight;
            }
            else
            {
                SizeF pageSizeInInches = this.PageSizeInInches;
                this.width = pageSizeInInches.Width;
                this.height = pageSizeInInches.Height;
            }
            this.RaisePropertiesChanged();
        }

        public void SetMargins(float left, float top, float right, float bottom, float dpi)
        {
            this.LeftMargin = GraphicsUnitConverter.Convert(left, dpi, this.MarginsUnit);
            this.TopMargin = GraphicsUnitConverter.Convert(top, dpi, this.MarginsUnit);
            this.RightMargin = GraphicsUnitConverter.Convert(right, dpi, this.MarginsUnit);
            this.BottomMargin = GraphicsUnitConverter.Convert(bottom, dpi, this.MarginsUnit);
        }

        internal void SetPageSize(SizeF pageSize, float dpi)
        {
            this.PageWidth = GraphicsUnitConverter.Convert(pageSize.Width, dpi, this.MarginsUnit);
            this.PageHeight = GraphicsUnitConverter.Convert(pageSize.Height, dpi, this.MarginsUnit);
            this.customWidth = this.width;
            this.customHeight = this.height;
        }

        public virtual IEnumerable<System.Drawing.Printing.PaperKind> AvailablePaperKinds { get; protected set; }

        public virtual System.Drawing.Printing.PaperKind PaperKind { get; set; }

        [BindableProperty]
        public virtual float PageWidth
        {
            get => 
                this.InchesToMarginsUnit(this.width);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num <= 10000f) && (num >= 1f))
                {
                    this.width = num;
                    if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
                    {
                        this.customWidth = this.width;
                    }
                }
            }
        }

        public virtual float PageHeight
        {
            get => 
                this.InchesToMarginsUnit(this.height);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num <= 10000f) && (num >= 1f))
                {
                    this.height = num;
                    if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
                    {
                        this.customHeight = this.height;
                    }
                }
            }
        }

        public bool CanChangePageSize =>
            this.PaperKind == System.Drawing.Printing.PaperKind.Custom;

        public virtual bool Landscape { get; set; }

        [BindableProperty]
        public virtual float LeftMargin
        {
            get => 
                this.InchesToMarginsUnit(this.leftMargin);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num >= this.minMargins.Left) && ((this.leftMargin + num) < this.PageSizeInInches.Width))
                {
                    this.leftMargin = num;
                }
            }
        }

        [BindableProperty]
        public virtual float TopMargin
        {
            get => 
                this.InchesToMarginsUnit(this.topMargin);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num >= this.minMargins.Top) && ((this.leftMargin + num) < this.PageSizeInInches.Height))
                {
                    this.topMargin = num;
                }
            }
        }

        [BindableProperty]
        public virtual float RightMargin
        {
            get => 
                this.InchesToMarginsUnit(this.rightMargin);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num >= this.minMargins.Right) && ((this.leftMargin + num) < this.PageSizeInInches.Width))
                {
                    this.rightMargin = num;
                }
            }
        }

        [BindableProperty]
        public virtual float BottomMargin
        {
            get => 
                this.InchesToMarginsUnit(this.bottomMargin);
            set
            {
                float num = this.MarginsUnitToInches(value);
                if ((num >= this.minMargins.Bottom) && ((this.topMargin + num) < this.PageSizeInInches.Height))
                {
                    this.bottomMargin = num;
                }
            }
        }

        public string DisplayFormat =>
            this.displayFormat;

        public string Mask =>
            this.mask;

        private SizeF PageSizeInInches =>
            PageSizeInfo.GetPageSizeF(this.PaperKind, 1f);

        private float MarginsUnit =>
            this.GetUsersIsMetric() ? 25.4f : 1f;
    }
}

