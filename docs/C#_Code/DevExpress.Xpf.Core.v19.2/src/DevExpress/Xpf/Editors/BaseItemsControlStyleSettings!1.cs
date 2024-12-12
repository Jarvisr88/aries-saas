namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public abstract class BaseItemsControlStyleSettings<T> : PopupBaseEditStyleSettings
    {
        protected BaseItemsControlStyleSettings()
        {
        }

        protected internal virtual IEnumerable<CustomItem> GetCustomItems(T editor) => 
            new List<CustomItem>();

        protected internal abstract Style GetItemContainerStyle(T control);
        protected internal abstract SelectionEventMode GetSelectionEventMode(ISelectorEdit ce);
        protected internal virtual bool ShowCustomItem(T editor) => 
            false;

        protected internal virtual bool ShowMRUItem() => 
            false;
    }
}

