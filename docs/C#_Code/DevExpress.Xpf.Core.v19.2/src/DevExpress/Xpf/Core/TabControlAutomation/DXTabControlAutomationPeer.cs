namespace DevExpress.Xpf.Core.TabControlAutomation
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class DXTabControlAutomationPeer : BaseNavigationAutomationPeer, IScrollProvider, ISelectionProvider
    {
        public DXTabControlAutomationPeer(DXTabControl ownerCore) : base(ownerCore)
        {
            if (this.TabControl != null)
            {
                this.TabControl.SelectionChanged += new TabControlSelectionChangedEventHandler(this.OnSelectionChanged);
                this.TabControl.ItemsChanged += new NotifyCollectionChangedEventHandler(this.OnItemsChanged);
            }
        }

        private void DecreaseOffset(double offset)
        {
            if (DXTabControlAutomationStringProvider.disableAnimationCore)
            {
                this.ScrollPanel.disableAnimation = true;
            }
            this.ScrollPanel.Offset = (this.ScrollPanel.MinOffset <= (this.ScrollPanel.Offset - offset)) ? (this.ScrollPanel.Offset - offset) : this.ScrollPanel.MinOffset;
            if (DXTabControlAutomationStringProvider.disableAnimationCore)
            {
                this.ScrollPanel.disableAnimation = false;
            }
        }

        protected override Func<DependencyObject>[] GetAttachedAutomationPropertySource() => 
            new Func<DependencyObject>[] { () => this.TabControl };

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Tab;

        protected override string GetAutomationIdCore()
        {
            bool flag;
            object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.AutomationIdProperty, out flag);
            return BarsAutomationHelper.CreateAutomationID(this.TabControl, this, flag ? ((string) obj2) : null);
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> list = new List<AutomationPeer>();
            for (int i = 0; i < this.TabControl.Items.Count; i++)
            {
                object obj2 = this.TabControl.ItemContainerGenerator.ContainerFromIndex(i);
                if (obj2 is FrameworkElement)
                {
                    AutomationPeer item = CreatePeerForElement(obj2 as FrameworkElement);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        protected override string GetClassNameCore() => 
            typeof(DXTabControl).Name;

        protected override string GetNameCore()
        {
            bool flag;
            object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.NameProperty, out flag);
            return (!flag ? ("DXTabControl" + Convert.ToString(this.TabControl.Name)) : ((string) obj2));
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Selection) ? ((patternInterface != PatternInterface.Scroll) ? null : this) : this;

        public IRawElementProviderSimple[] GetSelection()
        {
            if (this.TabControl.SelectedContainer == null)
            {
                return null;
            }
            AutomationPeer peer = CreatePeerForElement(this.TabControl.SelectedContainer);
            if (peer == null)
            {
                return null;
            }
            IRawElementProviderSimple simple = base.ProviderFromPeer(peer);
            if (simple == null)
            {
                return null;
            }
            return new IRawElementProviderSimple[] { simple };
        }

        private void IncreaseOffset(double offset)
        {
            if (DXTabControlAutomationStringProvider.disableAnimationCore)
            {
                this.ScrollPanel.disableAnimation = true;
            }
            this.ScrollPanel.Offset = (this.ScrollPanel.MaxOffset > (this.ScrollPanel.Offset + offset)) ? (this.ScrollPanel.Offset + offset) : this.ScrollPanel.MaxOffset;
            if (DXTabControlAutomationStringProvider.disableAnimationCore)
            {
                this.ScrollPanel.disableAnimation = false;
            }
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.ResetChildrenCache();
            base.RaiseAutomationEvent(AutomationEvents.StructureChanged);
        }

        private void OnSelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
        {
            DXTabItem element = null;
            DXTabItem item2 = null;
            if (this.TabControl != null)
            {
                element = (e.OldSelectedItem == null) ? null : (this.TabControl.ItemContainerGenerator.ContainerFromItem(e.OldSelectedItem) as DXTabItem);
                item2 = (e.NewSelectedItem == null) ? null : (this.TabControl.ItemContainerGenerator.ContainerFromItem(e.NewSelectedItem) as DXTabItem);
            }
            DXTabItemAutomationPeer oldPeer = null;
            DXTabItemAutomationPeer newPeer = null;
            if (element != null)
            {
                oldPeer = CreatePeerForElement(element) as DXTabItemAutomationPeer;
            }
            if (item2 != null)
            {
                newPeer = CreatePeerForElement(item2) as DXTabItemAutomationPeer;
            }
            base.Dispatcher.BeginInvoke(() => this.OnSelectionChangedCore(oldPeer, newPeer), DispatcherPriority.Render, new object[0]);
        }

        private void OnSelectionChangedCore(DXTabItemAutomationPeer oldPeer, DXTabItemAutomationPeer newPeer)
        {
            if (oldPeer != null)
            {
                oldPeer.ResetChildrenCache();
                oldPeer.RaiseAutomationEvent(AutomationEvents.StructureChanged);
            }
            if (newPeer != null)
            {
                newPeer.ResetChildrenCache();
                newPeer.RaiseAutomationEvent(AutomationEvents.StructureChanged);
            }
            base.RaiseAutomationEvent(AutomationEvents.StructureChanged);
            if (!ReferenceEquals(oldPeer, newPeer))
            {
                if (newPeer != null)
                {
                    newPeer.RaisePropertyChangedEvent(SelectionItemPatternIdentifiers.IsSelectedProperty, false, true);
                }
                if (oldPeer != null)
                {
                    oldPeer.RaisePropertyChangedEvent(SelectionItemPatternIdentifiers.IsSelectedProperty, true, false);
                }
                IRawElementProviderSimple provider = null;
                if (newPeer != null)
                {
                    provider = base.ProviderFromPeer(newPeer);
                }
                if (provider != null)
                {
                    AutomationInteropProvider.RaiseAutomationEvent(SelectionItemPatternIdentifiers.ElementSelectedEvent, provider, new AutomationEventArgs(SelectionItemPatternIdentifiers.ElementSelectedEvent));
                }
            }
        }

        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            if ((!this.VerticallyScrollable && (verticalAmount != ScrollAmount.NoAmount)) || (!this.HorizontallyScrollable && (horizontalAmount != ScrollAmount.NoAmount)))
            {
                throw new InvalidOperationException("Can not scroll to unsupported direcetion");
            }
            if (this.CanScrollCore)
            {
                double scrollPercent = this.ScrollPercent;
                ScrollAmount amount = this.HorizontallyScrollable ? horizontalAmount : verticalAmount;
                int firstFullVisibleItemIndex = this.ScrollPanel.GetFirstFullVisibleItemIndex();
                if (firstFullVisibleItemIndex != -1)
                {
                    switch (amount)
                    {
                        case ScrollAmount.LargeDecrement:
                            if (firstFullVisibleItemIndex <= 0)
                            {
                                return;
                            }
                            this.DecreaseOffset(this.ScrollPanel.VisibleLength);
                            break;

                        case ScrollAmount.SmallDecrement:
                        {
                            if (firstFullVisibleItemIndex <= 0)
                            {
                                return;
                            }
                            double offset = this.ScrollPanel.GetHeaderOffset(firstFullVisibleItemIndex) - this.ScrollPanel.GetHeaderOffset(firstFullVisibleItemIndex - 1);
                            this.DecreaseOffset(offset);
                            break;
                        }
                        case ScrollAmount.NoAmount:
                            return;

                        case ScrollAmount.LargeIncrement:
                            if (firstFullVisibleItemIndex >= (this.TabControl.Items.Count - 1))
                            {
                                return;
                            }
                            this.IncreaseOffset(this.ScrollPanel.VisibleLength);
                            break;

                        case ScrollAmount.SmallIncrement:
                        {
                            if (firstFullVisibleItemIndex >= (this.TabControl.Items.Count - 1))
                            {
                                return;
                            }
                            double offset = this.ScrollPanel.GetHeaderOffset(firstFullVisibleItemIndex + 1) - this.ScrollPanel.Offset;
                            this.IncreaseOffset(offset);
                            break;
                        }
                        default:
                            break;
                    }
                    if (scrollPercent != this.ScrollPercent)
                    {
                        this.RaisePropertyChangedEvent(this.HorizontallyScrollable ? ScrollPatternIdentifiers.HorizontalScrollPercentProperty : ScrollPatternIdentifiers.VerticalScrollPercentProperty, scrollPercent, this.ScrollPercent);
                    }
                }
            }
        }

        public void SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            if (this.VerticallyScrollable || ((verticalPercent == 0.0) || (verticalPercent == -1.0)))
            {
                if (this.HorizontallyScrollable || ((horizontalPercent == 0.0) || (horizontalPercent == -1.0)))
                {
                    if ((!this.VerticallyScrollable || (((verticalPercent >= 0.0) || (verticalPercent == -1.0)) && (verticalPercent <= 100.0))) && (!this.HorizontallyScrollable || (((horizontalPercent >= 0.0) || (horizontalPercent == -1.0)) && (horizontalPercent <= 100.0))))
                    {
                        if (this.VerticallyScrollable || this.HorizontallyScrollable)
                        {
                            double oldValue = this.HorizontallyScrollable ? this.HorizontalScrollPercent : this.VerticalScrollPercent;
                            double newValue = this.HorizontallyScrollable ? horizontalPercent : verticalPercent;
                            if (DXTabControlAutomationStringProvider.disableAnimationCore)
                            {
                                this.ScrollPanel.disableAnimation = true;
                            }
                            this.ScrollPanel.Offset = (newValue / 100.0) * this.ScrollPanel.MaxOffset;
                            if (DXTabControlAutomationStringProvider.disableAnimationCore)
                            {
                                this.ScrollPanel.disableAnimation = false;
                            }
                            this.RaisePropertyChangedEvent(this.HorizontallyScrollable ? ScrollPatternIdentifiers.HorizontalScrollPercentProperty : ScrollPatternIdentifiers.VerticalScrollPercentProperty, oldValue, newValue);
                        }
                        return;
                    }
                }
                else
                {
                    goto TR_0000;
                }
            }
            else
            {
                goto TR_0000;
            }
            throw new IndexOutOfRangeException("Scroll percent is out of range");
        TR_0000:
            throw new InvalidOperationException("Can not scroll to unsupported direcetion");
        }

        private DXTabControl TabControl =>
            base.Owner as DXTabControl;

        private TabControlViewBase View
        {
            get
            {
                Func<DXTabControl, TabControlViewBase> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<DXTabControl, TabControlViewBase> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.View;
                }
                return this.TabControl.With<DXTabControl, TabControlViewBase>(evaluator);
            }
        }

        private TabPanelScrollView ScrollPanel
        {
            get
            {
                Func<TabControlViewBase, TabControlScrollView> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<TabControlViewBase, TabControlScrollView> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.ScrollView;
                }
                Func<TabControlScrollView, TabPanelScrollView> func2 = <>c.<>9__5_1;
                if (<>c.<>9__5_1 == null)
                {
                    Func<TabControlScrollView, TabPanelScrollView> local2 = <>c.<>9__5_1;
                    func2 = <>c.<>9__5_1 = x => x.ScrollPanel;
                }
                return this.View.With<TabControlViewBase, TabControlScrollView>(evaluator).With<TabControlScrollView, TabPanelScrollView>(func2);
            }
        }

        private bool IsHorizontallyOriented =>
            !this.IsVerticallyOriented;

        private bool IsVerticallyOriented =>
            (this.View.HeaderLocation == HeaderLocation.Left) || (this.View.HeaderLocation == HeaderLocation.Right);

        private bool CanScrollCore
        {
            get
            {
                Func<TabPanelScrollView, bool> evaluator = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Func<TabPanelScrollView, bool> local1 = <>c.<>9__20_0;
                    evaluator = <>c.<>9__20_0 = x => x.CanScroll && !(x.MaxOffset == 0.0);
                }
                return this.ScrollPanel.Return<TabPanelScrollView, bool>(evaluator, (<>c.<>9__20_1 ??= () => false));
            }
        }

        private double ScrollPercent =>
            this.CanScrollCore ? ((this.ScrollPanel.Offset / this.ScrollPanel.MaxOffset) * 100.0) : -1.0;

        private double ViewSizeCore =>
            (!this.CanScrollCore || (this.ScrollPanel.PanelLength == 0.0)) ? 100.0 : ((this.ScrollPanel.MaxOffset / this.ScrollPanel.PanelLength) * 100.0);

        public double HorizontalScrollPercent =>
            this.IsHorizontallyOriented ? this.ScrollPercent : -1.0;

        public double HorizontalViewSize =>
            this.IsHorizontallyOriented ? this.ViewSizeCore : 100.0;

        public bool HorizontallyScrollable =>
            this.IsHorizontallyOriented && this.CanScrollCore;

        public double VerticalScrollPercent =>
            this.IsVerticallyOriented ? this.ScrollPercent : -1.0;

        public double VerticalViewSize =>
            this.IsVerticallyOriented ? this.ViewSizeCore : 100.0;

        public bool VerticallyScrollable =>
            this.IsVerticallyOriented && this.CanScrollCore;

        public bool CanSelectMultiple =>
            false;

        public bool IsSelectionRequired =>
            this.TabControl.HasItems;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabControlAutomationPeer.<>c <>9 = new DXTabControlAutomationPeer.<>c();
            public static Func<DXTabControl, TabControlViewBase> <>9__3_0;
            public static Func<TabControlViewBase, TabControlScrollView> <>9__5_0;
            public static Func<TabControlScrollView, TabPanelScrollView> <>9__5_1;
            public static Func<TabPanelScrollView, bool> <>9__20_0;
            public static Func<bool> <>9__20_1;

            internal bool <get_CanScrollCore>b__20_0(TabPanelScrollView x) => 
                x.CanScroll && !(x.MaxOffset == 0.0);

            internal bool <get_CanScrollCore>b__20_1() => 
                false;

            internal TabControlScrollView <get_ScrollPanel>b__5_0(TabControlViewBase x) => 
                x.ScrollView;

            internal TabPanelScrollView <get_ScrollPanel>b__5_1(TabControlScrollView x) => 
                x.ScrollPanel;

            internal TabControlViewBase <get_View>b__3_0(DXTabControl x) => 
                x.View;
        }
    }
}

