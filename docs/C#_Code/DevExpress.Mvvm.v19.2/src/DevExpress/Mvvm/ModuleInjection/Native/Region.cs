namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class Region : IRegionImplementation, IRegion
    {
        private readonly List<RegionItem> items;
        private readonly WeakReferenceManager<IUIRegion> serviceManager;
        private readonly IModuleManagerImplementation owner;
        private string navigationKey;
        private static string navigationKeyNull = "__region_navigationKeyNull__";
        private DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode logicalSerializationMode = DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Enabled;
        private DevExpress.Mvvm.ModuleInjection.VisualSerializationMode visualSerializationMode = DevExpress.Mvvm.ModuleInjection.VisualSerializationMode.PerViewType;
        private RegionVisualInfo visualState;
        private RegionInfo logicalState;
        private string restoreSelectedKey;
        private readonly Dictionary<string, DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode> customLogicalSerializationMode = new Dictionary<string, DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode>();
        private readonly Dictionary<string, DevExpress.Mvvm.ModuleInjection.VisualSerializationMode> customVisualSerializationMode = new Dictionary<string, DevExpress.Mvvm.ModuleInjection.VisualSerializationMode>();

        public Region(string regionName, IModuleManagerImplementation owner, bool isTestingMode)
        {
            this.LogicalSerializationMode = DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Enabled;
            this.RegionName = regionName;
            this.owner = owner;
            this.serviceManager = new WeakReferenceManager<IUIRegion>();
            this.items = new List<RegionItem>();
            if (isTestingMode)
            {
                this.RegisterUIRegion(new TestUIRegion(regionName, owner));
            }
            else
            {
                foreach (IUIRegion region in from x in ServiceContainer.Default.GetServices<IUIRegion>(true)
                    where x.RegionName == regionName
                    select x)
                {
                    this.RegisterUIRegion(region);
                }
            }
        }

        public void ApplyInfo(bool inject, bool navigate)
        {
            if (this.items.Count != 0)
            {
                if (inject)
                {
                    foreach (RegionItem item in this.items)
                    {
                        <>c__DisplayClass63_0 class_;
                        this.DoForeachUIRegion(x => item.Inject(x, new Action<string, object>(class_.OnViewModelCreated)));
                    }
                }
                if (navigate && (this.LogicalSerializationMode == DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Enabled))
                {
                    this.navigationKey = (this.restoreSelectedKey == null) ? navigationKeyNull : this.restoreSelectedKey;
                    this.TryNavigate(false);
                    this.restoreSelectedKey = null;
                }
            }
        }

        public void Clear()
        {
            this.SaveLogicalState();
            Action<IUIRegion> action = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Action<IUIRegion> local1 = <>c.<>9__27_0;
                action = <>c.<>9__27_0 = x => x.Clear();
            }
            this.DoForeachUIRegion(action);
            Action<RegionItem> action2 = <>c.<>9__27_1;
            if (<>c.<>9__27_1 == null)
            {
                Action<RegionItem> local2 = <>c.<>9__27_1;
                action2 = <>c.<>9__27_1 = x => x.RemoveViewModel();
            }
            this.items.ForEach(action2);
            this.items.Clear();
            this.navigationKey = null;
            this.SelectedKey = null;
        }

        public bool Contains(string key) => 
            this.GetItem(key) != null;

        private void DoForeachUIRegion(Action<IUIRegion> action)
        {
            this.GetUIRegions().Reverse<IUIRegion>().ToList<IUIRegion>().ForEach(action);
        }

        public void GetInfo(out RegionInfo logicalInfo, out RegionVisualInfo visualInfo)
        {
            this.SaveLogicalState();
            this.UpdateVisualState();
            visualInfo = this.VisualState;
            logicalInfo = this.LogicalState;
        }

        private RegionItem GetItem(object viewModel) => 
            (viewModel != null) ? (from x in this.items
                where x.ViewModel == viewModel
                select x).FirstOrDefault<RegionItem>() : null;

        private RegionItem GetItem(string key) => 
            (key != null) ? (from x in this.items
                where x.Key == key
                select x).FirstOrDefault<RegionItem>() : null;

        public string GetKey(object viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }
            RegionItem item = (from x in this.items
                where x.ViewModel == viewModel
                select x).FirstOrDefault<RegionItem>();
            return item?.Key;
        }

        public DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode GetLogicalSerializationMode(string key) => 
            this.GetSerializationMode<DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode>(this.customLogicalSerializationMode, key, this.LogicalSerializationMode);

        public void GetSavedVisualState(object viewModel, string viewPart, out string state)
        {
            state = null;
            RegionItem item = this.GetItem(viewModel);
            if (item != null)
            {
                RegionItemVisualInfo info = this.GetVisualInfo(item.Key, item.GetViewName(), viewPart, false);
                if ((info != null) && (info.State != null))
                {
                    state = info.State.State;
                }
            }
        }

        private T GetSerializationMode<T>(Dictionary<string, T> storage, string key, T globalValue) => 
            !storage.ContainsKey(key) ? globalValue : storage[key];

        private IEnumerable<IUIRegion> GetUIRegions() => 
            (from x in this.serviceManager.Get()
                where x.RegionName == this.RegionName
                select x).ToArray<IUIRegion>();

        public object GetViewModel(string key)
        {
            if (key == null)
            {
                return null;
            }
            RegionItem item = this.GetItem(key);
            return item?.ViewModel;
        }

        private RegionItemVisualInfo GetVisualInfo(string key, string viewName, string viewPart, bool createIfNotExist)
        {
            RegionItemVisualInfo objA = null;
            if (this.GetVisualSerializationMode(key) == DevExpress.Mvvm.ModuleInjection.VisualSerializationMode.Disabled)
            {
                return null;
            }
            if (this.GetVisualSerializationMode(key) == DevExpress.Mvvm.ModuleInjection.VisualSerializationMode.PerKey)
            {
                objA = this.VisualState.Items.FirstOrDefault<RegionItemVisualInfo>(x => (x.Key == key) && ((x.ViewName == viewName) && (x.ViewPart == viewPart)));
            }
            else if (this.GetVisualSerializationMode(key) == DevExpress.Mvvm.ModuleInjection.VisualSerializationMode.PerViewType)
            {
                objA = this.VisualState.Items.FirstOrDefault<RegionItemVisualInfo>(x => (x.ViewName == viewName) && (x.ViewPart == viewPart));
                if (objA != null)
                {
                    objA.Key = null;
                }
            }
            if (ReferenceEquals(objA, null) & createIfNotExist)
            {
                RegionItemVisualInfo info1 = new RegionItemVisualInfo();
                info1.ViewName = viewName;
                info1.ViewPart = viewPart;
                objA = info1;
                if (this.GetVisualSerializationMode(key) == DevExpress.Mvvm.ModuleInjection.VisualSerializationMode.PerKey)
                {
                    objA.Key = key;
                }
                this.VisualState.Items.Add(objA);
            }
            return objA;
        }

        public DevExpress.Mvvm.ModuleInjection.VisualSerializationMode GetVisualSerializationMode(string key) => 
            this.GetSerializationMode<DevExpress.Mvvm.ModuleInjection.VisualSerializationMode>(this.customVisualSerializationMode, key, this.VisualSerializationMode);

        public void Inject(IModule module, object parameter)
        {
            <>c__DisplayClass24_0 class_;
            RegionItem item = new RegionItem(this.owner, module, parameter, this.LogicalState.Items.FirstOrDefault<RegionItemInfo>(x => x.Key == module.Key));
            this.items.Add(item);
            this.DoForeachUIRegion(x => item.Inject(x, new Action<string, object>(class_.OnViewModelCreated)));
            this.TryNavigate(true);
        }

        public void Navigate(string key)
        {
            this.navigationKey = (key == null) ? navigationKeyNull : key;
            this.TryNavigate(true);
        }

        public void OnNavigation(string key, object vm)
        {
            this.SelectedKey = key;
            this.DoForeachUIRegion(x => x.SelectedViewModel = vm);
        }

        private void OnViewModelCreated(string key, object viewModel)
        {
            this.owner.RaiseViewModelCreated(new ViewModelCreatedEventArgs(this.RegionName, viewModel, key));
        }

        public void RegisterUIRegion(IUIRegion region)
        {
            if (!this.serviceManager.Contains(region))
            {
                foreach (RegionItem item in this.items)
                {
                    item.Inject(region, new Action<string, object>(this.OnViewModelCreated));
                }
                this.serviceManager.Add(region);
                if (!this.TryNavigate(true) && (this.SelectedKey != null))
                {
                    region.SelectedViewModel = this.SelectedViewModel;
                }
            }
        }

        public void Remove(string key)
        {
            if (key != null)
            {
                RegionItem item = this.GetItem(key);
                if (item != null)
                {
                    if (item.ViewModel != null)
                    {
                        this.SaveLogicalState(item);
                        this.DoForeachUIRegion(x => x.Remove(item.ViewModel));
                    }
                    this.items.Remove(item);
                    item.RemoveViewModel();
                }
            }
        }

        public void ResetVisualState()
        {
            this.VisualState = null;
        }

        private void SaveLogicalState()
        {
            if (this.LogicalSerializationMode == DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Enabled)
            {
                this.LogicalState.SelectedViewModelKey = this.SelectedKey;
            }
            Dictionary<string, int> order = new Dictionary<string, int>();
            int num = 0;
            foreach (RegionItem item in this.items)
            {
                order.Add(item.Key, num++);
                this.SaveLogicalState(item);
            }
            foreach (RegionItemInfo itemInfo in this.LogicalState.Items)
            {
                if (!this.items.Any<RegionItem>(x => (x.Key == itemInfo.Key)))
                {
                    itemInfo.IsInjected = false;
                    order.Add(itemInfo.Key, 0x7fffffff);
                }
            }
            this.LogicalState.Items.Sort((x, y) => order[x.Key] - order[y.Key]);
        }

        private void SaveLogicalState(RegionItem item)
        {
            if (item != null)
            {
                RegionItemInfo info = this.LogicalState.Items.FirstOrDefault<RegionItemInfo>(x => x.Key == item.Key);
                if (this.GetLogicalSerializationMode(item.Key) == DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Disabled)
                {
                    if (info != null)
                    {
                        this.LogicalState.Items.Remove(info);
                    }
                }
                else
                {
                    bool flag = this.items.Contains(item);
                    RegionItemInfo logicalInfo = item.GetLogicalInfo();
                    if (logicalInfo == null)
                    {
                        if (info != null)
                        {
                            info.IsInjected = false;
                        }
                    }
                    else if (info == null)
                    {
                        this.LogicalState.Items.Add(logicalInfo);
                        logicalInfo.IsInjected = flag;
                    }
                    else
                    {
                        info.IsInjected = flag;
                        info.ViewModelName = logicalInfo.ViewModelName;
                        info.ViewModelState = logicalInfo.ViewModelState;
                        info.ViewModelStateType = logicalInfo.ViewModelStateType;
                        info.ViewName = logicalInfo.ViewName;
                    }
                }
            }
        }

        public void SaveVisualState(object viewModel, string viewPart, string state)
        {
            RegionItem item = this.GetItem(viewModel);
            if (item != null)
            {
                RegionItemVisualInfo info = this.GetVisualInfo(item.Key, item.GetViewName(), viewPart, true);
                if (info != null)
                {
                    info.State = new SerializableState(state);
                }
            }
        }

        private void SetCustomSerializationMode<T>(Dictionary<string, T> storage, string key, T? mode) where T: struct
        {
            if (mode == null)
            {
                if (storage.ContainsKey(key))
                {
                    storage.Remove(key);
                }
            }
            else if (!storage.ContainsKey(key))
            {
                storage.Add(key, mode.Value);
            }
            else
            {
                storage[key] = mode.Value;
            }
        }

        public void SetInfo(RegionInfo logicalInfo, RegionVisualInfo visualInfo)
        {
            this.VisualState = visualInfo;
            this.LogicalState = logicalInfo;
            foreach (RegionItemInfo info in this.LogicalState.Items)
            {
                if ((this.GetLogicalSerializationMode(info.Key) != DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Disabled) && info.IsInjected)
                {
                    this.items.Add(new RegionItem(this.owner, info));
                }
            }
            if (this.LogicalSerializationMode == DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode.Enabled)
            {
                this.restoreSelectedKey = logicalInfo.SelectedViewModelKey;
            }
        }

        public void SetLogicalSerializationMode(string key, DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode? mode)
        {
            this.SetCustomSerializationMode<DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode>(this.customLogicalSerializationMode, key, mode);
        }

        public void SetVisualSerializationMode(string key, DevExpress.Mvvm.ModuleInjection.VisualSerializationMode? mode)
        {
            this.SetCustomSerializationMode<DevExpress.Mvvm.ModuleInjection.VisualSerializationMode>(this.customVisualSerializationMode, key, mode);
        }

        private bool TryNavigate(bool focus = true)
        {
            if ((this.navigationKey == null) || !this.GetUIRegions().Any<IUIRegion>())
            {
                return false;
            }
            if (this.navigationKey != navigationKeyNull)
            {
                RegionItem item = this.GetItem(this.navigationKey);
                if ((item == null) || (item.ViewModel == null))
                {
                    return false;
                }
                this.DoForeachUIRegion(delegate (IUIRegion x) {
                    x.SelectViewModel(item.ViewModel, focus);
                });
                this.navigationKey = null;
                return true;
            }
            Action<IUIRegion> action = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Action<IUIRegion> local1 = <>c.<>9__30_0;
                action = <>c.<>9__30_0 = delegate (IUIRegion x) {
                    x.SelectedViewModel = null;
                };
            }
            this.DoForeachUIRegion(action);
            this.navigationKey = null;
            return true;
        }

        public void UnregisterUIRegion(IUIRegion region)
        {
            if (this.serviceManager.Contains(region))
            {
                this.serviceManager.Remove(region);
            }
        }

        private void UpdateVisualState()
        {
            Func<object, IEnumerable<IVisualStateServiceImplementation>> selector = <>c.<>9__68_0;
            if (<>c.<>9__68_0 == null)
            {
                Func<object, IEnumerable<IVisualStateServiceImplementation>> local1 = <>c.<>9__68_0;
                selector = <>c.<>9__68_0 = x => VisualStateServiceHelper.GetServices(x, false, true);
            }
            Action<IVisualStateServiceImplementation> action = <>c.<>9__68_1;
            if (<>c.<>9__68_1 == null)
            {
                Action<IVisualStateServiceImplementation> local2 = <>c.<>9__68_1;
                action = <>c.<>9__68_1 = x => x.EnforceSaveState();
            }
            this.ViewModels.SelectMany<object, IVisualStateServiceImplementation>(selector).ToList<IVisualStateServiceImplementation>().ForEach(action);
        }

        public string RegionName { get; private set; }

        public IEnumerable<IUIRegion> UIRegions =>
            this.GetUIRegions();

        public IEnumerable<object> ViewModels
        {
            get
            {
                Func<RegionItem, bool> predicate = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<RegionItem, bool> local1 = <>c.<>9__12_0;
                    predicate = <>c.<>9__12_0 = x => x.ViewModel != null;
                }
                Func<RegionItem, object> selector = <>c.<>9__12_1;
                if (<>c.<>9__12_1 == null)
                {
                    Func<RegionItem, object> local2 = <>c.<>9__12_1;
                    selector = <>c.<>9__12_1 = x => x.ViewModel;
                }
                return this.items.Where<RegionItem>(predicate).Select<RegionItem, object>(selector);
            }
        }

        public string SelectedKey { get; private set; }

        public object SelectedViewModel =>
            this.GetViewModel(this.SelectedKey);

        public DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode LogicalSerializationMode
        {
            get => 
                this.logicalSerializationMode;
            set => 
                this.logicalSerializationMode = value;
        }

        public DevExpress.Mvvm.ModuleInjection.VisualSerializationMode VisualSerializationMode
        {
            get => 
                this.visualSerializationMode;
            set => 
                this.visualSerializationMode = value;
        }

        private RegionVisualInfo VisualState
        {
            get
            {
                RegionVisualInfo visualState = this.visualState;
                if (this.visualState == null)
                {
                    RegionVisualInfo local1 = this.visualState;
                    RegionVisualInfo info1 = new RegionVisualInfo();
                    info1.RegionName = this.RegionName;
                    visualState = this.visualState = info1;
                }
                return visualState;
            }
            set
            {
                if (value == null)
                {
                    RegionVisualInfo info1 = new RegionVisualInfo();
                    info1.RegionName = this.RegionName;
                    value = info1;
                }
                if (value.RegionName == this.RegionName)
                {
                    this.visualState = value;
                }
            }
        }

        private RegionInfo LogicalState
        {
            get
            {
                RegionInfo logicalState = this.logicalState;
                if (this.logicalState == null)
                {
                    RegionInfo local1 = this.logicalState;
                    RegionInfo info1 = new RegionInfo();
                    info1.RegionName = this.RegionName;
                    logicalState = this.logicalState = info1;
                }
                return logicalState;
            }
            set
            {
                if (value == null)
                {
                    RegionInfo info1 = new RegionInfo();
                    info1.RegionName = this.RegionName;
                    value = info1;
                }
                if (value.RegionName == this.RegionName)
                {
                    this.logicalState = value;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Region.<>c <>9 = new Region.<>c();
            public static Func<Region.RegionItem, bool> <>9__12_0;
            public static Func<Region.RegionItem, object> <>9__12_1;
            public static Action<IUIRegion> <>9__27_0;
            public static Action<Region.RegionItem> <>9__27_1;
            public static Action<IUIRegion> <>9__30_0;
            public static Func<object, IEnumerable<IVisualStateServiceImplementation>> <>9__68_0;
            public static Action<IVisualStateServiceImplementation> <>9__68_1;

            internal void <Clear>b__27_0(IUIRegion x)
            {
                x.Clear();
            }

            internal void <Clear>b__27_1(Region.RegionItem x)
            {
                x.RemoveViewModel();
            }

            internal bool <get_ViewModels>b__12_0(Region.RegionItem x) => 
                x.ViewModel != null;

            internal object <get_ViewModels>b__12_1(Region.RegionItem x) => 
                x.ViewModel;

            internal void <TryNavigate>b__30_0(IUIRegion x)
            {
                x.SelectedViewModel = null;
            }

            internal IEnumerable<IVisualStateServiceImplementation> <UpdateVisualState>b__68_0(object x) => 
                VisualStateServiceHelper.GetServices(x, false, true);

            internal void <UpdateVisualState>b__68_1(IVisualStateServiceImplementation x)
            {
                x.EnforceSaveState();
            }
        }

        private class RegionItem
        {
            private WeakReference viewModelRef;
            private object viewModel;
            private readonly bool keepViewModelAlive;
            private readonly IViewModelLocator viewModelLocator;
            private readonly IViewLocator viewLocator;
            private readonly IStateSerializer stateSerializer;

            public RegionItem(IModuleManagerImplementation manager, RegionItemInfo info) : this(manager.ViewModelLocator, manager.ViewLocator, manager.ViewModelStateSerializer, info.Key, null, info.ViewModelName, info.ViewName, null, null, manager.KeepViewModelsAlive)
            {
                Func<SerializableState, string> evaluator = <>c.<>9__37_0;
                if (<>c.<>9__37_0 == null)
                {
                    Func<SerializableState, string> local1 = <>c.<>9__37_0;
                    evaluator = <>c.<>9__37_0 = x => x.State;
                }
                this.SetViewModelState(info.ViewModelState.With<SerializableState, string>(evaluator));
            }

            public RegionItem(IModuleManagerImplementation manager, IModule module, object parameter, RegionItemInfo info) : this(manager.ViewModelLocator, manager.ViewLocator, manager.ViewModelStateSerializer, module.Key, module.ViewModelFactory, module.ViewModelName, module.ViewName, module.ViewType, parameter, manager.KeepViewModelsAlive)
            {
                if (info != null)
                {
                    Func<SerializableState, string> evaluator = <>c.<>9__36_0;
                    if (<>c.<>9__36_0 == null)
                    {
                        Func<SerializableState, string> local1 = <>c.<>9__36_0;
                        evaluator = <>c.<>9__36_0 = x => x.State;
                    }
                    this.SetViewModelState(info.ViewModelState.With<SerializableState, string>(evaluator));
                }
            }

            private RegionItem(IViewModelLocator viewModelLocator, IViewLocator viewLocator, IStateSerializer stateSerializer, string key, Func<object> factory, string viewModelName, string viewName, Type viewType, object parameter, bool keepViewModelAlive)
            {
                this.keepViewModelAlive = keepViewModelAlive;
                this.viewModelLocator = viewModelLocator;
                this.viewLocator = viewLocator;
                this.stateSerializer = stateSerializer;
                this.Key = key;
                this.Factory = factory;
                if (this.Factory == null)
                {
                    this.ViewModelName = viewModelName;
                }
                this.ViewType = viewType;
                if (this.ViewType == null)
                {
                    this.ViewName = viewName;
                }
                this.Parameter = parameter;
            }

            public RegionItemInfo GetLogicalInfo()
            {
                string str;
                if (this.ViewModel == null)
                {
                    return null;
                }
                RegionItemInfo info1 = new RegionItemInfo();
                info1.Key = this.Key;
                info1.ViewModelName = this.ViewModelName;
                info1.ViewName = this.ViewName;
                RegionItemInfo info = info1;
                this.GetViewModelState(out str);
                info.ViewModelStateType = null;
                info.ViewModelState = new SerializableState(str);
                return info;
            }

            private void GetViewModelState(out string state)
            {
                state = null;
                if (this.ViewModel != null)
                {
                    Type stateType = ISupportStateHelper.GetStateType(this.ViewModel.GetType());
                    if (stateType != null)
                    {
                        object obj2 = ISupportStateHelper.GetState(this.ViewModel);
                        if (obj2 != null)
                        {
                            state = this.stateSerializer.SerializeState(obj2, stateType);
                        }
                    }
                }
            }

            public string GetViewName()
            {
                this.InitViewName();
                return this.ViewName;
            }

            private void Init()
            {
                object target = null;
                if (this.Factory != null)
                {
                    target = this.Factory();
                }
                else if (this.ViewModelName != null)
                {
                    target = this.viewModelLocator.ResolveViewModel(this.ViewModelName);
                    if (target == null)
                    {
                        ModuleInjectionException.CannotResolveVM(this.ViewModelName);
                    }
                }
                if (target == null)
                {
                    ModuleInjectionException.NullVM();
                }
                if (this.keepViewModelAlive)
                {
                    this.viewModel = target;
                }
                else
                {
                    this.viewModelRef = new WeakReference(target);
                }
                this.InitParameter();
                if (string.IsNullOrEmpty(this.ViewModelName))
                {
                    this.ViewModelName = this.viewModelLocator.GetViewModelTypeName(target.GetType());
                }
                this.InitViewType();
                this.InitViewName();
            }

            private void InitParameter()
            {
                if (this.Parameter != null)
                {
                    Verifier.VerifyViewModelISupportParameter(this.ViewModel);
                    ((ISupportParameter) this.ViewModel).Parameter = this.Parameter;
                    this.Parameter = null;
                }
            }

            private void InitViewName()
            {
                if (string.IsNullOrEmpty(this.ViewName))
                {
                    if (this.ViewType == null)
                    {
                        this.ViewName = null;
                    }
                    else
                    {
                        this.ViewName = this.viewLocator.GetViewTypeName(this.ViewType);
                    }
                }
            }

            private void InitViewType()
            {
                if ((this.ViewType == null) && !string.IsNullOrEmpty(this.ViewName))
                {
                    this.ViewType = this.viewLocator.ResolveViewType(this.ViewName);
                }
            }

            public void Inject(IUIRegion service, Action<string, object> onViewModelCreated)
            {
                if (this.ViewModel == null)
                {
                    this.Init();
                    this.UpdateViewModelState();
                    onViewModelCreated(this.Key, this.ViewModel);
                }
                service.Inject(this.ViewModel, this.ViewType);
            }

            public void RemoveViewModel()
            {
                this.viewModel = null;
            }

            private void SetViewModelState(string state)
            {
                this.ViewModelState = state;
                this.UpdateViewModelState();
            }

            private void UpdateViewModelState()
            {
                if ((this.ViewModel != null) && !string.IsNullOrEmpty(this.ViewModelState))
                {
                    Type stateType = ISupportStateHelper.GetStateType(this.ViewModel.GetType());
                    if (stateType != null)
                    {
                        object state = this.stateSerializer.DeserializeState(this.ViewModelState, stateType);
                        ISupportStateHelper.RestoreState(this.ViewModel, state);
                    }
                }
            }

            public string Key { get; private set; }

            public object ViewModel
            {
                get
                {
                    object viewModel = this.viewModel;
                    if (this.viewModel == null)
                    {
                        object local1 = this.viewModel;
                        if (this.viewModelRef == null)
                        {
                            WeakReference viewModelRef = this.viewModelRef;
                            return null;
                        }
                        viewModel = this.viewModelRef.Target;
                    }
                    return viewModel;
                }
            }

            private Func<object> Factory { get; set; }

            private string ViewModelName { get; set; }

            private string ViewName { get; set; }

            private Type ViewType { get; set; }

            private string ViewModelState { get; set; }

            private object Parameter { get; set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly Region.RegionItem.<>c <>9 = new Region.RegionItem.<>c();
                public static Func<SerializableState, string> <>9__36_0;
                public static Func<SerializableState, string> <>9__37_0;

                internal string <.ctor>b__36_0(SerializableState x) => 
                    x.State;

                internal string <.ctor>b__37_0(SerializableState x) => 
                    x.State;
            }
        }

        private class TestUIRegion : IUIRegion, IUIWindowRegion
        {
            private readonly IModuleManagerImplementation owner;
            private readonly ObservableCollection<object> viewModels;
            private object selectedViewModel;
            private MessageBoxResult? setResult;

            public TestUIRegion(string regionName, IModuleManagerImplementation owner)
            {
                this.owner = owner;
                this.RegionName = regionName;
                this.viewModels = new ObservableCollection<object>();
            }

            void IUIRegion.Clear()
            {
                this.viewModels.Clear();
            }

            object IUIRegion.GetView(object viewModel) => 
                null;

            void IUIRegion.Inject(object viewModel, Type viewType)
            {
                if (viewModel != null)
                {
                    this.viewModels.Add(viewModel);
                }
            }

            void IUIRegion.Remove(object viewModel)
            {
                if (this.viewModels.Contains(viewModel))
                {
                    this.viewModels.Remove(viewModel);
                }
            }

            void IUIRegion.SelectViewModel(object vm, bool focus)
            {
                ((IUIRegion) this).SelectedViewModel = vm;
            }

            void IUIWindowRegion.SetResult(MessageBoxResult result)
            {
                this.setResult = new MessageBoxResult?(result);
            }

            private void OnSelectedViewModelChanged(object oldValue, object newValue)
            {
                this.owner.OnNavigation(this.RegionName, new NavigationEventArgs(this.RegionName, oldValue, newValue, this.owner.GetRegion(this.RegionName).GetKey(oldValue), this.owner.GetRegion(this.RegionName).GetKey(newValue)));
            }

            public string RegionName { get; private set; }

            IEnumerable<object> IUIRegion.ViewModels =>
                this.viewModels;

            object IUIRegion.SelectedViewModel
            {
                get => 
                    this.selectedViewModel;
                set
                {
                    if (this.selectedViewModel != value)
                    {
                        object selectedViewModel = this.selectedViewModel;
                        this.selectedViewModel = value;
                        this.OnSelectedViewModelChanged(selectedViewModel, this.selectedViewModel);
                    }
                }
            }

            MessageBoxResult? IUIWindowRegion.Result =>
                this.setResult;
        }
    }
}

