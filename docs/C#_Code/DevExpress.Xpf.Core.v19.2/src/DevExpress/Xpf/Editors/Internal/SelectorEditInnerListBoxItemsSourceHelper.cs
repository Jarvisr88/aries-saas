namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class SelectorEditInnerListBoxItemsSourceHelper
    {
        private IEnumerable customItemsSource;
        private IEnumerable contentItemsSource;

        public SelectorEditInnerListBoxItemsSourceHelper(ISelectorEditInnerListBox listBox)
        {
            this.ListBox = listBox;
        }

        public virtual void AssignItemsSource()
        {
            this.VerifySource();
            this.SetCustomItemsSource(this.ListBox.OwnerEdit.GetPopupContentCustomItemsSource());
            this.SetContentItemsSource(this.ListBox.OwnerEdit.GetPopupContentItemsSource() as IEnumerable);
        }

        public static SelectorEditInnerListBoxItemsSourceHelper CreateHelper(ISelectorEditInnerListBox listBox, bool useCustomItems) => 
            useCustomItems ? ((SelectorEditInnerListBoxItemsSourceHelper) new PlainListSourceHelper(listBox)) : ((SelectorEditInnerListBoxItemsSourceHelper) new HierarchicalListSourceHelper(listBox));

        public virtual bool? IsSelectAll()
        {
            if (this.ListBox.OwnerEdit.ItemsProvider.IsServerMode)
            {
                return false;
            }
            List<object> list = this.ListBox.SelectedItems.Cast<object>().Except<object>(this.CustomItemsSource.Cast<object>()).Intersect<object>(this.ContentItemsSource.Cast<object>()).ToList<object>();
            if (list.Count == 0)
            {
                return false;
            }
            return (list.Count == this.ContentItemsSource.Cast<object>().Count<object>());
        }

        public virtual void SetContentItemsSource(IEnumerable contentItemsSource)
        {
            this.contentItemsSource = contentItemsSource;
        }

        public virtual void SetCustomItemsSource(IEnumerable customItemsSource)
        {
            this.customItemsSource = customItemsSource;
        }

        protected virtual void VerifySource()
        {
        }

        protected ISelectorEditInnerListBox ListBox { get; private set; }

        public IEnumerable CustomItemsSource =>
            this.customItemsSource ?? new List<object>();

        public IEnumerable ContentItemsSource =>
            this.contentItemsSource ?? new List<object>();
    }
}

