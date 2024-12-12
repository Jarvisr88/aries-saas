namespace DevExpress.Office.Utils
{
    using System;
    using System.ComponentModel.Design;

    public static class ServiceUtils
    {
        public static T GetService<T>(IServiceProvider provider) where T: class
        {
            if (provider != null)
            {
                return (T) provider.GetService(typeof(T));
            }
            return default(T);
        }

        public static T ReplaceService<T>(IServiceContainer container, T newService) where T: class
        {
            T service = GetService<T>(container);
            if (service != null)
            {
                container.RemoveService(typeof(T));
            }
            if (newService != null)
            {
                container.AddService(typeof(T), newService);
            }
            return service;
        }
    }
}

