namespace DevExpress.Data.Utils
{
    using System;
    using System.Collections.Generic;

    public class ToolShell : IToolShell, IDisposable
    {
        protected List<IToolItem> toolItems;

        public ToolShell();
        public void AddToolItem(IToolItem item);
        public void Close();
        public virtual void Dispose();
        protected IToolItem GetItemBy(Guid itemKind);
        public void Hide();
        public void HideIfNotContains(IToolShell anotherToolShell);
        public void InitToolItems();
        public void RemoveToolItem(Guid itemKind);
        public virtual void ShowNoActivate();
        protected void ShowNoActivate(IToolItem obj);
        public virtual void UpdateToolItems();

        IToolItem IToolShell.this[Guid itemKind] { get; }
    }
}

