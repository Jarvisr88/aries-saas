namespace DevExpress.Utils.Controls
{
    using System;

    public interface IFilterItem
    {
        bool? IsChecked { get; set; }

        bool IsVisible { get; set; }
    }
}

