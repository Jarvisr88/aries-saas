namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XtraPageSettingsBase : IPageSettings, IDisposable
    {
        public const System.Drawing.Printing.PaperKind DefaultPaperKind = System.Drawing.Printing.PaperKind.Letter;
        internal static readonly string DefaultPaperName = "";
        protected PrintingSystemBase ps;
        private PageData data;
        private string printerName;
        private bool rollPaper;

        internal XtraPageSettingsBase(PrintingSystemBase ps)
        {
            this.ps = ps;
        }

        public static bool ApplyPageSettings(XtraPageSettingsBase pageSettings, System.Drawing.Printing.PaperKind paperKind, Size customPaperSize, System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, bool landscape) => 
            ApplyPageSettings(pageSettings, paperKind, customPaperSize, margins, minMargins, landscape, DefaultPaperName);

        public static bool ApplyPageSettings(XtraPageSettingsBase pageSettings, System.Drawing.Printing.PaperKind paperKind, Size customPaperSize, System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, bool landscape, string paperName)
        {
            if ((paperKind == System.Drawing.Printing.PaperKind.Custom) && !customPaperSize.IsEmpty)
            {
                pageSettings.Assign(margins, minMargins, paperKind, customPaperSize, landscape, paperName);
                return true;
            }
            Size pageSize = PageSizeInfo.GetPageSize(paperKind, Size.Empty);
            if (pageSize.IsEmpty)
            {
                return false;
            }
            pageSettings.Assign(margins, minMargins, paperKind, pageSize, landscape);
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Assign(PageData val)
        {
            this.data = val;
            this.ps.OnPageSettingsChanged();
        }

        public void Assign(PageSettings pageSettings, System.Drawing.Printing.Margins minMargins)
        {
            try
            {
                PageData val = CreatePageData(pageSettings, minMargins);
                this.Assign(val);
            }
            catch
            {
            }
        }

        public virtual void Assign(System.Drawing.Printing.Margins margins, System.Drawing.Printing.PaperKind paperKind, bool landscape)
        {
            try
            {
                this.Assign(new PageData(margins, EmptyMargins, paperKind, PageSizeInfo.GetPageSize(paperKind), landscape));
            }
            catch
            {
            }
        }

        public void Assign(DevExpress.XtraPrinting.Native.MarginsF margins, System.Drawing.Printing.PaperKind paperKind, SizeF pageSize, bool landscape)
        {
            try
            {
                SizeF ef = GraphicsUnitConverter.Convert(pageSize, (float) 300f, (float) 100f);
                PageData val = new PageData(margins, paperKind, landscape);
                val.SizeF = ef;
                val.PageSize = pageSize;
                this.Assign(val);
            }
            catch
            {
            }
        }

        public void Assign(System.Drawing.Printing.Margins margins, System.Drawing.Printing.PaperKind paperKind, Size paperSize, bool landscape)
        {
            try
            {
                this.Assign(new PageData(margins, EmptyMargins, paperKind, paperSize, landscape));
            }
            catch
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void Assign(System.Drawing.Printing.Margins margins, System.Drawing.Printing.PaperKind paperKind, string paperName, bool landscape)
        {
            this.ps.Extender.Assign(margins, paperKind, paperName, landscape);
        }

        public void Assign(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, System.Drawing.Printing.PaperKind paperKind, Size pageSize, bool landscape)
        {
            try
            {
                this.Assign(new PageData(margins, minMargins, paperKind, pageSize, landscape));
            }
            catch
            {
            }
        }

        public void Assign(DevExpress.XtraPrinting.Native.MarginsF margins, DevExpress.XtraPrinting.Native.MarginsF minMargins, System.Drawing.Printing.PaperKind paperKind, Size pageSize, bool landscape, string paperName)
        {
            try
            {
                PageData val = new PageData(margins, minMargins, paperKind, pageSize, landscape) {
                    PaperName = paperName
                };
                this.Assign(val);
            }
            catch
            {
            }
        }

        public void Assign(System.Drawing.Printing.Margins margins, System.Drawing.Printing.Margins minMargins, System.Drawing.Printing.PaperKind paperKind, Size pageSize, bool landscape, string paperName)
        {
            try
            {
                PageData val = new PageData(margins, minMargins, paperKind, pageSize, landscape) {
                    PaperName = paperName
                };
                this.Assign(val);
            }
            catch
            {
            }
        }

        public void AssignDefaultPageSettings()
        {
            this.Assign(DefaultMargins, EmptyMargins, System.Drawing.Printing.PaperKind.Letter, PageSizeInfo.GetPageSize(System.Drawing.Printing.PaperKind.Letter), false);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void AssignPrinterSettings(string printerName, string paperName, PrinterSettingsUsing settingsUsing)
        {
            this.ps.Extender.AssignPrinterSettings(printerName, paperName, settingsUsing);
        }

        protected virtual PageData CreateData() => 
            new PageData();

        protected static PageData CreatePageData(PageSettings sets, System.Drawing.Printing.Margins minMargins) => 
            new PageData(sets.Margins, minMargins, sets.PaperSize.Kind, PageData.ToSize(sets.PaperSize), sets.Landscape);

        public virtual void Dispose()
        {
        }

        private static int GetScaledValue(int value, double scale) => 
            (int) Math.Round((double) (value * scale), MidpointRounding.AwayFromZero);

        private float GetValidBottomMargin(float topMargin, float bottomMargin, float minBottomMargin)
        {
            float min = minBottomMargin + this.ps.Document.PageFootBounds.Bottom;
            return this.GetValidValue(bottomMargin, min, (this.PageSize.Height - topMargin) - this.MinUsefulPageHeight);
        }

        private float GetValidLeftMargin(float rightMargin, float leftMargin, float minLeftMargin)
        {
            float max = (this.PageSize.Width - rightMargin) - this.MinUsefulPageWidth;
            return this.GetValidValue(leftMargin, minLeftMargin, max);
        }

        private System.Drawing.Printing.Margins GetValidMargins(System.Drawing.Printing.Margins margins, Size size, bool landscape)
        {
            int height;
            int width;
            if (landscape)
            {
                height = size.Height;
                width = size.Width;
            }
            else
            {
                height = size.Width;
                width = size.Height;
            }
            if (((height - margins.Left) - margins.Right) < this.MinUsefulPageWidth)
            {
                double scale = (height - this.MinUsefulPageWidth) / ((float) (margins.Left + margins.Right));
                margins.Left = GetScaledValue(margins.Left, scale);
                margins.Right = GetScaledValue(margins.Right, scale);
            }
            if (((width - margins.Top) - margins.Bottom) < this.MinUsefulPageHeight)
            {
                double scale = (width - this.MinUsefulPageHeight) / ((float) (margins.Top + margins.Bottom));
                margins.Top = GetScaledValue(margins.Top, scale);
                margins.Bottom = GetScaledValue(margins.Bottom, scale);
            }
            return margins;
        }

        private float GetValidRightMargin(float leftMargin, float rightMargin, float minRightMargin)
        {
            float max = (this.PageSize.Width - leftMargin) - this.MinUsefulPageWidth;
            return this.GetValidValue(rightMargin, minRightMargin, max);
        }

        private float GetValidTopMargin(float bottomMargin, float topMargin, float minTopMargin)
        {
            float min = minTopMargin + this.ps.Document.PageHeadBounds.Bottom;
            return this.GetValidValue(topMargin, min, (this.PageSize.Height - bottomMargin) - this.MinUsefulPageHeight);
        }

        private float GetValidValue(float value, float min, float max)
        {
            max = Math.Max(min, max);
            return Math.Min(max, Math.Max(min, value));
        }

        private void RestoreCore(XtraSerializer serializer, object path)
        {
            serializer.DeserializeObject(this, path, "XtraPrintingPageSettings");
            this.IsPreset = true;
        }

        public void RestoreFromRegistry(string path)
        {
            this.RestoreCore(new RegistryXtraSerializer(), path);
        }

        public void RestoreFromStream(Stream stream)
        {
            this.RestoreCore(new XmlXtraSerializer(), stream);
        }

        public void RestoreFromXml(string xmlFile)
        {
            this.RestoreCore(new XmlXtraSerializer(), xmlFile);
        }

        private void SaveCore(XtraSerializer serializer, object path)
        {
            serializer.SerializeObject(this, path, "XtraPrintingPageSettings");
        }

        public void SaveToRegistry(string path)
        {
            this.SaveCore(new RegistryXtraSerializer(), path);
        }

        public void SaveToStream(Stream stream)
        {
            this.SaveCore(new XmlXtraSerializer(), stream);
        }

        public void SaveToXml(string xmlFile)
        {
            this.SaveCore(new XmlXtraSerializer(), xmlFile);
        }

        private void SetBottomMargin(float value)
        {
            this.MarginsF.Bottom = this.GetValidBottomMargin(this.MarginsF.Top, value, this.MinMarginsF.Bottom);
        }

        private void SetLeftMargin(float value)
        {
            this.MarginsF.Left = this.GetValidLeftMargin(this.MarginsF.Right, value, this.MinMarginsF.Left);
        }

        internal void SetMarginsInHundredthsOfInch(DevExpress.XtraPrinting.Native.MarginsF marginsInHundredthsOfInch)
        {
            DevExpress.XtraPrinting.Native.MarginsF objA = (DevExpress.XtraPrinting.Native.MarginsF) this.MarginsF.Clone();
            this.ps.OnBeforeMarginsChange(MarginSide.All, 0f);
            this.SetBottomMargin(marginsInHundredthsOfInch.Bottom);
            this.SetTopMargin(marginsInHundredthsOfInch.Top);
            this.SetLeftMargin(marginsInHundredthsOfInch.Left);
            this.SetRightMargin(marginsInHundredthsOfInch.Right);
            if (!Equals(objA, this.MarginsF))
            {
                this.ps.OnAfterMarginsChange(MarginSide.All, 0f);
            }
        }

        internal void SetPaperSize(PaperSize paperSize)
        {
            PageData val = new PageData(this.Margins, this.MinMargins, paperSize, this.Landscape) {
                PaperName = paperSize.PaperName
            };
            this.Assign(val);
        }

        private void SetRightMargin(float value)
        {
            this.MarginsF.Right = this.GetValidRightMargin(this.MarginsF.Left, value, this.MinMarginsF.Right);
        }

        private void SetTopMargin(float value)
        {
            this.MarginsF.Top = this.GetValidTopMargin(this.MarginsF.Bottom, value, this.MinMarginsF.Top);
        }

        SizeF IPageSettings.PageSize =>
            this.PageSize;

        internal static System.Drawing.Printing.Margins EmptyMargins =>
            new System.Drawing.Printing.Margins(0, 0, 0, 0);

        [Description("Specifies the default minimum margins for a report's pages.")]
        public static System.Drawing.Printing.Margins DefaultMinMargins =>
            new System.Drawing.Printing.Margins(20, 20, 20, 20);

        [Description("Specifies the default margins for a report's pages.")]
        public static System.Drawing.Printing.Margins DefaultMargins =>
            new System.Drawing.Printing.Margins(100, 100, 100, 100);

        internal bool IsPreset { get; set; }

        public string PaperName
        {
            get => 
                this.Data.PaperName;
            set => 
                this.Data.PaperName = value;
        }

        [XtraSerializableProperty]
        public string PrinterName
        {
            get => 
                this.printerName;
            set => 
                this.printerName = value;
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public PageData Data
        {
            get
            {
                this.data ??= this.CreateData();
                return this.data;
            }
        }

        internal DevExpress.XtraPrinting.Native.MarginsF MinMarginsF =>
            this.Data.MinMarginsF;

        [Description("Gets the margins of a report page measured in three hundredths of an inch.")]
        public DevExpress.XtraPrinting.Native.MarginsF MarginsF =>
            this.Data.MarginsF;

        [Description("Gets the width and height (in hundredths of an inch) of the page region that can contain data.")]
        public SizeF UsablePageSize =>
            this.UsablePageRect.Size;

        public SizeF UsablePageSizeInPixels =>
            GraphicsUnitConverter.Convert(this.UsefulPageRectF.Size, (float) 300f, (float) 96f);

        [Description("Gets the margins of a report page.")]
        public System.Drawing.Printing.Margins Margins =>
            this.Data.Margins;

        [Description("Gets the minimum size allowed for a report's margins.")]
        public System.Drawing.Printing.Margins MinMargins =>
            this.Data.MinMargins;

        [Description("Gets the bounds of a report page, taking into account the page orientation specified by the XtraPageSettingsBase.Landscape property.")]
        public Rectangle Bounds =>
            this.Data.Bounds;

        [Description("Gets or sets a value indicating whether the page orientation is landscape.")]
        public bool Landscape
        {
            get => 
                this.Data.Landscape;
            set
            {
                if (this.Data.Landscape != value)
                {
                    Size pageSize = PageSizeInfo.GetPageSize(this.PaperKind);
                    System.Drawing.Printing.Margins margins = this.GetValidMargins(this.Margins, pageSize, value);
                    this.Assign(new PageData(margins, this.MinMargins, this.PaperKind, pageSize, value));
                }
            }
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool RollPaper
        {
            get => 
                this.rollPaper;
            set => 
                this.rollPaper = value;
        }

        [Description("Gets or sets the type of paper for the document.")]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get => 
                this.Data.PaperKind;
            set
            {
                if (this.Data.PaperKind != value)
                {
                    Size pageSize = PageSizeInfo.GetPageSize(value);
                    System.Drawing.Printing.Margins margins = this.GetValidMargins(this.Margins, pageSize, this.Landscape);
                    this.Assign(new PageData(margins, this.MinMargins, value, pageSize, this.Landscape));
                }
            }
        }

        [Description("Gets or sets the left page margin.")]
        public int LeftMargin
        {
            get => 
                this.Margins.Left;
            set => 
                this.LeftMarginF = DevExpress.XtraPrinting.Native.MarginsF.FromHundredths((float) value);
        }

        [Description("Gets or sets the top page margin.")]
        public int TopMargin
        {
            get => 
                this.Margins.Top;
            set => 
                this.TopMarginF = DevExpress.XtraPrinting.Native.MarginsF.FromHundredths((float) value);
        }

        [Description("Gets or sets the right page margin.")]
        public int RightMargin
        {
            get => 
                this.Margins.Right;
            set => 
                this.RightMarginF = DevExpress.XtraPrinting.Native.MarginsF.FromHundredths((float) value);
        }

        [Description("Gets or sets the bottom page margin.")]
        public int BottomMargin
        {
            get => 
                this.Margins.Bottom;
            set => 
                this.BottomMarginF = DevExpress.XtraPrinting.Native.MarginsF.FromHundredths((float) value);
        }

        [Description("Gets or sets the left page margin.")]
        public float LeftMarginF
        {
            get => 
                this.MarginsF.Left;
            set
            {
                float left = this.MarginsF.Left;
                float num2 = DevExpress.XtraPrinting.Native.MarginsF.ToHundredths(value);
                float val = this.ps.OnBeforeMarginsChange(MarginSide.Left, num2);
                this.SetLeftMargin(DevExpress.XtraPrinting.Native.MarginsF.FromHundredths(val));
                if (left != this.MarginsF.Left)
                {
                    this.ps.OnAfterMarginsChange(MarginSide.Left, val);
                }
            }
        }

        [Description("Gets or sets the top page margin.")]
        public float TopMarginF
        {
            get => 
                this.MarginsF.Top;
            set
            {
                float top = this.MarginsF.Top;
                float num2 = DevExpress.XtraPrinting.Native.MarginsF.ToHundredths(value);
                float val = this.ps.OnBeforeMarginsChange(MarginSide.Top, num2);
                this.SetTopMargin(DevExpress.XtraPrinting.Native.MarginsF.FromHundredths(val));
                if (top != this.MarginsF.Top)
                {
                    this.ps.OnAfterMarginsChange(MarginSide.Top, val);
                }
            }
        }

        [Description("Gets or sets the right page margin.")]
        public float RightMarginF
        {
            get => 
                this.MarginsF.Right;
            set
            {
                float right = this.MarginsF.Right;
                float num2 = DevExpress.XtraPrinting.Native.MarginsF.ToHundredths(value);
                float val = this.ps.OnBeforeMarginsChange(MarginSide.Right, num2);
                this.SetRightMargin(DevExpress.XtraPrinting.Native.MarginsF.FromHundredths(val));
                if (right != this.MarginsF.Right)
                {
                    this.ps.OnAfterMarginsChange(MarginSide.Right, val);
                }
            }
        }

        [Description("Gets or sets the bottom page margin.")]
        public float BottomMarginF
        {
            get => 
                this.MarginsF.Bottom;
            set
            {
                float bottom = this.MarginsF.Bottom;
                float num2 = DevExpress.XtraPrinting.Native.MarginsF.ToHundredths(value);
                float val = this.ps.OnBeforeMarginsChange(MarginSide.Bottom, num2);
                this.SetBottomMargin(DevExpress.XtraPrinting.Native.MarginsF.FromHundredths(val));
                if (bottom != this.MarginsF.Bottom)
                {
                    this.ps.OnAfterMarginsChange(MarginSide.Bottom, val);
                }
            }
        }

        protected float MinUsefulPageWidth =>
            Math.Min(this.ps.Document.MinPageWidth, this.MaxUsefulPageWidth);

        protected float MinUsefulPageHeight =>
            Math.Min(this.ps.Document.MinPageHeight, this.MaxUsefulPageHeight);

        internal RectangleF MaxUsefulPageRect =>
            RectangleF.FromLTRB(this.MinMarginsF.Left, this.MinMarginsF.Top, this.PageSize.Width - this.MinMarginsF.Right, this.PageSize.Height - this.MinMarginsF.Bottom);

        internal float MaxUsefulPageWidth =>
            this.MaxUsefulPageRect.Width;

        internal float MaxUsefulPageHeight =>
            this.MaxUsefulPageRect.Height;

        [Description("Gets the rectangle on the page (in hundredths of an inch) that can contain data.")]
        public RectangleF UsablePageRect =>
            RectangleF.FromLTRB((float) this.Margins.Left, (float) this.Margins.Top, (float) (this.Bounds.Width - this.Margins.Right), (float) (this.Bounds.Height - this.Margins.Bottom));

        internal RectangleF UsefulPageRectF =>
            this.Data.UsefulPageRectF;

        internal float UsefulPageWidth =>
            this.Data.UsefulPageWidth;

        internal float UsefulPageHeight =>
            this.Data.UsefulPageHeight;

        internal SizeF PageSize =>
            this.Data.PageSize;

        internal RectangleF PageHeaderRect =>
            this.Data.PageHeaderRect;

        internal RectangleF PageFooterRect =>
            this.Data.PageFooterRect;
    }
}

