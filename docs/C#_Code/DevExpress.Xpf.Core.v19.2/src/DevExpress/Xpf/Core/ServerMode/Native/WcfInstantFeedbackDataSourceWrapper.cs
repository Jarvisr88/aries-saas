namespace DevExpress.Xpf.Core.ServerMode.Native
{
    using System;
    using System.ComponentModel;

    public class WcfInstantFeedbackDataSourceWrapper : InstantFeedbackDataSourceWrapper
    {
        private readonly Action<IListSource, string> setFixedFilter;

        public WcfInstantFeedbackDataSourceWrapper(Func<IListSource> createSource, Action<IListSource> refreshSource, Action<IListSource, bool> setAreSourceRowsThreadSafe, Action<IListSource, string> setDefaultSorting, Action<IListSource, string> setKeyExpression, Action<IListSource> subscribeGetQueryable, Action<IListSource> subscribeDismissQueryable, Action<IListSource> unsubscribeGetQueryable, Action<IListSource> unsubscribeDismissQueryable, Action<IListSource, string> setFixedFilter) : base(createSource, refreshSource, setAreSourceRowsThreadSafe, setDefaultSorting, setKeyExpression, subscribeGetQueryable, subscribeDismissQueryable, unsubscribeGetQueryable, unsubscribeDismissQueryable)
        {
            this.setFixedFilter = setFixedFilter;
        }

        public void SetFixedFilter(IListSource source, string value)
        {
            this.setFixedFilter(source, value);
        }
    }
}

