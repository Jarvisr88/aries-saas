namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public class ComboBoxEditItemAutomationPeer : AutomationPeer, ISelectionItemProvider, IScrollItemProvider
    {
        private ComboBoxEdit Owner;
        protected object Item;

        public ComboBoxEditItemAutomationPeer(object item, ComboBoxEdit owner)
        {
            this.Owner = owner;
            this.Item = item;
        }

        protected override string GetAcceleratorKeyCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetAcceleratorKey());
        }

        protected override string GetAccessKeyCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetAccessKey());
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.ListItem;

        protected override string GetAutomationIdCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetAutomationId());
        }

        protected override Rect GetBoundingRectangleCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetBoundingRectangle();
            }
            return new Rect();
        }

        protected override List<AutomationPeer> GetChildrenCore() => 
            this.GetWrapperPeer()?.GetChildren();

        protected override string GetClassNameCore() => 
            "ComboBoxEditItem";

        protected override Point GetClickablePointCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? new Point(double.NaN, double.NaN) : wrapperPeer.GetClickablePoint());
        }

        protected override string GetHelpTextCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetHelpText());
        }

        protected override string GetItemStatusCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetItemStatus());
        }

        protected override string GetItemTypeCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? string.Empty : wrapperPeer.GetItemType());
        }

        protected override AutomationPeer GetLabeledByCore() => 
            this.GetWrapperPeer()?.GetLabeledBy();

        protected override string GetNameCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            string name = null;
            if (wrapperPeer != null)
            {
                name = wrapperPeer.GetName();
            }
            if ((name == null) && (this.Item is string))
            {
                name = (string) this.Item;
            }
            return string.Empty;
        }

        protected override AutomationOrientation GetOrientationCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) ? AutomationOrientation.None : wrapperPeer.GetOrientation());
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.SelectionItem) ? ((patternInterface != PatternInterface.ScrollItem) ? null : this) : this;

        private FrameworkElement GetWrapper() => 
            !(this.Item is ComboBoxEditItem) ? ((this.Owner.ListBox == null) ? null : (this.Owner.ListBox.ItemContainerGenerator.ContainerFromItem(this.Item) as FrameworkElement)) : (this.Item as FrameworkElement);

        private AutomationPeer GetWrapperPeer()
        {
            UIElement wrapper = this.GetWrapper();
            return ((wrapper != null) ? UIElementAutomationPeer.CreatePeerForElement(wrapper) : null);
        }

        protected override bool HasKeyboardFocusCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer != null) && wrapperPeer.HasKeyboardFocus());
        }

        protected override bool IsContentElementCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) || wrapperPeer.IsContentElement());
        }

        protected override bool IsControlElementCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) || wrapperPeer.IsControlElement());
        }

        protected override bool IsEnabledCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer != null) && wrapperPeer.IsEnabled());
        }

        protected override bool IsKeyboardFocusableCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer != null) && wrapperPeer.IsKeyboardFocusable());
        }

        protected override bool IsOffscreenCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer == null) || wrapperPeer.IsOffscreen());
        }

        protected override bool IsPasswordCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer != null) && wrapperPeer.IsPassword());
        }

        protected override bool IsRequiredForFormCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            return ((wrapperPeer != null) && wrapperPeer.IsRequiredForForm());
        }

        protected override void SetFocusCore()
        {
            AutomationPeer wrapperPeer = this.GetWrapperPeer();
            if (wrapperPeer != null)
            {
                wrapperPeer.SetFocus();
            }
        }

        void IScrollItemProvider.ScrollIntoView()
        {
            if (this.Owner.ListBox != null)
            {
                this.Owner.ListBox.ScrollIntoView(this.Item);
            }
        }

        void ISelectionItemProvider.AddToSelection()
        {
            if (this.CanSelectMultiple && !this.Owner.SelectedItems.Contains(this.Item))
            {
                this.Owner.SelectedItems.Add(this.Item);
            }
            else
            {
                this.Owner.SelectedItem = this.Item;
            }
        }

        void ISelectionItemProvider.RemoveFromSelection()
        {
            if (this.CanSelectMultiple && this.Owner.SelectedItems.Contains(this.Item))
            {
                this.Owner.SelectedItems.Remove(this.Item);
            }
            else if (this.Item == this.Owner.SelectedItem)
            {
                this.Owner.SelectedItem = null;
            }
        }

        void ISelectionItemProvider.Select()
        {
            if (this.CanSelectMultiple)
            {
                this.Owner.SelectedItems.Clear();
                this.Owner.SelectedItems.Add(this.Item);
            }
            this.Owner.SelectedItem = this.Item;
        }

        private bool CanSelectMultiple =>
            this.Owner.EditStrategy.StyleSettings.GetSelectionMode(this.Owner) != SelectionMode.Single;

        bool ISelectionItemProvider.IsSelected =>
            !this.CanSelectMultiple ? (this.Owner.SelectedItem == this.Item) : this.Owner.SelectedItems.Contains(this.Item);

        IRawElementProviderSimple ISelectionItemProvider.SelectionContainer =>
            base.ProviderFromPeer(UIElementAutomationPeer.CreatePeerForElement(this.Owner));
    }
}

