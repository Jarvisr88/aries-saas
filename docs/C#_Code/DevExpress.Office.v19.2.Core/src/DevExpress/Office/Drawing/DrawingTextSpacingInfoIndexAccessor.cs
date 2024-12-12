namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextSpacingInfoIndexAccessor : IIndexAccessor<DrawingTextParagraphProperties, DrawingTextSpacingInfo, DocumentModelChangeActions>, IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>
    {
        private int index;

        public DrawingTextSpacingInfoIndexAccessor(int index)
        {
            this.index = index;
        }

        public bool ApplyDeferredChanges(DrawingTextParagraphProperties owner) => 
            owner.ReplaceInfo<DrawingTextSpacingInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(DrawingTextParagraphProperties owner, DrawingTextParagraphProperties from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<DocumentModelChangeActions> CreateHistoryItem(DrawingTextParagraphProperties owner) => 
            new DrawingTextSpacingInfoIndexChangeHistoryItem(owner, this.index);

        public DrawingTextSpacingInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((DrawingTextParagraphPropertiesBatchUpdateHelper) helper).TextSpacingInfos[this.index];

        public int GetDeferredInfoIndex(DrawingTextParagraphProperties owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(DrawingTextParagraphProperties owner) => 
            owner.TextSpacingInfoIndexes[this.index];

        public DrawingTextSpacingInfo GetInfo(DrawingTextParagraphProperties owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<DrawingTextSpacingInfo> GetInfoCache(DrawingTextParagraphProperties owner) => 
            owner.DrawingCache.DrawingTextSpacingInfoCache;

        public int GetInfoIndex(DrawingTextParagraphProperties owner, DrawingTextSpacingInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(DrawingTextParagraphProperties owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(DrawingTextParagraphProperties owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, DrawingTextSpacingInfo info)
        {
            ((DrawingTextParagraphPropertiesBatchUpdateHelper) helper).TextSpacingInfos[this.index] = info.Clone();
        }

        public void SetIndex(DrawingTextParagraphProperties owner, int value)
        {
            owner.AssignTextSpacingInfoIndex(this.index, value);
        }
    }
}

