namespace DevExpress.Xpf.Docking.VisualElements
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class psvTabCaption : TextBlock
    {
        protected override Geometry GetLayoutClip(Size layoutSlotSize) => 
            base.ClipToBounds ? base.GetLayoutClip(layoutSlotSize) : null;
    }
}

