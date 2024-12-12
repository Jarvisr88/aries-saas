namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.History;
    using System;

    public class Scene3DCameraRotationInfoIndexAccessor : IIndexAccessor<Scene3DProperties, Scene3DRotationInfo, PropertyKey>, IIndexAccessorBase<Scene3DProperties, PropertyKey>
    {
        public bool ApplyDeferredChanges(Scene3DProperties owner) => 
            owner.ReplaceInfo<Scene3DRotationInfo>(this, this.GetDeferredInfo(owner.BatchUpdateHelper), owner.GetBatchUpdateChangeActions());

        public void CopyDeferredInfo(Scene3DProperties owner, Scene3DProperties from)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(from));
        }

        public IndexChangedHistoryItemCore<PropertyKey> CreateHistoryItem(Scene3DProperties owner) => 
            new Scene3DCameraRotationInfoIndexChangeHistoryItem(owner);

        public Scene3DRotationInfo GetDeferredInfo(MultiIndexBatchUpdateHelper helper) => 
            ((Scene3DPropertiesBatchUpdateHelper) helper).CameraRotationInfo;

        public int GetDeferredInfoIndex(Scene3DProperties owner) => 
            this.GetInfoIndex(owner, this.GetDeferredInfo(owner.BatchUpdateHelper));

        public int GetIndex(Scene3DProperties owner) => 
            owner.CameraRotationInfoIndex;

        public Scene3DRotationInfo GetInfo(Scene3DProperties owner) => 
            this.GetInfoCache(owner)[this.GetIndex(owner)];

        private UniqueItemsCache<Scene3DRotationInfo> GetInfoCache(Scene3DProperties owner) => 
            owner.DrawingCache.Scene3DRotationInfoCache;

        public int GetInfoIndex(Scene3DProperties owner, Scene3DRotationInfo value) => 
            this.GetInfoCache(owner).GetItemIndex(value);

        public void InitializeDeferredInfo(Scene3DProperties owner)
        {
            this.SetDeferredInfo(owner.BatchUpdateHelper, this.GetInfo(owner));
        }

        public bool IsIndexValid(Scene3DProperties owner, int index) => 
            index < this.GetInfoCache(owner).Count;

        public void SetDeferredInfo(MultiIndexBatchUpdateHelper helper, Scene3DRotationInfo info)
        {
            ((Scene3DPropertiesBatchUpdateHelper) helper).CameraRotationInfo = info.Clone();
        }

        public void SetIndex(Scene3DProperties owner, int value)
        {
            owner.AssignCameraRotationInfoIndex(value);
        }
    }
}

