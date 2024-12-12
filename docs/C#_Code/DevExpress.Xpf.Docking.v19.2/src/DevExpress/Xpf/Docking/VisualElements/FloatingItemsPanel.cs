namespace DevExpress.Xpf.Docking.VisualElements
{
    using System.Windows;

    public class FloatingItemsPanel : GroupPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (!DocumentSelectorPreview.GetIsInPreview(this))
            {
                base.ArrangeOverride(finalSize);
            }
            return new Size();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (!DocumentSelectorPreview.GetIsInPreview(this))
            {
                base.MeasureOverride(availableSize);
            }
            return new Size();
        }
    }
}

