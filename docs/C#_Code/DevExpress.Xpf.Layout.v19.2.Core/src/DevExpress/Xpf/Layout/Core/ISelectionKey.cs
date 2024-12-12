namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ISelectionKey
    {
        object Item { get; }

        object ElementKey { get; }

        object ViewKey { get; }
    }
}

