namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using System;

    public interface IAsyncListServerDataView
    {
        void BusyChanged(bool busy);
        object GetValueFromProxy(DataProxy proxy);
        void NotifyApply();
        void NotifyCountChanged();
        void NotifyExceptionThrown(ServerModeExceptionThrownEventArgs e);
        void NotifyFindIncrementalCompleted(string text, int startIndex, bool searchNext, bool ignoreStartIndex, int controllerIndex);
        void NotifyInconsistencyDetected(ServerModeInconsistencyDetectedEventArgs e);
        void NotifyLoaded(int listSourceIndex);

        AsyncListWrapper Wrapper { get; }

        DevExpress.Xpf.Editors.Helpers.DataAccessor DataAccessor { get; }
    }
}

