namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class HistoryItem : IHistoryItem, IDisposable
    {
        private readonly IDocumentModelPart documentModelPart;

        protected HistoryItem(IDocumentModelPart documentModelPart)
        {
            Guard.ArgumentNotNull(documentModelPart, "documentModelPart");
            this.documentModelPart = documentModelPart;
        }

        public void Decorate(IHistoryItemDecorator decorator)
        {
            this.Decorator = decorator;
        }

        void IHistoryItem.ExecuteCore()
        {
            this.ExecuteCore();
        }

        void IHistoryItem.RedoCore()
        {
            this.RedoCore();
        }

        void IHistoryItem.UndoCore()
        {
            this.UndoCore();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            bool flag1 = disposing;
        }

        public void Execute()
        {
            if (this.Decorator != null)
            {
                this.Decorator.Execute();
            }
            else
            {
                this.ExecuteCore();
            }
        }

        protected virtual void ExecuteCore()
        {
            this.RedoCore();
        }

        public virtual object GetTargetObject() => 
            null;

        public virtual void Read(IHistoryReader reader)
        {
        }

        public void Redo()
        {
            if (this.Decorator != null)
            {
                this.Decorator.Redo();
            }
            else
            {
                this.RedoCore();
            }
        }

        protected abstract void RedoCore();
        public virtual void Register(IHistoryWriter writer, bool undone)
        {
            object targetObject = this.GetTargetObject();
            if ((targetObject != this.documentModelPart) && !ReferenceEquals(this.documentModelPart, this.documentModelPart.DocumentModel))
            {
                writer.RegisterObject(this.documentModelPart);
            }
            writer.RegisterObject(targetObject);
        }

        public void Undo()
        {
            if (this.Decorator != null)
            {
                this.Decorator.Undo();
            }
            else
            {
                this.UndoCore();
            }
        }

        protected abstract void UndoCore();
        public virtual void Write(IHistoryWriter writer)
        {
            writer.Write(writer.GetObjectId(this.documentModelPart));
            writer.Write(writer.GetObjectId(this.GetTargetObject()));
        }

        public virtual void WriteObjects(IHistoryWriter writer, bool undone)
        {
        }

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;

        public IDocumentModel DocumentModel =>
            this.documentModelPart.DocumentModel;

        public virtual bool ChangeModified =>
            true;

        private IHistoryItemDecorator Decorator { get; set; }
    }
}

