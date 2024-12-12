namespace DevExpress.Mvvm.ModuleInjection
{
    using DevExpress.Mvvm;
    using System;

    internal static class Verifier
    {
        public static void VerifyKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
        }

        public static void VerifyManager(IModuleManagerBase manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
        }

        public static void VerifyModule(IModule module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
        }

        public static void VerifyRegionName(string regionName)
        {
            if (string.IsNullOrEmpty(regionName))
            {
                throw new ArgumentNullException("regionName");
            }
        }

        public static void VerifyViewModel(object viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
        }

        public static void VerifyViewModelFactory(Func<object> viewModelFactory)
        {
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException("viewModelFactory");
            }
        }

        public static void VerifyViewModelISupportParameter(object viewModel)
        {
            if (!(viewModel is ISupportParameter))
            {
                ModuleInjectionException.VMNotSupportParameter();
            }
        }

        public static void VerifyViewModelISupportServices(object viewModel)
        {
            if (!(viewModel is ISupportServices))
            {
                ModuleInjectionException.VMNotSupportServices();
            }
        }

        public static void VerifyViewModelName(string viewModelName)
        {
            if (string.IsNullOrEmpty(viewModelName))
            {
                throw new ArgumentNullException("viewModelName");
            }
        }

        public static void VerifyVisualSerializationService(IVisualStateService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

