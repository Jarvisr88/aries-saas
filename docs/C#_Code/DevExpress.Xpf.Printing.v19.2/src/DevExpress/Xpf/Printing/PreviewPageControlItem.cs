namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows;

    public class PreviewPageControlItem : PageControlItem
    {
        private Size constraintSize = Size.Empty;

        public PreviewPageControlItem()
        {
            base.DefaultStyleKey = typeof(PreviewPageControlItem);
        }

        protected override Size ArrangeOverride(Size arrangeBounds) => 
            base.ArrangeOverride(this.constraintSize);

        protected override Size MeasureOverride(Size constraint)
        {
            Size size;
            this.constraintSize = size = base.MeasureOverride(constraint);
            return size;
        }

        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IPage newValue = e.NewValue as IPage;
            this.UpdateMargin((newValue != null) ? newValue.Margin : new Thickness(0.0));
        }

        protected override void OnIsCoverPageChanged(bool newValue)
        {
            this.UpdateMargin((base.DataContext != null) ? ((IPage) base.DataContext).Margin : new Thickness(0.0));
        }

        protected override void OnPositionChanged(PageControlItemPosition newValue)
        {
            this.UpdateMargin((base.DataContext != null) ? ((IPage) base.DataContext).Margin : new Thickness(0.0));
        }

        private void UpdateMargin(Thickness margin)
        {
            base.Margin = margin;
        }
    }
}

