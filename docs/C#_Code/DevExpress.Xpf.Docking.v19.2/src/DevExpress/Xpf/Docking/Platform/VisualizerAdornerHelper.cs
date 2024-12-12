namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Threading;

    public class VisualizerAdornerHelper : IDisposable
    {
        private bool isDisposing;
        private bool fUseAdornerWindow;
        private AdornerWindowHelper helper;
        private DevExpress.Xpf.Docking.Platform.SelectionAdorner selectionAdornerCore;
        private DockingHintAdorner dockingHintAdornerCore;
        private DevExpress.Xpf.Docking.Platform.TabHeadersAdorner tabHeadersAdornerCore;
        private ShadowResizeAdorner shadowResizeAdornerCore;
        private DispatcherTimer HideTimer;
        private bool fCancelHiding;
        private List<Action> delayedActions;
        private int lockHidingCounter;

        public VisualizerAdornerHelper(LayoutView view)
        {
            this.View = view;
            this.Container = view.Container;
            this.fUseAdornerWindow = !WindowHelper.IsXBAP;
            this.TryCreateAdornerWindowHelper(view);
            this.InitTimer();
        }

        public void BeginHideAdornerWindow()
        {
            this.fCancelHiding = false;
            this.HideTimer.Start();
        }

        public void BeginHideAdornerWindowAndResetDockingHints()
        {
            this.fCancelHiding = false;
            this.delayedActions.Add(new Action(this.ResetDockingHints));
            if (!this.Container.IsTransparencyDisabled || !(this.View is FloatingView))
            {
                this.HideTimer.Start();
            }
            else
            {
                this.DoHidingActions();
                if (this.fUseAdornerWindow)
                {
                    this.Container.Win32AdornerWindowProvider.EnqueueHideAdornerWindow();
                }
            }
        }

        public void BeginHideAdornerWindowAndResetTabHeadersHints()
        {
            this.fCancelHiding = false;
            this.delayedActions.Add(new Action(this.ResetTabHeadersHints));
            this.HideTimer.Start();
        }

        private bool canDock(DockLayoutElementDragInfo dragInfo, DockHintHitInfo adornerHitInfo) => 
            dragInfo.AcceptDocking(adornerHitInfo) && !this.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemDockingEvent, dragInfo.Item, dragInfo.Point, (adornerHitInfo.IsCenter ? dragInfo.Target : dragInfo.Target.GetRoot()), adornerHitInfo.DockType, adornerHitInfo.IsHideButton);

        private bool CanShowTabHeaderHint(DockLayoutElementDragInfo dragInfo, int insertIndex, Rect tab, Rect header)
        {
            IDockLayoutContainer dropTarget = dragInfo.DropTarget as IDockLayoutContainer;
            return ((dropTarget != null) && this.Container.RaiseShowingTabHintsEvent(dragInfo.Item, dropTarget.Item, insertIndex, tab, header));
        }

        private AdornerWindowHelper CreateAdornerWindowHelper(LayoutView view) => 
            new AdornerWindowHelper(view, this.Container);

        private void DestroyTimer()
        {
            this.HideTimer.Tick -= new EventHandler(this.HideTimer_Tick);
            this.delayedActions.Clear();
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.DestroyTimer();
                Ref.Dispose<ShadowResizeAdorner>(ref this.shadowResizeAdornerCore);
                Ref.Dispose<DevExpress.Xpf.Docking.Platform.TabHeadersAdorner>(ref this.tabHeadersAdornerCore);
                Ref.Dispose<DockingHintAdorner>(ref this.dockingHintAdornerCore);
                Ref.Dispose<DevExpress.Xpf.Docking.Platform.SelectionAdorner>(ref this.selectionAdornerCore);
                Ref.Dispose<AdornerWindowHelper>(ref this.helper);
                this.Container = null;
                this.View = null;
            }
            GC.SuppressFinalize(this);
        }

        private void DoHidingActions()
        {
            if (this.lockHidingCounter <= 0)
            {
                this.lockHidingCounter++;
                Action[] actionArray = this.delayedActions.ToArray();
                this.delayedActions.Clear();
                foreach (Action action in actionArray)
                {
                    if (action != null)
                    {
                        action();
                    }
                }
                this.lockHidingCounter--;
            }
        }

        public static void EnsureAdornerActivated(BaseSurfacedAdorner adorner)
        {
            if (adorner != null)
            {
                DockLayoutManager visual = LayoutHelper.FindParentObject<DockLayoutManager>(adorner.AdornedElement);
                if (((visual == null) || (!visual.IsInDesignTime || (AdornerLayer.GetAdornerLayer(visual) != null))) && !adorner.IsActivated)
                {
                    adorner.Activate();
                }
            }
        }

        public static T FindAdorner<T>(UIElement container) where T: BaseSurfacedAdorner
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(container);
            if (layer == null)
            {
                return default(T);
            }
            Adorner[] adorners = layer.GetAdorners(container);
            if (adorners == null)
            {
                return default(T);
            }
            Predicate<Adorner> match = <>c__57<T>.<>9__57_0;
            if (<>c__57<T>.<>9__57_0 == null)
            {
                Predicate<Adorner> local1 = <>c__57<T>.<>9__57_0;
                match = <>c__57<T>.<>9__57_0 = a => a is T;
            }
            return (Array.Find<Adorner>(adorners, match) as T);
        }

        protected UIElement GetAdornedElement()
        {
            UIElement element = null;
            if (this.fUseAdornerWindow)
            {
                element = this.TryGetAdornerWindowRoot();
            }
            return (element ?? this.View.RootUIElement);
        }

        public static double GetAdornerWindowIndent(UIElement element)
        {
            AdornerWindow window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(element) as AdornerWindow;
            return ((window == null) ? 0.0 : window.GetAdornerIndentWithoutTransform());
        }

        public DockingHintAdorner GetDockingHintAdorner()
        {
            if ((this.dockingHintAdornerCore == null) || this.dockingHintAdornerCore.IsDisposing)
            {
                UIElement adornedElement = this.GetAdornedElement();
                this.dockingHintAdornerCore = FindAdorner<DockingHintAdorner>(adornedElement);
                if (this.dockingHintAdornerCore == null)
                {
                    this.dockingHintAdornerCore = this.View.CreateDockingHintAdorner(adornedElement);
                    EnsureAdornerActivated(this.Container.DragAdorner);
                    EnsureAdornerActivated(this.dockingHintAdornerCore);
                }
            }
            return this.dockingHintAdornerCore;
        }

        public DockHintHitInfo GetHitInfo(Point point)
        {
            if (this.dockingHintAdornerCore == null)
            {
                return null;
            }
            return this.GetDockingHintAdorner()?.HitTest(point);
        }

        public RenameController GetRenameController() => 
            this.selectionAdornerCore?.RenameController;

        public DevExpress.Xpf.Docking.Platform.SelectionAdorner GetSelectionAdorner()
        {
            if ((this.selectionAdornerCore == null) || this.selectionAdornerCore.IsDisposing)
            {
                UIElement rootUIElement = this.View.RootUIElement;
                this.selectionAdornerCore = FindAdorner<DevExpress.Xpf.Docking.Platform.SelectionAdorner>(rootUIElement);
                if (this.selectionAdornerCore == null)
                {
                    this.selectionAdornerCore = this.View.CreateSelectionAdorner(rootUIElement);
                    EnsureAdornerActivated(this.selectionAdornerCore);
                }
            }
            return this.selectionAdornerCore;
        }

        public ShadowResizeAdorner GetShadowResizeAdorner()
        {
            if (this.shadowResizeAdornerCore == null)
            {
                UIElement adornedElement = this.GetAdornedElement();
                this.shadowResizeAdornerCore = FindAdorner<ShadowResizeAdorner>(adornedElement);
                if (this.shadowResizeAdornerCore == null)
                {
                    this.shadowResizeAdornerCore = this.View.CreateShadowResizeAdorner(adornedElement);
                    EnsureAdornerActivated(this.Container.DragAdorner);
                    EnsureAdornerActivated(this.shadowResizeAdornerCore);
                }
            }
            return this.shadowResizeAdornerCore;
        }

        public DevExpress.Xpf.Docking.Platform.TabHeadersAdorner GetTabHeadersAdorner()
        {
            if (this.tabHeadersAdornerCore == null)
            {
                UIElement adornedElement = this.GetAdornedElement();
                this.tabHeadersAdornerCore = FindAdorner<DevExpress.Xpf.Docking.Platform.TabHeadersAdorner>(adornedElement);
                if (this.tabHeadersAdornerCore == null)
                {
                    this.tabHeadersAdornerCore = this.View.CreateTabHeadersAdorner(adornedElement);
                    EnsureAdornerActivated(this.Container.DragAdorner);
                    EnsureAdornerActivated(this.tabHeadersAdornerCore);
                }
            }
            return this.tabHeadersAdornerCore;
        }

        public void HideSelection()
        {
            if (this.dockingHintAdornerCore != null)
            {
                DockingHintAdornerBase selectionAdorner = this.GetSelectionAdorner();
                if (selectionAdorner != null)
                {
                    selectionAdorner.ShowSelectionHints = false;
                    selectionAdorner.UpdateSelection();
                    selectionAdorner.Update(selectionAdorner.Visible);
                }
            }
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            this.HideTimer.Stop();
            this.DoHidingActions();
            if (this.fUseAdornerWindow && !this.fCancelHiding)
            {
                this.helper.HideAdornerWindow();
            }
        }

        private void InitTimer()
        {
            this.delayedActions = new List<Action>();
            this.HideTimer = InvokeHelper.GetBackgroundTimer(180.0);
            this.HideTimer.Tick += new EventHandler(this.HideTimer_Tick);
        }

        internal void InvalidateSelectionAdorner()
        {
            if (this.selectionAdornerCore != null)
            {
                DockingHintAdornerBase selectionAdornerCore = this.selectionAdornerCore;
                InvokeHelper.BeginInvoke(selectionAdornerCore, new Action(selectionAdornerCore.Update), DispatcherPriority.Render, new object[0]);
            }
        }

        public void Reset()
        {
            this.ResetDockingHints();
            this.ResetTabHeadersHints();
            Ref.Dispose<DockingHintAdorner>(ref this.dockingHintAdornerCore);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.TabHeadersAdorner>(ref this.tabHeadersAdornerCore);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.SelectionAdorner>(ref this.selectionAdornerCore);
            if (this.fUseAdornerWindow)
            {
                this.helper.Reset();
            }
        }

        public void ResetDockingHints()
        {
            if (this.dockingHintAdornerCore != null)
            {
                DockingHintAdornerBase dockingHintAdorner = this.GetDockingHintAdorner();
                if (dockingHintAdorner != null)
                {
                    dockingHintAdorner.ResetDocking();
                    dockingHintAdorner.Update(dockingHintAdorner.Visible);
                }
            }
        }

        public void ResetDragVisualization()
        {
            this.Container.CustomizationController.HideDragCursor();
            this.Container.CustomizationController.UpdateDragInfo(null);
        }

        public void ResetTabHeadersHints()
        {
            if (this.tabHeadersAdornerCore != null)
            {
                DevExpress.Xpf.Docking.Platform.TabHeadersAdorner tabHeadersAdorner = this.GetTabHeadersAdorner();
                if (tabHeadersAdorner != null)
                {
                    tabHeadersAdorner.ResetElements();
                    tabHeadersAdorner.Update(false);
                }
            }
        }

        public void ShowSelection()
        {
            if (this.Container.IsCustomization && !this.Container.IsInDesignTime)
            {
                DockingHintAdornerBase selectionAdorner = this.GetSelectionAdorner();
                if (selectionAdorner != null)
                {
                    selectionAdorner.ShowSelectionHints = true;
                    selectionAdorner.UpdateSelection();
                    selectionAdorner.Update(true);
                }
            }
        }

        protected void TryCreateAdornerWindowHelper(LayoutView view)
        {
            if (this.fUseAdornerWindow)
            {
                this.helper = this.CreateAdornerWindowHelper(view);
            }
        }

        private UIElement TryGetAdornerWindowRoot() => 
            this.helper.AdornerWindow?.RootElement;

        public void TryHideAdornerWindow()
        {
            this.HideTimer.Stop();
            this.DoHidingActions();
            if (this.fUseAdornerWindow)
            {
                this.helper.HideAdornerWindow();
            }
        }

        public void TryShowAdornerWindow(bool forceUpdateAdornerBounds = false)
        {
            this.fCancelHiding = true;
            if (this.fUseAdornerWindow)
            {
                if (this.Container.IsTransparencyDisabled && (this.View is FloatingView))
                {
                    this.Container.Win32AdornerWindowProvider.CancelHideAdornerWindow();
                }
                this.helper.ShowAdornerWindow(forceUpdateAdornerBounds);
            }
        }

        public void UpdateDockingHints(DockLayoutElementDragInfo dragInfo, bool forceUpdateAdornerBounds = false)
        {
            this.delayedActions.Remove(new Action(this.ResetDockingHints));
            this.TryShowAdornerWindow(forceUpdateAdornerBounds);
            DockingHintAdornerBase dockingHintAdorner = this.GetDockingHintAdorner();
            if ((dockingHintAdorner != null) && (dragInfo.Item != null))
            {
                DockHintHitInfo hitInfo = dockingHintAdorner.HitTest(dragInfo.Point);
                dockingHintAdorner.TargetRect = dragInfo.TargetRect;
                dockingHintAdorner.SetDockHintsConfiguration(dragInfo);
                if (dragInfo.Target != null)
                {
                    this.Container.RaiseShowingDockHintsEvent(dragInfo.Item, dragInfo.Target, dockingHintAdorner.DockHintsConfiguration);
                }
                dockingHintAdorner.UpdateHotTrack(hitInfo);
                dockingHintAdorner.UpdateState();
                if (this.canDock(dragInfo, hitInfo))
                {
                    dockingHintAdorner.HintRect = HintRectCalculator.Calc(dragInfo, hitInfo);
                }
                else
                {
                    dockingHintAdorner.HintRect = Rect.Empty;
                    dockingHintAdorner.ClearHotTrack();
                }
                dockingHintAdorner.Update(true);
            }
        }

        public void UpdateTabHeadersHint(DockLayoutElementDragInfo dragInfo, bool forceUpdateAdornerBounds = false)
        {
            this.delayedActions.Remove(new Action(this.ResetTabHeadersHints));
            this.TryShowAdornerWindow(forceUpdateAdornerBounds);
            DevExpress.Xpf.Docking.Platform.TabHeadersAdorner tabHeadersAdorner = this.GetTabHeadersAdorner();
            if ((tabHeadersAdorner != null) && (dragInfo.Item != null))
            {
                IDockLayoutContainer dropTarget = dragInfo.DropTarget as IDockLayoutContainer;
                if ((dropTarget != null) && dropTarget.IsTabContainer)
                {
                    TabHeaderInsertHelper helper = new TabHeaderInsertHelper(dropTarget, dragInfo.Point, !ReferenceEquals(dropTarget.Item, dragInfo.Item.Parent));
                    if (this.CanShowTabHeaderHint(dragInfo, helper.TabIndex, helper.Tab, helper.Header))
                    {
                        tabHeadersAdorner.ShowElements(dropTarget.TabHeaderLocation, helper.Tab, helper.Header);
                    }
                    else
                    {
                        tabHeadersAdorner.ResetElements();
                    }
                    tabHeadersAdorner.Update(true);
                }
            }
        }

        public LayoutView View { get; private set; }

        public DockLayoutManager Container { get; private set; }

        public DevExpress.Xpf.Docking.Platform.SelectionAdorner SelectionAdorner =>
            this.selectionAdornerCore;

        public DevExpress.Xpf.Docking.Platform.TabHeadersAdorner TabHeadersAdorner =>
            this.tabHeadersAdornerCore;

        internal bool IsTabHeadersHintEnabled
        {
            get
            {
                DevExpress.Xpf.Docking.Platform.TabHeadersAdorner tabHeadersAdorner = this.TabHeadersAdorner;
                if (tabHeadersAdorner != null)
                {
                    return tabHeadersAdorner.IsTabHeaderHintEnabled;
                }
                DevExpress.Xpf.Docking.Platform.TabHeadersAdorner local1 = tabHeadersAdorner;
                return false;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__57<T> where T: BaseSurfacedAdorner
        {
            public static readonly VisualizerAdornerHelper.<>c__57<T> <>9;
            public static Predicate<Adorner> <>9__57_0;

            static <>c__57()
            {
                VisualizerAdornerHelper.<>c__57<T>.<>9 = new VisualizerAdornerHelper.<>c__57<T>();
            }

            internal bool <FindAdorner>b__57_0(Adorner a) => 
                a is T;
        }
    }
}

