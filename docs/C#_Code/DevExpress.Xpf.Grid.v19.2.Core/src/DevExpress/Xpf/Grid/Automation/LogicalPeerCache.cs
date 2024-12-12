namespace DevExpress.Xpf.Grid.Automation
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class LogicalPeerCache
    {
        public LogicalPeerCache()
        {
            this.DataRows = new Dictionary<int, AutomationPeer>();
            this.GroupFooterRows = new Dictionary<int, AutomationPeer>();
        }

        public void Clear()
        {
            this.DataRows.Clear();
            this.GroupFooterRows.Clear();
        }

        public Dictionary<int, AutomationPeer> DataRows { get; private set; }

        public Dictionary<int, AutomationPeer> GroupFooterRows { get; private set; }
    }
}

