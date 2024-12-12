namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CheckInplaceEditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        event ColumnContentChangedEventHandler IInplaceEditorColumn.ContentChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public CheckInplaceEditorColumn(CheckEditingField editingField)
        {
            this.EditingField = editingField;
            this.EditTemplate = new ControlTemplate();
            this.EditTemplate.VisualTree = new FrameworkElementFactory(typeof(Grid));
        }

        public CheckEditingField EditingField { get; private set; }

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            HorizontalAlignment.Left;

        ControlTemplate IInplaceEditorColumn.DisplayTemplate =>
            null;

        DataTemplateSelector IInplaceEditorColumn.EditorTemplateSelector =>
            null;

        BaseEditSettings IInplaceEditorColumn.EditSettings =>
            null;

        public ControlTemplate EditTemplate { get; private set; }

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;
    }
}

