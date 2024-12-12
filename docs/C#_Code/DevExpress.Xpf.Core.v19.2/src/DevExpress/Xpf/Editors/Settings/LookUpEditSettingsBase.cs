namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Design.DataAccess;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    [LookupBindingProperties("ItemsSource", "DisplayMember", "ValueMember", "DisplayMember"), DataAccessMetadata("All", SupportedProcessingModes="GridLookup", DataSourceProperty="ItemsSource", Platform="WPF")]
    public abstract class LookUpEditSettingsBase : PopupBaseEditSettings, IItemsProviderOwner, IListNotificationOwner
    {
        private IItemsProvider2 itemsProvider;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty AllowCollectionViewProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ApplyItemTemplateToSelectedItemProperty;
        public static readonly DependencyProperty AutoCompleteProperty;
        public static readonly DependencyProperty IsCaseSensitiveSearchProperty;
        public static readonly DependencyProperty IsCaseSensitiveFilterProperty;
        public static readonly DependencyProperty ImmediatePopupProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty ValueMemberProperty;
        public static readonly DependencyProperty IncrementalFilteringProperty;
        public static readonly DependencyProperty SeparatorStringProperty;
        public static readonly DependencyProperty AddNewButtonPlacementProperty;
        public static readonly DependencyProperty FindButtonPlacementProperty;
        public static readonly DependencyProperty FilterConditionProperty;
        public static readonly DependencyProperty FindModeProperty;
        public static readonly DependencyProperty AllowRejectUnknownValuesProperty;
        public static readonly DependencyProperty AllowItemHighlightingProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty AllowLiveDataShapingProperty;
        public static readonly DependencyProperty SelectItemWithNullValueProperty;
        private bool isEnumItemsSourceAssigned;

        static LookUpEditSettingsBase()
        {
            Type ownerType = typeof(LookUpEditSettingsBase);
            AllowCollectionViewProperty = DependencyPropertyManager.Register("AllowCollectionView", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditSettingsBase) d).AllowCollectionViewChanged((bool) e.NewValue)));
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditSettingsBase) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue)));
            IsSynchronizedWithCurrentItemProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((LookUpEditSettingsBase) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue)));
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(LookUpEditSettingsBase.OnItemsSourceChanged)));
            ItemTemplateProperty = LookUpEditBase.ItemTemplateProperty.AddOwner(ownerType);
            ItemsPanelProperty = LookUpEditBase.ItemsPanelProperty.AddOwner(ownerType);
            ApplyItemTemplateToSelectedItemProperty = LookUpEditBase.ApplyItemTemplateToSelectedItemProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(false));
            AutoCompleteProperty = LookUpEditBase.AutoCompleteProperty.AddOwner(ownerType);
            IsCaseSensitiveSearchProperty = LookUpEditBase.IsCaseSensitiveSearchProperty.AddOwner(ownerType);
            IsCaseSensitiveFilterProperty = LookUpEditBase.IsCaseSensitiveFilterProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (o, args) => ((LookUpEditSettingsBase) o).IsCaseSensitiveFilterChanged((bool) args.NewValue)));
            ImmediatePopupProperty = LookUpEditBase.ImmediatePopupProperty.AddOwner(ownerType);
            DisplayMemberProperty = LookUpEditBase.DisplayMemberProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(LookUpEditSettingsBase.OnDisplayMemberChanged)));
            ValueMemberProperty = LookUpEditBase.ValueMemberProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(LookUpEditSettingsBase.OnValueMemberChanged)));
            IncrementalFilteringProperty = LookUpEditBase.IncrementalFilteringProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            SeparatorStringProperty = LookUpEditBase.SeparatorStringProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(";", new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            AddNewButtonPlacementProperty = LookUpEditBase.AddNewButtonPlacementProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            FindButtonPlacementProperty = LookUpEditBase.FindButtonPlacementProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            FilterConditionProperty = LookUpEditBase.FilterConditionProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((LookUpEditSettingsBase) d).FilterConditionChanged((DevExpress.Data.Filtering.FilterCondition?) e.NewValue)));
            FindModeProperty = LookUpEditBase.FindModeProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((LookUpEditSettingsBase) d).FindModeChanged((DevExpress.Xpf.Editors.FindMode?) e.NewValue)));
            AllowRejectUnknownValuesProperty = LookUpEditBase.AllowRejectUnknownValuesProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((LookUpEditSettingsBase) d).AllowRejectUnknownValuesChanged((bool) e.NewValue)));
            AllowLiveDataShapingProperty = LookUpEditBase.AllowLiveDataShapingProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((LookUpEditSettingsBase) d).AllowLiveDataShapingChanged((bool) e.NewValue)));
            AllowItemHighlightingProperty = DependencyPropertyManager.Register("AllowItemHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ItemTemplateSelectorProperty = LookUpEditBase.ItemTemplateSelectorProperty.AddOwner(ownerType);
            BaseEditSettings.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(EditSettingsHorizontalAlignment.Left, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            SelectItemWithNullValueProperty = LookUpEditBase.SelectItemWithNullValueProperty.AddOwner(ownerType, new FrameworkPropertyMetadata((d, e) => ((LookUpEditSettingsBase) d).SelectItemWithNullValueChanged((bool) e.NewValue)));
        }

        protected LookUpEditSettingsBase()
        {
        }

        protected virtual void AllowCollectionViewChanged(bool value)
        {
            this.ItemsProvider.Reset();
            this.OnSettingsChanged();
        }

        private void AllowLiveDataShapingChanged(bool newValue)
        {
            this.ItemsProvider.UpdateItemsSource();
            this.ItemsProvider.SyncWithCurrentItem();
            this.OnSettingsChanged();
        }

        protected virtual void AllowRejectUnknownValuesChanged(bool newValue)
        {
            this.ActualAllowRejectUnknownValues = newValue;
            this.ItemsProvider.UpdateValueMember();
            this.OnSettingsChanged();
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            if (edit2 != null)
            {
                edit2.ApplyItemTemplateToSelectedItem = this.ApplyItemTemplateToSelectedItem;
            }
            else
            {
                LookUpEditBase cb = edit as LookUpEditBase;
                if (cb != null)
                {
                    base.SetValueFromSettings(DisplayMemberProperty, () => cb.DisplayMember = this.DisplayMember);
                    base.SetValueFromSettings(ValueMemberProperty, () => cb.ValueMember = this.ValueMember);
                    base.SetValueFromSettings(AllowRejectUnknownValuesProperty, () => cb.AllowRejectUnknownValues = this.AllowRejectUnknownValues);
                    base.SetValueFromSettings(SelectItemWithNullValueProperty, () => cb.SelectItemWithNullValue = this.SelectItemWithNullValue);
                    base.SetValueFromSettings(AllowLiveDataShapingProperty, () => cb.AllowLiveDataShaping = this.AllowLiveDataShaping);
                    base.SetValueFromSettings(ItemsSourceProperty, () => cb.ItemsSource = this.ItemsSource);
                    base.SetValueFromSettings(ItemTemplateSelectorProperty, () => cb.ItemTemplateSelector = this.ItemTemplateSelector);
                    base.SetValueFromSettings(AllowItemHighlightingProperty, () => cb.AllowItemHighlighting = this.AllowItemHighlighting);
                    base.SetValueFromSettings(ItemsPanelProperty, () => cb.ItemsPanel = this.ItemsPanel);
                    base.SetValueFromSettings(PopupBaseEditSettings.PopupMaxHeightProperty, () => cb.PopupMaxHeight = this.PopupMaxHeight);
                    base.SetValueFromSettings(ApplyItemTemplateToSelectedItemProperty, () => cb.ApplyItemTemplateToSelectedItem = this.ApplyItemTemplateToSelectedItem);
                    base.SetValueFromSettings(AutoCompleteProperty, () => cb.AutoComplete = this.AutoComplete);
                    base.SetValueFromSettings(IsCaseSensitiveSearchProperty, () => cb.IsCaseSensitiveSearch = this.IsCaseSensitiveSearch);
                    base.SetValueFromSettings(IsCaseSensitiveFilterProperty, () => cb.IsCaseSensitiveFilter = this.IsCaseSensitiveFilter);
                    base.SetValueFromSettings(ImmediatePopupProperty, () => cb.ImmediatePopup = this.ImmediatePopup);
                    base.SetValueFromSettings(SeparatorStringProperty, () => cb.SeparatorString = this.SeparatorString);
                    base.SetValueFromSettings(IsSynchronizedWithCurrentItemProperty, () => cb.IsSynchronizedWithCurrentItem = this.IsSynchronizedWithCurrentItem);
                    base.SetValueFromSettings(AllowCollectionViewProperty, () => cb.AllowCollectionView = this.AllowCollectionView);
                    base.SetValueFromSettings(IncrementalFilteringProperty, () => cb.IncrementalFiltering = this.IncrementalFiltering);
                    base.SetValueFromSettings(AddNewButtonPlacementProperty, () => cb.AddNewButtonPlacement = this.AddNewButtonPlacement);
                    base.SetValueFromSettings(FindButtonPlacementProperty, () => cb.FindButtonPlacement = this.FindButtonPlacement);
                    base.SetValueFromSettings(FindModeProperty, () => cb.FindMode = this.FindMode);
                    base.SetValueFromSettings(FilterConditionProperty, () => cb.FilterCondition = this.FilterCondition);
                    base.SetValueFromSettings(ItemTemplateProperty, () => cb.ItemTemplate = this.ItemTemplate, () => this.ClearEditorPropertyIfNeeded(cb, LookUpEditBase.ItemTemplateProperty, ItemTemplateProperty));
                }
            }
        }

        private string CalcActualDisplayMember() => 
            this.isEnumItemsSourceAssigned ? EnumSourceHelperCore.DisplayMemberName : this.CachedDisplayMember;

        private string CalcActualValueMember() => 
            this.isEnumItemsSourceAssigned ? EnumSourceHelperCore.ValueMemberName : this.CachedValueMember;

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

        protected virtual void FilterConditionChanged(DevExpress.Data.Filtering.FilterCondition? filterCondition)
        {
            this.ItemsProvider.UpdateFilterCriteria();
            this.OnSettingsChanged();
        }

        protected virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.ItemsProvider.UpdateFilterCriteria();
            this.OnSettingsChanged();
        }

        protected virtual void FindModeChanged(DevExpress.Xpf.Editors.FindMode? findMode)
        {
            this.OnSettingsChanged();
        }

        public object GetItemFromValue(object value) => 
            this.ItemsProvider.GetItem(value, this.ItemsProvider.CurrentDataViewHandle);

        public object GetValueFromItem(object item) => 
            this.ItemsProvider.GetValueFromItem(item, this.ItemsProvider.CurrentDataViewHandle);

        protected virtual void IsCaseSensitiveFilterChanged(bool newValue)
        {
            this.ItemsProvider.UpdateIsCaseSensitiveFilter();
            this.OnSettingsChanged();
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
            this.CachedDisplayMember = this.DisplayMember;
            this.UpdateActualDisplayMember();
            this.ItemsProvider.UpdateDisplayMember();
            this.OnSettingsChanged();
        }

        private static void OnDisplayMemberChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditSettingsBase) obj).OnDisplayMemberChanged();
        }

        protected virtual void OnItemsSourceChanged(object itemsSource)
        {
            this.SetupItemsSource(itemsSource);
            this.ItemsProvider.UpdateItemsSource();
            this.ItemsProvider.SyncWithCurrentItem();
            this.OnSettingsChanged();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditSettingsBase) d).OnItemsSourceChanged(e.NewValue);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            ((IItemsProviderOwner) this).IsLoaded = true;
            this.ItemsProvider.DoRefresh();
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            ((IItemsProviderOwner) this).IsLoaded = false;
            this.ItemsProvider.DoRefresh();
        }

        protected virtual void OnValueMemberChanged()
        {
            this.CachedValueMember = this.ValueMember;
            this.UpdateActualValueMember();
            this.ItemsProvider.UpdateValueMember();
            this.OnSettingsChanged();
        }

        private static void OnValueMemberChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditSettingsBase) obj).OnValueMemberChanged();
        }

        protected virtual void SelectItemWithNullValueChanged(bool newValue)
        {
            this.ItemsProvider.Reset();
        }

        private void SetupItemsSource(object itemsSource)
        {
            this.isEnumItemsSourceAssigned = EnumItemsSource.IsEnumItemsSource(itemsSource);
            this.UpdateActualDisplayMember();
            this.UpdateActualValueMember();
        }

        private void UpdateActualDisplayMember()
        {
            this.ActualDisplayMember = this.CalcActualDisplayMember();
        }

        private void UpdateActualValueMember()
        {
            this.ActualValueMember = this.CalcActualValueMember();
        }

        protected internal override bool RequireDisplayTextSorting =>
            true;

        public bool AllowLiveDataShaping
        {
            get => 
                (bool) base.GetValue(AllowLiveDataShapingProperty);
            set => 
                base.SetValue(AllowLiveDataShapingProperty, value);
        }

        [Category("Behavior")]
        public EditorPlacement? FindButtonPlacement
        {
            get => 
                (EditorPlacement?) base.GetValue(FindButtonPlacementProperty);
            set => 
                base.SetValue(FindButtonPlacementProperty, value);
        }

        [Category("Behavior")]
        public EditorPlacement? AddNewButtonPlacement
        {
            get => 
                (EditorPlacement?) base.GetValue(AddNewButtonPlacementProperty);
            set => 
                base.SetValue(AddNewButtonPlacementProperty, value);
        }

        [Category("Appearance "), Description("")]
        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        [Category("Appearance "), Description("")]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Category("Appearance "), Description("")]
        public bool AllowItemHighlighting
        {
            get => 
                (bool) base.GetValue(AllowItemHighlightingProperty);
            set => 
                base.SetValue(AllowItemHighlightingProperty, value);
        }

        [Category("Appearance "), Description("")]
        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        [Category("Behavior"), Description("")]
        public bool ApplyItemTemplateToSelectedItem
        {
            get => 
                (bool) base.GetValue(ApplyItemTemplateToSelectedItemProperty);
            set => 
                base.SetValue(ApplyItemTemplateToSelectedItemProperty, value);
        }

        [Description(""), Category("Behavior")]
        public bool AutoComplete
        {
            get => 
                (bool) base.GetValue(AutoCompleteProperty);
            set => 
                base.SetValue(AutoCompleteProperty, value);
        }

        [Description(""), Category("Behavior")]
        public bool IsCaseSensitiveSearch
        {
            get => 
                (bool) base.GetValue(IsCaseSensitiveSearchProperty);
            set => 
                base.SetValue(IsCaseSensitiveSearchProperty, value);
        }

        public bool IsCaseSensitiveFilter
        {
            get => 
                (bool) base.GetValue(IsCaseSensitiveFilterProperty);
            set => 
                base.SetValue(IsCaseSensitiveFilterProperty, value);
        }

        [Description(""), Category("Behavior")]
        public bool ImmediatePopup
        {
            get => 
                (bool) base.GetValue(ImmediatePopupProperty);
            set => 
                base.SetValue(ImmediatePopupProperty, value);
        }

        [Category("Behavior"), Description("")]
        public bool? IncrementalFiltering
        {
            get => 
                (bool?) base.GetValue(IncrementalFilteringProperty);
            set => 
                base.SetValue(IncrementalFilteringProperty, value);
        }

        [Category("Behavior"), Description("")]
        public string SeparatorString
        {
            get => 
                (string) base.GetValue(SeparatorStringProperty);
            set => 
                base.SetValue(SeparatorStringProperty, value);
        }

        [Category("Behavior")]
        public bool IsSynchronizedWithCurrentItem
        {
            get => 
                (bool) base.GetValue(IsSynchronizedWithCurrentItemProperty);
            set => 
                base.SetValue(IsSynchronizedWithCurrentItemProperty, value);
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

        [Category("Data"), Description("")]
        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Category("Data"), Description("")]
        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        private string CachedValueMember { get; set; }

        private string CachedDisplayMember { get; set; }

        private bool CachedImageTemplateToSelectedItem { get; set; }

        private string ActualValueMember { get; set; }

        private string ActualDisplayMember { get; set; }

        private bool ActualCachedImageTemplateToSelectedItem { get; set; }

        private bool ActualAllowRejectUnknownValues { get; set; }

        [Description(""), Category("Data")]
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
        public DevExpress.Data.Filtering.FilterCondition? FilterCondition
        {
            get => 
                (DevExpress.Data.Filtering.FilterCondition?) base.GetValue(FilterConditionProperty);
            set => 
                base.SetValue(FilterConditionProperty, value);
        }

        [Category("Behavior")]
        public DevExpress.Xpf.Editors.FindMode? FindMode
        {
            get => 
                (DevExpress.Xpf.Editors.FindMode?) base.GetValue(FindModeProperty);
            set => 
                base.SetValue(FindModeProperty, value);
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
        public bool SelectItemWithNullValue
        {
            get => 
                (bool) base.GetValue(SelectItemWithNullValueProperty);
            set => 
                base.SetValue(SelectItemWithNullValueProperty, value);
        }

        string IItemsProviderOwner.DisplayMember
        {
            get => 
                this.ActualDisplayMember;
            set => 
                this.DisplayMember = value;
        }

        string IItemsProviderOwner.ValueMember
        {
            get => 
                this.ActualValueMember;
            set => 
                this.ValueMember = value;
        }

        IItemsProvider2 IItemsProviderOwner.ItemsProvider =>
            this.ItemsProvider;

        bool IItemsProviderOwner.IsLoaded { get; set; }

        ListItemCollection IItemsProviderOwner.Items =>
            new ListItemCollection(this);

        bool IItemsProviderOwner.IsInLookUpMode =>
            this.ActualAllowRejectUnknownValues || (!string.IsNullOrEmpty(this.ActualDisplayMember) || !string.IsNullOrEmpty(this.ActualValueMember));

        Dispatcher IItemsProviderOwner.Dispatcher =>
            base.Dispatcher;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookUpEditSettingsBase.<>c <>9 = new LookUpEditSettingsBase.<>c();

            internal void <.cctor>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).AllowCollectionViewChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue);
            }

            internal void <.cctor>b__27_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((LookUpEditSettingsBase) o).IsCaseSensitiveFilterChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__27_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).FilterConditionChanged((FilterCondition?) e.NewValue);
            }

            internal void <.cctor>b__27_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).FindModeChanged((FindMode?) e.NewValue);
            }

            internal void <.cctor>b__27_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).AllowRejectUnknownValuesChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).AllowLiveDataShapingChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__27_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditSettingsBase) d).SelectItemWithNullValueChanged((bool) e.NewValue);
            }
        }
    }
}

