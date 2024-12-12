namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Design.DataAccess;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;

    [ContentProperty("Items"), DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), LookupBindingProperties("ItemsSource", "DisplayMember", "ValueMember", "DisplayMember"), DataAccessMetadata("All", SupportedProcessingModes="GridLookup", EnableBindingToEnum=true), ComplexBindingProperties("ItemsSource", "ValueMember")]
    public class ListBoxEdit : BaseEdit, ISelectorEdit, IBaseEdit, IInputElement, IEventArgsConverterSource, IFilteredComponent, IFilteredComponentBase
    {
        public static TimeSpan TextSearchTimeOut = TextSearchEngineBase.DefaultTextSearchTimeout;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty ShowCustomItemsProperty;
        public static readonly DependencyProperty AllowCollectionViewProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty SelectionModeProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        protected static readonly DependencyPropertyKey SelectedItemsPropertyKey;
        public static readonly DependencyProperty SelectedItemsProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty ValueMemberProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly RoutedEvent SelectedIndexChangedEvent;
        public static readonly DependencyProperty AllowItemHighlightingProperty;
        public static readonly DependencyProperty AllowRejectUnknownValuesProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty AllowLiveDataShapingProperty;
        protected static readonly DependencyPropertyKey IsAsyncOperationInProgressPropertyKey;
        public static readonly DependencyProperty IsAsyncOperationInProgressProperty;
        public static readonly DependencyProperty ShowWaitIndicatorProperty;
        public static readonly DependencyProperty IncrementalSearchProperty;
        public static readonly DependencyProperty ScrollUnitProperty;
        public static readonly DependencyProperty SelectItemWithNullValueProperty;
        private readonly Locker selectedItemsInitialeLocker = new Locker();
        private object eventArgsConverter;
        private EventHandler propertiesChanged;
        private EventHandler rowFilterChanged;

        event EventHandler IFilteredComponentBase.PropertiesChanged
        {
            add
            {
                this.propertiesChanged += value;
            }
            remove
            {
                this.propertiesChanged -= value;
            }
        }

        event EventHandler IFilteredComponentBase.RowFilterChanged
        {
            add
            {
                this.rowFilterChanged += value;
            }
            remove
            {
                this.rowFilterChanged -= value;
            }
        }

        event RoutedEventHandler IBaseEdit.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        event RoutedEventHandler IBaseEdit.Unloaded
        {
            add
            {
                base.Unloaded += value;
            }
            remove
            {
                base.Unloaded -= value;
            }
        }

        [Category("Action")]
        public event RoutedEventHandler SelectedIndexChanged
        {
            add
            {
                base.AddHandler(SelectedIndexChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectedIndexChangedEvent, value);
            }
        }

        static ListBoxEdit()
        {
            Type forType = typeof(ListBoxEdit);
            BaseEdit.InvalidValueBehaviorProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(InvalidValueBehavior.AllowLeaveEditor));
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), forType, new FrameworkPropertyMetadata(null, (d, e) => ((ListBoxEdit) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue)));
            ShowCustomItemsProperty = DependencyPropertyManager.Register("ShowCustomItems", typeof(bool?), forType, new FrameworkPropertyMetadata(null, (d, e) => ((ListBoxEdit) d).ShowCustomItemsChanged((bool?) e.NewValue)));
            AllowCollectionViewProperty = DependencyPropertyManager.Register("AllowCollectionView", typeof(bool), forType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((ListBoxEdit) d).AllowCollectionViewChanged((bool) e.NewValue)));
            IsSynchronizedWithCurrentItemProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), forType, new FrameworkPropertyMetadata(false, (d, e) => ((ListBoxEdit) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue)));
            ItemContainerStyleProperty = ItemsControl.ItemContainerStyleProperty.AddOwner(forType);
            ItemsPanelProperty = ItemsControl.ItemsPanelProperty.AddOwner(forType, new FrameworkPropertyMetadata(ItemsControl.ItemsPanelProperty.GetMetadata(typeof(ItemsControl)).DefaultValue, new PropertyChangedCallback(ListBoxEdit.OnItemsPanelChanged)));
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(object), forType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ListBoxEdit.OnItemsSourceChanged)));
            SelectionModeProperty = ListBox.SelectionModeProperty.AddOwner(forType, new FrameworkPropertyMetadata(System.Windows.Controls.SelectionMode.Single, new PropertyChangedCallback(ListBoxEdit.SelectionModeChanged)));
            SelectedItemProperty = DependencyPropertyManager.Register("SelectedItem", typeof(object), forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ListBoxEdit.OnSelectedItemChanged), new CoerceValueCallback(ListBoxEdit.OnSelectedItemCoerce)));
            SelectedIndexProperty = DependencyPropertyManager.Register("SelectedIndex", typeof(int), forType, new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ListBoxEdit.OnSelectedIndexChanged), new CoerceValueCallback(ListBoxEdit.OnSelectedIndexCoerce)));
            SelectedItemsPropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectedItems", typeof(ObservableCollection<object>), forType, new PropertyMetadata(null, new PropertyChangedCallback(ListBoxEdit.OnSelectedItemsChanged)));
            SelectedItemsProperty = SelectedItemsPropertyKey.DependencyProperty;
            SelectedIndexChangedEvent = EventManager.RegisterRoutedEvent("SelectedIndexChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), forType);
            DisplayMemberProperty = DependencyPropertyManager.Register("DisplayMember", typeof(string), forType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ListBoxEdit.OnDisplayMemberChanged)));
            ValueMemberProperty = DependencyPropertyManager.Register("ValueMember", typeof(string), forType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ListBoxEdit.OnValueMemberChanged)));
            ItemTemplateProperty = ItemsControl.ItemTemplateProperty.AddOwner(forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ListBoxEdit.OnItemTemplateChanged)));
            AllowItemHighlightingProperty = DependencyPropertyManager.Register("AllowItemHighlighting", typeof(bool), forType, new FrameworkPropertyMetadata(false));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ListBoxEdit), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            AllowRejectUnknownValuesProperty = DependencyPropertyRegistrator.Register<ListBoxEdit, bool>(System.Linq.Expressions.Expression.Lambda<Func<ListBoxEdit, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ListBoxEdit.get_AllowRejectUnknownValues)), parameters), false, (@base, value, newValue) => @base.AllowRejectUnknownValuesChanged(newValue));
            BaseEdit.EditValueProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            ItemTemplateSelectorProperty = ItemsControl.ItemTemplateSelectorProperty.AddOwner(forType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ListBoxEdit.OnItemTemplateSelectorChanged)));
            AllowLiveDataShapingProperty = DependencyPropertyManager.Register("AllowLiveDataShaping", typeof(bool), forType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (o, args) => ((ListBoxEdit) o).AllowLiveDataShapingChanged((bool) args.NewValue)));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ListBoxEdit), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            IsAsyncOperationInProgressPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<ListBoxEdit, bool>(System.Linq.Expressions.Expression.Lambda<Func<ListBoxEdit, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ListBoxEdit.get_IsAsyncOperationInProgress)), expressionArray2), false, (owner, oldValue, newValue) => owner.IsAsyncOperationInProgressChanged(oldValue, newValue));
            IsAsyncOperationInProgressProperty = IsAsyncOperationInProgressPropertyKey.DependencyProperty;
            ShowWaitIndicatorProperty = DependencyPropertyManager.Register("ShowWaitIndicator", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            IncrementalSearchProperty = DependencyPropertyManager.Register("IncrementalSearch", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            ScrollUnitProperty = DependencyPropertyManager.Register("ScrollUnit", typeof(DevExpress.Xpf.Editors.ScrollUnit), forType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ScrollUnit.Pixel));
            SelectItemWithNullValueProperty = DependencyPropertyManager.Register("SelectItemWithNullValue", typeof(bool), forType, new PropertyMetadata(true, (d, e) => ((ListBoxEdit) d).SelectItemWithNullValueChanged((bool) e.NewValue)));
        }

        public ListBoxEdit()
        {
            using (this.selectedItemsInitialeLocker.Lock())
            {
                ObservableCollection<object> observables = new ObservableCollection<object>();
                observables.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSelectedItemsCollectionChanged);
                this.SelectedItems = observables;
            }
            Action<ListBoxEdit, object, ItemsProviderChangedEventArgs> onEventAction = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Action<ListBoxEdit, object, ItemsProviderChangedEventArgs> local1 = <>c.<>9__48_0;
                onEventAction = <>c.<>9__48_0 = (owner, o, e) => owner.EditStrategy.ItemsProviderChanged(e);
            }
            this.ItemsProviderChangedEventHandler = new ItemsProviderChangedEventHandler<ListBoxEdit>(this, onEventAction);
        }

        protected virtual void AllowCollectionViewChanged(bool value)
        {
            this.EditStrategy.AllowCollectionViewChanged(value);
        }

        protected virtual void AllowLiveDataShapingChanged(bool newValue)
        {
            this.Settings.AllowLiveDataShaping = newValue;
        }

        protected virtual void AllowRejectUnknownValuesChanged(bool newValue)
        {
            this.EditStrategy.AllowRejectUnknownValuesChanged(newValue);
        }

        public void BeginDataUpdate()
        {
            this.ItemsProvider.BeginInit();
        }

        protected virtual int CoerceSelectedIndex(int index) => 
            this.EditStrategy.CoerceSelectedIndex(index);

        protected virtual object CoerceSelectedItem(object item) => 
            this.EditStrategy.CoerceSelectedItem(item);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new ListBoxEditBasePropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new ListBoxEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new ListBoxEditStyleSettings();

        IEnumerable<FilterColumn> IFilteredComponent.CreateFilterColumnCollection() => 
            FilteredComponentHelper.GetColumnsByItemsSource(this, this.ItemsSource, false);

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        object ISelectorEdit.GetCurrentSelectedItem() => 
            this.EditStrategy.GetCurrentSelectedItem();

        IEnumerable ISelectorEdit.GetCurrentSelectedItems() => 
            this.EditStrategy.GetCurrentSelectedItems();

        IEnumerable ISelectorEdit.GetPopupContentCustomItemsSource() => 
            ((ISelectorEditStrategy) this.EditStrategy).GetInnerEditorCustomItemsSource();

        object ISelectorEdit.GetPopupContentItemsSource() => 
            ((ISelectorEditStrategy) this.EditStrategy).GetInnerEditorItemsSource();

        IEnumerable ISelectorEdit.GetPopupContentMRUItemsSource() => 
            ((ISelectorEditStrategy) this.EditStrategy).GetInnerEditorMRUItemsSource();

        public void EndDataUpdate()
        {
            this.ItemsProvider.EndInit();
        }

        protected virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.EditStrategy.FilterCriteriaChanged(criteriaOperator);
            this.RaiseFilteredComponentRowFilterChanged();
        }

        protected override bool FocusEditCore()
        {
            if (base.Focusable)
            {
                this.UpdateSelectedItemFocus();
            }
            return true;
        }

        protected virtual void IsAsyncOperationInProgressChanged(bool oldValue, bool newValue)
        {
            this.PropertyProvider.ShowWaitIndicator = this.ShowWaitIndicator & newValue;
        }

        protected virtual void IsSynchronizedWithCurrentItemChanged(bool value)
        {
            this.EditStrategy.IsSynchronizedWithCurrentItemChanged(value);
        }

        protected internal override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            ((base.EditMode != EditMode.InplaceActive) || ModifierKeysHelper.IsCtrlPressed(modifiers)) ? base.NeedsKey(key, modifiers) : false;

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ListBoxEditAutomationPeer(this);

        protected virtual void OnDisplayMemberChanged()
        {
            this.Settings.DisplayMember = this.DisplayMember;
            this.UpdateDisplayMemberPath();
            this.RaiseFilteredComponentPropertieChanged();
        }

        private static void OnDisplayMemberChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnDisplayMemberChanged();
        }

        protected override void OnEditCoreAssigned()
        {
            base.OnEditCoreAssigned();
            this.EditStrategy.ApplyStyleSettings(this.PropertyProvider.StyleSettings);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.UpdateSelectedItemFocus();
        }

        protected virtual void OnItemsPanelChanged()
        {
            this.Settings.ItemsPanel = this.ItemsPanel;
        }

        private static void OnItemsPanelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnItemsPanelChanged();
        }

        protected virtual void OnItemsSourceChanged(object itemsSource)
        {
            this.EditStrategy.ItemSourceChanged(itemsSource);
            this.RaiseFilteredComponentPropertieChanged();
        }

        private static void OnItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnItemsSourceChanged(e.NewValue);
        }

        protected virtual void OnItemTemplateChanged()
        {
            this.UpdateHasItemTemplate();
            this.UpdateDisplayMemberPath();
        }

        private static void OnItemTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnItemTemplateChanged();
        }

        protected virtual void OnItemTemplateSelectorChanged()
        {
            this.UpdateHasItemTemplate();
            this.Settings.ItemTemplateSelector = this.ItemTemplateSelector;
        }

        private static void OnItemTemplateSelectorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnItemTemplateSelectorChanged();
        }

        protected virtual void OnSelectedIndexChanged(int oldSelectedIndex, int selectedIndex)
        {
            this.EditStrategy.SelectedIndexChanged(oldSelectedIndex, selectedIndex);
        }

        private static void OnSelectedIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnSelectedIndexChanged((int) e.OldValue, (int) e.NewValue);
        }

        protected virtual object OnSelectedIndexCoerce(object baseValue) => 
            this.EditStrategy.CoerceSelectedIndex((int) baseValue);

        protected static object OnSelectedIndexCoerce(DependencyObject obj, object baseValue) => 
            ((ListBoxEdit) obj).OnSelectedIndexCoerce(baseValue);

        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            this.EditStrategy.SelectedItemChanged(oldValue, newValue);
        }

        private static void OnSelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnSelectedItemChanged(e.OldValue, e.NewValue);
        }

        protected virtual object OnSelectedItemCoerce(object baseValue) => 
            this.EditStrategy.CoerceSelectedItem(baseValue);

        protected static object OnSelectedItemCoerce(DependencyObject obj, object baseValue) => 
            ((ListBoxEdit) obj).OnSelectedItemCoerce(baseValue);

        protected virtual void OnSelectedItemsChanged(IList selectedItems)
        {
            if (!this.selectedItemsInitialeLocker.IsLocked)
            {
                this.EditStrategy.SelectedItemsChanged(null, selectedItems);
            }
        }

        protected virtual void OnSelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.EditStrategy.SelectedItemsChanged(null, this.SelectedItems);
        }

        private static void OnSelectedItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnSelectedItemsChanged((IList) e.NewValue);
        }

        private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.EditStrategy.SelectedItemsChanged(null, this.SelectedItems);
        }

        protected virtual void OnValueMemberChanged()
        {
            this.Settings.ValueMember = this.ValueMember;
            this.RaiseFilteredComponentPropertieChanged();
        }

        private static void OnValueMemberChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).OnValueMemberChanged();
        }

        private void RaiseFilteredComponentPropertieChanged()
        {
            this.rowFilterChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        private void RaiseFilteredComponentRowFilterChanged()
        {
            this.rowFilterChanged.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        public void ScrollIntoView(object item)
        {
            if (this.ListBoxCore != null)
            {
                this.ListBoxCore.ScrollIntoView(item);
            }
        }

        public override void SelectAll()
        {
            if (base.EditMode == EditMode.Standalone)
            {
                this.EditStrategy.SelectAll();
            }
            else
            {
                this.UpdateSelectedItemFocus();
            }
        }

        protected virtual void SelectionModeChanged()
        {
            this.EditStrategy.ApplyStyleSettings(this.PropertyProvider.StyleSettings);
            this.EditStrategy.SyncWithValue();
        }

        private static void SelectionModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEdit) obj).SelectionModeChanged();
        }

        protected virtual void SelectItemWithNullValueChanged(bool newValue)
        {
            this.Settings.SelectItemWithNullValue = newValue;
        }

        protected virtual void ShowCustomItemsChanged(bool? value)
        {
            this.EditStrategy.ShowCustomItemsChanged(value);
        }

        protected internal virtual void SubscribeToItemsProviderChanged()
        {
            this.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
        }

        protected override void SubscribeToSettings(BaseEditSettings settings)
        {
            base.SubscribeToSettings(settings);
            ListBoxEditSettings settings2 = (ListBoxEditSettings) settings;
            if (settings2 != null)
            {
                settings2.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        public void UnSelectAll()
        {
            this.EditStrategy.UnSelectAll();
        }

        protected override void UnsubscribeFromSettings(BaseEditSettings settings)
        {
            base.UnsubscribeFromSettings(settings);
            ListBoxEditSettings settings2 = (ListBoxEditSettings) settings;
            if (settings2 != null)
            {
                settings2.ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        private void UpdateDisplayMemberPath()
        {
            IValueConverter converter = new PopupListBoxDisplayMemberPathConverter();
            this.PropertyProvider.DisplayMemberPath = (string) converter.Convert(this, null, null, null);
        }

        private void UpdateHasItemTemplate()
        {
            this.PropertyProvider.HasItemTemplate = (this.ItemTemplate != null) || (this.ItemTemplateSelector != null);
        }

        private void UpdateSelectedItemFocus()
        {
            if ((this.ListBoxCore != null) && (base.EditMode != EditMode.InplaceInactive))
            {
                this.ListBoxCore.FocusSelectedItem();
            }
        }

        private ItemsProviderChangedEventHandler<ListBoxEdit> ItemsProviderChangedEventHandler { get; set; }

        public bool SelectItemWithNullValue
        {
            get => 
                (bool) base.GetValue(SelectItemWithNullValueProperty);
            set => 
                base.SetValue(SelectItemWithNullValueProperty, value);
        }

        public DevExpress.Xpf.Editors.ScrollUnit ScrollUnit
        {
            get => 
                (DevExpress.Xpf.Editors.ScrollUnit) base.GetValue(ScrollUnitProperty);
            set => 
                base.SetValue(ScrollUnitProperty, value);
        }

        public bool IsAsyncOperationInProgress
        {
            get => 
                (bool) base.GetValue(IsAsyncOperationInProgressProperty);
            internal set => 
                base.SetValue(IsAsyncOperationInProgressPropertyKey, value);
        }

        public bool IncrementalSearch
        {
            get => 
                (bool) base.GetValue(IncrementalSearchProperty);
            set => 
                base.SetValue(IncrementalSearchProperty, value);
        }

        public bool ShowWaitIndicator
        {
            get => 
                (bool) base.GetValue(ShowWaitIndicatorProperty);
            set => 
                base.SetValue(ShowWaitIndicatorProperty, value);
        }

        public bool AllowLiveDataShaping
        {
            get => 
                (bool) base.GetValue(AllowLiveDataShapingProperty);
            set => 
                base.SetValue(AllowLiveDataShapingProperty, value);
        }

        public bool AllowRejectUnknownValues
        {
            get => 
                (bool) base.GetValue(AllowRejectUnknownValuesProperty);
            set => 
                base.SetValue(AllowRejectUnknownValuesProperty, value);
        }

        public ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle =>
            this.Settings.GroupStyle;

        [Description("Gets or sets the listbox's data source. This is a dependency property."), Category("Common Properties"), Bindable(true)]
        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Description("Gets or sets how list items are selected. This is a dependency property."), Category("Common Properties")]
        public System.Windows.Controls.SelectionMode SelectionMode
        {
            get => 
                (System.Windows.Controls.SelectionMode) base.GetValue(SelectionModeProperty);
            set => 
                base.SetValue(SelectionModeProperty, value);
        }

        [Category("Common Properties"), Description("Gets or sets the index of the currently selected item. This is a dependency property.")]
        public int SelectedIndex
        {
            get => 
                (int) base.GetValue(SelectedIndexProperty);
            set => 
                base.SetValue(SelectedIndexProperty, value);
        }

        [Description("Gets or sets the currently selected item. This is a dependency property."), Category("Common Properties")]
        public object SelectedItem
        {
            get => 
                base.GetValue(SelectedItemProperty);
            set => 
                base.SetValue(SelectedItemProperty, value);
        }

        [Category("Common Properties"), Description("Gets or sets a field name in the bound data source whose contents are to be displayed by the list box. This is a dependency property."), DefaultValue("")]
        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        [Description("Gets or sets a field name in the bound data source, whose contents are assigned to item values. This is a dependency property."), Category("Common Properties"), DefaultValue("")]
        public string ValueMember
        {
            get => 
                (string) base.GetValue(ValueMemberProperty);
            set => 
                base.SetValue(ValueMemberProperty, value);
        }

        [Description("Gets the collection of selected items. This is a dependency property."), Category("Common Properties")]
        public ObservableCollection<object> SelectedItems
        {
            get => 
                (ObservableCollection<object>) base.GetValue(SelectedItemsProperty);
            private set => 
                base.SetValue(SelectedItemsPropertyKey, value);
        }

        [Description("Gets or sets whether list box items are highlighted on mouse hover. This is a dependency property.")]
        public bool AllowItemHighlighting
        {
            get => 
                (bool) base.GetValue(AllowItemHighlightingProperty);
            set => 
                base.SetValue(AllowItemHighlightingProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        [TypeConverter(typeof(FilterCriteriaConverter))]
        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        [Browsable(false)]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Category("Behavior")]
        public bool AllowCollectionView
        {
            get => 
                (bool) base.GetValue(AllowCollectionViewProperty);
            set => 
                base.SetValue(AllowCollectionViewProperty, value);
        }

        [Category("Behavior")]
        public bool IsSynchronizedWithCurrentItem
        {
            get => 
                (bool) base.GetValue(IsSynchronizedWithCurrentItemProperty);
            set => 
                base.SetValue(IsSynchronizedWithCurrentItemProperty, value);
        }

        protected internal ListBoxEditSettings Settings =>
            (ListBoxEditSettings) base.Settings;

        protected internal IItemsProvider2 ItemsProvider =>
            this.Settings.ItemsProvider;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the collection of items when the ListBoxEdit is not bound to a data source."), Category("Common Properties")]
        public ListItemCollection Items =>
            this.Settings.Items;

        public ICommand SelectAllItemsCommand { get; private set; }

        private ListBoxEditBasePropertyProvider PropertyProvider =>
            base.PropertyProvider as ListBoxEditBasePropertyProvider;

        public bool? ShowCustomItems
        {
            get => 
                (bool?) base.GetValue(ShowCustomItemsProperty);
            set => 
                base.SetValue(ShowCustomItemsProperty, value);
        }

        [Browsable(true), Category("Behavior")]
        public BaseEditStyleSettings StyleSettings
        {
            get => 
                base.StyleSettings;
            set => 
                base.StyleSettings = value;
        }

        protected internal override Type StyleSettingsType =>
            typeof(BaseListBoxEditStyleSettings);

        protected internal EditorListBox ListBoxCore =>
            base.EditCore as EditorListBox;

        protected ListBoxEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as ListBoxEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        EditStrategyBase ISelectorEdit.EditStrategy =>
            this.EditStrategy;

        SelectionEventMode ISelectorEdit.SelectionEventMode =>
            this.PropertyProvider.SelectionEventMode;

        bool ISelectorEdit.UseCustomItems =>
            this.PropertyProvider.ShowCustomItems();

        ISelectionProvider ISelectorEdit.SelectionProvider =>
            new SelectionProvider((ISelectorEditInnerListBox) base.EditCore);

        IListNotificationOwner ISelectorEdit.ListNotificationOwner =>
            this.Settings;

        IItemsProvider2 ISelectorEdit.ItemsProvider =>
            this.ItemsProvider;

        object IEventArgsConverterSource.EventArgsConverter
        {
            get
            {
                object eventArgsConverter = this.eventArgsConverter;
                if (this.eventArgsConverter == null)
                {
                    object local1 = this.eventArgsConverter;
                    eventArgsConverter = this.eventArgsConverter = new ListBoxEditEventArgsConverter(this);
                }
                return eventArgsConverter;
            }
        }

        CriteriaOperator IFilteredComponentBase.RowCriteria
        {
            get => 
                this.FilterCriteria;
            set => 
                this.FilterCriteria = value;
        }

        object IBaseEdit.DataContext
        {
            get => 
                base.DataContext;
            set => 
                base.DataContext = value;
        }

        HorizontalAlignment IBaseEdit.HorizontalContentAlignment
        {
            get => 
                base.HorizontalContentAlignment;
            set => 
                base.HorizontalContentAlignment = value;
        }

        VerticalAlignment IBaseEdit.VerticalContentAlignment
        {
            get => 
                base.VerticalContentAlignment;
            set => 
                base.VerticalContentAlignment = value;
        }

        double IBaseEdit.MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxEdit.<>c <>9 = new ListBoxEdit.<>c();
            public static Action<ListBoxEdit, object, ItemsProviderChangedEventArgs> <>9__48_0;

            internal void <.cctor>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEdit) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue);
            }

            internal void <.cctor>b__27_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEdit) d).ShowCustomItemsChanged((bool?) e.NewValue);
            }

            internal void <.cctor>b__27_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEdit) d).AllowCollectionViewChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEdit) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_4(ListBoxEdit @base, bool value, bool newValue)
            {
                @base.AllowRejectUnknownValuesChanged(newValue);
            }

            internal void <.cctor>b__27_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ListBoxEdit) o).AllowLiveDataShapingChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__27_6(ListBoxEdit owner, bool oldValue, bool newValue)
            {
                owner.IsAsyncOperationInProgressChanged(oldValue, newValue);
            }

            internal void <.cctor>b__27_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEdit) d).SelectItemWithNullValueChanged((bool) e.NewValue);
            }

            internal void <.ctor>b__48_0(ListBoxEdit owner, object o, ItemsProviderChangedEventArgs e)
            {
                owner.EditStrategy.ItemsProviderChanged(e);
            }
        }
    }
}

