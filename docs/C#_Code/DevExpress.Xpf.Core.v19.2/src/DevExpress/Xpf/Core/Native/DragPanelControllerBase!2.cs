namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DragPanelControllerBase<T, TVisual> : DragControllerBase where T: class, IDragPanel where TVisual: FrameworkElement, IDragPanelVisual
    {
        private IEnumerable<FrameworkElement> children;

        protected DragPanelControllerBase();
        protected override bool CanStartDrag(FrameworkElement dragChild);
        protected override Point GetMousePosition();
        protected double GetWidth(FrameworkElement child);
        public virtual void Initialize(T dragPanel);
        private void OnChildrenChanged(object sender, EventArgs e);
        protected override void OnDragFinished();
        public virtual void Uninitialize();

        protected T DragPanel { get; private set; }

        protected TVisual VisualPanel { get; }

        protected IEnumerable<FrameworkElement> Children { get; private set; }

        private SizeHelperBase OrientationHelper { get; }

        protected double CurrentX { get; }

        protected double StartX { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragPanelControllerBase<T, TVisual>.<>c <>9;
            public static Action<T> <>9__22_0;

            static <>c();
            internal void <OnDragFinished>b__22_0(T x);
        }
    }
}

