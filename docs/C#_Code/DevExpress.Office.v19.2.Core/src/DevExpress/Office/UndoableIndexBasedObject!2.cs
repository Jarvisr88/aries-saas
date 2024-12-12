namespace DevExpress.Office
{
    using DevExpress.Office.History;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class UndoableIndexBasedObject<T, TActions> : IIndexBasedObject<TActions>, IBatchUpdateable, IBatchUpdateHandler, IBatchInit, IBatchInitHandler, ISupportsSizeOf where T: ICloneable<T>, ISupportsCopyFrom<T>, ISupportsSizeOf where TActions: struct
    {
        private readonly IDocumentModelPart documentModelPart;
        private int index;
        private BatchUpdateHelper<T> batchUpdateHelper;

        protected UndoableIndexBasedObject(IDocumentModelPart documentModelPart)
        {
            Guard.ArgumentNotNull(documentModelPart, "documentModelPart");
            this.documentModelPart = documentModelPart;
        }

        protected internal abstract void ApplyChanges(TActions changeActions);
        public void BeginInit()
        {
            this.batchUpdateHelper ??= new BatchInitHelper<T>(this);
            if (!(this.batchUpdateHelper is BatchInitHelper<T>))
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidBeginInit);
            }
            this.batchUpdateHelper.BeginUpdate();
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper ??= new BatchUpdateHelper<T>(this);
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelInit()
        {
            if (!(this.batchUpdateHelper is BatchInitHelper<T>))
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

        private bool ChangeIndex(int newIndex, TActions changeActions)
        {
            IDocumentModel documentModel = this.DocumentModel;
            if (newIndex == this.Index)
            {
                return false;
            }
            this.ChangeIndexCore(newIndex, changeActions);
            return true;
        }

        public virtual void ChangeIndexCore(int newIndex, TActions changeActions)
        {
            IDocumentModel documentModel = this.DocumentModel;
            documentModel.BeginUpdate();
            try
            {
                this.OnBeginAssign();
                try
                {
                    IndexChangedHistoryItemCore<TActions> item = this.CreateIndexChangedHistoryItem();
                    item.OldIndex = this.index;
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

        public virtual void CopyFrom(UndoableIndexBasedObject<T, TActions> obj)
        {
            if (!ReferenceEquals(obj.DocumentModel, this.DocumentModel))
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidCopyFromDocumentModel);
            }
            if (this.IsUpdateLocked || obj.IsUpdateLocked)
            {
                this.CopyFrom(obj.Info);
            }
            else if (this.Index != obj.Index)
            {
                this.ChangeIndex(obj.Index, this.GetBatchUpdateChangeActions());
            }
            else
            {
                this.NotifyFakeAssign();
            }
        }

        public void CopyFrom(T newInfo)
        {
            T infoForModification = this.GetInfoForModification();
            infoForModification.CopyFrom(newInfo);
            if (!this.ReplaceInfo(infoForModification, this.GetBatchUpdateChangeActions()))
            {
                this.NotifyFakeAssign();
            }
        }

        public void CopyFromCore(UndoableIndexBasedObject<T, TActions> obj)
        {
            if (this.Index != obj.Index)
            {
                ((IIndexBasedObject<TActions>) this).SetIndex(obj.Index, this.GetBatchUpdateChangeActions());
            }
        }

        protected virtual IndexChangedHistoryItemCore<TActions> CreateIndexChangedHistoryItem() => 
            new IndexChangedHistoryItem<TActions>(this.DocumentModelPart, this);

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
            this.batchUpdateHelper.DeferredNotifications = this.InfoCore.Clone();
        }

        void IBatchInitHandler.OnLastCancelInit()
        {
            if (this.batchUpdateHelper.IsIndexRecalculationOnEndInitEnabled)
            {
                this.index = this.GetInfoIndex(this.batchUpdateHelper.DeferredNotifications);
            }
            this.batchUpdateHelper = null;
        }

        void IBatchInitHandler.OnLastEndInit()
        {
            if (this.batchUpdateHelper.IsIndexRecalculationOnEndInitEnabled)
            {
                this.index = this.GetInfoIndex(this.batchUpdateHelper.DeferredNotifications);
            }
            this.batchUpdateHelper = null;
        }

        int IIndexBasedObject<TActions>.GetIndex() => 
            this.index;

        void IIndexBasedObject<TActions>.SetIndex(int value, TActions changeActions)
        {
            if (this.index != value)
            {
                this.OnIndexChanging();
                this.index = value;
                this.ApplyChanges(changeActions);
                this.OnIndexChanged();
            }
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
            if (!(this.batchUpdateHelper is BatchInitHelper<T>))
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndInit);
            }
            this.batchUpdateHelper.EndUpdate();
        }

        public void EndUpdate()
        {
            this.batchUpdateHelper.EndUpdate();
        }

        public abstract TActions GetBatchUpdateChangeActions();
        protected internal abstract UniqueItemsCache<T> GetCache(IDocumentModel documentModel);
        public T GetInfoForModification() => 
            !this.IsUpdateLocked ? this.Info.Clone() : this.batchUpdateHelper.DeferredNotifications;

        public int GetInfoIndex(T value) => 
            this.GetCache(this.DocumentModel).GetItemIndex(value);

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
            this.batchUpdateHelper.DeferredNotifications = this.InfoCore.Clone();
        }

        protected internal virtual void OnIndexChanged()
        {
        }

        protected internal virtual void OnIndexChanging()
        {
        }

        protected internal virtual void OnLastCancelUpdateCore()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndUpdate);
            }
            this.batchUpdateHelper = null;
        }

        protected internal virtual void OnLastEndUpdateCore()
        {
            if (this.batchUpdateHelper == null)
            {
                Exceptions.ThrowInvalidOperationException(OfficeStringId.Msg_InvalidEndUpdate);
            }
            if (!this.ReplaceInfo(this.batchUpdateHelper.DeferredNotifications, this.GetBatchUpdateChangeActions()) && this.batchUpdateHelper.FakeAssignDetected)
            {
                this.NotifyFakeAssign();
            }
            this.batchUpdateHelper = null;
        }

        public bool ReplaceInfo(T newValue, TActions changeActions)
        {
            if (this.IsUpdateLocked)
            {
                return false;
            }
            int infoIndex = this.GetInfoIndex(newValue);
            return this.ChangeIndex(infoIndex, changeActions);
        }

        protected internal void ResumeDirectNotifications()
        {
            this.batchUpdateHelper.ResumeDirectNotifications();
        }

        protected internal void ResumeIndexRecalculationOnEndInit()
        {
            this.batchUpdateHelper.ResumeIndexRecalculationOnEndInit();
        }

        public void SetIndexInitial(int value)
        {
            this.index = value;
        }

        protected internal virtual void SetPropertyValue<U>(SetPropertyValueDelegate<T, TActions, U> setter, U newValue)
        {
            T infoForModification = this.GetInfoForModification();
            this.ReplaceInfo(infoForModification, setter(infoForModification, newValue));
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public void SuppressDirectNotifications()
        {
            this.batchUpdateHelper.SuppressDirectNotifications();
        }

        public void SuppressIndexRecalculationOnEndInit()
        {
            this.batchUpdateHelper.SuppressIndexRecalculationOnEndInit();
        }

        public T Info =>
            this.IsUpdateLocked ? this.batchUpdateHelper.DeferredNotifications : this.InfoCore;

        protected T DeferredInfo =>
            this.batchUpdateHelper.DeferredNotifications;

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;

        public IDocumentModel DocumentModel =>
            this.documentModelPart.DocumentModel;

        public T InfoCore =>
            this.GetCache(this.DocumentModel)[this.Index];

        public int Index =>
            this.index;

        protected internal bool IsDirectNotificationsEnabled =>
            !this.IsUpdateLocked || this.batchUpdateHelper.IsDirectNotificationsEnabled;

        protected internal bool IsIndexRecalculationOnEndInitEnabled =>
            !this.IsUpdateLocked || this.batchUpdateHelper.IsIndexRecalculationOnEndInitEnabled;

        BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            (this.batchUpdateHelper != null) && this.batchUpdateHelper.IsUpdateLocked;

        private protected delegate TActions SetPropertyValueDelegate<U>(T info, U newValue);
    }
}

