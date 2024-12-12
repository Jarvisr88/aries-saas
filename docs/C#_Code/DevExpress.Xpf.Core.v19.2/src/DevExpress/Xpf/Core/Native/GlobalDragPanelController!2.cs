namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Threading;

    public class GlobalDragPanelController<T, TVisual> : LocalDragPanelController<T, TVisual> where T: class, IDragPanel where TVisual: FrameworkElement, IDragPanelVisual
    {
        private DragWidgetWindow DragWidget;
        private DispatcherTimer startDragTimer;
        private bool shouldStartDrag;
        private Window currentWindow;

        protected override bool CanStartDrag(FrameworkElement dragChild);
        protected override Point GetMousePosition();
        private void InitializeAndShowThumb(DragWidgetWindow dragWidget, FrameworkElement dragObject);
        private bool IsSupportedWindow(Window w);
        protected override void OnDrag();
        protected override void OnDragStarted();
        protected override void OnDragStopped();
        protected override void OnDrop();
        private void OnGlobalDrag();
        private void OnGlobalDrop();
        private void OnStartDragTimerTick(object sender, EventArgs e);
        private void StartDragTimer();
        private void StartGlobalDrag();
        private void StartLocalDrag(TVisual visualPanel);
        private void UninitializeAndCloseThumb();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GlobalDragPanelController<T, TVisual>.<>c <>9;
            public static Func<Window, bool> <>9__16_0;
            public static Action<Window> <>9__16_1;
            public static Func<FrameworkElement, Window> <>9__17_0;

            static <>c();
            internal Window <IsSupportedWindow>b__17_0(FrameworkElement x);
            internal bool <OnGlobalDrag>b__16_0(Window x);
            internal void <OnGlobalDrag>b__16_1(Window x);
        }
    }
}

