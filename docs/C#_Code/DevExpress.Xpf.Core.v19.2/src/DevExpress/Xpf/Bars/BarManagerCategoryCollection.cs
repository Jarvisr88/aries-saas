namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;

    public class BarManagerCategoryCollection : ObservableCollection<BarManagerCategory>
    {
        private BarManager manager;
        private BarManagerCategory unassignedItems;
        private AllItemsBarManagerCategory allItems;

        public BarManagerCategoryCollection(BarManager manager);
        protected override void ClearItems();
        protected internal static AllItemsBarManagerCategory CreateAllItemsCategory(BarManager manager);
        protected internal static BarManagerCategory CreateUnassignedItemsCategory(BarManager manager);
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);

        [Description("Gets a BarManager object that owns the current collection.")]
        public BarManager Manager { get; }

        public BarManagerCategory this[string name] { get; }

        [Description("Gets a category object that combines all the bar items that are not assigned to any category.")]
        public BarManagerCategory UnassignedItems { get; }

        [Description("Gets a category object that combines all bar items (even items that are explicitly assigned to other categories).")]
        public AllItemsBarManagerCategory AllItems { get; }
    }
}

