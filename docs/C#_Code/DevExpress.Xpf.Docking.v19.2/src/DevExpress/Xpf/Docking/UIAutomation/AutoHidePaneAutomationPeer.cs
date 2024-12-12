namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class AutoHidePaneAutomationPeer : BaseAutomationPeer<AutoHidePane>, IExpandCollapseProvider
    {
        private readonly EventHandler weakItemsChangedHandler;

        public AutoHidePaneAutomationPeer(AutoHidePane owner) : base(owner)
        {
            if (owner != null)
            {
                this.weakItemsChangedHandler = new EventHandler(this.OnLayoutItemChanged);
                owner.LayoutItemChanged += this.weakItemsChangedHandler;
            }
        }

        public void Collapse()
        {
            Func<AutoHidePane, AutoHideTray> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<AutoHidePane, AutoHideTray> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.AutoHideTray;
            }
            AutoHideTray tray = base.Owner.With<AutoHidePane, AutoHideTray>(evaluator);
            if (tray.Items.Count == 0)
            {
                throw new InvalidOperationException();
            }
            tray.DoCollapse(null);
        }

        public void Expand()
        {
            Func<AutoHidePane, AutoHideTray> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<AutoHidePane, AutoHideTray> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.AutoHideTray;
            }
            AutoHideTray tray = base.Owner.With<AutoHidePane, AutoHideTray>(evaluator);
            if (tray != null)
            {
                if (tray.Items.Count == 0)
                {
                    throw new InvalidOperationException();
                }
                tray.DoExpand(base.Owner.LayoutItem);
            }
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> list = new List<AutomationPeer>();
            if (base.Owner.LayoutItem != null)
            {
                list.Add(CreatePeerForElement(base.Owner.LayoutItem));
            }
            return list;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.ExpandCollapse) ? base.GetPattern(patternInterface) : this;

        private void OnLayoutItemChanged(object sender, EventArgs e)
        {
            base.ResetChildrenCache();
        }

        protected override void SetFocusCore()
        {
            Func<AutoHidePane, BaseLayoutItem> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<AutoHidePane, BaseLayoutItem> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x.LayoutItem;
            }
            Action<BaseLayoutItem> action = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Action<BaseLayoutItem> local2 = <>c.<>9__8_1;
                action = <>c.<>9__8_1 = x => x.Focus();
            }
            base.Owner.With<AutoHidePane, BaseLayoutItem>(evaluator).Do<BaseLayoutItem>(action);
        }

        public System.Windows.Automation.ExpandCollapseState ExpandCollapseState
        {
            get
            {
                Func<AutoHidePane, AutoHideTray> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<AutoHidePane, AutoHideTray> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.AutoHideTray;
                }
                AutoHideTray tray = base.Owner.With<AutoHidePane, AutoHideTray>(evaluator);
                return (((tray == null) || (tray.Items.Count == 0)) ? System.Windows.Automation.ExpandCollapseState.LeafNode : (!tray.IsAnimated ? (!tray.IsExpanded ? System.Windows.Automation.ExpandCollapseState.Collapsed : System.Windows.Automation.ExpandCollapseState.Expanded) : System.Windows.Automation.ExpandCollapseState.PartiallyExpanded));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHidePaneAutomationPeer.<>c <>9 = new AutoHidePaneAutomationPeer.<>c();
            public static Func<AutoHidePane, AutoHideTray> <>9__4_0;
            public static Func<AutoHidePane, AutoHideTray> <>9__5_0;
            public static Func<AutoHidePane, AutoHideTray> <>9__7_0;
            public static Func<AutoHidePane, BaseLayoutItem> <>9__8_0;
            public static Action<BaseLayoutItem> <>9__8_1;

            internal AutoHideTray <Collapse>b__4_0(AutoHidePane x) => 
                x.AutoHideTray;

            internal AutoHideTray <Expand>b__5_0(AutoHidePane x) => 
                x.AutoHideTray;

            internal AutoHideTray <get_ExpandCollapseState>b__7_0(AutoHidePane x) => 
                x.AutoHideTray;

            internal BaseLayoutItem <SetFocusCore>b__8_0(AutoHidePane x) => 
                x.LayoutItem;

            internal void <SetFocusCore>b__8_1(BaseLayoutItem x)
            {
                x.Focus();
            }
        }
    }
}

