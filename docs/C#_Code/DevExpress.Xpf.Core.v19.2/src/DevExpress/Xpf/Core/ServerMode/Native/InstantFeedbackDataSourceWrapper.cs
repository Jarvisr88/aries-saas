namespace DevExpress.Xpf.Core.ServerMode.Native
{
    using System;
    using System.ComponentModel;

    public class InstantFeedbackDataSourceWrapper
    {
        private readonly Func<IListSource> createSource;
        private readonly Action<IListSource> refreshSource;
        private readonly Action<IListSource, bool> setAreSourceRowsThreadSafe;
        private readonly Action<IListSource, string> setDefaultSorting;
        private readonly Action<IListSource, string> setKeyExpression;
        private readonly Action<IListSource> subscribeGetQueryable;
        private readonly Action<IListSource> subscribeDismissQueryable;
        private readonly Action<IListSource> unsubscribeGetQueryable;
        private readonly Action<IListSource> unsubscribeDismissQueryable;

        public InstantFeedbackDataSourceWrapper(Func<IListSource> createSource, Action<IListSource> refreshSource, Action<IListSource, bool> setAreSourceRowsThreadSafe, Action<IListSource, string> setDefaultSorting, Action<IListSource, string> setKeyExpression, Action<IListSource> subscribeGetQueryable, Action<IListSource> subscribeDismissQueryable, Action<IListSource> unsubscribeGetQueryable, Action<IListSource> unsubscribeDismissQueryable)
        {
            this.createSource = createSource;
            this.refreshSource = refreshSource;
            this.setAreSourceRowsThreadSafe = setAreSourceRowsThreadSafe;
            this.setDefaultSorting = setDefaultSorting;
            this.setKeyExpression = setKeyExpression;
            this.subscribeGetQueryable = subscribeGetQueryable;
            this.subscribeDismissQueryable = subscribeDismissQueryable;
            this.unsubscribeGetQueryable = unsubscribeGetQueryable;
            this.unsubscribeDismissQueryable = unsubscribeDismissQueryable;
        }

        public IListSource Create() => 
            this.createSource();

        public void Refresh(IListSource source)
        {
            this.refreshSource(source);
        }

        public void SetAreSourceRowsThreadSafe(IListSource source, bool value)
        {
            this.setAreSourceRowsThreadSafe(source, value);
        }

        public void SetDefaultSorting(IListSource source, string value)
        {
            this.setDefaultSorting(source, value);
        }

        public void SetKeyExpression(IListSource source, string value)
        {
            this.setKeyExpression(source, value);
        }

        public void SubscribeDismissQueryable(IListSource source)
        {
            this.subscribeDismissQueryable(source);
        }

        public void SubscribeGetQueryable(IListSource source)
        {
            this.subscribeGetQueryable(source);
        }

        public void UnsubscribeDismissQueryable(IListSource source)
        {
            this.unsubscribeDismissQueryable(source);
        }

        public void UnsubscribeGetQueryable(IListSource source)
        {
            this.unsubscribeGetQueryable(source);
        }
    }
}

