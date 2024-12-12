namespace DevExpress.Office.History
{
    using System;

    public interface IHistoryItemDecorator
    {
        void Execute();
        void Redo();
        void Undo();
    }
}

