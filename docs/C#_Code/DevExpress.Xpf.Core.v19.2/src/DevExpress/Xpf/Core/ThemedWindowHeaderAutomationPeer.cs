namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class ThemedWindowHeaderAutomationPeer : FrameworkElementAutomationPeer, IValueProvider
    {
        public ThemedWindowHeaderAutomationPeer(ThemedWindowHeader owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.TitleBar;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            IEnumerable<AutomationPeer> enumerable1;
            List<AutomationPeer> childrenCore = base.GetChildrenCore();
            if (childrenCore == null)
            {
                enumerable1 = null;
            }
            else
            {
                Func<AutomationPeer, bool> predicate = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<AutomationPeer, bool> local1 = <>c.<>9__2_0;
                    predicate = <>c.<>9__2_0 = x => HeaderPeersFilter(x);
                }
                enumerable1 = childrenCore.Where<AutomationPeer>(predicate);
            }
            IEnumerable<AutomationPeer> source = enumerable1;
            return ((source != null) ? source.ToList<AutomationPeer>() : null);
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface == PatternInterface.Value) ? this.GetValueInterface() : null;

        protected virtual string GetValue() => 
            this.GetValueCore();

        protected virtual string GetValueCore()
        {
            string str;
            string title;
            ThemedWindowHeader owner = base.Owner as ThemedWindowHeader;
            if (owner != null)
            {
                title = owner.Window.Title;
            }
            else
            {
                ThemedWindowHeader local1 = owner;
                title = null;
            }
            string text2 = title;
            if (str == null)
            {
                string local2 = title;
                text2 = string.Empty;
            }
            return text2;
        }

        private object GetValueInterface() => 
            this;

        private static bool HeaderPeersFilter(AutomationPeer peer)
        {
            ButtonAutomationPeer peer2 = peer as ButtonAutomationPeer;
            return ((peer2 == null) ? (!(peer is ImageAutomationPeer) ? !(peer is TextBlockAutomationPeer) : false) : peer2.Owner.IsVisible);
        }

        protected virtual void SetValue(string value)
        {
            this.SetValueCore(value);
        }

        protected virtual void SetValueCore(string value)
        {
        }

        void IValueProvider.SetValue(string value)
        {
            this.SetValue(value);
        }

        bool IValueProvider.IsReadOnly =>
            false;

        string IValueProvider.Value =>
            this.GetValue();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowHeaderAutomationPeer.<>c <>9 = new ThemedWindowHeaderAutomationPeer.<>c();
            public static Func<AutomationPeer, bool> <>9__2_0;

            internal bool <GetChildrenCore>b__2_0(AutomationPeer x) => 
                ThemedWindowHeaderAutomationPeer.HeaderPeersFilter(x);
        }
    }
}

