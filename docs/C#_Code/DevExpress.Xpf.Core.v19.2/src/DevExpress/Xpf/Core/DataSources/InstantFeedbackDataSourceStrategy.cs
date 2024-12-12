namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.ServerMode;
    using System;

    internal class InstantFeedbackDataSourceStrategy : GenericPropertyDataSourceStrategy
    {
        public InstantFeedbackDataSourceStrategy(InstantFeedbackDataSourceBase owner) : base(owner)
        {
        }

        public override object CreateData(object value)
        {
            ((InstantFeedbackDataSourceBase) base.owner).ResetInstantFeedbackSource();
            return base.owner.Data;
        }
    }
}

