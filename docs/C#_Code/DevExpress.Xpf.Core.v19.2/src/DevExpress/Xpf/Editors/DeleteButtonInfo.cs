namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class DeleteButtonInfo : ButtonInfoBase
    {
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        private static readonly object Click = new object();

        static DeleteButtonInfo()
        {
            Type forType = typeof(DeleteButtonInfo);
            FrameworkContentElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), forType, new PropertyMetadata(null, (o, args) => ((DeleteButtonInfo) o).CommandChanged((ICommand) args.NewValue)));
            CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), forType, new PropertyMetadata(null, (o, args) => ((DeleteButtonInfo) o).CommandParameterChanged(args.NewValue)));
            CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), forType, new PropertyMetadata(null, (o, args) => ((DeleteButtonInfo) o).CommandTargetChanged((IInputElement) args.NewValue)));
        }

        private void CommandChanged(ICommand value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Command = value;
            }
        }

        private void CommandParameterChanged(object value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.CommandParameter = value;
            }
        }

        private void CommandTargetChanged(IInputElement value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.CommandTarget = value;
            }
        }

        protected override ButtonInfoBase CreateClone() => 
            new DeleteButtonInfo();

        protected override List<DependencyProperty> CreateCloneProperties()
        {
            List<DependencyProperty> list = base.CreateCloneProperties();
            list.Add(CommandProperty);
            list.Add(CommandParameterProperty);
            list.Add(ToolTipService.ShowDurationProperty);
            list.Add(ToolTipService.InitialShowDelayProperty);
            return list;
        }

        protected internal override void FindContent(RenderContentControlContext templatedParent)
        {
            base.FindContent(templatedParent);
        }

        protected internal override void FindContent(FrameworkElement templatedParent)
        {
            if (base.Template != null)
            {
                ButtonClose close = base.Template.FindName("PART_Item", templatedParent) as ButtonClose;
                if ((close != null) && (base.Owner != null))
                {
                    close.Click += new RoutedEventHandler(this.OnButtonClick);
                    if (base.IsDefaultButton)
                    {
                        this.Command = base.Owner.SetNullValueCommand;
                        Binding binding = new Binding();
                        binding.Source = base.Owner.PropertyProvider;
                        binding.Path = new PropertyPath(ButtonEditPropertyProvider.IsNullValueButtonVisibleProperty);
                        binding.Converter = new BooleanToVisibilityConverter();
                        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        BindingOperations.SetBinding(this, ButtonInfoBase.VisibilityProperty, binding);
                    }
                }
            }
        }

        protected internal override AutomationPeer GetRenderAutomationPeer() => 
            null;

        protected virtual void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ShouldRaiseClickEvent())
            {
                this.RaiseClickEvent(sender, e);
            }
        }

        protected virtual void RaiseClickEvent(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler handler = base.events[Click] as RoutedEventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private bool ShouldRaiseClickEvent() => 
            (base.Owner == null) || !base.Owner.If<ButtonEdit>(x => ((x.EditMode == EditMode.InplaceInactive) && !base.RaiseClickEventInInplaceInactiveMode)).ReturnSuccess<ButtonEdit>();

        private DevExpress.Xpf.Core.Native.RenderButtonContext RenderButtonContext { get; set; }

        [Description("Gets or sets a command associated with the button.This is a dependency property."), Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        [Description("Gets or sets a parameter to pass to the ButtonInfo.Command property.This is a dependency property."), Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        [Category("Action"), Description("Gets or sets the element on which to execute the associated command.This is a dependency property.")]
        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DeleteButtonInfo.<>c <>9 = new DeleteButtonInfo.<>c();

            internal void <.cctor>b__3_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((DeleteButtonInfo) o).CommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__3_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((DeleteButtonInfo) o).CommandParameterChanged(args.NewValue);
            }

            internal void <.cctor>b__3_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((DeleteButtonInfo) o).CommandTargetChanged((IInputElement) args.NewValue);
            }
        }
    }
}

