namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FastRenderPanel : Panel
    {
        public static readonly DependencyProperty ContentCacheModeProperty;
        private ItemsControl owner;

        static FastRenderPanel();
        public FastRenderPanel();
        private void AddItem(object item);
        protected override Size ArrangeOverride(Size finalSize);
        private void BindItem(ContentPresenter cp, ContentControl container);
        private void Clear();
        private void ClearBind(ContentPresenter cp);
        private DataTemplate GetActualContentTemplate(ContentControl container, object content);
        public void Initialize(ISelectorBase owner);
        public void Initialize(ItemsControl owner);
        private void InsertItem(object item, int index);
        protected override Size MeasureOverride(Size availableSize);
        private void OnContainerGeneratorStatusChanged(object sender, EventArgs e);
        private void OnContentCacheModeChanged(TabContentCacheMode oldValue, TabContentCacheMode newValue);
        private void OnItemsAdd(NotifyCollectionChangedEventArgs e);
        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e);
        private void OnItemsMove(NotifyCollectionChangedEventArgs e);
        private void OnItemsRemove(NotifyCollectionChangedEventArgs e);
        private void OnItemsReplace(NotifyCollectionChangedEventArgs e);
        private void OnLoaded(object sender, EventArgs e);
        private void OnOwnerChanged(ItemsControl oldValue, ItemsControl newValue);
        private void OnSelectionChanged(object sender, EventArgs e);
        private void RemoveItem(object item);
        private void SubsribeOwner(ItemsControl owner, EventHandler containerGeneratorStatusChangedHandler, NotifyCollectionChangedEventHandler itemsChangedHandler, EventHandler selectionChangedHandler);
        private void Synchronize();
        private void SynchronizeAllTabs();
        private void SynchronizeSelectedTab();
        public void Uninitialize();
        private void UnsubsribeOwner(ItemsControl owner, EventHandler containerGeneratorStatusChangedHandler, NotifyCollectionChangedEventHandler itemsChangedHandler, EventHandler selectionChangedHandler);
        private void UpdateItem(object item);
        private void UpdateSelection();

        public TabContentCacheMode ContentCacheMode { get; set; }

        public ItemsControl Owner { get; private set; }

        public bool IsFastModeInitialized { get; private set; }

        protected internal ContentPresenter SelectedItem { get; private set; }

        protected internal Dictionary<object, ContentPresenter> Items { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FastRenderPanel.<>c <>9;

            static <>c();
            internal void <.cctor>b__50_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

