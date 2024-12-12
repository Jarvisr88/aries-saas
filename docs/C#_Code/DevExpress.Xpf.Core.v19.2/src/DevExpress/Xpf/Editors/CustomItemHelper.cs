namespace DevExpress.Xpf.Editors
{
    using System;

    internal class CustomItemHelper
    {
        public static object GetDisplayValue(object item) => 
            IsCustomItem(item) ? ((ICustomItem) item).DisplayValue : null;

        public static object GetEditValue(object item) => 
            IsCustomItem(item) ? ((ICustomItem) item).EditValue : null;

        public static bool IsCustomItem(object value) => 
            value is ICustomItem;

        public static bool IsTemplatedItem(object value) => 
            value is ITemplatedCustomItem;
    }
}

