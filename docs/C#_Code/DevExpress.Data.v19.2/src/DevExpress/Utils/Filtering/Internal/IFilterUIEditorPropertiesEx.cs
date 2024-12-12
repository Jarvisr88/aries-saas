namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IFilterUIEditorPropertiesEx : IServiceProvider
    {
        void Register<TService>(TService service);
    }
}

