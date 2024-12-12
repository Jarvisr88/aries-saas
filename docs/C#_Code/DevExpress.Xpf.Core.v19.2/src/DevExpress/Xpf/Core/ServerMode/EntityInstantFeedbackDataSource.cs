namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data.Linq;
    using DevExpress.Xpf.Core.ServerMode.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class EntityInstantFeedbackDataSource : InstantFeedbackDataSourceBase
    {
        protected override InstantFeedbackDataSourceWrapper CreateDataSourceWrapper() => 
            new InstantFeedbackDataSourceWrapper(<>c.<>9__1_0 ??= () => new EntityInstantFeedbackSource(), delegate (IListSource x) {
                this.ToSource(x).Refresh();
            }, delegate (IListSource x, bool areSourceRowsThreadSafe) {
                this.ToSource(x).AreSourceRowsThreadSafe = areSourceRowsThreadSafe;
            }, delegate (IListSource x, string defaultSorting) {
                this.ToSource(x).DefaultSorting = defaultSorting;
            }, delegate (IListSource x, string keyExpression) {
                this.ToSource(x).KeyExpression = base.KeyExpression;
            }, delegate (IListSource x) {
                this.ToSource(x).GetQueryable += new EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.Data_GetQueryable);
            }, delegate (IListSource x) {
                this.ToSource(x).DismissQueryable += new EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.Data_DismissQueryable);
            }, delegate (IListSource x) {
                this.ToSource(x).GetQueryable -= new EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.Data_GetQueryable);
            }, delegate (IListSource x) {
                this.ToSource(x).DismissQueryable -= new EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.Data_DismissQueryable);
            });

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.EntityInstantFeedbackDataSource.png";

        private EntityInstantFeedbackSource ToSource(IListSource source) => 
            (EntityInstantFeedbackSource) source;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EntityInstantFeedbackDataSource.<>c <>9 = new EntityInstantFeedbackDataSource.<>c();
            public static Func<IListSource> <>9__1_0;

            internal IListSource <CreateDataSourceWrapper>b__1_0() => 
                new EntityInstantFeedbackSource();
        }
    }
}

