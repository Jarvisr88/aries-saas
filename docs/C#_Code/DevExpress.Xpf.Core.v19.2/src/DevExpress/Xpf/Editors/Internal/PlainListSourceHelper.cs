namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class PlainListSourceHelper : SelectorEditInnerListBoxItemsSourceHelper
    {
        public PlainListSourceHelper(ISelectorEditInnerListBox listBox) : base(listBox)
        {
        }

        public override void AssignItemsSource()
        {
            base.AssignItemsSource();
            base.ListBox.ItemsSource = this.ItemsSource;
        }

        protected virtual void InitializeItemsSource()
        {
            this.ItemsSource = new EditorsCompositeCollection();
        }

        protected virtual void RestoreItemsSource()
        {
            this.ItemsSource = (EditorsCompositeCollection) base.ListBox.ItemsSource;
        }

        public override void SetContentItemsSource(IEnumerable contentItemsSource)
        {
            base.SetContentItemsSource(contentItemsSource);
            this.ItemsSource.SetContentCollection((IList) contentItemsSource);
        }

        public override void SetCustomItemsSource(IEnumerable customItemsSource)
        {
            base.SetCustomItemsSource(customItemsSource);
            this.ItemsSource.SetCustomCollection((IList) customItemsSource);
        }

        protected override void VerifySource()
        {
            if (!(base.ListBox.ItemsSource is EditorsCompositeCollection))
            {
                this.InitializeItemsSource();
            }
            else
            {
                this.RestoreItemsSource();
            }
        }

        private EditorsCompositeCollection ItemsSource { get; set; }
    }
}

