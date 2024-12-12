namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;

    public class PageInfoTextBrick : PageInfoTextBrickBase
    {
        private DefaultBoolean continuousPageNumbering;

        public PageInfoTextBrick()
        {
            this.continuousPageNumbering = DefaultBoolean.Default;
        }

        public PageInfoTextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.continuousPageNumbering = DefaultBoolean.Default;
        }

        public PageInfoTextBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor, foreColor)
        {
            this.continuousPageNumbering = DefaultBoolean.Default;
        }

        protected internal override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            bool flag = base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
            page.PageNumberFormat = GetPageNumberFormat(this.Format, this.PageInfo);
            page.PageNumberOffset = this.StartPageNumber;
            page.PageInfo = this.PageInfo;
            return flag;
        }

        internal static int GetPageCountFromPS(PrintingSystemBase ps, int basePageNumber, DefaultBoolean continuousPageNumbering, IPageItem drawingPage) => 
            (ps != null) ? (((drawingPage != null) ? (ToBoolean(continuousPageNumbering, ps.ContinuousPageNumbering) ? drawingPage.PageCount : drawingPage.OriginalPageCount) : 1) + basePageNumber) : 0x3e8;

        private int GetPageNumber(bool psContinuousPageNumbering, IPageItem drawingPage)
        {
            int num = 0;
            if (drawingPage != null)
            {
                num = ToBoolean(this.continuousPageNumbering, psContinuousPageNumbering) ? drawingPage.Index : drawingPage.OriginalIndex;
            }
            return (num + this.StartPageNumber);
        }

        internal static string GetPageNumberFormat(string format, DevExpress.XtraPrinting.PageInfo pageInfo)
        {
            Match match = new Regex("{0[^}]*}").Match(!string.IsNullOrWhiteSpace(format) ? format : pageInfo.GetDefaultStringFormat());
            return ((match != null) ? match.Value : "{0}");
        }

        internal override string GetTextInfo(PrintingSystemBase ps, IPageItem drawingPage)
        {
            PageInfoDataProviderBase service = ps.GetService<PageInfoDataProviderBase>();
            if (service != null)
            {
                string text = service.GetText(ps, this);
                if (text != null)
                {
                    return text;
                }
            }
            if (this.PageInfo != DevExpress.XtraPrinting.PageInfo.DateTime)
            {
                IStreamingDocument document = ps.Document as IStreamingDocument;
                return ((document == null) ? this.PageInfo.GetText(this.Format, this.GetPageNumber(ps.ContinuousPageNumbering, drawingPage), GetPageCountFromPS(ps, base.StartPageIndex, this.continuousPageNumbering, drawingPage), base.ActualTextValue) : this.PageInfo.GetText(this.Format, this.GetPageNumber(ps.ContinuousPageNumbering, drawingPage), document.BuiltPageCount, base.ActualTextValue));
            }
            object[] values = new object[] { base.dateTime, base.ActualTextValue };
            return this.PageInfo.FormatValues(ps.Culture, this.Format, values);
        }

        [Description("Gets or sets a value indicating whether the page number should be changed according to reordering in the Pages collection."), XtraSerializableProperty, DefaultValue(2)]
        public DefaultBoolean ContinuousPageNumbering
        {
            get => 
                this.continuousPageNumbering;
            set => 
                this.continuousPageNumbering = value;
        }

        [Description("Gets or sets the text to be displayed within the current brick."), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override string Text
        {
            get => 
                base.Text;
            set => 
                base.Text = value;
        }

        [Description("Gets or sets the format string to display text information within the brick."), XtraSerializableProperty]
        public override string Format
        {
            get => 
                base.Format;
            set => 
                base.Format = value;
        }

        [Description("Gets or sets the type of information to be displayed within the PageInfoTextBrick."), XtraSerializableProperty]
        public override DevExpress.XtraPrinting.PageInfo PageInfo
        {
            get => 
                base.PageInfo;
            set => 
                base.PageInfo = value;
        }

        [Description("Gets or sets the initial value of a page counter."), XtraSerializableProperty]
        public override int StartPageNumber
        {
            get => 
                base.StartPageNumber;
            set => 
                base.StartPageNumber = value;
        }

        [Description("Gets the text string containing the brick type information.")]
        public override string BrickType =>
            "PageInfoText";
    }
}

