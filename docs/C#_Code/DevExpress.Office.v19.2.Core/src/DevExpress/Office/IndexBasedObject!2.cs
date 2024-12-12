namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class IndexBasedObject<TInfo, TOptions> : IBatchUpdateable, IBatchUpdateHandler, ISupportsSizeOf where TInfo: ICloneable<TInfo>, ISupportsCopyFrom<TInfo>, ISupportsSizeOf where TOptions: ICloneable<TOptions>, ISupportsCopyFrom<TOptions>, ISupportsSizeOf
    {
        private readonly IDocumentModel documentModel;
        private BatchUpdateHelper<TInfo, TOptions> batchUpdateHelper;
        private int infoIndex;
        private int optionsIndex;

        protected IndexBasedObject(IDocumentModel documentModel, int formattingInfoIndex, int formattingOptionsIndex)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            if (!this.InfoCache.IsIndexValid(formattingInfoIndex))
            {
                Exceptions.ThrowArgumentException("formattingInfoIndex", formattingInfoIndex);
            }
            if (!this.OptionsCache.IsIndexValid(formattingOptionsIndex))
            {
                Exceptions.ThrowArgumentException("formattingOptionsIndex", formattingOptionsIndex);
            }
            this.infoIndex = formattingInfoIndex;
            this.optionsIndex = formattingOptionsIndex;
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper ??= new BatchUpdateHelper<TInfo, TOptions>(this);
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelUpdate()
        {
            this.batchUpdateHelper.CancelUpdate();
        }

        public void CopyFrom(IndexBasedObject<TInfo, TOptions> obj)
        {
            this.CopyFromCore(obj.Info, obj.Options);
        }

        public void CopyFromCore(TInfo newInfo, TOptions newOptions)
        {
            TInfo infoForModification = this.GetInfoForModification();
            TOptions optionsForModification = this.GetOptionsForModification();
            infoForModification.CopyFrom(newInfo);
            optionsForModification.CopyFrom(newOptions);
            this.ReplaceInfo(infoForModification, optionsForModification);
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
            this.batchUpdateHelper.DeferredInfoNotifications = this.InfoCore.Clone();
            this.batchUpdateHelper.DeferredOptionsNotifications = this.OptionsCore.Clone();
        }

        void IBatchUpdateHandler.OnLastCancelUpdate()
        {
            this.batchUpdateHelper = null;
        }

        void IBatchUpdateHandler.OnLastEndUpdate()
        {
            this.ReplaceInfo(this.batchUpdateHelper.DeferredInfoNotifications, this.batchUpdateHelper.DeferredOptionsNotifications);
            this.batchUpdateHelper = null;
        }

        public void EndUpdate()
        {
            this.batchUpdateHelper.EndUpdate();
        }

        public override bool Equals(object obj)
        {
            IndexBasedObject<TInfo, TOptions> other = obj as IndexBasedObject<TInfo, TOptions>;
            return ((other != null) ? (!ReferenceEquals(other.DocumentModel, this.DocumentModel) ? this.PropertyEquals(other) : ((this.InfoIndex == other.InfoIndex) && (this.OptionsIndex == other.OptionsIndex))) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.Calculate(this.InfoIndex, this.OptionsIndex);

        public TInfo GetInfoForModification() => 
            !this.IsUpdateLocked ? this.Info.Clone() : this.batchUpdateHelper.DeferredInfoNotifications;

        protected internal int GetInfoIndex(TInfo value) => 
            this.InfoCache.GetItemIndex(value);

        public TOptions GetOptionsForModification() => 
            !this.IsUpdateLocked ? this.Options.Clone() : this.batchUpdateHelper.DeferredOptionsNotifications;

        protected internal int GetOptionsIndex(TOptions value) => 
            this.OptionsCache.GetItemIndex(value);

        protected internal void OnChanged()
        {
        }

        protected internal void OnChanging()
        {
        }

        protected abstract bool PropertyEquals(IndexBasedObject<TInfo, TOptions> other);
        public void ReplaceInfo(TInfo newInfo, TOptions newOptions)
        {
            if (!this.IsUpdateLocked)
            {
                this.ReplaceInfoCore(this.GetInfoIndex(newInfo), this.GetOptionsIndex(newOptions));
            }
        }

        protected void ReplaceInfoCore(int newInfoIndex, int newOptionsIndex)
        {
            this.optionsIndex = newOptionsIndex;
            this.infoIndex = newInfoIndex;
        }

        public void SetIndexInitial(int infoIndex, int optionsIndex)
        {
            this.infoIndex = infoIndex;
            this.optionsIndex = optionsIndex;
        }

        protected internal virtual void SetPropertyValue<U>(SetPropertyValueDelegate<TInfo, TOptions, U> setter, U newValue, SetOptionsValueDelegate<TInfo, TOptions> optionsSetter)
        {
            TInfo infoForModification = this.GetInfoForModification();
            TOptions optionsForModification = this.GetOptionsForModification();
            setter(infoForModification, newValue);
            optionsSetter(optionsForModification, true);
            this.ReplaceInfo(infoForModification, optionsForModification);
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public TInfo Info =>
            this.IsUpdateLocked ? this.batchUpdateHelper.DeferredInfoNotifications : this.InfoCore;

        public TOptions Options =>
            this.IsUpdateLocked ? this.batchUpdateHelper.DeferredOptionsNotifications : this.OptionsCore;

        public IDocumentModel DocumentModel =>
            this.documentModel;

        protected internal TInfo InfoCore =>
            this.InfoCache[this.InfoIndex];

        protected internal TOptions OptionsCore =>
            this.OptionsCache[this.OptionsIndex];

        protected internal abstract UniqueItemsCache<TInfo> InfoCache { get; }

        protected internal abstract UniqueItemsCache<TOptions> OptionsCache { get; }

        public int InfoIndex
        {
            get => 
                this.infoIndex;
            set => 
                this.infoIndex = value;
        }

        public int OptionsIndex
        {
            get => 
                this.optionsIndex;
            set => 
                this.optionsIndex = value;
        }

        BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            (this.batchUpdateHelper != null) && this.batchUpdateHelper.IsUpdateLocked;

        private protected delegate void SetOptionsValueDelegate(TOptions options, bool newValue);

        private protected delegate void SetPropertyValueDelegate<U>(TInfo info, U newValue);
    }
}

