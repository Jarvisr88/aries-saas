namespace DevExpress.Utils.Menu
{
    using System;

    public interface IDXMenuCheckItemCommandAdapter<T> where T: struct
    {
        IDXMenuCheckItem<T> CreateMenuItem(string groupId);
    }
}

