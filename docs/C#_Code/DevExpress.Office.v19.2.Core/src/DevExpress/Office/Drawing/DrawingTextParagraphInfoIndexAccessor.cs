namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextParagraphInfoIndexAccessor : IIndexAccessor<DrawingTextParagraphProperties, DrawingTextParagraphInfo, DocumentModelChangeActions>, IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>
    {
        public bool ApplyDeferredChanges(DrawingTextParagraphProperties owner) => 
            owner.ReplaceInfo<DrawingTextParagraphInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(DrawingTextParagraphProperties owner, DrawingTextParagraphProperties from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<DocumentModelChangeActions> CreateHistoryItem(DrawingTextParagraphProperties owner) => 
            new DrawingTextParagraphInfoIndexChangeHistoryItem(owner);

        public DrawingTextParagraphInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((DrawingTextParagraphPropertiesBatchUpdateHelper) helper).TextParagraphInfo;

        public int GetDeferredInfoIndex(DrawingTextParagraphProperties owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(DrawingTextParagraphProperties owner) => 
            owner.TextParagraphInfoIndex;

        public DrawingTextParagraphInfo GetInfo(DrawingTextParagraphProperties owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<DrawingTextParagraphInfo> GetInfoCache(DrawingTextParagraphProperties owner) => 
            owner.DrawingCache.DrawingTextParagraphInfoCache;

        public int GetInfoIndex(DrawingTextParagraphProperties owner, DrawingTextParagraphInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(DrawingTextParagraphProperties owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(DrawingTextParagraphProperties owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, DrawingTextParagraphInfo info)
        {
            ((DrawingTextParagraphPropertiesBatchUpdateHelper) helper).TextParagraphInfo = info.Clone();
        }

        public void SetIndex(DrawingTextParagraphProperties owner, int value)
        {
            owner.AssignTextParagraphInfoIndex(value);
        }
    }
}

