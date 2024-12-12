namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class TimeConsumingOperationHelper
    {
        public static readonly DependencyProperty IsBusyProperty = IsBusyPropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey IsBusyPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsBusy", typeof(bool), typeof(TimeConsumingOperationHelper), new UIPropertyMetadata(false));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BusyLockerProperty = BusyLockerPropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey BusyLockerPropertyKey = DependencyProperty.RegisterAttachedReadOnly("BusyLocker", typeof(Locker), typeof(TimeConsumingOperationHelper), new UIPropertyMetadata(null));

        public static void DoBusyAction(DependencyObject d, Action action)
        {
            LockElement(d);
            try
            {
                action();
            }
            finally
            {
                UnlockElement(d);
            }
        }

        private static Locker GetBusyLocker(DependencyObject obj) => 
            (Locker) obj.GetValue(BusyLockerProperty);

        public static bool GetIsBusy(DependencyObject obj) => 
            (bool) obj.GetValue(IsBusyProperty);

        private static Locker GetLocker(DependencyObject d)
        {
            Locker busyLocker = GetBusyLocker(d);
            if (busyLocker == null)
            {
                busyLocker = new Locker();
                SetBusyLocker(d, busyLocker);
            }
            return busyLocker;
        }

        public static void LockElement(DependencyObject d)
        {
            Locker locker = GetLocker(d);
            locker.Lock();
            UpdateIsBusy(d, locker);
        }

        private static void SetBusyLocker(DependencyObject obj, Locker value)
        {
            obj.SetValue(BusyLockerPropertyKey, value);
        }

        private static void SetIsBusy(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBusyPropertyKey, value);
        }

        public static void UnlockElement(DependencyObject d)
        {
            Locker locker = GetLocker(d);
            locker.Unlock();
            UpdateIsBusy(d, locker);
        }

        private static void UpdateIsBusy(DependencyObject d, Locker locker)
        {
            SetIsBusy(d, locker.IsLocked);
        }
    }
}

