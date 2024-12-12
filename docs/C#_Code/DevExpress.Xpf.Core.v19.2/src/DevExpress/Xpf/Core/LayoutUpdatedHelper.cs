namespace DevExpress.Xpf.Core
{
    using System;

    public static class LayoutUpdatedHelper
    {
        [ThreadStatic]
        private static Locker globalLocker;

        public static Locker GlobalLocker
        {
            get
            {
                globalLocker ??= new Locker();
                return globalLocker;
            }
        }
    }
}

