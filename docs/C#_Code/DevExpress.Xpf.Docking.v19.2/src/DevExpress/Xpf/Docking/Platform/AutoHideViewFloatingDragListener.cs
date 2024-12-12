namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;

    public class AutoHideViewFloatingDragListener : LayoutViewFloatingDragListener
    {
        protected override IFloatingHelper CreateFloatingHelper() => 
            new AutoHideFloatingHelper(base.View);
    }
}

