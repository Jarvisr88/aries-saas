namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;

    public abstract class LayoutControlBaseAutomationPeer<T> : BaseLayoutAutomationPeer<T> where T: LayoutControlBase
    {
        protected LayoutControlBaseAutomationPeer(T owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> children = new List<AutomationPeer>();
            BaseLayoutAutomationPeer.IteratorCallback callback = delegate (AutomationPeer peer) {
                children.Add(peer);
                return false;
            };
            Iterate(base.Owner, callback, <>c<T>.<>9__5_1 ??= obj => ((obj != null) && (obj is UIElement)));
            return children;
        }

        protected override string GetClassNameCore() => 
            base.Owner.GetType().Name;

        protected virtual bool IncludeInternalElements =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlBaseAutomationPeer<T>.<>c <>9;
            public static BaseLayoutAutomationPeer.FilterCallback <>9__5_1;

            static <>c()
            {
                LayoutControlBaseAutomationPeer<T>.<>c.<>9 = new LayoutControlBaseAutomationPeer<T>.<>c();
            }

            internal bool <GetChildrenCore>b__5_1(object obj) => 
                (obj != null) && (obj is UIElement);
        }
    }
}

