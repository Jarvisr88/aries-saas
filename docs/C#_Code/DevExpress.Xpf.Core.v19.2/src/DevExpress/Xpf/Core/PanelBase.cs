namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;

    public abstract class PanelBase : Panel, IPanel, IControl, IScrollBarUpdated
    {
        public const int LowZIndex = -1000;
        public const int NormalLowZIndex = -500;
        public const int NormalMediumLowZIndex = -100;
        public const int NormalMediumHighZIndex = 100;
        public const int NormalHighZIndex = 500;
        public const int HighZIndex = 0x3e8;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ClipListener = RegisterPropertyListener("Clip");
        private bool _IsAttachingPropertyListener;
        private bool _IsUpdatingClip;
        private static bool _IsInitializingChildPropertyListener;
        private List<ChildInfo> _ChildInfos;

        event EventHandler IScrollBarUpdated.ScrollBarUpdated
        {
            add
            {
                this.Controller.ScrollParamsChanged += value;
            }
            remove
            {
                this.Controller.ScrollParamsChanged -= value;
            }
        }

        public event EventHandler EndDrag
        {
            add
            {
                this.Controller.EndDrag += value;
            }
            remove
            {
                this.Controller.EndDrag -= value;
            }
        }

        public event EventHandler StartDrag
        {
            add
            {
                this.Controller.StartDrag += value;
            }
            remove
            {
                this.Controller.StartDrag -= value;
            }
        }

        public PanelBase()
        {
            ScrollViewerTouchBehavior.SetIsEnabled(this, true);
            this.Controller = this.CreateController();
            this.AttachToEvents();
            this.AttachPropertyListener("Clip", ClipListener);
            if (this.NeedsChildChangeNotifications)
            {
                this.InitializeChildChangeNotificationSystem();
            }
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            this.IsArranging = true;
            this.OnBeforeArrange(finalSize);
            while (true)
            {
                System.Windows.Size contentSize = this.OnArrange(this.CalculateContentBounds(finalSize));
                contentSize = this.CalculateSizeFromContentSize(contentSize);
                contentSize.Width = Math.Min(contentSize.Width, finalSize.Width);
                contentSize.Height = Math.Min(contentSize.Height, finalSize.Height);
                this.Size = contentSize;
                this.ChildrenBounds = this.GetChildrenBounds();
                if (!this.Controller.UpdateScrolling())
                {
                    this.Controller.OnArrange(finalSize);
                    this.IsArranging = false;
                    return contentSize;
                }
            }
        }

        protected static void AttachChildPropertyListener(FrameworkElement child, string propertyName, DependencyProperty propertyListener)
        {
            _IsInitializingChildPropertyListener = true;
            try
            {
                Binding binding = new Binding(propertyName);
                binding.Source = child;
                child.SetBinding(propertyListener, binding);
            }
            finally
            {
                _IsInitializingChildPropertyListener = false;
            }
        }

        protected virtual void AttachChildPropertyListeners(FrameworkElement child)
        {
        }

        protected void AttachPropertyListener(string propertyName, DependencyProperty propertyListener)
        {
            this._IsAttachingPropertyListener = true;
            try
            {
                Binding binding = new Binding(propertyName);
                binding.Source = this;
                base.SetBinding(propertyListener, binding);
            }
            finally
            {
                this._IsAttachingPropertyListener = false;
            }
        }

        protected virtual void AttachToEvents()
        {
            base.LayoutUpdated += delegate (object <sender>, EventArgs <e>) {
                if (!LayoutUpdatedHelper.GlobalLocker.IsLocked)
                {
                    this.OnLayoutUpdated();
                }
            };
            base.Loaded += (sender, e) => this.OnLoaded();
            base.SizeChanged += (sender, e) => this.OnSizeChanged(e);
            base.Unloaded += (sender, e) => this.OnUnloaded();
        }

        protected Rect CalculateClientBounds(System.Windows.Size size)
        {
            Rect rect = RectHelper.New(size);
            RectHelper.Deflate(ref rect, this.ClientPadding);
            return rect;
        }

        protected Rect CalculateContentBounds(Rect clientBounds)
        {
            RectHelper.Deflate(ref clientBounds, this.ContentPadding);
            return clientBounds;
        }

        protected Rect CalculateContentBounds(System.Windows.Size size) => 
            this.CalculateContentBounds(this.CalculateClientBounds(size));

        protected System.Windows.Size CalculateSizeFromClientSize(System.Windows.Size clientSize)
        {
            SizeHelper.Inflate(ref clientSize, this.ClientPadding);
            return clientSize;
        }

        protected System.Windows.Size CalculateSizeFromContentSize(System.Windows.Size contentSize)
        {
            SizeHelper.Inflate(ref contentSize, this.TotalPadding);
            return contentSize;
        }

        protected void Changed()
        {
            base.InvalidateMeasure();
        }

        private void CheckChildChanges()
        {
            if (this._ChildInfos != null)
            {
                this.UpdateChildInfos(this._ChildInfos, this.GetLogicalChildren(false));
            }
        }

        protected void CheckInternalElementsParent()
        {
            if (base.IsInitialized)
            {
                foreach (UIElement element in this.GetInternalElements())
                {
                    if (((FrameworkElement) element).GetParent() == null)
                    {
                        base.Children.Add(element);
                    }
                }
            }
        }

        public UIElement ChildAt(Point p) => 
            this.ChildAt(p, true, true);

        public UIElement ChildAt(Point p, bool ignoreInternalElements, bool ignoreTempChildren)
        {
            UIElement result = null;
            HitTestResultCallback resultCallback = <>c.<>9__13_1;
            if (<>c.<>9__13_1 == null)
            {
                HitTestResultCallback local1 = <>c.<>9__13_1;
                resultCallback = <>c.<>9__13_1 = hitTestResult => HitTestResultBehavior.Continue;
            }
            VisualTreeHelper.HitTest(this, delegate (DependencyObject hitObject) {
                UIElement objA = hitObject as UIElement;
                if (ReferenceEquals(objA, this))
                {
                    return HitTestFilterBehavior.Continue;
                }
                if (((!objA.IsVisible || (ignoreInternalElements && this.IsInternalElement(objA))) || (ignoreTempChildren && this.IsTempChild(objA))) || (VisualTreeHelper.HitTest(objA, this.TranslatePoint(p, objA)) == null))
                {
                    return HitTestFilterBehavior.ContinueSkipChildren;
                }
                result = objA;
                return HitTestFilterBehavior.Stop;
            }, resultCallback, new PointHitTestParameters(p));
            return result;
        }

        public UIElement ChildAt(Point p, bool ignoreInternalElements, bool ignoreTempChildren, bool useBounds)
        {
            UIElement element = this.ChildAt(p, ignoreInternalElements, ignoreTempChildren);
            if (useBounds && (element == null))
            {
                Rect rect = RectHelper.New(this.Size);
                if (rect.Contains(p) && (base.Visibility == Visibility.Visible))
                {
                    using (IEnumerator<FrameworkElement> enumerator = this.GetChildren(!ignoreInternalElements, !ignoreTempChildren, true).GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            FrameworkElement current = enumerator.Current;
                            if ((current.Visibility == Visibility.Visible) && current.GetVisualBounds().Contains(p))
                            {
                                return current;
                            }
                        }
                    }
                }
            }
            return element;
        }

        protected virtual ChildInfo CreateChildInfo(FrameworkElement child) => 
            new ChildInfo(child);

        protected virtual PanelControllerBase CreateController() => 
            new PanelControllerBase(this);

        public void DeleteChildren()
        {
            for (int i = base.Children.Count - 1; i >= 0; i--)
            {
                if (!this.IsInternalElement(base.Children[i]))
                {
                    base.Children.RemoveAt(i);
                }
            }
        }

        protected static void DetachChildPropertyListener(FrameworkElement child, DependencyProperty propertyListener)
        {
            _IsInitializingChildPropertyListener = true;
            try
            {
                child.ClearValue(propertyListener);
            }
            finally
            {
                _IsInitializingChildPropertyListener = false;
            }
        }

        protected virtual void DetachChildPropertyListeners(FrameworkElement child)
        {
        }

        protected virtual Rect GetChildBounds(FrameworkElement child) => 
            LayoutInformation.GetLayoutSlot(child);

        public FrameworkElements GetChildren(bool visibleOnly) => 
            this.GetChildren(false, true, visibleOnly);

        public FrameworkElements GetChildren(bool includeInternalElements, bool includeTempChildren, bool visibleOnly)
        {
            FrameworkElements elements = new FrameworkElements();
            foreach (FrameworkElement element in base.Children)
            {
                if ((element == null) || (visibleOnly && (element.Visibility == Visibility.Collapsed)))
                {
                    continue;
                }
                if ((includeInternalElements || !this.IsInternalElement(element)) && (includeTempChildren || !this.IsTempChild(element)))
                {
                    elements.Add(element);
                }
            }
            return elements;
        }

        protected virtual Rect GetChildrenBounds()
        {
            Rect empty = Rect.Empty;
            foreach (FrameworkElement element in this.GetLogicalChildren(true))
            {
                empty.Union(this.GetChildBounds(element));
            }
            return empty;
        }

        private double GetChildrenSize(double defaultValue, Func<FrameworkElement, double> getChildSize, Func<double, double, double> compareSizes)
        {
            double num = defaultValue;
            foreach (FrameworkElement element in this.GetLogicalChildren(false))
            {
                num = compareSizes(num, getChildSize(element));
            }
            return num;
        }

        protected virtual Thickness GetClientPadding()
        {
            Thickness padding = new Thickness(0.0);
            this.Controller.GetClientPadding(ref padding);
            return padding;
        }

        protected virtual Thickness GetContentPadding() => 
            new Thickness(0.0);

        protected virtual Geometry GetGeometry()
        {
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = RectHelper.New(this.Size);
            return geometry1;
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__56))]
        protected virtual IEnumerable<UIElement> GetInternalElements()
        {
            <GetInternalElements>d__56 d__1 = new <GetInternalElements>d__56(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        public FrameworkElements GetLogicalChildren(bool visibleOnly) => 
            this.GetChildren(false, false, visibleOnly);

        public FrameworkElements GetVisibleChildren() => 
            this.GetVisibleChildren(false, true);

        public FrameworkElements GetVisibleChildren(bool includeInternalElements, bool includeTempChildren) => 
            this.GetChildren(includeInternalElements, includeTempChildren, true);

        private void InitializeChildChangeNotificationSystem()
        {
            this._ChildInfos = new List<ChildInfo>();
        }

        protected bool IsInternalElement(UIElement element)
        {
            bool flag;
            using (IEnumerator<UIElement> enumerator = this.GetInternalElements().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = enumerator.Current;
                        if (!ReferenceEquals(element, current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected bool IsLogicalChild(UIElement child) => 
            !this.IsInternalElement(child) && !this.IsTempChild(child);

        protected virtual bool IsTempChild(UIElement child) => 
            false;

        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            System.Windows.Size size;
            bool flag;
            this.IsMeasuring = true;
            this.CheckInternalElementsParent();
            this.OnBeforeMeasure(availableSize);
            this.Controller.ResetScrollBarsVisibility();
            do
            {
                flag = false;
                Rect rect = this.CalculateContentBounds(availableSize);
                System.Windows.Size size3 = rect.Size();
                size = this.OnMeasure(size3);
                this.UpdateOriginalDesiredSize(ref size);
                this.OriginalDesiredSize = size;
                if (this.Controller.IsScrollable())
                {
                    size.Width = Math.Min(size.Width, size3.Width);
                    size.Height = Math.Min(size.Height, size3.Height);
                }
                bool flag2 = (size.Width == 0.0) || (size.Height == 0.0);
                this.Size = availableSize;
                this.ChildrenBounds = new Rect(rect.Location(), this.OriginalDesiredSize);
                this.Controller.UpdateScrollBarsVisibility();
                size = this.CalculateSizeFromContentSize(size);
                if (flag2)
                {
                    size.Width = Math.Min(size.Width, availableSize.Width);
                    size.Height = Math.Min(size.Height, availableSize.Height);
                    break;
                }
                if (this.Controller.IsScrollable())
                {
                    flag = ((size.Width - availableSize.Width) >= 1.0) || ((size.Height - availableSize.Height) >= 1.0);
                    if (((this.OriginalDesiredSize.Width - size3.Width) >= 1.0) && ((availableSize.Width - size.Width) >= 1.0))
                    {
                        flag = true;
                    }
                    if (((this.OriginalDesiredSize.Height - size3.Height) >= 1.0) && ((availableSize.Height - size.Height) >= 1.0))
                    {
                        flag = true;
                    }
                }
            }
            while (flag);
            System.Windows.Size size2 = new System.Windows.Size(double.IsInfinity(availableSize.Width) ? size.Width : availableSize.Width, double.IsInfinity(availableSize.Height) ? size.Height : availableSize.Height);
            this.Controller.OnMeasure(size2);
            this.IsMeasuring = false;
            return size;
        }

        protected virtual System.Windows.Size OnArrange(Rect bounds) => 
            base.ArrangeOverride(bounds.Size());

        protected static void OnAttachedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = o as FrameworkElement;
            if (element != null)
            {
                PanelBase parent = element.GetParent() as PanelBase;
                if (parent != null)
                {
                    parent.OnAttachedPropertyChanged(element, e.Property, e.OldValue, e.NewValue);
                }
            }
        }

        protected virtual void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
        }

        protected virtual void OnBeforeArrange(System.Windows.Size finalSize)
        {
        }

        protected virtual void OnBeforeMeasure(System.Windows.Size availableSize)
        {
            this.Controller.MeasureScrollBars();
        }

        protected virtual void OnChildAdded(FrameworkElement child)
        {
            this.AttachChildPropertyListeners(child);
        }

        private static void OnChildPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (!_IsInitializingChildPropertyListener)
            {
                FrameworkElement element = o as FrameworkElement;
                if (element != null)
                {
                    PanelBase parent = element.GetParent() as PanelBase;
                    if (parent != null)
                    {
                        parent.OnChildPropertyChanged(element, e.Property, e.OldValue, e.NewValue);
                    }
                }
            }
        }

        protected virtual void OnChildPropertyChanged(FrameworkElement child, DependencyProperty propertyListener, object oldValue, object newValue)
        {
        }

        protected virtual void OnChildRemoved(FrameworkElement child)
        {
            this.DetachChildPropertyListeners(child);
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.Controller.OnInitialized();
            base.OnInitialized(e);
        }

        protected virtual void OnLayoutUpdated()
        {
            this.CheckInternalElementsParent();
            this.CheckChildChanges();
        }

        protected virtual void OnLoaded()
        {
            this.CheckInternalElementsParent();
            this.CheckChildChanges();
        }

        protected virtual System.Windows.Size OnMeasure(System.Windows.Size availableSize) => 
            base.MeasureOverride(availableSize);

        protected virtual void OnPropertyChanged(DependencyProperty propertyListener, object oldValue, object newValue)
        {
            if (ReferenceEquals(propertyListener, ClipListener) && (newValue == null))
            {
                this.UpdateClip();
            }
        }

        protected virtual void OnSizeChanged(SizeChangedEventArgs e)
        {
            this.Size = e.NewSize;
            this.UpdateClip();
        }

        protected virtual void OnUnloaded()
        {
        }

        protected static DependencyProperty RegisterChildPropertyListener(string propertyName, Type ownerType) => 
            DependencyProperty.RegisterAttached("Child" + propertyName + "Listener", typeof(object), ownerType, new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnChildPropertyChanged)));

        protected static DependencyProperty RegisterPropertyListener(string propertyName)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__7_0;
                propertyChangedCallback = <>c.<>9__7_0 = delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                    PanelBase base2 = (PanelBase) o;
                    if (!base2._IsAttachingPropertyListener)
                    {
                        base2.OnPropertyChanged(e.Property, e.OldValue, e.NewValue);
                    }
                };
            }
            return DependencyProperty.Register(propertyName + "Listener", typeof(object), typeof(PanelBase), new PropertyMetadata(propertyChangedCallback));
        }

        private void UpdateChildInfos(List<ChildInfo> childInfos, FrameworkElements children)
        {
            int index = 0;
            while (index < childInfos.Count)
            {
                ChildInfo info = childInfos[index];
                int num2 = children.IndexOf(info.Instance);
                if (num2 == -1)
                {
                    this.OnChildRemoved(info.Instance);
                    childInfos.RemoveAt(index);
                    continue;
                }
                info.Update();
                children.RemoveAt(num2);
                index++;
            }
            foreach (FrameworkElement element in children)
            {
                childInfos.Add(this.CreateChildInfo(element));
                this.OnChildAdded(element);
            }
        }

        protected void UpdateClip()
        {
            if (this.IsClipped && !this._IsUpdatingClip)
            {
                this._IsUpdatingClip = true;
                try
                {
                    Geometry geometry = this.GetGeometry();
                    if ((base.Clip == null) || (base.Clip.GetType() == geometry.GetType()))
                    {
                        base.Clip = geometry;
                    }
                }
                finally
                {
                    this._IsUpdatingClip = false;
                }
            }
        }

        protected virtual void UpdateOriginalDesiredSize(ref System.Windows.Size originalDesiredSize)
        {
        }

        public Rect AbsoluteBounds =>
            this.GetBounds(null);

        public Point AbsolutePosition =>
            this.GetPosition(null);

        public Rect Bounds =>
            this.GetBounds();

        public Rect ChildrenBounds { get; private set; }

        public Rect ClientBounds =>
            this.CalculateClientBounds(this.Size);

        public Rect ContentBounds =>
            this.CalculateContentBounds(this.ClientBounds);

        public PanelControllerBase Controller { get; private set; }

        protected double ChildrenMinWidth
        {
            get
            {
                Func<FrameworkElement, double> getChildSize = <>c.<>9__64_0;
                if (<>c.<>9__64_0 == null)
                {
                    Func<FrameworkElement, double> local1 = <>c.<>9__64_0;
                    getChildSize = <>c.<>9__64_0 = child => child.MinWidth;
                }
                return this.GetChildrenSize(0.0, getChildSize, new Func<double, double, double>(Math.Max));
            }
        }

        protected double ChildrenMinHeight
        {
            get
            {
                Func<FrameworkElement, double> getChildSize = <>c.<>9__66_0;
                if (<>c.<>9__66_0 == null)
                {
                    Func<FrameworkElement, double> local1 = <>c.<>9__66_0;
                    getChildSize = <>c.<>9__66_0 = child => child.MinHeight;
                }
                return this.GetChildrenSize(0.0, getChildSize, new Func<double, double, double>(Math.Max));
            }
        }

        protected System.Windows.Size ChildrenMinSize =>
            new System.Windows.Size(this.ChildrenMinWidth, this.ChildrenMinHeight);

        protected double ChildrenMaxWidth
        {
            get
            {
                Func<FrameworkElement, double> getChildSize = <>c.<>9__70_0;
                if (<>c.<>9__70_0 == null)
                {
                    Func<FrameworkElement, double> local1 = <>c.<>9__70_0;
                    getChildSize = <>c.<>9__70_0 = child => child.MaxWidth;
                }
                return this.GetChildrenSize(double.PositiveInfinity, getChildSize, new Func<double, double, double>(Math.Min));
            }
        }

        protected double ChildrenMaxHeight
        {
            get
            {
                Func<FrameworkElement, double> getChildSize = <>c.<>9__72_0;
                if (<>c.<>9__72_0 == null)
                {
                    Func<FrameworkElement, double> local1 = <>c.<>9__72_0;
                    getChildSize = <>c.<>9__72_0 = child => child.MaxHeight;
                }
                return this.GetChildrenSize(double.PositiveInfinity, getChildSize, new Func<double, double, double>(Math.Min));
            }
        }

        protected System.Windows.Size ChildrenMaxSize =>
            new System.Windows.Size(this.ChildrenMaxWidth, this.ChildrenMaxHeight);

        protected virtual bool NeedsChildChangeNotifications =>
            false;

        protected Thickness ClientPadding =>
            this.GetClientPadding();

        protected Thickness ContentPadding =>
            this.GetContentPadding();

        protected bool IsArranging { get; private set; }

        protected bool IsMeasuring { get; private set; }

        protected System.Windows.Size OriginalDesiredSize { get; private set; }

        protected System.Windows.Size Size { get; private set; }

        protected Thickness TotalPadding
        {
            get
            {
                Thickness clientPadding = this.ClientPadding;
                ThicknessHelper.Inc(ref clientPadding, this.ContentPadding);
                return clientPadding;
            }
        }

        protected virtual bool IsClipped =>
            this.Controller.IsScrollable();

        FrameworkElement IControl.Control =>
            this;

        DevExpress.Xpf.Core.Controller IControl.Controller =>
            this.Controller;

        System.Windows.Size IPanel.ActualDesiredSize
        {
            get
            {
                System.Windows.Size minSize = this.CalculateSizeFromContentSize(this.OriginalDesiredSize);
                if (!double.IsNaN(base.Width))
                {
                    minSize.Width = this.GetRealWidth();
                }
                if (!double.IsNaN(base.Height))
                {
                    minSize.Height = this.GetRealHeight();
                }
                SizeHelper.UpdateMinSize(ref minSize, this.GetMaxSize());
                SizeHelper.UpdateMaxSize(ref minSize, this.GetMinSize());
                SizeHelper.Inflate(ref minSize, base.Margin);
                return minSize;
            }
        }

        UIElementCollection IPanel.Children =>
            base.Children;

        bool IControl.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PanelBase.<>c <>9 = new PanelBase.<>c();
            public static PropertyChangedCallback <>9__7_0;
            public static HitTestResultCallback <>9__13_1;
            public static Func<FrameworkElement, double> <>9__64_0;
            public static Func<FrameworkElement, double> <>9__66_0;
            public static Func<FrameworkElement, double> <>9__70_0;
            public static Func<FrameworkElement, double> <>9__72_0;

            internal HitTestResultBehavior <ChildAt>b__13_1(HitTestResult hitTestResult) => 
                HitTestResultBehavior.Continue;

            internal double <get_ChildrenMaxHeight>b__72_0(FrameworkElement child) => 
                child.MaxHeight;

            internal double <get_ChildrenMaxWidth>b__70_0(FrameworkElement child) => 
                child.MaxWidth;

            internal double <get_ChildrenMinHeight>b__66_0(FrameworkElement child) => 
                child.MinHeight;

            internal double <get_ChildrenMinWidth>b__64_0(FrameworkElement child) => 
                child.MinWidth;

            internal void <RegisterPropertyListener>b__7_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                PanelBase base2 = (PanelBase) o;
                if (!base2._IsAttachingPropertyListener)
                {
                    base2.OnPropertyChanged(e.Property, e.OldValue, e.NewValue);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <GetInternalElements>d__56 : IEnumerable<UIElement>, IEnumerable, IEnumerator<UIElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private UIElement <>2__current;
            private int <>l__initialThreadId;
            public PanelBase <>4__this;
            private IEnumerator<UIElement> <>7__wrap1;

            [DebuggerHidden]
            public <GetInternalElements>d__56(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.Controller.GetInternalElements().GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        UIElement current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<UIElement> IEnumerable<UIElement>.GetEnumerator()
            {
                PanelBase.<GetInternalElements>d__56 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new PanelBase.<GetInternalElements>d__56(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Windows.UIElement>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            UIElement IEnumerator<UIElement>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        protected class ChildInfo
        {
            public ChildInfo(FrameworkElement instance)
            {
                this.Instance = instance;
                this.Initialize();
            }

            protected virtual void Initialize()
            {
            }

            protected virtual void OnUpdate()
            {
            }

            public void Update()
            {
                this.OnUpdate();
                this.Initialize();
            }

            public FrameworkElement Instance { get; private set; }
        }
    }
}

