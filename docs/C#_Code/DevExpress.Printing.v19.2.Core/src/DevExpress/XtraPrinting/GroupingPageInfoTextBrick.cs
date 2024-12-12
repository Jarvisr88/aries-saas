namespace DevExpress.XtraPrinting
{
    using DevExpress.DocumentView;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(GroupingPageInfoTextBrickExporter))]
    public class GroupingPageInfoTextBrick : PageInfoTextBrickBase
    {
        private object groupingObject;
        private int[] parentPath;

        public GroupingPageInfoTextBrick()
        {
            this.parentPath = new int[0];
        }

        public GroupingPageInfoTextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.parentPath = new int[0];
        }

        public GroupingPageInfoTextBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor, foreColor)
        {
            this.parentPath = new int[0];
        }

        protected internal override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            IStreamingDocument document = this.PrintingSystem.Document as IStreamingDocument;
            if (document != null)
            {
                document.AddBrickToUpdate(this, page.Index);
            }
            return base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
        }

        internal override string GetTextInfo(PrintingSystemBase ps, IPageItem drawingPage)
        {
            if (ps.Document.IsModified)
            {
                return this.Text;
            }
            PageInfoDataProviderBase service = ps.GetService<PageInfoDataProviderBase>();
            if (service != null)
            {
                string text = service.GetText(ps, this);
                if (text != null)
                {
                    return text;
                }
            }
            if (this.PageInfo == PageInfo.DateTime)
            {
                object[] values = new object[] { base.dateTime, base.ActualTextValue };
                return this.PageInfo.FormatValues(ps.Culture, this.Format, values);
            }
            GroupingManager manager = ps.GetService<GroupingManager>();
            if ((manager == null) && (this.GroupingObject == null))
            {
                return this.Text;
            }
            int pageIndex = (drawingPage != null) ? drawingPage.Index : -1;
            GroupingInfo info = ((manager == null) || ((pageIndex < 0) || (this.GroupingObject == null))) ? null : manager.GetGroupingInfo(this.GroupingObject, this.parentPath, pageIndex);
            int num2 = (info != null) ? (info.EndPageIndex - info.BeginPageIndex) : 0;
            int num3 = (info != null) ? (pageIndex - info.BeginPageIndex) : 0;
            string str = this.PageInfo.GetText(this.Format, num3 + this.StartPageNumber, num2 + this.StartPageNumber, base.ActualTextValue);
            return (string.IsNullOrEmpty(str) ? this.Text : str);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public object GroupingObject
        {
            get => 
                this.groupingObject;
            set => 
                this.groupingObject = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int[] ParentPath
        {
            get => 
                this.parentPath;
            set => 
                this.parentPath = value;
        }

        [Description("")]
        public override string BrickType =>
            "GroupingPageInfoText";
    }
}

