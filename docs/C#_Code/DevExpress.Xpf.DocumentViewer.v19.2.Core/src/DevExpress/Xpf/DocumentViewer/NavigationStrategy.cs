namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class NavigationStrategy
    {
        private readonly Locker pageIndexSyncLocker = new Locker();
        private readonly Locker backPageIndexSyncLocker = new Locker();
        private readonly Locker scrollLocker = new Locker();
        private readonly Locker changedPositionLocker = new Locker();
        private readonly Locker defferedScrollLocker = new Locker();
        private readonly DocumentPresenterControl presenter;

        public NavigationStrategy(DocumentPresenterControl documentPresenter)
        {
            this.presenter = documentPresenter;
            this.PositionCalculator = this.CreatePositionCalculator();
        }

        protected double CalcFitRectangleZoomFactor(Size viewport, Rect rect)
        {
            double num = !this.ShouldUseHeight(rect) ? (viewport.Width / rect.Width) : (viewport.Height / rect.Height);
            return Math.Min(5.0, Math.Max((double) 0.1, (double) (this.BehaviorProvider.ZoomFactor * num)));
        }

        protected double CalcHorizontalAnchorPointForFitRectangle(Rect rect, double x)
        {
            double num = this.CalcMinHorizontalOffsetFromPanel();
            double num2 = num.GreaterThan(0.0) ? (x - num) : x;
            double horizontalOffset = this.ScrollViewer.HorizontalOffset;
            if (!this.ShouldUseHeight(rect))
            {
                return (num2 + horizontalOffset);
            }
            double num4 = this.BehaviorProvider.Viewport.Width / this.BehaviorProvider.Viewport.Height;
            return Math.Max((double) 0.0, (double) (((num2 + (rect.Width / 2.0)) + horizontalOffset) - ((rect.Height * num4) / 2.0)));
        }

        private double CalcMinHorizontalOffsetFromPanel()
        {
            double maxPageWidth = this.PositionCalculator.GetMaxPageWidth();
            return this.ItemsPanel.CalcPageHorizontalOffset(maxPageWidth);
        }

        private double CalcMouseWheelZoomFactor(bool isZoomIn) => 
            this.BehaviorProvider.GetNextZoomFactor(isZoomIn);

        protected double CalcVerticalAnchorPointForFitRectangle(Rect rect, double y)
        {
            double pageRelativeVerticalOffset = this.ItemsPanel.GetPageRelativeVerticalOffset(this.Presenter.ShowSingleItem);
            double num2 = this.Presenter.ShowSingleItem ? this.PositionCalculator.GetPageVerticalOffset(this.PageIndex, pageRelativeVerticalOffset) : this.ScrollViewer.VerticalOffset;
            double height = this.ItemsPanel.IndexCalculator.GetRealItemSize(this.PageIndex).Height;
            if (this.Presenter.ShowSingleItem && height.LessThanOrClose(this.ItemsPanel.ViewportHeight))
            {
                num2 -= (this.ItemsPanel.ViewportHeight - height) / 2.0;
            }
            if (this.ShouldUseHeight(rect))
            {
                return (y + num2);
            }
            double num4 = this.BehaviorProvider.Viewport.Width / this.BehaviorProvider.Viewport.Height;
            return Math.Max((double) 0.0, (double) (((y + (rect.Height / 2.0)) + num2) - ((rect.Width / num4) / 2.0)));
        }

        public void ChangePosition(NavigationState state)
        {
            this.ChangePositionInternal(state);
        }

        private void ChangePositionAndRegisterUndoAction(NavigationState oldState, NavigationState newState, UndoActionType state)
        {
            this.RegisterUndoAction(newState, state);
            this.defferedScrollLocker.Lock();
            this.ChangePosition(newState);
        }

        private void ChangePositionInternal(NavigationState state)
        {
            this.changedPositionLocker.DoLockedActionIfNotLocked(delegate {
                // Unresolved stack state at '00000185'
            });
        }

        private void ChangePositionWithDeferredScrollLocking(NavigationState state)
        {
            this.scrollLocker.LockOnce();
            this.ChangePosition(state);
            this.presenter.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.scrollLocker.Unlock()));
        }

        protected virtual DevExpress.Xpf.DocumentViewer.PositionCalculator CreatePositionCalculator() => 
            new DevExpress.Xpf.DocumentViewer.PositionCalculator(() => this.Pages);

        protected NavigationState GenerateCurrentState(double rotateAngle, double zoomFactor, ZoomMode zoomMode) => 
            this.GenerateCurrentState(rotateAngle, zoomFactor, zoomMode, delegate {
                Func<System.Windows.Controls.ScrollViewer, double> evaluator = <>c.<>9__44_1;
                if (<>c.<>9__44_1 == null)
                {
                    Func<System.Windows.Controls.ScrollViewer, double> local1 = <>c.<>9__44_1;
                    evaluator = <>c.<>9__44_1 = x => x.HorizontalOffset;
                }
                return this.ScrollViewer.Return<System.Windows.Controls.ScrollViewer, double>(evaluator, <>c.<>9__44_2 ??= () => 0.0);
            }, delegate {
                Func<System.Windows.Controls.ScrollViewer, double> evaluator = <>c.<>9__44_4;
                if (<>c.<>9__44_4 == null)
                {
                    Func<System.Windows.Controls.ScrollViewer, double> local1 = <>c.<>9__44_4;
                    evaluator = <>c.<>9__44_4 = x => x.VerticalOffset;
                }
                return this.ScrollViewer.Return<System.Windows.Controls.ScrollViewer, double>(evaluator, <>c.<>9__44_5 ??= () => 0.0);
            });

        private NavigationState GenerateCurrentState(double rotateAngle, double zoomFactor, ZoomMode zoomMode, Func<double> getHorizontalOffset, Func<double> getVerticalOffset)
        {
            int num = this.Presenter.ShowSingleItem ? this.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(this.ItemsPanel.GetVirtualVerticalOffset()) : this.PositionCalculator.GetPageIndex(getVerticalOffset());
            double relativeOffsetX = this.PositionCalculator.GetRelativeOffsetX(getHorizontalOffset(), this.ScrollViewer.ExtentWidth);
            double num3 = this.Presenter.ShowSingleItem ? this.ItemsPanel.GetPageRelativeVerticalOffset(true) : this.PositionCalculator.GetRelativeOffsetY(getVerticalOffset());
            NavigationState state1 = new NavigationState();
            state1.PageIndex = num;
            state1.Angle = rotateAngle;
            state1.ZoomFactor = zoomFactor;
            state1.OffsetX = relativeOffsetX;
            state1.OffsetY = num3;
            state1.ZoomMode = zoomMode;
            return state1;
        }

        public void GenerateStartUpState()
        {
            Action<UndoRedoManager> action = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<UndoRedoManager> local1 = <>c.<>9__36_0;
                action = <>c.<>9__36_0 = x => x.Flush();
            }
            this.presenter.UndoRedoManager.Do<UndoRedoManager>(action);
        }

        public bool IsRectangleInView(int pageWrapperIndex, Rect rect)
        {
            Func<bool> fallback = <>c.<>9__35_1;
            if (<>c.<>9__35_1 == null)
            {
                Func<bool> local1 = <>c.<>9__35_1;
                fallback = <>c.<>9__35_1 = () => false;
            }
            return this.ItemsPanel.Return<DocumentViewerPanel, bool>(x => x.IsRectangleVisible(pageWrapperIndex, rect), fallback);
        }

        public virtual void ProcessMarqueeZoom(Rect rect, double x, double y)
        {
            double zoomFactor = this.CalcFitRectangleZoomFactor(this.BehaviorProvider.Viewport, rect);
            this.ScrollToAnchorPoint(zoomFactor, this.CalcHorizontalAnchorPointForFitRectangle(rect, x), this.CalcVerticalAnchorPointForFitRectangle(rect, y), UndoActionType.Scroll);
        }

        public virtual void ProcessPageIndexChanged(int pageIndex)
        {
            this.pageIndexSyncLocker.DoIfNotLocked(delegate {
                this.backPageIndexSyncLocker.LockOnce();
                int pageWrapperIndex = this.PositionCalculator.GetPageWrapperIndex(pageIndex);
                this.ScrollIntoView(pageWrapperIndex, Rect.Empty, ScrollIntoViewMode.TopLeft);
            });
        }

        public virtual void ProcessRotateAngleChanged(RotateAngleChangedEventArgs e)
        {
            if (this.changedPositionLocker.IsLocked || (this.ScrollViewer == null))
            {
                this.UpdatePagesRotateAngle(e.NewValue);
            }
            else
            {
                NavigationState oldState = this.GenerateCurrentState(e.OldValue, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.ZoomMode);
                NavigationState newState = oldState.Clone();
                newState.Angle = e.NewValue;
                this.UpdatePagesRotateAngle(e.NewValue);
                this.ChangePositionAndRegisterUndoAction(oldState, newState, UndoActionType.Rotate);
            }
        }

        public virtual void ProcessScroll(ScrollCommand command)
        {
            switch (command)
            {
                case ScrollCommand.PageDown:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_5;
                    if (<>c.<>9__25_5 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local6 = <>c.<>9__25_5;
                        action = <>c.<>9__25_5 = x => x.PageDown();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.PageUp:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_4;
                    if (<>c.<>9__25_4 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local5 = <>c.<>9__25_4;
                        action = <>c.<>9__25_4 = x => x.PageUp();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.LineDown:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_1;
                    if (<>c.<>9__25_1 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local2 = <>c.<>9__25_1;
                        action = <>c.<>9__25_1 = x => x.LineDown();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.LineUp:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_0;
                    if (<>c.<>9__25_0 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local1 = <>c.<>9__25_0;
                        action = <>c.<>9__25_0 = x => x.LineUp();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.LineLeft:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_2;
                    if (<>c.<>9__25_2 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local3 = <>c.<>9__25_2;
                        action = <>c.<>9__25_2 = x => x.LineLeft();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.LineRight:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_3;
                    if (<>c.<>9__25_3 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local4 = <>c.<>9__25_3;
                        action = <>c.<>9__25_3 = x => x.LineRight();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.Home:
                {
                    Action<System.Windows.Controls.ScrollViewer> action = <>c.<>9__25_6;
                    if (<>c.<>9__25_6 == null)
                    {
                        Action<System.Windows.Controls.ScrollViewer> local7 = <>c.<>9__25_6;
                        action = <>c.<>9__25_6 = x => x.ScrollToHome();
                    }
                    this.ScrollViewer.Do<System.Windows.Controls.ScrollViewer>(action);
                    return;
                }
                case ScrollCommand.End:
                    if (this.PageIndex == (this.PageCount - 1))
                    {
                        this.ScrollViewer.ScrollToEnd();
                        return;
                    }
                    this.ScrollIntoView(this.PageCount - 1, Rect.Empty, ScrollIntoViewMode.TopLeft);
                    return;
            }
        }

        public virtual void ProcessScrollTo(double offset, bool isVertical)
        {
            if (isVertical)
            {
                this.ScrollViewer.ScrollToVerticalOffset(offset);
            }
            else
            {
                this.ScrollViewer.ScrollToHorizontalOffset(offset);
            }
        }

        public virtual void ProcessScrollViewerScrollChanged(ScrollChangedEventArgs e)
        {
            this.UpdatePageIndex();
            if (!this.scrollLocker.IsLocked)
            {
                this.RegisterUndoAction(this.GenerateCurrentState(this.BehaviorProvider.RotateAngle, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.ZoomMode), UndoActionType.DeferredScroll);
            }
        }

        public virtual void ProcessZoomChanged(ZoomChangedEventArgs e)
        {
            if (this.changedPositionLocker.IsLocked || (this.ScrollViewer == null))
            {
                this.UpdatePagesZoomFactor(e.ZoomFactor);
            }
            else
            {
                NavigationState oldState = this.GenerateCurrentState(this.BehaviorProvider.RotateAngle, e.OldZoomFactor, e.OldZoomMode);
                NavigationState newState = oldState.Clone();
                newState.ZoomFactor = e.ZoomFactor;
                newState.ZoomMode = e.ZoomMode;
                this.UpdatePagesZoomFactor(e.ZoomFactor);
                this.ChangePositionAndRegisterUndoAction(oldState, newState, UndoActionType.Zoom);
            }
        }

        private void RegisterUndoAction(NavigationState newState, UndoActionType state)
        {
            if (this.presenter.UndoRedoManager != null)
            {
                if (this.defferedScrollLocker.IsLocked && (state == UndoActionType.DeferredScroll))
                {
                    this.defferedScrollLocker.Unlock();
                }
                else
                {
                    this.scrollLocker.LockOnce();
                    UndoState action = new UndoState();
                    action.State = newState;
                    action.Perform = new Action<NavigationState>(this.ChangePositionWithDeferredScrollLocking);
                    action.ActionType = state;
                    this.presenter.UndoRedoManager.RegisterAction(action);
                    this.presenter.ImmediateActionsManager.EnqueueAction(new DelegateAction(() => this.scrollLocker.Unlock()));
                }
            }
        }

        public void ScrollIntoView(int pageWrapperIndex, Rect rect, ScrollIntoViewMode mode)
        {
            this.ItemsPanel.Do<DocumentViewerPanel>(x => x.ScrollIntoView(pageWrapperIndex, rect, mode));
        }

        public void ScrollToAnchorPoint(double zoomFactor, double horizontalAnchor, double verticalAnchor, UndoActionType actionType)
        {
            NavigationState oldState = this.GenerateCurrentState(this.BehaviorProvider.RotateAngle, this.BehaviorProvider.ZoomFactor, this.BehaviorProvider.ZoomMode);
            NavigationState newState = oldState.Clone();
            newState.ZoomFactor = zoomFactor;
            newState.ZoomMode = ZoomMode.Custom;
            double maxPageWidth = this.PositionCalculator.GetMaxPageWidth();
            newState.OffsetX = this.PositionCalculator.GetRelativeOffsetX(horizontalAnchor, maxPageWidth);
            newState.OffsetY = this.PositionCalculator.GetRelativeOffsetY(verticalAnchor);
            newState.PageIndex = this.PositionCalculator.GetPageIndex(verticalAnchor);
            this.ChangePositionAndRegisterUndoAction(oldState, newState, actionType);
        }

        public void ScrollToStartUp()
        {
            // Unresolved stack state at '000000E8'
        }

        private bool ShouldUseHeight(Rect rect)
        {
            double num = this.BehaviorProvider.Viewport.Width / this.BehaviorProvider.Viewport.Height;
            return rect.Width.LessThan((rect.Height * num));
        }

        private void UpdatePageIndex()
        {
            if (this.backPageIndexSyncLocker.IsLocked)
            {
                this.backPageIndexSyncLocker.Unlock();
            }
            else
            {
                int pageWrapperIndex = this.ItemsPanel.IndexCalculator.VerticalOffsetToIndex(this.ItemsPanel.GetVirtualVerticalOffset());
                this.pageIndexSyncLocker.DoLockedAction<int>(delegate {
                    int num;
                    this.BehaviorProvider.PageIndex = num = this.PositionCalculator.GetPageIndexFromWrapper(pageWrapperIndex);
                    return num;
                });
            }
        }

        private void UpdatePagesRotateAngle(double rotateAngle)
        {
            this.UpdatePagesRotateAngleInternal(rotateAngle);
            if (this.Presenter.HasPages)
            {
                foreach (PageWrapper wrapper in this.Pages)
                {
                    wrapper.RotateAngle = rotateAngle;
                }
                Action<DocumentViewerPanel> action = <>c.<>9__53_0;
                if (<>c.<>9__53_0 == null)
                {
                    Action<DocumentViewerPanel> local1 = <>c.<>9__53_0;
                    action = <>c.<>9__53_0 = x => x.InvalidatePanel();
                }
                this.ItemsPanel.Do<DocumentViewerPanel>(action);
            }
        }

        protected virtual void UpdatePagesRotateAngleInternal(double rotateAngle)
        {
        }

        private void UpdatePagesZoomFactor(double zoomFactor)
        {
            if (this.Presenter.HasPages)
            {
                foreach (PageWrapper wrapper in this.Pages)
                {
                    wrapper.ZoomFactor = zoomFactor;
                }
                Action<DocumentViewerPanel> action = <>c.<>9__52_0;
                if (<>c.<>9__52_0 == null)
                {
                    Action<DocumentViewerPanel> local1 = <>c.<>9__52_0;
                    action = <>c.<>9__52_0 = x => x.InvalidatePanel();
                }
                this.ItemsPanel.Do<DocumentViewerPanel>(action);
            }
        }

        public virtual void ZoomToAnchorPoint(bool isZoomIn, Point visibleAnchorPoint)
        {
            double zoomFactor = this.CalcMouseWheelZoomFactor(isZoomIn);
            double num2 = this.BehaviorProvider.ZoomFactor / zoomFactor;
            double horizontalAnchor = Math.Max((double) ((visibleAnchorPoint.X + this.ScrollViewer.HorizontalOffset) - (num2 * visibleAnchorPoint.X)), (double) 0.0);
            double num5 = 0.0;
            if (!this.Presenter.ShowSingleItem)
            {
                num5 = visibleAnchorPoint.Y + this.ScrollViewer.VerticalOffset;
            }
            else
            {
                double pageRelativeVerticalOffset = this.ItemsPanel.GetPageRelativeVerticalOffset(true);
                num5 = visibleAnchorPoint.Y + this.PositionCalculator.GetPageVerticalOffset(this.PageIndex, pageRelativeVerticalOffset);
                double height = this.ItemsPanel.IndexCalculator.GetRealItemSize(this.PageIndex).Height;
                if (height.LessThanOrClose(this.ItemsPanel.ViewportHeight))
                {
                    num5 -= (this.ItemsPanel.ViewportHeight - height) / 2.0;
                }
            }
            this.ScrollToAnchorPoint(zoomFactor, horizontalAnchor, Math.Max((double) (num5 - (num2 * visibleAnchorPoint.Y)), (double) 0.0), UndoActionType.Zoom);
        }

        protected DocumentViewerPanel ItemsPanel =>
            this.presenter.ItemsPanel;

        protected System.Windows.Controls.ScrollViewer ScrollViewer =>
            this.presenter.ScrollViewer;

        protected DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider =>
            this.presenter.BehaviorProvider;

        protected DocumentPresenterControl Presenter =>
            this.presenter;

        protected internal DevExpress.Xpf.DocumentViewer.PositionCalculator PositionCalculator { get; private set; }

        private int PageCount
        {
            get
            {
                Func<ObservableCollection<PageWrapper>, int> evaluator = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<ObservableCollection<PageWrapper>, int> local1 = <>c.<>9__19_0;
                    evaluator = <>c.<>9__19_0 = x => x.Count<PageWrapper>();
                }
                return this.presenter.Pages.Return<ObservableCollection<PageWrapper>, int>(evaluator, (<>c.<>9__19_1 ??= () => 0));
            }
        }

        private int PageIndex =>
            this.BehaviorProvider.PageIndex;

        private IEnumerable<PageWrapper> Pages =>
            this.presenter.Pages;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationStrategy.<>c <>9 = new NavigationStrategy.<>c();
            public static Func<ObservableCollection<PageWrapper>, int> <>9__19_0;
            public static Func<int> <>9__19_1;
            public static Action<ScrollViewer> <>9__25_0;
            public static Action<ScrollViewer> <>9__25_1;
            public static Action<ScrollViewer> <>9__25_2;
            public static Action<ScrollViewer> <>9__25_3;
            public static Action<ScrollViewer> <>9__25_4;
            public static Action<ScrollViewer> <>9__25_5;
            public static Action<ScrollViewer> <>9__25_6;
            public static Func<bool> <>9__35_1;
            public static Action<UndoRedoManager> <>9__36_0;
            public static Func<BehaviorProvider, double> <>9__37_0;
            public static Func<double> <>9__37_1;
            public static Func<BehaviorProvider, int> <>9__37_2;
            public static Func<int> <>9__37_3;
            public static Func<BehaviorProvider, bool> <>9__37_4;
            public static Func<bool> <>9__37_5;
            public static Func<BehaviorProvider, ZoomMode> <>9__37_6;
            public static Func<ZoomMode> <>9__37_7;
            public static Func<double> <>9__42_5;
            public static Func<double> <>9__42_7;
            public static Func<DocumentPresenterControl, ImmediateActionsManager> <>9__42_8;
            public static Func<ScrollViewer, double> <>9__44_1;
            public static Func<double> <>9__44_2;
            public static Func<ScrollViewer, double> <>9__44_4;
            public static Func<double> <>9__44_5;
            public static Action<DocumentViewerPanel> <>9__52_0;
            public static Action<DocumentViewerPanel> <>9__53_0;

            internal double <ChangePositionInternal>b__42_5() => 
                0.0;

            internal double <ChangePositionInternal>b__42_7() => 
                0.0;

            internal ImmediateActionsManager <ChangePositionInternal>b__42_8(DocumentPresenterControl x) => 
                x.ImmediateActionsManager;

            internal double <GenerateCurrentState>b__44_1(ScrollViewer x) => 
                x.HorizontalOffset;

            internal double <GenerateCurrentState>b__44_2() => 
                0.0;

            internal double <GenerateCurrentState>b__44_4(ScrollViewer x) => 
                x.VerticalOffset;

            internal double <GenerateCurrentState>b__44_5() => 
                0.0;

            internal void <GenerateStartUpState>b__36_0(UndoRedoManager x)
            {
                x.Flush();
            }

            internal int <get_PageCount>b__19_0(ObservableCollection<PageWrapper> x) => 
                x.Count<PageWrapper>();

            internal int <get_PageCount>b__19_1() => 
                0;

            internal bool <IsRectangleInView>b__35_1() => 
                false;

            internal void <ProcessScroll>b__25_0(ScrollViewer x)
            {
                x.LineUp();
            }

            internal void <ProcessScroll>b__25_1(ScrollViewer x)
            {
                x.LineDown();
            }

            internal void <ProcessScroll>b__25_2(ScrollViewer x)
            {
                x.LineLeft();
            }

            internal void <ProcessScroll>b__25_3(ScrollViewer x)
            {
                x.LineRight();
            }

            internal void <ProcessScroll>b__25_4(ScrollViewer x)
            {
                x.PageUp();
            }

            internal void <ProcessScroll>b__25_5(ScrollViewer x)
            {
                x.PageDown();
            }

            internal void <ProcessScroll>b__25_6(ScrollViewer x)
            {
                x.ScrollToHome();
            }

            internal double <ScrollToStartUp>b__37_0(BehaviorProvider x) => 
                x.RotateAngle;

            internal double <ScrollToStartUp>b__37_1() => 
                0.0;

            internal int <ScrollToStartUp>b__37_2(BehaviorProvider x) => 
                x.PageIndex;

            internal int <ScrollToStartUp>b__37_3() => 
                1;

            internal bool <ScrollToStartUp>b__37_4(BehaviorProvider x) => 
                x.ZoomFactor.IsNumber();

            internal bool <ScrollToStartUp>b__37_5() => 
                false;

            internal ZoomMode <ScrollToStartUp>b__37_6(BehaviorProvider x) => 
                x.ZoomMode;

            internal ZoomMode <ScrollToStartUp>b__37_7() => 
                ZoomMode.ActualSize;

            internal void <UpdatePagesRotateAngle>b__53_0(DocumentViewerPanel x)
            {
                x.InvalidatePanel();
            }

            internal void <UpdatePagesZoomFactor>b__52_0(DocumentViewerPanel x)
            {
                x.InvalidatePanel();
            }
        }
    }
}

