namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [TemplateVisualState(Name="Pressed", GroupName="CommonStates"), TemplateVisualState(Name="MouseOver", GroupName="CommonStates"), TemplateVisualState(Name="Disabled", GroupName="CommonStates"), TemplateVisualState(Name="Normal", GroupName="CommonStates")]
    public abstract class ControlBase : Control, IControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ClipListener = RegisterPropertyListener("Clip");
        private bool _IsInitializingPropertyListener;
        private bool _IsUpdatingClip;

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

        public ControlBase()
        {
            this.Controller = this.CreateController();
            this.AttachPropertyListener("Clip", ClipListener, null);
            base.LayoutUpdated += (sender, e) => this.OnLayoutUpdated();
            base.Loaded += (sender, e) => this.OnLoaded();
            base.SizeChanged += (sender, e) => this.OnSizeChanged(e);
            base.Unloaded += (sender, e) => this.OnUnloaded();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = this.OnArrange(RectHelper.New(finalSize));
            this.Controller.UpdateScrolling();
            return size;
        }

        protected void AttachPropertyListener(string propertyName, DependencyProperty propertyListener, object source = null)
        {
            this._IsInitializingPropertyListener = true;
            try
            {
                Binding binding = new Binding(propertyName);
                object obj1 = source;
                if (source == null)
                {
                    object local1 = source;
                    obj1 = this;
                }
                binding.Source = obj1;
                this.SetBinding(propertyListener, binding);
            }
            finally
            {
                this._IsInitializingPropertyListener = false;
            }
        }

        protected void Changed()
        {
            base.InvalidateMeasure();
        }

        protected virtual ControlControllerBase CreateController() => 
            new ControlControllerBase(this);

        protected void DetachPropertyListener(DependencyProperty propertyListener)
        {
            this._IsInitializingPropertyListener = true;
            try
            {
                base.ClearValue(propertyListener);
            }
            finally
            {
                this._IsInitializingPropertyListener = false;
            }
        }

        protected virtual Geometry GetGeometry()
        {
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = this.GetSize().ToRect();
            return geometry1;
        }

        protected void GoToState(string stateName, bool useTransitions)
        {
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            this.Controller.ResetScrollBarsVisibility();
            while (true)
            {
                Size size = this.OnMeasure(availableSize);
                this.OriginalDesiredSize = size;
                size.Width = Math.Min(size.Width, availableSize.Width);
                size.Height = Math.Min(size.Height, availableSize.Height);
                this.Controller.UpdateScrollBarsVisibility();
                if ((size.Width == 0.0) || ((size.Height == 0.0) || ((size.Width <= availableSize.Width) && (size.Height <= availableSize.Height))))
                {
                    return size;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateState(false);
        }

        protected virtual Size OnArrange(Rect bounds) => 
            base.ArrangeOverride(bounds.Size());

        protected virtual void OnLayoutUpdated()
        {
        }

        protected virtual void OnLoaded()
        {
        }

        protected virtual Size OnMeasure(Size availableSize) => 
            base.MeasureOverride(availableSize);

        protected virtual void OnPropertyChanged(DependencyProperty propertyListener, object oldValue, object newValue)
        {
            if (ReferenceEquals(propertyListener, ClipListener))
            {
                this.UpdateClip();
            }
        }

        protected virtual void OnSizeChanged(SizeChangedEventArgs e)
        {
            this.UpdateClip();
        }

        protected virtual void OnUnloaded()
        {
        }

        protected static DependencyProperty RegisterPropertyListener(string propertyListenerName)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__0_0;
                propertyChangedCallback = <>c.<>9__0_0 = delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                    ControlBase base2 = (ControlBase) o;
                    if (!base2._IsInitializingPropertyListener)
                    {
                        base2.OnPropertyChanged(e.Property, e.OldValue, e.NewValue);
                    }
                };
            }
            return DependencyProperty.Register(propertyListenerName + "Listener", typeof(object), typeof(ControlBase), new PropertyMetadata(propertyChangedCallback));
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

        protected virtual void UpdateState(bool useTransitions)
        {
            this.Controller.UpdateState(false);
        }

        public Rect AbsoluteBounds =>
            this.GetBounds(null);

        public Point AbsolutePosition =>
            this.GetPosition(null);

        public Rect Bounds =>
            this.GetBounds();

        public ControlControllerBase Controller { get; private set; }

        protected Size OriginalDesiredSize { get; private set; }

        protected virtual bool IsClipped =>
            this.Controller.IsScrollable();

        FrameworkElement IControl.Control =>
            this;

        DevExpress.Xpf.Core.Controller IControl.Controller =>
            this.Controller;

        bool IControl.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ControlBase.<>c <>9 = new ControlBase.<>c();
            public static PropertyChangedCallback <>9__0_0;

            internal void <RegisterPropertyListener>b__0_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ControlBase base2 = (ControlBase) o;
                if (!base2._IsInitializingPropertyListener)
                {
                    base2.OnPropertyChanged(e.Property, e.OldValue, e.NewValue);
                }
            }
        }
    }
}

