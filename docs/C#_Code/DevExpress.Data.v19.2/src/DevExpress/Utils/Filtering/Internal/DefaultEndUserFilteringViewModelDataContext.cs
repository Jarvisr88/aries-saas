namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultEndUserFilteringViewModelDataContext : IEndUserFilteringViewModelDataContext
    {
        internal static readonly IEndUserFilteringViewModelDataContext Instance = new DefaultEndUserFilteringViewModelDataContext();

        private DefaultEndUserFilteringViewModelDataContext()
        {
        }

        void IEndUserFilteringViewModelDataContext.Complete(string path)
        {
        }

        void IEndUserFilteringViewModelDataContext.DataBind(string path)
        {
        }

        void IEndUserFilteringViewModelDataContext.Initialize(string path)
        {
        }
    }
}

