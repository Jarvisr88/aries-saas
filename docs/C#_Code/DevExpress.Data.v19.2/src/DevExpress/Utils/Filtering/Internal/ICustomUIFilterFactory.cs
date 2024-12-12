namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFilterFactory
    {
        ICustomUIFilter Create(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider);
    }
}

