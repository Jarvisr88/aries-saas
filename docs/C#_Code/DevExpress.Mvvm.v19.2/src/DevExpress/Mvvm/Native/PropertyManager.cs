namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PropertyManager
    {
        internal Dictionary<string, object> propertyBag = new Dictionary<string, object>();

        private static bool CompareValues<T>(T storage, T value) => 
            Equals(storage, value);

        public T GetProperty<T>(string propertyName)
        {
            object obj2;
            if (this.propertyBag.TryGetValue(propertyName, out obj2))
            {
                return (T) obj2;
            }
            return default(T);
        }

        public bool SetProperty<T>(string propertyName, T value, Action<string> raiseNotification, Action changedCallback)
        {
            T local;
            bool flag = this.SetPropertyCore<T>(propertyName, value, out local);
            if (flag)
            {
                raiseNotification(propertyName);
                Action<Action> action = <>c__1<T>.<>9__1_0;
                if (<>c__1<T>.<>9__1_0 == null)
                {
                    Action<Action> local1 = <>c__1<T>.<>9__1_0;
                    action = <>c__1<T>.<>9__1_0 = delegate (Action x) {
                        x();
                    };
                }
                changedCallback.Do<Action>(action);
            }
            return flag;
        }

        public bool SetProperty<T>(string propertyName, T value, Action<string> raiseNotification, Action<T> changedCallback)
        {
            T oldValue;
            bool flag = this.SetPropertyCore<T>(propertyName, value, out oldValue);
            if (flag)
            {
                raiseNotification(propertyName);
                changedCallback.Do<Action<T>>(delegate (Action<T> x) {
                    x(oldValue);
                });
            }
            return flag;
        }

        public bool SetProperty<T>(ref T storage, T value, string propertyName, Action<string> raiseNotification, Action changedCallback)
        {
            T local = storage;
            bool flag = this.SetPropertyCore<T>(ref storage, value, propertyName);
            if (flag)
            {
                raiseNotification(propertyName);
                if (changedCallback != null)
                {
                    changedCallback();
                }
            }
            return flag;
        }

        public bool SetProperty<T>(ref T storage, T value, string propertyName, Action<string> raiseNotification, Action<T> changedCallback)
        {
            T oldValue = storage;
            bool flag = this.SetPropertyCore<T>(ref storage, value, propertyName);
            if (flag)
            {
                raiseNotification(propertyName);
                changedCallback.Do<Action<T>>(delegate (Action<T> x) {
                    x(oldValue);
                });
            }
            return flag;
        }

        protected virtual bool SetPropertyCore<T>(string propertyName, T value, out T oldValue)
        {
            object obj2;
            oldValue = default(T);
            if (this.propertyBag.TryGetValue(propertyName, out obj2))
            {
                oldValue = (T) obj2;
            }
            if (CompareValues<T>(oldValue, value))
            {
                return false;
            }
            Dictionary<string, object> propertyBag = this.propertyBag;
            lock (propertyBag)
            {
                this.propertyBag[propertyName] = value;
            }
            return true;
        }

        protected virtual bool SetPropertyCore<T>(ref T storage, T value, string propertyName)
        {
            if (CompareValues<T>(storage, value))
            {
                return false;
            }
            storage = value;
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T>
        {
            public static readonly PropertyManager.<>c__1<T> <>9;
            public static Action<Action> <>9__1_0;

            static <>c__1()
            {
                PropertyManager.<>c__1<T>.<>9 = new PropertyManager.<>c__1<T>();
            }

            internal void <SetProperty>b__1_0(Action x)
            {
                x();
            }
        }
    }
}

