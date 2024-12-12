namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm.ModuleInjection;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IRegionImplementation : IRegion
    {
        void ApplyInfo(bool inject, bool navigate);
        void Clear();
        bool Contains(string key);
        void GetInfo(out RegionInfo logicalInfo, out RegionVisualInfo visualInfo);
        void GetSavedVisualState(object viewModel, string viewPart, out string state);
        void Inject(IModule module, object parameter);
        void Navigate(string key);
        void OnNavigation(string key, object vm);
        void RegisterUIRegion(IUIRegion region);
        void Remove(string key);
        void SaveVisualState(object viewModel, string viewPart, string state);
        void SetInfo(RegionInfo logicalInfo, RegionVisualInfo visualInfo);
        void UnregisterUIRegion(IUIRegion region);

        IEnumerable<IUIRegion> UIRegions { get; }
    }
}

