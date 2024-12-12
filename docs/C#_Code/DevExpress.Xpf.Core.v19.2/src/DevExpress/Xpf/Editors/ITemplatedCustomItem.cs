namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Windows;

    public interface ITemplatedCustomItem : ICustomItem
    {
        void UpdateOwner(ISelectorEdit ownerEdit);

        DataTemplate ItemTemplate { get; }

        Style ItemContainerStyle { get; }
    }
}

