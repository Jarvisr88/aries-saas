namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;

    public interface IFloatingPane
    {
        DockLayoutManager Manager { get; }

        DevExpress.Xpf.Docking.FloatGroup FloatGroup { get; }
    }
}

