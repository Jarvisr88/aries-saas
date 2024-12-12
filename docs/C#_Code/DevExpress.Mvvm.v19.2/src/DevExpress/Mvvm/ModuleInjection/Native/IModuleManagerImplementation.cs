namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Threading.Tasks;

    public interface IModuleManagerImplementation : IModuleManagerBase, IModuleManager, IModuleWindowManager
    {
        IRegionImplementation GetRegionImplementation(string regionName);
        Task<WindowInjectionResult> GetWindowInjectionResult(string regionName, string key);
        void OnNavigation(string regionName, NavigationEventArgs e);
        void OnViewModelRemoved(string regionName, ViewModelRemovedEventArgs e);
        void OnViewModelRemoving(string regionName, ViewModelRemovingEventArgs e);
        void RaiseViewModelCreated(ViewModelCreatedEventArgs e);

        bool KeepViewModelsAlive { get; }

        IViewModelLocator ViewModelLocator { get; }

        IViewLocator ViewLocator { get; }

        IStateSerializer ViewModelStateSerializer { get; }
    }
}

