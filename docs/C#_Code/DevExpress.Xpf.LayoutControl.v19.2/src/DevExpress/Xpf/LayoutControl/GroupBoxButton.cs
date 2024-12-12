namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;

    [TemplatePart(Name="MinimizeElement", Type=typeof(FrameworkElement)), TemplatePart(Name="UnminimizeElement", Type=typeof(FrameworkElement)), TemplatePart(Name="MaximizeElement", Type=typeof(FrameworkElement)), TemplatePart(Name="UnmaximizeElement", Type=typeof(FrameworkElement))]
    public class GroupBoxButton : DXButton
    {
        private GroupBoxButtonKind _Kind;
        private const string MinimizeElementName = "MinimizeElement";
        private const string UnminimizeElementName = "UnminimizeElement";
        private const string MaximizeElementName = "MaximizeElement";
        private const string UnmaximizeElementName = "UnmaximizeElement";

        public GroupBoxButton()
        {
            base.DefaultStyleKey = typeof(GroupBoxButton);
        }

        protected override ControlControllerBase CreateController() => 
            new GroupBoxButtonController(this);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.MinimizeElement = (FrameworkElement) base.GetTemplateChild("MinimizeElement");
            this.UnminimizeElement = (FrameworkElement) base.GetTemplateChild("UnminimizeElement");
            this.MaximizeElement = (FrameworkElement) base.GetTemplateChild("MaximizeElement");
            this.UnmaximizeElement = (FrameworkElement) base.GetTemplateChild("UnmaximizeElement");
            this.UpdateTemplate();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new GroupBoxButtonAutomationPeer(this);

        protected virtual void UpdateTemplate()
        {
            if (this.MinimizeElement != null)
            {
                this.MinimizeElement.SetVisible(this.Kind == GroupBoxButtonKind.Minimize);
            }
            if (this.UnminimizeElement != null)
            {
                this.UnminimizeElement.SetVisible(this.Kind == GroupBoxButtonKind.Unminimize);
            }
            if (this.MaximizeElement != null)
            {
                this.MaximizeElement.SetVisible(this.Kind == GroupBoxButtonKind.Maximize);
            }
            if (this.UnmaximizeElement != null)
            {
                this.UnmaximizeElement.SetVisible(this.Kind == GroupBoxButtonKind.Unmaximize);
            }
        }

        public GroupBoxButtonKind Kind
        {
            get => 
                this._Kind;
            set
            {
                if (this._Kind != value)
                {
                    this._Kind = value;
                    this.UpdateTemplate();
                }
            }
        }

        protected FrameworkElement MinimizeElement { get; set; }

        protected FrameworkElement UnminimizeElement { get; set; }

        protected FrameworkElement MaximizeElement { get; set; }

        protected FrameworkElement UnmaximizeElement { get; set; }

        public GroupBoxButtonController Controller =>
            (GroupBoxButtonController) base.Controller;
    }
}

