namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using DevExpress.Data.Summary;
    using DevExpress.Export;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils;
    using DevExpress.Utils.Extensions.Helpers;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.DragDrop.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.ExpressionEditor.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid.Automation;
    using DevExpress.Xpf.Grid.EditForm;
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Grid.Printing;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraEditors.DXErrorProvider;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Printing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public abstract class DataViewBase : Control, ILogicalOwner, IInputElement, IColumnOwnerBase, IConvertClonePropertyValue, IPrintableControl, IRightToLeftSupport, INotifyPropertyChanged
    {
        public static readonly DependencyProperty FadeSelectionOnLostFocusProperty;
        protected static readonly DependencyPropertyKey ActualFadeSelectionOnLostFocusPropertyKey;
        public static readonly DependencyProperty ActualFadeSelectionOnLostFocusProperty;
        public static readonly DependencyProperty RowAnimationKindProperty;
        public static readonly RoutedEvent RowAnimationBeginEvent;
        internal static readonly DependencyPropertyKey HasValidationErrorPropertyKey;
        public static readonly DependencyProperty HasValidationErrorProperty;
        internal static readonly DependencyPropertyKey ValidationErrorPropertyKey;
        public static readonly DependencyProperty ValidationErrorProperty;
        public static readonly DependencyProperty AllowCommitOnValidationAttributeErrorProperty;
        public static readonly DependencyProperty RowHandleProperty;
        public static readonly DependencyProperty ScrollingModeProperty;
        public static readonly DependencyProperty IsDeferredScrollingProperty;
        public static readonly DependencyProperty EditorButtonShowModeProperty;
        public static readonly DependencyProperty AllowMoveColumnToDropAreaProperty;
        public static readonly DependencyProperty ColumnChooserTemplateProperty;
        public static readonly DependencyProperty ColumnChooserColumnDisplayModeProperty;
        public static readonly DependencyProperty ExtendedColumnChooserTemplateProperty;
        protected static readonly DependencyPropertyKey ActualColumnChooserTemplatePropertyKey;
        public static readonly DependencyProperty ActualColumnChooserTemplateProperty;
        public static readonly DependencyProperty IsColumnChooserVisibleProperty;
        public static readonly DependencyProperty ColumnHeaderDragIndicatorTemplateProperty;
        internal const string ColumnHeaderDragIndicatorTemplatePropertyName = "ColumnHeaderDragIndicatorTemplate";
        private static readonly DependencyPropertyKey ActiveEditorPropertyKey;
        public static readonly DependencyProperty ActiveEditorProperty;
        public static readonly DependencyProperty EditorShowModeProperty;
        public static readonly DependencyProperty IsFocusedRowProperty;
        public static readonly DependencyProperty IsFocusedCellProperty;
        private static readonly DependencyPropertyKey IsKeyboardFocusWithinViewPropertyKey;
        public static readonly DependencyProperty IsKeyboardFocusWithinViewProperty;
        private static readonly DependencyPropertyKey IsHorizontalScrollBarVisiblePropertyKey;
        public static readonly DependencyProperty IsHorizontalScrollBarVisibleProperty;
        private static readonly DependencyPropertyKey IsTouchScrollBarsModePropertyKey;
        public static readonly DependencyProperty IsTouchScrollBarsModeProperty;
        public static readonly DependencyProperty ColumnHeaderDragIndicatorSizeProperty;
        public static readonly DependencyProperty NavigationStyleProperty;
        public static readonly DependencyProperty ScrollStepProperty;
        private static readonly DependencyPropertyKey ColumnChooserColumnsPropertyKey;
        public static readonly DependencyProperty ColumnChooserColumnsProperty;
        public static readonly DependencyProperty ShowFocusedRectangleProperty;
        public static readonly DependencyProperty FocusedCellBorderTemplateProperty;
        public static readonly DependencyProperty ClipboardCopyWithHeadersProperty;
        public static readonly DependencyProperty ClipboardCopyAllowedProperty;
        public static readonly DependencyProperty FocusedRowProperty;
        public static readonly DependencyProperty IsSynchronizedWithCurrentItemProperty;
        private static readonly DependencyPropertyKey FocusedRowDataPropertyKey;
        public static readonly DependencyProperty FocusedRowDataProperty;
        public static readonly DependencyProperty ColumnChooserFactoryProperty;
        public static readonly DependencyProperty ColumnChooserStateProperty;
        public static readonly DependencyProperty AllowSortingProperty;
        [Obsolete("Instead use the AllowColumnMoving property.")]
        public static readonly DependencyProperty AllowMovingProperty;
        public static readonly DependencyProperty AllowColumnMovingProperty;
        public static readonly DependencyProperty AllowEditingProperty;
        public static readonly DependencyProperty AllowColumnFilteringProperty;
        public static readonly DependencyProperty AllowedGroupFiltersProperty;
        public static readonly DependencyProperty AllowFilterEditorProperty;
        private static readonly DependencyPropertyKey ShowEditFilterButtonPropertyKey;
        public static readonly DependencyProperty ShowEditFilterButtonProperty;
        private static Duration defaultAnimationDuration = new Duration(TimeSpan.FromMilliseconds(350.0));
        public static readonly DependencyProperty ColumnHeaderTemplateProperty;
        public static readonly DependencyProperty ColumnHeaderTemplateSelectorProperty;
        public static readonly DependencyProperty ColumnHeaderCustomizationAreaTemplateProperty;
        public static readonly DependencyProperty ColumnHeaderCustomizationAreaTemplateSelectorProperty;
        public static readonly DependencyProperty ShowTotalSummaryProperty;
        public static readonly DependencyProperty ShowColumnHeadersProperty;
        public static readonly DependencyProperty ShowFilterPanelModeProperty;
        private static readonly DependencyPropertyKey ActualShowFilterPanelPropertyKey;
        public static readonly DependencyProperty ActualShowFilterPanelProperty;
        private static readonly DependencyPropertyKey FilterPanelTextPropertyKey;
        public static readonly DependencyProperty FilterPanelTextProperty;
        public static readonly DependencyProperty ShowValidationAttributeErrorsProperty;
        private const string FilterEditorShowOperandTypeIconPropertyName = "FilterEditorShowOperandTypeIcon";
        public static readonly DependencyProperty FilterEditorShowOperandTypeIconProperty;
        private static readonly DependencyPropertyKey IsEditingPropertyKey;
        public static readonly DependencyProperty IsEditingProperty;
        private static readonly DependencyPropertyKey IsFocusedRowModifiedPropertyKey;
        public static readonly DependencyProperty IsFocusedRowModifiedProperty;
        public static readonly DependencyProperty ColumnHeaderContentStyleProperty;
        public static readonly DependencyProperty ColumnHeaderImageStyleProperty;
        public static readonly DependencyProperty CellStyleProperty;
        public static readonly DependencyProperty TotalSummaryContentStyleProperty;
        public static readonly DependencyProperty HeaderTemplateProperty;
        public static readonly DependencyProperty FooterTemplateProperty;
        public static readonly DependencyProperty TotalSummaryItemTemplateProperty;
        public static readonly DependencyProperty TotalSummaryItemTemplateSelectorProperty;
        private static readonly DependencyPropertyKey ActualTotalSummaryItemTemplateSelectorPropertyKey;
        public static readonly DependencyProperty ActualTotalSummaryItemTemplateSelectorProperty;
        public static readonly DependencyProperty IsColumnMenuEnabledProperty;
        public static readonly DependencyProperty IsTotalSummaryMenuEnabledProperty;
        public static readonly DependencyProperty IsRowCellMenuEnabledProperty;
        public static readonly DependencyProperty ColumnHeaderToolTipTemplateProperty;
        public static readonly DependencyProperty RowOpacityAnimationDurationProperty;
        public static readonly DependencyProperty WaitIndicatorTypeProperty;
        public static readonly DependencyProperty WaitIndicatorStyleProperty;
        public static readonly DependencyProperty IsWaitIndicatorVisibleProperty;
        private static readonly DependencyPropertyKey IsWaitIndicatorVisiblePropertyKey;
        public static readonly DependencyProperty ScrollableAreaMinWidthProperty;
        private static readonly DependencyPropertyKey ScrollableAreaMinWidthPropertyKey;
        public static readonly DependencyProperty TopRowIndexProperty;
        public static readonly DependencyProperty AllowLeaveFocusOnTabProperty;
        public static readonly DependencyProperty WheelScrollLinesProperty;
        public static readonly DependencyProperty TouchScrollThresholdProperty;
        public static readonly DependencyProperty ColumnFilterPopupModeProperty;
        public static readonly DependencyProperty ImmediateUpdateRowPositionProperty;
        public static readonly RoutedEvent FilterEditorCreatedEvent;
        public static readonly RoutedEvent ShownColumnChooserEvent;
        public static readonly RoutedEvent HiddenColumnChooserEvent;
        public static readonly RoutedEvent CustomFilterDisplayTextEvent;
        public static readonly RoutedEvent BeforeLayoutRefreshEvent;
        public static readonly DependencyProperty AutoScrollOnSortingProperty;
        public static readonly RoutedEvent ShowFilterPopupEvent;
        public static readonly RoutedEvent UnboundExpressionEditorCreatedEvent;
        public static readonly RoutedEvent PastingFromClipboardEvent;
        public static readonly DependencyProperty FocusedRowHandleProperty;
        public static readonly DependencyProperty AllowScrollToFocusedRowProperty;
        public static readonly DependencyProperty CellTemplateProperty;
        public static readonly DependencyProperty CellTemplateSelectorProperty;
        public static readonly DependencyProperty CellDisplayTemplateProperty;
        public static readonly DependencyProperty CellDisplayTemplateSelectorProperty;
        public static readonly DependencyProperty CellEditTemplateProperty;
        public static readonly DependencyProperty CellEditTemplateSelectorProperty;
        public static readonly RoutedEvent FocusedColumnChangedEvent;
        public static readonly RoutedEvent FocusedRowHandleChangedEvent;
        public static readonly RoutedEvent FocusedRowChangedEvent;
        public static readonly RoutedEvent FocusedViewChangedEvent;
        public static readonly RoutedEvent ShowGridMenuEvent;
        public static readonly RoutedEvent ColumnHeaderClickEvent;
        public static readonly DependencyProperty SummariesIgnoreNullValuesProperty;
        public static readonly DependencyProperty EnterMoveNextColumnProperty;
        public static readonly DependencyProperty RuntimeLocalizationStringsProperty;
        private static readonly DependencyPropertyKey LocalizationDescriptorPropertyKey;
        public static readonly DependencyProperty LocalizationDescriptorProperty;
        public static readonly DependencyProperty ColumnChooserColumnsSortOrderComparerProperty;
        public static readonly DependencyProperty DetailHeaderContentProperty;
        public static readonly DependencyProperty ItemsSourceErrorInfoShowModeProperty;
        public static readonly DependencyProperty SelectedRowsSourceProperty;
        public static readonly DependencyProperty AllItemsSelectedProperty;
        private static readonly DependencyPropertyKey AllItemsSelectedPropertyKey;
        public static readonly DependencyProperty EnabledCheckBoxSelectorProperty;
        private static readonly DependencyPropertyKey EnabledCheckBoxSelectorPropertyKey;
        public static readonly DependencyProperty UseExtendedMouseScrollingProperty;
        public static readonly DependencyProperty EnableImmediatePostingProperty;
        public static readonly DependencyProperty AllowLeaveInvalidEditorProperty;
        public static readonly DependencyProperty PrintHeaderTemplateProperty;
        public static readonly DependencyProperty PrintCellStyleProperty;
        public static readonly DependencyProperty PrintRowIndentStyleProperty;
        public static readonly DependencyProperty PrintRowIndentWidthProperty;
        public static readonly DependencyProperty PrintSelectedRowsOnlyProperty;
        public static readonly DependencyProperty PrintTotalSummaryProperty;
        public static readonly DependencyProperty PrintFixedTotalSummaryProperty;
        public static readonly DependencyProperty PrintTotalSummaryStyleProperty;
        public static readonly DependencyProperty PrintFixedTotalSummaryStyleProperty;
        public static readonly DependencyProperty PrintFooterTemplateProperty;
        public static readonly DependencyProperty PrintFixedFooterTemplateProperty;
        public static readonly DependencyProperty DataNavigatorButtonsProperty;
        public static readonly DependencyProperty FilterRowDelayProperty;
        public static readonly DependencyProperty ClipboardCopyOptionsProperty;
        public static readonly DependencyProperty ClipboardModeProperty;
        public static readonly DependencyProperty SelectionRectangleStyleProperty;
        public static readonly DependencyProperty ShowSelectionRectangleProperty;
        public static readonly DependencyProperty TotalSummaryElementStyleProperty;
        public static readonly DependencyProperty FixedTotalSummaryElementStyleProperty;
        public static readonly DependencyProperty IncrementalSearchModeProperty;
        public static readonly DependencyProperty UseOnlyCurrentColumnInIncrementalSearchProperty;
        public static readonly DependencyProperty IncrementalSearchClearDelayProperty;
        public static readonly DependencyProperty HasErrorsProperty;
        private static readonly DependencyPropertyKey HasErrorsPropertyKey;
        public static readonly DependencyProperty ErrorsWatchModeProperty;
        public static readonly DependencyProperty CellToolTipTemplateProperty;
        public static readonly DependencyProperty AllowDragDropProperty;
        public static readonly DependencyProperty ShowDragDropHintProperty;
        public static readonly DependencyProperty ShowTargetInfoInDragDropHintProperty;
        public static readonly DependencyProperty DropMarkerTemplateProperty;
        public static readonly DependencyProperty DragDropHintTemplateProperty;
        public static readonly DependencyProperty AllowScrollingOnDragProperty;
        public static readonly DependencyProperty AutoExpandOnDragProperty;
        public static readonly DependencyProperty AutoExpandDelayOnDragProperty;
        public static readonly DependencyProperty AllowSortedDataDragDropProperty;
        public static readonly RoutedEvent GiveRecordDragFeedbackEvent;
        public static readonly RoutedEvent ContinueRecordDragEvent;
        public static readonly RoutedEvent DragRecordOverEvent;
        public static readonly RoutedEvent DropRecordEvent;
        public static readonly RoutedEvent StartRecordDragEvent;
        public static readonly RoutedEvent CompleteRecordDragDropEvent;
        public static readonly RoutedEvent GetIsEditorActivationActionEvent;
        public static readonly RoutedEvent ProcessEditorActivationActionEvent;
        public static readonly RoutedEvent GetActiveEditorNeedsKeyEvent;
        public static readonly DependencyProperty EnableSelectedRowAppearanceProperty;
        private static readonly DependencyPropertyKey ActualShowCompactPanelPropertyKey;
        public static readonly DependencyProperty ActualShowCompactPanelProperty;
        internal readonly Locker ScrollAnimationLocker = new Locker();
        internal readonly Locker ScrollIntoViewLocker = new Locker();
        internal readonly Locker CommitEditingLocker = new Locker();
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty HeaderPositionProperty;
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty;
        public static readonly DependencyProperty ActualShowPagerProperty;
        private static readonly DependencyPropertyKey ActualShowPagerPropertyKey;
        public static readonly DependencyProperty ValidatesOnNotifyDataErrorsProperty;
        public static readonly DependencyProperty ColumnSortClearModeProperty;
        protected const double DefaultPrintRowIndentWidth = 20.0;
        protected static readonly DependencyPropertyKey IsAdditionalElementScrollBarVisiblePropertyKey;
        public static readonly DependencyProperty IsAdditionalElementScrollBarVisibleProperty;
        public static readonly DependencyProperty FilterEditorDialogServiceTemplateProperty;
        public static readonly DependencyProperty FilterEditorTemplateProperty;
        public static readonly DependencyProperty UseLegacyFilterEditorProperty;
        public static readonly DependencyProperty ColumnHeaderStyleProperty;
        public static readonly DependencyProperty ShowEmptyTextProperty;
        public static readonly DependencyProperty ActualSearchPanelPositionProperty;
        private static readonly DependencyPropertyKey ActualSearchPanelPositionPropertyKey;
        public static readonly DependencyProperty AreUpdateRowButtonsShownProperty;
        private static readonly DependencyPropertyKey AreUpdateRowButtonsShownPropertyKey;
        public static readonly DependencyProperty SummaryCalculationModeProperty;
        internal bool AssignEditorSettings = true;
        private RowsClipboardController clipboardController;
        private ClipboardOptions _optionsClipboard;
        private GridViewNavigationBase navigation;
        private GridViewNavigationBase additionalRowNavigation;
        private SelectionStrategyBase selectionStrategy;
        protected internal Locker UpdateVisibleIndexesLocker = new Locker();
        protected Locker applyColumnChooserStateLocker = new Locker();
        internal Locker layoutUpdatedLocker = new Locker();
        private IColumnCollection emptyColumns;
        private IList<ColumnBase> visibleColumnsCore;
        protected readonly DevExpress.Xpf.Grid.Native.VisualDataTreeBuilder visualDataTreeBuilder;
        private SelectionAnchorCell selectionAnchorCore;
        private SelectionAnchorCell selectionOldCellCore;
        private StartPointSelectionRectangleInfo selectionRectangleAnchorCore;
        private DevExpress.Xpf.Editors.ImmediateActionsManager immediateActionsManager;
        private bool isColumnFilterOpened;
        protected internal Locker FocusedRowHandleChangedLocker = new Locker();
        private readonly DataIteratorBase dataIterator;
        private bool rowsStateDirty = true;
        private IColumnChooser actualColumnChooser;
        private DataControlBase dataControl;
        private Lazy<BarManagerMenuController> columnMenuControllerValue;
        private Lazy<BarManagerMenuController> totalSummaryMenuControllerValue;
        private Lazy<BarManagerMenuController> rowCellMenuControllerValue;
        private Lazy<BarManagerMenuController> compactModeColumnsControllerValue;
        private Lazy<BarManagerMenuController> compactModeFilterMenuControllerValue;
        private Lazy<BarManagerMenuController> compactModeMergeMenuControllerValue;
        private bool initDataControlMenuWhenCreated;
        private DataControlPopupMenu dataControlMenu;
        private readonly NamePropertyChangeListener namePropertyChangeListener;
        private Locker updateColumnsLayoutLocker = new Locker();
        private List<object> logicalChildren = new List<object>();
        internal WeakEventHandler<EventArgs, EventHandler> IsKeyboardFocusWithinViewChanged = new WeakEventHandler<EventArgs, EventHandler>();
        private Locker updateColumnsLayoutSeparatorLocker = new Locker();
        private bool actualAllowCellMerge;
        private DependencyObject currentCell;
        internal Locker EnqueueScrollIntoViewLocker = new Locker();
        private bool postponedNavigationInProgress;
        internal FloatingContainer SummaryEditorContainer;
        internal FloatingContainer FilterControlContainer;
        internal FloatingContainer UnboundExpressionEditorContainer;
        public static readonly DependencyProperty ShowFixedTotalSummaryProperty;
        private IList<GridTotalSummaryData> fixedSummariesLeft = new ObservableCollection<GridTotalSummaryData>();
        private IList<GridTotalSummaryData> fixedSummariesRight = new ObservableCollection<GridTotalSummaryData>();
        private DevExpress.Xpf.Grid.FixedSummariesHelper fixedSummariesHelper = new DevExpress.Xpf.Grid.FixedSummariesHelper();
        public static readonly DependencyProperty SearchPanelFindFilterProperty;
        public static readonly DependencyProperty SearchPanelParseModeProperty;
        public static readonly DependencyProperty SearchPanelHighlightResultsProperty;
        public static readonly DependencyProperty SearchStringProperty;
        public static readonly DependencyProperty ShowSearchPanelCloseButtonProperty;
        public static readonly DependencyProperty SearchPanelFindModeProperty;
        public static readonly DependencyProperty ShowSearchPanelMRUButtonProperty;
        public static readonly DependencyProperty SearchPanelAllowFilterProperty;
        public static readonly DependencyProperty SearchPanelCriteriaOperatorTypeProperty;
        public static readonly DependencyProperty SearchColumnsProperty;
        public static readonly DependencyProperty ShowSearchPanelFindButtonProperty;
        public static readonly DependencyProperty SearchPanelClearOnCloseProperty;
        public static readonly DependencyProperty ShowSearchPanelModeProperty;
        public static readonly DependencyProperty ActualShowSearchPanelProperty;
        private static readonly DependencyPropertyKey ActualShowSearchPanelPropertyKey;
        public static readonly DependencyProperty SearchDelayProperty;
        public static readonly DependencyProperty SearchPanelImmediateMRUPopupProperty;
        public static readonly DependencyProperty SearchPanelHorizontalAlignmentProperty;
        public static readonly DependencyProperty ActualSearchPanelHorizontalAlignmentProperty;
        private static readonly DependencyPropertyKey ActualSearchPanelHorizontalAlignmentPropertyKey;
        public static readonly DependencyProperty SearchControlProperty;
        public static readonly DependencyProperty SearchPanelNullTextProperty;
        public static readonly DependencyProperty ShowSearchPanelNavigationButtonsProperty;
        private static readonly DependencyPropertyKey ActualShowSearchPanelNavigationButtonsPropertyKey;
        public static readonly DependencyProperty ActualShowSearchPanelNavigationButtonsProperty;
        public static readonly DependencyProperty ShowSearchPanelResultInfoProperty;
        private static readonly DependencyPropertyKey ActualShowSearchPanelResultInfoPropertyKey;
        public static readonly DependencyProperty ActualShowSearchPanelResultInfoProperty;
        private bool isKeyboardFocusInSearchPanel;
        private bool postponedSearchControlFocus;
        private IList<string> searchControlMru;
        private Locker columnChooserForceDestoyLocker = new Locker();
        private bool columnChooserStateDeserialized;
        private DependencyObject previousAutomationObject;
        private IEditFormManager editFormManagerCore;
        private Border selectionRactangle;
        private readonly Locker FilterOnDeserializationLock = new Locker();
        private TableTextSearchEngine textSearchEngineRoot;
        protected bool searchResult;
        private DevExpress.Xpf.Grid.ErrorWatch _errorWatch;
        protected readonly List<ColumnBase> ShowCheckBoxInHeaderColumns = new List<ColumnBase>();
        internal Dictionary<string, ImageSource> GlyphCache = new Dictionary<string, ImageSource>();
        private bool showLoadingRowCore;
        private Locker canSelectLocker = new Locker();
        internal readonly Locker CancelRowEditLocker = new Locker();
        internal readonly Locker UpdateRowButtonsLocker = new Locker();
        private DevExpress.Xpf.Grid.Native.UpdateRowRectangleHelper _updateRowRectangleHelper;
        private System.Windows.Shapes.Path updateRowRectangle;
        public static bool DisableOptimizedModeVerification = false;
        private IDataUpdateAnimationProvider dataUpdateAnimationProviderCore;

        public event CancelRoutedEventHandler BeforeLayoutRefresh
        {
            add
            {
                base.AddHandler(BeforeLayoutRefreshEvent, value);
            }
            remove
            {
                base.RemoveHandler(BeforeLayoutRefreshEvent, value);
            }
        }

        public event CanSelectRowEventHandler CanSelectRow;

        public event CanUnselectRowEventHandler CanUnselectRow;

        public event ColumnHeaderClickEventHandler ColumnHeaderClick
        {
            add
            {
                base.AddHandler(ColumnHeaderClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ColumnHeaderClickEvent, value);
            }
        }

        public event EventHandler<CompleteRecordDragDropEventArgs> CompleteRecordDragDrop
        {
            add
            {
                base.AddHandler(CompleteRecordDragDropEvent, value);
            }
            remove
            {
                base.RemoveHandler(CompleteRecordDragDropEvent, value);
            }
        }

        public event EventHandler<ContinueRecordDragEventArgs> ContinueRecordDrag
        {
            add
            {
                base.AddHandler(ContinueRecordDragEvent, value);
            }
            remove
            {
                base.RemoveHandler(ContinueRecordDragEvent, value);
            }
        }

        public event EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> CreateRootNodeCompleted
        {
            add
            {
                this.AddCreateRootNodeCompletedEvent(value);
            }
            remove
            {
                this.RemoveCreateRootNodeCompletedEvent(value);
            }
        }

        [Category("Options Behavior")]
        public event CustomFilterDisplayTextEventHandler CustomFilterDisplayText
        {
            add
            {
                base.AddHandler(CustomFilterDisplayTextEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomFilterDisplayTextEvent, value);
            }
        }

        event RoutedEventHandler ILogicalOwner.Loaded
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

        public event EventHandler<DragRecordOverEventArgs> DragRecordOver
        {
            add
            {
                base.AddHandler(DragRecordOverEvent, value);
            }
            remove
            {
                base.RemoveHandler(DragRecordOverEvent, value);
            }
        }

        public event EventHandler<DropRecordEventArgs> DropRecord
        {
            add
            {
                base.AddHandler(DropRecordEvent, value);
            }
            remove
            {
                base.RemoveHandler(DropRecordEvent, value);
            }
        }

        internal event EventHandler<EditorInitializedEventArgs> EditorInitialized;

        [Category("Options Behavior")]
        public event FilterEditorEventHandler FilterEditorCreated
        {
            add
            {
                base.AddHandler(FilterEditorCreatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FilterEditorCreatedEvent, value);
            }
        }

        [Obsolete("Use the DataControlBase.CurrentColumnChanged event instead"), Category("Behavior")]
        public event FocusedColumnChangedEventHandler FocusedColumnChanged
        {
            add
            {
                base.AddHandler(FocusedColumnChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FocusedColumnChangedEvent, value);
            }
        }

        [Obsolete("Use the DataControlBase.CurrentItemChanged event instead"), Category("Behavior")]
        public event FocusedRowChangedEventHandler FocusedRowChanged
        {
            add
            {
                base.AddHandler(FocusedRowChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FocusedRowChangedEvent, value);
            }
        }

        [Category("Behavior")]
        public event FocusedRowHandleChangedEventHandler FocusedRowHandleChanged
        {
            add
            {
                base.AddHandler(FocusedRowHandleChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FocusedRowHandleChangedEvent, value);
            }
        }

        [Category("Behavior")]
        public event FocusedViewChangedEventHandler FocusedViewChanged
        {
            add
            {
                base.AddHandler(FocusedViewChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(FocusedViewChangedEvent, value);
            }
        }

        public event EventHandler<GetActiveEditorNeedsKeyEventArgs> GetActiveEditorNeedsKey
        {
            add
            {
                base.AddHandler(GetActiveEditorNeedsKeyEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetActiveEditorNeedsKeyEvent, value);
            }
        }

        public event EventHandler<GetIsEditorActivationActionEventArgs> GetIsEditorActivationAction
        {
            add
            {
                base.AddHandler(GetIsEditorActivationActionEvent, value);
            }
            remove
            {
                base.RemoveHandler(GetIsEditorActivationActionEvent, value);
            }
        }

        public event EventHandler<GiveRecordDragFeedbackEventArgs> GiveRecordDragFeedback
        {
            add
            {
                base.AddHandler(GiveRecordDragFeedbackEvent, value);
            }
            remove
            {
                base.RemoveHandler(GiveRecordDragFeedbackEvent, value);
            }
        }

        [Category("OptionsCustomization")]
        public event RoutedEventHandler HiddenColumnChooser
        {
            add
            {
                base.AddHandler(HiddenColumnChooserEvent, value);
            }
            remove
            {
                base.RemoveHandler(HiddenColumnChooserEvent, value);
            }
        }

        [Obsolete("Use the DataControlBase.PastingFromClipboard event"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event PastingFromClipboardEventHandler PastingFromClipboard
        {
            add
            {
                base.AddHandler(PastingFromClipboardEvent, value);
            }
            remove
            {
                base.RemoveHandler(PastingFromClipboardEvent, value);
            }
        }

        public event EventHandler<ProcessEditorActivationActionEventArgs> ProcessEditorActivationAction
        {
            add
            {
                base.AddHandler(ProcessEditorActivationActionEvent, value);
            }
            remove
            {
                base.RemoveHandler(ProcessEditorActivationActionEvent, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal event EventHandler ResizingComplete;

        public event RowAnimationEventHandler RowAnimationBegin
        {
            add
            {
                base.AddHandler(RowAnimationBeginEvent, value);
            }
            remove
            {
                base.RemoveHandler(RowAnimationBeginEvent, value);
            }
        }

        public event SearchStringToFilterCriteriaEventHandler SearchStringToFilterCriteria;

        [Category("Options Filter")]
        public event FilterPopupEventHandler ShowFilterPopup
        {
            add
            {
                base.AddHandler(ShowFilterPopupEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowFilterPopupEvent, value);
            }
        }

        public event GridMenuEventHandler ShowGridMenu
        {
            add
            {
                base.AddHandler(ShowGridMenuEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShowGridMenuEvent, value);
            }
        }

        [Category("OptionsCustomization")]
        public event RoutedEventHandler ShownColumnChooser
        {
            add
            {
                base.AddHandler(ShownColumnChooserEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShownColumnChooserEvent, value);
            }
        }

        public event EventHandler<StartRecordDragEventArgs> StartRecordDrag
        {
            add
            {
                base.AddHandler(StartRecordDragEvent, value);
            }
            remove
            {
                base.RemoveHandler(StartRecordDragEvent, value);
            }
        }

        [Category("Options Behavior")]
        public event UnboundExpressionEditorEventHandler UnboundExpressionEditorCreated
        {
            add
            {
                base.AddHandler(UnboundExpressionEditorCreatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(UnboundExpressionEditorCreatedEvent, value);
            }
        }

        static DataViewBase()
        {
            Type ownerType = typeof(DataViewBase);
            FadeSelectionOnLostFocusProperty = DependencyPropertyManager.Register("FadeSelectionOnLostFocus", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnFadeSelectionOnLostFocusChanged)));
            ActualFadeSelectionOnLostFocusPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualFadeSelectionOnLostFocus", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((DataViewBase) d).UpdateRowDataFocusWithinState()));
            ActualFadeSelectionOnLostFocusProperty = ActualFadeSelectionOnLostFocusPropertyKey.DependencyProperty;
            RowAnimationKindProperty = DependencyPropertyManager.Register("RowAnimationKind", typeof(DevExpress.Xpf.Grid.RowAnimationKind), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.RowAnimationKind.Opacity));
            RowAnimationBeginEvent = EventManager.RegisterRoutedEvent("RowAnimationBegin", RoutingStrategy.Direct, typeof(RowAnimationEventHandler), ownerType);
            RowHandleProperty = DependencyPropertyManager.RegisterAttached("RowHandle", typeof(RowHandle), ownerType, new FrameworkPropertyMetadata(null));
            ValidationErrorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ValidationError", typeof(BaseValidationError), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnValidationErrorPropertyChanged)));
            ValidationErrorProperty = ValidationErrorPropertyKey.DependencyProperty;
            HasValidationErrorPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasValidationError", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasValidationErrorProperty = HasValidationErrorPropertyKey.DependencyProperty;
            AllowCommitOnValidationAttributeErrorProperty = DependencyPropertyManager.Register("AllowCommitOnValidationAttributeError", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            ScrollingModeProperty = DependencyPropertyManager.Register("ScrollingMode", typeof(DevExpress.Xpf.Grid.ScrollingMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ScrollingMode.Smart));
            IsDeferredScrollingProperty = DependencyPropertyManager.Register("IsDeferredScrolling", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            EditorButtonShowModeProperty = DependencyPropertyManager.Register("EditorButtonShowMode", typeof(DevExpress.Xpf.Grid.EditorButtonShowMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.EditorButtonShowMode.ShowOnlyInEditor, new PropertyChangedCallback(DataViewBase.OnEditorShowModeChanged)));
            AllowMoveColumnToDropAreaProperty = DependencyPropertyManager.Register("AllowMoveColumnToDropArea", typeof(bool), ownerType, new UIPropertyMetadata(true));
            ColumnChooserColumnDisplayModeProperty = DependencyPropertyManager.Register("ColumnChooserColumnDisplayMode", typeof(DevExpress.Xpf.Grid.ColumnChooserColumnDisplayMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ColumnChooserColumnDisplayMode.ShowAllColumns, (d, e) => ((DataViewBase) d).OnColumnChooserColumnDisplayModeChanged()));
            ExtendedColumnChooserTemplateProperty = DependencyPropertyManager.Register("ExtendedColumnChooserTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateActualColumnChooserTemplate()));
            ColumnChooserTemplateProperty = DependencyPropertyManager.Register("ColumnChooserTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateActualColumnChooserTemplate()));
            ActualColumnChooserTemplatePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualColumnChooserTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ActualColumnChooserTemplateProperty = ActualColumnChooserTemplatePropertyKey.DependencyProperty;
            IsColumnChooserVisibleProperty = DependencyPropertyManager.Register("IsColumnChooserVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnIsColumnChooserVisibleChanged()));
            ColumnHeaderDragIndicatorTemplateProperty = DependencyPropertyManager.Register("ColumnHeaderDragIndicatorTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ColumnHeaderDragIndicatorSizeProperty = DependencyPropertyManager.RegisterAttached("ColumnHeaderDragIndicatorSize", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));
            ActiveEditorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActiveEditor", typeof(BaseEdit), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).OnActiveEditorChanged()));
            ActiveEditorProperty = ActiveEditorPropertyKey.DependencyProperty;
            EditorShowModeProperty = DependencyPropertyManager.Register("EditorShowMode", typeof(DevExpress.Xpf.Core.EditorShowMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Core.EditorShowMode.Default));
            IsFocusedRowProperty = DependencyPropertyManager.RegisterAttached("IsFocusedRow", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            IsFocusedCellProperty = DependencyPropertyManager.RegisterAttached("IsFocusedCell", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            IsKeyboardFocusWithinViewPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsKeyboardFocusWithinView", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnIsKeyboardFocusWithinViewChanged()));
            IsKeyboardFocusWithinViewProperty = IsKeyboardFocusWithinViewPropertyKey.DependencyProperty;
            IsHorizontalScrollBarVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsHorizontalScrollBarVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnIsHorizontalScrollBarVisibleChanged()));
            IsHorizontalScrollBarVisibleProperty = IsHorizontalScrollBarVisiblePropertyKey.DependencyProperty;
            IsTouchScrollBarsModePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsTouchScrollBarsMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsTouchScrollBarsModeProperty = IsTouchScrollBarsModePropertyKey.DependencyProperty;
            NavigationStyleProperty = DependencyPropertyManager.Register("NavigationStyle", typeof(GridViewNavigationStyle), ownerType, new FrameworkPropertyMetadata(GridViewNavigationStyle.Cell, new PropertyChangedCallback(DataViewBase.OnNavigationStyleChanged)));
            ScrollStepProperty = DependencyPropertyManager.Register("ScrollStep", typeof(int), ownerType, new FrameworkPropertyMetadata((int) 10, (d, e) => d.CoerceValue(ScrollStepProperty), new CoerceValueCallback(DataViewBase.CoerceScrollStep)));
            ColumnChooserColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ColumnChooserColumns", typeof(ReadOnlyCollection<ColumnBase>), ownerType, new FrameworkPropertyMetadata(null));
            ColumnChooserColumnsProperty = ColumnChooserColumnsPropertyKey.DependencyProperty;
            ShowFocusedRectangleProperty = DependencyPropertyManager.Register("ShowFocusedRectangle", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnShowFocusedRectangleChanged()));
            FocusedCellBorderTemplateProperty = DependencyPropertyManager.Register("FocusedCellBorderTemplate", typeof(ControlTemplate), ownerType);
            ClipboardCopyWithHeadersProperty = DependencyPropertyManager.Register("ClipboardCopyWithHeaders", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ClipboardCopyAllowedProperty = DependencyPropertyManager.Register("ClipboardCopyAllowed", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            FocusedRowProperty = DependencyPropertyManager.Register("FocusedRow", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) => ((DataViewBase) d).OnFocusedRowChanged(e.OldValue, e.NewValue)));
            IsSynchronizedWithCurrentItemProperty = DependencyPropertyManager.Register("IsSynchronizedWithCurrentItem", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((DataViewBase) d).OnIsSynchronizedWithCurrentItemChanged((bool) e.OldValue, (bool) e.NewValue)));
            FocusedRowDataPropertyKey = DependencyPropertyManager.RegisterReadOnly("FocusedRowData", typeof(RowData), ownerType, new FrameworkPropertyMetadata(null));
            FocusedRowDataProperty = FocusedRowDataPropertyKey.DependencyProperty;
            FocusedRowHandleProperty = DependencyPropertyManager.Register("FocusedRowHandle", typeof(int), ownerType, new FrameworkPropertyMetadata(-2147483648, new PropertyChangedCallback(DataViewBase.OnFocusedRowHandleChanged), new CoerceValueCallback(DataViewBase.CoerceFocusedRowHandle)));
            AllowScrollToFocusedRowProperty = DependencyPropertyManager.Register("AllowScrollToFocusedRow", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ColumnChooserFactoryProperty = DependencyPropertyManager.Register("ColumnChooserFactory", typeof(IColumnChooserFactory), ownerType, new FrameworkPropertyMetadata(DefaultColumnChooserFactory.Instance, (d, e) => ((DataViewBase) d).OnColumnChooserFactoryChanged(), (CoerceValueCallback) ((d, baseValue) => ((DataViewBase) d).CoerceColumnChooserFactory((IColumnChooserFactory) baseValue))));
            ColumnChooserStateProperty = DependencyPropertyManager.Register("ColumnChooserState", typeof(IColumnChooserState), ownerType, new UIPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnColumnChooserStateChanged)));
            CellTemplateProperty = DependencyPropertyManager.Register("CellTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector()));
            CellTemplateSelectorProperty = DependencyPropertyManager.Register("CellTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector()));
            CellDisplayTemplateProperty = DependencyPropertyManager.Register("CellDisplayTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector()));
            CellDisplayTemplateSelectorProperty = DependencyPropertyManager.Register("CellDisplayTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector()));
            CellEditTemplateProperty = DependencyPropertyManager.Register("CellEditTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ColumnBase> updateColumnDelegate = <>c.<>9__214_19;
                if (<>c.<>9__214_19 == null)
                {
                    Action<ColumnBase> local1 = <>c.<>9__214_19;
                    updateColumnDelegate = <>c.<>9__214_19 = x => x.UpdateActualCellEditTemplateSelector();
                }
                ((DataViewBase) d).UpdateColumns(updateColumnDelegate);
            }));
            CellEditTemplateSelectorProperty = DependencyPropertyManager.Register("CellEditTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ColumnBase> updateColumnDelegate = <>c.<>9__214_21;
                if (<>c.<>9__214_21 == null)
                {
                    Action<ColumnBase> local1 = <>c.<>9__214_21;
                    updateColumnDelegate = <>c.<>9__214_21 = x => x.UpdateActualCellEditTemplateSelector();
                }
                ((DataViewBase) d).UpdateColumns(updateColumnDelegate);
            }));
            AllowSortingProperty = DependencyPropertyManager.Register("AllowSorting", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));
            AllowMovingProperty = DependencyPropertyManager.Register("AllowMoving", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((DataViewBase) d).AllowColumnMoving = (bool) e.NewValue));
            AllowColumnMovingProperty = DependencyPropertyManager.Register("AllowColumnMoving", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));
            AllowEditingProperty = DependencyPropertyManager.Register("AllowEditing", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((DataViewBase) d).UpdateEditorButtonVisibilities();
                ((DataViewBase) d).RaiseColumnAllowEditingChanged();
            }));
            AllowColumnFilteringProperty = DependencyPropertyManager.Register("AllowColumnFiltering", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));
            AllowedGroupFiltersProperty = DependencyPropertyManager.Register("AllowedGroupFilters", typeof(DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters?), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));
            AllowFilterEditorProperty = DependencyPropertyManager.Register("AllowFilterEditor", typeof(DefaultBoolean), ownerType, new FrameworkPropertyMetadata(DefaultBoolean.Default, (d, e) => ((DataViewBase) d).AllowFilterEditorChanged()));
            ShowEditFilterButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ShowEditFilterButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowEditFilterButtonProperty = ShowEditFilterButtonPropertyKey.DependencyProperty;
            ColumnHeaderTemplateProperty = DependencyPropertyManager.Register("ColumnHeaderTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualHeaderTemplateSelector()));
            ColumnHeaderTemplateSelectorProperty = DependencyPropertyManager.Register("ColumnHeaderTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualHeaderTemplateSelector()));
            ColumnHeaderCustomizationAreaTemplateProperty = DependencyPropertyManager.Register("ColumnHeaderCustomizationAreaTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualHeaderCustomizationAreaTemplateSelector()));
            ColumnHeaderCustomizationAreaTemplateSelectorProperty = DependencyPropertyManager.Register("ColumnHeaderCustomizationAreaTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnsActualHeaderCustomizationAreaTemplateSelector()));
            ShowTotalSummaryProperty = DependencyPropertyManager.Register("ShowTotalSummary", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).OnShowTotalSummaryChanged()));
            ShowColumnHeadersProperty = DependencyPropertyManager.Register("ShowColumnHeaders", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DataViewBase) d).InvalidateParentTree()));
            ShowFilterPanelModeProperty = DependencyPropertyManager.Register("ShowFilterPanelMode", typeof(DevExpress.Xpf.Grid.ShowFilterPanelMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ShowFilterPanelMode.Default, (d, e) => ((DataViewBase) d).UpdateFilterPanel()));
            ActualShowFilterPanelPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowFilterPanel", typeof(bool), ownerType, new PropertyMetadata(false));
            ActualShowFilterPanelProperty = ActualShowFilterPanelPropertyKey.DependencyProperty;
            FilterPanelTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("FilterPanelText", typeof(string), ownerType, new PropertyMetadata(string.Empty));
            FilterPanelTextProperty = FilterPanelTextPropertyKey.DependencyProperty;
            ShowValidationAttributeErrorsProperty = DependencyPropertyManager.Register("ShowValidationAttributeErrors", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnUpdateShowValidationAttributeError)));
            FilterEditorShowOperandTypeIconProperty = DependencyPropertyManager.Register("FilterEditorShowOperandTypeIcon", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsEditingPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsEditing", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnIsEditingChanged((bool) e.NewValue)));
            IsEditingProperty = IsEditingPropertyKey.DependencyProperty;
            IsFocusedRowModifiedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFocusedRowModified", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsFocusedRowModifiedProperty = IsFocusedRowModifiedPropertyKey.DependencyProperty;
            TotalSummaryItemTemplateProperty = DependencyPropertyManager.Register("TotalSummaryItemTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateActualTotalSummaryItemTemplateSelector()));
            TotalSummaryItemTemplateSelectorProperty = DependencyPropertyManager.Register("TotalSummaryItemTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateActualTotalSummaryItemTemplateSelector()));
            ActualTotalSummaryItemTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualTotalSummaryItemTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null));
            ActualTotalSummaryItemTemplateSelectorProperty = ActualTotalSummaryItemTemplateSelectorPropertyKey.DependencyProperty;
            HeaderTemplateProperty = DependencyPropertyManager.Register("HeaderTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
            FooterTemplateProperty = DependencyPropertyManager.Register("FooterTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ColumnHeaderContentStyleProperty = DependencyPropertyManager.Register("ColumnHeaderContentStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            ColumnHeaderImageStyleProperty = DependencyPropertyManager.Register("ColumnHeaderImageStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnHeaderImageStyle()));
            CellStyleProperty = DependencyPropertyManager.Register("CellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            TotalSummaryContentStyleProperty = DependencyPropertyManager.Register("TotalSummaryContentStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            RuntimeLocalizationStringsProperty = DependencyPropertyManager.Register("RuntimeLocalizationStrings", typeof(GridRuntimeStringCollection), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnRuntimeLocalizationStringsChanged)));
            LocalizationDescriptorPropertyKey = DependencyPropertyManager.RegisterReadOnly("LocalizationDescriptor", typeof(DevExpress.Xpf.Grid.LocalizationDescriptor), ownerType, new FrameworkPropertyMetadata(null));
            LocalizationDescriptorProperty = LocalizationDescriptorPropertyKey.DependencyProperty;
            WaitIndicatorTypeProperty = DependencyPropertyManager.Register("WaitIndicatorType", typeof(DevExpress.Xpf.Grid.WaitIndicatorType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.WaitIndicatorType.Default));
            WaitIndicatorStyleProperty = DependencyPropertyManager.Register("WaitIndicatorStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            IsWaitIndicatorVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsWaitIndicatorVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsWaitIndicatorVisibleProperty = IsWaitIndicatorVisiblePropertyKey.DependencyProperty;
            ScrollableAreaMinWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ScrollableAreaMinWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ScrollableAreaMinWidthProperty = ScrollableAreaMinWidthPropertyKey.DependencyProperty;
            TopRowIndexProperty = DependencyPropertyManager.Register("TopRowIndex", typeof(int), ownerType, new PropertyMetadata(0, (d, e) => ((DataViewBase) d).OnTopRowIndexChanged(), new CoerceValueCallback(DataViewBase.CoerceTopRowIndex)));
            AllowLeaveFocusOnTabProperty = DependencyPropertyManager.Register("AllowLeaveFocusOnTab", typeof(bool), ownerType, new PropertyMetadata(false));
            WheelScrollLinesProperty = DependencyPropertyManager.Register("WheelScrollLines", typeof(double), ownerType, new FrameworkPropertyMetadata((double) SystemParameters.WheelScrollLines, (d, e) => d.CoerceValue(WheelScrollLinesProperty), new CoerceValueCallback(DataViewBase.CoerceWheelScrollLines)));
            TouchScrollThresholdProperty = DependencyPropertyManager.Register("TouchScrollThreshold", typeof(double), ownerType, new PropertyMetadata(3.0));
            ColumnFilterPopupModeProperty = DependencyPropertyManager.Register("ColumnFilterPopupMode", typeof(DevExpress.Xpf.Grid.ColumnFilterPopupMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ColumnFilterPopupMode.Default, (d, e) => ((DataViewBase) d).OnColumnFilterPopupModeChanged()));
            ImmediateUpdateRowPositionProperty = DependencyPropertyManager.Register("ImmediateUpdateRowPosition", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DataViewBase) d).RefreshImmediateUpdateRowPositionProperty()));
            SummariesIgnoreNullValuesProperty = DependencyPropertyManager.Register("SummariesIgnoreNullValues", typeof(bool), typeof(DataViewBase), new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateSummariesIgnoreNullValues()));
            EnterMoveNextColumnProperty = DependencyPropertyManager.Register("EnterMoveNextColumn", typeof(bool), ownerType, new PropertyMetadata(false));
            ColumnChooserColumnsSortOrderComparerProperty = DependencyPropertyManager.Register("ColumnChooserColumnsSortOrderComparer", typeof(IComparer<ColumnBase>), ownerType, new PropertyMetadata(DefaultColumnChooserColumnsSortOrderComparer.Instance, (d, e) => ((DataViewBase) d).RebuildColumnChooserColumns()));
            ShownColumnChooserEvent = EventManager.RegisterRoutedEvent("ShownColumnChooser", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            HiddenColumnChooserEvent = EventManager.RegisterRoutedEvent("HiddenColumnChooser", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            BeforeLayoutRefreshEvent = EventManager.RegisterRoutedEvent("BeforeLayoutRefresh", RoutingStrategy.Bubble, typeof(CancelRoutedEventHandler), ownerType);
            CustomFilterDisplayTextEvent = EventManager.RegisterRoutedEvent("CustomFilterDisplayText", RoutingStrategy.Direct, typeof(CustomFilterDisplayTextEventHandler), ownerType);
            AutoScrollOnSortingProperty = DependencyPropertyManager.Register("AutoScrollOnSorting", typeof(bool), ownerType, new PropertyMetadata(true));
            IsColumnMenuEnabledProperty = DependencyPropertyManager.Register("IsColumnMenuEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            IsTotalSummaryMenuEnabledProperty = DependencyPropertyManager.Register("IsTotalSummaryMenuEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            IsRowCellMenuEnabledProperty = DependencyPropertyManager.Register("IsRowCellMenuEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ColumnHeaderToolTipTemplateProperty = DependencyPropertyManager.Register("ColumnHeaderToolTipTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnHeadersToolTipTemplate()));
            FilterEditorCreatedEvent = EventManager.RegisterRoutedEvent("FilterEditorCreated", RoutingStrategy.Direct, typeof(FilterEditorEventHandler), ownerType);
            RowOpacityAnimationDurationProperty = DependencyPropertyManager.Register("RowOpacityAnimationDuration", typeof(Duration), ownerType, new PropertyMetadata(defaultAnimationDuration));
            ShowFilterPopupEvent = EventManager.RegisterRoutedEvent("ShowFilterPopup", RoutingStrategy.Direct, typeof(FilterPopupEventHandler), ownerType);
            PastingFromClipboardEvent = EventManager.RegisterRoutedEvent("PastingFromClipboard", RoutingStrategy.Direct, typeof(PastingFromClipboardEventHandler), ownerType);
            UnboundExpressionEditorCreatedEvent = EventManager.RegisterRoutedEvent("UnboundExpressionEditorCreated", RoutingStrategy.Direct, typeof(UnboundExpressionEditorEventHandler), ownerType);
            FocusedColumnChangedEvent = EventManager.RegisterRoutedEvent("FocusedColumnChanged", RoutingStrategy.Direct, typeof(FocusedColumnChangedEventHandler), ownerType);
            FocusedRowHandleChangedEvent = EventManager.RegisterRoutedEvent("FocusedRowHandleChanged", RoutingStrategy.Direct, typeof(FocusedRowHandleChangedEventHandler), ownerType);
            FocusedRowChangedEvent = EventManager.RegisterRoutedEvent("FocusedRowChanged", RoutingStrategy.Direct, typeof(FocusedRowChangedEventHandler), ownerType);
            FocusedViewChangedEvent = EventManager.RegisterRoutedEvent("FocusedViewChanged", RoutingStrategy.Direct, typeof(FocusedViewChangedEventHandler), ownerType);
            ShowGridMenuEvent = EventManager.RegisterRoutedEvent("ShowGridMenu", RoutingStrategy.Direct, typeof(GridMenuEventHandler), ownerType);
            ColumnHeaderClickEvent = EventManager.RegisterRoutedEvent("ColumnHeaderClick", RoutingStrategy.Direct, typeof(ColumnHeaderClickEventHandler), ownerType);
            ShowFixedTotalSummaryProperty = DependencyPropertyManager.Register("ShowFixedTotalSummary", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false, (d, e) => ((DataViewBase) d).InvalidateParentTree()));
            DetailHeaderContentProperty = DependencyPropertyManager.Register("DetailHeaderContent", typeof(object), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateColumnChooserCaption()));
            ItemsSourceErrorInfoShowModeProperty = DependencyPropertyManager.Register("ItemsSourceErrorInfoShowMode", typeof(DevExpress.Xpf.Grid.ItemsSourceErrorInfoShowMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ItemsSourceErrorInfoShowMode.RowAndCell, (d, e) => ((DataViewBase) d).OnItemsSourceErrorInfoShowModeChanged()));
            SelectedRowsSourceProperty = DependencyPropertyManager.Register("SelectedRowsSource", typeof(IList), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).OnSelectedRowsSourceChanged()));
            AllItemsSelectedPropertyKey = DependencyProperty.RegisterReadOnly("AllItemsSelected", typeof(bool?), ownerType, new PropertyMetadata(false));
            AllItemsSelectedProperty = AllItemsSelectedPropertyKey.DependencyProperty;
            EnabledCheckBoxSelectorPropertyKey = DependencyProperty.RegisterReadOnly("EnabledCheckBoxSelector", typeof(bool), ownerType, new PropertyMetadata(true));
            EnabledCheckBoxSelectorProperty = EnabledCheckBoxSelectorPropertyKey.DependencyProperty;
            UseExtendedMouseScrollingProperty = DependencyPropertyManager.Register("UseExtendedMouseScrolling", typeof(bool), ownerType, new PropertyMetadata(true));
            EnableImmediatePostingProperty = DependencyPropertyManager.RegisterAttached("EnableImmediatePosting", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            DataNavigatorButtonsProperty = DependencyPropertyManager.RegisterAttached("DataNavigatorButtons", typeof(NavigatorButtonType), ownerType, new FrameworkPropertyMetadata(NavigatorButtonType.All));
            FilterRowDelayProperty = DependencyProperty.RegisterAttached("FilterRowDelay", typeof(int), ownerType, new PropertyMetadata(0));
            ClipboardCopyOptionsProperty = DependencyPropertyManager.RegisterAttached("ClipboardCopyOptions", typeof(DevExpress.Xpf.Grid.ClipboardCopyOptions), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ClipboardCopyOptions.All));
            ClipboardModeProperty = DependencyPropertyManager.RegisterAttached("ClipboardMode", typeof(DevExpress.Xpf.Grid.ClipboardMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ClipboardMode.PlainText));
            SelectionRectangleStyleProperty = DependencyPropertyManager.Register("SelectionRectangleStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateSelectionRectangle)));
            ShowSelectionRectangleProperty = DependencyPropertyManager.Register("ShowSelectionRectangle", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DataViewBase.OnUpdateSelectionRectangle)));
            TotalSummaryElementStyleProperty = DependencyPropertyManager.Register("TotalSummaryElementStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).OnSummaryDataChanged()));
            FixedTotalSummaryElementStyleProperty = DependencyPropertyManager.Register("FixedTotalSummaryElementStyle", typeof(Style), ownerType, new PropertyMetadata(null));
            SearchPanelFindFilterProperty = DependencyPropertyManager.Register("SearchPanelFindFilter", typeof(FilterCondition), typeof(DataViewBase), new PropertyMetadata(FilterCondition.Default));
            SearchPanelParseModeProperty = DependencyPropertyManager.Register("SearchPanelParseMode", typeof(DevExpress.Xpf.Editors.SearchPanelParseMode), typeof(DataViewBase), new PropertyMetadata(DevExpress.Xpf.Editors.SearchPanelParseMode.Mixed));
            SearchPanelCriteriaOperatorTypeProperty = DependencyPropertyManager.Register("SearchPanelCriteriaOperatorType", typeof(CriteriaOperatorType), typeof(DataViewBase), new FrameworkPropertyMetadata(CriteriaOperatorType.Or, (d, e) => ((DataViewBase) d).OnSeachPanelCriteriaOperatorTypeChanged()));
            SearchStringProperty = DependencyPropertyManager.Register("SearchString", typeof(string), typeof(DataViewBase), new PropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdateSearchPanelText()));
            SearchPanelHighlightResultsProperty = DependencyPropertyManager.Register("SearchPanelHighlightResults", typeof(bool), typeof(DataViewBase), new FrameworkPropertyMetadata(true));
            AllowLeaveInvalidEditorProperty = DependencyPropertyManager.RegisterAttached("AllowLeaveInvalidEditor", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateCellDataErrors()));
            ShowSearchPanelNavigationButtonsProperty = DependencyProperty.Register("ShowSearchPanelNavigationButtons", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateShowSearchPanelNavigationButtons()));
            ActualShowSearchPanelNavigationButtonsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowSearchPanelNavigationButtons", typeof(bool), ownerType, new PropertyMetadata(false));
            ActualShowSearchPanelNavigationButtonsProperty = ActualShowSearchPanelNavigationButtonsPropertyKey.DependencyProperty;
            ShowSearchPanelResultInfoProperty = DependencyProperty.Register("ShowSearchPanelResultInfo", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateShowSearchPanelResultInfo()));
            ActualShowSearchPanelResultInfoPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowSearchPanelResultInfo", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateFilterGrid()));
            ActualShowSearchPanelResultInfoProperty = ActualShowSearchPanelResultInfoPropertyKey.DependencyProperty;
            PrintHeaderTemplateProperty = DependencyProperty.Register("PrintHeaderTemplate", typeof(DataTemplate), ownerType, new UIPropertyMetadata(null));
            PrintCellStyleProperty = DependencyProperty.Register("PrintCellStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            PrintRowIndentStyleProperty = DependencyProperty.Register("PrintRowIndentStyle", typeof(Style), ownerType, new UIPropertyMetadata(null));
            PrintRowIndentWidthProperty = DependencyPropertyManager.Register("PrintRowIndentWidth", typeof(double), ownerType, new UIPropertyMetadata(20.0));
            PrintSelectedRowsOnlyProperty = DependencyPropertyManager.Register("PrintSelectedRowsOnly", typeof(bool), ownerType, new UIPropertyMetadata(false));
            PrintTotalSummaryProperty = DependencyPropertyManager.Register("PrintTotalSummary", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PrintFixedTotalSummaryProperty = DependencyPropertyManager.Register("PrintFixedTotalSummary", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PrintTotalSummaryStyleProperty = DependencyPropertyManager.Register("PrintTotalSummaryStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            PrintFixedTotalSummaryStyleProperty = DependencyPropertyManager.Register("PrintFixedTotalSummaryStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
            PrintFooterTemplateProperty = DependencyPropertyManager.Register("PrintFooterTemplate", typeof(DataTemplate), ownerType, new UIPropertyMetadata(null));
            PrintFixedFooterTemplateProperty = DependencyPropertyManager.Register("PrintFixedFooterTemplate", typeof(DataTemplate), ownerType, new UIPropertyMetadata(null));
            ShowSearchPanelCloseButtonProperty = DependencyPropertyManager.Register("ShowSearchPanelCloseButton", typeof(bool), typeof(DataViewBase), new PropertyMetadata(true));
            SearchPanelFindModeProperty = DependencyPropertyManager.Register("SearchPanelFindMode", typeof(FindMode), typeof(DataViewBase), new PropertyMetadata(FindMode.Always));
            ShowSearchPanelFindButtonProperty = DependencyPropertyManager.Register("ShowSearchPanelFindButton", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false));
            ShowSearchPanelMRUButtonProperty = DependencyPropertyManager.Register("ShowSearchPanelMRUButton", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false));
            SearchPanelAllowFilterProperty = DependencyPropertyManager.Register("SearchPanelAllowFilter", typeof(bool), typeof(DataViewBase), new FrameworkPropertyMetadata(true, (d, e) => ((DataViewBase) d).UpdateShowSearchPanelResultInfo()));
            SearchColumnsProperty = DependencyPropertyManager.Register("SearchColumns", typeof(string), typeof(DataViewBase), new FrameworkPropertyMetadata("*", (d, e) => ((DataViewBase) d).SearchColumnsChanged((string) e.NewValue)));
            SearchPanelClearOnCloseProperty = DependencyPropertyManager.Register("SearchPanelClearOnClose", typeof(bool), typeof(DataViewBase), new FrameworkPropertyMetadata(true));
            ShowSearchPanelModeProperty = DependencyPropertyManager.Register("ShowSearchPanelMode", typeof(DevExpress.Xpf.Grid.ShowSearchPanelMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Grid.ShowSearchPanelMode.Default, (d, e) => ((DataViewBase) d).UpdateSearchPanelVisibility(true)));
            ActualShowSearchPanelPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowSearchPanel", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).OnActualShowSearchPanelChanged()));
            ActualShowSearchPanelProperty = ActualShowSearchPanelPropertyKey.DependencyProperty;
            SearchDelayProperty = DependencyPropertyManager.Register("SearchDelay", typeof(int), ownerType, new PropertyMetadata(0x3e8));
            SearchPanelImmediateMRUPopupProperty = DependencyPropertyManager.Register("SearchPanelImmediateMRUPopup", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            SearchPanelHorizontalAlignmentProperty = DependencyPropertyManager.Register("SearchPanelHorizontalAlignment", typeof(HorizontalAlignment?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).SearchPanelHorizontalAlignmentChanged()));
            ActualSearchPanelHorizontalAlignmentPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualSearchPanelHorizontalAlignment", typeof(HorizontalAlignment), ownerType, new FrameworkPropertyMetadata(HorizontalAlignment.Right));
            ActualSearchPanelHorizontalAlignmentProperty = ActualSearchPanelHorizontalAlignmentPropertyKey.DependencyProperty;
            SearchControlProperty = DependencyPropertyManager.Register("SearchControl", typeof(DevExpress.Xpf.Editors.SearchControl), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).OnSearchControlChanged((DevExpress.Xpf.Editors.SearchControl) e.OldValue, (DevExpress.Xpf.Editors.SearchControl) e.NewValue)));
            SearchPanelNullTextProperty = DependencyPropertyManager.Register("SearchPanelNullText", typeof(string), ownerType, new FrameworkPropertyMetadata(null));
            IncrementalSearchModeProperty = DependencyProperty.Register("IncrementalSearchMode", typeof(DevExpress.Xpf.Grid.IncrementalSearchMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.IncrementalSearchMode.Default, (d, e) => ((DataViewBase) d).IncrementalSearchModeChanged()));
            UseOnlyCurrentColumnInIncrementalSearchProperty = DependencyProperty.Register("UseOnlyCurrentColumnInIncrementalSearch", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).OnUseOnlyCurrentColumnInIncrementalSearchChanged()));
            IncrementalSearchClearDelayProperty = DependencyProperty.Register("IncrementalSearchClearDelay", typeof(int?), ownerType, new PropertyMetadata(null, (d, e) => ((DataViewBase) d).IncrementalSearchModeChanged()));
            HasErrorsPropertyKey = DependencyProperty.RegisterReadOnly("HasErrors", typeof(bool), ownerType, new PropertyMetadata(false));
            HasErrorsProperty = HasErrorsPropertyKey.DependencyProperty;
            ErrorsWatchModeProperty = DependencyProperty.Register("ErrorsWatchMode", typeof(DevExpress.Xpf.Grid.ErrorsWatchMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ErrorsWatchMode.Default, (d, e) => ((DataViewBase) d).ErrorsWatchModeChanged()));
            CellToolTipTemplateProperty = DependencyPropertyManager.Register("CellToolTipTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).UpdatCellToolTipTemplate()));
            AllowDragDropProperty = DependencyPropertyManager.Register("AllowDragDrop", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false, (d, e) => ((DataViewBase) d).OnAllowDragDropChanged()));
            AutoExpandOnDragProperty = DependencyPropertyManager.Register("AutoExpandOnDrag", typeof(bool), typeof(DataViewBase), new PropertyMetadata(true));
            AutoExpandDelayOnDragProperty = DependencyPropertyManager.Register("AutoExpandDelayOnDrag", typeof(int), typeof(DataViewBase), new PropertyMetadata(0x3e8));
            AllowSortedDataDragDropProperty = DependencyPropertyManager.Register("AllowSortedDataDragDrop", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false));
            AllowScrollingOnDragProperty = DependencyPropertyManager.Register("AllowScrollingOnDrag", typeof(bool), typeof(DataViewBase), new PropertyMetadata(true, (d, e) => ((DataViewBase) d).RebuildDragManager()));
            ShowDragDropHintProperty = DependencyPropertyManager.Register("ShowDragDropHint", typeof(bool), typeof(DataViewBase), new PropertyMetadata(true, (d, e) => ((DataViewBase) d).RebuildDragManager()));
            ShowTargetInfoInDragDropHintProperty = DependencyPropertyManager.Register("ShowTargetInfoInDragDropHint", typeof(bool), typeof(DataViewBase), new PropertyMetadata(false));
            DropMarkerTemplateProperty = DependencyProperty.Register("DropMarkerTemplate", typeof(DataTemplate), typeof(DataViewBase), new PropertyMetadata(null));
            DragDropHintTemplateProperty = DependencyProperty.Register("DragDropHintTemplate", typeof(DataTemplate), typeof(DataViewBase), new PropertyMetadata(null));
            GiveRecordDragFeedbackEvent = EventManager.RegisterRoutedEvent("GiveRecordDragFeedback", RoutingStrategy.Direct, typeof(EventHandler<GiveRecordDragFeedbackEventArgs>), ownerType);
            ContinueRecordDragEvent = EventManager.RegisterRoutedEvent("ContinueRecordDrag", RoutingStrategy.Direct, typeof(EventHandler<ContinueRecordDragEventArgs>), ownerType);
            CompleteRecordDragDropEvent = EventManager.RegisterRoutedEvent("CompleteRecordDragDrop", RoutingStrategy.Direct, typeof(EventHandler<CompleteRecordDragDropEventArgs>), ownerType);
            DragRecordOverEvent = EventManager.RegisterRoutedEvent("DragRecordOver", RoutingStrategy.Direct, typeof(EventHandler<DragRecordOverEventArgs>), ownerType);
            DropRecordEvent = EventManager.RegisterRoutedEvent("DropRecord", RoutingStrategy.Direct, typeof(EventHandler<DropRecordEventArgs>), ownerType);
            StartRecordDragEvent = EventManager.RegisterRoutedEvent("StartRecordDrag", RoutingStrategy.Direct, typeof(EventHandler<StartRecordDragEventArgs>), ownerType);
            GetIsEditorActivationActionEvent = EventManager.RegisterRoutedEvent("GetIsEditorActivationAction", RoutingStrategy.Direct, typeof(EventHandler<GetIsEditorActivationActionEventArgs>), ownerType);
            ProcessEditorActivationActionEvent = EventManager.RegisterRoutedEvent("ProcessEditorActivationAction", RoutingStrategy.Direct, typeof(EventHandler<ProcessEditorActivationActionEventArgs>), ownerType);
            GetActiveEditorNeedsKeyEvent = EventManager.RegisterRoutedEvent("GetActiveEditorNeedsKey", RoutingStrategy.Direct, typeof(EventHandler<GetActiveEditorNeedsKeyEventArgs>), ownerType);
            EnableSelectedRowAppearanceProperty = DependencyPropertyManager.Register("EnableSelectedRowAppearance", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DataViewBase) d).EnableSelectedRowAppearanceChanged()));
            ActualShowCompactPanelPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowCompactPanel", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).UpdateVisibleGroupPanel()));
            ActualShowCompactPanelProperty = ActualShowCompactPanelPropertyKey.DependencyProperty;
            HeaderProperty = DependencyPropertyManager.Register("Header", typeof(object), ownerType, new PropertyMetadata(null));
            HeaderPositionProperty = DependencyPropertyManager.Register("HeaderPosition", typeof(DevExpress.Xpf.Grid.HeaderPosition), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.HeaderPosition.Top));
            HeaderHorizontalAlignmentProperty = DependencyPropertyManager.Register("HeaderHorizontalAlignment", typeof(HorizontalAlignment), ownerType, new PropertyMetadata(HorizontalAlignment.Left));
            ActualShowPagerPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowPager", typeof(bool), ownerType, new PropertyMetadata(false));
            ActualShowPagerProperty = ActualShowPagerPropertyKey.DependencyProperty;
            ValidatesOnNotifyDataErrorsProperty = DependencyPropertyManager.Register("ValidatesOnNotifyDataErrors", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DataViewBase) d).OnValidatesOnNotifyDataErrorsChanged()));
            ColumnSortClearModeProperty = DependencyPropertyManager.Register("ColumnSortClearMode", typeof(DevExpress.Xpf.Grid.ColumnSortClearMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ColumnSortClearMode.ClickWithCtrlPressed));
            IsAdditionalElementScrollBarVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsAdditionalElementScrollBarVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsAdditionalElementScrollBarVisibleProperty = IsAdditionalElementScrollBarVisiblePropertyKey.DependencyProperty;
            FilterEditorDialogServiceTemplateProperty = AssignableServiceHelper2<DataViewBase, IDialogService>.RegisterServiceTemplateProperty("FilterEditorDialogServiceTemplate");
            FilterEditorTemplateProperty = DependencyPropertyManager.Register("FilterEditorTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            UseLegacyFilterEditorProperty = DependencyProperty.Register("UseLegacyFilterEditor", typeof(bool?), typeof(DataViewBase), new PropertyMetadata(null));
            ColumnHeaderStyleProperty = DependencyPropertyManager.Register("ColumnHeaderStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DataViewBase) d).OnColumnHeaderStyleChanged()));
            ShowEmptyTextProperty = DependencyPropertyManager.Register("ShowEmptyText", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DataViewBase) d).OnShowEmptyTextChanged()));
            SummaryCalculationModeProperty = DependencyPropertyManager.Register("SummaryCalculationMode", typeof(GridSummaryCalculationMode), ownerType, new FrameworkPropertyMetadata(GridSummaryCalculationMode.AllRows, (d, e) => ((DataViewBase) d).OnSummaryCalculationModeChanged()));
            RegisterClassCommandBindings();
            EventManager.RegisterClassHandler(ownerType, DXSerializer.AllowPropertyEvent, new AllowPropertyEventHandler(DataViewBase.OnDeserializeAllowProperty));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.StartSerializingEvent, new RoutedEventHandler(DataViewBase.OnSerializeStart));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.StartDeserializingEvent, new StartDeserializingEventHandler(DataViewBase.OnDeserializeStart));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.EndDeserializingEvent, new EndDeserializingEventHandler(DataViewBase.OnDeserializeEnd));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.DeserializePropertyEvent, new XtraPropertyInfoEventHandler(DataViewBase.OnDeserializeProperty));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.CustomShouldSerializePropertyEvent, new CustomShouldSerializePropertyEventHandler(DataViewBase.OnCustomShouldSerializeProperty));
            DependencyPropertyKey[] knownKeys = new DependencyPropertyKey[] { ActualFadeSelectionOnLostFocusPropertyKey };
            CloneDetailHelper.RegisterKnownPropertyKeys(ownerType, knownKeys);
            ActualSearchPanelPositionPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualSearchPanelPosition", typeof(SearchPanelPosition), ownerType, new PropertyMetadata(SearchPanelPosition.OverGroupPanel));
            ActualSearchPanelPositionProperty = ActualSearchPanelPositionPropertyKey.DependencyProperty;
            AreUpdateRowButtonsShownPropertyKey = DependencyPropertyManager.RegisterReadOnly("AreUpdateRowButtonsShown", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DataViewBase) d).OnAreUpdateRowButtonsShownChanged()));
            AreUpdateRowButtonsShownProperty = AreUpdateRowButtonsShownPropertyKey.DependencyProperty;
        }

        internal DataViewBase(MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem, DataControlDetailDescriptor detailDescriptor)
        {
            this.namePropertyChangeListener = NamePropertyChangeListener.CreateDesignTimeOnly(this, () => this.DesignTimeAdorner.UpdateDesignTimeInfo());
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                BarNameScope.SetIsScopeOwner(this, true);
            }
            if (detailDescriptor != null)
            {
                this.OriginationView = detailDescriptor.DataControl.DataView;
            }
            this.visualDataTreeBuilder = new DevExpress.Xpf.Grid.Native.VisualDataTreeBuilder(this, masterRootNode, masterRootDataItem, (detailDescriptor != null) ? detailDescriptor.SynchronizationQueues : new SynchronizationQueues());
            this.ViewBehavior = this.CreateViewBehavior();
            this.emptyColumns = this.CreateEmptyColumnCollection();
            this.immediateActionsManager = new DevExpress.Xpf.Editors.ImmediateActionsManager(this);
            this.VisibleColumnsCore = new ObservableCollection<ColumnBase>();
            this.ColumnChooserColumns = new ReadOnlyCollection<ColumnBase>(new ColumnBase[0]);
            this.additionalRowNavigation = this.ViewBehavior.CreateAdditionalRowNavigation();
            this.dataIterator = this.CreateDataIterator();
            this.Commands = this.CreateCommandsContainer();
            this.SupressCacheCleanCountLocker = new Locker();
            this.RecreateLocalizationDescriptor();
            this.InplaceEditorOwner = new GridViewInplaceEditorOwner(this);
            this.EditorSetInactiveAfterClick = false;
            this.AllowMouseMoveSelection = true;
            this.columnMenuControllerValue = this.CreateMenuControllerLazyValue();
            this.totalSummaryMenuControllerValue = this.CreateMenuControllerLazyValue();
            this.rowCellMenuControllerValue = this.CreateMenuControllerLazyValue();
            this.compactModeColumnsControllerValue = this.CreateMenuControllerLazyValue();
            this.compactModeFilterMenuControllerValue = this.CreateMenuControllerLazyValue();
            this.compactModeMergeMenuControllerValue = this.CreateMenuControllerLazyValue();
            DataControlBase.SetActiveView(this, this);
            base.RequestBringIntoView += new RequestBringIntoViewEventHandler(this.DataViewBase_RequestBringIntoView);
            base.SizeChanged += new SizeChangedEventHandler(this.DataViewBase_SizeChanged);
            this.ErrorsWatchModeCore = DevExpress.Xpf.Grid.ErrorsWatchMode.Default;
            this.FocusedRowHandleCore = -2147483648;
            this.EnableSelectedRowAppearanceCore = true;
            this.IncrementalSearchModeCore = DevExpress.Xpf.Grid.IncrementalSearchMode.Default;
        }

        internal void AddChild(object child)
        {
            base.AddLogicalChild(child);
            this.logicalChildren.Add(child);
        }

        protected abstract void AddCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler);
        internal void AddScrollOneItemAfterPageDownAction(DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount)
        {
            this.PostponedNavigationInProgress = true;
            IAction action = null;
            action = !this.RootView.ViewBehavior.AllowPerPixelScrolling ? new ScrollOneItemAfterPageDownAction(this, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount) : new ScrollOneItemAfterPageDownPerPixelAction(this, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount);
            this.EnqueueImmediateAction(action);
        }

        internal void AddScrollOneItemAfterPageUpAction(DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount)
        {
            this.PostponedNavigationInProgress = true;
            this.EnqueueImmediateAction(new ScrollOneItemAfterPageUpAction(this, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount));
        }

        private void AllowFilterEditorChanged()
        {
            CommandManager.InvalidateRequerySuggested();
            this.UpdateShowEditFilterButtonCore();
        }

        internal void ApplyColumnVisibleIndex(BaseColumn column, int oldVisibleIndex)
        {
            if (!this.IsUpdateVisibleIndexesLocked && (!this.IsLoading && (!this.DataControl.IsDeserializing && (!this.ColumnsCore.IsLockUpdate && (!this.UseLegacyColumnVisibleIndexes || column.Visible)))))
            {
                this.UpdateVisibleIndexesLocker.Lock();
                try
                {
                    this.ViewBehavior.ApplyColumnVisibleIndex(column, oldVisibleIndex);
                }
                finally
                {
                    this.UpdateVisibleIndexesLocker.Unlock();
                }
            }
        }

        internal void ApplyResize(BaseColumn column, double value, double maxWidth)
        {
            this.ViewBehavior.ApplyResize(column, value, maxWidth);
        }

        internal void ApplySearchColumns()
        {
            if (!string.IsNullOrEmpty(this.SearchColumns) && (this.SearchPanelColumnProvider != null))
            {
                List<ColumnBase> columnsSource = new List<ColumnBase>();
                foreach (ColumnBase base2 in this.ColumnsCore)
                {
                    columnsSource.Add(base2);
                }
                ObservableCollection<string> searchColumns = GridColumnListParser.GetSearchColumns(columnsSource, this.SearchColumns);
                if (searchColumns == null)
                {
                    this.SearchPanelColumnProvider.FilterByColumnsMode = FilterByColumnsMode.Default;
                }
                else
                {
                    this.SearchPanelColumnProvider.FilterByColumnsMode = FilterByColumnsMode.Custom;
                    this.SearchPanelColumnProvider.CustomColumns = searchColumns;
                }
                if (this.SearchControl == null)
                {
                    this.SearchPanelColumnProvider.UpdateColumns();
                }
                else
                {
                    this.SearchControl.UpdateColumnProvider();
                }
            }
        }

        internal virtual void AssignVisibleColumns(IList<ColumnBase> visibleColumnsList)
        {
            bool changed = false;
            if (!ReferenceEquals(this.VisibleColumnsCore, visibleColumnsList) && !ListHelper.AreEqual<ColumnBase>(this.VisibleColumnsCore, visibleColumnsList))
            {
                this.VisibleColumnsCore = visibleColumnsList;
                changed = true;
                this.DesignTimeAdorner.OnColumnsLayoutChanged();
            }
            changed = this.OnVisibleColumnsAssigned(changed);
            if (changed)
            {
                if (this.DataPresenter == null)
                {
                    this.UpdateCellData();
                }
                else
                {
                    this.IsUpdateCellDataEnqueued = true;
                    this.ImmediateActionsManager.EnqueueAction(new Action(this.UpdateCellData));
                }
            }
            ITableView tView = this as ITableView;
            if ((tView != null) & changed)
            {
                this.updateColumnsLayoutSeparatorLocker.DoLockedAction(() => tView.OnChangeBandSeparator());
            }
            if (!this.updateColumnsLayoutSeparatorLocker.IsLocked)
            {
                this.ViewBehavior.UpdateColumnsLayout();
                this.ViewBehavior.UpdateViewportVisibleColumns();
            }
        }

        internal virtual bool AttachObservablePropertyScheme(ObservablePropertySchemeNode[] scheme)
        {
            if (this.DataControl == null)
            {
                return false;
            }
            if (!this.DataControl.DataProviderBase.SubscribeRowChangedForVisibleRows)
            {
                return false;
            }
            UpdateRowDataDelegate updateMethod = <>c.<>9__2235_0;
            if (<>c.<>9__2235_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__2235_0;
                updateMethod = <>c.<>9__2235_0 = delegate (RowData rowData) {
                    rowData.UpdateObservablePropertyScheme();
                };
            }
            this.UpdateRowData(updateMethod, true, true);
            return true;
        }

        internal void AutoExpandAllGroupsLockAction(Action action)
        {
            if ((action != null) && (this.DataControl != null))
            {
                bool autoExpandAllGroups = false;
                if (this.DataControl.DataProviderBase != null)
                {
                    autoExpandAllGroups = this.DataControl.DataProviderBase.AutoExpandAllGroups;
                    this.DataControl.DataProviderBase.AutoExpandAllGroups = false;
                }
                this.DataControl.BeginDataUpdate();
                action();
                this.DataControl.EndDataUpdate();
                if (this.DataControl.DataProviderBase != null)
                {
                    this.DataControl.DataProviderBase.AutoExpandAllGroups = autoExpandAllGroups;
                }
            }
        }

        protected internal virtual void BeforeMoveColumnToChooser(BaseColumn column, HeaderPresenterType sourceType)
        {
        }

        internal void BeginSelectionCore()
        {
            this.SelectionStrategy.BeginSelection();
        }

        internal virtual void BeginUpdateColumnsLayout()
        {
            this.updateColumnsLayoutLocker.Lock();
        }

        internal virtual void BindDetailContainerNextLevelItemsControl(ItemsControl itemsControl)
        {
        }

        internal virtual int CalcActualFocusedRowHandle() => 
            this.CalcActualRowHandle(this.FocusedRowHandleCore, this.DataControl.CurrentColumnCore);

        private int CalcActualRowHandle(int rowHandle, ColumnBase column)
        {
            if (!this.ActualAllowCellMerge || ((column == null) || (this.GetRowData(rowHandle) == null)))
            {
                return rowHandle;
            }
            int rowVisibleIndexByHandleCore = this.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);
            while (this.IsNextRowCellMerged(rowVisibleIndexByHandleCore, column, true))
            {
                rowVisibleIndexByHandleCore++;
            }
            return this.DataControl.GetRowHandleByVisibleIndexCore(rowVisibleIndexByHandleCore);
        }

        protected internal virtual double CalcCellFocusedRectangleOffset() => 
            0.0;

        internal int CalcFirstScrollRowScrollIndex() => 
            Math.Min((int) Math.Ceiling((double) (this.RootDataPresenter.ActualScrollOffset + this.ActualFixedTopRowsCount)), this.RootDataPresenter.ItemCount - 1);

        protected internal virtual int CalcGroupSummaryVisibleRowCount() => 
            0;

        internal abstract IDataViewHitInfo CalcHitInfoCore(DependencyObject source);
        internal int CalcLastScrollRowScrollIndex()
        {
            DataViewBase view = null;
            int visibleIndex = 0;
            this.GetLastScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            return ((view != null) ? view.ConvertVisibleIndexToScrollIndex(visibleIndex) : 0);
        }

        internal double CalcOffsetByHeight(double invisibleHeight, bool perPixelScrolling)
        {
            double num = 0.0;
            GridRowsEnumerator enumerator = this.RootView.CreateVisibleRowsEnumerator();
            RowDataBase rowDataToScroll = this.RootDataPresenter.GetRowDataToScroll();
            bool flag = false;
            Func<HierarchyPanel, double> evaluator = <>c.<>9__1548_0;
            if (<>c.<>9__1548_0 == null)
            {
                Func<HierarchyPanel, double> local1 = <>c.<>9__1548_0;
                evaluator = <>c.<>9__1548_0 = x => x.FixedTopRowsHeight;
            }
            double bottom = this.RootDataPresenter.Panel.Return<HierarchyPanel, double>(evaluator, <>c.<>9__1548_1 ??= () => 0.0);
            while (enumerator.MoveNext())
            {
                if (enumerator.CurrentNode.FixedRowPosition != FixedRowPosition.None)
                {
                    continue;
                }
                Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(this.GetRowVisibleElement(enumerator.CurrentRowData), this.RootDataPresenter);
                if (!flag)
                {
                    if ((rowDataToScroll != null) && !ReferenceEquals(rowDataToScroll.node, enumerator.CurrentNode))
                    {
                        bottom = relativeElementRect.Bottom;
                        continue;
                    }
                    flag = true;
                }
                double defineSize = this.SizeHelper.GetDefineSize(relativeElementRect.Size());
                if (this.SizeHelper.GetDefinePoint(relativeElementRect.Location()) < bottom)
                {
                    invisibleHeight -= this.SizeHelper.GetDefinePoint(relativeElementRect.Location()) - bottom;
                }
                if (defineSize >= invisibleHeight)
                {
                    num += perPixelScrolling ? (invisibleHeight / defineSize) : 1.0;
                    break;
                }
                invisibleHeight -= defineSize;
                num++;
                bottom = relativeElementRect.Bottom;
            }
            return num;
        }

        internal double CalcOffsetForBackwardScrolling(int firstInvisibleIndex)
        {
            FrameworkElement rowElementByVisibleIndex = this.GetRowElementByVisibleIndex(firstInvisibleIndex);
            if ((rowElementByVisibleIndex == null) || !rowElementByVisibleIndex.IsInVisualTree())
            {
                return 0.0;
            }
            double itemVisibleSize = this.GetItemVisibleSize(rowElementByVisibleIndex);
            return ((itemVisibleSize != 0.0) ? this.CalcOffsetByHeight(itemVisibleSize, true) : 0.0);
        }

        protected internal virtual double CalcOffsetForward(int rowHandle, bool perPixelScrolling)
        {
            FrameworkElement element = null;
            element = ((rowHandle != -2147483647) || !this.IsNewItemRowVisible) ? this.GetRowElementByVisibleIndex(this.dataControl.GetRowVisibleIndexByHandleCore(rowHandle)) : this.GetRowElementByRowHandle(rowHandle);
            return (((element == null) || !element.IsInVisualTree()) ? 1.0 : this.CalcOffsetByHeight(this.GetItemInvisibleSize(element), perPixelScrolling));
        }

        protected internal virtual double CalcVerticalDragIndicatorSize(UIElement panel, Point point, double width) => 
            this.ViewBehavior.CalcVerticalDragIndicatorSize(panel, point, width);

        protected internal virtual bool CanAddNewRow() => 
            !this.IsEditing && (this.IsAddDeleteInSource() && ((this.DataProviderBase.DataController != null) && this.DataProviderBase.DataController.AllowNew));

        protected internal virtual bool CanAdjustScrollbar() => 
            this.ViewBehavior.CanAdjustScrollbarCore();

        internal bool CanBecameFixed(BaseColumn column)
        {
            int fixedNoneColumnsCount = this.FixedNoneColumnsCount;
            if (this.DataControl.BandsCore.Count != 0)
            {
                fixedNoneColumnsCount = this.DataControl.BandsLayoutCore.FixedNoneVisibleBands.Count;
            }
            return (!column.Visible || ((column.Fixed != FixedStyle.None) || (fixedNoneColumnsCount != 1)));
        }

        protected internal virtual bool CanCancelEditFocusedRow() => 
            !this.IsAutoFilterRowFocused && (this.DataProviderBase.IsCurrentRowEditing || (this.ActiveEditor != null));

        protected bool CanCancelRowChangesCore() => 
            this.AreUpdateRowButtonsShown;

        internal void CancelEditFocusedRow()
        {
            if (this.CanCancelEditFocusedRow())
            {
                this.CancelRowEdit();
            }
        }

        protected internal void CancelRowChangesCore(bool useCache = false)
        {
            RowData rowData = this.GetRowData(this.FocusedRowHandle);
            if ((rowData != null) && (rowData.HasValidationErrorInternal && (rowData.CancelValuesCache.Count > 0)))
            {
                useCache = true;
            }
            if ((this.CanCancelRowChangesCore() || useCache) && (rowData != null))
            {
                int updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                this.AreUpdateRowButtonsShown = false;
                this.RootView.AreUpdateRowButtonsShown = false;
                try
                {
                    if (!this.DataControl.CanCancelRow(rowData.Row))
                    {
                        this.CancelRowEdit();
                    }
                    this.ValidationError = null;
                    if (useCache)
                    {
                        foreach (KeyValuePair<ColumnBase, object> pair in rowData.CancelValuesCache)
                        {
                            this.DataControl.SetCellValueCore(rowData.RowHandle.Value, pair.Key.FieldName, pair.Value);
                        }
                    }
                    else
                    {
                        foreach (ColumnBase base2 in this.VisibleColumnsCore)
                        {
                            EditGridCellData cellDataByColumn = rowData.GetCellDataByColumn(base2) as EditGridCellData;
                            if (cellDataByColumn != null)
                            {
                                cellDataByColumn.UpdateValue(true);
                                rowData.UpdateCellDataError(base2, cellDataByColumn, true);
                            }
                        }
                    }
                    this.CancelRowEditLocker.DoLockedAction(() => this.CancelRowEdit());
                    rowData.UpdateRowDataError();
                    rowData.UpdateDataErrors(true);
                    rowData.UpdateRowButtonsWasChanged = false;
                }
                finally
                {
                    updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
                    this.ViewBehavior.UpdateRowButtonsControl();
                }
            }
        }

        public void CancelRowEdit()
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.CancelEditInVisibleEditor();
            }
            if (this.DataProviderBase.IsCurrentRowEditing)
            {
                this.DataProviderBase.CancelCurrentRowEdit();
            }
            this.ViewBehavior.OnCancelRowEdit();
            this.DataControl.SetRowStateError(this.FocusedRowHandle, null);
        }

        internal bool CanClearColumnFilter(object commandParameter) => 
            (commandParameter is ColumnBase) && ((this.DataControl != null) && (commandParameter as ColumnBase).IsFiltered);

        internal bool CanClearFilter() => 
            (this.DataControl != null) && (this.DataControl.FilterCriteria != null);

        internal virtual bool CanCopyRows() => 
            false;

        protected internal virtual bool CanDeleteFocusedRow() => 
            !this.AreUpdateRowButtonsShown ? ((this.DataProviderBase != null) && (!this.IsAutoFilterRowFocused && (!this.IsEditing && (!this.IsInvalidFocusedRowHandle && (this.IsFocusedView && ((this.DataProviderBase.DataRowCount > 0) && this.IsAddDeleteInSource())))))) : false;

        protected internal virtual bool CanEditFocusedRow()
        {
            bool flag = false;
            if ((this is ITableView) && (((ITableView) this).EditFormShowMode == EditFormShowMode.None))
            {
                flag = true;
            }
            return (!((this.IsVirtualSource && (this.ShowUpdateRowButtonsCore == ShowUpdateRowButtons.Never)) & flag) ? ((!this.EditFormManager.AllowEditForm || !this.CanShowEditForm()) ? (!this.IsAutoFilterRowFocused && ((this.NavigationStyle == GridViewNavigationStyle.Cell) && ((this.DataControl != null) && ((this.DataControl.CurrentColumn != null) && ((this.ActiveEditor == null) && ((this.CurrentCellEditor != null) && this.CurrentCellEditor.CanShowEditor())))))) : true) : false);
        }

        protected internal virtual bool CanEndEditFocusedRow() => 
            !this.IsAutoFilterRowFocused && this.IsEditing;

        internal static void CanExecuteWithCheckActualView(CanExecuteRoutedEventArgs e, Func<bool> canExecute)
        {
            if ((e.Source is DataViewBase) && ((e.OriginalSource is DependencyObject) && (e.Source != ((DependencyObject) e.OriginalSource).GetValue(DataControlBase.ActiveViewProperty))))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = canExecute();
            }
        }

        internal bool CanHideColumnChooser() => 
            this.IsColumnChooserVisible;

        internal bool CanHighlightedState(int rowHandle, ColumnBase column, bool isCell, SelectionState state, bool isGroupRow = false)
        {
            ITableView view = this as ITableView;
            if ((view == null) || (!view.HighlightItemOnHover || ((this.DataControl == null) || ((state == SelectionState.Focused) || ((state == SelectionState.FocusedAndSelected) || ((state == SelectionState.CellMerge) || ((this.DataPresenter == null) || this.DataPresenter.IsAnimationInProgress)))))))
            {
                return false;
            }
            if (this.IsEditing && (this.FocusedRowHandle == rowHandle))
            {
                if (!isCell)
                {
                    return false;
                }
                if (ReferenceEquals(column, this.DataControl.CurrentColumn))
                {
                    return false;
                }
            }
            return (!isGroupRow ? (!isCell ? (this.DataControl.SelectionMode != MultiSelectMode.Cell) : (this.DataControl.SelectionMode == MultiSelectMode.Cell)) : true);
        }

        internal bool CanMouseNavigationWithUpdateRow() => 
            this.AreUpdateRowButtonsShown && !this.HasCellEditorError;

        internal bool CanMoveFirstCell() => 
            (this.DataControl != null) && (!this.ViewBehavior.NavigationStrategyBase.IsBeginNavigationIndex(this) && (this.CheckNavigationIndexAndFocusedRowHandle() && !this.DataControl.IsGroupRowHandleCore(this.FocusedRowHandle)));

        internal bool CanMoveFirstRow() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsFirstRow && (this.CheckNavigationIndex() && !this.IsInvalidFocusedRowHandle)) : false;

        internal bool CanMoveFromFocusedRow() => 
            !this.IsAutoFilterRowFocused || ((this.NavigationStyle != GridViewNavigationStyle.Cell) || ((this.DataControl.CurrentColumn != null) && this.DataControl.CurrentColumn.AllowFocus));

        internal bool CanMoveLastCell() => 
            (this.DataControl != null) && (!this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this) && (!this.IsInvalidFocusedRowHandle && !this.DataControl.IsGroupRowHandleCore(this.FocusedRowHandle)));

        internal bool CanMoveLastRow() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsLastRow && (this.CheckNavigationIndex() && !this.IsInvalidFocusedRowHandle)) : false;

        internal bool CanMoveNextCell() => 
            ((!this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this) && this.CheckNavigationIndex()) || !this.IsLastRow) && !this.IsInvalidFocusedRowHandle;

        internal bool CanMoveNextPage() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsLastRow && this.CheckNavigationIndexAndFocusedRowHandle()) : false;

        protected internal bool CanMoveNextRow() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsLastRow && (!this.IsInvalidFocusedRowHandle && ((this.DataControl != null) && (this.DataControl.VisibleRowCount != 0)))) : false;

        internal bool CanMovePrevCell() => 
            ((!this.ViewBehavior.NavigationStrategyBase.IsBeginNavigationIndex(this) && this.CheckNavigationIndex()) || !this.IsFirstRow) && !this.IsInvalidFocusedRowHandle;

        internal bool CanMovePrevPage() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsFirstRow && this.CheckNavigationIndexAndFocusedRowHandle()) : false;

        protected internal bool CanMovePrevRow() => 
            !this.AreUpdateRowButtonsShown ? ((this.FocusedRowHandle != -2147483645) ? (!this.IsFirstNewRow() ? (!this.IsFirstRow && (!this.IsInvalidFocusedRowHandle && ((this.DataControl != null) && (this.DataControl.VisibleRowCount != 0)))) : false) : false) : false;

        public bool CanMoveSearchResult() => 
            (this.NavigationStyle != GridViewNavigationStyle.None) && ((this.SearchControl != null) && (!string.IsNullOrEmpty(this.SearchControl.SearchText) && (this.CanMoveSearchResultCore() && (this.searchResult || ((this.SearchPanelFindMode == FindMode.FindClick) || ((this.DataControl != null) && (this.SearchPanelAllowFilter && (this.DataControl.VisibleRowCount > 0))))))));

        private bool CanMoveSearchResultCore()
        {
            GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
            return ((searchControl != null) ? searchControl.ClearButtonIsVisible : true);
        }

        internal bool CanNextRow()
        {
            if (this.AreUpdateRowButtonsShown)
            {
                return false;
            }
            if (this.IsAdditionalRowFocused && (!this.IsNewItemRowHandle(this.FocusedRowHandle) && (this.FocusedRowHandle != -2147483645)))
            {
                return false;
            }
            if ((this.DataControl == null) || (this.DataProviderBase == null))
            {
                return false;
            }
            if (this.DataControl.MasterDetailProvider.FindFirstDetailView(this.DataProviderBase.CurrentIndex) != null)
            {
                return true;
            }
            DataViewBase targetView = null;
            int targetVisibleIndex = -1;
            if (this.DataControl.DataControlParent.FindNextOuterMasterRow(out targetView, out targetVisibleIndex))
            {
                return true;
            }
            int index = (this.FocusedRowHandle == -2147483648) ? -1 : this.DataControl.CurrentIndex;
            return (!this.DataControl.IsLast(index) ? (!this.IsRootView || this.CanMoveNextRow()) : false);
        }

        internal bool CanPrevRow() => 
            !this.AreUpdateRowButtonsShown ? (!this.IsRootView || this.CanMovePrevRow()) : false;

        protected internal virtual bool CanRaiseAddingNewRow() => 
            false;

        internal bool CanRaiseCanSelectRow() => 
            this.EventTargetView.CanSelectRow != null;

        internal bool CanRaiseCanUnselectRow() => 
            this.EventTargetView.CanUnselectRow != null;

        protected internal virtual bool CanScrollDown() => 
            this.DataPresenter.CanApplyScroll() ? (!this.IsEditFormVisible ? ((this.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Offset + this.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Viewport) < this.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Extent) : true) : false;

        protected internal virtual bool CanScrollLeft() => 
            this.DataPresenter.CanApplyScroll() ? (this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset > 0.0) : false;

        protected internal virtual bool CanScrollRight() => 
            this.DataPresenter.CanApplyScroll() ? ((this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset + this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport) < this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Extent) : false;

        protected internal virtual bool CanScrollUp() => 
            this.DataPresenter.CanApplyScroll() ? (this.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Offset > 0.0) : false;

        protected internal virtual bool CanSelectCellInRow(int rowHandle) => 
            this.GetNavigation(rowHandle).CanSelectCell;

        internal bool CanShowColumnChooser() => 
            !this.IsColumnChooserVisible;

        protected internal virtual bool CanShowColumnInSummaryEditor(ColumnBase column) => 
            !string.IsNullOrEmpty(column.FieldName) && (this.DataProviderBase.Columns[column.FieldName] != null);

        internal virtual bool CanShowEditForm() => 
            false;

        internal virtual bool CanShowEditor(int rowHandle, ColumnBase column) => 
            ((rowHandle == -2147483645) || (column.GetAllowEditing() && (!this.DataProviderBase.IsAsyncOperationInProgress && !this.EditFormManager.AllowEditForm))) ? (!this.IsKeyboardFocusInSearchPanel() && this.RaiseShowingEditor(rowHandle, column)) : false;

        internal bool CanShowFilterEditor(object commandParameter)
        {
            bool actualAllowFilterEditor = true;
            if (commandParameter is ColumnBase)
            {
                actualAllowFilterEditor = ((ColumnBase) commandParameter).ActualAllowFilterEditor;
            }
            return (((this.DataControl != null) & actualAllowFilterEditor) && this.ShowEditFilterButton);
        }

        protected bool CanShowSearchPanel()
        {
            switch (this.ShowSearchPanelMode)
            {
                case DevExpress.Xpf.Grid.ShowSearchPanelMode.Default:
                    return !this.IsVirtualSource;

                case DevExpress.Xpf.Grid.ShowSearchPanelMode.HotKey:
                case DevExpress.Xpf.Grid.ShowSearchPanelMode.Always:
                    return true;
            }
            return false;
        }

        internal bool CanShowTotalSummaryEditor(object parameter) => 
            this.GetColumnByCommandParameter(parameter) != null;

        internal bool CanShowUnboundExpressionEditor(object commandParameter)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(commandParameter);
            return ((columnByCommandParameter != null) && columnByCommandParameter.AllowUnboundExpressionEditor);
        }

        protected internal virtual bool CanSortColumn(string fieldName) => 
            this.CanSortColumnCore(this.ColumnsCore[fieldName], fieldName, true);

        protected internal virtual bool CanSortColumn(ColumnBase column, string fieldName) => 
            this.CanSortColumnCore(column, fieldName, false);

        protected internal virtual bool CanSortColumnCore(ColumnBase column, string fieldName, bool prohibitColumnProperty)
        {
            if (!this.DataControl.DataProviderBase.CanColumnSortCore(fieldName))
            {
                return false;
            }
            if ((column != null) && (column.AllowSorting != DefaultBoolean.Default))
            {
                return ((column != null) && (prohibitColumnProperty || (column.AllowSorting != DefaultBoolean.False)));
            }
            DataColumnInfo columnInfo = this.DataProviderBase.Columns[fieldName];
            return ((columnInfo != null) && this.CanSortDataColumnInfo(columnInfo));
        }

        protected virtual bool CanSortDataColumnInfo(DataColumnInfo columnInfo) => 
            columnInfo.AllowSort;

        protected internal virtual bool CanStartDrag(GridColumnHeaderBase header) => 
            true;

        internal virtual bool CanStartDragSingleColumn() => 
            false;

        protected internal virtual bool CanStartSelection() => 
            this.EditorShowMode != DevExpress.Xpf.Core.EditorShowMode.MouseDown;

        protected internal virtual bool CanUpdateBorderForFocusedElement() => 
            this.DataControl.GetRootDataControl().IsKeyboardFocusWithin;

        protected internal bool CanUpdateRowCore() => 
            this.AreUpdateRowButtonsShown && (!this.HasCellEditorError && this.IsFocusedRowModified);

        internal void ChangeColumnsSortOrder(ChangeColumnsSortOrderMode mode)
        {
            Func<ColumnBase, bool> <>9__0;
            Func<ColumnBase, bool> predicate = <>9__0;
            if (<>9__0 == null)
            {
                Func<ColumnBase, bool> local1 = <>9__0;
                predicate = <>9__0 = x => (!mode.HasFlag(ChangeColumnsSortOrderMode.NotSortedColumns) || (x.SortOrder != ColumnSortOrder.None)) ? ((!mode.HasFlag(ChangeColumnsSortOrderMode.SortedColumns) || ((x.SortOrder == ColumnSortOrder.None) || (x.GroupIndexCore >= 0))) ? (mode.HasFlag(ChangeColumnsSortOrderMode.GroupedColumns) && ((x.SortOrder != ColumnSortOrder.None) && (x.GroupIndexCore >= 0))) : true) : true;
            }
            foreach (ColumnBase base2 in this.ColumnsCore.Cast<ColumnBase>().Where<ColumnBase>(predicate))
            {
                this.OnColumnHeaderClick(base2, true, false);
            }
        }

        [Browsable(false)]
        public void ChangeHorizontalScrollOffsetBy(double offset)
        {
            this.ViewBehavior.ChangeHorizontalOffsetBy(offset);
        }

        [Browsable(false)]
        public void ChangeVerticalScrollOffsetBy(double offset)
        {
            this.ViewBehavior.ChangeVerticalOffsetBy(offset);
        }

        protected bool ChangeVisibleRowExpand(int rowHandle) => 
            (rowHandle != -2147483648) ? this.ChangeVisibleRowExpandCore(rowHandle) : false;

        protected abstract bool ChangeVisibleRowExpandCore(int rowHandle);
        internal void CheckExport()
        {
            this.CheckPagedVirtualSourceLimitation("The GridControl bound to the Paged data source does not support exporting data.");
        }

        private bool CheckNavigationIndex() => 
            (this.NavigationStyle == GridViewNavigationStyle.Cell) ? (this.NavigationIndex != -2147483648) : true;

        private bool CheckNavigationIndexAndFocusedRowHandle() => 
            this.CheckNavigationIndex() && !this.IsInvalidFocusedRowHandle;

        private void CheckPagedVirtualSourceLimitation(string exceptionMessage)
        {
            if ((this.DataProviderBase != null) && this.DataProviderBase.IsPagedSource)
            {
                throw new NotSupportedException(exceptionMessage);
            }
        }

        protected internal virtual void CheckPagerControlPageCount()
        {
        }

        internal void CheckPrinting()
        {
            this.CheckPagedVirtualSourceLimitation("The GridControl bound to the Paged data source does not support printing data.");
        }

        protected void CheckShowUpdateRowButtonsWithEditFormShowMode()
        {
            if ((this is ITableView) && ((this.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.Never) && (((ITableView) this).EditFormShowMode != EditFormShowMode.None)))
            {
                throw new NotSupportedException("You cannnot specify the EditFormShowMode and ShowUpdateRowButtons properties simultaneously.");
            }
        }

        private bool CheckStartIncrementalSearch(DevExpress.Xpf.Grid.IncrementalSearchMode mode) => 
            !this.IsKeyboardFocusInSearchPanel() && (!this.IsKeyboardFocusInHeadersPanel() && (!this.IsAutoFilterRowFocused && ((mode != DevExpress.Xpf.Grid.IncrementalSearchMode.Disabled) && ((mode != DevExpress.Xpf.Grid.IncrementalSearchMode.Default) && ((mode == DevExpress.Xpf.Grid.IncrementalSearchMode.Enabled) && (!this.IsEditing && !this.IsKeyboardFocusWithinEditForm()))))));

        protected virtual void ClearAllStates()
        {
            this.Navigation.ClearAllStates();
            this.AdditionalRowNavigation.ClearAllStates();
            if (this.DataProviderBase != null)
            {
                this.ClearSelectionCore();
                this.SetFocusedRowHandle(-2147483648);
                if (this.DataControl != null)
                {
                    this.DataControl.ReInitializeCurrentColumn();
                }
            }
        }

        private void ClearColumnFilter(ExecutedRoutedEventArgs e)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(e.Parameter);
            if (columnByCommandParameter != null)
            {
                this.DataControl.ClearColumnFilter(columnByCommandParameter);
            }
        }

        internal void ClearCurrentCellIfNeeded()
        {
            if ((this.CurrentCell != null) && (this.GetRowElementByRowHandle(this.FocusedRowHandle) == null))
            {
                this.CurrentCell = null;
            }
        }

        internal void ClearEditorHighlightingText()
        {
            if (this.VisibleColumnsCore != null)
            {
                this.UpdateRowData(delegate (RowData rowData) {
                    Func<ColumnBase, string> selector = <>c.<>9__1368_1;
                    if (<>c.<>9__1368_1 == null)
                    {
                        Func<ColumnBase, string> local1 = <>c.<>9__1368_1;
                        selector = <>c.<>9__1368_1 = x => x.FieldName;
                    }
                    rowData.UpdateEditorHighlightingText(null, this.VisibleColumnsCore.Select<ColumnBase, string>(selector).ToArray<string>());
                }, false, false);
            }
        }

        internal void ClearFilter()
        {
            this.DataControl.FilterCriteria = null;
        }

        protected internal virtual void ClearFocusedRectangle()
        {
            if (this.RootView.FocusRectPresenter != null)
            {
                this.RootView.FocusRectPresenter.Visibility = Visibility.Collapsed;
            }
        }

        internal void ClearSelectionCore()
        {
            this.SelectionStrategy.ClearSelection();
        }

        public void CloseEditor()
        {
            this.CloseEditor(true, false);
        }

        internal void CloseEditor(bool closeEditor, bool cleanError = false)
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.CommitEditor(closeEditor);
                if (cleanError)
                {
                    this.CurrentCellEditor.ClearError();
                    if (this.HasCellEditorError)
                    {
                        this.CurrentCellEditor.CancelEditInVisibleEditor();
                    }
                }
            }
        }

        internal static int CoerceBestFitMaxRowCount(int baseValue) => 
            Math.Max(baseValue, -1);

        private IColumnChooserFactory CoerceColumnChooserFactory(IColumnChooserFactory baseValue) => 
            baseValue ?? DefaultColumnChooserFactory.Instance;

        internal ColumnBase CoerceFocusedColumn(ColumnBase newValue) => 
            !this.RequestUIUpdate() ? ((ColumnBase) this.GetCoerceOldValue(this.GetFocusedColumnProperty())) : newValue;

        internal virtual int CoerceFocusedRowHandle(int newValue)
        {
            int focusedRowHandle;
            this.FocusedRowHandleChangedLocker.Lock();
            try
            {
                if (this.IsAutoFilterRowFocused && this.DataControl.IsDataResetLocked)
                {
                    focusedRowHandle = this.FocusedRowHandle;
                }
                else if (newValue == this.FocusedRowHandle)
                {
                    focusedRowHandle = this.FocusedRowHandle;
                }
                else if (!this.FocusedView.CommitEditing())
                {
                    this.SelectionStrategy.OnNavigationCanceled();
                    focusedRowHandle = this.FocusedRowHandle;
                }
                else if (this.ViewBehavior.CheckNavigationStyle(newValue))
                {
                    focusedRowHandle = -2147483648;
                }
                else if (newValue == -2147483646)
                {
                    focusedRowHandle = this.FocusedRowHandle;
                }
                else if ((this.DataControl == null) || (this.DataControl.DataSourceChangingLocker.IsLocked || (!this.IsSynchronizedWithCurrentItem || ((this.DataProviderBase.CollectionViewSource == null) || (this.IsNewItemRowHandle(newValue) || (this.DataProviderBase.IsGroupRowHandle(newValue) || !this.dataControl.IsSync))))))
                {
                    focusedRowHandle = newValue;
                }
                else
                {
                    object rowValue = this.GetRowValue(new RowHandle(newValue));
                    this.DataProviderBase.CollectionViewSource.MoveCurrentTo(rowValue);
                    focusedRowHandle = (rowValue == this.DataProviderBase.CollectionViewSource.CurrentItem) ? newValue : this.FocusedRowHandle;
                }
            }
            finally
            {
                this.FocusedRowHandleChangedLocker.Unlock();
            }
            return focusedRowHandle;
        }

        private static object CoerceFocusedRowHandle(DependencyObject d, object value) => 
            ((DataViewBase) d).CoerceFocusedRowHandle((int) value);

        private static object CoerceScrollStep(DependencyObject d, object value) => 
            (((int) value) >= 1) ? value : 1;

        private object CoerceTopRowIndex(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (((this.DataControl != null) && (this.ScrollInfoOwner != null)) && (value > this.DataControl.VisibleRowCount))
            {
                DevExpress.Xpf.Grid.ScrollingMode scrollingModeCore = this.ScrollingModeCore;
                if (scrollingModeCore == DevExpress.Xpf.Grid.ScrollingMode.Normal)
                {
                    return (this.DataControl.VisibleRowCount - 1);
                }
                if (scrollingModeCore == DevExpress.Xpf.Grid.ScrollingMode.Smart)
                {
                    return (this.DataControl.VisibleRowCount - this.ScrollInfoOwner.ItemsOnPage);
                }
            }
            return value;
        }

        private static object CoerceTopRowIndex(DependencyObject d, object value) => 
            ((DataViewBase) d).CoerceTopRowIndex((int) value);

        private static object CoerceWheelScrollLines(DependencyObject d, object value)
        {
            double num = (double) value;
            return (((num >= 0.0) || (num == -1.0)) ? value : SystemParameters.WheelScrollLines);
        }

        internal bool CollapseFocusedRowCore() => 
            !this.IsInvalidFocusedRowHandle && (this.IsExpanded(this.FocusedRowHandle) && this.ChangeVisibleRowExpand(this.FocusedRowHandle));

        protected internal virtual void ColumnCheckedChanged(ColumnBase column)
        {
            if ((column != null) && ((column.IsChecked != null) && ((this.DataControl != null) && (this.DataControl.DataProviderBase != null))))
            {
                string fName = column.FieldName;
                bool val = column.IsChecked.Value;
                this.AutoExpandAllGroupsLockAction(delegate {
                    Action <>9__1;
                    Action action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action local1 = <>9__1;
                        action = <>9__1 = delegate {
                            for (int i = 0; i < this.DataControl.DataProviderBase.DataRowCount; i++)
                            {
                                object cellValue = this.DataControl.GetCellValue(i, fName);
                                if ((cellValue as bool) && (((bool) cellValue) != val))
                                {
                                    this.DataControl.SetCellValueCore(i, fName, val);
                                    this.RaiseRowUpdatedBase(i);
                                }
                            }
                        };
                    }
                    this.DataControl.UpdateGroupRowCheckedLocker.DoLockedAction(action);
                    foreach (ColumnBase item in this.ShowCheckBoxInHeaderColumns)
                    {
                        if (!ReferenceEquals(item, column) && (item.ActualShowCheckBoxInHeader && (item.FieldName == column.FieldName)))
                        {
                            item.UpdateIsCheckedLocker.DoLockedAction<bool?>(delegate {
                                bool? nullable = new bool?(val);
                                item.IsChecked = nullable;
                                return nullable;
                            });
                        }
                    }
                });
            }
        }

        internal bool ColumnChooserIsKeyboardFocus() => 
            this.IsActualColumnChooserCreated && ((this.ActualColumnChooser.TopContainer != null) && this.ActualColumnChooser.TopContainer.IsKeyboardFocusWithin);

        internal void CommitAndCleanEditor()
        {
            this.CommitEditing(true, true);
        }

        public bool CommitEditing() => 
            this.CommitEditing(false, false);

        public bool CommitEditing(bool forceCommit, bool cleanError = false) => 
            this.EnumerateViewsForCommitEditingAndRequestUIUpdate(view => view.CommitEditingCore(forceCommit, cleanError));

        private bool CommitEditingCore(bool forceCommit, bool cleanError = false)
        {
            bool result = this.RequestUIUpdateCore(cleanError);
            if (!result && !forceCommit)
            {
                return result;
            }
            if (!result)
            {
                this.HideEditor(false);
            }
            if (!this.AutoScrollOnSorting)
            {
                this.ScrollIntoViewLocker.Lock();
            }
            this.CommitEditingLocker.DoLockedAction(delegate {
                result = result && this.ViewBehavior.EndRowEdit();
            });
            if (!this.AutoScrollOnSorting)
            {
                this.ScrollIntoViewLocker.Unlock();
            }
            if (!result)
            {
                return false;
            }
            CancelRoutedEventArgs e = new GridCancelRoutedEventArgs(this.DataControl, BeforeLayoutRefreshEvent);
            this.RaiseBeforeLayoutRefresh(e);
            return !e.Cancel;
        }

        protected virtual int? CompareGroupedColumns(BaseColumn x, BaseColumn y) => 
            null;

        protected internal bool ConvertCommandParameterToBool(object commandParameter)
        {
            bool flag = true;
            try
            {
                flag = Convert.ToBoolean(commandParameter);
            }
            catch
            {
            }
            return flag;
        }

        internal SearchStringToFilterCriteriaWrapper ConvertSearchStringToFilterCriteria(string searchString, CriteriaOperator filterCriteria, List<FieldAndHighlightingString> highlighting)
        {
            if (this.SearchStringToFilterCriteria != null)
            {
                SearchStringToFilterCriteriaEventArgs e = this.CreateSearchStringToFilterCriteriaEventArgs(searchString, this.IsVirtualSource ? null : filterCriteria, this.IsVirtualSource ? null : highlighting);
                this.SearchStringToFilterCriteria(this, e);
                return new SearchStringToFilterCriteriaWrapper(e.Filter, e.Highlighting, e.ApplyToColumnsFilter);
            }
            if (this.IsVirtualSource)
            {
                throw new NotSupportedException("The GridControl bound to the Virtual data source does not support the search panel feature out of the box." + Environment.NewLine + "Handle the DataViewBase.SearchStringToFilterCriteria event to apply searching manually.");
            }
            return new SearchStringToFilterCriteriaWrapper(filterCriteria, highlighting, false);
        }

        protected internal virtual int ConvertVisibleIndexToScrollIndex(int visibleIndex)
        {
            int scrollIndexWithDetails = 0;
            this.DataControl.EnumerateThisAndParentDataControls(delegate (DataControlBase dataControl, int index) {
                int scrollIndex = dataControl.DataProviderBase.ConvertVisibleIndexToScrollIndex(index, dataControl.DataView.AllowFixedGroupsCore);
                scrollIndexWithDetails += scrollIndex;
                scrollIndexWithDetails += dataControl.MasterDetailProvider.CalcVisibleDetailRowsCountBeforeRow(scrollIndex);
            }, visibleIndex);
            int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            return (scrollIndexWithDetails + this.DataControl.MasterDetailProvider.CalcVisibleDetailRowsCountForRow(rowHandleByVisibleIndexCore));
        }

        internal void CopyAllRowsToClipboardCore(IEnumerable<KeyValuePair<DataControlBase, int>> rows)
        {
            this.ClipboardController.CopyRowsToClipboard(rows);
        }

        [Obsolete("Use the DataControlBase.CopyCurrentItemToClipboard method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public virtual void CopyFocusedRowToClipboard()
        {
            this.CopyFocusedRowToClipboardCore();
        }

        internal void CopyFocusedRowToClipboardCore()
        {
            List<int> rows = new List<int>();
            if (!this.IsInvalidFocusedRowHandle)
            {
                rows.Add(this.FocusedRowHandle);
            }
            this.ClipboardController.CopyRowsToClipboard(rows);
        }

        [Obsolete("Use the DataControlBase.CopyRangeToClipboard method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void CopyRangeToClipboard(int startRowHandle, int endRowHandle)
        {
            this.CopyRangeToClipboardCore(startRowHandle, endRowHandle);
        }

        internal void CopyRangeToClipboardCore(int startRowHandle, int endRowHandle)
        {
            this.ClipboardController.CopyRangeToClipboard(startRowHandle, endRowHandle);
        }

        [Obsolete("Use the DataControlBase.CopyRowsToClipboard method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void CopyRowsToClipboard(IEnumerable<int> rows)
        {
            this.CopyRowsToClipboardCore(rows);
        }

        internal void CopyRowsToClipboardCore(IEnumerable<int> rows)
        {
            this.ClipboardController.CopyRowsToClipboard(rows);
        }

        [Obsolete("Use the DataControlBase.CopySelectedItemsToClipboard method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void CopySelectedRowsToClipboard()
        {
            this.CopySelectedRowsToClipboardCore();
        }

        internal void CopySelectedRowsToClipboardCore()
        {
            this.ClipboardController.CopyRowsToClipboard(this.GetSelectedRowHandlesCore());
        }

        [Obsolete("Use the DataControlBase.CopyToClipboard method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void CopyToClipboard()
        {
            this.SelectionStrategy.CopyToClipboard();
        }

        protected internal virtual Point CorrectDragIndicatorLocation(UIElement panel, Point point) => 
            this.ViewBehavior.CorrectDragIndicatorLocation(panel, point);

        private SelectionState CorrectSelectionState(SelectionState selectionState, bool isCellSelectionState)
        {
            if (!this.EnableSelectedRowAppearanceCore && (!isCellSelectionState || (this.GetActualSelectionMode() != MultiSelectMode.Cell)))
            {
                switch (selectionState)
                {
                    case SelectionState.Focused:
                    case SelectionState.Selected:
                    case SelectionState.FocusedAndSelected:
                        selectionState = SelectionState.None;
                        break;

                    default:
                        break;
                }
            }
            return selectionState;
        }

        protected internal virtual FrameworkElement CreateAdditionalElement() => 
            null;

        internal VirtualItemsEnumerator CreateAllRowsEnumerator() => 
            new SkipCollapsedGroupVirtualItemsEnumerator(this.RootNodeContainer);

        protected internal virtual Func<int, bool> CreateAnnotationFilterFitPredicate() => 
            !this.CanStartIncrementalSearch ? this.CreateFilterFitPredicate() : (((this.TextSearchEngineRoot == null) || string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText)) ? (((this.SearchControl == null) || string.IsNullOrEmpty(this.SearchControl.SearchText)) ? null : this.CreateFilterFitPredicate()) : ((this.IncrementalSearchColumns.Length != 0) ? this.CreateFilterFitPredicate(IncrementalSearchHelper.GetCriteriaOperator(this.IncrementalSearchColumns, FilterCondition.StartsWith, this.TextSearchEngineRoot.SeachText, CriteriaOperatorType.And)) : null));

        protected internal virtual CriteriaOperator CreateAutoFilterCriteria(string fieldName, AutoFilterCondition condition, object value) => 
            (condition != AutoFilterCondition.Equals) ? (!string.IsNullOrEmpty(value.ToString()) ? ((condition != AutoFilterCondition.Contains) ? this.CreateAutoFilterCriteriaLike(fieldName, value) : this.CreateAutoFilterCriteriaContains(fieldName, value)) : null) : this.CreateAutoFilterCriteriaEquals(fieldName, value, true);

        internal CriteriaOperator CreateAutoFilterCriteria(string fieldName, object value, ClauseType clauseType) => 
            ((this.ColumnsCore.Count == 0) || (this.ColumnsCore[fieldName] == null)) ? null : FilterClauseHelper.CreateAutoFilterCriteria(fieldName, value, clauseType, this);

        protected internal virtual CriteriaOperator CreateAutoFilterCriteria(string fieldName, AutoFilterCondition condition, object value, ClauseType clauseType) => 
            this.CreateAutoFilterCriteria(fieldName, value, clauseType);

        internal CriteriaOperator CreateAutoFilterCriteriaContains(string fieldName, object value)
        {
            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), str };
            return new FunctionOperator(FunctionOperatorType.Contains, operands);
        }

        internal CriteriaOperator CreateAutoFilterCriteriaCustom(string fieldName, object value, BinaryOperatorType type)
        {
            ColumnBase base2 = this.DataControl.ColumnsCore[fieldName];
            if (value == null)
            {
                return null;
            }
            object obj2 = null;
            obj2 = ((base2 == null) || (base2.ColumnFilterMode != ColumnFilterMode.DisplayText)) ? this.DataProviderBase.CorrectFilterValueType(base2.FieldType, value, null) : value;
            object columnValue = (obj2 != null) ? obj2 : value;
            if ((type == BinaryOperatorType.Equal) || (type == BinaryOperatorType.NotEqual))
            {
                if ((base2.ActualEditSettings is CheckEditSettings) && (base2.ColumnFilterMode == ColumnFilterMode.DisplayText))
                {
                    return ((type == BinaryOperatorType.Equal) ? this.CreateAutoFilterCriteriaEquals(fieldName, value, true) : !this.CreateAutoFilterCriteriaEquals(fieldName, value, true));
                }
                FilterColumn column = FilterClauseHelper.CreateFilterColumn(base2, true, false);
                if (base2.RoundDateTimeForColumnFilter && ((column != null) && SummaryItemTypeHelper.IsDateTime(column.ColumnType)))
                {
                    return ((type != BinaryOperatorType.Equal) ? !this.DataProviderBase.CalcColumnFilterCriteriaByValue(base2, columnValue) : this.DataProviderBase.CalcColumnFilterCriteriaByValue(base2, columnValue));
                }
            }
            return new BinaryOperator(fieldName, columnValue, type);
        }

        internal CriteriaOperator CreateAutoFilterCriteriaCustom(string fieldName, object value, FunctionOperatorType type)
        {
            ColumnBase column = this.ColumnsCore[fieldName];
            if (column == null)
            {
                return null;
            }
            object displayObject = value;
            if ((column.ActualEditSettings is CheckEditSettings) && (column.ColumnFilterMode == ColumnFilterMode.DisplayText))
            {
                displayObject = this.GetDisplayObject(value, column);
            }
            string str = displayObject.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), str };
            return new FunctionOperator(type, operands);
        }

        internal CriteriaOperator CreateAutoFilterCriteriaEquals(string fieldName, object value, bool equals = true)
        {
            ColumnBase column = this.DataControl.ColumnsCore[fieldName];
            if ((column.ActualEditSettings is CheckEditSettings) && (column.ColumnFilterMode == ColumnFilterMode.DisplayText))
            {
                value = this.GetDisplayObject(value, column);
            }
            return ((!column.RoundDateTimeForColumnFilter || ((this.DataProviderBase.Columns == null) || (this.DataProviderBase.Columns[fieldName] == null))) ? new BinaryOperator(fieldName, value, equals ? BinaryOperatorType.Equal : BinaryOperatorType.NotEqual) : this.DataProviderBase.CalcColumnFilterCriteriaByValue(column, value));
        }

        internal CriteriaOperator CreateAutoFilterCriteriaLike(string fieldName, object value)
        {
            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if ((str[0] != '_') && (str[0] != '%'))
            {
                CriteriaOperator[] operatorArray2 = new CriteriaOperator[] { new OperandProperty(fieldName), str };
                return new FunctionOperator(FunctionOperatorType.StartsWith, operatorArray2);
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), str.Substring(1) };
            return new FunctionOperator(FunctionOperatorType.Contains, operands);
        }

        internal abstract FrameworkElement CreateAutoFilterRowElement(RowData rowData);
        internal RowValidationError CreateCellValidationError(object errorContent, ErrorType errorType, int rowHandle, ColumnBase column, Exception exception = null) => 
            this.CreateCellValidationError(errorContent, errorContent.YieldToArray<object>(), errorType, rowHandle, column, exception);

        internal abstract RowValidationError CreateCellValidationError(object errorContent, object[] errors, ErrorType errorType, int rowHandle, ColumnBase column, Exception exception);
        internal abstract GridRowValidationEventArgs CreateCellValidationEventArgs(object source, object value, int rowHandle, ColumnBase column);
        protected abstract RowsClipboardController CreateClipboardController();
        private IColumnChooser CreateColumnChooser()
        {
            IColumnChooser columnChooser = this.ColumnChooserFactory.Create(this);
            NullColumnChooserException.CheckColumnChooserNotNull(columnChooser);
            columnChooser.ApplyState(this.RootView.ColumnChooserState);
            return columnChooser;
        }

        protected abstract DataViewCommandsBase CreateCommandsContainer();
        protected internal virtual IDataAwareExportHelper CreateDataAwareExportHelper(ExportTarget exportTarget, IDataAwareExportOptions options) => 
            null;

        protected abstract DataIteratorBase CreateDataIterator();
        internal virtual IDataUpdateAnimationProvider CreateDataUpdateAnimationProvider() => 
            new EmptyDataUpdateAnimationProvider();

        protected internal virtual IDragElement CreateDragElement(BaseGridHeader columnHeader, Point offset) => 
            new ColumnHeaderDragElement(columnHeader, offset);

        internal virtual DragManagerBuilder CreateDragManagerBuilder()
        {
            DragManagerBuilder builder1 = new DragManagerBuilder();
            builder1.CreateDragEventFactory = () => new GridDragDataTransferEventFactory(this);
            builder1.GetDropMarkerTemplate = () => this.DropMarkerTemplate;
            builder1.GetDragDropHintTemplate = () => this.DragDropHintTemplate;
            builder1.ShowDragDropHint = this.ShowDragDropHint;
            builder1.GetShowTargetInfoInDragDropHint = () => this.ShowTargetInfoInDragDropHint;
            DragManagerBuilder builder = builder1;
            builder.CreateDropTargetValidatorFactory = () => new DropTargetValidatorFactory(activeDragInfo => new GridInternalDragValidator<DataViewBase>(this, activeDragInfo), () => new GridExternalDragValidator<DataViewBase>(this));
            if (this.AllowScrollingOnDrag)
            {
                builder.CreateScrollService = () => new GridDragScrollService(this);
            }
            return builder;
        }

        protected internal virtual IEditFormManager CreateEditFormManager() => 
            EmptyEditFormManager.Instance;

        protected internal virtual IEditFormOwner CreateEditFormOwner() => 
            null;

        protected internal virtual IDropTarget CreateEmptyBandDropTarget() => 
            new RemoveBandDropTarget();

        internal abstract IColumnCollection CreateEmptyColumnCollection();
        protected internal virtual IDropTarget CreateEmptyDropTarget() => 
            new RemoveColumnDropTarget();

        protected internal abstract PopupBaseEdit CreateExcelColumnFilterPopupEditor();
        protected internal virtual GridFilterColumn CreateFilterColumn(ColumnBase column, bool useDomainDataSourceRestrictions, bool useWcfSource) => 
            new GridFilterColumn(column, useDomainDataSourceRestrictions, useWcfSource);

        protected internal virtual Func<int, bool> CreateFilterFitPredicate() => 
            ((this.SearchControl == null) || Equals(this.SearchControl.FilterCriteria, null)) ? null : this.CreateFilterFitPredicate(this.SearchControl.FilterCriteria);

        protected internal virtual Func<int, bool> CreateFilterFitPredicate(CriteriaOperator criteria)
        {
            if (Equals(criteria, null))
            {
                return null;
            }
            DevExpress.Data.DataController.FilterRowStub stub = null;
            try
            {
                BaseRowStub.CachingCriteriaCompilerDescriptor descriptor = new BaseRowStub.CachingCriteriaCompilerDescriptor(new BaseRowStub.DataControllerCriteriaCompilerDescriptor(this.DataProviderBase.DataController), criteria);
                Func<BaseRowStub, bool> func2 = CriteriaCompiler.ToPredicate<BaseRowStub>(criteria, descriptor);
                stub = new DevExpress.Data.DataController.FilterRowStub(this.DataProviderBase.DataController, func2, descriptor.GetStubCleanUpCode());
            }
            catch
            {
                return null;
            }
            return delegate (int rowHandle) {
                bool flag;
                if (this.DataProviderBase.IsGroupRowHandle(rowHandle))
                {
                    GroupTextHighlightingProperties groupHighlightingProperties = this.GetGroupHighlightingProperties(rowHandle);
                    if ((groupHighlightingProperties == null) || (groupHighlightingProperties.TextHighlightingProperties == null))
                    {
                        return false;
                    }
                    object groupDisplayValue = this.GetGroupDisplayValue(rowHandle);
                    return ((groupDisplayValue != null) && groupDisplayValue.ToString().ToLower().EndsWith(groupHighlightingProperties.TextHighlightingProperties.Text.ToLower()));
                }
                stub.GoTo(this.DataProviderBase.DataController.GetListSourceRowIndex(rowHandle));
                try
                {
                    flag = stub.Filter();
                }
                finally
                {
                    stub.Reset();
                }
                return flag;
            };
        }

        protected virtual RowData CreateFocusedRowData() => 
            new StandaloneRowData(this.VisualDataTreeBuilder, true, true);

        protected internal virtual FrameworkElement CreateHeaderAdditionalElement() => 
            null;

        protected internal virtual FrameworkElement CreateLoadingRowElement(RowData rowData) => 
            null;

        protected Lazy<BarManagerMenuController> CreateMenuControllerLazyValue() => 
            new Lazy<BarManagerMenuController>(() => GridMenuInfoBase.CreateMenuController(this.DataControlMenu));

        protected GridViewNavigationBase CreateNavigation()
        {
            switch (this.RootView.NavigationStyle)
            {
                case GridViewNavigationStyle.Row:
                    return this.ViewBehavior.CreateRowNavigation();

                case GridViewNavigationStyle.Cell:
                    return this.ViewBehavior.CreateCellNavigation();

                case GridViewNavigationStyle.None:
                    return new DummyNavigation(this);
            }
            return null;
        }

        internal abstract FrameworkElement CreateNewItemRowElement(RowData rowData);
        private ClipboardOptions CreateOptionsClipboard()
        {
            ClipboardOptions options = new ClipboardOptions(true);
            this.SetActualClipboardOptions(options);
            return options;
        }

        internal abstract DataControlPopupMenu CreatePopupMenu();
        protected internal abstract PrintingDataTreeBuilderBase CreatePrintingDataTreeBuilder(double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy, MasterDetailPrintInfo masterDetailPrintInfo, BandsLayoutBase bandsLayout);
        protected abstract IRootDataNode CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize);
        protected abstract void CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize);
        internal abstract FrameworkElement CreateRowElement(RowData rowData);
        protected internal virtual FrameworkElement CreateRowsPanelAdditionalElement() => 
            null;

        protected internal virtual FrameworkElement CreateRowsPanelBackgroundContainer() => 
            null;

        internal abstract BaseValidationError CreateRowValidationError(object errorContent, object[] errors, ErrorType errorType, int rowHandle);
        protected internal virtual FrameworkElement CreateScrollBarAdditionalElement() => 
            null;

        protected virtual SearchStringToFilterCriteriaEventArgs CreateSearchStringToFilterCriteriaEventArgs(string searchString, CriteriaOperator filterCriteria, List<FieldAndHighlightingString> highlighting) => 
            new SearchStringToFilterCriteriaEventArgs(this.DataControl, searchString, null, null);

        protected internal abstract GridSelectionChangedEventArgs CreateSelectionChangedEventArgs(DevExpress.Data.SelectionChangedEventArgs e);
        protected abstract SelectionStrategyBase CreateSelectionStrategy();
        protected internal virtual FrameworkElement CreateSplitterAdditionalElement() => 
            null;

        protected abstract DataViewBehavior CreateViewBehavior();
        internal GridRowsEnumerator CreateVisibleRowsEnumerator() => 
            new SkipInvisibleGridRowsEnumerator(this, this.RootNodeContainer);

        internal void CurrentColumnChanged(ColumnBase oldColumn)
        {
            this.SelectionStrategy.OnFocusedColumnChanged();
            this.UpdateIsFocusedCellIfNeeded(this.FocusedRowHandle, oldColumn);
            this.UpdateIsFocusedCellIfNeeded(this.FocusedRowHandle, this.DataControl.CurrentColumn);
            this.UpdateFullRowState(this.FocusedRowHandle);
            this.ForceUpdateRowsState();
            this.ScrollToCurrentColumnIfNeeded();
        }

        internal virtual void CurrentRowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
        {
            if ((this.DataControl != null) && this.DataControl.VisibleItemsCreated)
            {
                this.DataProviderBase.UpdateVisibleItems(changedType, newHandle, oldRowHandle);
            }
            if (changedType != ListChangedType.ItemDeleted)
            {
                if (this.NeedErrorWatchCurrentRowChanged(changedType))
                {
                    this.ErrorWatch.CurrentRowChanged(changedType, newHandle, oldRowHandle);
                }
                GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
                if (searchControl == null)
                {
                    GridSearchControlBase local1 = searchControl;
                }
                else
                {
                    searchControl.SearchInfoChanged(changedType, newHandle, oldRowHandle);
                }
            }
            this.UpdateSearchResult(true);
            foreach (ColumnBase base2 in this.ShowCheckBoxInHeaderColumns)
            {
                base2.UpdateIsChecked(false);
            }
            if (!string.IsNullOrEmpty(this.GroupRowCheckBoxFieldNameCore))
            {
                if (oldRowHandle != null)
                {
                    this.DataControl.UpdateGroupRowChecked(oldRowHandle.Value, this.GroupRowCheckBoxFieldNameCore);
                }
                if (newHandle != -2147483648)
                {
                    this.DataControl.UpdateGroupRowChecked(newHandle, this.GroupRowCheckBoxFieldNameCore);
                }
            }
        }

        internal virtual void CurrentRowRemoving(int rowHandle)
        {
            this.ErrorWatch.CurrentRowChanged(ListChangedType.ItemDeleted, rowHandle, new int?(rowHandle));
            GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
            if (searchControl == null)
            {
                GridSearchControlBase local1 = searchControl;
            }
            else
            {
                searchControl.SearchInfoChanged(ListChangedType.ItemDeleted, rowHandle, new int?(rowHandle));
            }
        }

        private void DataViewBase_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            IInputElement focusedElement = Keyboard.FocusedElement;
            if (ReferenceEquals(focusedElement, this) || ReferenceEquals(focusedElement, this.DataPresenter))
            {
                e.Handled = true;
            }
        }

        private void DataViewBase_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                this.WidthChanged(e.PreviousSize.Width, e.NewSize.Width, false);
            }
            this.UpdateRowRectangleHelper.UpdatePosition(this);
        }

        internal void DeleteFocusedRow()
        {
            if (this.CanDeleteFocusedRow())
            {
                this.DeleteRowCore(this.FocusedRowHandle);
            }
        }

        internal virtual void DeleteFocusedRowCommand()
        {
            this.DeleteFocusedRow();
        }

        protected internal virtual void DeleteRowCore(int rowHandle)
        {
            if ((this.IsNewItemRowFocused && this.IsFocusedRowModified) || (rowHandle == -2147483647))
            {
                this.CancelEditFocusedRow();
            }
            else
            {
                int rowVisibleIndexByHandleCore = this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle);
                this.FocusedRowHandleChangedLocker.DoLockedAction(() => this.DataProviderBase.DeleteRow(new RowHandle(rowHandle)));
                int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(rowVisibleIndexByHandleCore);
                if (rowHandleByVisibleIndexCore == -2147483648)
                {
                    rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(this.DataControl.VisibleRowCount - 1);
                }
                this.SetFocusedRowHandle(rowHandleByVisibleIndexCore);
            }
        }

        void ILogicalOwner.AddChild(object child)
        {
            this.AddChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            this.RemoveChild(child);
        }

        bool IColumnOwnerBase.AllowFilterColumn(ColumnBase column) => 
            (column != null) && ((this.DataControl != null) && this.DataControl.DataProviderBase.CanFilter);

        bool IColumnOwnerBase.AllowSortColumn(ColumnBase column) => 
            (column != null) ? this.CanSortColumn(column, column.FieldName) : false;

        void IColumnOwnerBase.ApplyColumnVisibleIndex(BaseColumn column, int oldVisibleIndex)
        {
            this.ApplyColumnVisibleIndex(column, oldVisibleIndex);
        }

        void IColumnOwnerBase.CalcColumnsLayout()
        {
            if (!this.IsLockUpdateColumnsLayout && !this.ColumnsCore.IsLockUpdate)
            {
                this.RebuildColumns();
                Action<ColumnBase> updateColumnDelegate = <>c.<>9__1686_0;
                if (<>c.<>9__1686_0 == null)
                {
                    Action<ColumnBase> local1 = <>c.<>9__1686_0;
                    updateColumnDelegate = <>c.<>9__1686_0 = delegate (ColumnBase column) {
                        column.UpdateActualAllowResizing();
                        column.UpdateActualAllowMoving();
                    };
                }
                this.UpdateColumns(updateColumnDelegate);
                if ((this.DataControl != null) && (this.DataControl.BandsLayoutCore != null))
                {
                    Action<BandBase> action = <>c.<>9__1686_1;
                    if (<>c.<>9__1686_1 == null)
                    {
                        Action<BandBase> local2 = <>c.<>9__1686_1;
                        action = <>c.<>9__1686_1 = delegate (BandBase b) {
                            b.UpdateActualAllowResizing();
                            b.UpdateActualAllowMoving();
                        };
                    }
                    this.DataControl.BandsLayoutCore.ForeachBand(action);
                    ITableView view = this as ITableView;
                    if (view != null)
                    {
                        view.OnChangeBandSeparator();
                    }
                }
            }
        }

        bool IColumnOwnerBase.CanClearColumnFilter(ColumnBase column) => 
            this.CanClearColumnFilter(column);

        void IColumnOwnerBase.ChangeColumnSortOrder(ColumnBase column)
        {
            this.OnColumnHeaderClick(column);
        }

        void IColumnOwnerBase.ClearBindingValues(ColumnBase column)
        {
            this.UpdateRowData(rowData => rowData.ClearBindingValues(column), true, true);
        }

        void IColumnOwnerBase.ClearColumnFilter(ColumnBase column)
        {
            this.DataControl.ClearColumnFilter(column);
        }

        BaseEditSettings IColumnOwnerBase.CreateDefaultEditSettings(IDataColumnInfo column) => 
            GridColumnTypeHelper.CreateEditSettings(column.FieldType);

        Style IColumnOwnerBase.GetActualCellStyle(ColumnBase column) => 
            this.ViewBehavior.GetActualCellStyle(column);

        DataTemplate IColumnOwnerBase.GetActualCellTemplate() => 
            this.CellTemplate;

        ColumnBase IColumnOwnerBase.GetColumn(string fieldName) => 
            this.ColumnsCore[fieldName];

        Type IColumnOwnerBase.GetColumnType(ColumnBase column, DevExpress.Xpf.Data.DataProviderBase dataProvider) => 
            this.GetColumnType(column, dataProvider);

        HorizontalAlignment IColumnOwnerBase.GetDefaultColumnAlignment(ColumnBase column)
        {
            Type columnType = this.GetColumnType(column, null);
            return (((columnType == null) || (columnType.IsEnum || !DefaultColumnAlignmentHelper.IsColumnFarAlignedByDefault(columnType))) ? HorizontalAlignment.Left : HorizontalAlignment.Right);
        }

        IList<DevExpress.Xpf.Grid.SummaryItemBase> IColumnOwnerBase.GetTotalSummaryItems(ColumnBase column) => 
            column.TotalSummariesCore;

        object IColumnOwnerBase.GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item) => 
            this.DataControl.GetTotalSummaryValue(item);

        void IColumnOwnerBase.GroupColumn(string fieldName, int index, ColumnSortOrder sortOrder)
        {
            this.GroupColumn(fieldName, index, sortOrder);
        }

        void IColumnOwnerBase.RebuildColumnChooserColumns()
        {
            this.RebuildColumnChooserColumns();
        }

        void IColumnOwnerBase.UngroupColumn(string fieldName)
        {
            this.UngroupColumn(fieldName);
        }

        void IColumnOwnerBase.UpdateCellDataValues()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1697_0;
            if (<>c.<>9__1697_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1697_0;
                updateMethod = <>c.<>9__1697_0 = delegate (RowData rowData) {
                    rowData.UpdateRow();
                    rowData.UpdateCellDataValues();
                };
            }
            this.UpdateRowData(updateMethod, true, true);
        }

        void IColumnOwnerBase.UpdateContentLayout()
        {
            this.UpdateContentLayout();
        }

        void IColumnOwnerBase.UpdateShowEditFilterButton(bool newAllowColumnFilterEditor, bool oldAllowColumnFilterEditor)
        {
            if (this.DataControl != null)
            {
                this.DataControl.UpdateColumnFilteringCounters(new bool?(newAllowColumnFilterEditor), new bool?(oldAllowColumnFilterEditor));
                this.UpdateShowEditFilterButtonCore();
            }
        }

        object IConvertClonePropertyValue.ConvertClonePropertyValue(string propertyName, object sourceValue, DependencyObject destinationObject)
        {
            if (propertyName != "FocusedColumn")
            {
                return sourceValue;
            }
            if (sourceValue == null)
            {
                return null;
            }
            ColumnBase item = (ColumnBase) sourceValue;
            DataViewBase base3 = (DataViewBase) destinationObject;
            return ((base3.DataControl != null) ? CloneDetailHelper.SafeGetDependentCollectionItem<ColumnBase>(item, item.View.ColumnsCore, base3.ColumnsCore) : null);
        }

        IRootDataNode IPrintableControl.CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize) => 
            this.CreateRootNode(usablePageSize, reportHeaderSize, reportFooterSize, pageHeaderSize, pageFooterSize);

        void IPrintableControl.CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
        {
            this.CreateRootNodeAsync(usablePageSize, reportHeaderSize, reportFooterSize, pageHeaderSize, pageFooterSize);
        }

        IVisualTreeWalker IPrintableControl.GetCustomVisualTreeWalker() => 
            null;

        void IPrintableControl.PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters)
        {
            this.PagePrintedCallback(pageBrickEnumerator, brickUpdaters);
        }

        protected virtual MessageBoxResult DisplayInvalidRowError(IInvalidRowExceptionEventArgs e)
        {
            string invalidRowErrorText = this.GetInvalidRowErrorText(e.ErrorText);
            string windowCaption = e.WindowCaption;
            Window owner = this.DataControl.Window;
            return ((owner == null) ? MessageBox.Show(invalidRowErrorText, windowCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand) : MessageBox.Show(owner, invalidRowErrorText, windowCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand));
        }

        internal virtual void EditFocusedRow()
        {
            if (this.CanEditFocusedRow())
            {
                this.ShowEditor();
            }
        }

        private void EnableSelectedRowAppearanceChanged()
        {
            this.EnableSelectedRowAppearanceCore = this.EnableSelectedRowAppearance;
            UpdateRowDataDelegate updateMethod = <>c.<>9__1395_0;
            if (<>c.<>9__1395_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1395_0;
                updateMethod = <>c.<>9__1395_0 = x => x.UpdateSelectionState();
            }
            this.UpdateRowData(updateMethod, true, true);
        }

        internal void EndEditFocusedRow()
        {
            if (this.CanEndEditFocusedRow())
            {
                this.CommitEditing();
            }
        }

        internal void EndSelectionCore()
        {
            this.SelectionStrategy.EndSelection();
        }

        internal void EndUpdateColumnsLayout()
        {
            this.EndUpdateColumnsLayout(true);
        }

        internal virtual void EndUpdateColumnsLayout(bool calcLayout)
        {
            this.updateColumnsLayoutLocker.Unlock();
            this.updateColumnsLayoutLocker.DoIfNotLocked(delegate {
                if (calcLayout)
                {
                    ((IColumnOwnerBase) this).CalcColumnsLayout();
                }
            });
        }

        protected internal void EnqueueImmediateAction(IAction action)
        {
            this.ImmediateActionsManager.EnqueueAction(action);
        }

        protected internal void EnqueueImmediateAction(Action action)
        {
            this.ImmediateActionsManager.EnqueueAction(action);
        }

        private void EnqueueMakeCellVisible()
        {
            if (this.RootDataPresenter != null)
            {
                DataViewBehavior viewBehavior = this.ViewBehavior;
                this.ImmediateActionsManager.EnqueueAction(new Action(viewBehavior.MakeCellVisible));
            }
        }

        private void EnqueueScrollIntoView(int rowHandle, bool useLock = false, Action postAction = null)
        {
            ScrollRowIntoViewAction action = this.ImmediateActionsManager.FindActionOfType(typeof(ScrollRowIntoViewAction)) as ScrollRowIntoViewAction;
            if (action != null)
            {
                action.Reassign(this, rowHandle);
                action.UseLock = useLock;
                action.PostAction = postAction;
            }
            else
            {
                ScrollRowIntoViewAction action1 = new ScrollRowIntoViewAction(this, rowHandle, 0);
                action1.UseLock = useLock;
                action1.PostAction = postAction;
                this.EnqueueImmediateAction(action1);
            }
        }

        private bool EnumerateViewsForCommitEditingAndRequestUIUpdate(Func<DataViewBase, bool> getResult)
        {
            bool flag = getResult(this);
            if (!ReferenceEquals(this, this.MasterRootRowsContainer.FocusedView))
            {
                flag &= getResult(this.MasterRootRowsContainer.FocusedView);
            }
            return flag;
        }

        private void ErrorsWatchModeChanged()
        {
            this.ErrorsWatchModeCore = this.ErrorsWatchMode;
            this.UpdateWatchErrors();
        }

        internal bool ExpandFocusedRowCore() => 
            !this.IsInvalidFocusedRowHandle && (!this.IsExpanded(this.FocusedRowHandle) && this.ChangeVisibleRowExpand(this.FocusedRowHandle));

        public abstract void ExportToCsv(Stream stream);
        public abstract void ExportToCsv(string filePath);
        public abstract void ExportToCsv(Stream stream, CsvExportOptions options);
        public abstract void ExportToCsv(string filePath, CsvExportOptions options);
        public void ExportToDocx(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToDocx(this, stream);
        }

        public void ExportToDocx(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToDocx(this, filePath);
        }

        public void ExportToDocx(Stream stream, DocxExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToDocx(this, stream, options);
        }

        public void ExportToDocx(string filePath, DocxExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToDocx(this, filePath, options);
        }

        public void ExportToHtml(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToHtml(this, stream);
        }

        public void ExportToHtml(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToHtml(this, filePath);
        }

        public void ExportToHtml(Stream stream, HtmlExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToHtml(this, stream, options);
        }

        public void ExportToHtml(string filePath, HtmlExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToHtml(this, filePath, options);
        }

        public void ExportToImage(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToImage(this, stream);
        }

        public void ExportToImage(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToImage(this, filePath);
        }

        public void ExportToImage(Stream stream, ImageExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToImage(this, stream, options);
        }

        public void ExportToImage(string filePath, ImageExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToImage(this, filePath, options);
        }

        public void ExportToMht(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToMht(this, stream);
        }

        public void ExportToMht(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToMht(this, filePath);
        }

        public void ExportToMht(Stream stream, MhtExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToMht(this, stream, options);
        }

        public void ExportToMht(string filePath, MhtExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToMht(this, filePath, options);
        }

        public void ExportToPdf(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToPdf(this, stream);
        }

        public void ExportToPdf(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToPdf(this, filePath);
        }

        public void ExportToPdf(Stream stream, PdfExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToPdf(this, stream, options);
        }

        public void ExportToPdf(string filePath, PdfExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToPdf(this, filePath, options);
        }

        public void ExportToRtf(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToRtf(this, stream);
        }

        public void ExportToRtf(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToRtf(this, filePath);
        }

        public void ExportToRtf(Stream stream, RtfExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToRtf(this, stream, options);
        }

        public void ExportToRtf(string filePath, RtfExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToRtf(this, filePath, options);
        }

        public void ExportToText(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToText(this, stream);
        }

        public void ExportToText(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToText(this, filePath);
        }

        public void ExportToText(Stream stream, TextExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToText(this, stream, options);
        }

        public void ExportToText(string filePath, TextExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToText(this, filePath, options);
        }

        public abstract void ExportToXls(Stream stream);
        public abstract void ExportToXls(string filePath);
        public abstract void ExportToXls(Stream stream, XlsExportOptions options);
        public abstract void ExportToXls(string filePath, XlsExportOptions options);
        public abstract void ExportToXlsx(Stream stream);
        public abstract void ExportToXlsx(string filePath);
        public abstract void ExportToXlsx(Stream stream, XlsxExportOptions options);
        public abstract void ExportToXlsx(string filePath, XlsxExportOptions options);
        public void ExportToXps(Stream stream)
        {
            this.CheckExport();
            PrintHelper.ExportToXps(this, stream);
        }

        public void ExportToXps(string filePath)
        {
            this.CheckExport();
            PrintHelper.ExportToXps(this, filePath);
        }

        public void ExportToXps(Stream stream, XpsExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToXps(this, stream, options);
        }

        public void ExportToXps(string filePath, XpsExportOptions options)
        {
            this.CheckExport();
            PrintHelper.ExportToXps(this, filePath, options);
        }

        private void filterControlContainer_Hidden(object sender, RoutedEventArgs e)
        {
            this.FilterControlContainer.Hidden -= new RoutedEventHandler(this.filterControlContainer_Hidden);
            this.FilterControlContainer = null;
        }

        internal void FinalizeClonedDetail()
        {
            this.SelectionStrategy = null;
            this.Navigation = null;
        }

        private void FindCommonViewVisibleIndexes(DataViewBase targetView, int targetVisibleIndex, out KeyValuePair<DataViewBase, int> commonCurrentItem, out KeyValuePair<DataViewBase, int> commonTargetItem)
        {
            List<KeyValuePair<DataViewBase, int>> viewVisibleIndexChain = this.DataControl.GetViewVisibleIndexChain(this.DataProviderBase.CurrentIndex);
            if (viewVisibleIndexChain.Count == 0)
            {
                viewVisibleIndexChain = this.DataControl.GetViewVisibleIndexChain(targetVisibleIndex);
            }
            if (targetView == null)
            {
                commonCurrentItem = commonTargetItem = viewVisibleIndexChain[0];
            }
            else
            {
                List<KeyValuePair<DataViewBase, int>> list2 = targetView.DataControl.GetViewVisibleIndexChain(targetVisibleIndex);
                commonCurrentItem = viewVisibleIndexChain[0];
                commonTargetItem = list2[0];
                foreach (KeyValuePair<DataViewBase, int> pair2 in viewVisibleIndexChain)
                {
                    foreach (KeyValuePair<DataViewBase, int> pair3 in list2)
                    {
                        if (pair2.Key == pair3.Key)
                        {
                            commonCurrentItem = pair2;
                            commonTargetItem = pair3;
                            break;
                        }
                    }
                }
            }
        }

        protected internal DependencyObject FindNavigationIndex(int minIndex, int maxIndex, bool findMin, bool isTabNavigation)
        {
            DependencyObject obj2 = null;
            int num2 = findMin ? 0x7fffffff : -2147483648;
            DataViewBase rootView = this.RootView;
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(this.FocusedRowElement);
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    DependencyObject current = enumerator.Current;
                    if (current != null)
                    {
                        Predicate<DependencyObject> predicate = <>c.<>9__1463_0;
                        if (<>c.<>9__1463_0 == null)
                        {
                            Predicate<DependencyObject> local1 = <>c.<>9__1463_0;
                            predicate = <>c.<>9__1463_0 = el => el is DataViewBase;
                        }
                        if (ReferenceEquals(rootView, LayoutHelper.FindLayoutOrVisualParentObject(rootView, predicate, false, null)))
                        {
                            int navigationIndex = ColumnBase.GetNavigationIndex(current);
                            bool flag = (current is FrameworkElement) && UIElementHelper.IsVisibleInTree((FrameworkElement) current);
                            if (((navigationIndex == -2147483648) || ((navigationIndex < minIndex) || ((navigationIndex > maxIndex) || (!flag || ((navigationIndex < this.VisibleColumnsCore.Count) && !this.IsColumnNavigatable(this.VisibleColumnsCore[navigationIndex], isTabNavigation)))))) || (!((num2 > navigationIndex) & findMin) && ((num2 >= navigationIndex) || findMin)))
                            {
                                continue;
                            }
                            obj2 = current;
                            num2 = navigationIndex;
                            if ((!findMin || (num2 != minIndex)) && (findMin || (num2 != maxIndex)))
                            {
                                continue;
                            }
                        }
                    }
                }
                return obj2;
            }
        }

        protected internal DependencyObject FindNearLeftNavigationIndex(int currIndex, bool isTabNavigation) => 
            this.FindNavigationIndex(0, currIndex - 1, false, isTabNavigation);

        protected internal DependencyObject FindNearRightNavigationIndex(int currIndex, bool isTabNavigation) => 
            this.FindNavigationIndex(currIndex + 1, 0x7fffffff, true, isTabNavigation);

        protected internal static DependencyObject FindParentCell(DependencyObject obj)
        {
            DependencyObject dependencyObject = obj;
            while ((dependencyObject != null) && (ColumnBase.GetNavigationIndex(dependencyObject) == -2147483648))
            {
                if (dependencyObject is DataViewBase)
                {
                    return null;
                }
                dependencyObject = LayoutHelper.GetParent(dependencyObject, false);
            }
            return dependencyObject;
        }

        protected internal static DependencyObject FindParentRow(DependencyObject obj)
        {
            DependencyObject element = obj;
            while ((element != null) && (GetRowHandle(element) == null))
            {
                if (element is DataViewBase)
                {
                    return null;
                }
                element = LayoutHelper.GetParent(element, false);
            }
            return element;
        }

        protected internal virtual int FindRowHandle(DependencyObject element)
        {
            DependencyObject obj2 = FindParentRow(element);
            return ((obj2 != null) ? GetRowHandle(obj2).Value : -2147483648);
        }

        protected void FocusSearchPanel()
        {
            if (!this.AreUpdateRowButtonsShown)
            {
                if (this.SearchControl == null)
                {
                    if (this.ShowSearchPanelMode != DevExpress.Xpf.Grid.ShowSearchPanelMode.Never)
                    {
                        this.PostponedSearchControlFocus = true;
                    }
                }
                else if (!this.SearchControl.GetIsKeyboardFocusWithin())
                {
                    this.SearchControl.Focus();
                }
            }
        }

        internal void FocusViewAndRow(DataViewBase view, int rowHandle)
        {
            if (ReferenceEquals(this.MasterRootRowsContainer.FocusedView, view) || this.MasterRootRowsContainer.FocusedView.CommitEditing())
            {
                this.MasterRootRowsContainer.FocusedView = view;
                view.SetFocusedRowHandle(rowHandle);
            }
        }

        protected virtual DefaultColumnChooserState ForceCreateColumnChooserState() => 
            new DefaultColumnChooserState();

        protected void ForceCreateColumnChooserStateInternal()
        {
            if (this.ColumnChooserState == null)
            {
                this.applyColumnChooserStateLocker.Lock();
                this.ColumnChooserState = this.ForceCreateColumnChooserState();
                this.applyColumnChooserStateLocker.Unlock();
            }
        }

        internal void ForceDestroyColumnChooser()
        {
            this.columnChooserForceDestoyLocker.DoLockedAction(() => this.ActualColumnChooser = null);
        }

        protected internal virtual void ForceLayout()
        {
            base.InvalidateMeasure();
        }

        protected internal void ForceUpdateRowsState()
        {
            this.RowsStateDirty = true;
            this.UpdateRowsState();
        }

        protected internal virtual string FormatSummaryItemCaptionInSummaryEditor(ISummaryItem item, string defaultCaption) => 
            defaultCaption;

        protected DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters GetActualAllowedGroupFilters() => 
            (this.AllowedGroupFilters == null) ? (this.IsVirtualSource ? DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.None : DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.All) : this.AllowedGroupFilters.Value;

        internal GridSummaryCalculationMode GetActualCalculationMode(DevExpress.Xpf.Grid.SummaryItemBase summary) => 
            ((summary == null) || (summary.CalculationModeCore == null)) ? this.SummaryCalculationMode : summary.CalculationModeCore.Value;

        internal MultiSelectMode GetActualSelectionMode() => 
            ((this.DataControl == null) || (this.DataControl.SelectionMode == MultiSelectMode.None)) ? this.GetSelectionMode() : this.DataControl.SelectionMode;

        private bool GetActualShowFilterPanel() => 
            (this.ShowFilterPanelMode != DevExpress.Xpf.Grid.ShowFilterPanelMode.Never) ? ((this.ShowFilterPanelMode != DevExpress.Xpf.Grid.ShowFilterPanelMode.ShowAlways) ? (this.DataControl.FilterCriteria != null) : true) : false;

        private AdornerLayer GetAdornerLayerTopContainer()
        {
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(this.RootView);
            return ((topContainerWithAdornerLayer != null) ? AdornerLayer.GetAdornerLayer(topContainerWithAdornerLayer) : null);
        }

        internal virtual AllowedDataAnalysisFilters? GetAllowedDataAnalysisFilters(ColumnBase column) => 
            (this.DataProviderBase.IsServerMode || (this.DataProviderBase.IsAsyncServerMode || ((column != null) && (column.ColumnFilterMode == ColumnFilterMode.DisplayText)))) ? ((AllowedDataAnalysisFilters?) 0) : ((AllowedDataAnalysisFilters?) 0x3f);

        protected internal bool GetAllowGroupingSortingBySingleColumnOnly()
        {
            bool? allowGroupingSortingBySingleColumnOnlyCore = this.GetAllowGroupingSortingBySingleColumnOnlyCore();
            return ((allowGroupingSortingBySingleColumnOnlyCore == null) ? this.IsVirtualSource : allowGroupingSortingBySingleColumnOnlyCore.Value);
        }

        protected virtual bool? GetAllowGroupingSortingBySingleColumnOnlyCore() => 
            false;

        protected internal virtual object GetAutoFilterValue(ColumnBase column, CriteriaOperator op) => 
            column.GetAutoFilterValue(op);

        protected virtual AutomationPeer GetAutomationPeer(DependencyObject obj) => 
            ((this.NavigationStyle == GridViewNavigationStyle.Row) || this.IsGroupRowHandleCore(this.FocusedRowHandle)) ? this.DataControl.AutomationPeer.GetRowPeer(this.FocusedRowHandle) : (((this.NavigationStyle != GridViewNavigationStyle.Cell) || (this.dataControl.CurrentColumn == null)) ? null : this.DataControl.AutomationPeer.GetCellPeer(this.FocusedRowHandle, this.DataControl.CurrentColumn, false));

        protected abstract bool GetCanCreateRootNodeAsync();
        public FrameworkElement GetCellElementByMouseEventArgs(MouseEventArgs e) => 
            this.GetCellElementByTreeElement((DependencyObject) e.OriginalSource);

        public FrameworkElement GetCellElementByRowHandleAndColumn(int rowHandle, ColumnBase column) => 
            this.GetCellElementByRowHandleAndColumnCore(rowHandle, column);

        internal FrameworkElement GetCellElementByRowHandleAndColumnCore(int rowHandle, ColumnBase column)
        {
            FrameworkElement rowElementByRowHandle = this.GetRowElementByRowHandle(rowHandle);
            return ((rowElementByRowHandle != null) ? LayoutHelper.FindElement(rowElementByRowHandle, element => (element is IGridCellEditorOwner) && (ReferenceEquals(((IGridCellEditorOwner) element).AssociatedColumn, column) && (element.Visibility == Visibility.Visible))) : null);
        }

        public FrameworkElement GetCellElementByTreeElement(DependencyObject d) => 
            (FrameworkElement) LayoutHelper.FindLayoutOrVisualParentObject<IGridCellEditorOwner>(d, false, null);

        protected virtual ControlTemplate GetCellFocusedRectangleTemplate() => 
            this.FocusedCellBorderTemplate;

        protected internal SelectionState GetCellSelectionState(int rowHandle, bool isFocused, bool isSelected, bool isMouseOver, ColumnBase column)
        {
            SelectionState state = this.CorrectSelectionState(this.SelectionStrategy.GetCellSelectionState(rowHandle, isFocused, isSelected), true);
            return ((!isMouseOver || !this.CanHighlightedState(rowHandle, column, true, state, false)) ? state : SelectionState.Highlighted);
        }

        protected internal virtual CriteriaOperator GetCheckedFilterPopupFilterCriteria(ColumnBase column, List<object> selectedItems) => 
            column.ColumnFilterInfo.GetFilterCriteria();

        protected internal virtual IEnumerable GetCheckedFilterPopupSelectedItems(ColumnBase column, ComboBoxEdit comboBox, CriteriaOperator filterCriteria) => 
            this.GetCheckedFilterPopupSelectedItems(column, (IEnumerable) comboBox.ItemsSource, filterCriteria);

        protected internal virtual IEnumerable GetCheckedFilterPopupSelectedItems(ColumnBase column, IEnumerable items, CriteriaOperator filterCriteria) => 
            column.ColumnFilterInfo.GetSelectedItems(items, filterCriteria);

        protected internal ColumnBase GetColumnByCommandParameter(object commandParameter) => 
            !(commandParameter is string) ? (commandParameter as ColumnBase) : this.DataControl.ColumnsCore[(string) commandParameter];

        public ColumnBase GetColumnByMouseEventArgs(MouseEventArgs e) => 
            this.GetColumnByTreeElement((DependencyObject) e.OriginalSource);

        internal ColumnBase GetColumnBySortLevel(int level)
        {
            GridSortInfo sortInfoBySortLevel = this.GetSortInfoBySortLevel(level);
            return ((sortInfoBySortLevel != null) ? this.ColumnsCore[sortInfoBySortLevel.FieldName] : null);
        }

        public ColumnBase GetColumnByTreeElement(DependencyObject d)
        {
            IGridCellEditorOwner cellElementByTreeElement = this.GetCellElementByTreeElement(d) as IGridCellEditorOwner;
            return cellElementByTreeElement?.AssociatedColumn;
        }

        internal static string GetColumnChooserSortableCaption(BaseColumn column) => 
            (column.HeaderCaption != null) ? column.HeaderCaption.ToString() : string.Empty;

        internal virtual ICommand GetColumnCommand(ColumnBase column) => 
            column.Commands.ChangeColumnSortOrder;

        protected internal string GetColumnDisplayText(object value, ColumnBase column, int? rowHandle = new int?())
        {
            string displayText = this.GetDisplayObject(value, column)?.ToString();
            int? listSourceIndex = rowHandle;
            listSourceIndex = null;
            return this.RaiseCustomDisplayText(new int?((listSourceIndex != null) ? listSourceIndex.GetValueOrDefault() : -2147483648), listSourceIndex, column, value, displayText);
        }

        public static double GetColumnHeaderDragIndicatorSize(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (double) element.GetValue(ColumnHeaderDragIndicatorSizeProperty);
        }

        internal virtual Type GetColumnType(ColumnBase column, DevExpress.Xpf.Data.DataProviderBase dataProvider = null) => 
            this.GetColumnType(column.FieldName, dataProvider);

        internal virtual Type GetColumnType(string fieldName, DevExpress.Xpf.Data.DataProviderBase dataProvider = null)
        {
            dataProvider ??= this.DataProviderBase;
            if (dataProvider == null)
            {
                return null;
            }
            DataColumnInfo info = dataProvider.Columns[fieldName];
            return info?.Type;
        }

        protected virtual IVisualTreeWalker GetCustomVisualTreeWalker() => 
            null;

        internal abstract DevExpress.Data.DataController GetDataControllerForUnboundColumnsCore();
        internal abstract IEnumerable<DataDependentEntityInfo> GetDataDependentEntityInfo();
        protected internal void GetDataRowText(StringBuilder sb, int rowHandle)
        {
            this.ViewBehavior.GetDataRowText(sb, rowHandle);
        }

        protected internal string GetDefaultFilterItemLocalizedString(DefaultFilterItem item) => 
            this.GetLocalizedString(item.ToString());

        internal object GetDisplayObject(object value, ColumnBase column) => 
            this.GetDisplayObject(value, column, true);

        internal object GetDisplayObject(object value, ColumnBase column, bool applyFormat) => 
            !applyFormat ? column.ActualEditSettingsCore.GetDisplayText(value, applyFormat) : column.ActualEditSettingsCore.GetDisplayTextFromEditor(value);

        protected internal virtual object GetExportValue(int rowHandle, ColumnBase column)
        {
            object cellValueCore = this.DataControl.GetCellValueCore(rowHandle, column);
            return ((IsInLookUpMode(column) || ((cellValueCore is Enum) || (column.ActualEditSettings.DisplayTextConverter != null))) ? this.DataControl.GetCellDisplayText(rowHandle, column.FieldName) : cellValueCore);
        }

        protected internal virtual object GetExportValueFromItem(object item, ColumnBase column)
        {
            int? nullable;
            if (item is Enum)
            {
                nullable = null;
                return this.GetColumnDisplayText(item, column, nullable);
            }
            if (!IsInLookUpMode(column) && (column.ActualEditSettings.DisplayTextConverter == null))
            {
                return item;
            }
            object valueFromItem = ((LookUpEditSettingsBase) column.ActualEditSettings).GetValueFromItem(item);
            nullable = null;
            return this.GetColumnDisplayText(valueFromItem, column, nullable);
        }

        internal string GetFilterOperatorCustomText(CriteriaOperator filterCriteria)
        {
            Func<FormatConditionFilterInfo, CriteriaOperator> substituteTopBottomFilter = <>c.<>9__1389_2;
            if (<>c.<>9__1389_2 == null)
            {
                Func<FormatConditionFilterInfo, CriteriaOperator> local1 = <>c.<>9__1389_2;
                substituteTopBottomFilter = <>c.<>9__1389_2 = (Func<FormatConditionFilterInfo, CriteriaOperator>) (filterInfo => null);
            }
            filterCriteria = PredefinedFiltersSubstituteHelper.Substitute(filterCriteria, EmptyArray<Attributed<IPredefinedFilter>>.Instance, delegate {
                // Unresolved stack state at '00000048'
            }, substituteTopBottomFilter);
            return this.GetFilterOperatorCustomText_NoSubstitute(filterCriteria);
        }

        internal string GetFilterOperatorCustomText_NoSubstitute(CriteriaOperator filterCriteria) => 
            DisplayCriteriaHelper.GetFilterDisplayText(filterCriteria, DataControlBase.CreateDisplayCriteriaHelperClient(this), CompatibilitySettings.UseFriendlyDateRangePresentation, delegate (CriteriaOperator x) {
                CustomFilterDisplayTextEventArgs args1 = new CustomFilterDisplayTextEventArgs(this, x);
                args1.RoutedEvent = CustomFilterDisplayTextEvent;
                CustomFilterDisplayTextEventArgs e = args1;
                this.RaiseCustomFilterDisplayText(e);
                return e.Value;
            });

        internal virtual int GetFirstDataFocusedRowHandle() => 
            (this.FocusedRowHandle >= 0) ? this.FocusedRowHandle : 0;

        internal void GetFirstScrollRowViewAndVisibleIndex(out DataViewBase view, out int visibleIndex)
        {
            if (!this.DataControl.FindViewAndVisibleIndexByScrollIndex(this.CalcFirstScrollRowScrollIndex(), true, out view, out visibleIndex))
            {
                this.DataControl.FindViewAndVisibleIndexByScrollIndex(this.CalcFirstScrollRowScrollIndex(), false, out view, out visibleIndex);
            }
            DataViewBase objB = view;
            DataControlBase dataControl = view.DataControl;
            int rowHandleByVisibleIndexCore = dataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            if (dataControl.IsExpandedFixedRow(visibleIndex))
            {
                GridRowsEnumerator enumerator = this.RootView.CreateVisibleRowsEnumerator();
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        RowData currentRowData = enumerator.CurrentRowData as RowData;
                        if ((currentRowData == null) || (!ReferenceEquals(currentRowData.View, objB) || (currentRowData.RowHandle.Value != rowHandleByVisibleIndexCore)))
                        {
                            continue;
                        }
                    }
                    if (enumerator.MoveNext())
                    {
                        RowData currentRowData = enumerator.CurrentRowData as RowData;
                        if (currentRowData != null)
                        {
                            view = currentRowData.View;
                            visibleIndex = dataControl.GetRowVisibleIndexByHandleCore(currentRowData.RowHandle.Value);
                        }
                    }
                    break;
                }
            }
        }

        protected internal virtual FixedRowPosition GetFixedRowByItemCore(object item) => 
            FixedRowPosition.None;

        protected internal FixedRowPosition GetFixedRowByVisibleIndex(int visibleIndex)
        {
            if (this.DataControl == null)
            {
                return FixedRowPosition.None;
            }
            int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
            int listIndexByRowHandle = this.DataControl.DataProviderBase.GetListIndexByRowHandle(rowHandleByVisibleIndexCore);
            return this.GetFixedRowCore(listIndexByRowHandle);
        }

        protected internal FixedRowPosition GetFixedRowCore(int listSourceRowIndex) => 
            this.GetFixedRowByItemCore(this.DataControl.DataProviderBase.GetRowByListIndex(listSourceRowIndex));

        protected internal virtual int GetFixedRowIndex(int listSourceRowIndex) => 
            -1;

        internal string GetFixedSummariesLeftString() => 
            FixedTotalSummaryHelper.GetFixedSummariesString(this.FixedSummariesLeft);

        internal string GetFixedSummariesRightString() => 
            FixedTotalSummaryHelper.GetFixedSummariesString(this.FixedSummariesRight);

        internal abstract DependencyProperty GetFocusedColumnProperty();
        protected internal virtual FrameworkElement GetFooterRowElementByRowHandleCore(int rowHandle)
        {
            RowData data;
            return (!this.VisualDataTreeBuilder.GroupSummaryRows.TryGetValue(rowHandle, out data) ? null : data.WholeRowElement);
        }

        protected internal virtual FrameworkElement GetFooterSummaryElementByRowHandleAndColumnCore(int rowHandle, ColumnBase column) => 
            null;

        internal virtual IEnumerable<FormatConditionBase> GetFormatConditions() => 
            Enumerable.Empty<FormatConditionBase>();

        internal ImageSource GetGlyphFilterCriteria(string name)
        {
            if (!this.GlyphCache.ContainsKey(name))
            {
                ImageSource image = FilterControlNodeBase.GetImage(name);
                this.GlyphCache.Add(name, image);
            }
            return this.GlyphCache[name];
        }

        protected internal abstract object GetGroupDisplayValue(int rowHandle);
        protected internal abstract object[] GetGroupDisplayValues(int rowHandle);
        protected internal abstract GroupTextHighlightingProperties GetGroupHighlightingProperties(int rowHandle);
        protected internal abstract string GetGroupRowDisplayText(int rowHandle);
        protected virtual ControlTemplate GetGroupRowFocusedRectangleTemplate() => 
            null;

        protected internal abstract string GetGroupRowHeaderCaption(int rowHandle);
        protected internal abstract string[] GetGroupRowHeadersCaptions(int rowHandle);
        protected internal string GetGroupSummaryText(ColumnBase column, int rowHandle, bool groupFooter)
        {
            InlineCollectionInfo info = this.GetGroupSummaryTextValues(column, rowHandle, groupFooter);
            return info?.TextSource;
        }

        protected internal virtual InlineCollectionInfo GetGroupSummaryTextValues(ColumnBase column, int rowHandle, bool groupFooter) => 
            null;

        protected internal virtual void GetHeadersText(StringBuilder sb)
        {
            this.GetDataRowText(sb, -2147483648);
        }

        protected virtual string GetInvalidRowErrorText(string errorText) => 
            (errorText.EndsWith("\n") || errorText.EndsWith("\r")) ? (errorText + this.GetLocalizedString(GridControlStringId.InvalidRowExceptionMessage)) : (errorText + "\r\n" + this.GetLocalizedString(GridControlStringId.InvalidRowExceptionMessage));

        protected internal bool GetIsCellFocused(int rowHandle, ColumnBase column) => 
            this.GetNavigation(rowHandle).GetIsFocusedCell(rowHandle, column);

        internal virtual bool GetIsCompactMode() => 
            false;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool GetIsFocusedCell(DependencyObject dependencyObject) => 
            (bool) dependencyObject.GetValue(IsFocusedCellProperty);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool GetIsFocusedRow(DependencyObject dependencyObject) => 
            (bool) dependencyObject.GetValue(IsFocusedRowProperty);

        protected virtual double GetItemInvisibleSize(FrameworkElement elem)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(elem, this.RootDataPresenter);
            return (this.SizeHelper.GetDefineSize(relativeElementRect.Size()) - this.GetItemVisibleSize(elem));
        }

        protected virtual double GetItemVisibleSize(FrameworkElement elem)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(elem, this.RootDataPresenter);
            Func<HierarchyPanel, double> evaluator = <>c.<>9__1547_0;
            if (<>c.<>9__1547_0 == null)
            {
                Func<HierarchyPanel, double> local1 = <>c.<>9__1547_0;
                evaluator = <>c.<>9__1547_0 = x => x.FixedBottomRowsHeight;
            }
            return ((this.SizeHelper.GetDefineSize(this.RootDataPresenter.LastConstraint) - this.RootDataPresenter.Panel.Return<HierarchyPanel, double>(evaluator, (<>c.<>9__1547_1 ??= () => 0.0))) - this.SizeHelper.GetDefinePoint(relativeElementRect.Location()));
        }

        internal void GetLastScrollRowViewAndVisibleIndex(out DataViewBase view, out int visibleIndex)
        {
            RowData data = null;
            int num = 0;
            Point point = new Point();
            GridRowsEnumerator enumerator = this.RootView.CreateVisibleRowsEnumerator();
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    if (enumerator.CurrentNode.FixedRowPosition != FixedRowPosition.None)
                    {
                        continue;
                    }
                    Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(enumerator.CurrentRowData.WholeRowElement, this.RootDataPresenter);
                    Point point2 = relativeElementRect.TopLeft();
                    if ((point2.X >= point.X) || (point2.Y >= point.Y))
                    {
                        Func<HierarchyPanel, double> evaluator = <>c.<>9__1529_0;
                        if (<>c.<>9__1529_0 == null)
                        {
                            Func<HierarchyPanel, double> local1 = <>c.<>9__1529_0;
                            evaluator = <>c.<>9__1529_0 = x => x.FixedTopRowsHeight;
                        }
                        if (this.SizeHelper.GetDefinePoint(point2) >= this.RootDataPresenter.Panel.Return<HierarchyPanel, double>(evaluator, (<>c.<>9__1529_1 ??= () => 0.0)))
                        {
                            num++;
                        }
                    }
                    point = relativeElementRect.BottomRight();
                    RowData currentRowData = enumerator.CurrentRowData as RowData;
                    if ((currentRowData != null) && (!(currentRowData.MatchKey is GroupSummaryRowKey) && !this.IsLoadingRowData(currentRowData)))
                    {
                        data = currentRowData;
                    }
                    if (num < this.RootDataPresenter.FullyVisibleItemsCount)
                    {
                        continue;
                    }
                }
                if (data != null)
                {
                    view = data.View;
                    visibleIndex = data.View.DataControl.GetRowVisibleIndexByHandleCore(data.RowHandle.Value);
                    return;
                }
                view = null;
                visibleIndex = -1;
                return;
            }
        }

        internal string GetLocalizedString(GridControlStringId id)
        {
            string name = Enum.GetName(typeof(GridControlStringId), id);
            return this.GetLocalizedString(name);
        }

        internal string GetLocalizedString(string stringId) => 
            this.LocalizationDescriptor.GetValue(stringId);

        internal virtual DependencyObject GetMouseLeftButtonDownSourceObject(MouseButtonEventArgs e) => 
            e.OriginalSource as DependencyObject;

        protected internal virtual GridViewNavigationBase GetNavigation(int rowHandle) => 
            !this.ViewBehavior.IsAdditionalRow(rowHandle) ? this.Navigation : this.AdditionalRowNavigation;

        internal virtual Tuple<double, double> GetOffsetAndHeightMergeCell(CellEditorBase cell, RowData rowData, double finalSizeHeight) => 
            null;

        protected internal virtual double GetOffsetByRowElement(FrameworkElement rowElement) => 
            0.0;

        private bool GetOriginationViewUpdateVisibleIndexesLocked() => 
            this.UseLegacyColumnVisibleIndexes && ((this.OriginationView != null) && ((this.DataControl != null) && this.OriginationView.UpdateVisibleIndexesLocker.IsLocked));

        protected internal abstract DataTemplate GetPrintRowTemplate();
        protected internal string GetRowCellDisplayText(int rowHandle, int visibleColumnIndex)
        {
            if (this.DataControl.IsGroupRowHandleCore(rowHandle))
            {
                return this.GetGroupRowDisplayText(rowHandle);
            }
            if (this.VisibleColumnsCore[visibleColumnIndex].CopyValueAsDisplayText)
            {
                return this.GetColumnDisplayText(this.DataControl.GetCellValueCore(rowHandle, this.VisibleColumnsCore[visibleColumnIndex]), this.VisibleColumnsCore[visibleColumnIndex], new int?(rowHandle));
            }
            object cellValueCore = this.DataControl.GetCellValueCore(rowHandle, this.VisibleColumnsCore[visibleColumnIndex]);
            return ((cellValueCore != null) ? cellValueCore.ToString() : string.Empty);
        }

        internal RowData GetRowData(int rowHandle) => 
            this.ViewBehavior.GetRowData(rowHandle);

        public FrameworkElement GetRowElementByMouseEventArgs(MouseEventArgs e) => 
            this.GetRowElementByTreeElement((DependencyObject) e.OriginalSource);

        public FrameworkElement GetRowElementByRowHandle(int rowHandle)
        {
            DataRowNode node;
            this.Nodes.TryGetValue(rowHandle, out node);
            if ((node != null) && this.IsInvisibleGroupRow(node))
            {
                return null;
            }
            if ((rowHandle == -2147483645) || (this.IsNewItemRowHandle(rowHandle) && this.IsNewItemRowVisible))
            {
                return this.ViewBehavior.GetAdditionalRowElement(rowHandle);
            }
            RowData rowData = this.GetRowData(rowHandle);
            return (((rowData == null) || (rowData.VisibleIndex < 0)) ? null : rowData.RowElement);
        }

        public FrameworkElement GetRowElementByTreeElement(DependencyObject d) => 
            FindParentRow(d) as FrameworkElement;

        private FrameworkElement GetRowElementByVisibleIndex(int index)
        {
            RowData rowData = this.GetRowData(this.DataControl.GetRowHandleByVisibleIndexCore(index));
            return ((rowData != null) ? this.GetRowVisibleElement(rowData) : null);
        }

        protected abstract ControlTemplate GetRowFocusedRectangleTemplate();
        public static RowHandle GetRowHandle(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (RowHandle) element.GetValue(RowHandleProperty);
        }

        public int GetRowHandleByMouseEventArgs(MouseEventArgs e) => 
            this.GetRowHandleByTreeElement((DependencyObject) e.OriginalSource);

        public int GetRowHandleByTreeElement(DependencyObject d)
        {
            DependencyObject element = FindParentRow(d);
            return ((element != null) ? GetRowHandle(element).Value : -2147483648);
        }

        internal int GetRowParentIndex(int visibleIndex, int level)
        {
            int controllerRow = this.DataProviderBase.GetControllerRow(visibleIndex);
            while (this.DataProviderBase.GetRowLevelByControllerRow(controllerRow) > level)
            {
                controllerRow = this.DataProviderBase.GetParentRowHandle(controllerRow);
            }
            return this.DataProviderBase.GetRowVisibleIndexByHandle(controllerRow);
        }

        protected internal SelectionState GetRowSelectionState(int rowHandle, bool isMouseOver)
        {
            SelectionState state = this.CorrectSelectionState(this.SelectionStrategy.GetRowSelectionState(rowHandle), false);
            return ((!isMouseOver || !this.CanHighlightedState(rowHandle, null, false, state, this.IsGroupRowHandleCore(rowHandle))) ? state : SelectionState.Highlighted);
        }

        internal object GetRowValue(RowHandle rowHandle) => 
            this.DataProviderBase.GetRowValue(rowHandle.Value);

        private object GetRowValue(int handle) => 
            this.DataProviderBase.GetRowValue(handle);

        internal virtual FrameworkElement GetRowVisibleElement(RowDataBase rowData) => 
            rowData.WholeRowElement;

        [Obsolete("Use the DataControlBase.GetSelectedRowHandles method instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int[] GetSelectedRowHandles() => 
            this.GetSelectedRowHandlesCore();

        internal int[] GetSelectedRowHandlesCore() => 
            this.SelectionStrategy.GetSelectedRows();

        protected internal abstract MultiSelectMode GetSelectionMode();
        protected virtual bool GetShowEditFilterButton() => 
            this.AllowFilterEditor.ToBoolean(!this.IsVirtualSource) ? ((this.DataControl == null) || (this.DataControl.countColumnFilteringTrue != 0)) : false;

        internal virtual GridSortInfo GetSortInfoBySortLevel(int level) => 
            this.DataControl.ActualSortInfo[level];

        internal static DependencyObject GetStartHitTestObject(DependencyObject d, DataViewBase view)
        {
            if (!view.IsRootView)
            {
                throw new NotSupportedInMasterDetailException("Hit testing is supported only by a master view. Hit information cannot be calculated for detail views.");
            }
            for (DependencyObject obj2 = d; (obj2 != null) && !ReferenceEquals(obj2, view); obj2 = LayoutHelper.GetParent(obj2, false))
            {
                if (obj2 is DataViewBase)
                {
                    d = obj2;
                }
            }
            return d;
        }

        protected internal string GetTextForClipboard(int rowHandle, int visibleColumnIndex)
        {
            string str = (rowHandle == -2147483648) ? this.VisibleColumnsCore[visibleColumnIndex].HeaderCaption.ToString() : this.GetRowCellDisplayText(rowHandle, visibleColumnIndex);
            if (str == null)
            {
                return string.Empty;
            }
            if (str.Contains(Environment.NewLine))
            {
                str = "\"" + str.Replace("\"", "\"\"").Replace(Environment.NewLine, "\n") + "\"";
            }
            return str.Replace("\t", " ");
        }

        protected internal virtual TextHighlightingProperties GetTextHighlightingProperties(ColumnBase column) => 
            (!this.CanStartIncrementalSearch || ((this.TextSearchEngineRoot == null) || string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText))) ? this.SearchPanelColumnProvider?.GetTextHighlightingProperties(column) : (!this.IncrementalSearchColumns.Contains<string>(column.FieldName) ? null : new TextHighlightingProperties(this.TextSearchEngineRoot.SeachText, FilterCondition.StartsWith));

        [IteratorStateMachine(typeof(<GetTopLevelDropContainers>d__1151))]
        protected internal virtual IEnumerable<UIElement> GetTopLevelDropContainers()
        {
            if (!this.IsRootView)
            {
                throw new InvalidOperationException();
            }
            IColumnChooser visibleColumnChooser = null;
            this.UpdateAllOriginationViews(delegate (DataViewBase view) {
                if (view.IsColumnChooserVisible)
                {
                    visibleColumnChooser = view.ActualColumnChooser;
                }
            });
            if (visibleColumnChooser != null)
            {
                yield return visibleColumnChooser.TopContainer;
            }
            yield return LayoutHelper.GetTopContainerWithAdornerLayer(this);
        }

        internal virtual bool GetUpdateRowButtonsShow(RowData rowData) => 
            (this.DataControl != null) && (rowData.IsFocusedRow() && ((this.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.Never) && this.AreUpdateRowButtonsShown));

        internal virtual KeyValuePair<DataViewBase, int> GetViewAndVisibleIndex(double verticalOffset, bool calcDataArea = true) => 
            this.ViewBehavior.GetViewAndVisibleIndex(verticalOffset, calcDataArea);

        internal object GetWpfRow(RowHandle rowHandle, int listSourceRowIndex) => 
            this.DataProviderBase.GetWpfRow(rowHandle, listSourceRowIndex);

        protected virtual void GroupColumn(string fieldName, int index, ColumnSortOrder sortOrder)
        {
        }

        protected internal virtual void HandleGroupMoveAction(ColumnBase source, int newVisibleIndex, HeaderPresenterType moveFrom, HeaderPresenterType moveTo, MergeGroupPosition mergeGroupPosition = 0)
        {
        }

        protected void HandleInvalidRowExceptionEventArgs(ControllerRowExceptionEventArgs e, IInvalidRowExceptionEventArgs eventArgs)
        {
            if (eventArgs.ExceptionMode == ExceptionMode.DisplayError)
            {
                MessageBoxResult result = this.DisplayInvalidRowError(eventArgs);
                if ((result == MessageBoxResult.No) || (result == MessageBoxResult.Cancel))
                {
                    eventArgs.ExceptionMode = ExceptionMode.Ignore;
                }
                this.AllowMouseMoveSelection = false;
            }
            if (eventArgs.ExceptionMode == ExceptionMode.Ignore)
            {
                e.Action = ExceptionAction.CancelAction;
            }
            if (eventArgs.ExceptionMode == ExceptionMode.ThrowException)
            {
                throw e.Exception;
            }
            if (e.Action == ExceptionAction.CancelAction)
            {
                this.DataControl.SetRowStateError(e.RowHandle, null);
            }
        }

        internal abstract bool HasCustomColumnDisplayTextSubscription();
        internal virtual bool HasDataUpdateFormatConditions() => 
            false;

        internal bool HasFormatConditions() => 
            this.GetFormatConditions().Any<FormatConditionBase>();

        protected bool HasParentRow(int visibleRowIndex)
        {
            int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(visibleRowIndex);
            return (this.DataProviderBase.GetParentRowHandle(rowHandleByVisibleIndexCore) != -2147483648);
        }

        public void HideColumnChooser()
        {
            this.IsColumnChooserVisible = false;
        }

        public void HideEditor()
        {
            this.HideEditor(true);
        }

        internal void HideEditor(bool closeEditor)
        {
            if (this.CurrentCellEditor != null)
            {
                if (closeEditor)
                {
                    this.CurrentCellEditor.CancelEditInVisibleEditor();
                }
                else
                {
                    this.CurrentCellEditor.HideEditor(false);
                }
            }
        }

        public void HideSearchPanel()
        {
            if ((this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.Default) || (this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.HotKey))
            {
                GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
                if (searchControl != null)
                {
                    searchControl.FadeSearchPanel(() => this.ActualShowSearchPanel = false);
                }
                else
                {
                    this.ActualShowSearchPanel = false;
                }
            }
        }

        public void IncrementalSearchEnd()
        {
            this.ResetIncrementalSearch();
        }

        private void IncrementalSearchModeChanged()
        {
            this.IncrementalSearchModeCore = this.IncrementalSearchMode;
            this.ResetIncrementalSearch();
            if (this.CanStartIncrementalSearch)
            {
                this.TextSearchEngineRoot = new TableTextSearchEngine(new Func<string, TableTextSearchEngine.TableIndex, bool, bool, DataViewBase, TableTextSearchEngine.TableIndex>(IncrementalSearchHelper.SearchCallback), this.IncrementalSearchClearDelay);
            }
            else
            {
                this.TextSearchEngineRoot = null;
                if (this.DataControl != null)
                {
                    this.UpdateEditorHighlightingText();
                    this.UpdateFilterGrid();
                }
            }
        }

        public bool IncrementalSearchMoveNext() => 
            this.ProcessTextSearch(string.Empty, true);

        public bool IncrementalSearchMovePrev() => 
            this.ProcessTextSearch(string.Empty, false);

        public void IncrementalSearchStart(string value)
        {
            if (this.CanStartIncrementalSearch && !string.IsNullOrEmpty(value))
            {
                this.ResetIncrementalSearch();
                bool? down = null;
                this.ProcessTextSearch(value, down);
            }
        }

        internal void InitMenu()
        {
            if (this.dataControlMenu != null)
            {
                this.dataControlMenu.Init();
            }
            else
            {
                this.initDataControlMenuWhenCreated = true;
            }
        }

        protected void InvalidateParentTree()
        {
            if (this.DataControl != null)
            {
                this.DataControl.DataControlParent.InvalidateTree();
            }
        }

        internal virtual void InvalidateVirtualDataSource()
        {
        }

        protected bool IsAddDeleteInSource() => 
            !this.IsVirtualSource ? ((this.DataProviderBase != null) && ((this.DataProviderBase.DataSource is IList) || ((this.DataProviderBase.DataSource is IEditableCollectionView) || (this.DataProviderBase.DataSource is IListSource)))) : false;

        protected virtual bool IsCellMerged(int visibleIndex1, int visibleIndex2, ColumnBase column, bool checkRowData, int checkMDIndex) => 
            false;

        protected internal bool IsColumnNavigatable(ColumnBase column, bool isTabNavigation) => 
            (isTabNavigation ? column.TabStop : true) && (column.AllowFocus || (this.FocusedRowHandle == -2147483645));

        internal virtual bool IsColumnVisibleInHeaders(BaseColumn col) => 
            true;

        internal abstract bool IsDataRowNodeExpanded(DataRowNode rowNode);
        private bool IsDataViewDragDropManagerAttached()
        {
            if (this.AllowDragDropOnRootView)
            {
                return true;
            }
            Func<DataControlBase, bool> evaluator = <>c.<>9__940_0;
            if (<>c.<>9__940_0 == null)
            {
                Func<DataControlBase, bool> local1 = <>c.<>9__940_0;
                evaluator = <>c.<>9__940_0 = d => DataViewDragDropManagerHelper.GetIsAttached(d);
            }
            return this.DataControl.Return<DataControlBase, bool>(evaluator, (<>c.<>9__940_1 ??= () => false));
        }

        protected internal virtual bool IsEvenRow(int rowHandle) => 
            (rowHandle % 2) == 0;

        internal abstract bool IsExpandableRowFocused();
        internal bool IsExpanded(int rowHandle)
        {
            RowNode node = this.Nodes.ContainsKey(rowHandle) ? ((RowNode) this.Nodes[rowHandle]) : null;
            return ((node != null) ? node.IsRowExpandedForNavigation() : false);
        }

        protected virtual bool IsFirstNewRow() => 
            false;

        protected internal bool IsFocusedRowInCurrentPageBounds()
        {
            if (!this.IsPagingMode || (this.DataProviderBase == null))
            {
                return true;
            }
            int currentIndex = this.ViewBehavior.CurrentIndex;
            return ((currentIndex > -1) && ((currentIndex >= this.FirstVisibleIndexOnPage) && (currentIndex <= this.LastVisibleIndexOnPage)));
        }

        private bool IsFocusInSearchPanel()
        {
            DevExpress.Xpf.Editors.SearchControl element = this.IsRootView ? this.SearchControl : this.RootView?.SearchControl;
            return (((element == null) || !element.GetIsKeyboardFocusWithin()) ? this.IsKeyboardFocusInHeadersPanel() : true);
        }

        private bool IsGroupRowHandleCore(int handle) => 
            this.DataControl.IsGroupRowHandleCore(handle);

        internal static bool IsInLookUpMode(ColumnBase column) => 
            (column.EditSettings is LookUpEditSettingsBase) && LookUpEditHelper.IsInLookUpMode((LookUpEditSettingsBase) column.EditSettings);

        internal virtual bool IsInvisibleGroupRow(RowNode node) => 
            false;

        internal bool IsKeyboardFocusInHeadersPanel() => 
            (this.HeadersPanel != null) && this.HeadersPanel.GetIsKeyboardFocusWithin();

        internal bool IsKeyboardFocusInSearchPanel() => 
            this.isKeyboardFocusInSearchPanel;

        private bool IsKeyboardFocusWithinContentView()
        {
            DependencyObject focusedElement = FocusHelper.GetFocusedElement() as DependencyObject;
            if (focusedElement == null)
            {
                return false;
            }
            DataViewBase objA = LayoutHelper.FindLayoutOrVisualParentObject<DataViewBase>(focusedElement, false, null);
            return ((objA != null) && (!ReferenceEquals(objA, this) && objA.IsRootView));
        }

        private bool IsKeyboardFocusWithinEditForm()
        {
            DependencyObject focusedElement = FocusHelper.GetFocusedElement() as DependencyObject;
            return ((focusedElement != null) ? this.EditFormManager.IsInlineFormChild(focusedElement) : false);
        }

        protected internal bool IsLastVisibleColumn(BaseColumn column) => 
            column.Visible ? ((this.ViewBehavior.GetActualColumnFixed(column) == FixedStyle.None) ? (this.FixedNoneColumnsCount <= 1) : (this.VisibleColumnsCore.Count <= 1)) : false;

        protected internal bool IsLoadingRowData(RowData rowData) => 
            rowData is LoadingRowData;

        internal bool IsMergedCell(int rowHandle, ColumnBase column)
        {
            if (!this.DataControl.IsValidRowHandleCore(rowHandle) || this.ViewBehavior.IsAdditionalRowCore(rowHandle))
            {
                return false;
            }
            int rowVisibleIndexByHandleCore = this.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);
            return (this.IsPrevRowCellMerged(rowVisibleIndexByHandleCore, column, false) || this.IsNextRowCellMerged(rowVisibleIndexByHandleCore, column, false));
        }

        internal virtual bool IsNewItemRowHandle(int rowHandle) => 
            false;

        internal bool IsNextRowCellMerged(int visibleIndex, ColumnBase column, bool checkRowData) => 
            this.IsCellMerged(visibleIndex, visibleIndex + 1, column, checkRowData, visibleIndex);

        internal bool IsPrevRowCellMerged(int visibleIndex, ColumnBase column, bool checkRowData) => 
            this.IsCellMerged(visibleIndex, visibleIndex - 1, column, checkRowData, visibleIndex - 1);

        public bool IsRowSelected(int rowHandle) => 
            this.SelectionStrategy.IsRowSelected(rowHandle);

        protected internal bool IsScrollIndexInPageBounds(int scrollIndex)
        {
            int num = this.DataControl.DataProviderBase.ConvertScrollIndexToVisibleIndex(scrollIndex, this.AllowFixedGroupsCore);
            return ((num >= this.FirstVisibleIndexOnPage) && (num <= this.LastVisibleIndexOnPage));
        }

        protected internal bool IsSearchResult(Func<int, bool> fitSearch, int rowHandle, int rowVisibleIndex)
        {
            if (!MovementSearchResults.CanMovementSearchResult(this))
            {
                return fitSearch(rowHandle);
            }
            if (!fitSearch(rowHandle))
            {
                return false;
            }
            this.searchResult = true;
            return true;
        }

        protected internal virtual bool IsTreeColumn(ColumnBase column) => 
            false;

        internal DevExpress.Xpf.Grid.ErrorsWatchMode? IsValidRowByRowHandle(int rowHandle, DevExpress.Xpf.Grid.ErrorsWatchMode mode, bool ignoreAlowLeaveEditor = true)
        {
            if ((mode != DevExpress.Xpf.Grid.ErrorsWatchMode.None) && !this.DataProviderBase.IsGroupRowHandle(rowHandle))
            {
                object row = this.DataControl.GetRow(rowHandle);
                if (row == null)
                {
                    return null;
                }
                if (mode.HasFlag(DevExpress.Xpf.Grid.ErrorsWatchMode.Rows))
                {
                    MultiErrorInfo multiErrorInfo = this.DataProviderBase.GetMultiErrorInfo(rowHandle, row);
                    if (multiErrorInfo.HasErrors() && (multiErrorInfo.ErrorType != ErrorType.None))
                    {
                        return 2;
                    }
                }
                if (mode.HasFlag(DevExpress.Xpf.Grid.ErrorsWatchMode.Cells))
                {
                    bool flag = this.SupportValidateCell() && ((ignoreAlowLeaveEditor || this.AllowLeaveInvalidEditor) || (this.FocusedRowHandle == rowHandle));
                    using (IEnumerator<ColumnBase> enumerator = this.VisibleColumnsCore.GetEnumerator())
                    {
                        while (true)
                        {
                            DevExpress.Xpf.Grid.ErrorsWatchMode? nullable;
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            ColumnBase current = enumerator.Current;
                            MultiErrorInfo multiErrorInfo = this.DataProviderBase.GetMultiErrorInfo(rowHandle, this.DataProviderBase.GetActualColumnInfo(current.FieldName), row);
                            if (multiErrorInfo.HasErrors() && (multiErrorInfo.ErrorType != ErrorType.None))
                            {
                                nullable = 4;
                            }
                            else
                            {
                                if (!flag || (RowValidationHelper.ValidateEvents(this, this, this.DataControl.GetCellValueCore(rowHandle, current), rowHandle, current) == null))
                                {
                                    continue;
                                }
                                nullable = 4;
                            }
                            return nullable;
                        }
                    }
                }
            }
            return null;
        }

        protected internal virtual void MouseWheelDown(int lineCount)
        {
            Action<IScrollInfo> action = <>c.<>9__2298_0;
            if (<>c.<>9__2298_0 == null)
            {
                Action<IScrollInfo> local1 = <>c.<>9__2298_0;
                action = <>c.<>9__2298_0 = info => info.MouseWheelDown();
            }
            this.RepeatDataPresenterAction(action);
        }

        protected internal virtual void MouseWheelLeft(int lineCount)
        {
            Action<IScrollInfo> action = <>c.<>9__2299_0;
            if (<>c.<>9__2299_0 == null)
            {
                Action<IScrollInfo> local1 = <>c.<>9__2299_0;
                action = <>c.<>9__2299_0 = info => info.MouseWheelLeft();
            }
            this.RepeatDataPresenterAction(action);
        }

        protected internal virtual void MouseWheelRight(int lineCount)
        {
            Action<IScrollInfo> action = <>c.<>9__2300_0;
            if (<>c.<>9__2300_0 == null)
            {
                Action<IScrollInfo> local1 = <>c.<>9__2300_0;
                action = <>c.<>9__2300_0 = info => info.MouseWheelRight();
            }
            this.RepeatDataPresenterAction(action);
        }

        protected internal virtual void MouseWheelUp(int lineCount)
        {
            Action<IScrollInfo> action = <>c.<>9__2301_0;
            if (<>c.<>9__2301_0 == null)
            {
                Action<IScrollInfo> local1 = <>c.<>9__2301_0;
                action = <>c.<>9__2301_0 = info => info.MouseWheelUp();
            }
            this.RepeatDataPresenterAction(action);
        }

        public void MoveColumnTo(ColumnBase source, int newVisibleIndex, HeaderPresenterType moveFrom, HeaderPresenterType moveTo, MergeGroupPosition mergeGroupPosition = 0)
        {
            if (source != null)
            {
                this.ColumnsCore.BeginUpdate();
                IDataControlOriginationElement dataControlOriginationElement = this.DataControl.GetDataControlOriginationElement();
                if ((moveFrom != HeaderPresenterType.GroupPanel) && (moveTo != HeaderPresenterType.GroupPanel))
                {
                    dataControlOriginationElement.ColumnsChangedLocker.Lock();
                }
                else
                {
                    dataControlOriginationElement.SynchronizationLocker.Lock();
                    this.DataControl.SortUpdateLocker.Lock();
                }
                try
                {
                    if (moveFrom == HeaderPresenterType.ColumnChooser)
                    {
                        source.Visible = true;
                    }
                    this.HandleGroupMoveAction(source, newVisibleIndex, moveFrom, moveTo, mergeGroupPosition);
                    if (moveTo == HeaderPresenterType.Headers)
                    {
                        Tuple<ColumnBase, BandedViewDropPlace> tuple = this.ViewBehavior.GetColumnDropTarget(source, newVisibleIndex, moveFrom);
                        new BandsMover(this.DataControl).MoveColumnToColumn(source, tuple.Item1, tuple.Item2, moveFrom, this.UseLegacyColumnVisibleIndexes);
                    }
                }
                finally
                {
                    dataControlOriginationElement.SynchronizationLocker.Unlock();
                    IColumnCollection columnsCore = this.ColumnsCore;
                    this.DataControl.syncronizationLocker.DoLockedAction(new Action(columnsCore.EndUpdate));
                    dataControlOriginationElement.ColumnsChangedLocker.Unlock();
                    this.DataControl.SortUpdateLocker.Unlock();
                }
                this.NotifyDesignTimeAdornerOnColumnMoved(moveFrom, moveTo);
            }
        }

        internal void MoveFirstCell()
        {
            this.MoveFirstNavigationIndex();
        }

        internal virtual void MoveFirstMasterRow()
        {
            this.DataControl.NavigateToFirstMasterRow();
        }

        protected internal void MoveFirstNavigationIndex()
        {
            int updateButtonsModeAllowRequestUI;
            try
            {
                updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                this.MoveFirstNavigationIndex(false);
            }
            finally
            {
                updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
            }
        }

        internal void MoveFirstNavigationIndex(bool isTabNavigation)
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.ViewBehavior.NavigationStrategyBase.MoveFirstNavigationIndex(this, isTabNavigation);
            }
        }

        internal void MoveFirstNavigationIndexCore(bool isTabNavigation)
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.ViewBehavior.NavigationStrategyBase.MoveFirstNavigationIndexCore(this, isTabNavigation);
            }
        }

        internal void MoveFirstOrFirstMasterRow()
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                if (this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle) != 0)
                {
                    this.MoveFirstRow();
                }
                else
                {
                    this.MoveFirstMasterRow();
                }
            }
        }

        public virtual void MoveFirstRow()
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.MoveFocusedRow(this.IsPagingMode ? this.FirstVisibleIndexOnPage : 0);
            }
        }

        public void MoveFocusedRow(int visibleIndex)
        {
            this.FocusViewAndRow(this, this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex));
        }

        protected internal virtual void MoveFocusedRowToFirstScrollRow()
        {
            DataViewBase view = null;
            int visibleIndex = 0;
            this.GetFirstScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            this.MoveFocusedRowToScrollIndexForPageUpPageDown(view.ConvertVisibleIndexToScrollIndex(visibleIndex), true);
        }

        protected internal virtual void MoveFocusedRowToLastScrollRow()
        {
            this.MoveFocusedRowToScrollIndexForPageUpPageDown(this.CalcLastScrollRowScrollIndex(), false);
        }

        internal void MoveFocusedRowToScrollIndexForPageUpPageDown(int scrollIndex, bool pageUp)
        {
            DataViewBase targetView = null;
            int targetVisibleIndex = 0;
            if (!((scrollIndex == 0) & pageUp))
            {
                this.DataControl.FindViewAndVisibleIndexByScrollIndex(scrollIndex, pageUp, out targetView, out targetVisibleIndex);
            }
            else
            {
                targetView = this.RootView;
                targetVisibleIndex = this.IsPagingMode ? this.FirstVisibleIndexOnPage : 0;
            }
            int rowHandleByVisibleIndexCore = targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex);
            if ((targetView.DataControl.VisibleRowCount == 0) && targetView.IsNewItemRowVisible)
            {
                rowHandleByVisibleIndexCore = -2147483647;
            }
            if (this.RootDataPresenter.CanScrollWithAnimation && (ReferenceEquals(targetView, this.FocusedView) && (rowHandleByVisibleIndexCore == targetView.FocusedRowHandle)))
            {
                this.ScrollIntoView(rowHandleByVisibleIndexCore);
            }
            this.FocusViewAndRow(targetView, rowHandleByVisibleIndexCore);
        }

        internal void MoveLastCell()
        {
            this.MoveLastNavigationIndex();
        }

        internal virtual void MoveLastMasterRow()
        {
            this.DataControl.NavigateToLastMasterRow();
        }

        protected internal void MoveLastNavigationIndex()
        {
            int updateButtonsModeAllowRequestUI;
            try
            {
                updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                this.MoveLastNavigationIndex(false);
            }
            finally
            {
                updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
            }
        }

        internal void MoveLastNavigationIndex(bool isTabNavigation)
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.ViewBehavior.NavigationStrategyBase.MoveLastNavigationIndex(this, isTabNavigation);
            }
        }

        internal void MoveLastOrLastMasterRow()
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                if (this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle) != (this.DataProviderBase.VisibleCount - 1))
                {
                    this.MoveLastRow();
                }
                else
                {
                    this.MoveLastMasterRow();
                }
            }
        }

        public virtual void MoveLastRow()
        {
            if (this.IsFocusedRowInCurrentPageBounds() && this.CanMoveFromFocusedRow())
            {
                this.MoveFocusedRow(this.DataControl.LastRowIndex);
            }
        }

        public virtual void MoveNextCell()
        {
            this.MoveNextCell(false, false);
        }

        protected internal void MoveNextCell(bool isTabNavigation, bool isEnterNavigation = false)
        {
            if (this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this, isTabNavigation) || this.ShouldChangeRowByTab)
            {
                bool flag = this.IsTopNewItemRowFocused && ((this.IsRootView || this.ViewBehavior.IsNewItemRowEditing) | isEnterNavigation);
                if (flag | (this.IsBottomNewItemRowFocused && (this.ViewBehavior.IsNewItemRowEditing || (!this.IsRootView & isEnterNavigation))))
                {
                    this.MoveNextNewItemRowCell(true);
                }
                else if ((!this.IsAutoFilterRowFocused && this.ViewBehavior.AutoMoveRowFocusCore) && !this.DataControl.NavigateToFirstChildDetailCell(isTabNavigation))
                {
                    if (this.IsLastRow)
                    {
                        this.DataControl.NavigateToNextOuterMasterCell(isTabNavigation);
                    }
                    else
                    {
                        this.MoveNextRow();
                        this.MoveFirstNavigationIndex(isTabNavigation);
                        if (this.AreUpdateRowButtonsShown && this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this, isTabNavigation))
                        {
                            RowData rowData = this.GetRowData(this.FocusedRowHandle);
                            if (rowData != null)
                            {
                                rowData.UpdateButtonTabPress(false);
                            }
                        }
                    }
                }
            }
            else
            {
                int updateButtonsModeAllowRequestUI;
                try
                {
                    updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                    if (this.AreUpdateRowButtonsShown)
                    {
                        RowData rowData = this.GetRowData(this.FocusedRowHandle);
                        if ((rowData != null) && rowData.UpdateButtonIsFocused())
                        {
                            rowData.UpdateButtonTabPress(false);
                            return;
                        }
                    }
                    this.MoveNextCellCore(isTabNavigation);
                }
                finally
                {
                    updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
                }
            }
        }

        internal void MoveNextCellCore(bool isTabNavigation = false)
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.ViewBehavior.NavigationStrategyBase.MoveNextNavigationIndex(this, isTabNavigation);
            }
        }

        protected internal virtual void MoveNextNewItemRowCell(bool isTabNavigation = false)
        {
            if (this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this, isTabNavigation) && this.CommitEditing())
            {
                this.MoveFirstNavigationIndexCore(isTabNavigation);
                if (this.IsBottomNewItemRowFocused)
                {
                    this.ScrollIntoView(this.FocusedRowHandle);
                }
            }
        }

        public virtual void MoveNextPage()
        {
            if (((this.DataControl.VisibleRowCount != 0) || this.IsNewItemRowVisible) && (this.CanMoveFromFocusedRow() && this.IsFocusedRowInCurrentPageBounds()))
            {
                this.MoveNextPageCore();
            }
        }

        protected virtual void MoveNextPageCore()
        {
            if (this.HasFixedRows && ((this.ScrollInfoOwner.Offset + this.ScrollInfoOwner.ItemsOnPage) >= this.ScrollInfoOwner.ItemCount))
            {
                this.MoveLastRow();
                this.SelectionStrategy.OnNavigationComplete(false);
            }
            else if (!this.ShouldScrollOnePageForward())
            {
                this.MoveFocusedRowToLastScrollRow();
                this.SelectionStrategy.OnNavigationComplete(false);
            }
            else if (this.RootDataPresenter.CanScrollWithAnimation)
            {
                new ScrollOnePageDownWithAnimationAction(this).Execute();
            }
            else
            {
                new ScrollOnePageDownAction(this).Execute();
            }
        }

        public virtual void MoveNextRow()
        {
            this.ViewBehavior.MoveNextRow();
        }

        internal void MoveParentRow()
        {
            if (this.HasParentRow(this.DataProviderBase.CurrentIndex))
            {
                int parentRowIndex = this.DataProviderBase.GetParentRowIndex(this.DataProviderBase.CurrentIndex);
                this.MoveFocusedRow(parentRowIndex);
            }
        }

        public virtual void MovePrevCell()
        {
            this.MovePrevCell(false);
        }

        protected internal void MovePrevCell(bool isTabNavigation)
        {
            if (this.ViewBehavior.NavigationStrategyBase.IsBeginNavigationIndex(this, isTabNavigation) || this.ShouldChangeRowByTab)
            {
                if (!this.IsAutoFilterRowFocused && this.ViewBehavior.AutoMoveRowFocusCore)
                {
                    if (this.IsTopNewItemRowFocused && this.IsRootView)
                    {
                        this.MovePrevCellCore(isTabNavigation);
                    }
                    else if ((!this.IsFirstRow || this.CanNavigateToNewItemRow) ? this.IsTopNewItemRowFocused : true)
                    {
                        this.DataControl.NavigateToMasterCell(isTabNavigation);
                    }
                    else if (!this.DataControl.NavigateToPreviousInnerDetailCell(isTabNavigation))
                    {
                        this.MovePrevRow();
                        this.MoveLastNavigationIndex(isTabNavigation);
                        if (this.AreUpdateRowButtonsShown && this.ViewBehavior.NavigationStrategyBase.IsBeginNavigationIndex(this, isTabNavigation))
                        {
                            RowData rowData = this.GetRowData(this.FocusedRowHandle);
                            if (rowData != null)
                            {
                                rowData.UpdateButtonTabPress(true);
                            }
                        }
                    }
                }
            }
            else
            {
                int updateButtonsModeAllowRequestUI;
                try
                {
                    updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                    if (this.AreUpdateRowButtonsShown)
                    {
                        RowData rowData = this.GetRowData(this.FocusedRowHandle);
                        if ((rowData != null) && rowData.UpdateButtonIsFocused())
                        {
                            rowData.UpdateButtonTabPress(true);
                            return;
                        }
                    }
                    this.MovePrevCellCore(isTabNavigation);
                }
                finally
                {
                    updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
                }
            }
        }

        internal void MovePrevCellCore(bool isTabNavigation = false)
        {
            if (this.IsFocusedRowInCurrentPageBounds())
            {
                this.ViewBehavior.NavigationStrategyBase.MovePrevNavigationIndex(this, isTabNavigation);
            }
        }

        public virtual void MovePrevPage()
        {
            if (((this.DataControl.VisibleRowCount != 0) || this.IsNewItemRowVisible) && ((!this.IsAdditionalRowFocused || !this.IsRootView) && this.IsFocusedRowInCurrentPageBounds()))
            {
                this.MovePrevPageCore();
            }
        }

        protected virtual void MovePrevPageCore()
        {
            if (this.HasFixedRows && (this.ScrollInfoOwner.Offset == 0))
            {
                this.MoveFirstRow();
                this.SelectionStrategy.OnNavigationComplete(false);
            }
            else if (!this.ShouldScrollOnePageBack())
            {
                this.MoveFocusedRowToFirstScrollRow();
                this.SelectionStrategy.OnNavigationComplete(false);
            }
            else if (this.RootDataPresenter.CanScrollWithAnimation)
            {
                new ScrollOnePageUpWithAnimationAction(this).Execute();
            }
            else
            {
                new ScrollOnePageUpAction(this).Execute();
            }
        }

        public virtual void MovePrevRow()
        {
            this.ViewBehavior.MovePrevRow();
        }

        internal void NavigateToFirstRow()
        {
            if (!this.IsNewItemRowVisible)
            {
                this.MoveFirstRow();
            }
            else
            {
                this.FocusViewAndRow(this, -2147483647);
            }
        }

        internal void NavigateToLastRow()
        {
            if (this.DataControl.VisibleRowCount > 0)
            {
                this.MoveFocusedRow(this.DataControl.LastRowIndex - this.ActualFixedBottomRowsCount);
            }
            else if (this.IsNewItemRowVisible)
            {
                this.FocusViewAndRow(this, -2147483647);
            }
        }

        protected virtual bool NeedErrorWatchCurrentRowChanged(ListChangedType changedType) => 
            true;

        internal virtual bool NeedWatchRowChanged()
        {
            if ((this.DataProviderBase != null) && this.DataProviderBase.IsUpdateLocked)
            {
                return false;
            }
            DevExpress.Xpf.Grid.ErrorsWatchMode mode = DevExpress.Xpf.Grid.ErrorWatch.ActualErrorsWatchMode(this);
            return (((mode & DevExpress.Xpf.Grid.ErrorsWatchMode.Cells) != DevExpress.Xpf.Grid.ErrorsWatchMode.None) || ((mode & DevExpress.Xpf.Grid.ErrorsWatchMode.Rows) != DevExpress.Xpf.Grid.ErrorsWatchMode.None));
        }

        protected virtual void NotifyDesignTimeAdornerOnColumnMoved(HeaderPresenterType moveFrom, HeaderPresenterType moveTo)
        {
            this.DesignTimeAdorner.OnColumnMoved();
        }

        private void OnActiveEditorChanged()
        {
            if (this.CurrentCellEditor is CellEditorBase)
            {
                ((CellEditorBase) this.CurrentCellEditor).CellData.UpdateSelectionState();
            }
        }

        protected virtual void OnActualAllowCellMergeChanged()
        {
        }

        private void OnActualShowSearchPanelChanged()
        {
            if (this.ActualShowSearchPanel)
            {
                this.ShowSearchPanel(false);
            }
            else
            {
                this.HideSearchPanel();
            }
        }

        internal virtual void OnAllowComplexPropertyUpdatesChanged()
        {
            if (this.DataProviderBase != null)
            {
                this.DataProviderBase.OnDataSourceChanged();
            }
        }

        protected virtual void OnAllowDragDropChanged()
        {
            this.RebuildDragManager();
        }

        protected internal virtual void OnAllowPerPixelScrollingChanged()
        {
            if (this.DataControl != null)
            {
                this.DataControl.InvalidateDetailScrollInfoCache(false);
            }
            if ((this.DataPresenter != null) && (UIElementHelper.IsVisibleInTree(this.DataPresenter, true) && (this.DataControl != null)))
            {
                this.DataPresenter.ClearScrollInfoAndUpdate();
            }
        }

        protected void OnAreUpdateRowButtonsShownChanged()
        {
            if (this.AreUpdateRowButtonsShown && !this.UpdateRowButtonsLocker.IsLocked)
            {
                RowData rowData = this.GetRowData(this.FocusedRowHandle);
                if (rowData != null)
                {
                    rowData.UpdateCancelValuesCache();
                }
            }
        }

        protected internal virtual void OnArrangeRow(IItem item, double offset, double height)
        {
        }

        internal bool OnBeforeChangePixelScrollOffset() => 
            this.ViewBehavior.NavigationStrategyBase.OnBeforeChangePixelScrollOffset(this);

        protected internal virtual void OnBeginArrangeRows()
        {
        }

        private void OnCanCancelEditFocusedRow(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanCancelEditFocusedRow();
        }

        private void OnCanClearColumnFilter(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanClearColumnFilter(this.GetColumnByCommandParameter(e.Parameter));
        }

        private void OnCanClearFilter(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanClearFilter();
        }

        private void OnCanDeleteFocusedRow(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanDeleteFocusedRow();
        }

        private void OnCanEditFocusedRow(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanEditFocusedRow();
        }

        private void OnCanEndEditFocusedRow(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanEndEditFocusedRow();
        }

        private void OnCanHideColumnChooser(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanHideColumnChooser();
        }

        private void OnCanShowColumnChooser(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanShowColumnChooser();
        }

        private void OnCanShowFilterEditor(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanShowFilterEditor(this.GetColumnByCommandParameter(e.Parameter));
        }

        private void OnCanShowUnboundExpressionEditor(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanShowUnboundExpressionEditor(e.Parameter);
        }

        private static void OnChangeColumnSortOrder(object sender, ExecutedRoutedEventArgs e)
        {
            ((DataViewBase) sender).OnColumnHeaderClick(e.Parameter as ColumnBase);
        }

        private void OnColumnChooserColumnDisplayModeChanged()
        {
            this.UpdateActualColumnChooserTemplate();
        }

        private void OnColumnChooserFactoryChanged()
        {
            this.IsColumnChooserVisible = false;
            this.ActualColumnChooser = this.CreateColumnChooser();
        }

        private void OnColumnChooserStateChanged()
        {
            if (!this.applyColumnChooserStateLocker.IsLocked)
            {
                this.ActualColumnChooser.ApplyState(this.ColumnChooserState);
            }
        }

        private static void OnColumnChooserStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).OnColumnChooserStateChanged();
        }

        protected internal virtual void OnColumnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
        }

        private void OnColumnFilterPopupModeChanged()
        {
            if (this.dataControl != null)
            {
                foreach (ColumnBase base2 in this.dataControl.ColumnsCore)
                {
                    base2.ResetFilterPopup();
                }
            }
        }

        internal void OnColumnHeaderClick(ColumnBase column)
        {
            if ((column != null) && (this.DataControl.DataControlOwner.CanSortColumn(column) && this.CommitEditing()))
            {
                this.OnColumnHeaderClick(column, ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers), ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers));
                this.DesignTimeAdorner.OnColumnHeaderClick();
            }
        }

        protected virtual void OnColumnHeaderClick(ColumnBase column, bool isShift, bool isCtrl)
        {
            ColumnHeaderClickEventArgs args1 = new ColumnHeaderClickEventArgs(column, isShift, isCtrl);
            args1.RoutedEvent = ColumnHeaderClickEvent;
            ColumnHeaderClickEventArgs e = args1;
            this.RaiseColumnHeaderClick(e);
            if (e.Handled)
            {
                isShift = e.IsShift;
                isCtrl = e.IsCtrl;
            }
            if (!e.Handled || e.AllowSorting)
            {
                this.SortInfoCore.OnColumnHeaderClick(column.FieldName, isShift, isCtrl, this.GetAllowGroupingSortingBySingleColumnOnly(), column.AllowedSortOrders, column.DefaultSortOrder, this.ColumnSortClearMode);
            }
        }

        private void OnColumnHeaderStyleChanged()
        {
            foreach (BaseColumn column in this.ColumnsCore)
            {
                column.UpdateActualHeaderStyle();
            }
        }

        protected internal virtual void OnComittingEditFormValue(int rowHandle)
        {
        }

        internal virtual void OnCurrentCellChanged()
        {
            if (this.CurrentCell != null)
            {
                if (this.NavigationStyle == GridViewNavigationStyle.Cell)
                {
                    this.RaiseItemSelectAutomationEvents(this.CurrentCell);
                }
                this.UpdateBorderForFocusedUIElement();
            }
        }

        protected virtual void OnCustomShouldSerializeProperty(CustomShouldSerializePropertyEventArgs e)
        {
            if (ReferenceEquals(e.DependencyProperty, AllowMovingProperty))
            {
                e.CustomShouldSerialize = false;
            }
        }

        private static void OnCustomShouldSerializeProperty(object sender, CustomShouldSerializePropertyEventArgs e)
        {
            ((DataViewBase) sender).OnCustomShouldSerializeProperty(e);
        }

        protected internal virtual void OnDataChanged(bool rebuildVisibleColumns, bool updateColumnsDataPropertiesOnly = false)
        {
            if (rebuildVisibleColumns && !this.IsLockUpdateColumnsLayout)
            {
                this.RebuildColumns();
                this.UpdateColumnsViewInfo(updateColumnsDataPropertiesOnly);
            }
            this.UpdateFilterPanel();
            if (this.SearchPanelColumnProvider != null)
            {
                this.SearchPanelColumnProvider.UpdateColumns();
            }
            this.UpdateWatchErrors();
            this.UpdateShowSearchPanelNavigationButtons();
        }

        protected virtual void OnDataControlChanged(DataControlBase oldValue)
        {
            if (oldValue != null)
            {
                oldValue.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.OnDataControlIsVisibleChanged);
            }
            if (this.dataControl == null)
            {
                this.HideColumnChooser();
            }
            else
            {
                this.dataControl.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnDataControlIsVisibleChanged);
                if ((this.dataControl.BandsLayoutCore != null) && ((this.DataControl.ColumnsCore.Count == 0) && DesignerProperties.GetIsInDesignMode(this.dataControl)))
                {
                    this.dataControl.BandsLayoutCore.FillColumns();
                }
                this.ValidateSelectionStrategy();
                if (this.dataControl.IsSelectionInitialized && (this.dataControl.CurrentItem != null))
                {
                    this.UpdateFocusedRowHandleCore();
                }
                this.OnDataReset();
                this.SetFocusedRowHandle(this.dataControl.DataProviderBase.CurrentControllerRow);
                this.SelectionStrategy.OnAssignedToGrid();
                this.UpdateBorderForFocusedUIElement();
                this.UpdateMasterDetailViewProperties();
                this.UpdateSummariesIgnoreNullValues();
                this.UpdateActualAllowCellMergeCore();
                this.RebuildDragManager();
            }
            this.UpdateIsKeyboardFocusWithinView();
        }

        protected internal virtual void OnDataControlDeserializeEnd()
        {
        }

        private void OnDataControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((bool) e.NewValue))
            {
                this.IsColumnChooserVisible = false;
            }
        }

        protected internal virtual void OnDataReset()
        {
            this.CacheVersion = Guid.NewGuid();
            this.Nodes.Clear();
            this.VisualDataTreeBuilder.GroupSummaryNodes.Clear();
            this.RootNodeContainer.OnMasterRooDataChanged();
            this.UpdateDataObjects(true, true);
            this.UpdateWatchErrors();
        }

        protected internal virtual void OnDataSourceReset()
        {
            this.SetFocusOnCurrentControllerRow();
            this.SelectionStrategy.OnDataSourceReset();
            this.UpdateCheckBoxSelectorState();
            this.UpdateVisibleGroupPanel();
        }

        protected virtual bool OnDeserializeAllowProperty(AllowPropertyEventArgs e) => 
            ((this.DataControl == null) || !ReferenceEquals(e.DependencyProperty, ActualShowSearchPanelProperty)) ? ((this.DataControl != null) ? this.DataControl.OnDeserializeAllowProperty(e) : false) : true;

        private static void OnDeserializeAllowProperty(object sender, AllowPropertyEventArgs e)
        {
            ((DataViewBase) sender).OnDeserializeAllowPropertyInternal(e);
        }

        private void OnDeserializeAllowPropertyInternal(AllowPropertyEventArgs e)
        {
            e.Allow = this.OnDeserializeAllowProperty(e);
        }

        protected virtual void OnDeserializeEnd(EndDeserializingEventArgs e)
        {
            if (this.columnChooserStateDeserialized)
            {
                bool isActualColumnChooserCreated = this.IsActualColumnChooserCreated;
                this.ActualColumnChooser.ApplyState(this.ColumnChooserState);
                if (!isActualColumnChooserCreated)
                {
                    this.ForceDestroyColumnChooser();
                }
            }
        }

        private static void OnDeserializeEnd(object sender, EndDeserializingEventArgs e)
        {
            ((DataViewBase) sender).OnDeserializeEnd(e);
        }

        private void OnDeserializeProperty(XtraPropertyInfoEventArgs e)
        {
            if (ReferenceEquals(e.DependencyProperty, ColumnChooserStateProperty))
            {
                this.columnChooserStateDeserialized = true;
            }
            else if (ReferenceEquals(e.DependencyProperty, ActualShowSearchPanelProperty))
            {
                e.Handled = true;
                this.ActualShowSearchPanel = Convert.ToBoolean(e.Info.Value);
            }
        }

        private static void OnDeserializeProperty(object sender, XtraPropertyInfoEventArgs e)
        {
            ((DataViewBase) sender).OnDeserializeProperty(e);
        }

        protected virtual void OnDeserializeStart(StartDeserializingEventArgs e)
        {
            this.ForceCreateColumnChooserStateInternal();
            this.columnChooserStateDeserialized = false;
        }

        private static void OnDeserializeStart(object sender, StartDeserializingEventArgs e)
        {
            ((DataViewBase) sender).OnDeserializeStart(e);
        }

        private static void OnEditorShowModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).UpdateEditorButtonVisibilities();
        }

        protected internal virtual void OnEndArrangeRows()
        {
        }

        private static void OnFadeSelectionOnLostFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).UpdateActualFadeSelectionOnLostFocus(d, e);
        }

        private void OnFocusedColumnChanged(GridColumnBase oldValue, GridColumnBase newValue)
        {
            if ((this.DataControl != null) && !ReferenceEquals(this.DataControl.CurrentColumn, (ColumnBase) base.GetValue(this.GetFocusedColumnProperty())))
            {
                this.DataControl.CurrentColumn = (ColumnBase) base.GetValue(this.GetFocusedColumnProperty());
            }
            this.RaiseFocusedColumnChanged(oldValue, newValue);
        }

        protected static void OnFocusedColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).OnFocusedColumnChanged((GridColumnBase) e.OldValue, (GridColumnBase) e.NewValue);
        }

        protected void OnFocusedRowChanged(object oldValue, object newValue)
        {
            if ((this.DataControl != null) && (this.DataControl.CurrentItem != base.GetValue(FocusedRowProperty)))
            {
                this.DataControl.SetCurrentItemCore(base.GetValue(FocusedRowProperty));
            }
            this.RaiseFocusedRowChanged(oldValue, newValue);
        }

        private void OnFocusedRowHandleChanged(int oldRowHandle)
        {
            this.FocusedRowHandleCore = this.FocusedRowHandle;
            if (!this.FocusedRowHandleChangedLocker.IsLocked)
            {
                this.OnFocusedRowHandleChangedCore(oldRowHandle);
            }
        }

        private static void OnFocusedRowHandleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).OnFocusedRowHandleChanged((int) e.OldValue);
        }

        protected virtual void OnFocusedRowHandleChangedCore(int oldRowHandle)
        {
            if (this.FocusedRowHandle != -2147483648)
            {
                this.MasterRootRowsContainer.FocusedView = this;
            }
            this.UpdateFullRowState(oldRowHandle);
            this.UpdateIsFocusedCellIfNeeded(oldRowHandle, null);
            this.UpdateFullRowState(this.FocusedRowHandle);
            this.UpdateIsFocusedCellIfNeeded(this.FocusedRowHandle, null);
            this.SelectionStrategy.OnFocusedRowHandleChanged(oldRowHandle);
            if (this.IsAdditionalRowFocused)
            {
                this.ViewBehavior.UpdateAdditionalFocusedRowData();
                this.ForceUpdateRowsState();
                this.DataControl.UpdateCurrentItem();
                this.SelectionStrategy.OnFocusedRowDataChanged();
                this.ScrollToFocusedRowIfNeeded();
            }
            else
            {
                this.DataProviderBase.MakeRowVisible(this.FocusedRowHandle);
                if (this.DataProviderBase.AllowUpdateFocusedRowData)
                {
                    this.UpdateFocusedRowData();
                }
                else
                {
                    this.DataControl.UpdateCurrentItem();
                }
                int currentIndex = this.DataProviderBase.CurrentIndex;
                this.DataProviderBase.CurrentControllerRow = this.FocusedRowHandle;
                this.ForceUpdateRowsState();
                if (currentIndex == this.DataProviderBase.CurrentIndex)
                {
                    this.RowsStateDirty = true;
                }
                this.UpdateBorderForFocusedUIElement();
                this.ScrollToFocusedRowIfNeeded();
                GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
                if (searchControl == null)
                {
                    GridSearchControlBase local1 = searchControl;
                }
                else
                {
                    searchControl.UpdateResultIndex();
                }
            }
        }

        protected internal virtual void OnGroupRowCheckBoxFieldNameChanged()
        {
        }

        protected internal virtual void OnHideEditor(CellEditorBase editor, bool closeEditor)
        {
            this.IsEditorOpen = false;
            this.ViewBehavior.OnHideEditor(editor);
        }

        internal void OnInvalidateHorizontalScrolling()
        {
            this.ViewBehavior.NavigationStrategyBase.OnInvalidateHorizontalScrolling(this);
        }

        protected void OnIsColumnChooserVisibleChanged()
        {
            if ((this.OriginationView == null) && !DesignerProperties.GetIsInDesignMode(this))
            {
                if (this.RootView.IsLoaded)
                {
                    this.OnIsColumnChooserVisibleChangedCore();
                }
                else
                {
                    this.ImmediateActionsManager.EnqueueAction(new Action(this.OnIsColumnChooserVisibleChangedCore));
                }
            }
        }

        private void OnIsColumnChooserVisibleChangedCore()
        {
            if (!this.IsColumnChooserVisible)
            {
                this.ActualColumnChooser.Hide();
                this.ActualColumnChooser.SaveState(this.RootView.ColumnChooserState);
                this.RaiseHiddenColumnChooser(new RoutedEventArgs(HiddenColumnChooserEvent));
            }
            else
            {
                this.RootView.UpdateAllOriginationViews(delegate (DataViewBase view) {
                    if (!ReferenceEquals(view, this))
                    {
                        view.HideColumnChooser();
                    }
                });
                this.RootView.ForceCreateColumnChooserStateInternal();
                this.ActualColumnChooser.ApplyState(this.RootView.ColumnChooserState);
                this.ActualColumnChooser.Show();
                this.RaiseShownColumnChooser(new RoutedEventArgs(ShownColumnChooserEvent));
            }
        }

        private void OnIsColumnFilterOpenedChanged()
        {
            this.DataPresenter.Do<DataPresenterBase>(presenter => presenter.SetManipulation(this.IsColumnFilterOpened));
            this.ResetIncrementalSearch();
        }

        private void OnIsEditingChanged(bool newValue)
        {
            this.IsEditingCore = newValue;
            this.AreUpdateRowButtonsShown = this.AreUpdateRowButtonsShown || (((this.ShowUpdateRowButtonsCore == ShowUpdateRowButtons.OnCellEditorOpen) & newValue) && (this.FocusedRowHandle != -2147483645));
            this.RootView.AreUpdateRowButtonsShown = this.AreUpdateRowButtonsShown;
            this.ViewBehavior.UpdateRowButtonsControl();
        }

        protected void OnIsHorizontalScrollBarVisibleChanged()
        {
            this.UpdateRowRectangleHelper.UpdatePosition(this.RootView);
        }

        private void OnIsKeyboardFocusWithinViewChanged()
        {
            this.IsKeyboardFocusWithinViewChanged.SafeRaise<EventHandler, EventArgs>(this, new EventArgs());
            this.UpdateRowDataFocusWithinState();
        }

        protected void OnIsSynchronizedWithCurrentItemChanged(bool oldValue, bool newValue)
        {
        }

        private void OnItemsSourceErrorInfoShowModeChanged()
        {
            this.UpdateCellDataErrors();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ((this.CurrentCellEditor == null) || this.CurrentCellEditor.Edit.CanHandleBubblingEvent)
            {
                this.MasterRootRowsContainer.FocusedView.InplaceEditorOwner.ProcessKeyDown(e);
            }
            if ((e.Key == Key.Escape) && (this.AreUpdateRowButtonsShown && !this.IsEditing))
            {
                this.CancelRowChangesCore(false);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            this.MasterRootRowsContainer.FocusedView.InplaceEditorOwner.ProcessKeyUp(e);
        }

        protected internal virtual void OnLastConstraintChanged()
        {
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.ViewBehavior.OnViewMouseLeave();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            this.ViewBehavior.OnMouseLeftButtonUp();
        }

        internal virtual void OnMultiSelectModeChanged()
        {
            this.EditorSetInactiveAfterClick = false;
            if (this.DataControl != null)
            {
                this.DataControl.ValidateMasterDetailConsistency();
                this.ClearSelectionCore();
            }
            if (this.DataControl != null)
            {
                this.ResetSelectionStrategy();
                this.UpdateSelectionSummary();
            }
        }

        protected virtual void OnNavigationStyleChanged()
        {
            this.ClearAllStates();
            this.Navigation = null;
            this.ValidateSelectionStrategy();
            this.UpdateActualAllowCellMergeCore();
        }

        protected static void OnNavigationStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataViewBase base2 = (DataViewBase) d;
            if (base2 != null)
            {
                if (base2.DataControl == null)
                {
                    base2.OnNavigationStyleChanged();
                }
                else
                {
                    Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__1164_0 ??= delegate (DataControlBase dataControl) {
                        if (dataControl.DataView != null)
                        {
                            dataControl.DataView.OnNavigationStyleChanged();
                        }
                    };
                    base2.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, updateOpenDetailMethod);
                }
            }
        }

        protected internal virtual void OnOpeningEditor()
        {
            this.IsEditorOpen = true;
        }

        internal void OnPostponedNavigationComplete()
        {
            this.SelectionStrategy.OnNavigationComplete(false);
            this.PostponedNavigationInProgress = false;
            this.MasterRootRowsContainer.UpdatePostponedData(true, false);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (ReferenceEquals(this, LayoutHelper.FindLayoutOrVisualParentObject((DependencyObject) e.OriginalSource, typeof(DataViewBase), false, null)))
            {
                this.ViewBehavior.ProcessPreviewKeyDown(e);
            }
        }

        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDoubleClick(e);
            DataViewBase objA = this.DataControl.FindTargetView(e.OriginalSource);
            if (ReferenceEquals(objA, this) || (objA.OriginationView != null))
            {
                objA.ViewBehavior.OnDoubleClick(e);
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            this.ViewBehavior.OnViewMouseMove(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            if ((!e.Handled && this.CanStartIncrementalSearch) && (this.TextSearchEngineRoot != null))
            {
                string text = e.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    bool? down = null;
                    this.ProcessTextSearch(text, down);
                    e.Handled = true;
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (this.DataControl != null)
            {
                Func<DataControlBase, DependencyObject> getTarget = <>c.<>9__1761_0;
                if (<>c.<>9__1761_0 == null)
                {
                    Func<DataControlBase, DependencyObject> local1 = <>c.<>9__1761_0;
                    getTarget = <>c.<>9__1761_0 = dataControl => dataControl.DataView;
                }
                this.DataControl.GetDataControlOriginationElement().NotifyPropertyChanged(this.DataControl, e.Property, getTarget, typeof(DataViewBase));
            }
        }

        internal void OnRecivedErrorNotificationFromSource(int listIndex)
        {
            this.UpdateRowValidationError(this.DataControl.DataProviderBase.GetRowHandleByListIndex(listIndex));
        }

        private void OnRuntimeLocalizationStringsChanged(GridRuntimeStringCollection oldValue)
        {
            if (this.RuntimeLocalizationStrings != null)
            {
                this.RuntimeLocalizationStrings.CollectionChanged += new NotifyCollectionChangedEventHandler(this.RuntimeLocalizationStringsCollectionChanged);
            }
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.RuntimeLocalizationStringsCollectionChanged);
            }
            this.RecreateLocalizationDescriptor();
        }

        private static void OnRuntimeLocalizationStringsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).OnRuntimeLocalizationStringsChanged(e.OldValue as GridRuntimeStringCollection);
        }

        protected virtual void OnSeachPanelCriteriaOperatorTypeChanged()
        {
            this.SearchPanelParseMode = (this.SearchPanelCriteriaOperatorType == CriteriaOperatorType.And) ? DevExpress.Xpf.Editors.SearchPanelParseMode.And : DevExpress.Xpf.Editors.SearchPanelParseMode.Mixed;
        }

        protected virtual void OnSearchControlChanged(DevExpress.Xpf.Editors.SearchControl oldValue, DevExpress.Xpf.Editors.SearchControl newValue)
        {
            this.SearchControlCore = newValue;
            this.UpdateSearchControlMRU(oldValue, newValue);
            this.UpdateSearchPanelColumnProviderBindings();
        }

        protected virtual void OnSelectedRowsSourceChanged()
        {
            if (this.DataControl != null)
            {
                this.DataControl.SelectedItems = (IList) base.GetValue(SelectedRowsSourceProperty);
            }
        }

        protected internal virtual void OnSelectionChanged(DevExpress.Data.SelectionChangedEventArgs e)
        {
            this.SelectionStrategy.OnSelectionChanged(e);
            this.UpdateCheckBoxSelectorState();
            this.UpdateSelectionSummary();
        }

        protected virtual void OnSerializeStart()
        {
            this.ForceCreateColumnChooserStateInternal();
            bool isActualColumnChooserCreated = this.IsActualColumnChooserCreated;
            this.ActualColumnChooser.SaveState(this.ColumnChooserState);
            if (!isActualColumnChooserCreated)
            {
                this.ForceDestroyColumnChooser();
            }
        }

        private static void OnSerializeStart(object sender, RoutedEventArgs e)
        {
            ((DataViewBase) sender).OnSerializeStart();
        }

        protected internal virtual void OnShowEditor(CellEditorBase editor)
        {
            this.ViewBehavior.OnShowEditor(editor);
        }

        private void OnShowEmptyTextChanged()
        {
            this.RaisePropertyChanged("ShowEmptyText");
        }

        protected void OnShowFocusedRectangleChanged()
        {
            this.UpdateBorderForFocusedUIElement();
        }

        protected virtual void OnShowLoadingRowChanged()
        {
            if (this.ShowLoadingRow)
            {
                this.ForceLayout();
            }
        }

        private void OnShowTotalSummaryChanged()
        {
            this.InvalidateParentTree();
            if ((this.DataControl != null) && (this.DataControl.AutomationPeer != null))
            {
                this.DataControl.AutomationPeer.ResetChildrenCachePlatformIndependent();
            }
        }

        protected void OnShowUpdateRowButtonsChanged()
        {
            this.CheckShowUpdateRowButtonsWithEditFormShowMode();
        }

        protected virtual void OnSummaryCalculationModeChanged()
        {
            if ((this.DataControl != null) && ((this.DataControl.DataProviderBase != null) && (this.DataControl.DataProviderBase.DataController != null)))
            {
                this.DataControl.DataProviderBase.DataController.UpdateGroupSummary();
                this.UpdateGroupSummary();
                this.DataControl.DataProviderBase.DataController.UpdateTotalSummary();
                this.UpdateColumnsTotalSummary();
            }
        }

        protected internal virtual void OnSummaryDataChanged()
        {
            this.UpdateColumnsTotalSummary();
        }

        private void OnTopRowIndexChanged()
        {
            this.ViewBehavior.OnTopRowIndexChanged();
        }

        protected static void OnUpdateColumnsAppearance(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).UpdateColumnsAppearance();
        }

        internal static void OnUpdateColumnsViewInfo(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).UpdateColumnsViewInfo(false);
        }

        protected void OnUpdateRowRectangleStyleChanged()
        {
            this.UpdateRowRectangle = null;
            if (this.RootView.IsLoaded)
            {
                this.EnqueueImmediateAction(() => this.UpdateRowRectangleHelper.UpdatePosition(this));
            }
        }

        protected internal virtual void OnUpdateRowsCore()
        {
            this.SelectionStrategy.UpdateCachedSelection();
        }

        private static void OnUpdateSelectionRectangle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).OnUpdateSelectionRectangleChanged();
        }

        private void OnUpdateSelectionRectangleChanged()
        {
            this.SelectionRectangle = null;
            this.ValidateSelectionStrategy();
        }

        protected static void OnUpdateShowValidationAttributeError(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).UpdateShowValidationAttributeError();
        }

        private void OnUseOnlyCurrentColumnInIncrementalSearchChanged()
        {
            if ((this.TextSearchEngineRoot != null) && !string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText))
            {
                this.RootView.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                    dataControl.DataView.ClearEditorHighlightingText();
                    dataControl.DataView.UpdateEditorHighlightingText(new TextHighlightingProperties(this.TextSearchEngineRoot.SeachText, FilterCondition.StartsWith), dataControl.DataView.IncrementalSearchColumns);
                }, null);
                this.UpdateAfterIncrementalSearch();
            }
        }

        private void OnValidatesOnNotifyDataErrorsChanged()
        {
            if (this.DataControl != null)
            {
                UpdateRowDataDelegate updateMethod = <>c.<>9__2217_0;
                if (<>c.<>9__2217_0 == null)
                {
                    UpdateRowDataDelegate local1 = <>c.<>9__2217_0;
                    updateMethod = <>c.<>9__2217_0 = delegate (RowData rowData) {
                        rowData.UpatePropertyChangeSubscriptionMode();
                        rowData.UpdateDataErrors(true);
                    };
                }
                this.UpdateRowData(updateMethod, false, true);
            }
        }

        internal void OnValidation(ColumnBase column, GridRowValidationEventArgs e)
        {
            column.OnValidation(e);
            e.Source = this;
            this.EventTargetView.RaiseValidateCell(e);
        }

        private static void OnValidationErrorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataViewBase) d).ValidationErrorPropertyChanged((BaseValidationError) e.NewValue);
        }

        protected internal virtual void OnVerticalScrollBarWidthChanged()
        {
        }

        protected virtual bool OnVisibleColumnsAssigned(bool changed) => 
            this.ViewBehavior.OnVisibleColumnsAssigned(changed);

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            if (parent == null)
            {
                BindingOperations.ClearBinding(this, ThemeManager.TreeWalkerProperty);
            }
            else
            {
                Binding binding = new Binding();
                object[] pathParameters = new object[] { ThemeManager.TreeWalkerProperty };
                binding.Path = new PropertyPath("(0)", pathParameters);
                binding.Source = parent;
                base.SetBinding(ThemeManager.TreeWalkerProperty, binding);
            }
            base.OnVisualParentChanged(oldParent);
        }

        protected abstract void PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters);
        internal void PerformDataResetAction()
        {
            if (!this.SupressCacheCleanCountLocker.IsLocked)
            {
                this.DataControl.UpdateRowsCore(true, true);
            }
            else
            {
                this.RootNodeContainer.ReGenerateMasterRootItems();
            }
        }

        internal void PerformNavigationOnLeftButtonDownCore(MouseButtonEventArgs e)
        {
            DependencyObject mouseLeftButtonDownSourceObject = this.GetMouseLeftButtonDownSourceObject(e);
            IDataViewHitInfo hitInfo = this.RootView.CalcHitInfoCore(mouseLeftButtonDownSourceObject);
            this.CanSelectLocker.DoLockedAction(() => this.SelectionStrategy.OnBeforeMouseLeftButtonDown(e));
            this.GetNavigation(this.GetRowHandleByTreeElement(mouseLeftButtonDownSourceObject)).ProcessMouse(mouseLeftButtonDownSourceObject);
            this.CanSelectLocker.DoLockedAction(() => this.SelectionStrategy.OnAfterMouseLeftButtonDown(hitInfo, e.StylusDevice, e.ClickCount));
            this.ViewBehavior.OnAfterMouseLeftButtonDown(hitInfo);
        }

        protected internal virtual void PerformUpdateGroupSummaryDataAction(Action action)
        {
        }

        public void PostEditor()
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.PostEditor(true);
            }
        }

        public void Print()
        {
            this.CheckPrinting();
            PrintHelper.Print(this);
        }

        public void PrintDirect()
        {
            this.CheckPrinting();
            PrintHelper.PrintDirect(this);
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Grid.DataViewBase.PrintDirect(string printerName) method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public void PrintDirect(PrintQueue queue)
        {
            this.CheckPrinting();
            PrintHelper.PrintDirect(this, queue);
        }

        public void PrintDirect(string printerName)
        {
            this.CheckPrinting();
            PrintHelper.PrintDirect(this, printerName);
        }

        internal void ProcessFocusedElement()
        {
            if (this.FocusedRowElement != null)
            {
                this.UpdateRowCellFocusIfNeeded();
                if (this.GetNavigation(this.FocusedRowHandle).ShouldRaiseRowAutomationEvents && ((this.NavigationStyle == GridViewNavigationStyle.Row) || this.DataProviderBase.IsGroupRowHandle(this.FocusedRowHandle)))
                {
                    this.RaiseItemSelectAutomationEvents(this.FocusedRowElement);
                }
            }
        }

        internal void ProcessFocusedViewChange()
        {
            if (!this.IsFocusedView)
            {
                this.FocusedRowHandle = -2147483648;
            }
            this.UpdateIsKeyboardFocusWithinView();
            this.UpdateFullRowState();
            this.ForceUpdateRowsState();
            if (this.IsFocusedView)
            {
                this.ScrollIntoView(this.FocusedRowHandle);
                this.ScrollToCurrentColumnIfNeeded();
            }
        }

        internal void ProcessIsKeyboardFocusWithinChanged()
        {
            if (!base.IsKeyboardFocusWithin)
            {
                this.AllowMouseMoveSelection = false;
            }
            if (!this.IsKeyboardFocusInSearchPanel() && (!this.IsKeyboardFocusInHeadersPanel() && (!this.IsKeyboardFocusWithinContentView() && !this.IsKeyboardFocusWithinEditForm())))
            {
                this.InplaceEditorOwner.ProcessIsKeyboardFocusWithinChanged();
            }
            this.UpdateIsKeyboardFocusWithinView();
            this.UpdateBorderForFocusedUIElement();
            if (this.IsKeyboardFocusWithinView)
            {
                this.ViewBehavior.OnGotKeyboardFocus();
            }
        }

        internal void ProcessKeyDown(KeyEventArgs e)
        {
            if (this.ViewBehavior.IsNavigationLocked)
            {
                e.Handled = true;
            }
            else
            {
                this.ProcessKeyDownCore(e);
                if (!e.Handled)
                {
                    base.OnKeyDown(e);
                }
            }
        }

        private void ProcessKeyDownCore(KeyEventArgs e)
        {
            if (!this.IsColumnFilterOpened && !this.IsContextMenuOpened)
            {
                this.SelectionStrategy.OnBeforeProcessKeyDown(e);
                if ((this.RootDataPresenter != null) && this.RootDataPresenter.IsInAction)
                {
                    this.RootDataPresenter.ForceCompleteContinuousActions();
                }
                if ((e.Key == Key.Tab) && (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) || this.AllowLeaveFocusOnTab))
                {
                    this.InplaceEditorOwner.MoveFocus(e);
                    e.Handled = true;
                }
                if (this.CanStartIncrementalSearch)
                {
                    if (e.Key == Key.Escape)
                    {
                        this.IncrementalSearchEnd();
                    }
                    else if (ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                    {
                        if (e.Key == Key.Down)
                        {
                            if (this.IncrementalSearchMoveNext())
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                        else if ((e.Key == Key.Up) && this.IncrementalSearchMovePrev())
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                if (e.Key == Key.F3)
                {
                    ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
                    if (ModifierKeysHelper.IsShiftPressed(keyboardModifiers))
                    {
                        this.SearchResultPrev();
                    }
                    else if (ModifierKeysHelper.NoModifiers(keyboardModifiers))
                    {
                        this.SearchResultNext();
                    }
                }
                this.SearchControlKeyDownProcessing(e);
                this.GetNavigation(this.FocusedRowHandle).ProcessKey(e);
                this.CanSelectLocker.DoLockedAction(() => this.SelectionStrategy.OnAfterProcessKeyDown(e));
            }
        }

        protected internal virtual void ProcessManipulationScaling(Vector scale)
        {
        }

        internal void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            DataViewBase objA = this.DataControl.FindTargetView(e.OriginalSource);
            if (ReferenceEquals(objA, this) || !objA.IsRootView)
            {
                objA.UpdateAllowMouseMoveSelection(e, (this.CanStartSelection() || ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers)) || ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers));
                UIElement originalSource = e.OriginalSource as UIElement;
                if ((this.SearchControl != null) && ((originalSource != null) && LayoutHelper.IsChildElement(this.SearchControl, originalSource)))
                {
                    this.SetSearchPanelFocus(true);
                }
                else
                {
                    this.SetSearchPanelFocus(false);
                    if (!this.EditFormManager.IsInlineFormChild(originalSource))
                    {
                        this.SelectionStrategy.OnBeforeProcessMouseDown();
                        objA.InplaceEditorOwner.ProcessMouseLeftButtonDown(e);
                    }
                }
            }
        }

        internal void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
            this.DataControl.FindTargetView(e.OriginalSource).InplaceEditorOwner.ProcessMouseRightButtonDown(e);
        }

        internal void ProcessPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            this.InplaceEditorOwner.ProcessPreviewLostKeyboardFocus(e);
        }

        internal void ProcessStylusUp(DependencyObject source)
        {
            this.DataControl.FindTargetView(source).InplaceEditorOwner.ProcessStylusUpCore(source);
        }

        private bool ProcessTextSearch(string text, bool? down)
        {
            bool flag = false;
            if ((this.TextSearchEngineRoot != null) && (this.DataControl != null))
            {
                if (this.TextSearchEngineRoot.DoSearch(text, (this.MasterRootRowsContainer.FocusedView.FocusedRowHandle >= 0) ? this.MasterRootRowsContainer.FocusedView.FocusedRowHandle : 0, (this.MasterRootRowsContainer.FocusedView.DataControl.CurrentColumn != null) ? this.MasterRootRowsContainer.FocusedView.DataControl.CurrentColumn.ActualVisibleIndex : 0, ((this.MasterRootRowsContainer.FocusedView.FocusedRowData == null) || (this.MasterRootRowsContainer.FocusedView.FocusedRowData.DetailIndents == null)) ? 0 : this.MasterRootRowsContainer.FocusedView.FocusedRowData.DetailIndents.Count, this.MasterRootRowsContainer.FocusedView, down))
                {
                    if (this.TextSearchEngineRoot.MatchedItemIndex != null)
                    {
                        DataViewBase view = this.TextSearchEngineRoot.MatchedItemIndex.View;
                        view.FocusedRowHandle = this.TextSearchEngineRoot.MatchedItemIndex.RowIndex;
                        view.MoveFocusedRow(view.DataControl.GetRowVisibleIndexByHandleCore(view.FocusedRowHandle));
                        if (this.TextSearchEngineRoot.MatchedItemIndex != null)
                        {
                            view.DataControl.CurrentColumn = view.VisibleColumnsCore[this.TextSearchEngineRoot.MatchedItemIndex.ColumnIndex];
                        }
                    }
                    flag = true;
                }
                if (this.TextSearchEngineRoot != null)
                {
                    if (!string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText))
                    {
                        this.RootView.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                            dataControl.DataView.ClearEditorHighlightingText();
                            dataControl.DataView.UpdateEditorHighlightingText(new TextHighlightingProperties(this.TextSearchEngineRoot.SeachText, FilterCondition.StartsWith), dataControl.DataView.IncrementalSearchColumns);
                        }, null);
                    }
                    else
                    {
                        Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__2041_0;
                        if (<>c.<>9__2041_0 == null)
                        {
                            Action<DataControlBase> local1 = <>c.<>9__2041_0;
                            updateOpenDetailMethod = <>c.<>9__2041_0 = delegate (DataControlBase dataControl) {
                                dataControl.DataView.UpdateEditorHighlightingText();
                            };
                        }
                        this.RootView.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                    }
                    flag = !string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText);
                }
                this.UpdateAfterIncrementalSearch();
            }
            return flag;
        }

        protected internal virtual object RaiseAddingNewRow() => 
            null;

        protected virtual void RaiseBeforeLayoutRefresh(CancelRoutedEventArgs e)
        {
            this.RaiseEventInOriginationView(e);
        }

        internal bool RaiseCanSelectRow(int rowHandle)
        {
            if (!this.CanRaiseCanSelectRow() || ((this.DataControl == null) || !this.DataControl.IsValidRowHandleCore(rowHandle)))
            {
                return true;
            }
            CanSelectRowEventArgs e = new CanSelectRowEventArgs(this, rowHandle, true);
            this.EventTargetView.CanSelectRow(this, e);
            return e.CanSelectRow;
        }

        internal bool RaiseCanUnselectRow(int rowHandle)
        {
            if (!this.CanRaiseCanUnselectRow() || ((this.DataControl == null) || !this.DataControl.IsValidRowHandleCore(rowHandle)))
            {
                return true;
            }
            CanUnselectRowEventArgs e = new CanUnselectRowEventArgs(this, rowHandle, true);
            this.EventTargetView.CanUnselectRow(this, e);
            return e.CanUnselectRow;
        }

        internal abstract void RaiseCellValueChanged(int rowHandle, ColumnBase column, object newValue, object oldValue);
        internal abstract void RaiseCellValueChanging(int rowHandle, ColumnBase column, object value, object oldValue);
        internal void RaiseColumnAllowEditingChanged()
        {
            foreach (ColumnBase base2 in this.ColumnsCore)
            {
                base2.RaiseAllowEditingChanged();
            }
        }

        protected internal virtual void RaiseColumnHeaderClick(ColumnHeaderClickEventArgs e)
        {
            this.RaiseEventInOriginationView(e);
        }

        internal void RaiseCommandsCanExecuteChanged()
        {
            this.Commands.RaiseCanExecutedChanged();
        }

        protected internal abstract bool RaiseCopyingToClipboard(CopyingToClipboardEventArgsBase e);
        internal virtual bool RaiseCustomDataUpdateFormatCondition(CustomDataUpdateFormatConditionEventArgsSource argsSource) => 
            false;

        internal abstract string RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string displayText);
        internal abstract bool? RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string originalDisplayText, out string displayText);
        protected virtual void RaiseCustomFilterDisplayText(CustomFilterDisplayTextEventArgs e)
        {
            this.RaiseEventInOriginationView(e);
        }

        protected internal virtual void RaiseCustomScrollAnimation(CustomScrollAnimationEventArgs e)
        {
            base.RaiseEvent(e);
        }

        internal void RaiseEditorInitialized(ColumnBase column, IBaseEdit editor)
        {
            if (this.EditorInitialized != null)
            {
                this.EditorInitialized(this, new EditorInitializedEventArgs(column, editor));
            }
        }

        protected void RaiseEventInOriginationView(RoutedEventArgs e)
        {
            this.EventTargetView.RaiseEvent(e);
        }

        protected internal virtual void RaiseFilterPopupEvent(ColumnBase column, PopupBaseEdit popupBaseEdit, Lazy<ExcelColumnFilterSettings> excelColumnFilterSettingsLazy)
        {
            FilterPopupEventArgs e = new FilterPopupEventArgs(column, popupBaseEdit, excelColumnFilterSettingsLazy);
            e.RoutedEvent = ShowFilterPopupEvent;
            this.RaiseEventInOriginationView(e);
        }

        internal void RaiseFocusedColumnChanged(GridColumnBase oldValue, GridColumnBase newValue)
        {
            FocusedColumnChangedEventArgs e = new FocusedColumnChangedEventArgs(this, oldValue, newValue);
            e.RoutedEvent = FocusedColumnChangedEvent;
            this.RaiseEventInOriginationView(e);
        }

        internal void RaiseFocusedRowChanged(object oldValue, object newValue)
        {
            FocusedRowChangedEventArgs e = new FocusedRowChangedEventArgs(this, oldValue, newValue);
            e.RoutedEvent = FocusedRowChangedEvent;
            this.RaiseEventInOriginationView(e);
        }

        internal void RaiseFocusedRowHandleChanged()
        {
            FocusedRowHandleChangedEventArgs e = new FocusedRowHandleChangedEventArgs(this.FocusedRowData);
            e.RoutedEvent = FocusedRowHandleChangedEvent;
            base.RaiseEvent(e);
        }

        internal void RaiseFocusedViewChanged(DataViewBase oldView, DataViewBase newView)
        {
            FocusedViewChangedEventArgs e = new FocusedViewChangedEventArgs(oldView, newView);
            e.RoutedEvent = FocusedViewChangedEvent;
            base.RaiseEvent(e);
            this.RaisePropertyChanged("FocusedView");
        }

        protected virtual void RaiseHiddenColumnChooser(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        internal abstract void RaiseHiddenEditor(int rowHandle, ColumnBase column, IBaseEdit editCore);
        private void RaiseItemSelectAutomationEvents(DependencyObject obj)
        {
            if (!ReferenceEquals(this.previousAutomationObject, obj) && (obj != null))
            {
                this.previousAutomationObject = obj;
                if (this.DataControl.AutomationPeer != null)
                {
                    AutomationPeer automationPeer = this.GetAutomationPeer(obj);
                    if (automationPeer != null)
                    {
                        automationPeer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
                        automationPeer.RaiseAutomationEvent(AutomationEvents.AutomationFocusChanged);
                    }
                }
            }
        }

        protected internal virtual bool RaisePastingFromClipboard()
        {
            PastingFromClipboardEventArgs e = new PastingFromClipboardEventArgs(this.DataControl, PastingFromClipboardEvent);
            this.RaiseEventInOriginationView(e);
            return e.Handled;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void RaiseResizingComplete()
        {
            EventHandler resizingComplete = this.ResizingComplete;
            if (resizingComplete != null)
            {
                resizingComplete(this, new EventArgs());
            }
        }

        protected internal void RaiseRowAnimationBegin(LoadingAnimationHelper loadingAnimationHelper, bool isGroupRow)
        {
            RowAnimationBeginEventArgs args1 = new RowAnimationBeginEventArgs(this);
            args1.RoutedEvent = RowAnimationBeginEvent;
            args1.IsGroupRow = isGroupRow;
            RowAnimationBeginEventArgs e = args1;
            this.RaiseEventInOriginationView(e);
            loadingAnimationHelper.CustomAnimation = e.AnimationTimeline;
            loadingAnimationHelper.CustomPropertyPath = e.PropertyPath;
            loadingAnimationHelper.AddedEffect = e.AddedEffect;
        }

        protected virtual void RaiseRowUpdatedBase(int rowHandle)
        {
        }

        protected internal void RaiseSelectionChanged(DevExpress.Data.SelectionChangedEventArgs e)
        {
            this.RaiseSelectionChanged(this.CreateSelectionChangedEventArgs(e));
        }

        protected internal virtual void RaiseSelectionChanged(GridSelectionChangedEventArgs e)
        {
        }

        protected internal virtual void RaiseShowGridMenu(GridMenuEventArgs e)
        {
            this.RaiseEventInOriginationView(e);
        }

        internal abstract bool RaiseShowingEditor(int rowHanlde, ColumnBase columnBase);
        protected virtual void RaiseShownColumnChooser(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        internal abstract void RaiseShownEditor(int rowHandle, ColumnBase column, IBaseEdit editCore);
        protected internal abstract void RaiseValidateCell(GridRowValidationEventArgs e);
        protected internal void RebuildColumnChooserColumns()
        {
            if (!this.ColumnsCore.IsLockUpdate)
            {
                this.ViewBehavior.RebuildColumnChooserColumns();
            }
        }

        internal void RebuildColumns()
        {
            this.UpdateColumnsCore(new Action(this.ViewBehavior.RebuildColumns));
        }

        protected void RebuildDragManager()
        {
            Action<NativeDragManager> action = <>c.<>9__2206_0;
            if (<>c.<>9__2206_0 == null)
            {
                Action<NativeDragManager> local1 = <>c.<>9__2206_0;
                action = <>c.<>9__2206_0 = x => x.IsActive = false;
            }
            this.DragDropManager.Do<NativeDragManager>(action);
            this.DragDropManager = null;
            if (this.AllowDragDrop && (!this.IsDesignTime && ((this.OriginationView == null) && (this.DataControl != null))))
            {
                this.DragDropManager = this.CreateDragManagerBuilder().Build(this);
                this.DragDropManager.IsActive = true;
            }
        }

        private void RecreateLocalizationDescriptor()
        {
            this.LocalizationDescriptor = new DevExpress.Xpf.Grid.LocalizationDescriptor(this.RuntimeLocalizationStrings);
        }

        protected internal virtual void RefreshImmediateUpdateRowPositionProperty()
        {
        }

        private static void RegisterClassCommandBindings()
        {
            // Unresolved stack state at '00000616'
        }

        internal void RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
            this.logicalChildren.Remove(child);
        }

        protected abstract void RemoveCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler);
        private void RepeatDataPresenterAction(Action<IScrollInfo> action)
        {
            action(this.DataPresenter);
        }

        internal virtual bool RequestUIUpdate()
        {
            Func<DataViewBase, bool> getResult = <>c.<>9__1420_0;
            if (<>c.<>9__1420_0 == null)
            {
                Func<DataViewBase, bool> local1 = <>c.<>9__1420_0;
                getResult = <>c.<>9__1420_0 = view => view.RequestUIUpdateCore(false);
            }
            return this.EnumerateViewsForCommitEditingAndRequestUIUpdate(getResult);
        }

        private bool RequestUIUpdateCore(bool cleanError = false)
        {
            if (this.EnqueueScrollIntoViewLocker.IsLocked)
            {
                return true;
            }
            if (this.RootDataPresenter != null)
            {
                this.RootDataPresenter.ForceCompleteCurrentAction();
            }
            if (!this.RootView.LockEditorClose)
            {
                this.CloseEditor(this.Navigation.NavigationMouseLocker.IsLocked, cleanError);
            }
            return (this.EditFormManager.RequestUIUpdate() ? ((!this.AreUpdateRowButtonsShown || (this.UpdateButtonsModeAllowRequestUI != 0)) ? (!this.UpdateRowButtonsLocker.IsLocked ? !this.HasCellEditorError : true) : this.UpdateRowButtonsRequestUI()) : false);
        }

        protected internal virtual void ResetDataProvider()
        {
        }

        protected internal virtual void ResetHeadersChildrenCache()
        {
        }

        protected internal virtual void ResetIncrementalSearch()
        {
            if ((this.RootView != null) && ((this.RootView.DataControl != null) && (!this.ScrollAnimationLocker.IsLocked && (!this.layoutUpdatedLocker.IsLocked && !this.IsLockUpdateColumnsLayout))))
            {
                if ((this.TextSearchEngineRoot != null) && !string.IsNullOrEmpty(this.TextSearchEngineRoot.SeachText))
                {
                    Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__2042_0;
                    if (<>c.<>9__2042_0 == null)
                    {
                        Action<DataControlBase> local1 = <>c.<>9__2042_0;
                        updateOpenDetailMethod = <>c.<>9__2042_0 = dataControl => dataControl.DataView.ClearEditorHighlightingText();
                    }
                    this.RootView.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                }
                else
                {
                    Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__2042_1;
                    if (<>c.<>9__2042_1 == null)
                    {
                        Action<DataControlBase> local2 = <>c.<>9__2042_1;
                        updateOpenDetailMethod = <>c.<>9__2042_1 = dataControl => dataControl.DataView.UpdateEditorHighlightingText();
                    }
                    this.RootView.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                }
            }
            this.TextSearchEngineRoot = new TableTextSearchEngine(new Func<string, TableTextSearchEngine.TableIndex, bool, bool, DataViewBase, TableTextSearchEngine.TableIndex>(IncrementalSearchHelper.SearchCallback), this.IncrementalSearchClearDelay);
        }

        internal void ResetMenu()
        {
            this.DataControlMenu.Reset();
        }

        protected internal virtual void ResetSelectionStrategy()
        {
            this.SelectionStrategy = null;
            if (this.DataControl != null)
            {
                this.SelectionStrategy.OnAssignedToGrid();
                this.UpdateBorderForFocusedUIElement();
                this.UpdateIsSelected();
            }
        }

        private void RuntimeLocalizationStringsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RecreateLocalizationDescriptor();
        }

        public void ScrollIntoView(int rowHandle)
        {
            this.ScrollIntoViewCore(rowHandle, false, null);
        }

        public void ScrollIntoView(object row)
        {
            this.ScrollIntoView(this.DataProviderBase.FindRowByRowValue(row));
        }

        internal void ScrollIntoViewCore(int rowHandle, bool useLock, Action postAction = null)
        {
            this.ScrollIntoViewLocker.DoIfNotLocked(delegate {
                this.DataProviderBase.MakeRowVisible(rowHandle);
                this.EnqueueScrollIntoView(rowHandle, useLock, postAction);
            });
        }

        private void ScrollToCurrentColumnIfNeeded()
        {
            if (this.IsFocusedView)
            {
                this.ViewBehavior.NavigationStrategyBase.OnNavigationIndexChanged(this);
                this.EnqueueMakeCellVisible();
            }
        }

        private void ScrollToFocusedRowIfNeeded()
        {
            if (this.RootView.AllowScrollToFocusedRow && (!this.DataControl.IsRestoreOffsetInProgress && (!this.DataControl.IsDataResetLocked && this.IsFocusedView)))
            {
                this.ScrollIntoView(this.FocusedRowHandle);
            }
        }

        protected internal virtual void ScrollToHorizontalOffset(double offset, bool useAccumulator)
        {
            FrameworkElement firstVisibleRowElement = null;
            if (this.DataPresenter.CanApplyScroll(out firstVisibleRowElement))
            {
                double delta = useAccumulator ? this.DataPresenter.accumulatorHorizontal.GetCorrectedDelta(useAccumulator, firstVisibleRowElement.ActualWidth, -offset) : offset;
                this.ViewBehavior.ChangeHorizontalOffsetBy(delta);
            }
        }

        protected internal virtual void ScrollToVerticalOffset(double offset, bool useAccumulator)
        {
            FrameworkElement firstVisibleRowElement = null;
            if (this.DataPresenter.CanApplyScroll(out firstVisibleRowElement))
            {
                double delta = useAccumulator ? this.DataPresenter.accumulatorVertical.GetCorrectedDelta(!this.ViewBehavior.AllowPerPixelScrolling, firstVisibleRowElement.ActualHeight, offset) : offset;
                this.ViewBehavior.ChangeVerticalOffsetBy(delta);
            }
        }

        private void SearchColumnsChanged(string columnsString)
        {
            this.ApplySearchColumns();
        }

        protected virtual void SearchControlKeyButtonUpProcessing(KeyEventArgs e)
        {
        }

        protected void SearchControlKeyDownProcessing(KeyEventArgs e)
        {
            if (!this.IsRootView && (this.RootView != null))
            {
                this.RootView.SearchControlKeyDownProcessing(e);
            }
            else
            {
                if ((e.Key == Key.F) && ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                {
                    if (!this.ActualShowSearchPanel && this.CanShowSearchPanel())
                    {
                        this.FocusSearchPanel();
                        this.ActualShowSearchPanel = true;
                    }
                    this.FocusSearchPanel();
                }
                this.SearchControlKeyButtonUpProcessing(e);
            }
        }

        protected virtual void SearchPanelHorizontalAlignmentChanged()
        {
            this.ActualSearchPanelHorizontalAlignment = (this.SearchPanelHorizontalAlignment != null) ? this.SearchPanelHorizontalAlignment.Value : HorizontalAlignment.Left;
        }

        public void SearchResultNext()
        {
            this.SearchResultNextCore(false);
        }

        private void SearchResultNextCore(bool useCurrentPosition)
        {
            if (!this.IsVirtualSource)
            {
                GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
                if (searchControl != null)
                {
                    if (this.SearchPanelFindMode == FindMode.FindClick)
                    {
                        searchControl.FindCommandExecuted();
                    }
                    else
                    {
                        searchControl.DoValidate();
                    }
                }
                int visibleRowCount = this.DataControl.VisibleRowCount;
                this.searchResult = MovementSearchResults.MoveSearchResult(this, true, useCurrentPosition);
                if (this.DataControl.VisibleRowCount != visibleRowCount)
                {
                    this.UpdateScrollBarAnnotations();
                }
            }
        }

        public void SearchResultPrev()
        {
            if (!this.IsVirtualSource)
            {
                GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
                if ((searchControl != null) && (this.SearchPanelFindMode == FindMode.Always))
                {
                    searchControl.DoValidate();
                }
                int visibleRowCount = this.DataControl.VisibleRowCount;
                this.searchResult = MovementSearchResults.MoveSearchResult(this, false, false);
                if (this.DataControl.VisibleRowCount != visibleRowCount)
                {
                    this.UpdateScrollBarAnnotations();
                }
            }
        }

        [Obsolete("Use the DataControlBase.SelectAll method instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void SelectAll()
        {
            this.SelectAllCore();
        }

        internal void SelectAllCore()
        {
            this.SelectionStrategy.SelectAll();
        }

        private void SelectItemRowsByValue(string fieldName, object value)
        {
            OperationCompleted[] completed = new OperationCompleted[] { delegate (object arg) {
                int num = (int) arg;
                if (num != -2147483648)
                {
                    this.DataControl.SelectItem(num);
                }
            } };
            int rowHandle = this.DataProviderBase.DataController.FindRowByValue(fieldName, value, completed);
            if (rowHandle != -2147483648)
            {
                this.DataControl.SelectItem(rowHandle);
            }
        }

        internal void SelectRangeCore(int startRowHandle, int endRowHandle)
        {
            this.SelectionStrategy.SelectRange(startRowHandle, endRowHandle);
        }

        internal void SelectRowByValue(ColumnBase column, object value)
        {
            this.SelectRowByValueCore(column.FieldName, value, true);
        }

        internal void SelectRowByValue(string fieldName, object value)
        {
            this.SelectRowByValueCore(fieldName, value, true);
        }

        private void SelectRowByValueCore(string fieldName, object value, bool first)
        {
            OperationCompleted[] completed = new OperationCompleted[] { delegate (object arg) {
                if (this.DataControl.VisibleRowCount != 0)
                {
                    int rowHandle = (int) arg;
                    if (rowHandle != -2147483648)
                    {
                        this.FocusedRowHandle = rowHandle;
                        this.DataControl.SelectItem(rowHandle);
                    }
                    else if (first)
                    {
                        this.SelectRowByValueCore(fieldName, value, false);
                    }
                }
            } };
            int num = this.DataProviderBase.FindRowByValue(fieldName, value, completed);
            if ((num != -2147483648) && (num != -2147483638))
            {
                this.FocusedRowHandle = num;
            }
        }

        internal void SelectRowCore(int rowHandle)
        {
            this.SelectionStrategy.SelectRow(rowHandle);
        }

        internal void SelectRowForce()
        {
            this.SelectionStrategy.SelectRowForce();
        }

        internal void SelectRowsByValues(ColumnBase column, IEnumerable<object> values)
        {
            this.SelectRowsByValuesCore(column.FieldName, values);
        }

        internal void SelectRowsByValues(string fieldName, IEnumerable<object> values)
        {
            this.SelectRowsByValuesCore(fieldName, values);
        }

        private void SelectRowsByValuesCore(string fieldName, IEnumerable<object> values)
        {
            if (this.IsMultiSelection && ((values != null) && (this.FocusedRowHandle != -2147483638)))
            {
                foreach (object value in values)
                {
                    OperationCompleted[] completed = new OperationCompleted[] { delegate (object arg) {
                        if (this.DataControl.VisibleRowCount != 0)
                        {
                            int num = (int) arg;
                            if (num == -2147483648)
                            {
                                this.SelectItemRowsByValue(fieldName, value);
                            }
                            else
                            {
                                this.DataControl.SelectItem(num);
                            }
                        }
                    } };
                    int rowHandle = this.DataProviderBase.FindRowByValue(fieldName, value, completed);
                    if (rowHandle != -2147483648)
                    {
                        this.DataControl.SelectItem(rowHandle);
                    }
                    if (rowHandle == -2147483638)
                    {
                        this.FocusedRowHandle = rowHandle;
                    }
                }
            }
        }

        internal void SetActiveEditor()
        {
            this.SetActiveEditor((BaseEdit) BaseEditHelper.GetBaseEdit(this.InplaceEditorOwner.ActiveEditor));
        }

        internal void SetActiveEditor(BaseEdit baseEdit)
        {
            this.IsEditing = baseEdit != null;
            this.ActiveEditor = baseEdit;
            this.ResetIncrementalSearch();
        }

        protected void SetActualClipboardOptions(ClipboardOptions options)
        {
            options.AllowCsvFormat = this.ClipboardCopyOptions.HasFlag(DevExpress.Xpf.Grid.ClipboardCopyOptions.Csv) ? DefaultBoolean.True : DefaultBoolean.False;
            options.AllowExcelFormat = this.ClipboardCopyOptions.HasFlag(DevExpress.Xpf.Grid.ClipboardCopyOptions.Excel) ? DefaultBoolean.True : DefaultBoolean.False;
            options.AllowHtmlFormat = this.ClipboardCopyOptions.HasFlag(DevExpress.Xpf.Grid.ClipboardCopyOptions.Html) ? DefaultBoolean.True : DefaultBoolean.False;
            options.AllowRtfFormat = this.ClipboardCopyOptions.HasFlag(DevExpress.Xpf.Grid.ClipboardCopyOptions.Rtf) ? DefaultBoolean.True : DefaultBoolean.False;
            options.AllowTxtFormat = this.ClipboardCopyOptions.HasFlag(DevExpress.Xpf.Grid.ClipboardCopyOptions.Txt) ? DefaultBoolean.True : DefaultBoolean.False;
            options.ClipboardMode = (this.ClipboardMode == DevExpress.Xpf.Grid.ClipboardMode.Formatted) ? DevExpress.Export.ClipboardMode.Formatted : DevExpress.Export.ClipboardMode.PlainText;
            options.CopyColumnHeaders = this.ActualClipboardCopyWithHeaders ? DefaultBoolean.True : DefaultBoolean.False;
        }

        public static void SetColumnHeaderDragIndicatorSize(DependencyObject element, double value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ColumnHeaderDragIndicatorSizeProperty, value);
        }

        protected internal virtual bool SetDataAwareClipboardData(IClipboardFormatted clipboardDataProvider, bool callCopyFromCommand) => 
            false;

        protected virtual void SetDataControl(DataControlBase newValue)
        {
            if (!ReferenceEquals(this.dataControl, newValue))
            {
                DataControlBase dataControl = this.dataControl;
                this.dataControl = newValue;
                this.OnDataControlChanged(dataControl);
            }
        }

        internal virtual void SetFocusedRectangleOnCell()
        {
            double actualBandLeftSeparatorWidthCore = 0.0;
            double rightIndent = 0.0;
            if ((this.FocusedView.CurrentCellEditor != null) && ((this.DataControl != null) && (this.DataControl.CurrentColumn != null)))
            {
                actualBandLeftSeparatorWidthCore = this.DataControl.CurrentColumn.ActualBandLeftSeparatorWidthCore;
                rightIndent = this.DataControl.CurrentColumn.ActualBandRightSeparatorWidthCore;
            }
            this.SetFocusedRectangleOnElement((this.FocusedView.CurrentCellEditor != null) ? (this.FocusedView.CurrentCell as FrameworkElement) : null, this.GetCellFocusedRectangleTemplate(), this.CalcCellFocusedRectangleOffset() + actualBandLeftSeparatorWidthCore, rightIndent);
        }

        protected void SetFocusedRectangleOnElement(FrameworkElement element, ControlTemplate template, double leftIndent = 0.0, double rightIndent = 0.0)
        {
            if ((element == null) || !UIElementHelper.IsVisibleInTree(element, true))
            {
                this.ClearFocusedRectangle();
            }
            else if ((this.RootView.FocusRectPresenter != null) && (this.RootDataPresenter != null))
            {
                this.RootView.FocusRectPresenter.Owner = element;
                this.RootView.FocusRectPresenter.ChildTemplate = template;
                this.RootView.FocusRectPresenter.UpdateRendering(leftIndent, rightIndent);
                this.RootView.FocusRectPresenter.Visibility = Visibility.Visible;
            }
        }

        internal virtual void SetFocusedRectangleOnGroupRow()
        {
            this.SetFocusedRectangleOnRowData(this.GetGroupRowFocusedRectangleTemplate());
        }

        internal virtual void SetFocusedRectangleOnRow()
        {
            this.SetFocusedRectangleOnRowData(this.GetRowFocusedRectangleTemplate());
        }

        protected void SetFocusedRectangleOnRowData(ControlTemplate template)
        {
            RowData rowData = this.GetRowData(this.FocusedRowHandle);
            if (rowData == null)
            {
                this.ClearFocusedRectangle();
            }
            else
            {
                IFocusedRowBorderObject rowElement = rowData.RowElement as IFocusedRowBorderObject;
                FrameworkElement rowDataContent = rowData.RowElement;
                double leftIndent = 0.0;
                if (rowElement != null)
                {
                    rowDataContent = rowElement.RowDataContent;
                    leftIndent = rowElement.LeftIndent;
                }
                this.SetFocusedRectangleOnElement(rowDataContent, template, leftIndent, 0.0);
            }
        }

        internal void SetFocusedRowHandle(int rowHandle)
        {
            this.FocusedRowHandleChangedLocker.DoIfNotLocked(() => this.SetCurrentValue(FocusedRowHandleProperty, rowHandle));
        }

        protected internal void SetFocusOnCurrentControllerRow()
        {
            if ((this.DataControl != null) && !this.SelectionStrategy.IsFocusedRowHandleLocked)
            {
                this.SetFocusedRowHandle(this.DataProviderBase.CurrentControllerRow);
            }
        }

        internal void SetFocusOnCurrentItem()
        {
            if (this.DataControl.CurrentItem != null)
            {
                this.UpdateFocusedRowHandleCore();
            }
            else
            {
                this.SetFocusedRowHandle(-2147483648);
            }
        }

        internal void SetFocusToRowCell()
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.Edit.SetKeyboardFocus();
            }
            else if (this.FocusedRowElement != null)
            {
                KeyboardHelper.Focus((UIElement) this.FocusedRowElement);
            }
            else
            {
                KeyboardHelper.Focus((UIElement) this);
            }
        }

        internal void SetHasErrors(bool value)
        {
            this.HasErrors = value;
            if (this.RootView.DataControl.DetailDescriptorCore != null)
            {
                if (this.RootView.ErrorWatch.HasError)
                {
                    this.RootView.HasErrors = true;
                }
                else
                {
                    bool hasError = false;
                    this.RootView.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                        if (!hasError && ((dataControl != null) && (dataControl.DataView != null)))
                        {
                            hasError = dataControl.DataView.ErrorWatch.HasError;
                        }
                    }, null);
                    if (hasError)
                    {
                        this.RootView.HasErrors = true;
                    }
                    else
                    {
                        this.RootView.HasErrors = this.RootView.ErrorWatch.HasError;
                    }
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetIsFocusedCell(DependencyObject dependencyObject, bool focused)
        {
            dependencyObject.SetValue(IsFocusedCellProperty, focused);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetIsFocusedRow(DependencyObject dependencyObject, bool focused)
        {
            dependencyObject.SetValue(IsFocusedRowProperty, focused);
        }

        public static void SetRowHandle(DependencyObject element, RowHandle value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(RowHandleProperty, value);
        }

        internal void SetSearchPanelFocus(bool isKeyboardFocusInSearchPanel = false)
        {
            this.isKeyboardFocusInSearchPanel = isKeyboardFocusInSearchPanel;
        }

        protected internal virtual void SetSummariesIgnoreNullValues(bool value)
        {
        }

        protected abstract void SetVisibleColumns(IList<ColumnBase> columns);
        internal virtual bool ShouldCalcBestFitGroupSummary(ColumnBase column) => 
            false;

        protected internal virtual bool ShouldChangeForwardIndex(int rowHandle)
        {
            DataViewBase view = null;
            int visibleIndex = -1;
            this.GetLastScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            int rowVisibleIndexByHandleCore = this.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);
            return ((rowVisibleIndexByHandleCore <= (visibleIndex + 1)) && (ReferenceEquals(view, this) && (!ScrollActionsHelper.IsRowElementVisible(this, rowHandle) ? (ScrollActionsHelper.GetGroupSummaryRowCountBeforeRow(this, rowVisibleIndexByHandleCore, true) > 0) : true)));
        }

        private bool ShouldScrollOnePageBack()
        {
            KeyValuePair<DataViewBase, int> commonCurrentItem = new KeyValuePair<DataViewBase, int>();
            KeyValuePair<DataViewBase, int> commonTargetItem = new KeyValuePair<DataViewBase, int>();
            DataViewBase view = null;
            int visibleIndex = -1;
            this.GetFirstScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            if ((view != null) && (view.IsPagingMode && (visibleIndex == 0)))
            {
                return false;
            }
            this.FindCommonViewVisibleIndexes(view, visibleIndex, out commonCurrentItem, out commonTargetItem);
            return ((commonCurrentItem.Value - commonTargetItem.Value) <= 0);
        }

        private bool ShouldScrollOnePageForward()
        {
            if (this.IsTopNewItemRowFocused)
            {
                return false;
            }
            KeyValuePair<DataViewBase, int> commonCurrentItem = new KeyValuePair<DataViewBase, int>();
            KeyValuePair<DataViewBase, int> commonTargetItem = new KeyValuePair<DataViewBase, int>();
            DataViewBase view = null;
            int visibleIndex = -1;
            this.GetLastScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            if ((view == null) || (view.IsPagingMode && (visibleIndex == view.LastVisibleIndexOnPage)))
            {
                return false;
            }
            this.FindCommonViewVisibleIndexes(view, visibleIndex, out commonCurrentItem, out commonTargetItem);
            int num2 = commonCurrentItem.Value - commonTargetItem.Value;
            if (num2 == 0)
            {
                int item = this.ConvertVisibleIndexToScrollIndex(this.DataProviderBase.CurrentIndex);
                if ((item > view.ConvertVisibleIndexToScrollIndex(visibleIndex)) && view.DataControl.GetParentFixedRowsScrollIndexes(visibleIndex).Contains(item))
                {
                    return false;
                }
            }
            return (num2 >= 0);
        }

        [Browsable(false)]
        public bool ShouldSerializeColumnChooserColumns(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeColumnChooserColumnsSortOrderComparer(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeSearchPanelImmediateMRUPopup(XamlDesignerSerializationManager manager) => 
            false;

        internal bool ShouldUpdateRow(object row) => 
            this.DataControl.ShouldUpdateRow(row);

        internal void ShowCheckBoxInHeaderColumnsChanged()
        {
            this.ShowCheckBoxInHeaderColumns.Clear();
            foreach (ColumnBase base2 in this.VisibleColumnsCore)
            {
                if (base2.ActualShowCheckBoxInHeader)
                {
                    this.ShowCheckBoxInHeaderColumns.Add(base2);
                }
            }
        }

        public void ShowColumnChooser()
        {
            this.IsColumnChooserVisible = true;
        }

        internal FloatingContainer ShowDialogContent(FrameworkElement dialogContent, Size size, FloatingContainerParameters parameters)
        {
            PopupBaseEdit popupBaseEdit = BaseEdit.GetOwnerEdit(this) as PopupBaseEdit;
            bool? staysPopupOpen = false;
            popupBaseEdit.Do<PopupBaseEdit>(delegate (PopupBaseEdit edit) {
                staysPopupOpen = edit.StaysPopupOpen;
                edit.StaysPopupOpen = true;
            });
            FloatingContainer floatingContainer = null;
            if (!this.DesignTimeAdorner.IsDesignTime)
            {
                floatingContainer = FloatingContainer.ShowDialogContent(dialogContent, this.RootView, size, parameters);
            }
            else
            {
                this.DesignTimeAdorner.ShowDialogContent(dialogContent, this.RootView, size, parameters);
            }
            RoutedEventHandler floatingContainerHidden = null;
            floatingContainerHidden = delegate (object d, RoutedEventArgs e) {
                Action<PopupBaseEdit> <>9__2;
                floatingContainer.Hidden -= floatingContainerHidden;
                this.DataControl.Focus();
                Action<PopupBaseEdit> action = <>9__2;
                if (<>9__2 == null)
                {
                    Action<PopupBaseEdit> local1 = <>9__2;
                    action = <>9__2 = edit => edit.StaysPopupOpen = staysPopupOpen;
                }
                popupBaseEdit.Do<PopupBaseEdit>(action);
            };
            if (floatingContainer != null)
            {
                floatingContainer.Hidden += floatingContainerHidden;
            }
            return floatingContainer;
        }

        public void ShowEditor()
        {
            this.ShowEditor(false);
        }

        public void ShowEditor(bool selectAll)
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.ShowEditorIfNotVisible(selectAll);
            }
        }

        public void ShowFilterEditor(ColumnBase defaultColumn)
        {
            if (this.ActualUseLegacyFilterEditor)
            {
                this.ShowFilterEditorInternal(this.DataControl, DataControlBase.FilterCriteriaProperty.GetName(), defaultColumn);
            }
            else
            {
                this.ShowModernFilterEditor(defaultColumn?.FieldName);
            }
        }

        internal void ShowFilterEditor(object commandParameter)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(commandParameter);
            this.ShowFilterEditor(columnByCommandParameter);
        }

        private void ShowFilterEditor(ExecutedRoutedEventArgs e)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(e.Parameter);
            this.ShowFilterEditor(columnByCommandParameter);
        }

        [Obsolete("Use the ShowFilterEditor(ColumnBase defaultColumn) method instead")]
        public void ShowFilterEditor(object filterCriteriaSource, string filterCriteriaPropertyName, ColumnBase defaultColumn)
        {
            this.ShowFilterEditorInternal(filterCriteriaSource, filterCriteriaPropertyName, defaultColumn);
        }

        protected void ShowFilterEditorInternal(object filterCriteriaSource, string filterCriteriaPropertyName, ColumnBase defaultColumn)
        {
            if (!this.IsFilterControlOpened && (PresentationSource.FromVisual(this.RootView) != null))
            {
                FilterControl control1 = new FilterControl();
                control1.ShowBorder = false;
                control1.SupportDomainDataSource = this.DataControl.SupportDomainDataSource;
                control1.AllowedGroupFilters = this.GetActualAllowedGroupFilters();
                FilterControl control = control1;
                control.DefaultColumn = this.DataControl.GetFilterColumnFromGridColumn(defaultColumn, true, true, false);
                control.SourceControl = this.DataControl.FilteredComponent;
                Binding binding3 = new Binding {
                    Source = filterCriteriaSource,
                    Path = new PropertyPath(filterCriteriaPropertyName, new object[0]),
                    Mode = BindingMode.TwoWay
                };
                Binding binding = binding3;
                control.SetBinding(FilterControl.FilterCriteriaProperty, binding);
                binding3 = new Binding {
                    Source = this,
                    Path = new PropertyPath(FilterEditorShowOperandTypeIconProperty.GetName(), new object[0])
                };
                Binding binding2 = binding3;
                control.SetBinding(FilterControl.ShowOperandTypeIconProperty, binding2);
                FilterEditorEventArgs args1 = new FilterEditorEventArgs(this, control);
                args1.RoutedEvent = FilterEditorCreatedEvent;
                RoutedEventArgs e = args1;
                this.RaiseEventInOriginationView(e);
                if (!e.Handled)
                {
                    string localizedString = this.GetLocalizedString(GridControlStringId.FilterEditorTitle);
                    control.FlowDirection = base.FlowDirection;
                    FloatingContainerParameters parameters = new FloatingContainerParameters();
                    parameters.Title = localizedString;
                    parameters.AllowSizing = true;
                    parameters.ShowApplyButton = !this.IsDesignTime;
                    parameters.CloseOnEscape = false;
                    parameters.Icon = ImageHelper.CreateImageFromCoreEmbeddedResource("Editors.Images.FilterControl.filter.png");
                    this.FilterControlContainer = this.ShowDialogContent(control, new Size(500.0, 350.0), parameters);
                    if (this.FilterControlContainer != null)
                    {
                        this.FilterControlContainer.Hidden += new RoutedEventHandler(this.filterControlContainer_Hidden);
                    }
                }
            }
        }

        public void ShowFixedTotalSummaryEditor()
        {
            new GridTotalSummaryPanelHelper(this).ShowSummaryEditor();
        }

        protected internal virtual bool ShowGroupFooterSummaryItemVerticalSeparator(ColumnBase column) => 
            false;

        private void ShowModernFilterEditor(string propertyName)
        {
            FilteringUIContext filteringContext;
            DataControlBase dataControl = this.DataControl;
            if (dataControl != null)
            {
                filteringContext = dataControl.FilteringContext;
            }
            else
            {
                DataControlBase local1 = dataControl;
                filteringContext = null;
            }
            FilterEdtiorDialogShowHelper.Show(this, filteringContext, propertyName, this.GetLocalizedString(GridControlStringId.FilterEditorTitle), this.FilterEditorDialogServiceTemplate, this.FilterEditorTemplate);
        }

        public void ShowPrintPreview(FrameworkElement owner)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreview(owner, this);
        }

        public void ShowPrintPreview(FrameworkElement owner, string documentName)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreview(owner, this, documentName);
        }

        public void ShowPrintPreview(FrameworkElement owner, string documentName, string title)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreview(owner, this, documentName, title);
        }

        public void ShowPrintPreviewDialog(Window owner)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreviewDialog(owner, this);
        }

        public void ShowPrintPreviewDialog(Window owner, string documentName)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreviewDialog(owner, this, documentName);
        }

        public void ShowPrintPreviewDialog(Window owner, string documentName, string title)
        {
            this.CheckPrinting();
            PrintHelper.ShowPrintPreviewDialog(owner, this, documentName, title);
        }

        public void ShowRibbonPrintPreview(FrameworkElement owner)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreview(owner, this);
        }

        public void ShowRibbonPrintPreview(FrameworkElement owner, string documentName)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreview(owner, this, documentName);
        }

        public void ShowRibbonPrintPreview(FrameworkElement owner, string documentName, string title)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreview(owner, this, documentName, title);
        }

        public void ShowRibbonPrintPreviewDialog(Window owner)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreviewDialog(owner, this);
        }

        public void ShowRibbonPrintPreviewDialog(Window owner, string documentName)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreviewDialog(owner, this, documentName);
        }

        public void ShowRibbonPrintPreviewDialog(Window owner, string documentName, string title)
        {
            this.CheckPrinting();
            PrintHelper.ShowRibbonPrintPreviewDialog(owner, this, documentName, title);
        }

        public void ShowSearchPanel(bool moveFocusInSearchPanel)
        {
            if ((this.ShowSearchPanelMode != DevExpress.Xpf.Grid.ShowSearchPanelMode.Never) && (!this.IsVirtualSource || (this.ShowSearchPanelMode != DevExpress.Xpf.Grid.ShowSearchPanelMode.Default)))
            {
                if ((this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.Default) || (this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.HotKey))
                {
                    this.ActualShowSearchPanel ??= true;
                }
                if (moveFocusInSearchPanel)
                {
                    this.FocusSearchPanel();
                }
            }
        }

        public void ShowTotalSummaryEditor(ColumnBase column)
        {
            new GridTotalSummaryHelper(this, () => column).ShowSummaryEditor();
        }

        internal void ShowTotalSummaryEditor(object parameter)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(parameter);
            if (columnByCommandParameter != null)
            {
                this.ShowTotalSummaryEditor(columnByCommandParameter);
            }
        }

        public virtual void ShowUnboundExpressionEditor(ColumnBase column)
        {
            UnboundExpressionEditorEventArgs e = new UnboundExpressionEditorEventArgs(UnboundExpressionEditorCreatedEvent, column);
            this.RaiseEventInOriginationView(e);
            if (!e.Handled)
            {
                ExpressionEditorHelper.ShowExpressionEditor(e, delegate (string expression) {
                    if (expression != null)
                    {
                        column.UnboundExpression = expression;
                    }
                }, this);
            }
        }

        internal void ShowUnboundExpressionEditor(object commandParameter)
        {
            ColumnBase columnByCommandParameter = this.GetColumnByCommandParameter(commandParameter);
            if (this.CanShowUnboundExpressionEditor(columnByCommandParameter))
            {
                this.ShowUnboundExpressionEditor(columnByCommandParameter);
            }
        }

        private void ShowUnboundExpressionEditor(ExecutedRoutedEventArgs e)
        {
            this.ShowUnboundExpressionEditor(e.Parameter);
        }

        protected internal virtual void StopSelection()
        {
            this.ViewBehavior.StopSelection();
        }

        protected internal abstract bool SupportValidateCell();
        protected internal abstract bool SupportValidateRow();
        internal virtual void ThrowNotSupportedInDetailException()
        {
        }

        internal virtual void ThrowNotSupportedInMasterDetailException()
        {
            throw new NotSupportedInMasterDetailException("CardView and TreeListView are not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx");
        }

        protected internal virtual void TouchHorizontalOffset(double delta)
        {
            this.ViewBehavior.ChangeHorizontalOffsetBy(delta);
        }

        protected virtual void UngroupColumn(string fieldName)
        {
        }

        internal void UnselectRowCore(int rowHandle)
        {
            this.SelectionStrategy.UnselectRow(rowHandle);
        }

        private void UpdatCellToolTipTemplate()
        {
            foreach (ColumnBase base2 in this.ColumnsCore)
            {
                base2.UpdateActualCellToolTipTemplate();
            }
        }

        protected internal virtual void UpdateActualAllowCellMergeCore()
        {
        }

        internal void UpdateActualColumnChooserTemplate()
        {
            this.ActualColumnChooserTemplate = this.ViewBehavior.GetActualColumnChooserTemplate();
        }

        internal virtual void UpdateActualFadeSelectionOnLostFocus(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataViewBase base2 = (DataViewBase) d;
            base2.ActualFadeSelectionOnLostFocus = base2.FadeSelectionOnLostFocus;
        }

        internal void UpdateActualTemplateSelector(DependencyPropertyKey propertyKey, DataTemplateSelector selector, DataTemplate template, Func<DataTemplateSelector, DataTemplate, ActualTemplateSelectorWrapper> createWrapper = null)
        {
            DataControlOriginationElementHelper.UpdateActualTemplateSelector(this, this.OriginationView, propertyKey, selector, template, createWrapper);
        }

        private void UpdateActualTotalSummaryItemTemplateSelector()
        {
            this.UpdateActualTemplateSelector(ActualTotalSummaryItemTemplateSelectorPropertyKey, this.TotalSummaryItemTemplateSelector, this.TotalSummaryItemTemplate, null);
        }

        protected virtual void UpdateAfterIncrementalSearch()
        {
        }

        internal void UpdateAllDependentViews(Action<DataViewBase> updateMethod)
        {
            this.UpdateAllDependentViewsCore(updateMethod, delegate (Action<DataViewBase> method) {
                Func<DataControlBase, DataViewBase> getTarget = <>c.<>9__1764_1;
                if (<>c.<>9__1764_1 == null)
                {
                    Func<DataControlBase, DataViewBase> local1 = <>c.<>9__1764_1;
                    getTarget = <>c.<>9__1764_1 = dataControl => dataControl.DataView;
                }
                DataControlOriginationElementHelper.EnumerateDependentElementsIncludingSource<DataViewBase>(this.DataControl, getTarget, method, null);
            });
        }

        private void UpdateAllDependentViewsCore(Action<DataViewBase> updateMethod, Action<Action<DataViewBase>> iterateMethod)
        {
            if (this.DataControl == null)
            {
                updateMethod(this);
            }
            else
            {
                iterateMethod(updateMethod);
            }
        }

        internal void UpdateAllOriginationViews(Action<DataViewBase> updateMethod)
        {
            this.UpdateAllDependentViewsCore(updateMethod, method => this.DataControl.UpdateAllOriginationDataControls(delegate (DataControlBase dataControl) {
                if (dataControl.DataView != null)
                {
                    method(dataControl.DataView);
                }
            }));
        }

        private void UpdateAllowMouseMoveSelection(MouseButtonEventArgs e, bool canStartSelection)
        {
            if (this.AllowDragDropOnRootView)
            {
                Func<ITableViewHitInfo, bool> evaluator = <>c.<>9__1453_1;
                if (<>c.<>9__1453_1 == null)
                {
                    Func<ITableViewHitInfo, bool> local1 = <>c.<>9__1453_1;
                    evaluator = <>c.<>9__1453_1 = x => x.IsRowIndicator;
                }
                this.AllowMouseMoveSelection = (e.OriginalSource as DependencyObject).With<DependencyObject, ITableViewHitInfo>(d => (this.RootView.CalcHitInfoCore(d) as ITableViewHitInfo)).Return<ITableViewHitInfo, bool>(evaluator, <>c.<>9__1453_2 ??= () => false);
            }
            else
            {
                Func<DataControlBase, Func<MouseEventArgs, bool>> evaluator = <>c.<>9__1453_3;
                if (<>c.<>9__1453_3 == null)
                {
                    Func<DataControlBase, Func<MouseEventArgs, bool>> local3 = <>c.<>9__1453_3;
                    evaluator = <>c.<>9__1453_3 = dc => DragManager.GetAllowMouseMoveSelectionFunc(dc);
                }
                this.AllowMouseMoveSelection = this.DataControl.With<DataControlBase, Func<MouseEventArgs, bool>>(evaluator).Return<Func<MouseEventArgs, bool>, bool>(selectfunc => selectfunc(e), () => canStartSelection);
            }
        }

        internal void UpdateAllRowData(UpdateRowDataDelegate updateMethod)
        {
            if (this.DataControl != null)
            {
                this.DataControl.UpdateAllDetailDataControls(dataControl => dataControl.viewCore.UpdateRowData(updateMethod, false, false), null);
            }
        }

        protected internal virtual void UpdateAlternateRowBackground()
        {
        }

        internal void UpdateBorderForFocusedUIElement()
        {
            if (this.DataControl != null)
            {
                this.SelectionStrategy.UpdateBorderForFocusedElement();
            }
        }

        protected internal virtual void UpdateCellData()
        {
            this.ViewBehavior.UpdateCellData();
            this.IsUpdateCellDataEnqueued = false;
        }

        protected internal void UpdateCellData(ColumnsRowDataBase rowData)
        {
            this.ViewBehavior.UpdateCellData(rowData);
        }

        internal void UpdateCellDataErrors()
        {
            if (this.DataControl != null)
            {
                UpdateRowDataDelegate updateMethod = <>c.<>9__1399_0;
                if (<>c.<>9__1399_0 == null)
                {
                    UpdateRowDataDelegate local1 = <>c.<>9__1399_0;
                    updateMethod = <>c.<>9__1399_0 = rowData => rowData.UpdateDataErrors(true);
                }
                this.UpdateRowData(updateMethod, false, true);
            }
        }

        internal void UpdateCellDataLanguage()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1370_0;
            if (<>c.<>9__1370_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1370_0;
                updateMethod = <>c.<>9__1370_0 = rowData => rowData.UpdateCellDataLanguage();
            }
            this.UpdateRowData(updateMethod, true, true);
        }

        protected internal virtual void UpdateCellDataValues(ColumnBase column)
        {
            this.UpdateRowData(rowData => rowData.GetCellDataByColumn(column).UpdateValue(false), true, true);
        }

        protected internal virtual void UpdateCellMergingPanels(bool force = false)
        {
        }

        internal void UpdateCheckBoxSelectorState()
        {
            bool isSelectedEnabled = true;
            bool isUnselectEnabled = true;
            this.AllItemsSelected = this.SelectionStrategy.GetAllItemsSelected(out isSelectedEnabled, out isUnselectEnabled);
            bool? allItemsSelected = this.AllItemsSelected;
            bool flag3 = true;
            if ((allItemsSelected.GetValueOrDefault() == flag3) ? (allItemsSelected != null) : false)
            {
                this.EnabledCheckBoxSelector = isUnselectEnabled;
            }
            else
            {
                allItemsSelected = this.AllItemsSelected;
                flag3 = false;
                if ((allItemsSelected.GetValueOrDefault() == flag3) ? (allItemsSelected != null) : false)
                {
                    this.EnabledCheckBoxSelector = isSelectedEnabled;
                }
            }
        }

        internal void UpdateColumnAllowSearchPanel(BaseColumn columnBase)
        {
            if ((this.SearchPanelColumnProvider != null) && (this.SearchControl != null))
            {
                this.SearchControl.UpdateColumnProvider();
            }
        }

        internal void UpdateColumnChooserCaption()
        {
            if (this.IsActualColumnChooserCreated)
            {
                DefaultColumnChooser actualColumnChooser = this.ActualColumnChooser as DefaultColumnChooser;
                if (actualColumnChooser != null)
                {
                    actualColumnChooser.UpdateCaption();
                }
            }
        }

        private void UpdateColumnHeaderImageStyle()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1403_0;
            if (<>c.<>9__1403_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1403_0;
                updateColumnDelegate = <>c.<>9__1403_0 = column => column.UpdateActualHeaderImageStyle();
            }
            this.UpdateColumns(updateColumnDelegate);
            this.ViewBehavior.UpdateBandsLayoutProperties();
        }

        private void UpdateColumnHeadersToolTipTemplate()
        {
            foreach (ColumnBase base2 in this.ColumnsCore)
            {
                base2.UpdateActualHeaderToolTipTemplate();
            }
        }

        internal virtual void UpdateColumns(Action<ColumnBase> updateColumnDelegate)
        {
            foreach (ColumnBase base2 in this.ColumnsCore)
            {
                updateColumnDelegate(base2);
            }
        }

        internal void UpdateColumnsActualCellTemplateSelector()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1729_0;
            if (<>c.<>9__1729_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1729_0;
                updateColumnDelegate = <>c.<>9__1729_0 = x => x.UpdateActualCellTemplateSelector();
            }
            this.UpdateColumns(updateColumnDelegate);
        }

        private void UpdateColumnsActualHeaderCustomizationAreaTemplateSelector()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1385_0;
            if (<>c.<>9__1385_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1385_0;
                updateColumnDelegate = <>c.<>9__1385_0 = column => column.UpdateActualHeaderCustomizationAreaTemplateSelector();
            }
            this.UpdateColumns(updateColumnDelegate);
        }

        private void UpdateColumnsActualHeaderTemplateSelector()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1384_0;
            if (<>c.<>9__1384_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1384_0;
                updateColumnDelegate = <>c.<>9__1384_0 = column => column.UpdateActualHeaderTemplateSelector();
            }
            this.UpdateColumns(updateColumnDelegate);
        }

        internal void UpdateColumnsAppearance()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1402_0;
            if (<>c.<>9__1402_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1402_0;
                updateColumnDelegate = <>c.<>9__1402_0 = column => column.UpdateAppearance();
            }
            this.UpdateColumns(updateColumnDelegate);
        }

        private void UpdateColumnsCore(Action updateAction)
        {
            if ((this.DataControl != null) && (ReferenceEquals(this.DataControl.DataView, this) && (!this.DataControl.IsLoading && (!this.DataControl.IsDeserializing && (!this.IsLockUpdateColumnsLayout && !this.ColumnsCore.IsLockUpdate)))))
            {
                updateAction();
                this.RebuildColumnChooserColumns();
                this.DataControl.UpdateAllDetailViewIndents();
                this.ViewBehavior.VisibleColumnsChanged();
                this.ShowCheckBoxInHeaderColumnsChanged();
                this.UpdateWatchErrors();
            }
        }

        internal void UpdateColumnsPositions()
        {
            this.UpdateColumnsCore(new Action(this.ViewBehavior.UpdateColumnsPositions));
        }

        protected internal virtual void UpdateColumnsTotalSummary()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1401_0;
            if (<>c.<>9__1401_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1401_0;
                updateColumnDelegate = <>c.<>9__1401_0 = column => column.UpdateTotalSummaries();
            }
            this.UpdateColumns(updateColumnDelegate);
            this.UpdateTotalSummaries();
        }

        protected internal virtual void UpdateColumnSummaries(NotifyCollectionChangedAction action)
        {
        }

        internal void UpdateColumnsViewInfo(bool updateDataPropertiesOnly = false)
        {
            if ((this.DataControl != null) && (ReferenceEquals(this.DataControl.DataView, this) && (!this.DataControl.IsLoading && (!this.DataControl.IsDeserializing && (!this.IsLockUpdateColumnsLayout && !this.ColumnsCore.IsLockUpdate)))))
            {
                if ((this.DataControl != null) && ((this.DataControl.DataProviderBase != null) && this.DataControl.DataProviderBase.IsICollectionView))
                {
                    ICollectionViewHelper dataSource = this.DataProviderBase.DataController.DataSource as ICollectionViewHelper;
                    if (dataSource != null)
                    {
                        dataSource.AllowSyncSortingAndGrouping = this.AllowSorting;
                    }
                }
                this.ViewBehavior.UpdateColumnsViewInfo(updateDataPropertiesOnly);
                this.UpdateTotalSummaries();
                this.UpdateShowEditFilterButtonCore();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        protected internal void UpdateContentLayout()
        {
            if (this.DataControl != null)
            {
                ((INotificationManager) this.DataControl).AcceptNotification(this, NotificationType.Layout);
            }
        }

        internal void UpdateDataObjects(bool updateColumnsViewInfo = true, bool updateDataObjects = true)
        {
            if (updateColumnsViewInfo)
            {
                this.UpdateColumnsViewInfo(true);
            }
            this.UpdateFocusedRowData();
            if (updateDataObjects)
            {
                this.ViewBehavior.UpdateAdditionalRowsData();
            }
        }

        internal void UpdateEditorButtonVisibilities()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1365_0;
            if (<>c.<>9__1365_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1365_0;
                updateMethod = <>c.<>9__1365_0 = rowData => rowData.UpdateEditorButtonVisibilities();
            }
            this.UpdateRowData(updateMethod, true, true);
        }

        private void UpdateEditorButtonVisibilities(int rowHandle)
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1364_0;
            if (<>c.<>9__1364_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1364_0;
                updateMethod = <>c.<>9__1364_0 = rowData => rowData.UpdateEditorButtonVisibilities();
            }
            this.UpdateRowDataByRowHandle(rowHandle, updateMethod);
        }

        internal void UpdateEditorHighlightingText()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1367_0;
            if (<>c.<>9__1367_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1367_0;
                updateMethod = <>c.<>9__1367_0 = rowData => rowData.UpdateEditorHighlightingText();
            }
            this.UpdateRowData(updateMethod, false, false);
        }

        internal void UpdateEditorHighlightingText(TextHighlightingProperties textHighlightingProperties, string[] columns)
        {
            this.UpdateRowData(rowData => rowData.UpdateEditorHighlightingText(textHighlightingProperties, columns), false, false);
        }

        protected internal virtual void UpdateFilterGrid()
        {
            GridSearchControlBase searchControl = this.SearchControl as GridSearchControlBase;
            if (searchControl == null)
            {
                GridSearchControlBase local1 = searchControl;
            }
            else
            {
                searchControl.InitSearchInfo();
            }
            this.UpdateSearchResult(true);
            this.UpdateSelectionSummary();
            if (!this.SearchPanelAllowFilter && ((this.DataControl != null) && (!this.DataControl.RestoreLayoutIsLock && (!this.FilterOnDeserializationLock.IsLocked && (!this.DataControl.IsServerMode && ((this.SearchPanelFindMode == FindMode.Always) || !this.DataControl.FilterCriteriaChangedLocker.IsLocked))))))
            {
                this.SearchResultNextCore(true);
            }
        }

        protected internal virtual void UpdateFilterOnDeserializationLock()
        {
            this.FilterOnDeserializationLock.Lock();
        }

        protected internal virtual void UpdateFilterOnDeserializationUnlock(bool forceUpdate = true)
        {
            this.FilterOnDeserializationLock.Unlock();
        }

        internal void UpdateFilterPanel()
        {
            if ((this.DataControl != null) && !this.DataControl.IsLoading)
            {
                this.ActualShowFilterPanel = this.GetActualShowFilterPanel();
                this.FilterPanelText = this.GetFilterOperatorCustomText(this.DataControl.FilterCriteria);
                this.DataControl.UpdateActiveFilterInfo();
            }
        }

        internal void UpdateFocusAndInvalidatePanels()
        {
            this.UpdateCellMergingPanels(true);
            this.ForceUpdateRowsState();
        }

        internal void UpdateFocusedRowData()
        {
            if (!this.FocusedRowHandleChangedLocker.IsLocked)
            {
                if ((this.FocusedRowData == null) || this.ViewBehavior.IsAdditionalRowData(this.FocusedRowData))
                {
                    this.FocusedRowData = this.CreateFocusedRowData();
                }
                RowHandle rowHandle = this.FocusedRowData.RowHandle;
                object row = this.FocusedRowData.Row;
                this.FocusedRowData.AssignFrom(this.FocusedRowHandle);
                if (this.dataControl != null)
                {
                    this.dataControl.UpdateCurrentItem();
                    if (!Equals(rowHandle, this.FocusedRowData.RowHandle) || !Equals(row, this.FocusedRowData.Row))
                    {
                        this.DataControl.UpdateFocusedRowDataposponedAction.PerformIfNotInProgress(new Action(this.RaiseFocusedRowHandleChanged));
                    }
                    this.SelectionStrategy.OnFocusedRowDataChanged();
                }
            }
        }

        internal void UpdateFocusedRowHandleCore()
        {
            if (!Equals(this.DataProviderBase.GetRowValue(this.FocusedRowHandle), this.DataControl.CurrentItem) || this.DataProviderBase.IsGroupRowHandle(this.FocusedRowHandle))
            {
                int rowHandle = this.DataProviderBase.FindRowByRowValue(this.DataControl.CurrentItem);
                if ((rowHandle != -2147483648) || (!this.DataControl.HasCurrentItemBinding() || this.DataControl.AllowUpdateTwoWayBoundPropertiesOnSynchronization))
                {
                    this.SetFocusedRowHandle(rowHandle);
                }
            }
        }

        private void UpdateFullRowState()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1361_0;
            if (<>c.<>9__1361_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1361_0;
                updateMethod = <>c.<>9__1361_0 = rowData => rowData.UpdateFullState();
            }
            this.UpdateRowData(updateMethod, false, true);
        }

        private void UpdateFullRowState(int rowHandle)
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1360_0;
            if (<>c.<>9__1360_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1360_0;
                updateMethod = <>c.<>9__1360_0 = rowData => rowData.UpdateFullState();
            }
            this.UpdateRowDataByRowHandle(rowHandle, updateMethod);
        }

        protected virtual void UpdateGridControlColumnProvider()
        {
            if (this.DataControl != null)
            {
                GridControlColumnProviderBase base1 = new GridControlColumnProviderBase();
                base1.FilterByColumnsMode = FilterByColumnsMode.Default;
                base1.AllowColumnsHighlighting = false;
                GridControlColumnProviderBase base2 = base1;
                GridControlColumnProviderBase.SetColumnProvider(this.DataControl, base2);
            }
        }

        protected internal virtual void UpdateGroupPanelDragText()
        {
        }

        protected internal virtual void UpdateGroupSummary()
        {
        }

        protected internal void UpdateHighlightSelectionState()
        {
            ITableView view = this as ITableView;
            if ((view != null) && view.HighlightItemOnHover)
            {
                UpdateRowDataDelegate updateMethod = <>c.<>9__2219_0;
                if (<>c.<>9__2219_0 == null)
                {
                    UpdateRowDataDelegate local1 = <>c.<>9__2219_0;
                    updateMethod = <>c.<>9__2219_0 = x => x.UpdateSelectionState();
                }
                this.UpdateAllRowData(updateMethod);
            }
        }

        protected internal void UpdateIsCheckedForHeaderColumns()
        {
            foreach (ColumnBase base2 in this.ShowCheckBoxInHeaderColumns)
            {
                base2.UpdateIsChecked(false);
            }
        }

        private void UpdateIsFocused()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1362_0;
            if (<>c.<>9__1362_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1362_0;
                updateMethod = <>c.<>9__1362_0 = rowData => rowData.UpdateIsFocused();
            }
            this.UpdateRowData(updateMethod, false, true);
        }

        private void UpdateIsFocusedCellIfNeeded(int rowHandle, ColumnBase column = null)
        {
            if (this.ActualAllowCellMerge)
            {
                if (column == null)
                {
                    if (this.DataControl == null)
                    {
                        return;
                    }
                    column = this.DataControl.CurrentColumn;
                }
                int num = this.CalcActualRowHandle(rowHandle, column);
                if (num != rowHandle)
                {
                    RowData rowData = this.GetRowData(num);
                    if (rowData != null)
                    {
                        rowData.UpdateIsFocusedCell();
                    }
                }
            }
        }

        internal void UpdateIsKeyboardFocusWithinView()
        {
            if (this.DataControl == null)
            {
                this.IsKeyboardFocusWithinView = false;
            }
            else
            {
                this.IsKeyboardFocusWithinView = this.IsFocusedView && this.DataControl.GetRootDataControl().IsKeyboardFocusWithin;
            }
        }

        private void UpdateIsSelected()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__1363_0;
            if (<>c.<>9__1363_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__1363_0;
                updateMethod = <>c.<>9__1363_0 = rowData => rowData.UpdateIsSelected();
            }
            this.UpdateRowData(updateMethod, false, true);
        }

        protected internal virtual void UpdateLoadingRow(bool show)
        {
            this.ShowLoadingRow = show;
            UpdateRowDataDelegate updateMethod = <>c.<>9__2144_0;
            if (<>c.<>9__2144_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__2144_0;
                updateMethod = <>c.<>9__2144_0 = rowData => ((LoadingRowData) rowData).UpdateLoadingState();
            }
            this.UpdateRowDataByRowHandle(-2147483646, updateMethod);
        }

        internal virtual void UpdateMasterDetailViewProperties()
        {
            this.ViewBehavior.UpdateActualProperties();
        }

        protected internal virtual void UpdateNewItemRowData()
        {
        }

        protected internal virtual void UpdatePagerControlItemCount()
        {
        }

        protected internal virtual void UpdatePagerControlPageIndex(bool initFromSource = false)
        {
        }

        protected internal virtual void UpdatePagerControlPageSize()
        {
        }

        protected virtual bool UpdateRowButtonsRequestUI() => 
            false;

        protected virtual void UpdateRowCellFocusIfNeeded()
        {
            if ((((((!FloatingContainer.IsModalContainerOpened && this.IsKeyboardFocusWithinView) && !LayoutHelper.IsChildElementEx(this.FocusedRowElement, KeyboardHelper.FocusedElement, false)) && !this.IsColumnFilterOpened) && (!this.IsKeyboardFocusInSearchPanel() && !this.IsFocusInSearchPanel())) && !this.ColumnChooserIsKeyboardFocus()) && !(this.CurrentCellEditor is FilterRowCellEditor))
            {
                Predicate<DependencyObject> predicate = <>c.<>9__1356_0;
                if (<>c.<>9__1356_0 == null)
                {
                    Predicate<DependencyObject> local1 = <>c.<>9__1356_0;
                    predicate = <>c.<>9__1356_0 = e => e is FilterPanelControlBase;
                }
                if (LayoutHelper.FindLayoutOrVisualParentObject(KeyboardHelper.FocusedElement, predicate, false, null) == null)
                {
                    this.SetFocusToRowCell();
                }
            }
        }

        protected internal void UpdateRowCore()
        {
            if (this.CanUpdateRowCore())
            {
                this.UpdateRowButtonsLocker.DoLockedAction(delegate {
                    int updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                    this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI + 1;
                    try
                    {
                        this.AreUpdateRowButtonsShown = false;
                        this.RootView.AreUpdateRowButtonsShown = false;
                        this.CloseEditor();
                        RowData rowData = this.GetRowData(this.FocusedRowHandle);
                        rowData.UpdateRowButtonsWasChanged = false;
                        using (IEnumerator<ColumnBase> enumerator = this.VisibleColumnsCore.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator.MoveNext())
                                {
                                    break;
                                }
                                ColumnBase current = enumerator.Current;
                                EditGridCellData cellDataByColumn = rowData.GetCellDataByColumn(current) as EditGridCellData;
                                if (cellDataByColumn != null)
                                {
                                    if (cellDataByColumn.Editor != null)
                                    {
                                        if (cellDataByColumn.Editor.PostEditor(true))
                                        {
                                            cellDataByColumn.Editor.HideEditor(true);
                                            continue;
                                        }
                                        this.AreUpdateRowButtonsShown = true;
                                        this.RootView.AreUpdateRowButtonsShown = true;
                                        rowData.UpdateRowButtonsWasChanged = true;
                                        this.DataControl.CurrentColumn = current;
                                        this.CurrentCellEditor = cellDataByColumn.Editor;
                                        this.ScrollToCurrentColumnIfNeeded();
                                        this.ShowEditor();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            this.DataControl.SetCellValueCore(this.FocusedRowHandle, cellDataByColumn.Column.FieldName, cellDataByColumn.Value);
                                            continue;
                                        }
                                        catch (Exception exception)
                                        {
                                            this.DataControl.CurrentColumn = current;
                                            this.ScrollToCurrentColumnIfNeeded();
                                            this.ShowEditor();
                                            cellDataByColumn = rowData.GetCellDataByColumn(current) as EditGridCellData;
                                            this.AreUpdateRowButtonsShown = true;
                                            this.RootView.AreUpdateRowButtonsShown = true;
                                            rowData.UpdateRowButtonsWasChanged = true;
                                            ((CellEditor) cellDataByColumn.Editor).SetValidationError(this.CreateCellValidationError(exception.Message, ErrorType.Default, this.FocusedRowHandle, current, exception));
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                        if (!this.CommitEditing())
                        {
                            this.AreUpdateRowButtonsShown = true;
                            this.RootView.AreUpdateRowButtonsShown = true;
                            rowData.UpdateRowButtonsWasChanged = true;
                        }
                    }
                    finally
                    {
                        updateButtonsModeAllowRequestUI = this.UpdateButtonsModeAllowRequestUI;
                        this.UpdateButtonsModeAllowRequestUI = updateButtonsModeAllowRequestUI - 1;
                        this.ViewBehavior.UpdateRowButtonsControl();
                    }
                });
            }
        }

        protected internal virtual void UpdateRowData(UpdateRowDataDelegate updateMethod, bool updateInvisibleRows = true, bool updateFocusedRow = true)
        {
            this.ViewBehavior.UpdateRowData(updateMethod, updateInvisibleRows, updateFocusedRow);
        }

        internal void UpdateRowDataByRowHandle(int rowHandle, UpdateRowDataDelegate updateMethod)
        {
            RowData focusedRowData = this.FocusedRowData;
            if ((focusedRowData != null) && (focusedRowData.RowHandle.Value == rowHandle))
            {
                updateMethod(focusedRowData);
            }
            RowData rowData = this.GetRowData(rowHandle);
            if ((rowData != null) && !ReferenceEquals(rowData, focusedRowData))
            {
                updateMethod(rowData);
            }
        }

        private void UpdateRowDataFocusWithinState()
        {
            if (this.DataControl != null)
            {
                Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__1226_0;
                if (<>c.<>9__1226_0 == null)
                {
                    Action<DataControlBase> local1 = <>c.<>9__1226_0;
                    updateOpenDetailMethod = <>c.<>9__1226_0 = delegate (DataControlBase dataControl) {
                        UpdateRowDataDelegate updateMethod = <>c.<>9__1226_1;
                        if (<>c.<>9__1226_1 == null)
                        {
                            UpdateRowDataDelegate local1 = <>c.<>9__1226_1;
                            updateMethod = <>c.<>9__1226_1 = x => x.UpdateClientFocusWithinState();
                        }
                        dataControl.DataView.ViewBehavior.UpdateViewRowData(updateMethod);
                    };
                }
                this.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
            }
        }

        protected internal virtual void UpdateRowFocusWithinState(RowData rowData)
        {
        }

        protected internal virtual bool UpdateRowsState()
        {
            this.FocusedView.UpdateBorderForFocusedUIElement();
            if (!this.RowsStateDirty)
            {
                return false;
            }
            this.CurrentCell = null;
            this.Navigation.UpdateRowsState();
            this.AdditionalRowNavigation.UpdateRowsState();
            this.RowsStateDirty = false;
            return true;
        }

        internal virtual void UpdateRowValidationError(int rowHandle)
        {
            this.ErrorWatch.CurrentRowChanged(ListChangedType.ItemChanged, rowHandle, new int?(rowHandle));
        }

        protected internal virtual void UpdateScrollBarAnnotations()
        {
        }

        private void UpdateSearchControlMRU(DevExpress.Xpf.Editors.SearchControl oldValue, DevExpress.Xpf.Editors.SearchControl newValue)
        {
            if ((oldValue != null) || (newValue != null))
            {
                if (oldValue != null)
                {
                    this.searchControlMru = oldValue.MRU;
                }
                if ((newValue != null) && (this.searchControlMru != null))
                {
                    newValue.MRU = this.searchControlMru;
                }
            }
        }

        private void UpdateSearchPanelColumnProviderBindings()
        {
            if ((this.DataControl != null) && (this.SearchControl != null))
            {
                if (this.SearchPanelColumnProvider == null)
                {
                    this.UpdateGridControlColumnProvider();
                }
                if ((this.SearchPanelColumnProvider.DataControlBase != null) && !ReferenceEquals(this.SearchPanelColumnProvider.DataControlBase, this.DataControl))
                {
                    this.SearchPanelColumnProvider.DataControlBase = this.DataControl;
                }
                Binding binding4 = new Binding {
                    Source = this.SearchPanelColumnProvider,
                    Path = new PropertyPath(GridControlColumnProviderBase.FilterByColumnsModeProperty.GetName(), new object[0]),
                    Mode = BindingMode.OneWay
                };
                Binding binding = binding4;
                this.SearchControl.SetBinding(DevExpress.Xpf.Editors.SearchControl.FilterByColumnsModeProperty, binding);
                this.SearchControl.ColumnProvider = this.SearchPanelColumnProvider;
                binding4 = new Binding {
                    Source = this,
                    Path = new PropertyPath(SearchPanelHighlightResultsProperty.GetName(), new object[0]),
                    Mode = BindingMode.OneWay
                };
                Binding binding2 = binding4;
                this.SearchPanelColumnProvider.SetBinding(GridControlColumnProviderBase.AllowTextHighlightingProperty, binding2);
                binding4 = new Binding {
                    Source = this,
                    Path = new PropertyPath(SearchPanelAllowFilterProperty.GetName(), new object[0]),
                    Mode = BindingMode.OneWay
                };
                Binding binding3 = binding4;
                this.SearchPanelColumnProvider.SetBinding(GridControlColumnProviderBase.AllowGridExtraFilterProperty, binding3);
                this.ApplySearchColumns();
            }
        }

        private void UpdateSearchPanelText()
        {
            this.UpdateSearchPanelVisibility(false);
            this.UpdateSearchPanelTextCore();
            this.RaisePropertyChanged(SearchStringProperty.Name);
            this.UpdateShowSearchPanelResultInfo();
        }

        private void UpdateSearchPanelTextCore()
        {
            if ((this.SearchControl == null) && ((this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.Never) || (this.IsVirtualSource && (this.ShowSearchPanelMode == DevExpress.Xpf.Grid.ShowSearchPanelMode.Default))))
            {
                GridSearchControlBase base1 = new GridSearchControlBase();
                base1.View = this;
                this.SearchControl = base1;
            }
        }

        private void UpdateSearchPanelVisibility(bool updateSearchControl = true)
        {
            switch (this.ShowSearchPanelMode)
            {
                case DevExpress.Xpf.Grid.ShowSearchPanelMode.Default:
                    this.ActualShowSearchPanel = !this.IsVirtualSource && !string.IsNullOrEmpty(this.SearchString);
                    break;

                case DevExpress.Xpf.Grid.ShowSearchPanelMode.HotKey:
                    this.ActualShowSearchPanel ??= !string.IsNullOrEmpty(this.SearchString);
                    break;

                case DevExpress.Xpf.Grid.ShowSearchPanelMode.Always:
                    this.ActualShowSearchPanel = true;
                    break;

                case DevExpress.Xpf.Grid.ShowSearchPanelMode.Never:
                    this.ActualShowSearchPanel = false;
                    break;

                default:
                    break;
            }
            if (updateSearchControl && !string.IsNullOrEmpty(this.SearchString))
            {
                this.UpdateSearchPanelTextCore();
            }
        }

        protected internal void UpdateSearchResult(bool checkNavigationButtons = true)
        {
            if ((this.DataProviderBase == null) || !this.DataProviderBase.IsUpdateLocked)
            {
                if ((this.DataControl != null) && this.DataControl.IsServerMode)
                {
                    this.searchResult = true;
                }
                else
                {
                    this.searchResult = false;
                    if (MovementSearchResults.CanMovementSearchResult(this))
                    {
                        this.searchResult = false;
                        if (this.SearchPanelAllowFilter)
                        {
                            this.searchResult = this.DataControl.VisibleRowCount > 0;
                        }
                        else if (this.ActualShowSearchPanelResultInfo && (this.SearchControl != null))
                        {
                            this.searchResult = this.SearchControl.ResultCount > 0;
                        }
                        else if (!checkNavigationButtons || this.ShowSearchPanelNavigationButtons)
                        {
                            Func<int, bool> func = this.CreateFilterFitPredicate();
                            if (func != null)
                            {
                                for (int i = 0; i < this.DataControl.VisibleRowCount; i++)
                                {
                                    int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(i);
                                    if (func(rowHandleByVisibleIndexCore))
                                    {
                                        this.searchResult = true;
                                        return;
                                    }
                                }
                                if ((this.DataControl.VisibleRowCount != 0) && ((this.DataProviderBase != null) && (this.ShowSearchPanelNavigationButtons && ((this.DataProviderBase.DataRowCount != this.DataControl.VisibleRowCount) || (this.GroupCount > 0)))))
                                {
                                    for (int j = 0; j < this.DataProviderBase.DataRowCount; j++)
                                    {
                                        if (!this.DataProviderBase.IsFilteredByRowHandle(j) && func(j))
                                        {
                                            this.searchResult = true;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected virtual void UpdateSelectionSummary()
        {
            if ((this.DataControl != null) && (this.DataControl.GroupSummaryCore.FirstOrDefault<DevExpress.Xpf.Grid.SummaryItemBase>(x => (this.GetActualCalculationMode(x) != GridSummaryCalculationMode.AllRows)) != null))
            {
                List<SummaryItem> changedItems = (from x in this.DataControl.DataProviderBase.DataController.GroupSummary
                    where this.GetActualCalculationMode((DevExpress.Xpf.Grid.SummaryItemBase) x.Tag) != GridSummaryCalculationMode.AllRows
                    select x).ToList<SummaryItem>();
                if (changedItems.Count != 0)
                {
                    this.DataControl.DataProviderBase.DataController.UpdateGroupSummary(changedItems);
                    this.UpdateGroupSummary();
                }
            }
            if ((this.DataControl != null) && (this.DataControl.TotalSummaryCore.FirstOrDefault<DevExpress.Xpf.Grid.SummaryItemBase>(x => (this.GetActualCalculationMode(x) != GridSummaryCalculationMode.AllRows)) != null))
            {
                List<SummaryItem> changedItems = (from x in this.DataControl.DataProviderBase.DataController.TotalSummary
                    where this.GetActualCalculationMode((DevExpress.Xpf.Grid.SummaryItemBase) x.Tag) != GridSummaryCalculationMode.AllRows
                    select x).ToList<SummaryItem>();
                if (changedItems.Count != 0)
                {
                    this.DataControl.DataProviderBase.DataController.UpdateTotalSummary(changedItems);
                    this.UpdateColumnsTotalSummary();
                }
            }
        }

        internal void UpdateShowEditFilterButtonCore()
        {
            this.ShowEditFilterButton = this.GetShowEditFilterButton();
        }

        private void UpdateShowSearchPanelNavigationButtons()
        {
            this.ActualShowSearchPanelNavigationButtons = !this.IsVirtualSource && this.ShowSearchPanelNavigationButtons;
        }

        internal void UpdateShowSearchPanelResultInfo()
        {
            this.ActualShowSearchPanelResultInfo = (this.ShowSearchPanelResultInfo && !this.SearchPanelAllowFilter) && !string.IsNullOrEmpty(this.SearchString);
        }

        internal void UpdateShowValidationAttributeError()
        {
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__1398_0;
            if (<>c.<>9__1398_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__1398_0;
                updateColumnDelegate = <>c.<>9__1398_0 = column => column.UpdateActualShowValidationAttributeErrors();
            }
            this.UpdateColumns(updateColumnDelegate);
            this.UpdateCellDataErrors();
        }

        internal void UpdateSummariesIgnoreNullValues()
        {
            this.SetSummariesIgnoreNullValues(this.SummariesIgnoreNullValues);
        }

        protected virtual void UpdateTotalSummaries()
        {
            if (this.dataControl != null)
            {
                FixedTotalSummaryHelper.GenerateTotalSummaries(this.fixedSummariesHelper.FixedSummariesLeftCore, this.ColumnsCore, new Func<DevExpress.Xpf.Grid.SummaryItemBase, object>(this.dataControl.GetTotalSummaryValue), this.FixedSummariesLeft);
                FixedTotalSummaryHelper.GenerateTotalSummaries(this.fixedSummariesHelper.FixedSummariesRightCore, this.ColumnsCore, new Func<DevExpress.Xpf.Grid.SummaryItemBase, object>(this.dataControl.GetTotalSummaryValue), this.FixedSummariesRight);
            }
        }

        protected internal virtual void UpdateUseLightweightTemplates()
        {
        }

        protected internal virtual void UpdateVisibleGroupPanel()
        {
        }

        internal void UpdateWatchErrors()
        {
            if ((this.DataProviderBase == null) || !this.DataProviderBase.IsUpdateLocked)
            {
                this.ErrorWatch.UpdateWatchErrors();
            }
        }

        public void ValidateEditor()
        {
            if (this.CurrentCellEditor != null)
            {
                this.CurrentCellEditor.ValidateEditor(true);
            }
        }

        internal void ValidateSelectionStrategy()
        {
            if ((this.selectionStrategy != null) && (this.selectionStrategy.GetType() != this.CreateSelectionStrategy().GetType()))
            {
                this.ResetSelectionStrategy();
            }
        }

        private void ValidationErrorPropertyChanged(BaseValidationError error)
        {
            this.HasValidationError = error != null;
        }

        internal int VisibleComparison(BaseColumn x, BaseColumn y)
        {
            int? nullable = this.ViewBehavior.VisibleComparisonCore(x, y);
            if (nullable != null)
            {
                return nullable.Value;
            }
            if (BaseColumn.GetVisibleIndex(x) == BaseColumn.GetVisibleIndex(y))
            {
                return Comparer<int>.Default.Compare(x.index, y.index);
            }
            nullable = this.CompareGroupedColumns(x, y);
            return ((nullable == null) ? ((BaseColumn.GetVisibleIndex(x) >= 0) ? ((BaseColumn.GetVisibleIndex(y) >= 0) ? Comparer<int>.Default.Compare(BaseColumn.GetVisibleIndex(x), BaseColumn.GetVisibleIndex(y)) : -1) : 1) : nullable.Value);
        }

        protected internal virtual void WidthChanged(double previousWidth, double newWidth, bool forceUpdate)
        {
        }

        internal abstract int GroupCount { get; }

        internal virtual bool PrintAllGroupsCore =>
            true;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public Thickness BorderThickness
        {
            get => 
                base.BorderThickness;
            set => 
                base.BorderThickness = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public Brush Background
        {
            get => 
                base.Background;
            set => 
                base.Background = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public Brush BorderBrush
        {
            get => 
                base.BorderBrush;
            set => 
                base.BorderBrush = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public Brush Foreground
        {
            get => 
                base.Foreground;
            set => 
                base.Foreground = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public Thickness Padding
        {
            get => 
                base.Padding;
            set => 
                base.Padding = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public HorizontalAlignment HorizontalContentAlignment
        {
            get => 
                base.HorizontalContentAlignment;
            set => 
                base.HorizontalContentAlignment = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public VerticalAlignment VerticalContentAlignment
        {
            get => 
                base.VerticalContentAlignment;
            set => 
                base.VerticalContentAlignment = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool UseLegacyColumnVisibleIndexes { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool? AllItemsSelected
        {
            get => 
                (bool?) base.GetValue(AllItemsSelectedProperty);
            private set => 
                base.SetValue(AllItemsSelectedPropertyKey, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool EnabledCheckBoxSelector
        {
            get => 
                (bool) base.GetValue(EnabledCheckBoxSelectorProperty);
            private set => 
                base.SetValue(EnabledCheckBoxSelectorPropertyKey, value);
        }

        [Description("Specifies whether to enable horizontal scrolling via the mouse wheel and vertical scrolling via Microsoft Touch Mouse devices. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool UseExtendedMouseScrolling
        {
            get => 
                (bool) base.GetValue(UseExtendedMouseScrollingProperty);
            set => 
                base.SetValue(UseExtendedMouseScrollingProperty, value);
        }

        [Description("Gets or sets whether selected rows are faded when the grid control loses focus. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool FadeSelectionOnLostFocus
        {
            get => 
                (bool) base.GetValue(FadeSelectionOnLostFocusProperty);
            set => 
                base.SetValue(FadeSelectionOnLostFocusProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Force)]
        public bool ActualFadeSelectionOnLostFocus
        {
            get => 
                (bool) base.GetValue(ActualFadeSelectionOnLostFocusProperty);
            protected set => 
                base.SetValue(ActualFadeSelectionOnLostFocusPropertyKey, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public SearchPanelPosition ActualSearchPanelPosition
        {
            get => 
                (SearchPanelPosition) base.GetValue(ActualSearchPanelPositionProperty);
            protected set => 
                base.SetValue(ActualSearchPanelPositionPropertyKey, value);
        }

        [Description("Gets whether the buttons which allow you to update rows are shown."), Category("Editing")]
        public bool AreUpdateRowButtonsShown
        {
            get => 
                (bool) base.GetValue(AreUpdateRowButtonsShownProperty);
            protected internal set => 
                base.SetValue(AreUpdateRowButtonsShownPropertyKey, value);
        }

        [Description("Gets or sets a value that specifies what animation is played while data rows are being asynchronously retrieved by the data source. This is a dependency property."), Category("Appearance ")]
        public DevExpress.Xpf.Grid.RowAnimationKind RowAnimationKind
        {
            get => 
                (DevExpress.Xpf.Grid.RowAnimationKind) base.GetValue(RowAnimationKindProperty);
            set => 
                base.SetValue(RowAnimationKindProperty, value);
        }

        [Description("Gets or sets a value that specifies how async data loading operations are indicated within the grid. This is a dependency property."), Category("Appearance "), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.WaitIndicatorType WaitIndicatorType
        {
            get => 
                (DevExpress.Xpf.Grid.WaitIndicatorType) base.GetValue(WaitIndicatorTypeProperty);
            set => 
                base.SetValue(WaitIndicatorTypeProperty, value);
        }

        [Description("Gets whether the wait indicator is displayed within the grid. This is a dependency property."), Category("Appearance ")]
        public bool IsWaitIndicatorVisible
        {
            get => 
                (bool) base.GetValue(IsWaitIndicatorVisibleProperty);
            internal set => 
                base.SetValue(IsWaitIndicatorVisiblePropertyKey, value);
        }

        [Description("Gets or sets the wait indicator's style. This is a dependency property."), Category("Appearance ")]
        public Style WaitIndicatorStyle
        {
            get => 
                (Style) base.GetValue(WaitIndicatorStyleProperty);
            set => 
                base.SetValue(WaitIndicatorStyleProperty, value);
        }

        [Description("Gets or sets a style of the marquee selection rectangle. This is a dependency property."), Category("Appearance ")]
        public Style SelectionRectangleStyle
        {
            get => 
                (Style) base.GetValue(SelectionRectangleStyleProperty);
            set => 
                base.SetValue(SelectionRectangleStyleProperty, value);
        }

        [Description("Specifies whether to enable the selection rectangle feature. This is a dependency property."), Category("Appearance ")]
        public bool ShowSelectionRectangle
        {
            get => 
                (bool) base.GetValue(ShowSelectionRectangleProperty);
            set => 
                base.SetValue(ShowSelectionRectangleProperty, value);
        }

        [Description("Gets or sets the collection of resource strings that can be localized at runtime. This is a dependency property."), Category("Appearance ")]
        public GridRuntimeStringCollection RuntimeLocalizationStrings
        {
            get => 
                (GridRuntimeStringCollection) base.GetValue(RuntimeLocalizationStringsProperty);
            set => 
                base.SetValue(RuntimeLocalizationStringsProperty, value);
        }

        [Browsable(false)]
        public DevExpress.Xpf.Grid.LocalizationDescriptor LocalizationDescriptor
        {
            get => 
                (DevExpress.Xpf.Grid.LocalizationDescriptor) base.GetValue(LocalizationDescriptorProperty);
            internal set => 
                base.SetValue(LocalizationDescriptorPropertyKey, value);
        }

        [Description("Gets or sets whether an end user can filter data by column. This is a dependency property."), Category("Options Filter"), XtraSerializableProperty]
        public bool AllowColumnFiltering
        {
            get => 
                (bool) base.GetValue(AllowColumnFilteringProperty);
            set => 
                base.SetValue(AllowColumnFilteringProperty, value);
        }

        [Description("Gets or sets group filters that the GridControl supports."), Category("Options Filter")]
        public DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters? AllowedGroupFilters
        {
            get => 
                (DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters?) base.GetValue(AllowedGroupFiltersProperty);
            set => 
                base.SetValue(AllowedGroupFiltersProperty, value);
        }

        [Description("Gets or sets whether end-users can invoke the Filter Editor to build complex filter criteria. This is a dependency property."), Category("Options Filter")]
        public DefaultBoolean AllowFilterEditor
        {
            get => 
                (DefaultBoolean) base.GetValue(AllowFilterEditorProperty);
            set => 
                base.SetValue(AllowFilterEditorProperty, value);
        }

        [Browsable(false)]
        public bool ShowEditFilterButton
        {
            get => 
                (bool) base.GetValue(ShowEditFilterButtonProperty);
            protected set => 
                base.SetValue(ShowEditFilterButtonPropertyKey, value);
        }

        [Description("Gets or sets whether an end-user can sort data. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool AllowSorting
        {
            get => 
                (bool) base.GetValue(AllowSortingProperty);
            set => 
                base.SetValue(AllowSortingProperty, value);
        }

        [Obsolete("Instead use the AllowColumnMoving property."), XtraSerializableProperty, EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Description("Gets or sets whether an end-user is allowed to move columns by dragging their headers. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Options Behavior")]
        public bool AllowMoving
        {
            get => 
                (bool) base.GetValue(AllowMovingProperty);
            set => 
                base.SetValue(AllowMovingProperty, value);
        }

        [Description("Gets or sets whether an end-user is allowed to move columns by dragging their headers. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool AllowColumnMoving
        {
            get => 
                (bool) base.GetValue(AllowColumnMovingProperty);
            set => 
                base.SetValue(AllowColumnMovingProperty, value);
        }

        [Description("Gets or sets whether end-users are allowed to change cell values. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool AllowEditing
        {
            get => 
                (bool) base.GetValue(AllowEditingProperty);
            set => 
                base.SetValue(AllowEditingProperty, value);
        }

        internal int FocusedRowHandleCore { get; private set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public int FocusedRowHandle
        {
            get => 
                (int) DependencyObjectHelper.GetCoerceValue(this, FocusedRowHandleProperty);
            set => 
                base.SetValue(FocusedRowHandleProperty, value);
        }

        [XtraSerializableProperty, Category("Options Behavior")]
        public bool AllowScrollToFocusedRow
        {
            get => 
                (bool) base.GetValue(AllowScrollToFocusedRowProperty);
            set => 
                base.SetValue(AllowScrollToFocusedRowProperty, value);
        }

        [Description("Gets or sets the offset by which the View is scrolled when clicking scrollbar buttons. This is a dependency property."), Category("Options Behavior"), CloneDetailMode(CloneDetailMode.Skip)]
        public int ScrollStep
        {
            get => 
                (int) base.GetValue(ScrollStepProperty);
            set => 
                base.SetValue(ScrollStepProperty, value);
        }

        [Description("Gets whether the view has a validation error. This is a dependency property.")]
        public bool HasValidationError
        {
            get => 
                (bool) base.GetValue(HasValidationErrorProperty);
            internal set => 
                base.SetValue(HasValidationErrorPropertyKey, value);
        }

        [Description("Provides access to the object, which contains information about the validation error. This is a dependency property.")]
        public BaseValidationError ValidationError
        {
            get => 
                (BaseValidationError) base.GetValue(ValidationErrorProperty);
            internal set => 
                base.SetValue(ValidationErrorPropertyKey, value);
        }

        [Description("Gets or sets whether a cell's value that has failed validation specified by the Data Annotations attribute(s), can be posted to a data source. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool AllowCommitOnValidationAttributeError
        {
            get => 
                (bool) base.GetValue(AllowCommitOnValidationAttributeErrorProperty);
            set => 
                base.SetValue(AllowCommitOnValidationAttributeErrorProperty, value);
        }

        [Description("Gets or sets a value that specifies how many rows are displayed onscreen when a View is scrolled to the bottom. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public DevExpress.Xpf.Grid.ScrollingMode ScrollingMode
        {
            get => 
                (DevExpress.Xpf.Grid.ScrollingMode) base.GetValue(ScrollingModeProperty);
            set => 
                base.SetValue(ScrollingModeProperty, value);
        }

        [Description("Gets or sets whether deferred scrolling is enabled. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool IsDeferredScrolling
        {
            get => 
                (bool) base.GetValue(IsDeferredScrollingProperty);
            set => 
                base.SetValue(IsDeferredScrollingProperty, value);
        }

        [Description("Gets or sets which cells should display editor buttons. This is a dependency property."), XtraSerializableProperty, Category("Options Behavior")]
        public DevExpress.Xpf.Grid.EditorButtonShowMode EditorButtonShowMode
        {
            get => 
                (DevExpress.Xpf.Grid.EditorButtonShowMode) base.GetValue(EditorButtonShowModeProperty);
            set => 
                base.SetValue(EditorButtonShowModeProperty, value);
        }

        [Description("Gets or sets whether users can drag a column's header outside of the GridControl to hide the column. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool AllowMoveColumnToDropArea
        {
            get => 
                (bool) base.GetValue(AllowMoveColumnToDropAreaProperty);
            set => 
                base.SetValue(AllowMoveColumnToDropAreaProperty, value);
        }

        [Description("Gets or sets a value that specifies how the column chooser displays columns. This is a dependency property."), Category("Appearance "), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ColumnChooserColumnDisplayMode ColumnChooserColumnDisplayMode
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnChooserColumnDisplayMode) base.GetValue(ColumnChooserColumnDisplayModeProperty);
            set => 
                base.SetValue(ColumnChooserColumnDisplayModeProperty, value);
        }

        [Description(""), Category("Appearance "), CloneDetailMode(CloneDetailMode.Skip)]
        public ControlTemplate ExtendedColumnChooserTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ExtendedColumnChooserTemplateProperty);
            set => 
                base.SetValue(ExtendedColumnChooserTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the Column Band Chooser's presentation. This is a dependency property."), Category("Appearance "), CloneDetailMode(CloneDetailMode.Skip)]
        public ControlTemplate ColumnChooserTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ColumnChooserTemplateProperty);
            set => 
                base.SetValue(ColumnChooserTemplateProperty, value);
        }

        [Description("Gets the actual template that defines the Column Band Chooser's presentation. This is a dependency property.")]
        public ControlTemplate ActualColumnChooserTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ActualColumnChooserTemplateProperty);
            protected set => 
                base.SetValue(ActualColumnChooserTemplatePropertyKey, value);
        }

        [Browsable(false)]
        public bool IsColumnChooserVisible
        {
            get => 
                (bool) base.GetValue(IsColumnChooserVisibleProperty);
            set => 
                base.SetValue(IsColumnChooserVisibleProperty, value);
        }

        [Description("Gets or sets a template that defines the presentation of the drag indicator. This is a dependency property."), Category("Appearance ")]
        public DataTemplate ColumnHeaderDragIndicatorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnHeaderDragIndicatorTemplateProperty);
            set => 
                base.SetValue(ColumnHeaderDragIndicatorTemplateProperty, value);
        }

        [Description("Gets a collection of hidden columns displayed in the Column Chooser. This is a dependency property."), Browsable(false)]
        public ReadOnlyCollection<ColumnBase> ColumnChooserColumns
        {
            get => 
                (ReadOnlyCollection<ColumnBase>) base.GetValue(ColumnChooserColumnsProperty);
            protected internal set => 
                base.SetValue(ColumnChooserColumnsPropertyKey, value);
        }

        [Browsable(false)]
        public double ScrollableAreaMinWidth
        {
            get => 
                (double) base.GetValue(ScrollableAreaMinWidthProperty);
            protected set => 
                base.SetValue(ScrollableAreaMinWidthPropertyKey, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public int TopRowIndex
        {
            get => 
                (int) base.GetValue(TopRowIndexProperty);
            set => 
                base.SetValue(TopRowIndexProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip), Category("Options Behavior")]
        public bool AllowLeaveFocusOnTab
        {
            get => 
                (bool) base.GetValue(AllowLeaveFocusOnTabProperty);
            set => 
                base.SetValue(AllowLeaveFocusOnTabProperty, value);
        }

        [Description("Gets or sets the number of rows to scroll vertically in response to mouse wheel events. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public double WheelScrollLines
        {
            get => 
                (double) base.GetValue(WheelScrollLinesProperty);
            set => 
                base.SetValue(WheelScrollLinesProperty, value);
        }

        [Description("Specifies the touch scroll threshold. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public double TouchScrollThreshold
        {
            get => 
                (double) base.GetValue(TouchScrollThresholdProperty);
            set => 
                base.SetValue(TouchScrollThresholdProperty, value);
        }

        [Description("Gets or sets the display mode of the Drop-Down Filter for all columns within the view. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ColumnFilterPopupMode ColumnFilterPopupMode
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnFilterPopupMode) base.GetValue(ColumnFilterPopupModeProperty);
            set => 
                base.SetValue(ColumnFilterPopupModeProperty, value);
        }

        [Description("Gets or sets whether a row's position is immediately updated according to the sorting, grouping and filtering settings after the current row data has been posted."), Category("Appearance ")]
        public bool ImmediateUpdateRowPosition
        {
            get => 
                (bool) base.GetValue(ImmediateUpdateRowPositionProperty);
            set => 
                base.SetValue(ImmediateUpdateRowPositionProperty, value);
        }

        [Description("Gets or sets the detail section's header content."), Category("Master-Detail"), XtraSerializableProperty, GridUIProperty]
        public object DetailHeaderContent
        {
            get => 
                base.GetValue(DetailHeaderContentProperty);
            set => 
                base.SetValue(DetailHeaderContentProperty, value);
        }

        [Description("Gets or sets data error types to be visualized by a data-aware control (e.g. GridControl). This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ItemsSourceErrorInfoShowMode ItemsSourceErrorInfoShowMode
        {
            get => 
                (DevExpress.Xpf.Grid.ItemsSourceErrorInfoShowMode) base.GetValue(ItemsSourceErrorInfoShowModeProperty);
            set => 
                base.SetValue(ItemsSourceErrorInfoShowModeProperty, value);
        }

        [Description("Gets the active editor. This is a dependency property.")]
        public BaseEdit ActiveEditor
        {
            get => 
                (BaseEdit) base.GetValue(ActiveEditorProperty);
            private set => 
                base.SetValue(ActiveEditorPropertyKey, value);
        }

        [Description("Gets or sets a value which specifies how a cell editor is activated by the mouse. This is a dependency property."), XtraSerializableProperty, Category("Options Behavior")]
        public DevExpress.Xpf.Core.EditorShowMode EditorShowMode
        {
            get => 
                (DevExpress.Xpf.Core.EditorShowMode) base.GetValue(EditorShowModeProperty);
            set => 
                base.SetValue(EditorShowModeProperty, value);
        }

        [Description("Gets or sets whether row and cell focusing is allowed. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public GridViewNavigationStyle NavigationStyle
        {
            get => 
                (GridViewNavigationStyle) base.GetValue(NavigationStyleProperty);
            set => 
                base.SetValue(NavigationStyleProperty, value);
        }

        [Description("Gets or sets whether a focus rectangle is painted around the focused cell or row. This is a dependency property."), Category("Appearance ")]
        public bool ShowFocusedRectangle
        {
            get => 
                (bool) base.GetValue(ShowFocusedRectangleProperty);
            set => 
                base.SetValue(ShowFocusedRectangleProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of a focused cell's border. This is a dependency property."), Category("Appearance ")]
        public ControlTemplate FocusedCellBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(FocusedCellBorderTemplateProperty);
            set => 
                base.SetValue(FocusedCellBorderTemplateProperty, value);
        }

        [Obsolete("Use the DataControlBase.ClipboardCopyMode property instead"), Category("Options Copy"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ClipboardCopyWithHeaders
        {
            get => 
                (bool) base.GetValue(ClipboardCopyWithHeadersProperty);
            set => 
                base.SetValue(ClipboardCopyWithHeadersProperty, value);
        }

        protected internal virtual bool ActualClipboardCopyWithHeaders =>
            ((this.DataControl == null) || (this.DataControl.ClipboardCopyMode == ClipboardCopyMode.Default)) ? ((bool) base.GetValue(ClipboardCopyWithHeadersProperty)) : (this.DataControl.ClipboardCopyMode == ClipboardCopyMode.IncludeHeader);

        [Obsolete("Use the DataControlBase.ClipboardCopyMode property instead"), Category("Options Copy"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ClipboardCopyAllowed
        {
            get => 
                (bool) base.GetValue(ClipboardCopyAllowedProperty);
            set => 
                base.SetValue(ClipboardCopyAllowedProperty, value);
        }

        protected internal virtual bool ActualClipboardCopyAllowed =>
            ((this.DataControl == null) || (this.DataControl.ClipboardCopyMode == ClipboardCopyMode.Default)) ? ((bool) base.GetValue(ClipboardCopyAllowedProperty)) : (this.DataControl.ClipboardCopyMode != ClipboardCopyMode.None);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeyboardFocusWithinView
        {
            get => 
                (bool) base.GetValue(IsKeyboardFocusWithinViewProperty);
            private set => 
                base.SetValue(IsKeyboardFocusWithinViewPropertyKey, value);
        }

        [Obsolete("Use the DataControlBase.SelectedItem/DataControlBase.CurrentItem property instead"), Category("Data"), CloneDetailMode(CloneDetailMode.Skip)]
        public object FocusedRow
        {
            get => 
                base.GetValue(FocusedRowProperty);
            set => 
                base.SetValue(FocusedRowProperty, value);
        }

        [Category("Appearance ")]
        public bool IsSynchronizedWithCurrentItem
        {
            get => 
                (bool) base.GetValue(IsSynchronizedWithCurrentItemProperty);
            set => 
                base.SetValue(IsSynchronizedWithCurrentItemProperty, value);
        }

        [Description("Gets an object that represents the focused row's data.")]
        public RowData FocusedRowData
        {
            get => 
                (RowData) base.GetValue(FocusedRowDataProperty);
            internal set => 
                base.SetValue(FocusedRowDataPropertyKey, value);
        }

        [Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public IColumnChooserFactory ColumnChooserFactory
        {
            get => 
                (IColumnChooserFactory) base.GetValue(ColumnChooserFactoryProperty);
            set => 
                base.SetValue(ColumnChooserFactoryProperty, value);
        }

        [Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public bool IsHorizontalScrollBarVisible
        {
            get => 
                (bool) base.GetValue(IsHorizontalScrollBarVisibleProperty);
            internal set => 
                base.SetValue(IsHorizontalScrollBarVisiblePropertyKey, value);
        }

        [Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public bool IsTouchScrollBarsMode
        {
            get => 
                (bool) base.GetValue(IsTouchScrollBarsModeProperty);
            internal set => 
                base.SetValue(IsTouchScrollBarsModePropertyKey, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Content), GridUIProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public IColumnChooserState ColumnChooserState
        {
            get => 
                (IColumnChooserState) base.GetValue(ColumnChooserStateProperty);
            set => 
                base.SetValue(ColumnChooserStateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of data cells. This is a dependency property."), Category("Appearance ")]
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

        [Category("Appearance ")]
        public DataTemplate CellEditTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CellEditTemplateProperty);
            set => 
                base.SetValue(CellEditTemplateProperty, value);
        }

        [Category("Appearance ")]
        public DataTemplateSelector CellEditTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(CellEditTemplateSelectorProperty);
            set => 
                base.SetValue(CellEditTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the length of an animation. This is a dependency property."), Category("Appearance ")]
        public Duration RowOpacityAnimationDuration
        {
            get => 
                (Duration) base.GetValue(RowOpacityAnimationDurationProperty);
            set => 
                base.SetValue(RowOpacityAnimationDurationProperty, value);
        }

        [Description("Gets or sets whether a View is automatically scrolled after the order of rows has been changed. This is a dependency property."), Category("Options View")]
        public bool AutoScrollOnSorting
        {
            get => 
                (bool) base.GetValue(AutoScrollOnSortingProperty);
            set => 
                base.SetValue(AutoScrollOnSortingProperty, value);
        }

        internal bool IsEditingCore { get; private set; }

        [Description("Gets whether the focused cell is currently being edited. This is a dependency property.")]
        public bool IsEditing
        {
            get => 
                (bool) base.GetValue(IsEditingProperty);
            internal set => 
                base.SetValue(IsEditingPropertyKey, value);
        }

        [Description("Indicates whether the edit value of an editor within the focused row has been modified. This is a dependency property.")]
        public bool IsFocusedRowModified
        {
            get => 
                (bool) base.GetValue(IsFocusedRowModifiedProperty);
            internal set => 
                base.SetValue(IsFocusedRowModifiedPropertyKey, value);
        }

        [Description("Gets or sets whether the operand's value can be swapped. This is a dependency property."), Category("Options Filter")]
        public bool FilterEditorShowOperandTypeIcon
        {
            get => 
                (bool) base.GetValue(FilterEditorShowOperandTypeIconProperty);
            set => 
                base.SetValue(FilterEditorShowOperandTypeIconProperty, value);
        }

        [Description("Gets or sets whether error icons are displayed within cells that fail validation specified by the Data Annotations attribute(s). This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool ShowValidationAttributeErrors
        {
            get => 
                (bool) base.GetValue(ShowValidationAttributeErrorsProperty);
            set => 
                base.SetValue(ShowValidationAttributeErrorsProperty, value);
        }

        [Description("Gets whether the Filter Panel is displayed within a View. This is a dependency property.")]
        public bool ActualShowFilterPanel
        {
            get => 
                (bool) base.GetValue(ActualShowFilterPanelProperty);
            private set => 
                base.SetValue(ActualShowFilterPanelPropertyKey, value);
        }

        [Description("Gets the text displayed within the Filter Panel. This is a dependency property.")]
        public string FilterPanelText
        {
            get => 
                (string) base.GetValue(FilterPanelTextProperty);
            private set => 
                base.SetValue(FilterPanelTextPropertyKey, value);
        }

        [Description("Gets or sets when the Filter Panel is displayed within a View. This is a dependency property."), Category("Options View"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ShowFilterPanelMode ShowFilterPanelMode
        {
            get => 
                (DevExpress.Xpf.Grid.ShowFilterPanelMode) base.GetValue(ShowFilterPanelModeProperty);
            set => 
                base.SetValue(ShowFilterPanelModeProperty, value);
        }

        [Description("Gets or sets whether the Summary Panel is displayed. This is a dependency property."), Category("Options View"), XtraSerializableProperty]
        public bool ShowTotalSummary
        {
            get => 
                (bool) base.GetValue(ShowTotalSummaryProperty);
            set => 
                base.SetValue(ShowTotalSummaryProperty, value);
        }

        [Description("Gets or sets whether a view displays column headers. This is a dependency property."), Category("Options View"), XtraSerializableProperty]
        public bool ShowColumnHeaders
        {
            get => 
                (bool) base.GetValue(ShowColumnHeadersProperty);
            set => 
                base.SetValue(ShowColumnHeadersProperty, value);
        }

        [Description("Gets or sets the style applied to total summary items displayed within a View. This is a dependency property."), Category("Appearance ")]
        public Style TotalSummaryContentStyle
        {
            get => 
                (Style) base.GetValue(TotalSummaryContentStyleProperty);
            set => 
                base.SetValue(TotalSummaryContentStyleProperty, value);
        }

        [Description("Gets or sets the style of the text within column headers. This is a dependency property."), Category("Appearance ")]
        public Style ColumnHeaderContentStyle
        {
            get => 
                (Style) base.GetValue(ColumnHeaderContentStyleProperty);
            set => 
                base.SetValue(ColumnHeaderContentStyleProperty, value);
        }

        [Category("Appearance ")]
        public Style ColumnHeaderImageStyle
        {
            get => 
                (Style) base.GetValue(ColumnHeaderImageStyleProperty);
            set => 
                base.SetValue(ColumnHeaderImageStyleProperty, value);
        }

        [Description("Gets or sets the style applied to data cells displayed within a View. This is a dependency property."), Category("Appearance ")]
        public Style CellStyle
        {
            get => 
                (Style) base.GetValue(CellStyleProperty);
            set => 
                base.SetValue(CellStyleProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of total summary items. This is a dependency property."), Category("Appearance ")]
        public DataTemplate TotalSummaryItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TotalSummaryItemTemplateProperty);
            set => 
                base.SetValue(TotalSummaryItemTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a total summary template based on custom logic. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance ")]
        public DataTemplateSelector TotalSummaryItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(TotalSummaryItemTemplateSelectorProperty);
            set => 
                base.SetValue(TotalSummaryItemTemplateSelectorProperty, value);
        }

        [Description("Gets the actual template selector that chooses a total summary item template based on custom logic. This is a dependency property.")]
        public DataTemplateSelector ActualTotalSummaryItemTemplateSelector =>
            (DataTemplateSelector) base.GetValue(ActualTotalSummaryItemTemplateSelectorProperty);

        [Description("Gets or sets the template that defines the presentation of a column header panel. This is a dependency property."), Category("Appearance ")]
        public DataTemplate HeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderTemplateProperty);
            set => 
                base.SetValue(HeaderTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the Summary Panel's presentation. This is a dependency property."), Category("Appearance ")]
        public DataTemplate FooterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FooterTemplateProperty);
            set => 
                base.SetValue(FooterTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of column header content. This is a dependency property."), Category("Appearance ")]
        public DataTemplate ColumnHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnHeaderTemplateProperty);
            set => 
                base.SetValue(ColumnHeaderTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a column header template based on custom logic. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance ")]
        public DataTemplateSelector ColumnHeaderTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ColumnHeaderTemplateSelectorProperty);
            set => 
                base.SetValue(ColumnHeaderTemplateSelectorProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of the customization area displayed within column headers. This is a dependency property."), Category("Appearance ")]
        public DataTemplate ColumnHeaderCustomizationAreaTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnHeaderCustomizationAreaTemplateProperty);
            set => 
                base.SetValue(ColumnHeaderCustomizationAreaTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a template based on custom logic. This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance ")]
        public DataTemplateSelector ColumnHeaderCustomizationAreaTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ColumnHeaderCustomizationAreaTemplateSelectorProperty);
            set => 
                base.SetValue(ColumnHeaderCustomizationAreaTemplateSelectorProperty, value);
        }

        [Description("Gets or sets whether an end user can right-click a column header to access its context menu."), Category("Options Behavior"), XtraSerializableProperty]
        public bool IsColumnMenuEnabled
        {
            get => 
                (bool) base.GetValue(IsColumnMenuEnabledProperty);
            set => 
                base.SetValue(IsColumnMenuEnabledProperty, value);
        }

        [Description("Gets or sets whether an end user can right-click the Total Summary Panel or the Fixed Summary Panel to access their context menus. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool IsTotalSummaryMenuEnabled
        {
            get => 
                (bool) base.GetValue(IsTotalSummaryMenuEnabledProperty);
            set => 
                base.SetValue(IsTotalSummaryMenuEnabledProperty, value);
        }

        [Description("Gets or sets whether an end user can right-click a row cell to access its context menu. This is a dependency property."), Category("Options Behavior"), XtraSerializableProperty]
        public bool IsRowCellMenuEnabled
        {
            get => 
                (bool) base.GetValue(IsRowCellMenuEnabledProperty);
            set => 
                base.SetValue(IsRowCellMenuEnabledProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of a column header's tooltip. This is a dependency property."), Category("Appearance ")]
        public DataTemplate ColumnHeaderToolTipTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnHeaderToolTipTemplateProperty);
            set => 
                base.SetValue(ColumnHeaderToolTipTemplateProperty, value);
        }

        public bool ActualShowPager
        {
            get => 
                (bool) base.GetValue(ActualShowPagerProperty);
            protected set => 
                base.SetValue(ActualShowPagerPropertyKey, value);
        }

        [Description("Provides access to view commands.")]
        public DataViewCommandsBase Commands { get; private set; }

        [Description("Gets or sets whether null values must be ignored when calculating data summaries. This is a dependency property."), Category("Options View")]
        public bool SummariesIgnoreNullValues
        {
            get => 
                (bool) base.GetValue(SummariesIgnoreNullValuesProperty);
            set => 
                base.SetValue(SummariesIgnoreNullValuesProperty, value);
        }

        [Category("Options Behavior")]
        public bool EnterMoveNextColumn
        {
            get => 
                (bool) base.GetValue(EnterMoveNextColumnProperty);
            set => 
                base.SetValue(EnterMoveNextColumnProperty, value);
        }

        [Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public IComparer<ColumnBase> ColumnChooserColumnsSortOrderComparer
        {
            get => 
                (IComparer<ColumnBase>) base.GetValue(ColumnChooserColumnsSortOrderComparerProperty);
            set => 
                base.SetValue(ColumnChooserColumnsSortOrderComparerProperty, value);
        }

        [Description("Gets or sets whether the Fixed Summary Panel is displayed within the grid. This is a dependency property."), Category("Options View"), XtraSerializableProperty]
        public bool ShowFixedTotalSummary
        {
            get => 
                (bool) base.GetValue(ShowFixedTotalSummaryProperty);
            set => 
                base.SetValue(ShowFixedTotalSummaryProperty, value);
        }

        [Description("Gets or sets whether to post values to a data source immediately after changing a cell value. This is a dependency property."), Category("Options Copy")]
        public bool EnableImmediatePosting
        {
            get => 
                (bool) base.GetValue(EnableImmediatePostingProperty);
            set => 
                base.SetValue(EnableImmediatePostingProperty, value);
        }

        [Description("Gets or sets whether an editor that did not pass validation can lose focus. This is a dependency property."), Category("Options Behavior")]
        public bool AllowLeaveInvalidEditor
        {
            get => 
                (bool) base.GetValue(AllowLeaveInvalidEditorProperty);
            set => 
                base.SetValue(AllowLeaveInvalidEditorProperty, value);
        }

        [Description("Gets or sets the style applied to individual text elements in the total summary items within a view. This is a dependency property."), Category("Appearance ")]
        public Style TotalSummaryElementStyle
        {
            get => 
                (Style) base.GetValue(TotalSummaryElementStyleProperty);
            set => 
                base.SetValue(TotalSummaryElementStyleProperty, value);
        }

        [Description("Gets or sets the style applied to individual text elements in the fixed total summary item. This is a dependency property."), Category("Appearance ")]
        public Style FixedTotalSummaryElementStyle
        {
            get => 
                (Style) base.GetValue(FixedTotalSummaryElementStyleProperty);
            set => 
                base.SetValue(FixedTotalSummaryElementStyleProperty, value);
        }

        [Description(""), Category("Appearance ")]
        public DataTemplate FilterEditorDialogServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FilterEditorDialogServiceTemplateProperty);
            set => 
                base.SetValue(FilterEditorDialogServiceTemplateProperty, value);
        }

        [Description(""), Category("Appearance ")]
        public DataTemplate FilterEditorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(FilterEditorTemplateProperty);
            set => 
                base.SetValue(FilterEditorTemplateProperty, value);
        }

        [Description(""), Category("Options Behavior")]
        public bool? UseLegacyFilterEditor
        {
            get => 
                (bool?) base.GetValue(UseLegacyFilterEditorProperty);
            set => 
                base.SetValue(UseLegacyFilterEditorProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of column headers when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public DataTemplate PrintHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PrintHeaderTemplateProperty);
            set => 
                base.SetValue(PrintHeaderTemplateProperty, value);
        }

        [Description("Gets or sets the style applied to all data cells when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintCellStyle
        {
            get => 
                (Style) base.GetValue(PrintCellStyleProperty);
            set => 
                base.SetValue(PrintCellStyleProperty, value);
        }

        [Description("Gets or sets the style applied to the row indents when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintRowIndentStyle
        {
            get => 
                (Style) base.GetValue(PrintRowIndentStyleProperty);
            set => 
                base.SetValue(PrintRowIndentStyleProperty, value);
        }

        [Description("Gets or sets the width of the row indents when the GridControl is printed. This is a dependency property."), Category("Appearance Print")]
        public double PrintRowIndentWidth
        {
            get => 
                (double) base.GetValue(PrintRowIndentWidthProperty);
            set => 
                base.SetValue(PrintRowIndentWidthProperty, value);
        }

        [Description("Gets or sets whether only the selected rows are printed/exported. This is a dependency property."), Category("Options Print"), XtraSerializableProperty]
        public bool PrintSelectedRowsOnly
        {
            get => 
                (bool) base.GetValue(PrintSelectedRowsOnlyProperty);
            set => 
                base.SetValue(PrintSelectedRowsOnlyProperty, value);
        }

        [Description("Gets or sets whether the Summary Panel is printed. This is a dependency property."), Category("Options Print"), XtraSerializableProperty]
        public bool PrintTotalSummary
        {
            get => 
                (bool) base.GetValue(PrintTotalSummaryProperty);
            set => 
                base.SetValue(PrintTotalSummaryProperty, value);
        }

        [Description("Gets or sets whether the Fixed Summary Panel is printed. This is a dependency property."), Category("Options Print"), XtraSerializableProperty]
        public bool PrintFixedTotalSummary
        {
            get => 
                (bool) base.GetValue(PrintFixedTotalSummaryProperty);
            set => 
                base.SetValue(PrintFixedTotalSummaryProperty, value);
        }

        [Description("Gets or sets the style applied to total summary items when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintTotalSummaryStyle
        {
            get => 
                (Style) base.GetValue(PrintTotalSummaryStyleProperty);
            set => 
                base.SetValue(PrintTotalSummaryStyleProperty, value);
        }

        [Description("Gets or sets the style applied to summary items displayed within the Fixed Summary Panel when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintFixedTotalSummaryStyle
        {
            get => 
                (Style) base.GetValue(PrintFixedTotalSummaryStyleProperty);
            set => 
                base.SetValue(PrintFixedTotalSummaryStyleProperty, value);
        }

        [Description("Gets or sets the template that defines the Summary Panel's presentation when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public DataTemplate PrintFooterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PrintFooterTemplateProperty);
            set => 
                base.SetValue(PrintFooterTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the Fixed Summary Panel's presentation when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public DataTemplate PrintFixedFooterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PrintFixedFooterTemplateProperty);
            set => 
                base.SetValue(PrintFixedFooterTemplateProperty, value);
        }

        internal DevExpress.Xpf.Grid.FixedSummariesHelper FixedSummariesHelper =>
            this.fixedSummariesHelper;

        [Category("Layout")]
        public IList<GridTotalSummaryData> FixedSummariesLeft =>
            this.fixedSummariesLeft;

        [Category("Layout")]
        public IList<GridTotalSummaryData> FixedSummariesRight =>
            this.fixedSummariesRight;

        protected internal virtual bool ForceShowTotalSummaryColumnName =>
            false;

        [Obsolete("Use the DataControlBase.SelectedItems property instead"), Browsable(false)]
        public IList SelectedRows =>
            this.DataControl?.SelectedItems;

        [Obsolete("Use the DataControlBase.SelectedItems property instead"), CloneDetailMode(CloneDetailMode.Skip), Category("Options Selection")]
        public IList SelectedRowsSource
        {
            get => 
                (IList) base.GetValue(SelectedRowsSourceProperty);
            set => 
                base.SetValue(SelectedRowsSourceProperty, value);
        }

        [Description("Gets or sets whether to display the Close button within the Search Panel."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool ShowSearchPanelCloseButton
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelCloseButtonProperty);
            set => 
                base.SetValue(ShowSearchPanelCloseButtonProperty, value);
        }

        [Description("Gets or sets the type of the comparison operator used to create filter conditions. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public FilterCondition SearchPanelFindFilter
        {
            get => 
                (FilterCondition) base.GetValue(SearchPanelFindFilterProperty);
            set => 
                base.SetValue(SearchPanelFindFilterProperty, value);
        }

        [Description("Gets or sets a mode that specifies how the search string is parsed. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public DevExpress.Xpf.Editors.SearchPanelParseMode SearchPanelParseMode
        {
            get => 
                (DevExpress.Xpf.Editors.SearchPanelParseMode) base.GetValue(SearchPanelParseModeProperty);
            set => 
                base.SetValue(SearchPanelParseModeProperty, value);
        }

        [Description("Gets or sets the search string specified within the Search Panel. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, GridUIProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public string SearchString
        {
            get => 
                (string) base.GetValue(SearchStringProperty);
            set => 
                base.SetValue(SearchStringProperty, value);
        }

        [Description("Gets or sets whether to highlight search results within located records. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool SearchPanelHighlightResults
        {
            get => 
                (bool) base.GetValue(SearchPanelHighlightResultsProperty);
            set => 
                base.SetValue(SearchPanelHighlightResultsProperty, value);
        }

        [Description("Gets or sets whether to display the Find button within the Search Panel."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool ShowSearchPanelFindButton
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelFindButtonProperty);
            set => 
                base.SetValue(ShowSearchPanelFindButtonProperty, value);
        }

        [Description("Gets or sets whether data searching starts automatically, or must be started manually. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public FindMode SearchPanelFindMode
        {
            get => 
                (FindMode) base.GetValue(SearchPanelFindModeProperty);
            set => 
                base.SetValue(SearchPanelFindModeProperty, value);
        }

        [Description("Gets or sets whether a button used to invoke the MRU search dropdown list is displayed within the Search Panel. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool ShowSearchPanelMRUButton
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelMRUButtonProperty);
            set => 
                base.SetValue(ShowSearchPanelMRUButtonProperty, value);
        }

        [Description("Gets or sets whether the grid displays only those records that match the search criteria. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool SearchPanelAllowFilter
        {
            get => 
                (bool) base.GetValue(SearchPanelAllowFilterProperty);
            set => 
                base.SetValue(SearchPanelAllowFilterProperty, value);
        }

        [Obsolete("Use the SearchPanelParseMode property instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), CloneDetailMode(CloneDetailMode.Skip)]
        public CriteriaOperatorType SearchPanelCriteriaOperatorType
        {
            get => 
                (CriteriaOperatorType) base.GetValue(SearchPanelCriteriaOperatorTypeProperty);
            set => 
                base.SetValue(SearchPanelCriteriaOperatorTypeProperty, value);
        }

        [Description("Gets or sets the field names against which searches are performed by the Search Panel. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public string SearchColumns
        {
            get => 
                (string) base.GetValue(SearchColumnsProperty);
            set => 
                base.SetValue(SearchColumnsProperty, value);
        }

        [Description("Gets or sets whether the search string is cleared when closing the Search Panel. This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public bool SearchPanelClearOnClose
        {
            get => 
                (bool) base.GetValue(SearchPanelClearOnCloseProperty);
            set => 
                base.SetValue(SearchPanelClearOnCloseProperty, value);
        }

        [Description("Gets or sets a value that specifies when the Search Panel is displayed."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public DevExpress.Xpf.Grid.ShowSearchPanelMode ShowSearchPanelMode
        {
            get => 
                (DevExpress.Xpf.Grid.ShowSearchPanelMode) base.GetValue(ShowSearchPanelModeProperty);
            set => 
                base.SetValue(ShowSearchPanelModeProperty, value);
        }

        [Description("Gets whether the Search Panel is displayed within a View. This is a dependency property."), XtraSerializableProperty, XtraResetProperty(ResetPropertyMode.None)]
        public bool ActualShowSearchPanel
        {
            get => 
                (bool) base.GetValue(ActualShowSearchPanelProperty);
            private set => 
                base.SetValue(ActualShowSearchPanelPropertyKey, value);
        }

        [Description("Gets or sets the amount of time in milliseconds, after which a data search is initiated (in an automatic find mode). This is a dependency property."), Category("Search Panel"), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip)]
        public int SearchDelay
        {
            get => 
                (int) base.GetValue(SearchDelayProperty);
            set => 
                base.SetValue(SearchDelayProperty, value);
        }

        [Description("Gets or sets whether on not the MRU search dropdown is automatically invoked when typing within the Search box. This is a dependency property."), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip), Category("Search Panel")]
        public bool? SearchPanelImmediateMRUPopup
        {
            get => 
                (bool?) base.GetValue(SearchPanelImmediateMRUPopupProperty);
            set => 
                base.SetValue(SearchPanelImmediateMRUPopupProperty, value);
        }

        [Description("Gets or sets the Search Panel's alignment. This is a dependency property."), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip), Category("Search Panel")]
        public HorizontalAlignment? SearchPanelHorizontalAlignment
        {
            get => 
                (HorizontalAlignment?) base.GetValue(SearchPanelHorizontalAlignmentProperty);
            set => 
                base.SetValue(SearchPanelHorizontalAlignmentProperty, value);
        }

        [Description(""), XtraSerializableProperty, CloneDetailMode(CloneDetailMode.Skip), Category("Search Panel")]
        public HorizontalAlignment ActualSearchPanelHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(ActualSearchPanelHorizontalAlignmentProperty);
            internal set => 
                base.SetValue(ActualSearchPanelHorizontalAlignmentPropertyKey, value);
        }

        [Description("Gets or sets a value that specifies which buttons are displayed by the data navigator."), Category("Options Behavior")]
        public NavigatorButtonType DataNavigatorButtons
        {
            get => 
                (NavigatorButtonType) base.GetValue(DataNavigatorButtonsProperty);
            set => 
                base.SetValue(DataNavigatorButtonsProperty, value);
        }

        [Description("Gets or sets the delay in milliseconds, after which data filtering is initiated (if automatic search mode is active)."), Category("Options Filter")]
        public int FilterRowDelay
        {
            get => 
                (int) base.GetValue(FilterRowDelayProperty);
            set => 
                base.SetValue(FilterRowDelayProperty, value);
        }

        [Description("Gets or sets with which formats (RTF, HTML, Biff8(xls), CSV, Text, UnicodeText) the data copied from this Grid Control should be compatible.This is a dependency property."), Category("Options Copy")]
        public DevExpress.Xpf.Grid.ClipboardCopyOptions ClipboardCopyOptions
        {
            get => 
                (DevExpress.Xpf.Grid.ClipboardCopyOptions) base.GetValue(ClipboardCopyOptionsProperty);
            set => 
                base.SetValue(ClipboardCopyOptionsProperty, value);
        }

        [Description("Specifies whether the grid cell data should be copied together with the format settings.This is a dependency property."), Category("Conditional Formatting")]
        public DevExpress.Xpf.Grid.ClipboardMode ClipboardMode
        {
            get => 
                (DevExpress.Xpf.Grid.ClipboardMode) base.GetValue(ClipboardModeProperty);
            set => 
                base.SetValue(ClipboardModeProperty, value);
        }

        [Description("Gets whether the view has any data editing errors. This is a dependency property.")]
        public bool HasErrors
        {
            get => 
                (bool) base.GetValue(HasErrorsProperty);
            private set => 
                base.SetValue(HasErrorsPropertyKey, value);
        }

        [Description("Gets or sets which type of errors the grid control should detect during the initial data loading.")]
        public DevExpress.Xpf.Grid.ErrorsWatchMode ErrorsWatchMode
        {
            get => 
                (DevExpress.Xpf.Grid.ErrorsWatchMode) base.GetValue(ErrorsWatchModeProperty);
            set => 
                base.SetValue(ErrorsWatchModeProperty, value);
        }

        [Description("Gets or sets the template that defines the cell tooltips's appearance. This is a dependency property."), Category("Appearance ")]
        public DataTemplate CellToolTipTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CellToolTipTemplateProperty);
            set => 
                base.SetValue(CellToolTipTemplateProperty, value);
        }

        [Description("Gets or sets the view's header. This is a dependency property."), Category("Appearance "), XtraSerializableProperty]
        public object Header
        {
            get => 
                base.GetValue(HeaderProperty);
            set => 
                base.SetValue(HeaderProperty, value);
        }

        [Description("Gets or sets the grid view's header position. This is a dependency property."), Category("Appearance "), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.HeaderPosition HeaderPosition
        {
            get => 
                (DevExpress.Xpf.Grid.HeaderPosition) base.GetValue(HeaderPositionProperty);
            set => 
                base.SetValue(HeaderPositionProperty, value);
        }

        [Description(""), Category("Appearance "), XtraSerializableProperty]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(HeaderHorizontalAlignmentProperty);
            set => 
                base.SetValue(HeaderHorizontalAlignmentProperty, value);
        }

        [Description(""), Category("Options Behavior"), XtraSerializableProperty]
        public bool ValidatesOnNotifyDataErrors
        {
            get => 
                (bool) base.GetValue(ValidatesOnNotifyDataErrorsProperty);
            set => 
                base.SetValue(ValidatesOnNotifyDataErrorsProperty, value);
        }

        [Description(""), Category("Options Behavior"), XtraSerializableProperty]
        public DevExpress.Xpf.Grid.ColumnSortClearMode ColumnSortClearMode
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnSortClearMode) base.GetValue(ColumnSortClearModeProperty);
            set => 
                base.SetValue(ColumnSortClearModeProperty, value);
        }

        public bool IsAdditionalElementScrollBarVisible
        {
            get => 
                (bool) base.GetValue(IsAdditionalElementScrollBarVisibleProperty);
            protected internal set => 
                base.SetValue(IsAdditionalElementScrollBarVisiblePropertyKey, value);
        }

        [Description("Gets or sets a style applied to the all column headers within the View. This is a dependency property."), Category("Appearance ")]
        public Style ColumnHeaderStyle
        {
            get => 
                (Style) base.GetValue(ColumnHeaderStyleProperty);
            set => 
                base.SetValue(ColumnHeaderStyleProperty, value);
        }

        [Description("Gets or sets whether to show the No Records text when the GridControl's data area displays no records."), Category("Appearance "), XtraSerializableProperty]
        public bool ShowEmptyText
        {
            get => 
                (bool) base.GetValue(ShowEmptyTextProperty);
            set => 
                base.SetValue(ShowEmptyTextProperty, value);
        }

        [Description("Gets or sets a mode that specifies by which rows the summary value is calculated."), Category("Options View"), XtraSerializableProperty]
        public GridSummaryCalculationMode SummaryCalculationMode
        {
            get => 
                (GridSummaryCalculationMode) base.GetValue(SummaryCalculationModeProperty);
            set => 
                base.SetValue(SummaryCalculationModeProperty, value);
        }

        protected internal RowsClipboardController ClipboardController
        {
            get
            {
                this.clipboardController ??= this.CreateClipboardController();
                return this.clipboardController;
            }
        }

        protected internal ClipboardOptions OptionsClipboard
        {
            get
            {
                this._optionsClipboard ??= this.CreateOptionsClipboard();
                return this._optionsClipboard;
            }
        }

        internal bool ShouldPrintTotalSummary =>
            this.ShowTotalSummary && this.PrintTotalSummary;

        internal bool ShouldPrintFixedTotalSummary =>
            this.ShowFixedTotalSummary && this.PrintFixedTotalSummary;

        internal virtual bool ShouldPrintGroupSummaryInTotal =>
            false;

        internal bool EnableSelectedRowAppearanceCore { get; private set; }

        [Description("Gets or sets whether the selected rows are decorated with a specific appearance. This is a dependency property."), Category("Appearance "), XtraSerializableProperty]
        public bool EnableSelectedRowAppearance
        {
            get => 
                (bool) base.GetValue(EnableSelectedRowAppearanceProperty);
            set => 
                base.SetValue(EnableSelectedRowAppearanceProperty, value);
        }

        protected virtual int FixedNoneColumnsCount =>
            this.VisibleColumnsCore.Count;

        internal GridViewNavigationBase AdditionalRowNavigation =>
            this.additionalRowNavigation;

        protected internal bool AllowMouseMoveSelection { get; set; }

        protected internal GridViewNavigationBase Navigation
        {
            get
            {
                this.navigation ??= this.CreateNavigation();
                return this.navigation;
            }
            protected set => 
                this.navigation = value;
        }

        internal SelectionStrategyBase SelectionStrategy
        {
            get
            {
                this.selectionStrategy ??= this.CreateSelectionStrategy();
                return this.selectionStrategy;
            }
            set => 
                this.selectionStrategy = value;
        }

        internal bool IsFirstRow =>
            (this.DataProviderBase != null) && (this.DataProviderBase.CurrentIndex == 0);

        internal bool IsLastRow =>
            (this.DataProviderBase != null) && ((this.DataProviderBase.CurrentIndex == (this.DataControl.VisibleRowCount - 1)) && !this.IsTopNewItemRowFocused);

        internal DataViewBehavior ViewBehavior { get; private set; }

        protected virtual bool IsLoading =>
            (this.DataControl == null) || this.DataControl.IsLoading;

        [Browsable(false)]
        public FrameworkElement ScrollContentPresenter { get; protected internal set; }

        internal bool UseMouseUpFocusedEditorShowModeStrategy =>
            this.ViewBehavior.UseMouseUpFocusedEditorShowModeStrategy || this.IsDataViewDragDropManagerAttached();

        internal bool HasCellEditorError =>
            this.HasValidationError && ((this.ValidationError is RowValidationError) && ((RowValidationError) this.ValidationError).IsCellError);

        internal IList<ColumnBase> VisibleColumnsCore
        {
            get => 
                this.visibleColumnsCore;
            set
            {
                this.visibleColumnsCore = value;
                this.SetVisibleColumns(this.visibleColumnsCore);
                if (this.DataControl != null)
                {
                    if ((this.DataControl.CurrentColumn != null) && !this.visibleColumnsCore.Contains(this.DataControl.CurrentColumn))
                    {
                        this.DataControl.CurrentColumn = null;
                    }
                    this.DataControl.InitializeCurrentColumn();
                }
            }
        }

        internal IEnumerable<ColumnBase> PrintableColumns
        {
            get
            {
                Func<ColumnBase, bool> predicate = <>c.<>9__948_0;
                if (<>c.<>9__948_0 == null)
                {
                    Func<ColumnBase, bool> local1 = <>c.<>9__948_0;
                    predicate = <>c.<>9__948_0 = column => column.AllowPrinting;
                }
                return this.VisibleColumnsCore.Where<ColumnBase>(predicate);
            }
        }

        protected internal bool IsDesignTime =>
            DesignerProperties.GetIsInDesignMode(this);

        internal bool IsInvalidFocusedRowHandle =>
            this.FocusedRowHandle == -2147483648;

        protected internal virtual DevExpress.Xpf.Data.DataProviderBase DataProviderBase =>
            this.DataControl?.DataProviderBase;

        protected internal ColumnBase CheckBoxSelectorColumn { get; protected set; }

        protected internal virtual bool IsCheckBoxSelectorColumnVisible =>
            false;

        protected internal virtual bool ShowGroupedColumnsCore =>
            false;

        protected internal virtual Orientation OrientationCore =>
            Orientation.Vertical;

        internal IScrollInfoOwner ScrollInfoOwner { get; set; }

        internal DataPresenterBase DataPresenter =>
            this.ScrollInfoOwner as DataPresenterBase;

        internal DataPresenterBase RootDataPresenter =>
            this.RootView.DataPresenter;

        internal virtual bool AllowFixedGroupsCore =>
            false;

        internal virtual bool AllowPartialGroupingCore =>
            false;

        internal bool LockEditorClose { get; set; }

        [Description("Gets the information about column headers.")]
        public DevExpress.Xpf.Grid.HeadersData HeadersData =>
            this.visualDataTreeBuilder.HeadersData;

        internal DevExpress.Xpf.Grid.Native.VisualDataTreeBuilder VisualDataTreeBuilder =>
            this.visualDataTreeBuilder;

        protected internal DevExpress.Xpf.Grid.DetailNodeContainer RootNodeContainer =>
            this.visualDataTreeBuilder.RootNodeContainer;

        internal MasterNodeContainer MasterRootNodeContainer =>
            this.visualDataTreeBuilder.MasterRootNodeContainer;

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public DevExpress.Xpf.Grid.DetailRowsContainer RootRowsContainer =>
            this.visualDataTreeBuilder.RootRowsContainer;

        public MasterRowsContainer MasterRootRowsContainer =>
            this.visualDataTreeBuilder.MasterRootRowsContainer;

        protected internal Dictionary<int, DataRowNode> Nodes =>
            this.visualDataTreeBuilder.Nodes;

        public bool IsFocusedView =>
            ReferenceEquals(this.MasterRootRowsContainer.FocusedView, this);

        internal IColumnCollection ColumnsCore =>
            (this.DataControl != null) ? this.DataControl.ColumnsCore : this.emptyColumns;

        internal GridViewInplaceEditorOwner InplaceEditorOwner { get; set; }

        protected internal bool EditorSetInactiveAfterClick { get; set; }

        internal SelectionAnchorCell SelectionAnchor
        {
            get => 
                this.RootView.selectionAnchorCore;
            set => 
                this.RootView.selectionAnchorCore = value;
        }

        internal SelectionAnchorCell SelectionOldCell
        {
            get => 
                this.RootView.selectionOldCellCore;
            set => 
                this.RootView.selectionOldCellCore = value;
        }

        internal StartPointSelectionRectangleInfo SelectionRectangleAnchor
        {
            get => 
                this.RootView.selectionRectangleAnchorCore;
            set => 
                this.RootView.selectionRectangleAnchorCore = value;
        }

        internal FrameworkElement RootBandsContainer { get; set; }

        internal FrameworkElement HeadersPanel { get; set; }

        internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager =>
            this.RootView.immediateActionsManager;

        protected internal bool IsColumnFilterOpened
        {
            get => 
                this.isColumnFilterOpened;
            set
            {
                if (this.isColumnFilterOpened != value)
                {
                    this.isColumnFilterOpened = value;
                    this.OnIsColumnFilterOpenedChanged();
                }
            }
        }

        protected internal bool IsColumnFilterLoaded { get; set; }

        internal virtual DevExpress.Xpf.Grid.Native.RowMarginControlDisplayMode RowMarginControlDisplayMode =>
            DevExpress.Xpf.Grid.Native.RowMarginControlDisplayMode.Hidden;

        protected internal Locker KeyboardLocker =>
            this.InplaceEditorOwner.KeyboardLocker;

        protected internal DataIteratorBase DataIterator =>
            this.dataIterator;

        internal bool RowsStateDirty
        {
            get => 
                this.rowsStateDirty;
            set => 
                this.rowsStateDirty = value;
        }

        protected internal Guid CacheVersion { get; protected set; }

        internal IColumnChooser ActualColumnChooser
        {
            get
            {
                if (this.actualColumnChooser == null)
                {
                    this.ActualColumnChooser = this.CreateColumnChooser();
                }
                return this.actualColumnChooser;
            }
            set
            {
                if (!ReferenceEquals(this.actualColumnChooser, value))
                {
                    if (this.actualColumnChooser != null)
                    {
                        if (this.columnChooserForceDestoyLocker.IsLocked && (this.actualColumnChooser is DefaultColumnChooser))
                        {
                            ((DefaultColumnChooser) this.actualColumnChooser).DestroyBase(true);
                        }
                        else
                        {
                            this.actualColumnChooser.Destroy();
                        }
                    }
                    this.actualColumnChooser = value;
                }
            }
        }

        protected internal bool IsActualColumnChooserCreated =>
            this.actualColumnChooser != null;

        [Description("Gets the data-aware control (e.g., GridControl, TreeListControl) which owns the current View.")]
        public DataControlBase DataControl
        {
            get => 
                this.dataControl;
            internal set => 
                this.SetDataControl(value);
        }

        internal SortInfoCollectionBase SortInfoCore =>
            this.DataControl.SortInfoCore;

        internal Locker SupressCacheCleanCountLocker { get; private set; }

        protected internal FrameworkElement FocusedRowElement =>
            !this.ViewBehavior.IsAdditionalRow(this.FocusedRowHandle) ? this.GetRowElementByRowHandle(this.FocusedRowHandle) : this.ViewBehavior.GetAdditionalRowElement(this.FocusedRowHandle);

        protected override bool HandlesScrolling =>
            true;

        protected internal InplaceEditorBase CurrentCellEditor
        {
            get => 
                (CellEditorBase) this.InplaceEditorOwner.CurrentCellEditor;
            set => 
                this.InplaceEditorOwner.CurrentCellEditor = value;
        }

        internal int NavigationIndex
        {
            get => 
                ((this.DataControl == null) || (this.DataControl.CurrentColumn == null)) ? -2147483648 : this.DataControl.CurrentColumn.ActualVisibleIndex;
            set
            {
                ColumnBase objB = null;
                objB = ((value <= -1) || (value >= this.VisibleColumnsCore.Count)) ? this.VisibleColumnsCore.FirstOrDefault<ColumnBase>() : this.VisibleColumnsCore[value];
                if ((this.NavigationIndex != value) || !ReferenceEquals(this.DataControl.CurrentColumn, objB))
                {
                    this.DataControl.CurrentColumn = objB;
                }
            }
        }

        internal virtual bool IsDesignTimeAdornerPanelLeftAligned =>
            false;

        protected internal IDesignTimeAdornerBase DesignTimeAdorner =>
            (this.DataControl != null) ? this.DataControl.DesignTimeAdorner : EmptyDesignTimeAdornerBase.Instance;

        internal BarManagerMenuController ColumnMenuController =>
            this.columnMenuControllerValue.Value;

        internal BarManagerMenuController TotalSummaryMenuController =>
            this.totalSummaryMenuControllerValue.Value;

        internal BarManagerMenuController RowCellMenuController =>
            this.rowCellMenuControllerValue.Value;

        [Description(""), Browsable(false)]
        public BarManagerActionCollection ColumnMenuCustomizations =>
            this.ColumnMenuController.ActionContainer.Actions;

        [Description(""), Browsable(false)]
        public BarManagerActionCollection TotalSummaryMenuCustomizations =>
            this.TotalSummaryMenuController.ActionContainer.Actions;

        [Description(""), Browsable(false)]
        public BarManagerActionCollection RowCellMenuCustomizations =>
            this.RowCellMenuController.ActionContainer.Actions;

        internal BarManagerMenuController CompactModeColumnsController =>
            this.compactModeColumnsControllerValue.Value;

        internal BarManagerMenuController CompactModeFilterMenuController =>
            this.compactModeFilterMenuControllerValue.Value;

        internal BarManagerMenuController CompactModeMergeMenuController =>
            this.compactModeMergeMenuControllerValue.Value;

        [Description("Gets the context menu currently being displayed within a View.")]
        public DataControlPopupMenu DataControlMenu
        {
            get
            {
                if (this.dataControlMenu == null)
                {
                    this.dataControlMenu = this.CreatePopupMenu();
                    if (this.initDataControlMenuWhenCreated)
                    {
                        this.dataControlMenu.Init();
                    }
                    AutomationProperties.SetAutomationId(this.dataControlMenu, "GridContextMenu");
                }
                return this.dataControlMenu;
            }
        }

        internal int PageVisibleDataRowCount =>
            (this.RootNodeContainer.CurrentLevelItemCount - this.ActualFixedTopRowsCount) - this.ActualFixedBottomRowsCount;

        internal int PageVisibleTopRowIndex =>
            (!this.IsPagingMode || (this.DataProviderBase == null)) ? (this.RootNodeContainer.StartScrollIndex + this.ActualFixedTopRowsCount) : this.DataProviderBase.ConvertScrollIndexToVisibleIndex(this.RootNodeContainer.StartScrollIndex, false);

        protected internal bool IsLockUpdateColumnsLayout =>
            this.updateColumnsLayoutLocker.IsLocked;

        protected override IEnumerator LogicalChildren =>
            this.logicalChildren.GetEnumerator();

        internal bool IsNewItemRowVisible =>
            this.ViewBehavior.IsNewItemRowVisible;

        protected internal virtual DispatcherTimer ScrollTimer =>
            this.ViewBehavior.ScrollTimer;

        protected internal virtual bool IsAdditionalRowFocused =>
            this.ViewBehavior.IsAdditionalRow(this.FocusedRowHandle);

        protected internal virtual bool IsAutoFilterRowFocused =>
            this.ViewBehavior.IsAutoFilterRowFocused;

        protected internal bool IsContextMenuOpened =>
            (this.dataControlMenu != null) && this.dataControlMenu.IsOpen;

        internal bool UpdateActionEnqueued { get; set; }

        protected internal virtual bool NeedCellsWidthUpdateOnScrolling =>
            false;

        private bool IsUpdateVisibleIndexesLocked =>
            this.UpdateVisibleIndexesLocker.IsLocked || this.GetOriginationViewUpdateVisibleIndexesLocked();

        internal bool IsEditorOpen { get; private set; }

        internal bool IsUpdateCellDataEnqueued { get; private set; }

        internal DevExpress.Xpf.Grid.FocusRectPresenter FocusRectPresenter { get; set; }

        internal bool IsMultiSelection =>
            this.GetActualSelectionMode() != MultiSelectMode.None;

        internal bool IsMultiCellSelection =>
            this.GetActualSelectionMode() == MultiSelectMode.Cell;

        internal bool IsMultiRowSelection =>
            (this.GetActualSelectionMode() == MultiSelectMode.Row) || (this.GetActualSelectionMode() == MultiSelectMode.MultipleRow);

        internal virtual bool GetAllowGroupSummaryCascadeUpdate =>
            false;

        protected internal bool ActualAllowCellMerge
        {
            get => 
                this.actualAllowCellMerge;
            set
            {
                if (this.actualAllowCellMerge != value)
                {
                    this.actualAllowCellMerge = value;
                    this.OnActualAllowCellMergeChanged();
                }
            }
        }

        internal DependencyObject CurrentCell
        {
            get => 
                this.currentCell;
            set
            {
                if (!ReferenceEquals(this.CurrentCell, value))
                {
                    this.currentCell = value;
                    this.OnCurrentCellChanged();
                }
            }
        }

        [Description("Gets or sets the search control. This is a dependency property."), Category("Search Panel"), CloneDetailMode(CloneDetailMode.Skip), Browsable(false)]
        public DevExpress.Xpf.Editors.SearchControl SearchControl
        {
            get => 
                (DevExpress.Xpf.Editors.SearchControl) base.GetValue(SearchControlProperty);
            set => 
                base.SetValue(SearchControlProperty, value);
        }

        [Description("Gets or sets the text displayed within the search panel edit box when the search text is null. This is a dependency property."), Category("Search Panel"), CloneDetailMode(CloneDetailMode.Skip)]
        public string SearchPanelNullText
        {
            get => 
                (string) base.GetValue(SearchPanelNullTextProperty);
            set => 
                base.SetValue(SearchPanelNullTextProperty, value);
        }

        [Description("Gets or sets whether to show navigation buttons in the grid's search panel."), Category("Search Panel"), CloneDetailMode(CloneDetailMode.Skip)]
        public bool ShowSearchPanelNavigationButtons
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelNavigationButtonsProperty);
            set => 
                base.SetValue(ShowSearchPanelNavigationButtonsProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public bool ActualShowSearchPanelNavigationButtons
        {
            get => 
                (bool) base.GetValue(ActualShowSearchPanelNavigationButtonsProperty);
            private set => 
                base.SetValue(ActualShowSearchPanelNavigationButtonsPropertyKey, value);
        }

        [Description("Gets or sets whether to show the information about search results in the searh panel."), Category("Search Panel"), CloneDetailMode(CloneDetailMode.Skip)]
        public bool ShowSearchPanelResultInfo
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelResultInfoProperty);
            set => 
                base.SetValue(ShowSearchPanelResultInfoProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), CloneDetailMode(CloneDetailMode.Skip)]
        public bool ActualShowSearchPanelResultInfo
        {
            get => 
                (bool) base.GetValue(ActualShowSearchPanelResultInfoProperty);
            internal set => 
                base.SetValue(ActualShowSearchPanelResultInfoPropertyKey, value);
        }

        protected virtual bool CanNavigateToNewItemRow =>
            false;

        protected virtual bool ShouldChangeRowByTab =>
            this.IsExpandableRowFocused();

        internal bool PostponedNavigationInProgress
        {
            get => 
                this.postponedNavigationInProgress;
            private set => 
                this.postponedNavigationInProgress = value;
        }

        protected internal SizeHelperBase SizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.OrientationCore);

        internal bool ActualUseLegacyFilterEditor
        {
            get
            {
                bool? useLegacyFilterEditor = this.UseLegacyFilterEditor;
                return ((useLegacyFilterEditor != null) ? useLegacyFilterEditor.GetValueOrDefault() : CompatibilitySettings.UseLegacyFilterEditor);
            }
        }

        protected bool IsVirtualSource =>
            (this.DataControl != null) && this.DataControl.DataProviderBase.IsVirtualSource;

        internal bool IsFilterControlOpened =>
            this.FilterControlContainer != null;

        internal double FixedExtent =>
            this.ViewBehavior.GetFixedExtent();

        internal double FixedViewport =>
            this.ViewBehavior.HorizontalViewportCore;

        bool IColumnOwnerBase.AllowGrouping =>
            this.AllowGroupingCore;

        protected internal virtual DevExpress.Xpf.Grid.ScrollingMode ScrollingModeCore =>
            this.ScrollingMode;

        internal virtual bool AllowGroupingCore =>
            false;

        ActualTemplateSelectorWrapper IColumnOwnerBase.ActualGroupValueTemplateSelector =>
            this.ActualGroupValueTemplateSelectorCore;

        internal virtual ActualTemplateSelectorWrapper ActualGroupValueTemplateSelectorCore =>
            null;

        Style IColumnOwnerBase.AutoFilterRowCellStyle =>
            this.ViewBehavior.AutoFilterRowCellStyle;

        Style IColumnOwnerBase.NewItemRowCellStyle =>
            this.GetNewItemRowCellStyle;

        internal virtual Style GetNewItemRowCellStyle =>
            null;

        bool IColumnOwnerBase.AllowColumnsResizing =>
            this.ViewBehavior.AllowColumnResizingCore;

        bool IColumnOwnerBase.AllowSorting =>
            this.AllowSorting && !this.IsEditFormVisible;

        bool IColumnOwnerBase.AllowColumnMoving =>
            this.AllowColumnMoving;

        bool IColumnOwnerBase.AllowEditing =>
            this.AllowEditing;

        bool IColumnOwnerBase.AllowColumnFiltering =>
            this.AllowColumnFiltering && !this.IsEditFormVisible;

        bool IColumnOwnerBase.AllowResizing =>
            this.ViewBehavior.AllowResizingCore;

        bool IColumnOwnerBase.UpdateAllowResizingOnWidthChanging =>
            this.ViewBehavior.UpdateAllowResizingOnWidthChanging;

        bool IColumnOwnerBase.AutoWidth =>
            this.ViewBehavior.AutoWidthCore;

        IList<ColumnBase> IColumnOwnerBase.VisibleColumns =>
            this.VisibleColumnsCore;

        bool IColumnOwnerBase.LockEditorClose
        {
            get => 
                this.LockEditorClose;
            set => 
                this.LockEditorClose = value;
        }

        bool IColumnOwnerBase.ShowAllTableValuesInFilterPopup =>
            (this.DataControl != null) && this.DataControl.ShowAllTableValuesInFilterPopup;

        bool IColumnOwnerBase.ShowAllTableValuesInCheckedFilterPopup =>
            (this.DataControl == null) || this.DataControl.ShowAllTableValuesInCheckedFilterPopup;

        internal virtual bool AllowMasterDetailCore =>
            false;

        protected internal virtual bool ShowGroupPanelsForUngroupedDetailsCore =>
            true;

        internal DataViewBase OriginationView { get; private set; }

        internal DataViewBase EventTargetView =>
            this.OriginationView ?? this;

        internal DataViewBase RootView =>
            (this.DataControl != null) ? (((this.OriginationView != null) || !ReferenceEquals(this.DataControl.DataControlOwner, NullDataControlOwner.Instance)) ? this.DataControl.GetRootDataControl().DataView : this) : this;

        internal bool HasClonedDetails =>
            !this.IsRootView && ((this.DataControl != null) && this.DataControl.DetailClones.Any<DataControlBase>());

        internal bool HasClonedExpandedDetails
        {
            get
            {
                if (this.IsRootView || (this.DataControl == null))
                {
                    return false;
                }
                bool result = false;
                Func<DataControlBase, DataControlBase> getTarget = <>c.<>9__1755_0;
                if (<>c.<>9__1755_0 == null)
                {
                    Func<DataControlBase, DataControlBase> local1 = <>c.<>9__1755_0;
                    getTarget = <>c.<>9__1755_0 = grid => grid;
                }
                DataControlOriginationElementHelper.EnumerateDependentElements<DataControlBase>(this.DataControl, getTarget, delegate (DataControlBase grid) {
                    result = true;
                }, null);
                return result;
            }
        }

        public bool IsRootView =>
            ReferenceEquals(this.RootView, this);

        public DataViewBase FocusedView =>
            this.MasterRootRowsContainer.FocusedView;

        internal GridControlColumnProviderBase SearchPanelColumnProvider =>
            GridControlColumnProviderBase.GetColumnProvider(this.DataControl);

        internal bool PostponedSearchControlFocus
        {
            get => 
                this.postponedSearchControlFocus;
            set => 
                this.postponedSearchControlFocus = value;
        }

        internal IList<string> SearchControlMru
        {
            get => 
                this.searchControlMru;
            set => 
                this.searchControlMru = value;
        }

        internal DevExpress.Xpf.Editors.SearchControl SearchControlCore { get; private set; }

        protected internal virtual bool ShowGroupSummaryFooter =>
            false;

        protected internal virtual bool ShouldDisplayBottomRow =>
            false;

        protected internal virtual bool ShouldDisplayLoadingRow =>
            false;

        public bool IsEditFormVisible =>
            this.EditFormManager.IsEditFormVisible;

        protected internal bool IsNewItemRowFocused =>
            this.IsNewItemRowHandle(this.FocusedRowHandle);

        protected internal bool IsTopNewItemRowFocused =>
            this.IsNewItemRowFocused && !this.ShouldDisplayBottomRow;

        protected internal bool IsBottomNewItemRowFocused =>
            this.IsNewItemRowFocused && this.ShouldDisplayBottomRow;

        internal IEditFormManager EditFormManager
        {
            get
            {
                this.editFormManagerCore ??= this.CreateEditFormManager();
                return this.editFormManagerCore;
            }
        }

        protected internal virtual bool ShouldUpdateCellData =>
            (this.DataControl != null) && !this.DataControl.DataSourceChangingLocker.IsLocked;

        bool IRightToLeftSupport.RightToLeftLayout =>
            base.FlowDirection == FlowDirection.RightToLeft;

        bool IPrintableControl.CanCreateRootNodeAsync =>
            this.GetCanCreateRootNodeAsync();

        internal Border SelectionRectangle
        {
            get => 
                this.RootView.selectionRactangle;
            set
            {
                if (this.RootView.selectionRactangle != null)
                {
                    this.RootView.RemoveChild(this.selectionRactangle);
                    if (this.RootView.SelectionRectanglAdorner != null)
                    {
                        AdornerLayer adornerLayerTopContainer = this.GetAdornerLayerTopContainer();
                        if (adornerLayerTopContainer != null)
                        {
                            adornerLayerTopContainer.Remove(this.RootView.SelectionRectanglAdorner);
                        }
                    }
                }
                this.RootView.selectionRactangle = value;
                if (this.RootView.selectionRactangle == null)
                {
                    this.RootView.SelectionRectanglAdorner = null;
                }
                else
                {
                    this.RootView.AddChild(this.RootView.selectionRactangle);
                    AdornerLayer adornerLayerTopContainer = this.GetAdornerLayerTopContainer();
                    if (adornerLayerTopContainer != null)
                    {
                        this.RootView.SelectionRectanglAdorner = new PositionedAdornerContainer(this.RootView, this.RootView.selectionRactangle);
                        adornerLayerTopContainer.Add(this.RootView.SelectionRectanglAdorner);
                    }
                }
            }
        }

        internal PositionedAdornerContainer SelectionRectanglAdorner { get; private set; }

        internal DevExpress.Xpf.Grid.IncrementalSearchMode IncrementalSearchModeCore { get; private set; }

        public DevExpress.Xpf.Grid.IncrementalSearchMode IncrementalSearchMode
        {
            get => 
                (DevExpress.Xpf.Grid.IncrementalSearchMode) base.GetValue(IncrementalSearchModeProperty);
            set => 
                base.SetValue(IncrementalSearchModeProperty, value);
        }

        public bool UseOnlyCurrentColumnInIncrementalSearch
        {
            get => 
                (bool) base.GetValue(UseOnlyCurrentColumnInIncrementalSearchProperty);
            set => 
                base.SetValue(UseOnlyCurrentColumnInIncrementalSearchProperty, value);
        }

        public int? IncrementalSearchClearDelay
        {
            get => 
                (int?) base.GetValue(IncrementalSearchClearDelayProperty);
            set => 
                base.SetValue(IncrementalSearchClearDelayProperty, value);
        }

        internal string[] IncrementalSearchColumns
        {
            get
            {
                if (!this.CanStartIncrementalSearch)
                {
                    return new string[0];
                }
                List<string> list = new List<string>();
                if (this.UseOnlyCurrentColumnInIncrementalSearch && (this.NavigationStyle != GridViewNavigationStyle.Row))
                {
                    string str = ((this.DataControl.CurrentColumn == null) || !this.DataControl.CurrentColumn.AllowIncrementalSearch) ? null : this.DataControl.CurrentColumn.FieldName;
                    if (!string.IsNullOrEmpty(str))
                    {
                        list.Add(str);
                    }
                }
                else
                {
                    foreach (ColumnBase base2 in this.VisibleColumnsCore)
                    {
                        if (base2.AllowIncrementalSearch)
                        {
                            list.Add(base2.FieldName);
                        }
                    }
                }
                return list.ToArray();
            }
        }

        protected internal bool CanStartIncrementalSearch =>
            (this.ActualIncrementalSearchMode != DevExpress.Xpf.Grid.IncrementalSearchMode.Default) && ((this.ActualIncrementalSearchMode != DevExpress.Xpf.Grid.IncrementalSearchMode.Disabled) && ((this.NavigationStyle != GridViewNavigationStyle.None) && (!this.IsPagingMode && (((this.DataProviderBase == null) || (!this.DataProviderBase.IsAsyncServerMode && (!this.DataProviderBase.IsServerMode && !this.IsVirtualSource))) && this.CheckStartIncrementalSearch(this.ActualIncrementalSearchMode)))));

        protected DevExpress.Xpf.Grid.IncrementalSearchMode ActualIncrementalSearchMode
        {
            get
            {
                if (this.IsRootView)
                {
                    return this.IncrementalSearchModeCore;
                }
                if (this.IncrementalSearchMode != DevExpress.Xpf.Grid.IncrementalSearchMode.Default)
                {
                    return this.IncrementalSearchMode;
                }
                DataControlBase masterGridCore = this.DataControl?.GetMasterGridCore();
                return (((masterGridCore == null) || (masterGridCore.DataView == null)) ? DevExpress.Xpf.Grid.IncrementalSearchMode.Default : masterGridCore.DataView.ActualIncrementalSearchMode);
            }
        }

        protected TableTextSearchEngine TextSearchEngineRoot
        {
            get => 
                (this.RootView != null) ? this.RootView.textSearchEngineRoot : null;
            set
            {
                if (this.RootView != null)
                {
                    this.RootView.textSearchEngineRoot = value;
                }
            }
        }

        internal DevExpress.Xpf.Grid.ErrorsWatchMode ErrorsWatchModeCore { get; private set; }

        internal virtual int? OldChangedRowHandle { get; set; }

        internal DevExpress.Xpf.Grid.ErrorWatch ErrorWatch
        {
            get
            {
                this._errorWatch ??= new DevExpress.Xpf.Grid.ErrorWatch(this);
                return this._errorWatch;
            }
        }

        internal virtual string GroupRowCheckBoxFieldNameCore =>
            null;

        protected internal virtual bool CanUseVisibleIndicesForBestFit =>
            false;

        protected internal virtual bool ShouldBestFitCollapsedRows =>
            false;

        protected internal int ActualFixedTopRowsCount { get; set; }

        protected internal int ActualFixedBottomRowsCount { get; set; }

        protected internal virtual bool HasFixedRows =>
            false;

        protected internal virtual bool ExtendRowMarginControlHeight =>
            false;

        internal bool CanShowDetailColumnHeadersControl =>
            this.ViewBehavior.CanShowDetailColumnHeadersControl;

        [Description("Gets whether the compact panel is displayed within a view."), Category("Appearance "), XtraSerializableProperty]
        public bool ActualShowCompactPanel
        {
            get => 
                (bool) base.GetValue(ActualShowCompactPanelProperty);
            protected internal set => 
                base.SetValue(ActualShowCompactPanelPropertyKey, value);
        }

        protected internal virtual bool IsPagingMode =>
            false;

        protected internal virtual int ItemsOnPage =>
            0;

        protected internal virtual int PageOffset =>
            0;

        protected internal virtual int LastVisibleIndexOnPage =>
            0;

        protected internal virtual int FirstVisibleIndexOnPage =>
            0;

        protected internal bool ShowLoadingRow
        {
            get => 
                this.showLoadingRowCore;
            set
            {
                if (this.ShowLoadingRow != value)
                {
                    this.showLoadingRowCore = value;
                    this.OnShowLoadingRowChanged();
                }
            }
        }

        public bool AllowDragDrop
        {
            get => 
                (bool) base.GetValue(AllowDragDropProperty);
            set => 
                base.SetValue(AllowDragDropProperty, value);
        }

        public bool AutoExpandOnDrag
        {
            get => 
                (bool) base.GetValue(AutoExpandOnDragProperty);
            set => 
                base.SetValue(AutoExpandOnDragProperty, value);
        }

        public int AutoExpandDelayOnDrag
        {
            get => 
                (int) base.GetValue(AutoExpandDelayOnDragProperty);
            set => 
                base.SetValue(AutoExpandDelayOnDragProperty, value);
        }

        public bool AllowSortedDataDragDrop
        {
            get => 
                (bool) base.GetValue(AllowSortedDataDragDropProperty);
            set => 
                base.SetValue(AllowSortedDataDragDropProperty, value);
        }

        public bool ShowDragDropHint
        {
            get => 
                (bool) base.GetValue(ShowDragDropHintProperty);
            set => 
                base.SetValue(ShowDragDropHintProperty, value);
        }

        public bool ShowTargetInfoInDragDropHint
        {
            get => 
                (bool) base.GetValue(ShowTargetInfoInDragDropHintProperty);
            set => 
                base.SetValue(ShowTargetInfoInDragDropHintProperty, value);
        }

        public DataTemplate DropMarkerTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DropMarkerTemplateProperty);
            set => 
                base.SetValue(DropMarkerTemplateProperty, value);
        }

        public DataTemplate DragDropHintTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DragDropHintTemplateProperty);
            set => 
                base.SetValue(DragDropHintTemplateProperty, value);
        }

        public bool AllowScrollingOnDrag
        {
            get => 
                (bool) base.GetValue(AllowScrollingOnDragProperty);
            set => 
                base.SetValue(AllowScrollingOnDragProperty, value);
        }

        internal NativeDragManager DragDropManager { get; private set; }

        private bool AllowDragDropOnRootView =>
            this.RootView.AllowDragDrop;

        internal Locker CanSelectLocker =>
            this.RootView.canSelectLocker;

        protected internal bool AllowDropRecordToItself { get; set; }

        protected internal virtual ShowUpdateRowButtons ShowUpdateRowButtonsCore =>
            ShowUpdateRowButtons.Never;

        internal int UpdateButtonsModeAllowRequestUI { get; set; }

        protected internal virtual DevExpress.Xpf.Grid.Native.UpdateRowRectangleHelper UpdateRowRectangleHelper
        {
            get
            {
                this._updateRowRectangleHelper ??= new DevExpress.Xpf.Grid.Native.UpdateRowRectangleHelper();
                return this._updateRowRectangleHelper;
            }
        }

        internal PositionedAdornerContainer UpdateRowRectangleAdorner { get; private set; }

        internal System.Windows.Shapes.Path UpdateRowRectangle
        {
            get => 
                this.RootView.updateRowRectangle;
            set
            {
                if (this.RootView.IsLoaded)
                {
                    if (this.RootView.updateRowRectangle != null)
                    {
                        this.RootView.RemoveChild(this.RootView.updateRowRectangle);
                        if (this.RootView.UpdateRowRectangleAdorner != null)
                        {
                            AdornerLayer adornerLayerTopContainer = this.GetAdornerLayerTopContainer();
                            if (adornerLayerTopContainer != null)
                            {
                                adornerLayerTopContainer.Remove(this.RootView.UpdateRowRectangleAdorner);
                            }
                        }
                    }
                    if (value == null)
                    {
                        this.RootView.updateRowRectangle = null;
                        this.RootView.UpdateRowRectangleAdorner = null;
                    }
                    else
                    {
                        AdornerLayer adornerLayerTopContainer = this.GetAdornerLayerTopContainer();
                        if ((adornerLayerTopContainer == null) || !adornerLayerTopContainer.IsVisible)
                        {
                            this.RootView.updateRowRectangle = null;
                            this.RootView.UpdateRowRectangleAdorner = null;
                        }
                        else
                        {
                            this.RootView.updateRowRectangle = value;
                            this.RootView.AddChild(this.RootView.updateRowRectangle);
                            this.RootView.UpdateRowRectangleAdorner = new PositionedAdornerContainer(this.RootView, this.RootView.updateRowRectangle);
                            adornerLayerTopContainer.Add(this.RootView.UpdateRowRectangleAdorner);
                        }
                    }
                }
            }
        }

        protected internal virtual double MaxAvailableWidth =>
            double.PositiveInfinity;

        protected internal virtual double ActualColumnsWidth =>
            0.0;

        protected internal virtual double AdditionalElementWidth
        {
            get => 
                0.0;
            set
            {
            }
        }

        protected internal virtual double AdditionalElementOffset =>
            0.0;

        protected internal virtual double ActualAdditionalElementOffset =>
            0.0;

        internal IDataUpdateAnimationProvider DataUpdateAnimationProvider
        {
            get
            {
                this.dataUpdateAnimationProviderCore ??= this.CreateDataUpdateAnimationProvider();
                return this.dataUpdateAnimationProviderCore;
            }
            set => 
                this.dataUpdateAnimationProviderCore = value;
        }

        internal bool ShouldListenToItemsSourceErrors
        {
            get
            {
                ITableView view = this as ITableView;
                return ((view != null) ? (((view.ScrollBarAnnotationMode == null) || (!view.ScrollBarAnnotationMode.Value.HasFlag(ScrollBarAnnotationMode.InvalidCells) && !view.ScrollBarAnnotationMode.Value.HasFlag(ScrollBarAnnotationMode.InvalidRows))) ? (this.ErrorsWatchMode.HasFlag(DevExpress.Xpf.Grid.ErrorsWatchMode.Cells) || this.ErrorsWatchMode.HasFlag(DevExpress.Xpf.Grid.ErrorsWatchMode.Rows)) : true) : false);
            }
        }

        internal virtual bool ActualAllowCountTotalSummary =>
            true;

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataViewBase.<>c <>9 = new DataViewBase.<>c();
            public static Action<ColumnBase> <>9__214_19;
            public static Action<ColumnBase> <>9__214_21;
            public static ExecutedRoutedEventHandler <>9__226_0;
            public static CanExecuteRoutedEventHandler <>9__226_1;
            public static ExecutedRoutedEventHandler <>9__226_2;
            public static CanExecuteRoutedEventHandler <>9__226_3;
            public static ExecutedRoutedEventHandler <>9__226_4;
            public static CanExecuteRoutedEventHandler <>9__226_5;
            public static ExecutedRoutedEventHandler <>9__226_6;
            public static CanExecuteRoutedEventHandler <>9__226_7;
            public static ExecutedRoutedEventHandler <>9__226_8;
            public static CanExecuteRoutedEventHandler <>9__226_9;
            public static ExecutedRoutedEventHandler <>9__226_10;
            public static CanExecuteRoutedEventHandler <>9__226_11;
            public static ExecutedRoutedEventHandler <>9__226_12;
            public static CanExecuteRoutedEventHandler <>9__226_13;
            public static ExecutedRoutedEventHandler <>9__226_14;
            public static CanExecuteRoutedEventHandler <>9__226_15;
            public static ExecutedRoutedEventHandler <>9__226_16;
            public static CanExecuteRoutedEventHandler <>9__226_17;
            public static ExecutedRoutedEventHandler <>9__226_19;
            public static CanExecuteRoutedEventHandler <>9__226_20;
            public static ExecutedRoutedEventHandler <>9__226_22;
            public static CanExecuteRoutedEventHandler <>9__226_23;
            public static ExecutedRoutedEventHandler <>9__226_25;
            public static CanExecuteRoutedEventHandler <>9__226_26;
            public static ExecutedRoutedEventHandler <>9__226_28;
            public static CanExecuteRoutedEventHandler <>9__226_29;
            public static ExecutedRoutedEventHandler <>9__226_31;
            public static CanExecuteRoutedEventHandler <>9__226_32;
            public static ExecutedRoutedEventHandler <>9__226_34;
            public static CanExecuteRoutedEventHandler <>9__226_35;
            public static ExecutedRoutedEventHandler <>9__226_36;
            public static CanExecuteRoutedEventHandler <>9__226_37;
            public static ExecutedRoutedEventHandler <>9__226_39;
            public static CanExecuteRoutedEventHandler <>9__226_40;
            public static ExecutedRoutedEventHandler <>9__226_42;
            public static CanExecuteRoutedEventHandler <>9__226_43;
            public static ExecutedRoutedEventHandler <>9__226_44;
            public static CanExecuteRoutedEventHandler <>9__226_45;
            public static ExecutedRoutedEventHandler <>9__226_46;
            public static CanExecuteRoutedEventHandler <>9__226_47;
            public static Func<DataControlBase, bool> <>9__940_0;
            public static Func<bool> <>9__940_1;
            public static Func<ColumnBase, bool> <>9__948_0;
            public static Action<DataControlBase> <>9__1164_0;
            public static UpdateRowDataDelegate <>9__1226_1;
            public static Action<DataControlBase> <>9__1226_0;
            public static Predicate<DependencyObject> <>9__1356_0;
            public static UpdateRowDataDelegate <>9__1360_0;
            public static UpdateRowDataDelegate <>9__1361_0;
            public static UpdateRowDataDelegate <>9__1362_0;
            public static UpdateRowDataDelegate <>9__1363_0;
            public static UpdateRowDataDelegate <>9__1364_0;
            public static UpdateRowDataDelegate <>9__1365_0;
            public static UpdateRowDataDelegate <>9__1367_0;
            public static Func<ColumnBase, string> <>9__1368_1;
            public static UpdateRowDataDelegate <>9__1370_0;
            public static Action<ColumnBase> <>9__1384_0;
            public static Action<ColumnBase> <>9__1385_0;
            public static Func<FormatConditionFilter, bool> <>9__1389_1;
            public static Func<FormatConditionFilterInfo, CriteriaOperator> <>9__1389_2;
            public static UpdateRowDataDelegate <>9__1395_0;
            public static Action<ColumnBase> <>9__1398_0;
            public static UpdateRowDataDelegate <>9__1399_0;
            public static Action<ColumnBase> <>9__1401_0;
            public static Action<ColumnBase> <>9__1402_0;
            public static Action<ColumnBase> <>9__1403_0;
            public static Func<DataViewBase, bool> <>9__1420_0;
            public static Func<ITableViewHitInfo, bool> <>9__1453_1;
            public static Func<bool> <>9__1453_2;
            public static Func<DataControlBase, Func<MouseEventArgs, bool>> <>9__1453_3;
            public static Predicate<DependencyObject> <>9__1463_0;
            public static Func<HierarchyPanel, double> <>9__1529_0;
            public static Func<double> <>9__1529_1;
            public static Func<HierarchyPanel, double> <>9__1547_0;
            public static Func<double> <>9__1547_1;
            public static Func<HierarchyPanel, double> <>9__1548_0;
            public static Func<double> <>9__1548_1;
            public static Action<ColumnBase> <>9__1686_0;
            public static Action<BandBase> <>9__1686_1;
            public static UpdateRowDataDelegate <>9__1697_0;
            public static Action<ColumnBase> <>9__1729_0;
            public static Func<DataControlBase, DataControlBase> <>9__1755_0;
            public static Func<DataControlBase, DependencyObject> <>9__1761_0;
            public static Func<DataControlBase, DataViewBase> <>9__1764_1;
            public static Action<DataControlBase> <>9__2041_0;
            public static Action<DataControlBase> <>9__2042_0;
            public static Action<DataControlBase> <>9__2042_1;
            public static UpdateRowDataDelegate <>9__2144_0;
            public static Action<NativeDragManager> <>9__2206_0;
            public static UpdateRowDataDelegate <>9__2217_0;
            public static UpdateRowDataDelegate <>9__2219_0;
            public static UpdateRowDataDelegate <>9__2235_0;
            public static Action<IScrollInfo> <>9__2298_0;
            public static Action<IScrollInfo> <>9__2299_0;
            public static Action<IScrollInfo> <>9__2300_0;
            public static Action<IScrollInfo> <>9__2301_0;

            internal void <.cctor>b__214_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateRowDataFocusWithinState();
            }

            internal void <.cctor>b__214_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnColumnChooserColumnDisplayModeChanged();
            }

            internal void <.cctor>b__214_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnFocusedRowChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__214_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnIsSynchronizedWithCurrentItemChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__214_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnColumnChooserFactoryChanged();
            }

            internal object <.cctor>b__214_13(DependencyObject d, object baseValue) => 
                ((DataViewBase) d).CoerceColumnChooserFactory((IColumnChooserFactory) baseValue);

            internal void <.cctor>b__214_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector();
            }

            internal void <.cctor>b__214_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector();
            }

            internal void <.cctor>b__214_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector();
            }

            internal void <.cctor>b__214_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualCellTemplateSelector();
            }

            internal void <.cctor>b__214_18(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ColumnBase> updateColumnDelegate = <>9__214_19;
                if (<>9__214_19 == null)
                {
                    Action<ColumnBase> local1 = <>9__214_19;
                    updateColumnDelegate = <>9__214_19 = x => x.UpdateActualCellEditTemplateSelector();
                }
                ((DataViewBase) d).UpdateColumns(updateColumnDelegate);
            }

            internal void <.cctor>b__214_19(ColumnBase x)
            {
                x.UpdateActualCellEditTemplateSelector();
            }

            internal void <.cctor>b__214_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateActualColumnChooserTemplate();
            }

            internal void <.cctor>b__214_20(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ColumnBase> updateColumnDelegate = <>9__214_21;
                if (<>9__214_21 == null)
                {
                    Action<ColumnBase> local1 = <>9__214_21;
                    updateColumnDelegate = <>9__214_21 = x => x.UpdateActualCellEditTemplateSelector();
                }
                ((DataViewBase) d).UpdateColumns(updateColumnDelegate);
            }

            internal void <.cctor>b__214_21(ColumnBase x)
            {
                x.UpdateActualCellEditTemplateSelector();
            }

            internal void <.cctor>b__214_22(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).AllowColumnMoving = (bool) e.NewValue;
            }

            internal void <.cctor>b__214_23(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateEditorButtonVisibilities();
                ((DataViewBase) d).RaiseColumnAllowEditingChanged();
            }

            internal void <.cctor>b__214_24(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).AllowFilterEditorChanged();
            }

            internal void <.cctor>b__214_25(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualHeaderTemplateSelector();
            }

            internal void <.cctor>b__214_26(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualHeaderTemplateSelector();
            }

            internal void <.cctor>b__214_27(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualHeaderCustomizationAreaTemplateSelector();
            }

            internal void <.cctor>b__214_28(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsActualHeaderCustomizationAreaTemplateSelector();
            }

            internal void <.cctor>b__214_29(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnShowTotalSummaryChanged();
            }

            internal void <.cctor>b__214_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateActualColumnChooserTemplate();
            }

            internal void <.cctor>b__214_30(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).InvalidateParentTree();
            }

            internal void <.cctor>b__214_31(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateFilterPanel();
            }

            internal void <.cctor>b__214_32(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnIsEditingChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__214_33(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateActualTotalSummaryItemTemplateSelector();
            }

            internal void <.cctor>b__214_34(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateActualTotalSummaryItemTemplateSelector();
            }

            internal void <.cctor>b__214_35(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnHeaderImageStyle();
            }

            internal void <.cctor>b__214_36(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnTopRowIndexChanged();
            }

            internal void <.cctor>b__214_37(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                d.CoerceValue(DataViewBase.WheelScrollLinesProperty);
            }

            internal void <.cctor>b__214_38(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnColumnFilterPopupModeChanged();
            }

            internal void <.cctor>b__214_39(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).RefreshImmediateUpdateRowPositionProperty();
            }

            internal void <.cctor>b__214_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnIsColumnChooserVisibleChanged();
            }

            internal void <.cctor>b__214_40(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateSummariesIgnoreNullValues();
            }

            internal void <.cctor>b__214_41(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).RebuildColumnChooserColumns();
            }

            internal void <.cctor>b__214_42(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnHeadersToolTipTemplate();
            }

            internal void <.cctor>b__214_43(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).InvalidateParentTree();
            }

            internal void <.cctor>b__214_44(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnChooserCaption();
            }

            internal void <.cctor>b__214_45(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnItemsSourceErrorInfoShowModeChanged();
            }

            internal void <.cctor>b__214_46(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnSelectedRowsSourceChanged();
            }

            internal void <.cctor>b__214_47(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnSummaryDataChanged();
            }

            internal void <.cctor>b__214_48(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnSeachPanelCriteriaOperatorTypeChanged();
            }

            internal void <.cctor>b__214_49(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateSearchPanelText();
            }

            internal void <.cctor>b__214_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnActiveEditorChanged();
            }

            internal void <.cctor>b__214_50(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateCellDataErrors();
            }

            internal void <.cctor>b__214_51(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateShowSearchPanelNavigationButtons();
            }

            internal void <.cctor>b__214_52(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateShowSearchPanelResultInfo();
            }

            internal void <.cctor>b__214_53(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateFilterGrid();
            }

            internal void <.cctor>b__214_54(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateShowSearchPanelResultInfo();
            }

            internal void <.cctor>b__214_55(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).SearchColumnsChanged((string) e.NewValue);
            }

            internal void <.cctor>b__214_56(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateSearchPanelVisibility(true);
            }

            internal void <.cctor>b__214_57(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnActualShowSearchPanelChanged();
            }

            internal void <.cctor>b__214_58(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).SearchPanelHorizontalAlignmentChanged();
            }

            internal void <.cctor>b__214_59(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnSearchControlChanged((SearchControl) e.OldValue, (SearchControl) e.NewValue);
            }

            internal void <.cctor>b__214_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnIsKeyboardFocusWithinViewChanged();
            }

            internal void <.cctor>b__214_60(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).IncrementalSearchModeChanged();
            }

            internal void <.cctor>b__214_61(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnUseOnlyCurrentColumnInIncrementalSearchChanged();
            }

            internal void <.cctor>b__214_62(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).IncrementalSearchModeChanged();
            }

            internal void <.cctor>b__214_63(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).ErrorsWatchModeChanged();
            }

            internal void <.cctor>b__214_64(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdatCellToolTipTemplate();
            }

            internal void <.cctor>b__214_65(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnAllowDragDropChanged();
            }

            internal void <.cctor>b__214_66(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).RebuildDragManager();
            }

            internal void <.cctor>b__214_67(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).RebuildDragManager();
            }

            internal void <.cctor>b__214_68(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).EnableSelectedRowAppearanceChanged();
            }

            internal void <.cctor>b__214_69(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateVisibleGroupPanel();
            }

            internal void <.cctor>b__214_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnIsHorizontalScrollBarVisibleChanged();
            }

            internal void <.cctor>b__214_70(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnValidatesOnNotifyDataErrorsChanged();
            }

            internal void <.cctor>b__214_71(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnColumnHeaderStyleChanged();
            }

            internal void <.cctor>b__214_72(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnShowEmptyTextChanged();
            }

            internal void <.cctor>b__214_73(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnSummaryCalculationModeChanged();
            }

            internal void <.cctor>b__214_74(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnAreUpdateRowButtonsShownChanged();
            }

            internal void <.cctor>b__214_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                d.CoerceValue(DataViewBase.ScrollStepProperty);
            }

            internal void <.cctor>b__214_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnShowFocusedRectangleChanged();
            }

            internal void <AttachObservablePropertyScheme>b__2235_0(RowData rowData)
            {
                rowData.UpdateObservablePropertyScheme();
            }

            internal double <CalcOffsetByHeight>b__1548_0(HierarchyPanel x) => 
                x.FixedTopRowsHeight;

            internal double <CalcOffsetByHeight>b__1548_1() => 
                0.0;

            internal string <ClearEditorHighlightingText>b__1368_1(ColumnBase x) => 
                x.FieldName;

            internal void <DevExpress.Xpf.Grid.IColumnOwnerBase.CalcColumnsLayout>b__1686_0(ColumnBase column)
            {
                column.UpdateActualAllowResizing();
                column.UpdateActualAllowMoving();
            }

            internal void <DevExpress.Xpf.Grid.IColumnOwnerBase.CalcColumnsLayout>b__1686_1(BandBase b)
            {
                b.UpdateActualAllowResizing();
                b.UpdateActualAllowMoving();
            }

            internal void <DevExpress.Xpf.Grid.IColumnOwnerBase.UpdateCellDataValues>b__1697_0(RowData rowData)
            {
                rowData.UpdateRow();
                rowData.UpdateCellDataValues();
            }

            internal void <EnableSelectedRowAppearanceChanged>b__1395_0(RowData x)
            {
                x.UpdateSelectionState();
            }

            internal bool <FindNavigationIndex>b__1463_0(DependencyObject el) => 
                el is DataViewBase;

            internal DataControlBase <get_HasClonedExpandedDetails>b__1755_0(DataControlBase grid) => 
                grid;

            internal bool <get_PrintableColumns>b__948_0(ColumnBase column) => 
                column.AllowPrinting;

            internal bool <GetFilterOperatorCustomText>b__1389_1(FormatConditionFilter x) => 
                !x.Info.Type.IsTopBottomOrAverageOrUniqueDuplicate();

            internal CriteriaOperator <GetFilterOperatorCustomText>b__1389_2(FormatConditionFilterInfo filterInfo) => 
                null;

            internal double <GetItemVisibleSize>b__1547_0(HierarchyPanel x) => 
                x.FixedBottomRowsHeight;

            internal double <GetItemVisibleSize>b__1547_1() => 
                0.0;

            internal double <GetLastScrollRowViewAndVisibleIndex>b__1529_0(HierarchyPanel x) => 
                x.FixedTopRowsHeight;

            internal double <GetLastScrollRowViewAndVisibleIndex>b__1529_1() => 
                0.0;

            internal bool <IsDataViewDragDropManagerAttached>b__940_0(DataControlBase d) => 
                DataViewDragDropManagerHelper.GetIsAttached(d);

            internal bool <IsDataViewDragDropManagerAttached>b__940_1() => 
                false;

            internal void <MouseWheelDown>b__2298_0(IScrollInfo info)
            {
                info.MouseWheelDown();
            }

            internal void <MouseWheelLeft>b__2299_0(IScrollInfo info)
            {
                info.MouseWheelLeft();
            }

            internal void <MouseWheelRight>b__2300_0(IScrollInfo info)
            {
                info.MouseWheelRight();
            }

            internal void <MouseWheelUp>b__2301_0(IScrollInfo info)
            {
                info.MouseWheelUp();
            }

            internal void <OnNavigationStyleChanged>b__1164_0(DataControlBase dataControl)
            {
                if (dataControl.DataView != null)
                {
                    dataControl.DataView.OnNavigationStyleChanged();
                }
            }

            internal DependencyObject <OnPropertyChanged>b__1761_0(DataControlBase dataControl) => 
                dataControl.DataView;

            internal void <OnValidatesOnNotifyDataErrorsChanged>b__2217_0(RowData rowData)
            {
                rowData.UpatePropertyChangeSubscriptionMode();
                rowData.UpdateDataErrors(true);
            }

            internal void <ProcessTextSearch>b__2041_0(DataControlBase dataControl)
            {
                dataControl.DataView.UpdateEditorHighlightingText();
            }

            internal void <RebuildDragManager>b__2206_0(NativeDragManager x)
            {
                x.IsActive = false;
            }

            internal void <RegisterClassCommandBindings>b__226_0(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).ClearColumnFilter(e);
            }

            internal void <RegisterClassCommandBindings>b__226_1(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanClearColumnFilter(e);
            }

            internal void <RegisterClassCommandBindings>b__226_10(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MoveNextCell();
            }

            internal void <RegisterClassCommandBindings>b__226_11(object d, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = ((DataViewBase) d).CanMoveNextCell();
            }

            internal void <RegisterClassCommandBindings>b__226_12(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MoveFirstCell();
            }

            internal void <RegisterClassCommandBindings>b__226_13(object d, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = ((DataViewBase) d).CanMoveFirstCell();
            }

            internal void <RegisterClassCommandBindings>b__226_14(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MoveLastCell();
            }

            internal void <RegisterClassCommandBindings>b__226_15(object d, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = ((DataViewBase) d).CanMoveLastCell();
            }

            internal void <RegisterClassCommandBindings>b__226_16(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MoveFirstRow();
            }

            internal void <RegisterClassCommandBindings>b__226_17(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanPrevRow());
            }

            internal void <RegisterClassCommandBindings>b__226_19(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.MoveLastOrLastMasterRow();
            }

            internal void <RegisterClassCommandBindings>b__226_2(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).ShowFilterEditor(e);
            }

            internal void <RegisterClassCommandBindings>b__226_20(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanNextRow());
            }

            internal void <RegisterClassCommandBindings>b__226_22(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.Navigation.OnUp(false);
            }

            internal void <RegisterClassCommandBindings>b__226_23(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanPrevRow());
            }

            internal void <RegisterClassCommandBindings>b__226_25(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.Navigation.OnDown();
            }

            internal void <RegisterClassCommandBindings>b__226_26(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanNextRow());
            }

            internal void <RegisterClassCommandBindings>b__226_28(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.Navigation.OnPageUp();
            }

            internal void <RegisterClassCommandBindings>b__226_29(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanPrevRow());
            }

            internal void <RegisterClassCommandBindings>b__226_3(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanShowFilterEditor(e);
            }

            internal void <RegisterClassCommandBindings>b__226_31(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.Navigation.OnPageDown();
            }

            internal void <RegisterClassCommandBindings>b__226_32(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanNextRow());
            }

            internal void <RegisterClassCommandBindings>b__226_34(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).ClearFilter();
            }

            internal void <RegisterClassCommandBindings>b__226_35(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanClearFilter(e);
            }

            internal void <RegisterClassCommandBindings>b__226_36(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.DeleteFocusedRow();
            }

            internal void <RegisterClassCommandBindings>b__226_37(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanDeleteFocusedRow());
            }

            internal void <RegisterClassCommandBindings>b__226_39(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MasterRootRowsContainer.FocusedView.EditFocusedRow();
            }

            internal void <RegisterClassCommandBindings>b__226_4(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).ShowColumnChooser();
            }

            internal void <RegisterClassCommandBindings>b__226_40(object d, CanExecuteRoutedEventArgs e)
            {
                DataViewBase.CanExecuteWithCheckActualView(e, () => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanEditFocusedRow());
            }

            internal void <RegisterClassCommandBindings>b__226_42(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).CancelEditFocusedRow();
            }

            internal void <RegisterClassCommandBindings>b__226_43(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanCancelEditFocusedRow(e);
            }

            internal void <RegisterClassCommandBindings>b__226_44(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).EndEditFocusedRow();
            }

            internal void <RegisterClassCommandBindings>b__226_45(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanEndEditFocusedRow(e);
            }

            internal void <RegisterClassCommandBindings>b__226_46(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).ShowUnboundExpressionEditor(e);
            }

            internal void <RegisterClassCommandBindings>b__226_47(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanShowUnboundExpressionEditor(e);
            }

            internal void <RegisterClassCommandBindings>b__226_5(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanShowColumnChooser(e);
            }

            internal void <RegisterClassCommandBindings>b__226_6(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).HideColumnChooser();
            }

            internal void <RegisterClassCommandBindings>b__226_7(object d, CanExecuteRoutedEventArgs e)
            {
                ((DataViewBase) d).OnCanHideColumnChooser(e);
            }

            internal void <RegisterClassCommandBindings>b__226_8(object d, ExecutedRoutedEventArgs e)
            {
                ((DataViewBase) d).MovePrevCell();
            }

            internal void <RegisterClassCommandBindings>b__226_9(object d, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = ((DataViewBase) d).CanMovePrevCell();
            }

            internal bool <RequestUIUpdate>b__1420_0(DataViewBase view) => 
                view.RequestUIUpdateCore(false);

            internal void <ResetIncrementalSearch>b__2042_0(DataControlBase dataControl)
            {
                dataControl.DataView.ClearEditorHighlightingText();
            }

            internal void <ResetIncrementalSearch>b__2042_1(DataControlBase dataControl)
            {
                dataControl.DataView.UpdateEditorHighlightingText();
            }

            internal DataViewBase <UpdateAllDependentViews>b__1764_1(DataControlBase dataControl) => 
                dataControl.DataView;

            internal bool <UpdateAllowMouseMoveSelection>b__1453_1(ITableViewHitInfo x) => 
                x.IsRowIndicator;

            internal bool <UpdateAllowMouseMoveSelection>b__1453_2() => 
                false;

            internal Func<MouseEventArgs, bool> <UpdateAllowMouseMoveSelection>b__1453_3(DataControlBase dc) => 
                DragManager.GetAllowMouseMoveSelectionFunc(dc);

            internal void <UpdateCellDataErrors>b__1399_0(RowData rowData)
            {
                rowData.UpdateDataErrors(true);
            }

            internal void <UpdateCellDataLanguage>b__1370_0(RowData rowData)
            {
                rowData.UpdateCellDataLanguage();
            }

            internal void <UpdateColumnHeaderImageStyle>b__1403_0(ColumnBase column)
            {
                column.UpdateActualHeaderImageStyle();
            }

            internal void <UpdateColumnsActualCellTemplateSelector>b__1729_0(ColumnBase x)
            {
                x.UpdateActualCellTemplateSelector();
            }

            internal void <UpdateColumnsActualHeaderCustomizationAreaTemplateSelector>b__1385_0(ColumnBase column)
            {
                column.UpdateActualHeaderCustomizationAreaTemplateSelector();
            }

            internal void <UpdateColumnsActualHeaderTemplateSelector>b__1384_0(ColumnBase column)
            {
                column.UpdateActualHeaderTemplateSelector();
            }

            internal void <UpdateColumnsAppearance>b__1402_0(ColumnBase column)
            {
                column.UpdateAppearance();
            }

            internal void <UpdateColumnsTotalSummary>b__1401_0(ColumnBase column)
            {
                column.UpdateTotalSummaries();
            }

            internal void <UpdateEditorButtonVisibilities>b__1364_0(RowData rowData)
            {
                rowData.UpdateEditorButtonVisibilities();
            }

            internal void <UpdateEditorButtonVisibilities>b__1365_0(RowData rowData)
            {
                rowData.UpdateEditorButtonVisibilities();
            }

            internal void <UpdateEditorHighlightingText>b__1367_0(RowData rowData)
            {
                rowData.UpdateEditorHighlightingText();
            }

            internal void <UpdateFullRowState>b__1360_0(RowData rowData)
            {
                rowData.UpdateFullState();
            }

            internal void <UpdateFullRowState>b__1361_0(RowData rowData)
            {
                rowData.UpdateFullState();
            }

            internal void <UpdateHighlightSelectionState>b__2219_0(RowData x)
            {
                x.UpdateSelectionState();
            }

            internal void <UpdateIsFocused>b__1362_0(RowData rowData)
            {
                rowData.UpdateIsFocused();
            }

            internal void <UpdateIsSelected>b__1363_0(RowData rowData)
            {
                rowData.UpdateIsSelected();
            }

            internal void <UpdateLoadingRow>b__2144_0(RowData rowData)
            {
                ((LoadingRowData) rowData).UpdateLoadingState();
            }

            internal bool <UpdateRowCellFocusIfNeeded>b__1356_0(DependencyObject e) => 
                e is FilterPanelControlBase;

            internal void <UpdateRowDataFocusWithinState>b__1226_0(DataControlBase dataControl)
            {
                UpdateRowDataDelegate updateMethod = <>9__1226_1;
                if (<>9__1226_1 == null)
                {
                    UpdateRowDataDelegate local1 = <>9__1226_1;
                    updateMethod = <>9__1226_1 = x => x.UpdateClientFocusWithinState();
                }
                dataControl.DataView.ViewBehavior.UpdateViewRowData(updateMethod);
            }

            internal void <UpdateRowDataFocusWithinState>b__1226_1(RowData x)
            {
                x.UpdateClientFocusWithinState();
            }

            internal void <UpdateShowValidationAttributeError>b__1398_0(ColumnBase column)
            {
                column.UpdateActualShowValidationAttributeErrors();
            }
        }


        private class DefaultColumnChooserColumnsSortOrderComparer : IComparer<ColumnBase>
        {
            public static readonly IComparer<ColumnBase> Instance = new DataViewBase.DefaultColumnChooserColumnsSortOrderComparer();

            private DefaultColumnChooserColumnsSortOrderComparer()
            {
            }

            int IComparer<ColumnBase>.Compare(ColumnBase x, ColumnBase y) => 
                Comparer<string>.Default.Compare(DataViewBase.GetColumnChooserSortableCaption(x), DataViewBase.GetColumnChooserSortableCaption(y));
        }
    }
}

