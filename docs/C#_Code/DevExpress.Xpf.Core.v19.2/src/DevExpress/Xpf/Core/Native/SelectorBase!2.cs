namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class SelectorBase<TContainer, TItem> : ItemsControl, ISelectorBase where TContainer: SelectorBase<TContainer, TItem> where TItem: SelectorItemBase<TContainer, TItem>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedIndexProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedItemProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedContainerProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedValueProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedValuePathProperty;
        private static readonly DependencyPropertyKey SelectedItemContentPropertyKey;
        private static readonly DependencyPropertyKey SelectedItemContentTemplatePropertyKey;
        private static readonly DependencyPropertyKey SelectedItemContentTemplateSelectorPropertyKey;
        private static readonly DependencyPropertyKey SelectedItemContentStringFormatPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedItemContentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedItemContentTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedItemContentTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SelectedItemContentStringFormatProperty;
        private List<DependencyObject> logicalChildren;
        private bool lockSelectedItemChanged;
        private bool lockSelectedContainerChanged;
        private bool lockSelectedValueChanged;
        private EventHandler selectionChanged;
        private SelectorPropertyDescriptor DataAccessDescriptor;
        [CompilerGenerated]
        private NotifyCollectionChangedEventHandler ItemsChanged;
        private bool lockItemsChanged;
        private bool IsSelectionInitialized;
        private readonly SelectorBase<TContainer, TItem>.ContainerCache cache;

        event EventHandler ISelectorBase.SelectionChanged;

        public event NotifyCollectionChangedEventHandler ItemsChanged;

        static SelectorBase();
        public SelectorBase();
        protected void AddLocalLogicalChild(DependencyObject child);
        protected internal object AddNewItem();
        private int CalcSelectFirstIndex(bool IfTrueMoveLast = false);
        private int CalcSelectNextIndex(bool IfTrueMovePrev = false);
        protected internal bool CanSelectNextItem(bool cycle = false);
        protected internal bool CanSelectPrevItem(bool cycle = false);
        private void ClearContainer(TItem container);
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected virtual object CoerceSelectedContainer(FrameworkElement value);
        protected virtual object CoerceSelectedIndex(int value);
        protected virtual object CoerceSelectedItem(object value);
        protected virtual object CoerceSelectedValue(object value);
        protected abstract TItem CreateContainer();
        protected virtual TItem CreateContainerForNewItem();
        ContentControl ISelectorBase.GetContainer(int index);
        ContentControl ISelectorBase.GetContainer(object item);
        protected virtual int GetCoercedSelectedIndex(int index, NotifyCollectionChangedAction? originativeAction);
        internal int GetCoercedSelectedIndexCore(IEnumerable<TItem> containers, int index);
        protected internal TItem GetContainer(int index);
        protected internal TItem GetContainer(object item);
        protected override DependencyObject GetContainerForItemOverride();
        protected internal IEnumerable<TItem> GetContainers();
        protected int GetIndex(Func<object, bool> comparer);
        protected virtual bool GetIsNavigationInversed(FlowDirection flowDirection);
        internal int GetNotHiddenItemIndexAfter(int index);
        internal int GetNotHiddenItemIndexBefore(int index);
        protected internal void HideItem(int index, bool raiseEvents = true);
        protected internal void HideItem(object item, bool raiseEvents = true);
        public int IndexOf(object item);
        private void InitContainer(TItem container);
        private void InitSelection(int index = 0, bool force = false);
        protected internal void InsertItem(object item, int index);
        private void InsertItemCore(object item, int index);
        public bool IsIndexInRange(int index);
        protected override bool IsItemItsOwnContainerOverride(object item);
        protected internal virtual bool IsVisibleAndEnabledItem(TItem item);
        protected internal void MoveItem(int oldIndex, int newIndex);
        protected internal void MoveItem(object item, int index);
        private void NavigateCore(int index);
        protected virtual void OnAddItem(object newItem, int index);
        public override void OnApplyTemplate();
        protected internal virtual void OnContainerIsEnabledChanged(TItem container, bool oldValue, bool newValue);
        protected internal virtual void OnContainerVisibilityChanged(TItem container, Visibility oldValue, Visibility newValue);
        protected virtual void OnCurrentChanged(object sender, EventArgs e);
        protected override void OnInitialized(EventArgs e);
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsSynchronizedWithCurrentItemChanged(bool? oldValue, bool? newValue);
        protected virtual void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemContainerGeneratorStatusChanged(object sender, EventArgs e);
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e);
        protected void OnLogicalElementTemplateChanged(DependencyProperty property, DependencyPropertyKey propertyKey, DataTemplate template);
        protected virtual void OnMoveItem(object item, int oldIndex, int newIndex);
        protected virtual void OnRemoveItem(object oldItem, int index);
        protected virtual void OnReplaceItem(object oldItem, object newItem, int index);
        protected virtual void OnResetItem();
        protected virtual void OnSelectedContainerChanged(TItem oldValue, TItem newValue);
        protected virtual void OnSelectedIndexChanged(int oldValue, int newValue);
        protected virtual void OnSelectedItemChanged(object oldValue, object newValue);
        protected virtual void OnSelectedValueChanged(object oldValue, object newValue);
        protected virtual void OnSelectedValuePathChanged(string oldValue, string newValue);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected virtual void RaiseItemAdded(int index, object item);
        protected virtual void RaiseItemAdding(out object item, CancelEventArgs e);
        protected virtual void RaiseItemHidden(int index, object item);
        protected virtual void RaiseItemHiding(int index, object item, CancelEventArgs e);
        protected virtual void RaiseItemInserted(int index, object item);
        protected virtual void RaiseItemInserting(int index, object item, CancelEventArgs e);
        protected virtual void RaiseItemMoved(int oldIndex, int newIndex, object item);
        protected virtual void RaiseItemMoving(int oldIndex, int newIndex, object item, CancelEventArgs e);
        protected virtual void RaiseItemRemoved(int index, object item);
        protected virtual void RaiseItemRemoving(int index, object item, CancelEventArgs e);
        protected virtual void RaiseItemShowing(int index, object item, CancelEventArgs e);
        protected virtual void RaiseItemShown(int index, object item);
        protected virtual void RaiseSelectionChanged(int oldIndex, int newIndex, object oldItem, object newItem);
        protected virtual void RaiseSelectionChanging(int oldIndex, int newIndex, object oldItem, object newItem, CancelEventArgs e);
        protected internal void RemoveItem(int index);
        protected internal void RemoveItem(object item);
        protected void RemoveLocalLogicalChild(DependencyObject child);
        protected internal void SelectFirstItem();
        protected internal void SelectItem(object item);
        protected internal void SelectItem(int index, bool forceRaiseSelectionChanging = false);
        protected internal void SelectLastItem();
        protected internal void SelectNextItem(bool cycle = false);
        protected internal void SelectPrevItem(bool cycle = false);
        private void SetItemVisibility(int index, Visibility visibility, Action<int, object, CancelEventArgs> raiseCancelableEvent, Action<int, object> raiseCompletedEvent, bool raiseEvents);
        internal void SetSelectedContainer(TItem value);
        protected internal void ShowItem(int index, bool raiseEvents = true);
        protected internal void ShowItem(object item, bool raiseEvents = true);
        protected internal virtual void UpdateSelectionProperties();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TItem SelectedContainer { get; set; }

        [TypeConverter(typeof(NullableBoolConverter))]
        public bool? IsSynchronizedWithCurrentItem { get; set; }

        public object SelectedValue { get; set; }

        public string SelectedValuePath { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItemContent { get; protected set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTemplate SelectedItemContentTemplate { get; protected set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTemplateSelector SelectedItemContentTemplateSelector { get; protected set; }

        public string SelectedItemContentStringFormat { get; protected set; }

        protected override IEnumerator LogicalChildren { get; }

        protected virtual bool RequiresSelection { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorBase<TContainer, TItem>.<>c <>9;
            public static Func<DataTemplate, DependencyObject> <>9__49_1;
            public static Action<TItem> <>9__59_0;
            public static Action<TItem> <>9__59_1;
            public static Action<TItem> <>9__63_0;

            static <>c();
            internal void <.cctor>b__154_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__154_1(DependencyObject d, object e);
            internal void <.cctor>b__154_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__154_3(DependencyObject d, object e);
            internal void <.cctor>b__154_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__154_5(DependencyObject d, object e);
            internal void <.cctor>b__154_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__154_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__154_8(DependencyObject d, object e);
            internal void <.cctor>b__154_9(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal DependencyObject <OnLogicalElementTemplateChanged>b__49_1(DataTemplate x);
            internal void <OnSelectedContainerChanged>b__59_0(TItem x);
            internal void <OnSelectedContainerChanged>b__59_1(TItem x);
            internal void <UpdateSelectionProperties>b__63_0(TItem x);
        }

        private class ContainerCache
        {
            private readonly SelectorBase<TContainer, TItem> owner;
            private List<TItem> containers;

            public ContainerCache(SelectorBase<TContainer, TItem> owner);
            public TItem GetContainer(int index);
            public TItem GetContainer(object item);
            public IEnumerable<TItem> GetContainers();
            public int IndexOf(object item);
            public void Invalidate();
        }
    }
}

