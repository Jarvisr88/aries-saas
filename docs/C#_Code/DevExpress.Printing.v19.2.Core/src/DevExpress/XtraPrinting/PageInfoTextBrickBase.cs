namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(PageInfoTextBrickBaseExporter))]
    public abstract class PageInfoTextBrickBase : TextBrick
    {
        private int startPageNumber;
        private string format;
        private DevExpress.XtraPrinting.PageInfo pageInfo;
        protected readonly DateTime dateTime;

        public PageInfoTextBrickBase()
        {
            this.startPageNumber = 1;
            this.format = string.Empty;
            this.dateTime = DateTimeHelper.Now;
            this.XlsExportNativeFormat = DefaultBoolean.False;
        }

        public PageInfoTextBrickBase(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.startPageNumber = 1;
            this.format = string.Empty;
            this.dateTime = DateTimeHelper.Now;
            this.XlsExportNativeFormat = DefaultBoolean.False;
        }

        public PageInfoTextBrickBase(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor, foreColor)
        {
            this.startPageNumber = 1;
            this.format = string.Empty;
            this.dateTime = DateTimeHelper.Now;
            this.XlsExportNativeFormat = DefaultBoolean.False;
        }

        protected internal override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            this.Text = this.GetTextInfo(this.PrintingSystem, page);
            return base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
        }

        internal abstract string GetTextInfo(PrintingSystemBase ps, IPageItem drawingPage);
        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            this.Text = this.GetTextInfo(context.PrintingSystem, context.DrawingPage);
            if (this.AutoWidthCore && base.IsInitialized)
            {
                SizeF ef = context.Measurer.MeasureString(this.Text, base.Font, GraphicsUnit.Document);
                this.Width = base.Padding.IsEmpty ? (ef.Width + (GraphicsUnitConverter.DipToDoc((float) 1f) * base.GetScaleFactor(context))) : base.Padding.InflateWidth(ef.Width, 300f);
            }
            base.PerformLayout(context);
        }

        protected string ActualTextValue =>
            string.IsNullOrEmpty(this.TextValue as string) ? string.Empty : ((string) this.TextValue);

        protected int StartPageIndex =>
            Math.Max(0, this.startPageNumber - 1);

        [Description("Gets or sets the format string to display text information within the brick."), DefaultValue("")]
        public virtual string Format
        {
            get => 
                this.format;
            set => 
                this.format = value;
        }

        [Description("Gets or sets the type of information to be displayed within the PageInfoTextBrick."), DefaultValue(0)]
        public virtual DevExpress.XtraPrinting.PageInfo PageInfo
        {
            get => 
                this.pageInfo;
            set => 
                this.pageInfo = value;
        }

        [Description("Gets or sets the initial value of a page counter."), DefaultValue(1)]
        public virtual int StartPageNumber
        {
            get => 
                this.startPageNumber;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("StartPageNumber");
                }
                this.startPageNumber = value;
            }
        }

        [Description("For internal use. Specifies the format settings that are applied to a document when it is exported to XLS format."), XtraSerializableProperty, DefaultValue(1)]
        public DefaultBoolean XlsExportNativeFormat
        {
            get => 
                base.XlsExportNativeFormat;
            set => 
                base.XlsExportNativeFormat = value;
        }

        protected virtual bool AutoWidthCore =>
            false;
    }
}

