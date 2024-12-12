namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    [TemplatePart(Name="PART_CustomContent", Type=typeof(ContentPresenter)), TemplatePart(Name="PART_CloseButton", Type=typeof(ControlBoxButtonPresenter))]
    public class BaseControlBoxControl : psvControl
    {
        public static readonly DependencyProperty ButtonWidthProperty;
        public static readonly DependencyProperty ButtonHeightProperty;
        public static readonly DependencyProperty CloseButtonTemplateProperty;
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty ButtonStyleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ControlBoxProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsCloseButtonVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ControlBoxContentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ControlBoxContentTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsControlBoxVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsActuallyVisibleProperty;
        private DelegateCommand<object> _ContextCommand;

        static BaseControlBoxControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseControlBoxControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<BaseControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<double>("ButtonWidth", ref ButtonWidthProperty, double.NaN, null, null);
            registrator.Register<double>("ButtonHeight", ref ButtonHeightProperty, double.NaN, null, null);
            registrator.Register<DataTemplate>("CloseButtonTemplate", ref CloseButtonTemplateProperty, null, null, null);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, ea) => ((BaseControlBoxControl) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.NewValue), null);
            registrator.Register<Style>("ButtonStyle", ref ButtonStyleProperty, null, null, null);
            registrator.RegisterAttachedInherited<BaseControlBoxControl>("ControlBox", ref ControlBoxProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseControlBoxControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseControlBoxControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseControlBoxControl>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BaseControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseControlBoxControl.get_IsCloseButtonVisible)), parameters), out IsCloseButtonVisibleProperty, false, (d, oldValue, newValue) => d.OnIsCloseButtonVisibleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseControlBoxControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseControlBoxControl> registrator2 = registrator1.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<BaseControlBoxControl, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseControlBoxControl.get_ControlBoxContent)), expressionArray2), out ControlBoxContentProperty, null, (d, oldValue, newValue) => d.OnControlBoxContentChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseControlBoxControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseControlBoxControl> registrator3 = registrator2.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<BaseControlBoxControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseControlBoxControl.get_ControlBoxContentTemplate)), expressionArray3), out ControlBoxContentTemplateProperty, null, (d, oldValue, newValue) => d.OnControlBoxContentTemplateChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseControlBoxControl), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseControlBoxControl> registrator4 = registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BaseControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseControlBoxControl.get_IsControlBoxVisible)), expressionArray4), out IsControlBoxVisibleProperty, false, (d, oldValue, newValue) => d.OnIsControlBoxVisibleChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseControlBoxControl), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<BaseControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseControlBoxControl.get_IsActuallyVisible)), expressionArray5), out IsActuallyVisibleProperty, true, (d, oldValue, newValue) => d.OnIsActuallyVisibleChanged(oldValue, newValue), (d, value) => d.OnCoerceIsActuallyVisible(value), frameworkOptions);
        }

        public BaseControlBoxControl()
        {
            base.Background = Brushes.Transparent;
            DockPane.SetHitTestType(this, HitTestType.ControlBox);
            base.SetValue(ControlBoxProperty, this);
        }

        private bool CanExecute(object param)
        {
            ContextAction contextAction = GetContextAction(param);
            Func<bool> fallback = <>c.<>9__77_1;
            if (<>c.<>9__77_1 == null)
            {
                Func<bool> local1 = <>c.<>9__77_1;
                fallback = <>c.<>9__77_1 = () => false;
            }
            return base.Container.Return<DockLayoutManager, bool>(x => x.CanExecuteContextAction(contextAction, this.LayoutItem), fallback);
        }

        protected virtual void ClearControlBoxBindings()
        {
            if ((this.PartCustomContent != null) && this.AllowCustomContent)
            {
                this.PartCustomContent.ClearValue(ContentPresenter.ContentProperty);
                this.PartCustomContent.ClearValue(ContentPresenter.ContentTemplateProperty);
                this.PartCustomContent.ClearValue(UIElement.VisibilityProperty);
            }
            if (this.PartCloseButton != null)
            {
                this.PartCloseButton.ClearValue(UIElement.VisibilityProperty);
            }
            base.ClearValue(IsCloseButtonVisibleProperty);
            base.ClearValue(ControlBoxContentProperty);
            base.ClearValue(ControlBoxContentTemplateProperty);
            base.ClearValue(IsControlBoxVisibleProperty);
        }

        protected void CoerceIsActuallyVisible()
        {
            base.CoerceValue(IsActuallyVisibleProperty);
        }

        private void EnsureControlBoxContent()
        {
            if (this.LayoutItem != null)
            {
                this.SetControlBoxBindings();
            }
            else
            {
                this.ClearControlBoxBindings();
            }
        }

        protected T EnsurePresenter<T>(T part, string name) where T: psvContentPresenter
        {
            if ((part != null) && !LayoutItemsHelper.IsTemplateChild<BaseControlBoxControl>(part, this))
            {
                part.Dispose();
            }
            return (base.GetTemplateChild(name) as T);
        }

        protected virtual void EnsureTemplateChildren()
        {
            this.PartButtonsPanel = LayoutItemsHelper.GetChild<Panel>(this);
            this.PartCustomContent = base.GetTemplateChild("PART_CustomContent") as ContentPresenter;
            this.PartCloseButton = this.EnsurePresenter<ControlBoxButtonPresenter>(this.PartCloseButton, "PART_CloseButton");
        }

        private static ContextAction GetContextAction(object param)
        {
            try
            {
                return (ContextAction) TypeCastHelper.TryCast(param, typeof(ContextAction));
            }
            catch (Exception)
            {
                return ContextAction.Undefined;
            }
        }

        internal static BaseControlBoxControl GetControlBox(DependencyObject obj) => 
            (BaseControlBoxControl) obj.GetValue(ControlBoxProperty);

        internal static object GetToolTip(DockingStringId dockingStringId)
        {
            string str = DockingLocalizer.GetString(dockingStringId);
            return (string.IsNullOrEmpty(str) ? null : str);
        }

        public static void Invalidate(DependencyObject sender)
        {
            Action<BaseControlBoxControl> action = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Action<BaseControlBoxControl> local1 = <>c.<>9__15_0;
                action = <>c.<>9__15_0 = x => x.InvalidateMeasure();
            }
            GetControlBox(sender).Do<BaseControlBoxControl>(action);
        }

        protected void InvalidateContextCommand()
        {
            this._ContextCommand.RaiseCanExecuteChanged();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.EnsureTemplateChildren();
            if (this.PartCloseButton != null)
            {
                this.PartCloseButton.SetToolTip(GetToolTip(DockingStringId.ControlButtonClose));
            }
            this.EnsureControlBoxContent();
            this.CoerceIsActuallyVisible();
        }

        protected virtual bool OnCoerceIsActuallyVisible(bool value)
        {
            if (this.LayoutItem == null)
            {
                return true;
            }
            bool flag = (this.IsControlBoxVisible && (this.PartCustomContent != null)) && ((this.ControlBoxContent != null) || (this.ControlBoxContentTemplate != null));
            return (this.IsCloseButtonVisible || flag);
        }

        protected virtual void OnControlBoxContentChanged(object oldValue, object newValue)
        {
            this.CoerceIsActuallyVisible();
        }

        protected virtual void OnControlBoxContentTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            this.CoerceIsActuallyVisible();
        }

        protected override void OnDispose()
        {
            this.ClearControlBoxBindings();
            this.LayoutItem = null;
            if (this.PartButtonsPanel != null)
            {
                this.PartButtonsPanel = null;
            }
            if (this.PartCloseButton != null)
            {
                this.PartCloseButton.Dispose();
                this.PartCloseButton = null;
            }
            base.OnDispose();
        }

        private void OnExecute(object param)
        {
            ContextAction contextAction = GetContextAction(param);
            base.Container.Do<DockLayoutManager>(x => x.ExecuteContextAction(contextAction, this.LayoutItem));
        }

        protected virtual void OnIsActuallyVisibleChanged(bool oldValue, bool newValue)
        {
        }

        protected virtual void OnIsCloseButtonVisibleChanged(bool oldValue, bool newValue)
        {
            this.CoerceIsActuallyVisible();
        }

        protected virtual void OnIsControlBoxVisibleChanged(bool oldValue, bool newValue)
        {
            this.CoerceIsActuallyVisible();
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item)
        {
        }

        internal static void SetControlBox(DependencyObject obj, ControlBox value)
        {
            obj.SetValue(ControlBoxProperty, value);
        }

        protected virtual void SetControlBoxBindings()
        {
            BindingHelper.SetBinding(this, IsCloseButtonVisibleProperty, this.LayoutItem, BaseLayoutItem.IsCloseButtonVisibleProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ControlBoxContentProperty, this.LayoutItem, BaseLayoutItem.ControlBoxContentProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, ControlBoxContentTemplateProperty, this.LayoutItem, BaseLayoutItem.ControlBoxContentTemplateProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, IsControlBoxVisibleProperty, this.LayoutItem, BaseLayoutItem.IsControlBoxVisibleProperty, BindingMode.OneWay);
            if ((this.PartCustomContent != null) && this.AllowCustomContent)
            {
                BindingHelper.SetBinding(this.PartCustomContent, ContentPresenter.ContentProperty, this.LayoutItem, "ControlBoxContent");
                BindingHelper.SetBinding(this.PartCustomContent, ContentPresenter.ContentTemplateProperty, this.LayoutItem, "ControlBoxContentTemplate");
                BindingHelper.SetBinding(this.PartCustomContent, UIElement.VisibilityProperty, this.LayoutItem, BaseLayoutItem.IsControlBoxVisibleProperty, new BooleanToVisibilityConverter());
            }
            if (this.PartCloseButton != null)
            {
                BindingHelper.SetBinding(this.PartCloseButton, UIElement.VisibilityProperty, this.LayoutItem, BaseLayoutItem.IsCloseButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
        }

        [Obsolete, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected internal virtual void SetHotButton(HitTestType hitTest)
        {
        }

        [Obsolete, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected internal virtual void SetPressedButton(HitTestType hitTest)
        {
        }

        public double ButtonHeight
        {
            get => 
                (double) base.GetValue(ButtonHeightProperty);
            set => 
                base.SetValue(ButtonHeightProperty, value);
        }

        public Style ButtonStyle
        {
            get => 
                (Style) base.GetValue(ButtonStyleProperty);
            set => 
                base.SetValue(ButtonStyleProperty, value);
        }

        public double ButtonWidth
        {
            get => 
                (double) base.GetValue(ButtonWidthProperty);
            set => 
                base.SetValue(ButtonWidthProperty, value);
        }

        public DataTemplate CloseButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CloseButtonTemplateProperty);
            set => 
                base.SetValue(CloseButtonTemplateProperty, value);
        }

        public ICommand ContextCommand
        {
            get
            {
                DelegateCommand<object> command2 = this._ContextCommand;
                if (this._ContextCommand == null)
                {
                    DelegateCommand<object> local1 = this._ContextCommand;
                    command2 = this._ContextCommand = new DelegateCommand<object>(new Action<object>(this.OnExecute), new Func<object, bool>(this.CanExecute), false);
                }
                return command2;
            }
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public ControlBoxButtonPresenter PartCloseButton { get; private set; }

        public ContentPresenter PartCustomContent { get; private set; }

        internal object ControlBoxContent =>
            base.GetValue(ControlBoxContentProperty);

        internal DataTemplate ControlBoxContentTemplate =>
            (DataTemplate) base.GetValue(ControlBoxContentTemplateProperty);

        internal bool IsActuallyVisible =>
            (bool) base.GetValue(IsActuallyVisibleProperty);

        internal bool IsCloseButtonVisible =>
            (bool) base.GetValue(IsCloseButtonVisibleProperty);

        internal bool IsControlBoxVisible =>
            (bool) base.GetValue(IsControlBoxVisibleProperty);

        protected virtual bool AllowCustomContent =>
            true;

        protected Panel PartButtonsPanel { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseControlBoxControl.<>c <>9 = new BaseControlBoxControl.<>c();
            public static Action<BaseControlBoxControl> <>9__15_0;
            public static Func<bool> <>9__77_1;

            internal void <.cctor>b__11_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((BaseControlBoxControl) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.NewValue);
            }

            internal void <.cctor>b__11_1(BaseControlBoxControl d, bool oldValue, bool newValue)
            {
                d.OnIsCloseButtonVisibleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_2(BaseControlBoxControl d, object oldValue, object newValue)
            {
                d.OnControlBoxContentChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_3(BaseControlBoxControl d, DataTemplate oldValue, DataTemplate newValue)
            {
                d.OnControlBoxContentTemplateChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_4(BaseControlBoxControl d, bool oldValue, bool newValue)
            {
                d.OnIsControlBoxVisibleChanged(oldValue, newValue);
            }

            internal void <.cctor>b__11_5(BaseControlBoxControl d, bool oldValue, bool newValue)
            {
                d.OnIsActuallyVisibleChanged(oldValue, newValue);
            }

            internal bool <.cctor>b__11_6(BaseControlBoxControl d, bool value) => 
                d.OnCoerceIsActuallyVisible(value);

            internal bool <CanExecute>b__77_1() => 
                false;

            internal void <Invalidate>b__15_0(BaseControlBoxControl x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

