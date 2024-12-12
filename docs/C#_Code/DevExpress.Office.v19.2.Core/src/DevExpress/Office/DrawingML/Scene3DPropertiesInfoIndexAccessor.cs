namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.History;
    using System;

    public class Scene3DPropertiesInfoIndexAccessor : IIndexAccessor<Scene3DProperties, Scene3DPropertiesInfo, PropertyKey>, IIndexAccessorBase<Scene3DProperties, PropertyKey>
    {
        public bool ApplyDeferredChanges(Scene3DProperties owner) => 
            owner.ReplaceInfo<Scene3DPropertiesInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(Scene3DProperties owner, Scene3DProperties from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<PropertyKey> CreateHistoryItem(Scene3DProperties owner) => 
            new Scene3DPropertiesInfoIndexChangeHistoryItem(owner);

        public Scene3DPropertiesInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((Scene3DPropertiesBatchUpdateHelper) helper).Info;

        public int GetDeferredInfoIndex(Scene3DProperties owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(Scene3DProperties owner) => 
            owner.InfoIndex;

        public Scene3DPropertiesInfo GetInfo(Scene3DProperties owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<Scene3DPropertiesInfo> GetInfoCache(Scene3DProperties owner) => 
            owner.DrawingCache.Scene3DPropertiesInfoCache;

        public int GetInfoIndex(Scene3DProperties owner, Scene3DPropertiesInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(Scene3DProperties owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(Scene3DProperties owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, Scene3DPropertiesInfo info)
        {
            ((Scene3DPropertiesBatchUpdateHelper) helper).Info = info.Clone();
        }

        public void SetIndex(Scene3DProperties owner, int value)
        {
            owner.AssignInfoIndex(value);
        }
    }
}

