namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public interface IServerModeCollectionView : ICollectionView, IEnumerable, INotifyCollectionChanged
    {
        void CancelItem(int visibleIndex);
        void FetchItem(int visibleIndex);
        object GetItem(int visibleIndex);
        int IndexOfValue(object value);
        bool IsTempItem(int visibleIndex);
        IDisposable LockWhileUpdatingSelection(IEnumerable<int> allowedIndices);
    }
}

