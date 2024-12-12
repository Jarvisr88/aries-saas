namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class PropertiesProviderExtensions
    {
        public static Task<string> GetDataSourceDisplayNameAsync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
        public static string GetDataSourceDisplayNameSync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
        public static Task<IPropertyDescriptor[]> GetItemPropertiesAsync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
        public static IPropertyDescriptor[] GetItemPropertiesSync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
        public static Task<IPropertyDescriptor[]> GetListItemPropertiesAsync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
        public static IPropertyDescriptor[] GetListItemPropertiesSync(this IPropertiesProvider propertiesProvider, object dataSource, string dataMember);
    }
}

