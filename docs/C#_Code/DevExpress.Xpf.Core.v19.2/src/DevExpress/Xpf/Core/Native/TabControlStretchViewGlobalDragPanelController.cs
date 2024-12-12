namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Input;

    public class TabControlStretchViewGlobalDragPanelController : GlobalDragPanelController<TabControlStretchViewDragPanelBase, TabPanelStretchView>
    {
        protected override bool IsMouseLeftButtonDownOnDragChild(object sender, MouseButtonEventArgs e);

        protected override bool StartDragDropOnHandledMouseEvents { get; }
    }
}

