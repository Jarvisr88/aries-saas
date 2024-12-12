namespace DevExpress.Data.Utils
{
    using System;
    using System.Reflection;

    public interface IToolShell : IDisposable
    {
        void AddToolItem(IToolItem item);
        void Close();
        void Hide();
        void HideIfNotContains(IToolShell anotherToolShell);
        void InitToolItems();
        void RemoveToolItem(Guid itemKind);
        void ShowNoActivate();
        void UpdateToolItems();

        IToolItem this[Guid itemKind] { get; }
    }
}

