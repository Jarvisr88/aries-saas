namespace DevExpress.Xpf.Editors.Internal
{
    using System.Windows;
    using System.Windows.Controls;

    public class EmptySizePanel : Grid
    {
        protected override Size ArrangeOverride(Size finalSize) => 
            base.ArrangeOverride(finalSize);

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            return new Size(0.0, size.Height);
        }
    }
}

