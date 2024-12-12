namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class FormBorderControl : psvControl
    {
        public static readonly DependencyProperty SingleBorderTemplateProperty;
        public static readonly DependencyProperty FormBorderTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualShadowMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualContentMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualBorderMarginProperty;
        public static readonly DependencyProperty BorderStyleProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty EmptyBorderTemplateProperty;
        public static readonly DependencyProperty FormBorderMarginProperty;
        public static readonly DependencyProperty SingleBorderMarginProperty;
        public static readonly DependencyProperty MaximizedBorderMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemProperty;

        static FormBorderControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<FormBorderControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<FormBorderControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<ControlTemplate>("EmptyBorderTemplate", ref EmptyBorderTemplateProperty, null, (dObj, e) => ((FormBorderControl) dObj).SelectContainerTemplate(), null);
            registrator.Register<ControlTemplate>("SingleBorderTemplate", ref SingleBorderTemplateProperty, null, (dObj, e) => ((FormBorderControl) dObj).SelectContainerTemplate(), null);
            registrator.Register<ControlTemplate>("FormBorderTemplate", ref FormBorderTemplateProperty, null, (dObj, e) => ((FormBorderControl) dObj).SelectContainerTemplate(), null);
            registrator.Register<FloatGroupBorderStyle>("BorderStyle", ref BorderStyleProperty, FloatGroupBorderStyle.Form, (dObj, e) => ((FormBorderControl) dObj).OnBorderStyleChanged((FloatGroupBorderStyle) e.OldValue, (FloatGroupBorderStyle) e.NewValue), null);
            registrator.Register<bool>("IsMaximized", ref IsMaximizedProperty, false, (dObj, e) => ((FormBorderControl) dObj).OnIsMaximizedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("ActualBorderMargin", ref ActualBorderMarginProperty, defValue, null, null);
            defValue = new Thickness();
            registrator.Register<Thickness>("ActualShadowMargin", ref ActualShadowMarginProperty, defValue, null, null);
            defValue = new Thickness();
            registrator.Register<Thickness>("ActualContentMargin", ref ActualContentMarginProperty, defValue, null, (CoerceValueCallback) ((dObj, value) => ((FormBorderControl) dObj).CoerceActualContentMargin()));
            registrator.Register<Thickness>("SingleBorderMargin", ref SingleBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FormBorderControl) dObj).OnContentMarginChanged(), null);
            registrator.Register<Thickness>("FormBorderMargin", ref FormBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FormBorderControl) dObj).OnContentMarginChanged(), null);
            registrator.Register<Thickness>("MaximizedBorderMargin", ref MaximizedBorderMarginProperty, new Thickness(double.NaN), (dObj, e) => ((FormBorderControl) dObj).OnContentMarginChanged(), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(FormBorderControl), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FormBorderControl>.New().AddOwner<BaseLayoutItem>(System.Linq.Expressions.Expression.Lambda<Func<FormBorderControl, BaseLayoutItem>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockLayoutManager.GetLayoutItem), arguments), parameters), out LayoutItemProperty, DockLayoutManager.LayoutItemProperty, null, (d, oldValue, newValue) => d.OnLayoutItemChanged(oldValue, newValue));
        }

        public FormBorderControl()
        {
            base.DefaultStyleKey = typeof(FormBorderControl);
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartItemsControl != null) && !LayoutItemsHelper.IsTemplateChild<FormBorderControl>(this.PartItemsControl, this))
            {
                this.PartItemsControl.Dispose();
            }
            FormBorderContentControl templateChild = LayoutItemsHelper.GetTemplateChild<FormBorderContentControl>(this);
            this.PartItemsControl = (templateChild != null) ? (templateChild.Content as LayoutItemsControl) : LayoutItemsHelper.GetTemplateChild<LayoutItemsControl>(this);
            this.UpdateVisualState();
        }

        protected virtual void OnBorderStyleChanged(FloatGroupBorderStyle oldValue, FloatGroupBorderStyle newValue)
        {
            base.CoerceValue(ActualContentMarginProperty);
            this.SelectContainerTemplate();
        }

        protected virtual void OnContentMarginChanged()
        {
            base.CoerceValue(ActualContentMarginProperty);
        }

        protected override void OnDispose()
        {
            if (this.PartItemsControl != null)
            {
                this.PartItemsControl.Dispose();
                this.PartItemsControl = null;
            }
            base.OnDispose();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SelectContainerTemplate();
        }

        protected virtual void OnIsActiveChanged(bool value)
        {
            this.UpdateVisualState();
        }

        protected virtual void OnIsMaximizedChanged(bool oldValue, bool newValue)
        {
            base.CoerceValue(ActualContentMarginProperty);
            this.UpdateVisualState();
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            FloatGroup control = newValue as FloatGroup;
            if (control != null)
            {
                control.Forward(this, BorderStyleProperty, "BorderStyle", BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(BorderStyleProperty);
            }
        }

        private void SelectContainerTemplate()
        {
            if (base.IsInitializedCore)
            {
                switch (this.BorderStyle)
                {
                    case FloatGroupBorderStyle.Single:
                        base.Template = this.SingleBorderTemplate;
                        return;

                    case FloatGroupBorderStyle.Form:
                        base.Template = this.FormBorderTemplate;
                        return;

                    case FloatGroupBorderStyle.Empty:
                        base.Template = this.EmptyBorderTemplate;
                        return;
                }
            }
        }

        private void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, this.IsMaximized ? "Maximized" : "EmptySizeState", false);
        }

        protected LayoutItemsControl PartItemsControl { get; private set; }

        public ControlTemplate FormBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(FormBorderTemplateProperty);
            set => 
                base.SetValue(FormBorderTemplateProperty, value);
        }

        public ControlTemplate SingleBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SingleBorderTemplateProperty);
            set => 
                base.SetValue(SingleBorderTemplateProperty, value);
        }

        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            set => 
                base.SetValue(IsMaximizedProperty, value);
        }

        public FloatGroupBorderStyle BorderStyle
        {
            get => 
                (FloatGroupBorderStyle) base.GetValue(BorderStyleProperty);
            set => 
                base.SetValue(BorderStyleProperty, value);
        }

        public ControlTemplate EmptyBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyBorderTemplateProperty);
            set => 
                base.SetValue(EmptyBorderTemplateProperty, value);
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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormBorderControl.<>c <>9 = new FormBorderControl.<>c();

            internal void <.cctor>b__12_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).SelectContainerTemplate();
            }

            internal void <.cctor>b__12_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).SelectContainerTemplate();
            }

            internal void <.cctor>b__12_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).SelectContainerTemplate();
            }

            internal void <.cctor>b__12_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).OnBorderStyleChanged((FloatGroupBorderStyle) e.OldValue, (FloatGroupBorderStyle) e.NewValue);
            }

            internal void <.cctor>b__12_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).OnIsMaximizedChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal object <.cctor>b__12_5(DependencyObject dObj, object value) => 
                ((FormBorderControl) dObj).CoerceActualContentMargin();

            internal void <.cctor>b__12_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).OnContentMarginChanged();
            }

            internal void <.cctor>b__12_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).OnContentMarginChanged();
            }

            internal void <.cctor>b__12_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderControl) dObj).OnContentMarginChanged();
            }

            internal void <.cctor>b__12_9(FormBorderControl d, BaseLayoutItem oldValue, BaseLayoutItem newValue)
            {
                d.OnLayoutItemChanged(oldValue, newValue);
            }
        }
    }
}

