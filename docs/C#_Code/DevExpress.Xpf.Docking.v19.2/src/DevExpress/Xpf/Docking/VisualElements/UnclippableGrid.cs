namespace DevExpress.Xpf.Docking.VisualElements
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class UnclippableGrid : Grid
    {
        protected override Geometry GetLayoutClip(Size layoutSlotSize) => 
            base.ClipToBounds ? base.GetLayoutClip(layoutSlotSize) : null;
    }
}

