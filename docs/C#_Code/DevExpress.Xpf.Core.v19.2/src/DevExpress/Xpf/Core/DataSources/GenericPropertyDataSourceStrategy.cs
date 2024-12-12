namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public class GenericPropertyDataSourceStrategy : DataSourceStrategyBase
    {
        public GenericPropertyDataSourceStrategy(IDataSource owner) : base(owner)
        {
        }

        public override Type GetDataObjectType() => 
            this.OwnerDataMemberType.IsGenericType ? this.OwnerDataMemberType.GetGenericArguments()[0] : null;
    }
}

