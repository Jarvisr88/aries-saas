namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class SpinButtonInfo : ButtonInfoBase
    {
        private const string spinUpButtonName = "PART_SpinUpButton";
        private const string spinDownButtonName = "PART_SpinDownButton";
        protected static readonly object spinUpClickLocker = new object();
        protected static readonly object spinDownClickLocker = new object();
        public static readonly DependencyProperty SpinStyleProperty;
        public static readonly DependencyProperty SpinUpCommandProperty;
        public static readonly DependencyProperty SpinUpCommandParameterProperty;
        public static readonly DependencyProperty SpinDownCommandProperty;
        public static readonly DependencyProperty SpinDownCommandParameterProperty;
        public static readonly DependencyProperty SpinUpCommandTargetProperty;
        public static readonly DependencyProperty SpinDownCommandTargetProperty;
        private static readonly DependencyPropertyKey ActualSpinUpCommandPropertyKey;
        public static readonly DependencyProperty ActualSpinUpCommandProperty;
        private static readonly DependencyPropertyKey ActualSpinDownCommandPropertyKey;
        public static readonly DependencyProperty ActualSpinDownCommandProperty;
        private RenderButtonContext spinUpButtonContext;
        private RenderButtonContext spinDownButtonContext;

        [Category("Behavior")]
        public event RoutedEventHandler SpinDownClick
        {
            add
            {
                base.events.AddHandler(spinDownClickLocker, value);
            }
            remove
            {
                base.events.RemoveHandler(spinDownClickLocker, value);
            }
        }

        [Category("Behavior")]
        public event RoutedEventHandler SpinUpClick
        {
            add
            {
                base.events.AddHandler(spinUpClickLocker, value);
            }
            remove
            {
                base.events.RemoveHandler(spinUpClickLocker, value);
            }
        }

        static SpinButtonInfo()
        {
            Type ownerType = typeof(SpinButtonInfo);
            SpinStyleProperty = DependencyPropertyManager.Register("SpinStyle", typeof(DevExpress.Xpf.Editors.SpinStyle), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.SpinStyle.Vertical));
            SpinUpCommandProperty = DependencyPropertyManager.Register("SpinUpCommand", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinUpCommandChanged((ICommand) args.NewValue)));
            SpinUpCommandParameterProperty = DependencyPropertyManager.Register("SpinUpCommandParameter", typeof(object), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinUpCommandParameterChanged(args.NewValue)));
            SpinDownCommandProperty = DependencyPropertyManager.Register("SpinDownCommand", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinDownCommandChanged((ICommand) args.NewValue)));
            SpinDownCommandParameterProperty = DependencyPropertyManager.Register("SpinDownCommandParameter", typeof(object), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinDownCommandParameterChanged(args.NewValue)));
            FrameworkContentElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            SpinUpCommandTargetProperty = DependencyPropertyManager.Register("SpinUpCommandTarget", typeof(IInputElement), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinUpCommandTargetChanged((IInputElement) args.NewValue)));
            SpinDownCommandTargetProperty = DependencyPropertyManager.Register("SpinDownCommandTarget", typeof(IInputElement), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).SpinDownCommandTargetChanged((IInputElement) args.NewValue)));
            ActualSpinUpCommandPropertyKey = DependencyProperty.RegisterReadOnly("ActualSpinUpCommand", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).ActualSpinUpCommandChanged((ICommand) args.NewValue)));
            ActualSpinUpCommandProperty = ActualSpinUpCommandPropertyKey.DependencyProperty;
            ActualSpinDownCommandPropertyKey = DependencyProperty.RegisterReadOnly("ActualSpinDownCommand", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((SpinButtonInfo) o).ActualSpinDownCommandChanged((ICommand) args.NewValue)));
            ActualSpinDownCommandProperty = ActualSpinDownCommandPropertyKey.DependencyProperty;
        }

        private void ActualSpinDownCommandChanged(ICommand value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.Command = value;
            }
        }

        private void ActualSpinUpCommandChanged(ICommand value)
        {
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.Command = value;
            }
        }

        private void AssignButtonProperties()
        {
            this.VisibilityChanged(base.Visibility);
            this.OnIsEnabledChanged(base.IsEnabled);
            this.ClickModeChanged(base.ClickMode);
            this.ActualSpinUpCommandChanged(this.ActualSpinUpCommand);
            this.SpinUpCommandParameterChanged(this.SpinUpCommandParameter);
            this.SpinUpCommandTargetChanged(this.SpinUpCommandTarget);
            this.ActualSpinDownCommandChanged(this.ActualSpinDownCommand);
            this.SpinDownCommandParameterChanged(this.SpinDownCommandParameter);
            this.SpinDownCommandTargetChanged(this.SpinDownCommandTarget);
        }

        protected override void ClickModeChanged(ClickMode value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.ClickMode = new ClickMode?(value);
            }
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.ClickMode = new ClickMode?(value);
            }
        }

        protected override ButtonInfoBase CreateClone() => 
            new SpinButtonInfo();

        protected override List<DependencyProperty> CreateCloneProperties()
        {
            List<DependencyProperty> list = base.CreateCloneProperties();
            list.Add(SpinUpCommandProperty);
            list.Add(SpinUpCommandParameterProperty);
            list.Add(SpinDownCommandProperty);
            list.Add(SpinDownCommandParameterProperty);
            list.Add(SpinUpCommandTargetProperty);
            list.Add(SpinDownCommandTargetProperty);
            return list;
        }

        protected internal override void FindContent(RenderContentControlContext templatedParent)
        {
            base.FindContent(templatedParent);
            if (templatedParent != null)
            {
                RenderButtonContext context = templatedParent as RenderButtonContext;
                this.spinUpButtonContext = null;
                this.spinDownButtonContext = null;
                if ((context != null) && (context.Name == "PART_SpinUpButton"))
                {
                    this.spinUpButtonContext = context;
                }
                if ((context != null) && (context.Name == "PART_SpinDownButton"))
                {
                    this.spinDownButtonContext = context;
                }
                if (base.IsDefaultButton)
                {
                    this.UpdateActualSpinCommands();
                }
                else
                {
                    if (this.spinUpButtonContext != null)
                    {
                        this.spinUpButtonContext.Click += new RenderEventHandler(this.OnRenderSpinUpButtonClick);
                    }
                    if (this.spinDownButtonContext != null)
                    {
                        this.spinDownButtonContext.Click += new RenderEventHandler(this.OnRenderSpinDownButtonClick);
                    }
                }
                this.AssignButtonProperties();
            }
        }

        protected internal override void FindContent(FrameworkElement templatedParent)
        {
            if (base.Template != null)
            {
                ButtonBase base2 = base.Template.FindName("PART_SpinUpButton", templatedParent) as ButtonBase;
                ButtonBase base3 = base.Template.FindName("PART_SpinDownButton", templatedParent) as ButtonBase;
                if (base.IsDefaultButton)
                {
                    this.UpdateActualSpinCommands();
                }
                else
                {
                    if (base2 != null)
                    {
                        base2.Click += new RoutedEventHandler(this.OnSpinUpButtonClick);
                    }
                    if (base3 != null)
                    {
                        base3.Click += new RoutedEventHandler(this.OnSpinDownButtonClick);
                    }
                }
            }
        }

        protected override void ForegroundChanged(Brush value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.Foreground = value;
            }
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.Foreground = value;
            }
        }

        protected internal override AutomationPeer GetRenderAutomationPeer() => 
            new SpinButtonInfoAutomationPeer(this);

        protected override void IsDefaultButtonChanged(bool value)
        {
            this.UpdateActualSpinCommands();
        }

        protected override void OnIsEnabledChanged(bool value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.IsEnabled = value;
            }
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.IsEnabled = value;
            }
        }

        protected internal virtual void OnRenderSpinDownButtonClick(IFrameworkRenderElementContext sender, RenderEventArgsBase args)
        {
            if (base.RaiseClickEventInInplaceInactiveMode)
            {
                this.RaiseSpinDownClickEvent(sender, args.OriginalEventArgs as RoutedEventArgs);
            }
        }

        protected internal virtual void OnRenderSpinUpButtonClick(IFrameworkRenderElementContext sender, RenderEventArgsBase args)
        {
            if (base.RaiseClickEventInInplaceInactiveMode)
            {
                this.RaiseSpinUpClickEvent(sender, args.OriginalEventArgs as RoutedEventArgs);
            }
        }

        protected internal virtual void OnSpinDownButtonClick(object sender, RoutedEventArgs e)
        {
            this.RaiseSpinDownClickEvent(sender, e);
        }

        protected internal virtual void OnSpinUpButtonClick(object sender, RoutedEventArgs e)
        {
            this.RaiseSpinUpClickEvent(sender, e);
        }

        protected virtual void RaiseSpinDownClickEvent(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler handler = base.events[spinDownClickLocker] as RoutedEventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void RaiseSpinUpClickEvent(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler handler = base.events[spinUpClickLocker] as RoutedEventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private void SpinDownCommandChanged(ICommand value)
        {
            this.UpdateActualSpinDownCommand(base.IsDefaultButton, value);
        }

        private void SpinDownCommandParameterChanged(object value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.CommandParameter = value;
            }
        }

        private void SpinDownCommandTargetChanged(IInputElement value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.CommandTarget = value;
            }
        }

        private void SpinUpCommandChanged(ICommand value)
        {
            this.UpdateActualSpinUpCommand(base.IsDefaultButton, value);
        }

        private void SpinUpCommandParameterChanged(object value)
        {
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.CommandParameter = value;
            }
        }

        private void SpinUpCommandTargetChanged(IInputElement value)
        {
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.CommandTarget = value;
            }
        }

        public void UpdateActualSpinCommands()
        {
            this.UpdateActualSpinUpCommand(base.IsDefaultButton, this.SpinUpCommand);
            this.UpdateActualSpinDownCommand(base.IsDefaultButton, this.SpinDownCommand);
        }

        private void UpdateActualSpinDownCommand(bool isDefault, ICommand command)
        {
            if (base.Owner != null)
            {
                this.ActualSpinDownCommand = isDefault ? base.Owner.SpinDownCommand : command;
            }
        }

        private void UpdateActualSpinUpCommand(bool isDefault, ICommand command)
        {
            if (base.Owner != null)
            {
                this.ActualSpinUpCommand = isDefault ? base.Owner.SpinUpCommand : command;
            }
        }

        protected override void VisibilityChanged(Visibility value)
        {
            if (this.spinDownButtonContext != null)
            {
                this.spinDownButtonContext.Visibility = new Visibility?(value);
            }
            if (this.spinUpButtonContext != null)
            {
                this.spinUpButtonContext.Visibility = new Visibility?(value);
            }
        }

        public ICommand ActualSpinUpCommand
        {
            get => 
                (ICommand) base.GetValue(ActualSpinUpCommandProperty);
            private set => 
                base.SetValue(ActualSpinUpCommandPropertyKey, value);
        }

        public ICommand ActualSpinDownCommand
        {
            get => 
                (ICommand) base.GetValue(ActualSpinDownCommandProperty);
            private set => 
                base.SetValue(ActualSpinDownCommandPropertyKey, value);
        }

        [Category("Appearance")]
        public DevExpress.Xpf.Editors.SpinStyle SpinStyle
        {
            get => 
                (DevExpress.Xpf.Editors.SpinStyle) base.GetValue(SpinStyleProperty);
            set => 
                base.SetValue(SpinStyleProperty, value);
        }

        [Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand SpinUpCommand
        {
            get => 
                (ICommand) base.GetValue(SpinUpCommandProperty);
            set => 
                base.SetValue(SpinUpCommandProperty, value);
        }

        [Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public object SpinUpCommandParameter
        {
            get => 
                base.GetValue(SpinUpCommandParameterProperty);
            set => 
                base.SetValue(SpinUpCommandParameterProperty, value);
        }

        [Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand SpinDownCommand
        {
            get => 
                (ICommand) base.GetValue(SpinDownCommandProperty);
            set => 
                base.SetValue(SpinDownCommandProperty, value);
        }

        [Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public object SpinDownCommandParameter
        {
            get => 
                base.GetValue(SpinDownCommandParameterProperty);
            set => 
                base.SetValue(SpinDownCommandParameterProperty, value);
        }

        [Category("Action")]
        public IInputElement SpinUpCommandTarget
        {
            get => 
                (IInputElement) base.GetValue(SpinUpCommandTargetProperty);
            set => 
                base.SetValue(SpinUpCommandTargetProperty, value);
        }

        [Category("Action")]
        public IInputElement SpinDownCommandTarget
        {
            get => 
                (IInputElement) base.GetValue(SpinDownCommandTargetProperty);
            set => 
                base.SetValue(SpinDownCommandTargetProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SpinButtonInfo.<>c <>9 = new SpinButtonInfo.<>c();

            internal void <.cctor>b__15_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinUpCommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__15_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinUpCommandParameterChanged(args.NewValue);
            }

            internal void <.cctor>b__15_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinDownCommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__15_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinDownCommandParameterChanged(args.NewValue);
            }

            internal void <.cctor>b__15_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinUpCommandTargetChanged((IInputElement) args.NewValue);
            }

            internal void <.cctor>b__15_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).SpinDownCommandTargetChanged((IInputElement) args.NewValue);
            }

            internal void <.cctor>b__15_6(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).ActualSpinUpCommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__15_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SpinButtonInfo) o).ActualSpinDownCommandChanged((ICommand) args.NewValue);
            }
        }
    }
}

