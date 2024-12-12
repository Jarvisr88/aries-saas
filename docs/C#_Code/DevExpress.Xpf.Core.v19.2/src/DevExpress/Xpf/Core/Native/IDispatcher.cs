namespace DevExpress.Xpf.Core.Native
{
    using System.Windows.Threading;

    public interface IDispatcher
    {
        IMediaContext From(Dispatcher dispatcher);
    }
}

