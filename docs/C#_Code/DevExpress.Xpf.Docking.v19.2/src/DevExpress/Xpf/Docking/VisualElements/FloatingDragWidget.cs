namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows.Controls;

    public class FloatingDragWidget : Border
    {
        public FloatingDragWidget()
        {
            base.SetCurrentValue(WindowChrome.IsHitTestVisibleInChromeProperty, true);
        }
    }
}

