namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;

    public class ThumbnailPageControlItem : PageControlItem
    {
        static ThumbnailPageControlItem()
        {
            Type type = typeof(ThumbnailPageControlItem);
        }

        public ThumbnailPageControlItem()
        {
            base.DefaultStyleKey = typeof(ThumbnailPageControlItem);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            IPage dataContext = base.DataContext as IPage;
            return (((base.DocumentViewer == null) || (dataContext == null)) ? base.MeasureOverride(constraint) : (base.IsVertical ? new Size(dataContext.PageSize.Width * base.DocumentViewer.ZoomFactor, (dataContext.PageSize.Height * base.DocumentViewer.ZoomFactor) + base.DocumentViewer.VerticalPageSpacing) : new Size(dataContext.PageSize.Height * base.DocumentViewer.ZoomFactor, (dataContext.PageSize.Width * base.DocumentViewer.ZoomFactor) + base.DocumentViewer.VerticalPageSpacing)));
        }
    }
}

