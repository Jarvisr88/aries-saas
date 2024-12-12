namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.UIAutomation;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Data;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Button", Type=typeof(DevExpress.Xpf.Docking.VisualElements.ControlBoxButton))]
    public class ControlBoxButtonPresenter : psvContentPresenter, ICommandSource
    {
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandTargetProperty;

        static ControlBoxButtonPresenter()
        {
            DependencyPropertyRegistrator<ControlBoxButtonPresenter> registrator = new DependencyPropertyRegistrator<ControlBoxButtonPresenter>();
            registrator.Register<ICommand>("Command", ref CommandProperty, null, null, null);
            registrator.Register<object>("CommandParameter", ref CommandParameterProperty, null, null, null);
            registrator.Register<IInputElement>("CommandTarget", ref CommandTargetProperty, null, null, null);
        }

        public ControlBoxButtonPresenter()
        {
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
        }

        protected internal bool AutomationClick()
        {
            if (this.PartButton == null)
            {
                return false;
            }
            this.PartButton.PerformClick();
            return true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartButton != null) && !LayoutItemsHelper.IsTemplateChild<ControlBoxButtonPresenter>(this.PartButton, this))
            {
                this.PartButton.Dispose();
            }
            this.PartButton = base.GetTemplateChild("PART_Button") as DevExpress.Xpf.Docking.VisualElements.ControlBoxButton;
            if (this.PartButton != null)
            {
                this.Forward(this.PartButton, UIElement.IsEnabledProperty, "IsEnabled", BindingMode.OneWay);
                this.Forward(this.PartButton, DevExpress.Xpf.Docking.VisualElements.ControlBoxButton.CommandTargetProperty, "CommandTarget", BindingMode.OneWay);
                this.Forward(this.PartButton, DevExpress.Xpf.Docking.VisualElements.ControlBoxButton.CommandParameterProperty, "CommandParameter", BindingMode.OneWay);
                this.Forward(this.PartButton, DevExpress.Xpf.Docking.VisualElements.ControlBoxButton.CommandProperty, "Command", BindingMode.OneWay);
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ControlBoxButtonPresenterAutomationPeer(this);

        protected override void OnDispose()
        {
            if (this.PartButton != null)
            {
                this.PartButton.ClearValue(UIElement.IsEnabledProperty);
                this.PartButton.Dispose();
                this.PartButton = null;
            }
            base.OnDispose();
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BaseControlBoxControl.Invalidate(this);
        }

        [Obsolete, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public void SetIsHot(bool isHot)
        {
        }

        [Obsolete, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public void SetIsPresesd(bool isPressed)
        {
        }

        internal void SetToolTip(object tooltip)
        {
            this.AttachToolTip(tooltip);
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

        public DevExpress.Xpf.Docking.VisualElements.ControlBoxButton PartButton { get; private set; }
    }
}

