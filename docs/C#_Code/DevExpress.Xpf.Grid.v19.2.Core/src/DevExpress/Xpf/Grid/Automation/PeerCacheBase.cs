namespace DevExpress.Xpf.Grid.Automation
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;

    public abstract class PeerCacheBase
    {
        private Dictionary<object, AutomationPeer> peers;

        public void AddPeer(DependencyObject obj, AutomationPeer peer, bool checkShouldAdd)
        {
            if (!checkShouldAdd || this.ShouldAddPeerToCache(peer))
            {
                this.Peers.Add(obj, peer);
            }
        }

        public AutomationPeer GetPeer(DependencyObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            AutomationPeer peer = null;
            this.Peers.TryGetValue(obj, out peer);
            return peer;
        }

        protected abstract bool ShouldAddPeerToCache(AutomationPeer peer);

        protected Dictionary<object, AutomationPeer> Peers
        {
            get
            {
                this.peers ??= new Dictionary<object, AutomationPeer>();
                return this.peers;
            }
        }
    }
}

