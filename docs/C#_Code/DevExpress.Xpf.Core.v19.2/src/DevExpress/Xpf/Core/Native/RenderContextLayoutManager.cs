namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public sealed class RenderContextLayoutManager : DispatcherObject
    {
        [ThreadStatic]
        private static RenderContextLayoutManager current;
        private static readonly Func<object> getMediaContextHandler;
        private static readonly Action<object, DispatcherOperationCallback, object> beginInvokeOnRenderHandler;
        private RenderContextLayoutManager.RenderContextLayoutManagerWrapper wrapper;
        private static readonly DispatcherOperationCallback UpdateCallback;
        private LayoutEventList automationEvents;
        private FrameworkRenderElementContext forceLayoutElement;
        private FrameworkRenderElementContext lastExceptionElement;
        private RenderMeasureQueue measureQueue;
        private RenderArrangeQueue arrangeQueue;
        private EventHandler shutdownHandler;
        internal static int SLayoutRecursionLimit;
        private int arrangesOnStack;
        private int measuresOnStack;
        private static readonly DispatcherOperationCallback updateLayoutBackground;
        public bool isDead;
        private bool isUpdating;
        private bool isInUpdateLayout;
        private bool gotException;
        private bool layoutRequestPosted;

        static RenderContextLayoutManager();
        public RenderContextLayoutManager(IChrome chrome);
        internal void EnterArrange();
        internal void EnterMeasure();
        internal void ExitArrange();
        internal void ExitMeasure();
        public void ForceInvalidateArrangeToRoot(FrameworkRenderElementContext context);
        public void ForceInvalidateMeasureToRoot(FrameworkRenderElementContext context);
        public void ForceLayout(FrameworkRenderElementContext e);
        internal FrameworkRenderElementContext GetLastExceptionElement();
        private Rect GetProperArrangeRect(FrameworkRenderElementContext element);
        private bool HasDirtiness(RenderContextLayoutManagerFlags flags);
        private void InvalidateTreeIfRecovering();
        private bool IsArrangeFlagSet(RenderContextLayoutManagerFlags flags);
        private bool IsMeasureFlagSet(RenderContextLayoutManagerFlags flags);
        private void MarkTreeDirty(FrameworkRenderElementContext e);
        private void MarkTreeDirtyHelper(FrameworkRenderElementContext v);
        public void NeedsRecalc();
        private void OnDispatcherShutdown(object sender, EventArgs e);
        internal void SetLastExceptionElement(FrameworkRenderElementContext e);
        public void UpdateLayout(RenderContextLayoutManagerFlags mode);
        private static object UpdateLayoutBackground(object arg);
        private static object UpdateLayoutCallback(object arg);

        public static RenderContextLayoutManager Current { get; }

        internal RenderLayoutQueue MeasureQueue { get; }

        internal RenderLayoutQueue ArrangeQueue { get; }

        internal LayoutEventList AutomationEvents { get; }

        private class RenderContextLayoutManagerWrapper : UIElement
        {
            private RenderContextLayoutManager rclm;
            private bool fake;

            public RenderContextLayoutManagerWrapper(RenderContextLayoutManager rclm);
            protected override void ArrangeCore(Rect finalRect);
            protected override Size MeasureCore(Size availableSize);
        }
    }
}

