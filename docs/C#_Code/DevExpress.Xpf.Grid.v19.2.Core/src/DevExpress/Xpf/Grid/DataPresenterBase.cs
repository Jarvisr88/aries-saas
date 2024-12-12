namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public abstract class DataPresenterBase : ContentControl, IScrollInfoOwner, IScrollInfo, INotifyCurrentViewChanged
    {
        public const double DefaultTouchScrollThreshold = 3.0;
        public const double WheelScrollLinesPerPage = -1.0;
        private DevExpress.Xpf.Grid.Native.ScrollInfo scrollInfoCore;
        private Size lastConstraint;
        private DataViewBase viewCore;
        private IContinousAction currentContinousAction;
        private Queue<IContinousAction> continousActionsQueue = new Queue<IContinousAction>();
        private bool prohibitAdjustment;
        private bool adjustmentInProgress;
        private double childDesiredHeight;
        private double lastEmptyHeight;
        protected bool rendered;
        private double adjustmentDelta;
        internal ScrollAccumulator accumulatorHorizontal = new ScrollAccumulator();
        internal ScrollAccumulator accumulatorVertical = new ScrollAccumulator();

        static DataPresenterBase()
        {
            GenerateItemsCount = 5;
        }

        protected DataPresenterBase()
        {
            base.Unloaded += new RoutedEventHandler(this.DataPresenter_Unloaded);
            base.Loaded += new RoutedEventHandler(this.DataPresenter_Loaded);
            base.Content = this.CreateContent();
            base.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            base.FocusVisualStyle = null;
        }

        internal void AddUpdateRowsStateAction()
        {
            this.View.EnqueueImmediateAction(new Action(this.View.ForceUpdateRowsState));
        }

        protected bool AreAllElementsVisible(NodeContainer containerItem)
        {
            if (containerItem.Items.Count != 0)
            {
                for (int i = containerItem.Items.Count - 1; i >= 0; i--)
                {
                    RowNode node = containerItem.Items[i];
                    if (!this.IsElementVisible(node) && (node.FixedRowPosition != FixedRowPosition.Bottom))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override Size ArrangeOverride(Size arrangeBounds) => 
            (this.View != null) ? this.ArrangeOverrideCore(arrangeBounds) : arrangeBounds;

        protected virtual Size ArrangeOverrideCore(Size arrangeBounds) => 
            base.ArrangeOverride(arrangeBounds);

        private static void AutoSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        protected Size BaseMeasureOverride(Size constraint) => 
            base.MeasureOverride(this.GetMeasureSize(constraint));

        internal bool CanApplyScroll()
        {
            FrameworkElement firstVisibleRowElement = null;
            return this.CanApplyScroll(out firstVisibleRowElement);
        }

        internal bool CanApplyScroll(out FrameworkElement firstVisibleRowElement)
        {
            firstVisibleRowElement = null;
            if (this.View == null)
            {
                return false;
            }
            firstVisibleRowElement = this.View.DataPresenter.GetFirstVisibleRow();
            return (firstVisibleRowElement != null);
        }

        protected virtual void CancelAllGetRows()
        {
        }

        protected virtual bool CheckIsTreeValid() => 
            UIElementHelper.IsVisibleInTree(this, true);

        internal void ClearInvisibleItems()
        {
            if (this.View != null)
            {
                this.View.RootNodeContainer.ReGenerateItemsCore(this.GenerateItemsOffset, this.FullyVisibleItemsCount);
            }
        }

        internal void ClearScrollInfoAndUpdate()
        {
            this.scrollInfoCore.ClearScrollInfo();
            this.UpdateScrollInfo();
            this.SetVerticalOffsetForce(0.0);
        }

        protected abstract FrameworkElement CreateContent();
        protected virtual VirtualDataStackPanelScrollInfo CreateScrollInfo() => 
            new VirtualDataStackPanelScrollInfo(this);

        private void DataPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            this.UpdateView();
            base.InvalidateMeasure();
        }

        private void DataPresenter_Unloaded(object sender, RoutedEventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
        }

        void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
        {
            this.viewCore = DataControlBase.GetCurrentView(this);
            this.UpdateView();
        }

        void IScrollInfoOwner.InvalidateHorizontalScrolling()
        {
            if (this.View != null)
            {
                this.View.OnInvalidateHorizontalScrolling();
            }
        }

        void IScrollInfoOwner.InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        bool IScrollInfoOwner.OnBeforeChangeItemScrollOffset() => 
            this.View.RequestUIUpdate();

        bool IScrollInfoOwner.OnBeforeChangePixelScrollOffset()
        {
            Func<DataViewBase, bool> evaluator = <>c.<>9__89_0;
            if (<>c.<>9__89_0 == null)
            {
                Func<DataViewBase, bool> local1 = <>c.<>9__89_0;
                evaluator = <>c.<>9__89_0 = x => x.OnBeforeChangePixelScrollOffset();
            }
            return this.View.Return<DataViewBase, bool>(evaluator, (<>c.<>9__89_1 ??= () => true));
        }

        void IScrollInfoOwner.OnDefineScrollInfoChanged()
        {
            this.OnDefineScrollInfoChangedCore();
        }

        void IScrollInfoOwner.OnSecondaryScrollInfoChanged()
        {
            this.OnSecondaryScrollInfoChangedCore();
        }

        double IScrollInfoOwner.ScrollInsideActiveEditorIfNeeded(Visual visual, Rect rectangle)
        {
            if (this.View == null)
            {
                return 0.0;
            }
            Func<bool> fallback = <>c.<>9__116_1;
            if (<>c.<>9__116_1 == null)
            {
                Func<bool> local1 = <>c.<>9__116_1;
                fallback = <>c.<>9__116_1 = () => true;
            }
            if (this.View.ActiveEditor.Return<BaseEdit, bool>(x => !LayoutHelper.IsChildElement(x, visual), fallback))
            {
                return 0.0;
            }
            RowData rowDataToScroll = this.GetRowDataToScroll() as RowData;
            if ((rowDataToScroll == null) || (!this.View.ViewBehavior.AllowPerPixelScrolling && (rowDataToScroll.RowHandle.Value == this.View.FocusedRowHandle)))
            {
                return 0.0;
            }
            double invisiblePart = this.GetInvisiblePart(visual.TransformToVisual(this.View.ScrollContentPresenter).TransformBounds(rectangle));
            if (invisiblePart == 0.0)
            {
                return 0.0;
            }
            this.View.LockEditorClose = true;
            if (invisiblePart < 0.0)
            {
                this.SetVerticalOffsetForce(this.CurrentOffset + (invisiblePart / rowDataToScroll.WholeRowElement.ActualHeight));
            }
            else
            {
                this.SetVerticalOffsetForce(this.ScrollOffset + this.View.CalcOffsetByHeight(invisiblePart, this.View.ViewBehavior.AllowPerPixelScrolling));
            }
            this.View.LockEditorClose = false;
            return invisiblePart;
        }

        internal void EnqueueContinousAction(IContinousAction action)
        {
            this.continousActionsQueue.Enqueue(action);
        }

        protected virtual void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount)
        {
        }

        protected virtual void ExecuteImmediateActions()
        {
            this.View.ImmediateActionsManager.ExecuteActions();
        }

        internal void ForceCompleteContinuousActions()
        {
            this.ForceCompleteCurrentAction();
            this.continousActionsQueue.Clear();
        }

        internal void ForceCompleteCurrentAction()
        {
            if (this.currentContinousAction != null)
            {
                this.currentContinousAction.ForceComplete();
                this.currentContinousAction = null;
            }
        }

        protected virtual void GenerateItems(int count, double availableHeight)
        {
            this.View.RootNodeContainer.GenerateItems(count);
        }

        protected double GetChildHeight()
        {
            UIElement content = base.Content as UIElement;
            return ((content != null) ? content.DesiredSize.Height : 0.0);
        }

        protected internal FrameworkElement GetFirstVisibleRow()
        {
            RowDataBase rowDataToScroll = this.GetRowDataToScroll();
            return rowDataToScroll?.WholeRowElement;
        }

        private double GetInvisiblePart(Rect rect)
        {
            if (rect.Top < 0.0)
            {
                return rect.Top;
            }
            double num = rect.Bottom - this.View.ScrollContentPresenter.ActualHeight;
            return ((num <= 0.0) ? 0.0 : num);
        }

        protected virtual Size GetMeasureSize(Size constraint) => 
            new Size(constraint.Width, double.PositiveInfinity);

        protected virtual double GetOffset() => 
            0.0;

        protected internal RowDataBase GetRowDataToScroll()
        {
            if (this.View.DataControl == null)
            {
                return null;
            }
            return this.View.RootNodeContainer.GetNodeToScroll()?.GetRowData();
        }

        internal bool IsElementPartiallyVisible(FrameworkElement element)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, this);
            return this.IsElementPartiallyVisible(this.LastConstraint, relativeElementRect);
        }

        internal bool IsElementPartiallyVisible(Size size, Rect elementRect)
        {
            Func<HierarchyPanel, double> evaluator = <>c.<>9__148_0;
            if (<>c.<>9__148_0 == null)
            {
                Func<HierarchyPanel, double> local1 = <>c.<>9__148_0;
                evaluator = <>c.<>9__148_0 = x => x.FixedBottomRowsHeight;
            }
            return (this.SizeHelper.GetDefinePoint(elementRect.BottomRight()) > (this.SizeHelper.GetDefineSize(size) - this.Panel.Return<HierarchyPanel, double>(evaluator, (<>c.<>9__148_1 ??= () => 0.0))));
        }

        protected virtual bool IsElementVisible(RowNode node)
        {
            if (!node.IsRowVisible)
            {
                return true;
            }
            FrameworkElement rowElement = node.GetRowElement();
            if (rowElement == null)
            {
                return false;
            }
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(rowElement, this);
            Func<HierarchyPanel, double> evaluator = <>c.<>9__149_0;
            if (<>c.<>9__149_0 == null)
            {
                Func<HierarchyPanel, double> local1 = <>c.<>9__149_0;
                evaluator = <>c.<>9__149_0 = x => x.FixedBottomRowsHeight;
            }
            return (this.SizeHelper.GetDefinePoint(relativeElementRect.Location()) < ((this.SizeHelper.GetDefineSize(this.LastConstraint) + this.CollapseBufferSize) - this.Panel.Return<HierarchyPanel, double>(evaluator, (<>c.<>9__149_1 ??= () => 0.0))));
        }

        internal virtual bool IsEnoughExpandingItems() => 
            true;

        private bool IsEnoughItems()
        {
            if (this.IsEnoughExpandingItems())
            {
                if (!this.View.RootNodeContainer.IsFinished)
                {
                    return !this.AreAllElementsVisible(this.View.RootNodeContainer);
                }
                if (((this.View.DataControl == null) || (this.View.DataControl.BottomRowBelowOldVisibleRowCount && !this.ShouldDisplayLoadingRow)) || this.View.RootNodeContainer.IsEnumeratorValid)
                {
                    return true;
                }
                this.View.RootNodeContainer.OnDataChangedCore();
            }
            return false;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.DataControl == null)
            {
                return new Size();
            }
            if ((base.DesiredSize.Height != constraint.Height) && !this.adjustmentInProgress)
            {
                this.prohibitAdjustment = false;
            }
            if (this.View == null)
            {
                return new Size(0.0, 0.0);
            }
            bool flag = ((this.currentContinousAction == null) && (this.LastConstraint != constraint)) && !this.adjustmentInProgress;
            this.LastConstraint = ColumnsLayoutParametersValidator.GetLastDataPresenterConstraint(constraint);
            if (!this.View.IsDesignTime)
            {
                InfiniteGridSizeException.ValidateDefineSize(this.SizeHelper.GetDefineSize(this.LastConstraint), this.View.OrientationCore, this.DataControl.GetType().Name);
            }
            ClearAutomationEventsHelper.ClearAutomationEvents();
            this.PregenerateItems(constraint);
            int itemCount = this.View.RootNodeContainer.ItemCount;
            if (this.AdjustmentInProgress)
            {
                itemCount += (int) Math.Ceiling(this.adjustmentDelta);
            }
            this.CancelAllGetRows();
            int commonStartScrollIndex = Math.Min(this.GenerateItemsOffset, Math.Max(0, this.ItemCount - 1));
            this.View.RootNodeContainer.ReGenerateItems(commonStartScrollIndex, itemCount, true);
            this.EnsureAllRowsLoaded(this.ScrollOffset, itemCount);
            Size result = base.MeasureOverride(this.GetMeasureSize(constraint));
            if (this.DataControl.AutomationPeer != null)
            {
                this.DataControl.AutomationPeer.ResetDataPanelChildrenForce();
            }
            if ((this.childDesiredHeight != result.Height) && !this.adjustmentInProgress)
            {
                this.prohibitAdjustment = false;
            }
            this.childDesiredHeight = result.Height;
            if (flag)
            {
                this.UpdateScrollInfo();
            }
            return this.View.ViewBehavior.CorrectMeasureResult(this.ActualScrollOffset, constraint, result);
        }

        protected virtual void OnDefineScrollInfoChangedCore()
        {
            this.ScrollInfoCore.OnScrollInfoChanged();
            if (this.View != null)
            {
                this.View.ViewBehavior.UpdateTopRowIndex();
            }
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.rendered = true;
            if (!LayoutUpdatedHelper.GlobalLocker.IsLocked && ((this.View == null) || !this.View.layoutUpdatedLocker.IsLocked))
            {
                if (!this.CheckIsTreeValid())
                {
                    this.ForceCompleteContinuousActions();
                }
                else if ((this.View != null) && (this.DataControl != null))
                {
                    this.View.RaiseCommandsCanExecuteChanged();
                    this.DataControl.UpdateAllDetailAndOriginationDataControls(dataControl => dataControl.DataView.ViewBehavior.EnsureSurroundingsActualSize(this.LastConstraint));
                    if ((this.currentContinousAction != null) && this.currentContinousAction.IsDone)
                    {
                        this.currentContinousAction = null;
                    }
                    if ((this.currentContinousAction == null) && this.HasQueuedContinuousActions)
                    {
                        this.currentContinousAction = this.continousActionsQueue.Dequeue();
                        this.currentContinousAction.Prepare();
                        base.InvalidateMeasure();
                    }
                    else if (this.OnLayoutUpdatedCore())
                    {
                        this.View.MasterRootRowsContainer.SynchronizationQueues.ClearFreeRowData();
                        Action<DataControlBase> updateMethod = <>c.<>9__52_1;
                        if (<>c.<>9__52_1 == null)
                        {
                            Action<DataControlBase> local1 = <>c.<>9__52_1;
                            updateMethod = <>c.<>9__52_1 = dataControl => dataControl.OwnerDetailDescriptor.SynchronizationQueues.ClearFreeRowData();
                        }
                        this.View.DataControl.MasterDetailProvider.UpdateOriginationDataControls(updateMethod);
                        if (this.currentContinousAction == null)
                        {
                            this.UpdateScrollInfo();
                            if (!this.View.FocusedView.IsRootView)
                            {
                                this.View.FocusedView.ClearCurrentCellIfNeeded();
                            }
                            Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__52_2;
                            if (<>c.<>9__52_2 == null)
                            {
                                Action<DataControlBase> local2 = <>c.<>9__52_2;
                                updateOpenDetailMethod = <>c.<>9__52_2 = dataControl => dataControl.DataView.UpdateRowsState();
                            }
                            this.View.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                            this.ExecuteImmediateActions();
                        }
                        if (this.currentContinousAction != null)
                        {
                            this.currentContinousAction.Execute();
                        }
                    }
                }
            }
        }

        protected virtual bool OnLayoutUpdatedCore()
        {
            if (!this.IsEnoughItems())
            {
                this.GenerateItems(GenerateItemsCount, this.LastConstraint.Height);
                return false;
            }
            this.View.RootNodeContainer.IsScrolling = false;
            if (this.View.CanAdjustScrollbar())
            {
                if (!this.prohibitAdjustment && (this.currentContinousAction == null))
                {
                    double childHeight = this.GetChildHeight();
                    if (this.adjustmentInProgress && (childHeight > base.DesiredSize.Height))
                    {
                        this.prohibitAdjustment = true;
                        if (!this.View.ViewBehavior.AllowPerPixelScrolling)
                        {
                            this.SetVerticalOffsetForce(this.CurrentOffset + 1.0);
                        }
                        else
                        {
                            double height = this.GetFirstVisibleRow().DesiredSize.Height;
                            this.SetVerticalOffsetForce(this.CurrentOffset + (((height * (1.0 - this.ScrollItemOffset)) - this.lastEmptyHeight) / height));
                        }
                        return false;
                    }
                    if ((childHeight < base.DesiredSize.Height) && (this.CurrentOffset > 0.0))
                    {
                        this.lastEmptyHeight = base.DesiredSize.Height - childHeight;
                        this.adjustmentInProgress = true;
                        this.View.LockEditorClose = true;
                        double num3 = 1.0;
                        if (this.View.ViewBehavior.AllowPerPixelScrolling && ((this.ScrollItemOffset != 0.0) && (this.GetFirstVisibleRow() != null)))
                        {
                            double height = this.GetFirstVisibleRow().DesiredSize.Height;
                            num3 = ((height * this.ScrollItemOffset) <= (base.DesiredSize.Height - childHeight)) ? this.ScrollItemOffset : ((base.DesiredSize.Height - childHeight) / height);
                        }
                        this.adjustmentDelta = num3;
                        if ((!this.View.IsEditFormVisible || (!this.View.ViewBehavior.AllowPerPixelScrolling && (Math.Abs(num3) > 1.0))) || (this.View.ViewBehavior.AllowPerPixelScrolling && (Math.Abs(num3) > 0.1)))
                        {
                            this.SetVerticalOffsetForce(this.CurrentOffset - num3);
                            return false;
                        }
                    }
                }
                this.View.LockEditorClose = false;
                if (this.adjustmentInProgress)
                {
                    this.adjustmentInProgress = false;
                    this.UpdatePostponedData();
                }
            }
            return true;
        }

        protected virtual void OnMouseWheelDown()
        {
            if ((this.View == null) || !this.View.IsEditFormVisible)
            {
                this.ScrollInfo.MouseWheelDown();
            }
        }

        protected virtual void OnMouseWheelUp()
        {
            if ((this.View == null) || !this.View.IsEditFormVisible)
            {
                this.ScrollInfo.MouseWheelUp();
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, UIElement.IsVisibleProperty) && base.IsVisible)
            {
                base.InvalidateMeasure();
            }
        }

        private void OnSecondaryScrollInfoChangedCore()
        {
            this.ScrollInfoCore.OnScrollInfoChanged();
            if (this.View != null)
            {
                this.UpdateSecondarySizeScrollInfo(false);
                Action<DataControlBase> action = <>c.<>9__119_0;
                if (<>c.<>9__119_0 == null)
                {
                    Action<DataControlBase> local1 = <>c.<>9__119_0;
                    action = <>c.<>9__119_0 = delegate (DataControlBase x) {
                        Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__119_1;
                        if (<>c.<>9__119_1 == null)
                        {
                            Action<DataControlBase> local1 = <>c.<>9__119_1;
                            updateOpenDetailMethod = <>c.<>9__119_1 = dc => dc.DataView.ViewBehavior.UpdateViewportVisibleColumns();
                        }
                        x.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
                    };
                }
                this.View.DataControl.Do<DataControlBase>(action);
                DataViewBehavior viewBehavior = this.View.ViewBehavior;
                this.View.EnqueueImmediateAction(new Action(viewBehavior.ResetHeadersChildrenCache));
            }
        }

        protected virtual void PregenerateItems(Size constraint)
        {
        }

        internal void SetDefineScrollOffset(double scrollOffset, bool skipValidation = false)
        {
            this.ScrollInfoCore.DefineSizeScrollInfo.SetOffsetForce(scrollOffset, skipValidation);
        }

        internal void SetHorizontalOffsetForce(double value)
        {
            this.ScrollInfoCore.SetHorizontalOffsetForce(value);
        }

        protected internal virtual void SetManipulation(bool isColumnFilterOpened)
        {
        }

        internal void SetVerticalOffsetForce(double value)
        {
            this.ScrollInfoCore.SetVerticalOffsetForce(value);
        }

        void IScrollInfo.LineDown()
        {
            this.ScrollInfo.LineDown();
        }

        void IScrollInfo.LineLeft()
        {
            this.ScrollInfo.LineLeft();
        }

        void IScrollInfo.LineRight()
        {
            this.ScrollInfo.LineRight();
        }

        void IScrollInfo.LineUp()
        {
            this.ScrollInfo.LineUp();
        }

        Rect IScrollInfo.MakeVisible(Visual visual, Rect rectangle) => 
            this.ScrollInfo.MakeVisible(visual, rectangle);

        void IScrollInfo.MouseWheelDown()
        {
            this.OnMouseWheelDown();
        }

        void IScrollInfo.MouseWheelLeft()
        {
            this.ScrollInfo.MouseWheelLeft();
        }

        void IScrollInfo.MouseWheelRight()
        {
            this.ScrollInfo.MouseWheelRight();
        }

        void IScrollInfo.MouseWheelUp()
        {
            this.OnMouseWheelUp();
        }

        void IScrollInfo.PageDown()
        {
            this.ScrollInfo.PageDown();
        }

        void IScrollInfo.PageLeft()
        {
            this.ScrollInfo.PageLeft();
        }

        void IScrollInfo.PageRight()
        {
            this.ScrollInfo.PageRight();
        }

        void IScrollInfo.PageUp()
        {
            this.ScrollInfo.PageUp();
        }

        void IScrollInfo.SetHorizontalOffset(double offset)
        {
            this.ScrollInfo.SetHorizontalOffset(offset);
        }

        void IScrollInfo.SetVerticalOffset(double offset)
        {
            this.ScrollInfo.SetVerticalOffset(offset);
        }

        protected internal virtual void UpdateAutoSize()
        {
            this.ScrollInfoCore.HorizontalScrollInfo.SetOffsetForce(0.0, false);
        }

        private void UpdateDefineSizeScrollInfo()
        {
            double viewportHeight = this.ScrollInfo.ViewportHeight;
            this.ScrollInfoCore.DefineSizeScrollInfo.UpdateScrollInfo((this.View.ScrollingModeCore == ScrollingMode.Normal) ? 1.0 : Math.Max(0.0, this.PanelViewport), this.Extent);
            if (viewportHeight != this.ScrollInfo.ViewportHeight)
            {
                this.UpdatePostponedData();
                this.View.UpdateCellMergingPanels(false);
            }
        }

        private void UpdatePostponedData()
        {
            this.View.MasterRootRowsContainer.UpdatePostponedData(false, false);
        }

        internal void UpdateScrollInfo()
        {
            this.UpdateDefineSizeScrollInfo();
            this.UpdateSecondarySizeScrollInfo(true);
        }

        internal void UpdateSecondarySizeScrollInfo(bool allowUpdateViewportVisibleColumns = true)
        {
            if (((this.View != null) && ((this.View.DataProviderBase == null) || !this.View.DataProviderBase.IsUpdateLocked)) && (this.DataControl != null))
            {
                ScrollBarCombineHelper helper = new ScrollBarCombineHelper();
                this.DataControl.UpdateAllDetailDataControls(dataControl => helper.ProcessScrollInfo(dataControl.DataView.FixedViewport, dataControl.DataView.FixedExtent), null);
                this.ScrollInfoCore.SecondarySizeScrollInfo.UpdateScrollInfo(helper.FinalViewport, helper.FinalExtent);
                this.ScrollInfoCore.UpdateIsHorizontalScrollBarVisible();
                this.DataControl.UpdateAllDetailDataControls(dataControl => dataControl.DataView.ViewBehavior.UpdateSecondaryScrollInfoCore(-helper.ConvertToRealOffset(this.ScrollInfoCore.SecondarySizeScrollInfo.Offset, dataControl.DataView.FixedViewport, dataControl.DataView.FixedExtent), allowUpdateViewportVisibleColumns), null);
            }
        }

        protected virtual void UpdateView()
        {
            if (this.View != null)
            {
                this.View.ScrollInfoOwner = this;
                this.View.WidthChanged(double.NaN, double.NaN, true);
                this.AddUpdateRowsStateAction();
                this.UpdateViewCore();
            }
        }

        protected abstract void UpdateViewCore();
        public bool UseSmartMouseScrolling() => 
            true;

        internal static int GenerateItemsCount { get; set; }

        internal Size LastConstraint
        {
            get => 
                this.lastConstraint;
            private set
            {
                if (this.lastConstraint != value)
                {
                    this.lastConstraint = value;
                    this.View.OnLastConstraintChanged();
                    if (this.Panel != null)
                    {
                        this.Panel.CalcViewport(this.lastConstraint);
                    }
                }
            }
        }

        internal HierarchyPanel Panel { get; set; }

        private bool HasQueuedContinuousActions =>
            this.continousActionsQueue.Count > 0;

        public virtual double ScrollItemOffset =>
            0.0;

        internal int ItemCount =>
            ((this.View == null) || !this.View.IsPagingMode) ? ((this.DataControl == null) ? 0 : ((this.DataControl.VisibleRowCount + this.View.CalcGroupSummaryVisibleRowCount()) + this.View.DataControl.MasterDetailProvider.CalcVisibleDetailRowsCount())) : this.View.ItemsOnPage;

        internal FrameworkElement ContentElement =>
            base.Content as FrameworkElement;

        public DevExpress.Xpf.Grid.Native.ScrollInfo ScrollInfoCore
        {
            get
            {
                this.scrollInfoCore ??= this.CreateScrollInfo();
                return this.scrollInfoCore;
            }
        }

        internal DataViewBase View =>
            this.viewCore;

        internal DataControlBase DataControl =>
            this.View?.DataControl;

        internal SizeHelperBase SizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.View.OrientationCore);

        internal double CollapseBufferSize { get; set; }

        internal bool IsInAction =>
            this.currentContinousAction != null;

        internal bool AdjustmentInProgress =>
            this.adjustmentInProgress;

        FrameworkElement IScrollInfoOwner.ScrollContentPresenter =>
            this.View.ScrollContentPresenter;

        int IScrollInfoOwner.ScrollStep =>
            this.View.ScrollStep;

        bool IScrollInfoOwner.IsDeferredScrolling =>
            this.View.IsDeferredScrolling;

        int IScrollInfoOwner.ItemCount
        {
            get
            {
                Func<DataViewBase, int> evaluator = <>c.<>9__97_0;
                if (<>c.<>9__97_0 == null)
                {
                    Func<DataViewBase, int> local1 = <>c.<>9__97_0;
                    evaluator = <>c.<>9__97_0 = x => x.ActualFixedTopRowsCount + x.ActualFixedBottomRowsCount;
                }
                return (this.ItemCount - this.View.Return<DataViewBase, int>(evaluator, (<>c.<>9__97_1 ??= () => 0)));
            }
        }

        int IScrollInfoOwner.Offset =>
            this.ScrollOffset;

        DataControlScrollMode IScrollInfoOwner.VerticalScrollMode =>
            this.VerticalScrollModeCore;

        protected virtual DataControlScrollMode VerticalScrollModeCore =>
            this.View.ViewBehavior.AllowPerPixelScrolling ? DataControlScrollMode.ItemPixel : DataControlScrollMode.Item;

        DataControlScrollMode IScrollInfoOwner.HorizontalScrollMode =>
            this.HorizontalScrollModeCore;

        protected virtual DataControlScrollMode HorizontalScrollModeCore =>
            DataControlScrollMode.Pixel;

        bool IScrollInfoOwner.IsHorizontalScrollBarVisible
        {
            get => 
                this.View.IsHorizontalScrollBarVisible;
            set => 
                this.View.IsHorizontalScrollBarVisible = value;
        }

        bool IScrollInfoOwner.IsTouchScrollBarsMode
        {
            get => 
                this.View.IsTouchScrollBarsMode;
            set => 
                this.View.IsTouchScrollBarsMode = value;
        }

        int IScrollInfoOwner.ItemsOnPage =>
            this.ViewPort;

        internal int FullyVisibleItemsCount =>
            (this.Panel != null) ? this.Panel.FullyVisibleItemsCount : 0;

        private double PanelViewport =>
            (this.Panel != null) ? this.Panel.Viewport : 0.0;

        protected virtual double Extent =>
            (this.View != null) ? ((double) Math.Max(0, (this.ItemCount - this.viewCore.ActualFixedTopRowsCount) - this.viewCore.ActualFixedBottomRowsCount)) : ((double) this.ItemCount);

        protected internal int ViewPort =>
            (int) Math.Ceiling(this.PanelViewport);

        protected internal int ScrollOffset =>
            (int) this.ScrollInfoCore.DefineSizeScrollInfo.Offset;

        protected internal virtual int GenerateItemsOffset =>
            this.ScrollOffset;

        protected internal double ActualScrollOffset =>
            this.ScrollInfoCore.DefineSizeScrollInfo.Offset;

        protected internal double ActualViewPort =>
            this.ScrollInfoCore.DefineSizeScrollInfo.Viewport;

        protected internal virtual bool CanScrollWithAnimation =>
            false;

        protected internal virtual bool IsAnimationInProgress =>
            false;

        protected internal virtual double CurrentOffset =>
            this.ActualScrollOffset;

        private bool ShouldDisplayLoadingRow =>
            this.View.ShouldDisplayLoadingRow && (this.View.RootNodeContainer.ItemCount == 0);

        public double WheelScrollLines =>
            (this.viewCore != null) ? this.viewCore.WheelScrollLines : ((double) SystemParameters.WheelScrollLines);

        protected IScrollInfo ScrollInfo =>
            ((this.View == null) || (this.DataControl == null)) ? ((IScrollInfo) new FakeScrollInfo()) : ((IScrollInfo) this.ScrollInfoCore);

        bool IScrollInfo.CanHorizontallyScroll
        {
            get => 
                this.ScrollInfo.CanHorizontallyScroll;
            set => 
                this.ScrollInfo.CanHorizontallyScroll = value;
        }

        bool IScrollInfo.CanVerticallyScroll
        {
            get => 
                this.ScrollInfo.CanVerticallyScroll;
            set => 
                this.ScrollInfo.CanVerticallyScroll = value;
        }

        double IScrollInfo.ExtentHeight =>
            this.ScrollInfo.ExtentHeight;

        double IScrollInfo.ExtentWidth =>
            this.ScrollInfo.ExtentWidth;

        double IScrollInfo.HorizontalOffset =>
            this.ScrollInfo.HorizontalOffset;

        ScrollViewer IScrollInfo.ScrollOwner
        {
            get => 
                this.ScrollInfo.ScrollOwner;
            set => 
                this.ScrollInfo.ScrollOwner = value;
        }

        double IScrollInfo.VerticalOffset =>
            this.ScrollInfo.VerticalOffset;

        double IScrollInfo.ViewportHeight =>
            this.ScrollInfo.ViewportHeight;

        double IScrollInfo.ViewportWidth =>
            this.ScrollInfo.ViewportWidth;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataPresenterBase.<>c <>9 = new DataPresenterBase.<>c();
            public static Action<DataControlBase> <>9__52_1;
            public static Action<DataControlBase> <>9__52_2;
            public static Func<DataViewBase, bool> <>9__89_0;
            public static Func<bool> <>9__89_1;
            public static Func<DataViewBase, int> <>9__97_0;
            public static Func<int> <>9__97_1;
            public static Func<bool> <>9__116_1;
            public static Action<DataControlBase> <>9__119_1;
            public static Action<DataControlBase> <>9__119_0;
            public static Func<HierarchyPanel, double> <>9__148_0;
            public static Func<double> <>9__148_1;
            public static Func<HierarchyPanel, double> <>9__149_0;
            public static Func<double> <>9__149_1;

            internal int <DevExpress.Xpf.Grid.IScrollInfoOwner.get_ItemCount>b__97_0(DataViewBase x) => 
                x.ActualFixedTopRowsCount + x.ActualFixedBottomRowsCount;

            internal int <DevExpress.Xpf.Grid.IScrollInfoOwner.get_ItemCount>b__97_1() => 
                0;

            internal bool <DevExpress.Xpf.Grid.IScrollInfoOwner.OnBeforeChangePixelScrollOffset>b__89_0(DataViewBase x) => 
                x.OnBeforeChangePixelScrollOffset();

            internal bool <DevExpress.Xpf.Grid.IScrollInfoOwner.OnBeforeChangePixelScrollOffset>b__89_1() => 
                true;

            internal bool <DevExpress.Xpf.Grid.IScrollInfoOwner.ScrollInsideActiveEditorIfNeeded>b__116_1() => 
                true;

            internal double <IsElementPartiallyVisible>b__148_0(HierarchyPanel x) => 
                x.FixedBottomRowsHeight;

            internal double <IsElementPartiallyVisible>b__148_1() => 
                0.0;

            internal double <IsElementVisible>b__149_0(HierarchyPanel x) => 
                x.FixedBottomRowsHeight;

            internal double <IsElementVisible>b__149_1() => 
                0.0;

            internal void <OnLayoutUpdated>b__52_1(DataControlBase dataControl)
            {
                dataControl.OwnerDetailDescriptor.SynchronizationQueues.ClearFreeRowData();
            }

            internal void <OnLayoutUpdated>b__52_2(DataControlBase dataControl)
            {
                dataControl.DataView.UpdateRowsState();
            }

            internal void <OnSecondaryScrollInfoChangedCore>b__119_0(DataControlBase x)
            {
                Action<DataControlBase> updateOpenDetailMethod = <>9__119_1;
                if (<>9__119_1 == null)
                {
                    Action<DataControlBase> local1 = <>9__119_1;
                    updateOpenDetailMethod = <>9__119_1 = dc => dc.DataView.ViewBehavior.UpdateViewportVisibleColumns();
                }
                x.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
            }

            internal void <OnSecondaryScrollInfoChangedCore>b__119_1(DataControlBase dc)
            {
                dc.DataView.ViewBehavior.UpdateViewportVisibleColumns();
            }
        }
    }
}

