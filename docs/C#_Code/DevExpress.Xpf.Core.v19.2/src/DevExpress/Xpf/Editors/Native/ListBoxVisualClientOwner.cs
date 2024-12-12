namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class ListBoxVisualClientOwner : SelectorVisualClientOwner
    {
        public ListBoxVisualClientOwner(PopupBaseEdit editor) : base(editor)
        {
        }

        protected override void ClearInnerEditor(FrameworkElement editor)
        {
            base.ClearInnerEditor(editor);
            PopupListBox box = editor as PopupListBox;
            if ((box != null) && this.IsServerMode)
            {
                box.ItemsSource = null;
            }
        }

        protected override FrameworkElement FindEditor() => 
            (LookUpEditHelper.GetPopupContentOwner(this.Editor).Child != null) ? LayoutHelper.FindElementByName(LookUpEditHelper.GetPopupContentOwner(this.Editor).Child, "PART_Content") : null;

        public override object GetSelectedItem() => 
            this.IsLoaded ? this.GetSelectedRowKey(this.ListBox.SelectedItem) : null;

        public override IEnumerable GetSelectedItems()
        {
            if (!this.IsLoaded)
            {
                return new List<object>();
            }
            IEnumerable<object> selectedItems = this.ListBox.GetSelectedItems();
            return ((selectedItems != null) ? selectedItems.Select<object, object>(new Func<object, object>(this.GetSelectedRowKey)) : new List<object>());
        }

        protected virtual object GetSelectedRowKey(object item)
        {
            if (!this.IsServerMode)
            {
                return item;
            }
            Func<DataProxy, object> evaluator = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<DataProxy, object> local1 = <>c.<>9__17_0;
                evaluator = <>c.<>9__17_0 = x => x.f_RowKey;
            }
            return ((DataProxy) item).With<DataProxy, object>(evaluator);
        }

        public override void PopupOpened()
        {
            base.PopupOpened();
            PopupListBox listBox = this.ListBox;
            if (listBox == null)
            {
                PopupListBox local1 = listBox;
            }
            else
            {
                listBox.InvalidateMeasure();
            }
        }

        protected override bool ProcessKeyDownInternal(KeyEventArgs e) => 
            true;

        protected override bool ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.ListBox.ProcessDownKey(e);
            }
            return true;
        }

        protected override void SetupEditor()
        {
            if (this.IsLoaded)
            {
                this.ListBox.SetupEditor();
            }
        }

        public override void SyncProperties(bool syncDataSource)
        {
            if (this.IsLoaded)
            {
                this.ListBox.SyncWithOwnerEdit(syncDataSource);
            }
        }

        public override void SyncValues(bool resetTotals = false)
        {
            if (this.IsLoaded)
            {
                this.ListBox.SyncValuesWithOwnerEdit(resetTotals);
            }
        }

        private LookUpEditBasePropertyProvider PropertyProvider =>
            (LookUpEditBasePropertyProvider) this.Editor.PropertyProvider;

        private bool IsServerMode =>
            this.PropertyProvider.IsServerMode;

        protected override bool IsLoaded =>
            base.IsLoaded && (this.ListBox != null);

        private PopupListBox ListBox =>
            base.InnerEditor as PopupListBox;

        private ComboBoxEdit Editor =>
            base.Editor as ComboBoxEdit;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxVisualClientOwner.<>c <>9 = new ListBoxVisualClientOwner.<>c();
            public static Func<DataProxy, object> <>9__17_0;

            internal object <GetSelectedRowKey>b__17_0(DataProxy x) => 
                x.f_RowKey;
        }
    }
}

