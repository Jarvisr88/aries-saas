namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class SelectionProvider : ISelectionProvider
    {
        public SelectionProvider(ISelectorEditInnerListBox listBox)
        {
            this.ListBox = listBox;
        }

        public void SelectAll()
        {
            this.ListBox.SelectAll();
        }

        public void SetSelectAll(bool? value)
        {
        }

        public void UnselectAll()
        {
            this.ListBox.UnselectAll();
        }

        private ISelectorEditInnerListBox ListBox { get; set; }
    }
}

