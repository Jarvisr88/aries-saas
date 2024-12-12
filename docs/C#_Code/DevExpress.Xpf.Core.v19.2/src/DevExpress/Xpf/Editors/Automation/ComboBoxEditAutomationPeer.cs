namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public class ComboBoxEditAutomationPeer : PopupBaseEditAutomationPeer, ISelectionProvider
    {
        public ComboBoxEditAutomationPeer(ComboBoxEdit element) : base(element)
        {
        }

        protected virtual ComboBoxEditItemAutomationPeer CreateItemAutomationPeer(object item) => 
            new ComboBoxEditItemAutomationPeer(item, this.Editor);

        protected override AutomationPeer CreatePopupRootPeer() => 
            null;

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.ComboBox;

        protected override List<AutomationPeer> GetChildrenCore() => 
            this.GetItemPeers(base.GetChildrenCore());

        private List<AutomationPeer> GetItemPeers() => 
            this.GetItemPeers(null);

        private List<AutomationPeer> GetItemPeers(List<AutomationPeer> baseChildren)
        {
            ClearAutomationEventsHelper.ClearAutomationEvents();
            List<AutomationPeer> list = new List<AutomationPeer>();
            if (baseChildren != null)
            {
                list = new List<AutomationPeer>(baseChildren);
            }
            if (this.Editor.IsPopupOpen)
            {
                if (this.Editor.EditStrategy.IsAsyncServerMode)
                {
                    return list;
                }
                foreach (object obj2 in this.Editor.ItemsProvider.VisibleListSource)
                {
                    list.Add(this.CreateItemAutomationPeer(obj2));
                }
            }
            return list;
        }

        public override object GetPattern(PatternInterface pattern) => 
            (pattern != PatternInterface.Selection) ? base.GetPattern(pattern) : this;

        protected override string GetValueCore() => 
            this.Editor.Text;

        protected override void SetValueCore(string value)
        {
            this.Editor.SetCurrentValue(TextEditBase.TextProperty, value);
        }

        IRawElementProviderSimple[] ISelectionProvider.GetSelection()
        {
            List<AutomationPeer> itemPeers = this.GetItemPeers();
            if ((this.Editor.SelectedItems.Count <= 0) || (itemPeers.Count <= 0))
            {
                return null;
            }
            List<IRawElementProviderSimple> list2 = new List<IRawElementProviderSimple>();
            foreach (ComboBoxEditItemAutomationPeer peer in itemPeers)
            {
                if (peer != null)
                {
                    list2.Add(base.ProviderFromPeer(peer));
                }
            }
            return list2.ToArray();
        }

        protected ComboBoxEdit Editor =>
            base.Editor as ComboBoxEdit;

        bool ISelectionProvider.CanSelectMultiple =>
            this.Editor.EditStrategy.StyleSettings.GetSelectionMode(this.Editor) != SelectionMode.Single;

        bool ISelectionProvider.IsSelectionRequired =>
            false;
    }
}

