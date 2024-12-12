namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    internal class DragCursorWindow : WindowContentHolder
    {
        public DragCursorWindow(BaseFloatingContainer container) : base(container)
        {
        }

        protected override void TrySetOwnerCore(Window containerWindow)
        {
        }
    }
}

