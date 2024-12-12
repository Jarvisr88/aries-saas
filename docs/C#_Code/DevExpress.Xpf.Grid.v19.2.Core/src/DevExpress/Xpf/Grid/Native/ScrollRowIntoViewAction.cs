namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScrollRowIntoViewAction : IAction
    {
        private DataViewBase view;
        private int rowHandle;
        private int tryCount;
        private bool prohibitAnimation;

        public ScrollRowIntoViewAction(DataViewBase view, int rowHandle, int tryCount)
        {
            this.view = view;
            this.rowHandle = rowHandle;
            this.tryCount = tryCount;
            this.prohibitAnimation = this.View.ScrollAnimationLocker.IsLocked || this.View.DataControl.IsDataResetLocked;
        }

        private void EnqueueScrollIntoViewAgain()
        {
            ScrollRowIntoViewAction action = new ScrollRowIntoViewAction(this.View, this.RowHandle, this.tryCount + 1);
            action.UseLock = this.UseLock;
            action.PostAction = this.PostAction;
            this.View.EnqueueImmediateAction(action);
        }

        public void Execute()
        {
            try
            {
                if (this.UseLock)
                {
                    this.View.EnqueueScrollIntoViewLocker.Lock();
                }
                if ((this.View.DataControl.IsValidRowHandleCore(this.RowHandle) && (this.View.DataControl.IsRowVisibleCore(this.RowHandle) && ((!this.View.HasFixedRows || (this.View.GetFixedRowByItemCore(this.View.DataControl.GetRow(this.RowHandle)) == FixedRowPosition.None)) && (!this.IsTopNewItemRow || !this.View.IsRootView)))) && (this.tryCount <= 8))
                {
                    if (this.View.RootDataPresenter != null)
                    {
                        Func<HierarchyPanel, bool> evaluator = <>c.<>9__30_0;
                        if (<>c.<>9__30_0 == null)
                        {
                            Func<HierarchyPanel, bool> local1 = <>c.<>9__30_0;
                            evaluator = <>c.<>9__30_0 = x => !x.IsMeasureValid;
                        }
                        if (!this.View.RootDataPresenter.Panel.Return<HierarchyPanel, bool>(evaluator, (<>c.<>9__30_1 ??= () => false)))
                        {
                            this.UpdateScrollIndexes();
                            this.View.EditFormManager.OnBeforeScroll(this.RowHandle);
                            if (this.ProhibitAnimation)
                            {
                                this.View.ScrollAnimationLocker.Lock();
                            }
                            try
                            {
                                this.ScrollIntoViewCore();
                            }
                            finally
                            {
                                if (this.ProhibitAnimation)
                                {
                                    this.View.ScrollAnimationLocker.Unlock();
                                }
                                this.View.EditFormManager.OnAfterScroll();
                                Action postAction = this.PostAction;
                                if (postAction == null)
                                {
                                    Action local3 = postAction;
                                }
                                else
                                {
                                    postAction();
                                }
                            }
                            if (!this.View.RootDataPresenter.CanScrollWithAnimation && (this.ShouldScrollBack() || this.ShouldScrollForward()))
                            {
                                this.EnqueueScrollIntoViewAgain();
                            }
                            return;
                        }
                    }
                    this.EnqueueScrollIntoViewAgain();
                }
            }
            finally
            {
                if (this.UseLock)
                {
                    this.View.EnqueueScrollIntoViewLocker.Unlock();
                }
            }
        }

        private int FindFirstInnerChildScrollIndex()
        {
            if (this.IsTopNewItemRow)
            {
                return this.View.DataControl.FindFirstInnerChildScrollIndex();
            }
            int rowVisibleIndexByHandleCore = this.View.DataControl.GetRowVisibleIndexByHandleCore(this.RowHandle);
            return this.View.DataControl.FindFirstInnerChildScrollIndex(rowVisibleIndexByHandleCore);
        }

        private int FindTopNewItemRowScrollIndex() => 
            (this.View.DataControl.VisibleRowCount <= 0) ? this.View.DataControl.FindFirstInnerChildScrollIndex() : (this.View.DataControl.FindFirstInnerChildScrollIndex(this.View.DataControl.VisibleRowCount - 1) + 1);

        private int GetBackScrollIndex() => 
            !this.IsTopNewItemRow ? (this.FirstInnerScrollIndex - this.View.ActualFixedTopRowsCount) : this.TopNewItemRowScrollIndex;

        private int GetForwardScrollIndex() => 
            this.FirstInnerScrollIndex;

        private FrameworkElement GetRowElement()
        {
            if (this.IsTopNewItemRow)
            {
                return this.View.GetRowElementByRowHandle(-2147483647);
            }
            GridRowsEnumerator enumerator = this.view.RootView.CreateVisibleRowsEnumerator();
            while (enumerator.MoveNext())
            {
                RowData currentRowData = enumerator.CurrentRowData as RowData;
                if ((currentRowData != null) && (ReferenceEquals(currentRowData.View, this.view) && (currentRowData.RowHandle.Value == this.rowHandle)))
                {
                    return enumerator.CurrentRow;
                }
            }
            return null;
        }

        private bool IsRowVisibleOnScreen(DataViewBase view)
        {
            int rowHandle = this.RowHandle;
            DataPresenterBase rootDataPresenter = this.View.RootDataPresenter;
            FrameworkElement rowElement = this.GetRowElement();
            return (((rowElement == null) || !rowElement.IsVisible) ? (rootDataPresenter.LastConstraint.Height == double.PositiveInfinity) : !rootDataPresenter.IsElementPartiallyVisible(rowElement));
        }

        public void Reassign(DataViewBase view, int rowHandle)
        {
            this.view = view;
            this.rowHandle = rowHandle;
        }

        private void ScrollBack()
        {
            this.View.RootDataPresenter.SetDefineScrollOffset((double) this.GetBackScrollIndex(), false);
        }

        private void ScrollForward()
        {
            double scrollOffset = 0.0;
            double actualViewPort = this.View.RootDataPresenter.ActualViewPort;
            if (!this.View.ShouldChangeForwardIndex(this.RowHandle))
            {
                double num4 = Math.Max(actualViewPort, 1.0);
                scrollOffset = (((this.GetForwardScrollIndex() - this.View.ActualFixedTopRowsCount) - num4) + this.View.DataControl.CalcTotalLevelByRowHandle(this.RowHandle)) + 1.0;
            }
            else
            {
                IScrollInfoOwner rootDataPresenter = this.View.RootDataPresenter;
                double num3 = 1.0;
                if (actualViewPort > 1.0)
                {
                    num3 = this.View.CalcOffsetForward(this.RowHandle, this.View.ViewBehavior.AllowPerPixelScrolling);
                    if (!ScrollActionsHelper.IsRowElementVisible(this.view, this.RowHandle))
                    {
                        num3 += ScrollActionsHelper.GetGroupSummaryRowCountBeforeRowByRowHandle(this.View, this.RowHandle, true);
                    }
                }
                scrollOffset = rootDataPresenter.Offset + num3;
            }
            scrollOffset += ScrollActionsHelper.GetGroupSummaryRowCountAfterRowByRowHandle(this.View, this.RowHandle, false, false);
            bool allowPerPixelScrolling = this.view.ViewBehavior.AllowPerPixelScrolling;
            allowPerPixelScrolling ??= this.view.IsEditFormVisible;
            this.View.RootDataPresenter.SetDefineScrollOffset(scrollOffset, allowPerPixelScrolling);
        }

        private void ScrollIntoViewCore()
        {
            if (this.ShouldScrollBack())
            {
                this.ScrollBack();
            }
            else if (this.ShouldScrollForward())
            {
                this.ScrollForward();
            }
        }

        private bool ShouldScrollBack()
        {
            double actualScrollOffset = this.View.RootDataPresenter.ActualScrollOffset;
            return ((this.GetBackScrollIndex() < actualScrollOffset) && (actualScrollOffset > 0.0));
        }

        private bool ShouldScrollForward()
        {
            IScrollInfoOwner rootDataPresenter = this.View.RootDataPresenter;
            return ((this.GetForwardScrollIndex() > rootDataPresenter.Offset) ? ((!this.View.IsPagingMode || this.View.IsScrollIndexInPageBounds(this.GetForwardScrollIndex())) ? (!this.IsRowVisibleOnScreen(this.View) ? ((this.View.DataControl.CalcTotalLevelByRowHandle(this.RowHandle) <= ScrollActionsHelper.CalcLastScrollRowTotalLevel(this.View)) ? (this.GetForwardScrollIndex() > this.View.CalcLastScrollRowScrollIndex()) : true) : false) : false) : false);
        }

        private void UpdateScrollIndexes()
        {
            this.FirstInnerScrollIndex = this.FindFirstInnerChildScrollIndex();
            if (this.IsTopNewItemRow)
            {
                this.TopNewItemRowScrollIndex = this.FindTopNewItemRowScrollIndex();
            }
        }

        protected DataViewBase View =>
            this.view;

        protected int RowHandle =>
            this.rowHandle;

        protected int FirstInnerScrollIndex { get; set; }

        protected int TopNewItemRowScrollIndex { get; set; }

        protected bool ProhibitAnimation =>
            this.prohibitAnimation;

        private bool IsTopNewItemRow =>
            this.View.IsNewItemRowVisible && (this.RowHandle == -2147483647);

        internal bool UseLock { get; set; }

        internal Action PostAction { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollRowIntoViewAction.<>c <>9 = new ScrollRowIntoViewAction.<>c();
            public static Func<HierarchyPanel, bool> <>9__30_0;
            public static Func<bool> <>9__30_1;

            internal bool <Execute>b__30_0(HierarchyPanel x) => 
                !x.IsMeasureValid;

            internal bool <Execute>b__30_1() => 
                false;
        }
    }
}

