namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;

    internal class DragCursorWindowContainer : FloatingWindowContainer
    {
        protected override WindowContentHolder CreateWindow() => 
            new DragCursorWindow(this);
    }
}

