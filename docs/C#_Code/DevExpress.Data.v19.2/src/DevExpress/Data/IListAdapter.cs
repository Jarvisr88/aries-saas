namespace DevExpress.Data
{
    using System;

    public interface IListAdapter
    {
        void FillList(IServiceProvider servProvider);

        bool IsFilled { get; }

        bool ShouldRefill { get; }
    }
}

