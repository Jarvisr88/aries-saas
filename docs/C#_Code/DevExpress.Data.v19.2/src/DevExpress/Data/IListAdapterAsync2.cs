namespace DevExpress.Data
{
    using System;
    using System.Threading;

    public interface IListAdapterAsync2 : IListAdapterAsync, IListAdapter, IListAdapter2
    {
        IAsyncResult BeginFillList(IServiceProvider servProvider, CancellationToken token, string[] queriesToFill);
    }
}

