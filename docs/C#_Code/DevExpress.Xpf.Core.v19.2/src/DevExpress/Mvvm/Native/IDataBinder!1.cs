namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDataBinder<T> : IDisposable
    {
        IDataBinder<T> BindOneTime(string property, string[] path, object fallbackValue = null);
        IDataBinder<T> BindOneTime(string property, string[] path, Action<T, object> set, object fallbackValue = null);
        IDataBinder<T> BindOneTimeToSource(string property, string[] path, object fallbackValue = null);
        IDataBinder<T> BindOneTimeToSource(string property, string[] path, Func<T, Type, object> get, object fallbackValue = null);
        IDataBinder<T> BindOneWay(string property, string[] path, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindOneWay(string property, string[] path, Action<T, object> set, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindOneWayExplicit(string property, string[] path, object fallbackValue = null, bool syncFirstTime = false);
        IDataBinder<T> BindOneWayExplicit(string property, string[] path, Action<T, object> set, object fallbackValue = null, bool syncFirstTime = false);
        IDataBinder<T> BindOneWayToSource(string property, string[] path, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindOneWayToSource(string property, string[] path, Func<T, Type, object> get, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindTwoWay(string property, string[] path, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindTwoWay(string property, string[] path, Func<T, Type, object> get, Action<T, object> set, object fallbackValue = null, bool syncFirstTime = true);
        IDataBinder<T> BindTwoWayExplicit(string property, string[] path, object fallbackValue = null, bool syncFirstTime = false);
        IDataBinder<T> BindTwoWayExplicit(string property, string[] path, Func<T, Type, object> get, Action<T, object> set, object fallbackValue = null, bool syncFirstTime = false);
        bool IsPropertyBound(string property);
        void RestoreTargetUpdates();
        void SuppressTargetUpdates();
        void UnbindProperty(string property);
        void UpdateSource(string property);
        void UpdateTarget(string property);
    }
}

