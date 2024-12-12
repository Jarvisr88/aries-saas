namespace DevExpress.Data
{
    using System;
    using System.Threading;

    public interface IListAdapterAsync : IListAdapter
    {
        IAsyncResult BeginFillList(IServiceProvider servProvider, CancellationToken token);
        void EndFillList(IAsyncResult result, CancellationToken token);
    }
}

