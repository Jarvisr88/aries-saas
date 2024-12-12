namespace DevExpress.Utils.Menu
{
    using System;

    public interface IDXPopupMenu<T> : IDXMenuItemBase<T> where T: struct
    {
        void AddItem(IDXMenuItemBase<T> item);

        int ItemsCount { get; }

        T Id { get; set; }

        string Caption { get; set; }

        bool Visible { get; set; }

        bool Enabled { get; set; }
    }
}

