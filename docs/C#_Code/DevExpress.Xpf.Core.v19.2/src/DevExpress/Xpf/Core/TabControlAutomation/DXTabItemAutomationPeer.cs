namespace DevExpress.Xpf.Core.TabControlAutomation
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Media;

    public class DXTabItemAutomationPeer : BaseNavigationAutomationPeer, IScrollItemProvider, ISelectionItemProvider
    {
        public DXTabItemAutomationPeer(DXTabItem ownerCore) : base(ownerCore)
        {
        }

        public void AddToSelection()
        {
            if (this.TabControl != null)
            {
                AutomationPeer peer = CreatePeerForElement(this.TabControl);
                if (peer != null)
                {
                    ISelectionProvider pattern = peer.GetPattern(PatternInterface.Selection) as ISelectionProvider;
                    if ((pattern != null) && (!pattern.CanSelectMultiple && (this.TabControl.SelectedContainer != null)))
                    {
                        throw new InvalidOperationException("DXTabControl does not allow multiselection");
                    }
                }
                this.SelectCore(AutomationEvents.SelectionItemPatternOnElementSelected);
            }
        }

        protected override Func<DependencyObject>[] GetAttachedAutomationPropertySource() => 
            new Func<DependencyObject>[] { () => ((this.TabControl == null) || (this.TabControl.ItemContainerGenerator == null)) ? null : (this.TabControl.ItemContainerGenerator.ItemFromContainer(this.TabItem) as DependencyObject), () => this.TabItem };

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.TabItem;

        protected override string GetAutomationIdCore()
        {
            bool flag;
            object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.AutomationIdProperty, out flag);
            return BarsAutomationHelper.CreateAutomationID(this.TabItem, this, flag ? ((string) obj2) : null);
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> children = new List<AutomationPeer>();
            if (this.TabItem.IsSelected)
            {
                DependencyObject owner = null;
                if (this.TabControl == null)
                {
                    return children;
                }
                if (this.TabControl.TabContentCacheMode == TabContentCacheMode.None)
                {
                    if (this.TabControl.ContentPresenter == null)
                    {
                        return children;
                    }
                    DependencyObject obj3 = (VisualTreeHelper.GetChildrenCount(this.TabControl.ContentPresenter) == 0) ? null : VisualTreeHelper.GetChild(this.TabControl.ContentPresenter, 0);
                    owner = (obj3 == null) ? this.TabControl.ContentPresenter : obj3;
                }
                else
                {
                    if ((this.TabControl.FastRenderPanel == null) || (this.TabControl.FastRenderPanel.SelectedItem == null))
                    {
                        return children;
                    }
                    DependencyObject obj4 = (VisualTreeHelper.GetChildrenCount(this.TabControl.FastRenderPanel.SelectedItem) == 0) ? null : VisualTreeHelper.GetChild(this.TabControl.FastRenderPanel.SelectedItem, 0);
                    owner = (obj4 == null) ? this.TabControl.FastRenderPanel.SelectedItem : obj4;
                }
                if (owner == null)
                {
                    return children;
                }
                BarsAutomationHelper.GetChildrenRecursive(children, owner);
            }
            return children;
        }

        protected override string GetClassNameCore() => 
            typeof(DXTabItem).Name;

        protected override string GetNameCore()
        {
            bool flag;
            object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.NameProperty, out flag);
            return (!flag ? ("DXTabItem" + Convert.ToString(this.TabItem.Header)) : ((string) obj2));
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.SelectionItem) ? ((patternInterface != PatternInterface.ScrollItem) ? null : this) : this;

        public void RemoveFromSelection()
        {
            bool isSelected = this.IsSelected;
            if (this.TabControl != null)
            {
                AutomationPeer peer = CreatePeerForElement(this.TabControl);
                if (peer != null)
                {
                    ISelectionProvider pattern = peer.GetPattern(PatternInterface.Selection) as ISelectionProvider;
                    if ((pattern != null) && pattern.IsSelectionRequired)
                    {
                        throw new ArgumentException("You cannot remove item from selection because it is required for DXTabControl");
                    }
                }
            }
        }

        public void ScrollIntoView()
        {
            if ((this.TabControl != null) && ((this.ScrollPanel != null) && this.ScrollPanel.CanScroll))
            {
                int index = this.TabControl.IndexOf(this.TabItem);
                if ((index != -1) && this.ScrollPanel.CanScrollTo(index))
                {
                    this.ScrollPanel.ScrollTo(index);
                }
            }
        }

        public void Select()
        {
            this.SelectCore(AutomationEvents.SelectionItemPatternOnElementAddedToSelection);
        }

        public void SelectCore(AutomationEvents rEvent)
        {
            if (this.TabControl != null)
            {
                bool isSelected = this.IsSelected;
                this.TabControl.SelectedItem = this.TabItem;
            }
        }

        private DXTabItem TabItem =>
            base.Owner as DXTabItem;

        private DXTabControl TabControl
        {
            get
            {
                Func<DXTabItem, DXTabControl> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<DXTabItem, DXTabControl> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.Owner;
                }
                return this.TabItem.With<DXTabItem, DXTabControl>(evaluator);
            }
        }

        private TabControlViewBase View
        {
            get
            {
                Func<DXTabControl, TabControlViewBase> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<DXTabControl, TabControlViewBase> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.View;
                }
                return this.TabControl.With<DXTabControl, TabControlViewBase>(evaluator);
            }
        }

        private TabPanelScrollView ScrollPanel
        {
            get
            {
                Func<TabControlViewBase, TabControlScrollView> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<TabControlViewBase, TabControlScrollView> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.ScrollView;
                }
                Func<TabControlScrollView, TabPanelScrollView> func2 = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<TabControlScrollView, TabPanelScrollView> local2 = <>c.<>9__7_1;
                    func2 = <>c.<>9__7_1 = x => x.ScrollPanel;
                }
                return this.View.With<TabControlViewBase, TabControlScrollView>(evaluator).With<TabControlScrollView, TabPanelScrollView>(func2);
            }
        }

        public bool IsSelected =>
            (this.TabControl != null) && (this.TabControl.SelectedContainer == this.TabItem);

        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                if (this.TabControl == null)
                {
                    return null;
                }
                AutomationPeer peer = CreatePeerForElement(this.TabControl);
                return ((peer != null) ? base.ProviderFromPeer(peer) : null);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabItemAutomationPeer.<>c <>9 = new DXTabItemAutomationPeer.<>c();
            public static Func<DXTabItem, DXTabControl> <>9__3_0;
            public static Func<DXTabControl, TabControlViewBase> <>9__5_0;
            public static Func<TabControlViewBase, TabControlScrollView> <>9__7_0;
            public static Func<TabControlScrollView, TabPanelScrollView> <>9__7_1;

            internal TabControlScrollView <get_ScrollPanel>b__7_0(TabControlViewBase x) => 
                x.ScrollView;

            internal TabPanelScrollView <get_ScrollPanel>b__7_1(TabControlScrollView x) => 
                x.ScrollPanel;

            internal DXTabControl <get_TabControl>b__3_0(DXTabItem x) => 
                x.Owner;

            internal TabControlViewBase <get_View>b__5_0(DXTabControl x) => 
                x.View;
        }
    }
}

