namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class ThemedWindowBackButton : Button
    {
        private ThemedWindow themedWindow;

        static ThemedWindowBackButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowBackButton), new FrameworkPropertyMetadata(typeof(ThemedWindowBackButton)));
        }

        private void ExecuteNavigateCommand()
        {
            if ((this.themedWindow.NavigateBackCommand != null) && this.themedWindow.NavigateBackCommand.CanExecute(null))
            {
                this.themedWindow.NavigateBackCommand.Execute(null);
            }
        }

        public override void OnApplyTemplate()
        {
            this.themedWindow = TreeHelper.GetParent<ThemedWindow>(this, null, true, true);
            base.OnApplyTemplate();
        }

        protected override void OnClick()
        {
            ThemedWindowBackButtonEventArgs e = new ThemedWindowBackButtonEventArgs(ThemedWindow.BackRequestedEvent, this);
            this.themedWindow.RaiseEvent(e);
            this.ExecuteNavigateCommand();
            base.OnClick();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            base.IsVisible ? base.OnCreateAutomationPeer() : null;
    }
}

