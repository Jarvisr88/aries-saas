namespace DevExpress.Utils.Menu
{
    public interface IDXMenuItemCommandAdapter<T> where T: struct
    {
        IDXMenuItem<T> CreateMenuItem(DXMenuItemPriority priority);
    }
}

