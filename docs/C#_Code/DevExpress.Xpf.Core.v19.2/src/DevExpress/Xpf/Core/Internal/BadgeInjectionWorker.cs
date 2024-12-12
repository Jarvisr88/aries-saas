namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class BadgeInjectionWorker : BadgeWorkerBase
    {
        private Border badgeContainer;
        private VisualBrush brush;
        private FrameworkElement fake;
        private EventHandler contextsChanged;
        private static Func<UIElement, object> get_drawingContent;
        private static Type tRenderDataDrawingContext;
        private static Type tIDrawingContent;
        private static Func<object, object> get_renderData;
        private static Action<object, object> set_renderData;
        private static Action<object, EventHandler, bool> propagateChangedHandler;
        private static Func<Visual, EventHandler> get_ContentsChangedHandler;
        private object currentDrawingContent;

        static BadgeInjectionWorker();
        public BadgeInjectionWorker(FrameworkElement target);
        protected override bool CalculateIsVisibleOverride();
        protected override void Destroy();
        protected override void HideOverride();
        private void OnTargetLayoutUpdated(object sender, EventArgs e);
        protected override void OnTargetSizeChanged(object sender, SizeChangedEventArgs e);
        private void Render(DrawingContext dc);
        protected override void ShowOverride();
        private void UpdateDrawingContent();
    }
}

