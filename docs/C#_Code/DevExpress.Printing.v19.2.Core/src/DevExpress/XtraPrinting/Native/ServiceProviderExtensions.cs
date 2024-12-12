namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) where T: class;
        public static TCast GetService<TGet, TCast>(this IServiceProvider serviceProvider) where TCast: class;
        public static void PerformIfNotNull<T>(this IServiceProvider serviceProvider, Action<T> callback) where T: class;
    }
}

