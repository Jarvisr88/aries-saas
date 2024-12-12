namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.InteropServices;

    public interface IModuleManager : IModuleManagerBase
    {
        void Clear(string regionName);
        void Inject(string regionName, string key, object parameter = null);
        bool IsInjected(string regionName, string key);
        void Navigate(string regionName, string key);
        void Remove(string regionName, string key, bool raiseViewModelRemovingEvent = true);
    }
}

