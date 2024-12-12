namespace DevExpress.Utils.Menu
{
    using System;

    public interface IDXMenuItemBase<T> where T: struct
    {
        bool BeginGroup { get; set; }
    }
}

