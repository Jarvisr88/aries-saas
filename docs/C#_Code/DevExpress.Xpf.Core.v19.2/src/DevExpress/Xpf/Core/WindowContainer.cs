namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    internal class WindowContainer
    {
        private bool isHandleRequired;

        public event EventHandler Initialized;

        public WindowContainer(DependencyObject windowObject) : this(windowObject, true)
        {
        }

        public WindowContainer(DependencyObject windowObject, bool isHandleRequired)
        {
            if (windowObject == null)
            {
                throw new ArgumentNullException("WindowObject");
            }
            this.isHandleRequired = isHandleRequired;
            this.WindowObject = windowObject;
            this.FrameworkObject = this.WindowObject as FrameworkElement;
            this.Initialize();
        }

        public void ActivateWindow()
        {
            SplashScreenHelper.InvokeAsync(this, new Action(this.ActivateWindowCore), DispatcherPriority.Send, AsyncInvokeMode.AllowSyncInvoke);
        }

        private void ActivateWindowCore()
        {
            if ((this.Window != null) && (!this.Window.IsActive && (this.Window.IsVisible && !SplashScreenHelper.ApplicationHasActiveWindow())))
            {
                this.Window.Activate();
            }
        }

        private void CompleteInitialization()
        {
            this.CompleteInitializationOverride();
            this.IsInitialized = true;
            this.Initialized.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected virtual void CompleteInitializationOverride()
        {
        }

        public virtual WindowRelationInfo CreateOwnerContainer() => 
            new WindowRelationInfo(this);

        private bool EnsureWindowHandle(out IntPtr handle)
        {
            handle = IntPtr.Zero;
            WindowInteropHelper helper = new WindowInteropHelper(this.Window);
            if (helper.Handle == IntPtr.Zero)
            {
                if ((this.Window is DXWindow) || (this.Window is ThemedWindow))
                {
                    this.Window.SourceInitialized += new EventHandler(this.OnWindowSourceInitialized);
                    return false;
                }
                try
                {
                    helper.EnsureHandle();
                }
                catch (InvalidOperationException)
                {
                    this.IsWindowClosedBeforeInit = true;
                    return false;
                }
            }
            handle = helper.Handle;
            return true;
        }

        private System.Windows.Window FindOwner()
        {
            System.Windows.Window windowObject = this.WindowObject as System.Windows.Window;
            if (windowObject == null)
            {
                bool preferVisualTreeForOwnerSearch = SplashScreenOwner.GetPreferVisualTreeForOwnerSearch(this.WindowObject);
                if (preferVisualTreeForOwnerSearch)
                {
                    windowObject = LayoutHelper.FindParentObject<System.Windows.Window>(this.WindowObject);
                }
                windowObject ??= System.Windows.Window.GetWindow(this.WindowObject);
                if (!preferVisualTreeForOwnerSearch && ((windowObject != null) && SplashScreenOwner.GetPreferVisualTreeForOwnerSearch(windowObject)))
                {
                    preferVisualTreeForOwnerSearch = true;
                    System.Windows.Window window2 = LayoutHelper.FindParentObject<System.Windows.Window>(this.WindowObject);
                    if (window2 != null)
                    {
                        windowObject = window2;
                    }
                }
            }
            return windowObject;
        }

        private void Initialize()
        {
            this.TryInitializeWindow();
            if (!this.IsInitialized && (this.Window == null))
            {
                Func<FrameworkElement, bool> evaluator = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__39_0;
                    evaluator = <>c.<>9__39_0 = x => x.IsLoaded;
                }
                if (!this.FrameworkObject.Return<FrameworkElement, bool>(evaluator, (<>c.<>9__39_1 ??= () => true)))
                {
                    this.FrameworkObject.Loaded += new RoutedEventHandler(this.OnControlLoaded);
                }
                else
                {
                    this.TryInitializeWindowForm();
                }
            }
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            this.FrameworkObject.Loaded -= new RoutedEventHandler(this.OnControlLoaded);
            this.Initialize();
        }

        private void OnWindowSourceInitialized(object sender, EventArgs e)
        {
            (sender as System.Windows.Window).SourceInitialized -= new EventHandler(this.OnWindowSourceInitialized);
            this.Initialize();
        }

        private void TryInitializeWindow()
        {
            if (!this.IsInitialized && !this.IsWindowClosedBeforeInit)
            {
                this.Window = this.FindOwner();
                if (this.Window != null)
                {
                    if (!this.isHandleRequired)
                    {
                        if (this.Window.Dispatcher != null)
                        {
                            this.ManagedThreadId = this.Window.Dispatcher.Thread.ManagedThreadId;
                            this.CompleteInitialization();
                        }
                    }
                    else
                    {
                        IntPtr ptr;
                        if (this.EnsureWindowHandle(out ptr))
                        {
                            this.Handle = ptr;
                            this.ManagedThreadId = this.Window.Dispatcher.Thread.ManagedThreadId;
                            this.CompleteInitialization();
                        }
                    }
                }
            }
        }

        private void TryInitializeWindowForm()
        {
            if (!this.IsInitialized)
            {
                Func<Visual, HwndSource> evaluator = <>c.<>9__40_0;
                if (<>c.<>9__40_0 == null)
                {
                    Func<Visual, HwndSource> local1 = <>c.<>9__40_0;
                    evaluator = <>c.<>9__40_0 = x => PresentationSource.FromVisual(x) as HwndSource;
                }
                HwndSource source = (this.WindowObject as Visual).With<Visual, HwndSource>(evaluator);
                if ((source != null) && (source.Handle != IntPtr.Zero))
                {
                    Func<Control, System.Windows.Forms.Form> func2 = <>c.<>9__40_1;
                    if (<>c.<>9__40_1 == null)
                    {
                        Func<Control, System.Windows.Forms.Form> local2 = <>c.<>9__40_1;
                        func2 = <>c.<>9__40_1 = x => x.FindForm();
                    }
                    this.Form = Control.FromChildHandle(source.Handle).With<Control, System.Windows.Forms.Form>(func2);
                    if (this.Form != null)
                    {
                        this.Handle = this.Form.Handle;
                        Func<int> method = <>c.<>9__40_2;
                        if (<>c.<>9__40_2 == null)
                        {
                            Func<int> local3 = <>c.<>9__40_2;
                            method = <>c.<>9__40_2 = () => Thread.CurrentThread.ManagedThreadId;
                        }
                        this.ManagedThreadId = (int) this.Form.Invoke(method);
                        this.CompleteInitialization();
                    }
                }
            }
        }

        public DependencyObject WindowObject { get; private set; }

        public System.Windows.Window Window { get; private set; }

        public System.Windows.Forms.Form Form { get; private set; }

        public IntPtr Handle { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool IsWindowClosedBeforeInit { get; private set; }

        public int ManagedThreadId { get; private set; }

        protected FrameworkElement FrameworkObject { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowContainer.<>c <>9 = new WindowContainer.<>c();
            public static Func<FrameworkElement, bool> <>9__39_0;
            public static Func<bool> <>9__39_1;
            public static Func<Visual, HwndSource> <>9__40_0;
            public static Func<Control, Form> <>9__40_1;
            public static Func<int> <>9__40_2;

            internal bool <Initialize>b__39_0(FrameworkElement x) => 
                x.IsLoaded;

            internal bool <Initialize>b__39_1() => 
                true;

            internal HwndSource <TryInitializeWindowForm>b__40_0(Visual x) => 
                PresentationSource.FromVisual(x) as HwndSource;

            internal Form <TryInitializeWindowForm>b__40_1(Control x) => 
                x.FindForm();

            internal int <TryInitializeWindowForm>b__40_2() => 
                Thread.CurrentThread.ManagedThreadId;
        }
    }
}

