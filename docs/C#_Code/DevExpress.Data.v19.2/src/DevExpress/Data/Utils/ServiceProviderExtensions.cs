namespace DevExpress.Data.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider);
    }
}

