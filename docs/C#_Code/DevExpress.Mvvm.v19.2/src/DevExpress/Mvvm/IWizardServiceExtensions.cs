namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class IWizardServiceExtensions
    {
        public static void GoBack(this IWizardService service)
        {
            VerifyService(service);
            service.GoBack(null);
        }

        public static void GoForward(this IWizardService service)
        {
            VerifyService(service);
            service.GoForward(null);
        }

        public static void Navigate(this IWizardService service, object viewModel, object param = null, object parentViewModel = null)
        {
            NavigateCore(service, null, viewModel, param, parentViewModel);
        }

        public static void Navigate(this IWizardService service, string viewType, object param = null, object parentViewModel = null)
        {
            NavigateCore(service, viewType, null, param, parentViewModel);
        }

        private static void NavigateCore(IWizardService service, string viewType, object viewModel, object param, object parentViewModel)
        {
            VerifyService(service);
            service.Navigate(viewType, viewModel, param, parentViewModel);
        }

        private static void VerifyService(IWizardService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

