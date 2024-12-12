namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    public static class DXSplashScreen
    {
        public static readonly DependencyProperty SplashScreenTypeProperty = DependencyProperty.RegisterAttached("SplashScreenType", typeof(Type), typeof(DXSplashScreen), new PropertyMetadata(null, new PropertyChangedCallback(DXSplashScreen.OnSplashScreenTypeChanged)));
        internal static SplashScreenContainer SplashContainer = null;
        private static bool lastIsActive = false;

        static DXSplashScreen()
        {
            MainThreadDelay = 700;
            DisableThreadingProblemsDetection = false;
        }

        public static void CallSplashScreenMethod<T>(Action<T> action) where T: Window
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Show SplashScreen before calling this method.");
            }
            SplashContainer.CallSplashScreenMethod<T>(action);
        }

        internal static void CheckSplashScreenType(Type splashScreenType)
        {
            if (typeof(Window).IsAssignableFrom(splashScreenType) && !typeof(ISplashScreen).IsAssignableFrom(splashScreenType))
            {
                throw new InvalidOperationException($"{splashScreenType.Name} should implement the ISplashScreen interface");
            }
            if (!typeof(FrameworkElement).IsAssignableFrom(splashScreenType))
            {
                throw new InvalidOperationException("Incorrect SplashScreen Type.");
            }
        }

        public static void Close()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Show SplashScreen before calling this method.");
            }
            SplashContainer.Close();
            NotifyIsActiveChanged();
        }

        private static object CreateDefaultSplashScreen(object parameter)
        {
            Type type = SplashScreenHelper.FindParameter<Type>(parameter, null);
            SplashScreenViewModel model = SplashScreenHelper.FindParameter<SplashScreenViewModel>(parameter, null);
            model = (model == null) ? new SplashScreenViewModel() : model.Clone();
            object input = null;
            if (type != null)
            {
                input = Activator.CreateInstance(type);
                Func<object, FrameworkElement> evaluator = <>c.<>9__49_0;
                if (<>c.<>9__49_0 == null)
                {
                    Func<object, FrameworkElement> local1 = <>c.<>9__49_0;
                    evaluator = <>c.<>9__49_0 = x => x as FrameworkElement;
                }
                input.With<object, FrameworkElement>(evaluator).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContext = model;
                });
            }
            return input;
        }

        private static Window CreateDefaultSplashScreenWindow(object parameter)
        {
            SplashScreenWindow window1 = new SplashScreenWindow();
            window1.WindowStyle = WindowStyle.None;
            window1.ResizeMode = ResizeMode.NoResize;
            window1.AllowsTransparency = true;
            window1.Background = new SolidColorBrush(Colors.Transparent);
            window1.ShowInTaskbar = false;
            window1.Topmost = true;
            window1.SizeToContent = SizeToContent.WidthAndHeight;
            window1.WindowStartupLocation = SplashScreenHelper.FindParameter<WindowStartupLocation>(parameter, WindowStartupLocation.CenterScreen);
            return window1;
        }

        public static Type GetSplashScreenType(Window obj) => 
            (Type) obj.GetValue(SplashScreenTypeProperty);

        private static void NotifyIsActiveChanged()
        {
            bool isActive = IsActive;
            if (lastIsActive != isActive)
            {
                lastIsActive = isActive;
                foreach (ISplashScreenStateAware aware in WeakSplashScreenStateAwareContainer.Default.GetRegisteredServices())
                {
                    aware.OnIsActiveChanged(isActive);
                }
            }
        }

        private static void OnSplashScreenClosed(object container, EventArgs args)
        {
            NotifyIsActiveChanged();
        }

        internal static void OnSplashScreenTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(obj))
            {
                Window input = (Window) obj;
                Func<Window, SplashScreenOwner> evaluator = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Func<Window, SplashScreenOwner> local1 = <>c.<>9__34_0;
                    evaluator = <>c.<>9__34_0 = x => new SplashScreenOwner(x);
                }
                Show((Type) e.NewValue, WindowStartupLocation.CenterScreen, input.With<Window, SplashScreenOwner>(evaluator), SplashScreenClosingMode.Default);
                input.Dispatcher.BeginInvoke(new Action(DXSplashScreen.Close), DispatcherPriority.Loaded, new object[0]);
            }
        }

        public static void Progress(double value, double maxValue = 100.0)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Show SplashScreen before calling this method.");
            }
            SplashContainer.Progress(value, maxValue);
        }

        public static void SetSplashScreenType(Window obj, Type value)
        {
            obj.SetValue(SplashScreenTypeProperty, value);
        }

        public static void SetState(object state)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Show SplashScreen before calling this method.");
            }
            SplashContainer.SetState(state);
        }

        public static void Show<T>(WindowStartupLocation startupLocation = 1, SplashScreenOwner owner = null, SplashScreenClosingMode closingMode = 0)
        {
            Show(typeof(T), startupLocation, owner, closingMode);
        }

        public static void Show<T>(Action action, WindowStartupLocation startupLocation = 1, SplashScreenOwner owner = null, SplashScreenClosingMode closingMode = 0)
        {
            Show<T>(startupLocation, owner, closingMode);
            try
            {
                action();
            }
            finally
            {
                Close();
            }
        }

        public static void Show(Func<object, Window> windowCreator, Func<object, object> splashScreenCreator, object windowCreatorParameter, object splashScreenCreatorParameter)
        {
            if (IsActive)
            {
                throw new InvalidOperationException("SplashScreen has been displayed. Only one splash screen can be displayed simultaneously.");
            }
            if (SplashContainer == null)
            {
                SplashContainer = new SplashScreenContainer();
                SplashContainer.Closed += new EventHandler(DXSplashScreen.OnSplashScreenClosed);
            }
            Func<object, Window> defaultSplashScreenWindowCreator = windowCreator;
            if (windowCreator == null)
            {
                Func<object, Window> local1 = windowCreator;
                defaultSplashScreenWindowCreator = DefaultSplashScreenWindowCreator;
            }
            SplashContainer.Show(defaultSplashScreenWindowCreator, splashScreenCreator, windowCreatorParameter, splashScreenCreatorParameter, new Action(DXSplashScreen.NotifyIsActiveChanged));
        }

        public static void Show(Type splashScreenType, WindowStartupLocation startupLocation = 1, SplashScreenOwner owner = null, SplashScreenClosingMode closingMode = 0)
        {
            CheckSplashScreenType(splashScreenType);
            if (typeof(Window).IsAssignableFrom(splashScreenType))
            {
                object[] windowCreatorParameter = new object[] { splashScreenType, startupLocation, owner, closingMode };
                Show(<>c.<>9__38_0 ??= delegate (object p) {
                    Window window = (Window) Activator.CreateInstance(SplashScreenHelper.FindParameter<Type>(p, null));
                    window.WindowStartupLocation = SplashScreenHelper.FindParameter<WindowStartupLocation>(p, WindowStartupLocation.CenterScreen);
                    return window;
                }, null, windowCreatorParameter, null);
            }
            else if (typeof(FrameworkElement).IsAssignableFrom(splashScreenType))
            {
                object[] windowCreatorParameter = new object[] { startupLocation, owner, closingMode };
                object[] splashScreenCreatorParameter = new object[] { splashScreenType };
                Show(<>c.<>9__38_1 ??= delegate (object p) {
                    Window window = DefaultSplashScreenWindowCreator(p);
                    WindowFadeAnimationBehavior.SetEnableAnimation(window, true);
                    return window;
                }, new Func<object, object>(DXSplashScreen.CreateDefaultSplashScreen), windowCreatorParameter, splashScreenCreatorParameter);
            }
        }

        public static bool UseLegacyLocationLogic { get; set; }

        public static bool UseDefaultAltTabBehavior { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static bool DisableThreadingProblemsDetection { get; set; }

        public static DevExpress.Xpf.Core.NotInitializedStateMethodCallPolicy NotInitializedStateMethodCallPolicy { get; set; }

        public static DevExpress.Xpf.Core.UIThreadReleaseMode UIThreadReleaseMode { get; set; }

        public static int UIThreadDelay
        {
            get => 
                MainThreadDelay;
            set => 
                MainThreadDelay = value;
        }

        private static int MainThreadDelay { get; set; }

        public static bool IsActive =>
            (SplashContainer != null) && SplashContainer.IsActive;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<object, Window> DefaultSplashScreenWindowCreator =>
            new Func<object, Window>(DXSplashScreen.CreateDefaultSplashScreenWindow);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<object, object> DefaultSplashScreenContentCreator =>
            new Func<object, object>(DXSplashScreen.CreateDefaultSplashScreen);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXSplashScreen.<>c <>9 = new DXSplashScreen.<>c();
            public static Func<Window, SplashScreenOwner> <>9__34_0;
            public static Func<object, Window> <>9__38_0;
            public static Func<object, Window> <>9__38_1;
            public static Func<object, FrameworkElement> <>9__49_0;

            internal FrameworkElement <CreateDefaultSplashScreen>b__49_0(object x) => 
                x as FrameworkElement;

            internal SplashScreenOwner <OnSplashScreenTypeChanged>b__34_0(Window x) => 
                new SplashScreenOwner(x);

            internal Window <Show>b__38_0(object p)
            {
                Window window = (Window) Activator.CreateInstance(SplashScreenHelper.FindParameter<Type>(p, null));
                window.WindowStartupLocation = SplashScreenHelper.FindParameter<WindowStartupLocation>(p, WindowStartupLocation.CenterScreen);
                return window;
            }

            internal Window <Show>b__38_1(object p)
            {
                Window window = DXSplashScreen.DefaultSplashScreenWindowCreator(p);
                WindowFadeAnimationBehavior.SetEnableAnimation(window, true);
                return window;
            }
        }

        private class CloseCallback : DXSplashScreen.SplashScreenCallbackBase
        {
            public override void Execute(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                DXSplashScreen.SplashScreenContainer.CloseCore(info);
            }
        }

        internal interface ISplashScreenStateAware
        {
            void OnIsActiveChanged(bool newValue);
        }

        private class SetProgressCallback : DXSplashScreen.SplashScreenCallbackBase
        {
            private double progress;
            private double maxProgress;

            public SetProgressCallback(double progress, double maxProgress)
            {
                this.progress = progress;
                this.maxProgress = maxProgress;
            }

            public override void Execute(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                DXSplashScreen.SplashScreenContainer.SetProgressCore(info, this.progress, this.maxProgress);
            }
        }

        private class SetStateCallback : DXSplashScreen.SplashScreenCallbackBase
        {
            private object state;

            public SetStateCallback(object state)
            {
                this.state = state;
            }

            public override void Dispose()
            {
                this.state = null;
            }

            public override void Execute(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                DXSplashScreen.SplashScreenContainer.SetStateCore(info, this.state);
            }
        }

        private abstract class SplashScreenCallbackBase : IDisposable
        {
            protected SplashScreenCallbackBase()
            {
            }

            public virtual void Dispose()
            {
            }

            public abstract void Execute(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info);
        }

        internal class SplashScreenCallbacks : IDisposable
        {
            private IList<DXSplashScreen.SplashScreenCallbackBase> callbacks = new List<DXSplashScreen.SplashScreenCallbackBase>();
            private object syncRoot = new object();
            private DXSplashScreen.SplashScreenContainer.SplashScreenInfo info;
            private bool isInitialized;

            public SplashScreenCallbacks(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                this.info = info;
            }

            private bool AddCallbackCore(DXSplashScreen.SplashScreenCallbackBase callback)
            {
                bool flag = false;
                bool flag2 = false;
                object syncRoot = this.syncRoot;
                lock (syncRoot)
                {
                    if (this.isInitialized)
                    {
                        callback.Dispose();
                        flag2 = (this.callbacks.Count > 0) && (this.info.Dispatcher != null);
                    }
                    else if (this.Policy == NotInitializedStateMethodCallPolicy.CallWhenReady)
                    {
                        this.callbacks.Add(callback);
                        flag = true;
                    }
                    else
                    {
                        if (this.Policy != NotInitializedStateMethodCallPolicy.Discard)
                        {
                            throw new InvalidOperationException("The SplashScreen has not been initialized yet.");
                        }
                        callback.Dispose();
                        flag = true;
                    }
                }
                if (flag2)
                {
                    this.info.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.ExecuteExceptClose));
                }
                return flag;
            }

            public void Dispose()
            {
                foreach (DXSplashScreen.SplashScreenCallbackBase base2 in this.callbacks)
                {
                    base2.Dispose();
                }
                this.callbacks.Clear();
                this.info = null;
            }

            public void ExecuteClose()
            {
                if (this.HasCloseCallback && (this.callbacks.Count != 0))
                {
                    Dispatcher dispatcher = this.info.Dispatcher;
                    if (dispatcher != null)
                    {
                        object[] args = new object[] { this.info };
                        dispatcher.BeginInvoke(new Action<DXSplashScreen.SplashScreenContainer.SplashScreenInfo>(this.ExecuteCloseCore), DispatcherPriority.Loaded, args);
                    }
                }
            }

            private void ExecuteCloseCore(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                object syncRoot = this.syncRoot;
                lock (syncRoot)
                {
                    foreach (DXSplashScreen.SplashScreenCallbackBase base2 in this.callbacks)
                    {
                        base2.Execute(info);
                        base2.Dispose();
                    }
                    this.callbacks.Clear();
                }
            }

            public void ExecuteExceptClose()
            {
                object syncRoot = this.syncRoot;
                lock (syncRoot)
                {
                    while (this.callbacks.Count > 0)
                    {
                        DXSplashScreen.SplashScreenCallbackBase base2 = this.callbacks[0];
                        if (base2 is DXSplashScreen.CloseCallback)
                        {
                            break;
                        }
                        this.callbacks.RemoveAt(0);
                        base2.Execute(this.info);
                        base2.Dispose();
                    }
                }
            }

            public void Initialize()
            {
                object syncRoot = this.syncRoot;
                lock (syncRoot)
                {
                    this.isInitialized = true;
                }
            }

            public bool PushCloseCallback()
            {
                this.HasCloseCallback = true;
                return this.AddCallbackCore(new DXSplashScreen.CloseCallback());
            }

            public bool PushSetProgressCallback(double progress, double maxProgress) => 
                this.AddCallbackCore(new DXSplashScreen.SetProgressCallback(progress, maxProgress));

            public bool PushSetStateCallback(object state) => 
                this.AddCallbackCore(new DXSplashScreen.SetStateCallback(state));

            public bool PushSplashScreenMethodCallback<T>(Action<T> action) => 
                this.AddCallbackCore(new DXSplashScreen.SplashScreenMethodCallback<T>(action));

            public bool HasCloseCallback { get; private set; }

            public NotInitializedStateMethodCallPolicy Policy { get; set; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public class SplashScreenContainer
        {
            private static IList<DXSplashScreen.SplashScreenContainer> instances = new List<DXSplashScreen.SplashScreenContainer>();
            private static object instanceLocker = new object();
            internal EventHandler Closed;
            private static bool hasUnhandledExceptionSubscriver;
            internal volatile AutoResetEvent SyncEvent = new AutoResetEvent(false);
            internal SplashScreenInfo ActiveInfo = new SplashScreenInfo();
            private object internalLocker = new object();
            private IList<SplashScreenInfo> infosForRelease = new List<SplashScreenInfo>();

            public void CallSplashScreenMethod<T>(Action<T> action) where T: Window
            {
                if (!ViewModelBase.IsInDesignMode)
                {
                    if (!this.IsActive)
                    {
                        throw new InvalidOperationException("Show SplashScreen before calling this method.");
                    }
                    if (!this.ActiveInfo.Callbacks.PushSplashScreenMethodCallback<T>(action))
                    {
                        object[] args = new object[] { this.ActiveInfo.SplashScreen };
                        this.InvokeOnSplashScreenDispatcher(action, args);
                    }
                }
            }

            private void ChangeActiveContainer()
            {
                object internalLocker = this.internalLocker;
                lock (internalLocker)
                {
                    SplashScreenInfo activeInfo = this.ActiveInfo;
                    this.ActiveInfo = new SplashScreenInfo();
                    this.infosForRelease.Add(activeInfo);
                }
            }

            public void Close()
            {
                if (!ViewModelBase.IsInDesignMode)
                {
                    if (!this.IsActive)
                    {
                        throw new InvalidOperationException("Show SplashScreen before calling this method.");
                    }
                    DXSplashScreen.SplashScreenCallbacks callbacks = this.ActiveInfo.Callbacks;
                    if (callbacks != null)
                    {
                        if (!callbacks.PushCloseCallback())
                        {
                            object[] args = new object[] { this.ActiveInfo };
                            this.InvokeOnSplashScreenDispatcher(new Action<SplashScreenInfo>(DXSplashScreen.SplashScreenContainer.CloseCore), DispatcherPriority.Render, args);
                        }
                        this.ChangeActiveContainer();
                    }
                }
            }

            internal static void CloseCore(SplashScreenInfo info)
            {
                if (info.IsActive)
                {
                    if (info.SplashScreen is ISplashScreen)
                    {
                        ((ISplashScreen) info.SplashScreen).CloseSplashScreen();
                    }
                    else
                    {
                        info.SplashScreen.Close();
                    }
                }
            }

            private object GetOwnerContainer(object parameter)
            {
                Func<WindowContainer, WindowRelationInfo> evaluator = <>c.<>9__21_0;
                if (<>c.<>9__21_0 == null)
                {
                    Func<WindowContainer, WindowRelationInfo> local1 = <>c.<>9__21_0;
                    evaluator = <>c.<>9__21_0 = x => x.CreateOwnerContainer();
                }
                WindowRelationInfo local2 = SplashScreenHelper.FindParameter<WindowContainer>(parameter, null).Return<WindowContainer, WindowRelationInfo>(evaluator, null);
                WindowRelationInfo local4 = local2;
                if (local2 == null)
                {
                    WindowRelationInfo local3 = local2;
                    local4 = SplashScreenHelper.FindParameter<WindowRelationInfo>(parameter, null);
                }
                object obj2 = local4;
                if (obj2 != null)
                {
                    return obj2;
                }
                WindowStartupLocation startupLocation = SplashScreenHelper.FindParameter<WindowStartupLocation>(parameter, WindowStartupLocation.CenterScreen);
                return SplashScreenHelper.FindParameter<SplashScreenOwner>(parameter, null).With<SplashScreenOwner, WindowContainer>(x => x.CreateOwnerContainer(startupLocation));
            }

            private static SplashScreenViewModel GetViewModel(SplashScreenInfo info)
            {
                Func<FrameworkElement, SplashScreenViewModel> evaluator = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Func<FrameworkElement, SplashScreenViewModel> local1 = <>c.<>9__34_0;
                    evaluator = <>c.<>9__34_0 = x => x.DataContext as SplashScreenViewModel;
                }
                return (info.SplashScreen.Content as FrameworkElement).With<FrameworkElement, SplashScreenViewModel>(evaluator);
            }

            private void InternalThreadEntryPoint(object parameter)
            {
                bool local6;
                Func<object, Window> func = ((object[]) parameter)[0] as Func<object, Window>;
                Func<object, object> input = ((object[]) parameter)[1] as Func<object, object>;
                object arg = ((object[]) parameter)[2];
                object splashScreenCreatorParameter = ((object[]) parameter)[3];
                UIThreadReleaseMode? nullable = new UIThreadReleaseMode?(SplashScreenHelper.FindParameter<UIThreadReleaseMode>(parameter, DXSplashScreen.UIThreadReleaseMode));
                object syncRoot = ((ICollection) new Style().Resources).SyncRoot;
                SplashScreenInfo local1 = SplashScreenHelper.FindParameter<SplashScreenInfo>(parameter, null);
                SplashScreenInfo activeInfo = local1;
                if (local1 == null)
                {
                    SplashScreenInfo local2 = local1;
                    activeInfo = this.ActiveInfo;
                }
                SplashScreenInfo info = activeInfo;
                info.SplashScreen = func(arg);
                info.Dispatcher = info.SplashScreen.Dispatcher;
                info.Callbacks.Initialize();
                if (Monitor.TryEnter(syncRoot))
                {
                    Monitor.Exit(syncRoot);
                }
                else
                {
                    nullable = null;
                    this.SyncEvent.Set();
                }
                input.Do<Func<object, object>>(x => info.SplashScreen.Content = x(splashScreenCreatorParameter));
                SetProgressStateCore(info, true);
                info.InitializeOwner(parameter);
                this.SubscribeParentEvents(arg);
                if ((nullable != null) && (((UIThreadReleaseMode) nullable.Value) == UIThreadReleaseMode.WaitForSplashScreenInitialized))
                {
                    this.SyncEvent.Set();
                }
                info.Callbacks.ExecuteExceptClose();
                if (!info.CloseWithParent)
                {
                    local6 = false;
                }
                else
                {
                    Func<WindowRelationInfo, bool> evaluator = <>c.<>9__20_1;
                    if (<>c.<>9__20_1 == null)
                    {
                        Func<WindowRelationInfo, bool> local3 = <>c.<>9__20_1;
                        evaluator = <>c.<>9__20_1 = x => x.ActualIsParentClosed;
                    }
                    local6 = info.RelationInfo.Return<WindowRelationInfo, bool>(evaluator, <>c.<>9__20_2 ??= () => false);
                }
                bool flag = local6;
                bool flag2 = (nullable != null) && (((UIThreadReleaseMode) nullable.Value) == UIThreadReleaseMode.WaitForSplashScreenLoaded);
                if (flag)
                {
                    if (flag2)
                    {
                        this.SyncEvent.Set();
                    }
                }
                else
                {
                    if (flag2)
                    {
                        info.SplashScreen.Loaded += new RoutedEventHandler(this.OnSplashScreenLoaded);
                    }
                    this.PatchSplashScreenWindowStyle(info.SplashScreen, info.Owner != null);
                    info.Callbacks.ExecuteClose();
                    info.SplashScreen.ShowDialog();
                    if (flag2)
                    {
                        info.SplashScreen.Loaded -= new RoutedEventHandler(this.OnSplashScreenLoaded);
                    }
                    info.ActivateOwner();
                }
                this.ReleaseResources(info);
            }

            private void InvokeOnSplashScreenDispatcher(Delegate method, params object[] args)
            {
                this.InvokeOnSplashScreenDispatcher(method, DispatcherPriority.Normal, args);
            }

            private void InvokeOnSplashScreenDispatcher(Delegate method, DispatcherPriority priority, params object[] args)
            {
                if (this.ActiveInfo != null)
                {
                    Dispatcher dispatcher = this.ActiveInfo.Dispatcher;
                    if (dispatcher != null)
                    {
                        dispatcher.BeginInvoke(method, priority, args);
                    }
                }
            }

            private static void OnAppUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
            {
                DXSplashScreen.SplashScreenContainer[] containerArray;
                object instanceLocker = DXSplashScreen.SplashScreenContainer.instanceLocker;
                lock (instanceLocker)
                {
                    if (instances.Count != 0)
                    {
                        containerArray = new DXSplashScreen.SplashScreenContainer[instances.Count];
                        instances.CopyTo(containerArray, 0);
                    }
                    else
                    {
                        return;
                    }
                }
                foreach (DXSplashScreen.SplashScreenContainer container in containerArray)
                {
                    if (container.IsActive)
                    {
                        container.Close();
                    }
                }
            }

            private void OnSplashScreenLoaded(object sender, RoutedEventArgs e)
            {
                this.SyncEvent.Set();
            }

            private void OnSplashScreenOwnerClosed(object sender, EventArgs e)
            {
                if (this.IsActive)
                {
                    this.Close();
                }
            }

            private void OnSplashScreenSourceInitialized(object sender, EventArgs e)
            {
                Window splashScreen = (Window) sender;
                splashScreen.SourceInitialized -= new EventHandler(this.OnSplashScreenSourceInitialized);
                Func<SplashScreenInfo, bool> evaluator = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<SplashScreenInfo, bool> local1 = <>c.<>9__26_0;
                    evaluator = <>c.<>9__26_0 = x => x.Owner != null;
                }
                this.PatchSplashScreenWindowStyle(splashScreen, this.ActiveInfo.Return<SplashScreenInfo, bool>(evaluator, <>c.<>9__26_1 ??= () => false));
            }

            private void PatchSplashScreenWindowStyle(Window splashScreen, bool hasOwner)
            {
                if (!SplashScreenHelper.PatchWindowStyle(splashScreen, hasOwner))
                {
                    splashScreen.SourceInitialized += new EventHandler(this.OnSplashScreenSourceInitialized);
                }
            }

            public void Progress(double value, double maxValue)
            {
                if (!ViewModelBase.IsInDesignMode)
                {
                    if (!this.IsActive)
                    {
                        throw new InvalidOperationException("Show SplashScreen before calling this method.");
                    }
                    if (!this.ActiveInfo.Callbacks.PushSetProgressCallback(value, maxValue))
                    {
                        object[] args = new object[] { this.ActiveInfo, value, maxValue };
                        this.InvokeOnSplashScreenDispatcher(new Action<SplashScreenInfo, double, double>(DXSplashScreen.SplashScreenContainer.SetProgressCore), args);
                    }
                }
            }

            private void ReleaseResources(SplashScreenInfo container)
            {
                object instanceLocker = DXSplashScreen.SplashScreenContainer.instanceLocker;
                lock (instanceLocker)
                {
                    instances.Remove(this);
                    if ((instances.Count == 0) && hasUnhandledExceptionSubscriver)
                    {
                        SplashScreenHelper.InvokeAsync(Application.Current, new Action(this.UnsubscribeUnhandledException), DispatcherPriority.Send, AsyncInvokeMode.AsyncOnly);
                    }
                }
                Dispatcher dispatcher = Dispatcher.FromThread(container.InternalThread);
                container.RelationInfo.Do<WindowRelationInfo>(delegate (WindowRelationInfo x) {
                    x.ParentClosed -= new EventHandler(this.OnSplashScreenOwnerClosed);
                });
                container.ReleaseResources();
                if (this.Closed != null)
                {
                    this.Closed(this, EventArgs.Empty);
                }
                object internalLocker = this.internalLocker;
                lock (internalLocker)
                {
                    this.infosForRelease.Remove(container);
                }
                try
                {
                    if (dispatcher != null)
                    {
                        dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
                        Dispatcher.Run();
                    }
                }
                catch
                {
                }
            }

            internal static void SetProgressCore(SplashScreenInfo info, double progress, double maxProgress)
            {
                if (info.IsActive)
                {
                    if (!(info.SplashScreen is ISplashScreen))
                    {
                        GetViewModel(info).Do<SplashScreenViewModel>(delegate (SplashScreenViewModel x) {
                            x.Progress = progress;
                            x.MaxProgress = maxProgress;
                        });
                    }
                    else
                    {
                        ((ISplashScreen) info.SplashScreen).Progress(progress);
                        SetProgressStateCore(info, false);
                    }
                }
            }

            private static void SetProgressStateCore(SplashScreenInfo info, bool isIndeterminate)
            {
                if (info.SplashScreen is ISplashScreen)
                {
                    bool? nullable = info.IsIndeterminate;
                    if ((isIndeterminate == nullable.GetValueOrDefault()) ? (nullable == null) : true)
                    {
                        ((ISplashScreen) info.SplashScreen).SetProgressState(isIndeterminate);
                        info.IsIndeterminate = new bool?(isIndeterminate);
                    }
                }
            }

            public void SetState(object state)
            {
                if (!ViewModelBase.IsInDesignMode)
                {
                    if (!this.IsActive)
                    {
                        throw new InvalidOperationException("Show splash screen before calling this method.");
                    }
                    if (!this.ActiveInfo.Callbacks.PushSetStateCallback(state))
                    {
                        object[] args = new object[] { this.ActiveInfo, state };
                        this.InvokeOnSplashScreenDispatcher(new Action<SplashScreenInfo, object>(DXSplashScreen.SplashScreenContainer.SetStateCore), args);
                    }
                }
            }

            internal static void SetStateCore(SplashScreenInfo info, object state)
            {
                if (info.IsActive && !(info.SplashScreen is ISplashScreen))
                {
                    GetViewModel(info).Do<SplashScreenViewModel>(x => x.State = state);
                }
            }

            public void Show(Func<object, Window> windowCreator, Func<object, object> splashScreenCreator, object windowCreatorParameter, object splashScreenCreatorParameter, Action beforeStartThreadAction)
            {
                if (!ViewModelBase.IsInDesignMode)
                {
                    if (this.IsActive)
                    {
                        throw new InvalidOperationException("SplashScreen has been displayed. Only one splash screen can be displayed simultaneously.");
                    }
                    object instanceLocker = DXSplashScreen.SplashScreenContainer.instanceLocker;
                    lock (instanceLocker)
                    {
                        instances.Add(this);
                        if (!hasUnhandledExceptionSubscriver)
                        {
                            hasUnhandledExceptionSubscriver = true;
                            Action<Application> action = <>c.<>9__13_0;
                            if (<>c.<>9__13_0 == null)
                            {
                                Action<Application> local1 = <>c.<>9__13_0;
                                action = <>c.<>9__13_0 = delegate (Application x) {
                                    x.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(DXSplashScreen.SplashScreenContainer.OnAppUnhandledException);
                                };
                            }
                            Application.Current.Do<Application>(action);
                        }
                    }
                    this.ActiveInfo.EnsureCallbacksContainer();
                    this.ActiveInfo.InternalThread = new Thread(new ParameterizedThreadStart(this.InternalThreadEntryPoint));
                    this.ActiveInfo.InternalThread.SetApartmentState(ApartmentState.STA);
                    if (beforeStartThreadAction != null)
                    {
                        beforeStartThreadAction();
                    }
                    object[] parameter = new object[] { windowCreator, splashScreenCreator, windowCreatorParameter, splashScreenCreatorParameter, this.GetOwnerContainer(windowCreatorParameter), this.ActiveInfo };
                    this.ActiveInfo.InternalThread.Start(parameter);
                    if (DXSplashScreen.MainThreadDelay > 0)
                    {
                        this.SyncEvent.WaitOne(DXSplashScreen.MainThreadDelay);
                    }
                    else if (DXSplashScreen.MainThreadDelay < 0)
                    {
                        this.SyncEvent.WaitOne();
                    }
                }
            }

            private void SubscribeParentEvents(object parameter)
            {
                SplashScreenClosingMode mode = SplashScreenHelper.FindParameter<SplashScreenClosingMode>(parameter, SplashScreenClosingMode.Default);
                this.ActiveInfo.CloseWithParent = (mode == SplashScreenClosingMode.Default) || (mode == SplashScreenClosingMode.OnParentClosed);
                if (this.ActiveInfo.CloseWithParent)
                {
                    this.ActiveInfo.RelationInfo.Do<WindowRelationInfo>(delegate (WindowRelationInfo x) {
                        x.ParentClosed += new EventHandler(this.OnSplashScreenOwnerClosed);
                    });
                }
            }

            private void UnsubscribeUnhandledException()
            {
                object instanceLocker = DXSplashScreen.SplashScreenContainer.instanceLocker;
                lock (instanceLocker)
                {
                    if ((instances.Count == 0) && hasUnhandledExceptionSubscriver)
                    {
                        Action<Application> action = <>c.<>9__23_0;
                        if (<>c.<>9__23_0 == null)
                        {
                            Action<Application> local1 = <>c.<>9__23_0;
                            action = <>c.<>9__23_0 = delegate (Application x) {
                                x.DispatcherUnhandledException -= new DispatcherUnhandledExceptionEventHandler(DXSplashScreen.SplashScreenContainer.OnAppUnhandledException);
                            };
                        }
                        Application.Current.Do<Application>(action);
                        hasUnhandledExceptionSubscriver = false;
                    }
                }
            }

            public bool IsActive
            {
                get
                {
                    object internalLocker = this.internalLocker;
                    lock (internalLocker)
                    {
                        return this.ActiveInfo.IsActive;
                    }
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXSplashScreen.SplashScreenContainer.<>c <>9 = new DXSplashScreen.SplashScreenContainer.<>c();
                public static Action<Application> <>9__13_0;
                public static Func<WindowRelationInfo, bool> <>9__20_1;
                public static Func<bool> <>9__20_2;
                public static Func<WindowContainer, WindowRelationInfo> <>9__21_0;
                public static Action<Application> <>9__23_0;
                public static Func<DXSplashScreen.SplashScreenContainer.SplashScreenInfo, bool> <>9__26_0;
                public static Func<bool> <>9__26_1;
                public static Func<FrameworkElement, SplashScreenViewModel> <>9__34_0;

                internal WindowRelationInfo <GetOwnerContainer>b__21_0(WindowContainer x) => 
                    x.CreateOwnerContainer();

                internal SplashScreenViewModel <GetViewModel>b__34_0(FrameworkElement x) => 
                    x.DataContext as SplashScreenViewModel;

                internal bool <InternalThreadEntryPoint>b__20_1(WindowRelationInfo x) => 
                    x.ActualIsParentClosed;

                internal bool <InternalThreadEntryPoint>b__20_2() => 
                    false;

                internal bool <OnSplashScreenSourceInitialized>b__26_0(DXSplashScreen.SplashScreenContainer.SplashScreenInfo x) => 
                    x.Owner != null;

                internal bool <OnSplashScreenSourceInitialized>b__26_1() => 
                    false;

                internal void <Show>b__13_0(Application x)
                {
                    x.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(DXSplashScreen.SplashScreenContainer.OnAppUnhandledException);
                }

                internal void <UnsubscribeUnhandledException>b__23_0(Application x)
                {
                    x.DispatcherUnhandledException -= new DispatcherUnhandledExceptionEventHandler(DXSplashScreen.SplashScreenContainer.OnAppUnhandledException);
                }
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public class SplashScreenInfo
            {
                internal volatile Window SplashScreen;
                internal volatile Thread InternalThread;
                internal volatile WindowRelationInfo RelationInfo;
                internal volatile WindowContainer Owner;
                internal DXSplashScreen.SplashScreenCallbacks Callbacks;
                internal bool? IsIndeterminate;
                internal bool CloseWithParent;
                internal volatile System.Windows.Threading.Dispatcher Dispatcher;

                internal void ActivateOwner()
                {
                    if (this.SplashScreen.IsActive)
                    {
                        Action<WindowContainer> action = <>c.<>9__11_0;
                        if (<>c.<>9__11_0 == null)
                        {
                            Action<WindowContainer> local1 = <>c.<>9__11_0;
                            action = <>c.<>9__11_0 = x => x.ActivateWindow();
                        }
                        this.Owner.Do<WindowContainer>(action);
                    }
                }

                public void EnsureCallbacksContainer()
                {
                    this.Callbacks ??= new DXSplashScreen.SplashScreenCallbacks(this);
                    this.Callbacks.Policy = DXSplashScreen.NotInitializedStateMethodCallPolicy;
                }

                internal void InitializeOwner(object parameter)
                {
                    Func<WindowContainer, WindowRelationInfo> evaluator = <>c.<>9__13_0;
                    if (<>c.<>9__13_0 == null)
                    {
                        Func<WindowContainer, WindowRelationInfo> local1 = <>c.<>9__13_0;
                        evaluator = <>c.<>9__13_0 = x => x.CreateOwnerContainer();
                    }
                    WindowRelationInfo local2 = SplashScreenHelper.FindParameter<WindowContainer>(parameter, null).Return<WindowContainer, WindowRelationInfo>(evaluator, null);
                    WindowRelationInfo local5 = local2;
                    if (local2 == null)
                    {
                        WindowRelationInfo local3 = local2;
                        local5 = SplashScreenHelper.FindParameter<WindowRelationInfo>(parameter, null);
                    }
                    this.RelationInfo = local5;
                    this.Owner = this.RelationInfo.With<WindowRelationInfo, WindowContainer>(<>c.<>9__13_1 ??= x => x.Parent);
                    this.RelationInfo.Do<WindowRelationInfo>(x => x.AttachChild(this.SplashScreen, false));
                }

                internal void ReleaseResources()
                {
                    this.Callbacks.Dispose();
                    this.Callbacks = null;
                    this.Owner = null;
                    this.Dispatcher = null;
                    Action<WindowRelationInfo> action = <>c.<>9__12_0;
                    if (<>c.<>9__12_0 == null)
                    {
                        Action<WindowRelationInfo> local1 = <>c.<>9__12_0;
                        action = <>c.<>9__12_0 = x => x.Release();
                    }
                    this.RelationInfo.Do<WindowRelationInfo>(action);
                    this.RelationInfo = null;
                    this.SplashScreen.Content = null;
                    this.SplashScreen = null;
                    this.InternalThread = null;
                }

                public bool IsActive =>
                    this.InternalThread != null;

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly DXSplashScreen.SplashScreenContainer.SplashScreenInfo.<>c <>9 = new DXSplashScreen.SplashScreenContainer.SplashScreenInfo.<>c();
                    public static Action<WindowContainer> <>9__11_0;
                    public static Action<WindowRelationInfo> <>9__12_0;
                    public static Func<WindowContainer, WindowRelationInfo> <>9__13_0;
                    public static Func<WindowRelationInfo, WindowContainer> <>9__13_1;

                    internal void <ActivateOwner>b__11_0(WindowContainer x)
                    {
                        x.ActivateWindow();
                    }

                    internal WindowRelationInfo <InitializeOwner>b__13_0(WindowContainer x) => 
                        x.CreateOwnerContainer();

                    internal WindowContainer <InitializeOwner>b__13_1(WindowRelationInfo x) => 
                        x.Parent;

                    internal void <ReleaseResources>b__12_0(WindowRelationInfo x)
                    {
                        x.Release();
                    }
                }
            }
        }

        private class SplashScreenMethodCallback<T> : DXSplashScreen.SplashScreenCallbackBase
        {
            private Action<T> callback;

            public SplashScreenMethodCallback(Action<T> callback)
            {
                this.callback = callback;
            }

            public override void Dispose()
            {
                this.callback = null;
            }

            public override void Execute(DXSplashScreen.SplashScreenContainer.SplashScreenInfo info)
            {
                object splashScreen = info.SplashScreen;
                this.callback((T) splashScreen);
            }
        }

        internal class SplashScreenWindow : Window
        {
            private const int WM_SYSCOMMAND = 0x112;
            private const int SC_CLOSE = 0xf060;

            public SplashScreenWindow()
            {
                base.ShowActivated = false;
                base.Loaded += new RoutedEventHandler(this.OnWindowLoaded);
            }

            protected override void OnClosed(EventArgs e)
            {
                if (this.Handle != IntPtr.Zero)
                {
                    HwndSource.FromHwnd(this.Handle).Do<HwndSource>(x => x.RemoveHook(new HwndSourceHook(this.WndProc)));
                }
            }

            protected override void OnClosing(CancelEventArgs e)
            {
                this.IsActiveOnClosing = base.IsActive;
                base.OnClosing(e);
            }

            protected override void OnSourceInitialized(EventArgs e)
            {
                base.OnSourceInitialized(e);
                if (SplashScreenHelper.HasAccess(this))
                {
                    this.Handle = new WindowInteropHelper(this).Handle;
                    HwndSource.FromHwnd(this.Handle).AddHook(new HwndSourceHook(this.WndProc));
                }
            }

            private void OnWindowLoaded(object sender, RoutedEventArgs e)
            {
                base.Loaded -= new RoutedEventHandler(this.OnWindowLoaded);
                if (base.SizeToContent != SizeToContent.Manual)
                {
                    SizeToContent sizeToContent = base.SizeToContent;
                    base.SizeToContent = SizeToContent.Manual;
                    base.SizeToContent = sizeToContent;
                }
            }

            private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                if ((msg == 0x112) && ((wParam.ToInt32() & 0xfff0) == 0xf060))
                {
                    handled = true;
                }
                return IntPtr.Zero;
            }

            public bool IsActiveOnClosing { get; private set; }

            protected IntPtr Handle { get; set; }
        }

        internal class WeakSplashScreenStateAwareContainer
        {
            private static DXSplashScreen.WeakSplashScreenStateAwareContainer instance;
            private List<WeakReference> services = new List<WeakReference>();

            private WeakSplashScreenStateAwareContainer()
            {
            }

            public IList<DXSplashScreen.ISplashScreenStateAware> GetRegisteredServices()
            {
                List<WeakReference> list = new List<WeakReference>();
                List<DXSplashScreen.ISplashScreenStateAware> list2 = new List<DXSplashScreen.ISplashScreenStateAware>();
                foreach (WeakReference reference in this.services)
                {
                    DXSplashScreen.ISplashScreenStateAware target = reference.Target as DXSplashScreen.ISplashScreenStateAware;
                    if (target == null)
                    {
                        list.Add(reference);
                        continue;
                    }
                    list2.Add(target);
                }
                list.ForEach(delegate (WeakReference x) {
                    this.services.Remove(x);
                });
                return list2;
            }

            public void Register(DXSplashScreen.ISplashScreenStateAware service)
            {
                this.services.Add(new WeakReference(service));
            }

            public void Unregister(DXSplashScreen.ISplashScreenStateAware service)
            {
                WeakReference item = this.services.FirstOrDefault<WeakReference>(x => x.Target == service);
                if (item != null)
                {
                    this.services.Remove(item);
                }
            }

            internal static DXSplashScreen.WeakSplashScreenStateAwareContainer Default =>
                instance ??= new DXSplashScreen.WeakSplashScreenStateAwareContainer();
        }
    }
}

