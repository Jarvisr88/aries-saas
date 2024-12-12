namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;

    public abstract class SelectorVisualClientOwner : VisualClientOwner
    {
        protected SelectorVisualClientOwner(PopupBaseEdit editor) : base(editor)
        {
        }

        public abstract object GetSelectedItem();
        public abstract IEnumerable GetSelectedItems();
    }
}

