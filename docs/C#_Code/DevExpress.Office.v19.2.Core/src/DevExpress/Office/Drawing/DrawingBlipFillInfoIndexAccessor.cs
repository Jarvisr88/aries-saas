namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingBlipFillInfoIndexAccessor : IIndexAccessor<DrawingBlipFill, DrawingBlipFillInfo, PropertyKey>, IIndexAccessorBase<DrawingBlipFill, PropertyKey>
    {
        public bool ApplyDeferredChanges(DrawingBlipFill owner) => 
            owner.ReplaceInfo<DrawingBlipFillInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(DrawingBlipFill owner, DrawingBlipFill from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<PropertyKey> CreateHistoryItem(DrawingBlipFill owner) => 
            new DrawingBlipFillInfoIndexChangeHistoryItem(owner);

        public DrawingBlipFillInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((DrawingBlipFillBatchUpdateHelper) helper).BlipFillInfo;

        public int GetDeferredInfoIndex(DrawingBlipFill owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(DrawingBlipFill owner) => 
            owner.FillInfoIndex;

        public DrawingBlipFillInfo GetInfo(DrawingBlipFill owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<DrawingBlipFillInfo> GetInfoCache(DrawingBlipFill owner) => 
            owner.DrawingCache.DrawingBlipFillInfoCache;

        public int GetInfoIndex(DrawingBlipFill owner, DrawingBlipFillInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(DrawingBlipFill owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(DrawingBlipFill owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, DrawingBlipFillInfo info)
        {
            ((DrawingBlipFillBatchUpdateHelper) helper).BlipFillInfo = info.Clone();
        }

        public void SetIndex(DrawingBlipFill owner, int value)
        {
            owner.AssignFillInfoIndex(value);
        }
    }
}

