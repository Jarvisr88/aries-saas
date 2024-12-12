namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [POCOViewModel]
    public class PageSetupViewModel : ViewModelBase, IPageSetupViewModel
    {
        private readonly Locker updatePropertiesLocker;
        private float customWidth;
        private float customHeight;
        private GraphicsUnit currentUnit;
        private static readonly SizeF MinPageSize = PrintingDocument.MinPageSize;

        public PageSetupViewModel(ReadonlyPageData pageData) : this(pageData, RegionInfo.CurrentRegion.IsMetric ? GraphicsUnit.Millimeter : GraphicsUnit.Inch)
        {
        }

        public PageSetupViewModel(XtraPageSettingsBase pageSettings) : this(pageSettings, RegionInfo.CurrentRegion.IsMetric ? GraphicsUnit.Millimeter : GraphicsUnit.Inch)
        {
        }

        public PageSetupViewModel(ReadonlyPageData pageData, GraphicsUnit unit)
        {
            this.updatePropertiesLocker = new Locker();
            this.customWidth = 1f;
            this.customHeight = 1f;
            this.Unit = unit;
            this.Landscape = pageData.Landscape;
            this.PaperKind = pageData.PaperKind;
            this.SetPageSize(pageData.PageSize, 300f);
            this.SetMargins(pageData.MarginsF.Left, pageData.MarginsF.Top, pageData.MarginsF.Right, pageData.MarginsF.Bottom, 300f);
            this.currentUnit = unit;
            this.DisplayFormat = this.GetFormatString(unit);
            this.Mask = this.GetMask(unit);
            this.PaperSizes = PageSizeInfo.CreatePaperSizeCollection(true).Cast<PaperSize>();
        }

        public PageSetupViewModel(XtraPageSettingsBase pageSettings, GraphicsUnit unit)
        {
            this.updatePropertiesLocker = new Locker();
            this.customWidth = 1f;
            this.customHeight = 1f;
            this.Unit = unit;
            this.Landscape = pageSettings.Landscape;
            this.PaperKind = pageSettings.PaperKind;
            this.SetPageSize(pageSettings.Data.PageSize, 300f);
            this.SetMargins(pageSettings.Data.MarginsF.Left, pageSettings.Data.MarginsF.Top, pageSettings.Data.MarginsF.Right, pageSettings.Data.MarginsF.Bottom, 300f);
            this.currentUnit = unit;
            this.DisplayFormat = this.GetFormatString(unit);
            this.Mask = this.GetMask(unit);
            this.PaperSizes = PageSizeInfo.CreatePaperSizeCollection(true).Cast<PaperSize>();
        }

        private string GetFormatString(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.Pixel:
                    return "{0:0} px";

                case GraphicsUnit.Inch:
                    return "{0:0.##} ''";

                case GraphicsUnit.Millimeter:
                    return "{0:0} mm";
            }
            throw new InvalidOperationException();
        }

        private string GetMask(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.Pixel:
                case GraphicsUnit.Millimeter:
                    return "d";

                case GraphicsUnit.Inch:
                    return "f";
            }
            throw new InvalidOperationException();
        }

        private float GetPageHeightOnPreview() => 
            this.Landscape ? this.PageSizeInUnits.Width : this.PageSizeInUnits.Height;

        private float GetPageWidthOnPreview() => 
            this.Landscape ? this.PageSizeInUnits.Height : this.PageSizeInUnits.Width;

        protected void OnBottomMarginChanged()
        {
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                float num = GraphicsUnitConverter.Convert(MinPageSize.Height, (float) 300f, this.MarginsUnit);
                float num2 = Math.Min(Math.Max(0f, this.BottomMargin), (this.PaperKind == System.Drawing.Printing.PaperKind.Custom) ? ((this.customHeight - this.TopMargin) - num) : ((this.GetPageHeightOnPreview() - this.TopMargin) - num));
                if (num2 != this.BottomMargin)
                {
                    this.BottomMargin = num2;
                }
            });
        }

        protected void OnLandscapeChanged()
        {
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                float paperWidth = this.PaperWidth;
                this.PaperWidth = this.PaperHeight;
                this.PaperHeight = paperWidth;
                if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
                {
                    this.customWidth = this.PaperWidth;
                    this.customHeight = this.PaperHeight;
                }
                if (this.Landscape)
                {
                    float leftMargin = this.LeftMargin;
                    this.LeftMargin = this.BottomMargin;
                    this.BottomMargin = this.RightMargin;
                    this.RightMargin = this.TopMargin;
                    this.TopMargin = leftMargin;
                }
                else
                {
                    float topMargin = this.TopMargin;
                    this.TopMargin = this.RightMargin;
                    this.RightMargin = this.BottomMargin;
                    this.BottomMargin = this.LeftMargin;
                    this.LeftMargin = topMargin;
                }
            });
        }

        protected void OnLeftMarginChanged()
        {
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                float num = GraphicsUnitConverter.Convert(MinPageSize.Width, (float) 300f, this.MarginsUnit);
                float num2 = Math.Min(Math.Max(0f, this.LeftMargin), (this.PaperKind == System.Drawing.Printing.PaperKind.Custom) ? ((this.customWidth - this.RightMargin) - num) : ((this.GetPageWidthOnPreview() - this.RightMargin) - num));
                if (num2 != this.LeftMargin)
                {
                    this.LeftMargin = num2;
                }
            });
        }

        protected void OnPaperHeightChanged()
        {
            if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
            {
                this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                    if (this.PaperHeight > 2.147484E+09f)
                    {
                        this.PaperHeight = 2.147484E+09f;
                    }
                    else if (this.PaperHeight < 0f)
                    {
                        this.PaperHeight = 0f;
                    }
                    this.ValidateMargins(this.customWidth, this.customHeight);
                    this.customHeight = this.PaperHeight;
                });
            }
        }

        protected void OnPaperKindChanged()
        {
            float prevWidth = this.PaperWidth;
            float prevHeight = this.PaperHeight;
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
                {
                    this.PaperWidth = this.customWidth;
                    this.PaperHeight = this.customHeight;
                }
                else
                {
                    SizeF pageSizeInUnits = this.PageSizeInUnits;
                    this.PaperWidth = pageSizeInUnits.Width;
                    this.PaperHeight = pageSizeInUnits.Height;
                }
                this.ValidateMargins(prevWidth, prevHeight);
            });
        }

        protected void OnPaperWidthChanged()
        {
            if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
            {
                this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                    if (this.PaperWidth > 2.147484E+09f)
                    {
                        this.PaperWidth = 2.147484E+09f;
                    }
                    else if (this.PaperWidth < 0f)
                    {
                        this.PaperWidth = 0f;
                    }
                    this.ValidateMargins(this.customWidth, this.customHeight);
                    this.customWidth = this.PaperWidth;
                });
            }
        }

        protected void OnRightMarginChanged()
        {
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                float num = GraphicsUnitConverter.Convert(MinPageSize.Width, (float) 300f, this.MarginsUnit);
                float num2 = Math.Min(Math.Max(0f, this.RightMargin), (this.PaperKind == System.Drawing.Printing.PaperKind.Custom) ? ((this.customWidth - this.LeftMargin) - num) : ((this.GetPageWidthOnPreview() - this.LeftMargin) - num));
                if (num2 != this.RightMargin)
                {
                    this.RightMargin = num2;
                }
            });
        }

        protected void OnTopMarginChanged()
        {
            this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                float num = GraphicsUnitConverter.Convert(MinPageSize.Height, (float) 300f, this.MarginsUnit);
                float num2 = Math.Min(Math.Max(0f, this.TopMargin), (this.PaperKind == System.Drawing.Printing.PaperKind.Custom) ? ((this.customHeight - this.BottomMargin) - num) : ((this.GetPageHeightOnPreview() - this.BottomMargin) - num));
                if (num2 != this.TopMargin)
                {
                    this.TopMargin = num2;
                }
            });
        }

        protected void OnUnitChanged()
        {
            if (this.Unit != this.currentUnit)
            {
                this.updatePropertiesLocker.DoLockedActionIfNotLocked(delegate {
                    this.DisplayFormat = this.GetFormatString(this.Unit);
                    this.BottomMargin = GraphicsUnitConverter.Convert(this.BottomMargin, this.currentUnit, this.Unit);
                    this.LeftMargin = GraphicsUnitConverter.Convert(this.LeftMargin, this.currentUnit, this.Unit);
                    this.RightMargin = GraphicsUnitConverter.Convert(this.RightMargin, this.currentUnit, this.Unit);
                    this.TopMargin = GraphicsUnitConverter.Convert(this.TopMargin, this.currentUnit, this.Unit);
                    this.PaperWidth = GraphicsUnitConverter.Convert(this.PaperWidth, this.currentUnit, this.Unit);
                    this.PaperHeight = GraphicsUnitConverter.Convert(this.PaperHeight, this.currentUnit, this.Unit);
                    this.customWidth = GraphicsUnitConverter.Convert(this.customWidth, this.currentUnit, this.Unit);
                    this.customHeight = GraphicsUnitConverter.Convert(this.customHeight, this.currentUnit, this.Unit);
                    this.currentUnit = this.Unit;
                });
            }
        }

        internal void SetMargins(float left, float top, float right, float bottom, float dpi)
        {
            this.LeftMargin = GraphicsUnitConverter.Convert(left, dpi, this.MarginsUnit);
            this.TopMargin = GraphicsUnitConverter.Convert(top, dpi, this.MarginsUnit);
            this.RightMargin = GraphicsUnitConverter.Convert(right, dpi, this.MarginsUnit);
            this.BottomMargin = GraphicsUnitConverter.Convert(bottom, dpi, this.MarginsUnit);
        }

        internal void SetPageSize(SizeF pageSize, float dpi)
        {
            this.PaperWidth = GraphicsUnitConverter.Convert(pageSize.Width, dpi, this.MarginsUnit);
            this.PaperHeight = GraphicsUnitConverter.Convert(pageSize.Height, dpi, this.MarginsUnit);
            this.customWidth = this.PaperWidth;
            this.customHeight = this.PaperHeight;
        }

        private void ValidateMargins(float prevWidth, float prevHeight)
        {
            if (((this.PaperHeight > 0f) && (this.PaperWidth > 0f)) && (((this.LeftMargin + this.RightMargin) > this.PaperWidth) || ((this.TopMargin + this.BottomMargin) > this.PaperHeight)))
            {
                float num = Math.Min((float) (this.PaperWidth / prevWidth), (float) (this.PaperHeight / prevHeight));
                this.LeftMargin *= num;
                this.RightMargin *= num;
                this.TopMargin *= num;
                this.BottomMargin *= num;
            }
        }

        public virtual IEnumerable<PaperSize> PaperSizes { get; set; }

        public virtual System.Drawing.Printing.PaperKind PaperKind { get; set; }

        public virtual bool Landscape { get; set; }

        public virtual string DisplayFormat { get; set; }

        public string Mask { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="OnUnitChanged")]
        public virtual GraphicsUnit Unit { get; set; }

        protected virtual float MarginsUnit =>
            RegionInfo.CurrentRegion.IsMetric ? 25.4f : 1f;

        private SizeF PageSizeInUnits =>
            PageSizeInfo.GetPageSizeF(this.PaperKind, this.MarginsUnit);

        [BindableProperty(OnPropertyChangedMethodName="OnPaperWidthChanged")]
        public virtual float PaperWidth { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnPaperHeightChanged")]
        public virtual float PaperHeight { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnLeftMarginChanged")]
        public virtual float LeftMargin { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnTopMarginChanged")]
        public virtual float TopMargin { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnRightMarginChanged")]
        public virtual float RightMargin { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnBottomMarginChanged")]
        public virtual float BottomMargin { get; set; }

        [BindableProperty]
        public virtual bool EnableUnitsEditor { get; set; }
    }
}

