namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    [ToolboxItem(false)]
    public class EditorListBox : ListBox, ISelectorEditInnerListBox
    {
        [ThreadStatic]
        private static DevExpress.Xpf.Core.Internal.ReflectionHelper helper;
        private static DataTemplate customItemContentTemplate;
        public static readonly DependencyProperty AllowItemHighlightingProperty = DependencyPropertyManager.Register("AllowItemHighlighting", typeof(bool), typeof(EditorListBox), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty ScrollUnit2Property;
        private SelectorEditInnerListBoxItemsSourceHelper itemsSourceHelper;
        private readonly Locker clearHighlightedTextLocker = new Locker();
        private static readonly Point UninitializedMousePosition = new Point(double.NegativeInfinity, double.NegativeInfinity);
        private static readonly Point EditBoxMousePosition = new Point(double.PositiveInfinity, double.PositiveInfinity);
        private Point lastMousePosition = UninitializedMousePosition;
        private readonly Locker selectAllLocker = new Locker();

        static EditorListBox()
        {
            ScrollUnit2Property = DependencyPropertyManager.Register("ScrollUnit2", typeof(DevExpress.Xpf.Editors.ScrollUnit), typeof(EditorListBox), new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ScrollUnit.Item, FrameworkPropertyMetadataOptions.None, (o, args) => ((EditorListBox) o).ScrollUnit2Changed((DevExpress.Xpf.Editors.ScrollUnit) args.NewValue)));
        }

        public EditorListBox()
        {
            base.IsTextSearchEnabled = false;
            base.IsSynchronizedWithCurrentItem = false;
            this.SelectionLocker = new Locker();
            this.SelectionChangedLocker = new Locker();
            this.SynchronizationLocker = new Locker();
            this.InnerItemSynchronizationLocker = new Locker();
            base.Loaded += new RoutedEventHandler(this.EditorListBoxLoaded);
            base.Unloaded += new RoutedEventHandler(this.EditorListBoxUnloaded);
            this.SelectAllAction = new PostponedAction(() => this.IsSelectionChangerActive);
            base.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.OnRequestBringIntoView);
        }

        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }

        protected virtual void AssignGroupStyle()
        {
            if (!this.OwnerEdit.GroupStyle.SequenceEqual<GroupStyle>(base.GroupStyle))
            {
                base.GroupStyle.Clear();
                foreach (GroupStyle style in this.OwnerEdit.GroupStyle)
                {
                    base.GroupStyle.Add(style);
                }
            }
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            this.SynchronizationLocker.DoLockedAction(() => this.ClearContainerForItemOverrideInternal(element, item));
        }

        protected virtual void ClearContainerForItemOverrideInternal(DependencyObject element, object item)
        {
            ListBoxItem item2 = element as ListBoxItem;
            if (item2 != null)
            {
                if ((item == null) || CustomItemHelper.IsTemplatedItem(item))
                {
                    this.ClearTemplatedItem(item2, (CustomItem) item);
                }
                if (item is DataProxy)
                {
                    this.ClearServerModeItem(item2, (DataProxy) item);
                }
            }
        }

        private bool ClearHighlightedText()
        {
            if (this.clearHighlightedTextLocker.IsLocked || string.IsNullOrEmpty((string) base.GetValue(TextBlockService.HighlightedTextProperty)))
            {
                return false;
            }
            this.SetHighlightedText(string.Empty);
            return true;
        }

        private void ClearServerModeItem(ListBoxItem listBoxItem, DataProxy item)
        {
            IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
            if (itemsSource != null)
            {
                itemsSource.CancelItem(base.ItemContainerGenerator.IndexFromContainer(listBoxItem));
            }
        }

        protected virtual void ClearTemplatedItem(ListBoxItem element, CustomItem item)
        {
            element.ClearValue(ContentControl.ContentTemplateProperty);
            element.ClearValue(FrameworkElement.StyleProperty);
        }

        private void ClearTemplateSelector(ListBoxItem element)
        {
            element.ClearValue(ContentControl.ContentTemplateSelectorProperty);
        }

        private bool ContainsSelectAllItem(IList source) => 
            source.Cast<object>().OfType<SelectAllItem>().Any<SelectAllItem>();

        protected virtual SelectorEditInnerListBoxItemsSourceHelper CreateItemsSourceHelper() => 
            (this.OwnerEdit != null) ? SelectorEditInnerListBoxItemsSourceHelper.CreateHelper(this, (!this.OwnerEdit.AllowCollectionView && !this.IsServerMode) && this.OwnerEdit.UseCustomItems) : null;

        private static DevExpress.Xpf.Core.Internal.ReflectionHelper CreateReflectionHelper() => 
            new DevExpress.Xpf.Core.Internal.ReflectionHelper();

        ICustomItem ISelectorEditInnerListBox.GetCustomItem(Func<object, bool> getNeedItem)
        {
            ICustomItem item2;
            using (IEnumerator enumerator = this.ItemsSourceHelper.CustomItemsSource.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ICustomItem current = (ICustomItem) enumerator.Current;
                        if (!getNeedItem(current))
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item2;
        }

        bool ISelectorEditInnerListBox.IsCustomItem(ICustomItem customItem) => 
            this.SelectorEditInnerListBox.GetCustomItem(current => current == customItem) != null;

        void ISelectorEditInnerListBox.ScrollIntoView(object item)
        {
            base.ScrollIntoView(item);
        }

        void ISelectorEditInnerListBox.SelectAll()
        {
            if (!this.SynchronizationLocker.IsLocked)
            {
                this.SelectAllAction.PerformPostpone(() => base.SelectAll());
            }
        }

        void ISelectorEditInnerListBox.UnselectAll()
        {
            if (!this.SynchronizationLocker.IsLocked)
            {
                this.SelectAllAction.PerformPostpone(() => base.UnselectAll());
            }
        }

        private void EditorListBoxLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, true);
        }

        private void EditorListBoxUnloaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, false);
        }

        internal void FocusContainer(DependencyObject container)
        {
            ListBoxItem item = container as ListBoxItem;
            if (item != null)
            {
                item.Focus();
            }
        }

        private void FocusItem(object item)
        {
            if (item != null)
            {
                this.FocusContainer(base.ItemContainerGenerator.ContainerFromItem(item));
            }
        }

        private void FocusSearchResult(object result)
        {
            object item = !this.IsSyncServerMode ? this.GetItemByValue(result) : this.GetSelectedItemByEditValueInServerMode(result);
            if (!this.EditStrategy.IsSingleSelection && ((base.SelectionMode != SelectionMode.Extended) || ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers)))
            {
                this.MakeVisibleAndFocusItem(item);
            }
            else
            {
                base.SelectedItem = item;
                this.FocusSelectedItem();
            }
        }

        public void FocusSelectedItem()
        {
            int selectedIndex = base.SelectedIndex;
            if ((selectedIndex == -1) && (base.Items.Count > 0))
            {
                selectedIndex = 0;
            }
            if (selectedIndex != -1)
            {
                ListBoxItem item = base.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as ListBoxItem;
                if (item != null)
                {
                    item.Focus();
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ListBoxEditItem();

        internal int GetFocusedItemIndex()
        {
            int focusedItemIndexCore = this.GetFocusedItemIndexCore();
            if (!this.IsServerMode && (focusedItemIndexCore != -1))
            {
                DependencyObject container = base.ItemContainerGenerator.ContainerFromIndex(focusedItemIndexCore);
                object item = base.ItemContainerGenerator.ItemFromContainer(container);
                focusedItemIndexCore = this.OwnerEdit.ItemsProvider.GetIndexByItem(item, this.EditStrategy.GetCurrentDataViewHandle());
            }
            return focusedItemIndexCore;
        }

        private int GetFocusedItemIndexCore()
        {
            ListBoxEditItem focusedElement = Keyboard.FocusedElement as ListBoxEditItem;
            return ((focusedElement != null) ? base.ItemContainerGenerator.IndexFromContainer(focusedElement) : -1);
        }

        private object GetItemByValue(object value) => 
            this.EditStrategy.ItemsProvider.GetItem(value, this.EditStrategy.CurrentDataViewHandle);

        private Point GetPosition(ListBoxEditItem item, MouseEventArgs e) => 
            e.GetPosition(LayoutHelper.FindParentObject<ListBox>(item));

        private int GetSelectedIndexByEditValueInAsyncServerMode(object editValue)
        {
            IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
            return ((itemsSource != null) ? itemsSource.IndexOfValue(editValue) : -1);
        }

        private int GetSelectedIndexByEditValueInServerMode(object editValue)
        {
            IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
            return ((itemsSource != null) ? itemsSource.IndexOfValue(editValue) : -1);
        }

        protected object GetSelectedItemByEditValueInServerMode(object editValue)
        {
            IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
            if (itemsSource == null)
            {
                return -1;
            }
            int visibleIndex = itemsSource.IndexOfValue(editValue);
            return ((visibleIndex > -1) ? itemsSource.GetItem(visibleIndex) : null);
        }

        public virtual IEnumerable<object> GetSelectedItems() => 
            this.GetSelectedItemsInternal();

        private object GetSelectedItemsFromEditor() => 
            ((ISelectorEditStrategy) this.OwnerEdit.EditStrategy).GetSelectedItems(this.OwnerEdit.EditStrategy.EditValue);

        protected List<object> GetSelectedItemsInServerMode(IEnumerable<object> values)
        {
            if (values == null)
            {
                return new List<object>();
            }
            Func<object, bool> predicate = <>c.<>9__117_0;
            if (<>c.<>9__117_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__117_0;
                predicate = <>c.<>9__117_0 = x => x != null;
            }
            return values.Select<object, object>(new Func<object, object>(this.GetSelectedItemByEditValueInServerMode)).Where<object>(predicate).ToList<object>();
        }

        protected virtual IEnumerable<object> GetSelectedItemsInternal() => 
            this.OwnerEdit.GetCurrentSelectedItems().Cast<object>();

        private bool HasMouseMoved(ListBoxEditItem item, MouseEventArgs e) => 
            (this.lastMousePosition != UninitializedMousePosition) && (this.GetPosition(item, e) != this.lastMousePosition);

        protected bool IsEditorReadOnly() => 
            (this.OwnerEdit != null) && this.OwnerEdit.IsReadOnly;

        private bool IsItemVisible(ListBoxEditItem item) => 
            true;

        protected bool IsNavKey(Key key) => 
            (key == Key.Left) || ((key == Key.Right) || ((key == Key.Down) || ((key == Key.Up) || ((key == Key.Next) || (key == Key.Next)))));

        public void MakeSelectionVisible()
        {
            if (base.SelectionMode != SelectionMode.Single)
            {
                if ((base.SelectedItems != null) && (base.SelectedItems.Count > 0))
                {
                    base.ScrollIntoView(base.SelectedItems[base.SelectedItems.Count - 1]);
                }
            }
            else if (this.IsAsyncServerMode)
            {
                IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
                if ((itemsSource != null) && ((base.SelectedIndex >= 0) && !itemsSource.IsTempItem(base.SelectedIndex)))
                {
                    DataProxy item = itemsSource.GetItem(base.SelectedIndex) as DataProxy;
                    if (item != null)
                    {
                        base.ScrollIntoView(item.f_component);
                    }
                }
            }
            else if (base.SelectedItem != null)
            {
                base.ScrollIntoView(base.SelectedItem);
            }
        }

        internal void MakeVisibleAndFocusItem(object item)
        {
            base.ScrollIntoView(item);
            this.FocusItem(item);
        }

        protected void MakeVisibleIfNeeded()
        {
            if (this.ShouldMakeSelectionVisible())
            {
                this.MakeSelectionVisible();
            }
        }

        private bool NavigateToNextSearchedItem()
        {
            bool result = false;
            this.clearHighlightedTextLocker.DoLockedAction(delegate {
                object nextValueFromSearchText = this.EditStrategy.GetNextValueFromSearchText(this.GetFocusedItemIndex());
                if (nextValueFromSearchText != null)
                {
                    this.FocusSearchResult(nextValueFromSearchText);
                }
                result = true;
            });
            return result;
        }

        private bool NavigateToPrevSearchedItem()
        {
            bool result = false;
            this.clearHighlightedTextLocker.DoLockedAction(delegate {
                object prevValueFromSearchText = this.EditStrategy.GetPrevValueFromSearchText(this.GetFocusedItemIndex());
                if (prevValueFromSearchText != null)
                {
                    this.FocusSearchResult(prevValueFromSearchText);
                }
                result = true;
            });
            return result;
        }

        protected internal virtual void NotifyItemMouseMove(ListBoxEditItem item, MouseEventArgs e)
        {
            this.SelectItemOnMouseMove(item, e);
            this.SetLastMousePosition(this.GetPosition(item, e));
        }

        internal void NotifyOwnerSelectionChanged(ListBoxEditItem listBoxEditItem, bool isSelected)
        {
        }

        protected internal virtual void OnInnerItemContentChanged(ListBoxItem container)
        {
            if (this.OwnerEdit != null)
            {
                IListNotificationOwner listNotification = this.OwnerEdit.ListNotificationOwner;
                NotifyItemsProviderChangedEventArgs listChangedEventArgs = new NotifyItemsProviderChangedEventArgs(ListChangedType.ItemChanged, base.ItemContainerGenerator.ItemFromContainer(container));
                this.InnerItemSynchronizationLocker.DoLockedActionIfNotLocked(() => listNotification.OnCollectionChanged(listChangedEventArgs));
            }
        }

        protected virtual void OnInplaceKeyDown(KeyEventArgs e)
        {
            if ((base.SelectionMode != SelectionMode.Multiple) && (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && this.IsNavKey(e.Key)))
            {
                ListBoxEditItem focusedElement = Keyboard.FocusedElement as ListBoxEditItem;
                if (focusedElement != null)
                {
                    int num = base.ItemContainerGenerator.IndexFromContainer(focusedElement);
                    if (num > -1)
                    {
                        base.SelectedIndex = num;
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.OwnerEdit.EditMode == EditMode.InplaceActive)
            {
                this.OnInplaceKeyDown(e);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.SetLastMousePosition(UninitializedMousePosition);
            base.OnMouseLeave(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (this.IsEditorReadOnly() && this.IsNavKey(e.Key))
            {
                e.Handled = true;
            }
            else
            {
                if (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                {
                    if (e.Key == Key.Down)
                    {
                        e.Handled = this.NavigateToNextSearchedItem();
                        return;
                    }
                    if (e.Key == Key.Up)
                    {
                        e.Handled = this.NavigateToPrevSearchedItem();
                        return;
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = this.ClearHighlightedText();
                    this.OwnerEdit.EditStrategy.CancelTextSearch();
                }
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            string text = e.Text;
            if (this.OwnerEdit.IncrementalSearch && !string.IsNullOrEmpty(text))
            {
                this.ProcessTextSearch(text);
            }
        }

        private void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.SelectionChangedLocker.DoLockedAction(() => this.<>n__0(e));
            this.SelectionChangedLocker.DoIfNotLocked(() => this.SelectAllAction.PerformForce());
            this.OnSelectionChangedInternal(e);
            this.MakeVisibleIfNeeded();
            this.ClearHighlightedText();
        }

        protected virtual void OnSelectionChangedInternal(SelectionChangedEventArgs e)
        {
            if (this.OwnerEdit != null)
            {
                this.SynchronizationLocker.DoLockedActionIfNotLocked(delegate {
                    Func<VisibleListWrapper, bool> evaluator = <>c.<>9__100_1;
                    if (<>c.<>9__100_1 == null)
                    {
                        Func<VisibleListWrapper, bool> local1 = <>c.<>9__100_1;
                        evaluator = <>c.<>9__100_1 = x => x.SelectionLocker.IsLocked;
                    }
                    if (!(this.ItemsSource as VisibleListWrapper).If<VisibleListWrapper>(evaluator).ReturnSuccess<VisibleListWrapper>())
                    {
                        this.OwnerEdit.EditStrategy.SyncWithEditor();
                    }
                    if (this.OwnerEdit != null)
                    {
                        this.SyncSelectAll(e);
                    }
                });
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            this.SynchronizationLocker.DoLockedAction(() => this.PrepareContainerForItemOverrideInternal(element, item));
        }

        protected virtual void PrepareContainerForItemOverrideInternal(DependencyObject element, object item)
        {
            ListBoxItem item2 = element as ListBoxItem;
            if (item2 != null)
            {
                if (CustomItemHelper.IsCustomItem(item))
                {
                    this.SetupCustomItem(item2, (ICustomItem) item);
                }
                if (CustomItemHelper.IsTemplatedItem(item))
                {
                    this.UpdateTemplatedItem(item2, (ITemplatedCustomItem) item);
                }
                if (item is DataProxy)
                {
                    this.UpdateServerModeItem(item2, (DataProxy) item);
                }
                if (item == null)
                {
                    item2.Template = null;
                }
            }
        }

        private void ProcessTextSearch(string text)
        {
            <>c__DisplayClass27_0 class_;
            object result = null;
            this.clearHighlightedTextLocker.DoLockedAction(delegate {
                if (this.OwnerEdit.EditStrategy.DoTextSearch(text, this.GetFocusedItemIndex(), ref result))
                {
                    this.FocusSearchResult(result);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () => class_.SetHighlightedText(class_.EditStrategy.SearchText));
                }
                else if (string.IsNullOrEmpty(this.EditStrategy.SearchText))
                {
                    this.SetHighlightedText(this.EditStrategy.SearchText);
                }
            });
        }

        protected virtual void ResetItemsSourceHelper()
        {
            this.itemsSourceHelper = null;
        }

        private void ScrollUnit2Changed(DevExpress.Xpf.Editors.ScrollUnit newValue)
        {
            ItemsControlHelper.SetScrollUnit(this, newValue);
        }

        private void SelectItemOnMouseMove(ListBoxEditItem item, MouseEventArgs e)
        {
            if (((this.SelectionEventMode == DevExpress.Xpf.Editors.Popups.SelectionEventMode.MouseEnter) && this.HasMouseMoved(item, e)) && this.IsItemVisible(item))
            {
                item.SetCurrentValue(ListBoxItem.IsSelectedProperty, true);
            }
        }

        public void SetEditBoxMousePosition()
        {
            this.lastMousePosition = EditBoxMousePosition;
        }

        private void SetHighlightedText(string text)
        {
            DependencyObject ownerEdit = this.OwnerEdit as DependencyObject;
            if (ownerEdit != null)
            {
                ownerEdit.SetValue(TextBlockService.HighlightedTextProperty, text);
            }
        }

        private void SetLastMousePosition(Point p)
        {
            this.lastMousePosition = p;
        }

        protected virtual void SetPropertiesFromStyleSettings()
        {
            base.SelectionMode = this.OwnerEdit.SelectionMode;
            this.SelectionEventMode = this.OwnerEdit.SelectionEventMode;
            this.AssignGroupStyle();
        }

        protected virtual void SetupCustomItem(ListBoxItem element, ICustomItem item)
        {
            this.ClearTemplateSelector(element);
            element.ContentTemplate = CustomItemContentTemplate;
        }

        protected virtual bool ShouldMakeSelectionVisible() => 
            (this.OwnerEdit != null) ? (this.OwnerEdit.SelectionMode == SelectionMode.Single) : false;

        protected virtual void SyncItemsProperty()
        {
            if ((this.OwnerEdit != null) && !this.IsServerMode)
            {
                this.OwnerEdit.Items.UpdateSelection(this.GetSelectedItemsFromEditor());
            }
        }

        protected virtual void SyncItemsSource()
        {
            this.ResetItemsSourceHelper();
            this.ItemsSourceHelper.AssignItemsSource();
        }

        protected virtual void SyncItemsSourceInternal()
        {
        }

        protected virtual void SyncSelectAll(SelectionChangedEventArgs e)
        {
            if ((base.SelectionMode != SelectionMode.Single) && (this.SelectionViewModel != null))
            {
                if (this.ContainsSelectAllItem(e.AddedItems))
                {
                    this.SelectionViewModel.SelectAll = true;
                }
                else if (this.ContainsSelectAllItem(e.RemovedItems))
                {
                    this.SelectionViewModel.SelectAll = false;
                }
                else
                {
                    this.SyncSelectAllWithoutUpdate();
                }
            }
        }

        internal void SyncSelectAllWithoutUpdate()
        {
            this.SelectionViewModel.SetSelectAllWithoutUpdates(this.ItemsSourceHelper.IsSelectAll());
        }

        protected void SyncSelectedItems(bool updateTotals)
        {
            if (this.OwnerEdit != null)
            {
                if (base.SelectionMode == SelectionMode.Single)
                {
                    if (this.IsSyncServerMode)
                    {
                        object currentEditableValue = this.EditStrategy.GetCurrentEditableValue();
                        base.SelectedItem = this.GetSelectedItemByEditValueInServerMode(currentEditableValue);
                    }
                    else if (!this.IsAsyncServerMode)
                    {
                        base.SelectedItem = LookUpEditHelper.GetSelectedItem(this.OwnerEdit);
                    }
                    else
                    {
                        object currentEditableValue = this.EditStrategy.GetCurrentEditableValue();
                        base.SelectedIndex = this.GetSelectedIndexByEditValueInAsyncServerMode(currentEditableValue);
                    }
                }
                else
                {
                    IEnumerable<object> selectedItems;
                    this.SyncSelectedItemsInternal(updateTotals);
                    if (!this.IsSyncServerMode)
                    {
                        IEnumerable<object> second = CustomItem.FilterCustomItems(this.GetSelectedItems());
                        selectedItems = base.Items.Cast<object>().Intersect<object>(second).ToList<object>();
                        this.selectAllLocker.DoLockedAction<bool>(() => this.SetSelectedItems(selectedItems));
                    }
                    else
                    {
                        List<int> list1;
                        IEnumerable<object> editValue = LookUpEditHelper.GetEditValue(this.OwnerEdit) as IEnumerable<object>;
                        selectedItems = this.GetSelectedItemsInServerMode(editValue);
                        if (editValue == null)
                        {
                            list1 = new List<int>();
                        }
                        else
                        {
                            Func<int, bool> predicate = <>c.<>9__116_0;
                            if (<>c.<>9__116_0 == null)
                            {
                                Func<int, bool> local1 = <>c.<>9__116_0;
                                predicate = <>c.<>9__116_0 = x => x > -1;
                            }
                            list1 = editValue.Select<object, int>(new Func<object, int>(this.GetSelectedIndexByEditValueInServerMode)).Where<int>(predicate).ToList<int>();
                        }
                        List<int> allowedIndices = list1;
                        IServerModeCollectionView itemsSource = base.ItemsSource as IServerModeCollectionView;
                        if (itemsSource != null)
                        {
                            using (itemsSource.LockWhileUpdatingSelection(allowedIndices))
                            {
                                this.selectAllLocker.DoLockedAction<bool>(() => this.SetSelectedItems(selectedItems));
                            }
                        }
                    }
                    this.SelectionViewModel.SetSelectAllWithoutUpdates(this.ItemsSourceHelper.IsSelectAll());
                }
            }
        }

        protected virtual void SyncSelectedItemsInternal(bool updateTotals)
        {
        }

        protected internal virtual void SyncValuesWithOwnerEdit(bool resetTotals)
        {
            this.SyncSelectedItems(resetTotals);
            this.SyncItemsProperty();
        }

        protected internal void SyncWithOwnerEdit(bool updateSource)
        {
            if (this.OwnerEdit != null)
            {
                this.SynchronizationLocker.DoLockedActionIfNotLocked(() => this.SyncWithOwnerEditPropertiesInternal(updateSource));
            }
        }

        protected internal virtual void SyncWithOwnerEditPropertiesInternal(bool updateSource)
        {
            this.SetPropertiesFromStyleSettings();
            if (updateSource)
            {
                this.SelectionLocker.DoLockedAction(new Action(this.SyncItemsSource));
            }
            this.SyncValuesWithOwnerEdit(true);
        }

        protected internal virtual void SyncWithOwnerEditWithSelectionLock(bool updateSource)
        {
            if (this.OwnerEdit != null)
            {
                this.SynchronizationLocker.DoIfNotLocked(() => this.SyncWithOwnerEdit(updateSource));
            }
        }

        private void UpdateServerModeItem(ListBoxItem element, DataProxy item)
        {
            ((IServerModeCollectionView) base.ItemsSource).FetchItem(base.ItemContainerGenerator.IndexFromContainer(element));
        }

        protected virtual void UpdateTemplatedItem(ListBoxItem element, ITemplatedCustomItem itemEx)
        {
            this.ClearTemplateSelector(element);
            itemEx.UpdateOwner(this.OwnerEdit);
            element.ContentTemplate = itemEx.ItemTemplate;
            element.Style = itemEx.ItemContainerStyle;
        }

        private static DevExpress.Xpf.Core.Internal.ReflectionHelper ReflectionHelper =>
            helper ??= CreateReflectionHelper();

        private static DataTemplate CustomItemContentTemplate =>
            customItemContentTemplate ??= XamlHelper.GetTemplate("<ContentPresenter Content=\"{Binding DisplayValue}\"/>");

        private SelectorEditInnerListBoxItemsSourceHelper ItemsSourceHelper
        {
            get
            {
                this.itemsSourceHelper ??= this.CreateItemsSourceHelper();
                return (this.itemsSourceHelper ?? new DummyListSourceHelper());
            }
        }

        private DevExpress.Xpf.Editors.Popups.SelectionViewModel SelectionViewModel =>
            ((ISelectorEditPropertyProvider) ActualPropertyProvider.GetProperties((BaseEdit) this.OwnerEdit)).SelectionViewModel;

        private bool IsSelectionChangerActive
        {
            get
            {
                object entity = ReflectionHelper.GetPropertyValue<object>(this, "SelectionChange", BindingFlags.NonPublic | BindingFlags.Instance);
                return ReflectionHelper.GetPropertyValue<bool>(entity, "IsActive", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        public ISelectorEditStrategy EditStrategy =>
            this.OwnerEdit.EditStrategy as ISelectorEditStrategy;

        public DevExpress.Xpf.Editors.ScrollUnit ScrollUnit2
        {
            get => 
                (DevExpress.Xpf.Editors.ScrollUnit) base.GetValue(ScrollUnit2Property);
            set => 
                base.SetValue(ScrollUnit2Property, value);
        }

        protected Locker SelectionLocker { get; private set; }

        protected Locker SynchronizationLocker { get; private set; }

        protected DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode { get; set; }

        private Locker InnerItemSynchronizationLocker { get; set; }

        private Locker SelectionChangedLocker { get; set; }

        private PostponedAction SelectAllAction { get; set; }

        private bool IsServerMode =>
            this.OwnerEdit.ItemsProvider.IsServerMode;

        private bool IsSyncServerMode =>
            this.OwnerEdit.ItemsProvider.IsSyncServerMode;

        internal bool IsAsyncServerMode =>
            this.OwnerEdit.ItemsProvider.IsAsyncServerMode;

        public bool AllowItemHighlighting
        {
            get => 
                (bool) base.GetValue(AllowItemHighlightingProperty);
            set => 
                base.SetValue(AllowItemHighlightingProperty, value);
        }

        public ISelectorEdit OwnerEdit =>
            (ISelectorEdit) BaseEdit.GetOwnerEdit(this);

        public bool? IsSelectAll =>
            this.ItemsSourceHelper.IsSelectAll();

        Locker ISelectorEditInnerListBox.SelectAllLocker =>
            this.selectAllLocker;

        private ISelectorEditInnerListBox SelectorEditInnerListBox =>
            this;

        Style ISelectorEditInnerListBox.ItemContainerStyle =>
            base.ItemContainerStyle;

        ObservableCollection<GroupStyle> ISelectorEditInnerListBox.GroupStyle =>
            base.GroupStyle;

        DataTemplateSelector ISelectorEditInnerListBox.ItemTemplateSelector
        {
            get => 
                base.ItemTemplateSelector;
            set => 
                base.ItemTemplateSelector = value;
        }

        ItemsPanelTemplate ISelectorEditInnerListBox.ItemsPanel =>
            base.ItemsPanel;

        IEnumerable ISelectorEditInnerListBox.ItemsSource
        {
            get => 
                base.ItemsSource;
            set => 
                base.ItemsSource = value;
        }

        IList ISelectorEditInnerListBox.SelectedItems =>
            base.SelectedItems;

        object ISelectorEditInnerListBox.SelectedItem
        {
            get => 
                base.SelectedItem;
            set => 
                base.SelectedItem = value;
        }

        int ISelectorEditInnerListBox.SelectedIndex
        {
            get => 
                base.SelectedIndex;
            set => 
                base.SelectedIndex = value;
        }

        ItemContainerGenerator ISelectorEditInnerListBox.ItemContainerGenerator =>
            base.ItemContainerGenerator;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorListBox.<>c <>9 = new EditorListBox.<>c();
            public static Func<VisibleListWrapper, bool> <>9__100_1;
            public static Func<int, bool> <>9__116_0;
            public static Func<object, bool> <>9__117_0;

            internal void <.cctor>b__9_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((EditorListBox) o).ScrollUnit2Changed((DevExpress.Xpf.Editors.ScrollUnit) args.NewValue);
            }

            internal bool <GetSelectedItemsInServerMode>b__117_0(object x) => 
                x != null;

            internal bool <OnSelectionChangedInternal>b__100_1(VisibleListWrapper x) => 
                x.SelectionLocker.IsLocked;

            internal bool <SyncSelectedItems>b__116_0(int x) => 
                x > -1;
        }
    }
}

