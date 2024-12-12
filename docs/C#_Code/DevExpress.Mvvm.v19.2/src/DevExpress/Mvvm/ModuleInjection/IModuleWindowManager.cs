namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public interface IModuleWindowManager : IModuleManagerBase
    {
        void Activate(string regionName, string key);
        void Clear(string regionName);
        void Close(string regionName, string key, MessageBoxResult? dialogResult = new MessageBoxResult?(), bool raiseViewModelRemovingEvent = true);
        bool IsShown(string regionName, string key);
        Task<WindowInjectionResult> Show(string regionName, string key, object parameter = null);
    }
}

