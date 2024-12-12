namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class ModuleInjectionException : Exception
    {
        private const string moduleMissing = "Cannot find a module with the passed key. Register module before working with it.";
        private const string moduleExists = "A module with the same key already exists.";
        private const string cannotAttach = "This service can be only attached to a FrameworkElement or FrameworkContentElement";
        private const string noStrategy = "Cannot find an appropriate strategy for the {0} container type.";
        private const string nullVM = "A view model to inject cannot be null.";
        private const string vmNotSupportServices = "This ViewModel does not implement the ISupportServices interface.";
        private const string vmNotSupportParameter = "This ViewModel does not implement the ISupportParameter interface.";
        private const string visualStateServiceName = "VisualStateService with the same Name already exists. If you are using several VisualStateServices in one View, be sure that they have different names.";
        private const string cannotResolveVM = "Cannot create a view model instance by the {0} type name. Setup ViewModelLocator.";

        private ModuleInjectionException(string regionName, string key, string message) : base(message)
        {
            this.RegionName = regionName;
        }

        public static void CannotAttach()
        {
            throw new ModuleInjectionException(null, null, "This service can be only attached to a FrameworkElement or FrameworkContentElement");
        }

        public static void CannotResolveVM(string vmName)
        {
            throw new ModuleInjectionException(null, null, $"Cannot create a view model instance by the {vmName} type name. Setup ViewModelLocator.");
        }

        public static void ModuleAlreadyExists(string regionName, string key)
        {
            throw new ModuleInjectionException(regionName, key, "A module with the same key already exists.");
        }

        public static void ModuleMissing(string regionName, string key)
        {
            throw new ModuleInjectionException(regionName, key, "Cannot find a module with the passed key. Register module before working with it.");
        }

        public static void NoStrategy(Type containerType)
        {
            throw new ModuleInjectionException(null, null, $"Cannot find an appropriate strategy for the {containerType.Name} container type.");
        }

        public static void NullVM()
        {
            throw new ModuleInjectionException(null, null, "A view model to inject cannot be null.");
        }

        public static void VisualStateServiceName()
        {
            throw new ModuleInjectionException(null, null, "VisualStateService with the same Name already exists. If you are using several VisualStateServices in one View, be sure that they have different names.");
        }

        public static void VMNotSupportParameter()
        {
            throw new ModuleInjectionException(null, null, "This ViewModel does not implement the ISupportParameter interface.");
        }

        public static void VMNotSupportServices()
        {
            throw new ModuleInjectionException(null, null, "This ViewModel does not implement the ISupportServices interface.");
        }

        public string Key { get; private set; }

        public string RegionName { get; private set; }
    }
}

