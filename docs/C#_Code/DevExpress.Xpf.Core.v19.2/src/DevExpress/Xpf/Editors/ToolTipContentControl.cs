namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class ToolTipContentControl : ContentControl
    {
        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ToolTipContentControlAutomationPeer(this);
    }
}

