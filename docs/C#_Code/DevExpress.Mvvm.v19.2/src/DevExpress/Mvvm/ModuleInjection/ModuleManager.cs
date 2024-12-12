namespace DevExpress.Mvvm.ModuleInjection
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection.Native;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class ModuleManager : IModuleManagerBase, IModuleManager, IModuleWindowManager, IModuleManagerImplementation
    {
        private static IModuleManager _defaultInstance = new ModuleManager(false);
        private static IModuleManager _default;
        private readonly bool allowSaveRestoreLayout;
        private readonly bool isTestingMode;
        private readonly List<ModuleWrapper> modules;
        private readonly List<IRegionImplementation> regions;
        private readonly Dictionary<string, RegionEventManager> regionEvents;
        private readonly Dictionary<WeakReference, ViewModelEventManager> viewModelEvents;
        private readonly List<WindowClosedHandler> windowClosedHandlers;
        private readonly IViewModelLocator viewModelLocator;
        private readonly IViewLocator viewLocator;
        private readonly IStateSerializer viewModelStateSerializer;
        private readonly bool keepViewModelsAlive;

        public ModuleManager(bool keepViewModelsAlive = false) : this(null, null, null, true, false, keepViewModelsAlive)
        {
        }

        public ModuleManager(bool allowSaveRestoreLayout, bool isTestingMode, bool keepViewModelsAlive = false) : this(null, null, null, allowSaveRestoreLayout, isTestingMode, keepViewModelsAlive)
        {
        }

        public ModuleManager(IViewModelLocator viewModelLocator, IViewLocator viewLocator, IStateSerializer viewModelStateSerializer, bool allowSaveRestoreLayout, bool isTestingMode, bool keepViewModelsAlive = false)
        {
            this.modules = new List<ModuleWrapper>();
            this.regions = new List<IRegionImplementation>();
            this.regionEvents = new Dictionary<string, RegionEventManager>();
            this.viewModelEvents = new Dictionary<WeakReference, ViewModelEventManager>();
            this.windowClosedHandlers = new List<WindowClosedHandler>();
            this.keepViewModelsAlive = keepViewModelsAlive;
            this.allowSaveRestoreLayout = allowSaveRestoreLayout;
            this.isTestingMode = isTestingMode;
            this.viewModelLocator = viewModelLocator;
            this.viewLocator = viewLocator;
            this.viewModelStateSerializer = viewModelStateSerializer;
        }

        protected virtual IRegionImplementation CreateRegion(string regionName)
        {
            Verifier.VerifyRegionName(regionName);
            return new Region(regionName, this, this.isTestingMode);
        }

        void IModuleManager.Clear(string regionName)
        {
            IRegionImplementation region = this.GetRegion(regionName);
            Func<object, object> elementSelector = <>c.<>9__41_1;
            if (<>c.<>9__41_1 == null)
            {
                Func<object, object> local1 = <>c.<>9__41_1;
                elementSelector = <>c.<>9__41_1 = x => x;
            }
            Dictionary<string, object> dictionary = region.ViewModels.ToDictionary<object, string, object>(x => region.GetKey(x), elementSelector);
            this.GetRegion(regionName).Clear();
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                ViewModelRemovedEventArgs e = new ViewModelRemovedEventArgs(regionName, pair.Value, pair.Key);
                this.RaiseViewModelRemovedEvent(regionName, e);
            }
        }

        void IModuleManager.Inject(string regionName, string key, object parameter)
        {
            IModule module = this.GetModule(regionName, key, true);
            this.GetRegion(regionName).Inject(module, parameter);
        }

        bool IModuleManager.IsInjected(string regionName, string key) => 
            (this.GetModule(regionName, key, false) != null) ? this.GetRegion(regionName).Contains(key) : false;

        void IModuleManager.Navigate(string regionName, string key)
        {
            this.GetRegion(regionName).Navigate(key);
        }

        void IModuleManager.Remove(string regionName, string key, bool raiseViewModelRemovingEvent)
        {
            if (((IModuleManager) this).IsInjected(regionName, key))
            {
                IRegionImplementation region = this.GetRegion(regionName);
                object viewModel = region.GetViewModel(key);
                if ((viewModel != null) & raiseViewModelRemovingEvent)
                {
                    ViewModelRemovingEventArgs e = new ViewModelRemovingEventArgs(regionName, viewModel, key);
                    this.RaiseViewModelRemovingEvent(regionName, e);
                    if (e.Cancel)
                    {
                        return;
                    }
                }
                region.Remove(key);
                if (viewModel != null)
                {
                    ViewModelRemovedEventArgs e = new ViewModelRemovedEventArgs(regionName, viewModel, key);
                    this.RaiseViewModelRemovedEvent(regionName, e);
                }
            }
        }

        IViewModelEventManager IModuleManagerBase.GetEvents(object viewModel) => 
            this.GetEvents(viewModel, true);

        IRegionEventManager IModuleManagerBase.GetEvents(string regionName) => 
            this.GetEvents(regionName, true);

        IModule IModuleManagerBase.GetModule(string regionName, string key) => 
            this.GetModule(regionName, key, false);

        IRegion IModuleManagerBase.GetRegion(string regionName) => 
            this.GetRegion(regionName);

        IEnumerable<IRegion> IModuleManagerBase.GetRegions(object viewModel) => 
            (IEnumerable<IRegion>) (from x in this.regions
                where x.ViewModels.Contains<object>(viewModel)
                select x).ToList<IRegionImplementation>();

        void IModuleManagerBase.Register(string regionName, IModule module)
        {
            Verifier.VerifyRegionName(regionName);
            Verifier.VerifyModule(module);
            if (this.GetModule(regionName, module.Key, false) != null)
            {
                ModuleInjectionException.ModuleAlreadyExists(regionName, module.Key);
            }
            this.modules.Add(new ModuleWrapper(regionName, module));
        }

        bool IModuleManagerBase.Restore(string logicalState, string visualState)
        {
            IEnumerable<string> enumerable1;
            IEnumerable<string> enumerable5;
            <>c__DisplayClass36_2 class_1;
            if (!this.allowSaveRestoreLayout)
            {
                return false;
            }
            VisualInfo visualInfo = VisualInfo.Deserialize(visualState);
            LogicalInfo logicalInfo = LogicalInfo.Deserialize(logicalState);
            if (logicalInfo == null)
            {
                enumerable1 = new string[0];
            }
            else
            {
                Func<RegionInfo, string> selector = <>c.<>9__36_0;
                if (<>c.<>9__36_0 == null)
                {
                    Func<RegionInfo, string> local1 = <>c.<>9__36_0;
                    selector = <>c.<>9__36_0 = x => x.RegionName;
                }
                enumerable1 = logicalInfo.Regions.Select<RegionInfo, string>(selector);
            }
            IEnumerable<string> first = enumerable1;
            if (visualInfo == null)
            {
                enumerable5 = new string[0];
            }
            else
            {
                Func<RegionVisualInfo, string> selector = <>c.<>9__36_1;
                if (<>c.<>9__36_1 == null)
                {
                    Func<RegionVisualInfo, string> local2 = <>c.<>9__36_1;
                    selector = <>c.<>9__36_1 = x => x.RegionName;
                }
                enumerable5 = visualInfo.Regions.Select<RegionVisualInfo, string>(selector);
            }
            List<IRegionImplementation> list = (from x in first.Union<string>(enumerable5) select this.GetRegion(x)).ToList<IRegionImplementation>();
            Func<LogicalInfo, string, RegionInfo> getRegionInfo = <>c.<>9__36_3 ??= (info, regionName) => ((info != null) ? info.Regions.FirstOrDefault<RegionInfo>(x => (x.RegionName == regionName)) : null);
            if (<>c.<>9__36_5 == null)
            {
                class_1 = (<>c__DisplayClass36_2) (<>c.<>9__36_5 = (info, regionName) => (info != null) ? info.Regions.FirstOrDefault<RegionVisualInfo>(x => (x.RegionName == regionName)) : null);
            }
            <>c.<>9__36_5.getRegionVisualInfo = (Func<VisualInfo, string, RegionVisualInfo>) class_1;
            list.ForEach(delegate (IRegionImplementation region) {
                if ((region.LogicalSerializationMode != LogicalSerializationMode.Disabled) && (getRegionInfo(logicalInfo, region.RegionName) != null))
                {
                    ((IModuleManager) this).Clear(region.RegionName);
                }
            });
            list.ForEach(delegate (IRegionImplementation region) {
                Func<VisualInfo, string, RegionVisualInfo> getRegionVisualInfo;
                region.SetInfo(getRegionInfo(logicalInfo, region.RegionName), getRegionVisualInfo(visualInfo, region.RegionName));
            });
            if (logicalInfo != null)
            {
                Action<IRegionImplementation> action = <>c.<>9__36_9;
                if (<>c.<>9__36_9 == null)
                {
                    Action<IRegionImplementation> local5 = <>c.<>9__36_9;
                    action = <>c.<>9__36_9 = delegate (IRegionImplementation x) {
                        x.ApplyInfo(true, false);
                    };
                }
                list.ForEach(action);
                Action<IRegionImplementation> action2 = <>c.<>9__36_10;
                if (<>c.<>9__36_10 == null)
                {
                    Action<IRegionImplementation> local6 = <>c.<>9__36_10;
                    action2 = <>c.<>9__36_10 = delegate (IRegionImplementation x) {
                        x.ApplyInfo(false, true);
                    };
                }
                list.ForEach(action2);
            }
            return (logicalInfo != null);
        }

        void IModuleManagerBase.Save(string regionName, out string logicalState, out string visualState)
        {
            if (!this.allowSaveRestoreLayout)
            {
                logicalState = null;
                visualState = null;
            }
            else
            {
                List<IRegionImplementation> list1;
                if (string.IsNullOrEmpty(regionName))
                {
                    list1 = new List<IRegionImplementation>(this.regions);
                }
                else
                {
                    IRegionImplementation[] collection = new IRegionImplementation[] { this.GetRegion(regionName) };
                    list1 = new List<IRegionImplementation>(collection);
                }
                LogicalInfo info = new LogicalInfo();
                VisualInfo info2 = new VisualInfo();
                foreach (IRegionImplementation implementation in list1)
                {
                    RegionInfo info3;
                    RegionVisualInfo info4;
                    implementation.GetInfo(out info3, out info4);
                    if (info3 != null)
                    {
                        info.Regions.Add(info3);
                    }
                    if (info4 != null)
                    {
                        info2.Regions.Add(info4);
                    }
                }
                logicalState = LogicalInfo.Serialize(info);
                visualState = VisualInfo.Serialize(info2);
            }
        }

        void IModuleManagerBase.Unregister(string regionName, string key)
        {
            IModule module = this.GetModule(regionName, key, false);
            if (module != null)
            {
                ((IModuleManager) this).Remove(regionName, module.Key, false);
                this.modules.Remove(this.modules.First<ModuleWrapper>(x => ReferenceEquals(x.Module, module)));
            }
        }

        void IModuleWindowManager.Activate(string regionName, string key)
        {
            ((IModuleManager) this).Navigate(regionName, key);
        }

        void IModuleWindowManager.Clear(string regionName)
        {
            ((IModuleManager) this).Clear(regionName);
        }

        void IModuleWindowManager.Close(string regionName, string key, MessageBoxResult? dialogResult, bool raiseViewModelRemovingEvent)
        {
            IUIWindowRegion uIWindowRegion = this.GetUIWindowRegion(regionName);
            if ((dialogResult != null) && (uIWindowRegion != null))
            {
                uIWindowRegion.SetResult(dialogResult.Value);
            }
            ((IModuleManager) this).Remove(regionName, key, raiseViewModelRemovingEvent);
        }

        bool IModuleWindowManager.IsShown(string regionName, string key) => 
            ((IModuleManager) this).IsInjected(regionName, key);

        Task<WindowInjectionResult> IModuleWindowManager.Show(string regionName, string key, object parameter)
        {
            Task<WindowInjectionResult> task = this.GetWindowInjectionResult(regionName, key, false);
            ((IModuleManager) this).Inject(regionName, key, parameter);
            return task;
        }

        IRegionImplementation IModuleManagerImplementation.GetRegionImplementation(string regionName) => 
            this.GetRegion(regionName);

        Task<WindowInjectionResult> IModuleManagerImplementation.GetWindowInjectionResult(string regionName, string key) => 
            this.GetWindowInjectionResult(regionName, key, true);

        void IModuleManagerImplementation.OnNavigation(string regionName, NavigationEventArgs e)
        {
            IRegionImplementation region = this.GetRegion(regionName);
            if (!Equals(region.SelectedKey, e.NewViewModelKey))
            {
                region.OnNavigation(e.NewViewModelKey, e.NewViewModel);
                this.RaiseNavigationEvent(regionName, e);
            }
        }

        void IModuleManagerImplementation.OnViewModelRemoved(string regionName, ViewModelRemovedEventArgs e)
        {
            this.GetRegion(regionName).Remove(e.ViewModelKey);
            this.RaiseViewModelRemovedEvent(regionName, e);
        }

        void IModuleManagerImplementation.OnViewModelRemoving(string regionName, ViewModelRemovingEventArgs e)
        {
            if (this.GetRegion(regionName).Contains(e.ViewModelKey))
            {
                this.RaiseViewModelRemovingEvent(regionName, e);
            }
        }

        void IModuleManagerImplementation.RaiseViewModelCreated(ViewModelCreatedEventArgs e)
        {
            this.RaiseViewModelCreatedEvent(e.RegionName, e);
        }

        private IViewModelEventManagerImplementation GetEvents(object viewModel, bool createIfNotExist)
        {
            Verifier.VerifyViewModel(viewModel);
            Func<WeakReference, bool> predicate = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<WeakReference, bool> local1 = <>c.<>9__23_0;
                predicate = <>c.<>9__23_0 = x => !x.IsAlive;
            }
            this.viewModelEvents.Keys.Where<WeakReference>(predicate).ToList<WeakReference>().ForEach(delegate (WeakReference x) {
                this.viewModelEvents.Remove(x);
            });
            WeakReference key = (from x in this.viewModelEvents.Keys
                where x.Target == viewModel
                select x).FirstOrDefault<WeakReference>();
            if (key == null)
            {
                if (!createIfNotExist)
                {
                    return null;
                }
                key = new WeakReference(viewModel);
                this.viewModelEvents.Add(key, new ViewModelEventManager(viewModel));
            }
            return this.viewModelEvents[key];
        }

        private IRegionEventManagerImplementation GetEvents(string regionName, bool createIfNotExist)
        {
            Verifier.VerifyRegionName(regionName);
            if (!this.regionEvents.ContainsKey(regionName))
            {
                if (!createIfNotExist)
                {
                    return null;
                }
                this.regionEvents.Add(regionName, new RegionEventManager());
            }
            return this.regionEvents[regionName];
        }

        protected IModule GetModule(string regionName, string key, bool throwIfNull)
        {
            Verifier.VerifyRegionName(regionName);
            Verifier.VerifyKey(key);
            Func<ModuleWrapper, IModule> evaluator = <>c.<>9__16_1;
            if (<>c.<>9__16_1 == null)
            {
                Func<ModuleWrapper, IModule> local1 = <>c.<>9__16_1;
                evaluator = <>c.<>9__16_1 = x => x.Module;
            }
            IModule objA = this.modules.FirstOrDefault<ModuleWrapper>(x => ((x.RegionName == regionName) && (x.Module.Key == key))).With<ModuleWrapper, IModule>(evaluator);
            if (ReferenceEquals(objA, null) & throwIfNull)
            {
                ModuleInjectionException.ModuleMissing(regionName, key);
            }
            return objA;
        }

        protected IRegionImplementation GetRegion(string regionName)
        {
            Verifier.VerifyRegionName(regionName);
            IRegionImplementation item = this.regions.FirstOrDefault<IRegionImplementation>(x => x.RegionName == regionName);
            if (item == null)
            {
                item = this.CreateRegion(regionName);
                this.regions.Add(item);
            }
            return item;
        }

        private IUIWindowRegion GetUIWindowRegion(string regionName) => 
            this.GetRegion(regionName).UIRegions.OfType<IUIWindowRegion>().LastOrDefault<IUIWindowRegion>();

        private Task<WindowInjectionResult> GetWindowInjectionResult(string regionName, string key, bool returnNullIfNotShown)
        {
            if (returnNullIfNotShown && !((IModuleWindowManager) this).IsShown(regionName, key))
            {
                return null;
            }
            TaskCompletionSource<WindowInjectionResult> task = new TaskCompletionSource<WindowInjectionResult>();
            this.windowClosedHandlers.Add(new WindowClosedHandler(regionName, key, this, task, delegate (WindowClosedHandler x) {
                this.windowClosedHandlers.Remove(x);
            }));
            return task.Task;
        }

        private void RaiseNavigationEvent(string regionName, NavigationEventArgs e)
        {
            if (e.OldViewModel != null)
            {
                this.GetEvents(e.OldViewModel, false).Do<IViewModelEventManagerImplementation>(x => x.RaiseNavigatedAway(this, e));
            }
            if (e.NewViewModel != null)
            {
                this.GetEvents(e.NewViewModel, false).Do<IViewModelEventManagerImplementation>(x => x.RaiseNavigated(this, e));
            }
            this.GetEvents(regionName, false).Do<IRegionEventManagerImplementation>(x => x.RaiseNavigation(this, e));
        }

        private void RaiseViewModelCreatedEvent(string regionName, ViewModelCreatedEventArgs e)
        {
            this.GetEvents(regionName, false).Do<IRegionEventManagerImplementation>(x => x.RaiseViewModelCreated(this, e));
        }

        private void RaiseViewModelRemovedEvent(string regionName, ViewModelRemovedEventArgs e)
        {
            if (e.ViewModel != null)
            {
                this.GetEvents(e.ViewModel, false).Do<IViewModelEventManagerImplementation>(x => x.RaiseViewModelRemoved(this, e));
            }
            this.GetEvents(regionName, false).Do<IRegionEventManagerImplementation>(x => x.RaiseViewModelRemoved(this, e));
        }

        private void RaiseViewModelRemovingEvent(string regionName, ViewModelRemovingEventArgs e)
        {
            if (e.ViewModel != null)
            {
                this.GetEvents(e.ViewModel, false).Do<IViewModelEventManagerImplementation>(x => x.RaiseViewModelRemoving(this, e));
            }
            this.GetEvents(regionName, false).Do<IRegionEventManagerImplementation>(x => x.RaiseViewModelRemoving(this, e));
        }

        public static IModuleManager DefaultManager
        {
            get => 
                _default ?? _defaultInstance;
            set => 
                _default = value;
        }

        public static IModuleWindowManager DefaultWindowManager =>
            (IModuleWindowManager) DefaultManager;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IModuleManagerImplementation DefaultImplementation =>
            (IModuleManagerImplementation) DefaultManager;

        bool IModuleManagerImplementation.KeepViewModelsAlive =>
            this.keepViewModelsAlive;

        IViewModelLocator IModuleManagerImplementation.ViewModelLocator =>
            this.viewModelLocator ?? ViewModelLocator.Default;

        IViewLocator IModuleManagerImplementation.ViewLocator =>
            this.viewLocator ?? ViewLocatorHelper.Default;

        IStateSerializer IModuleManagerImplementation.ViewModelStateSerializer =>
            this.viewModelStateSerializer ?? StateSerializer.Default;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModuleManager.<>c <>9 = new ModuleManager.<>c();
            public static Func<ModuleManager.ModuleWrapper, IModule> <>9__16_1;
            public static Func<WeakReference, bool> <>9__23_0;
            public static Func<RegionInfo, string> <>9__36_0;
            public static Func<RegionVisualInfo, string> <>9__36_1;
            public static Func<LogicalInfo, string, RegionInfo> <>9__36_3;
            public static Func<VisualInfo, string, RegionVisualInfo> <>9__36_5;
            public static Action<IRegionImplementation> <>9__36_9;
            public static Action<IRegionImplementation> <>9__36_10;
            public static Func<object, object> <>9__41_1;

            internal object <DevExpress.Mvvm.ModuleInjection.IModuleManager.Clear>b__41_1(object x) => 
                x;

            internal string <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_0(RegionInfo x) => 
                x.RegionName;

            internal string <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_1(RegionVisualInfo x) => 
                x.RegionName;

            internal void <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_10(IRegionImplementation x)
            {
                x.ApplyInfo(false, true);
            }

            internal RegionInfo <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_3(LogicalInfo info, string regionName) => 
                (info != null) ? info.Regions.FirstOrDefault<RegionInfo>(x => (x.RegionName == regionName)) : null;

            internal RegionVisualInfo <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_5(VisualInfo info, string regionName) => 
                (info != null) ? info.Regions.FirstOrDefault<RegionVisualInfo>(x => (x.RegionName == regionName)) : null;

            internal void <DevExpress.Mvvm.ModuleInjection.IModuleManagerBase.Restore>b__36_9(IRegionImplementation x)
            {
                x.ApplyInfo(true, false);
            }

            internal bool <GetEvents>b__23_0(WeakReference x) => 
                !x.IsAlive;

            internal IModule <GetModule>b__16_1(ModuleManager.ModuleWrapper x) => 
                x.Module;
        }

        private class ModuleWrapper
        {
            public ModuleWrapper(string regionName, IModule module)
            {
                this.RegionName = regionName;
                this.Module = module;
            }

            public string RegionName { get; private set; }

            public IModule Module { get; private set; }
        }

        private class WindowClosedHandler
        {
            private readonly ModuleManager owner;
            private readonly TaskCompletionSource<WindowInjectionResult> task;
            private readonly Action<ModuleManager.WindowClosedHandler> dispose;

            public WindowClosedHandler(string regionName, string key, ModuleManager owner, TaskCompletionSource<WindowInjectionResult> task, Action<ModuleManager.WindowClosedHandler> dispose)
            {
                this.RegionName = regionName;
                this.Key = key;
                this.owner = owner;
                this.task = task;
                this.dispose = dispose;
                owner.GetEvents(regionName, true).ViewModelRemoved += new EventHandler<ViewModelRemovedEventArgs>(this.OnWindowClosed);
            }

            private void OnWindowClosed(object sender, ViewModelRemovedEventArgs e)
            {
                if (e.ViewModelKey == this.Key)
                {
                    MessageBoxResult? result;
                    IUIWindowRegion uIWindowRegion = this.owner.GetUIWindowRegion(e.RegionName);
                    this.owner.GetEvents(e.RegionName, true).ViewModelRemoved -= new EventHandler<ViewModelRemovedEventArgs>(this.OnWindowClosed);
                    if (uIWindowRegion != null)
                    {
                        result = uIWindowRegion.Result;
                    }
                    else
                    {
                        result = null;
                    }
                    this.task.SetResult(new WindowInjectionResult(e.RegionName, e.ViewModel, e.ViewModelKey, result));
                    this.dispose(this);
                }
            }

            public string RegionName { get; private set; }

            public string Key { get; private set; }
        }
    }
}

