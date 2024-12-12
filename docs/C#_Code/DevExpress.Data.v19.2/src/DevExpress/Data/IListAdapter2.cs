namespace DevExpress.Data
{
    using System;

    public interface IListAdapter2 : IListAdapter
    {
        void FillList(IServiceProvider servProvider, string[] queriesToFill);
        bool ShouldRefillQuery(string query);
    }
}

