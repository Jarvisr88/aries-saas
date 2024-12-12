namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Threading;

    [TemplateVisualState(Name="Normal", GroupName="CommonStates"), TemplateVisualState(Name="MouseOver", GroupName="CommonStates"), TemplateVisualState(Name="Pressed", GroupName="CommonStates"), TemplateVisualState(Name="Disabled", GroupName="CommonStates"), TemplatePart(Name="PART_GlyphPresenter", Type=typeof(ControlBoxGlyphPresenter)), ContentProperty("Content")]
    public class ControlBoxButton : psvControl, IWeakEventListener, ISupportVisualStates, ICommandSource
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty HasCommandProperty;
        private static readonly DependencyPropertyKey HasCommandPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsPressedProperty;
        private static readonly DependencyPropertyKey IsPressedPropertyKey;
        public static readonly DependencyProperty IsActiveProperty;
        private static readonly DependencyPropertyKey IsActivePropertyKey;
        public static readonly DependencyProperty BorderStyleProperty;
        public static readonly RoutedEvent ClickEvent;
        private readonly DevExpress.Xpf.Docking.VisualElements.VisualStateController VisualStateController;
        private bool _CanExecute = true;
        private BaseLayoutItem _item;
        private BaseControlBoxControl controlBox;
        private FrameworkElement PartButtonBorder;

        [Category("Behavior")]
        public event RoutedEventHandler Click
        {
            add
            {
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ClickEvent, value);
            }
        }

        static ControlBoxButton()
        {
            DependencyPropertyRegistrator<DevExpress.Xpf.Docking.VisualElements.ControlBoxButton> registrator = new DependencyPropertyRegistrator<DevExpress.Xpf.Docking.VisualElements.ControlBoxButton>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<object>("Content", ref ContentProperty, null, (dObj, e) => ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) dObj).OnContentChanged(e.NewValue, e.OldValue), null);
            registrator.Register<DataTemplate>("ContentTemplate", ref ContentTemplateProperty, null, null, null);
            registrator.Register<DataTemplateSelector>("ContentTemplateSelector", ref ContentTemplateSelectorProperty, null, null, null);
            BaseControlBoxControl.ControlBoxProperty.AddPropertyChangedCallback(typeof(DevExpress.Xpf.Docking.VisualElements.ControlBoxButton), (dObj, e) => ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) dObj).ControlBox = (BaseControlBoxControl) e.NewValue);
            registrator.Register<ICommand>("Command", ref CommandProperty, null, (d, e) => ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue), null);
            registrator.Register<object>("CommandParameter", ref CommandParameterProperty, null, null, null);
            registrator.Register<IInputElement>("CommandTarget", ref CommandTargetProperty, null, null, null);
            registrator.RegisterReadonly<bool>("HasCommand", ref HasCommandPropertyKey, ref HasCommandProperty, false, null, null);
            registrator.RegisterReadonly<bool>("IsPressed", ref IsPressedPropertyKey, ref IsPressedProperty, false, null, null);
            registrator.RegisterReadonly<bool>("IsActive", ref IsActivePropertyKey, ref IsActiveProperty, false, null, null);
            registrator.Register<Style>("BorderStyle", ref BorderStyleProperty, null, null, null);
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DevExpress.Xpf.Docking.VisualElements.ControlBoxButton));
        }

        public ControlBoxButton()
        {
            this.VisualStateController = new DevExpress.Xpf.Docking.VisualElements.VisualStateController(this);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnMouseLeftButtonDown);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnMouseLeftButtonUp);
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.ControlBoxButton_IsVisibleChanged);
        }

        private void ControlBoxButton_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Action<BaseLayoutItem> action = <>c.<>9__79_0;
            if (<>c.<>9__79_0 == null)
            {
                Action<BaseLayoutItem> local1 = <>c.<>9__79_0;
                action = <>c.<>9__79_0 = x => x.InvalidateTabHeader();
            }
            this.Item.Do<BaseLayoutItem>(action);
        }

        void ISupportVisualStates.UpdateVisualState()
        {
            this.UpdateVisualState();
        }

        private void EnsurePartGlyphControl(object sender, SizeChangedEventArgs e)
        {
            this.PartGlyphControl = LayoutItemsHelper.GetTemplateChild<ControlBoxGlyphPresenter>(this);
            if (this.PartGlyphControl != null)
            {
                this.VisualStateController.Add(this.PartGlyphControl);
                this.UpdateVisualState();
            }
        }

        private void HookCommand(ICommand command)
        {
            command.CanExecuteChanged += new EventHandler(this.OnCanExecuteChanged);
            this.UpdateCanExecute();
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            Action<BaseLayoutItem> action = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                Action<BaseLayoutItem> local1 = <>c.<>9__70_0;
                action = <>c.<>9__70_0 = x => x.InvalidateTabHeader();
            }
            this.Item.Do<BaseLayoutItem>(action);
        }

        public override void OnApplyTemplate()
        {
            if (this.PartButtonBorder != null)
            {
                this.VisualStateController.Remove(this.PartButtonBorder);
            }
            if (this.PartGlyphControl != null)
            {
                this.VisualStateController.Remove(this.PartGlyphControl);
            }
            base.OnApplyTemplate();
            this.PartButtonBorder = LayoutItemsHelper.GetTemplateChild<ControlBoxButtonBorder>(this);
            if (this.PartButtonBorder != null)
            {
                this.VisualStateController.Add(this.PartButtonBorder);
                this.PartButtonBorder.SizeChanged += new SizeChangedEventHandler(this.EnsurePartGlyphControl);
            }
            this.UpdateStyle();
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateCanExecute();
        }

        protected virtual void OnClick()
        {
            base.RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            CommandHelper.ExecuteCommand(this);
        }

        private void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            base.SetValue(HasCommandPropertyKey, newValue != null);
            if (oldValue != null)
            {
                this.UnhookCommand(oldValue);
            }
            if (newValue != null)
            {
                this.HookCommand(newValue);
            }
        }

        protected virtual void OnContentChanged(object content, object oldContent)
        {
            base.RemoveLogicalChild(oldContent);
            if (content != null)
            {
                base.AddLogicalChild(content);
            }
        }

        private void OnControlBoxChanged(BaseControlBoxControl oldValue, BaseControlBoxControl newValue)
        {
            this.Item = this.ControlBox?.LayoutItem;
            this.UpdateStyle();
        }

        protected override void OnDispose()
        {
            this.Item = null;
            base.OnDispose();
        }

        protected virtual void OnItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            if (oldValue != null)
            {
                BaseLayoutItemWeakEventManager.RemoveListener(oldValue, this);
            }
            if (newValue != null)
            {
                BaseLayoutItemWeakEventManager.AddListener(newValue, this);
                base.SetValue(IsActivePropertyKey, newValue.IsActive);
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            InvokeHelper.BeginInvoke(this, () => ReferenceEquals(base.Container.GetView(this.Item.GetRoot()), null), new Action(this.UpdateCanExecute), DispatcherPriority.Normal);
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.SetValue(IsPressedPropertyKey, false);
            base.OnLostMouseCapture(e);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.SetValue(IsPressedPropertyKey, true);
            base.CaptureMouse();
            e.Handled = true;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            base.SetValue(IsPressedPropertyKey, false);
            base.ReleaseMouseCapture();
            if (((e == null) || !e.Handled) && base.IsMouseOver)
            {
                this.OnClick();
                e.Handled = true;
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            this.UpdateStyle();
        }

        internal void PerformClick()
        {
            this.OnClick();
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(BaseLayoutItemWeakEventManager)))
            {
                return false;
            }
            this.VisualStateController.UpdateState();
            base.SetValue(IsActivePropertyKey, this.Item.IsActive);
            return true;
        }

        private void UnhookCommand(ICommand command)
        {
            command.CanExecuteChanged -= new EventHandler(this.OnCanExecuteChanged);
            this.UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            this.CanExecute = (this.Command == null) || CommandHelper.CanExecuteCommand(this);
        }

        private void UpdateStyle()
        {
            if ((this.PartButtonBorder != null) && (this.ControlBox != null))
            {
                if (this.ControlBox.ButtonStyle != null)
                {
                    base.Style = this.ControlBox.ButtonStyle;
                }
                else
                {
                    base.ClearValue(FrameworkElement.StyleProperty);
                }
            }
        }

        protected virtual void UpdateVisualState()
        {
            if (this.Item != null)
            {
                bool flag = this.ControlBox is TabHeaderControlBoxControl;
                string stateName = this.Item.IsActive ? "Active" : ((this.Item.IsSelectedItem & flag) ? "Selected" : "Inactive");
                VisualStateManager.GoToState(this, "EmptyActiveState", false);
                VisualStateManager.GoToState(this, stateName, false);
                if (this.PartGlyphControl != null)
                {
                    VisualStateManager.GoToState(this.PartGlyphControl, "EmptyActiveState", false);
                    VisualStateManager.GoToState(this.PartGlyphControl, stateName, false);
                }
                if (this.PartButtonBorder != null)
                {
                    VisualStateManager.GoToState(this.PartButtonBorder, "EmptyActiveState", false);
                    VisualStateManager.GoToState(this.PartButtonBorder, stateName, false);
                }
            }
        }

        public Style BorderStyle
        {
            get => 
                (Style) base.GetValue(BorderStyleProperty);
            set => 
                base.SetValue(BorderStyleProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }

        public bool HasCommand =>
            (bool) base.GetValue(HasCommandProperty);

        public bool IsActive =>
            (bool) base.GetValue(IsActiveProperty);

        public bool IsPressed =>
            (bool) base.GetValue(IsPressedProperty);

        protected override bool IsEnabledCore =>
            base.IsEnabledCore && this.CanExecute;

        protected BaseLayoutItem Item
        {
            get => 
                this._item;
            set
            {
                if (!ReferenceEquals(this._item, value))
                {
                    BaseLayoutItem oldValue = this._item;
                    this._item = value;
                    this.OnItemChanged(oldValue, value);
                }
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                if (this.Content != null)
                {
                    list.Add(this.Content);
                }
                return list.GetEnumerator();
            }
        }

        protected ControlBoxGlyphPresenter PartGlyphControl { get; private set; }

        private bool CanExecute
        {
            get => 
                this._CanExecute;
            set
            {
                if (value != this._CanExecute)
                {
                    this._CanExecute = value;
                    base.CoerceValue(UIElement.IsEnabledProperty);
                }
            }
        }

        private BaseControlBoxControl ControlBox
        {
            get => 
                this.controlBox;
            set
            {
                if (!ReferenceEquals(this.controlBox, value))
                {
                    BaseControlBoxControl controlBox = this.controlBox;
                    this.controlBox = value;
                    this.OnControlBoxChanged(controlBox, value);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Docking.VisualElements.ControlBoxButton.<>c <>9 = new DevExpress.Xpf.Docking.VisualElements.ControlBoxButton.<>c();
            public static Action<BaseLayoutItem> <>9__70_0;
            public static Action<BaseLayoutItem> <>9__79_0;

            internal void <.cctor>b__14_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) dObj).OnContentChanged(e.NewValue, e.OldValue);
            }

            internal void <.cctor>b__14_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) dObj).ControlBox = (BaseControlBoxControl) e.NewValue;
            }

            internal void <.cctor>b__14_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Docking.VisualElements.ControlBoxButton) d).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <ControlBoxButton_IsVisibleChanged>b__79_0(BaseLayoutItem x)
            {
                x.InvalidateTabHeader();
            }

            internal void <OnActualSizeChanged>b__70_0(BaseLayoutItem x)
            {
                x.InvalidateTabHeader();
            }
        }
    }
}

