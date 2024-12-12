namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using System;

    public interface IHistoryItem
    {
        void Decorate(IHistoryItemDecorator decorator);
        void Execute();
        void ExecuteCore();
        void Redo();
        void RedoCore();
        void Undo();
        void UndoCore();

        IDocumentModelPart DocumentModelPart { get; }

        IDocumentModel DocumentModel { get; }
    }
}

