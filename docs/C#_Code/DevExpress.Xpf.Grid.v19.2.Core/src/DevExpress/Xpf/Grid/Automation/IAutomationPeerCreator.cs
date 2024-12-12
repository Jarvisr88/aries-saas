namespace DevExpress.Xpf.Grid.Automation
{
    using System.Windows;
    using System.Windows.Automation.Peers;

    public interface IAutomationPeerCreator
    {
        AutomationPeer CreatePeer(DependencyObject obj);
    }
}

