namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class DockHintsConfiguration
    {
        private Dictionary<object, DockHintState> Hints = new Dictionary<object, DockHintState>();

        public void Disable(object hint)
        {
            this.GetDockHintState(hint).IsEnabled = false;
        }

        internal bool GetAutoHideIsEnabled(DockVisualizerElement element) => 
            this.GetIsEnabled(element.ToAutoHideDockHint());

        internal bool GetAutoHideIsVisible(DockVisualizerElement element) => 
            this.GetIsVisible(element.ToAutoHideDockHint());

        private DockHintState GetDockHintState(object hint)
        {
            DockHintState state;
            if (!this.Hints.TryGetValue(hint, out state))
            {
                state = new DockHintState();
                this.Hints[hint] = state;
            }
            return state;
        }

        internal bool GetGuideIsVisible(DockVisualizerElement element) => 
            this.GetIsVisible(element.ToDockGuide());

        public bool GetIsEnabled(object hint) => 
            this.GetDockHintState(hint).IsEnabled && !this.DisableAll;

        public bool GetIsVisible(object hint) => 
            this.GetDockHintState(hint).IsVisible && !this.HideAll;

        internal bool GetSideIsEnabled(DockVisualizerElement element) => 
            this.GetIsEnabled(element.ToSideDockHint());

        internal bool GetSideIsVisible(DockVisualizerElement element) => 
            this.GetIsVisible(element.ToSideDockHint());

        public void Hide(object hint)
        {
            this.GetDockHintState(hint).IsVisible = false;
        }

        public void Invalidate()
        {
            this.Hints.Clear();
            this.DisableAll = false;
            this.HideAll = false;
            this.ShowSideDockHints = false;
            this.ShowSelfDockHint = false;
        }

        internal void SetCanDockCenter(bool canDock)
        {
            bool flag2;
            bool flag3;
            this.GetDockHintState(DockHint.CenterBottom).IsEnabled = flag3 = canDock;
            this.GetDockHintState(DockHint.CenterTop).IsEnabled = flag2 = flag3;
            this.GetDockHintState(DockHint.CenterLeft).IsEnabled = this.GetDockHintState(DockHint.CenterRight).IsEnabled = flag2;
        }

        internal void SetCanDockToSide(bool canDock)
        {
            bool flag2;
            bool flag3;
            this.GetDockHintState(DockHint.SideBottom).IsEnabled = flag3 = canDock;
            this.GetDockHintState(DockHint.SideTop).IsEnabled = flag2 = flag3;
            this.GetDockHintState(DockHint.SideLeft).IsEnabled = this.GetDockHintState(DockHint.SideRight).IsEnabled = flag2;
        }

        internal void SetCanDockToTab(bool canDock)
        {
            bool flag2;
            bool flag3;
            this.GetDockHintState(DockHint.TabTop).IsEnabled = flag3 = canDock;
            this.GetDockHintState(DockHint.TabRight).IsEnabled = flag2 = flag3;
            this.GetDockHintState(DockHint.TabBottom).IsEnabled = this.GetDockHintState(DockHint.TabLeft).IsEnabled = flag2;
        }

        internal void SetCanFill(bool canFill)
        {
            DockHintState dockHintState = this.GetDockHintState(DockHint.Center);
            dockHintState.IsVisible = dockHintState.IsEnabled = canFill;
        }

        public void SetCanHide(bool canHide)
        {
            bool flag2;
            bool flag3;
            this.GetDockHintState(DockHint.AutoHideTop).IsEnabled = flag3 = canHide;
            this.GetDockHintState(DockHint.AutoHideRight).IsEnabled = flag2 = flag3;
            this.GetDockHintState(DockHint.AutoHideBottom).IsEnabled = this.GetDockHintState(DockHint.AutoHideLeft).IsEnabled = flag2;
        }

        internal void SetCenterGuidVisibility(bool isVisible)
        {
            this.GetDockHintState(DockGuide.Center).IsVisible = isVisible;
        }

        public void SetConfiguration(DockLayoutManager manager, DockLayoutElementDragInfo dragInfo)
        {
            if (manager != null)
            {
                this.ShowSideDockHints = dragInfo.View.Type == HostType.Layout;
                this.ShowSelfDockHint = dragInfo.AcceptSelfDock();
                bool flag = dragInfo.AcceptDockCenter();
                bool canFill = dragInfo.AcceptFill();
                bool canDock = dragInfo.CanDock;
                bool canDockToTab = dragInfo.CanDockToTab;
                bool canDockToSide = dragInfo.CanDockToSide;
                bool canHide = dragInfo.CanHide;
                this.SetCanHide(canHide);
                this.SetCanDockCenter(canDock);
                this.SetCanDockToTab(canDockToTab);
                this.SetCanDockToSide(canDockToSide);
                this.SetCanFill(canFill);
                this.SetSideGuidsVisibility(this.ShowSideDockHints && (canDockToSide | canHide));
                this.SetCenterGuidVisibility((this.ShowSelfDockHint & flag) && ((canFill | canDock) | canDockToTab));
                BaseLayoutItem target = dragInfo.Target;
                if (!target.GetIsDocumentHost())
                {
                    this.DisplayMode = DockGuidDisplayMode.DockOnly;
                }
                else
                {
                    this.DisplayMode = ((manager.DockingStyle != DockingStyle.VS2010) || !dragInfo.Item.GetAllowDockToDocumentGroup()) ? DockGuidDisplayMode.DockOnly : DockGuidDisplayMode.Full;
                    BaseLayoutItem item2 = dragInfo.Item;
                    if (item2.ItemType == LayoutItemType.Document)
                    {
                        this.DisplayMode = DockGuidDisplayMode.TabOnly;
                    }
                    if ((item2 is FloatGroup) && ((FloatGroup) item2).IsDocumentHost)
                    {
                        this.DisplayMode = DockGuidDisplayMode.TabOnly;
                    }
                    LayoutGroup group = target.Parent ?? ((LayoutGroup) target);
                    if (group.Items.Count<BaseLayoutItem>((<>c.<>9__41_0 ??= item => ((item is DocumentGroup) && (((DocumentGroup) item).Items.Count > 0)))) > 1)
                    {
                        if (group.Orientation == Orientation.Horizontal)
                        {
                            this.Disable(DockHint.TabTop);
                            this.Disable(DockHint.TabBottom);
                            if (DockControllerHelper.GetNextNotEmptyDocumentGroup(target) != null)
                            {
                                this.Disable(DockHint.CenterRight);
                            }
                            if (DockControllerHelper.GetPreviousNotEmptyDocumentGroup(target) != null)
                            {
                                this.Disable(DockHint.CenterLeft);
                            }
                        }
                        else
                        {
                            this.Disable(DockHint.TabLeft);
                            this.Disable(DockHint.TabRight);
                            if (DockControllerHelper.GetNextNotEmptyDocumentGroup(target) != null)
                            {
                                this.Disable(DockHint.CenterBottom);
                            }
                            if (DockControllerHelper.GetPreviousNotEmptyDocumentGroup(target) != null)
                            {
                                this.Disable(DockHint.CenterTop);
                            }
                        }
                    }
                }
            }
        }

        internal void SetSideGuidsVisibility(bool isVisible)
        {
            bool flag2;
            bool flag3;
            this.GetDockHintState(DockGuide.Bottom).IsVisible = flag3 = isVisible;
            this.GetDockHintState(DockGuide.Top).IsVisible = flag2 = flag3;
            this.GetDockHintState(DockGuide.Left).IsVisible = this.GetDockHintState(DockGuide.Right).IsVisible = flag2;
        }

        public bool DisableAll { get; set; }

        public bool HideAll { get; set; }

        public bool ShowSideDockHints { get; set; }

        public bool ShowSelfDockHint { get; set; }

        public DockGuidDisplayMode DisplayMode { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockHintsConfiguration.<>c <>9 = new DockHintsConfiguration.<>c();
            public static Func<BaseLayoutItem, bool> <>9__41_0;

            internal bool <SetConfiguration>b__41_0(BaseLayoutItem item) => 
                (item is DocumentGroup) && (((DocumentGroup) item).Items.Count > 0);
        }

        private class DockHintState
        {
            private bool _IsEnabled = true;
            private bool _IsVisible = true;

            public bool IsEnabled
            {
                get => 
                    this._IsEnabled;
                set => 
                    this._IsEnabled = value;
            }

            public bool IsVisible
            {
                get => 
                    this._IsVisible;
                set => 
                    this._IsVisible = value;
            }
        }
    }
}

