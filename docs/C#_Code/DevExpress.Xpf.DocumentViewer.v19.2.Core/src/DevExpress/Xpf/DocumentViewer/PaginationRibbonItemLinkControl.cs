namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation.Peers;

    public class PaginationRibbonItemLinkControl : BarButtonItemLinkControl
    {
        static PaginationRibbonItemLinkControl()
        {
            NavigationAutomationPeersCreator.Default.RegisterObject(typeof(PaginationRibbonItemLinkControl), typeof(PaginationItemLinkControlRibbonAutomationPeer), (CreateObjectMethod<AutomationPeer>) (owner => new PaginationItemLinkControlRibbonAutomationPeer((PaginationRibbonItemLinkControl) owner)));
        }

        protected override void UpdateLayoutPanel()
        {
            base.UpdateLayoutPanel();
            if (base.LayoutPanel != null)
            {
                base.LayoutPanel.ShowFirstBorder = false;
                base.LayoutPanel.ShowSecondBorder = false;
            }
        }

        protected override void UpdateLayoutPanelSplitContent()
        {
            base.UpdateLayoutPanelSplitContent();
            if (base.LayoutPanel != null)
            {
                base.LayoutPanel.SplitTextMode = 0;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaginationRibbonItemLinkControl.<>c <>9 = new PaginationRibbonItemLinkControl.<>c();

            internal AutomationPeer <.cctor>b__0_0(object owner) => 
                new PaginationItemLinkControlRibbonAutomationPeer((PaginationRibbonItemLinkControl) owner);
        }
    }
}

