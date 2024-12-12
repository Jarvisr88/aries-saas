namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFiltersPropertiesEx : IServiceProvider
    {
        void Register<TService>(TService service);
    }
}

