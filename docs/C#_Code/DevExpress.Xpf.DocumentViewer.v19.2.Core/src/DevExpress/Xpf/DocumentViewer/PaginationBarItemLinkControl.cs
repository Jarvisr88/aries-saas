namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation.Peers;

    public class PaginationBarItemLinkControl : BarStaticItemLinkControl
    {
        static PaginationBarItemLinkControl()
        {
            NavigationAutomationPeersCreator.Default.RegisterObject(typeof(PaginationBarItemLinkControl), typeof(PaginationItemLinkControlBarAutomationPeer), (CreateObjectMethod<AutomationPeer>) (owner => new PaginationItemLinkControlBarAutomationPeer((PaginationBarItemLinkControl) owner)));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaginationBarItemLinkControl.<>c <>9 = new PaginationBarItemLinkControl.<>c();

            internal AutomationPeer <.cctor>b__0_0(object owner) => 
                new PaginationItemLinkControlBarAutomationPeer((PaginationBarItemLinkControl) owner);
        }
    }
}

