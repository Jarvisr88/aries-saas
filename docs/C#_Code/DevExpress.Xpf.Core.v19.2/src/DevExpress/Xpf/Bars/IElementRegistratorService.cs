namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IElementRegistratorService
    {
        void Changed(IBarNameScopeSupport element, object registratorKey);
        IEnumerable<TRegistratorKey> GetElements<TRegistratorKey>();
        IEnumerable<TRegistratorKey> GetElements<TRegistratorKey>(ScopeSearchSettings searchMode);
        IEnumerable<TRegistratorKey> GetElements<TRegistratorKey>(object name);
        IEnumerable<TRegistratorKey> GetElements<TRegistratorKey>(object name, ScopeSearchSettings searchMode);
        void NameChanged(IBarNameScopeSupport element, object registratorKey, object oldName, object newName, bool skipNameEqualityCheck = false);
    }
}

