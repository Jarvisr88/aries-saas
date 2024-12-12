namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Core;
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Utils;
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public abstract class TableViewBehavior : DataViewBehavior, IFormatConditionCollectionOwner, INotifyPropertyChanged
    {
        private MouseMoveSelectionBase mouseMoveSelection;
        private GridViewInfo viewInfo;
        private DispatcherTimer scrollTimer;
        private IList<ColumnBase> fixedLeftVisibleColumns;
        private IList<ColumnBase> fixedRightVisibleColumns;
        private IList<ColumnBase> fixedNoneVisibleColumns;
        private DevExpress.Xpf.Grid.Native.ColumnsLayoutCalculator columnsLayoutCalculator;
        private DevExpress.Xpf.Grid.AdditionalRowItemsControl additionalRowItemsControl;
        internal Locker BestFitLocker;
        private DependencyObject autoFilterRowState;
        private CollectionChangedWeakEventHandler<TableViewBehavior> compactModeFilterItemsCollectionChangedHandler;
        private Decorator bestFitControlDecorator;
        private bool updatePanelsEnqueued;
        internal readonly Locker CellMergeLocker;
        private static UseLightweightTemplates? DefaultUseLightweightTemplates;
        internal bool canChangeUseLightweightTemplates;
        private FormatConditionCollection formatConditions;
        private ConditionalFormatSummaryInfo[] originalSummaries;
        private IDictionary<ServiceSummaryItemKey, ServiceSummaryItem> convertedSummaries;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        protected TableViewBehavior(DataViewBase view) : base(view)
        {
            this.BestFitLocker = new Locker();
            this.autoFilterRowState = new RowStateObject();
            this.CellMergeLocker = new Locker();
            this.canChangeUseLightweightTemplates = true;
            this.FixedLeftVisibleColumns = new ObservableCollection<ColumnBase>();
            this.FixedRightVisibleColumns = new ObservableCollection<ColumnBase>();
            this.FixedNoneVisibleColumns = new ObservableCollection<ColumnBase>();
            this.scrollTimer = new DispatcherTimer();
            this.scrollTimer.Interval = TimeSpan.FromMilliseconds(10.0);
            this.scrollTimer.Tick += new EventHandler(this.OnScrollTimer_Tick);
            AdditionalRowData data1 = new AdditionalRowData(view.VisualDataTreeBuilder);
            data1.RowHandle = new RowHandle(-2147483645);
            this.AutoFilterRowData = data1;
            this.BestFitCalculator = new DataControlBestFitCalculator(base.View);
        }

        protected internal virtual void AddFormatConditionCore(FormatConditionBase formatCondition)
        {
            if (formatCondition != null)
            {
                IModelItem dataControlModelItem = base.View.DesignTimeAdorner.DataControlModelItem;
                using (IModelEditingScope scope = dataControlModelItem.BeginEdit("Add format condition"))
                {
                    IModelItemCollection formatConditionsModelItemCollection = this.GetFormatConditionsModelItemCollection(dataControlModelItem);
                    if (formatCondition is IndicatorFormatConditionBase)
                    {
                        foreach (IModelItem item2 in (from x in formatConditionsModelItemCollection
                            where (x.ItemType == formatCondition.GetType()) && ((x.Properties[FormatConditionBase.FieldNameProperty.Name].ComputedValue as string) == formatCondition.FieldName)
                            select x).ToArray<IModelItem>())
                        {
                            formatConditionsModelItemCollection.Remove(item2);
                        }
                    }
                    formatConditionsModelItemCollection.Add(base.View.DesignTimeAdorner.CreateModelItem(formatCondition, dataControlModelItem));
                    scope.Complete();
                }
            }
        }

        internal bool AllowMergeEditor(ColumnBase column, int rowHandle1, int rowHandle2) => 
            (base.View.IsEditorOpen || !base.View.InplaceEditorOwner.EditorWasClosed) ? (!base.View.RootView.IsKeyboardFocusWithin || (base.View.RootView.IsKeyboardFocusInSearchPanel() || (((rowHandle1 == base.View.FocusedRowHandle) || (rowHandle2 == base.View.FocusedRowHandle)) ? (ReferenceEquals(column, base.DataControl.CurrentColumn) ? !base.View.CanShowEditor(base.View.FocusedRowHandle, base.DataControl.CurrentColumn) : true) : true))) : true;

        protected internal override void ApplyResize(BaseColumn column, double value, double maxWidth)
        {
            this.ColumnsLayoutCalculator.ApplyResize(column, value, maxWidth);
        }

        internal void BestFitColumn(BaseColumn column)
        {
            this.BestFitColumnCore(column, column.IsBand || (base.DataControl.BandsCore.Count == 0));
        }

        internal void BestFitColumn(object commandParameter)
        {
            BaseColumn column = !(commandParameter is BandBase) ? base.View.GetColumnByCommandParameter(commandParameter) : (commandParameter as BaseColumn);
            if (column != null)
            {
                this.BestFitColumn(column);
            }
        }

        private void BestFitColumnCore(BaseColumn column, bool updateWidths)
        {
            if (this.IsBestFitControlDecoratorLoaded && column.GetAllowResizing())
            {
                if (!column.IsBand)
                {
                    this.ColumnsLayoutCalculator.ResetBestFitBandWidth(column.GetRootParentBand());
                }
                this.ColumnsLayoutCalculator.ApplyBestFit(column, this.CalcColumnBestFitWidthCore(column), updateWidths);
            }
        }

        private void BestFitColumnIfAllowed(BaseColumn column, bool updateWidths)
        {
            if (this.CanBestFitColumn(column))
            {
                this.BestFitColumnCore(column, updateWidths);
            }
        }

        internal void BestFitColumns()
        {
            if (this.IsBestFitControlDecoratorLoaded)
            {
                if (base.DataControl.BandsCore.Count != 0)
                {
                    this.BestFitLocker.DoLockedAction(delegate {
                        foreach (BandBase base2 in base.DataControl.BandsCore)
                        {
                            this.TableView.BestFitColumn(base2);
                        }
                    });
                }
                else
                {
                    Dictionary<ColumnBase, double> widthsCache = new Dictionary<ColumnBase, double>();
                    foreach (ColumnBase base2 in base.View.VisibleColumnsCore)
                    {
                        if (this.CanBestFitColumn(base2))
                        {
                            widthsCache[base2] = this.CalcColumnBestFitWidthCore(base2);
                        }
                    }
                    this.BestFitLocker.DoLockedAction(delegate {
                        this.View.BeginUpdateColumnsLayout();
                        foreach (ColumnBase base2 in widthsCache.Keys)
                        {
                            this.ColumnsLayoutCalculator.ApplyResize(base2, widthsCache[base2], double.MaxValue, 0.0, false);
                        }
                        this.View.EndUpdateColumnsLayout();
                    });
                }
            }
        }

        internal int? CalcActualFocusedRowHandle()
        {
            if ((base.View.FocusedRowHandleCore == -2147483647) && base.View.ActualAllowCellMerge)
            {
                return new int?(base.View.FocusedRowHandleCore);
            }
            return null;
        }

        internal double CalcColumnBestFitWidthCore(BaseColumn column)
        {
            double num;
            InplaceEditorBase currentCellEditor = base.View.CurrentCellEditor;
            try
            {
                if (!column.IsBand)
                {
                    num = this.BestFitCalculator.CalcColumnBestFitWidth(column as IBestFitColumn);
                }
                else
                {
                    base.View.BeginUpdateColumnsLayout();
                    try
                    {
                        num = this.BestFitCalculator.CalcColumnBestFitWidthCore(column as BandBase);
                    }
                    finally
                    {
                        base.View.EndUpdateColumnsLayout();
                    }
                }
            }
            finally
            {
                base.View.CurrentCellEditor = currentCellEditor;
            }
            return num;
        }

        protected internal override double CalcColumnMaxWidth(ColumnBase column) => 
            this.ColumnsLayoutCalculator.CalcColumnMaxWidth(column);

        protected virtual double CalcDesiredWidth(Size constraint)
        {
            double num = ((this.HorizontalExtent + this.LeftIndent) + this.RightIndent) + (this.TableView.ActualShowIndicator ? this.TableView.IndicatorWidth : 0.0);
            if (this.HasFixedLeftElements)
            {
                num += this.TableView.FixedLineWidth;
            }
            if (this.HasFixedRightElements)
            {
                num += this.TableView.FixedLineWidth;
            }
            return num;
        }

        protected internal override double CalcVerticalDragIndicatorSize(UIElement panel, Point point, double width)
        {
            ITableView rootView = (ITableView) base.View.RootView;
            double num = width;
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(panel, rootView.ViewBase);
            double totalLeftIndent = this.GetTotalLeftIndent(true, true);
            if (point.X < (totalLeftIndent - relativeElementRect.Left))
            {
                num -= (totalLeftIndent - relativeElementRect.Left) - point.X;
            }
            if (((rootView.ViewBase.ActualWidth - this.GetVerticalScrollBarWidth()) - relativeElementRect.Left) < (point.X + width))
            {
                num -= (((point.X + width) - rootView.ViewBase.ActualWidth) + this.GetVerticalScrollBarWidth()) + relativeElementRect.Left;
            }
            return num;
        }

        internal override bool CanAdjustScrollbarCore() => 
            base.View.ScrollingMode == ScrollingMode.Smart;

        internal override bool CanBestFitAllColumns() => 
            this.TableView.AllowBestFit && this.TableView.AllowResizing;

        internal bool CanBestFitColumn(BaseColumn column) => 
            column.AllowResizing.GetValue(this.TableView.AllowResizing) && this.CanBestFitColumnCore(column);

        internal override bool CanBestFitColumnCore(BaseColumn column) => 
            column.AllowBestFit.GetValue(this.TableView.AllowBestFit);

        internal bool CanBestFitColumns() => 
            this.TableView.AllowBestFit;

        protected virtual bool CanNavigateToAdditionalRow(bool allowNavigateToAutoFilterRow) => 
            this.CanNavigateToAutoFilterRow(allowNavigateToAutoFilterRow);

        private bool CanNavigateToAutoFilterRow(bool allowNavigateToAutoFilterRow) => 
            allowNavigateToAutoFilterRow && this.TableView.ShowAutoFilterRow;

        private bool CheckCompactModeWidth()
        {
            if ((base.View.RootDataPresenter == null) || (base.View.DataControl == null))
            {
                return false;
            }
            double num = base.View.RootDataPresenter.ActualWidth - this.GetTotalLeftIndent(true, true);
            return (this.TableView.SwitchToCompactModeWidth >= num);
        }

        internal override bool CheckNavigationStyle(int newValue) => 
            base.CheckNavigationStyle(newValue) && (newValue != -2147483645);

        private void ClearColumnsLayoutCalculator()
        {
            this.columnsLayoutCalculator = null;
        }

        protected internal virtual void ClearFormatConditionsFromAllColumnsCore()
        {
            IModelItemCollection formatConditionsModelItemCollection = this.GetFormatConditionsModelItemCollection(base.View.DesignTimeAdorner.DataControlModelItem);
            Func<FormatConditionBase, bool> searchPredicate = <>c.<>9__436_0;
            if (<>c.<>9__436_0 == null)
            {
                Func<FormatConditionBase, bool> local1 = <>c.<>9__436_0;
                searchPredicate = <>c.<>9__436_0 = x => ((ISupportManager) x).AllowUserCustomization;
            }
            IModelItem[] conditionsToDelete = this.FindConditionModelItems(formatConditionsModelItemCollection, searchPredicate);
            if (conditionsToDelete.Length == formatConditionsModelItemCollection.Count<IModelItem>())
            {
                formatConditionsModelItemCollection.Clear();
            }
            else
            {
                this.RemoveConditions(conditionsToDelete, formatConditionsModelItemCollection);
            }
            this.RemoveOrphanConditionFilters();
        }

        protected internal virtual void ClearFormatConditionsFromColumnCore(ColumnBase column)
        {
            if (column != null)
            {
                IModelItemCollection formatConditionsModelItemCollection = this.GetFormatConditionsModelItemCollection(base.View.DesignTimeAdorner.DataControlModelItem);
                IModelItem[] conditionsToDelete = this.FindConditionModelItems(formatConditionsModelItemCollection, x => (x.FieldName == column.FieldName) && ((ISupportManager) x).AllowUserCustomization);
                this.RemoveConditions(conditionsToDelete, formatConditionsModelItemCollection);
                this.RemoveOrphanConditionFilters();
            }
        }

        private void CompactModeFilterItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList compactModeFilterItemsSource = this.TableView.CompactModeFilterItemsSource as IList;
            if (compactModeFilterItemsSource != null)
            {
                Func<object, object> convertItemAction = <>c.<>9__338_0;
                if (<>c.<>9__338_0 == null)
                {
                    Func<object, object> local1 = <>c.<>9__338_0;
                    convertItemAction = <>c.<>9__338_0 = obj => obj as ICustomItem;
                }
                SyncCollectionHelper.SyncCollection(e, this.TableView.CompactModeFilterItems, compactModeFilterItemsSource, convertItemAction, null, null, null);
            }
        }

        internal void CompactModeUpdate(double oldWeight, double newWeight, bool forceUpdate)
        {
            if ((this.TableView.SwitchToCompactModeWidth != 0.0) && ((this.TableView.DataRowCompactTemplate != null) && (base.View.RootDataPresenter != null)))
            {
                if (forceUpdate)
                {
                    this.UpdateCompactModeCore();
                }
                else if (double.IsNaN(oldWeight) || double.IsNaN(newWeight))
                {
                    this.UpdateCompactModeCore();
                }
                else
                {
                    double totalLeftIndent = this.GetTotalLeftIndent(true, true);
                    double num2 = base.View.RootDataPresenter.ActualWidth - totalLeftIndent;
                    double num3 = (base.View.RootDataPresenter.ActualWidth + (newWeight - oldWeight)) - totalLeftIndent;
                    if (((num2 <= this.TableView.SwitchToCompactModeWidth) && (num3 > this.TableView.SwitchToCompactModeWidth)) || ((num2 > this.TableView.SwitchToCompactModeWidth) && (num3 <= this.TableView.SwitchToCompactModeWidth)))
                    {
                        this.UpdateCompactModeCore();
                    }
                }
            }
        }

        internal void CopyCellsToClipboard(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
        {
            if (this.IsValidRowHandleAndColumn(startRowHandle, startColumn) && this.IsValidRowHandleAndColumn(endRowHandle, endColumn))
            {
                List<CellBase> gridCells = new List<CellBase>();
                base.IterateCells(startRowHandle, startColumn.ActualVisibleIndex, endRowHandle, endColumn.ActualVisibleIndex, (rowHandle, column) => gridCells.Add(this.TableView.CreateGridCell(rowHandle, column)));
                this.TableView.CopyCellsToClipboard(gridCells);
            }
        }

        internal override void CopyToDetail(DataControlBase dataControl)
        {
            base.CopyToDetail(dataControl);
            CloneDetailHelper.CopyToCollection<FormatConditionBase>(this.FormatConditions, GetFormatConditions(dataControl));
        }

        protected internal override Point CorrectDragIndicatorLocation(UIElement panel, Point point)
        {
            ITableView rootView = (ITableView) base.View.RootView;
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(panel, rootView.ViewBase);
            double totalLeftIndent = this.GetTotalLeftIndent(true, true);
            return ((point.X >= (totalLeftIndent - relativeElementRect.Left)) ? (((rootView.ViewBase.ActualWidth - this.GetVerticalScrollBarWidth()) >= (relativeElementRect.Left + point.X)) ? point : new Point((rootView.ViewBase.ActualWidth - this.GetVerticalScrollBarWidth()) - relativeElementRect.Left, point.Y)) : new Point(totalLeftIndent - relativeElementRect.Left, point.Y));
        }

        protected internal override Size CorrectMeasureResult(double scrollOffset, Size constraint, Size result)
        {
            if ((((scrollOffset > 0.0) || (base.View.ScrollingMode == ScrollingMode.Normal)) && !double.IsPositiveInfinity(constraint.Height)) && (result.Height < constraint.Height))
            {
                result.Height = constraint.Height;
            }
            result.Width = Math.Min(constraint.Width, Math.Max(result.Width, this.CalcDesiredWidth(constraint)));
            return result;
        }

        internal override GridViewNavigationBase CreateAdditionalRowNavigation() => 
            new AdditionalRowNavigation(base.View);

        internal abstract BestFitControlBase CreateBestFitControl(ColumnBase column);
        internal abstract BestFitControlBase CreateBestFitGroupControl(ColumnBase column);
        internal abstract BestFitControlBase CreateBestFitGroupFooterSummaryControl(ColumnBase column);
        internal override GridViewNavigationBase CreateCellNavigation() => 
            new GridViewCellNavigation(base.View);

        internal CustomDataUpdateFormatConditionEventArgs CreateCustomDataUpdateFormatConditionArgs(CustomDataUpdateFormatConditionEventArgsSource argsSource) => 
            CustomDataUpdateFormatConditionEventArgs.Create(argsSource, this.TableView);

        internal IDataUpdateAnimationProvider CreateDataUpdateAnimationProvider() => 
            new DataUpdateAnimationProvider { View = this.TableView };

        internal abstract DetailRowControlBase CreateDetailColumnHeadersElement();
        internal abstract DetailHeaderControlBase CreateDetailContentElement(bool showLastDetailMargin);
        internal abstract DetailRowControlBase CreateDetailFixedTotalSummaryElement();
        internal abstract DetailHeaderControlBase CreateDetailHeaderElement(bool showLastDetailMargin);
        internal abstract DetailHeaderControlBase CreateDetailMarginElement(bool isTop);
        internal abstract DetailRowControlBase CreateDetailNewItemRowElement();
        internal abstract DetailTabHeadersControlBase CreateDetailTabHeadersElement();
        internal abstract DetailRowControlBase CreateDetailTotalSummaryElement();
        internal FrameworkElement CreateElement(Func<FrameworkElement> lightweightDelegate, Func<FrameworkElement> ordinaryDelegate, UseLightweightTemplates flag)
        {
            if (this.canChangeUseLightweightTemplates)
            {
                this.ForbidChangeUseLightweightTemplatesProperty();
                this.ValidateRowStyle(this.TableView.RowStyle);
            }
            return (!this.UseLightweightTemplatesHasFlag(flag) ? ordinaryDelegate() : lightweightDelegate());
        }

        protected virtual FormatConditionBase CreateFormatCondition(string conditionTypeName)
        {
            Assembly assembly = typeof(FormatConditionBase).Assembly;
            if (assembly == null)
            {
                return null;
            }
            Type type = assembly.GetType($"{"DevExpress.Xpf.Grid"}.{conditionTypeName}");
            return ((type != null) ? (Activator.CreateInstance(type) as FormatConditionBase) : null);
        }

        internal abstract GridColumnHeaderBase CreateGridColumnHeader();
        internal abstract FrameworkElement CreateGridTotalSummaryControl();
        private void CreateInternalContentObjectOnDeserialization(XtraCreateCollectionItemEventArgs e, DependencyProperty property, DependencyObject owner)
        {
            if (e.Item.ChildProperties[property.Name] != null)
            {
                owner.SetValue(property, Activator.CreateInstance(property.PropertyType));
            }
        }

        internal override MasterDetailProviderBase CreateMasterDetailProvider() => 
            new MasterDetailProvider(this);

        protected internal virtual GridViewInfo CreatePrintViewInfo() => 
            this.CreatePrintViewInfo(null);

        protected internal virtual GridViewInfo CreatePrintViewInfo(BandsLayoutBase bandsLayout) => 
            new GridPrintViewInfo(base.View, bandsLayout);

        internal override GridViewNavigationBase CreateRowNavigation() => 
            new GridViewRowNavigation(base.View);

        protected internal virtual GridViewInfo CreateViewInfo() => 
            new GridViewInfo(base.View);

        void IFormatConditionCollectionOwner.OnFormatConditionCollectionChanged(FormatConditionChangeType changeType)
        {
            DataControlBase dataControl = base.DataControl;
            if (dataControl == null)
            {
                DataControlBase local1 = dataControl;
            }
            else
            {
                dataControl.AttachToFormatConditions(changeType);
            }
            DataControlBase base2 = base.DataControl;
            if (base2 == null)
            {
                DataControlBase local2 = base2;
            }
            else
            {
                base2.UpdatePropertySchemeController();
            }
            DataControlBase base3 = base.DataControl;
            if (base3 == null)
            {
                DataControlBase local3 = base3;
            }
            else
            {
                base3.NotifyFormatConditionsChanged();
            }
            base.View.UpdateFilterPanel();
        }

        void IFormatConditionCollectionOwner.SyncFormatConditionCollectionWithDetails(NotifyCollectionChangedEventArgs e)
        {
            if ((base.DataControl != null) && base.DataControl.DataProviderBase.IsVirtualSource)
            {
                Func<FormatConditionBase, bool> predicate = <>c.<>9__524_0;
                if (<>c.<>9__524_0 == null)
                {
                    Func<FormatConditionBase, bool> local1 = <>c.<>9__524_0;
                    predicate = <>c.<>9__524_0 = x => x.GetType() != typeof(FormatCondition);
                }
                if (this.FormatConditions.Any<FormatConditionBase>(predicate))
                {
                    throw new NotSupportedException("The GridControl bound to the Virtual data source supports the FormatCondition conditional formats only.");
                }
            }
            base.DataControl.Do<DataControlBase>(delegate (DataControlBase x) {
                Func<DataControlBase, IList> getCollection = <>c.<>9__524_2;
                if (<>c.<>9__524_2 == null)
                {
                    Func<DataControlBase, IList> local1 = <>c.<>9__524_2;
                    getCollection = <>c.<>9__524_2 = dc => GetFormatConditions(dc);
                }
                x.GetDataControlOriginationElement().NotifyCollectionChanged(x, getCollection, <>c.<>9__524_3 ??= item => CloneDetailHelper.CloneElement<FormatConditionBase>((FormatConditionBase) item, (Action<FormatConditionBase>) null, (Func<FormatConditionBase, Locker>) null, (object[]) null), e);
            });
        }

        void IFormatConditionCollectionOwner.SyncFormatConditionPropertyWithDetails(FormatConditionBase item, DependencyPropertyChangedEventArgs e)
        {
            base.DataControl.Do<DataControlBase>(delegate (DataControlBase x) {
                Func<DataControlBase, DependencyObject> <>9__1;
                Func<DataControlBase, DependencyObject> getTarget = <>9__1;
                if (<>9__1 == null)
                {
                    Func<DataControlBase, DependencyObject> local1 = <>9__1;
                    getTarget = <>9__1 = (Func<DataControlBase, DependencyObject>) (dc => CloneDetailHelper.SafeGetDependentCollectionItem<FormatConditionBase>(item, this.FormatConditions, GetFormatConditions(dc)));
                }
                x.GetDataControlOriginationElement().NotifyPropertyChanged(x, e.Property, getTarget, typeof(FormatConditionBase));
            });
        }

        private void DoRowAction(int rowHandle, Func<RowData, Action> getTarget)
        {
            RowData rowData = this.GetRowData(rowHandle);
            if (rowData != null)
            {
                Action action = getTarget(rowData);
                if (action != null)
                {
                    action();
                }
            }
        }

        private void DragScroll()
        {
            if (DragDropScroller.IsDragging(base.View))
            {
                if ((this.LastMousePosition.X - this.TableView.IndicatorWidth) < 0.0)
                {
                    this.ChangeHorizontalOffsetBy(-10.0);
                }
                if (this.LastMousePosition.X > base.View.ScrollContentPresenter.ActualWidth)
                {
                    this.ChangeHorizontalOffsetBy(10.0);
                }
            }
        }

        protected internal override bool EndRowEdit()
        {
            bool flag = base.EndRowEdit();
            this.UpdateFocusedRowIndicator();
            return flag;
        }

        internal override void EnsureSurroundingsActualSize(Size finalSize)
        {
            this.ViewInfo.ColumnsLayoutSize = finalSize;
        }

        internal void FillByLastFixedColumn(double arrangeWidth)
        {
            this.UpdateColumnsStrategy.FillByLastFixedColumn(this, arrangeWidth);
        }

        private IModelItem[] FindConditionModelItems(IModelItemCollection formatConditions, Func<FormatConditionBase, bool> searchPredicate) => 
            (from x in formatConditions
                where searchPredicate((FormatConditionBase) x.GetCurrentValue())
                select x).ToArray<IModelItem>();

        internal double FixedLeftColumnsWidth()
        {
            double totalLeftIndent = this.GetTotalLeftIndent(true, true);
            foreach (ColumnBase base2 in this.FixedLeftVisibleColumns)
            {
                totalLeftIndent += this.GetColumnWidth(base2);
            }
            if (this.FixedLeftVisibleColumns.Count != 0)
            {
                totalLeftIndent += this.TableView.FixedLineWidth;
            }
            return totalLeftIndent;
        }

        private void ForbidChangeUseLightweightTemplatesProperty()
        {
            this.canChangeUseLightweightTemplates = false;
        }

        protected internal override ControlTemplate GetActualColumnChooserTemplate() => 
            (base.View.ColumnChooserColumnDisplayMode != ColumnChooserColumnDisplayMode.ShowHiddenColumnsOnly) ? base.View.ExtendedColumnChooserTemplate : (((base.DataControl == null) || ((base.DataControl.BandsLayoutCore == null) || !base.DataControl.BandsLayoutCore.ShowBandsInCustomizationForm)) ? base.View.ColumnChooserTemplate : this.TableView.ColumnBandChooserTemplate);

        internal override FixedStyle GetActualColumnFixed(BaseColumn column) => 
            this.UpdateColumnsStrategy.GetFixedStyle(column, this);

        private double GetActualColumnOffset(BaseColumn column) => 
            (base.DataControl.BandsLayoutCore == null) ? this.GetColumnOffset((ColumnBase) column) : this.GetBandOffset(column);

        internal virtual DataTemplate GetActualCompactTemplate() => 
            ((this.TableView.SwitchToCompactModeWidth == 0.0) || (this.TableView.DataRowCompactTemplate == null)) ? null : (this.CheckCompactModeWidth() ? this.TableView.DataRowCompactTemplate : null);

        protected internal override FrameworkElement GetAdditionalRowElement(int rowHandle)
        {
            FrameworkElement additionalElement;
            DevExpress.Xpf.Grid.AdditionalRowItemsControl additionalRowItemsControl = this.AdditionalRowItemsControl;
            if (additionalRowItemsControl == null)
            {
                return null;
            }
            using (IEnumerator enumerator = ((IEnumerable) additionalRowItemsControl.Items).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IAdditionalRowElement current = (IAdditionalRowElement) enumerator.Current;
                        if (current.RowHandle != rowHandle)
                        {
                            continue;
                        }
                        additionalElement = current.AdditionalElement;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return additionalElement;
        }

        protected virtual int GetAdditionalRowHandle() => 
            -2147483645;

        private double GetBandOffset(BaseColumn column)
        {
            double num = 0.0;
            if (base.View != null)
            {
                foreach (BandBase base2 in base.DataControl.BandsLayoutCore.GetBands(column.ParentBandInternal, true, false))
                {
                    num += base2.ActualHeaderWidth;
                }
                if (!column.IsBand)
                {
                    for (int i = 0; (i < column.BandRow.Columns.Count) && (column.BandRow.Columns[i] != column); i++)
                    {
                        num += column.BandRow.Columns[i].ActualHeaderWidth;
                    }
                }
                if (base.DataControl.BandsLayoutCore.FixedLeftVisibleBands.Count > 0)
                {
                    num += this.TableView.FixedLineWidth;
                }
            }
            return num;
        }

        private GridColumnData GetCellData(RowData rowData, string fieldName) => 
            rowData.With<RowData, GridColumnData>(delegate (RowData x) {
                Func<DataControlBase, ColumnBase> <>9__1;
                Func<DataControlBase, ColumnBase> evaluator = <>9__1;
                if (<>9__1 == null)
                {
                    Func<DataControlBase, ColumnBase> local1 = <>9__1;
                    evaluator = <>9__1 = y => y.ColumnsCore[fieldName];
                }
                return x.GetCellDataByColumn(this.DataControl.With<DataControlBase, ColumnBase>(evaluator));
            });

        internal ColumnBase GetColumn(double offset)
        {
            ColumnBase base3;
            if (this.TableView.ShowIndicator)
            {
                offset -= this.TableView.IndicatorWidth;
            }
            if (offset < -this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset)
            {
                return base.View.VisibleColumnsCore[0];
            }
            double totalGroupAreaIndent = this.ViewInfo.TotalGroupAreaIndent;
            using (IEnumerator<ColumnBase> enumerator = this.FixedLeftVisibleColumns.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    ColumnBase current = enumerator.Current;
                    if (offset > (totalGroupAreaIndent + this.GetColumnWidth(current)))
                    {
                        totalGroupAreaIndent += this.GetColumnWidth(current);
                        continue;
                    }
                    return current;
                }
            }
            if (this.FixedLeftVisibleColumns.Count != 0)
            {
                totalGroupAreaIndent += this.TableView.FixedLineWidth;
            }
            double num2 = totalGroupAreaIndent - this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
            using (IEnumerator<ColumnBase> enumerator2 = this.FixedNoneVisibleColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        ColumnBase current = enumerator2.Current;
                        bool flag = ((offset - num2) - this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset) > (this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport + this.TableView.FixedLineWidth);
                        if (!((this.FixedRightVisibleColumns.Count != 0) & flag))
                        {
                            if (offset > (num2 + this.GetColumnWidth(current)))
                            {
                                num2 += this.GetColumnWidth(current);
                                continue;
                            }
                            return current;
                        }
                    }
                    break;
                }
            }
            totalGroupAreaIndent += this.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport + this.TableView.FixedLineWidth;
            using (IEnumerator<ColumnBase> enumerator3 = this.FixedRightVisibleColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator3.MoveNext())
                    {
                        ColumnBase current = enumerator3.Current;
                        if (offset > (totalGroupAreaIndent + this.GetColumnWidth(current)))
                        {
                            totalGroupAreaIndent += this.GetColumnWidth(current);
                            continue;
                        }
                        base3 = current;
                    }
                    else
                    {
                        return base.View.VisibleColumnsCore[base.View.VisibleColumnsCore.Count - 1];
                    }
                    break;
                }
            }
            return base3;
        }

        internal ColumnBase GetColumnByCoordinateWithOffset(double offset, double? coord = new double?())
        {
            ColumnBase base3;
            if (this.RootDataPresenter == null)
            {
                return null;
            }
            double num = ((coord != null) ? coord.Value : offset) - this.GetTotalLeftIndent(true, true);
            if (num >= 0.0)
            {
                if (coord != null)
                {
                    coord = new double?(num);
                }
                offset -= this.GetTotalLeftIndent(true, true);
                double num2 = (base.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? 0.0 : (this.TableView.ActualShowDetailButtons ? 0.0 : this.ViewInfo.TotalGroupAreaIndent);
                using (IEnumerator<ColumnBase> enumerator2 = this.FixedLeftVisibleColumns.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator2.MoveNext())
                        {
                            break;
                        }
                        ColumnBase current = enumerator2.Current;
                        if (coord == null)
                        {
                            if (((offset - num2) - this.GetColumnWidth(current)) < 0.0)
                            {
                                return current;
                            }
                        }
                        else if (((coord.Value - num2) - this.GetColumnWidth(current)) < 0.0)
                        {
                            return current;
                        }
                        num2 += this.GetColumnWidth(current);
                    }
                }
                if (this.FixedLeftVisibleColumns.Count != 0)
                {
                    num2 += this.TableView.FixedLineWidth;
                }
                double num3 = num2;
                using (IEnumerator<ColumnBase> enumerator3 = this.FixedNoneVisibleColumns.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator3.MoveNext())
                        {
                            ColumnBase current = enumerator3.Current;
                            bool flag = ((offset - this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset) - num3) > (this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport + this.TableView.FixedLineWidth);
                            if (!((this.FixedRightVisibleColumns.Count != 0) & flag))
                            {
                                if (offset > (num3 + this.GetColumnWidth(current)))
                                {
                                    num3 += this.GetColumnWidth(current);
                                    continue;
                                }
                                return current;
                            }
                        }
                        break;
                    }
                }
                num2 += Math.Min(this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Extent, this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport) + this.TableView.FixedLineWidth;
                using (IEnumerator<ColumnBase> enumerator4 = this.FixedRightVisibleColumns.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator4.MoveNext())
                        {
                            ColumnBase current = enumerator4.Current;
                            if ((offset - this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset) > (num2 + this.GetColumnWidth(current)))
                            {
                                num2 += this.GetColumnWidth(current);
                                continue;
                            }
                            base3 = current;
                        }
                        else
                        {
                            return base.View.VisibleColumnsCore[base.View.VisibleColumnsCore.Count - 1];
                        }
                        break;
                    }
                }
            }
            else
            {
                if ((this.FixedLeftVisibleColumns != null) && (this.FixedLeftVisibleColumns.Count > 0))
                {
                    return this.FixedLeftVisibleColumns[0];
                }
                double num4 = this.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset - this.GetTotalLeftIndent(true, true);
                using (IEnumerator<ColumnBase> enumerator = this.FixedNoneVisibleColumns.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ColumnBase current = enumerator.Current;
                            if ((num4 - this.GetColumnWidth(current)) > 0.0)
                            {
                                num4 -= this.GetColumnWidth(current);
                                continue;
                            }
                            base3 = current;
                        }
                        else
                        {
                            return ((base.View.VisibleColumnsCore.Count > 0) ? base.View.VisibleColumnsCore[0] : null);
                        }
                        break;
                    }
                }
            }
            return base3;
        }

        private double GetColumnOffset(ColumnBase column)
        {
            double num = 0.0;
            int num2 = 0;
            while ((num2 < base.View.VisibleColumnsCore.Count) && (base.View.VisibleColumnsCore[num2] != column))
            {
                num += this.GetColumnWidth(base.View.VisibleColumnsCore[num2]);
                num2++;
            }
            if ((this.FixedLeftVisibleColumns.Count > 0) && (num2 >= this.FixedLeftVisibleColumns.Count))
            {
                num += this.TableView.FixedLineWidth;
            }
            return (num + this.TotalVisibleIndent);
        }

        private double GetColumnWidth(ColumnBase column) => 
            (column != null) ? ((column.ActualDataWidth + column.ActualBandLeftSeparatorWidthCore) + column.ActualBandRightSeparatorWidthCore) : 0.0;

        protected internal override void GetDataRowText(StringBuilder sb, int rowHandle)
        {
            for (int i = 0; i < base.View.VisibleColumnsCore.Count; i++)
            {
                sb.Append(base.View.GetTextForClipboard(rowHandle, i));
                if (base.DataControl.IsGroupRowHandleCore(rowHandle))
                {
                    return;
                }
                if (i != (base.View.VisibleColumnsCore.Count - 1))
                {
                    sb.Append("\t");
                }
            }
        }

        public IList<CellBase> GetDetailsSelectedCells() => 
            base.View.SelectionStrategy.GetDetailsSelectedCells();

        protected internal override double GetFixedExtent() => 
            this.HorizontalExtent;

        internal int GetFixedLeftColumnsCount() => 
            base.RebuildColumnsLayoutHelper.GetFixedLeftColumnsCount(this);

        private IList<ColumnBase> GetFixedLeftVisibleColumns() => 
            this.GetFixedVisibleColumns(FixedStyle.Left);

        internal int GetFixedNoneColumnsCount() => 
            base.RebuildColumnsLayoutHelper.GetFixedNoneColumnsCount(this);

        private IList<ColumnBase> GetFixedNoneVisibleColumns() => 
            this.GetFixedVisibleColumns(FixedStyle.None);

        internal int GetFixedRightColumnsCount() => 
            base.RebuildColumnsLayoutHelper.GetFixedRightColumnsCount(this);

        private IList<ColumnBase> GetFixedRightVisibleColumns() => 
            this.GetFixedVisibleColumns(FixedStyle.Right);

        private IList<ColumnBase> GetFixedVisibleColumns(FixedStyle fixedStyle) => 
            this.UpdateColumnsStrategy.GetFixedVisibleColumns(fixedStyle, base.View);

        private static FormatConditionCollection GetFormatConditions(DataControlBase dataControl) => 
            ((TableViewBehavior) dataControl.DataView.ViewBehavior).FormatConditions;

        private IModelItemCollection GetFormatConditionsModelItemCollection(IModelItem dataControl) => 
            dataControl.Properties["View"].Value.Properties["FormatConditions"].Collection;

        internal abstract GridColumnData GetGroupSummaryColumnData(int rowHandle, IBestFitColumn column);
        protected internal override IndicatorState GetIndicatorState(RowData rowData)
        {
            int rowHandle = rowData.RowHandle.Value;
            if (rowHandle == -2147483645)
            {
                return IndicatorState.AutoFilterRow;
            }
            if (BaseEditHelper.GetValidationError(rowData) != null)
            {
                return (((rowHandle != base.View.FocusedRowHandleCore) || (base.View.NavigationStyle == GridViewNavigationStyle.None)) ? IndicatorState.Error : IndicatorState.FocusedError);
            }
            bool flag = (rowHandle == base.View.FocusedRowHandleCore) && ((base.View.NavigationStyle != GridViewNavigationStyle.None) || base.View.IsNewItemRowHandle(base.View.FocusedRowHandleCore));
            return (!(rowData.View.IsFocusedView & flag) ? (!base.View.IsNewItemRowHandle(rowHandle) ? IndicatorState.None : IndicatorState.NewItemRow) : ((base.View.IsFocusedRowModified || base.View.EditFormManager.IsEditFormModified) ? IndicatorState.Changed : ((base.View.IsEditing || base.View.IsEditFormVisible) ? IndicatorState.Editing : IndicatorState.Focused)));
        }

        protected internal override bool GetIsCellSelected(int rowHandle, ColumnBase column) => 
            base.View.SelectionStrategy.IsCellSelected(rowHandle, column);

        protected internal virtual MouseMoveSelectionBase GetMouseMoveSelection(IDataViewHitInfo hitInfo)
        {
            if (!base.View.AllowMouseMoveSelection || (base.DataControl.SelectionMode == MultiSelectMode.MultipleRow))
            {
                return MouseMoveSelectionNone.Instance;
            }
            ITableViewHitInfo info = (ITableViewHitInfo) hitInfo;
            if ((!base.View.ShowSelectionRectangle || (!this.TableView.ViewBase.IsMultiSelection || (!hitInfo.IsDataArea && (!hitInfo.InRow || info.IsRowIndicator)))) || this.IsAdditionalRow(base.View.FocusedRowHandle))
            {
                return ((!info.IsRowIndicator || (!this.TableView.ViewBase.IsMultiSelection || (!this.TableView.UseIndicatorForSelection || (this.IsAdditionalRow(base.View.FocusedRowHandle) || (ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) || ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers)))))) ? ((!hitInfo.IsRowCell || (!this.TableView.ViewBase.IsMultiCellSelection || this.IsAdditionalRow(base.View.FocusedRowHandle))) ? null : ((MouseMoveSelectionBase) MouseMoveStrategyGridCell.Instance)) : ((MouseMoveSelectionBase) MouseMoveSelectionRowIndicator.Instance));
            }
            return (!this.TableView.ViewBase.IsMultiCellSelection ? ((MouseMoveSelectionBase) MouseMoveSelectionRectangleRowIndicator.Instance) : ((MouseMoveSelectionBase) MouseMoveSelectionRectangleGridCell.Instance));
        }

        internal Tuple<double, double> GetOffsetAndHeightMergeCell(CellEditorBase cell, RowData rowData, double finalSizeHeight, double invisibleCellVerticalOffset)
        {
            int rowVisibleIndexByHandleCore = base.DataControl.GetRowVisibleIndexByHandleCore(rowData.RowHandle.Value);
            if (base.View.IsNextRowCellMerged(rowVisibleIndexByHandleCore, cell.Column, true))
            {
                return new Tuple<double, double>(invisibleCellVerticalOffset, finalSizeHeight);
            }
            if (!rowData.IsRowInView())
            {
                return null;
            }
            double num2 = 0.0;
            double num3 = finalSizeHeight;
            bool flag = true;
            while (base.View.IsPrevRowCellMerged(rowVisibleIndexByHandleCore, cell.Column, true))
            {
                flag = false;
                RowData data = base.View.GetRowData(base.DataControl.GetRowHandleByVisibleIndexCore(rowVisibleIndexByHandleCore - 1));
                double height = data.RowElement.DesiredSize.Height;
                if (data.IsLastFixedRow)
                {
                    Size desiredSize = rowData.RowElement.DesiredSize;
                    height -= (this.FixedLineHeight + finalSizeHeight) - desiredSize.Height;
                }
                num2 -= height;
                num3 += height;
                rowVisibleIndexByHandleCore--;
            }
            return (flag ? null : new Tuple<double, double>(num2, num3));
        }

        protected internal override RowData GetRowData(int rowHandle) => 
            (rowHandle != -2147483645) ? base.GetRowData(rowHandle) : this.AutoFilterRowData;

        protected internal override DependencyObject GetRowState(int rowHandle) => 
            (rowHandle != -2147483645) ? null : this.autoFilterRowState;

        public IList<CellBase> GetSelectedCells() => 
            base.View.SelectionStrategy.GetSelectedCells();

        internal double GetSelectedColumnOffset(ColumnBase column)
        {
            double totalLeftIndent = this.GetTotalLeftIndent(true, true);
            int num2 = 0;
            while ((num2 < base.View.VisibleColumnsCore.Count) && (base.View.VisibleColumnsCore[num2] != column))
            {
                totalLeftIndent += this.GetColumnWidth(base.View.VisibleColumnsCore[num2]);
                num2++;
            }
            if ((this.FixedLeftVisibleColumns.Count > 0) && (num2 >= this.FixedLeftVisibleColumns.Count))
            {
                totalLeftIndent += this.TableView.FixedLineWidth;
            }
            return ((totalLeftIndent + this.TotalVisibleIndent) + (this.GetColumnWidth(column) / 2.0));
        }

        internal override IDictionary<ServiceSummaryItemKey, ServiceSummaryItem> GetServiceSummaries()
        {
            if (this.originalSummaries != this.FormatConditions.Summaries)
            {
                this.originalSummaries = this.FormatConditions.Summaries;
                Func<ServiceSummaryItem, ServiceSummaryItemKey> keySelector = <>c.<>9__520_1;
                if (<>c.<>9__520_1 == null)
                {
                    Func<ServiceSummaryItem, ServiceSummaryItemKey> local1 = <>c.<>9__520_1;
                    keySelector = <>c.<>9__520_1 = y => ConditionalFormatSummaryInfoHelper.ToSummaryItemKey(y);
                }
                this.convertedSummaries = (from x in this.FormatConditions.Summaries select ConditionalFormatSummaryInfoHelper.ToSummaryItem(x.SummaryType, x.FieldName, base.DataControl.DataProviderBase)).ToDictionary<ServiceSummaryItem, ServiceSummaryItemKey>(keySelector);
            }
            return this.convertedSummaries;
        }

        internal override IEnumerable<IColumnInfo> GetServiceUnboundColumns() => 
            this.FormatConditions.GetUnboundColumns();

        internal abstract int GetTopRow(int pageVisibleTopRowIndex);
        internal double GetTotalLeftIndent(bool includeIndicator, bool includeCurrentGroupAreaIndentWhenDetailButtonVisible)
        {
            double indent = (!includeIndicator || !this.TableView.ActualShowIndicator) ? 0.0 : this.TableView.IndicatorWidth;
            DataControlBase originationDataControl = base.View.DataControl.GetOriginationDataControl();
            originationDataControl.EnumerateThisAndOwnerDataControls(delegate (DataControlBase dataControl) {
                ITableView dataView = (ITableView) dataControl.DataView;
                indent += !ReferenceEquals(dataControl, originationDataControl) ? dataView.ActualDetailMargin.Left : 0.0;
                if ((includeCurrentGroupAreaIndentWhenDetailButtonVisible && dataView.ActualShowDetailButtons) || !ReferenceEquals(dataControl, originationDataControl))
                {
                    indent += dataView.TableViewBehavior.ViewInfo.TotalGroupAreaIndent;
                }
            });
            originationDataControl.EnumerateThisAndOwnerDetailDescriptor(delegate (DetailDescriptorBase x) {
                indent += x.Margin.Left;
            });
            if (this.TableView.ActualShowDetailButtons)
            {
                indent += this.TableView.ActualExpandDetailButtonWidth;
            }
            return (indent + this.ActualFixRowButtonWidth);
        }

        internal double GetTotalRightIndent()
        {
            double indent = 0.0;
            DataControlBase originationDataControl = base.View.DataControl.GetOriginationDataControl();
            int level = -1;
            originationDataControl.EnumerateThisAndOwnerDataControls(delegate (DataControlBase dataControl) {
                int num = level;
                level = num + 1;
                ITableView dataView = (ITableView) dataControl.DataView;
                indent += (ReferenceEquals(dataControl, originationDataControl) || !dataView.ActualShowDetailButtons) ? 0.0 : dataView.ActualDetailMargin.Right;
            });
            originationDataControl.EnumerateThisAndOwnerDetailDescriptor(delegate (DetailDescriptorBase x) {
                indent += x.Margin.Right;
            });
            return indent;
        }

        internal override int GetValueForSelectionAnchorRowHandle(int value) => 
            (value != -2147483645) ? base.GetValueForSelectionAnchorRowHandle(value) : base.GetTopRowHandle();

        private double GetVerticalScrollBarWidth() => 
            base.View.IsTouchScrollBarsMode ? 0.0 : this.ViewInfo.VerticalScrollBarWidth;

        protected internal override KeyValuePair<DataViewBase, int> GetViewAndVisibleIndex(double verticalOffset, bool calcDataArea = true)
        {
            double num = 0.0;
            FrameworkElement firstVisibleRow = this.RootDataPresenter.GetFirstVisibleRow();
            if (firstVisibleRow != null)
            {
                num = (this.RootDataPresenter.ActualScrollOffset % 1.0) * firstVisibleRow.DesiredSize.Height;
            }
            double num2 = 0.0;
            GridRowsEnumerator enumerator = base.View.RootView.CreateVisibleRowsEnumerator();
            double num3 = Math.Max((double) 0.0, (double) (base.View.RootView.ScrollContentPresenter.ActualHeight - this.RootDataPresenter.DesiredSize.Height));
            RowDataBase currentRowData = null;
            RowNode currentNode = null;
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    currentNode = enumerator.CurrentNode;
                    currentRowData = enumerator.CurrentRowData;
                    if (currentNode == null)
                    {
                        continue;
                    }
                    FrameworkElement currentRow = enumerator.CurrentRow;
                    if (currentRow == null)
                    {
                        continue;
                    }
                    double actualHeight = currentRow.ActualHeight;
                    double num6 = 0.0;
                    if (currentRowData.FixedRowPositionCore == FixedRowPosition.None)
                    {
                        num2 -= num;
                        num = 0.0;
                        num6 = Math.Max((double) 0.0, (double) (((num2 + actualHeight) - base.View.RootView.ScrollContentPresenter.ActualHeight) + base.View.RootView.DataPresenter.Panel.FixedBottomRowsHeight));
                    }
                    else if (currentRowData.FixedRowPositionCore == FixedRowPosition.Bottom)
                    {
                        num2 += num3;
                        num3 = 0.0;
                    }
                    num2 += actualHeight - num6;
                    bool flag = verticalOffset < 0.0;
                    bool flag2 = verticalOffset < num2;
                    if (!((((num2 == base.View.RootView.ScrollContentPresenter.ActualHeight) && (num6 > 0.0)) | flag) | flag2))
                    {
                        continue;
                    }
                    if (currentNode.MatchKey is GroupSummaryRowKey)
                    {
                        return new KeyValuePair<DataViewBase, int>(base.View, -1);
                    }
                }
                if (currentRowData == null)
                {
                    return new KeyValuePair<DataViewBase, int>(base.View, -1);
                }
                DataViewBase view = currentRowData.View;
                int rowVisibleIndexByHandleCore = -1;
                if (currentNode is DataRowNode)
                {
                    if (!calcDataArea && (verticalOffset > num2))
                    {
                        return new KeyValuePair<DataViewBase, int>(base.View, -2147483648);
                    }
                    rowVisibleIndexByHandleCore = view.DataControl.GetRowVisibleIndexByHandleCore(((DataRowNode) currentNode).RowHandle.Value);
                }
                return new KeyValuePair<DataViewBase, int>(view, rowVisibleIndexByHandleCore);
            }
        }

        private Func<IFormatsOwner, ConditionalFormattingDialogViewModel> GetViewModelFactory(FormatConditionDialogType dialogKind)
        {
            switch (dialogKind)
            {
                case FormatConditionDialogType.GreaterThan:
                    return GreaterThanConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.LessThan:
                    return LessThanConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.Between:
                    return BetweenConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.EqualTo:
                    return EqualToConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.TextThatContains:
                    return TextThatContainsConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.ADateOccurring:
                    return DateOccurringConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.CustomCondition:
                    return CustomConditionConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.Top10Items:
                    return Top10ItemsConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.Bottom10Items:
                    return Bottom10ItemsConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.Top10Percent:
                    return Top10PercentConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.Bottom10Percent:
                    return Bottom10PercentConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.AboveAverage:
                    return AboveAverageConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.BelowAverage:
                    return BelowAverageConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.UniqueDuplicate:
                    return UniqueDuplicateConditionalFormattingDialogViewModel.Factory;

                case FormatConditionDialogType.DataUpdate:
                    return DataUpdateFormatConditionDialogViewModel.Factory;
            }
            throw new InvalidOperationException();
        }

        private IList<IList<AnimationTimeline>> GroupAnimationsByGeneration(FormatConditionBase condition)
        {
            if (condition == null)
            {
                return null;
            }
            IConditionalAnimationFactory factory = condition.Info.CreateAnimationFactory();
            factory.UpdateDefaultSettings(DataUpdateAnimationProvider.CreateDefaultAnimationSettings(this.TableView));
            return SequentialAnimationHelper.GroupAnimationsByGeneration(factory.CreateAnimations());
        }

        internal bool HasDataUpdateFormatConditions() => 
            this.TableView.AnimateConditionalFormattingTransition || this.TableView.FormatConditions.HasDataUpdateFormatConditions();

        internal override bool HasRowConditions() => 
            this.FormatConditions.HasRowConditions();

        internal void InvalidateSelection()
        {
            base.View.CanSelectLocker.DoLockedAction(() => this.MouseMoveSelection.UpdateSelection(this.TableView));
        }

        private bool IsCellMergeCore(int visibleIndex1, int visibleIndex2, ColumnBase column, bool checkRowData, int checkMDIndex, bool includeFirstColumn = true)
        {
            if (!includeFirstColumn && (column.ActualVisibleIndex == 0))
            {
                return false;
            }
            if ((column.AllowCellMerge != null) ? !column.AllowCellMerge.Value : !this.TableView.AllowCellMerge)
            {
                return false;
            }
            int rowHandleByVisibleIndexCore = base.DataControl.GetRowHandleByVisibleIndexCore(checkMDIndex);
            if (base.DataControl.MasterDetailProvider.IsMasterRowExpanded(rowHandleByVisibleIndexCore, null) || this.UseRowDetailsTemplate(rowHandleByVisibleIndexCore))
            {
                return false;
            }
            int rowHandle = base.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex2);
            if (base.DataControl.IsGroupRowHandleCore(rowHandle) || !base.DataControl.IsValidRowHandleCore(rowHandle))
            {
                return false;
            }
            if (checkRowData)
            {
                RowData rowData = this.GetRowData(rowHandle);
                if ((rowData == null) || !rowData.IsRowInView())
                {
                    return false;
                }
            }
            int num3 = base.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex1);
            if (base.View.EditFormManager.IsEditFormVisible)
            {
                if ((this.TableView.EditFormShowMode == EditFormShowMode.Inline) && (rowHandleByVisibleIndexCore == base.View.FocusedRowHandle))
                {
                    return false;
                }
                if ((this.TableView.EditFormShowMode == EditFormShowMode.InlineHideRow) && (rowHandle == base.View.FocusedRowHandle))
                {
                    return false;
                }
            }
            return (this.TableView.AllowMergeEditor(column, rowHandle, num3) ? (((rowHandle == -2147483647) || (num3 == -2147483647)) ? false : ((base.View.GetFixedRowByVisibleIndex(visibleIndex1) == base.View.GetFixedRowByVisibleIndex(visibleIndex2)) ? this.TableView.RaiseCellMerge(column, rowHandle, num3, true).Value : false)) : false);
        }

        internal bool IsCellMerged(int visibleIndex1, int visibleIndex2, ColumnBase column, bool checkRowData, int checkMDIndex, bool includeFirstColumn = true)
        {
            bool result = false;
            this.CellMergeLocker.DoLockedActionIfNotLocked(delegate {
                result = this.IsCellMergeCore(visibleIndex1, visibleIndex2, column, checkRowData, checkMDIndex, includeFirstColumn);
            });
            return result;
        }

        public bool IsCellSelected(int rowHandle, ColumnBase column) => 
            this.IsValidRowHandleAndColumn(rowHandle, column) ? base.View.SelectionStrategy.IsCellSelected(rowHandle, column) : false;

        private bool IsNullOrDefaultTemplate(DataTemplate template) => 
            (template == null) || (template is DefaultDataTemplate);

        internal override bool IsRowIndicator(DependencyObject originalSource) => 
            this.TableView.CalcHitInfo(originalSource).IsRowIndicator;

        internal bool IsValidRowHandleAndColumn(int rowHandle, ColumnBase column) => 
            base.DataControl.IsValidRowHandleCore(rowHandle) && (column != null);

        internal override void MakeCellVisible()
        {
            this.HorizontalNavigationStrategy.MakeCellVisible(this);
        }

        public virtual void MakeColumnVisible(BaseColumn column)
        {
            if ((this.RootDataPresenter != null) && ((base.DataControl.BandsLayoutCore == null) || (column.IsBand || (column.BandRow != null))))
            {
                double left = -this.HorizontalOffset + this.GetActualColumnOffset(column);
                this.MakeRangeVisible(left, left + column.ActualHeaderWidth, this.UpdateColumnsStrategy.GetFixedStyle(column, this));
            }
        }

        public void MakeCurrentCellVisible()
        {
            DependencyObject currentCell = base.View.CurrentCell;
            if ((currentCell != null) && ((DataControlBase.FindCurrentView(currentCell) != null) && LayoutHelper.IsChildElement(base.View.RootView, currentCell)))
            {
                Rect relativeElementRect = LayoutHelper.GetRelativeElementRect((UIElement) currentCell, this.RootDataPresenter);
                double totalLeftIndent = this.GetTotalLeftIndent(true, false);
                this.MakeRangeVisible(relativeElementRect.Left - totalLeftIndent, relativeElementRect.Right - totalLeftIndent, TableViewProperties.GetFixedAreaStyle(currentCell));
            }
        }

        protected virtual void MakeRangeVisible(double left, double right, FixedStyle fixedStyle)
        {
            if (fixedStyle == FixedStyle.None)
            {
                double num = left - this.LeftIndent;
                if (this.FixedLeftVisibleColumns.Count != 0)
                {
                    num -= this.TableView.FixedLineWidth;
                }
                if (num < 0.0)
                {
                    this.RootDataPresenter.SetHorizontalOffsetForce(this.HorizontalOffset + num);
                }
                else
                {
                    double num2 = (right - this.LeftIndent) - this.HorizontalViewportCore;
                    if (num2 > 0.0)
                    {
                        if ((this.FixedRightVisibleColumns.Count > 0) || (this.FixedLeftVisibleColumns.Count != 0))
                        {
                            num2 -= this.TableView.FixedLineWidth;
                        }
                        double num3 = num - num2;
                        if (num3 < 0.0)
                        {
                            num2 += num3;
                        }
                        this.RootDataPresenter.SetHorizontalOffsetForce(this.HorizontalOffset + num2);
                    }
                }
            }
        }

        internal override void MoveNextRow()
        {
            if (!base.MoveNextRowCore() && base.View.CanMoveFromFocusedRow())
            {
                if (base.View.IsAdditionalRowFocused)
                {
                    if (this.IsAutoFilterRowFocused && this.IsNewItemRowVisible)
                    {
                        base.View.SetFocusedRowHandle(-2147483647);
                        return;
                    }
                    if (base.View.IsTopNewItemRowFocused && !base.View.CommitEditing())
                    {
                        return;
                    }
                    if (base.DataControl.VisibleRowCount > 0)
                    {
                        if (base.View.IsPagingMode)
                        {
                            base.View.SetFocusedRowHandle(base.DataControl.GetRowHandleByVisibleIndexCore(base.View.FirstVisibleIndexOnPage));
                            return;
                        }
                        base.View.SetFocusedRowHandle(base.DataControl.GetRowHandleByVisibleIndexCore(0));
                        return;
                    }
                }
                if (!base.View.IsBottomNewItemRowFocused || !this.IsNewItemRowEditing)
                {
                    base.DataControl.NavigateToNextOuterMasterRow();
                }
                else if (base.View.CommitEditing())
                {
                    base.View.ScrollIntoView(base.View.FocusedRowHandle);
                }
            }
        }

        internal override void MovePrevRow()
        {
            this.MovePrevRow(false);
        }

        internal void MovePrevRow(bool allowNavigateToAutoFilterRow)
        {
            if (!base.MovePrevRowCore())
            {
                if ((((base.View.DataProviderBase.CurrentIndex == 0) || (base.View.IsPagingMode && (base.View.DataProviderBase.CurrentIndex == base.View.FirstVisibleIndexOnPage))) && this.CanNavigateToAdditionalRow(allowNavigateToAutoFilterRow)) && !base.View.IsAdditionalRowFocused)
                {
                    base.View.SetFocusedRowHandle(this.GetAdditionalRowHandle());
                }
                else if (this.IsNewItemRowFocused && this.CanNavigateToAutoFilterRow(allowNavigateToAutoFilterRow))
                {
                    base.View.SetFocusedRowHandle(-2147483645);
                }
                else
                {
                    base.DataControl.NavigateToMasterRow();
                }
            }
        }

        internal override void NotifyBandsLayoutChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__98_0;
            if (<>c.<>9__98_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__98_0;
                updateMethod = <>c.<>9__98_0 = rowData => rowData.UpdateCellsPanel();
            }
            this.UpdateViewRowData(updateMethod);
        }

        internal override void NotifyFixedLeftBandsChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__100_0;
            if (<>c.<>9__100_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__100_0;
                updateMethod = <>c.<>9__100_0 = rowData => rowData.UpdateClientFixedLeftBands();
            }
            this.UpdateViewRowData(updateMethod);
        }

        internal override void NotifyFixedNoneBandsChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__99_0;
            if (<>c.<>9__99_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__99_0;
                updateMethod = <>c.<>9__99_0 = rowData => rowData.UpdateClientFixedNoneBands();
            }
            this.UpdateViewRowData(updateMethod);
        }

        internal override void NotifyFixedRightBandsChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__101_0;
            if (<>c.<>9__101_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__101_0;
                updateMethod = <>c.<>9__101_0 = rowData => rowData.UpdateClientFixedRightBands();
            }
            this.UpdateViewRowData(updateMethod);
        }

        internal void OnActualAllowCellMergeChanged()
        {
            base.View.OnMultiSelectModeChanged();
            UpdateRowDataDelegate updateMethod = <>c.<>9__483_0;
            if (<>c.<>9__483_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__483_0;
                updateMethod = <>c.<>9__483_0 = delegate (RowData rowData) {
                    rowData.UpdateSelectionState();
                    rowData.UpdateCellsPanel();
                };
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnActualAlternateRowBackgroundChanged()
        {
            if (base.DataControl != null)
            {
                base.DataControl.UpdateRowsCore(true, true);
            }
            UpdateRowDataDelegate updateMethod = <>c.<>9__456_0;
            if (<>c.<>9__456_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__456_0;
                updateMethod = <>c.<>9__456_0 = x => x.UpdateClientAlternateBackground();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnActualDataRowTemplateSelectorChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__307_0;
            if (<>c.<>9__307_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__307_0;
                updateMethod = <>c.<>9__307_0 = x => x.UpdateContent();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnActualIndicatorWidthChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__302_0;
            if (<>c.<>9__302_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__302_0;
                updateMethod = <>c.<>9__302_0 = x => x.UpdateClientIndicatorWidth();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnActualShowIndicatorChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__303_0;
            if (<>c.<>9__303_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__303_0;
                updateMethod = <>c.<>9__303_0 = x => x.UpdateClientShowIndicator();
            }
            this.UpdateViewRowData(updateMethod);
            this.UpdateBandsIndicator();
        }

        internal override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
        {
            base.OnAfterMouseLeftButtonDown(hitInfo);
            if (!this.TableView.IsEditing && (Mouse.RightButton == MouseButtonState.Released))
            {
                this.SetMouseMoveSelectionStrategy(this.GetMouseMoveSelection(hitInfo));
                this.MouseMoveSelection.OnMouseDown(base.View, hitInfo);
            }
        }

        internal void OnAllowHorizontalScrollingVirtualizationChanged()
        {
            base.View.UpdateColumnsPositions();
            base.View.UpdateCellData();
        }

        private void OnAlternateRowPropertiesChanged()
        {
            Action<DataViewBase> action = <>c.<>9__455_0;
            if (<>c.<>9__455_0 == null)
            {
                Action<DataViewBase> local1 = <>c.<>9__455_0;
                action = <>c.<>9__455_0 = view => view.UpdateAlternateRowBackground();
            }
            base.View.Do<DataViewBase>(action);
        }

        internal void OnAutoWidthChanged()
        {
            this.ClearColumnsLayoutCalculator();
            base.View.UpdateColumnsPositions();
            if (!base.View.IsLockUpdateColumnsLayout)
            {
                this.UpdateCellData();
                if (base.View.DataPresenter != null)
                {
                    base.View.DataPresenter.UpdateAutoSize();
                }
            }
        }

        internal override void OnBandsLayoutChanged()
        {
            base.View.UpdateActualAllowCellMergeCore();
            this.ClearColumnsLayoutCalculator();
        }

        protected internal override void OnCancelRowEdit()
        {
            base.OnCancelRowEdit();
            this.UpdateFocusedRowIndicator();
        }

        internal override void OnCellContentPresenterRowChanged(FrameworkElement presenter)
        {
            BarManager.SetDXContextMenu(presenter, base.View.DataControlMenu);
            DataControlPopupMenu.SetGridMenuType(presenter, 2);
        }

        protected internal virtual void OnCellItemsControlLoaded()
        {
        }

        internal override void OnColumnResizerDoubleClick(BaseColumn column)
        {
            base.OnColumnResizerDoubleClick(column);
            this.BestFitColumnIfAllowed(column, column.IsBand || (base.DataControl.BandsCore.Count == 0));
        }

        private void OnCompactModeFilterItemsSourceUpdate(object oldValue)
        {
            INotifyCollectionChanged changed = oldValue as INotifyCollectionChanged;
            if (changed != null)
            {
                changed.CollectionChanged -= this.CompactModeFilterItemsCollectionChangedHandler.Handler;
            }
            this.TableView.UnsubscribeFilterItemsSource(oldValue as IEnumerable);
            Func<object, object> convertItemAction = <>c.<>9__336_0;
            if (<>c.<>9__336_0 == null)
            {
                Func<object, object> local1 = <>c.<>9__336_0;
                convertItemAction = <>c.<>9__336_0 = obj => obj as ICustomItem;
            }
            SyncCollectionHelper.PopulateCore(this.TableView.CompactModeFilterItems, this.TableView.CompactModeFilterItemsSource, convertItemAction, null, null);
            INotifyCollectionChanged compactModeFilterItemsSource = this.TableView.CompactModeFilterItemsSource as INotifyCollectionChanged;
            if (compactModeFilterItemsSource != null)
            {
                compactModeFilterItemsSource.CollectionChanged += this.CompactModeFilterItemsCollectionChangedHandler.Handler;
            }
        }

        protected internal override void OnCurrentCellEditCancelled()
        {
            base.OnCurrentCellEditCancelled();
            this.UpdateFocusedRowIndicator();
        }

        protected internal virtual void OnDeserializeCreateFormatCondition(XtraCreateCollectionItemEventArgs e)
        {
            XtraPropertyInfo info = e.Item.ChildProperties["TypeName"];
            if (info != null)
            {
                FormatConditionBase owner = this.CreateFormatCondition(info.Value.ToString());
                if (owner != null)
                {
                    owner.OnDeserializeStart();
                    this.CreateInternalContentObjectOnDeserialization(e, owner.FormatPropertyForBinding, owner);
                    this.CreateInternalContentObjectOnDeserialization(e, owner.ActualAnimationSettingsProperty, owner);
                    this.FormatConditions.Add(owner);
                    e.CollectionItem = owner;
                }
            }
        }

        protected internal virtual void OnDeserializeFormatConditionsEnd()
        {
            foreach (FormatConditionBase base2 in this.FormatConditions)
            {
                base2.OnDeserializeEnd();
            }
            this.FormatConditions.EndUpdate();
        }

        protected internal virtual void OnDeserializeFormatConditionsStart()
        {
            this.FormatConditions.BeginUpdate();
        }

        internal override void OnDoubleClick(MouseButtonEventArgs e)
        {
            base.OnDoubleClick(e);
            ITableViewHitInfo hitInfo = ((ITableView) base.View.RootView).CalcHitInfo(e.OriginalSource as DependencyObject);
            if (hitInfo.InRow)
            {
                base.View.EditFormManager.OnDoubleClick(e);
                this.TableView.RaiseRowDoubleClickEvent(hitInfo, e.ChangedButton);
            }
        }

        protected internal override void OnEditorActivated()
        {
            base.OnEditorActivated();
            this.UpdateFocusedRowIndicator();
        }

        internal static void OnFadeSelectionOnLostFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ITableView) d).TableViewBehavior.UpdateActualFadeSelectionOnLostFocus();
        }

        private void OnFixedLeftContentWidthChanged()
        {
        }

        private void OnFixedLineWidthChanged()
        {
            base.View.UpdateColumnsPositions();
            UpdateRowDataDelegate updateMethod = <>c.<>9__300_0;
            if (<>c.<>9__300_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__300_0;
                updateMethod = <>c.<>9__300_0 = x => x.UpdateClientFixedLineWidth();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnFixedNoneContentWidthChanged()
        {
            this.UpdateServiceRowData(rowData => this.UpdateFixedNoneContentWidth(rowData));
            this.UpdateFixedNoneContentWidth(base.View.HeadersData);
            base.View.UpdateRowData(rowData => this.UpdateFixedNoneContentWidth(rowData), true, true);
        }

        private void OnFixedRightContentWidthChanged()
        {
        }

        private void OnFixedVisibleColumnsChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__301_0;
            if (<>c.<>9__301_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__301_0;
                updateMethod = <>c.<>9__301_0 = x => x.UpdateClientFixedLineVisibility();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private static void OnFixedVisibleColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TableViewBehavior tableViewBehavior = ((ITableView) d).TableViewBehavior;
            if (tableViewBehavior != null)
            {
                tableViewBehavior.OnFixedVisibleColumnsChanged();
            }
        }

        protected internal override void OnFocusedRowCellModified()
        {
            base.OnFocusedRowCellModified();
            this.UpdateFocusedRowIndicator();
        }

        protected internal override void OnGotKeyboardFocus()
        {
            base.OnGotKeyboardFocus();
            if ((base.View.DataProviderBase.DataRowCount == 0) && ((base.View.FocusedRowHandle == -2147483648) && ((base.View.DataControl != null) && (base.View.IsRootView || !base.View.DataControl.IsOriginationDataControl()))))
            {
                if (this.TableView.ShowAutoFilterRow)
                {
                    base.View.SetFocusedRowHandle(-2147483645);
                }
                else if (this.IsNewItemRowVisible || base.View.ShouldDisplayBottomRow)
                {
                    base.View.SetFocusedRowHandle(-2147483647);
                }
            }
        }

        protected internal override void OnHideEditor(CellEditorBase editor)
        {
            base.OnHideEditor(editor);
            this.UpdateFocusedRowIndicator();
        }

        internal void OnHideEditor(CellEditorBase editor, bool closeEditor)
        {
            if (base.View.ActualAllowCellMerge)
            {
                base.View.UpdateFocusAndInvalidatePanels();
            }
        }

        internal static void OnIndicatorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ITableView) d).TableViewBehavior.UpdateActualIndicatorWidth();
            ((ITableView) d).ViewBase.UpdateColumnsPositions();
        }

        private void OnIsCompactModeChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__337_0;
            if (<>c.<>9__337_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__337_0;
                updateMethod = <>c.<>9__337_0 = rowData => rowData.UpdateClientSummary();
            }
            base.View.UpdateRowData(updateMethod, true, true);
        }

        internal override void OnMouseLeftButtonUp()
        {
            this.StopSelection();
        }

        internal static void OnMultiSelectModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ITableView) d).ViewBase.OnMultiSelectModeChanged();
        }

        protected internal void OnOpeningEditor()
        {
            if (base.View.ActualAllowCellMerge)
            {
                base.View.UpdateFocusAndInvalidatePanels();
            }
        }

        internal override void OnResizingComplete()
        {
            base.View.DesignTimeAdorner.OnColumnResized();
            base.View.RaiseResizingComplete();
        }

        protected internal virtual void OnRowDecorationTemplateChanged()
        {
        }

        private void OnRowIndicatorContentTemplateChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__306_0;
            if (<>c.<>9__306_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__306_0;
                updateMethod = <>c.<>9__306_0 = x => x.UpdateIndicatorContentTemplate();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnRowMinHeightChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__305_0;
            if (<>c.<>9__305_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__305_0;
                updateMethod = <>c.<>9__305_0 = x => x.UpdateClientMinHeight();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnRowStyleChanged(Style newStyle)
        {
            this.ValidateRowStyle(newStyle);
            UpdateRowDataDelegate updateMethod = <>c.<>9__453_0;
            if (<>c.<>9__453_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__453_0;
                updateMethod = <>c.<>9__453_0 = rowData => rowData.UpdateClientRowStyle();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnScrollingVirtualizationMarginChanged()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__297_0;
            if (<>c.<>9__297_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__297_0;
                updateMethod = <>c.<>9__297_0 = x => x.UpdateClientScrollingMargin();
            }
            this.UpdateViewRowData(updateMethod);
        }

        public virtual void OnScrollTimer_Tick(object sender, EventArgs e)
        {
            if (((base.View != null) && (base.View.ScrollContentPresenter != null)) && ((this.LastMousePosition.X != double.NaN) && (this.LastMousePosition.Y != double.NaN)))
            {
                int num = 0;
                int num2 = 0;
                this.DragScroll();
                if (this.MouseMoveSelection.CanScrollVertically)
                {
                    FixedRowPosition position = base.View.HasFixedRows ? base.View.GetFixedRowByItemCore(base.View.DataControl.GetRow(base.View.SelectionAnchor.RowHandleCore)) : FixedRowPosition.None;
                    if ((this.LastMousePosition.Y < this.DataPresenter.Panel.FixedTopRowsHeight) && (position != FixedRowPosition.Top))
                    {
                        num = -1;
                    }
                    if ((this.LastMousePosition.Y > (base.View.ScrollContentPresenter.ActualHeight - this.DataPresenter.Panel.FixedBottomRowsHeight)) && (position != FixedRowPosition.Bottom))
                    {
                        num = 1;
                    }
                }
                if (this.MouseMoveSelection.CanScrollHorizontally)
                {
                    double num3 = this.TableView.ShowIndicator ? this.TableView.IndicatorWidth : 0.0;
                    if ((this.LastMousePosition.X - num3) < 0.0)
                    {
                        num2 = -10;
                    }
                    if (this.LastMousePosition.X > base.View.ScrollContentPresenter.ActualWidth)
                    {
                        num2 = 10;
                    }
                }
                if (num2 != 0)
                {
                    this.ChangeHorizontalOffsetBy((double) num2);
                }
                if (num != 0)
                {
                    this.ChangeVerticalOffsetBy((double) num);
                }
                if ((num != 0) || (num2 != 0))
                {
                    base.View.EnqueueImmediateAction(() => this.InvalidateSelection());
                }
            }
        }

        internal void OnShowAutoFilterRowChanged()
        {
            if (base.View.DataControl != null)
            {
                base.View.DataControl.ValidateMasterDetailConsistency();
            }
            if (this.TableView.ShowAutoFilterRow && (base.View.DataControl != null))
            {
                this.AutoFilterRowData.UpdateData();
            }
            else if (base.View.IsAutoFilterRowFocused)
            {
                base.View.SetFocusedRowHandle(base.DataControl.GetRowHandleByVisibleIndexCore(0));
            }
            if (this.AutoFilterRowData is AdditionalRowData)
            {
                ((AdditionalRowData) this.AutoFilterRowData).UpdateVisible();
            }
        }

        internal void OnShowCriteriaInAutoFilterRowChanged()
        {
            if (this.TableView.ShowAutoFilterRow && (base.View.DataControl != null))
            {
                this.AutoFilterRowData.UpdateData();
            }
            Action<ColumnBase> updateColumnDelegate = <>c.<>9__352_0;
            if (<>c.<>9__352_0 == null)
            {
                Action<ColumnBase> local1 = <>c.<>9__352_0;
                updateColumnDelegate = <>c.<>9__352_0 = col => col.ActualShowCriteriaInAutoFilterRowChanged();
            }
            base.View.UpdateColumns(updateColumnDelegate);
        }

        private void OnShowHorizontalLinesChanged()
        {
            base.View.UpdateContentLayout();
            UpdateRowDataDelegate updateMethod = <>c.<>9__298_0;
            if (<>c.<>9__298_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__298_0;
                updateMethod = <>c.<>9__298_0 = x => x.UpdateHorizontalLineVisibility();
            }
            this.UpdateViewRowData(updateMethod);
        }

        internal static void OnShowIndicatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ITableView) d).TableViewBehavior.UpdateActualShowIndicator();
            ((ITableView) d).TableViewBehavior.UpdateShowTotalSummaryIndicatorIndent((ITableView) d);
            ((ITableView) d).TableViewBehavior.UpdateExpandColumnPosition();
            ((ITableView) d).ViewBase.UpdateColumnsPositions();
        }

        private void OnShowVerticalLinesChanged()
        {
            base.View.UpdateContentLayout();
            UpdateRowDataDelegate updateMethod = <>c.<>9__299_0;
            if (<>c.<>9__299_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__299_0;
                updateMethod = <>c.<>9__299_0 = x => x.UpdateClientVerticalLineVisibility();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void OnTotalGroupAreaIndentChanged()
        {
        }

        private void OnUseLightweightTemplatesChanged()
        {
            if (!this.canChangeUseLightweightTemplates && !DataViewBase.DisableOptimizedModeVerification)
            {
                throw new InvalidOperationException("Can't change the UseLightweightTemplates property after the GridControl has been initialized.");
            }
            base.View.UpdateColumnsAppearance();
            this.UpdateActualDataRowTemplateSelector();
            this.ValidateRowStyle(this.TableView.RowStyle);
        }

        private void OnVerticalScrollBarWidthChanged()
        {
            base.View.OnVerticalScrollBarWidthChanged();
        }

        internal override void OnViewMouseLeave()
        {
            this.MouseMoveSelection.CaptureMouse(base.View);
        }

        internal override void OnViewMouseMove(MouseEventArgs e)
        {
            if (!MouseHelper.IsMouseLeftButtonPressed(e))
            {
                this.StopSelection();
            }
            this.LastMousePosition = e.GetPosition(base.View.ScrollContentPresenter);
            this.InvalidateSelection();
        }

        protected internal override bool OnVisibleColumnsAssigned(bool changed)
        {
            changed = this.UpdateFixedColumns(changed);
            if (this.DataPresenter != null)
            {
                this.DataPresenter.UpdateSecondarySizeScrollInfo(true);
            }
            base.View.DataControl.ResetGridChildPeersIfNeeded();
            return changed;
        }

        internal override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonUp(e);
            this.StopSelection();
        }

        internal override void ProcessPreviewKeyDown(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDown(e);
            base.View.EditFormManager.OnPreviewKeyDown(e);
            if ((e.Key == Key.Delete) && (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && (base.View.IsAutoFilterRowFocused && (base.DataControl.CurrentColumn != null))))
            {
                base.DataControl.ClearColumnFilter(base.DataControl.CurrentColumn);
            }
        }

        internal abstract CustomBestFitEventArgsBase RaiseCustomBestFit(ColumnBase column, BestFitMode bestFitMode);
        protected override void RebuildColumnChooserColumnsCore()
        {
            if (base.DataControl.BandsLayoutCore == null)
            {
                base.RebuildColumnChooserColumnsCore();
            }
            else
            {
                base.DataControl.BandsLayoutCore.RebuildColumnChooserColumns();
            }
        }

        internal static DependencyPropertyKey RegisterActualAlternateRowBackgroundProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__56_0;
            if (<>c.<>9__56_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__56_0;
                propertyChangedCallback = <>c.<>9__56_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnActualAlternateRowBackgroundChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ActualAlternateRowBackground", typeof(Brush), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterActualDataRowTemplateSelectorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__26_0;
                propertyChangedCallback = <>c.<>9__26_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnActualDataRowTemplateSelectorChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ActualDataRowTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterActualIndicatorWidthPropertyKey(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__6_0;
                propertyChangedCallback = <>c.<>9__6_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs _) {
                    ((ITableView) d).TableViewBehavior.OnActualIndicatorWidthChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ActualIndicatorWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(16.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterActualRowDetailsTemplateSelectorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__493_0;
            if (<>c.<>9__493_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__493_0;
                propertyChangedCallback = <>c.<>9__493_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateDetails();
                };
            }
            return DependencyProperty.RegisterReadOnly("ActualRowDetailsTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterActualShowIndicatorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__9_0;
                propertyChangedCallback = <>c.<>9__9_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs _) {
                    ((ITableView) d).TableViewBehavior.OnActualShowIndicatorChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ActualShowIndicator", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowAdvancedHorizontalNavigationProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__67_0;
            if (<>c.<>9__67_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__67_0;
                propertyChangedCallback = <>c.<>9__67_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowAdvancedHorizontalNavigation", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowAdvancedVerticalNavigationProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__66_0;
            if (<>c.<>9__66_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__66_0;
                propertyChangedCallback = <>c.<>9__66_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowAdvancedVerticalNavigation", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowBandMovingProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__64_0;
                propertyChangedCallback = <>c.<>9__64_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowBandMoving", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowBandMultiRowProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__83_0;
            if (<>c.<>9__83_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__83_0;
                propertyChangedCallback = <>c.<>9__83_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowBandMultiRow", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowBandResizingProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__65_0;
            if (<>c.<>9__65_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__65_0;
                propertyChangedCallback = <>c.<>9__65_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowBandResizing", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowBestFitProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowBestFit", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));

        internal static DependencyProperty RegisterAllowCascadeUpdateProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowCascadeUpdate", typeof(bool), ownerType, new PropertyMetadata(false));

        internal static DependencyProperty RegisterAllowChangeBandParentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__62_0;
            if (<>c.<>9__62_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__62_0;
                propertyChangedCallback = <>c.<>9__62_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowChangeBandParent", typeof(bool), ownerType, new PropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowChangeColumnParentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__61_0;
                propertyChangedCallback = <>c.<>9__61_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("AllowChangeColumnParent", typeof(bool), ownerType, new PropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowConditionalFormattingManagerProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowConditionalFormattingManager", typeof(bool), ownerType, new PropertyMetadata(true));

        internal static DependencyProperty RegisterAllowConditionalFormattingMenuProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowConditionalFormattingMenu", typeof(bool), ownerType, new PropertyMetadata(null));

        internal static DependencyProperty RegisterAllowDataUpdateFormatConditionMenuProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("AllowDataUpdateFormatConditionMenu", typeof(bool), typeof(T), new PropertyMetadata(false));

        internal static DependencyProperty RegisterAllowFixedColumnMenuProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowFixedColumnMenu", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));

        internal static DependencyProperty RegisterAllowHorizontalScrollingVirtualizationProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__23_0;
                propertyChangedCallback = <>c.<>9__23_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnAllowHorizontalScrollingVirtualizationChanged();
                };
            }
            return DependencyPropertyManager.Register("AllowHorizontalScrollingVirtualization", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowPerPixelScrollingProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__43_0;
                propertyChangedCallback = <>c.<>9__43_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((DataViewBase) d).OnAllowPerPixelScrollingChanged();
                };
            }
            return DependencyPropertyManager.Register("AllowPerPixelScrolling", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAllowResizingProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowResizing", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));

        internal static DependencyProperty RegisterAllowScrollAnimationProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowScrollAnimation", typeof(bool), ownerType, new PropertyMetadata(false));

        internal static DependencyProperty RegisterAllowScrollHeadersProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AllowScrollHeaders", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));

        internal static DependencyProperty RegisterAlternateRowBackgroundProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__55_0;
            if (<>c.<>9__55_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__55_0;
                propertyChangedCallback = <>c.<>9__55_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
                };
            }
            return DependencyPropertyManager.Register("AlternateRowBackground", typeof(Brush), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAlternationCountProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__59_0;
            if (<>c.<>9__59_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__59_0;
                propertyChangedCallback = <>c.<>9__59_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnActualAlternateRowBackgroundChanged();
                };
            }
            return DependencyPropertyManager.Register("AlternationCount", typeof(int), ownerType, new PropertyMetadata(2, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterAnimateConditionalFormattingTransitionProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("AnimateConditionalFormattingTransition", typeof(bool), typeof(T), new PropertyMetadata(false));

        internal static DependencyProperty RegisterAutoMoveRowFocusProperty(Type ownerType) => 
            DependencyPropertyManager.Register("AutoMoveRowFocus", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));

        internal static DependencyProperty RegisterAutoWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__52_0;
                propertyChangedCallback = <>c.<>9__52_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnAutoWidthChanged();
                };
            }
            return DependencyPropertyManager.Register("AutoWidth", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterBandHeaderTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__69_0;
            if (<>c.<>9__69_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__69_0;
                propertyChangedCallback = <>c.<>9__69_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("BandHeaderTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterBandHeaderTemplateSelectorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__70_0;
                propertyChangedCallback = <>c.<>9__70_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("BandHeaderTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterBandHeaderToolTipTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__71_0;
                propertyChangedCallback = <>c.<>9__71_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("BandHeaderToolTipTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterColumnBandChooserTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__54_0;
                propertyChangedCallback = <>c.<>9__54_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((DataViewBase) d).UpdateActualColumnChooserTemplate();
                };
            }
            return DependencyPropertyManager.Register("ColumnBandChooserTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterColumnChooserBandsSortOrderComparerProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__68_0;
            if (<>c.<>9__68_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__68_0;
                propertyChangedCallback = <>c.<>9__68_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("ColumnChooserBandsSortOrderComparer", typeof(IComparer<BandBase>), ownerType, new PropertyMetadata(DefaultColumnChooserBandsSortOrderComparer.Instance, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterCompactModeFilterItemsSource(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__95_0;
            if (<>c.<>9__95_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__95_0;
                propertyChangedCallback = <>c.<>9__95_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnCompactModeFilterItemsSourceUpdate(e.OldValue);
                };
            }
            return DependencyProperty.Register("CompactModeFilterItemsSource", typeof(IEnumerable), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterCompactPanelShowMode(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__94_0;
            if (<>c.<>9__94_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__94_0;
                propertyChangedCallback = <>c.<>9__94_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateCompactPanelShowMode();
                };
            }
            return DependencyPropertyManager.Register("CompactPanelShowMode", typeof(CompactPanelShowMode), ownerType, new PropertyMetadata(CompactPanelShowMode.CompactMode, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterConditionalFormattingTransitionDurationProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("ConditionalFormattingTransitionDuration", typeof(Duration), typeof(T), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(1.0))));

        internal static RoutedEvent RegisterCustomScrollAnimationEvent(Type ownerType) => 
            EventManager.RegisterRoutedEvent("CustomScrollAnimation", RoutingStrategy.Direct, typeof(CustomScrollAnimationEventHandler), ownerType);

        internal static DependencyProperty RegisterDataRowCompactTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__29_0;
                propertyChangedCallback = <>c.<>9__29_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateCompactModeCore();
                };
            }
            return DependencyPropertyManager.Register("DataRowCompactTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterDataRowTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__28_0;
                propertyChangedCallback = <>c.<>9__28_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateActualDataRowTemplateSelector();
                };
            }
            return DependencyPropertyManager.Register("DataRowTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterDataRowTemplateSelectorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__27_0;
                propertyChangedCallback = <>c.<>9__27_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateActualDataRowTemplateSelector();
                };
            }
            return DependencyPropertyManager.Register("DataRowTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterDataUpdateAnimationHideDurationProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("DataUpdateAnimationHideDuration", typeof(Duration), typeof(T), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(200.0))));

        internal static DependencyProperty RegisterDataUpdateAnimationHoldDurationProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("DataUpdateAnimationHoldDuration", typeof(Duration), typeof(T), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(600.0))));

        internal static DependencyProperty RegisterDataUpdateAnimationShowDurationProperty<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("DataUpdateAnimationShowDuration", typeof(Duration), typeof(T), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(200.0))));

        internal static DependencyProperty RegisterDefaultDataRowTemplateProperty(Type ownerType) => 
            DependencyPropertyManager.Register("DefaultDataRowTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));

        internal static DependencyProperty RegisterEvenRowBackgroundProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__57_0;
            if (<>c.<>9__57_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__57_0;
                propertyChangedCallback = <>c.<>9__57_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
                };
            }
            return DependencyPropertyManager.Register("EvenRowBackground", typeof(Brush), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterExtendScrollBarToFixedColumnsProperty(Type ownerType) => 
            DependencyPropertyManager.Register("ExtendScrollBarToFixedColumns", typeof(bool), ownerType, new PropertyMetadata(false));

        internal static DependencyPropertyKey RegisterFixedLeftContentWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__17_0;
                propertyChangedCallback = <>c.<>9__17_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnFixedLeftContentWidthChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("FixedLeftContentWidth", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterFixedLeftVisibleColumnsProperty<T>(Type ownerType) where T: ColumnBase => 
            DependencyPropertyManager.RegisterReadOnly("FixedLeftVisibleColumns", typeof(IList<T>), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(TableViewBehavior.OnFixedVisibleColumnsChanged)));

        internal static DependencyProperty RegisterFixedLineWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__34_0;
                propertyChangedCallback = <>c.<>9__34_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnFixedLineWidthChanged();
                };
            }
            return DependencyPropertyManager.Register("FixedLineWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(2.0, propertyChangedCallback, <>c.<>9__34_1 ??= ((CoerceValueCallback) ((d, value) => Math.Max(0.0, (double) value)))));
        }

        internal static DependencyPropertyKey RegisterFixedNoneContentWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__14_0;
                propertyChangedCallback = <>c.<>9__14_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnFixedNoneContentWidthChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("FixedNoneContentWidth", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterFixedNoneVisibleColumnsProperty<T>(Type ownerType) where T: ColumnBase => 
            DependencyPropertyManager.RegisterReadOnly("FixedNoneVisibleColumns", typeof(IList<T>), ownerType, new FrameworkPropertyMetadata(null));

        internal static DependencyPropertyKey RegisterFixedRightContentWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__18_0;
                propertyChangedCallback = <>c.<>9__18_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnFixedRightContentWidthChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("FixedRightContentWidth", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterFixedRightVisibleColumnsProperty<T>(Type ownerType) where T: ColumnBase => 
            DependencyPropertyManager.RegisterReadOnly("FixedRightVisibleColumns", typeof(IList<T>), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(TableViewBehavior.OnFixedVisibleColumnsChanged)));

        internal static DependencyProperty RegisterFocusedRowBorderTemplateProperty(Type ownerType) => 
            DependencyPropertyManager.Register("FocusedRowBorderTemplate", typeof(ControlTemplate), ownerType);

        internal static DependencyPropertyKey RegisterFooterRowStyle(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("FooterRowStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));

        internal static DependencyPropertyKey RegisterFooterSummaryContentStyle(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("FooterSummaryContentStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));

        internal static DependencyProperty RegisterFormatConditionGeneratorTemplateProperty<T>(DependencyProperty itemsAttachedBehaviorProperty) where T: DependencyObject => 
            DependencyProperty.Register("FormatConditionGeneratorTemplate", typeof(DataTemplate), typeof(T), new PropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ItemsAttachedBehaviorCore<T, FormatConditionBase>.OnItemsGeneratorTemplatePropertyChanged(d, e, itemsAttachedBehaviorProperty);
            }));

        internal static DependencyProperty RegisterFormatConditionGeneratorTemplateSelectorProperty<T>(DependencyProperty itemsAttachedBehaviorProperty) where T: DependencyObject => 
            DependencyProperty.Register("FormatConditionGeneratorTemplateSelector", typeof(DataTemplateSelector), typeof(T), new PropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ItemsAttachedBehaviorCore<T, FormatConditionBase>.OnItemsGeneratorTemplatePropertyChanged(d, e, itemsAttachedBehaviorProperty);
            }));

        internal static DependencyProperty RegisterFormatConditionsItemsAttachedBehaviorProperty<T>() where T: DependencyObject => 
            DependencyProperty.Register("FormatConditionsItemsAttachedBehavior", typeof(ItemsAttachedBehaviorCore<T, FormatConditionBase>), typeof(T), new PropertyMetadata(null));

        internal static DependencyProperty RegisterFormatConditionsSourceProperty<T>(DependencyProperty itemsAttachedBehaviorProperty, DependencyProperty generatorTemplateProperty, DependencyProperty generatorTemplateSelectorProperty) where T: DependencyObject, ITableView => 
            DependencyProperty.Register("FormatConditionsSource", typeof(IEnumerable), typeof(T), new PropertyMetadata(delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Func<T, IList> getTargetFunction = <>c__80<T>.<>9__80_1;
                if (<>c__80<T>.<>9__80_1 == null)
                {
                    Func<T, IList> local1 = <>c__80<T>.<>9__80_1;
                    getTargetFunction = <>c__80<T>.<>9__80_1 = x => x.FormatConditions;
                }
                ItemsAttachedBehaviorCore<T, FormatConditionBase>.OnItemsSourcePropertyChanged(d, e, itemsAttachedBehaviorProperty, generatorTemplateProperty, generatorTemplateSelectorProperty, null, getTargetFunction, <>c__80<T>.<>9__80_2 ??= x => new FormatCondition(), null, null, null, null, true, true, null, false);
            }));

        internal static DependencyProperty RegisterHeaderPanelMinHeightProperty(Type ownerType) => 
            DependencyPropertyManager.Register("HeaderPanelMinHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(20.0));

        internal static DependencyPropertyKey RegisterHorizontalViewportProperty(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("HorizontalViewport", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));

        internal static DependencyPropertyKey RegisterIndicatorHeaderWidthProperty(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("IndicatorHeaderWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));

        internal static DependencyProperty RegisterIndicatorWidthProperty(Type ownerType) => 
            DependencyPropertyManager.Register("IndicatorWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(16.0, new PropertyChangedCallback(TableViewBehavior.OnIndicatorWidthChanged)));

        internal static DependencyPropertyKey RegisterIsCompactMode(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__96_0;
            if (<>c.<>9__96_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__96_0;
                propertyChangedCallback = <>c.<>9__96_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnIsCompactModeChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("IsCompactMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterLeftDataAreaIndentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__51_0;
                propertyChangedCallback = <>c.<>9__51_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((DataViewBase) d).UpdateColumnsPositions();
                };
            }
            return DependencyPropertyManager.Register("LeftDataAreaIndent", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterMultiSelectModeProperty(Type ownerType) => 
            DependencyPropertyManager.Register("MultiSelectMode", typeof(TableViewSelectMode), ownerType, new FrameworkPropertyMetadata(TableViewSelectMode.None, new PropertyChangedCallback(TableViewBehavior.OnMultiSelectModeChanged)));

        internal static DependencyProperty RegisterPredefinedColorScaleFormatsProperty(Type ownerType) => 
            DependencyPropertyManager.Register("PredefinedColorScaleFormats", typeof(FormatInfoCollection), ownerType, new PropertyMetadata(null));

        internal static DependencyProperty RegisterPredefinedDataBarFormatsProperty(Type ownerType) => 
            DependencyPropertyManager.Register("PredefinedDataBarFormats", typeof(FormatInfoCollection), ownerType, new PropertyMetadata(null));

        internal static DependencyProperty RegisterPredefinedFormatsProperty(Type ownerType) => 
            DependencyPropertyManager.Register("PredefinedFormats", typeof(FormatInfoCollection), ownerType, new PropertyMetadata(null));

        internal static DependencyProperty RegisterPredefinedIconSetFormatsProperty(Type ownerType) => 
            DependencyPropertyManager.Register("PredefinedIconSetFormats", typeof(FormatInfoCollection), ownerType, new PropertyMetadata(null));

        internal static DependencyProperty RegisterPrintBandHeaderStyleProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__72_0;
            if (<>c.<>9__72_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__72_0;
                propertyChangedCallback = <>c.<>9__72_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("PrintBandHeaderStyle", typeof(Style), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRightDataAreaIndentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__49_0;
                propertyChangedCallback = <>c.<>9__49_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((DataViewBase) d).UpdateColumnsPositions();
                };
            }
            return DependencyPropertyManager.Register("RightDataAreaIndent", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowDecorationTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__31_0;
                propertyChangedCallback = <>c.<>9__31_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnRowDecorationTemplateChanged();
                };
            }
            return DependencyPropertyManager.Register("RowDecorationTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowDetailsTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__491_0;
            if (<>c.<>9__491_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__491_0;
                propertyChangedCallback = <>c.<>9__491_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateActualRowDetailsTemplateSelector();
                };
            }
            return DependencyProperty.Register("RowDetailsTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowDetailsTemplateSelectorProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__492_0;
            if (<>c.<>9__492_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__492_0;
                propertyChangedCallback = <>c.<>9__492_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateActualRowDetailsTemplateSelector();
                };
            }
            return DependencyProperty.Register("RowDetailsTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowDetailsVisibilityModeProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__494_0;
            if (<>c.<>9__494_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__494_0;
                propertyChangedCallback = <>c.<>9__494_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateDetails();
                };
            }
            return DependencyProperty.Register("RowDetailsVisibilityMode", typeof(RowDetailsVisibilityMode), ownerType, new FrameworkPropertyMetadata(RowDetailsVisibilityMode.VisibleWhenFocused, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowIndicatorContentTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__25_0;
                propertyChangedCallback = <>c.<>9__25_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnRowIndicatorContentTemplateChanged();
                };
            }
            return DependencyPropertyManager.Register("RowIndicatorContentTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowMinHeightProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__12_0;
                propertyChangedCallback = <>c.<>9__12_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs _) {
                    ((ITableView) d).TableViewBehavior.OnRowMinHeightChanged();
                };
            }
            return DependencyPropertyManager.Register("RowMinHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(20.0, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterRowStyleProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__22_0;
                propertyChangedCallback = <>c.<>9__22_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnRowStyleChanged((Style) e.NewValue);
                };
            }
            return DependencyPropertyManager.Register("RowStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterScrollAnimationDurationProperty(Type ownerType) => 
            DependencyPropertyManager.Register("ScrollAnimationDuration", typeof(double), ownerType, new PropertyMetadata(350.0));

        internal static DependencyProperty RegisterScrollAnimationModeProperty(Type ownerType) => 
            DependencyPropertyManager.Register("ScrollAnimationMode", typeof(DevExpress.Xpf.Grid.ScrollAnimationMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ScrollAnimationMode.EaseOut));

        internal static DependencyPropertyKey RegisterScrollingHeaderVirtualizationMarginProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__20_0;
                propertyChangedCallback = <>c.<>9__20_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnScrollingVirtualizationMarginChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ScrollingHeaderVirtualizationMargin", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterScrollingVirtualizationMarginProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__21_0;
                propertyChangedCallback = <>c.<>9__21_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnScrollingVirtualizationMarginChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("ScrollingVirtualizationMargin", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowAutoFilterRowProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__40_0;
            if (<>c.<>9__40_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__40_0;
                propertyChangedCallback = <>c.<>9__40_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnShowAutoFilterRowChanged();
                };
            }
            return DependencyPropertyManager.Register("ShowAutoFilterRow", typeof(bool), ownerType, new PropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowBandsInCustomizationFormProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__63_0;
                propertyChangedCallback = <>c.<>9__63_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("ShowBandsInCustomizationForm", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowBandsPanelProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__60_0;
            if (<>c.<>9__60_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__60_0;
                propertyChangedCallback = <>c.<>9__60_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
                };
            }
            return DependencyPropertyManager.Register("ShowBandsPanel", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowCriteriaInAutoFilterRowProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__41_0;
                propertyChangedCallback = <>c.<>9__41_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnShowCriteriaInAutoFilterRowChanged();
                };
            }
            return DependencyPropertyManager.Register("ShowCriteriaInAutoFilterRow", typeof(bool), ownerType, new PropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowHorizontalLinesProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__32_0;
                propertyChangedCallback = <>c.<>9__32_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnShowHorizontalLinesChanged();
                };
            }
            return DependencyPropertyManager.Register("ShowHorizontalLines", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterShowIndicatorProperty(Type ownerType) => 
            DependencyPropertyManager.Register("ShowIndicator", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(TableViewBehavior.OnShowIndicatorChanged)));

        internal static DependencyPropertyKey RegisterShowTotalSummaryIndicatorIndentPropertyKey(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("ShowTotalSummaryIndicatorIndent", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));

        internal static DependencyProperty RegisterShowVerticalLinesProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__33_0;
                propertyChangedCallback = <>c.<>9__33_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnShowVerticalLinesChanged();
                };
            }
            return DependencyPropertyManager.Register("ShowVerticalLines", typeof(bool), ownerType, new PropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterSwitchToCompactModeWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__93_0;
            if (<>c.<>9__93_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__93_0;
                propertyChangedCallback = <>c.<>9__93_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateCompactModeCore();
                };
            }
            return DependencyPropertyManager.Register("SwitchToCompactModeWidth", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterTotalGroupAreaIndentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__19_0;
                propertyChangedCallback = <>c.<>9__19_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnTotalGroupAreaIndentChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("TotalGroupAreaIndent", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterTotalSummaryFixedNoneContentWidthProperty(Type ownerType) => 
            DependencyPropertyManager.RegisterReadOnly("TotalSummaryFixedNoneContentWidth", typeof(double), ownerType, new PropertyMetadata(0.0));

        internal static DependencyProperty RegisterUpdateRowButtonsTemplateProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__495_0;
            if (<>c.<>9__495_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__495_0;
                propertyChangedCallback = <>c.<>9__495_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.UpdateRowButtonsControl();
                };
            }
            return DependencyProperty.Register("UpdateRowButtonsTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterUseConstantDataBarAnimationSpeed<T>() where T: DependencyObject => 
            DependencyPropertyManager.Register("UseConstantDataBarAnimationSpeed", typeof(bool), typeof(T), new PropertyMetadata(true));

        internal static DependencyProperty RegisterUseEvenRowBackgroundProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__58_0;
            if (<>c.<>9__58_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__58_0;
                propertyChangedCallback = <>c.<>9__58_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
                };
            }
            return DependencyPropertyManager.Register("UseEvenRowBackground", typeof(bool), ownerType, new PropertyMetadata(false, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterUseGroupShadowIndentProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__50_0;
                propertyChangedCallback = <>c.<>9__50_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((DataViewBase) d).UpdateColumnsPositions();
                };
            }
            return DependencyPropertyManager.Register("UseGroupShadowIndent", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, propertyChangedCallback));
        }

        internal static DependencyProperty RegisterUseIndicatorForSelectionProperty(Type ownerType) => 
            DependencyPropertyManager.Register("UseIndicatorForSelection", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));

        internal static DependencyProperty RegisterUseLightweightTemplatesProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__490_0;
            if (<>c.<>9__490_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__490_0;
                propertyChangedCallback = <>c.<>9__490_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnUseLightweightTemplatesChanged();
                };
            }
            return DependencyProperty.Register("UseLightweightTemplates", typeof(UseLightweightTemplates?), ownerType, new FrameworkPropertyMetadata(null, propertyChangedCallback));
        }

        internal static DependencyPropertyKey RegisterVerticalScrollBarWidthProperty(Type ownerType)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__16_0;
                propertyChangedCallback = <>c.<>9__16_0 = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                    ((ITableView) d).TableViewBehavior.OnVerticalScrollBarWidthChanged();
                };
            }
            return DependencyPropertyManager.RegisterReadOnly("VerticalScrollBarWidth", typeof(double), ownerType, new PropertyMetadata(0.0, propertyChangedCallback));
        }

        private void RemoveConditions(IModelItem[] conditionsToDelete, IModelItemCollection formatConditions)
        {
            if (conditionsToDelete.Length != 0)
            {
                this.FormatConditions.BeginUpdate();
                try
                {
                    foreach (IModelItem item in conditionsToDelete)
                    {
                        formatConditions.Remove(item);
                    }
                }
                finally
                {
                    this.FormatConditions.EndUpdate();
                }
            }
        }

        private void RemoveOrphanConditionFilters()
        {
            if (!base.DataControl.IsInDesignTool() && base.DataControl.HasConditionFormatFilters)
            {
                IEnumerable<FormatConditionFilter> filters = base.DataControl.GetFormatConditionFilters();
                base.DataControl.FilterCriteria = FormatConditionFiltersHelper.RemoveConditionFormatFilters(base.DataControl.FilterCriteria, info => !filters.Any<FormatConditionFilter>(x => x.IsMatchedInfo(info)));
            }
        }

        protected internal override void ResetHeadersChildrenCache()
        {
            base.View.ResetHeadersChildrenCache();
        }

        internal void ResetHorizontalVirtualizationOffset()
        {
            this.HorizontalVirtualizationOffset = 0.0;
            this.HorizontalHeaderVirtualizationOffset = 0.0;
        }

        internal override void ResetServiceSummaryCache()
        {
            this.originalSummaries = null;
            this.convertedSummaries = null;
        }

        protected internal void SelectCell(int rowHandle, ColumnBase column)
        {
            if (this.IsValidRowHandleAndColumn(rowHandle, column))
            {
                base.View.SelectionStrategy.SelectCell(rowHandle, column);
            }
        }

        protected internal void SelectCells(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
        {
            if (this.IsValidRowHandleAndColumn(startRowHandle, startColumn) && this.IsValidRowHandleAndColumn(endRowHandle, endColumn))
            {
                base.View.SelectionStrategy.SetCellsSelection(startRowHandle, startColumn, endRowHandle, endColumn, true);
            }
        }

        internal void SelectMasterDetailRangeCell(int startCommonVisibleIndex, int endCommonVisibleIndex, double startX, double endX, DataControlBase rootDataControl)
        {
            base.View.SelectionStrategy.SelectMasterDetailRangeCell(startCommonVisibleIndex, endCommonVisibleIndex, startX, endX, rootDataControl, true, false, false);
        }

        internal abstract void SetGroupElementsForBestFit(FrameworkElement element, IBestFitColumn column, int rowHandle);
        internal void SetHorizontalViewport(double value)
        {
            if (this.TableView.HorizontalViewport != value)
            {
                this.TableView.SetHorizontalViewport(value);
                this.UpdateViewportVisibleColumns();
            }
        }

        private void SetMouseMoveSelectionStrategy(MouseMoveSelectionBase mouseMoveSelectionStrategy)
        {
            (base.View.RootView.ViewBehavior as TableViewBehavior).mouseMoveSelection = mouseMoveSelectionStrategy;
        }

        internal override void SetVerticalScrollBarWidth(double width)
        {
            this.ViewInfo.VerticalScrollBarWidth = width;
        }

        protected internal virtual void ShowConditionalFormattingManagerCore(ColumnBase column)
        {
            AssignableServiceHelper2<FrameworkElement, IDialogService>.DoServiceAction(base.View, this.TableView.ConditionalFormattingManagerServiceTemplate, delegate (IDialogService service) {
                DataControlDialogContext arg = null;
                arg = (column == null) ? (((this.DataControl == null) || (this.DataControl.ColumnsCore.Count <= 0)) ? new DataControlDialogContext(null, this.DataControl) : new DataControlDialogContext(this.DataControl.ColumnsCore[0], this.DataControl)) : new DataControlDialogContext(column, this.DataControl);
                ManagerViewModel viewModel = ManagerViewModel.Factory(arg);
                MessageBoxResult? defaultButton = null;
                defaultButton = null;
                List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton);
                UICommand item = new UICommand();
                item.Caption = XtraLocalizer<ConditionalFormattingStringId>.Active.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Apply);
                item.IsCancel = false;
                item.IsDefault = false;
                bool? useCommandManager = null;
                item.Command = new DelegateCommand<CancelEventArgs>(delegate (CancelEventArgs e) {
                    e.Cancel = true;
                    viewModel.ApplyChanges();
                }, x => viewModel.CanApply, useCommandManager);
                dialogCommands.Add(item);
                if (service.ShowDialog(dialogCommands, viewModel.Description, viewModel) == dialogCommands[0])
                {
                    viewModel.ApplyChanges();
                    this.RemoveOrphanConditionFilters();
                }
            });
        }

        protected internal virtual void ShowFormatConditionDialogCore(ColumnBase column, FormatConditionDialogType dialogKind)
        {
            AssignableServiceHelper2<FrameworkElement, IDialogService>.DoServiceAction(base.View, this.TableView.FormatConditionDialogServiceTemplate, delegate (IDialogService service) {
                ConditionalFormattingDialogViewModel viewModel = this.GetViewModelFactory(dialogKind)(this.TableView);
                viewModel.Initialize(new DataControlDialogContext(column, this.DataControl));
                MessageBoxResult? defaultButton = null;
                defaultButton = null;
                List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton);
                dialogCommands[0].Command = new DelegateCommand<CancelEventArgs>(x => x.Cancel = !viewModel.TryClose());
                if (service.ShowDialog(dialogCommands, viewModel.Title, viewModel) == dialogCommands[0])
                {
                    IModelItem dataControlModelItem = this.View.DesignTimeAdorner.DataControlModelItem;
                    IModelItemCollection formatConditionsModelItemCollection = this.GetFormatConditionsModelItemCollection(dataControlModelItem);
                    IModelItem condition = viewModel.CreateCondition(dataControlModelItem.Context, column.FieldName);
                    condition.Properties[FormatConditionBase.FieldNameProperty.Name].SetValue(column.FieldName);
                    viewModel.SetFormatProperty(condition);
                    formatConditionsModelItemCollection.Add(condition);
                }
            });
        }

        internal void StartAnimation(int rowHandle, DataUpdateFormatCondition condition)
        {
            this.DoRowAction(rowHandle, x => () => x.StartAnimation(this.GroupAnimationsByGeneration(condition)));
        }

        internal void StartAnimation(string fieldName, int rowHandle, DataUpdateFormatCondition condition)
        {
            this.DoRowAction(rowHandle, delegate (RowData x) {
                GridColumnData data = this.GetCellData(x, fieldName);
                return (data != null) ? () => data.StartAnimation(this.GroupAnimationsByGeneration(condition)) : null;
            });
        }

        internal void StopAnimation(int rowHandle)
        {
            Func<RowData, Action> getTarget = <>c.<>9__446_0;
            if (<>c.<>9__446_0 == null)
            {
                Func<RowData, Action> local1 = <>c.<>9__446_0;
                getTarget = <>c.<>9__446_0 = x => new Action(x.StopAnimation);
            }
            this.DoRowAction(rowHandle, getTarget);
        }

        internal void StopAnimation(string fieldName, int rowHandle)
        {
            this.DoRowAction(rowHandle, delegate (RowData x) {
                GridColumnData cellData = this.GetCellData(x, fieldName);
                return (cellData != null) ? new Action(cellData.StopAnimation) : null;
            });
        }

        protected internal override void StopSelection()
        {
            this.MouseMoveSelection.OnMouseUp(base.View);
            this.SetMouseMoveSelectionStrategy(null);
            this.MouseMoveSelection.ReleaseMouseCapture(base.View);
        }

        protected internal void UnselectCell(int rowHandle, ColumnBase column)
        {
            if (this.IsValidRowHandleAndColumn(rowHandle, column))
            {
                base.View.SelectionStrategy.UnselectCell(rowHandle, column);
            }
        }

        protected internal void UnselectCells(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
        {
            if (this.IsValidRowHandleAndColumn(startRowHandle, startColumn) && this.IsValidRowHandleAndColumn(endRowHandle, endColumn))
            {
                base.View.SelectionStrategy.SetCellsSelection(startRowHandle, startColumn, endRowHandle, endColumn, false);
            }
        }

        internal void UpdateActualAllowCellMergeCore()
        {
            bool flag1;
            if ((!this.TableView.AllowCellMerge && ((base.DataControl == null) || (base.DataControl.countColumnCellMerge <= 0))) || base.View.IsMultiSelection)
            {
                flag1 = false;
            }
            else
            {
                flag1 = base.View.NavigationStyle != GridViewNavigationStyle.Row;
            }
            base.View.ActualAllowCellMerge = flag1;
        }

        internal virtual void UpdateActualDataRowTemplateSelector()
        {
            this.UpdateActualDataRowTemplateSelectorCore(this.TableView.DataRowTemplate);
        }

        protected virtual void UpdateActualDataRowTemplateSelectorCore(DataTemplate template)
        {
            if (this.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row) && (template is DefaultDataTemplate))
            {
                template = null;
            }
            base.View.UpdateActualTemplateSelector(this.TableView.ActualDataRowTemplateSelectorPropertyKey, this.TableView.DataRowTemplateSelector, template, null);
        }

        internal virtual void UpdateActualDetailMargin()
        {
        }

        internal virtual void UpdateActualExpandDetailButtonWidth()
        {
        }

        private void UpdateActualFadeSelectionOnLostFocus()
        {
            this.UpdateActualRootProperty(view => view.SetActualFadeSelectionOnLostFocus(base.View.RootView.FadeSelectionOnLostFocus));
        }

        private void UpdateActualIndicatorWidth()
        {
            this.UpdateActualRootProperty(view => view.SetActualIndicatorWidth(((ITableView) base.View.RootView).IndicatorWidth));
        }

        internal override void UpdateActualProperties()
        {
            this.UpdateShowTotalSummaryIndicatorIndent(this.TableView);
            this.UpdateActualShowIndicator();
            this.UpdateActualIndicatorWidth();
            this.UpdateActualExpandDetailButtonWidth();
            this.UpdateActualDetailMargin();
            this.UpdateActualFadeSelectionOnLostFocus();
            this.UpdateExpandColumnPosition();
        }

        private void UpdateActualRootProperty(SetPropertyIntoView setProperty)
        {
            base.View.UpdateAllOriginationViews(view => setProperty((ITableView) view));
        }

        protected virtual void UpdateActualRowDetailsTemplateSelector()
        {
            base.View.UpdateActualTemplateSelector(this.TableView.ActualRowDetailsTemplateSelectorPropertyKey, this.TableView.RowDetailsTemplateSelector, this.TableView.RowDetailsTemplate, null);
        }

        private void UpdateActualShowIndicator()
        {
            this.UpdateActualRootProperty(view => view.SetActualShowIndicator(((ITableView) base.View.RootView).ShowIndicator));
        }

        internal override void UpdateAdditionalFocusedRowData()
        {
            this.UpdateAdditionalFocusedRowDataCore();
            base.View.RaiseFocusedRowHandleChanged();
        }

        protected virtual void UpdateAdditionalFocusedRowDataCore()
        {
            if (base.View.IsAutoFilterRowFocused)
            {
                base.View.FocusedRowData = this.AutoFilterRowData;
            }
        }

        protected internal override void UpdateAdditionalRowsData()
        {
            if (this.TableView.ShowAutoFilterRow)
            {
                this.AutoFilterRowData.UpdateData();
            }
        }

        private void UpdateBandsIndicator()
        {
            if ((base.DataControl != null) && (base.DataControl.BandsLayoutCore != null))
            {
                base.DataControl.BandsLayoutCore.UpdateShowIndicator(this.TableView.ActualShowIndicator);
            }
        }

        protected internal override void UpdateBandsLayoutProperties()
        {
            base.UpdateBandsLayoutProperties();
            if ((base.DataControl != null) && (base.DataControl.BandsLayoutCore != null))
            {
                base.DataControl.BandsLayoutCore.ShowBandsPanel = this.TableView.ShowBandsPanel;
                base.DataControl.BandsLayoutCore.AllowChangeColumnParent = this.TableView.AllowChangeColumnParent;
                base.DataControl.BandsLayoutCore.AllowChangeBandParent = this.TableView.AllowChangeBandParent;
                base.DataControl.BandsLayoutCore.ShowBandsInCustomizationForm = this.TableView.ShowBandsInCustomizationForm;
                base.DataControl.BandsLayoutCore.AllowBandMoving = this.TableView.AllowBandMoving;
                base.DataControl.BandsLayoutCore.AllowBandResizing = this.TableView.AllowBandResizing;
                base.DataControl.BandsLayoutCore.AllowAdvancedVerticalNavigation = this.TableView.AllowAdvancedVerticalNavigation;
                base.DataControl.BandsLayoutCore.AllowAdvancedHorizontalNavigation = this.TableView.AllowAdvancedHorizontalNavigation;
                base.DataControl.BandsLayoutCore.ColumnChooserBandsSortOrderComparer = this.TableView.ColumnChooserBandsSortOrderComparer;
                base.DataControl.BandsLayoutCore.BandHeaderTemplate = this.TableView.BandHeaderTemplate;
                base.DataControl.BandsLayoutCore.BandHeaderTemplateSelector = this.TableView.BandHeaderTemplateSelector;
                base.DataControl.BandsLayoutCore.BandHeaderToolTipTemplate = this.TableView.BandHeaderToolTipTemplate;
                base.DataControl.BandsLayoutCore.PrintBandHeaderStyle = this.TableView.PrintBandHeaderStyle;
                base.DataControl.BandsLayoutCore.AllowBandMultiRow = this.TableView.AllowBandMultiRow;
                this.UpdateBandsIndicator();
            }
        }

        protected internal abstract void UpdateBestFitGroupFooterSummaryControl(BestFitControlBase bestFitControl, int rowHandle, IBestFitColumn column);
        protected internal override void UpdateCellData(ColumnsRowDataBase rowData)
        {
            rowData.CellData = rowData.CreateCellDataList();
            rowData.UpdateFixedLeftCellData();
            this.HorizontalNavigationStrategy.UpdateFixedNoneCellData(rowData, this);
            rowData.UpdateFixedRightCellData();
        }

        internal void UpdateCellMergingPanels(bool force = false)
        {
            if (this.TableView.HasDetailViews || base.View.ActualAllowCellMerge)
            {
                if (force)
                {
                    this.UpdateCellMergingPanelsCore();
                    this.updatePanelsEnqueued = false;
                }
                else if (!this.updatePanelsEnqueued)
                {
                    base.View.EnqueueImmediateAction(new Action(this.UpdateCellMergingPanelsCore));
                    this.updatePanelsEnqueued = true;
                }
            }
        }

        private void UpdateCellMergingPanelsCore()
        {
            this.updatePanelsEnqueued = false;
            UpdateRowDataDelegate updateMethod = <>c.<>9__474_0;
            if (<>c.<>9__474_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__474_0;
                updateMethod = <>c.<>9__474_0 = delegate (RowData data) {
                    if (data.View.ActualAllowCellMerge)
                    {
                        data.InvalidateCellsPanel();
                        data.UpdateIsFocusedCell();
                    }
                };
            }
            base.View.UpdateAllRowData(updateMethod);
        }

        internal void UpdateColumnDataWidths(out double totalFixedNoneSize, out double totalFixedLeftSize, out double totalFixedRightSize)
        {
            this.UpdateColumnsStrategy.UpdateColumnDataWidths(base.View, this.ViewInfo, out totalFixedNoneSize, out totalFixedLeftSize, out totalFixedRightSize);
        }

        internal override void UpdateColumnsLayout()
        {
            if (base.DataControl.BandsLayoutCore == null)
            {
                this.UpdateHasLeftRightSibling();
            }
            this.ViewInfo.CalcColumnsLayout();
        }

        protected internal override void UpdateColumnsViewInfo(bool updateDataPropertiesOnly)
        {
            base.UpdateColumnsViewInfo(updateDataPropertiesOnly);
            if ((base.DataControl != null) && (base.DataControl.BandsLayoutCore != null))
            {
                base.DataControl.BandsLayoutCore.ForeachBand(b => b.UpdateViewInfo(updateDataPropertiesOnly));
            }
        }

        private void UpdateCompactModeCore()
        {
            DataTemplate compactTemplate = this.GetActualCompactTemplate();
            this.UpdateCompactPanelShowMode();
            this.TableView.SetIsCompactMode(compactTemplate != null);
            base.View.UpdateVisibleGroupPanel();
            this.UpdateRowData(x => x.UpdateCompactMode(compactTemplate), true, true);
        }

        protected void UpdateCompactPanelShowMode()
        {
            bool flag = false;
            switch (this.TableView.CompactPanelShowMode)
            {
                case CompactPanelShowMode.CompactMode:
                    flag = this.CheckCompactModeWidth();
                    break;

                case CompactPanelShowMode.Always:
                    flag = true;
                    break;

                default:
                    flag = false;
                    break;
            }
            if (flag != base.View.ActualShowCompactPanel)
            {
                base.View.ActualShowCompactPanel = flag;
            }
        }

        private void UpdateDetails()
        {
            UpdateRowDataDelegate updateMethod = <>c.<>9__502_0;
            if (<>c.<>9__502_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__502_0;
                updateMethod = <>c.<>9__502_0 = x => x.UpdateDetails();
            }
            this.UpdateViewRowData(updateMethod);
        }

        private void UpdateExpandColumnPosition()
        {
            if (base.View.IsRootView && !this.TableView.ShowIndicator)
            {
                this.TableView.SetExpandColumnPosition(ColumnPosition.Left);
            }
            else
            {
                this.TableView.SetExpandColumnPosition(ColumnPosition.Middle);
            }
        }

        internal override void UpdateFixedAreaColumnsCount(int fixedLeftColumnsCount, int fixedNoneColumnsCount, int fixedRightColumnsCount)
        {
            this.FixedLeftColumnsCount = fixedLeftColumnsCount;
            this.FixedNoneColumnsCount = fixedNoneColumnsCount;
            this.FixedRightColumnsCount = fixedRightColumnsCount;
        }

        private bool UpdateFixedColumns(bool changed)
        {
            IList<ColumnBase> fixedLeftVisibleColumns = this.GetFixedLeftVisibleColumns();
            if (!ListHelper.AreEqual<ColumnBase>(this.FixedLeftVisibleColumns, fixedLeftVisibleColumns))
            {
                this.FixedLeftVisibleColumns = fixedLeftVisibleColumns;
                changed = true;
            }
            IList<ColumnBase> fixedRightVisibleColumns = this.GetFixedRightVisibleColumns();
            if (!ListHelper.AreEqual<ColumnBase>(this.FixedRightVisibleColumns, fixedRightVisibleColumns))
            {
                this.FixedRightVisibleColumns = fixedRightVisibleColumns;
                changed = true;
            }
            IList<ColumnBase> fixedNoneVisibleColumns = this.GetFixedNoneVisibleColumns();
            if (!ListHelper.AreEqual<ColumnBase>(this.FixedNoneVisibleColumns, fixedNoneVisibleColumns))
            {
                this.FixedNoneVisibleColumns = fixedNoneVisibleColumns;
                changed = true;
            }
            return changed;
        }

        protected internal override void UpdateFixedNoneContentWidth(ColumnsRowDataBase rowData)
        {
            rowData.FixedNoneContentWidth = Math.Max(0.0, rowData.GetFixedNoneContentWidth(this.TableView.FixedNoneContentWidth));
        }

        private void UpdateFocusedRowIndicator()
        {
            RowData rowData = base.View.ViewBehavior.GetRowData(base.View.FocusedRowHandle);
            if (rowData != null)
            {
                rowData.UpdateIndicatorState();
            }
        }

        private void UpdateHasLeftRightSibling()
        {
            this.ColumnsLayoutCalculator.UpdateHasLeftRightSibling(this.FixedLeftVisibleColumns);
            this.ColumnsLayoutCalculator.UpdateHasLeftRightSibling(this.FixedRightVisibleColumns);
            this.ColumnsLayoutCalculator.UpdateHasLeftRightSibling(this.FixedNoneVisibleColumns);
        }

        protected internal override void UpdateLastPostition(IndependentMouseEventArgs e)
        {
            if (base.View.ScrollContentPresenter != null)
            {
                this.LastMousePosition = e.GetPosition(base.View.ScrollContentPresenter);
            }
        }

        internal void UpdateNewItemRowPosition()
        {
            if (this.AdditionalRowItemsControl != null)
            {
                this.AdditionalRowItemsControl.UpdateRows();
            }
        }

        protected internal override void UpdateRowData(UpdateRowDataDelegate updateMethod, bool updateInvisibleRows = true, bool updateFocusedRow = true)
        {
            base.UpdateRowData(updateMethod, updateInvisibleRows, updateFocusedRow);
            if (updateInvisibleRows || this.TableView.ShowAutoFilterRow)
            {
                updateMethod(this.AutoFilterRowData);
            }
        }

        internal void UpdateScrollBarAnnotations(int rowHandle)
        {
            this.TableView.ScrollBarAnnotationsManager.ScrollBarAnnotationChanged(ListChangedType.ItemChanged, rowHandle, new int?(rowHandle));
        }

        internal override void UpdateSecondaryScrollInfoCore(double secondaryOffset, bool allowUpdateViewportVisibleColumns)
        {
            double num = -secondaryOffset;
            if (this.HorizontalOffset != num)
            {
                this.HorizontalOffset = num;
                if (allowUpdateViewportVisibleColumns)
                {
                    this.UpdateViewportVisibleColumns();
                }
            }
        }

        internal void UpdateSelectionRectCore(int rowHandle, ColumnBase column)
        {
            base.View.SelectionStrategy.UpdateSelectionRect(rowHandle, column);
        }

        protected internal override void UpdateServiceRowData(Action<ColumnsRowDataBase> updateMethod)
        {
            if (base.View.DataControl != null)
            {
                foreach (ColumnsRowDataBase base2 in base.View.DataControl.DataControlParent.GetColumnsRowDataEnumerator())
                {
                    updateMethod(base2);
                }
            }
        }

        private void UpdateShowTotalSummaryIndicatorIndent(ITableView view)
        {
            if (!view.ViewBase.IsRootView)
            {
                view.SetShowTotalSummaryIndicatorIndent(false);
            }
            else
            {
                view.SetShowTotalSummaryIndicatorIndent(view.ShowIndicator);
            }
        }

        internal override void UpdateViewportVisibleColumns()
        {
            this.HorizontalNavigationStrategy.UpdateViewportVisibleColumns(this);
            this.TableView.ScrollingVirtualizationMargin = this.UpdateColumnsStrategy.GetScrollingMargin(this.TableView, -this.HorizontalOffset, this.HorizontalVirtualizationOffset);
            this.TableView.ScrollingHeaderVirtualizationMargin = this.UpdateColumnsStrategy.GetScrollingMargin(this.TableView, -this.HorizontalOffset, this.HorizontalHeaderVirtualizationOffset);
        }

        internal void UpdateViewportVisibleColumnsCore()
        {
            if (this.RootDataPresenter != null)
            {
                List<ColumnBase> viewportVisibleColumns = this.UpdateColumnsStrategy.GetViewportVisibleColumns(this);
                if ((this.TableView.ViewportVisibleColumns == null) || !ListHelper.AreEqual<ColumnBase>(this.TableView.ViewportVisibleColumns, viewportVisibleColumns))
                {
                    this.TableView.ViewportVisibleColumns = viewportVisibleColumns;
                    this.UpdateVirtualizedData();
                }
            }
        }

        protected internal void UpdateVirtualizedData()
        {
            base.View.HeadersData.UpdateFixedNoneCellData(true);
            UpdateRowDataDelegate updateMethod = <>c.<>9__268_0;
            if (<>c.<>9__268_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__268_0;
                updateMethod = <>c.<>9__268_0 = delegate (RowData rowData) {
                    rowData.UpdateRow();
                    rowData.UpdateFixedNoneCellData(true);
                };
            }
            this.UpdateRowData(updateMethod, true, true);
            Action<ColumnsRowDataBase> action1 = <>c.<>9__268_1;
            if (<>c.<>9__268_1 == null)
            {
                Action<ColumnsRowDataBase> local2 = <>c.<>9__268_1;
                action1 = <>c.<>9__268_1 = rowData => rowData.UpdateFixedNoneCellData(true);
            }
            this.UpdateServiceRowData(action1);
        }

        internal virtual bool UseDataRowTemplate(RowData rowData) => 
            this.IsNullOrDefaultTemplate(this.TableView.DataRowTemplate) ? ((this.TableView.DataRowTemplateSelector != null) && !this.IsNullOrDefaultTemplate(this.TableView.DataRowTemplateSelector.SelectTemplate(rowData, null))) : true;

        internal bool UseLightweightTemplatesHasFlag(UseLightweightTemplates flag) => 
            this.ActualUseLightweightTemplates.HasFlag(flag);

        internal override bool UseRowDetailsTemplate(int rowHandle) => 
            (base.View.RowMarginControlDisplayMode != RowMarginControlDisplayMode.InCellsControl) ? (((this.TableView.RowDetailsTemplate != null) || (this.TableView.RowDetailsTemplateSelector != null)) ? ((this.TableView.RowDetailsVisibilityMode != RowDetailsVisibilityMode.Collapsed) ? ((this.TableView.RowDetailsVisibilityMode == RowDetailsVisibilityMode.Visible) || (rowHandle == base.View.FocusedRowHandle)) : false) : false) : false;

        internal virtual void ValidateRowStyle(Style newStyle)
        {
        }

        internal override void VisibleColumnsChanged()
        {
            this.ColumnsLayoutCalculator.VisibleColumnsChanged();
        }

        protected internal override int? VisibleComparisonCore(BaseColumn x, BaseColumn y)
        {
            if (x.Fixed != y.Fixed)
            {
                if (x.Fixed == FixedStyle.Left)
                {
                    return -1;
                }
                if (y.Fixed == FixedStyle.Left)
                {
                    return 1;
                }
                if (x.Fixed == FixedStyle.Right)
                {
                    return 1;
                }
                if (y.Fixed == FixedStyle.Right)
                {
                    return -1;
                }
            }
            return null;
        }

        internal double HorizontalOffset { get; private set; }

        internal override bool IsNavigationLocked =>
            !this.MouseMoveSelection.AllowNavigation;

        internal override bool IsAutoFilterRowFocused =>
            base.View.FocusedRowHandle == -2147483645;

        internal override bool CanShowFixedColumnMenu =>
            this.TableView.AllowFixedColumnMenu;

        internal override bool AllowResizingCore =>
            this.TableView.AllowResizing;

        internal override bool UpdateAllowResizingOnWidthChanging =>
            (base.DataControl == null) || ReferenceEquals(base.DataControl.BandsLayoutCore, null);

        internal override bool AutoWidthCore =>
            this.ColumnsLayoutCalculator.AutoWidth;

        internal override bool AllowColumnResizingCore =>
            true;

        internal override double HorizontalViewportCore =>
            this.TableView.HorizontalViewport;

        internal override bool AutoMoveRowFocusCore =>
            this.TableView.AutoMoveRowFocus;

        internal double HorizontalVirtualizationOffset { get; private set; }

        internal double HorizontalHeaderVirtualizationOffset { get; private set; }

        protected internal virtual bool IsNewItemRowFocused =>
            false;

        public RowData AutoFilterRowData { get; private set; }

        internal DevExpress.Xpf.Grid.Native.ColumnsLayoutCalculator ColumnsLayoutCalculator
        {
            get
            {
                this.columnsLayoutCalculator ??= this.ViewInfo.CreateColumnsLayoutCalculator();
                return this.columnsLayoutCalculator;
            }
        }

        internal NormalHorizontalNavigationStrategy HorizontalNavigationStrategy =>
            (NormalHorizontalNavigationStrategy) this.NavigationStrategyBase;

        internal override HorizontalNavigationStrategyBase NavigationStrategyBase =>
            (!this.TableView.AllowHorizontalScrollingVirtualization || this.TableView.AutoWidth) ? NormalHorizontalNavigationStrategy.NormalHorizontalNavigationStrategyInstance : VirtualizedHorizontalNavigationStrategy.VirtualizedHorizontalNavigationStrategyInstance;

        internal bool HasFixedLeftElements =>
            this.UpdateColumnsStrategy.HasFixedLeftElements(this.TableView);

        internal IList<ColumnBase> FixedLeftVisibleColumns
        {
            get => 
                this.fixedLeftVisibleColumns;
            set
            {
                this.fixedLeftVisibleColumns = value;
                this.TableView.SetFixedLeftVisibleColumns(this.fixedLeftVisibleColumns);
            }
        }

        internal bool HasFixedRightElements =>
            this.UpdateColumnsStrategy.HasFixedRightElements(this.TableView);

        internal IList<ColumnBase> FixedRightVisibleColumns
        {
            get => 
                this.fixedRightVisibleColumns;
            set
            {
                this.fixedRightVisibleColumns = value;
                this.TableView.SetFixedRightVisibleColumns(this.fixedRightVisibleColumns);
            }
        }

        internal IList<ColumnBase> FixedNoneVisibleColumns
        {
            get => 
                this.fixedNoneVisibleColumns;
            set
            {
                this.fixedNoneVisibleColumns = value;
                this.TableView.SetFixedNoneVisibleColumns(this.fixedNoneVisibleColumns);
            }
        }

        internal int FixedLeftColumnsCount { get; private set; }

        internal int FixedNoneColumnsCount { get; private set; }

        internal int FixedRightColumnsCount { get; private set; }

        internal double HorizontalExtent { get; set; }

        internal DevExpress.Xpf.Grid.AdditionalRowItemsControl AdditionalRowItemsControl
        {
            get => 
                this.additionalRowItemsControl;
            set
            {
                this.additionalRowItemsControl = value;
                this.UpdateNewItemRowPosition();
            }
        }

        internal double LeftIndent { get; set; }

        internal double RightIndent { get; set; }

        internal MouseMoveSelectionBase MouseMoveSelection
        {
            get
            {
                TableViewBehavior viewBehavior = base.View.RootView.ViewBehavior as TableViewBehavior;
                return ((viewBehavior.mouseMoveSelection != null) ? viewBehavior.mouseMoveSelection : MouseMoveSelectionNone.Instance);
            }
        }

        public GridViewInfo ViewInfo
        {
            get
            {
                this.viewInfo ??= this.CreateViewInfo();
                return this.viewInfo;
            }
        }

        internal ITableView TableView =>
            (ITableView) base.View;

        internal DataPresenterBase DataPresenter =>
            base.View.DataPresenter;

        internal DataPresenterBase RootDataPresenter =>
            base.View.RootDataPresenter;

        protected internal override DispatcherTimer ScrollTimer =>
            this.scrollTimer;

        protected internal override bool AllowCascadeUpdate =>
            this.TableView.AllowCascadeUpdate;

        protected internal override bool AllowPerPixelScrolling =>
            this.TableView.AllowPerPixelScrolling;

        protected internal override bool KeepViewportOnDataUpdate =>
            this.TableView.KeepViewportOnDataUpdate;

        protected internal override double ScrollAnimationDuration =>
            this.TableView.ScrollAnimationDuration;

        protected internal override DevExpress.Xpf.Grid.ScrollAnimationMode ScrollAnimationMode =>
            this.TableView.ScrollAnimationMode;

        protected internal override bool AllowScrollAnimation =>
            this.TableView.AllowScrollAnimation;

        protected double TotalVisibleIndent =>
            (base.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? 0.0 : this.ViewInfo.TotalGroupAreaIndent;

        private bool IsBestFitControlDecoratorLoaded =>
            (this.BestFitControlDecorator != null) && UIElementHelper.IsVisibleInTree(this.BestFitControlDecorator);

        private UpdateColumnsStrategyBase UpdateColumnsStrategy =>
            ((base.DataControl == null) || (base.DataControl.BandsLayoutCore == null)) ? TableViewUpdateColumnsStrategy.Instance : BandedViewUpdateColumnsStrategy.Instance;

        private CollectionChangedWeakEventHandler<TableViewBehavior> CompactModeFilterItemsCollectionChangedHandler
        {
            get
            {
                if (this.compactModeFilterItemsCollectionChangedHandler == null)
                {
                    Action<TableViewBehavior, object, NotifyCollectionChangedEventArgs> onEventAction = <>c.<>9__335_0;
                    if (<>c.<>9__335_0 == null)
                    {
                        Action<TableViewBehavior, object, NotifyCollectionChangedEventArgs> local1 = <>c.<>9__335_0;
                        onEventAction = <>c.<>9__335_0 = delegate (TableViewBehavior e, object o, NotifyCollectionChangedEventArgs args) {
                            e.CompactModeFilterItemsCollectionChanged(o, args);
                        };
                    }
                    this.compactModeFilterItemsCollectionChangedHandler = new CollectionChangedWeakEventHandler<TableViewBehavior>(this, onEventAction);
                }
                return this.compactModeFilterItemsCollectionChangedHandler;
            }
        }

        protected internal override double FirstColumnIndent =>
            this.ViewInfo.FirstColumnIndent;

        protected internal override double NewItemRowIndent =>
            this.ViewInfo.NewItemRowIndent;

        protected internal override double NewItemRowCellIndent =>
            this.TableView.TotalGroupAreaIndent;

        public Decorator BestFitControlDecorator
        {
            get => 
                ((ITableView) base.View.RootView).TableViewBehavior.bestFitControlDecorator;
            set => 
                this.bestFitControlDecorator = value;
        }

        internal DataControlBestFitCalculator BestFitCalculator { get; private set; }

        FormatInfoCollection IFormatConditionCollectionOwner.PredefinedIconSetFormats =>
            this.TableView.PredefinedIconSetFormats;

        protected internal virtual bool ActualShowFixRowButton =>
            false;

        protected internal virtual double ActualFixRowButtonWidth =>
            0.0;

        protected internal virtual double FixedLineHeight =>
            0.0;

        internal override bool CanShowDetailColumnHeadersControl =>
            base.CanShowDetailColumnHeadersControl || ((base.DataControl != null) && ((base.DataControl.BandsLayoutCore != null) && this.TableView.ShowBandsPanel));

        protected internal override bool HasRowTemplateSelector =>
            this.TableView.DataRowTemplateSelector != null;

        private UseLightweightTemplates ActualUseLightweightTemplates
        {
            get
            {
                UseLightweightTemplates? useLightweightTemplates = ((ITableView) base.View).UseLightweightTemplates;
                if (useLightweightTemplates != null)
                {
                    return useLightweightTemplates.GetValueOrDefault();
                }
                UseLightweightTemplates? nullable2 = ((ITableView) base.View.RootView).UseLightweightTemplates;
                if (nullable2 != null)
                {
                    return nullable2.GetValueOrDefault();
                }
                UseLightweightTemplates? defaultUseLightweightTemplates = DefaultUseLightweightTemplates;
                return ((defaultUseLightweightTemplates != null) ? defaultUseLightweightTemplates.GetValueOrDefault() : UseLightweightTemplates.All);
            }
        }

        internal FormatConditionCollection FormatConditions
        {
            get
            {
                FormatConditionCollection formatConditions = this.formatConditions;
                if (this.formatConditions == null)
                {
                    FormatConditionCollection local1 = this.formatConditions;
                    formatConditions = this.formatConditions = new FormatConditionCollection(this);
                }
                return formatConditions;
            }
        }

        protected internal override bool HasFormatConditions =>
            (this.formatConditions != null) && (this.formatConditions.Count > 0);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TableViewBehavior.<>c <>9 = new TableViewBehavior.<>c();
            public static PropertyChangedCallback <>9__6_0;
            public static PropertyChangedCallback <>9__9_0;
            public static PropertyChangedCallback <>9__12_0;
            public static PropertyChangedCallback <>9__14_0;
            public static PropertyChangedCallback <>9__16_0;
            public static PropertyChangedCallback <>9__17_0;
            public static PropertyChangedCallback <>9__18_0;
            public static PropertyChangedCallback <>9__19_0;
            public static PropertyChangedCallback <>9__20_0;
            public static PropertyChangedCallback <>9__21_0;
            public static PropertyChangedCallback <>9__22_0;
            public static PropertyChangedCallback <>9__23_0;
            public static PropertyChangedCallback <>9__25_0;
            public static PropertyChangedCallback <>9__26_0;
            public static PropertyChangedCallback <>9__27_0;
            public static PropertyChangedCallback <>9__28_0;
            public static PropertyChangedCallback <>9__29_0;
            public static PropertyChangedCallback <>9__31_0;
            public static PropertyChangedCallback <>9__32_0;
            public static PropertyChangedCallback <>9__33_0;
            public static PropertyChangedCallback <>9__34_0;
            public static CoerceValueCallback <>9__34_1;
            public static PropertyChangedCallback <>9__40_0;
            public static PropertyChangedCallback <>9__41_0;
            public static PropertyChangedCallback <>9__43_0;
            public static PropertyChangedCallback <>9__49_0;
            public static PropertyChangedCallback <>9__50_0;
            public static PropertyChangedCallback <>9__51_0;
            public static PropertyChangedCallback <>9__52_0;
            public static PropertyChangedCallback <>9__54_0;
            public static PropertyChangedCallback <>9__55_0;
            public static PropertyChangedCallback <>9__56_0;
            public static PropertyChangedCallback <>9__57_0;
            public static PropertyChangedCallback <>9__58_0;
            public static PropertyChangedCallback <>9__59_0;
            public static PropertyChangedCallback <>9__60_0;
            public static PropertyChangedCallback <>9__61_0;
            public static PropertyChangedCallback <>9__62_0;
            public static PropertyChangedCallback <>9__63_0;
            public static PropertyChangedCallback <>9__64_0;
            public static PropertyChangedCallback <>9__65_0;
            public static PropertyChangedCallback <>9__66_0;
            public static PropertyChangedCallback <>9__67_0;
            public static PropertyChangedCallback <>9__68_0;
            public static PropertyChangedCallback <>9__69_0;
            public static PropertyChangedCallback <>9__70_0;
            public static PropertyChangedCallback <>9__71_0;
            public static PropertyChangedCallback <>9__72_0;
            public static PropertyChangedCallback <>9__83_0;
            public static PropertyChangedCallback <>9__93_0;
            public static PropertyChangedCallback <>9__94_0;
            public static PropertyChangedCallback <>9__95_0;
            public static PropertyChangedCallback <>9__96_0;
            public static UpdateRowDataDelegate <>9__98_0;
            public static UpdateRowDataDelegate <>9__99_0;
            public static UpdateRowDataDelegate <>9__100_0;
            public static UpdateRowDataDelegate <>9__101_0;
            public static UpdateRowDataDelegate <>9__268_0;
            public static Action<ColumnsRowDataBase> <>9__268_1;
            public static UpdateRowDataDelegate <>9__297_0;
            public static UpdateRowDataDelegate <>9__298_0;
            public static UpdateRowDataDelegate <>9__299_0;
            public static UpdateRowDataDelegate <>9__300_0;
            public static UpdateRowDataDelegate <>9__301_0;
            public static UpdateRowDataDelegate <>9__302_0;
            public static UpdateRowDataDelegate <>9__303_0;
            public static UpdateRowDataDelegate <>9__305_0;
            public static UpdateRowDataDelegate <>9__306_0;
            public static UpdateRowDataDelegate <>9__307_0;
            public static Action<TableViewBehavior, object, NotifyCollectionChangedEventArgs> <>9__335_0;
            public static Func<object, object> <>9__336_0;
            public static UpdateRowDataDelegate <>9__337_0;
            public static Func<object, object> <>9__338_0;
            public static Action<ColumnBase> <>9__352_0;
            public static Func<FormatConditionBase, bool> <>9__436_0;
            public static Func<RowData, Action> <>9__446_0;
            public static UpdateRowDataDelegate <>9__453_0;
            public static Action<DataViewBase> <>9__455_0;
            public static UpdateRowDataDelegate <>9__456_0;
            public static UpdateRowDataDelegate <>9__474_0;
            public static UpdateRowDataDelegate <>9__483_0;
            public static PropertyChangedCallback <>9__490_0;
            public static PropertyChangedCallback <>9__491_0;
            public static PropertyChangedCallback <>9__492_0;
            public static PropertyChangedCallback <>9__493_0;
            public static PropertyChangedCallback <>9__494_0;
            public static PropertyChangedCallback <>9__495_0;
            public static UpdateRowDataDelegate <>9__502_0;
            public static Func<ServiceSummaryItem, ServiceSummaryItemKey> <>9__520_1;
            public static Func<FormatConditionBase, bool> <>9__524_0;
            public static Func<DataControlBase, IList> <>9__524_2;
            public static Func<object, object> <>9__524_3;

            internal bool <ClearFormatConditionsFromAllColumnsCore>b__436_0(FormatConditionBase x) => 
                ((ISupportManager) x).AllowUserCustomization;

            internal object <CompactModeFilterItemsCollectionChanged>b__338_0(object obj) => 
                obj as ICustomItem;

            internal bool <DevExpress.Xpf.Grid.Native.IFormatConditionCollectionOwner.SyncFormatConditionCollectionWithDetails>b__524_0(FormatConditionBase x) => 
                x.GetType() != typeof(FormatCondition);

            internal IList <DevExpress.Xpf.Grid.Native.IFormatConditionCollectionOwner.SyncFormatConditionCollectionWithDetails>b__524_2(DataControlBase dc) => 
                TableViewBehavior.GetFormatConditions(dc);

            internal object <DevExpress.Xpf.Grid.Native.IFormatConditionCollectionOwner.SyncFormatConditionCollectionWithDetails>b__524_3(object item) => 
                CloneDetailHelper.CloneElement<FormatConditionBase>((FormatConditionBase) item, (Action<FormatConditionBase>) null, (Func<FormatConditionBase, Locker>) null, (object[]) null);

            internal void <get_CompactModeFilterItemsCollectionChangedHandler>b__335_0(TableViewBehavior e, object o, NotifyCollectionChangedEventArgs args)
            {
                e.CompactModeFilterItemsCollectionChanged(o, args);
            }

            internal ServiceSummaryItemKey <GetServiceSummaries>b__520_1(ServiceSummaryItem y) => 
                ConditionalFormatSummaryInfoHelper.ToSummaryItemKey(y);

            internal void <NotifyBandsLayoutChanged>b__98_0(RowData rowData)
            {
                rowData.UpdateCellsPanel();
            }

            internal void <NotifyFixedLeftBandsChanged>b__100_0(RowData rowData)
            {
                rowData.UpdateClientFixedLeftBands();
            }

            internal void <NotifyFixedNoneBandsChanged>b__99_0(RowData rowData)
            {
                rowData.UpdateClientFixedNoneBands();
            }

            internal void <NotifyFixedRightBandsChanged>b__101_0(RowData rowData)
            {
                rowData.UpdateClientFixedRightBands();
            }

            internal void <OnActualAllowCellMergeChanged>b__483_0(RowData rowData)
            {
                rowData.UpdateSelectionState();
                rowData.UpdateCellsPanel();
            }

            internal void <OnActualAlternateRowBackgroundChanged>b__456_0(RowData x)
            {
                x.UpdateClientAlternateBackground();
            }

            internal void <OnActualDataRowTemplateSelectorChanged>b__307_0(RowData x)
            {
                x.UpdateContent();
            }

            internal void <OnActualIndicatorWidthChanged>b__302_0(RowData x)
            {
                x.UpdateClientIndicatorWidth();
            }

            internal void <OnActualShowIndicatorChanged>b__303_0(RowData x)
            {
                x.UpdateClientShowIndicator();
            }

            internal void <OnAlternateRowPropertiesChanged>b__455_0(DataViewBase view)
            {
                view.UpdateAlternateRowBackground();
            }

            internal object <OnCompactModeFilterItemsSourceUpdate>b__336_0(object obj) => 
                obj as ICustomItem;

            internal void <OnFixedLineWidthChanged>b__300_0(RowData x)
            {
                x.UpdateClientFixedLineWidth();
            }

            internal void <OnFixedVisibleColumnsChanged>b__301_0(RowData x)
            {
                x.UpdateClientFixedLineVisibility();
            }

            internal void <OnIsCompactModeChanged>b__337_0(RowData rowData)
            {
                rowData.UpdateClientSummary();
            }

            internal void <OnRowIndicatorContentTemplateChanged>b__306_0(RowData x)
            {
                x.UpdateIndicatorContentTemplate();
            }

            internal void <OnRowMinHeightChanged>b__305_0(RowData x)
            {
                x.UpdateClientMinHeight();
            }

            internal void <OnRowStyleChanged>b__453_0(RowData rowData)
            {
                rowData.UpdateClientRowStyle();
            }

            internal void <OnScrollingVirtualizationMarginChanged>b__297_0(RowData x)
            {
                x.UpdateClientScrollingMargin();
            }

            internal void <OnShowCriteriaInAutoFilterRowChanged>b__352_0(ColumnBase col)
            {
                col.ActualShowCriteriaInAutoFilterRowChanged();
            }

            internal void <OnShowHorizontalLinesChanged>b__298_0(RowData x)
            {
                x.UpdateHorizontalLineVisibility();
            }

            internal void <OnShowVerticalLinesChanged>b__299_0(RowData x)
            {
                x.UpdateClientVerticalLineVisibility();
            }

            internal void <RegisterActualAlternateRowBackgroundProperty>b__56_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnActualAlternateRowBackgroundChanged();
            }

            internal void <RegisterActualDataRowTemplateSelectorProperty>b__26_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnActualDataRowTemplateSelectorChanged();
            }

            internal void <RegisterActualIndicatorWidthPropertyKey>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ITableView) d).TableViewBehavior.OnActualIndicatorWidthChanged();
            }

            internal void <RegisterActualRowDetailsTemplateSelectorProperty>b__493_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateDetails();
            }

            internal void <RegisterActualShowIndicatorProperty>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ITableView) d).TableViewBehavior.OnActualShowIndicatorChanged();
            }

            internal void <RegisterAllowAdvancedHorizontalNavigationProperty>b__67_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowAdvancedVerticalNavigationProperty>b__66_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowBandMovingProperty>b__64_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowBandMultiRowProperty>b__83_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowBandResizingProperty>b__65_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowChangeBandParentProperty>b__62_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowChangeColumnParentProperty>b__61_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterAllowHorizontalScrollingVirtualizationProperty>b__23_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnAllowHorizontalScrollingVirtualizationChanged();
            }

            internal void <RegisterAllowPerPixelScrollingProperty>b__43_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).OnAllowPerPixelScrollingChanged();
            }

            internal void <RegisterAlternateRowBackgroundProperty>b__55_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
            }

            internal void <RegisterAlternationCountProperty>b__59_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnActualAlternateRowBackgroundChanged();
            }

            internal void <RegisterAutoWidthProperty>b__52_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnAutoWidthChanged();
            }

            internal void <RegisterBandHeaderTemplateProperty>b__69_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterBandHeaderTemplateSelectorProperty>b__70_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterBandHeaderToolTipTemplateProperty>b__71_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterColumnBandChooserTemplateProperty>b__54_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateActualColumnChooserTemplate();
            }

            internal void <RegisterColumnChooserBandsSortOrderComparerProperty>b__68_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterCompactModeFilterItemsSource>b__95_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnCompactModeFilterItemsSourceUpdate(e.OldValue);
            }

            internal void <RegisterCompactPanelShowMode>b__94_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateCompactPanelShowMode();
            }

            internal void <RegisterDataRowCompactTemplateProperty>b__29_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateCompactModeCore();
            }

            internal void <RegisterDataRowTemplateProperty>b__28_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateActualDataRowTemplateSelector();
            }

            internal void <RegisterDataRowTemplateSelectorProperty>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateActualDataRowTemplateSelector();
            }

            internal void <RegisterEvenRowBackgroundProperty>b__57_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
            }

            internal void <RegisterFixedLeftContentWidthProperty>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnFixedLeftContentWidthChanged();
            }

            internal void <RegisterFixedLineWidthProperty>b__34_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnFixedLineWidthChanged();
            }

            internal object <RegisterFixedLineWidthProperty>b__34_1(DependencyObject d, object value) => 
                Math.Max(0.0, (double) value);

            internal void <RegisterFixedNoneContentWidthProperty>b__14_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnFixedNoneContentWidthChanged();
            }

            internal void <RegisterFixedRightContentWidthProperty>b__18_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnFixedRightContentWidthChanged();
            }

            internal void <RegisterIsCompactMode>b__96_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnIsCompactModeChanged();
            }

            internal void <RegisterLeftDataAreaIndentProperty>b__51_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsPositions();
            }

            internal void <RegisterPrintBandHeaderStyleProperty>b__72_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterRightDataAreaIndentProperty>b__49_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsPositions();
            }

            internal void <RegisterRowDecorationTemplateProperty>b__31_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnRowDecorationTemplateChanged();
            }

            internal void <RegisterRowDetailsTemplateProperty>b__491_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateActualRowDetailsTemplateSelector();
            }

            internal void <RegisterRowDetailsTemplateSelectorProperty>b__492_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateActualRowDetailsTemplateSelector();
            }

            internal void <RegisterRowDetailsVisibilityModeProperty>b__494_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateDetails();
            }

            internal void <RegisterRowIndicatorContentTemplateProperty>b__25_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnRowIndicatorContentTemplateChanged();
            }

            internal void <RegisterRowMinHeightProperty>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs _)
            {
                ((ITableView) d).TableViewBehavior.OnRowMinHeightChanged();
            }

            internal void <RegisterRowStyleProperty>b__22_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnRowStyleChanged((Style) e.NewValue);
            }

            internal void <RegisterScrollingHeaderVirtualizationMarginProperty>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnScrollingVirtualizationMarginChanged();
            }

            internal void <RegisterScrollingVirtualizationMarginProperty>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnScrollingVirtualizationMarginChanged();
            }

            internal void <RegisterShowAutoFilterRowProperty>b__40_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnShowAutoFilterRowChanged();
            }

            internal void <RegisterShowBandsInCustomizationFormProperty>b__63_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterShowBandsPanelProperty>b__60_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateBandsLayoutProperties();
            }

            internal void <RegisterShowCriteriaInAutoFilterRowProperty>b__41_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnShowCriteriaInAutoFilterRowChanged();
            }

            internal void <RegisterShowHorizontalLinesProperty>b__32_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnShowHorizontalLinesChanged();
            }

            internal void <RegisterShowVerticalLinesProperty>b__33_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnShowVerticalLinesChanged();
            }

            internal void <RegisterSwitchToCompactModeWidthProperty>b__93_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateCompactModeCore();
            }

            internal void <RegisterTotalGroupAreaIndentProperty>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnTotalGroupAreaIndentChanged();
            }

            internal void <RegisterUpdateRowButtonsTemplateProperty>b__495_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.UpdateRowButtonsControl();
            }

            internal void <RegisterUseEvenRowBackgroundProperty>b__58_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnAlternateRowPropertiesChanged();
            }

            internal void <RegisterUseGroupShadowIndentProperty>b__50_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataViewBase) d).UpdateColumnsPositions();
            }

            internal void <RegisterUseLightweightTemplatesProperty>b__490_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnUseLightweightTemplatesChanged();
            }

            internal void <RegisterVerticalScrollBarWidthProperty>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ITableView) d).TableViewBehavior.OnVerticalScrollBarWidthChanged();
            }

            internal Action <StopAnimation>b__446_0(RowData x) => 
                new Action(x.StopAnimation);

            internal void <UpdateCellMergingPanelsCore>b__474_0(RowData data)
            {
                if (data.View.ActualAllowCellMerge)
                {
                    data.InvalidateCellsPanel();
                    data.UpdateIsFocusedCell();
                }
            }

            internal void <UpdateDetails>b__502_0(RowData x)
            {
                x.UpdateDetails();
            }

            internal void <UpdateVirtualizedData>b__268_0(RowData rowData)
            {
                rowData.UpdateRow();
                rowData.UpdateFixedNoneCellData(true);
            }

            internal void <UpdateVirtualizedData>b__268_1(ColumnsRowDataBase rowData)
            {
                rowData.UpdateFixedNoneCellData(true);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__80<T> where T: DependencyObject, ITableView
        {
            public static readonly TableViewBehavior.<>c__80<T> <>9;
            public static Func<T, IList> <>9__80_1;
            public static Func<T, FormatConditionBase> <>9__80_2;

            static <>c__80()
            {
                TableViewBehavior.<>c__80<T>.<>9 = new TableViewBehavior.<>c__80<T>();
            }

            internal IList <RegisterFormatConditionsSourceProperty>b__80_1(T x) => 
                x.FormatConditions;

            internal FormatConditionBase <RegisterFormatConditionsSourceProperty>b__80_2(T x) => 
                new FormatCondition();
        }

        private class BandedViewUpdateColumnsStrategy : TableViewBehavior.UpdateColumnsStrategyBase
        {
            public static readonly TableViewBehavior.UpdateColumnsStrategyBase Instance = new TableViewBehavior.BandedViewUpdateColumnsStrategy();

            internal override void FillByLastFixedColumn(TableViewBehavior viewBehavior, double arrangeWidth)
            {
            }

            private double GetBandViewportVisibleColumns(BandBase band, double currentWidth, double leftVisibleBound, double rightVisibleBound, TableViewBehavior viewBehavior, List<ColumnBase> viewportColumns)
            {
                if (band.ActualRows.Count != 0)
                {
                    foreach (BandRow row in band.ActualRows)
                    {
                        double num = currentWidth;
                        foreach (ColumnBase base2 in row.Columns)
                        {
                            if (num > (rightVisibleBound + viewBehavior.viewInfo.TotalGroupAreaIndent))
                            {
                                break;
                            }
                            if ((num + base2.ActualHeaderWidth) > leftVisibleBound)
                            {
                                viewportColumns.Add(base2);
                            }
                            num += base2.ActualHeaderWidth;
                        }
                    }
                }
                return ((band.VisibleBands.Count != 0) ? currentWidth : (currentWidth + band.ActualHeaderWidth));
            }

            private IEnumerable GetFixedBands(FixedStyle fixedStyle, DataViewBase view) => 
                (fixedStyle != FixedStyle.Left) ? ((fixedStyle != FixedStyle.Right) ? view.DataControl.BandsLayoutCore.FixedNoneVisibleBands : view.DataControl.BandsLayoutCore.FixedRightVisibleBands) : view.DataControl.BandsLayoutCore.FixedLeftVisibleBands;

            internal override FixedStyle GetFixedStyle(BaseColumn column, TableViewBehavior viewBehavior) => 
                viewBehavior.DataControl.BandsLayoutCore.GetRootBand(column.ParentBandInternal).Fixed;

            internal override IList<ColumnBase> GetFixedVisibleColumns(FixedStyle fixedStyle, DataViewBase view)
            {
                IList<ColumnBase> result = new ObservableCollection<ColumnBase>();
                BandsLayoutBase.ForeachVisibleBand(this.GetFixedBands(fixedStyle, view), delegate (BandBase band) {
                    foreach (BandRow row in band.ActualRows)
                    {
                        foreach (ColumnBase base2 in row.Columns)
                        {
                            result.Add(base2);
                        }
                    }
                });
                return result;
            }

            protected override Thickness GetScrollingMarginCore(double secondaryOffset, double virtualizationOffset) => 
                new Thickness(secondaryOffset, 0.0, 0.0, 0.0);

            internal override List<ColumnBase> GetViewportVisibleColumns(TableViewBehavior viewBehavior)
            {
                double leftVisibleBound;
                double rightVisibleBound;
                List<ColumnBase> viewportColumns = new List<ColumnBase>();
                double currentWidth = 0.0;
                base.GetColumnVisibleBounds(viewBehavior, out leftVisibleBound, out rightVisibleBound);
                BandsLayoutBase.ForeachVisibleBand(viewBehavior.DataControl.BandsLayoutCore.FixedNoneVisibleBands, delegate (BandBase band) {
                    currentWidth = this.GetBandViewportVisibleColumns(band, currentWidth, leftVisibleBound, rightVisibleBound, viewBehavior, viewportColumns);
                });
                return viewportColumns;
            }

            internal override bool HasFixedLeftElements(ITableView view) => 
                view.TableViewBehavior.DataControl.BandsLayoutCore.FixedLeftVisibleBands.Count > 0;

            internal override bool HasFixedRightElements(ITableView view) => 
                view.TableViewBehavior.DataControl.BandsLayoutCore.FixedRightVisibleBands.Count > 0;

            internal override void UpdateColumnDataWidths(DataViewBase view, GridViewInfo viewInfo, out double totalFixedNoneSize, out double totalFixedLeftSize, out double totalFixedRightSize)
            {
                totalFixedNoneSize = 0.0;
                totalFixedLeftSize = 0.0;
                totalFixedRightSize = 0.0;
                for (int i = 0; i < view.DataControl.BandsLayoutCore.VisibleBands.Count; i++)
                {
                    BandBase base2 = view.DataControl.BandsLayoutCore.VisibleBands[i];
                    double actualHeaderWidth = base2.ActualHeaderWidth;
                    if (i == 0)
                    {
                        actualHeaderWidth = (view.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? (actualHeaderWidth - viewInfo.ActualLeftDataAreaIndent) : (actualHeaderWidth - viewInfo.FirstColumnIndent);
                    }
                    if (base2.Fixed == FixedStyle.None)
                    {
                        totalFixedNoneSize += actualHeaderWidth;
                    }
                    else if (base2.Fixed == FixedStyle.Left)
                    {
                        totalFixedLeftSize += actualHeaderWidth;
                    }
                    else
                    {
                        totalFixedRightSize += actualHeaderWidth;
                    }
                }
            }
        }

        private class DefaultColumnChooserBandsSortOrderComparer : IComparer<BandBase>
        {
            public static readonly IComparer<BandBase> Instance = new TableViewBehavior.DefaultColumnChooserBandsSortOrderComparer();

            private DefaultColumnChooserBandsSortOrderComparer()
            {
            }

            int IComparer<BandBase>.Compare(BandBase x, BandBase y) => 
                Comparer<string>.Default.Compare(DataViewBase.GetColumnChooserSortableCaption(x), DataViewBase.GetColumnChooserSortableCaption(y));
        }

        private delegate void SetPropertyIntoView(ITableView view);

        private class TableViewUpdateColumnsStrategy : TableViewBehavior.UpdateColumnsStrategyBase
        {
            public static readonly TableViewBehavior.UpdateColumnsStrategyBase Instance = new TableViewBehavior.TableViewUpdateColumnsStrategy();

            internal override void FillByLastFixedColumn(TableViewBehavior viewBehavior, double arrangeWidth)
            {
                if (!viewBehavior.TableView.AutoWidth && ((viewBehavior.View.VisibleColumnsCore.Count > 1) && (viewBehavior.FixedRightVisibleColumns.Count != 0)))
                {
                    double desiredColumnsWidth = viewBehavior.ViewInfo.GetDesiredColumnsWidth(viewBehavior.View.VisibleColumnsCore);
                    double num2 = Math.Max((double) 0.0, (double) (arrangeWidth - desiredColumnsWidth));
                    if (num2 > 0.0)
                    {
                        ColumnBase base2 = viewBehavior.View.VisibleColumnsCore[viewBehavior.View.VisibleColumnsCore.Count - 1];
                        base2.ActualDataWidth += num2;
                        base2.ActualHeaderWidth += num2;
                    }
                }
            }

            internal override FixedStyle GetFixedStyle(BaseColumn column, TableViewBehavior viewBehavior) => 
                column.Fixed;

            internal override IList<ColumnBase> GetFixedVisibleColumns(FixedStyle fixedStyle, DataViewBase view)
            {
                IList<ColumnBase> list = new ObservableCollection<ColumnBase>();
                int num = 0;
                for (int i = 0; i < view.VisibleColumnsCore.Count; i++)
                {
                    ColumnBase element = view.VisibleColumnsCore[i];
                    if (element.Fixed == fixedStyle)
                    {
                        BaseColumn.SetActualCollectionIndex(element, num++);
                        list.Add(element);
                    }
                }
                return list;
            }

            protected override Thickness GetScrollingMarginCore(double secondaryOffset, double virtualizationOffset) => 
                new Thickness(secondaryOffset + virtualizationOffset, 0.0, 0.0, 0.0);

            internal override List<ColumnBase> GetViewportVisibleColumns(TableViewBehavior viewBehavior)
            {
                double num4;
                double num5;
                List<ColumnBase> list = new List<ColumnBase>();
                double num = 0.0;
                double num2 = 0.0;
                int num3 = 0;
                viewBehavior.ResetHorizontalVirtualizationOffset();
                base.GetColumnVisibleBounds(viewBehavior, out num4, out num5);
                while (true)
                {
                    if (num3 < viewBehavior.FixedNoneVisibleColumns.Count)
                    {
                        if ((num + viewBehavior.FixedNoneVisibleColumns[num3].ActualDataWidth) > num4)
                        {
                            list.Add(viewBehavior.FixedNoneVisibleColumns[num3]);
                        }
                        if (num <= num4)
                        {
                            viewBehavior.HorizontalVirtualizationOffset = num;
                            viewBehavior.HorizontalHeaderVirtualizationOffset = num2;
                        }
                        num += viewBehavior.FixedNoneVisibleColumns[num3].ActualDataWidth;
                        num2 += viewBehavior.FixedNoneVisibleColumns[num3].ActualHeaderWidth;
                        num3++;
                        if (num <= num5)
                        {
                            continue;
                        }
                    }
                    return list;
                }
            }

            internal override bool HasFixedLeftElements(ITableView view) => 
                view.TableViewBehavior.FixedLeftVisibleColumns.Count > 0;

            internal override bool HasFixedRightElements(ITableView view) => 
                view.TableViewBehavior.fixedRightVisibleColumns.Count > 0;

            internal override void UpdateColumnDataWidths(DataViewBase view, GridViewInfo viewInfo, out double totalFixedNoneSize, out double totalFixedLeftSize, out double totalFixedRightSize)
            {
                totalFixedNoneSize = 0.0;
                totalFixedLeftSize = 0.0;
                totalFixedRightSize = 0.0;
                for (int i = 0; i < view.VisibleColumnsCore.Count; i++)
                {
                    ColumnBase base2 = view.VisibleColumnsCore[i];
                    if (base2.Fixed == FixedStyle.None)
                    {
                        totalFixedNoneSize += base2.ActualDataWidth;
                    }
                    else if (base2.Fixed == FixedStyle.Left)
                    {
                        totalFixedLeftSize += base2.ActualDataWidth;
                    }
                    else
                    {
                        totalFixedRightSize += base2.ActualDataWidth;
                    }
                }
            }
        }

        private abstract class UpdateColumnsStrategyBase
        {
            protected UpdateColumnsStrategyBase()
            {
            }

            internal abstract void FillByLastFixedColumn(TableViewBehavior viewBehavior, double arrangeWidth);
            protected void GetColumnVisibleBounds(TableViewBehavior viewBehavior, out double leftVisibleBound, out double rightVisibleBound)
            {
                if (!this.IsTotalSummaryVisible(viewBehavior) || (viewBehavior.HasFixedLeftElements && viewBehavior.HasFixedRightElements))
                {
                    leftVisibleBound = viewBehavior.HorizontalOffset - viewBehavior.ViewInfo.TotalGroupAreaIndent;
                    rightVisibleBound = viewBehavior.HorizontalOffset + viewBehavior.TableView.HorizontalViewport;
                }
                else
                {
                    leftVisibleBound = viewBehavior.HorizontalOffset;
                    if (!viewBehavior.HasFixedLeftElements)
                    {
                        if (viewBehavior.TableView.ActualShowDetailButtons)
                        {
                            leftVisibleBound -= viewBehavior.TableView.ActualExpandDetailButtonWidth;
                        }
                        leftVisibleBound -= (viewBehavior.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) ? 0.0 : viewBehavior.ViewInfo.TotalGroupAreaIndent;
                        if (viewBehavior.TableView.ViewBase.IsRootView && viewBehavior.TableView.ActualShowIndicator)
                        {
                            leftVisibleBound -= viewBehavior.TableView.ActualIndicatorWidth;
                        }
                    }
                    double totalSummaryFixedNoneContentWidth = 0.0;
                    totalSummaryFixedNoneContentWidth = viewBehavior.TableView.TotalSummaryFixedNoneContentWidth;
                    rightVisibleBound = leftVisibleBound + totalSummaryFixedNoneContentWidth;
                }
            }

            internal abstract FixedStyle GetFixedStyle(BaseColumn column, TableViewBehavior viewBehavior);
            internal abstract IList<ColumnBase> GetFixedVisibleColumns(FixedStyle fixedStyle, DataViewBase view);
            internal Thickness GetScrollingMargin(ITableView tableView, double secondaryOffset, double virtualizationOffset) => 
                this.GetScrollingMarginCore(secondaryOffset, virtualizationOffset);

            protected abstract Thickness GetScrollingMarginCore(double secondaryOffset, double virtualizationOffset);
            internal abstract List<ColumnBase> GetViewportVisibleColumns(TableViewBehavior viewBehavior);
            internal abstract bool HasFixedLeftElements(ITableView view);
            internal abstract bool HasFixedRightElements(ITableView view);
            private bool IsTotalSummaryVisible(TableViewBehavior viewBehavior)
            {
                bool flag;
                List<DataControlBase> dataControls = new List<DataControlBase>();
                viewBehavior.View.DataControl.EnumerateThisAndParentDataControls(delegate (DataControlBase dataControl) {
                    dataControls.Add(dataControl);
                });
                using (List<DataControlBase>.Enumerator enumerator = dataControls.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            DataControlBase current = enumerator.Current;
                            if (!current.viewCore.ShowTotalSummary)
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            internal abstract void UpdateColumnDataWidths(DataViewBase view, GridViewInfo viewInfo, out double totalFixedNoneSize, out double totalFixedLeftSize, out double totalFixedRightSize);
        }
    }
}

