namespace DevExpress.Mvvm
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ViewInjectionServiceExtensions
    {
        public static object GetViewModel(this IViewInjectionService service, object key)
        {
            VerifyService(service);
            return service.ViewModels.FirstOrDefault<object>(x => Equals(service.GetKey(x), key));
        }

        public static void Inject(this IViewInjectionService service, object key, object viewModel)
        {
            VerifyService(service);
            service.Inject(key, viewModel, string.Empty, null);
        }

        public static void Inject(this IViewInjectionService service, object key, object viewModel, string viewName)
        {
            VerifyService(service);
            service.Inject(key, viewModel, viewName, null);
        }

        public static void Inject(this IViewInjectionService service, object key, object viewModel, Type viewType)
        {
            VerifyService(service);
            service.Inject(key, viewModel, null, viewType);
        }

        private static void VerifyService(IViewInjectionService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

