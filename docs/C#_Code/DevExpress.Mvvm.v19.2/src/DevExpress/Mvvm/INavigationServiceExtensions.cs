namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class INavigationServiceExtensions
    {
        public static void Navigate(this INavigationService service, object viewModel, object param = null, object parentViewModel = null)
        {
            NavigateCore(service, null, viewModel, param, parentViewModel, true);
        }

        public static void Navigate(this INavigationService service, string viewType, object param = null, object parentViewModel = null)
        {
            NavigateCore(service, viewType, null, param, parentViewModel, true);
        }

        public static void Navigate(this INavigationService service, string viewType, object param, object parentViewModel, bool saveToJournal)
        {
            NavigateCore(service, viewType, null, param, parentViewModel, saveToJournal);
        }

        private static void NavigateCore(INavigationService service, string viewType, object viewModel, object param, object parentViewModel, bool saveToJournal)
        {
            VerifyService(service);
            service.Navigate(viewType, viewModel, param, parentViewModel, saveToJournal);
        }

        private static void VerifyService(INavigationService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

