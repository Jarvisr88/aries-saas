namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Header"), Localizability(LocalizationCategory.None, Readability=Readability.Unreadable)]
    public abstract class GridColumnBase : ColumnBase
    {
        protected GridColumnBase()
        {
        }

        internal abstract bool SupportValidate();
    }
}

