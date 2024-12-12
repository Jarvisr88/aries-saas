namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Input;

    public class TabControlStretchViewLocalDragPanelController : LocalDragPanelController<TabControlStretchViewDragPanelBase, TabPanelStretchView>
    {
        protected override bool IsMouseLeftButtonDownOnDragChild(object sender, MouseButtonEventArgs e);

        protected override bool StartDragDropOnHandledMouseEvents { get; }
    }
}

