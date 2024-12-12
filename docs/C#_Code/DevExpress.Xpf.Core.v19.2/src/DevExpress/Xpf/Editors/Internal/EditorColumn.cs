namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class EditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        private ColumnContentChangedEventHandler contentChanged;

        event ColumnContentChangedEventHandler IInplaceEditorColumn.ContentChanged
        {
            add
            {
                this.contentChanged += value;
            }
            remove
            {
                this.contentChanged -= value;
            }
        }

        public EditorColumn(TokenItemData owner)
        {
            this.OwnerItem = owner;
        }

        internal void RaiseContentChanged()
        {
            if (this.contentChanged != null)
            {
                this.contentChanged(this, new ColumnContentChangedEventArgs(null));
            }
        }

        private TokenItemData OwnerItem { get; set; }

        BaseEditSettings IInplaceEditorColumn.EditSettings =>
            this.OwnerItem.Settings;

        DataTemplateSelector IInplaceEditorColumn.EditorTemplateSelector =>
            null;

        ControlTemplate IInplaceEditorColumn.EditTemplate =>
            null;

        ControlTemplate IInplaceEditorColumn.DisplayTemplate =>
            null;

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            HorizontalAlignment.Stretch;

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;
    }
}

