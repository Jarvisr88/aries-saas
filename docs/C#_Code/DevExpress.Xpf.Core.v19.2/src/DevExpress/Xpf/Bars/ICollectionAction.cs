namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;

    public interface ICollectionAction
    {
        CollectionActionKind Kind { get; }

        object Element { get; }

        IList Collection { get; }

        int Index { get; }
    }
}

