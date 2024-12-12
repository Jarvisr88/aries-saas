namespace DevExpress.Utils.Menu
{
    public interface IMenuBuilderUIFactory<TCommand, TMenuId> where TCommand: Command where TMenuId: struct
    {
        IDXMenuCheckItemCommandAdapter<TMenuId> CreateMenuCheckItemAdapter(TCommand command);
        IDXMenuItemCommandAdapter<TMenuId> CreateMenuItemAdapter(TCommand command);
        IDXPopupMenu<TMenuId> CreatePopupMenu();
        IDXPopupMenu<TMenuId> CreateSubMenu();
    }
}

