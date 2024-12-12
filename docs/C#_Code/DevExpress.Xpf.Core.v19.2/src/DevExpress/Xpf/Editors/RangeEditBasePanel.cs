namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Automation;
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class RangeEditBasePanel : Control
    {
        public RangeEditBasePanel()
        {
            base.Loaded += new RoutedEventHandler(this.RangeEditBasePanel_Loaded);
        }

        internal void ForceLoaded()
        {
            if (this.Owner != null)
            {
                this.Owner.UpdateDataContext(this.Owner);
                this.Owner.EditStrategy.OnLoaded();
                base.ApplyTemplate();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Owner.EditStrategy.OnApplyTemplate();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new RangeEditBasePanelAutomationPeer(this);

        private void RangeEditBasePanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.ForceLoaded();
        }

        private RangeBaseEdit Owner =>
            base.GetValue(BaseEdit.OwnerEditProperty) as RangeBaseEdit;
    }
}

