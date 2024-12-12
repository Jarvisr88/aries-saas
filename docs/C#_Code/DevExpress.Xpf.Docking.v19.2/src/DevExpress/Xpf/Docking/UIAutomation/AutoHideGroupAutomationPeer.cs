namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public class AutoHideGroupAutomationPeer : LayoutGroupAutomationPeer, IDockProvider
    {
        public AutoHideGroupAutomationPeer(LayoutGroup element) : base(element)
        {
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Dock) ? base.GetPattern(patternInterface) : this;

        public void SetDockPosition(System.Windows.Automation.DockPosition dockPosition)
        {
            AutoHideGroup owner = (AutoHideGroup) base.Owner;
            if (owner != null)
            {
                switch (dockPosition)
                {
                    case System.Windows.Automation.DockPosition.Top:
                        owner.DockType = Dock.Top;
                        return;

                    case System.Windows.Automation.DockPosition.Left:
                        owner.DockType = Dock.Left;
                        return;

                    case System.Windows.Automation.DockPosition.Bottom:
                        owner.DockType = Dock.Bottom;
                        return;

                    case System.Windows.Automation.DockPosition.Right:
                        owner.DockType = Dock.Right;
                        return;
                }
                throw new InvalidOperationException();
            }
        }

        protected override void SetFocusCore()
        {
            if (base.Owner.SelectedItem != null)
            {
                base.Owner.Manager.Activate(base.Owner.SelectedItem);
            }
            else if (base.Owner.Items.Count > 0)
            {
                base.Owner.Manager.Activate(base.Owner.Items[0]);
            }
            else
            {
                base.SetFocusCore();
            }
        }

        public System.Windows.Automation.DockPosition DockPosition
        {
            get
            {
                switch (((AutoHideGroup) base.Owner).DockType)
                {
                    case Dock.Left:
                        return System.Windows.Automation.DockPosition.Left;

                    case Dock.Top:
                        return System.Windows.Automation.DockPosition.Top;

                    case Dock.Right:
                        return System.Windows.Automation.DockPosition.Right;

                    case Dock.Bottom:
                        return System.Windows.Automation.DockPosition.Bottom;
                }
                return System.Windows.Automation.DockPosition.None;
            }
        }
    }
}

