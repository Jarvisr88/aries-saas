namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ChromeSlave
    {
        private static readonly double DefaultFontSize;
        private static readonly Func<UIElement, object> get_ArrangeRequest;
        private static readonly Func<UIElement, object> get_MeasureRequest;
        private readonly System.Windows.FrameworkElement feOwner;
        private readonly DevExpress.Xpf.Core.Native.IChrome iChromeOwner;
        private readonly bool propagateMouseEvents;
        private readonly DevExpress.Xpf.Core.Native.Namescope namescope;
        private bool contextUnloaded;
        private bool contextInvalid;
        private FrameworkRenderElementContext context;
        private readonly Func<FrameworkRenderElementContext> initializeContext;
        private readonly Action<FrameworkRenderElementContext> releaseContext;
        private readonly Func<bool> validateTemplate;
        private readonly Func<Size, Size> baseMeasure;
        private readonly Func<Size, Size> baseArrange;
        private readonly bool destroyContextOnUnloaded;
        private readonly Action<Brush> foregroundChanged;
        private RenderTemplate template;
        private bool? isEnabled;
        private RenderFontSettings fontSettings;
        private FrameworkRenderElementContext capturedElement;

        static ChromeSlave();
        public ChromeSlave(DevExpress.Xpf.Core.Native.IChrome owner, bool propagateMouseEvents, Func<FrameworkRenderElementContext> initializeContext, Action<FrameworkRenderElementContext> releaseContext, Func<bool> validateTemplate = null, Func<Size, Size> baseMeasure = null, Func<Size, Size> baseArrange = null, bool destroyContextOnUnloaded = true, Action<Brush> foregroundChanged = null);
        public Size ArrangeOverride(Size finalSize);
        public bool CaptureMouse(FrameworkRenderElementContext context);
        public void ChildDesiredSizeChanged(UIElement child);
        private void ChromeIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        private void ChromeLoaded(object sender, RoutedEventArgs e);
        private void ChromeUnloaded(object sender, RoutedEventArgs e);
        private void CleanupCapturedElement();
        private void CoerceValue(int key);
        public static void CoerceValue(DevExpress.Xpf.Core.Native.IChrome chrome, int propertyKey);
        private void CoerceValues(FrameworkRenderElementContext c);
        private FrameworkRenderElementContext CreateContext();
        public FrameworkRenderElementContext CreateContext(RenderTemplate template, Action<FrameworkRenderElementContext> initializeCallback = null);
        public void DestroyContext();
        private void FontSettingsChanged();
        private void ForceInvalidateLayout(DependencyObject visualAdded);
        public void ForegroundChanged(Brush newValue);
        public static FrameworkPropertyMetadataOptions GetDefaultOptions(DependencyProperty property);
        private static FrameworkRenderElementContext[] GetDescendantsUnderMouse(FrameworkRenderElementContext context, Point relativePoint, out FrameworkRenderElementContext[] addedElements, out FrameworkRenderElementContext[] removedElements, bool actualIsMouseOver, bool hasCapturedElement);
        private bool GetIsTouchEnabled(System.Windows.FrameworkElement frameworkElement);
        public void GoToState(string stateName);
        public void InvalidateArrange();
        private static void InvalidateArrange(UIElement element, bool invalidateIfNotBroken);
        public void InvalidateContext();
        public void InvalidateMeasure();
        private static void InvalidateMeasure(UIElement element, bool invalidateIfNotBroken);
        private void InvalidateMeasureAndVisual();
        [IteratorStateMachine(typeof(ChromeSlave.<IterateChildren>d__71))]
        private IEnumerable<Visual> IterateChildren();
        public Size MeasureOverride(Size availableSize);
        protected virtual void OnIsEnabledChanged();
        private void OnLostMouseCapture(object sender, MouseEventArgs e);
        private static void OnLostMouseCaptureThunk(object sender, MouseEventArgs e);
        private void OnMouseDown(object sender, MouseButtonEventArgs e);
        private static void OnMouseDownThunk(object sender, MouseButtonEventArgs e);
        private void OnMouseEnter(object sender, MouseEventArgs e);
        private static void OnMouseEnterThunk(object sender, MouseEventArgs e);
        private void OnMouseLeave(object sender, MouseEventArgs e);
        private static void OnMouseLeaveThunk(object sender, MouseEventArgs e);
        private void OnMouseMove(object sender, MouseEventArgs e);
        private static void OnMouseMoveThunk(object sender, MouseEventArgs e);
        private void OnMouseUp(object sender, MouseButtonEventArgs e);
        private static void OnMouseUpThunk(object sender, MouseButtonEventArgs e);
        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e);
        private static void OnPreviewMouseDownThunk(object sender, MouseButtonEventArgs e);
        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e);
        private static void OnPreviewMouseUpThunk(object sender, MouseButtonEventArgs e);
        public void OnRender(DrawingContext dc);
        public void PropagateEvent(RoutedEventArgs args, RenderEvents renderEvent);
        private void PropagateMouseEvent(DevExpress.Xpf.Core.Native.IChrome chrome, MouseEventArgs args, RenderEvents renderEvent, RoutingStrategy? strategy = new RoutingStrategy?());
        private void RaiseEvent(RenderEventArgsBase args, IEnumerable<FrameworkRenderElementContext> eventRoute);
        public void RefreshForeground();
        public void ReleaseContext(FrameworkRenderElementContext context);
        public void ReleaseMouseCapture(FrameworkRenderElementContext context);
        public void SetRoot(FrameworkRenderElementContext context);
        private void SubscribeEvents();
        private void SubscribeMouseEvents();
        private static void SubscribeStaticMouseEvents();
        public void UpdateFontFamily(FontFamily fontFamily);
        public void UpdateFontSize(double fontSize);
        public void UpdateFontStretch(FontStretch fontStretch);
        public void UpdateFontStyle(FontStyle fontStyle);
        public void UpdateFontWeight(FontWeight fontWeight);
        public virtual void UpdateSubTree();
        public void UpdateSvgPalette(WpfSvgPalette palette);
        public void UpdateSvgState(string state);
        public void VisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);

        private System.Windows.FrameworkElement FrameworkElement { get; }

        private DevExpress.Xpf.Core.Native.IChrome IChrome { get; }

        public INamescope Namescope { get; }

        public IPropertyChangedListener PropertyChangedListener { get; }

        public IElementHost ElementHost { get; }

        public FrameworkRenderElementContext Root { get; }

        public RenderTemplate Template { get; }

        public RenderFontSettings FontSettings { get; set; }

        public bool? IsEnabled { get; set; }

        private FrameworkRenderElementContext Context { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChromeSlave.<>c <>9;
            public static Action<FrameworkRenderElementContext> <>9__75_0;
            public static Action<FrameworkRenderElementContext> <>9__82_0;
            public static Func<RenderEventArgsBase, RoutedEventArgs> <>9__99_0;
            public static Func<RoutedEventArgs, RoutedEvent> <>9__99_1;
            public static Func<RoutedEvent, bool> <>9__99_2;
            public static Action<RoutedEventArgs> <>9__99_3;
            public static Func<FrameworkRenderElementContext, RenderHitTestFilterBehavior> <>9__100_0;
            public static Func<FrameworkRenderElementContext, bool> <>9__100_2;
            public static Func<FrameworkRenderElementContext, bool> <>9__100_3;

            static <>c();
            internal RenderHitTestFilterBehavior <GetDescendantsUnderMouse>b__100_0(FrameworkRenderElementContext frec);
            internal bool <GetDescendantsUnderMouse>b__100_2(FrameworkRenderElementContext x);
            internal bool <GetDescendantsUnderMouse>b__100_3(FrameworkRenderElementContext x);
            internal RoutedEventArgs <RaiseEvent>b__99_0(RenderEventArgsBase x);
            internal RoutedEvent <RaiseEvent>b__99_1(RoutedEventArgs x);
            internal bool <RaiseEvent>b__99_2(RoutedEvent x);
            internal void <RaiseEvent>b__99_3(RoutedEventArgs x);
            internal void <ReleaseContext>b__82_0(FrameworkRenderElementContext x);
            internal void <UpdateSubTree>b__75_0(FrameworkRenderElementContext x);
        }

        [CompilerGenerated]
        private sealed class <IterateChildren>d__71 : IEnumerable<Visual>, IEnumerable, IEnumerator<Visual>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Visual <>2__current;
            private int <>l__initialThreadId;
            public ChromeSlave <>4__this;
            private int <childIndex>5__1;

            [DebuggerHidden]
            public <IterateChildren>d__71(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<Visual> IEnumerable<Visual>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            Visual IEnumerator<Visual>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

