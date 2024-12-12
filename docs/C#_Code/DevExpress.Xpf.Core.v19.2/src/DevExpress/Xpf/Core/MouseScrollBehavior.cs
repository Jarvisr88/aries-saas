namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class MouseScrollBehavior : Behavior<FrameworkElement>
    {
        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
        {
            this.VerifyMouseHWheelListening();
        }

        private void AssociatedObjectPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseWheelEventArgsEx ex = e as MouseWheelEventArgsEx;
            bool flag = ScrollBarExtensions.GetAllowShiftKeyMode(base.AssociatedObject) && ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift);
            bool flag2 = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            if (((ex == null) && !flag2) & flag)
            {
                e = new MouseWheelEventArgsWrapper(e, -e.Delta);
            }
            ex = e as MouseWheelEventArgsEx;
            if (ex != null)
            {
                this.PerformHorizontalScrolling(ex);
            }
            else
            {
                this.PerformScope(e);
                if (!e.Handled)
                {
                    this.PerformVerticalScrolling(e);
                }
            }
        }

        private bool CanScope(ScrollBehaviorInfo scrollInfo, double delta) => 
            !scrollInfo.PreventParentScrolling() ? scrollInfo.CanScope(delta) : true;

        private bool CanScrollHorizontal(ScrollBehaviorInfo scrollInfo, double delta) => 
            !scrollInfo.PreventParentScrolling() ? ((delta <= 0.0) ? ((delta < 0.0) && scrollInfo.CanScrollLeft()) : scrollInfo.CanScrollRight()) : true;

        private bool CanScrollVertical(ScrollBehaviorInfo scrollInfo, double delta) => 
            !scrollInfo.PreventParentScrolling() ? ((delta <= 0.0) ? ((delta < 0.0) && scrollInfo.CanScrollDown()) : scrollInfo.CanScrollUp()) : true;

        private ScrollBehaviorInfo FindScrollBehaviorInfo(DependencyObject source)
        {
            Func<DependencyObject, ScrollBehaviorBase> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<DependencyObject, ScrollBehaviorBase> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = s => ScrollBarExtensions.GetScrollBehavior(s);
            }
            return source.With<DependencyObject, ScrollBehaviorBase>(evaluator).With<ScrollBehaviorBase, ScrollBehaviorInfo>(b => new ScrollBehaviorInfo(source, b));
        }

        private ScrollBehaviorInfo GetScrollBehaviorInfo(FrameworkElement source, Func<ScrollBehaviorInfo, bool> canScroll, MouseEventArgs args, bool findPopups = true)
        {
            ScrollBehaviorInfo info3;
            List<ScrollBehaviorInfo> list = new List<ScrollBehaviorInfo>();
            if (findPopups)
            {
                Func<PresentationSource, FrameworkElement> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<PresentationSource, FrameworkElement> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.RootVisual as FrameworkElement;
                }
                FrameworkElement element = (Mouse.DirectlyOver as DependencyObject).With<DependencyObject, PresentationSource>(new Func<DependencyObject, PresentationSource>(PresentationSource.FromDependencyObject)).With<PresentationSource, FrameworkElement>(evaluator);
                Func<PresentationSource, FrameworkElement> func2 = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<PresentationSource, FrameworkElement> local2 = <>c.<>9__3_1;
                    func2 = <>c.<>9__3_1 = x => x.RootVisual as FrameworkElement;
                }
                if ((element != PresentationSource.FromDependencyObject(source).With<PresentationSource, FrameworkElement>(func2)) && (element != null))
                {
                    ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(element, canScroll, args, false);
                    if (info != null)
                    {
                        return info;
                    }
                }
            }
            VisualTreeHelper.HitTest(source, hitTest => this.HitTestFilterCallback(list, hitTest), new System.Windows.Media.HitTestResultCallback(this.HitTestResultCallback), new PointHitTestParameters(args.GetPosition(source)));
            using (List<ScrollBehaviorInfo>.Enumerator enumerator = list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ScrollBehaviorInfo current = enumerator.Current;
                        if (!current.PreventMouseScrolling())
                        {
                            continue;
                        }
                        info3 = current;
                    }
                    else
                    {
                        List<ScrollBehaviorInfo> list = list.Where<ScrollBehaviorInfo>(canScroll).ToList<ScrollBehaviorInfo>();
                        return ((findPopups || (list.Count != 0)) ? (list.LastOrDefault<ScrollBehaviorInfo>() ?? this.FindScrollBehaviorInfo(source).If<ScrollBehaviorInfo>(info => (!info.CheckHandlesMouseWheelScrolling() ? false : (info.PreventMouseScrolling() ? true : canScroll(info))))) : list.FirstOrDefault<ScrollBehaviorInfo>());
                    }
                    break;
                }
            }
            return info3;
        }

        private HitTestFilterBehavior HitTestFilterCallback(List<ScrollBehaviorInfo> list, DependencyObject potentialHitTestTarget)
        {
            Func<ScrollBehaviorInfo, bool> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ScrollBehaviorInfo, bool> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = info => info.CheckHandlesMouseWheelScrolling();
            }
            potentialHitTestTarget.With<DependencyObject, ScrollBehaviorInfo>(new Func<DependencyObject, ScrollBehaviorInfo>(this.FindScrollBehaviorInfo)).If<ScrollBehaviorInfo>(evaluator).Do<ScrollBehaviorInfo>(new Action<ScrollBehaviorInfo>(list.Add));
            return HitTestFilterBehavior.Continue;
        }

        private HitTestResultBehavior HitTestResultCallback(HitTestResult result) => 
            HitTestResultBehavior.Continue;

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.AssociatedObjectOnLoaded);
            base.AssociatedObject.Unloaded += new RoutedEventHandler(this.AssociatedObject_Unloaded);
            base.AssociatedObject.PreviewMouseWheel += new MouseWheelEventHandler(this.AssociatedObjectPreviewMouseWheel);
            this.VerifyMouseHWheelListening();
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObjectOnLoaded);
            base.AssociatedObject.Unloaded -= new RoutedEventHandler(this.AssociatedObject_Unloaded);
            base.AssociatedObject.PreviewMouseWheel -= new MouseWheelEventHandler(this.AssociatedObjectPreviewMouseWheel);
            base.OnDetaching();
        }

        private void PerformHorizontalPerPixelScrolling(MouseWheelEventArgsEx e)
        {
            ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(base.AssociatedObject, info => this.CanScrollHorizontal(info, (double) e.DeltaX), e, true);
            if ((info != null) && !info.PreventMouseScrolling())
            {
                info.ScrollToHorizontalOffset((double) e.DeltaX);
            }
        }

        private void PerformHorizontalScrolling(MouseWheelEventArgsEx e)
        {
            if ((e.DeltaX % 120) == 0)
            {
                this.PerformHorizontalStandardScrolling(e);
            }
            else
            {
                this.PerformHorizontalPerPixelScrolling(e);
            }
            if (e.DeltaY == 0)
            {
                e.SetHandled(true);
            }
        }

        private void PerformHorizontalStandardScrolling(MouseWheelEventArgsEx e)
        {
            ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(base.AssociatedObject, info => this.CanScrollHorizontal(info, (double) e.DeltaX), e, true);
            if ((info != null) && !info.PreventMouseScrolling())
            {
                int lineCount = e.DeltaX / 120;
                if (lineCount > 0)
                {
                    info.MouseWheelRight(lineCount);
                }
                else if (lineCount < 0)
                {
                    info.MouseWheelLeft(Math.Abs(lineCount));
                }
            }
        }

        private void PerformScope(MouseWheelEventArgs e)
        {
            ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(base.AssociatedObject, info => this.CanScope(info, (double) e.Delta), e, true);
            if ((info != null) && !info.PreventMouseScrolling())
            {
                ScopeEventArgs args = new ScopeEventArgs((double) e.Delta);
                info.Scope(args);
                e.Handled = args.Handled;
            }
        }

        private void PerformVerticalPerPixelScrolling(MouseWheelEventArgs e)
        {
            ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(base.AssociatedObject, info => this.CanScrollVertical(info, (double) e.Delta), e, true);
            if ((info != null) && !info.PreventMouseScrolling())
            {
                info.ScrollToVerticalOffset((double) e.Delta);
                e.Handled = true;
            }
        }

        private void PerformVerticalScrolling(MouseWheelEventArgs e)
        {
            if ((e.Delta % 120) == 0)
            {
                this.PerformVerticalStandardScrolling(e);
            }
            else
            {
                this.PerformVerticalPerPixelScrolling(e);
            }
        }

        private void PerformVerticalStandardScrolling(MouseWheelEventArgs e)
        {
            ScrollBehaviorInfo info = this.GetScrollBehaviorInfo(base.AssociatedObject, info => this.CanScrollVertical(info, (double) e.Delta), e, true);
            if (((info != null) && !info.PreventMouseScrolling()) && ScrollBarExtensions.GetHandlesDefaultMouseScrolling(info.Item1))
            {
                int lineCount = e.Delta / 120;
                if (lineCount > 0)
                {
                    info.MouseWheelUp(lineCount);
                }
                else if (lineCount < 0)
                {
                    info.MouseWheelDown(Math.Abs(lineCount));
                }
                e.Handled = lineCount != 0;
            }
        }

        private void VerifyMouseHWheelListening()
        {
            if (base.AssociatedObject.IsLoaded)
            {
                DependencyObject objA = LayoutHelper.FindRoot(base.AssociatedObject, false);
                if (!Equals(objA, base.AssociatedObject) && (objA is Visual))
                {
                    ScrollBarExtensions.SetHorizontalMouseWheelListeningInitializedProperty(objA, true);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MouseScrollBehavior.<>c <>9 = new MouseScrollBehavior.<>c();
            public static Func<PresentationSource, FrameworkElement> <>9__3_0;
            public static Func<PresentationSource, FrameworkElement> <>9__3_1;
            public static Func<DependencyObject, ScrollBehaviorBase> <>9__4_0;
            public static Func<MouseScrollBehavior.ScrollBehaviorInfo, bool> <>9__5_0;

            internal ScrollBehaviorBase <FindScrollBehaviorInfo>b__4_0(DependencyObject s) => 
                ScrollBarExtensions.GetScrollBehavior(s);

            internal FrameworkElement <GetScrollBehaviorInfo>b__3_0(PresentationSource x) => 
                x.RootVisual as FrameworkElement;

            internal FrameworkElement <GetScrollBehaviorInfo>b__3_1(PresentationSource x) => 
                x.RootVisual as FrameworkElement;

            internal bool <HitTestFilterCallback>b__5_0(MouseScrollBehavior.ScrollBehaviorInfo info) => 
                info.CheckHandlesMouseWheelScrolling();
        }

        private class ScrollBehaviorInfo : Tuple<DependencyObject, ScrollBehaviorBase>
        {
            public ScrollBehaviorInfo(DependencyObject source, ScrollBehaviorBase behavior) : base(source, behavior)
            {
            }

            public bool CanScope(double offset) => 
                base.Item2.CanScope(base.Item1, offset);

            public bool CanScrollDown() => 
                base.Item2.CanScrollDown(base.Item1);

            public bool CanScrollLeft() => 
                base.Item2.CanScrollLeft(base.Item1);

            public bool CanScrollRight() => 
                base.Item2.CanScrollRight(base.Item1);

            public bool CanScrollUp() => 
                base.Item2.CanScrollUp(base.Item1);

            public bool CheckHandlesMouseWheelScrolling() => 
                base.Item2.CheckHandlesMouseWheelScrolling(base.Item1);

            public void MouseWheelDown(int lineCount)
            {
                base.Item2.MouseWheelDown(base.Item1, lineCount);
            }

            public void MouseWheelLeft(int lineCount)
            {
                base.Item2.MouseWheelLeft(base.Item1, lineCount);
            }

            public void MouseWheelRight(int lineCount)
            {
                base.Item2.MouseWheelRight(base.Item1, lineCount);
            }

            public void MouseWheelUp(int lineCount)
            {
                base.Item2.MouseWheelUp(base.Item1, lineCount);
            }

            public bool PreventMouseScrolling() => 
                base.Item2.PreventMouseScrolling(base.Item1);

            public bool PreventParentScrolling() => 
                ScrollBarExtensions.GetPreventParentScrolling(base.Item1);

            internal void Scope(ScopeEventArgs e)
            {
                base.Item2.Scope(base.Item1, e);
            }

            public void ScrollToHorizontalOffset(double offset)
            {
                base.Item2.ScrollToHorizontalOffset(base.Item1, offset);
            }

            public void ScrollToVerticalOffset(double offset)
            {
                base.Item2.ScrollToVerticalOffset(base.Item1, offset);
            }
        }
    }
}

