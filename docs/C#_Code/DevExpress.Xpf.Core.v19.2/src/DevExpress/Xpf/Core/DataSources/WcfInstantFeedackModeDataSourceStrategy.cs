namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.ServerMode;
    using System;

    public class WcfInstantFeedackModeDataSourceStrategy : WcfDataSourceStrategyBase
    {
        public WcfInstantFeedackModeDataSourceStrategy(WcfInstantFeedbackDataSourceBase owner) : base(owner)
        {
        }

        public override object CreateData(object value)
        {
            this.Owner.ResetInstantFeedbackSource();
            return this.Owner.Data;
        }

        private WcfInstantFeedbackDataSourceBase Owner =>
            (WcfInstantFeedbackDataSourceBase) base.owner;
    }
}

