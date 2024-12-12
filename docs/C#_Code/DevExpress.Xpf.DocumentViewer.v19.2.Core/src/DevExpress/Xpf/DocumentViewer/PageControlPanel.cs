namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class PageControlPanel : StackPanel
    {
        public static readonly DependencyProperty PageWrapperProperty = DependencyProperty.Register("PageWrapper", typeof(DevExpress.Xpf.DocumentViewer.PageWrapper), typeof(PageControlPanel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.PageWrapper == null)
            {
                return base.ArrangeOverride(finalSize);
            }
            foreach (Control control in base.InternalChildren)
            {
                Size desiredSize = control.DesiredSize;
                IPage dataContext = (IPage) control.DataContext;
                Rect pageRect = this.PageWrapper.GetPageRect(dataContext);
                bool flag = this.PageWrapper.Pages.First<IPage>() == dataContext;
                Rect finalRect = new Rect(new Point((pageRect.Left - base.HorizontalOffset) - (flag ? dataContext.Margin.Left : (this.PageWrapper.HorizontalPageSpacing / 2.0)), 0.0), desiredSize);
                control.Arrange(finalRect);
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            return new Size(Math.Max(this.DocumentViewer.ActualBehaviorProvider.Viewport.Width, size.Width), size.Height);
        }

        private IDocumentViewerControl DocumentViewer =>
            DocumentViewerControl.GetActualViewer(this);

        public DevExpress.Xpf.DocumentViewer.PageWrapper PageWrapper
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PageWrapper) base.GetValue(PageWrapperProperty);
            set => 
                base.SetValue(PageWrapperProperty, value);
        }
    }
}

