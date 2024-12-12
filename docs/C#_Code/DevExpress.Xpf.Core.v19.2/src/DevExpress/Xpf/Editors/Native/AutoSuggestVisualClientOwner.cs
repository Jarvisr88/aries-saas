namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class AutoSuggestVisualClientOwner : SelectorVisualClientOwner
    {
        public AutoSuggestVisualClientOwner(AutoSuggestEdit editor) : base(editor)
        {
        }

        protected override void ClearInnerEditor(FrameworkElement editor)
        {
            if (this.ListBox != null)
            {
                this.ListBox.ClearEditor();
                this.ListBox = null;
            }
            base.ClearInnerEditor(editor);
        }

        protected override FrameworkElement FindEditor()
        {
            if (this.Editor.PopupContentOwner == null)
            {
                return null;
            }
            FrameworkElement element = LayoutHelper.FindElementByName(this.Editor.PopupContentOwner.Child, "PART_Content");
            if (element == null)
            {
                return null;
            }
            this.ListBox = SelectorWrapper.Create(element);
            return element;
        }

        public override object GetSelectedItem() => 
            this.IsLoaded ? this.ListBox.SelectedItem : null;

        public override IEnumerable GetSelectedItems() => 
            this.IsLoaded ? this.ListBox.GetSelectedItems() : new List<object>();

        public override void PopupOpened()
        {
            base.PopupOpened();
            if (this.ListBox != null)
            {
                this.ListBox.InvalidateMeasure();
            }
        }

        protected override bool ProcessKeyDownInternal(KeyEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.ListBox.ProcessKeyDown(e);
            }
            return true;
        }

        protected override bool ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.ListBox.ProcessPreviewKeyDown(e);
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

        protected override bool IsLoaded =>
            base.IsLoaded && (this.ListBox != null);

        private AutoSuggestEdit Editor =>
            base.Editor as AutoSuggestEdit;

        private SelectorWrapper ListBox { get; set; }
    }
}

