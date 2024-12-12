namespace DevExpress.Xpf.Core.Commands
{
    using System.Windows.Threading;

    internal interface IDispatcherInfo
    {
        System.Windows.Threading.Dispatcher Dispatcher { get; }
    }
}

