namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ContainerLockerService : ServiceBase, IContainerLockerService
    {
        public static readonly DependencyProperty OwnerLockProperty;
        public static readonly DependencyProperty OwnerProperty;
        public static readonly DependencyProperty OwnerSearchModeProperty;
        public static readonly DependencyProperty IsLockedProperty;
        public static readonly DependencyProperty WaitForServiceAttachedProperty;
        private ContainerLocker locker;

        static ContainerLockerService();
        private object CoerceIsLocked(object baseValue);
        void IContainerLockerService.Lock();
        void IContainerLockerService.Unlock();
        private DependencyObject FindOwner();
        protected virtual void LockCore();
        protected override void OnDetaching();
        protected virtual void OnIsLockedChanged();
        protected virtual void UnlockCore();

        public SplashScreenLock OwnerLock { get; set; }

        public FrameworkElement Owner { get; set; }

        public SplashScreenOwnerSearchMode OwnerSearchMode { get; set; }

        public bool IsLocked { get; set; }

        public bool WaitForServiceAttached { get; set; }

        bool IContainerLockerService.IsContainerLocked { get; }

        private bool ActualIsLocked { get; }

        private bool CanLock { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainerLockerService.<>c <>9;
            public static Action<ContainerLocker> <>9__31_0;

            static <>c();
            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__5_1(DependencyObject d, object o);
            internal void <UnlockCore>b__31_0(ContainerLocker x);
        }
    }
}

