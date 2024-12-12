namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using System;

    public interface IIndexAccessorBase<TOwner, TActions> where TOwner: MultiIndexObject<TOwner, TActions> where TActions: struct
    {
        bool ApplyDeferredChanges(TOwner owner);
        void CopyDeferredInfo(TOwner owner, TOwner from);
        IndexChangedHistoryItemCore<TActions> CreateHistoryItem(TOwner owner);
        int GetDeferredInfoIndex(TOwner owner);
        int GetIndex(TOwner owner);
        void InitializeDeferredInfo(TOwner owner);
        bool IsIndexValid(TOwner owner, int index);
        void SetIndex(TOwner owner, int value);
    }
}

