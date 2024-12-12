namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ServiceContainerExtensions
    {
        private static T CheckService<T>(T service) where T: class
        {
            if (service == null)
            {
                throw new ServiceNotFoundException();
            }
            return service;
        }

        public static T GetRequiredService<T>(this IServiceContainer serviceContainer, ServiceSearchMode searchMode = 0) where T: class
        {
            bool flag;
            VerifyServiceContainer(serviceContainer);
            return CheckService<T>((T) serviceContainer.GetService(typeof(T), null, searchMode, out flag));
        }

        public static T GetRequiredService<T>(this IServiceContainer serviceContainer, string key, ServiceSearchMode searchMode = 0) where T: class
        {
            bool flag;
            VerifyServiceContainer(serviceContainer);
            return CheckService<T>((T) serviceContainer.GetService(typeof(T), key, searchMode, out flag));
        }

        public static object GetService(this IServiceContainer serviceContainer, Type type, string key, ServiceSearchMode searchMode = 0)
        {
            bool flag;
            VerifyServiceContainer(serviceContainer);
            return serviceContainer.GetService(type, key, searchMode, out flag);
        }

        public static IEnumerable<T> GetServices<T>(this IServiceContainer serviceContainer, bool localOnly = true) where T: class
        {
            VerifyServiceContainer(serviceContainer);
            return serviceContainer.GetServices(typeof(T), localOnly).OfType<T>();
        }

        private static void VerifyServiceContainer(IServiceContainer serviceContainer)
        {
            if (serviceContainer == null)
            {
                throw new ArgumentNullException("serviceContainer");
            }
        }
    }
}

