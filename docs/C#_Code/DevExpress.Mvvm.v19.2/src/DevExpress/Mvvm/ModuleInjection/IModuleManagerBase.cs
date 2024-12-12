namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IModuleManagerBase
    {
        IViewModelEventManager GetEvents(object viewModel);
        IRegionEventManager GetEvents(string regionName);
        IModule GetModule(string regionName, string key);
        IRegion GetRegion(string regionName);
        IEnumerable<IRegion> GetRegions(object viewModel);
        void Register(string regionName, IModule module);
        bool Restore(string logicalState, string visualState);
        void Save(string regionName, out string logicalState, out string visualState);
        void Unregister(string regionName, string key);
    }
}

