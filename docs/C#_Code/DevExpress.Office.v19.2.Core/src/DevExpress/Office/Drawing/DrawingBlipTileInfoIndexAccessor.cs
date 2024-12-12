namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingBlipTileInfoIndexAccessor : IIndexAccessor<DrawingBlipFill, DrawingBlipTileInfo, PropertyKey>, IIndexAccessorBase<DrawingBlipFill, PropertyKey>
    {
        public bool ApplyDeferredChanges(DrawingBlipFill owner) => 
            owner.ReplaceInfo<DrawingBlipTileInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(DrawingBlipFill owner, DrawingBlipFill from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<PropertyKey> CreateHistoryItem(DrawingBlipFill owner) => 
            new DrawingBlipTileInfoIndexChangeHistoryItem(owner);

        public DrawingBlipTileInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((DrawingBlipFillBatchUpdateHelper) helper).BlipTileInfo;

        public int GetDeferredInfoIndex(DrawingBlipFill owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(DrawingBlipFill owner) => 
            owner.TileInfoIndex;

        public DrawingBlipTileInfo GetInfo(DrawingBlipFill owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<DrawingBlipTileInfo> GetInfoCache(DrawingBlipFill owner) => 
            owner.DrawingCache.DrawingBlipTileInfoCache;

        public int GetInfoIndex(DrawingBlipFill owner, DrawingBlipTileInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(DrawingBlipFill owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(DrawingBlipFill owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, DrawingBlipTileInfo info)
        {
            ((DrawingBlipFillBatchUpdateHelper) helper).BlipTileInfo = info.Clone();
        }

        public void SetIndex(DrawingBlipFill owner, int value)
        {
            owner.AssignTileInfoIndex(value);
        }
    }
}

