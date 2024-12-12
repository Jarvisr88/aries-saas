namespace DevExpress.Xpf.Core
{
    using System.Windows;

    public interface IManipulationClientEx : IManipulationClient
    {
        Vector GetMinScrollDelta();
    }
}

