namespace DevExpress.Mvvm.ModuleInjection
{
    using DevExpress.Mvvm.ModuleInjection.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public static class ModuleManagerExtensions
    {
        public static IRegion GetRegion(this IModuleManagerBase manager, object viewModel)
        {
            Verifier.VerifyManager(manager);
            IEnumerable<IRegion> regions = manager.GetRegions(viewModel);
            return (regions.Any<IRegion>() ? regions.Single<IRegion>() : null);
        }

        public static void InjectOrNavigate(this IModuleManager manager, string regionName, string key, object parameter = null)
        {
            Verifier.VerifyManager(manager);
            if (manager.GetModule(regionName, key) == null)
            {
                ModuleInjectionException.ModuleMissing(regionName, key);
            }
            if (!manager.IsInjected(regionName, key))
            {
                manager.Inject(regionName, key, parameter);
            }
            manager.Navigate(regionName, key);
        }

        public static void RegisterOrInjectOrNavigate(this IModuleManager manager, string regionName, IModule module, object parameter = null)
        {
            Verifier.VerifyManager(manager);
            Verifier.VerifyModule(module);
            if (manager.GetModule(regionName, module.Key) == null)
            {
                manager.Register(regionName, module);
            }
            manager.InjectOrNavigate(regionName, module.Key, parameter);
        }

        public static Task<WindowInjectionResult> RegisterOrShowOrActivate(this IModuleWindowManager manager, string regionName, IModule module, object parameter = null)
        {
            Verifier.VerifyManager(manager);
            Verifier.VerifyModule(module);
            if (manager.GetModule(regionName, module.Key) == null)
            {
                manager.Register(regionName, module);
            }
            return manager.ShowOrActivate(regionName, module.Key, parameter);
        }

        public static void Save(this IModuleManagerBase manager, out string logicalState, out string visualState)
        {
            Verifier.VerifyManager(manager);
            manager.Save(null, out logicalState, out visualState);
        }

        public static Task<WindowInjectionResult> ShowOrActivate(this IModuleWindowManager manager, string regionName, string key, object parameter = null)
        {
            Verifier.VerifyManager(manager);
            if (manager.GetModule(regionName, key) == null)
            {
                ModuleInjectionException.ModuleMissing(regionName, key);
            }
            if (!manager.IsShown(regionName, key))
            {
                return manager.Show(regionName, key, parameter);
            }
            Task<WindowInjectionResult> windowInjectionResult = ((IModuleManagerImplementation) manager).GetWindowInjectionResult(regionName, key);
            manager.Activate(regionName, key);
            return windowInjectionResult;
        }
    }
}

