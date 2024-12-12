namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Platform.Shell;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class FloatingPaneWindow : WindowContentHolder, IDisposable, IAdornerWindowClient, IFloatingPane, IDraggableWindow
    {
        public static readonly DependencyProperty InheritOwnerCommandBindingsProperty;
        public static readonly DependencyProperty InheritOwnerInputBindingsProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CanMaximizeProperty = DependencyProperty.Register("CanMaximize", typeof(bool), typeof(FloatingPaneWindow), new PropertyMetadata(true, new PropertyChangedCallback(FloatingPaneWindow.OnCanMaximizeChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CanMinimizeProperty = DependencyProperty.Register("CanMinimize", typeof(bool), typeof(FloatingPaneWindow), new PropertyMetadata(true, new PropertyChangedCallback(FloatingPaneWindow.OnCanMinimizeChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FloatingMaxSizeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FloatingMinSizeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty AllowAeroSnapProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty FloatStateProperty;
        public static readonly DependencyProperty OverlapTaskbarProperty;
        internal bool hiddenInitialization;
        private readonly List<KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>> _messageTable;
        private readonly Locker changeWindowStateLocker;
        private readonly DevExpress.Xpf.Docking.LockHelper sizeChangedLockHelper;
        private readonly Locker transformLocker;
        private readonly Locker TryCheckRelativeLocationAsyncLocker;
        private WindowState _previousWindowState;
        private Rect _restoreBounds;
        private Window bindingsOwner;
        private DispatcherOperation changeFloatStateOperation;
        private bool correctBoundsRequested;
        private WeakReference dragWidgetRef;
        private DispatcherOperation ensureRelativeSizeOperation;
        private bool fBoundsChangeRequested;
        private bool fFirstRun;
        private bool firstCheck;
        private FloatingWindowLock floatingWindowState;
        private bool isDisposingCore;
        private bool isDragging;
        private bool isPerMonitorDpiAware;
        private Rect lastFloatingBounds;
        private Rect lastRestoreBounds;
        private int lockCorrectBounds;
        private Window OwnerWindow;
        private Rect savedBounds;
        private FrameworkElement savedOwner;
        private bool suspendDragging;
        private MatrixTransform transform;
        private DispatcherOperation tryCheckRelativeLocationAsyncOperation;
        private DispatcherOperation tryCorrectBoundsAsyncOperation;
        private DispatcherOperation updateOwnerBindingsOperation;

        static FloatingPaneWindow()
        {
            Size defaultValue = new Size();
            FloatingMaxSizeProperty = DependencyProperty.Register("FloatingMaxSize", typeof(Size), typeof(FloatingPaneWindow), new PropertyMetadata(defaultValue, new PropertyChangedCallback(FloatingPaneWindow.OnFloatingMaxSizeChanged)));
            defaultValue = new Size();
            FloatingMinSizeProperty = DependencyProperty.Register("FloatingMinSize", typeof(Size), typeof(FloatingPaneWindow), new PropertyMetadata(defaultValue, new PropertyChangedCallback(FloatingPaneWindow.OnFloatingMinSizeChanged)));
            AllowAeroSnapProperty = DependencyProperty.Register("AllowAeroSnap", typeof(bool), typeof(FloatingPaneWindow), new PropertyMetadata(false, new PropertyChangedCallback(FloatingPaneWindow.OnAllowAeroSnapChanged)));
            FloatStateProperty = DependencyProperty.Register("FloatState", typeof(DevExpress.Xpf.Docking.FloatState), typeof(FloatingPaneWindow), new PropertyMetadata(DevExpress.Xpf.Docking.FloatState.Normal, new PropertyChangedCallback(FloatingPaneWindow.OnFloatStateChanged)));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(FloatingPaneWindow), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FloatingPaneWindow> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FloatingPaneWindow>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<FloatingPaneWindow, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FloatingPaneWindow.get_InheritOwnerCommandBindings)), parameters), out InheritOwnerCommandBindingsProperty, true, d => d.OnInheritOwnerCommandBindingsChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(FloatingPaneWindow), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<FloatingPaneWindow> registrator2 = registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<FloatingPaneWindow, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FloatingPaneWindow.get_InheritOwnerInputBindings)), expressionArray2), out InheritOwnerInputBindingsProperty, true, d => d.OnInheritOwnerInputBindingsChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(FloatingPaneWindow), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<FloatingPaneWindow, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FloatingPaneWindow.get_OverlapTaskbar)), expressionArray3), out OverlapTaskbarProperty, false, (d, oldValue, newValue) => d.OverlapTaskbarChanged(newValue), frameworkOptions);
            Window.WindowStateProperty.AddOwner(typeof(FloatingPaneWindow), new FrameworkPropertyMetadata(WindowState.Normal, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(FloatingPaneWindow.OnWindowStateChanged)));
        }

        public FloatingPaneWindow(BaseFloatingContainer container) : base(container)
        {
            this.changeWindowStateLocker = new Locker();
            this.sizeChangedLockHelper = new DevExpress.Xpf.Docking.LockHelper();
            this.transformLocker = new Locker();
            this.TryCheckRelativeLocationAsyncLocker = new Locker();
            this.fFirstRun = true;
            this.firstCheck = true;
            this.lastRestoreBounds = Rect.Empty;
            List<KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>> list1 = new List<KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>>();
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_CAPTURECHANGED, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmCaptureChanged)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_LBUTTONUP, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmLButtonUp)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_EXITSIZEMOVE, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmExitSizeMove)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_SIZE, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmSize)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_SIZING, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmSizing)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_MOVING, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmMoving)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_GETMINMAXINFO, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmGetMinMaxInfo)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_SHOWWINDOW, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmShowWindow)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_SETFOCUS, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmSetFocus)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_NCHITTEST, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmNcHitTest)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_WINDOWPOSCHANGING, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmWindowPosChanging)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_SYSCOMMAND, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmSysCommand)));
            list1.Add(new KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>(WM.WM_INITMENUPOPUP, new DevExpress.Xpf.Docking.Platform.Shell.MessageHandler(this.OnWmInitMenuPopup)));
            this._messageTable = list1;
            this.FloatGroup = DockLayoutManager.GetLayoutItem(container) as DevExpress.Xpf.Docking.FloatGroup;
            this.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.FloatGroup.WindowStyle);
            this.hiddenInitialization = true;
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Manager = DockLayoutManager.GetDockLayoutManager(container);
            this.Manager.SizeChanged += new SizeChangedEventHandler(this.OnManagerSizeChanged);
            WindowHelper.BindFlowDirection(this, this.Manager);
            base.AllowsTransparency = !this.Manager.IsTransparencyDisabled;
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Activated += new EventHandler(this.OnActivated);
            base.LocationChanged += new EventHandler(this.OnLocationChanged);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.PreviewGotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnPreviewGotKeyboardFocus);
            Binding binding = new Binding("AllowAeroSnap");
            binding.Source = this.Manager;
            base.SetBinding(AllowAeroSnapProperty, binding);
            Binding binding2 = new Binding("ActualMaxSize");
            binding2.Source = this.FloatGroup;
            base.SetBinding(FloatingMaxSizeProperty, binding2);
            Binding binding3 = new Binding("ActualMinSize");
            binding3.Source = this.FloatGroup;
            base.SetBinding(FloatingMinSizeProperty, binding3);
            Binding binding4 = new Binding("CanMaximize");
            binding4.Source = this.FloatGroup;
            base.SetBinding(CanMaximizeProperty, binding4);
            Binding binding5 = new Binding("CanMinimize");
            binding5.Source = this.FloatGroup;
            base.SetBinding(CanMinimizeProperty, binding5);
            Binding binding6 = new Binding("FloatState");
            binding6.Source = this.FloatGroup;
            base.SetBinding(FloatStateProperty, binding6);
            Binding binding7 = new Binding();
            binding7.Source = this.FloatGroup;
            binding7.Path = new PropertyPath(DevExpress.Xpf.Docking.FloatGroup.ActualWindowTaskbarTitleProperty);
            base.SetBinding(Window.TitleProperty, binding7);
            Binding binding8 = new Binding();
            binding8.Source = this.FloatGroup;
            binding8.Path = new PropertyPath(DevExpress.Xpf.Docking.FloatGroup.ActualWindowTaskbarIconProperty);
            base.SetBinding(Window.IconProperty, binding8);
            this.FloatGroup.LayoutChanged += new EventHandler(this.OnFloatGroupLayoutChanged);
            this.FloatGroup.Forward(this, Window.SizeToContentProperty, "SizeToContent", BindingMode.TwoWay);
            this.FloatGroup.Forward(this, UIElement.IsEnabledProperty, "IsEnabled", BindingMode.OneWay);
            if (this.FloatGroup.ScreenLocationBeforeClose != null)
            {
                Point point = (this.FloatState == DevExpress.Xpf.Docking.FloatState.Maximized) ? ScreenHelper.GetScreenRect(this.FloatGroup.ScreenLocationBeforeClose.Value).Location : this.FloatGroup.ScreenLocationBeforeClose.Value;
                base.Left = point.X;
                base.Top = point.Y;
            }
            DevExpress.Xpf.Docking.Platform.Shell.WindowChrome.SetWindowChrome(this, new DevExpress.Xpf.Docking.Platform.Shell.WindowChrome());
            this.isPerMonitorDpiAware = DevExpress.Xpf.Docking.Platform.Win32.DpiHelper.IsPerMonitorDpiAware;
            if (CompatibilitySettings.MakeGetWindowReturnActualFloatPanelWindow)
            {
                DevExpress.Xpf.Docking.WindowServiceHelper.SetIWindowService(this.FloatGroup, this);
                Window target = GetWindow(this.Manager);
                if (target != null)
                {
                    Window window2 = DevExpress.Xpf.Docking.WindowServiceHelper.GetRootWindow(target) ?? target;
                    DevExpress.Xpf.Docking.WindowServiceHelper.SetRootWindow(this, window2);
                }
            }
        }

        private void ChangeFloatStateCore(DevExpress.Xpf.Docking.FloatState state)
        {
            this.EnsureWindowState(state);
            this.changeFloatStateOperation = null;
        }

        private void ChangeWindowState(DevExpress.Xpf.Docking.FloatState state)
        {
            this.changeWindowStateLocker.Unlock();
            if (!this.changeWindowStateLocker)
            {
                this.ChangeWindowStateCore(state);
            }
        }

        private void ChangeWindowStateCore(DevExpress.Xpf.Docking.FloatState state)
        {
            if (!this.isDisposingCore)
            {
                if ((state == DevExpress.Xpf.Docking.FloatState.Maximized) && (base.WindowState != WindowState.Maximized))
                {
                    this.lastRestoreBounds = (this.changeFloatStateOperation == null) ? this.GetRestoreBounds() : Rect.Empty;
                    base.WindowState = WindowState.Maximized;
                }
                if ((state == DevExpress.Xpf.Docking.FloatState.Normal) && (base.WindowState != WindowState.Normal))
                {
                    this.lastRestoreBounds = this.lastFloatingBounds;
                    this.LockHelper.LockOnce(FloatingWindowLock.LockerKey.Restore);
                    base.WindowState = WindowState.Normal;
                }
                if ((state == DevExpress.Xpf.Docking.FloatState.Minimized) && (base.WindowState != WindowState.Minimized))
                {
                    this.lastRestoreBounds = this.GetRestoreBounds();
                    base.WindowState = WindowState.Minimized;
                }
            }
        }

        private static bool CheckClosedKeyGesture() => 
            Keyboard.IsKeyDown(Key.F4) && Keyboard2.IsAltPressed;

        private static bool CheckEscapeKeyGesture() => 
            Keyboard.IsKeyDown(Key.Escape);

        private void CheckTransform()
        {
            FrameworkElement ownerWindow = this.OwnerWindow;
            if ((ownerWindow == null) || !this.Manager.IsDescendantOf(ownerWindow))
            {
                ownerWindow = LayoutHelper.GetTopLevelVisual(this.Manager);
            }
            if (ownerWindow != null)
            {
                this.transform = this.Manager.TransformToVisual(ownerWindow) as MatrixTransform;
                if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
                {
                    Matrix matrix = this.transform.Matrix;
                    this.transform = new MatrixTransform(Math.Abs(matrix.M11), matrix.M12, matrix.M21, Math.Abs(matrix.M22), 0.0, 0.0);
                    if (!ReferenceEquals(this.transform, ((FrameworkElement) base.Content).LayoutTransform))
                    {
                        ((FrameworkElement) base.Content).LayoutTransform = this.transform;
                        if (!this.transform.Matrix.IsIdentity)
                        {
                            this.transformLocker.LockOnce();
                        }
                    }
                }
            }
        }

        private void CorrectBoundsAction(FrameworkElement owner, Rect bounds)
        {
            if ((this.lockCorrectBounds <= 0) && this.correctBoundsRequested)
            {
                try
                {
                    this.lockCorrectBounds++;
                    this.correctBoundsRequested = false;
                    base.CorrectBoundsCore(owner, bounds);
                }
                finally
                {
                    this.lockCorrectBounds--;
                }
            }
        }

        private void CorrectRestoreBounds()
        {
            Rect lastRestoreBounds = this.lastRestoreBounds;
            if (!this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.RestoreBounds) && (lastRestoreBounds != Rect.Empty))
            {
                using (this.LockHelper.Lock(FloatingWindowLock.LockerKey.RestoreBounds))
                {
                    base.Left = lastRestoreBounds.Left;
                    base.Top = lastRestoreBounds.Top;
                    base.Width = lastRestoreBounds.Width;
                    base.Height = lastRestoreBounds.Height;
                    this.lastRestoreBounds = Rect.Empty;
                }
            }
        }

        private MultiBinding CreateShowInTaskBarBinding()
        {
            MultiBinding binding1 = new MultiBinding();
            binding1.Converter = new ShowInTaskBarConverter();
            MultiBinding binding = binding1;
            Binding item = new Binding(DockLayoutManager.ShowFloatWindowsInTaskbarProperty.Name);
            item.Source = this.Manager;
            binding.Bindings.Add(item);
            Binding binding3 = new Binding(DevExpress.Xpf.Docking.FloatGroup.IsActuallyVisibleProperty.Name);
            binding3.Source = this.FloatGroup;
            binding.Bindings.Add(binding3);
            return binding;
        }

        internal void DisableSizeToContent()
        {
            if (this.IsAutoSize)
            {
                base.SizeToContent = SizeToContent.Manual;
            }
        }

        private void DoMoving()
        {
            this.Win32DragService.DoDragging();
        }

        private void DoSizing()
        {
            if (this.Win32DragService.IsResizing)
            {
                this.Win32DragService.DoSizing();
                this.EnsureRelativeSize();
            }
        }

        private bool EnqueueUpdateBounds(Rect bounds)
        {
            DevExpress.Xpf.Docking.LockHelper.LockHelperDelegate action = delegate {
                this.UpdateBoundsNative(bounds);
            };
            DevExpress.Xpf.Docking.LockHelper themeChangingLocker = this.LockHelper.GetLocker(FloatingWindowLock.LockerKey.ThemeChanging);
            if (themeChangingLocker)
            {
                themeChangingLocker.Reset();
            }
            if (this.Manager.IsThemeChangedLocked)
            {
                themeChangingLocker.Lock();
                themeChangingLocker.DoWhenUnlocked(action);
                base.Dispatcher.BeginInvoke(delegate {
                    themeChangingLocker.Unlock();
                }, DispatcherPriority.Normal, new object[0]);
                return true;
            }
            DevExpress.Xpf.Docking.LockHelper locker = this.LockHelper.GetLocker(FloatingWindowLock.LockerKey.CheckFloatBounds);
            if (locker)
            {
                locker.Reset();
            }
            if (!locker)
            {
                return false;
            }
            locker.DoWhenUnlocked(action);
            return true;
        }

        private void EnsureRelativeLocation()
        {
            Rect rect = this.TransformToRelativeBounds(this.WindowRect);
            using (this.LockHelper.Lock(FloatingWindowLock.LockerKey.FloatingBounds))
            {
                this.FloatGroup.FloatLocation = rect.Location;
            }
        }

        protected override void EnsureRelativeLocationCore(Point floatLocation)
        {
            if (floatLocation != PointHelper.Empty)
            {
                this.FloatGroup.EnsureFloatLocation(floatLocation);
            }
        }

        private void EnsureRelativeSize()
        {
            if ((this.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized) && (base.WindowState != WindowState.Maximized))
            {
                Size size = this.TranslateWindowSizeToFloatSize();
                using (this.LockHelper.Lock(FloatingWindowLock.LockerKey.FloatingBounds))
                {
                    this.FloatGroup.FloatSize = size;
                }
            }
        }

        private void EnsureRelativeSizeWithTransform()
        {
            this.transformLocker.Unlock();
            this.EnsureRelativeSize();
        }

        private void EnsureWindowState(DevExpress.Xpf.Docking.FloatState state)
        {
            this.changeWindowStateLocker.Lock();
            new Action<DevExpress.Xpf.Docking.FloatState>(this.ChangeWindowState)(state);
        }

        private void FinishDragging()
        {
            if (this.Win32DragService.IsResizing)
            {
                this.EnsureRelativeSize();
            }
            this.Win32DragService.FinishDragging();
        }

        protected object GetField(string fieldName)
        {
            Type type = typeof(Window);
            return this.GetField(fieldName, type);
        }

        protected object GetField(string fieldName, Type type) => 
            type.GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(this);

        [SecuritySafeCritical]
        protected HwndTarget GetHwndTarget()
        {
            PresentationSource source = PresentationSource.FromVisual(this);
            return ((source == null) ? null : (source.CompositionTarget as HwndTarget));
        }

        private static int GetInt(IntPtr ptr) => 
            (IntPtr.Size == 8) ? ((int) ptr.ToInt64()) : ptr.ToInt32();

        protected override unsafe Point GetRelativeLocation(FrameworkElement owner)
        {
            Point point1;
            PresentationSource source = PresentationSource.FromDependencyObject(owner);
            PresentationSource source2 = PresentationSource.FromDependencyObject(this);
            if ((source == null) || (source.CompositionTarget == null))
            {
                return base.GetRelativeLocation(owner);
            }
            if ((base.WindowState != WindowState.Maximized) || (source2 == null))
            {
                point1 = new Point(base.Left, base.Top);
            }
            else
            {
                Point point4 = new Point();
                point1 = base.PointToScreen(point4);
            }
            Point point = source.CompositionTarget.TransformToDevice.Transform(point1);
            if (double.IsNaN(point.X) || double.IsNaN(point.Y))
            {
                return PointHelper.Empty;
            }
            Point point3 = owner.PointFromScreen(point);
            if (base.FlowDirection == FlowDirection.RightToLeft)
            {
                Point* pointPtr1 = &point3;
                pointPtr1.X -= base.ActualWidth;
            }
            return point3;
        }

        private Rect GetRestoreBounds() => 
            this.IsRestoreBoundsValid() ? base.RestoreBounds : new Rect(0.0, 0.0, this._restoreBounds.Width, this._restoreBounds.Height);

        protected WindowMinMax GetWindowMinMax()
        {
            WindowMinMax max = new WindowMinMax();
            Point point = new Point((double) this.GetField("_trackMaxWidthDeviceUnits"), (double) this.GetField("_trackMaxHeightDeviceUnits"));
            Point point2 = new Point((double) this.GetField("_trackMinWidthDeviceUnits"), (double) this.GetField("_trackMinHeightDeviceUnits"));
            max.minWidth = Math.Max(base.MinWidth, point2.X);
            max.maxWidth = (base.MinWidth <= base.MaxWidth) ? (double.IsPositiveInfinity(base.MaxWidth) ? point.X : Math.Min(base.MaxWidth, point.X)) : Math.Min(base.MinWidth, point.X);
            max.minHeight = Math.Max(base.MinHeight, point2.Y);
            if (base.MinHeight > base.MaxHeight)
            {
                max.maxHeight = Math.Min(base.MinHeight, point.Y);
                return max;
            }
            if (!double.IsPositiveInfinity(base.MaxHeight))
            {
                max.maxHeight = Math.Min(base.MaxHeight, point.Y);
                return max;
            }
            max.maxHeight = point.Y;
            Point source = new Point(max.minWidth, max.minHeight);
            Point point4 = new Point(max.maxWidth, max.maxHeight);
            source = this.RoundPointToScreenPixels(source);
            point4 = this.RoundPointToScreenPixels(point4);
            max.maxHeight = point4.Y;
            max.maxWidth = point4.X;
            max.minHeight = source.Y;
            max.minWidth = source.X;
            return max;
        }

        [SecuritySafeCritical]
        private unsafe Rect GetWindowRect()
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rect = new DevExpress.Xpf.Core.NativeMethods.RECT();
            if (base.interopHelperCore != null)
            {
                DevExpress.Xpf.Core.NativeMethods.GetWindowRect(new HandleRef(this, base.interopHelperCore.Handle), ref rect);
            }
            Rect rect2 = new Rect((double) rect.left, (double) rect.top, (double) (rect.right - rect.left), (double) (rect.bottom - rect.top));
            if (base.FlowDirection == FlowDirection.RightToLeft)
            {
                Rect* rectPtr1 = &rect2;
                rectPtr1.X += rect.Width;
            }
            return rect2;
        }

        protected override IntPtr HwndSourceHookHandler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((base.interopHelperCore != null) && (base.interopHelperCore.Handle == hwnd))
            {
                WM uMsg = (WM) msg;
                using (List<KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler>>.Enumerator enumerator = this._messageTable.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        KeyValuePair<WM, DevExpress.Xpf.Docking.Platform.Shell.MessageHandler> current = enumerator.Current;
                        if (((WM) current.Key) == uMsg)
                        {
                            return current.Value(uMsg, wParam, lParam, out handled);
                        }
                    }
                }
            }
            return base.HwndSourceHookHandler(hwnd, msg, wParam, lParam, ref handled);
        }

        private void InvokeUpdateOwnerBindings()
        {
            if (this.updateOwnerBindingsOperation == null)
            {
                DispatcherOperation updateOwnerBindingsOperation = this.updateOwnerBindingsOperation;
            }
            else
            {
                this.updateOwnerBindingsOperation.Abort();
            }
            this.updateOwnerBindingsOperation = base.Dispatcher.BeginInvoke(() => this.UpdateOwnerBindings(), new object[0]);
        }

        [SecuritySafeCritical]
        private bool IsRestoreBoundsValid()
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            if (handle == IntPtr.Zero)
            {
                return true;
            }
            DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT windowPlacement = NativeHelper.GetWindowPlacement(handle);
            return ((windowPlacement.rcNormalPosition.left <= windowPlacement.rcNormalPosition.right) && (windowPlacement.rcNormalPosition.top <= windowPlacement.rcNormalPosition.bottom));
        }

        protected Point LogicalPixelsToScreen(Point point)
        {
            HwndTarget hwndTarget = this.GetHwndTarget();
            return ((hwndTarget == null) ? point : hwndTarget.TransformToDevice.Transform(point));
        }

        internal bool Maximize()
        {
            if (this.CanMaximize)
            {
                this.DisableSizeToContent();
                base.WindowState = WindowState.Maximized;
            }
            return this.CanMaximize;
        }

        private void MaximizeFloatGroup()
        {
            this.Manager.MDIController.Maximize(this.FloatGroup.IsMaximizable ? this.FloatGroup : this.FloatGroup[0]);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            WindowMinMax windowMinMax = this.GetWindowMinMax();
            Point point = this.ScreenToLogicalPixels(new Point(windowMinMax.minWidth, windowMinMax.minHeight));
            Point point2 = this.ScreenToLogicalPixels(new Point(windowMinMax.maxWidth, windowMinMax.maxHeight));
            Size constraint = new Size {
                Width = Math.Max(point.X, Math.Min(availableSize.Width, point2.X)),
                Height = Math.Max(point.Y, Math.Min(availableSize.Height, point2.Y))
            };
            Size size2 = this.MeasureOverriderCore(constraint);
            return new Size(Math.Max(size2.Width, windowMinMax.minWidth), Math.Max(size2.Height, windowMinMax.minHeight));
        }

        private Size MeasureOverriderCore(Size constraint)
        {
            if (this.VisualChildrenCount > 0)
            {
                UIElement visualChild = this.GetVisualChild(0) as UIElement;
                if (visualChild != null)
                {
                    Size size = new Size(0.0, 0.0);
                    Size availableSize = new Size {
                        Width = double.IsPositiveInfinity(constraint.Width) ? double.PositiveInfinity : Math.Max((double) 0.0, (double) (constraint.Width - size.Width)),
                        Height = double.IsPositiveInfinity(constraint.Height) ? double.PositiveInfinity : Math.Max((double) 0.0, (double) (constraint.Height - size.Height))
                    };
                    visualChild.Measure(availableSize);
                    Size desiredSize = visualChild.DesiredSize;
                    Point source = new Point(desiredSize.Width + size.Width, desiredSize.Height + size.Height);
                    Point point2 = this.RoundPointToScreenPixels(source);
                    return new Size(point2.X, point2.Y);
                }
            }
            return new Size(0.0, 0.0);
        }

        internal bool Minimize()
        {
            if (this.CanMinimize)
            {
                base.WindowState = WindowState.Minimized;
            }
            return this.CanMinimize;
        }

        private void MinimizeFloatGroup()
        {
            this.Manager.MDIController.Minimize(this.FloatGroup.IsMinimizable ? this.FloatGroup : this.FloatGroup[0]);
        }

        private void OnActivated(object sender, EventArgs e)
        {
            base.Activated -= new EventHandler(this.OnActivated);
            if (this.Manager.IsFloating && this.AllowNativeDragging)
            {
                this.Manager.Win32DragService.EnqueueDragging(this);
            }
        }

        protected virtual void OnAllowAeroSnapChanged(bool oldValue, bool newValue)
        {
            this.UpdateResizingMode();
            this.UpdateMinMax();
        }

        private static void OnAllowAeroSnapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatingPaneWindow window = d as FloatingPaneWindow;
            if (window != null)
            {
                window.OnAllowAeroSnapChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }

        private void OnBindingsOwnerChanged(Window oldValue, Window newValue)
        {
            this.InvokeUpdateOwnerBindings();
        }

        private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateSystemCommands((FloatingPaneWindow) d);
        }

        private static void OnCanMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateSystemCommands((FloatingPaneWindow) d);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.isDragging)
            {
                this.Win32DragService.ReleaseCapture();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel && (CheckClosedKeyGesture() && !this.isDisposingCore))
            {
                if (this.OwnerWindow != null)
                {
                    base.Dispatcher.BeginInvoke(new Action(this.OwnerWindow.Close), new object[0]);
                }
                e.Cancel = true;
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new FloatingPaneWindowAutomationPeer(this);

        protected void OnDisposing()
        {
            base.ClearValue(FloatStateProperty);
            base.ClearValue(AllowAeroSnapProperty);
            base.ClearValue(FloatingMaxSizeProperty);
            base.ClearValue(FloatingMinSizeProperty);
            base.ClearValue(CanMaximizeProperty);
            base.ClearValue(CanMinimizeProperty);
            base.ClearValue(Window.TitleProperty);
            base.ClearValue(Window.IconProperty);
            Action<DispatcherOperation> action = <>c.<>9__112_0;
            if (<>c.<>9__112_0 == null)
            {
                Action<DispatcherOperation> local1 = <>c.<>9__112_0;
                action = <>c.<>9__112_0 = x => x.Abort();
            }
            this.ensureRelativeSizeOperation.Do<DispatcherOperation>(action);
            Action<DispatcherOperation> action2 = <>c.<>9__112_1;
            if (<>c.<>9__112_1 == null)
            {
                Action<DispatcherOperation> local2 = <>c.<>9__112_1;
                action2 = <>c.<>9__112_1 = x => x.Abort();
            }
            this.changeFloatStateOperation.Do<DispatcherOperation>(action2);
            Action<DispatcherOperation> action3 = <>c.<>9__112_2;
            if (<>c.<>9__112_2 == null)
            {
                Action<DispatcherOperation> local3 = <>c.<>9__112_2;
                action3 = <>c.<>9__112_2 = x => x.Abort();
            }
            this.tryCorrectBoundsAsyncOperation.Do<DispatcherOperation>(action3);
            Action<DispatcherOperation> action4 = <>c.<>9__112_3;
            if (<>c.<>9__112_3 == null)
            {
                Action<DispatcherOperation> local4 = <>c.<>9__112_3;
                action4 = <>c.<>9__112_3 = x => x.Abort();
            }
            this.tryCheckRelativeLocationAsyncOperation.Do<DispatcherOperation>(action4);
            this.sizeChangedLockHelper.Reset();
            this.UnsubscribeDragWidget();
            if (this.OwnerWindow != null)
            {
                this.UnSubscribe(this.OwnerWindow);
                this.OwnerWindow = null;
            }
            this.LockHelper.GetLocker(FloatingWindowLock.LockerKey.ThemeChanging).Reset();
            this.LockHelper.GetLocker(FloatingWindowLock.LockerKey.CheckFloatBounds).Reset();
            base.Close();
            base.LayoutUpdated -= new EventHandler(this.OnFloatGroupLayoutChangedLayoutUpdated);
            this.Manager.SizeChanged -= new SizeChangedEventHandler(this.OnManagerSizeChanged);
            this.FloatGroup.LayoutChanged -= new EventHandler(this.OnFloatGroupLayoutChanged);
            this.FloatGroup.ScreenLocationBeforeClose = new Point(base.Left, base.Top);
            this.FloatGroup.ClearValue(DevExpress.Xpf.Docking.WindowServiceHelper.IWindowServiceProperty);
        }

        private void OnDragWidgetPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.AllowNativeDragging && ((e.ChangedButton == MouseButton.Left) && HitTestHelper.IsDraggable(e.OriginalSource as DependencyObject)))
            {
                e.Handled = this.TryStartDragging(false);
            }
        }

        private void OnFloatGroupLayoutChanged(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnFloatGroupLayoutChangedLayoutUpdated);
            base.LayoutUpdated += new EventHandler(this.OnFloatGroupLayoutChangedLayoutUpdated);
        }

        private void OnFloatGroupLayoutChangedLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnFloatGroupLayoutChangedLayoutUpdated);
            base.Dispatcher.BeginInvoke(delegate {
                this.UnsubscribeDragWidget();
                this.SubscribeDragWidget();
            }, new object[0]);
        }

        private static void OnFloatingMaxSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatingPaneWindow window = d as FloatingPaneWindow;
            if (window != null)
            {
                window.OnFloatingMaxSizeChanged((Size) e.OldValue, (Size) e.NewValue);
            }
        }

        protected virtual void OnFloatingMaxSizeChanged(Size oldValue, Size newValue)
        {
            this.UpdateMinMax();
        }

        private static void OnFloatingMinSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatingPaneWindow window = d as FloatingPaneWindow;
            if (window != null)
            {
                window.OnFloatingMinSizeChanged((Size) e.OldValue, (Size) e.NewValue);
            }
        }

        protected virtual void OnFloatingMinSizeChanged(Size oldValue, Size newValue)
        {
            this.UpdateMinMax();
        }

        protected virtual void OnFloatStateChanged(DevExpress.Xpf.Docking.FloatState oldValue, DevExpress.Xpf.Docking.FloatState newValue)
        {
            if (this.changeFloatStateOperation != null)
            {
                this.changeFloatStateOperation.Abort();
            }
            Action<DevExpress.Xpf.Docking.FloatState> method = new Action<DevExpress.Xpf.Docking.FloatState>(this.ChangeFloatStateCore);
            if ((newValue != DevExpress.Xpf.Docking.FloatState.Maximized) || ScreenHelper.IsAttachedToPresentationSource(this))
            {
                this.ChangeFloatStateCore(newValue);
            }
            else
            {
                object[] args = new object[] { newValue };
                this.changeFloatStateOperation = base.Dispatcher.BeginInvoke(method, args);
            }
        }

        private static void OnFloatStateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FloatingPaneWindow window = o as FloatingPaneWindow;
            if (window != null)
            {
                window.OnFloatStateChanged((DevExpress.Xpf.Docking.FloatState) e.OldValue, (DevExpress.Xpf.Docking.FloatState) e.NewValue);
            }
        }

        private void OnInheritOwnerCommandBindingsChanged()
        {
            this.InvokeUpdateOwnerBindings();
        }

        private void OnInheritOwnerInputBindingsChanged()
        {
            this.InvokeUpdateOwnerBindings();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.FloatGroup.RaiseWindowLoaded();
            base.Dispatcher.BeginInvoke(delegate {
                this.UnsubscribeDragWidget();
                this.SubscribeDragWidget();
            }, new object[0]);
            this.EnsureWindowState(this.FloatState);
        }

        protected virtual void OnLocationChanged(object sender, EventArgs e)
        {
            if (this.fBoundsChangeRequested || ((!this.FloatGroup.IsMaximized && (!this.Win32DragService.IsInEvent && !this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.FloatingBounds))) && !this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.NativeBounds)))
            {
                this.EnsureRelativeLocation();
            }
        }

        private void OnManagerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.sizeChangedLockHelper.IsLocked)
            {
                this.sizeChangedLockHelper.Reset();
            }
            if (this.Manager.IsThemeChangedLocked)
            {
                this.sizeChangedLockHelper.Lock();
                this.sizeChangedLockHelper.DoWhenUnlocked(() => this.TryCheckRelativeLocationAsync(sender));
                base.Dispatcher.BeginInvoke(() => this.sizeChangedLockHelper.Unlock(), DispatcherPriority.Render, new object[0]);
            }
            else
            {
                if ((base.Owner is DXWindow) || (base.Owner is FloatingPaneWindow))
                {
                    this.TryCheckRelativeLocationAsyncLocker.LockOnce();
                }
                this.TryCheckRelativeLocationAsync(sender);
                this.CheckTransform();
                if (this.transformLocker)
                {
                    if (this.TryCheckRelativeLocationAsyncLocker)
                    {
                        this.TryCheckRelativeLocationAsyncLocker.Unlocked += new EventHandler(this.OnTryCheckRelativeLocationAsyncLockerUnlocked);
                    }
                    else
                    {
                        this.EnsureRelativeSizeWithTransform();
                    }
                }
            }
        }

        private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                IInputElement focusedElement = FocusManager.GetFocusedElement(this.FloatGroup);
                if ((focusedElement != null) && (!ReferenceEquals(focusedElement, sender) && focusedElement.Focusable))
                {
                    IInputElement objB = Keyboard.FocusedElement;
                    Visual visual = focusedElement as Visual;
                    if ((visual != null) && !visual.IsDescendantOf(this))
                    {
                        FocusManager.SetFocusedElement(this.FloatGroup, null);
                    }
                    else
                    {
                        focusedElement.Focus();
                        if (ReferenceEquals(Keyboard.FocusedElement, focusedElement) || !ReferenceEquals(Keyboard.FocusedElement, objB))
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void OnPreviewKey(object sender, KeyEventArgs e)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseEvent(e);
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            this.OnPreviewKey(this, e);
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            this.OnPreviewKey(this, e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (this.AllowNativeSizing)
            {
                this.TryStartSizing();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (!base.AllowsTransparency)
            {
                drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Black, 1.0), new Rect(0.0, 0.0, base.Width, base.Height));
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.UpdateHeaderSize(sizeInfo.NewSize);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.Manager.IsThemeChangedLocked && (this.Win32DragService.IsResizing || ((this.correctBoundsRequested || (this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.NativeBounds) || this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.FloatingBounds))) ? this.IsAutoSize : true)))
            {
                if (this.IsAutoSize)
                {
                    this.ensureRelativeSizeOperation = base.Dispatcher.BeginInvoke(new Action(this.EnsureRelativeSize), new object[0]);
                }
                else
                {
                    this.EnsureRelativeSize();
                }
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            UpdateSystemCommands(this);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            this.UpdateHeaderSize(new Size(base.Width, base.Height));
        }

        private void OnTryCheckRelativeLocationAsyncLockerUnlocked(object sender, EventArgs e)
        {
            this.TryCheckRelativeLocationAsyncLocker.Unlocked -= new EventHandler(this.OnTryCheckRelativeLocationAsyncLockerUnlocked);
            this.EnsureRelativeSizeWithTransform();
        }

        private static void OnWindowStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatingPaneWindow window = d as FloatingPaneWindow;
            if (window != null)
            {
                window.OnWindowStateChanged((WindowState) e.OldValue, (WindowState) e.NewValue);
            }
        }

        private unsafe void OnWindowStateChanged(WindowState oldValue, WindowState newValue)
        {
            if (!this.isDisposingCore)
            {
                Rect restoreBounds;
                if ((newValue != WindowState.Normal) && this.IsRestoreBoundsValid())
                {
                    this._restoreBounds = base.RestoreBounds;
                }
                this.LockHelper.Unlock(FloatingWindowLock.LockerKey.Restore);
                this._previousWindowState = newValue;
                if (newValue == WindowState.Maximized)
                {
                    if (this.FloatGroup.FloatState == DevExpress.Xpf.Docking.FloatState.Maximized)
                    {
                        if (!this.Win32DragService.IsDragging)
                        {
                            restoreBounds = this.TransformFromRelativeBounds(DocumentPanel.GetRestoreBounds(this.FloatGroup));
                            if ((restoreBounds.Width == 0.0) || (restoreBounds.Height == 0.0))
                            {
                                return;
                            }
                            base.Left = restoreBounds.Left;
                            if (this.Manager.FlowDirection == FlowDirection.RightToLeft)
                            {
                                base.Left -= restoreBounds.Width;
                            }
                            base.Top = restoreBounds.Top;
                            base.Width = restoreBounds.Width;
                            base.Height = restoreBounds.Height;
                        }
                    }
                    else
                    {
                        try
                        {
                            Rect restoreBounds;
                            if (this.Win32DragService.IsDragging)
                            {
                                this.LockHelper.Lock(FloatingWindowLock.LockerKey.Maximize);
                            }
                            this.MaximizeFloatGroup();
                            restoreBounds = base.RestoreBounds;
                            if (this.Manager.FlowDirection == FlowDirection.RightToLeft)
                            {
                                Rect* rectPtr1 = &restoreBounds;
                                restoreBounds = base.RestoreBounds;
                                rectPtr1.X += restoreBounds.Width;
                            }
                            restoreBounds = this.TransformToRelativeBounds(restoreBounds);
                            restoreBounds = new Rect();
                            if (DocumentPanel.GetRestoreBounds(this.FloatGroup) == restoreBounds)
                            {
                                DocumentPanel.SetRestoreBounds(this.FloatGroup, restoreBounds);
                            }
                        }
                        finally
                        {
                            this.LockHelper.Unlock(FloatingWindowLock.LockerKey.Maximize);
                        }
                    }
                }
                if (newValue == WindowState.Minimized)
                {
                    if (this.FloatGroup.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized)
                    {
                        this.MinimizeFloatGroup();
                        restoreBounds = base.RestoreBounds;
                        if (this.Manager.FlowDirection == FlowDirection.RightToLeft)
                        {
                            Rect* rectPtr2 = &restoreBounds;
                            rectPtr2.X += base.RestoreBounds.Width;
                        }
                        restoreBounds = this.TransformToRelativeBounds(restoreBounds);
                        DocumentPanel.SetRestoreBounds(this.FloatGroup, restoreBounds);
                    }
                    else if (!this.Win32DragService.IsDragging)
                    {
                        restoreBounds = this.TransformFromRelativeBounds(DocumentPanel.GetRestoreBounds(this.FloatGroup));
                        if ((restoreBounds.Width == 0.0) || (restoreBounds.Height == 0.0))
                        {
                            return;
                        }
                        base.Left = restoreBounds.Left;
                        if (this.Manager.FlowDirection == FlowDirection.RightToLeft)
                        {
                            base.Left -= restoreBounds.Width;
                        }
                        base.Top = restoreBounds.Top;
                        base.Width = restoreBounds.Width;
                        base.Height = restoreBounds.Height;
                    }
                }
                if ((newValue == WindowState.Normal) && ((this.FloatGroup.FloatState != DevExpress.Xpf.Docking.FloatState.Normal) && !this.Win32DragService.IsInEvent))
                {
                    this.RestoreFloatGroup();
                }
                if (this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.ParentOpening))
                {
                    base.Dispatcher.BeginInvoke(delegate {
                        if (this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.ParentOpening))
                        {
                            this.LockHelper.Unlock(FloatingWindowLock.LockerKey.ParentOpening);
                            if (oldValue == WindowState.Maximized)
                            {
                                this.WindowState = WindowState.Maximized;
                            }
                        }
                    }, new object[0]);
                }
            }
        }

        [SecuritySafeCritical]
        private IntPtr OnWmCaptureChanged(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            if (this.Win32DragService.IsInEvent && CheckEscapeKeyGesture())
            {
                FloatingView view = this.Manager.GetView(((LayoutGroup) this.FloatGroup)) as FloatingView;
                if (view != null)
                {
                    view.OnKeyDown(Key.Escape);
                }
            }
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmExitSizeMove(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            this.FinishDragging();
            base.InvalidateVisual();
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmGetMinMaxInfo(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = false;
            if (!this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.NativeBounds) && !this.Win32DragService.IsResizing)
            {
                NativeHelper.WmGetMinMaxInfo(base.interopHelperCore.Handle, lParam, this.OverlapTaskbar);
                handled = true;
            }
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmInitMenuPopup(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            IntPtr systemMenuSafe = NativeHelper.GetSystemMenuSafe(base.interopHelperCore.Handle, false);
            if (systemMenuSafe != IntPtr.Zero)
            {
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf020, 1);
                bool isMaximized = this.FloatGroup.IsMaximized;
                uint num = (isMaximized || !this.CanMaximize) ? 1 : 0;
                uint num2 = ((this.FloatGroup.FloatState == DevExpress.Xpf.Docking.FloatState.Minimized) || !this.CanMinimize) ? 1 : 0;
                uint num3 = (this.FloatGroup.FloatState == DevExpress.Xpf.Docking.FloatState.Normal) ? 1 : 0;
                uint num4 = (!this.AllowNativeDragging || isMaximized) ? 1 : 0;
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf030, 0 | num);
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf020, 0 | num2);
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf120, 0 | num3);
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf010, 0 | num4);
                NativeHelper.EnableMenuItemSafe(systemMenuSafe, 0xf000, 0 | num4);
            }
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmLButtonUp(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            if (!this.Win32DragService.IsInEvent)
            {
                handled = false;
            }
            else
            {
                this.FinishDragging();
                handled = true;
            }
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmMoving(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            this.DoMoving();
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmNcHitTest(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            if (base.WindowState == WindowState.Minimized)
            {
                handled = true;
                return new IntPtr(1);
            }
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmSetFocus(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.Focus);
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmShowWindow(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = this.WmShowWindow(wParam, lParam);
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmSize(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            this.WmSizeChanged(wParam);
            handled = false;
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmSizing(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            this.DisableSizeToContent();
            if (!this.Win32DragService.IsResizing)
            {
                handled = false;
            }
            else
            {
                this.DoSizing();
                handled = true;
            }
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmSysCommand(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = false;
            int num = GetInt(wParam) & 0xfff0;
            if (num <= 0xf030)
            {
                if (num != 0xf020)
                {
                    if ((num == 0xf030) && !this.FloatGroup.IsMaximized)
                    {
                        this.MaximizeFloatGroup();
                        handled = true;
                    }
                }
                else if (this.FloatGroup.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized)
                {
                    this.MinimizeFloatGroup();
                    handled = true;
                }
            }
            else if (num == 0xf060)
            {
                if (!CheckClosedKeyGesture())
                {
                    this.Manager.DockController.Close(this.FloatGroup);
                    handled = true;
                }
            }
            else if (num == 0xf120)
            {
                if (base.WindowState != WindowState.Normal)
                {
                    this.LockHelper.LockOnce(FloatingWindowLock.LockerKey.Restore);
                }
                if (this.FloatGroup.IsMaximized)
                {
                    this.RestoreFloatGroup();
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        [SecuritySafeCritical]
        private IntPtr OnWmWindowPosChanging(WM umsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            if (this.Win32DragService.IsDragging)
            {
                DevExpress.Xpf.Docking.Platform.Win32.WINDOWPOS structure = (DevExpress.Xpf.Docking.Platform.Win32.WINDOWPOS) Marshal.PtrToStructure(lParam, typeof(DevExpress.Xpf.Docking.Platform.Win32.WINDOWPOS));
                if (this.suspendDragging || this.Manager.RaiseDockItemDraggingEvent(this.FloatGroup, new Point((double) structure.x, (double) structure.y)))
                {
                    structure.x = (int) base.Left;
                    structure.y = (int) base.Top;
                    Marshal.StructureToPtr<DevExpress.Xpf.Docking.Platform.Win32.WINDOWPOS>(structure, lParam, true);
                }
            }
            handled = false;
            return IntPtr.Zero;
        }

        private void OverlapTaskbarChanged(bool newValue)
        {
            DevExpress.Xpf.Docking.Platform.Shell.WindowChrome.SetOverlapTaskbar(this, newValue);
        }

        internal bool Restore()
        {
            base.WindowState = WindowState.Normal;
            return true;
        }

        internal unsafe Rect RestoreBoundsToFloatBounds()
        {
            if (!this.IsRestoreBoundsValid() || base.RestoreBounds.IsEmpty)
            {
                return Rect.Empty;
            }
            if ((base.WindowState == WindowState.Normal) && !base.IsVisible)
            {
                return Rect.Empty;
            }
            Rect restoreBounds = base.RestoreBounds;
            if (this.Manager.FlowDirection == FlowDirection.RightToLeft)
            {
                Rect* rectPtr1 = &restoreBounds;
                rectPtr1.X += restoreBounds.Width;
            }
            Rect rect = restoreBounds;
            PresentationSource source = PresentationSource.FromDependencyObject(base.Container.Owner);
            if (source != null)
            {
                rect.Transform(source.CompositionTarget.TransformToDevice);
            }
            rect = this.TransformToRelativeBounds(rect);
            return new Rect(this.TransformWindowBoundsToFloatBounds(rect).Location, restoreBounds.Size);
        }

        private void RestoreFloatGroup()
        {
            this.Manager.MDIController.Restore(this.FloatGroup.IsMaximizable ? this.FloatGroup : this.FloatGroup[0]);
        }

        private double Round(double source) => 
            Math.Round((double) (source + 0.5));

        private Point RoundPoint(Point source) => 
            new Point(this.Round(source.X), this.Round(source.Y));

        private Point RoundPointToScreenPixels(Point source)
        {
            Point point = this.LogicalPixelsToScreen(source);
            point = this.RoundPoint(point);
            return this.ScreenToLogicalPixels(point);
        }

        protected Point ScreenToLogicalPixels(Point point)
        {
            HwndTarget hwndTarget = this.GetHwndTarget();
            return ((hwndTarget == null) ? point : hwndTarget.TransformFromDevice.Transform(point));
        }

        protected override unsafe void SetBounds(Rect bounds)
        {
            if ((base.Content != null) && this.firstCheck)
            {
                this.CheckTransform();
                base.SetBinding(Window.ShowInTaskbarProperty, this.CreateShowInTaskBarBinding());
                this.firstCheck = false;
            }
            Point? screenLocationBeforeClose = this.FloatGroup.ScreenLocationBeforeClose;
            if ((screenLocationBeforeClose != null) && (this.FloatState != DevExpress.Xpf.Docking.FloatState.Maximized))
            {
                screenLocationBeforeClose = this.FloatGroup.ScreenLocationBeforeClose;
                Point point = screenLocationBeforeClose.Value;
                bounds = new Rect(new Point(point.X, point.Y), bounds.Size);
            }
            screenLocationBeforeClose = null;
            this.FloatGroup.ScreenLocationBeforeClose = screenLocationBeforeClose;
            bounds = this.TransformFloatBoundsToWindowBounds(bounds);
            if (this.isPerMonitorDpiAware)
            {
                double dpiFactor = DevExpress.Xpf.Docking.Platform.Win32.DpiHelper.GetDpiFactor(base.Container, this);
                Rect* rectPtr1 = &bounds;
                rectPtr1.X *= dpiFactor;
                Rect* rectPtr2 = &bounds;
                rectPtr2.Y *= dpiFactor;
            }
            if (ScreenHelper.IsAttachedToPresentationSource(this))
            {
                this.UpdateBoundsNative(bounds);
            }
            else
            {
                base.SetBounds(bounds);
            }
        }

        private void SetWindowSize(Size newSize)
        {
            base.Width = newSize.Width;
            base.Height = newSize.Height;
        }

        protected override void Subscribe(Window ownerWindow)
        {
            if (ownerWindow != null)
            {
                base.Subscribe(ownerWindow);
                this.BindingsOwner = ownerWindow;
            }
        }

        private void SubscribeDragWidget()
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__181_0;
            if (<>c.<>9__181_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__181_0;
                predicate = <>c.<>9__181_0 = x => (x is FloatingDragWidget) && x.IsVisible;
            }
            FrameworkElement target = LayoutHelper.FindElement(this, predicate);
            if (target == null)
            {
                Predicate<FrameworkElement> predicate2 = <>c.<>9__181_1;
                if (<>c.<>9__181_1 == null)
                {
                    Predicate<FrameworkElement> local2 = <>c.<>9__181_1;
                    predicate2 = <>c.<>9__181_1 = x => DockPane.GetHitTestType(x) == HitTestType.Header;
                }
                target = LayoutHelper.FindElement(this, predicate2);
            }
            if (target != null)
            {
                target.PreviewMouseDown += new MouseButtonEventHandler(this.OnDragWidgetPreviewMouseDown);
                this.dragWidgetRef = new WeakReference(target);
            }
        }

        void IDisposable.Dispose()
        {
            if (!this.isDisposingCore)
            {
                this.isDisposingCore = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        private Rect TransformFloatBoundsToWindowBounds(Rect bounds)
        {
            if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
            {
                Point point = this.transform.Transform(new Point(bounds.Width, bounds.Height));
                bounds.Width = point.X;
                bounds.Height = point.Y;
            }
            return bounds;
        }

        private Rect TransformFromRelativeBounds(Rect rect)
        {
            FrameworkElement owner = base.Container.Owner;
            if ((owner != null) && ScreenHelper.IsAttachedToPresentationSource(owner))
            {
                Point point = owner.PointToScreen(rect.Location);
                rect.Location = point;
            }
            return rect;
        }

        private Rect TransformToRelativeBounds(Rect rect)
        {
            FrameworkElement owner = base.Container.Owner;
            if ((owner != null) && ScreenHelper.IsAttachedToPresentationSource(owner))
            {
                Point point = owner.PointFromScreen(rect.Location);
                rect.Location = point;
            }
            return rect;
        }

        private Rect TransformWindowBoundsToFloatBounds(Rect bounds)
        {
            if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
            {
                Point point = this.transform.Inverse.Transform(new Point(bounds.Width, bounds.Height));
                bounds.Width = point.X;
                bounds.Height = point.Y;
            }
            return bounds;
        }

        private Rect TranslateBoundsToFloatBounds(Rect bounds)
        {
            PresentationSource source = PresentationSource.FromDependencyObject(base.Container.Owner);
            if (source != null)
            {
                bounds.Transform(source.CompositionTarget.TransformFromDevice);
            }
            bounds = this.TransformToRelativeBounds(bounds);
            bounds = this.TransformWindowBoundsToFloatBounds(bounds);
            return bounds;
        }

        internal Size TranslateWindowSizeToFloatSize()
        {
            Rect windowRect = this.WindowRect;
            return this.TranslateBoundsToFloatBounds(windowRect).Size;
        }

        protected override void TryCheckRelativeLocationAsync(object sender)
        {
            this.FloatGroup.ScreenLocationBeforeClose = null;
            if (!(base.Owner is DXWindow) && (!(base.Owner is FloatingPaneWindow) && !(base.Owner is ThemedWindow)))
            {
                base.TryCheckRelativeLocationAsync(sender);
            }
            else
            {
                Action<DispatcherOperation> action = <>c.<>9__126_0;
                if (<>c.<>9__126_0 == null)
                {
                    Action<DispatcherOperation> local1 = <>c.<>9__126_0;
                    action = <>c.<>9__126_0 = x => x.Abort();
                }
                this.tryCheckRelativeLocationAsyncOperation.Do<DispatcherOperation>(action);
                this.tryCheckRelativeLocationAsyncOperation = base.Dispatcher.BeginInvoke(delegate {
                    if (this.OwnerWindow != null)
                    {
                        base.CheckRelativeLocation();
                        this.TryCheckRelativeLocationAsyncLocker.Unlock();
                    }
                }, DispatcherPriority.Render, new object[0]);
            }
        }

        protected override void TryCorrectBoundsAsync(FrameworkElement owner, Rect bounds)
        {
            if (!ScreenHelper.IsAttachedToPresentationSource(this))
            {
                this.SetWindowSize(bounds.Size);
                if (this.Manager.IsMeasureValid)
                {
                    base.TryCorrectBoundsAsync(owner, bounds);
                    return;
                }
            }
            if (!this.TryCheckRelativeLocationAsyncLocker.IsLocked || !(base.Owner is FloatingPaneWindow))
            {
                if ((this.OwnerWindow != null) && ((this.OwnerWindow is DXWindow) || ((base.Owner is FloatingPaneWindow) || !this.Manager.IsMeasureValid)))
                {
                    this.correctBoundsRequested = true;
                    if (this.fFirstRun)
                    {
                        this.fFirstRun = !this.OwnerWindow.IsArrangeValid || !this.Manager.IsMeasureValid;
                        this.savedOwner = owner;
                        this.savedBounds = bounds;
                        if (!this.OwnerWindow.IsArrangeValid || !this.Manager.IsMeasureValid)
                        {
                            Action<DispatcherOperation> action = <>c.<>9__127_0;
                            if (<>c.<>9__127_0 == null)
                            {
                                Action<DispatcherOperation> local1 = <>c.<>9__127_0;
                                action = <>c.<>9__127_0 = x => x.Abort();
                            }
                            this.tryCorrectBoundsAsyncOperation.Do<DispatcherOperation>(action);
                            this.tryCorrectBoundsAsyncOperation = base.Dispatcher.BeginInvoke(delegate {
                                if (this.correctBoundsRequested)
                                {
                                    this.CorrectBoundsAction(this.savedOwner, this.savedBounds);
                                }
                            }, DispatcherPriority.Normal, new object[0]);
                            return;
                        }
                    }
                }
                this.correctBoundsRequested = false;
                base.TryCorrectBoundsAsync(owner, bounds);
            }
        }

        protected override void TrySetOwnerCore(Window containerWindow)
        {
            if (this.Manager.OwnsFloatWindows)
            {
                base.TrySetOwnerCore(containerWindow);
            }
            this.OwnerWindow ??= containerWindow;
        }

        private bool TryStartDragging(bool isFloating = false) => 
            this.Win32DragService.TryStartDragging(this, isFloating);

        private void TryStartSizing()
        {
            if (this.Win32DragService.TryStartSizing(this))
            {
                this.EnsureRelativeSize();
            }
        }

        protected override void UnSubscribe(Window ownerWindow)
        {
            ownerWindow ??= this.OwnerWindow;
            base.UnSubscribe(ownerWindow);
            this.BindingsOwner = null;
        }

        private void UnsubscribeDragWidget()
        {
            if (this.dragWidgetRef != null)
            {
                FrameworkElement target = this.dragWidgetRef.Target as FrameworkElement;
                if (target != null)
                {
                    target.PreviewMouseDown -= new MouseButtonEventHandler(this.OnDragWidgetPreviewMouseDown);
                }
            }
        }

        public void UpdateBoundsNative(Rect bounds)
        {
            if (!this.EnqueueUpdateBounds(bounds))
            {
                this.lastFloatingBounds = bounds;
                if ((!this.Manager.IsThemeChangedLocked && (!this.Manager.SerializationController.IsDeserializing && ((this.FloatGroup.FloatState != DevExpress.Xpf.Docking.FloatState.Minimized) || base.ShowInTaskbar))) && ((!this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.WindowState) && (this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.Maximize) || !this.Win32DragService.IsInEvent)) && !this.LockHelper.IsLocked(FloatingWindowLock.LockerKey.FloatingBounds)))
                {
                    if (this.IsAutoSize)
                    {
                        Size size = SizeToContentHelper.FitSizeToContent(this.FloatGroup.SizeToContent, new Size(base.RenderSize.Width, base.RenderSize.Height), bounds.Size);
                        bounds.Size = size;
                    }
                    using (this.LockHelper.Lock(FloatingWindowLock.LockerKey.NativeBounds))
                    {
                        HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
                        if ((source != null) && (source.CompositionTarget != null))
                        {
                            bounds.Transform(source.CompositionTarget.TransformToDevice);
                            NativeHelper.SetWindowPosSafe(source.Handle, IntPtr.Zero, (int) bounds.X, (int) bounds.Y, (int) bounds.Width, (int) bounds.Height, 0x54);
                        }
                    }
                }
            }
        }

        internal void UpdateFloatGroupBounds()
        {
            this.fBoundsChangeRequested = true;
        }

        private void UpdateHeaderSize(Size size)
        {
            if (base.WindowState == WindowState.Minimized)
            {
                MinimizedFloatGroupsItem item = LayoutTreeHelper.GetVisualChildren(this).OfType<MinimizedFloatGroupsItem>().FirstOrDefault<MinimizedFloatGroupsItem>();
                if (item != null)
                {
                    item.Width = size.Width;
                }
            }
        }

        private void UpdateMinMax()
        {
            base.ClearValue(FrameworkElement.MinHeightProperty);
            base.ClearValue(FrameworkElement.MinWidthProperty);
            base.ClearValue(FrameworkElement.MaxHeightProperty);
            base.ClearValue(FrameworkElement.MaxWidthProperty);
            Size resizingMinSize = LayoutItemsHelper.GetResizingMinSize(this.FloatGroup);
            Size resizingMaxSize = LayoutItemsHelper.GetResizingMaxSize(this.FloatGroup);
            if (!double.IsNaN(resizingMinSize.Height))
            {
                base.MinHeight = resizingMinSize.Height;
            }
            if (!double.IsNaN(resizingMinSize.Width))
            {
                base.MinWidth = resizingMinSize.Width;
            }
            if (!double.IsNaN(resizingMaxSize.Height))
            {
                base.MaxHeight = resizingMaxSize.Height;
            }
            if (!double.IsNaN(resizingMaxSize.Width))
            {
                base.MaxWidth = resizingMaxSize.Width;
            }
        }

        private void UpdateOwnerBindings()
        {
            this.UpdateOwnerInputBindings();
            this.UpdateOwnerCommandBindings();
        }

        private void UpdateOwnerCommandBindings()
        {
            if ((this.BindingsOwner != null) && this.InheritOwnerCommandBindings)
            {
                base.CommandBindings.AddRange(this.BindingsOwner.CommandBindings);
            }
            else
            {
                base.CommandBindings.Clear();
            }
        }

        private void UpdateOwnerInputBindings()
        {
            if ((this.BindingsOwner != null) && this.InheritOwnerInputBindings)
            {
                base.InputBindings.AddRange(this.BindingsOwner.InputBindings);
            }
            else
            {
                base.InputBindings.Clear();
            }
        }

        internal void UpdateOwnerWindow()
        {
            Window window = LayoutHelper.FindParentObject<Window>(this.Manager);
            if (this.OwnerWindow != null)
            {
                this.UnSubscribe(this.OwnerWindow);
            }
            this.OwnerWindow = window;
            if (this.Manager.OwnsFloatWindows)
            {
                base.Owner = this.OwnerWindow;
            }
            if (this.OwnerWindow != null)
            {
                this.Subscribe(this.OwnerWindow);
            }
        }

        internal void UpdateRenderOptions()
        {
            base.Dispatcher.BeginInvoke(() => VisualEffectsInheritanceHelper.SetTextAndRenderOptions(this, this.Manager), DispatcherPriority.Background, new object[0]);
        }

        private void UpdateResizingMode()
        {
            this.ResizeMode = (!this.AllowAeroSnap || !this.AllowNativeDragging) ? ResizeMode.NoResize : ResizeMode.CanResize;
        }

        private static void UpdateSystemCommands(FloatingPaneWindow window)
        {
            IntPtr handle = new WindowInteropHelper(window).Handle;
            if (handle != IntPtr.Zero)
            {
                int windowLongSafe = NativeHelper.GetWindowLongSafe(handle, -16);
                windowLongSafe = !window.CanMaximize ? (windowLongSafe & -65537) : (windowLongSafe | 0x10000);
                windowLongSafe = !window.CanMinimize ? (windowLongSafe & -131073) : (windowLongSafe | 0x20000);
                NativeHelper.SetWindowLongSafe(handle, -16, windowLongSafe);
            }
        }

        private bool WmShowWindow(IntPtr wParam, IntPtr lParam)
        {
            int num = DevExpress.Xpf.Core.NativeMethods.IntPtrToInt32(lParam);
            if ((num != 1) && (num == 3))
            {
                base.ShowActivated = base.WindowState == WindowState.Maximized;
                this.LockHelper.LockOnce(FloatingWindowLock.LockerKey.ParentOpening);
            }
            return false;
        }

        private bool WmSizeChanged(IntPtr wParam)
        {
            switch (((int) wParam))
            {
                case 0:
                    if (this._previousWindowState != WindowState.Normal)
                    {
                        this.CorrectRestoreBounds();
                        this._previousWindowState = WindowState.Normal;
                    }
                    break;

                case 1:
                    this._previousWindowState = WindowState.Minimized;
                    break;

                case 2:
                    this._previousWindowState = WindowState.Maximized;
                    break;

                default:
                    break;
            }
            return false;
        }

        public Rect Bounds =>
            (base.WindowState == WindowState.Maximized) ? this.lastFloatingBounds : new Rect(base.Left, base.Top, base.Width, base.Height);

        public bool InheritOwnerCommandBindings
        {
            get => 
                (bool) base.GetValue(InheritOwnerCommandBindingsProperty);
            set => 
                base.SetValue(InheritOwnerCommandBindingsProperty, value);
        }

        public bool InheritOwnerInputBindings
        {
            get => 
                (bool) base.GetValue(InheritOwnerInputBindingsProperty);
            set => 
                base.SetValue(InheritOwnerInputBindingsProperty, value);
        }

        public DockLayoutManager Manager { get; private set; }

        public bool OverlapTaskbar
        {
            get => 
                (bool) base.GetValue(OverlapTaskbarProperty);
            set => 
                base.SetValue(OverlapTaskbarProperty, value);
        }

        internal bool AllowAeroSnap
        {
            get => 
                (bool) base.GetValue(AllowAeroSnapProperty);
            set => 
                base.SetValue(AllowAeroSnapProperty, value);
        }

        internal FloatingWindowLock LockHelper
        {
            get
            {
                FloatingWindowLock floatingWindowState = this.floatingWindowState;
                if (this.floatingWindowState == null)
                {
                    FloatingWindowLock local1 = this.floatingWindowState;
                    floatingWindowState = this.floatingWindowState = new FloatingWindowLock();
                }
                return floatingWindowState;
            }
        }

        protected internal DevExpress.Xpf.Docking.FloatGroup FloatGroup { get; private set; }

        protected internal Rect WindowRect =>
            this.GetWindowRect();

        private bool AllowNativeDragging =>
            this.Manager.EnableNativeDragging && (base.WindowState != WindowState.Minimized);

        private bool AllowNativeSizing =>
            this.AllowNativeDragging && this.Manager.RedrawContentWhenResizing;

        private Window BindingsOwner
        {
            get => 
                this.bindingsOwner;
            set
            {
                if (!ReferenceEquals(this.bindingsOwner, value))
                {
                    Window bindingsOwner = this.bindingsOwner;
                    this.bindingsOwner = value;
                    this.OnBindingsOwnerChanged(bindingsOwner, value);
                }
            }
        }

        private bool CanMaximize =>
            (bool) base.GetValue(CanMaximizeProperty);

        private bool CanMinimize =>
            (bool) base.GetValue(CanMinimizeProperty);

        private DevExpress.Xpf.Docking.FloatState FloatState =>
            (DevExpress.Xpf.Docking.FloatState) base.GetValue(FloatStateProperty);

        private bool IsAutoSize =>
            base.SizeToContent != SizeToContent.Manual;

        private DevExpress.Xpf.Docking.Platform.Win32DragService Win32DragService =>
            this.Manager.Win32DragService;

        DockLayoutManager IFloatingPane.Manager =>
            this.Manager;

        DevExpress.Xpf.Docking.FloatGroup IFloatingPane.FloatGroup =>
            this.FloatGroup;

        DevExpress.Xpf.Docking.FloatGroup IDraggableWindow.FloatGroup =>
            this.FloatGroup;

        bool IDraggableWindow.IsDragging
        {
            get => 
                this.isDragging;
            set
            {
                bool flag;
                this.isDragging = flag = value;
                base.SetCurrentValue(UIElement.IsHitTestVisibleProperty, !flag);
            }
        }

        DockLayoutManager IDraggableWindow.Manager =>
            this.Manager;

        bool IDraggableWindow.SuspendDragging
        {
            get => 
                this.suspendDragging;
            set => 
                this.suspendDragging = value;
        }

        Window IDraggableWindow.Window =>
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingPaneWindow.<>c <>9 = new FloatingPaneWindow.<>c();
            public static Action<DispatcherOperation> <>9__112_0;
            public static Action<DispatcherOperation> <>9__112_1;
            public static Action<DispatcherOperation> <>9__112_2;
            public static Action<DispatcherOperation> <>9__112_3;
            public static Action<DispatcherOperation> <>9__126_0;
            public static Action<DispatcherOperation> <>9__127_0;
            public static Predicate<FrameworkElement> <>9__181_0;
            public static Predicate<FrameworkElement> <>9__181_1;

            internal void <.cctor>b__9_0(FloatingPaneWindow d)
            {
                d.OnInheritOwnerCommandBindingsChanged();
            }

            internal void <.cctor>b__9_1(FloatingPaneWindow d)
            {
                d.OnInheritOwnerInputBindingsChanged();
            }

            internal void <.cctor>b__9_2(FloatingPaneWindow d, bool oldValue, bool newValue)
            {
                d.OverlapTaskbarChanged(newValue);
            }

            internal void <OnDisposing>b__112_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <OnDisposing>b__112_1(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <OnDisposing>b__112_2(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <OnDisposing>b__112_3(DispatcherOperation x)
            {
                x.Abort();
            }

            internal bool <SubscribeDragWidget>b__181_0(FrameworkElement x) => 
                (x is FloatingDragWidget) && x.IsVisible;

            internal bool <SubscribeDragWidget>b__181_1(FrameworkElement x) => 
                DockPane.GetHitTestType(x) == HitTestType.Header;

            internal void <TryCheckRelativeLocationAsync>b__126_0(DispatcherOperation x)
            {
                x.Abort();
            }

            internal void <TryCorrectBoundsAsync>b__127_0(DispatcherOperation x)
            {
                x.Abort();
            }
        }

        private class ShowInTaskBarConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
                !((bool) values[0]) ? ((object) 0) : ((object) ((bool) values[1]));

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct WindowMinMax
        {
            internal double minWidth;
            internal double maxWidth;
            internal double minHeight;
            internal double maxHeight;
            internal WindowMinMax(double minSize, double maxSize)
            {
                this.minWidth = minSize;
                this.maxWidth = maxSize;
                this.minHeight = minSize;
                this.maxHeight = maxSize;
            }
        }
    }
}

