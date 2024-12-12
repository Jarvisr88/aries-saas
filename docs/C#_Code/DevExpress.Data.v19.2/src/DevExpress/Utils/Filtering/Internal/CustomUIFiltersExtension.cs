namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CustomUIFiltersExtension
    {
        internal static void InitializeUIProperties(this ICustomUIFilters filters, ICustomUIFiltersProperties properties, IServiceProvider serviceProvider)
        {
            if ((filters != null) && ((properties != null) && (serviceProvider != null)))
            {
                serviceProvider.GetService<ICustomUIFiltersPropertiesFactory>().Do<ICustomUIFiltersPropertiesFactory>(factory => properties.Assign(factory.Create(filters)));
                (properties as ICustomUIFiltersPropertiesEx).Do<ICustomUIFiltersPropertiesEx>(delegate (ICustomUIFiltersPropertiesEx propertiesEx) {
                    string path = filters.Metric.Path;
                    serviceProvider.GetService<IDisplayTemplatesServiceFactory>().Do<IDisplayTemplatesServiceFactory>(factory => propertiesEx.Register<IDisplayTemplatesService>(factory.Create(path)));
                });
            }
        }
    }
}

