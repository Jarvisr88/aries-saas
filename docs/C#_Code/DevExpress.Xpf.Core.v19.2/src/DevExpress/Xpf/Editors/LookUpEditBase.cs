namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    [DXToolboxBrowsable(false), LookupBindingProperties("ItemsSource", "DisplayMember", "ValueMember", "DisplayMember"), ComplexBindingProperties("ItemsSource", "ValueMember")]
    public abstract class LookUpEditBase : PopupBaseEdit, ISelectorEdit, IBaseEdit, IInputElement
    {
        public static TimeSpan TextSearchTimeOut = TextSearchEngineBase.DefaultTextSearchTimeout;
        public static readonly DependencyProperty AllowCollectionViewProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly DependencyProperty SelectedItemProperty;
        protected static readonly DependencyPropertyKey SelectedItemsPropertyKey;
        public static readonly DependencyProperty SelectedItemsProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty ValueMemberProperty;
        public static readonly RoutedEvent ProcessNewValueEvent;
        public static readonly RoutedEvent SelectedIndexChangedEvent;
        public static readonly RoutedEvent PopupContentSelectionChangedEvent;
        public static readonly DependencyProperty ApplyItemTemplateToSelectedItemProperty;
        public static readonly DependencyProperty SeparatorStringProperty;
        public static readonly DependencyProperty AutoCompleteProperty;
        public static readonly DependencyProperty ClearSelectionOnBackspaceProperty;
        public static readonly DependencyProperty IsCaseSensitiveSearchProperty;
        public static readonly DependencyProperty IsCaseSensitiveFilterProperty;
        public static readonly DependencyProperty AutoSearchTextProperty;
        private static readonly DependencyPropertyKey AutoSearchTextPropertyKey;
        public static readonly DependencyProperty ImmediatePopupProperty;
        protected static readonly DependencyPropertyKey SelectedItemValuePropertyKey;
        public static readonly DependencyProperty SelectedItemValueProperty;
        public static readonly DependencyProperty AllowItemHighlightingProperty;
        public static readonly DependencyProperty IncrementalFilteringProperty;
        public static readonly DependencyProperty AddNewButtonPlacementProperty;
        public static readonly DependencyProperty FindButtonPlacementProperty;
        public static readonly DependencyProperty FindModeProperty;
        public static readonly DependencyProperty FilterConditionProperty;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty AssignNullValueOnClearingEditTextProperty;
        private static readonly DependencyPropertyKey IsTokenModePropertyKey;
        public static readonly DependencyProperty IsTokenModeProperty;
        public static readonly DependencyProperty IsAsyncOperationInProgressProperty;
        private static readonly DependencyPropertyKey IsAsyncOperationInProgressPropertyKey;
        public static readonly DependencyProperty AllowRejectUnknownValuesProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty AllowLiveDataShapingProperty;
        public static readonly DependencyProperty ShowEditorWaitIndicatorProperty;
        public static readonly DependencyProperty ShowPopupWaitIndicatorProperty;
        public static readonly DependencyProperty IncrementalSearchProperty;
        public static readonly DependencyProperty SelectAllOnAcceptPopupProperty;
        public static readonly DependencyProperty SelectItemWithNullValueProperty;
        private readonly Locker selectedItemsInitialeLocker = new Locker();
        private ICommand addNewCommand;

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
        public event SelectionChangedEventHandler PopupContentSelectionChanged
        {
            add
            {
                base.AddHandler(PopupContentSelectionChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PopupContentSelectionChangedEvent, value);
            }
        }

        [Category("Action")]
        public event ProcessNewValueEventHandler ProcessNewValue
        {
            add
            {
                base.AddHandler(ProcessNewValueEvent, value);
            }
            remove
            {
                base.RemoveHandler(ProcessNewValueEvent, value);
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

        public event EventHandler<SubstituteDisplayFilterEventArgs> SubstituteDisplayFilter;

        static LookUpEditBase()
        {
            Type ownerType = typeof(LookUpEditBase);
            AllowLiveDataShapingProperty = DependencyProperty.Register("AllowLiveDataShaping", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditBase) d).AllowLiveDataShapingChanged((bool) e.NewValue)));
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditBase) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue)));
            AllowCollectionViewProperty = DependencyPropertyManager.Register("AllowCollectionView", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditBase) d).AllowCollectionViewChanged((bool) e.NewValue)));
            IsSynchronizedWithCurrentItemProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((LookUpEditBase) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue)));
            ItemTemplateProperty = ItemsControl.ItemTemplateProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(LookUpEditBase.OnItemTemplateChanged)));
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(object), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(LookUpEditBase.ItemsSourceChanged)));
            ItemContainerStyleProperty = ItemsControl.ItemContainerStyleProperty.AddOwner(ownerType);
            DisplayMemberProperty = DependencyPropertyManager.Register("DisplayMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((LookUpEditBase) d).OnDisplayMemberChanged((string) e.NewValue)));
            ValueMemberProperty = DependencyPropertyManager.Register("ValueMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((LookUpEditBase) d).OnValueMemberChanged((string) e.NewValue)));
            SelectedItemProperty = DependencyPropertyManager.Register("SelectedItem", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(LookUpEditBase.OnSelectedItemChanged), new CoerceValueCallback(LookUpEditBase.OnSelectedItemCoerce)));
            SelectedIndexProperty = DependencyPropertyManager.Register("SelectedIndex", typeof(int), ownerType, new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(LookUpEditBase.OnSelectedIndexChanged), new CoerceValueCallback(LookUpEditBase.OnSelectedIndexCoerce)));
            SelectedItemsPropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectedItems", typeof(ObservableCollection<object>), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(LookUpEditBase.OnSelectedItemsChanged)));
            SelectedItemsProperty = SelectedItemsPropertyKey.DependencyProperty;
            SelectedIndexChangedEvent = EventManager.RegisterRoutedEvent("SelectedIndexChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            PopupContentSelectionChangedEvent = EventManager.RegisterRoutedEvent("PopupContentSelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), ownerType);
            ProcessNewValueEvent = EventManager.RegisterRoutedEvent("ProcessNewValue", RoutingStrategy.Direct, typeof(ProcessNewValueEventHandler), ownerType);
            ApplyItemTemplateToSelectedItemProperty = DependencyPropertyManager.Register("ApplyItemTemplateToSelectedItem", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(LookUpEditBase.ApplyItemTemplateToSelectedItemChanged)));
            SeparatorStringProperty = DependencyPropertyManager.Register("SeparatorString", typeof(string), ownerType, new FrameworkPropertyMetadata(";", new PropertyChangedCallback(LookUpEditBase.OnSeparatorStringChanged)));
            ClearSelectionOnBackspaceProperty = DependencyPropertyManager.Register("ClearSelectionOnBackspace", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditBase) d).ClearSelectionOnBackspaceChanged((bool) e.NewValue)));
            AutoCompleteProperty = DependencyPropertyManager.Register("AutoComplete", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((LookUpEditBase) d).AutoCompleteChanged((bool) e.NewValue)));
            IsCaseSensitiveSearchProperty = DependencyPropertyManager.Register("IsCaseSensitiveSearch", typeof(bool), ownerType, new PropertyMetadata(false));
            IsCaseSensitiveFilterProperty = DependencyPropertyManager.Register("IsCaseSensitiveFilter", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (o, args) => ((LookUpEditBase) o).IsCaseSensitiveFilterChanged((bool) args.NewValue)));
            AutoSearchTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("AutoSearchText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(LookUpEditBase.OnAutoSearchTextPropertyChanged)));
            AutoSearchTextProperty = AutoSearchTextPropertyKey.DependencyProperty;
            ImmediatePopupProperty = DependencyPropertyManager.Register("ImmediatePopup", typeof(bool), ownerType, new PropertyMetadata(false));
            SelectedItemValuePropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectedItemValue", typeof(object), ownerType, new PropertyMetadata(null));
            SelectedItemValueProperty = SelectedItemValuePropertyKey.DependencyProperty;
            AllowItemHighlightingProperty = DependencyPropertyManager.Register("AllowItemHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            IncrementalFilteringProperty = DependencyPropertyManager.Register("IncrementalFiltering", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(LookUpEditBase.OnIncrementalFilteringChanged)));
            AddNewButtonPlacementProperty = DependencyPropertyManager.Register("AddNewButtonPlacement", typeof(EditorPlacement?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((LookUpEditBase) d).AddNewButtonPlacementChanged((EditorPlacement?) e.NewValue)));
            FindButtonPlacementProperty = DependencyPropertyManager.Register("FindButtonPlacement", typeof(EditorPlacement?), ownerType, new FrameworkPropertyMetadata(null));
            FindModeProperty = DependencyPropertyManager.Register("FindMode", typeof(DevExpress.Xpf.Editors.FindMode?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((LookUpEditBase) d).FindModeChanged((DevExpress.Xpf.Editors.FindMode?) e.NewValue)));
            FilterConditionProperty = DependencyPropertyManager.Register("FilterCondition", typeof(DevExpress.Data.Filtering.FilterCondition?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((LookUpEditBase) d).FilterConditionChanged((DevExpress.Data.Filtering.FilterCondition?) e.NewValue), (CoerceValueCallback) ((d, v) => ((LookUpEditBase) d).CoerceFilterCondition((DevExpress.Data.Filtering.FilterCondition?) v))));
            AssignNullValueOnClearingEditTextProperty = DependencyPropertyManager.Register("AssignNullValueOnClearingEditText", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsTokenModePropertyKey = DependencyProperty.RegisterReadOnly("IsTokenMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((LookUpEditBase) d).OnIsTokenModeChanged()));
            IsTokenModeProperty = IsTokenModePropertyKey.DependencyProperty;
            ItemsPanelProperty = ItemsControl.ItemsPanelProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(ItemsControl.ItemsPanelProperty.DefaultMetadata.DefaultValue, new PropertyChangedCallback(LookUpEditBase.OnItemsPanelChanged)));
            ItemTemplateSelectorProperty = ItemsControl.ItemTemplateSelectorProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(LookUpEditBase.OnItemTemplateSelectorChanged)));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(LookUpEditBase), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            IsAsyncOperationInProgressPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<LookUpEditBase, bool>(System.Linq.Expressions.Expression.Lambda<Func<LookUpEditBase, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LookUpEditBase.get_IsAsyncOperationInProgress)), parameters), false, (owner, oldValue, newValue) => owner.IsAsyncOperationInProgressChanged(oldValue, newValue));
            IsAsyncOperationInProgressProperty = IsAsyncOperationInProgressPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(LookUpEditBase), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            AllowRejectUnknownValuesProperty = DependencyPropertyRegistrator.Register<LookUpEditBase, bool>(System.Linq.Expressions.Expression.Lambda<Func<LookUpEditBase, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(LookUpEditBase.get_AllowRejectUnknownValues)), expressionArray2), false, (@base, value, newValue) => @base.AllowRejectUnknownValuesChanged(newValue));
            ShowEditorWaitIndicatorProperty = DependencyPropertyManager.Register("ShowEditorWaitIndicator", typeof(bool), ownerType, new PropertyMetadata(true));
            ShowPopupWaitIndicatorProperty = DependencyPropertyManager.Register("ShowPopupWaitIndicator", typeof(bool), ownerType, new PropertyMetadata(true));
            IncrementalSearchProperty = DependencyPropertyManager.Register("IncrementalSearch", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            SelectAllOnAcceptPopupProperty = DependencyPropertyManager.Register("SelectAllOnAcceptPopup", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            SelectItemWithNullValueProperty = DependencyProperty.Register("SelectItemWithNullValue", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((LookUpEditBase) d).SelectItemWithNullValueChanged((bool) e.NewValue)));
        }

        protected LookUpEditBase()
        {
            this.SetDefaultStyleKey(typeof(LookUpEditBase));
            using (this.selectedItemsInitialeLocker.Lock())
            {
                ObservableCollection<object> observables = new ObservableCollection<object>();
                observables.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSelectedItemsCollectionChanged);
                this.SelectedItems = observables;
            }
            this.SelectedValueRenderer = new SelectedItemValueRenderer(this);
            this.FindCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.FindInternal), false);
            this.AddNewCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.AddNewInternal), false);
            this.SelectAllItemsCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.ChangeSelection), new Func<object, bool>(this.CanChangeSelection), false);
            Action<LookUpEditBase, object, ItemsProviderChangedEventArgs> onEventAction = <>c.<>9__67_0;
            if (<>c.<>9__67_0 == null)
            {
                Action<LookUpEditBase, object, ItemsProviderChangedEventArgs> local1 = <>c.<>9__67_0;
                onEventAction = <>c.<>9__67_0 = (owner, o, e) => owner.EditStrategy.ItemProviderChanged(e);
            }
            this.ItemsProviderChangedEventHandler = new ItemsProviderChangedEventHandler<LookUpEditBase>(this, onEventAction);
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptPopupValue(false);
        }

        protected virtual void AddNewButtonPlacementChanged(EditorPlacement? placement)
        {
            base.UpdatePopupElements();
        }

        protected virtual void AddNewCommandChanged()
        {
            this.EditStrategy.AddNewCommandChanged();
        }

        protected virtual void AddNewInternal(object parameter)
        {
            this.EditStrategy.AddNew(parameter);
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

        private static void ApplyItemTemplateToSelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnApplyItemTemplateToSelectedItemChanged();
        }

        protected virtual void AutoCompleteChanged(bool value)
        {
            this.EditStrategy.AutoCompleteChanged(value);
        }

        protected internal override void BeforePopupOpened()
        {
            base.BeforePopupOpened();
            this.EditStrategy.BeforePopupOpened();
        }

        public void BeginDataUpdate()
        {
            this.ItemsProvider.BeginInit();
        }

        protected virtual bool CanChangeSelection(object parameter) => 
            !this.PropertyProvider.IsSingleSelection;

        protected virtual bool CanShowPopupCore() => 
            (this.ItemsProvider != null) && (this.ItemsProvider.IsAsyncServerMode || (this.ItemsProvider.GetCount(this.EditStrategy.CurrentDataViewHandle) > 0));

        protected virtual void ChangeSelection(ChangeSelectionAction changeSelection)
        {
            if (changeSelection == ChangeSelectionAction.SelectAll)
            {
                this.SelectAllItems();
            }
            else if (changeSelection == ChangeSelectionAction.UnselectAll)
            {
                this.UnselectAllItems();
            }
        }

        private void ChangeSelection(object parameter)
        {
            if (parameter != null)
            {
                this.ChangeSelection((ChangeSelectionAction) parameter);
            }
        }

        protected virtual void ClearSelectionOnBackspaceChanged(bool value)
        {
            this.EditStrategy.ClearSelectionOnBackspace(value);
        }

        protected virtual DevExpress.Data.Filtering.FilterCondition? CoerceFilterCondition(DevExpress.Data.Filtering.FilterCondition? filterCondition) => 
            ((filterCondition == null) || ((((DevExpress.Data.Filtering.FilterCondition) filterCondition.Value) == DevExpress.Data.Filtering.FilterCondition.StartsWith) || ((((DevExpress.Data.Filtering.FilterCondition) filterCondition.Value) == DevExpress.Data.Filtering.FilterCondition.Contains) || (((DevExpress.Data.Filtering.FilterCondition) filterCondition.Value) == DevExpress.Data.Filtering.FilterCondition.Default)))) ? filterCondition : ((DevExpress.Data.Filtering.FilterCondition?) 3);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new LookUpEditBasePropertyProvider(this);

        protected virtual ButtonInfoBase CreateAddNewButtonInfo()
        {
            ButtonInfo info1 = new ButtonInfo();
            info1.GlyphKind = GlyphKind.Plus;
            info1.Content = EditorLocalizer.Active.GetLocalizedString(EditorStringId.LookUpAddNew);
            info1.Command = this.AddNewCommand;
            return info1;
        }

        protected override EditBoxWrapper CreateEditBoxWrapper() => 
            (!this.IsTokenMode || !this.IsTokenEditorInEditCore()) ? base.CreateEditBoxWrapper() : this.CreateTokenEditorWrapper();

        protected virtual ButtonInfoBase CreateFindButtonInfo()
        {
            ButtonInfo info1 = new ButtonInfo();
            info1.GlyphKind = GlyphKind.Search;
            info1.Content = EditorLocalizer.Active.GetLocalizedString(EditorStringId.LookUpFind);
            info1.Command = this.FindCommand;
            return info1;
        }

        protected virtual ButtonInfoBase CreateLoadingButtonInfo()
        {
            LoadingIndicatorButtonInfo info = new LoadingIndicatorButtonInfo();
            Binding binding = new Binding("IsAsyncOperationInProgress");
            binding.Source = this;
            binding.Converter = new DevExpress.Xpf.Editors.Helpers.BooleanToVisibilityConverter();
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            info.SetBinding(ButtonInfoBase.VisibilityProperty, binding);
            return info;
        }

        private EditBoxWrapper CreateTokenEditorWrapper() => 
            new TokenEditorWrapper(this);

        protected internal override void DestroyPopupContent(EditorPopupBase popup)
        {
            base.DestroyPopupContent(popup);
            this.EditStrategy.PopupDestroyed();
        }

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

        protected virtual void FilterConditionChanged(DevExpress.Data.Filtering.FilterCondition? filterCondition)
        {
            this.EditStrategy.FilterConditionChanged(filterCondition);
        }

        protected virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.EditStrategy.FilterCriteriaChanged(criteriaOperator);
        }

        protected virtual void FindInternal(object parameter)
        {
            this.EditStrategy.Find(parameter);
        }

        protected virtual void FindModeChanged(DevExpress.Xpf.Editors.FindMode? findMode)
        {
            this.EditStrategy.FindModeChanged(findMode);
        }

        internal void FocusCore()
        {
            this.FocusEditCore();
        }

        protected override string GetDisplayTextCore(object editValue, bool applyFormatting)
        {
            if (this.PropertyProvider.IsSingleSelection && !this.IsTokenMode)
            {
                return base.GetDisplayTextCore(editValue, applyFormatting);
            }
            bool flag = true;
            IList list = editValue as IList;
            if ((list != null) && (list.Count == 0))
            {
                return base.GetDisplayTextCore(null, applyFormatting);
            }
            if (list == null)
            {
                return base.GetDisplayTextCore(editValue, applyFormatting);
            }
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in list)
            {
                if (!flag)
                {
                    builder.Append(this.SeparatorString);
                }
                else
                {
                    flag = false;
                }
                builder.Append(base.GetDisplayTextCore(obj2, applyFormatting));
            }
            return builder.ToString();
        }

        public object GetDisplayValue(int index) => 
            this.ItemsProvider.GetDisplayValueByIndex(index, this.ItemsProvider.CurrentDataViewHandle);

        public int GetIndexByKeyValue(object keyValue) => 
            this.ItemsProvider.IndexOfValue(keyValue, this.ItemsProvider.CurrentDataViewHandle);

        internal bool GetIsEditorKeyboardFocused() => 
            this.IsEditorKeyboardFocused;

        internal bool GetIsPopupCloseInProgress() => 
            base.IsPopupCloseInProgress;

        public object GetItemByKeyValue(object keyValue) => 
            this.ItemsProvider.GetItem(keyValue, this.ItemsProvider.CurrentDataViewHandle);

        public object GetKeyValue(int index) => 
            this.ItemsProvider.GetValueByIndex(index, this.ItemsProvider.CurrentDataViewHandle);

        protected override object GetTextValueInternal()
        {
            ITextExportSettings settings = this;
            return (this.EditStrategy.IsInLookUpMode ? settings.Text : base.GetTextValueInternal());
        }

        protected override bool? GetXlsExportNativeFormatInternal() => 
            this.EditStrategy.IsInLookUpMode ? false : base.GetXlsExportNativeFormatInternal();

        protected override void InsertCommandButtonInfo(IList<ButtonInfoBase> collection)
        {
            base.InsertCommandButtonInfo(collection);
            EditorPlacement? addNewButtonPlacement = this.AddNewButtonPlacement;
            EditorPlacement editBox = EditorPlacement.EditBox;
            if ((((EditorPlacement) addNewButtonPlacement.GetValueOrDefault()) == editBox) ? (addNewButtonPlacement != null) : false)
            {
                collection.Insert(0, this.CreateAddNewButtonInfo());
            }
            if (this.ItemsProvider.IsAsyncServerMode && this.ShowEditorWaitIndicator)
            {
                collection.Insert(0, this.CreateLoadingButtonInfo());
            }
        }

        protected virtual void IsAsyncOperationInProgressChanged(bool oldValue, bool newValue)
        {
            this.PropertyProvider.ShowWaitIndicator = this.ShowPopupWaitIndicator & newValue;
        }

        protected virtual void IsCaseSensitiveFilterChanged(bool newValue)
        {
            this.EditStrategy.IsCaseSensitiveFilterChanged(newValue);
        }

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithAcceptGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithAcceptGesture(key, modifiers);

        protected override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithCancelGesture(key, modifiers) && this.EditStrategy.IsClosePopupWithCancelGesture(key, modifiers);

        protected virtual void IsSynchronizedWithCurrentItemChanged(bool value)
        {
            this.EditStrategy.IsSynchronizedWithCurrentItemChanged(value);
        }

        private bool IsTokenEditorInEditCore() => 
            (base.EditCore != null) && (base.EditCore is TokenEditor);

        protected virtual void ItemsSourceChanged(object itemsSource)
        {
            this.EditStrategy.ItemSourceChanged(itemsSource);
        }

        private static void ItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).ItemsSourceChanged(e.NewValue);
        }

        protected override bool NeedsEnter(ModifierKeys modifiers) => 
            this.EditStrategy.NeedsEnterKey(modifiers);

        protected virtual void OnApplyItemTemplateToSelectedItemChanged()
        {
            this.PropertyProvider.SetApplyItemTemplateToSelectedItem(this.ApplyItemTemplateToSelectedItem);
            base.UpdateActualEditorControlTemplate();
        }

        protected virtual void OnAutoSearchTextChanged(string text)
        {
            this.EditStrategy.AutoSeachTextChanged(text);
        }

        private static void OnAutoSearchTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) d).OnAutoSearchTextChanged((string) e.NewValue);
        }

        protected virtual void OnDisplayMemberChanged(string displayMember)
        {
            this.EditStrategy.DisplayMemberChanged(displayMember);
        }

        protected override void OnEditCoreAssignedInTokenMode()
        {
            if (this.IsTokenMode)
            {
                this.UpdateEditBoxWrapper();
                (base.EditCore as TokenEditor).Do<TokenEditor>(x => x.EditBehavior = this.PropertyProvider.TokenEditorBehavior);
                this.EditStrategy.UpdateDisplayFilter();
            }
        }

        protected virtual void OnIncrementalFilteringChanged()
        {
            this.EditStrategy.OnIncrementalFilteringChanged();
        }

        protected static void OnIncrementalFilteringChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnIncrementalFilteringChanged();
        }

        protected virtual void OnIsTokenModeChanged()
        {
            this.EditStrategy.OnTokenModeChanged();
            this.UpdateEditBoxWrapper();
        }

        protected virtual void OnItemsPanelChanged()
        {
            this.Settings.ItemsPanel = this.ItemsPanel;
        }

        private static void OnItemsPanelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnItemsPanelChanged();
        }

        protected virtual void OnItemTemplateChanged()
        {
            this.PropertyProvider.HasItemTemplate = this.ItemTemplate != null;
            this.Settings.ItemTemplate = this.ItemTemplate;
        }

        private static void OnItemTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnItemTemplateChanged();
        }

        protected virtual void OnItemTemplateSelectorChanged()
        {
            this.Settings.ItemTemplateSelector = this.ItemTemplateSelector;
        }

        private static void OnItemTemplateSelectorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnItemTemplateSelectorChanged();
        }

        protected override void OnPopupClosed()
        {
            base.OnPopupClosed();
            this.EditStrategy.PopupClosed();
        }

        protected virtual void OnSelectedIndexChanged(int oldSelectedIndex, int selectedIndex)
        {
            this.EditStrategy.SelectedIndexChanged(oldSelectedIndex, selectedIndex);
        }

        private static void OnSelectedIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnSelectedIndexChanged((int) e.OldValue, (int) e.NewValue);
        }

        protected virtual object OnSelectedIndexCoerce(object baseValue) => 
            this.EditStrategy.CoerceSelectedIndex((int) baseValue);

        protected static object OnSelectedIndexCoerce(DependencyObject obj, object baseValue) => 
            ((LookUpEditBase) obj).OnSelectedIndexCoerce(baseValue);

        protected virtual void OnSelectedItemChanged(object oldValue, object newValue)
        {
            this.EditStrategy.SelectedItemChanged(oldValue, newValue);
        }

        private static void OnSelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnSelectedItemChanged(e.OldValue, e.NewValue);
        }

        protected virtual object OnSelectedItemCoerce(object baseValue) => 
            this.EditStrategy.CoerceSelectedItem(baseValue);

        protected static object OnSelectedItemCoerce(DependencyObject obj, object baseValue) => 
            ((LookUpEditBase) obj).OnSelectedItemCoerce(baseValue);

        protected virtual void OnSelectedItemsChanged(IList oldSelectedItems, IList selectedItems)
        {
            if (!this.selectedItemsInitialeLocker.IsLocked)
            {
                this.EditStrategy.SelectedItemsChanged(oldSelectedItems, selectedItems);
            }
        }

        private static void OnSelectedItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnSelectedItemsChanged((IList) e.OldValue, (IList) e.NewValue);
        }

        private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.EditStrategy.SelectedItemsCollectionChanged(e);
        }

        protected virtual void OnSeparatorStringChanged()
        {
            this.EditStrategy.UpdateDisplayText();
        }

        private static void OnSeparatorStringChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((LookUpEditBase) obj).OnSeparatorStringChanged();
        }

        private void OnTokenEditorTextChanged(object sender, EventArgs e)
        {
            this.EditStrategy.SyncWithEditor();
        }

        private void OnTokenEditorTokenClosed(object sender, EventArgs e)
        {
            this.EditStrategy.OnTokenEditorTokenClosed();
        }

        private void OnTokenEditorValueChanged(object sender, EventArgs e)
        {
            ChangeTextItem item = new ChangeTextItem();
            item.Text = string.Empty;
            this.EditStrategy.UpdateAutoSearchText(item, false);
            this.EditStrategy.SetEditValueOnTokenEditorValueChanged();
        }

        protected virtual void OnValueMemberChanged(string valueMember)
        {
            this.EditStrategy.ValueMemberChanged(valueMember);
        }

        protected internal virtual void RaisePopupContentSelectionChanged(IList removedItems, IList addedItems)
        {
            IList list1 = removedItems;
            if (removedItems == null)
            {
                IList local1 = removedItems;
                list1 = new List<object>();
            }
            this.EditStrategy.RaisePopupContentSelectionChanged(list1, addedItems ?? new List<object>());
        }

        protected internal void RaiseSubstituteDisplayFilter(SubstituteDisplayFilterEventArgs args)
        {
            EventHandler<SubstituteDisplayFilterEventArgs> substituteDisplayFilter = this.SubstituteDisplayFilter;
            if (substituteDisplayFilter != null)
            {
                substituteDisplayFilter(this, args);
            }
        }

        public void RefreshData()
        {
            this.ItemsProvider.DoRefresh();
            this.EditStrategy.UpdateDisplayText();
        }

        private void ResetSelectedIndex()
        {
            base.CoerceValue(SelectedIndexProperty);
        }

        public virtual void SelectAllItems()
        {
            this.EditStrategy.SelectAllItems();
        }

        protected virtual void SelectionModeChanged(SelectionMode? value)
        {
            this.EditStrategy.SelectionModeChanged(value);
        }

        protected virtual void SelectItemWithNullValueChanged(bool baseValue)
        {
            this.EditStrategy.SelectItemWithNullValueChanged(baseValue);
        }

        public static void SetupComboBoxEnumItemSource<T, DataType>(LookUpEditBase comboBox)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Not enum type", "<T>");
            }
            comboBox.ValueMember = "Value";
            comboBox.DisplayMember = "Text";
            List<LookUpEditEnumItem<DataType>> list = new List<LookUpEditEnumItem<DataType>>();
            foreach (string str in Enum.GetNames(typeof(T)))
            {
                T local = (T) Enum.Parse(typeof(T), str);
                LookUpEditEnumItem<DataType> item = new LookUpEditEnumItem<DataType> {
                    Text = str,
                    Value = (DataType) Convert.ChangeType(local, typeof(DataType), CultureInfo.CurrentCulture)
                };
                list.Add(item);
            }
            comboBox.ItemsSource = list;
        }

        public static void SetupComboBoxSettingsEnumItemSource<T>(LookUpEditSettingsBase settings)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Not enum type", "<T>");
            }
            settings.ValueMember = "Value";
            settings.DisplayMember = "Text";
            List<LookUpEditEnumItem<int>> list = new List<LookUpEditEnumItem<int>>();
            foreach (string str in Enum.GetNames(typeof(T)))
            {
                T local = (T) Enum.Parse(typeof(T), str);
                LookUpEditEnumItem<int> item = new LookUpEditEnumItem<int>();
                item.Text = str;
                item.Value = Convert.ToInt32(local);
                list.Add(item);
            }
            settings.ItemsSource = list;
        }

        protected virtual void SubcribeTokenEditorEvents()
        {
            TokenEditor editCore = base.EditCore as TokenEditor;
            if (editCore != null)
            {
                editCore.TextChanged += new EventHandler<EventArgs>(this.OnTokenEditorTextChanged);
                editCore.TokenClosed += new EventHandler<EventArgs>(this.OnTokenEditorTokenClosed);
                editCore.ValueChanged += new EventHandler<EventArgs>(this.OnTokenEditorValueChanged);
            }
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            if (this.IsTokenMode)
            {
                this.SubcribeTokenEditorEvents();
            }
        }

        protected internal virtual void SubscribeToItemsProviderChanged()
        {
            this.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
        }

        protected override void SubscribeToSettings(BaseEditSettings settings)
        {
            base.SubscribeToSettings(settings);
            if (settings != null)
            {
                ((LookUpEditSettingsBase) settings).ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
            }
            if (this.PropertyProvider.EditMode == EditMode.InplaceActive)
            {
                this.EditStrategy.UpdateIncrementalFilteringSnapshot(true);
                this.EditStrategy.UpdateTokenEditorSnapshot(true);
            }
        }

        public virtual void UnselectAllItems()
        {
            this.EditStrategy.UnselectAllItems();
        }

        protected virtual void UnsubcribeTokenEditorEvents()
        {
            TokenEditor editCore = base.EditCore as TokenEditor;
            if (editCore != null)
            {
                editCore.TextChanged -= new EventHandler<EventArgs>(this.OnTokenEditorTextChanged);
                editCore.TokenClosed -= new EventHandler<EventArgs>(this.OnTokenEditorTokenClosed);
                editCore.ValueChanged -= new EventHandler<EventArgs>(this.OnTokenEditorValueChanged);
            }
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            if (this.IsTokenMode)
            {
                this.UnsubcribeTokenEditorEvents();
            }
        }

        protected override void UnsubscribeFromSettings(BaseEditSettings settings)
        {
            base.UnsubscribeFromSettings(settings);
            if (settings != null)
            {
                ((LookUpEditSettingsBase) settings).ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
            }
            if (this.PropertyProvider.EditMode == EditMode.InplaceActive)
            {
                this.EditStrategy.UpdateIncrementalFilteringSnapshot(false);
            }
        }

        protected internal virtual void UnsubscribeToItemsProviderChanged()
        {
            this.ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
        }

        private ItemsProviderChangedEventHandler<LookUpEditBase> ItemsProviderChangedEventHandler { get; set; }

        protected internal LookUpEditSettingsBase Settings =>
            base.Settings as LookUpEditSettingsBase;

        protected internal SelectorVisualClientOwner VisualClient =>
            base.VisualClient as SelectorVisualClientOwner;

        protected internal IItemsProvider2 ItemsProvider =>
            this.Settings.ItemsProvider;

        internal SelectedItemValueRenderer SelectedValueRenderer { get; private set; }

        public bool SelectItemWithNullValue
        {
            get => 
                (bool) base.GetValue(SelectItemWithNullValueProperty);
            set => 
                base.SetValue(SelectItemWithNullValueProperty, value);
        }

        public bool? SelectAllOnAcceptPopup
        {
            get => 
                (bool?) base.GetValue(SelectAllOnAcceptPopupProperty);
            set => 
                base.SetValue(SelectAllOnAcceptPopupProperty, value);
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

        public bool IsAsyncOperationInProgress
        {
            get => 
                (bool) base.GetValue(IsAsyncOperationInProgressProperty);
            internal set => 
                base.SetValue(IsAsyncOperationInProgressPropertyKey, value);
        }

        public bool ShowEditorWaitIndicator
        {
            get => 
                (bool) base.GetValue(ShowEditorWaitIndicatorProperty);
            set => 
                base.SetValue(ShowEditorWaitIndicatorProperty, value);
        }

        public bool ShowPopupWaitIndicator
        {
            get => 
                (bool) base.GetValue(ShowPopupWaitIndicatorProperty);
            set => 
                base.SetValue(ShowPopupWaitIndicatorProperty, value);
        }

        public bool IncrementalSearch
        {
            get => 
                (bool) base.GetValue(IncrementalSearchProperty);
            set => 
                base.SetValue(IncrementalSearchProperty, value);
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
        public DevExpress.Xpf.Editors.FindMode? FindMode
        {
            get => 
                (DevExpress.Xpf.Editors.FindMode?) base.GetValue(FindModeProperty);
            set => 
                base.SetValue(FindModeProperty, value);
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
        public EditorPlacement? AddNewButtonPlacement
        {
            get => 
                (EditorPlacement?) base.GetValue(AddNewButtonPlacementProperty);
            set => 
                base.SetValue(AddNewButtonPlacementProperty, value);
        }

        [Description("Gets or sets whether to apply the ItemTemplate to the selected item, displayed within the edit box. This is a dependency property."), Category("Behavior")]
        public bool ApplyItemTemplateToSelectedItem
        {
            get => 
                (bool) base.GetValue(ApplyItemTemplateToSelectedItemProperty);
            set => 
                base.SetValue(ApplyItemTemplateToSelectedItemProperty, value);
        }

        [Description("Gets or sets a template that defines the presentation of items contained within the dropdown list. This is a dependency property."), Browsable(false)]
        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a list item template based on custom logic. This is a dependency property."), Browsable(false)]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Description("Gets or sets whether item highlighting is enabled. This is a dependency property.")]
        public bool AllowItemHighlighting
        {
            get => 
                (bool) base.GetValue(AllowItemHighlightingProperty);
            set => 
                base.SetValue(AllowItemHighlightingProperty, value);
        }

        [Description("Gets or sets the template that defines the panel that controls the layout of items displayed within the editor's dropdown. This is a dependency property."), Browsable(false)]
        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        [Category("Common Properties"), Description("Gets or sets the currently selected item. This is a dependency property.")]
        public object SelectedItem
        {
            get => 
                base.GetValue(SelectedItemProperty);
            set => 
                base.SetValue(SelectedItemProperty, value);
        }

        [Category("Common Properties"), Description("Gets the collection of selected items. This is a dependency property.")]
        public ObservableCollection<object> SelectedItems
        {
            get => 
                (ObservableCollection<object>) base.GetValue(SelectedItemsProperty);
            private set => 
                base.SetValue(SelectedItemsPropertyKey, value);
        }

        [Category("Common Properties"), Description("Gets or sets the index of the selected item. This is a dependency property.")]
        public int SelectedIndex
        {
            get => 
                (int) base.GetValue(SelectedIndexProperty);
            set => 
                base.SetValue(SelectedIndexProperty, value);
        }

        [Bindable(true), Category("Common Properties"), Description("Gets or sets the lookup editor's (ComboBoxEdit, LookUpEdit) data source. This is a dependency property.")]
        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Category("Common Properties"), DefaultValue(""), Description("Gets or sets a field name in the bound data source whose values are displayed by the editor. This is a dependency property.")]
        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        [DefaultValue(""), Category("Common Properties"), Description("Gets or sets a field name in the bound data source,  whose values are assigned to item values. This is a dependency property.")]
        public string ValueMember
        {
            get => 
                (string) base.GetValue(ValueMemberProperty);
            set => 
                base.SetValue(ValueMemberProperty, value);
        }

        [Description("Gets or sets the style applied to the container element generated for each item within the editor's dropdown. This is a dependency property."), Browsable(false)]
        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        [Description("Gets or sets the string separating checked items in the edit value. This is a dependency property."), Category("Appearance")]
        public string SeparatorString
        {
            get => 
                (string) base.GetValue(SeparatorStringProperty);
            set => 
                base.SetValue(SeparatorStringProperty, value);
        }

        [Description("Gets or sets whether pressing Backspace clears the selection within the edit box in auto-complete mode. This is a dependency property."), Category("Behavior")]
        public bool ClearSelectionOnBackspace
        {
            get => 
                (bool) base.GetValue(ClearSelectionOnBackspaceProperty);
            set => 
                base.SetValue(ClearSelectionOnBackspaceProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the automatic completion is enabled. This is a dependency property.")]
        public bool AutoComplete
        {
            get => 
                (bool) base.GetValue(AutoCompleteProperty);
            set => 
                base.SetValue(AutoCompleteProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the automatic completion is case sensitive. This is a dependency property.")]
        public bool IsCaseSensitiveSearch
        {
            get => 
                (bool) base.GetValue(IsCaseSensitiveSearchProperty);
            set => 
                base.SetValue(IsCaseSensitiveSearchProperty, value);
        }

        [Description("Gets or sets whether the automatic completion is case sensitive. This is a dependency property."), Category("Behavior")]
        public bool IsCaseSensitiveFilter
        {
            get => 
                (bool) base.GetValue(IsCaseSensitiveFilterProperty);
            set => 
                base.SetValue(IsCaseSensitiveFilterProperty, value);
        }

        [Description("Gets or sets whether the dropdown list is displayed immediately after an end user has typed a character in the edit box. This is a dependency property."), Category("Behavior")]
        public bool ImmediatePopup
        {
            get => 
                (bool) base.GetValue(ImmediatePopupProperty);
            set => 
                base.SetValue(ImmediatePopupProperty, value);
        }

        [Description("Specifies whether to enable the incremental filtering feature. This is a dependency property."), Category("Behavior")]
        public bool? IncrementalFiltering
        {
            get => 
                (bool?) base.GetValue(IncrementalFilteringProperty);
            set => 
                base.SetValue(IncrementalFilteringProperty, value);
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
        public bool AllowCollectionView
        {
            get => 
                (bool) base.GetValue(AllowCollectionViewProperty);
            set => 
                base.SetValue(AllowCollectionViewProperty, value);
        }

        [Category("Behavior"), TypeConverter(typeof(FilterCriteriaConverter))]
        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        [Description("Searches for an item(s) that meets the specified criteria.")]
        public ICommand FindCommand { get; private set; }

        [Description("Adds a new item.")]
        public ICommand AddNewCommand
        {
            get => 
                this.addNewCommand;
            private set
            {
                if (!ReferenceEquals(value, this.addNewCommand))
                {
                    this.addNewCommand = value;
                    this.AddNewCommandChanged();
                }
            }
        }

        public ICommand SelectAllItemsCommand { get; private set; }

        [Browsable(false)]
        public string AutoSearchText
        {
            get => 
                (string) base.GetValue(AutoSearchTextProperty);
            internal set => 
                base.SetValue(AutoSearchTextPropertyKey, value);
        }

        [Browsable(false)]
        public object SelectedItemValue
        {
            get => 
                base.GetValue(SelectedItemValueProperty);
            internal set => 
                base.SetValue(SelectedItemValuePropertyKey, value);
        }

        [Browsable(false)]
        public bool IsTokenMode
        {
            get => 
                (bool) base.GetValue(IsTokenModeProperty);
            internal set => 
                base.SetValue(IsTokenModePropertyKey, value);
        }

        public bool AssignNullValueOnClearingEditText
        {
            get => 
                (bool) base.GetValue(AssignNullValueOnClearingEditTextProperty);
            set => 
                base.SetValue(AssignNullValueOnClearingEditTextProperty, value);
        }

        protected LookUpEditBasePropertyProvider PropertyProvider =>
            (LookUpEditBasePropertyProvider) base.PropertyProvider;

        protected internal override Type StyleSettingsType =>
            typeof(BaseLookUpStyleSettings);

        protected internal override bool CanShowPopup =>
            base.CanShowPopup && this.CanShowPopupCore();

        public override FrameworkElement PopupElement =>
            this.VisualClient.InnerEditor;

        protected internal LookUpEditStrategyBase EditStrategy
        {
            get => 
                base.EditStrategy as LookUpEditStrategyBase;
            set => 
                base.EditStrategy = value;
        }

        ObservableCollection<GroupStyle> ISelectorEdit.GroupStyle =>
            new ObservableCollection<GroupStyle>();

        EditStrategyBase ISelectorEdit.EditStrategy =>
            this.EditStrategy;

        SelectionEventMode ISelectorEdit.SelectionEventMode =>
            SelectionEventMode.MouseDown;

        ListItemCollection ISelectorEdit.Items =>
            ((IItemsProviderOwner) this.Settings).Items;

        ISelectionProvider ISelectorEdit.SelectionProvider =>
            new DummySelectionProvider();

        IListNotificationOwner ISelectorEdit.ListNotificationOwner =>
            this.Settings;

        IItemsProvider2 ISelectorEdit.ItemsProvider =>
            this.ItemsProvider;

        SelectionMode ISelectorEdit.SelectionMode =>
            this.EditStrategy.StyleSettings.GetSelectionMode(this);

        bool ISelectorEdit.UseCustomItems =>
            this.PropertyProvider.ShowCustomItems();

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

        bool IBaseEdit.ShowEditorButtons
        {
            get => 
                base.ShowEditorButtons;
            set => 
                base.ShowEditorButtons = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookUpEditBase.<>c <>9 = new LookUpEditBase.<>c();
            public static Action<LookUpEditBase, object, ItemsProviderChangedEventArgs> <>9__67_0;

            internal void <.cctor>b__47_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).AllowLiveDataShapingChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).FilterCriteriaChanged((CriteriaOperator) e.NewValue);
            }

            internal void <.cctor>b__47_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).FindModeChanged((FindMode?) e.NewValue);
            }

            internal void <.cctor>b__47_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).FilterConditionChanged((FilterCondition?) e.NewValue);
            }

            internal object <.cctor>b__47_12(DependencyObject d, object v) => 
                ((LookUpEditBase) d).CoerceFilterCondition((FilterCondition?) v);

            internal void <.cctor>b__47_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).OnIsTokenModeChanged();
            }

            internal void <.cctor>b__47_14(LookUpEditBase owner, bool oldValue, bool newValue)
            {
                owner.IsAsyncOperationInProgressChanged(oldValue, newValue);
            }

            internal void <.cctor>b__47_15(LookUpEditBase @base, bool value, bool newValue)
            {
                @base.AllowRejectUnknownValuesChanged(newValue);
            }

            internal void <.cctor>b__47_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).SelectItemWithNullValueChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).AllowCollectionViewChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).IsSynchronizedWithCurrentItemChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).OnDisplayMemberChanged((string) e.NewValue);
            }

            internal void <.cctor>b__47_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).OnValueMemberChanged((string) e.NewValue);
            }

            internal void <.cctor>b__47_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).ClearSelectionOnBackspaceChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).AutoCompleteChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__47_8(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((LookUpEditBase) o).IsCaseSensitiveFilterChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__47_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LookUpEditBase) d).AddNewButtonPlacementChanged((EditorPlacement?) e.NewValue);
            }

            internal void <.ctor>b__67_0(LookUpEditBase owner, object o, ItemsProviderChangedEventArgs e)
            {
                owner.EditStrategy.ItemProviderChanged(e);
            }
        }
    }
}

