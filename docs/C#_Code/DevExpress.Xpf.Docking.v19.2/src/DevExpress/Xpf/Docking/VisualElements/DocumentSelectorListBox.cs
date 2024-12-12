namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class DocumentSelectorListBox : ListBox
    {
        public DocumentSelectorListBox()
        {
            base.Focusable = false;
        }

        private void ClearAnchorItem()
        {
            PropertyInfo property = typeof(ListBox).GetProperty("AnchorItem", BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                property.SetValue(this, null, null);
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new DocumentSelectorListBoxItem();

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (!base.HasItems)
            {
                this.ClearAnchorItem();
            }
        }
    }
}

