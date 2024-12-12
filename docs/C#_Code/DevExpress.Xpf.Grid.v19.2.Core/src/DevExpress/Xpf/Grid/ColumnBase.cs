namespace DevExpress.Xpf.Grid
{
    using DevExpress.Core;
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.GridData;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraGrid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    public abstract class ColumnBase : BaseColumn, IColumnInfo, IDefaultEditorViewInfo, IBestFitColumn, IDataColumnInfo, IInplaceEditorColumn, INotifyPropertyChanged, IMergeWithPreviousGroup
    {
        protected internal static readonly DependencyPropertyKey IsSortedPropertyKey;
        public static readonly DependencyProperty IsSortedProperty;
        public static readonly DependencyProperty SortOrderProperty;
        public static readonly DependencyProperty DefaultSortOrderProperty;
        public static readonly DependencyProperty AllowedSortOrdersProperty;
        private static readonly DependencyPropertyKey IsSortedBySummaryPropertyKey;
        public static readonly DependencyProperty IsSortedBySummaryProperty;
        internal const string FieldNamePropertyName = "FieldName";
        public static readonly DependencyProperty FieldNameProperty;
        private static readonly DependencyPropertyKey FieldTypePropertyKey;
        public static readonly DependencyProperty FieldTypeProperty;
        public static readonly DependencyProperty UnboundTypeProperty;
        public static readonly DependencyProperty UnboundExpressionProperty;
        public static readonly DependencyProperty ReadOnlyProperty;
        public static readonly DependencyProperty AllowEditingProperty;
        public static readonly DependencyProperty EditSettingsProperty;
        public static readonly DependencyProperty DisplayTemplateProperty;
        public static readonly DependencyProperty EditTemplateProperty;
        public static readonly DependencyProperty SortModeProperty;
        public static readonly DependencyProperty SortIndexProperty;
        public static readonly DependencyProperty CellTemplateProperty;
        public static readonly DependencyProperty CellTemplateSelectorProperty;
        public static readonly DependencyProperty CellDisplayTemplateProperty;
        public static readonly DependencyProperty CellDisplayTemplateSelectorProperty;
        public static readonly DependencyProperty CellEditTemplateProperty;
        public static readonly DependencyProperty CellEditTemplateSelectorProperty;
        private static readonly DependencyPropertyKey ActualCellTemplateSelectorPropertyKey;
        public static readonly DependencyProperty ActualCellTemplateSelectorProperty;
        public static readonly DependencyProperty HeaderCustomizationAreaTemplateProperty;
        public static readonly DependencyProperty HeaderCustomizationAreaTemplateSelectorProperty;
        private static readonly DependencyPropertyKey ActualHeaderCustomizationAreaTemplateSelectorPropertyKey;
        public static readonly DependencyProperty ActualHeaderCustomizationAreaTemplateSelectorProperty;
        public static readonly DependencyProperty FilterEditorHeaderTemplateProperty;
        public static readonly DependencyProperty HeaderPresenterTypeProperty;
        private static readonly DependencyPropertyKey ActualDataWidthPropertyKey;
        public static readonly DependencyProperty ActualDataWidthProperty;
        private static readonly DependencyPropertyKey ActualAdditionalRowDataWidthPropertyKey;
        public static readonly DependencyProperty ActualAdditionalRowDataWidthProperty;
        private static readonly DependencyPropertyKey TotalSummaryTextPropertyKey;
        public static readonly DependencyProperty TotalSummaryTextProperty;
        private static readonly DependencyPropertyKey TotalSummaryTextInfoPropertyKey;
        public static readonly DependencyProperty TotalSummaryTextInfoProperty;
        private static readonly DependencyPropertyKey HasTotalSummariesPropertyKey;
        public static readonly DependencyProperty HasTotalSummariesProperty;
        private static readonly DependencyPropertyKey TotalSummariesPropertyKey;
        public static readonly DependencyProperty TotalSummariesProperty;
        public static readonly DependencyProperty AllowSortingProperty;
        private static readonly DependencyPropertyKey ActualAllowSortingPropertyKey;
        public static readonly DependencyProperty ActualAllowSortingProperty;
        private static readonly DependencyPropertyKey ActualEditSettingsPropertyKey;
        public static readonly DependencyProperty ActualEditSettingsProperty;
        private static readonly DependencyPropertyKey ActualHorizontalContentAlignmentPropertyKey;
        public static readonly DependencyProperty ActualHorizontalContentAlignmentProperty;
        public static readonly DependencyProperty NavigationIndexProperty;
        public static readonly DependencyProperty CellStyleProperty;
        public static readonly DependencyProperty AutoFilterRowCellStyleProperty;
        public static readonly DependencyProperty NewItemRowCellStyleProperty;
        public static readonly DependencyProperty ColumnHeaderContentStyleProperty;
        public static readonly DependencyProperty TotalSummaryContentStyleProperty;
        public static readonly DependencyProperty ActualCellStyleProperty;
        public static readonly DependencyProperty ActualAutoFilterRowCellStyleProperty;
        public static readonly DependencyProperty ActualNewItemRowCellStyleProperty;
        public static readonly DependencyProperty ActualColumnHeaderContentStyleProperty;
        public static readonly DependencyProperty ActualTotalSummaryContentStyleProperty;
        private static readonly DependencyPropertyKey ActualCellStylePropertyKey;
        private static readonly DependencyPropertyKey ActualAutoFilterRowCellStylePropertyKey;
        private static readonly DependencyPropertyKey ActualNewItemRowCellStylePropertyKey;
        private static readonly DependencyPropertyKey ActualColumnHeaderContentStylePropertyKey;
        private static readonly DependencyPropertyKey ActualTotalSummaryContentStylePropertyKey;
        internal const string ActualAllowColumnFilteringPropertyName = "ActualAllowColumnFiltering";
        protected static readonly DependencyPropertyKey ActualAllowColumnFilteringPropertyKey;
        public static readonly DependencyProperty ActualAllowColumnFilteringProperty;
        protected static readonly DependencyPropertyKey ActualAllowFilterEditorPropertyKey;
        public static readonly DependencyProperty ActualAllowFilterEditorProperty;
        internal const string IsFilteredPropertyName = "IsFiltered";
        protected static readonly DependencyPropertyKey IsFilteredPropertyKey;
        public static readonly DependencyProperty IsFilteredProperty;
        public static readonly DependencyProperty AutoFilterValueProperty;
        public static readonly DependencyProperty CustomColumnFilterPopupTemplateProperty;
        public static readonly DependencyProperty AutoFilterConditionProperty;
        public static readonly DependencyProperty AutoFilterCriteriaProperty;
        public static readonly DependencyProperty ShowCriteriaInAutoFilterRowProperty;
        public static readonly DependencyProperty AllowAutoFilterProperty;
        public static readonly DependencyProperty ActualAllowAutoFilterProperty;
        protected static readonly DependencyPropertyKey ActualAllowAutoFilterPropertyKey;
        public static readonly DependencyProperty ImmediateUpdateAutoFilterProperty;
        public static readonly DependencyProperty AllowColumnFilteringProperty;
        public static readonly DependencyProperty FilterPopupModeProperty;
        public static readonly DependencyProperty ColumnFilterModeProperty;
        public static readonly DependencyProperty ImmediateUpdateColumnFilterProperty;
        public static readonly DependencyProperty ColumnFilterPopupMaxRecordsCountProperty;
        public static readonly DependencyProperty ShowEmptyDateFilterProperty;
        public static readonly DependencyProperty AutoFilterRowDisplayTemplateProperty;
        public static readonly DependencyProperty AutoFilterRowEditTemplateProperty;
        public static readonly DependencyProperty RoundDateTimeForColumnFilterProperty;
        public static readonly DependencyProperty RoundDateDisplayFormatProperty;
        public static readonly DependencyProperty FilterPopupGroupFieldsProperty;
        public static readonly DependencyProperty AllowedUnaryFiltersProperty;
        public static readonly DependencyProperty AllowedBinaryFiltersProperty;
        public static readonly DependencyProperty AllowedAnyOfFiltersProperty;
        public static readonly DependencyProperty AllowedBetweenFiltersProperty;
        public static readonly DependencyProperty AllowedDateTimeFiltersProperty;
        public static readonly DependencyProperty AllowedDataAnalysisFiltersProperty;
        public static readonly DependencyProperty AllowedTotalSummariesProperty;
        public static readonly DependencyProperty ShowAllTableValuesInCheckedFilterPopupProperty;
        public static readonly DependencyProperty ShowAllTableValuesInFilterPopupProperty;
        public static readonly DependencyProperty ShowInColumnChooserProperty;
        public static readonly DependencyProperty ColumnChooserHeaderCaptionProperty;
        protected static readonly DependencyPropertyKey ActualColumnChooserHeaderCaptionPropertyKey;
        internal const string ActualColumnChooserHeaderCaptionPropertyName = "ActualColumnChooserHeaderCaption";
        public static readonly DependencyProperty ActualColumnChooserHeaderCaptionProperty;
        public static readonly DependencyProperty BestFitMaxRowCountProperty;
        public static readonly DependencyProperty BestFitModeProperty;
        public static readonly DependencyProperty BestFitAreaProperty;
        private static readonly DependencyPropertyKey IsLastPropertyKey;
        public static readonly DependencyProperty IsLastProperty;
        private static readonly DependencyPropertyKey IsFirstPropertyKey;
        public static readonly DependencyProperty IsFirstProperty;
        public static readonly DependencyProperty AllowUnboundExpressionEditorProperty;
        public static readonly DependencyProperty PrintCellStyleProperty;
        public static readonly DependencyProperty PrintColumnHeaderStyleProperty;
        public static readonly DependencyProperty PrintTotalSummaryStyleProperty;
        public static readonly DependencyProperty ActualPrintCellStyleProperty;
        public static readonly DependencyProperty ActualPrintColumnHeaderStyleProperty;
        public static readonly DependencyProperty ActualPrintTotalSummaryStyleProperty;
        private static readonly DependencyPropertyKey ActualPrintCellStylePropertyKey;
        private static readonly DependencyPropertyKey ActualPrintColumnHeaderStylePropertyKey;
        private static readonly DependencyPropertyKey ActualPrintTotalSummaryStylePropertyKey;
        public static readonly DependencyProperty AllowFocusProperty;
        public static readonly DependencyProperty TabStopProperty;
        public static readonly DependencyProperty ShowValidationAttributeErrorsProperty;
        private static readonly DependencyPropertyKey ActualShowValidationAttributeErrorsPropertyKey;
        public static readonly DependencyProperty ActualShowValidationAttributeErrorsProperty;
        public static readonly DependencyProperty AllowSearchPanelProperty;
        public static readonly DependencyProperty AllowIncrementalSearchProperty;
        public static readonly DependencyProperty CopyValueAsDisplayTextProperty;
        public static readonly DependencyProperty IsSmartProperty;
        public static readonly DependencyProperty AllowCellMergeProperty;
        public static readonly DependencyProperty AllowConditionalFormattingMenuProperty;
        public static readonly DependencyProperty EditFormCaptionProperty;
        public static readonly DependencyProperty EditFormColumnSpanProperty;
        public static readonly DependencyProperty EditFormRowSpanProperty;
        public static readonly DependencyProperty EditFormStartNewRowProperty;
        public static readonly DependencyProperty EditFormVisibleProperty;
        public static readonly DependencyProperty EditFormVisibleIndexProperty;
        public static readonly DependencyProperty EditFormTemplateProperty;
        public static readonly DependencyProperty SortFieldNameProperty;
        public static readonly DependencyProperty ShowCheckBoxInHeaderProperty;
        private static readonly DependencyPropertyKey ActualShowCheckBoxInHeaderPropertyKey;
        public static readonly DependencyProperty ActualShowCheckBoxInHeaderProperty;
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty PredefinedFiltersProperty;
        internal bool SeparatorForceUpdate;
        private BindingBase displayMemberBinding;
        private DataControlBase ownerControlCore;
        private readonly NamePropertyChangeListener namePropertyChangeListener;
        private ColumnFilterInfoBase columnFilterInfo;
        private static bool AllowVirtualSourceEditing;
        internal Locker filterUpdateLocker = new Locker();
        private bool lockAutoFilterUpdate;
        internal readonly Locker InsertLocker = new Locker();
        internal ActualTemplateSelectorWrapper ActualCellDisplayTemplateSelector;
        internal ActualTemplateSelectorWrapper ActualCellEditTemplateSelector;
        private UnsubscribeAction filteringUIUnsubscribeAction;
        private IItemsProvider2 itemsProvider;
        private bool oldIIsAsyncLookup;
        private BaseEditSettings defaultSettings;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private DevExpress.Xpf.Grid.DisplayMemberBindingCalculator displayMemberBindingCalculator;
        private DevExpress.Xpf.Grid.SimpleBindingProcessor simpleBindingProcessor;
        internal bool isAutoDetectedUnboundType = true;
        private Locker isCheckedLocker = new Locker();
        internal Locker UpdateIsCheckedLocker = new Locker();

        internal event EventHandler OwnerChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        static ColumnBase()
        {
            Type ownerType = typeof(ColumnBase);
            IsSortedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsSorted", typeof(bool), ownerType, new PropertyMetadata(false));
            IsSortedProperty = IsSortedPropertyKey.DependencyProperty;
            IsSortedBySummaryPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsSortedBySummary", typeof(bool), ownerType, new PropertyMetadata(false));
            IsSortedBySummaryProperty = IsSortedBySummaryPropertyKey.DependencyProperty;
            SortOrderProperty = DependencyPropertyManager.Register("SortOrder", typeof(ColumnSortOrder), ownerType, new PropertyMetadata(ColumnSortOrder.None, new PropertyChangedCallback(ColumnBase.OnSortOrderChanged)));
            DefaultSortOrderProperty = DependencyPropertyManager.Register("DefaultSortOrder", typeof(ListSortDirection), ownerType, new PropertyMetadata(ListSortDirection.Ascending));
            AllowedSortOrdersProperty = DependencyPropertyManager.Register("AllowedSortOrders", typeof(DevExpress.Data.AllowedSortOrders), ownerType, new PropertyMetadata(DevExpress.Data.AllowedSortOrders.All));
            FieldTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("FieldType", typeof(Type), ownerType, new PropertyMetadata(typeof(object), (d, e) => ((ColumnBase) d).OnFieldTypeChanged()));
            FieldTypeProperty = FieldTypePropertyKey.DependencyProperty;
            FieldNameProperty = DependencyPropertyManager.Register("FieldName", typeof(string), ownerType, new FrameworkPropertyMetadata("", new PropertyChangedCallback(ColumnBase.OnFieldNameChanged), (column, fieldName) => ((ColumnBase) column).CoerceFieldName(fieldName)));
            UnboundTypeProperty = DependencyPropertyManager.Register("UnboundType", typeof(UnboundColumnType), ownerType, new FrameworkPropertyMetadata(UnboundColumnType.Bound, new PropertyChangedCallback(ColumnBase.OnUnboundTypeChanged)));
            UnboundExpressionProperty = DependencyPropertyManager.Register("UnboundExpression", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((ColumnBase) d).OnUnboundExpressionChanged()));
            ReadOnlyProperty = DependencyPropertyManager.Register("ReadOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((ColumnBase) d).OnReadOnlyChanged()));
            AllowEditingProperty = DependencyPropertyManager.Register("AllowEditing", typeof(DefaultBoolean), ownerType, new FrameworkPropertyMetadata(DefaultBoolean.Default, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((ColumnBase) d).UpdateEditorButtonVisibilities();
                ((ColumnBase) d).RaiseAllowEditingChanged();
            }));
            EditSettingsProperty = DependencyPropertyManager.Register("EditSettings", typeof(BaseEditSettings), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnEditSettingsChanged)));
            EditTemplateProperty = DependencyPropertyManager.Register("EditTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            DisplayTemplateProperty = DependencyPropertyManager.Register("DisplayTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnDisplayTemplateChanged)));
            SortModeProperty = DependencyPropertyManager.Register("SortMode", typeof(ColumnSortMode), ownerType, new FrameworkPropertyMetadata(ColumnSortMode.Default, new PropertyChangedCallback(ColumnBase.OnDataPropertyChanged)));
            SortIndexProperty = DependencyPropertyManager.Register("SortIndex", typeof(int), ownerType, new PropertyMetadata(-1, new PropertyChangedCallback(ColumnBase.OnSortIndexChanged)));
            CellTemplateProperty = DependencyPropertyManager.Register("CellTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellTemplateSelector()));
            CellTemplateSelectorProperty = DependencyPropertyManager.Register("CellTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellTemplateSelector()));
            CellDisplayTemplateProperty = DependencyPropertyManager.Register("CellDisplayTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellTemplateSelector()));
            CellDisplayTemplateSelectorProperty = DependencyPropertyManager.Register("CellDisplayTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellTemplateSelector()));
            CellEditTemplateProperty = DependencyPropertyManager.Register("CellEditTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellEditTemplateSelector()));
            CellEditTemplateSelectorProperty = DependencyPropertyManager.Register("CellEditTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualCellEditTemplateSelector()));
            ActualCellTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualCellTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, _) => ((ColumnBase) d).UpdateContentLayout()));
            ActualCellTemplateSelectorProperty = ActualCellTemplateSelectorPropertyKey.DependencyProperty;
            HeaderCustomizationAreaTemplateProperty = DependencyPropertyManager.Register("HeaderCustomizationAreaTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualHeaderCustomizationAreaTemplateSelector()));
            HeaderCustomizationAreaTemplateSelectorProperty = DependencyPropertyManager.Register("HeaderCustomizationAreaTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).UpdateActualHeaderCustomizationAreaTemplateSelector()));
            ActualHeaderCustomizationAreaTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualHeaderCustomizationAreaTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).RaiseContentChanged(ActualHeaderCustomizationAreaTemplateSelectorProperty)));
            ActualHeaderCustomizationAreaTemplateSelectorProperty = ActualHeaderCustomizationAreaTemplateSelectorPropertyKey.DependencyProperty;
            FilterEditorHeaderTemplateProperty = DependencyPropertyManager.Register("FilterEditorHeaderTemplate", typeof(DataTemplate), ownerType);
            HeaderPresenterTypeProperty = DependencyPropertyManager.RegisterAttached("HeaderPresenterType", typeof(HeaderPresenterType), ownerType, new FrameworkPropertyMetadata(HeaderPresenterType.Headers, FrameworkPropertyMetadataOptions.Inherits));
            ActualDataWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualDataWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, (d, _) => ((ColumnBase) d).UpdateContentLayout(), (CoerceValueCallback) ((d, baseValue) => Math.Max(0.0, (double) baseValue))));
            ActualDataWidthProperty = ActualDataWidthPropertyKey.DependencyProperty;
            ActualAdditionalRowDataWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAdditionalRowDataWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, null, (CoerceValueCallback) ((d, baseValue) => Math.Max(0.0, (double) baseValue))));
            ActualAdditionalRowDataWidthProperty = ActualAdditionalRowDataWidthPropertyKey.DependencyProperty;
            TotalSummaryTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("TotalSummaryText", typeof(string), ownerType, new PropertyMetadata(" "));
            TotalSummaryTextProperty = TotalSummaryTextPropertyKey.DependencyProperty;
            TotalSummaryTextInfoPropertyKey = DependencyPropertyManager.RegisterReadOnly("TotalSummaryTextInfo", typeof(InlineCollectionInfo), ownerType, new PropertyMetadata(null));
            TotalSummaryTextInfoProperty = TotalSummaryTextInfoPropertyKey.DependencyProperty;
            HasTotalSummariesPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasTotalSummaries", typeof(bool), ownerType, new PropertyMetadata(false));
            HasTotalSummariesProperty = HasTotalSummariesPropertyKey.DependencyProperty;
            TotalSummariesPropertyKey = DependencyPropertyManager.RegisterReadOnly("TotalSummaries", typeof(IList<GridTotalSummaryData>), ownerType, new PropertyMetadata(null));
            TotalSummariesProperty = TotalSummariesPropertyKey.DependencyProperty;
            NavigationIndexProperty = DependencyPropertyManager.RegisterAttached("NavigationIndex", typeof(int), ownerType, new PropertyMetadata(-2147483648, new PropertyChangedCallback(ColumnBase.OnNavigationIndexChanged)));
            AllowSortingProperty = DependencyPropertyManager.Register("AllowSorting", typeof(DefaultBoolean), ownerType, new FrameworkPropertyMetadata(DefaultBoolean.Default, (d, e) => ((ColumnBase) d).UpdateActualAllowSorting(), (CoerceValueCallback) ((d, value) => ((ColumnBase) d).CoerceAllowSorting((DefaultBoolean) value))));
            ActualAllowSortingPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAllowSorting", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ActualAllowSortingProperty = ActualAllowSortingPropertyKey.DependencyProperty;
            ActualEditSettingsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualEditSettings", typeof(BaseEditSettings), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnActualEditSettingsChanged)));
            ActualEditSettingsProperty = ActualEditSettingsPropertyKey.DependencyProperty;
            ActualHorizontalContentAlignmentPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualHorizontalContentAlignment", typeof(HorizontalAlignment), ownerType, new PropertyMetadata(HorizontalAlignment.Right));
            ActualHorizontalContentAlignmentProperty = ActualHorizontalContentAlignmentPropertyKey.DependencyProperty;
            CellStyleProperty = DependencyPropertyManager.Register("CellStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            AutoFilterRowCellStyleProperty = DependencyPropertyManager.Register("AutoFilterRowCellStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            NewItemRowCellStyleProperty = DependencyPropertyManager.Register("NewItemRowCellStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            ColumnHeaderContentStyleProperty = DependencyPropertyManager.Register("ColumnHeaderContentStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            ActualCellStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualCellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, (d, _) => ((ColumnBase) d).OnActualCellStyleCahnged()));
            ActualCellStyleProperty = ActualCellStylePropertyKey.DependencyProperty;
            ActualAutoFilterRowCellStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAutoFilterRowCellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualAutoFilterRowCellStyleProperty = ActualAutoFilterRowCellStylePropertyKey.DependencyProperty;
            ActualNewItemRowCellStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualNewItemRowCellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualNewItemRowCellStyleProperty = ActualNewItemRowCellStylePropertyKey.DependencyProperty;
            ActualColumnHeaderContentStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualColumnHeaderContentStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).RaiseContentChanged(ActualColumnHeaderContentStyleProperty)));
            ActualColumnHeaderContentStyleProperty = ActualColumnHeaderContentStylePropertyKey.DependencyProperty;
            PrintCellStyleProperty = DependencyPropertyManager.Register("PrintCellStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            PrintColumnHeaderStyleProperty = DependencyPropertyManager.Register("PrintColumnHeaderStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            PrintTotalSummaryStyleProperty = DependencyPropertyManager.Register("PrintTotalSummaryStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            ActualPrintCellStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPrintCellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualPrintCellStyleProperty = ActualPrintCellStylePropertyKey.DependencyProperty;
            ActualPrintColumnHeaderStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPrintColumnHeaderStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualPrintColumnHeaderStyleProperty = ActualPrintColumnHeaderStylePropertyKey.DependencyProperty;
            ActualPrintTotalSummaryStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPrintTotalSummaryStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualPrintTotalSummaryStyleProperty = ActualPrintTotalSummaryStylePropertyKey.DependencyProperty;
            ActualAllowColumnFilteringPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAllowColumnFiltering", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(ColumnBase.OnActualAllowColumnFilteringChanged)));
            ActualAllowColumnFilteringProperty = ActualAllowColumnFilteringPropertyKey.DependencyProperty;
            ActualAllowFilterEditorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAllowFilterEditor", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(ColumnBase.OnActualAllowFilterEditorChanged)));
            ActualAllowFilterEditorProperty = ActualAllowFilterEditorPropertyKey.DependencyProperty;
            IsFilteredPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFiltered", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((ColumnBase) d).RaiseContentChanged(IsFilteredProperty)));
            IsFilteredProperty = IsFilteredPropertyKey.DependencyProperty;
            AutoFilterValueProperty = DependencyPropertyManager.Register("AutoFilterValue", typeof(object), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAutoFilterValueChanged)));
            CustomColumnFilterPopupTemplateProperty = DependencyPropertyManager.Register("CustomColumnFilterPopupTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            AutoFilterConditionProperty = DependencyPropertyManager.Register("AutoFilterCondition", typeof(DevExpress.Xpf.Grid.AutoFilterCondition), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.AutoFilterCondition.Default, new PropertyChangedCallback(ColumnBase.OnAutoFilterConditionChanged)));
            AutoFilterCriteriaProperty = DependencyPropertyManager.Register("AutoFilterCriteria", typeof(ClauseType?), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAutoFilterCriteriaChanged)));
            ShowCriteriaInAutoFilterRowProperty = DependencyPropertyManager.Register("ShowCriteriaInAutoFilterRow", typeof(bool?), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnShowCriteriaInAutoFilterRowChanged)));
            AllowAutoFilterProperty = DependencyPropertyManager.Register("AllowAutoFilter", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(ColumnBase.OnAllowAutoFilterChanged)));
            ActualAllowAutoFilterPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAllowAutoFilter", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ColumnBase.OnActualAllowAutoFilterChanged)));
            ActualAllowAutoFilterProperty = ActualAllowAutoFilterPropertyKey.DependencyProperty;
            ImmediateUpdateAutoFilterProperty = DependencyPropertyManager.Register("ImmediateUpdateAutoFilter", typeof(bool), ownerType, new PropertyMetadata(true));
            AllowColumnFilteringProperty = DependencyPropertyManager.Register("AllowColumnFiltering", typeof(DefaultBoolean), ownerType, new PropertyMetadata(DefaultBoolean.Default, new PropertyChangedCallback(ColumnBase.OnAllowColumnFilteringChanged)));
            FilterPopupModeProperty = DependencyPropertyManager.Register("FilterPopupMode", typeof(DevExpress.Xpf.Grid.FilterPopupMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.FilterPopupMode.Default, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            ColumnFilterModeProperty = DependencyPropertyManager.Register("ColumnFilterMode", typeof(DevExpress.Xpf.Grid.ColumnFilterMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ColumnFilterMode.Value, new PropertyChangedCallback(ColumnBase.OnColumnFilterModeChanged)));
            ImmediateUpdateColumnFilterProperty = DependencyPropertyManager.Register("ImmediateUpdateColumnFilter", typeof(bool), ownerType, new PropertyMetadata(true));
            ColumnFilterPopupMaxRecordsCountProperty = DependencyPropertyManager.Register("ColumnFilterPopupMaxRecordsCount", typeof(int), ownerType, new PropertyMetadata(-1));
            ShowEmptyDateFilterProperty = DependencyPropertyManager.Register("ShowEmptyDateFilter", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AutoFilterRowDisplayTemplateProperty = DependencyPropertyManager.Register("AutoFilterRowDisplayTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAutoFilterRowDisplayTemplateChanged)));
            AutoFilterRowEditTemplateProperty = DependencyPropertyManager.Register("AutoFilterRowEditTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            RoundDateTimeForColumnFilterProperty = DependencyPropertyManager.Register("RoundDateTimeForColumnFilter", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ColumnBase.OnRoundDateChanged)));
            RoundDateDisplayFormatProperty = DependencyPropertyManager.Register("RoundDateDisplayFormat", typeof(string), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnRoundDateChanged)));
            FilterPopupGroupFieldsProperty = DependencyPropertyManager.Register("FilterPopupGroupFields", typeof(string), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnGroupFieldsChanged)));
            AllowedUnaryFiltersProperty = DependencyPropertyManager.Register("AllowedUnaryFilters", typeof(DevExpress.Xpf.Grid.AllowedUnaryFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedBinaryFiltersProperty = DependencyPropertyManager.Register("AllowedBinaryFilters", typeof(DevExpress.Xpf.Grid.AllowedBinaryFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedAnyOfFiltersProperty = DependencyPropertyManager.Register("AllowedAnyOfFilters", typeof(DevExpress.Xpf.Grid.AllowedAnyOfFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedBetweenFiltersProperty = DependencyPropertyManager.Register("AllowedBetweenFilters", typeof(DevExpress.Xpf.Grid.AllowedBetweenFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedDateTimeFiltersProperty = DependencyPropertyManager.Register("AllowedDateTimeFilters", typeof(DevExpress.Xpf.Grid.AllowedDateTimeFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedDataAnalysisFiltersProperty = DependencyPropertyManager.Register("AllowedDataAnalysisFilters", typeof(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnFilterPopupModeChanged)));
            AllowedTotalSummariesProperty = DependencyPropertyManager.Register("AllowedTotalSummaries", typeof(DevExpress.Xpf.Grid.AllowedTotalSummaries?), ownerType, new FrameworkPropertyMetadata(null));
            ShowAllTableValuesInFilterPopupProperty = DependencyPropertyManager.Register("ShowAllTableValuesInFilterPopup", typeof(DefaultBoolean), ownerType, new PropertyMetadata(DefaultBoolean.Default));
            ShowAllTableValuesInCheckedFilterPopupProperty = DependencyPropertyManager.Register("ShowAllTableValuesInCheckedFilterPopup", typeof(DefaultBoolean), ownerType, new PropertyMetadata(DefaultBoolean.Default));
            ShowInColumnChooserProperty = DependencyPropertyManager.Register("ShowInColumnChooser", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((ColumnBase) d).RebuildColumnChooserColumns()));
            ActualColumnChooserHeaderCaptionPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualColumnChooserHeaderCaption", typeof(object), ownerType, new PropertyMetadata((d, e) => ((ColumnBase) d).RaiseContentChanged(ActualColumnChooserHeaderCaptionProperty)));
            ActualColumnChooserHeaderCaptionProperty = ActualColumnChooserHeaderCaptionPropertyKey.DependencyProperty;
            ColumnChooserHeaderCaptionProperty = DependencyPropertyManager.Register("ColumnChooserHeaderCaption", typeof(object), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnColumnChooserHeaderCaptionChanged)));
            TotalSummaryContentStyleProperty = DependencyPropertyManager.Register("TotalSummaryContentStyle", typeof(Style), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnAppearanceChanged)));
            ActualTotalSummaryContentStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualTotalSummaryContentStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualTotalSummaryContentStyleProperty = ActualTotalSummaryContentStylePropertyKey.DependencyProperty;
            BestFitMaxRowCountProperty = DependencyPropertyManager.Register("BestFitMaxRowCount", typeof(int), ownerType, new FrameworkPropertyMetadata(-1, null, (CoerceValueCallback) ((d, baseValue) => DataViewBase.CoerceBestFitMaxRowCount(Convert.ToInt32(baseValue)))));
            BestFitModeProperty = DependencyPropertyManager.Register("BestFitMode", typeof(DevExpress.Xpf.Core.BestFitMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Core.BestFitMode.Default));
            BestFitAreaProperty = DependencyPropertyManager.Register("BestFitArea", typeof(DevExpress.Xpf.Grid.BestFitArea), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.BestFitArea.None));
            AllowUnboundExpressionEditorProperty = DependencyPropertyManager.Register("AllowUnboundExpressionEditor", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.AllowPropertyEvent, new AllowPropertyEventHandler(ColumnBase.OnDeserializeAllowProperty));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.DeserializePropertyEvent, new XtraPropertyInfoEventHandler(ColumnBase.OnDeserializeProperty));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.CustomShouldSerializePropertyEvent, new CustomShouldSerializePropertyEventHandler(ColumnBase.OnCustomShouldSerializeProperty));
            IsLastPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsLast", typeof(bool), ownerType, new PropertyMetadata(false));
            IsLastProperty = IsLastPropertyKey.DependencyProperty;
            IsFirstPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFirst", typeof(bool), ownerType, new PropertyMetadata(false));
            IsFirstProperty = IsFirstPropertyKey.DependencyProperty;
            AllowFocusProperty = DependencyPropertyManager.Register("AllowFocus", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(ColumnBase.OnAllowFocusChanged)));
            TabStopProperty = DependencyPropertyManager.Register("TabStop", typeof(bool), ownerType, new PropertyMetadata(true));
            ShowValidationAttributeErrorsProperty = DependencyPropertyManager.Register("ShowValidationAttributeErrors", typeof(DefaultBoolean), ownerType, new FrameworkPropertyMetadata(DefaultBoolean.Default, (d, e) => ((ColumnBase) d).UpdateShowValidationAttributeErrors()));
            ActualShowValidationAttributeErrorsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowValidationAttributeErrors", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ActualShowValidationAttributeErrorsProperty = ActualShowValidationAttributeErrorsPropertyKey.DependencyProperty;
            AllowSearchPanelProperty = DependencyPropertyManager.Register("AllowSearchPanel", typeof(DefaultBoolean), ownerType, new FrameworkPropertyMetadata(DefaultBoolean.Default, (d, e) => ((ColumnBase) d).OnAllowSearchPanelChanged()));
            AllowIncrementalSearchProperty = DependencyPropertyManager.Register("AllowIncrementalSearch", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            CopyValueAsDisplayTextProperty = DependencyPropertyManager.Register("CopyValueAsDisplayText", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            IsSmartProperty = DependencyPropertyManager.Register("IsSmart", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((ColumnBase) d).OnIsSmartChanged()));
            AllowCellMergeProperty = DependencyPropertyManager.Register("AllowCellMerge", typeof(bool?), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).OnAllowCellMergeChanged((bool?) e.OldValue)));
            AllowConditionalFormattingMenuProperty = DependencyPropertyManager.Register("AllowConditionalFormattingMenu", typeof(bool?), typeof(ColumnBase), new PropertyMetadata(null));
            EditFormCaptionProperty = DependencyProperty.Register("EditFormCaption", typeof(object), ownerType, new PropertyMetadata(null));
            EditFormColumnSpanProperty = DependencyProperty.Register("EditFormColumnSpan", typeof(int?), ownerType, new PropertyMetadata(null));
            EditFormRowSpanProperty = DependencyProperty.Register("EditFormRowSpan", typeof(int?), ownerType, new PropertyMetadata(null));
            EditFormStartNewRowProperty = DependencyProperty.Register("EditFormStartNewRow", typeof(bool), ownerType, new PropertyMetadata(false));
            EditFormVisibleProperty = DependencyProperty.Register("EditFormVisible", typeof(bool?), ownerType, new PropertyMetadata(null));
            EditFormVisibleIndexProperty = DependencyProperty.Register("EditFormVisibleIndex", typeof(int), ownerType, new PropertyMetadata(0));
            EditFormTemplateProperty = DependencyProperty.Register("EditFormTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            SortFieldNameProperty = DependencyProperty.Register("SortFieldName", typeof(string), ownerType, new PropertyMetadata(null, (d, e) => ((ColumnBase) d).OnSortFieldNameChanged()));
            ShowCheckBoxInHeaderProperty = DependencyPropertyManager.Register("ShowCheckBoxInHeader", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((ColumnBase) d).OnShowCheckBoxInHeaderChanged()));
            ActualShowCheckBoxInHeaderPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowCheckBoxInHeader", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((ColumnBase) d).OnActualShowCheckBoxInHeaderChanged(false)));
            ActualShowCheckBoxInHeaderProperty = ActualShowCheckBoxInHeaderPropertyKey.DependencyProperty;
            IsCheckedProperty = DependencyPropertyManager.Register("IsChecked", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ColumnBase) d).OnIsCheckedChanged()));
            PredefinedFiltersProperty = DependencyProperty.Register("PredefinedFilters", typeof(PredefinedFilterCollection), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ColumnBase.OnPredefinedFiltersChanged)));
            DependencyPropertyKey[] knownKeys = new DependencyPropertyKey[] { ActualAdditionalRowDataWidthPropertyKey, ActualDataWidthPropertyKey };
            CloneDetailHelper.RegisterKnownPropertyKeys(ownerType, knownKeys);
        }

        public ColumnBase()
        {
            this.namePropertyChangeListener = NamePropertyChangeListener.CreateDesignTimeOnly(this, () => this.DesignTimeGridAdorner.UpdateDesignTimeInfo());
            Action<ColumnBase, object, EventArgs> onEventAction = <>c.<>9__420_1;
            if (<>c.<>9__420_1 == null)
            {
                Action<ColumnBase, object, EventArgs> local1 = <>c.<>9__420_1;
                onEventAction = <>c.<>9__420_1 = (owner, o, e) => owner.OnEditSettingsContentChanged();
            }
            this.EditSettingsChangedEventHandler = new EditSettingsChangedEventHandler<ColumnBase>(this, onEventAction);
            this.Commands = new GridColumnCommands(this);
            this.TotalSummariesCore = new List<DevExpress.Xpf.Grid.SummaryItemBase>();
            this.GroupSummariesCore = new List<DevExpress.Xpf.Grid.SummaryItemBase>();
        }

        internal bool ActualAllowTotalSummary() => 
            this.GetActualAllowedTotalSummariesTypes() != DevExpress.Xpf.Grid.AllowedTotalSummaries.None;

        internal bool ActualAllowTotalSummary(SummaryItemType summaryItemType)
        {
            DevExpress.Xpf.Grid.AllowedTotalSummaries actualAllowedTotalSummariesTypes = this.GetActualAllowedTotalSummariesTypes();
            switch (summaryItemType)
            {
                case SummaryItemType.Sum:
                    return actualAllowedTotalSummariesTypes.HasFlag(DevExpress.Xpf.Grid.AllowedTotalSummaries.Sum);

                case SummaryItemType.Min:
                    return actualAllowedTotalSummariesTypes.HasFlag(DevExpress.Xpf.Grid.AllowedTotalSummaries.Min);

                case SummaryItemType.Max:
                    return actualAllowedTotalSummariesTypes.HasFlag(DevExpress.Xpf.Grid.AllowedTotalSummaries.Max);

                case SummaryItemType.Count:
                    return ((base.View == null) || base.View.ActualAllowCountTotalSummary);

                case SummaryItemType.Average:
                    return actualAllowedTotalSummariesTypes.HasFlag(DevExpress.Xpf.Grid.AllowedTotalSummaries.Average);
            }
            return !this.GetIsVirtualSource();
        }

        internal void ActualShowCriteriaInAutoFilterRowChanged()
        {
            this.OnPropertyChanged("ActualShowCriteriaInAutoFilterRow");
        }

        protected internal bool AllowAnyFilter(params DevExpress.Xpf.Grid.AllowedDateTimeFilters[] currentOperators)
        {
            foreach (DevExpress.Xpf.Grid.AllowedDateTimeFilters filters in currentOperators)
            {
                if (this.AllowFilter(filters))
                {
                    return true;
                }
            }
            return false;
        }

        private bool AllowColumnFilteingCore() => 
            (!this.IsUnbound || !this.GetIsVirtualSource()) ? this.GetAllowFilterColumn() : false;

        protected internal bool AllowFilter(DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters currentOperator) => 
            (this.AllowedDataAnalysisFilters != null) ? ((((DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters) this.AllowedDataAnalysisFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        protected internal bool AllowFilter(DevExpress.Xpf.Grid.AllowedAnyOfFilters currentOperator) => 
            (this.AllowedAnyOfFilters != null) ? ((((DevExpress.Xpf.Grid.AllowedAnyOfFilters) this.AllowedAnyOfFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        protected internal bool AllowFilter(DevExpress.Xpf.Grid.AllowedBetweenFilters currentOperator) => 
            (this.AllowedBetweenFilters != null) ? ((((DevExpress.Xpf.Grid.AllowedBetweenFilters) this.AllowedBetweenFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        protected internal bool AllowFilter(DevExpress.Xpf.Grid.AllowedBinaryFilters currentOperator) => 
            (this.AllowedBinaryFilters != null) ? ((((DevExpress.Xpf.Grid.AllowedBinaryFilters) this.AllowedBinaryFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        protected internal bool AllowFilter(DevExpress.Xpf.Grid.AllowedDateTimeFilters currentOperator) => 
            (this.AllowedDateTimeFilters != null) ? ((((DevExpress.Xpf.Grid.AllowedDateTimeFilters) this.AllowedDateTimeFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        protected internal bool AllowFilter(DevExpress.Xpf.Grid.AllowedUnaryFilters currentOperator) => 
            (this.AllowedUnaryFilters != null) ? ((((DevExpress.Xpf.Grid.AllowedUnaryFilters) this.AllowedUnaryFilters.Value) & currentOperator) == currentOperator) : !this.GetIsVirtualSource();

        internal void ApplyColumnFilter(CriteriaOperator op)
        {
            if (this.OwnerControl != null)
            {
                if (op != null)
                {
                    this.OwnerControl.MergeColumnFilters(op);
                }
                else
                {
                    this.OwnerControl.ClearColumnFilter(this);
                }
            }
        }

        internal void AutoFilterCriteriaUpdate()
        {
            base.RaiseContentChanged(FilterCriteriaControlBase.DefaultIndexProperty);
        }

        private bool CalcActualAllowSorting() => 
            this.Owner.AllowSortColumn(this) ? this.GetActualAllowSorting() : false;

        private bool CalcActualShowValidationAttributeErrors() => 
            this.ShowValidationAttributeErrors.GetValue(this.Owner.ShowValidationAttributeErrors);

        internal bool CanBeGroupedByDataControlOwner()
        {
            Func<bool> fallback = <>c.<>9__843_1;
            if (<>c.<>9__843_1 == null)
            {
                Func<bool> local1 = <>c.<>9__843_1;
                fallback = <>c.<>9__843_1 = () => false;
            }
            return this.OwnerControl.Return<DataControlBase, bool>(d => d.GetOriginationDataControl().DataControlOwner.CanGroupColumn(this), fallback);
        }

        protected virtual void CellToolTipBindingPatching()
        {
            DevExpress.Xpf.Grid.DisplayMemberBindingCalculator.ValidateBinding(base.CellToolTipBinding, "RowData.Row", true);
        }

        internal void ChangeOwner()
        {
            this.OnPropertyChanged("View");
            this.OnOwnerChanged();
        }

        protected internal void ClearBindingValues()
        {
            if (this.Binding != null)
            {
                this.Owner.ClearBindingValues(this);
            }
        }

        private DefaultBoolean CoerceAllowSorting(DefaultBoolean value) => 
            ((this.OwnerControl == null) || !this.OwnerControl.IsDissalowSortingColumn(this)) ? value : DefaultBoolean.False;

        private object CoerceFieldName(object fieldName) => 
            (fieldName != null) ? fieldName : string.Empty;

        internal override Func<DataControlBase, BaseColumn> CreateCloneAccessor() => 
            (base.ParentBand == null) ? ((Func<DataControlBase, BaseColumn>) (dc => CloneDetailHelper.SafeGetDependentCollectionItem<ColumnBase>(this, this.OwnerControl.ColumnsCore, dc.ColumnsCore))) : ((Func<DataControlBase, BaseColumn>) BandWalker.CreateColumnCloneAccessor(this));

        protected virtual ColumnFilterInfoBase CreateColumnFilterInfo()
        {
            ColumnFilterInfoBase base5;
            DataControlBase ownerControl = this.OwnerControl;
            if (ownerControl != null)
            {
                base5 = ownerControl.CreateDateColumnFilterInfo(this);
            }
            else
            {
                DataControlBase local1 = ownerControl;
                base5 = null;
            }
            ColumnFilterInfoBase base2 = base5;
            if (base2 != null)
            {
                return base2;
            }
            switch (this.FilterPopupMode)
            {
                case DevExpress.Xpf.Grid.FilterPopupMode.Default:
                {
                    ColumnFilterPopupMode? nullable2;
                    ColumnFilterPopupMode? nullable1;
                    DataControlBase base3 = this.OwnerControl;
                    if (base3 == null)
                    {
                        DataControlBase local2 = base3;
                        nullable2 = null;
                        nullable1 = nullable2;
                    }
                    else
                    {
                        DataViewBase dataView = base3.DataView;
                        if (dataView != null)
                        {
                            nullable1 = new ColumnFilterPopupMode?(dataView.ColumnFilterPopupMode);
                        }
                        else
                        {
                            DataViewBase local3 = dataView;
                            nullable2 = null;
                            nullable1 = nullable2;
                        }
                    }
                    ColumnFilterPopupMode? nullable = nullable1;
                    if (nullable != null)
                    {
                        switch (nullable.GetValueOrDefault())
                        {
                            case ColumnFilterPopupMode.Default:
                                return (CompatibilitySettings.UseLegacyColumnFilterPopup ? ((ColumnFilterInfoBase) new ListColumnFilterInfo(this)) : ((ColumnFilterInfoBase) new ExcelSmartColumnFilterInfo(this)));

                            case ColumnFilterPopupMode.Excel:
                                return new ExcelColumnFilterInfo(this);

                            case ColumnFilterPopupMode.ExcelSmart:
                                return new ExcelSmartColumnFilterInfo(this);

                            default:
                                break;
                        }
                    }
                    return new ListColumnFilterInfo(this);
                }
                case DevExpress.Xpf.Grid.FilterPopupMode.List:
                    return new ListColumnFilterInfo(this);

                case DevExpress.Xpf.Grid.FilterPopupMode.CheckedList:
                    return new CheckedListColumnFilterInfo(this);

                case DevExpress.Xpf.Grid.FilterPopupMode.Custom:
                    return new CustomColumnFilterInfo(this);

                case DevExpress.Xpf.Grid.FilterPopupMode.Excel:
                    return new ExcelColumnFilterInfo(this);

                case DevExpress.Xpf.Grid.FilterPopupMode.ExcelSmart:
                    return new ExcelSmartColumnFilterInfo(this);
            }
            return null;
        }

        private DevExpress.Xpf.Grid.DisplayMemberBindingCalculator CreateDisplayMemberBindingCalculator()
        {
            if ((base.View == null) || (this.Binding == null))
            {
                return null;
            }
            this.displayMemberBindingCalculator = new DevExpress.Xpf.Grid.DisplayMemberBindingCalculator(base.View, this);
            return this.displayMemberBindingCalculator;
        }

        private IList<GridTotalSummaryData> CreateSummaryData(IList<DevExpress.Xpf.Grid.SummaryItemBase> summaries, Func<DevExpress.Xpf.Grid.SummaryItemBase, object> getSummaryValue) => 
            (from item in summaries select new GridTotalSummaryData(item, getSummaryValue(item), this.Owner.GetColumn(item.FieldName))).ToList<GridTotalSummaryData>();

        protected ClauseType? CriteriaOperatorToClauseType(CriteriaOperator op) => 
            FilterClauseHelper.CriteriaOperatorToClauseType(op);

        private DevExpress.Xpf.Grid.AllowedTotalSummaries GetActualAllowedTotalSummariesTypes() => 
            (!this.IsUnbound || !this.GetIsVirtualSource()) ? ((this.AllowedTotalSummaries != null) ? this.AllowedTotalSummaries.Value : (this.GetIsVirtualSource() ? DevExpress.Xpf.Grid.AllowedTotalSummaries.None : DevExpress.Xpf.Grid.AllowedTotalSummaries.All)) : DevExpress.Xpf.Grid.AllowedTotalSummaries.None;

        internal virtual bool GetActualAllowGroupingCore() => 
            this.GetActualAllowSorting();

        protected override bool GetActualAllowResizing(bool autoWidth) => 
            DesignerHelper.GetValue(this, this.GetAllowResizing() && this.Owner.AllowColumnsResizing, true) && ((this.Owner.VisibleColumns != null) && (!autoWidth || ((base.Fixed != FixedStyle.None) || (base.HasRightSibling ? (((this.OwnerControl == null) || (this.OwnerControl.BandsLayoutCore == null)) ? ((this.Owner.VisibleColumns.IndexOf(this) != (this.Owner.VisibleColumns.Count - 1)) && (this.HasSizeableColumnsFromRight() && (this.CanReduce || this.HasSizeableColumnsFromRight()))) : base.HasRightSibling) : false))));

        internal bool GetActualAllowSorting() => 
            !this.GetIsVirtualSource() ? this.AllowSorting.GetValue(this.Owner.AllowSorting) : (!this.IsUnbound && this.AllowSorting.GetValue(false));

        protected IList<DevExpress.Xpf.Grid.SummaryItemBase> GetActualPrintingTotalSummaries() => 
            (base.View != null) ? (from item in this.TotalSummariesCore
                where (base.View.GetActualCalculationMode(item) == GridSummaryCalculationMode.AllRows) && base.View.DataProviderBase.IsSummaryItemExists(SummaryItemCollectionType.Total, item)
                select item).ToList<DevExpress.Xpf.Grid.SummaryItemBase>() : this.TotalSummariesCore;

        internal bool GetActualShowCriteriaInAutoFilterRow() => 
            (this.ShowCriteriaInAutoFilterRow == null) ? ((base.View is ITableView) && ((ITableView) base.View).ShowCriteriaInAutoFilterRow) : this.ShowCriteriaInAutoFilterRow.Value;

        protected internal override DataTemplate GetActualTemplate() => 
            base.HeaderTemplate ?? this.Owner.ColumnHeaderTemplate;

        protected internal override DataTemplateSelector GetActualTemplateSelector() => 
            base.HeaderTemplateSelector ?? this.Owner.ColumnHeaderTemplateSelector;

        protected IList<DevExpress.Xpf.Grid.SummaryItemBase> GetActualTotalSummaries() => 
            (base.View != null) ? (from item in this.TotalSummariesCore
                where base.View.DataProviderBase.IsSummaryItemExists(SummaryItemCollectionType.Total, item)
                select item).ToList<DevExpress.Xpf.Grid.SummaryItemBase>() : this.TotalSummariesCore;

        protected internal virtual bool GetAllowConditionFormatingFilterEditor() => 
            this.Owner.AllowFilterColumn(this);

        protected internal virtual bool GetAllowEditing() => 
            DesignerHelper.GetValue(this, this.AllowEditing.GetValue(this.Owner.AllowEditing), false);

        private bool GetAllowFilterColumn() => 
            this.Owner.AllowFilterColumn(this) && this.AllowColumnFiltering.GetValue(this.Owner.AllowColumnFiltering);

        internal object GetAutoFilterValue(CriteriaOperator op) => 
            this.GetAutoFilterValue(op, this.AutoFilterCriteria);

        protected object GetAutoFilterValue(CriteriaOperator op, ClauseType? autoFilterCriteria)
        {
            ClauseType? nullable;
            ClauseType doesNotContain;
            UnaryOperator operator5;
            BinaryOperator @operator = op as BinaryOperator;
            if (@operator != null)
            {
                OperandValue rightOperand = @operator.RightOperand as OperandValue;
                if (rightOperand == null)
                {
                    return null;
                }
                object obj2 = rightOperand.Value;
                return ((obj2 != null) ? (((this.ResolveAutoFilterCondition() != DevExpress.Xpf.Grid.AutoFilterCondition.Equals) || (@operator.OperatorType != BinaryOperatorType.Equal)) ? (((autoFilterCriteria == null) || !FilterClauseHelper.IsValidClause(this, autoFilterCriteria.Value, this.ColumnFilterMode == DevExpress.Xpf.Grid.ColumnFilterMode.DisplayText, true)) ? null : obj2) : obj2) : null);
            }
            FunctionOperator operator2 = null;
            if (autoFilterCriteria != null)
            {
                nullable = autoFilterCriteria;
                doesNotContain = ClauseType.DoesNotContain;
                if ((((ClauseType) nullable.GetValueOrDefault()) == doesNotContain) ? (nullable != null) : false)
                {
                    goto TR_0029;
                }
                else
                {
                    nullable = autoFilterCriteria;
                    doesNotContain = ClauseType.NotLike;
                    if ((((ClauseType) nullable.GetValueOrDefault()) == doesNotContain) ? (nullable != null) : false)
                    {
                        goto TR_0029;
                    }
                }
            }
            operator2 = op as FunctionOperator;
        TR_0027:
            if (operator2 != null)
            {
                if (operator2.Operands.Count != 2)
                {
                    if ((operator2.Operands.Count == 3) && (operator2.OperatorType == FunctionOperatorType.Custom))
                    {
                        ConstantValue value4 = operator2.Operands[0] as ConstantValue;
                        if ((value4 != null) && ((value4.Value as string) == "Like"))
                        {
                            OperandValue value5 = operator2.Operands[2] as OperandValue;
                            if (value5 != null)
                            {
                                return (value5.Value as string);
                            }
                        }
                    }
                    return null;
                }
                OperandValue value3 = operator2.Operands[1] as OperandValue;
                if (value3 == null)
                {
                    return null;
                }
                string str = value3.Value as string;
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                DevExpress.Xpf.Grid.AutoFilterCondition condition2 = this.ResolveAutoFilterCondition();
                if ((condition2 == DevExpress.Xpf.Grid.AutoFilterCondition.Contains) && (operator2.OperatorType == FunctionOperatorType.Contains))
                {
                    return str;
                }
                if ((condition2 == DevExpress.Xpf.Grid.AutoFilterCondition.Like) && (operator2.OperatorType == FunctionOperatorType.Contains))
                {
                    if (autoFilterCriteria != null)
                    {
                        nullable = autoFilterCriteria;
                        doesNotContain = ClauseType.Like;
                        if (!((((ClauseType) nullable.GetValueOrDefault()) == doesNotContain) ? (nullable != null) : false))
                        {
                            return str;
                        }
                    }
                    return ("%" + str);
                }
                if ((condition2 == DevExpress.Xpf.Grid.AutoFilterCondition.Like) && (operator2.OperatorType == FunctionOperatorType.StartsWith))
                {
                    return str;
                }
                nullable = autoFilterCriteria;
                doesNotContain = ClauseType.EndsWith;
                if ((((ClauseType) nullable.GetValueOrDefault()) == doesNotContain) ? (nullable != null) : false)
                {
                    return str;
                }
            }
            GroupOperator groupOperator = op as GroupOperator;
            if (groupOperator != null)
            {
                return FilterClauseHelper.TryParseDayEqualsOperatorValue(groupOperator);
            }
            UnaryOperator operator4 = op as UnaryOperator;
            if (operator4 == null)
            {
                return null;
            }
            GroupOperator operand = operator4.Operand as GroupOperator;
            return ((operand == null) ? null : FilterClauseHelper.TryParseDayEqualsOperatorValue(operand));
        TR_0029:
            operator5 = op as UnaryOperator;
            if (operator5 != null)
            {
                operator2 = operator5.Operand as FunctionOperator;
            }
            goto TR_0027;
        }

        internal static string GetDisplayName(ColumnBase column, string fieldName) => 
            (column != null) ? column.HeaderCaption.ToString() : fieldName;

        internal object GetExportValueFromItem(object item)
        {
            DataViewBase view = base.View;
            if (view != null)
            {
                return view.GetExportValueFromItem(item, this);
            }
            DataViewBase local1 = view;
            return null;
        }

        public static HeaderPresenterType GetHeaderPresenterType(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (HeaderPresenterType) element.GetValue(HeaderPresenterTypeProperty);
        }

        protected internal double GetHorizontalCorrection() => 
            ((base.ColumnPosition != ColumnPosition.Left) || !base.HasLeftSibling) ? 0.0 : 1.0;

        protected virtual bool GetIsVirtualSource() => 
            (this.OwnerControl != null) && this.OwnerControl.DataProviderBase.IsVirtualSource;

        private IItemsProvider2 GetItemsProvider()
        {
            LookUpEditSettingsBase actualEditSettingsCore = this.ActualEditSettingsCore as LookUpEditSettingsBase;
            return ((actualEditSettingsCore != null) ? LookUpEditHelper.GetItemsProvider(actualEditSettingsCore) : null);
        }

        public static int GetNavigationIndex(DependencyObject dependencyObject) => 
            (int) dependencyObject.GetValue(NavigationIndexProperty);

        internal override DataControlBase GetNotifySourceControl() => 
            this.OwnerControl;

        protected override BaseColumn GetOriginationColumn() => 
            ((this.OwnerControl == null) || this.OwnerControl.IsOriginationDataControl()) ? null : ((BaseColumn) CloneDetailHelper.SafeGetDependentCollectionItem<ColumnBase>(this, this.OwnerControl.ColumnsCore, this.OwnerControl.GetOriginationDataControl().ColumnsCore));

        internal string GetPrintingTotalSummaryText(Func<DevExpress.Xpf.Grid.SummaryItemBase, object> getSummaryValue) => 
            this.GetTotalSummaryText(this.GetTotalSummaryTextValues(this.CreateSummaryData(this.GetActualPrintingTotalSummaries(), getSummaryValue)));

        internal bool GetShowAllTableValuesInCheckedFilterPopup() => 
            (this.ShowAllTableValuesInCheckedFilterPopup != DefaultBoolean.Default) ? (this.ShowAllTableValuesInCheckedFilterPopup == DefaultBoolean.True) : this.Owner.ShowAllTableValuesInCheckedFilterPopup;

        internal bool GetShowAllTableValuesInFilterPopup() => 
            (this.ShowAllTableValuesInFilterPopup != DefaultBoolean.Default) ? (this.ShowAllTableValuesInFilterPopup == DefaultBoolean.True) : this.Owner.ShowAllTableValuesInFilterPopup;

        protected internal ColumnSortMode GetSortMode() => 
            (this.SortMode != ColumnSortMode.Default) ? this.SortMode : (BaseEditHelper.GetRequireDisplayTextSorting(this.ActualEditSettings) ? ColumnSortMode.DisplayText : ColumnSortMode.Value);

        internal static string GetSummaryDisplayName(ColumnBase column, DevExpress.Xpf.Grid.SummaryItemBase summaryItem) => 
            GetDisplayName(column, summaryItem.FieldName);

        internal static string GetSummaryText(GridSummaryData data)
        {
            bool forceShowColumnName = false;
            Func<ColumnBase, DataViewBase> evaluator = <>c.<>9__699_0;
            if (<>c.<>9__699_0 == null)
            {
                Func<ColumnBase, DataViewBase> local1 = <>c.<>9__699_0;
                evaluator = <>c.<>9__699_0 = column => column.View;
            }
            data.Column.With<ColumnBase, DataViewBase>(evaluator).Do<DataViewBase>(delegate (DataViewBase view) {
                forceShowColumnName = view.ForceShowTotalSummaryColumnName;
            });
            return GetSummaryText(data, forceShowColumnName);
        }

        internal static string GetSummaryText(GridSummaryData data, bool forceShowColumnName) => 
            GetSummaryText(data, forceShowColumnName, (data.Column != null) ? data.Column.DisplayFormat : string.Empty, false);

        internal static string GetSummaryText(GridSummaryData data, string forceDisplayFormat) => 
            GetSummaryText(data, false, forceDisplayFormat, true);

        internal static string GetSummaryText(GridSummaryData data, bool forceShowColumnName, string forceDisplayFormat, bool useForceDisplayFormat) => 
            !forceShowColumnName ? data.Item.GetFooterDisplayText(CultureInfo.CurrentCulture, GetSummaryDisplayName(data.Column, data.Item), data.Value, forceDisplayFormat, useForceDisplayFormat) : data.Item.GetFooterDisplayTextWithColumnName(CultureInfo.CurrentCulture, GetSummaryDisplayName(data.Column, data.Item), data.Value, forceDisplayFormat);

        private string GetTotalSummaryText(InlineCollectionInfo info)
        {
            string textSource = info?.TextSource;
            return (!string.IsNullOrEmpty(textSource) ? textSource : " ");
        }

        private InlineCollectionInfo GetTotalSummaryTextValues(IList<GridTotalSummaryData> summaryData)
        {
            SummaryInlineInfoCreator creator = new SummaryInlineInfoCreator {
                SeparatorText = Environment.NewLine
            };
            Func<DataViewBase, Style> evaluator = <>c.<>9__695_0;
            if (<>c.<>9__695_0 == null)
            {
                Func<DataViewBase, Style> local1 = <>c.<>9__695_0;
                evaluator = <>c.<>9__695_0 = x => x.TotalSummaryElementStyle;
            }
            creator.DefaultStyle = base.View.With<DataViewBase, Style>(evaluator);
            Func<GridSummaryData, string> func2 = <>c.<>9__695_1;
            if (<>c.<>9__695_1 == null)
            {
                Func<GridSummaryData, string> local2 = <>c.<>9__695_1;
                func2 = <>c.<>9__695_1 = d => GetSummaryText(d);
            }
            creator.GetItemDisplayText = func2;
            Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> func3 = <>c.<>9__695_2;
            if (<>c.<>9__695_2 == null)
            {
                Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> local3 = <>c.<>9__695_2;
                func3 = <>c.<>9__695_2 = s => s.TotalSummaryElementStyle;
            }
            creator.GetSummaryStyle = func3;
            return creator.Create((IEnumerable<GridSummaryData>) summaryData);
        }

        protected internal virtual string GetValidationAttributesErrorText(object value, int rowHandle)
        {
            DataControlBase ownerControl = this.OwnerControl;
            if (ownerControl != null)
            {
                return ownerControl.GetValidationAttributesErrorText(value, this.FieldName, rowHandle);
            }
            DataControlBase local1 = ownerControl;
            return null;
        }

        protected object GetValue(DevExpress.Xpf.Grid.SummaryItemBase summary) => 
            this.Owner.GetTotalSummaryValue(summary);

        protected virtual bool HasSizeableColumnsFromRight()
        {
            if (this.Owner.VisibleColumns == null)
            {
                return false;
            }
            int index = this.Owner.VisibleColumns.IndexOf(this);
            for (int i = index + 1; i < this.Owner.VisibleColumns.Count; i++)
            {
                if (((this.CanReduce || this.Owner.VisibleColumns[i].CanReduce) && this.Owner.VisibleColumns[i].GetAllowResizing()) && !this.Owner.VisibleColumns[i].FixedWidth)
                {
                    return true;
                }
            }
            return ((index < (this.Owner.VisibleColumns.Count - 1)) && this.Owner.VisibleColumns[index + 1].CanReduce);
        }

        protected override void HeaderCaptionChanged()
        {
            base.HeaderCaptionChanged();
            if ((this.OwnerControl != null) && (!this.OwnerControl.IsLoading && (!this.OwnerControl.IsDeserializing && (!this.OwnerControl.DataSourceChangingLocker.IsLocked && (base.View != null)))))
            {
                UpdateRowDataDelegate updateMethod = <>c.<>9__421_0;
                if (<>c.<>9__421_0 == null)
                {
                    UpdateRowDataDelegate local1 = <>c.<>9__421_0;
                    updateMethod = <>c.<>9__421_0 = data => data.OnHeaderCaptionChanged();
                }
                base.View.UpdateRowData(updateMethod, false, false);
                base.View.UpdateFilterPanel();
            }
        }

        protected internal bool IsNameOrFieldOrCaption(string fieldName) => 
            this.FieldName == fieldName;

        private bool IsReadOnlyVirtualSource()
        {
            if (!this.GetIsVirtualSource() || AllowVirtualSourceEditing)
            {
                return false;
            }
            bool flag = false;
            if ((base.View is ITableView) && (((ITableView) base.View).EditFormShowMode != EditFormShowMode.None))
            {
                flag = true;
            }
            return ((base.View == null) || (((base.View.ShowUpdateRowButtonsCore == ShowUpdateRowButtons.Never) && !flag) || (this.AllowEditing != DefaultBoolean.True)));
        }

        private void itemsProvider_ItemsProviderChanged(object sender, ItemsProviderChangedEventArgs e)
        {
            ItemsProviderOnBusyChangedEventArgs busyChangedArgs = e as ItemsProviderOnBusyChangedEventArgs;
            if (busyChangedArgs != null)
            {
                this.OwnerControl.Do<DataControlBase>(x => x.UpdateColumnInstantFeedbackCounter(busyChangedArgs.IsBusy));
                if (this.IsFiltered && !busyChangedArgs.IsBusy)
                {
                    Action<DataViewBase> action = <>c.<>9__734_1;
                    if (<>c.<>9__734_1 == null)
                    {
                        Action<DataViewBase> local1 = <>c.<>9__734_1;
                        action = <>c.<>9__734_1 = x => x.UpdateFilterPanel();
                    }
                    base.View.Do<DataViewBase>(action);
                }
            }
        }

        private void NotifyEditSettingsChanged()
        {
            DataViewBase view = base.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                DataControlBase dataControl = view.DataControl;
                if (dataControl == null)
                {
                    DataControlBase local2 = dataControl;
                }
                else
                {
                    dataControl.NotifyEditSettingsChanged(this.FieldName);
                }
            }
        }

        private void OnActualAllowAutoFilterChanged()
        {
            this.ActualShowCriteriaInAutoFilterRowChanged();
            base.RaiseContentChanged(ActualAllowAutoFilterProperty);
        }

        private static void OnActualAllowAutoFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnActualAllowAutoFilterChanged();
        }

        private static void OnActualAllowColumnFilteringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).RaiseContentChanged(ActualAllowColumnFilteringProperty);
        }

        private static void OnActualAllowFilterEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).UpdateShowEditFilterButton(e);
        }

        private void OnActualCellStyleCahnged()
        {
            base.UpdateContentLayout();
            base.RaiseContentChanged(ActualCellStyleProperty);
        }

        private void OnActualEditSettingsChanged(BaseEditSettings newValue, BaseEditSettings oldValue)
        {
            if ((oldValue != null) && ReferenceEquals(oldValue.Parent, this))
            {
                base.RemoveLogicalChild(oldValue);
            }
            if ((newValue != null) && (newValue.Parent == null))
            {
                base.AddLogicalChild(newValue);
            }
            this.ActualEditSettingsCore = newValue;
            this.UpdateActualCellTemplateSelector();
            if (oldValue != null)
            {
                this.EditSettingsChangedEventHandler.Unsubscribe(oldValue);
            }
            if (newValue != null)
            {
                this.EditSettingsChangedEventHandler.Subscribe(newValue);
            }
            this.OnEditSettingsContentChanged();
            this.OnShowCheckBoxInHeaderChanged();
            if (base.View != null)
            {
                base.View.OnGroupRowCheckBoxFieldNameChanged();
            }
        }

        private static void OnActualEditSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = d as ColumnBase;
            if (base2 != null)
            {
                base2.OnActualEditSettingsChanged((BaseEditSettings) e.NewValue, (BaseEditSettings) e.OldValue);
            }
        }

        private void OnActualShowCheckBoxInHeaderChanged(bool ownerChanged = false)
        {
            base.RaiseContentChanged(ActualShowCheckBoxInHeaderProperty);
            this.UpdateIsChecked(ownerChanged);
            if (base.View != null)
            {
                base.View.ShowCheckBoxInHeaderColumnsChanged();
            }
        }

        private void OnAllowAutoFilterChanged()
        {
            this.UpdateActualAllowAutoFilter();
        }

        private static void OnAllowAutoFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnAllowAutoFilterChanged();
        }

        private void OnAllowCellMergeChanged(bool? oldValue)
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.UpdateColumnCellMergeCounter(oldValue, this.AllowCellMerge);
                Action<DataViewBase> action = <>c.<>9__831_0;
                if (<>c.<>9__831_0 == null)
                {
                    Action<DataViewBase> local1 = <>c.<>9__831_0;
                    action = <>c.<>9__831_0 = x => x.UpdateCellMergingPanels(false);
                }
                this.OwnerControl.DataView.Do<DataViewBase>(action);
            }
        }

        private static void OnAllowColumnFilteringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).UpdateActualAllowColumnFilterPopup();
            ((ColumnBase) d).UpdateActualAllowFilterEditor();
        }

        private static void OnAllowFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).ReInitializeFocusedColumnIfNeeded();
        }

        private void OnAllowSearchPanelChanged()
        {
            base.UpdateSearchInfo();
        }

        private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).UpdateAppearance();
        }

        private static void OnAutoFilterConditionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).UpdateAutoFilter(false);
        }

        private void OnAutoFilterCriteriaChanged()
        {
            this.UpdateActualAllowAutoFilter();
            base.RaiseContentChanged(AutoFilterCriteriaProperty);
            this.filterUpdateLocker.DoIfNotLocked(() => this.UpdateAutoFilter(false));
        }

        private static void OnAutoFilterCriteriaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnAutoFilterCriteriaChanged();
        }

        private void OnAutoFilterRowDisplayTemplateChanged()
        {
            base.RaiseContentChanged(AutoFilterRowDisplayTemplateProperty);
        }

        private static void OnAutoFilterRowDisplayTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnAutoFilterRowDisplayTemplateChanged();
        }

        private void OnAutoFilterValueChanged()
        {
            base.RaiseContentChanged(AutoFilterValueProperty);
            this.filterUpdateLocker.DoIfNotLocked(() => this.UpdateAutoFilter(true));
        }

        private static void OnAutoFilterValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnAutoFilterValueChanged();
        }

        protected internal override bool OnBandSeparatorChanged()
        {
            bool flag;
            this.SeparatorForceUpdate = false;
            ITableView view = base.View as ITableView;
            if (((view == null) || (base.ActualVisibleIndex == -1)) || ((view.UseLightweightTemplates != null) && (((UseLightweightTemplates) view.UseLightweightTemplates.Value) != UseLightweightTemplates.All)))
            {
                return base.OnBandSeparatorChanged();
            }
            double actualBandLeftSeparatorWidthCore = base.ActualBandLeftSeparatorWidthCore;
            double actualBandRightSeparatorWidthCore = base.ActualBandRightSeparatorWidthCore;
            Brush actualBandCellLeftSeparatorColorCore = base.ActualBandCellLeftSeparatorColorCore;
            Brush actualBandCellRightSeparatorColorCore = base.ActualBandCellRightSeparatorColorCore;
            if (base.ParentBand == null)
            {
                base.OnBandSeparatorChanged();
            }
            else
            {
                if ((base.ParentBand.ActualRows != null) && (base.ParentBand.ActualRows.Count > 0))
                {
                    using (List<BandRow>.Enumerator enumerator = base.ParentBand.ActualRows.GetEnumerator())
                    {
                        BandRow current;
                        goto TR_0014;
                    TR_000A:
                        this.SetLeftSeparator(current.Columns);
                    TR_0014:
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            current = enumerator.Current;
                            if ((current.Columns != null) && current.Columns.Contains(this))
                            {
                                base.OnBandSeparatorChanged();
                                if ((current.Columns != null) && (current.Columns.Count > 0))
                                {
                                    Func<ColumnBase, int> keySelector = <>c.<>9__193_0;
                                    if (<>c.<>9__193_0 == null)
                                    {
                                        Func<ColumnBase, int> local1 = <>c.<>9__193_0;
                                        keySelector = <>c.<>9__193_0 = x => x.ActualVisibleIndex;
                                    }
                                    if (this != current.Columns.MaxBy<ColumnBase, int>(keySelector))
                                    {
                                        goto TR_000A;
                                    }
                                    else
                                    {
                                        base.ActualBandRightSeparatorWidth = base.ValidateActualSeparatorWidth(base.ParentBand.ActualBandRightSeparatorWidthCore, true);
                                        base.ActualBandCellRightSeparatorColor = base.ParentBand.ActualBandCellRightSeparatorColorCore;
                                        base.ActualBandHeaderRightSeparatorColor = base.ParentBand.ActualBandHeaderRightSeparatorColorCore;
                                        base.ActualBandCellLeftSeparatorColor = base.ParentBand.ActualBandCellLeftSeparatorColorCore;
                                        Brush actualBandHeaderLeftSeparatorColorCore = base.ParentBand.ActualBandHeaderLeftSeparatorColorCore;
                                        Brush brush3 = actualBandHeaderLeftSeparatorColorCore;
                                        if (actualBandHeaderLeftSeparatorColorCore == null)
                                        {
                                            Brush local2 = actualBandHeaderLeftSeparatorColorCore;
                                            brush3 = base.ActualBandCellLeftSeparatorColorCore;
                                        }
                                        this.ActualBandHeaderLeftSeparatorColor = brush3;
                                        this.SetLeftSeparator(current.Columns);
                                    }
                                    break;
                                }
                                goto TR_000A;
                            }
                        }
                        goto TR_0002;
                    }
                }
                base.OnBandSeparatorChanged();
            }
        TR_0002:
            flag = (actualBandLeftSeparatorWidthCore != base.ActualBandLeftSeparatorWidthCore) || !(actualBandRightSeparatorWidthCore == base.ActualBandRightSeparatorWidthCore);
            if (flag || (!ReferenceEquals(actualBandCellLeftSeparatorColorCore, base.ActualBandCellLeftSeparatorColorCore) || !ReferenceEquals(actualBandCellRightSeparatorColorCore, base.ActualBandCellRightSeparatorColorCore)))
            {
                base.UpdateContentLayout();
            }
            this.SeparatorForceUpdate = flag;
            return flag;
        }

        protected internal virtual void OnCellDisplayTemplateChanged()
        {
            base.RaiseContentChanged(CellDisplayTemplateProperty);
        }

        protected override void OnCellToolTipBindingChanged()
        {
            if (!this.IsCloned)
            {
                this.CellToolTipBindingPatching();
            }
            base.RaiseContentChanged(BaseColumn.ActualCellToolTipTemplateProperty);
        }

        private static void OnColumnChooserHeaderCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).SetActualColumnChooserHeaderCaption();
        }

        protected void OnColumnFilterModeChanged()
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.DestroyFilterData();
            }
            this.ResetFilterPopup();
            base.RaiseContentChanged(ColumnFilterModeProperty);
        }

        private static void OnColumnFilterModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnColumnFilterModeChanged();
        }

        private void OnCustomShouldSerializeProperty(CustomShouldSerializePropertyEventArgs e)
        {
            if (ReferenceEquals(e.DependencyProperty, BaseColumn.ActualWidthProperty))
            {
                e.CustomShouldSerialize = true;
            }
            else if (ReferenceEquals(e.DependencyProperty, FieldNameProperty))
            {
                e.CustomShouldSerialize = true;
            }
        }

        private static void OnCustomShouldSerializeProperty(object sender, CustomShouldSerializePropertyEventArgs e)
        {
            ((ColumnBase) sender).OnCustomShouldSerializeProperty(e);
        }

        protected virtual void OnDataPropertyChanged()
        {
            if (this.ParentCollection != null)
            {
                this.ParentCollection.OnColumnsChanged();
            }
        }

        protected static void OnDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnDataPropertyChanged();
        }

        protected virtual bool OnDeserializeAllowProperty(AllowPropertyEventArgs e)
        {
            if ((this.OwnerControl != null) && ReferenceEquals(e.DependencyProperty, BaseColumn.ActualWidthProperty))
            {
                return true;
            }
            DataControlBase ownerControl = this.OwnerControl;
            if (ownerControl != null)
            {
                return ownerControl.OnDeserializeAllowProperty(e);
            }
            DataControlBase local1 = ownerControl;
            return false;
        }

        private static void OnDeserializeAllowProperty(object sender, AllowPropertyEventArgs e)
        {
            ((ColumnBase) sender).OnDeserializeAllowPropertyInternal(e);
        }

        private void OnDeserializeAllowPropertyInternal(AllowPropertyEventArgs e)
        {
            e.Allow = this.OnDeserializeAllowProperty(e);
        }

        protected virtual void OnDeserializeProperty(XtraPropertyInfoEventArgs e)
        {
            if (ReferenceEquals(e.DependencyProperty, BaseColumn.ActualWidthProperty))
            {
                if (e.Info != null)
                {
                    base.ActualWidth = Convert.ToDouble(e.Info.Value, CultureInfo.InvariantCulture);
                }
                e.Handled = true;
            }
        }

        private static void OnDeserializeProperty(object sender, XtraPropertyInfoEventArgs e)
        {
            ((ColumnBase) sender).OnDeserializeProperty(e);
        }

        protected virtual void OnDisplayMemberBindingChanged()
        {
            if (this.Binding == null)
            {
                this.SetUnboundType(UnboundColumnType.Bound);
            }
            if (!this.IsCloned)
            {
                DevExpress.Xpf.Grid.DisplayMemberBindingCalculator.ValidateBinding(this.Binding);
            }
            this.simpleBindingProcessor = new DevExpress.Xpf.Grid.SimpleBindingProcessor(this);
            this.ValidateSimpleBinding(SimpleBindingState.Binding);
            this.SetUnboundType();
            if (string.IsNullOrEmpty(this.FieldName))
            {
                this.FieldName = this.PatchBindingName(DevExpress.Xpf.Grid.DisplayMemberBindingCalculator.GetBindingName(this.Binding), 0);
            }
            else
            {
                this.OnFieldNameChanged(this.FieldName);
            }
        }

        protected virtual void OnDisplayTemplateChanged()
        {
            base.RaiseContentChanged(DisplayTemplateProperty);
        }

        private static void OnDisplayTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnDisplayTemplateChanged();
        }

        protected virtual void OnEditSettingsChanged(BaseEditSettings oldValue)
        {
            if (!this.IsCloned)
            {
                this.UpdateDependencyObjectExtensionsProperties(oldValue);
            }
            this.UpdateActualEditSettings();
            this.UpdateActualCellTemplateSelector();
            base.RaiseContentChanged(EditSettingsProperty);
            this.DesignTimeGridAdorner.UpdateDesignTimeInfo();
            this.NotifyEditSettingsChanged();
        }

        private static void OnEditSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnEditSettingsChanged((BaseEditSettings) e.OldValue);
        }

        protected void OnEditSettingsContentChanged()
        {
            this.UpdateActualHorizontalContentAlignment();
            this.ItemsProvider = this.GetItemsProvider();
            if (this.oldIIsAsyncLookup != this.IsAsyncLookup)
            {
                this.oldIIsAsyncLookup = this.IsAsyncLookup;
                base.RaiseContentChanged(FilterPopupModeProperty);
            }
        }

        protected virtual void OnFieldNameChanged(string oldValue)
        {
            this.SetHeaderCaption();
            if (this.ShouldRepopulateColumns)
            {
                this.OnUnboundChanged();
            }
            this.UpdateCellDataValues();
            this.UpdateSorting(oldValue);
            this.UpdateGrouping(oldValue);
            this.OnDataPropertyChanged();
            this.ValidateSimpleBinding(SimpleBindingState.Field);
        }

        private static void OnFieldNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnFieldNameChanged(e.OldValue as string);
        }

        private void OnFieldTypeChanged()
        {
            base.RaiseContentChanged(FieldTypeProperty);
            if ((this.FilterPopupMode != DevExpress.Xpf.Grid.FilterPopupMode.Default) && (this.FilterPopupMode != DevExpress.Xpf.Grid.FilterPopupMode.Excel))
            {
                this.UpdateActualAllowAutoFilter();
            }
            else
            {
                this.OnFilterPopupModeChanged();
            }
        }

        internal void OnFilterPopupModeChanged()
        {
            this.ResetFilterPopup();
        }

        private static void OnFilterPopupModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnFilterPopupModeChanged();
            ((ColumnBase) d).UpdateActualAllowFilterEditor();
        }

        protected override void OnFixedChanged()
        {
            base.OnFixedChanged();
            this.DesignTimeGridAdorner.OnColumnsLayoutChanged();
        }

        private static void OnGroupFieldsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = (ColumnBase) d;
            DataViewBase view = base2.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                DataControlBase dataControl = view.DataControl;
                if (dataControl == null)
                {
                    DataControlBase local2 = dataControl;
                }
                else
                {
                    dataControl.NotifyGroupFieldsChanged(base2.FieldName);
                }
            }
        }

        protected override void OnHasLeftSiblingChanged()
        {
            base.UpdateContentLayout();
            base.OnHasLeftSiblingChanged();
        }

        protected override void OnHasRightSiblingChanged()
        {
            base.UpdateContentLayout();
            base.OnHasRightSiblingChanged();
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).SetHeaderCaption();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateAutoFilterInitialized();
        }

        protected virtual void OnIsCheckedChanged()
        {
            if ((base.View != null) && !this.UpdateIsCheckedLocker.IsLocked)
            {
                this.isCheckedLocker.DoLockedAction(() => base.View.ColumnCheckedChanged(this));
            }
        }

        private void OnIsSmartChanged()
        {
            this.OwnerControl.Do<DataControlBase>(owner => owner.ApplyColumnSmartProperties(this));
        }

        protected override void OnLayoutPropertyChanged()
        {
            this.Owner.CalcColumnsLayout();
        }

        private static void OnNavigationIndexChanged(DependencyObject dObject, DependencyPropertyChangedEventArgs e)
        {
            INotifyNavigationIndexChanged changed = dObject as INotifyNavigationIndexChanged;
            if (changed != null)
            {
                changed.OnNavigationIndexChanged();
            }
        }

        protected virtual void OnOwnerChanged()
        {
            this.UpdateActualHeaderCustomizationAreaTemplateSelector();
            base.UpdateActualHeaderTemplateSelector();
            this.UpdateActualHeaderToolTipTemplate();
            this.UpdateActualCellToolTipTemplate();
            this.UpdateActualCellTemplateSelector();
            this.UpdateActualCellEditTemplateSelector();
            this.UpdateActualEditSettings();
            this.UpdateAppearance();
            this.UpdateActualHeaderImageStyle();
            this.ValidateSimpleBinding(SimpleBindingState.Data);
            this.SetUnboundType();
            this.UpdateActualAllowAutoFilter();
            this.UpdateAutoFilterInitialized();
            this.OnActualShowCheckBoxInHeaderChanged(true);
            base.UpdateActualHeaderStyle();
        }

        private void OnOwnerChanging(EventArgs e)
        {
            EventHandler ownerChanging = this.OwnerChanging;
            if (ownerChanging != null)
            {
                ownerChanging(this, e);
            }
        }

        private static void OnPredefinedFiltersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = (ColumnBase) d;
            PredefinedFilterCollection newValue = (PredefinedFilterCollection) e.NewValue;
            if (newValue == null)
            {
                PredefinedFilterCollection local1 = newValue;
            }
            else
            {
                newValue.Freeze();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, DependencyObjectExtensions.DataContextProperty))
            {
                this.UpdateDependencyObjectExtensionsProperties(null);
            }
            base.OnPropertyChanged(e);
        }

        internal void OnReadOnlyChanged()
        {
            base.RaiseContentChanged(ReadOnlyProperty);
        }

        private static void OnRoundDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = (ColumnBase) d;
            DataViewBase view = base2.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                DataControlBase dataControl = view.DataControl;
                if (dataControl == null)
                {
                    DataControlBase local2 = dataControl;
                }
                else
                {
                    dataControl.NotifyRoundDateChanged(base2.FieldName);
                }
            }
        }

        private void OnShowCheckBoxInHeaderChanged()
        {
            this.ActualShowCheckBoxInHeader = this.ShowCheckBoxInHeader && (this.ActualEditSettings is CheckEditSettings);
        }

        private void OnShowCriteriaInAutoFilterRowChanged()
        {
            base.RaiseContentChanged(ShowCriteriaInAutoFilterRowProperty);
            this.filterUpdateLocker.DoIfNotLocked(() => this.UpdateAutoFilter(false));
            this.UpdateActualAllowAutoFilter();
            this.ActualShowCriteriaInAutoFilterRowChanged();
        }

        private static void OnShowCriteriaInAutoFilterRowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnShowCriteriaInAutoFilterRowChanged();
        }

        private void OnSortFieldNameChanged()
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.ApplyColumnSortOrder(this);
            }
        }

        private void OnSortIndexChanged()
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.ApplyColumnSortIndex(this);
            }
        }

        private static void OnSortIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = d as ColumnBase;
            if (base2 != null)
            {
                base2.OnSortIndexChanged();
            }
        }

        private void OnSortOrderChanged()
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.ApplyColumnSortOrder(this);
            }
            base.RaiseContentChanged(SortOrderProperty);
        }

        private static void OnSortOrderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = d as ColumnBase;
            if (base2 != null)
            {
                base2.OnSortOrderChanged();
            }
        }

        private void OnUnboundChanged()
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.UpdatePropertySchemeController();
                this.OwnerControl.UpdateUnboundColumnAllowSorting(this);
                this.OwnerControl.OnColumnUnboundChangedPosponed();
            }
            this.UpdateActualAllowSorting();
            this.UpdateActualAllowColumnFilterPopup();
            this.UpdateActualAllowFilterEditor();
            this.UpdateActualAllowAutoFilter();
        }

        internal virtual void OnUnboundExpressionChanged()
        {
            this.OnUnboundChanged();
        }

        protected virtual void OnUnboundTypeChanged()
        {
            if ((this.Binding != null) && ((this.UnboundType == UnboundColumnType.Bound) && ((this.OwnerControl == null) || !this.OwnerControl.IsDeserializing)))
            {
                throw new ArgumentException("The UnboundType property cannot be set to UnboundColumnType.Bound if the Binding property has been specified.");
            }
            this.OnDataPropertyChanged();
            this.OnUnboundChanged();
            if (!this.IsCloned)
            {
                this.isAutoDetectedUnboundType = false;
            }
        }

        private static void OnUnboundTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnBase) d).OnUnboundTypeChanged();
        }

        protected internal abstract void OnValidation(GridRowValidationEventArgs e);
        protected override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            this.ReInitializeFocusedColumnIfNeeded();
        }

        private string PatchBindingName(string name, int uid = 0)
        {
            if ((this.ParentCollection == null) || string.IsNullOrEmpty(name))
            {
                return name;
            }
            string str = (uid > 0) ? (name + uid) : name;
            return ((this.ParentCollection[str] != null) ? this.PatchBindingName(name, uid + 1) : str);
        }

        protected internal void RaiseAllowEditingChanged()
        {
            base.RaiseContentChanged(AllowEditingProperty);
        }

        private void RebuildColumnChooserColumns()
        {
            this.Owner.RebuildColumnChooserColumns();
        }

        private void ReInitializeFocusedColumnIfNeeded()
        {
            if ((this.OwnerControl != null) && (ReferenceEquals(this.OwnerControl.CurrentColumn, this) && (!this.AllowFocus || !base.Visible)))
            {
                this.OwnerControl.ReInitializeCurrentColumn();
            }
        }

        internal void ResetFilterPopup()
        {
            this.UpdateActualAllowAutoFilter();
            if (this.columnFilterInfo != null)
            {
                this.columnFilterInfo = null;
                base.RaiseContentChanged(FilterPopupModeProperty);
            }
            this.UpdateActualAllowColumnFilterPopup();
        }

        protected internal DevExpress.Xpf.Grid.AutoFilterCondition ResolveAutoFilterCondition() => 
            !(this.ActualEditSettings is CheckEditSettings) ? ((this.AutoFilterCondition == DevExpress.Xpf.Grid.AutoFilterCondition.Default) ? ((this.ColumnFilterMode != DevExpress.Xpf.Grid.ColumnFilterMode.DisplayText) ? (((this.ActualEditSettings is DateEditSettings) || ((this.ActualEditSettings is ImageEditSettings) || ((this.ActualEditSettings is ProgressBarEditSettings) || ((this.ActualEditSettings is TrackBarEditSettings) || (this.ActualEditSettings is LookUpEditSettingsBase))))) ? DevExpress.Xpf.Grid.AutoFilterCondition.Equals : DevExpress.Xpf.Grid.AutoFilterCondition.Like) : DevExpress.Xpf.Grid.AutoFilterCondition.Like) : this.AutoFilterCondition) : DevExpress.Xpf.Grid.AutoFilterCondition.Equals;

        private void SetActualColumnChooserHeaderCaption()
        {
            object columnChooserHeaderCaption = this.ColumnChooserHeaderCaption;
            object headerCaption = columnChooserHeaderCaption;
            if (columnChooserHeaderCaption == null)
            {
                object local1 = columnChooserHeaderCaption;
                headerCaption = base.HeaderCaption;
            }
            this.ActualColumnChooserHeaderCaption = headerCaption;
        }

        internal void SetAutoFilterValue(object value)
        {
            if (this.AutoFilterValue != value)
            {
                this.filterUpdateLocker.DoLockedAction<object>(delegate {
                    object obj2;
                    this.AutoFilterValue = obj2 = value;
                    return obj2;
                });
                base.View.EnqueueImmediateAction(() => this.UpdateAutoFilter(true));
            }
        }

        internal virtual void SetGroupMerge(bool mergeWithPreviousGroup, int groupLevel)
        {
        }

        protected override void SetHeaderCaption()
        {
            object formattedHeader = base.GetFormattedHeader();
            formattedHeader ??= DevExpress.Data.Helpers.SplitStringHelper.SplitPascalCaseString(this.FieldName);
            base.SetValue(BaseColumn.HeaderCaptionPropertyKey, formattedHeader);
            this.SetActualColumnChooserHeaderCaption();
            this.RebuildColumnChooserColumns();
        }

        public static void SetHeaderPresenterType(DependencyObject element, HeaderPresenterType value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(HeaderPresenterTypeProperty, value);
        }

        private void SetLeftSeparator(IList<ColumnBase> actualColumns)
        {
            if ((actualColumns != null) && (actualColumns.Count > 0))
            {
                Func<ColumnBase, int> keySelector = <>c.<>9__191_0;
                if (<>c.<>9__191_0 == null)
                {
                    Func<ColumnBase, int> local1 = <>c.<>9__191_0;
                    keySelector = <>c.<>9__191_0 = x => x.VisibleIndex;
                }
                if (this == actualColumns.MinBy<ColumnBase, int>(keySelector))
                {
                    base.ActualBandLeftSeparatorWidth = base.ValidateActualSeparatorWidth(base.ParentBand.ActualBandLeftSeparatorWidth, false);
                    base.ActualBandCellRightSeparatorColor = base.ParentBand.ActualBandCellRightSeparatorColor;
                    base.ActualBandHeaderRightSeparatorColor = base.ParentBand.ActualBandHeaderRightSeparatorColor;
                    base.ActualBandCellLeftSeparatorColor = base.ParentBand.ActualBandCellLeftSeparatorColor;
                    Brush actualBandHeaderLeftSeparatorColor = base.ParentBand.ActualBandHeaderLeftSeparatorColor;
                    Brush actualBandCellLeftSeparatorColor = actualBandHeaderLeftSeparatorColor;
                    if (actualBandHeaderLeftSeparatorColor == null)
                    {
                        Brush local2 = actualBandHeaderLeftSeparatorColor;
                        actualBandCellLeftSeparatorColor = base.ActualBandCellLeftSeparatorColor;
                    }
                    this.ActualBandHeaderLeftSeparatorColor = actualBandCellLeftSeparatorColor;
                }
            }
        }

        public static void SetNavigationIndex(DependencyObject dependencyObject, int index)
        {
            dependencyObject.SetValue(NavigationIndexProperty, index);
        }

        protected internal virtual void SetSortInfo(ColumnSortOrder sortOrder, bool isGrouped)
        {
            if (!this.IsSortedBySummary)
            {
                this.SortOrder = sortOrder;
            }
            base.SetValue(IsSortedPropertyKey, sortOrder != ColumnSortOrder.None);
        }

        internal void SetUnboundType()
        {
            this.displayMemberBindingCalculator = null;
            if ((this.Binding != null) && (this.DisplayMemberBindingCalculator != null))
            {
                this.SetUnboundType(this.DisplayMemberBindingCalculator.GetUnboundColumnType());
            }
        }

        internal void SetUnboundType(UnboundColumnType unboundType)
        {
            if (this.isAutoDetectedUnboundType)
            {
                this.UnboundType = unboundType;
                this.isAutoDetectedUnboundType = true;
            }
        }

        [Browsable(false)]
        public bool ShouldSerializeTotalSummaries(XamlDesignerSerializationManager manager) => 
            false;

        private void SubscribeEditSettingsChanged()
        {
            object itemsSource;
            if (this.filteringUIUnsubscribeAction == null)
            {
                UnsubscribeAction filteringUIUnsubscribeAction = this.filteringUIUnsubscribeAction;
            }
            else
            {
                this.filteringUIUnsubscribeAction();
            }
            this.filteringUIUnsubscribeAction = null;
            DataControlBase ownerControl = this.OwnerControl;
            if (ownerControl != null)
            {
                itemsSource = ownerControl.ItemsSource;
            }
            else
            {
                DataControlBase local2 = ownerControl;
                itemsSource = null;
            }
            if (itemsSource != null)
            {
                this.filteringUIUnsubscribeAction = FilteringUIContextHelper.SubscribeSettings(this.EditSettings, (_, __) => this.NotifyEditSettingsChanged());
            }
        }

        protected internal void TestActualEditSettingsOnSynchronizeCurrentItem()
        {
            IItemsProviderOwner actualEditSettings = this.ActualEditSettings as IItemsProviderOwner;
            if ((actualEditSettings != null) && actualEditSettings.IsSynchronizedWithCurrentItem)
            {
                throw new NotSupportedException("The IsSynchronizedWithCurrentItem property cannot be set to True if an editor is used as an in-place editor.");
            }
        }

        private void UpdateActualAllowAutoFilter()
        {
            bool allowAutoFilter;
            if ((this.IsUnbound && this.GetIsVirtualSource()) || !this.AllowAutoFilter)
            {
                allowAutoFilter = false;
            }
            else
            {
                allowAutoFilter = FilterClauseHelper.GetAllowAutoFilter(this);
            }
            this.ActualAllowAutoFilter = allowAutoFilter;
        }

        internal void UpdateActualAllowColumnFilterPopup()
        {
            bool flag1;
            if (!this.AllowColumnFilteingCore())
            {
                flag1 = false;
            }
            else
            {
                ColumnFilterInfoBase columnFilterInfo = this.ColumnFilterInfo;
                if (columnFilterInfo != null)
                {
                    flag1 = columnFilterInfo.CanShowFilterPopup();
                }
                else
                {
                    ColumnFilterInfoBase local1 = columnFilterInfo;
                    flag1 = false;
                }
            }
            this.ActualAllowColumnFiltering = flag1;
        }

        private void UpdateActualAllowFilterEditor()
        {
            if (base.View != null)
            {
                if (base.View.ActualUseLegacyFilterEditor)
                {
                    this.ActualAllowFilterEditor = this.AllowColumnFilteingCore() && (FilterClauseHelper.GetDefaultOperation(this, true, false) != null);
                }
                else
                {
                    this.ActualAllowFilterEditor = this.AllowColumnFilteingCore();
                }
            }
        }

        protected virtual void UpdateActualAllowSorting()
        {
            this.ActualAllowSorting = this.CalcActualAllowSorting();
        }

        internal void UpdateActualCellEditTemplateSelector()
        {
            DataTemplateSelector cellEditTemplateSelector = this.CellEditTemplateSelector;
            DataTemplateSelector selector = cellEditTemplateSelector;
            if (cellEditTemplateSelector == null)
            {
                DataTemplateSelector local1 = cellEditTemplateSelector;
                selector = this.Owner.CellEditTemplateSelector;
            }
            this.ActualCellEditTemplateSelector = new ActualTemplateSelectorWrapper(selector, this.CellEditTemplate ?? this.Owner.CellEditTemplate);
        }

        internal void UpdateActualCellTemplateSelector()
        {
            // Unresolved stack state at '0000004B'
        }

        protected internal override void UpdateActualCellToolTipTemplate()
        {
            DataTemplate cellToolTipTemplate = base.CellToolTipTemplate;
            DataTemplate template2 = cellToolTipTemplate;
            if (cellToolTipTemplate == null)
            {
                DataTemplate local1 = cellToolTipTemplate;
                DataViewBase view = base.View;
                if (view != null)
                {
                    template2 = view.CellToolTipTemplate;
                }
                else
                {
                    DataViewBase local2 = view;
                    template2 = null;
                }
            }
            this.ActualCellToolTipTemplate = template2;
        }

        private void UpdateActualEditSettings()
        {
            if (this.EditSettings != null)
            {
                this.UpdateActualEditSettingsAndRaiseContentChanged(this.EditSettings);
            }
            else
            {
                BaseEditSettings settings = this.Owner.CreateDefaultEditSettings(this);
                if ((this.defaultSettings == null) || (settings.GetType() != this.defaultSettings.GetType()))
                {
                    this.defaultSettings = settings;
                }
                this.UpdateActualEditSettingsAndRaiseContentChanged(this.defaultSettings);
            }
            base.RaiseContentChanged(FilterCriteriaControlBase.ColumnProperty);
            this.SubscribeEditSettingsChanged();
        }

        private void UpdateActualEditSettingsAndRaiseContentChanged(BaseEditSettings editSettings)
        {
            if (!ReferenceEquals(this.ActualEditSettings, editSettings))
            {
                this.ActualEditSettings = editSettings;
                this.TestActualEditSettingsOnSynchronizeCurrentItem();
                base.RaiseContentChanged(null);
            }
        }

        internal void UpdateActualHeaderCustomizationAreaTemplateSelector()
        {
            DataTemplateSelector headerCustomizationAreaTemplateSelector = this.HeaderCustomizationAreaTemplateSelector;
            DataTemplateSelector selector = headerCustomizationAreaTemplateSelector;
            if (headerCustomizationAreaTemplateSelector == null)
            {
                DataTemplateSelector local1 = headerCustomizationAreaTemplateSelector;
                selector = this.Owner.ColumnHeaderCustomizationAreaTemplateSelector;
            }
            this.ActualHeaderCustomizationAreaTemplateSelector = new ActualTemplateSelectorWrapper(selector, this.HeaderCustomizationAreaTemplate ?? this.Owner.ColumnHeaderCustomizationAreaTemplate);
        }

        internal override void UpdateActualHeaderImageStyle()
        {
            base.UpdateActualHeaderImageStyle();
            base.ActualHeaderImageStyle ??= this.Owner.ColumnHeaderImageStyle;
        }

        protected internal override void UpdateActualHeaderToolTipTemplate()
        {
            DataTemplate headerToolTipTemplate = base.HeaderToolTipTemplate;
            DataTemplate columnHeaderToolTipTemplate = headerToolTipTemplate;
            if (headerToolTipTemplate == null)
            {
                DataTemplate local1 = headerToolTipTemplate;
                DataViewBase view = base.View;
                if (view != null)
                {
                    columnHeaderToolTipTemplate = view.ColumnHeaderToolTipTemplate;
                }
                else
                {
                    DataViewBase local2 = view;
                    columnHeaderToolTipTemplate = null;
                }
            }
            this.ActualHeaderToolTipTemplate = columnHeaderToolTipTemplate;
        }

        private void UpdateActualHorizontalContentAlignment()
        {
            if (this.ActualEditSettings != null)
            {
                this.ActualHorizontalContentAlignment = EditSettingsHorizontalAlignmentHelper.GetHorizontalAlignment(this.ActualEditSettings.HorizontalContentAlignment, this.Owner.GetDefaultColumnAlignment(this));
            }
        }

        internal void UpdateActualPrintProperties(DataViewBase view)
        {
            if (view != null)
            {
                Style printCellStyle = this.PrintCellStyle;
                Style style4 = printCellStyle;
                if (printCellStyle == null)
                {
                    Style local1 = printCellStyle;
                    style4 = view.PrintCellStyle;
                }
                this.ActualPrintCellStyle = style4;
                Style printColumnHeaderStyle = this.PrintColumnHeaderStyle;
                Style local7 = printColumnHeaderStyle;
                if (printColumnHeaderStyle == null)
                {
                    Style local2 = printColumnHeaderStyle;
                    ITableView evaluator = view.With<DataViewBase, ITableView>(<>c.<>9__770_0 ??= v => (v as ITableView));
                    if (<>c.<>9__770_1 == null)
                    {
                        ITableView local4 = view.With<DataViewBase, ITableView>(<>c.<>9__770_0 ??= v => (v as ITableView));
                        evaluator = (ITableView) (<>c.<>9__770_1 = tb => tb.PrintColumnHeaderStyle);
                    }
                    local7 = ((ITableView) <>c.<>9__770_1).With<ITableView, Style>(evaluator);
                }
                this.ActualPrintColumnHeaderStyle = local7;
                Style printTotalSummaryStyle = this.PrintTotalSummaryStyle;
                Style style5 = printTotalSummaryStyle;
                if (printTotalSummaryStyle == null)
                {
                    Style local5 = printTotalSummaryStyle;
                    style5 = view.PrintTotalSummaryStyle;
                }
                this.ActualPrintTotalSummaryStyle = style5;
            }
        }

        internal void UpdateActualShowValidationAttributeErrors()
        {
            this.ActualShowValidationAttributeErrors = this.CalcActualShowValidationAttributeErrors();
        }

        internal virtual void UpdateAppearance()
        {
            this.ActualCellStyle = this.Owner.GetActualCellStyle(this);
            Style autoFilterRowCellStyle = this.AutoFilterRowCellStyle;
            Style style5 = autoFilterRowCellStyle;
            if (autoFilterRowCellStyle == null)
            {
                Style local1 = autoFilterRowCellStyle;
                style5 = this.Owner.AutoFilterRowCellStyle;
            }
            this.ActualAutoFilterRowCellStyle = style5;
            Style newItemRowCellStyle = this.NewItemRowCellStyle;
            Style style6 = newItemRowCellStyle;
            if (newItemRowCellStyle == null)
            {
                Style local2 = newItemRowCellStyle;
                style6 = this.Owner.NewItemRowCellStyle;
            }
            this.ActualNewItemRowCellStyle = style6;
            Style columnHeaderContentStyle = this.ColumnHeaderContentStyle;
            Style style7 = columnHeaderContentStyle;
            if (columnHeaderContentStyle == null)
            {
                Style local3 = columnHeaderContentStyle;
                style7 = this.Owner.ColumnHeaderContentStyle;
            }
            this.ActualColumnHeaderContentStyle = style7;
            Style totalSummaryContentStyle = this.TotalSummaryContentStyle;
            Style style8 = totalSummaryContentStyle;
            if (totalSummaryContentStyle == null)
            {
                Style local4 = totalSummaryContentStyle;
                style8 = this.Owner.TotalSummaryContentStyle;
            }
            this.ActualTotalSummaryContentStyle = style8;
            this.UpdatePrintAppearance();
        }

        protected void UpdateAutoFilter(bool autoFilterValueChanged)
        {
            if (base.IsInitialized && !this.InsertLocker.IsLocked)
            {
                this.Owner.LockEditorClose = true;
                this.lockAutoFilterUpdate = true;
                DevExpress.Xpf.Grid.AutoFilterCondition condition = this.ResolveAutoFilterCondition();
                try
                {
                    if ((base.View != null) && ((this.AutoFilterValue != null) && !string.IsNullOrEmpty(this.AutoFilterValue.ToString())))
                    {
                        CriteriaOperator op = (this.AutoFilterCriteria == null) ? (this.Owner as DataViewBase).CreateAutoFilterCriteria(this.FieldName, condition, this.AutoFilterValue) : (this.Owner as DataViewBase).CreateAutoFilterCriteria(this.FieldName, condition, this.AutoFilterValue, this.AutoFilterCriteria.Value);
                        this.ApplyColumnFilter(op);
                    }
                    else if (autoFilterValueChanged)
                    {
                        this.ApplyColumnFilter(null);
                    }
                }
                finally
                {
                    this.Owner.LockEditorClose = false;
                    this.lockAutoFilterUpdate = false;
                }
            }
        }

        protected internal virtual void UpdateAutoFilterInitialized()
        {
            if ((this.OwnerControl != null) && !this.OwnerControl.IsDeserializing)
            {
                if (this.AutoFilterValue != null)
                {
                    this.UpdateAutoFilter(false);
                }
                else
                {
                    this.UpdateAutoFilterValueCore();
                }
            }
        }

        protected virtual void UpdateAutoFilterValue(CriteriaOperator op)
        {
            if (this.Owner is DataViewBase)
            {
                if (this.ActualShowCriteriaInAutoFilterRow)
                {
                    ClauseType? nullable = this.CriteriaOperatorToClauseType(op);
                    if (nullable != null)
                    {
                        this.AutoFilterCriteria = nullable;
                    }
                }
                this.AutoFilterValue = ((DataViewBase) this.Owner).GetAutoFilterValue(this, op);
            }
        }

        internal void UpdateAutoFilterValueCore()
        {
            if (base.IsInitialized && ((base.View != null) && (base.View.DataControl != null)))
            {
                bool flag;
                CriteriaOperator op = base.View.DataControl.GetColumnFilterCriteriaCore(this, out flag, false);
                this.IsFiltered = flag;
                if ((!this.lockAutoFilterUpdate || this.OwnerControl.IsFilterSortEventLocked) && !this.InsertLocker.IsLocked)
                {
                    this.filterUpdateLocker.DoLockedAction(() => this.UpdateAutoFilterValue(op));
                }
            }
        }

        protected virtual void UpdateCellDataValues()
        {
            this.Owner.UpdateCellDataValues();
        }

        internal void UpdateColumnTypeProperties(DataProviderBase dataProvider)
        {
            this.UpdateFieldType(dataProvider);
            this.UpdateActualEditSettings();
        }

        private void UpdateDependencyObjectExtensionsProperties(BaseEditSettings oldValue)
        {
            if (oldValue != null)
            {
                oldValue.ClearValue(DependencyObjectExtensions.DataContextProperty);
            }
            if (this.EditSettings != null)
            {
                this.EditSettings.SetValue(DependencyObjectExtensions.DataContextProperty, DependencyObjectExtensions.GetDataContext(this));
            }
        }

        internal void UpdateDisplayMemberBindingData()
        {
            this.ValidateSimpleBinding(SimpleBindingState.Data);
        }

        internal void UpdateEditFormViewInfo()
        {
            this.UpdateActualAllowSorting();
            this.UpdateActualAllowColumnFilterPopup();
        }

        private void UpdateEditorButtonVisibilities()
        {
            if (base.View != null)
            {
                base.View.UpdateEditorButtonVisibilities();
            }
        }

        private void UpdateFieldType(DataProviderBase dataProvider)
        {
            Type columnType = this.Owner.GetColumnType(this, dataProvider);
            Type type1 = columnType;
            if (columnType == null)
            {
                Type local1 = columnType;
                type1 = typeof(object);
            }
            this.FieldType = type1;
        }

        private void UpdateGrouping(string oldFieldName)
        {
            if (this.OwnerControl != null)
            {
                this.OwnerControl.UpdateGroupingFromInvalidGroupCache(this);
            }
            this.UpdateGroupingCore(oldFieldName);
        }

        protected virtual void UpdateGroupingCore(string oldFieldName)
        {
        }

        internal void UpdateHasBottomElement()
        {
            if ((base.ParentBand != null) && (base.ParentBand.ActualRows.Count > 0))
            {
                base.HasBottomElement = base.ParentBand.ActualRows.IndexOf(base.BandRow) != (base.ParentBand.ActualRows.Count - 1);
            }
            else
            {
                base.HasBottomElement = false;
            }
        }

        internal void UpdateHasTopElement()
        {
            bool flag = false;
            if (base.ParentBand != null)
            {
                flag = base.ParentBand.ActualRows.IndexOf(base.BandRow) > 0;
            }
            if (base.HasTopElement != flag)
            {
                base.HasTopElement = flag;
            }
            else
            {
                base.RaiseHasTopElementChanged();
            }
        }

        internal void UpdateIsChecked(bool ownerChanged = false)
        {
            if ((base.View == null) || ((base.View.DataProviderBase == null) || !base.View.DataProviderBase.IsUpdateLocked))
            {
                if (!this.ActualShowCheckBoxInHeader || (this.isCheckedLocker.IsLocked || ((base.View == null) || ((base.View.DataControl == null) || (base.View.DataControl.DataProviderBase == null)))))
                {
                    if (!this.isCheckedLocker.IsLocked)
                    {
                        this.UpdateIsCheckedLocker.DoLockedAction<bool?>(delegate {
                            bool? nullable = null;
                            this.IsChecked = nullable;
                            return nullable;
                        });
                    }
                }
                else
                {
                    bool? result = null;
                    for (int i = 0; i < base.View.DataControl.DataProviderBase.VisibleCount; i++)
                    {
                        bool? nullable = result;
                        object cellValue = base.View.DataControl.GetCellValue(base.View.DataControl.GetRowHandleByVisibleIndexCore(i), this.FieldName);
                        if (!(cellValue as bool))
                        {
                            result = null;
                            break;
                        }
                        result = new bool?((bool) cellValue);
                        if (nullable != null)
                        {
                            bool? nullable2 = nullable;
                            bool? nullable3 = result;
                            if ((nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault()) ? ((nullable2 != null) != (nullable3 != null)) : true)
                            {
                                result = null;
                                break;
                            }
                        }
                    }
                    this.UpdateIsCheckedLocker.DoLockedAction<bool?>(delegate {
                        bool? nullable;
                        this.IsChecked = nullable = result;
                        return nullable;
                    });
                }
            }
        }

        private void UpdatePrintAppearance()
        {
            this.UpdatePrintAppearanceCore();
        }

        protected virtual void UpdatePrintAppearanceCore()
        {
            this.UpdateActualPrintProperties(this.Owner as DataViewBase);
        }

        private void UpdateShowEditFilterButton(DependencyPropertyChangedEventArgs e)
        {
            this.Owner.UpdateShowEditFilterButton((bool) e.NewValue, (bool) e.OldValue);
        }

        private void UpdateShowValidationAttributeErrors()
        {
            this.UpdateActualShowValidationAttributeErrors();
            DataViewBase view = base.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                view.UpdateCellDataErrors();
            }
        }

        internal void UpdateSimpleBindingLanguage()
        {
            if (this.simpleBindingProcessor != null)
            {
                this.simpleBindingProcessor.ResetLanguage();
            }
        }

        private void UpdateSorting(string oldFieldName)
        {
            if (this.OwnerControl != null)
            {
                if (this.OwnerControl.InvalidSortCache.ContainsKey(this.FieldName))
                {
                    this.OwnerControl.UpdateSortingFromInvalidSortCache(this);
                }
                else
                {
                    this.OwnerControl.UpdateDefaultSorting();
                    this.OwnerControl.ApplyColumnSorting(this);
                    if (oldFieldName != this.FieldName)
                    {
                        this.OwnerControl.SortInfoCore.OnColumnHeaderClickRemoveSort(oldFieldName);
                    }
                }
            }
        }

        internal void UpdateTotalSummaries()
        {
            IList<DevExpress.Xpf.Grid.SummaryItemBase> actualTotalSummaries = this.GetActualTotalSummaries();
            if (this.HasTotalSummaries || actualTotalSummaries.Any<DevExpress.Xpf.Grid.SummaryItemBase>())
            {
                this.HasTotalSummaries = actualTotalSummaries.Any<DevExpress.Xpf.Grid.SummaryItemBase>();
                this.TotalSummaries = this.CreateSummaryData(actualTotalSummaries, new Func<DevExpress.Xpf.Grid.SummaryItemBase, object>(this.GetValue));
            }
            InlineCollectionInfo totalSummaryTextValues = this.GetTotalSummaryTextValues(this.TotalSummaries);
            string totalSummaryText = this.GetTotalSummaryText(totalSummaryTextValues);
            if (totalSummaryTextValues == null)
            {
                totalSummaryTextValues = new InlineCollectionInfo(totalSummaryText, null);
            }
            else
            {
                totalSummaryTextValues.TextSource = totalSummaryText;
            }
            this.TotalSummaryTextInfo = totalSummaryTextValues;
            this.TotalSummaryText = totalSummaryTextValues.TextSource;
        }

        internal override void UpdateViewInfo(bool updateDataPropertiesOnly = false)
        {
            base.UpdateViewInfo(updateDataPropertiesOnly);
            this.UpdateColumnTypeProperties(null);
            this.UpdateActualHorizontalContentAlignment();
            this.UpdateTotalSummaries();
            this.UpdateActualAllowSorting();
            if (!updateDataPropertiesOnly)
            {
                this.UpdateActualShowValidationAttributeErrors();
                this.UpdateAppearance();
                this.ResetFilterPopup();
                this.UpdateActualAllowColumnFilterPopup();
                this.UpdateActualAllowFilterEditor();
            }
        }

        private void ValidateSimpleBinding(SimpleBindingState state)
        {
            if ((this.simpleBindingProcessor != null) && !this.IsCloned)
            {
                bool isEnabled = this.simpleBindingProcessor.IsEnabled;
                this.simpleBindingProcessor.Validate(state);
                if (isEnabled != this.simpleBindingProcessor.IsEnabled)
                {
                    this.UpdateCellDataValues();
                }
            }
        }

        internal override BandBase ParentBandInternal =>
            base.ParentBand;

        internal virtual int GroupIndexCore
        {
            get => 
                -1;
            set
            {
            }
        }

        protected internal override IColumnOwnerBase ResizeOwner =>
            this.Owner;

        internal IDesignTimeAdornerBase DesignTimeGridAdorner =>
            (this.OwnerControl != null) ? this.OwnerControl.DesignTimeAdorner : EmptyDesignTimeAdornerBase.Instance;

        protected internal IColumnOwnerBase Owner
        {
            get
            {
                IColumnOwnerBase base2;
                DataViewBase viewCore;
                DataControlBase ownerControl = this.OwnerControl;
                if (ownerControl != null)
                {
                    viewCore = ownerControl.viewCore;
                }
                else
                {
                    DataControlBase local1 = ownerControl;
                    viewCore = null;
                }
                DataViewBase instance = viewCore;
                if (base2 == null)
                {
                    DataViewBase local2 = viewCore;
                    instance = (DataViewBase) EmptyColumnOwnerBase.Instance;
                }
                return instance;
            }
        }

        protected virtual IColumnCollection ParentCollection
        {
            get
            {
                DataControlBase ownerControl = this.OwnerControl;
                if (ownerControl != null)
                {
                    return ownerControl.ColumnsCore;
                }
                DataControlBase local1 = ownerControl;
                return null;
            }
        }

        internal DataControlBase OwnerControl
        {
            get => 
                this.ownerControlCore;
            set
            {
                if (!ReferenceEquals(this.ownerControlCore, value))
                {
                    this.OnOwnerChanging(new EventArgs());
                    this.ownerControlCore = value;
                    this.ChangeOwner();
                }
            }
        }

        internal bool IsUnbound =>
            this.UnboundType != UnboundColumnType.Bound;

        [Description("Gets whether error icons can be displayed within column cells that fail validation specified by the Data Annotations attribute(s). This is a dependency property.")]
        public bool ActualShowValidationAttributeErrors
        {
            get => 
                (bool) base.GetValue(ActualShowValidationAttributeErrorsProperty);
            private set => 
                base.SetValue(ActualShowValidationAttributeErrorsPropertyKey, value);
        }

        [Description("Gets or sets whether error icons are displayed within column cells that fail validation specified by the Data Annotations attribute(s). This is a dependency property."), Category("Layout"), XtraSerializableProperty]
        public DefaultBoolean ShowValidationAttributeErrors
        {
            get => 
                (DefaultBoolean) base.GetValue(ShowValidationAttributeErrorsProperty);
            set => 
                base.SetValue(ShowValidationAttributeErrorsProperty, value);
        }

        [Description("Gets whether data is sorted by the values of this column. This is a dependency property.")]
        public bool IsSorted =>
            (bool) base.GetValue(IsSortedProperty);

        [Description("Gets or sets whether the column can be focused. This is a dependency property."), Category("Options Behavior")]
        public bool AllowFocus
        {
            get => 
                (bool) base.GetValue(AllowFocusProperty);
            set => 
                base.SetValue(AllowFocusProperty, value);
        }

        [Description("Gets or sets whether the column can be focused via the TAB key."), Category("Options Behavior")]
        public bool TabStop
        {
            get => 
                (bool) base.GetValue(TabStopProperty);
            set => 
                base.SetValue(TabStopProperty, value);
        }

        [Description("Gets whether group rows that correspond to this grouping column are sorted by group summary values. This is a dependency property.")]
        public bool IsSortedBySummary
        {
            get => 
                (bool) base.GetValue(IsSortedBySummaryProperty);
            internal set => 
                base.SetValue(IsSortedBySummaryPropertyKey, value);
        }

        [Description("Gets or sets the initial sort order.")]
        public ListSortDirection DefaultSortOrder
        {
            get => 
                (ListSortDirection) base.GetValue(DefaultSortOrderProperty);
            set => 
                base.SetValue(DefaultSortOrderProperty, value);
        }

        [Description("Gets or sets allowed sorting orders that the GridControl's column supports.")]
        public DevExpress.Data.AllowedSortOrders AllowedSortOrders
        {
            get => 
                (DevExpress.Data.AllowedSortOrders) base.GetValue(AllowedSortOrdersProperty);
            set => 
                base.SetValue(AllowedSortOrdersProperty, value);
        }

        [Browsable(false)]
        public ColumnSortOrder SortOrder
        {
            get => 
                (ColumnSortOrder) base.GetValue(SortOrderProperty);
            set => 
                base.SetValue(SortOrderProperty, value);
        }

        [Browsable(false)]
        public int SortIndex
        {
            get => 
                (int) base.GetValue(SortIndexProperty);
            set => 
                base.SetValue(SortIndexProperty, value);
        }

        [Browsable(false), XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraResetProperty(ResetPropertyMode.None), GridUIProperty]
        public int GridRow
        {
            get => 
                BandBase.GetGridRow(this);
            set => 
                BandBase.SetGridRow(this, value);
        }

        [Description("Gets or sets the name of the database field assigned to this column. This is a dependency property."), DefaultValue(""), Category("Data"), XtraSerializableProperty, XtraResetProperty(ResetPropertyMode.None), GridSerializeAlwaysProperty]
        public virtual string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type FieldType
        {
            get => 
                (Type) base.GetValue(FieldTypeProperty);
            internal set => 
                base.SetValue(FieldTypePropertyKey, value);
        }

        [Description("Gets or sets the data type and binding mode of the column. This is a dependency property."), DefaultValue(0), Category("Data"), XtraSerializableProperty]
        public UnboundColumnType UnboundType
        {
            get => 
                (UnboundColumnType) base.GetValue(UnboundTypeProperty);
            set => 
                base.SetValue(UnboundTypeProperty, value);
        }

        [Description("Gets or sets an expression used to evaluate values for this column if it is not bound to a data source field."), DefaultValue(""), Category("Data"), XtraSerializableProperty, GridUIProperty]
        public string UnboundExpression
        {
            get => 
                (string) base.GetValue(UnboundExpressionProperty);
            set => 
                base.SetValue(UnboundExpressionProperty, value);
        }

        [Description("Gets or sets whether the column's values cannot be changed by an end user. This is a dependency property."), DefaultValue(false), Category("Editing"), XtraSerializableProperty]
        public bool ReadOnly
        {
            get => 
                (bool) base.GetValue(ReadOnlyProperty);
            set => 
                base.SetValue(ReadOnlyProperty, value);
        }

        [Description("Gets or sets whether end-users can edit column values. This is a dependency property."), Category("Editing"), XtraSerializableProperty]
        public DefaultBoolean AllowEditing
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowEditingProperty);
            set => 
                base.SetValue(AllowEditingProperty, value);
        }

        [Description("Gets or sets an object that contains the column editor's settings. This is a dependency property."), Category("Editing")]
        public BaseEditSettings EditSettings
        {
            get => 
                (BaseEditSettings) base.GetValue(EditSettingsProperty);
            set => 
                base.SetValue(EditSettingsProperty, value);
        }

        [Description("Gets or sets a template that displays a custom editor used to edit column values. This is a dependency property."), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ControlTemplate EditTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EditTemplateProperty);
            set => 
                base.SetValue(EditTemplateProperty, value);
        }

        [Description("Gets or sets a template that displays column values. This is a dependency property."), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ControlTemplate DisplayTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(DisplayTemplateProperty);
            set => 
                base.SetValue(DisplayTemplateProperty, value);
        }

        [Obsolete("Use the Binding property instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue((string) null)]
        public BindingBase DisplayMemberBinding
        {
            get => 
                this.Binding;
            set => 
                this.Binding = value;
        }

        [Description("Gets or sets the binding that associates the column with a property in the data source."), DefaultValue((string) null), Category("Data")]
        public BindingBase Binding
        {
            get => 
                this.displayMemberBinding;
            set
            {
                if (!ReferenceEquals(this.displayMemberBinding, value))
                {
                    this.displayMemberBinding = value;
                    this.OnDisplayMemberBindingChanged();
                }
            }
        }

        [Description("Gets or sets the style of total summary items displayed within this column. This is a dependency property."), Category("Appearance ")]
        public Style TotalSummaryContentStyle
        {
            get => 
                (Style) base.GetValue(TotalSummaryContentStyleProperty);
            set => 
                base.SetValue(TotalSummaryContentStyleProperty, value);
        }

        [Description("Gets the actual style of total summary items displayed within this column. This is a dependency property.")]
        public Style ActualTotalSummaryContentStyle
        {
            get => 
                (Style) base.GetValue(ActualTotalSummaryContentStyleProperty);
            private set => 
                base.SetValue(ActualTotalSummaryContentStylePropertyKey, value);
        }

        [Description("Gets or sets how the column's data is sorted when sorting is applied to it. This is a dependency property."), DefaultValue(0), Category("Data"), XtraSerializableProperty]
        public ColumnSortMode SortMode
        {
            get => 
                (ColumnSortMode) base.GetValue(SortModeProperty);
            set => 
                base.SetValue(SortModeProperty, value);
        }

        [Description("Gets or sets the style of cells displayed within this column. This is a dependency property."), Category("Appearance ")]
        public Style CellStyle
        {
            get => 
                (Style) base.GetValue(CellStyleProperty);
            set => 
                base.SetValue(CellStyleProperty, value);
        }

        [Description("Gets or sets the style applied to the column's data cell displayed within the Auto Filter Row. This is a dependency property."), Category("Appearance ")]
        public Style AutoFilterRowCellStyle
        {
            get => 
                (Style) base.GetValue(AutoFilterRowCellStyleProperty);
            set => 
                base.SetValue(AutoFilterRowCellStyleProperty, value);
        }

        [Description("Gets or sets the style applied to the column's data cell displayed within the New Item Row. This is a dependency property."), Category("Appearance ")]
        public Style NewItemRowCellStyle
        {
            get => 
                (Style) base.GetValue(NewItemRowCellStyleProperty);
            set => 
                base.SetValue(NewItemRowCellStyleProperty, value);
        }

        [Description("Gets or sets the style of the text within the column header. This is a dependency property."), Category("Appearance ")]
        public Style ColumnHeaderContentStyle
        {
            get => 
                (Style) base.GetValue(ColumnHeaderContentStyleProperty);
            set => 
                base.SetValue(ColumnHeaderContentStyleProperty, value);
        }

        [Description("Gets the actual style applied to column cells. This is a dependency property.")]
        public Style ActualCellStyle
        {
            get => 
                (Style) base.GetValue(ActualCellStyleProperty);
            private set => 
                base.SetValue(ActualCellStylePropertyKey, value);
        }

        [Description("Gets the actual style applied to the column's cell displayed within the Auto Filter Row.")]
        public Style ActualAutoFilterRowCellStyle
        {
            get => 
                (Style) base.GetValue(ActualAutoFilterRowCellStyleProperty);
            private set => 
                base.SetValue(ActualAutoFilterRowCellStylePropertyKey, value);
        }

        [Description("")]
        public Style ActualNewItemRowCellStyle
        {
            get => 
                (Style) base.GetValue(ActualNewItemRowCellStyleProperty);
            private set => 
                base.SetValue(ActualNewItemRowCellStylePropertyKey, value);
        }

        [Description("Gets the actual style applied to the column header's contents. This is a dependency property.")]
        public Style ActualColumnHeaderContentStyle
        {
            get => 
                (Style) base.GetValue(ActualColumnHeaderContentStyleProperty);
            private set => 
                base.SetValue(ActualColumnHeaderContentStylePropertyKey, value);
        }

        [Browsable(false)]
        public bool ActualAllowColumnFiltering
        {
            get => 
                (bool) base.GetValue(ActualAllowColumnFilteringProperty);
            private set => 
                base.SetValue(ActualAllowColumnFilteringPropertyKey, value);
        }

        [Browsable(false)]
        public bool ActualAllowFilterEditor
        {
            get => 
                (bool) base.GetValue(ActualAllowFilterEditorProperty);
            private set => 
                base.SetValue(ActualAllowFilterEditorPropertyKey, value);
        }

        [Browsable(false)]
        public bool IsFiltered
        {
            get => 
                (bool) base.GetValue(IsFilteredProperty);
            private set => 
                base.SetValue(IsFilteredPropertyKey, value);
        }

        [Description("Gets or sets the number of cells taken into account when calculating the optimal width required for the column to completely display its contents."), Category("BestFit"), XtraSerializableProperty]
        public int BestFitMaxRowCount
        {
            get => 
                (int) base.GetValue(BestFitMaxRowCountProperty);
            set => 
                base.SetValue(BestFitMaxRowCountProperty, value);
        }

        [Description("Gets or sets how the optimal width required for this column to completely display its contents, is calculated."), Category("BestFit"), XtraSerializableProperty]
        public DevExpress.Xpf.Core.BestFitMode BestFitMode
        {
            get => 
                (DevExpress.Xpf.Core.BestFitMode) base.GetValue(BestFitModeProperty);
            set => 
                base.SetValue(BestFitModeProperty, value);
        }

        [Description("Gets or sets whether an end-user can open the Expression Editor for the current unbound column, using a context menu."), Category("Data"), XtraSerializableProperty]
        public bool AllowUnboundExpressionEditor
        {
            get => 
                (bool) base.GetValue(AllowUnboundExpressionEditorProperty);
            set => 
                base.SetValue(AllowUnboundExpressionEditorProperty, value);
        }

        [Description("Gets or sets which interface elements are taken into account when calculating the column's optimal width."), Category("BestFit"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.BestFitArea BestFitArea
        {
            get => 
                (DevExpress.Xpf.Grid.BestFitArea) base.GetValue(BestFitAreaProperty);
            set => 
                base.SetValue(BestFitAreaProperty, value);
        }

        [Description("Gets whether the column is the last visible column displayed within a View. This is a dependency property.")]
        public bool IsLast
        {
            get => 
                (bool) base.GetValue(IsLastProperty);
            internal set => 
                base.SetValue(IsLastPropertyKey, value);
        }

        [Description("Gets whether the column is the first visible column displayed within a View. This is a dependency property.")]
        public bool IsFirst
        {
            get => 
                (bool) base.GetValue(IsFirstProperty);
            internal set => 
                base.SetValue(IsFirstPropertyKey, value);
        }

        [Description("Gets or sets the style applied to column cells when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintCellStyle
        {
            get => 
                (Style) base.GetValue(PrintCellStyleProperty);
            set => 
                base.SetValue(PrintCellStyleProperty, value);
        }

        [Description("Gets or sets the style applied to the column's header when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintColumnHeaderStyle
        {
            get => 
                (Style) base.GetValue(PrintColumnHeaderStyleProperty);
            set => 
                base.SetValue(PrintColumnHeaderStyleProperty, value);
        }

        [Description("Gets or sets the style of total summary items displayed within this column, when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintTotalSummaryStyle
        {
            get => 
                (Style) base.GetValue(PrintTotalSummaryStyleProperty);
            set => 
                base.SetValue(PrintTotalSummaryStyleProperty, value);
        }

        [Description("Gets the actual style of total summary items displayed within this column, when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style ActualPrintTotalSummaryStyle
        {
            get => 
                (Style) base.GetValue(ActualPrintTotalSummaryStyleProperty);
            protected set => 
                base.SetValue(ActualPrintTotalSummaryStylePropertyKey, value);
        }

        [Description("Gets the actual style applied to column cells when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style ActualPrintCellStyle
        {
            get => 
                (Style) base.GetValue(ActualPrintCellStyleProperty);
            protected set => 
                base.SetValue(ActualPrintCellStylePropertyKey, value);
        }

        [Description("Gets the actual style applied to the column header's contents when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style ActualPrintColumnHeaderStyle
        {
            get => 
                (Style) base.GetValue(ActualPrintColumnHeaderStyleProperty);
            protected set => 
                base.SetValue(ActualPrintColumnHeaderStylePropertyKey, value);
        }

        public bool CopyValueAsDisplayText
        {
            get => 
                (bool) base.GetValue(CopyValueAsDisplayTextProperty);
            set => 
                base.SetValue(CopyValueAsDisplayTextProperty, value);
        }

        [XtraSerializableProperty]
        public object Tag
        {
            get => 
                base.GetValue(FrameworkContentElement.TagProperty);
            set => 
                base.SetValue(FrameworkContentElement.TagProperty, value);
        }

        [Description("Gets or sets whether to enable the smart columns generation feature for the column. This is a dependency property."), Category("Data"), XtraSerializableProperty]
        public bool IsSmart
        {
            get => 
                (bool) base.GetValue(IsSmartProperty);
            set => 
                base.SetValue(IsSmartProperty, value);
        }

        [Description("Gets or sets whether to enable the Cell Merging feature for the column. This is a dependency property."), Category("Options Layout"), XtraSerializableProperty]
        public bool? AllowCellMerge
        {
            get => 
                (bool?) base.GetValue(AllowCellMergeProperty);
            set => 
                base.SetValue(AllowCellMergeProperty, value);
        }

        public bool? AllowConditionalFormattingMenu
        {
            get => 
                (bool?) base.GetValue(AllowConditionalFormattingMenuProperty);
            set => 
                base.SetValue(AllowConditionalFormattingMenuProperty, value);
        }

        public object EditFormCaption
        {
            get => 
                base.GetValue(EditFormCaptionProperty);
            set => 
                base.SetValue(EditFormCaptionProperty, value);
        }

        public int? EditFormColumnSpan
        {
            get => 
                (int?) base.GetValue(EditFormColumnSpanProperty);
            set => 
                base.SetValue(EditFormColumnSpanProperty, value);
        }

        public int? EditFormRowSpan
        {
            get => 
                (int?) base.GetValue(EditFormRowSpanProperty);
            set => 
                base.SetValue(EditFormRowSpanProperty, value);
        }

        public bool EditFormStartNewRow
        {
            get => 
                (bool) base.GetValue(EditFormStartNewRowProperty);
            set => 
                base.SetValue(EditFormStartNewRowProperty, value);
        }

        public bool? EditFormVisible
        {
            get => 
                (bool?) base.GetValue(EditFormVisibleProperty);
            set => 
                base.SetValue(EditFormVisibleProperty, value);
        }

        public int EditFormVisibleIndex
        {
            get => 
                (int) base.GetValue(EditFormVisibleIndexProperty);
            set => 
                base.SetValue(EditFormVisibleIndexProperty, value);
        }

        public DataTemplate EditFormTemplate
        {
            get => 
                (DataTemplate) base.GetValue(EditFormTemplateProperty);
            set => 
                base.SetValue(EditFormTemplateProperty, value);
        }

        public string SortFieldName
        {
            get => 
                (string) base.GetValue(SortFieldNameProperty);
            set => 
                base.SetValue(SortFieldNameProperty, value);
        }

        public bool ShowCheckBoxInHeader
        {
            get => 
                (bool) base.GetValue(ShowCheckBoxInHeaderProperty);
            set => 
                base.SetValue(ShowCheckBoxInHeaderProperty, value);
        }

        public bool ActualShowCheckBoxInHeader
        {
            get => 
                (bool) base.GetValue(ActualShowCheckBoxInHeaderProperty);
            protected set => 
                base.SetValue(ActualShowCheckBoxInHeaderPropertyKey, value);
        }

        public bool? IsChecked
        {
            get => 
                (bool?) base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        [Description("Provides access to column commands.")]
        public GridColumnCommands Commands { get; private set; }

        private EditSettingsChangedEventHandler<ColumnBase> EditSettingsChangedEventHandler { get; set; }

        internal ColumnFilterInfoBase ColumnFilterInfo
        {
            get
            {
                this.columnFilterInfo ??= this.CreateColumnFilterInfo();
                return this.columnFilterInfo;
            }
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code."), CloneDetailMode(CloneDetailMode.Force)]
        public double ActualDataWidth
        {
            get => 
                (double) base.GetValue(ActualDataWidthProperty);
            internal set => 
                base.SetValue(ActualDataWidthPropertyKey, value);
        }

        [Description("For internal use only."), CloneDetailMode(CloneDetailMode.Force)]
        public double ActualAdditionalRowDataWidth
        {
            get => 
                (double) base.GetValue(ActualAdditionalRowDataWidthProperty);
            internal set => 
                base.SetValue(ActualAdditionalRowDataWidthPropertyKey, value);
        }

        [Description("Gets or sets whether an end-user can sort data by the column's values. This is a dependency property."), Category("Layout"), XtraSerializableProperty]
        public DefaultBoolean AllowSorting
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowSortingProperty);
            set => 
                base.SetValue(AllowSortingProperty, value);
        }

        [Description("Gets whether an end-user can sort data by the column's values. This is a dependency property.")]
        public bool ActualAllowSorting
        {
            get => 
                (bool) base.GetValue(ActualAllowSortingProperty);
            private set => 
                base.SetValue(ActualAllowSortingPropertyKey, value);
        }

        protected bool CanReduce =>
            this.GetAllowResizing() && ((this.ActualDataWidth - base.MinWidth) > 1.0);

        internal bool IsDisplayMemberBindingEditable
        {
            get
            {
                if (this.Binding == null)
                {
                    return true;
                }
                System.Windows.Data.Binding binding = this.Binding as System.Windows.Data.Binding;
                if ((binding != null) && (binding.Mode == BindingMode.TwoWay))
                {
                    return true;
                }
                MultiBinding binding2 = this.Binding as MultiBinding;
                return ((binding2 != null) && (binding2.Mode == BindingMode.TwoWay));
            }
        }

        internal bool IsActualReadOnly =>
            this.ReadOnly || (this.IsReadOnlyVirtualSource() || (!this.IsDisplayMemberBindingEditable || ((base.View != null) && ((base.View.DataProviderBase != null) && base.View.DataProviderBase.IsColumnReadonly(this.FieldName, true)))));

        [Description("Gets or sets the template that defines the presentation of column cells. This is a dependency property."), Category("Appearance ")]
        public DataTemplate CellTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CellTemplateProperty);
            set => 
                base.SetValue(CellTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a cell template based on custom logic. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance ")]
        public DataTemplateSelector CellTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CellTemplateSelectorProperty);
            set => 
                base.SetValue(CellTemplateSelectorProperty, value);
        }

        [Description("Gets the actual template selector that chooses a cell template based on custom logic. This is a dependency property.")]
        public DataTemplateSelector ActualCellTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualCellTemplateSelectorProperty);
            private set => 
                base.SetValue(ActualCellTemplateSelectorPropertyKey, value);
        }

        [Category("Appearance ")]
        public DataTemplate CellDisplayTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CellDisplayTemplateProperty);
            set => 
                base.SetValue(CellDisplayTemplateProperty, value);
        }

        [Category("Appearance ")]
        public DataTemplateSelector CellDisplayTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CellDisplayTemplateSelectorProperty);
            set => 
                base.SetValue(CellDisplayTemplateSelectorProperty, value);
        }

        [Category("Editing")]
        public DataTemplate CellEditTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CellEditTemplateProperty);
            set => 
                base.SetValue(CellEditTemplateProperty, value);
        }

        [Category("Editing")]
        public DataTemplateSelector CellEditTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CellEditTemplateSelectorProperty);
            set => 
                base.SetValue(CellEditTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the column's header displayed within the Filter Editor. This is a dependency property."), Category("Appearance ")]
        public DataTemplate FilterEditorHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FilterEditorHeaderTemplateProperty);
            set => 
                base.SetValue(FilterEditorHeaderTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the customization area displayed within the column's header. This is a dependency property."), Category("Appearance ")]
        public DataTemplate HeaderCustomizationAreaTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderCustomizationAreaTemplateProperty);
            set => 
                base.SetValue(HeaderCustomizationAreaTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a template based on custom logic. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance ")]
        public DataTemplateSelector HeaderCustomizationAreaTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(HeaderCustomizationAreaTemplateSelectorProperty);
            set => 
                base.SetValue(HeaderCustomizationAreaTemplateSelectorProperty, value);
        }

        [Description("Gets the actual template selector that chooses a template which defines the presentation of the customization area displayed within the column's header based on custom logic. This is a dependency property.")]
        public DataTemplateSelector ActualHeaderCustomizationAreaTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualHeaderCustomizationAreaTemplateSelectorProperty);
            private set => 
                base.SetValue(ActualHeaderCustomizationAreaTemplateSelectorPropertyKey, value);
        }

        protected internal IList<DevExpress.Xpf.Grid.SummaryItemBase> TotalSummariesCore { get; set; }

        protected internal IList<DevExpress.Xpf.Grid.SummaryItemBase> GroupSummariesCore { get; set; }

        [Description("Gets whether the column displays total summaries. This is a dependency property.")]
        public bool HasTotalSummaries
        {
            get => 
                (bool) base.GetValue(HasTotalSummariesProperty);
            private set => 
                base.SetValue(HasTotalSummariesPropertyKey, value);
        }

        [Description("Gets the list of total summary items displayed within this column. This is a dependency property.")]
        public IList<GridTotalSummaryData> TotalSummaries
        {
            get => 
                (IList<GridTotalSummaryData>) base.GetValue(TotalSummariesProperty);
            private set => 
                base.SetValue(TotalSummariesPropertyKey, value);
        }

        [Description("Gets the text displayed within the summary panel's cell. This is a dependency property.")]
        public string TotalSummaryText
        {
            get => 
                (string) base.GetValue(TotalSummaryTextProperty);
            private set => 
                base.SetValue(TotalSummaryTextPropertyKey, value);
        }

        public InlineCollectionInfo TotalSummaryTextInfo
        {
            get => 
                (InlineCollectionInfo) base.GetValue(TotalSummaryTextInfoProperty);
            private set => 
                base.SetValue(TotalSummaryTextInfoPropertyKey, value);
        }

        [Category("Options Filter"), Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public object AutoFilterValue
        {
            get => 
                base.GetValue(AutoFilterValueProperty);
            set => 
                base.SetValue(AutoFilterValueProperty, value);
        }

        [Description("Gets or sets the type of the comparison operator used to create filter conditions for the current column using the Automatic Filter Row. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Xpf.Grid.AutoFilterCondition AutoFilterCondition
        {
            get => 
                (DevExpress.Xpf.Grid.AutoFilterCondition) base.GetValue(AutoFilterConditionProperty);
            set => 
                base.SetValue(AutoFilterConditionProperty, value);
        }

        [Description("Gets or sets the type of the filter criteria used to filter the grid data."), Category("Options Filter"), XtraSerializableProperty]
        public ClauseType? AutoFilterCriteria
        {
            get => 
                (ClauseType?) base.GetValue(AutoFilterCriteriaProperty);
            set => 
                base.SetValue(AutoFilterCriteriaProperty, value);
        }

        [Description("Gets or sets whether to display the criteria selector button for a column in the Automatic Filter Row."), Category("Options Filter"), XtraSerializableProperty]
        public bool? ShowCriteriaInAutoFilterRow
        {
            get => 
                (bool?) base.GetValue(ShowCriteriaInAutoFilterRowProperty);
            set => 
                base.SetValue(ShowCriteriaInAutoFilterRowProperty, value);
        }

        [Description("Gets or sets whether an end-user can filter data by the column's values using the automatic filter row. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool AllowAutoFilter
        {
            get => 
                (bool) base.GetValue(AllowAutoFilterProperty);
            set => 
                base.SetValue(AllowAutoFilterProperty, value);
        }

        [Category("Options Filter"), Browsable(false)]
        public bool ActualAllowAutoFilter
        {
            get => 
                (bool) base.GetValue(ActualAllowAutoFilterProperty);
            private set => 
                base.SetValue(ActualAllowAutoFilterPropertyKey, value);
        }

        [Description("Gets or sets whether the column's filter condition is updated as soon as an end-user modifies the text within the auto filter row's cell. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool ImmediateUpdateAutoFilter
        {
            get => 
                (bool) base.GetValue(ImmediateUpdateAutoFilterProperty);
            set => 
                base.SetValue(ImmediateUpdateAutoFilterProperty, value);
        }

        [Description("Gets or sets whether an end user can filter data by column. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public DefaultBoolean AllowColumnFiltering
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowColumnFilteringProperty);
            set => 
                base.SetValue(AllowColumnFilteringProperty, value);
        }

        [Description("Gets or sets the display mode of the column's Drop-Down Filter. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.FilterPopupMode FilterPopupMode
        {
            get => 
                (DevExpress.Xpf.Grid.FilterPopupMode) base.GetValue(FilterPopupModeProperty);
            set => 
                base.SetValue(FilterPopupModeProperty, value);
        }

        [Description("Gets or sets how column values are filtered. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ColumnFilterMode ColumnFilterMode
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnFilterMode) base.GetValue(ColumnFilterModeProperty);
            set => 
                base.SetValue(ColumnFilterModeProperty, value);
        }

        [Description("Gets or sets whether the column's filter condition is updated as soon as an end-user selects an item within the column's Drop-down Filter. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool ImmediateUpdateColumnFilter
        {
            get => 
                (bool) base.GetValue(ImmediateUpdateColumnFilterProperty);
            set => 
                base.SetValue(ImmediateUpdateColumnFilterProperty, value);
        }

        [Description("Gets or sets the maximum number of unique column values displayed within the column's filter dropdown. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public int ColumnFilterPopupMaxRecordsCount
        {
            get => 
                (int) base.GetValue(ColumnFilterPopupMaxRecordsCountProperty);
            set => 
                base.SetValue(ColumnFilterPopupMaxRecordsCountProperty, value);
        }

        [Description("Gets or sets whether the date-time column's filter dropdown has a filter item used to select records that contain null values. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool ShowEmptyDateFilter
        {
            get => 
                (bool) base.GetValue(ShowEmptyDateFilterProperty);
            set => 
                base.SetValue(ShowEmptyDateFilterProperty, value);
        }

        [Description("Gets or sets a template that displays a column's value within the Automatic Filter Row. This is a dependency property."), Category("Appearance ")]
        public ControlTemplate AutoFilterRowDisplayTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(AutoFilterRowDisplayTemplateProperty);
            set => 
                base.SetValue(AutoFilterRowDisplayTemplateProperty, value);
        }

        [Description("Gets or sets the BaseEdit.EditTemplate for an Automatic Filter Row cell editor. This is a dependency property."), Category("Appearance ")]
        public ControlTemplate AutoFilterRowEditTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(AutoFilterRowEditTemplateProperty);
            set => 
                base.SetValue(AutoFilterRowEditTemplateProperty, value);
        }

        [Description("Gets or sets whether to ignore time values when you use the Excel-style Drop-down Filter to filter by DateTime values. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool RoundDateTimeForColumnFilter
        {
            get => 
                (bool) base.GetValue(RoundDateTimeForColumnFilterProperty);
            set => 
                base.SetValue(RoundDateTimeForColumnFilterProperty, value);
        }

        [Description("Gets or sets a format that defines how to display rounded dates."), Category("Options Filter"), XtraSerializableProperty]
        public string RoundDateDisplayFormat
        {
            get => 
                (string) base.GetValue(RoundDateDisplayFormatProperty);
            set => 
                base.SetValue(RoundDateDisplayFormatProperty, value);
        }

        [Description("Gets or sets names of the fields by which filter values are grouped in the column's drop-down filter. These names should be separated by comma, semicolon, or space. The order of field names determines the hierarchy in the group."), Category("Options Filter"), XtraSerializableProperty]
        public string FilterPopupGroupFields
        {
            get => 
                (string) base.GetValue(FilterPopupGroupFieldsProperty);
            set => 
                base.SetValue(FilterPopupGroupFieldsProperty, value);
        }

        [Description("Gets or sets unary filters that the GridControl's column supports."), Category("Options Filter")]
        public DevExpress.Xpf.Grid.AllowedUnaryFilters? AllowedUnaryFilters
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedUnaryFilters?) base.GetValue(AllowedUnaryFiltersProperty);
            set => 
                base.SetValue(AllowedUnaryFiltersProperty, value);
        }

        [Description("Gets or sets binary filters that the GridControl's column supports."), Category("Options Filter")]
        public DevExpress.Xpf.Grid.AllowedBinaryFilters? AllowedBinaryFilters
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedBinaryFilters?) base.GetValue(AllowedBinaryFiltersProperty);
            set => 
                base.SetValue(AllowedBinaryFiltersProperty, value);
        }

        [Description("Gets or sets any of filters that the GridControl's column supports."), Category("Options Filter")]
        public DevExpress.Xpf.Grid.AllowedAnyOfFilters? AllowedAnyOfFilters
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedAnyOfFilters?) base.GetValue(AllowedAnyOfFiltersProperty);
            set => 
                base.SetValue(AllowedAnyOfFiltersProperty, value);
        }

        [Description("Gets or sets between filters that the GridControl's column supports."), Category("Options Filter")]
        public DevExpress.Xpf.Grid.AllowedBetweenFilters? AllowedBetweenFilters
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedBetweenFilters?) base.GetValue(AllowedBetweenFiltersProperty);
            set => 
                base.SetValue(AllowedBetweenFiltersProperty, value);
        }

        [Description("Gets or sets date-time filters that the GridControl's column supports."), Category("Options Filter")]
        public DevExpress.Xpf.Grid.AllowedDateTimeFilters? AllowedDateTimeFilters
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedDateTimeFilters?) base.GetValue(AllowedDateTimeFiltersProperty);
            set => 
                base.SetValue(AllowedDateTimeFiltersProperty, value);
        }

        [Description(""), Category("Options Filter")]
        public DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters? AllowedDataAnalysisFilters
        {
            get => 
                (DevExpress.Xpf.Core.FilteringUI.AllowedDataAnalysisFilters?) base.GetValue(AllowedDataAnalysisFiltersProperty);
            set => 
                base.SetValue(AllowedDataAnalysisFiltersProperty, value);
        }

        [Description(""), Category("Options Filter"), XtraSerializableProperty]
        public PredefinedFilterCollection PredefinedFilters
        {
            get => 
                (PredefinedFilterCollection) base.GetValue(PredefinedFiltersProperty);
            set => 
                base.SetValue(PredefinedFiltersProperty, value);
        }

        [Description("Gets or sets possible total summaries that the GridControl supports."), Category("Data")]
        public DevExpress.Xpf.Grid.AllowedTotalSummaries? AllowedTotalSummaries
        {
            get => 
                (DevExpress.Xpf.Grid.AllowedTotalSummaries?) base.GetValue(AllowedTotalSummariesProperty);
            set => 
                base.SetValue(AllowedTotalSummariesProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the column's Drop-down Filter. This is a dependency property."), Category("Options Filter")]
        public DataTemplate CustomColumnFilterPopupTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CustomColumnFilterPopupTemplateProperty);
            set => 
                base.SetValue(CustomColumnFilterPopupTemplateProperty, value);
        }

        [Description("Gets or sets whether filter dropdowns display all unique values in the View's data source. This is a dependency property."), XtraSerializableProperty, Category("Options Filter")]
        public DefaultBoolean ShowAllTableValuesInCheckedFilterPopup
        {
            get => 
                (DefaultBoolean) base.GetValue(ShowAllTableValuesInCheckedFilterPopupProperty);
            set => 
                base.SetValue(ShowAllTableValuesInCheckedFilterPopupProperty, value);
        }

        [Description("Gets or sets whether filter dropdowns display all unique values in the View's data source. This is a dependency property."), XtraSerializableProperty, Category("Options Filter")]
        public DefaultBoolean ShowAllTableValuesInFilterPopup
        {
            get => 
                (DefaultBoolean) base.GetValue(ShowAllTableValuesInFilterPopupProperty);
            set => 
                base.SetValue(ShowAllTableValuesInFilterPopupProperty, value);
        }

        [Description("Gets or sets whether to display the column in the Column Chooser. This is a dependency property."), Category("Options Column"), XtraSerializableProperty]
        public bool ShowInColumnChooser
        {
            get => 
                (bool) base.GetValue(ShowInColumnChooserProperty);
            set => 
                base.SetValue(ShowInColumnChooserProperty, value);
        }

        [Description("Gets or sets the column's caption when its header is displayed within the Column Band Chooser. This is a dependency property."), Category("Data"), TypeConverter(typeof(ObjectConverter)), XtraSerializableProperty]
        public object ColumnChooserHeaderCaption
        {
            get => 
                base.GetValue(ColumnChooserHeaderCaptionProperty);
            set => 
                base.SetValue(ColumnChooserHeaderCaptionProperty, value);
        }

        [Description("Gets the column's caption when its header is displayed within the Column Band Chooser. This is a dependency property.")]
        public object ActualColumnChooserHeaderCaption
        {
            get => 
                base.GetValue(ActualColumnChooserHeaderCaptionProperty);
            private set => 
                base.SetValue(ActualColumnChooserHeaderCaptionPropertyKey, value);
        }

        [Description("Gets or sets whether the column data is taken into account when searching via the Search Panel. This is a dependency property."), CloneDetailMode(CloneDetailMode.Skip)]
        public DefaultBoolean AllowSearchPanel
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowSearchPanelProperty);
            set => 
                base.SetValue(AllowSearchPanelProperty, value);
        }

        internal bool ActualAllowSearchPanel =>
            this.AllowSearchPanel.ToBoolean(base.Visible);

        [Description("Gets or sets whether incremental searches can be performed on the column."), CloneDetailMode(CloneDetailMode.Skip)]
        public bool AllowIncrementalSearch
        {
            get => 
                (bool) base.GetValue(AllowIncrementalSearchProperty);
            set => 
                base.SetValue(AllowIncrementalSearchProperty, value);
        }

        internal BaseEditSettings ActualEditSettingsCore { get; private set; }

        [Description("Gets the actual edit settings used by the grid to create an inplace editor for the column. This is a dependency property.")]
        public BaseEditSettings ActualEditSettings
        {
            get => 
                (BaseEditSettings) base.GetValue(ActualEditSettingsProperty);
            private set => 
                base.SetValue(ActualEditSettingsPropertyKey, value);
        }

        [DXBrowsable(false)]
        public HorizontalAlignment ActualHorizontalContentAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(ActualHorizontalContentAlignmentProperty);
            private set => 
                base.SetValue(ActualHorizontalContentAlignmentPropertyKey, value);
        }

        public bool ActualShowCriteriaInAutoFilterRow =>
            this.ActualAllowAutoFilter ? this.GetActualShowCriteriaInAutoFilterRow() : false;

        internal string DisplayFormat =>
            (this.ActualEditSettings != null) ? FormatStringConverter.GetDisplayFormat(this.ActualEditSettings.DisplayFormat) : string.Empty;

        internal bool ShouldRepopulateColumns =>
            (this.UnboundType != UnboundColumnType.Bound) || this.FieldName.Contains(".");

        internal bool HasTemplateSelector { get; private set; }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                if ((this.EditSettings == null) || !ReferenceEquals(this.EditSettings.Parent, this))
                {
                    return new object[0].GetEnumerator();
                }
                object[] objArray1 = new object[] { this.EditSettings };
                return objArray1.GetEnumerator();
            }
        }

        internal bool IsAsyncLookup =>
            (this.itemsProvider != null) && this.itemsProvider.IsAsyncServerMode;

        internal bool IsAsyncLookupBusy =>
            (this.itemsProvider != null) && this.itemsProvider.IsBusy;

        internal IItemsProvider2 ItemsProvider
        {
            get => 
                this.itemsProvider;
            set
            {
                if (!ReferenceEquals(this.itemsProvider, value))
                {
                    if (this.itemsProvider != null)
                    {
                        this.itemsProvider.ItemsProviderChanged -= new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
                        if ((this.OwnerControl != null) && this.IsAsyncLookupBusy)
                        {
                            this.OwnerControl.UpdateColumnInstantFeedbackCounter(false);
                        }
                    }
                    this.itemsProvider = value;
                    if (this.itemsProvider != null)
                    {
                        this.itemsProvider.ItemsProviderChanged += new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
                        if ((this.OwnerControl != null) && this.IsAsyncLookupBusy)
                        {
                            this.OwnerControl.UpdateColumnInstantFeedbackCounter(true);
                        }
                    }
                }
            }
        }

        string IColumnInfo.FieldName =>
            this.FieldName;

        ColumnSortOrder IColumnInfo.SortOrder =>
            this.SortOrder;

        UnboundColumnType IColumnInfo.UnboundType =>
            this.UnboundType;

        bool IColumnInfo.ReadOnly =>
            this.ReadOnly;

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            this.Owner.GetDefaultColumnAlignment(this);

        bool IDefaultEditorViewInfo.HasTextDecorations
        {
            get
            {
                Func<ITableView, FormatConditionCollection> evaluator = <>c.<>9__777_0;
                if (<>c.<>9__777_0 == null)
                {
                    Func<ITableView, FormatConditionCollection> local1 = <>c.<>9__777_0;
                    evaluator = <>c.<>9__777_0 = x => x.FormatConditions;
                }
                FormatConditionCollection conditions = (base.View as ITableView).With<ITableView, FormatConditionCollection>(evaluator);
                return (((conditions != null) && conditions.HasNonEmptyTextDecorations()) && (((ConditionalFormattingMaskHelper.GetConditionsMask(conditions.GetInfoByFieldName(this.FieldName)) | ConditionalFormattingMaskHelper.GetConditionsMask(conditions.GetInfoByFieldName(string.Empty))) & ConditionalFormatMask.TextDecorations) > ConditionalFormatMask.None));
            }
        }

        string IDataColumnInfo.Caption =>
            Convert.ToString(base.HeaderCaption);

        string IDataColumnInfo.FieldName =>
            this.FieldName;

        string IDataColumnInfo.Name =>
            base.Name;

        string IDataColumnInfo.UnboundExpression =>
            this.UnboundExpression;

        DataControllerBase IDataColumnInfo.Controller
        {
            get
            {
                DataViewBase view = base.View;
                if (view != null)
                {
                    return view.GetDataControllerForUnboundColumnsCore();
                }
                DataViewBase local1 = view;
                return null;
            }
        }

        List<IDataColumnInfo> IDataColumnInfo.Columns
        {
            get
            {
                List<IDataColumnInfo> list = new List<IDataColumnInfo>();
                if (this.ParentCollection != null)
                {
                    foreach (ColumnBase base2 in this.ParentCollection)
                    {
                        if (!ReferenceEquals(base2, this))
                        {
                            list.Add(base2);
                        }
                    }
                }
                return list;
            }
        }

        BaseEditSettings IInplaceEditorColumn.EditSettings =>
            this.ActualEditSettingsCore;

        DataTemplateSelector IInplaceEditorColumn.EditorTemplateSelector =>
            this.ActualCellTemplateSelector;

        internal DevExpress.Xpf.Grid.DisplayMemberBindingCalculator DisplayMemberBindingCalculator =>
            ((this.displayMemberBindingCalculator == null) || !ReferenceEquals(this.displayMemberBindingCalculator.GridView, base.View)) ? this.CreateDisplayMemberBindingCalculator() : this.displayMemberBindingCalculator;

        internal ISimpleBindingProcessor SimpleBindingProcessor =>
            this.simpleBindingProcessor;

        internal bool IsSimpleBindingEnabled =>
            (this.simpleBindingProcessor != null) && this.simpleBindingProcessor.IsEnabled;

        internal BindingBase ActualBinding =>
            this.IsSimpleBindingEnabled ? null : this.Binding;

        internal bool IsCloned { get; set; }

        bool IMergeWithPreviousGroup.MergeWithPreviousGroup =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnBase.<>c <>9 = new ColumnBase.<>c();
            public static Func<ColumnBase, int> <>9__191_0;
            public static Func<ColumnBase, int> <>9__193_0;
            public static Action<ColumnBase, object, EventArgs> <>9__420_1;
            public static UpdateRowDataDelegate <>9__421_0;
            public static Func<DataViewBase, Style> <>9__695_0;
            public static Func<GridSummaryData, string> <>9__695_1;
            public static Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> <>9__695_2;
            public static Func<ColumnBase, DataViewBase> <>9__699_0;
            public static Action<DataViewBase> <>9__734_1;
            public static Func<DataViewBase, ITableView> <>9__770_0;
            public static Func<ITableView, Style> <>9__770_1;
            public static Func<ITableView, FormatConditionCollection> <>9__777_0;
            public static Action<DataViewBase> <>9__831_0;
            public static Func<bool> <>9__843_1;

            internal void <.cctor>b__152_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnFieldTypeChanged();
            }

            internal object <.cctor>b__152_1(DependencyObject column, object fieldName) => 
                ((ColumnBase) column).CoerceFieldName(fieldName);

            internal void <.cctor>b__152_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellEditTemplateSelector();
            }

            internal void <.cctor>b__152_11(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ColumnBase) d).UpdateContentLayout();
            }

            internal void <.cctor>b__152_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualHeaderCustomizationAreaTemplateSelector();
            }

            internal void <.cctor>b__152_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualHeaderCustomizationAreaTemplateSelector();
            }

            internal void <.cctor>b__152_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).RaiseContentChanged(ColumnBase.ActualHeaderCustomizationAreaTemplateSelectorProperty);
            }

            internal void <.cctor>b__152_15(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ColumnBase) d).UpdateContentLayout();
            }

            internal object <.cctor>b__152_16(DependencyObject d, object baseValue) => 
                Math.Max(0.0, (double) baseValue);

            internal object <.cctor>b__152_17(DependencyObject d, object baseValue) => 
                Math.Max(0.0, (double) baseValue);

            internal void <.cctor>b__152_18(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualAllowSorting();
            }

            internal object <.cctor>b__152_19(DependencyObject d, object value) => 
                ((ColumnBase) d).CoerceAllowSorting((DefaultBoolean) value);

            internal void <.cctor>b__152_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnUnboundExpressionChanged();
            }

            internal void <.cctor>b__152_20(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ColumnBase) d).OnActualCellStyleCahnged();
            }

            internal void <.cctor>b__152_21(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).RaiseContentChanged(ColumnBase.ActualColumnHeaderContentStyleProperty);
            }

            internal void <.cctor>b__152_22(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).RaiseContentChanged(ColumnBase.IsFilteredProperty);
            }

            internal void <.cctor>b__152_23(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).RebuildColumnChooserColumns();
            }

            internal void <.cctor>b__152_24(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).RaiseContentChanged(ColumnBase.ActualColumnChooserHeaderCaptionProperty);
            }

            internal object <.cctor>b__152_25(DependencyObject d, object baseValue) => 
                DataViewBase.CoerceBestFitMaxRowCount(Convert.ToInt32(baseValue));

            internal void <.cctor>b__152_26(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateShowValidationAttributeErrors();
            }

            internal void <.cctor>b__152_27(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnAllowSearchPanelChanged();
            }

            internal void <.cctor>b__152_28(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnIsSmartChanged();
            }

            internal void <.cctor>b__152_29(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnAllowCellMergeChanged((bool?) e.OldValue);
            }

            internal void <.cctor>b__152_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnReadOnlyChanged();
            }

            internal void <.cctor>b__152_30(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnSortFieldNameChanged();
            }

            internal void <.cctor>b__152_31(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnShowCheckBoxInHeaderChanged();
            }

            internal void <.cctor>b__152_32(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnActualShowCheckBoxInHeaderChanged(false);
            }

            internal void <.cctor>b__152_33(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).OnIsCheckedChanged();
            }

            internal void <.cctor>b__152_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateEditorButtonVisibilities();
                ((ColumnBase) d).RaiseAllowEditingChanged();
            }

            internal void <.cctor>b__152_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellTemplateSelector();
            }

            internal void <.cctor>b__152_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellTemplateSelector();
            }

            internal void <.cctor>b__152_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellTemplateSelector();
            }

            internal void <.cctor>b__152_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellTemplateSelector();
            }

            internal void <.cctor>b__152_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnBase) d).UpdateActualCellEditTemplateSelector();
            }

            internal void <.ctor>b__420_1(ColumnBase owner, object o, EventArgs e)
            {
                owner.OnEditSettingsContentChanged();
            }

            internal bool <CanBeGroupedByDataControlOwner>b__843_1() => 
                false;

            internal FormatConditionCollection <DevExpress.Xpf.Editors.Settings.IDefaultEditorViewInfo.get_HasTextDecorations>b__777_0(ITableView x) => 
                x.FormatConditions;

            internal DataViewBase <GetSummaryText>b__699_0(ColumnBase column) => 
                column.View;

            internal Style <GetTotalSummaryTextValues>b__695_0(DataViewBase x) => 
                x.TotalSummaryElementStyle;

            internal string <GetTotalSummaryTextValues>b__695_1(GridSummaryData d) => 
                ColumnBase.GetSummaryText(d);

            internal Style <GetTotalSummaryTextValues>b__695_2(DevExpress.Xpf.Grid.SummaryItemBase s) => 
                s.TotalSummaryElementStyle;

            internal void <HeaderCaptionChanged>b__421_0(RowData data)
            {
                data.OnHeaderCaptionChanged();
            }

            internal void <itemsProvider_ItemsProviderChanged>b__734_1(DataViewBase x)
            {
                x.UpdateFilterPanel();
            }

            internal void <OnAllowCellMergeChanged>b__831_0(DataViewBase x)
            {
                x.UpdateCellMergingPanels(false);
            }

            internal int <OnBandSeparatorChanged>b__193_0(ColumnBase x) => 
                x.ActualVisibleIndex;

            internal int <SetLeftSeparator>b__191_0(ColumnBase x) => 
                x.VisibleIndex;

            internal ITableView <UpdateActualPrintProperties>b__770_0(DataViewBase v) => 
                v as ITableView;

            internal Style <UpdateActualPrintProperties>b__770_1(ITableView tb) => 
                tb.PrintColumnHeaderStyle;
        }
    }
}

