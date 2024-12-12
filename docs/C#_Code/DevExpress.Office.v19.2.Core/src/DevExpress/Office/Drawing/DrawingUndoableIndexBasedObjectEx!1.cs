namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public abstract class DrawingUndoableIndexBasedObjectEx<T> : UndoableIndexBasedObject<T, PropertyKey>, IOfficeNotifyPropertyChanged where T: ICloneable<T>, ISupportsCopyFrom<T>, ISupportsSizeOf
    {
        private readonly PropertyChangedNotifier notifier;
        private InvalidateProxy innerParent;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        protected DrawingUndoableIndexBasedObjectEx(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
            this.innerParent = new InvalidateProxy();
            this.notifier = new PropertyChangedNotifier(this);
        }

        protected internal override void ApplyChanges(PropertyKey propertyKey)
        {
            this.InvalidateParent();
            this.Notifier.OnPropertyChanged(propertyKey);
        }

        public void AssignInfo(T info)
        {
            base.SetIndexInitial(this.GetCache(base.DocumentModel).AddItem(info));
        }

        public virtual void CopyFrom(DrawingUndoableIndexBasedObjectEx<T> sourceItem)
        {
            if (ReferenceEquals(sourceItem.DocumentModel, base.DocumentModel))
            {
                base.CopyFrom(sourceItem);
            }
            else
            {
                base.BeginUpdate();
                sourceItem.BeginUpdate();
                try
                {
                    base.Info.CopyFrom(sourceItem.Info);
                }
                finally
                {
                    base.EndUpdate();
                    sourceItem.EndUpdate();
                }
            }
        }

        protected override IndexChangedHistoryItemCore<PropertyKey> CreateIndexChangedHistoryItem() => 
            base.CreateIndexChangedHistoryItem();

        public override PropertyKey GetBatchUpdateChangeActions() => 
            PropertyKey.Undefined;

        protected void InvalidateParent()
        {
            this.innerParent.Invalidate();
        }

        protected PropertyChangedNotifier Notifier =>
            this.notifier;

        protected internal IDrawingCache DrawingCache =>
            base.DocumentModel.DrawingCache;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        protected InvalidateProxy InnerParent =>
            this.innerParent;
    }
}

