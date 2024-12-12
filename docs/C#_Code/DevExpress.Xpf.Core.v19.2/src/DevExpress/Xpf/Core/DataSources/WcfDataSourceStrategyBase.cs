namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class WcfDataSourceStrategyBase : GenericPropertyDataSourceStrategy
    {
        public WcfDataSourceStrategyBase(IWcfDataSource owner) : base(owner)
        {
        }

        public override bool CanUpdateData() => 
            base.CanUpdateData() && (this.ServiceRoot != null);

        public override object CreateContextIstance()
        {
            object[] args = new object[] { this.ServiceRoot };
            return Activator.CreateInstance(base.ContextType, args);
        }

        public void Update(Uri serviceRoot)
        {
            this.ServiceRoot = serviceRoot;
        }

        private IWcfDataSource Owner =>
            (IWcfDataSource) base.owner;

        protected Uri ServiceRoot { get; private set; }
    }
}

