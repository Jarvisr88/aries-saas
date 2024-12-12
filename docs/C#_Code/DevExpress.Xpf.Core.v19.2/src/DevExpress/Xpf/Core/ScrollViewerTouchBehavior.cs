namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class ScrollViewerTouchBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ScrollViewerTouchBehavior), new PropertyMetadata(false, new PropertyChangedCallback(ScrollViewerTouchBehavior.IsEnabledChanged)));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation?), typeof(ScrollViewerTouchBehavior), new UIPropertyMetadata(null, new PropertyChangedCallback(ScrollViewerTouchBehavior.OnOrientationChanged)));
        private ScrollableObjectBase ScrollViewer;
        private ScrollableObjectFactory _ScrollFactory;
        private DispatcherTimer updaterTimer;

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.AssociatedObject == null)
            {
                (sender as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Loaded -= new RoutedEventHandler(this.AssociatedObject_Loaded);
                });
            }
            else
            {
                base.AssociatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObject_Loaded);
                this.InitializeScrollViewer();
            }
        }

        private void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (ScrollBarExtensions.GetScrollBarMode(base.AssociatedObject) != ScrollBarMode.Standard)
            {
                ScrollBarExtensions.SetScrollViewerMouseMoved(base.AssociatedObject, true);
                this.StartUpdateTimer();
            }
        }

        private void AssociatedObject_ScrollChanged(object sender, EventArgs e)
        {
            if (ScrollBarExtensions.GetScrollBarMode(base.AssociatedObject) != ScrollBarMode.Standard)
            {
                ScrollBarExtensions.SetScrollViewerMouseMoved(base.AssociatedObject, true);
                this.StartUpdateTimer();
            }
        }

        private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ScrollBarExtensions.GetScrollBarMode(base.AssociatedObject) != ScrollBarMode.Standard)
            {
                ScrollBarExtensions.SetScrollViewerSizeChanged(base.AssociatedObject, true);
                this.StartUpdateTimer();
            }
        }

        public static bool GetIsEnabled(DependencyObject d) => 
            (bool) d.GetValue(IsEnabledProperty);

        private void InitializeScrollViewer()
        {
            this.ScrollViewer = this.ScrollFactory.Resolve(base.AssociatedObject);
            if (this.ScrollViewer != null)
            {
                this.ScrollViewer.ScrollChanged += new EventHandler(this.AssociatedObject_ScrollChanged);
                this.ScrollViewer.Attach(base.AssociatedObject);
            }
            base.AssociatedObject.SizeChanged += new SizeChangedEventHandler(this.AssociatedObject_SizeChanged);
            base.AssociatedObject.PreviewMouseMove += new MouseEventHandler(this.AssociatedObject_PreviewMouseMove);
            this.UpdateAssociatedObjectScrollViewerOrientation();
        }

        private static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection source = Interaction.GetBehaviors(d);
            if ((bool) e.NewValue)
            {
                source.Add(new ScrollViewerTouchBehavior());
            }
            else
            {
                Func<Behavior, bool> predicate = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<Behavior, bool> local1 = <>c.<>9__3_0;
                    predicate = <>c.<>9__3_0 = x => x is ScrollViewerTouchBehavior;
                }
                Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                if (behavior != null)
                {
                    source.Remove(behavior);
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (base.AssociatedObject.IsLoaded)
            {
                this.InitializeScrollViewer();
            }
            else
            {
                base.AssociatedObject.Loaded += new RoutedEventHandler(this.AssociatedObject_Loaded);
            }
        }

        protected override void OnDetaching()
        {
            this.StopTimer();
            if (this.ScrollViewer != null)
            {
                this.ScrollViewer.ScrollChanged -= new EventHandler(this.AssociatedObject_ScrollChanged);
                this.ScrollViewer.Detach();
                this.ScrollViewer = null;
            }
            base.AssociatedObject.SizeChanged -= new SizeChangedEventHandler(this.AssociatedObject_SizeChanged);
            base.AssociatedObject.PreviewMouseMove -= new MouseEventHandler(this.AssociatedObject_PreviewMouseMove);
            if (this.updaterTimer != null)
            {
                this.updaterTimer.Tick -= new EventHandler(this.UpdaterTimerTick);
            }
            base.OnDetaching();
        }

        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation? oldValue, System.Windows.Controls.Orientation? newValue)
        {
            this.UpdateAssociatedObjectScrollViewerOrientation();
        }

        private static void OnOrientationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewerTouchBehavior behavior = o as ScrollViewerTouchBehavior;
            if (behavior != null)
            {
                behavior.OnOrientationChanged((System.Windows.Controls.Orientation?) e.OldValue, (System.Windows.Controls.Orientation?) e.NewValue);
            }
        }

        public static void SetIsEnabled(DependencyObject d, bool value)
        {
            d.SetValue(IsEnabledProperty, value);
        }

        private void StartUpdateTimer()
        {
            if (this.updaterTimer == null)
            {
                this.updaterTimer = new DispatcherTimer();
                this.updaterTimer.Interval = TimeSpan.FromMilliseconds(100.0);
                this.updaterTimer.Tick += new EventHandler(this.UpdaterTimerTick);
            }
            this.StopTimer();
            this.updaterTimer.Start();
        }

        private void StopTimer()
        {
            if ((this.updaterTimer != null) && this.updaterTimer.IsEnabled)
            {
                this.updaterTimer.Stop();
            }
        }

        private void UpdateAssociatedObjectScrollViewerOrientation()
        {
            if ((base.AssociatedObject != null) && (ScrollBarExtensions.GetScrollViewerOrientation(base.AssociatedObject) == null))
            {
                ScrollBarExtensions.SetScrollViewerOrientation(base.AssociatedObject, this.Orientation);
            }
        }

        private void UpdaterTimerTick(object sender, EventArgs e)
        {
            this.updaterTimer.Stop();
            if (base.AssociatedObject != null)
            {
                ScrollBarExtensions.SetScrollViewerMouseMoved(base.AssociatedObject, false);
                ScrollBarExtensions.SetScrollViewerSizeChanged(base.AssociatedObject, false);
            }
        }

        public System.Windows.Controls.Orientation? Orientation
        {
            get => 
                (System.Windows.Controls.Orientation?) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        private ScrollableObjectFactory ScrollFactory
        {
            get
            {
                this._ScrollFactory ??= new ScrollableObjectFactory();
                return this._ScrollFactory;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollViewerTouchBehavior.<>c <>9 = new ScrollViewerTouchBehavior.<>c();
            public static Func<Behavior, bool> <>9__3_0;

            internal bool <IsEnabledChanged>b__3_0(Behavior x) => 
                x is ScrollViewerTouchBehavior;
        }

        private class ScrollableObjectBase
        {
            public event EventHandler ScrollChanged;

            public void Attach(FrameworkElement associatedObject)
            {
                this.AssociatedObject = associatedObject;
                this.OnAttach();
            }

            public void Detach()
            {
                this.OnDetach();
                this.AssociatedObject = null;
            }

            protected virtual void OnAttach()
            {
            }

            protected virtual void OnDetach()
            {
            }

            protected void RaiseScrollChanged()
            {
                if (this.ScrollChanged != null)
                {
                    this.ScrollChanged(this, EventArgs.Empty);
                }
            }

            protected FrameworkElement AssociatedObject { get; private set; }
        }

        private class ScrollableObjectFactory
        {
            private IDictionary<Type, CreateInstance> initializers = new Dictionary<Type, CreateInstance>();

            public ScrollableObjectFactory()
            {
                this.InitializeFactory();
            }

            private Type GetInitializerType(IDictionary<Type, CreateInstance> initializers, Type elementType) => 
                !elementType.GetInterfaces().Contains<Type>(typeof(IScrollBarUpdated)) ? initializers.Keys.FirstOrDefault<Type>(x => x.IsAssignableFrom(elementType)) : typeof(IScrollBarUpdated);

            private void InitializeFactory()
            {
                CreateInstance instance1 = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    CreateInstance local1 = <>c.<>9__5_0;
                    instance1 = <>c.<>9__5_0 = (CreateInstance) (() => new ScrollViewerTouchBehavior.ScrollViewerScrollableObject());
                }
                this.Initializers[typeof(ScrollViewer)] = instance1;
                CreateInstance instance2 = <>c.<>9__5_1;
                if (<>c.<>9__5_1 == null)
                {
                    CreateInstance local2 = <>c.<>9__5_1;
                    instance2 = <>c.<>9__5_1 = (CreateInstance) (() => new ScrollViewerTouchBehavior.ScrollBarObject());
                }
                this.Initializers[typeof(IScrollBarUpdated)] = instance2;
            }

            public ScrollViewerTouchBehavior.ScrollableObjectBase Resolve(object associatedObject)
            {
                if (associatedObject != null)
                {
                    CreateInstance instance = null;
                    Type initializerType = this.GetInitializerType(this.initializers, associatedObject.GetType());
                    if ((initializerType != null) && this.initializers.TryGetValue(initializerType, out instance))
                    {
                        return instance();
                    }
                }
                return new ScrollViewerTouchBehavior.ScrollableObjectBase();
            }

            private IDictionary<Type, CreateInstance> Initializers =>
                this.initializers;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ScrollViewerTouchBehavior.ScrollableObjectFactory.<>c <>9 = new ScrollViewerTouchBehavior.ScrollableObjectFactory.<>c();
                public static ScrollViewerTouchBehavior.ScrollableObjectFactory.CreateInstance <>9__5_0;
                public static ScrollViewerTouchBehavior.ScrollableObjectFactory.CreateInstance <>9__5_1;

                internal ScrollViewerTouchBehavior.ScrollableObjectBase <InitializeFactory>b__5_0() => 
                    new ScrollViewerTouchBehavior.ScrollViewerScrollableObject();

                internal ScrollViewerTouchBehavior.ScrollableObjectBase <InitializeFactory>b__5_1() => 
                    new ScrollViewerTouchBehavior.ScrollBarObject();
            }

            private delegate ScrollViewerTouchBehavior.ScrollableObjectBase CreateInstance();
        }

        private class ScrollBarObject : ScrollViewerTouchBehavior.ScrollableObjectBase
        {
            private IScrollBarUpdated sourceObject;

            protected override void OnAttach()
            {
                base.OnAttach();
                this.sourceObject = base.AssociatedObject as IScrollBarUpdated;
                if (this.sourceObject != null)
                {
                    this.sourceObject.ScrollBarUpdated += new EventHandler(this.PanelBase_ScrollChanged);
                }
            }

            protected override void OnDetach()
            {
                if (this.sourceObject != null)
                {
                    this.sourceObject.ScrollBarUpdated -= new EventHandler(this.PanelBase_ScrollChanged);
                    base.OnDetach();
                }
            }

            private void PanelBase_ScrollChanged(object sender, EventArgs e)
            {
                base.RaiseScrollChanged();
            }
        }

        private class ScrollViewerScrollableObject : ScrollViewerTouchBehavior.ScrollableObjectBase
        {
            private ScrollViewer scrollViewer;

            protected override void OnAttach()
            {
                base.OnAttach();
                this.scrollViewer = base.AssociatedObject as ScrollViewer;
                if (this.scrollViewer != null)
                {
                    this.scrollViewer.ScrollChanged += new ScrollChangedEventHandler(this.OnScrollChanged);
                }
            }

            protected override void OnDetach()
            {
                if (this.scrollViewer != null)
                {
                    this.scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(this.OnScrollChanged);
                }
                base.OnDetach();
            }

            private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
            {
                if (e.OriginalSource == base.AssociatedObject)
                {
                    base.RaiseScrollChanged();
                }
            }
        }
    }
}

