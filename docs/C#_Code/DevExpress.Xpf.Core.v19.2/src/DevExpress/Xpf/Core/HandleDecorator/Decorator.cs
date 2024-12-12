namespace DevExpress.Xpf.Core.HandleDecorator
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.HandleDecorator.Helpers;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;
    using System.Windows.Media;

    public abstract class Decorator : IDisposable
    {
        private const int firstAppearanceAnimationDurationMs = 220;
        private const int restoreAnimationDurationMs = 300;
        private string themeName;
        private Thickness decoratorOffset;
        private Thickness decoratorLeftMargins;
        private Thickness decoratorRightMargins;
        private Thickness decoratorTopMargins;
        private Thickness decoratorBottomMargins;
        private SolidColorBrush defaultActiveColor = new SolidColorBrush(Colors.Black);
        private SolidColorBrush defaultInactiveColor = new SolidColorBrush(Colors.Black);
        private SolidColorBrush activeColor;
        private SolidColorBrush inactiveColor;
        private DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands prevWindowPlacement;
        private WindowInteropHelper interopHelperCore;
        private HandleDecoratorWindow _windowCore;
        private IntPtr addedHookHandle;
        private Window controlCore;
        private Timer appearanceTimer;
        private Rectangle prevControlRect;
        private bool startupActive = true;
        private bool restoreAnimation;
        private bool firstAppearance = true;
        private bool fDisposed;

        public Decorator(SolidColorBrush activeColor, SolidColorBrush inactiveColor, Thickness offset, StructDecoratorMargins structDecoratorMargins, bool startupActiveState)
        {
            this.startupActive = startupActiveState;
            this.ActiveColor = activeColor;
            this.InactiveColor = inactiveColor;
            this.decoratorOffset = offset;
            this.decoratorLeftMargins = structDecoratorMargins.LeftMargins;
            this.decoratorRightMargins = structDecoratorMargins.RightMargins;
            this.decoratorTopMargins = structDecoratorMargins.TopMargins;
            this.decoratorBottomMargins = structDecoratorMargins.BottomMargins;
            this.EnsureDelayTimer();
        }

        internal void ActiveStateChanged(bool value)
        {
            if (this._windowCore != null)
            {
                this._windowCore.IsActive = value;
            }
        }

        private void AddHook(Window window)
        {
            this.interopHelperCore = new WindowInteropHelper(window);
            if (this.interopHelperCore.Handle != IntPtr.Zero)
            {
                HwndSource.FromHwnd(this.interopHelperCore.Handle).AddHook(new HwndSourceHook(this.HwndSourceHookHandler));
                this.addedHookHandle = this.interopHelperCore.Handle;
            }
        }

        private void appearanceTimer_Tick(object sender, EventArgs e)
        {
            if (!this.fDisposed)
            {
                if (this.firstAppearance)
                {
                    this.firstAppearance = false;
                }
                this.StopDelayTimer();
                this.UpdateDecoratorWindows(false, false);
                this.restoreAnimation = false;
            }
        }

        private void control_Closed(object sender, EventArgs e)
        {
            this.RemoveHook(this.addedHookHandle);
        }

        private void controlCore_HandleCreated(object sender, EventArgs e)
        {
            this.controlCore.Dispatcher.BeginInvoke(delegate {
                this.EnsureThemeProps();
                this.InitDecoratorWindowHandle();
            }, new object[0]);
        }

        private void controlCore_HandleDestroyed(object sender, EventArgs e)
        {
            this.DestroyDecoratorWindowHandle();
        }

        protected abstract HandleDecoratorWindow CreateHandleDecoratorWindow(bool startupActive);
        private void DestroyDecoratorWindow()
        {
            if (this._windowCore != null)
            {
                this._windowCore.GetPainter().ClearImages();
                this._windowCore.Dispose();
                this._windowCore = null;
            }
        }

        private void DestroyDecoratorWindowHandle()
        {
            this.DestroyDelayTimer();
            this.RemoveHook(this.Control);
            this.DestroyDecoratorWindow();
        }

        private void DestroyDelayTimer()
        {
            if (this.appearanceTimer != null)
            {
                this.appearanceTimer.Stop();
                this.appearanceTimer.Tick -= new EventHandler(this.appearanceTimer_Tick);
                this.appearanceTimer.Dispose();
                this.appearanceTimer = null;
            }
        }

        public void Dispose()
        {
            this.fDisposed = true;
            this.DestroyDelayTimer();
            this.UnSubscribeControlEvents(this.Control);
            this.DestroyDecoratorWindowHandle();
        }

        private void EnsureDelayTimer()
        {
            if (this.appearanceTimer == null)
            {
                this.appearanceTimer = new Timer();
                this.appearanceTimer.Interval = 220;
                this.appearanceTimer.Tick += new EventHandler(this.appearanceTimer_Tick);
            }
        }

        private void EnsureThemeProps()
        {
            if (this.Control != null)
            {
                this.themeName = this.GetThemeName(this.Control);
                DXWindow control = this.Control as DXWindow;
                if (control != null)
                {
                    this.decoratorTopMargins = control.BorderEffectTopMargins;
                    this.decoratorBottomMargins = control.BorderEffectBottomMargins;
                    this.decoratorLeftMargins = control.BorderEffectLeftMargins;
                    this.decoratorRightMargins = control.BorderEffectRightMargins;
                    this.decoratorOffset = control.BorderEffectOffset;
                }
            }
        }

        private Rectangle GetFormRectangle()
        {
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.RECT lpRect = new DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.RECT();
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetWindowRect(NativeHelper.GetHandle(this.Control), ref lpRect);
            return lpRect.ToRectangle();
        }

        private string GetThemeName(DependencyObject obj)
        {
            Assembly themeAssembly = null;
            string realThemeName = ThemeHelper.GetRealThemeName(obj);
            if (realThemeName == null)
            {
                return "loadDefault";
            }
            try
            {
                themeAssembly = AssemblyHelper.GetThemeAssembly(realThemeName);
            }
            finally
            {
                if (themeAssembly == null)
                {
                    realThemeName = "loadDefault";
                }
            }
            return realThemeName;
        }

        protected internal void Hide()
        {
            if (this._windowCore != null)
            {
                this._windowCore.HideWnd();
            }
        }

        protected virtual IntPtr HwndSourceHookHandler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            this.interopHelperCore = new WindowInteropHelper(this.Control);
            if (msg == 2)
            {
                this.Hide();
                return IntPtr.Zero;
            }
            if (msg == 0x47)
            {
                if ((hwnd == this.interopHelperCore.Handle) && (!this.firstAppearance && !this.restoreAnimation))
                {
                    this.UpdateDecoratorWindowWithDelayIfNecessary();
                    return IntPtr.Zero;
                }
                this.UpdateDecoratorWindowPos(this.GetFormRectangle());
            }
            return IntPtr.Zero;
        }

        private void InitDecoratorWindowHandle()
        {
            this.EnsureDelayTimer();
            this.AddHook(this.Control);
            this._windowCore = this.CreateHandleDecoratorWindow(this.startupActive);
            this._windowCore.EnsureHandle();
            this.UpdateDecoratorWindowWithDelayIfNecessary();
        }

        private void OnControlChanged(Window old, Window newControl)
        {
            this.UnSubscribeControlEvents(old);
            this.SubscribeControlEvents(newControl);
            if ((old != null) && NativeHelper.IsHandleCreated(old))
            {
                this.DestroyDecoratorWindowHandle();
            }
            if ((newControl != null) && NativeHelper.IsHandleCreated(newControl))
            {
                this.InitDecoratorWindowHandle();
            }
        }

        private void RemoveHook(IntPtr handle)
        {
            try
            {
                HwndSource.FromHwnd(handle).RemoveHook(new HwndSourceHook(this.HwndSourceHookHandler));
                this.DestroyDecoratorWindow();
            }
            catch
            {
            }
        }

        private void RemoveHook(Window window)
        {
            this.interopHelperCore = new WindowInteropHelper(window);
            if (((int) this.interopHelperCore.Handle) > 0)
            {
                HwndSource.FromHwnd(this.interopHelperCore.Handle).RemoveHook(new HwndSourceHook(this.HwndSourceHookHandler));
            }
        }

        public void RenderDecorator()
        {
            if (this._windowCore != null)
            {
                this._windowCore.BmpManager.ReleaseCachedBitmap();
                this._windowCore.RenderDecoratorWindow(false);
            }
        }

        public void SetDecoratorSizingMargins(Thickness leftMargins, Thickness rightMargins, Thickness topMargins, Thickness bottomMargins)
        {
            this.decoratorLeftMargins = leftMargins;
            this.decoratorRightMargins = rightMargins;
            this.decoratorTopMargins = topMargins;
            this.decoratorBottomMargins = bottomMargins;
        }

        public void SetDecoratorWindowOffset(Thickness decoratorOffset)
        {
            this.decoratorOffset = decoratorOffset;
        }

        private bool ShouldDelayAppearance(IntPtr hWnd)
        {
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.WINDOWPLACEMENT windowplacement;
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.GetWindowPlacement(hWnd, out windowplacement);
            DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands showCmd = windowplacement.ShowCmd;
            bool flag = false;
            if ((showCmd == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Normal) && ((this.prevWindowPlacement == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Maximize) || ((this.prevWindowPlacement == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Maximize) || ((this.prevWindowPlacement == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Minimize) || ((this.prevWindowPlacement == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.ShowMinimized) || (this.prevWindowPlacement == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Hide))))))
            {
                flag = true;
            }
            if (showCmd == DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.ShowWindowCommands.Hide)
            {
                this.firstAppearance = true;
            }
            this.prevWindowPlacement = showCmd;
            return flag;
        }

        private bool ShouldDelayResize(Rectangle currentCtrlBounds, Rectangle previousCtrlBounds)
        {
            if (this._windowCore == null)
            {
                return true;
            }
            bool flag = false;
            int width = HandleDecoratorWindowLayoutCalculator.Calculate(HandleDecoratorWindowTypes.Left, currentCtrlBounds, this._windowCore.GetPainter()).Width;
            if ((this._windowCore.GetPainter().GetOffsetByWindowType(HandleDecoratorWindowTypes.Left) != 0) && ((Math.Abs((int) (previousCtrlBounds.Width - currentCtrlBounds.Width)) > width) || (Math.Abs((int) (previousCtrlBounds.Height - currentCtrlBounds.Height)) > width)))
            {
                flag = true;
            }
            return flag;
        }

        private void StartDelayTimer()
        {
            if (!this.firstAppearance)
            {
                this.appearanceTimer.Stop();
                this.restoreAnimation = true;
            }
            this.appearanceTimer.Interval = this.firstAppearance ? 220 : 300;
            this.appearanceTimer.Start();
        }

        private void StopDelayTimer()
        {
            if (this.appearanceTimer != null)
            {
                this.appearanceTimer.Stop();
            }
        }

        private void SubscribeControlEvents(Window control)
        {
            if (control != null)
            {
                control.SourceInitialized += new EventHandler(this.controlCore_HandleCreated);
                control.Unloaded += new RoutedEventHandler(this.controlCore_HandleDestroyed);
                control.Closed += new EventHandler(this.control_Closed);
            }
        }

        private void UnSubscribeControlEvents(Window control)
        {
            if (control != null)
            {
                control.SourceInitialized -= new EventHandler(this.controlCore_HandleCreated);
                control.Unloaded -= new RoutedEventHandler(this.controlCore_HandleDestroyed);
                control.Closed -= new EventHandler(this.control_Closed);
            }
        }

        public void UpdateDecoratorBitmaps(Window owner)
        {
            if (owner != null)
            {
                if (this._windowCore != null)
                {
                    this._windowCore.GetPainter().ClearImages();
                }
                this.themeName = ThemeManager.GetThemeName(owner);
                this.RenderDecorator();
            }
        }

        private void UpdateDecoratorWindowPos(Rectangle formRect)
        {
            if (this._windowCore != null)
            {
                this._windowCore.UpdateWindowPos(formRect);
            }
        }

        private void UpdateDecoratorWindows(bool appearanceDelay, bool resizeDelay)
        {
            if (appearanceDelay)
            {
                this.StartDelayTimer();
            }
            else if (this.firstAppearance)
            {
                this.firstAppearance = false;
            }
            else
            {
                this.StopDelayTimer();
                if (this._windowCore != null)
                {
                    this.UpdateHandleDecoratorWindowPositions(resizeDelay);
                }
            }
        }

        private void UpdateDecoratorWindowWithDelayIfNecessary()
        {
            Rectangle formRectangle = this.GetFormRectangle();
            this.UpdateDecoratorWindowPos(formRectangle);
            bool appearanceDelay = this.ShouldDelayAppearance(NativeHelper.GetHandle(this.Control));
            this.UpdateDecoratorWindows(appearanceDelay, this.ShouldDelayResize(formRectangle, this.prevControlRect));
            this.prevControlRect = formRectangle;
        }

        private void UpdateHandleDecoratorWindowPositions(bool resizeDelay)
        {
            if (this._windowCore != null)
            {
                this._windowCore.IsVisible = this.ShouldShowHandleDecoratorWindow;
                this._windowCore.CommitChanges(this.restoreAnimation, resizeDelay);
            }
        }

        protected internal string CurrentThemeName
        {
            get => 
                this.themeName;
            set => 
                this.themeName = value;
        }

        protected internal SolidColorBrush ActiveColor
        {
            get => 
                this.activeColor;
            set
            {
                if ((value != null) && (value.Color.A != 0))
                {
                    this.activeColor = value;
                }
                else
                {
                    this.activeColor = this.defaultActiveColor;
                }
            }
        }

        public SolidColorBrush InactiveColor
        {
            get => 
                this.inactiveColor;
            set
            {
                if ((value != null) && (value.Color.A != 0))
                {
                    this.inactiveColor = value;
                }
                else
                {
                    this.inactiveColor = this.defaultInactiveColor;
                }
            }
        }

        protected internal Thickness DecoratorOffset =>
            this.decoratorOffset;

        protected internal Thickness DecoratorLeftMargins =>
            this.decoratorLeftMargins;

        protected internal Thickness DecoratorRightMargins =>
            this.decoratorRightMargins;

        protected internal Thickness DecoratorTopMargins =>
            this.decoratorTopMargins;

        protected internal Thickness DecoratorBottomMargins =>
            this.decoratorBottomMargins;

        public Window Control
        {
            get => 
                this.controlCore;
            set
            {
                Window controlCore = this.controlCore;
                this.controlCore = value;
                this.themeName = this.GetThemeName(this.controlCore);
                if (!ReferenceEquals(controlCore, this.controlCore))
                {
                    this.OnControlChanged(controlCore, this.controlCore);
                }
            }
        }

        private bool ShouldShowHandleDecoratorWindow =>
            (this.Control != null) ? ((this.Control.Width != 0.0) && ((this.Control.Height != 0.0) && (DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.IsWindowVisible(NativeHelper.GetHandle(this.Control)) && (!DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.IsIconic(NativeHelper.GetHandle(this.Control)) && !DevExpress.Xpf.Core.HandleDecorator.Helpers.NativeMethods.IsZoomed(NativeHelper.GetHandle(this.Control)))))) : false;
    }
}

