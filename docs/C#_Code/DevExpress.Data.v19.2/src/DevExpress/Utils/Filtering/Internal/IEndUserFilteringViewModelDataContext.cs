namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEndUserFilteringViewModelDataContext
    {
        void Complete(string path);
        void DataBind(string path);
        void Initialize(string path);
    }
}

