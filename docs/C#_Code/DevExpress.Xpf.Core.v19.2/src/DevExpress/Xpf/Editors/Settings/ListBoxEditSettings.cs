namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Design.DataAccess;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    [DataAccessMetadata("All", SupportedProcessingModes="GridLookup", DataSourceProperty="ItemsSource", Platform="WPF"), LookupBindingProperties("ItemsSource", "DisplayMember", "ValueMember", "DisplayMember")]
    public class ListBoxEditSettings : BaseEditSettings, IItemsProviderOwner, IListNotificationOwner
    {
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty AllowCollectionViewProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty SelectionModeProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty ValueMemberProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty AllowRejectUnknownValuesProperty;
        public static readonly DependencyProperty AllowLiveDataShapingProperty;
        public static readonly DependencyProperty SelectItemWithNullValueProperty;
        private IItemsProvider2 itemsProvider;
        private ListItemCollection items;
        private ObservableCollection<object> mruItems;
        private ObservableCollection<System.Windows.Controls.GroupStyle> groupStyle;

        static ListBoxEditSettings()
        {
            Type ownerType = typeof(ListBoxEditSettings);
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new PropertyMetadata(null, (d, e) => ((ListBoxEditSettings) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue)));
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ListBoxEditSettings.OnItemsSourceChanged)));
            SelectionModeProperty = ListBoxEdit.SelectionModeProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.SelectionMode.Single, new PropertyChangedCallback(ListBoxEditSettings.OnSelectionModeChanged)));
            DisplayMemberProperty = ListBoxEdit.DisplayMemberProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(ListBoxEditSettings.OnDisplayMemberChanged)));
            ValueMemberProperty = ListBoxEdit.ValueMemberProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(ListBoxEditSettings.OnValueMemberChanged)));
            ItemTemplateProperty = ListBoxEdit.ItemTemplateProperty.AddOwner(ownerType);
            ItemsPanelProperty = ListBoxEdit.ItemsPanelProperty.AddOwner(ownerType);
            AllowCollectionViewProperty = DependencyPropertyManager.Register("AllowCollectionView", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((ListBoxEditSettings) d).AllowCollectionViewChanged((bool) e.NewValue)));
            IsSynchronizedWithCurrentItemProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((ListBoxEditSettings) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue)));
            ItemTemplateSelectorProperty = ListBoxEdit.ItemTemplateSelectorProperty.AddOwner(typeof(ListBoxEditSettings));
            AllowRejectUnknownValuesProperty = ListBoxEdit.AllowRejectUnknownValuesProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (o, args) => ((ListBoxEditSettings) o).AllowRejectUnknownValuesChanged((bool) args.NewValue)));
            AllowLiveDataShapingProperty = ListBoxEdit.AllowLiveDataShapingProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (o, args) => ((ListBoxEditSettings) o).AllowLiveDataShapingChanged((bool) args.NewValue)));
            SelectItemWithNullValueProperty = ListBoxEdit.SelectItemWithNullValueProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None));
        }

        protected virtual void AllowCollectionViewChanged(bool value)
        {
            this.ItemsProvider.Reset();
            this.OnSettingsChanged();
        }

        private void AllowLiveDataShapingChanged(bool newValue)
        {
            this.UpdateItemsSource();
        }

        protected virtual void AllowRejectUnknownValuesChanged(bool newValue)
        {
            this.ItemsProvider.UpdateValueMember();
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            ListBoxEdit listBox = edit as ListBoxEdit;
            if (listBox != null)
            {
                base.SetValueFromSettings(ItemsSourceProperty, () => listBox.ItemsSource = this.ItemsSource);
                base.SetValueFromSettings(DisplayMemberProperty, () => listBox.DisplayMember = this.DisplayMember);
                base.SetValueFromSettings(ValueMemberProperty, () => listBox.ValueMember = this.ValueMember);
                base.SetValueFromSettings(AllowRejectUnknownValuesProperty, () => listBox.AllowRejectUnknownValues = this.AllowRejectUnknownValues);
                base.SetValueFromSettings(SelectItemWithNullValueProperty, () => listBox.SelectItemWithNullValue = this.SelectItemWithNullValue);
                base.SetValueFromSettings(AllowLiveDataShapingProperty, () => listBox.AllowLiveDataShaping = this.AllowLiveDataShaping);
                base.SetValueFromSettings(ItemTemplateProperty, () => listBox.ItemTemplate = this.ItemTemplate, () => this.ClearEditorPropertyIfNeeded(listBox, ListBoxEdit.ItemTemplateProperty, ItemTemplateProperty));
                base.SetValueFromSettings(SelectionModeProperty, () => listBox.SelectionMode = this.SelectionMode);
                base.SetValueFromSettings(IsSynchronizedWithCurrentItemProperty, () => listBox.IsSynchronizedWithCurrentItem = this.IsSynchronizedWithCurrentItem);
                base.SetValueFromSettings(AllowCollectionViewProperty, () => listBox.AllowCollectionView = this.AllowCollectionView);
                base.SetValueFromSettings(FilterCriteriaProperty, () => listBox.FilterCriteria = this.FilterCriteria);
                base.SetValueFromSettings(ItemTemplateSelectorProperty, () => listBox.ItemTemplateSelector = this.ItemTemplateSelector);
                if (this.ItemsPanel != null)
                {
                    base.SetValueFromSettings(ItemsPanelProperty, () => listBox.ItemsPanel = this.ItemsPanel);
                }
                if (!ReferenceEquals(listBox.Settings, this))
                {
                    listBox.Items.Assign(this.Items);
                }
            }
        }

        protected virtual IItemsProvider2 CreateItemsProvider() => 
            new ItemsProvider2(this);

        void IListNotificationOwner.OnCollectionChanged(NotifyItemsProviderChangedEventArgs e)
        {
            this.OnCollectionChanged(e);
        }

        void IListNotificationOwner.OnCollectionChanged(NotifyItemsProviderSelectionChangedEventArgs e)
        {
            this.OnCollectionChanged(e);
        }

        protected virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.ItemsProvider.UpdateFilterCriteria();
        }

        protected virtual void IsSynchronizedWithCurrentItemChanged(bool value)
        {
            this.ItemsProvider.SyncWithCurrentItem();
            this.OnSettingsChanged();
        }

        protected virtual void OnCollectionChanged(NotifyItemsProviderChangedEventArgs e)
        {
            this.ItemsProvider.ProcessCollectionChanged(e);
            this.OnSettingsChanged();
        }

        protected virtual void OnCollectionChanged(NotifyItemsProviderSelectionChangedEventArgs e)
        {
            this.ItemsProvider.ProcessSelectionChanged(e);
            this.OnSettingsChanged();
        }

        protected virtual void OnDisplayMemberChanged()
        {
            this.ItemsProvider.UpdateDisplayMember();
            this.OnSettingsChanged();
        }

        private static void OnDisplayMemberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEditSettings) d).OnDisplayMemberChanged();
        }

        protected virtual void OnItemsSourceChanged(object itemsSource)
        {
            this.UpdateItemsSource();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEditSettings) d).OnItemsSourceChanged(e.NewValue);
        }

        protected virtual void OnSelectionModeChanged()
        {
            this.OnSettingsChanged();
        }

        private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEditSettings) d).OnSelectionModeChanged();
        }

        protected virtual void OnValueMemberChanged()
        {
            this.ItemsProvider.UpdateValueMember();
        }

        private static void OnValueMemberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ListBoxEditSettings) d).OnValueMemberChanged();
        }

        private void UpdateItemsSource()
        {
            this.ItemsProvider.UpdateItemsSource();
            this.ItemsProvider.SyncWithCurrentItem();
        }

        [Category("Data"), Description("Gets or sets the listbox's data source. This is a dependency property.")]
        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets how list items are selected. This is a dependency property.")]
        public System.Windows.Controls.SelectionMode SelectionMode
        {
            get => 
                (System.Windows.Controls.SelectionMode) base.GetValue(SelectionModeProperty);
            set => 
                base.SetValue(SelectionModeProperty, value);
        }

        [Category("Appearance "), Description("Gets or sets a template that defines the presentation of items contained within the list. This is a dependency property.")]
        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        [Category("Appearance "), Description("Gets or sets an object that chooses a list item template based on custom logic. This is a dependency property.")]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Category("Appearance "), Description("Gets or sets the template that defines the presentation of a container panel used by the editor to arrange its items. This is a dependency property.")]
        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        [Category("Data"), Description("Gets or sets a field name in the bound data source whose contents are to be displayed by the list box. This is a dependency property.")]
        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        [Category("Data"), Description("Gets or sets a field name in the bound data source whose contents are assigned to item values. This is a dependency property.")]
        public string ValueMember
        {
            get => 
                (string) base.GetValue(ValueMemberProperty);
            set => 
                base.SetValue(ValueMemberProperty, value);
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

        [Category("Behavior")]
        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        [Category("Behavior")]
        public bool AllowRejectUnknownValues
        {
            get => 
                (bool) base.GetValue(AllowRejectUnknownValuesProperty);
            set => 
                base.SetValue(AllowRejectUnknownValuesProperty, value);
        }

        [Category("Behavior")]
        public bool AllowLiveDataShaping
        {
            get => 
                (bool) base.GetValue(AllowLiveDataShapingProperty);
            set => 
                base.SetValue(AllowLiveDataShapingProperty, value);
        }

        [Category("Behavior")]
        public bool SelectItemWithNullValue
        {
            get => 
                (bool) base.GetValue(SelectItemWithNullValueProperty);
            set => 
                base.SetValue(SelectItemWithNullValueProperty, value);
        }

        protected internal IItemsProvider2 ItemsProvider
        {
            get
            {
                IItemsProvider2 itemsProvider = this.itemsProvider;
                if (this.itemsProvider == null)
                {
                    IItemsProvider2 local1 = this.itemsProvider;
                    itemsProvider = this.itemsProvider = this.CreateItemsProvider();
                }
                return itemsProvider;
            }
        }

        [Description("Provides access to the collection of items when the ListBoxEdit is not bound to a data source.")]
        public ListItemCollection Items
        {
            get
            {
                ListItemCollection items = this.items;
                if (this.items == null)
                {
                    ListItemCollection local1 = this.items;
                    items = this.items = new ListItemCollection(this);
                }
                return items;
            }
        }

        public ObservableCollection<object> MRUItems
        {
            get
            {
                ObservableCollection<object> mruItems = this.mruItems;
                if (this.mruItems == null)
                {
                    ObservableCollection<object> local1 = this.mruItems;
                    mruItems = this.mruItems = new ObservableCollection<object>();
                }
                return mruItems;
            }
        }

        public ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle
        {
            get
            {
                ObservableCollection<System.Windows.Controls.GroupStyle> groupStyle = this.groupStyle;
                if (this.groupStyle == null)
                {
                    ObservableCollection<System.Windows.Controls.GroupStyle> local1 = this.groupStyle;
                    groupStyle = this.groupStyle = new ObservableCollection<System.Windows.Controls.GroupStyle>();
                }
                return groupStyle;
            }
        }

        IItemsProvider2 IItemsProviderOwner.ItemsProvider =>
            this.ItemsProvider;

        bool IItemsProviderOwner.IsCaseSensitiveFilter =>
            false;

        bool IItemsProviderOwner.IsLoaded { get; set; }

        bool IItemsProviderOwner.IsInLookUpMode =>
            this.AllowRejectUnknownValues || (!string.IsNullOrEmpty(this.DisplayMember) || !string.IsNullOrEmpty(this.ValueMember));

        Dispatcher IItemsProviderOwner.Dispatcher =>
            base.Dispatcher;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxEditSettings.<>c <>9 = new ListBoxEditSettings.<>c();

            internal void <.cctor>b__13_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEditSettings) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue);
            }

            internal void <.cctor>b__13_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEditSettings) d).AllowCollectionViewChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__13_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEditSettings) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__13_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ListBoxEditSettings) o).AllowRejectUnknownValuesChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__13_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ListBoxEditSettings) o).AllowLiveDataShapingChanged((bool) args.NewValue);
            }
        }
    }
}

