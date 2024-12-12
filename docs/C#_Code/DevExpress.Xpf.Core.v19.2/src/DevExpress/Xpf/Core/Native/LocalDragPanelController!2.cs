namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class LocalDragPanelController<T, TVisual> : DragPanelControllerBase<T, TVisual> where T: class, IDragPanel where TVisual: FrameworkElement, IDragPanelVisual
    {
        private FrameworkElement OverChild;
        private IEnumerable<double> ChildXs;
        private double DragChildMinOffset;
        private double DragChildMaxOffset;
        protected internal bool SuppressAnimation;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ChildStoryboardProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ChildOffsetProperty;

        static LocalDragPanelController();
        private void CleanUpOffset();
        private static double GetChildOffset(FrameworkElement obj);
        private static Storyboard GetChildStoryboard(FrameworkElement obj);
        private void GetCoordinates();
        protected virtual double GetDragChildMaxOffset();
        protected virtual double GetDragChildMinOffset();
        protected virtual double GetDragOffset();
        private FrameworkElement GetOverChild();
        protected override void OnDrag();
        protected override void OnDragStarted();
        protected override void OnDragStopped();
        protected override void OnDrop();
        private static void SetChildOffset(FrameworkElement obj, double value);
        private static void SetChildStoryboard(FrameworkElement obj, Storyboard value);
        private void SetOffset(FrameworkElement child, double offset);
        private void SetUpOffset();

        protected int DragChildIndex { get; }

        private int OverChildIndex { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocalDragPanelController<T, TVisual>.<>c <>9;
            public static Action<Storyboard> <>9__25_0;

            static <>c();
            internal void <CleanUpOffset>b__25_0(Storyboard x);
        }
    }
}

