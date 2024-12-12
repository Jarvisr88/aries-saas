namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class FloatPanePresenter : BaseFloatingContainer, IUIElement, IDisposable
    {
        public static readonly DependencyProperty BorderStyleProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty FormBorderMarginProperty;
        public static readonly DependencyProperty SingleBorderMarginProperty;
        public static readonly DependencyProperty MaximizedBorderMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualContentMarginProperty;
        public static readonly DependencyProperty ShadowMarginProperty;
        public static readonly DependencyProperty BorderMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualShadowMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualBorderMarginProperty;
        private bool isDisposing;
        private UIChildren uiChildren = new UIChildren();

        static FloatPanePresenter()
        {
            DependencyPropertyRegistrator<FloatPanePresenter> registrator = new DependencyPropertyRegistrator<FloatPanePresenter>();
            registrator.OverrideFrameworkMetadata<bool>(BaseFloatingContainer.IsOpenProperty, true, null, null);
            registrator.Register<FloatGroupBorderStyle>("BorderStyle", ref BorderStyleProperty, FloatGroupBorderStyle.Empty, (dObj, e) => ((FloatPanePresenter) dObj).OnBorderStyleChanged((FloatGroupBorderStyle) e.NewValue), null);
            registrator.Register<bool>("IsMaximized", ref IsMaximizedProperty, false, (dObj, e) => ((FloatPanePresenter) dObj).OnIsMaximizedChanged((bool) e.NewValue), null);
            registrator.Register<Thickness>("ShadowMargin", ref ShadowMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FloatPanePresenter) dObj).OnBorderMarginChanged(), null);
            registrator.Register<Thickness>("BorderMargin", ref BorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FloatPanePresenter) dObj).OnBorderMarginChanged(), null);
            registrator.Register<Thickness>("ActualBorderMargin", ref ActualBorderMarginProperty, new Thickness(0.0), null, (CoerceValueCallback) ((dObj, value) => ((FloatPanePresenter) dObj).CoerceActualBorderMargin()));
            registrator.Register<Thickness>("ActualShadowMargin", ref ActualShadowMarginProperty, new Thickness(0.0), null, (CoerceValueCallback) ((dObj, value) => ((FloatPanePresenter) dObj).CoerceActualShadowMargin()));
            registrator.Register<Thickness>("SingleBorderMargin", ref SingleBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FloatPanePresenter) dObj).OnContentMarginChanged(), null);
            registrator.Register<Thickness>("FormBorderMargin", ref FormBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FloatPanePresenter) dObj).OnContentMarginChanged(), null);
            registrator.Register<Thickness>("MaximizedBorderMargin", ref MaximizedBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FloatPanePresenter) dObj).OnContentMarginChanged(), null);
            registrator.Register<Thickness>("ActualContentMargin", ref ActualContentMarginProperty, new Thickness(0.0), null, (CoerceValueCallback) ((dObj, value) => ((FloatPanePresenter) dObj).CoerceActualContentMargin()));
        }

        protected FloatPanePresenter()
        {
        }

        public void Activate(DockLayoutManager container)
        {
            this.BringToFront(container);
            this.ActivateContentHolder();
        }

        protected abstract void ActivateContentHolder();
        protected virtual Thickness CoerceActualBorderMargin()
        {
            Thickness thickness = !this.IsMaximized ? this.BorderMargin : new Thickness(0.0);
            return (!MathHelper.AreEqual(thickness, new Thickness(double.NaN)) ? thickness : new Thickness(0.0));
        }

        protected virtual Thickness CoerceActualContentMargin()
        {
            Thickness maximizedBorderMargin = this.MaximizedBorderMargin;
            if (!this.IsMaximized)
            {
                maximizedBorderMargin = (this.BorderStyle == FloatGroupBorderStyle.Single) ? this.SingleBorderMargin : this.FormBorderMargin;
            }
            return (!MathHelper.AreEqual(maximizedBorderMargin, new Thickness(double.NaN)) ? maximizedBorderMargin : new Thickness(0.0));
        }

        protected virtual Thickness CoerceActualShadowMargin()
        {
            Thickness thickness = !this.IsMaximized ? this.ShadowMargin : new Thickness(0.0);
            return (!MathHelper.AreEqual(thickness, new Thickness(double.NaN)) ? thickness : new Thickness(0.0));
        }

        protected override BaseFloatingContainer.ManagedContentPresenter CreateContentPresenter() => 
            new FloatingContentPresenter(this);

        public void Deactivate()
        {
            this.OnIsOpenChanged(false);
            this.DeactivateContentHolder();
        }

        protected abstract void DeactivateContentHolder();
        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.DeactivateContentHolder();
                this.OnDisposing();
                base.Owner = null;
                if (base.Presenter != null)
                {
                    base.Presenter.Content = null;
                }
                if (base.Decorator != null)
                {
                    base.Decorator.Child = null;
                }
                DockLayoutManager.Release(base.Presenter);
                DockLayoutManager.Release(this);
            }
            GC.SuppressFinalize(this);
        }

        internal virtual void EnsureOwnerWindow()
        {
        }

        internal virtual Thickness GetFloatingMargin()
        {
            if (base.Presenter != null)
            {
                return ((FloatingContentPresenter) base.Presenter).GetFloatingMargin();
            }
            return new Thickness();
        }

        public static void Invalidate(DependencyObject dObj)
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.Ensure(this, false);
        }

        protected virtual void OnBorderMarginChanged()
        {
            base.CoerceValue(ActualBorderMarginProperty);
            base.CoerceValue(ActualShadowMarginProperty);
        }

        protected virtual void OnBorderStyleChanged(FloatGroupBorderStyle borderStyle)
        {
            base.CoerceValue(ActualContentMarginProperty);
            if (base.Presenter != null)
            {
                ((FloatingContentPresenter) base.Presenter).UpdateBorderStyle(borderStyle);
            }
        }

        protected override void OnContentChanged(object content)
        {
            this.Container = DockLayoutManager.Ensure(this, false);
            if ((content == null) && (this.Container == null))
            {
                this.Deactivate();
            }
            base.Owner = this.Container;
            base.OnContentChanged(content);
        }

        protected virtual void OnContentMarginChanged()
        {
            base.CoerceValue(ActualContentMarginProperty);
        }

        protected override void OnContentUpdated(object content, FrameworkElement owner)
        {
        }

        protected abstract void OnDisposing();
        protected override void OnHierarchyCreated()
        {
            base.OnHierarchyCreated();
            this.Forward(base.Presenter, FloatingContentPresenter.ActualContentMarginProperty, "ActualContentMargin", BindingMode.OneWay);
            this.Forward(base.Presenter, FloatingContentPresenter.ActualBorderMarginProperty, "ActualBorderMargin", BindingMode.OneWay);
            this.Forward(base.Presenter, FloatingContentPresenter.ActualShadowMarginProperty, "ActualShadowMargin", BindingMode.OneWay);
        }

        protected virtual void OnIsMaximizedChanged(bool maximized)
        {
            base.CoerceValue(ActualContentMarginProperty);
            base.CoerceValue(ActualBorderMarginProperty);
            base.CoerceValue(ActualShadowMarginProperty);
            if (base.Presenter != null)
            {
                ((FloatingContentPresenter) base.Presenter).UpdateResizeBordersVisibility(maximized);
            }
        }

        internal abstract bool TryGetActualRenderSize(out Size autoSize);

        public FloatGroupBorderStyle BorderStyle
        {
            get => 
                (FloatGroupBorderStyle) base.GetValue(BorderStyleProperty);
            set => 
                base.SetValue(BorderStyleProperty, value);
        }

        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            set => 
                base.SetValue(IsMaximizedProperty, value);
        }

        public Thickness ShadowMargin
        {
            get => 
                (Thickness) base.GetValue(ShadowMarginProperty);
            set => 
                base.SetValue(ShadowMarginProperty, value);
        }

        public Thickness BorderMargin
        {
            get => 
                (Thickness) base.GetValue(BorderMarginProperty);
            set => 
                base.SetValue(BorderMarginProperty, value);
        }

        public Thickness FormBorderMargin
        {
            get => 
                (Thickness) base.GetValue(FormBorderMarginProperty);
            set => 
                base.SetValue(FormBorderMarginProperty, value);
        }

        public Thickness SingleBorderMargin
        {
            get => 
                (Thickness) base.GetValue(SingleBorderMarginProperty);
            set => 
                base.SetValue(SingleBorderMarginProperty, value);
        }

        public Thickness MaximizedBorderMargin
        {
            get => 
                (Thickness) base.GetValue(MaximizedBorderMarginProperty);
            set => 
                base.SetValue(MaximizedBorderMarginProperty, value);
        }

        public Thickness ActualShadowMargin =>
            (Thickness) base.GetValue(ActualShadowMarginProperty);

        public Thickness ActualBorderMargin =>
            (Thickness) base.GetValue(ActualBorderMarginProperty);

        public Thickness ActualContentMargin =>
            (Thickness) base.GetValue(ActualContentMarginProperty);

        public UIElement Element =>
            base.Presenter;

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetDockLayoutManager(this);

        UIChildren IUIElement.Children
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        protected DockLayoutManager Container { get; private set; }

        protected override IEnumerator LogicalChildren =>
            new SingleLogicalChildEnumerator(base.Presenter);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatPanePresenter.<>c <>9 = new FloatPanePresenter.<>c();

            internal void <.cctor>b__10_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnBorderStyleChanged((FloatGroupBorderStyle) e.NewValue);
            }

            internal void <.cctor>b__10_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnIsMaximizedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__10_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnBorderMarginChanged();
            }

            internal void <.cctor>b__10_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnBorderMarginChanged();
            }

            internal object <.cctor>b__10_4(DependencyObject dObj, object value) => 
                ((FloatPanePresenter) dObj).CoerceActualBorderMargin();

            internal object <.cctor>b__10_5(DependencyObject dObj, object value) => 
                ((FloatPanePresenter) dObj).CoerceActualShadowMargin();

            internal void <.cctor>b__10_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnContentMarginChanged();
            }

            internal void <.cctor>b__10_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnContentMarginChanged();
            }

            internal void <.cctor>b__10_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FloatPanePresenter) dObj).OnContentMarginChanged();
            }

            internal object <.cctor>b__10_9(DependencyObject dObj, object value) => 
                ((FloatPanePresenter) dObj).CoerceActualContentMargin();
        }

        [TemplatePart(Name="PART_Content", Type=typeof(UIElement)), TemplatePart(Name="PART_SingleBorder", Type=typeof(Grid)), TemplatePart(Name="PART_FormBorder", Type=typeof(Grid)), TemplatePart(Name="PART_ResizeBorders", Type=typeof(Grid))]
        public class FloatingContentPresenter : BaseFloatingContainer.ManagedContentPresenter
        {
            public static readonly DependencyProperty ActualContentMarginProperty;
            public static readonly DependencyProperty ActualShadowMarginProperty;
            public static readonly DependencyProperty ActualBorderMarginProperty;

            static FloatingContentPresenter()
            {
                DependencyPropertyRegistrator<FloatPanePresenter.FloatingContentPresenter> registrator = new DependencyPropertyRegistrator<FloatPanePresenter.FloatingContentPresenter>();
                registrator.Register<Thickness>("ActualContentMargin", ref ActualContentMarginProperty, new Thickness(0.0), null, null);
                registrator.Register<Thickness>("ActualShadowMargin", ref ActualShadowMarginProperty, new Thickness(0.0), null, null);
                registrator.Register<Thickness>("ActualBorderMargin", ref ActualBorderMarginProperty, new Thickness(0.0), null, null);
            }

            public FloatingContentPresenter(FloatPanePresenter container) : base(container)
            {
            }

            public Thickness GetFloatingMargin()
            {
                BaseLayoutItem item = ((IDockLayoutElement) this.Presenter.Container.GetViewElement(this.Presenter)).Item;
                FloatGroup group = item as FloatGroup;
                if (group == null)
                {
                    return new Thickness();
                }
                if (group.HasSingleItem)
                {
                    item = group.Items[0];
                }
                return item.GetFloatingBorderMargin();
            }

            public override void OnApplyTemplate()
            {
                base.OnApplyTemplate();
                this.Manager = DockLayoutManager.Ensure(this, false);
                if (this.Manager != null)
                {
                    FloatingView view = this.Manager.ViewAdapter.GetView(base.Container) as FloatingView;
                    if (view != null)
                    {
                        view.Initialize((IUIElement) base.Container);
                    }
                }
                this.PartContent = base.GetTemplateChild("PART_Content") as UIElement;
                this.PartSingleBorder = base.GetTemplateChild("PART_SingleBorder") as FrameworkElement;
                this.PartFormBorder = base.GetTemplateChild("PART_FormBorder") as FrameworkElement;
                this.PartResizeBorders = base.GetTemplateChild("PART_ResizeBorders") as FrameworkElement;
                this.PartBorderControl = base.GetTemplateChild("PART_FormBorderControl") as FormBorderControl;
                this.UpdateBorderStyle(this.Presenter.BorderStyle);
                this.UpdateResizeBordersVisibility(this.Presenter.IsMaximized);
                this.SetupBindings();
            }

            private void SetupBindings()
            {
                if (this.PartBorderControl != null)
                {
                    BindingHelper.SetBinding(this.PartBorderControl, FormBorderControl.ActualBorderMarginProperty, this.Presenter, FloatPanePresenter.ActualBorderMarginProperty, BindingMode.OneWay);
                    BindingHelper.SetBinding(this.PartBorderControl, FormBorderControl.ActualShadowMarginProperty, this.Presenter, FloatPanePresenter.ActualShadowMarginProperty, BindingMode.OneWay);
                    BindingHelper.SetBinding(this.PartBorderControl, FormBorderControl.IsMaximizedProperty, this.Presenter, FloatPanePresenter.IsMaximizedProperty, BindingMode.OneWay);
                }
            }

            internal void UpdateBorderStyle(FloatGroupBorderStyle borderStyle)
            {
                this.UpdateSingleBorderVisibility(borderStyle);
                this.UpdateFormBorderVisibility(borderStyle);
            }

            private void UpdateFormBorderVisibility(FloatGroupBorderStyle borderStyle)
            {
                if (this.PartFormBorder != null)
                {
                    this.PartFormBorder.Visibility = (borderStyle == FloatGroupBorderStyle.Form) ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            internal void UpdateResizeBordersVisibility(bool maximized)
            {
                if (this.PartResizeBorders != null)
                {
                    this.PartResizeBorders.Visibility = maximized ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            private void UpdateSingleBorderVisibility(FloatGroupBorderStyle borderStyle)
            {
                if (this.PartSingleBorder != null)
                {
                    this.PartSingleBorder.Visibility = (borderStyle == FloatGroupBorderStyle.Single) ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            public UIElement PartContent { get; private set; }

            public FrameworkElement PartSingleBorder { get; private set; }

            public FrameworkElement PartFormBorder { get; private set; }

            public FrameworkElement PartResizeBorders { get; private set; }

            public DockLayoutManager Manager { get; private set; }

            public FormBorderControl PartBorderControl { get; private set; }

            private FloatPanePresenter Presenter =>
                (FloatPanePresenter) base.Container;

            public Thickness ActualShadowMargin
            {
                get => 
                    (Thickness) base.GetValue(ActualShadowMarginProperty);
                set => 
                    base.SetValue(ActualShadowMarginProperty, value);
            }

            public Thickness ActualBorderMargin
            {
                get => 
                    (Thickness) base.GetValue(ActualBorderMarginProperty);
                set => 
                    base.SetValue(ActualBorderMarginProperty, value);
            }

            public Thickness ActualContentMargin
            {
                get => 
                    (Thickness) base.GetValue(ActualContentMarginProperty);
                set => 
                    base.SetValue(ActualContentMarginProperty, value);
            }
        }
    }
}

