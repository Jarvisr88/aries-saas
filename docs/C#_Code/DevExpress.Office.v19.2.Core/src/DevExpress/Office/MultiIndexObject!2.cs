namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class MultiIndexObject<TOwner, TActions> : IBatchUpdateable, IBatchInit, IBatchUpdateHandler, IBatchInitHandler, ISupportsSizeOf where TOwner: MultiIndexObject<TOwner, TActions> where TActions: struct
    {
        private MultiIndexBatchUpdateHelper batchUpdateHelper;

        protected MultiIndexObject()
        {
        }

        protected internal abstract void ApplyChanges(TActions actions);
        public void BeginInit()
        {
            this.batchUpdateHelper ??= this.CreateBatchInitHelper();
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidBeginInit);
            }
            this.batchUpdateHelper.BeginUpdate();
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper ??= this.CreateBatchUpdateHelper();
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidBeginUpdate);
            }
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelInit()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndInit);
            }
            this.batchUpdateHelper.CancelUpdate();
        }

        public void CancelUpdate()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndUpdate);
            }
            this.batchUpdateHelper.CancelUpdate();
        }

        private bool ChangeIndex(IIndexAccessorBase<TOwner, TActions> indexHolder, int newIndex, TActions changeActions)
        {
            if (newIndex == indexHolder.GetIndex(this.GetOwner()))
            {
                return false;
            }
            this.ChangeIndexCore(indexHolder, newIndex, changeActions);
            return true;
        }

        public virtual void ChangeIndexCore(IIndexAccessorBase<TOwner, TActions> indexHolder, int newIndex, TActions changeActions)
        {
            IDocumentModel documentModel = this.DocumentModel;
            documentModel.BeginUpdate();
            try
            {
                this.OnBeginAssign();
                try
                {
                    IndexChangedHistoryItemCore<TActions> item = indexHolder.CreateHistoryItem(this.GetOwner());
                    item.OldIndex = indexHolder.GetIndex(this.GetOwner());
                    item.NewIndex = newIndex;
                    item.ChangeActions = changeActions;
                    documentModel.History.Add(item);
                    item.Execute();
                }
                finally
                {
                    this.OnEndAssign();
                }
            }
            finally
            {
                documentModel.EndUpdate();
            }
        }

        public void CloneCore(TOwner clone)
        {
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                IIndexAccessorBase<TOwner, TActions> base2 = this.IndexAccessors[i];
                base2.SetIndex(clone, base2.GetIndex(this.GetOwner()));
            }
        }

        public virtual void CopyFrom(MultiIndexObject<TOwner, TActions> obj)
        {
            if (ReferenceEquals(this.DocumentModel, obj.DocumentModel))
            {
                this.CopyFromSameModel(obj);
            }
            else
            {
                try
                {
                    this.BeginUpdate();
                    this.CopyFromDeferred(obj);
                }
                finally
                {
                    this.EndUpdate();
                }
            }
        }

        public virtual void CopyFromDeferred(MultiIndexObject<TOwner, TActions> obj)
        {
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                this.IndexAccessors[i].CopyDeferredInfo(this.GetOwner(), obj.GetOwner());
            }
        }

        private void CopyFromSameModel(MultiIndexObject<TOwner, TActions> obj)
        {
            if (!ReferenceEquals(this.DocumentModel, obj.DocumentModel))
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidCopyFromDocumentModel);
            }
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                if (!this.ChangeIndex(this.IndexAccessors[i], obj.IndexAccessors[i].GetIndex(obj.GetOwner()), this.GetBatchUpdateChangeActions()))
                {
                    this.NotifyFakeAssign();
                }
            }
        }

        protected abstract MultiIndexBatchUpdateHelper CreateBatchInitHelper();
        protected abstract MultiIndexBatchUpdateHelper CreateBatchUpdateHelper();
        void IBatchInitHandler.OnBeginInit()
        {
        }

        void IBatchInitHandler.OnCancelInit()
        {
        }

        void IBatchInitHandler.OnEndInit()
        {
        }

        void IBatchInitHandler.OnFirstBeginInit()
        {
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                this.IndexAccessors[i].InitializeDeferredInfo(this.GetOwner());
            }
        }

        void IBatchInitHandler.OnLastCancelInit()
        {
            this.OnLastEndInitCore();
        }

        void IBatchInitHandler.OnLastEndInit()
        {
            this.OnLastEndInitCore();
        }

        void IBatchUpdateHandler.OnBeginUpdate()
        {
        }

        void IBatchUpdateHandler.OnCancelUpdate()
        {
        }

        void IBatchUpdateHandler.OnEndUpdate()
        {
        }

        void IBatchUpdateHandler.OnFirstBeginUpdate()
        {
            this.OnFirstBeginUpdateCore();
        }

        void IBatchUpdateHandler.OnLastCancelUpdate()
        {
            this.OnLastCancelUpdateCore();
        }

        void IBatchUpdateHandler.OnLastEndUpdate()
        {
            this.OnLastEndUpdateCore();
        }

        public void EndInit()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndInit);
            }
            this.batchUpdateHelper.EndUpdate();
        }

        public void EndUpdate()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndUpdate);
            }
            this.batchUpdateHelper.EndUpdate();
        }

        public override bool Equals(object obj)
        {
            MultiIndexObject<TOwner, TActions> obj2 = obj as MultiIndexObject<TOwner, TActions>;
            if (obj2 == null)
            {
                return false;
            }
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                IIndexAccessorBase<TOwner, TActions> base2 = this.IndexAccessors[i];
                if (base2.GetIndex(this.GetOwner()) != base2.GetIndex(obj2.GetOwner()))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract TActions GetBatchUpdateChangeActions();
        protected abstract IDocumentModel GetDocumentModel();
        public override int GetHashCode()
        {
            CombinedHashCode code = new CombinedHashCode();
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                code.AddInt(this.IndexAccessors[i].GetIndex(this.GetOwner()));
            }
            return code.CombinedHash32;
        }

        public virtual TInfo GetInfoForModification<TInfo>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder) where TInfo: ICloneable<TInfo>, ISupportsSizeOf => 
            !this.IsUpdateLocked ? indexHolder.GetInfo(this.GetOwner()).Clone() : indexHolder.GetDeferredInfo(this.BatchUpdateHelper);

        public abstract TOwner GetOwner();
        protected internal virtual void NotifyFakeAssign()
        {
            if (this.IsUpdateLocked)
            {
                this.batchUpdateHelper.FakeAssignDetected = true;
            }
            else
            {
                this.DocumentModel.BeginUpdate();
                try
                {
                    this.OnBeginAssign();
                    try
                    {
                    }
                    finally
                    {
                        this.OnEndAssign();
                    }
                }
                finally
                {
                    this.DocumentModel.EndUpdate();
                }
            }
        }

        protected internal virtual void OnBeginAssign()
        {
        }

        protected internal virtual void OnEndAssign()
        {
        }

        protected internal virtual void OnFirstBeginUpdateCore()
        {
            this.batchUpdateHelper.Transaction = new HistoryTransaction(this.DocumentModel.History);
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                this.IndexAccessors[i].InitializeDeferredInfo(this.GetOwner());
            }
            this.BatchUpdateHelper.BeginUpdateDeferredChanges();
        }

        protected internal virtual void OnLastCancelUpdateCore()
        {
            this.BatchUpdateHelper.CancelUpdateDeferredChanges();
            this.batchUpdateHelper.Transaction.Dispose();
            this.batchUpdateHelper = null;
        }

        private void OnLastEndInitCore()
        {
            if (this.batchUpdateHelper.IsIndexRecalculationOnEndInitEnabled)
            {
                int length = this.IndexAccessors.Length;
                for (int i = 0; i < length; i++)
                {
                    this.IndexAccessors[i].SetIndex(this.GetOwner(), this.IndexAccessors[i].GetDeferredInfoIndex(this.GetOwner()));
                }
            }
            this.batchUpdateHelper = null;
        }

        protected internal virtual void OnLastEndUpdateCore()
        {
            this.BatchUpdateHelper.EndUpdateDeferredChanges();
            bool flag = true;
            int length = this.IndexAccessors.Length;
            for (int i = 0; i < length; i++)
            {
                flag = !this.IndexAccessors[i].ApplyDeferredChanges(this.GetOwner()) & flag;
            }
            if (flag && this.batchUpdateHelper.FakeAssignDetected)
            {
                this.NotifyFakeAssign();
            }
            this.batchUpdateHelper.Transaction.Dispose();
            this.batchUpdateHelper = null;
        }

        public virtual bool ReplaceInfo<TInfo>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder, TInfo newValue, TActions changeActions) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            if (this.IsUpdateLocked)
            {
                return false;
            }
            int infoIndex = indexHolder.GetInfoIndex(this.GetOwner(), newValue);
            return this.ChangeIndex(indexHolder, infoIndex, changeActions);
        }

        public virtual bool ReplaceInfoForFlags<TInfo>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder, TInfo newValue, TActions changeActions) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            if (this.IsUpdateLocked)
            {
                indexHolder.SetDeferredInfo(this.BatchUpdateHelper, newValue);
                return false;
            }
            int infoIndex = indexHolder.GetInfoIndex(this.GetOwner(), newValue);
            return this.ChangeIndex(indexHolder, infoIndex, changeActions);
        }

        public void SetIndexCore(IIndexAccessorBase<TOwner, TActions> indexHolder, int value, TActions changeActions)
        {
            if (indexHolder.GetIndex(this.GetOwner()) != value)
            {
                indexHolder.SetIndex(this.GetOwner(), value);
                this.ApplyChanges(changeActions);
            }
        }

        protected internal virtual void SetPropertyValue<TInfo, U>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder, SetPropertyValueDelegate<TOwner, TActions, TInfo, U> setter, U newValue) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            this.DocumentModel.BeginUpdate();
            try
            {
                this.SetPropertyValueCore<TInfo, U>(indexHolder, setter, newValue);
            }
            finally
            {
                this.DocumentModel.EndUpdate();
            }
        }

        protected virtual void SetPropertyValueCore<TInfo, U>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder, SetPropertyValueDelegate<TOwner, TActions, TInfo, U> setter, U newValue) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            TInfo infoForModification = this.GetInfoForModification<TInfo>(indexHolder);
            this.ReplaceInfo<TInfo>(indexHolder, infoForModification, setter(infoForModification, newValue));
        }

        protected internal virtual void SetPropertyValueForStruct<TInfo, U>(IIndexAccessor<TOwner, TInfo, TActions> indexHolder, SetPropertyValueDelegateForStruct<TOwner, TActions, TInfo, U> setter, U newValue) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            TInfo infoForModification = this.GetInfoForModification<TInfo>(indexHolder);
            this.ReplaceInfoForFlags<TInfo>(indexHolder, infoForModification, setter(ref infoForModification, newValue));
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        protected internal virtual int ValidateNewIndexBeforeReplaceInfo(int newValue) => 
            newValue;

        public IDocumentModel DocumentModel =>
            this.GetDocumentModel();

        DevExpress.Utils.BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        protected MultiIndexBatchUpdateHelper BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            (this.batchUpdateHelper != null) && this.batchUpdateHelper.IsUpdateLocked;

        protected abstract IIndexAccessorBase<TOwner, TActions>[] IndexAccessors { get; }

        public int IndexAccessorsCount =>
            this.IndexAccessors.Length;

        private protected delegate TActions SetPropertyValueDelegate<TInfo, U>(TInfo info, U newValue);

        private protected delegate TActions SetPropertyValueDelegateForStruct<TInfo, U>(ref TInfo info, U newValue);
    }
}

