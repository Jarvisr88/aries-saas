namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    internal class WindowGlowWorker : DependencyObject
    {
        public static readonly DependencyProperty WindowGlowWorkerProperty;
        private ThemedWindow window;
        private WindowGlowChrome currentGlow;
        private readonly GlowWindow[] glowWindows;
        private bool isGlowVisible;
        private bool showGlow;
        private bool useGlowColors;
        internal int deferGlowChangesCount;
        private bool updatingZOrder;
        private SolidColorBrush defaultGlowColor;
        private DispatcherTimer makeGlowVisibleTimer;
        private const int MinimizeAnimationDurationMilliseconds = 250;

        static WindowGlowWorker();
        public WindowGlowWorker();
        internal void ApplyWindowGlow();
        private void AttachToWindow(ThemedWindow newWindow);
        private void CreateGlowWindowHandles();
        public IDisposable DeferGlowChanges();
        private void DestroyGlowWindows();
        private void DetachFromWindow();
        internal void EndDeferGlowChanges();
        private GlowWindow GetOrCreateGlowWindow(int direction);
        public static WindowGlowWorker GetWindowGlowWorker(ThemedWindow window);
        private void OnDelayedVisibilityTimerTick(object sender, EventArgs e);
        private void OnGlowRequestRepaint(object sender, EventArgs e);
        private void OnShowGlowChanged(bool newValue);
        private void OnSourceInitialized(object sender, EventArgs e);
        private void OnUseGlowColorsChanged();
        private void OnWindowActivated(object sender, EventArgs e);
        private void OnWindowClosed(object sender, EventArgs e);
        private void OnWindowDeactivated(object sender, EventArgs e);
        private static void OnWindowGlowWorkerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public void SetWindowGlow(WindowGlowChrome newGlow);
        public static void SetWindowGlowWorker(ThemedWindow window, WindowGlowWorker glow);
        private void StopTimer();
        private void UnsubscribeWindowEvents();
        private void UpdateAttachedChromeProperties();
        private void UpdateGlowActiveColor(SolidColorBrush brush);
        private void UpdateGlowActiveState();
        private void UpdateGlowInactiveColor(SolidColorBrush brush);
        internal void UpdateGlowPosition(bool needToDelay);
        [SecuritySafeCritical]
        private void UpdateGlowVisibility(bool delayIfNecessary);
        [SecuritySafeCritical]
        private void UpdateGlowWindowPositions(bool delayIfNecessary = false);
        [SecuritySafeCritical]
        private void UpdateZOrderOfOwner(IntPtr hwndOwner);
        [SecuritySafeCritical]
        private void UpdateZOrderOfThisAndOwner();

        private bool IsGlowVisible { get; set; }

        private bool ShouldShowGlow { [SecuritySafeCritical] get; }

        private IEnumerable<GlowWindow> LoadedGlowWindows { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowGlowWorker.<>c <>9;
            public static Func<GlowWindow, bool> <>9__22_0;

            static <>c();
            internal bool <get_LoadedGlowWindows>b__22_0(GlowWindow w);
        }
    }
}

