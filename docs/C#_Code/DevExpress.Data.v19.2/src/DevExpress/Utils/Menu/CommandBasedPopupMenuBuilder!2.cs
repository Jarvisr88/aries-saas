namespace DevExpress.Utils.Menu
{
    using System;

    public abstract class CommandBasedPopupMenuBuilder<TCommand, TMenuId> where TCommand: Command where TMenuId: struct
    {
        private readonly IMenuBuilderUIFactory<TCommand, TMenuId> uiFactory;

        protected CommandBasedPopupMenuBuilder(IMenuBuilderUIFactory<TCommand, TMenuId> uiFactory);
        protected internal virtual IDXMenuCheckItem<TMenuId> AddMenuCheckItem(IDXPopupMenu<TMenuId> menu, TCommand command);
        protected internal virtual IDXMenuCheckItem<TMenuId> AddMenuCheckItem(IDXPopupMenu<TMenuId> menu, TCommand command, string groupId);
        protected internal virtual void AddMenuCheckItemIfCommandVisible(IDXPopupMenu<TMenuId> menu, TCommand command, string groupId);
        protected internal virtual IDXMenuItem<TMenuId> AddMenuItem(IDXPopupMenu<TMenuId> menu, TCommand command);
        protected internal virtual IDXMenuItem<TMenuId> AddMenuItem(IDXPopupMenu<TMenuId> menu, TCommand command, DXMenuItemPriority priority);
        protected internal virtual void AddMenuItemIfCommandVisible(IDXPopupMenu<TMenuId> menu, TCommand command);
        protected internal virtual void AddMenuItemIfCommandVisible(IDXPopupMenu<TMenuId> menu, TCommand command, bool beginGroup);
        protected internal virtual void AppendSubmenu(IDXPopupMenu<TMenuId> menu, IDXPopupMenu<TMenuId> subMenu, bool beginGroup);
        public virtual IDXPopupMenu<TMenuId> CreatePopupMenu();
        public virtual IDXPopupMenu<TMenuId> CreateSubMenu();
        public abstract void PopulatePopupMenu(IDXPopupMenu<TMenuId> menu);

        public IMenuBuilderUIFactory<TCommand, TMenuId> UiFactory { get; }
    }
}

