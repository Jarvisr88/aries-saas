namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public static class WindowServiceExtensions
    {
        public static void Show(this IWindowService service, object viewModel)
        {
            VerifyService(service);
            service.Show(null, viewModel, null, null);
        }

        public static void Show(this IWindowService service, string documentType, object viewModel)
        {
            VerifyService(service);
            service.Show(documentType, viewModel, null, null);
        }

        public static void Show(this IWindowService service, string documentType, object parameter, object parentViewModel)
        {
            VerifyService(service);
            service.Show(documentType, null, parameter, parentViewModel);
        }

        private static void VerifyService(IWindowService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

