namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections;

    public class HierarchicalListSourceHelper : SelectorEditInnerListBoxItemsSourceHelper
    {
        public HierarchicalListSourceHelper(ISelectorEditInnerListBox listBox) : base(listBox)
        {
        }

        public override void AssignItemsSource()
        {
            base.AssignItemsSource();
            base.ListBox.ItemsSource = this.HierarchicalListSource;
        }

        private IEnumerable GetHierarchicalListSource() => 
            base.ContentItemsSource;

        private IEnumerable HierarchicalListSource =>
            this.GetHierarchicalListSource();
    }
}

