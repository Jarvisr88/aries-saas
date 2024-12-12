namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public interface IServiceContainer
    {
        void Clear();
        T GetService<T>(ServiceSearchMode searchMode = 0) where T: class;
        T GetService<T>(string key, ServiceSearchMode searchMode = 0) where T: class;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        T GetService<T>(string key, ServiceSearchMode searchMode, out bool serviceHasKey) where T: class;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        object GetService(Type type, string key, ServiceSearchMode searchMode, out bool serviceHasKey);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        IEnumerable<object> GetServices(Type type, bool localOnly);
        void RegisterService(object service, bool yieldToParent = false);
        void RegisterService(string key, object service, bool yieldToParent = false);
        void UnregisterService(object service);
    }
}

