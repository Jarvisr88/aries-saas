namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using System;

    public interface IListServerDataView
    {
        object GetValueFromProxy(DataProxy proxy);
        void NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e);
        void NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        void Reset();

        SyncListWrapper Wrapper { get; }

        DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor { get; }
    }
}

