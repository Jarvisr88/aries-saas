namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Automation.Peers;

    public class ThemedWindowAutomationPeer : WindowAutomationPeer
    {
        public ThemedWindowAutomationPeer(ThemedWindow owner) : base(owner)
        {
        }
    }
}

