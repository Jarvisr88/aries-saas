namespace DevExpress.Utils.Design.DataAccess
{
    using System;

    public interface IBindingPropertiesProvider
    {
        string DataSourceProperty { get; }

        string DataMemberProperty { get; }
    }
}

