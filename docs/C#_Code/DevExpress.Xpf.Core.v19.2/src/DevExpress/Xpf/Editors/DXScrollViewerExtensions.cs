namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class DXScrollViewerExtensions
    {
        public static readonly DependencyProperty ScrollDataProperty = DependencyPropertyManager.RegisterAttached("ScrollData", typeof(ScrollData), typeof(DXScrollViewerExtensions), new PropertyMetadata(null, new PropertyChangedCallback(DXScrollViewerExtensions.OnScrollDataChanged)));

        public static void AnimateScrollToVerticalOffset(this DXScrollViewer scrollViewer, double offset, Action onStart = null, Action onCompleted = null, Action preCompleted = null, Func<double, double> ensureStep = null, ScrollDataAnimationEase animationEase = 0)
        {
            if (!scrollViewer.VerticalOffset.AreClose(offset))
            {
                ScrollData scrollData = GetScrollData(scrollViewer);
                if (scrollData == null)
                {
                    scrollData = new ScrollData();
                    SetScrollData(scrollViewer, scrollData);
                }
                Action<Action> action = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Action<Action> local1 = <>c.<>9__4_0;
                    action = <>c.<>9__4_0 = x => x();
                }
                onStart.Do<Action>(action);
                scrollData.AnimateScrollToVerticalOffset(offset, preCompleted, onCompleted, ensureStep, animationEase);
            }
        }

        public static ScrollData GetScrollData(DependencyObject d) => 
            (ScrollData) d.GetValue(ScrollDataProperty);

        private static void OnScrollDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollData oldValue = (ScrollData) e.OldValue;
            ScrollData data2 = (ScrollData) d.GetValue(ScrollDataProperty);
            DXScrollViewer viewer = d as DXScrollViewer;
            if (oldValue != null)
            {
                oldValue.ScrollOwner = null;
            }
            if (data2 != null)
            {
                data2.ScrollOwner = viewer;
            }
        }

        public static void SetScrollData(DependencyObject d, ScrollData value)
        {
            d.SetValue(ScrollDataProperty, value);
        }

        public static void StopCurrentAnimatedScroll(this DXScrollViewer scrollViewer)
        {
            ScrollData scrollData = GetScrollData(scrollViewer);
            if (scrollData != null)
            {
                scrollData.StopCurrentAnimatedScroll();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXScrollViewerExtensions.<>c <>9 = new DXScrollViewerExtensions.<>c();
            public static Action<Action> <>9__4_0;

            internal void <AnimateScrollToVerticalOffset>b__4_0(Action x)
            {
                x();
            }
        }
    }
}

