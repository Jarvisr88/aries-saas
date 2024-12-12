namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;

    public static class ServiceContainerExtensions
    {
        public static void AddService<T>(this IServiceContainer container, T service);
        public static void AddService<T>(this IServiceContainer container, ServiceCreatorCallback<T> callback);
        public static void CopyServiceFrom<T>(this IServiceContainer container, IServiceProvider provider) where T: class;
        public static void RemoveService<T>(this IServiceContainer container);
        public static void ReplaceService<T>(this IServiceContainer container, T service);
        public static void ReplaceService<T>(this IServiceContainer container, ServiceCreatorCallback<T> callback);
        public static void ReplaceService(this IServiceContainer container, Type type, ServiceCreatorCallback callback);
        public static void ReplaceService(this IServiceContainer container, Type serviceType, object service);
    }
}

