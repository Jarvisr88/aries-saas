namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TestInplaceEditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        private readonly TextEditSettings settings = new TextEditSettings();
        private readonly ControlTemplate emptyValueDisplayTemplate;

        event ColumnContentChangedEventHandler IInplaceEditorColumn.ContentChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public TestInplaceEditorColumn(ControlTemplate emptyValueDisplayTemplate, EditableDataObject data)
        {
            this.emptyValueDisplayTemplate = emptyValueDisplayTemplate;
            this.Data = data;
        }

        public EditableDataObject Data { get; private set; }

        BaseEditSettings IInplaceEditorColumn.EditSettings =>
            this.settings;

        DataTemplateSelector IInplaceEditorColumn.EditorTemplateSelector =>
            null;

        ControlTemplate IInplaceEditorColumn.EditTemplate =>
            null;

        ControlTemplate IInplaceEditorColumn.DisplayTemplate =>
            ((this.Data.Value == null) || ((this.Data.Value as string) == string.Empty)) ? this.emptyValueDisplayTemplate : null;

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            HorizontalAlignment.Left;

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;
    }
}

