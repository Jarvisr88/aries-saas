namespace DevExpress.Xpf.Grid.Automation
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation.Peers;

    internal static class AutomationPeerExtensions
    {
        public static void ResetChildrenCachePlatformIndependent(this AutomationPeer peer)
        {
            peer.ResetChildrenCache();
        }
    }
}

