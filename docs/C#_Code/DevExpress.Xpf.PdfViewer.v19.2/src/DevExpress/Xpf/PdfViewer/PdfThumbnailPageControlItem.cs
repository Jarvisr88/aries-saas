namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class PdfThumbnailPageControlItem : PageControlItem
    {
        public static readonly DependencyProperty IsHighlightedProperty;

        static PdfThumbnailPageControlItem()
        {
            Type type = typeof(PdfThumbnailPageControlItem);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailPageControlItem), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            IsHighlightedProperty = DependencyPropertyRegistrator.Register<PdfThumbnailPageControlItem, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailPageControlItem, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailPageControlItem.get_IsHighlighted)), parameters), false);
        }

        public PdfThumbnailPageControlItem()
        {
            base.DefaultStyleKey = typeof(PdfThumbnailPageControlItem);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            IPage dataContext = base.DataContext as IPage;
            return (((base.DocumentViewer == null) || (dataContext == null)) ? base.MeasureOverride(constraint) : (base.IsVertical ? new Size(dataContext.PageSize.Width * base.DocumentViewer.ZoomFactor, ((dataContext.PageSize.Height * base.DocumentViewer.ZoomFactor) + base.DocumentViewer.VerticalPageSpacing) + dataContext.Margin.Top) : new Size(dataContext.PageSize.Height * base.DocumentViewer.ZoomFactor, ((dataContext.PageSize.Width * base.DocumentViewer.ZoomFactor) + base.DocumentViewer.VerticalPageSpacing) + dataContext.Margin.Right)));
        }

        public bool IsHighlighted
        {
            get => 
                (bool) base.GetValue(IsHighlightedProperty);
            set => 
                base.SetValue(IsHighlightedProperty, value);
        }
    }
}

