namespace DevExpress.Mvvm.Native
{
    using System;

    public static class SetPropertyHelper
    {
        public static bool Set<T>(ref T storage, T value) => 
            SetCore<T>(ref storage, value);

        public static bool Set<T>(ref T storage, T value, Action changedCallback)
        {
            if (!SetCore<T>(ref storage, value))
            {
                return false;
            }
            changedCallback();
            return true;
        }

        public static bool Set<T>(ref T storage, T value, Action<T> changedCallback)
        {
            T local = storage;
            if (!SetCore<T>(ref storage, value))
            {
                return false;
            }
            changedCallback(local);
            return true;
        }

        public static bool Set<T>(ref T storage, T value, Action<T, T> changedCallback)
        {
            T local = storage;
            if (!SetCore<T>(ref storage, value))
            {
                return false;
            }
            changedCallback(local, value);
            return true;
        }

        private static bool SetCore<T>(ref T storage, T value)
        {
            if (Equals(value, (T) storage))
            {
                return false;
            }
            storage = value;
            return true;
        }
    }
}

