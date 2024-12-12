namespace DevExpress.Xpf.Grid.Automation
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Media;

    public abstract class DataControlAutomationPeerBase : FrameworkElementAutomationPeer, IAutomationPeerCreator
    {
        private DataControlBase dataControl;

        protected DataControlAutomationPeerBase(DataControlBase dataControl, FrameworkElement element) : base(element)
        {
            this.dataControl = dataControl;
        }

        public virtual AutomationPeer CreatePeerCore(DependencyObject obj) => 
            CreatePeerDefault(obj);

        public static AutomationPeer CreatePeerDefault(DependencyObject obj) => 
            !(obj is UIElement3D) ? (!(obj is UIElement) ? null : CreatePeerForElement(obj as UIElement)) : UIElement3DAutomationPeer.CreatePeerForElement(obj as UIElement3D);

        AutomationPeer IAutomationPeerCreator.CreatePeer(DependencyObject obj)
        {
            AutomationPeer peer = this.DataControl.PeerCache.GetPeer(obj);
            if (peer == null)
            {
                peer = this.CreatePeerCore(obj);
                this.DataControl.PeerCache.AddPeer(obj, peer, true);
            }
            return peer;
        }

        public static DependencyObject FindObjectInVisualTree(DependencyObject root, string objectName)
        {
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(root);
            while (enumerator.MoveNext())
            {
                if ((enumerator.Current is FrameworkElement) && ((enumerator.Current as FrameworkElement).Name == objectName))
                {
                    return enumerator.Current;
                }
            }
            return null;
        }

        public static DependencyObject FindObjectInVisualTreeByType(DependencyObject root, Type objectType)
        {
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(root);
            while (enumerator.MoveNext())
            {
                if ((enumerator.Current is FrameworkElement) && objectType.IsAssignableFrom(enumerator.Current.GetType()))
                {
                    return enumerator.Current;
                }
            }
            return null;
        }

        public static AutomationPeer FindParentAutomationPeerByType(AutomationPeer root, Type type)
        {
            AutomationPeer parent = root;
            while ((parent != null) && (parent.GetType() != type))
            {
                parent = parent.GetParent();
            }
            return parent;
        }

        protected override List<AutomationPeer> GetChildrenCore() => 
            null;

        protected List<AutomationPeer> GetUIChildrenCore(DependencyObject obj) => 
            GetUIChildrenCore(obj, this);

        public static List<AutomationPeer> GetUIChildrenCore(DependencyObject obj, IAutomationPeerCreator owner)
        {
            List<AutomationPeer> children = null;
            GetUIChildrenCore(obj, owner, ref children);
            return children;
        }

        protected void GetUIChildrenCore(DependencyObject obj, ref List<AutomationPeer> children)
        {
            GetUIChildrenCore(obj, this, ref children);
        }

        public static void GetUIChildrenCore(DependencyObject obj, IAutomationPeerCreator owner, ref List<AutomationPeer> children)
        {
            if (obj != null)
            {
                AutomationPeer item = null;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    item = null;
                    if (child != null)
                    {
                        item = owner.CreatePeer(child);
                        if (item != null)
                        {
                            children ??= new List<AutomationPeer>();
                            children.Add(item);
                        }
                    }
                    if (item == null)
                    {
                        GetUIChildrenCore(child, owner, ref children);
                    }
                }
            }
        }

        protected override bool HasKeyboardFocusCore() => 
            this.DataControl.viewCore.IsFocused;

        protected override bool IsKeyboardFocusableCore() => 
            this.DataControl.viewCore.Focusable;

        protected override void SetFocusCore()
        {
            this.DataControl.viewCore.Focus();
        }

        public DataControlBase DataControl =>
            this.dataControl;
    }
}

