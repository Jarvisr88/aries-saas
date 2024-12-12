namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ViewInjectionManagerExtensions
    {
        public static void Inject(this IViewInjectionManager service, string regionName, object key, Func<object> viewModelFactory)
        {
            VerifyService(service);
            service.Inject(regionName, key, viewModelFactory, null, null);
        }

        public static void Inject(this IViewInjectionManager service, string regionName, object key, Func<object> viewModelFactory, string viewName)
        {
            VerifyService(service);
            service.Inject(regionName, key, viewModelFactory, viewName, null);
        }

        public static void Inject(this IViewInjectionManager service, string regionName, object key, Func<object> viewModelFactory, Type viewType)
        {
            VerifyService(service);
            service.Inject(regionName, key, viewModelFactory, null, viewType);
        }

        private static void VerifyService(IViewInjectionManager service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

