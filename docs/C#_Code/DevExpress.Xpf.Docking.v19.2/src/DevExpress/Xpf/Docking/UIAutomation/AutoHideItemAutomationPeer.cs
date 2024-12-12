namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class AutoHideItemAutomationPeer : BaseLayoutItemAutomationPeer, IExpandCollapseProvider
    {
        public AutoHideItemAutomationPeer(BaseLayoutItem frameworkElement) : base(frameworkElement)
        {
        }

        public void Collapse()
        {
            AutoHideTray autoHideTray = this.GetAutoHideTray();
            if (autoHideTray.Items.Count == 0)
            {
                throw new InvalidOperationException();
            }
            autoHideTray.DoCollapse(null);
        }

        public void Expand()
        {
            AutoHideTray autoHideTray = this.GetAutoHideTray();
            if (autoHideTray != null)
            {
                if (autoHideTray.Items.Count == 0)
                {
                    throw new InvalidOperationException();
                }
                autoHideTray.DoExpand(base.Owner);
            }
        }

        private AutoHideTray GetAutoHideTray()
        {
            AutoHidePaneHeaderItem uIElement = base.Owner.GetUIElement<AutoHidePaneHeaderItem>();
            return uIElement?.HeadersGroup.Tray;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.ExpandCollapse) ? base.GetPattern(patternInterface) : this;

        protected internal void RaiseExpandCollapseAutomationEvent(bool value)
        {
            this.RaisePropertyChangedEvent(ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty, !value ? System.Windows.Automation.ExpandCollapseState.Expanded : System.Windows.Automation.ExpandCollapseState.Collapsed, value ? System.Windows.Automation.ExpandCollapseState.Expanded : System.Windows.Automation.ExpandCollapseState.Collapsed);
        }

        public System.Windows.Automation.ExpandCollapseState ExpandCollapseState
        {
            get
            {
                AutoHideTray autoHideTray = this.GetAutoHideTray();
                return (((autoHideTray == null) || (autoHideTray.Items.Count == 0)) ? System.Windows.Automation.ExpandCollapseState.LeafNode : (!autoHideTray.IsAnimated ? (!autoHideTray.IsExpanded ? System.Windows.Automation.ExpandCollapseState.Collapsed : System.Windows.Automation.ExpandCollapseState.Expanded) : System.Windows.Automation.ExpandCollapseState.PartiallyExpanded));
            }
        }
    }
}

