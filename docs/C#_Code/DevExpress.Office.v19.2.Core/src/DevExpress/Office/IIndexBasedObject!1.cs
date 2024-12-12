namespace DevExpress.Office
{
    using System;

    public interface IIndexBasedObject<TActions> where TActions: struct
    {
        int GetIndex();
        void SetIndex(int index, TActions changeActions);

        IDocumentModelPart DocumentModelPart { get; }
    }
}

