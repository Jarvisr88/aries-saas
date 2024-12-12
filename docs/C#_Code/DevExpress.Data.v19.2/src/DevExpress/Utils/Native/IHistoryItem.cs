namespace DevExpress.Utils.Native
{
    using System;

    public interface IHistoryItem : IDisposable
    {
        void Redo();
        void Undo();

        object ObjectToSelect { get; }
    }
}

