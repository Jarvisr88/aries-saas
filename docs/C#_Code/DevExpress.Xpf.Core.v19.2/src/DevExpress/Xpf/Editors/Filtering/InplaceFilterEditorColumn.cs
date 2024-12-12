namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class InplaceFilterEditorColumn : IInplaceEditorColumn, IDefaultEditorViewInfo
    {
        private readonly BaseEditSettings settings;
        private readonly ControlTemplate emptyValueDisplayTemplate;
        private readonly ControlTemplate valueTemplate;
        private readonly ControlTemplate stringEmptyDisplayTemplate;
        private readonly ControlTemplate booleanValueTemplate;
        private DisplayTextIsEmptyDelegate DisplayTextIsEmpty;

        event ColumnContentChangedEventHandler IInplaceEditorColumn.ContentChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public InplaceFilterEditorColumn(BaseEditSettings settings, ControlTemplate emptyValueDisplayTemplate, ControlTemplate stringEmptyDisplayTemplate, ControlTemplate valueTemplate, ControlTemplate booleanValueTemplate, EditableDataObject data)
        {
            this.settings = settings;
            this.emptyValueDisplayTemplate = emptyValueDisplayTemplate;
            this.valueTemplate = valueTemplate;
            this.stringEmptyDisplayTemplate = stringEmptyDisplayTemplate;
            this.booleanValueTemplate = booleanValueTemplate;
            this.Data = data;
        }

        private bool IsBooleanEditSettings() => 
            (this.settings is CheckEditSettings) || (this.settings is ToggleSwitchEditSettings);

        internal void SetDisplayTextIsEmpty(DisplayTextIsEmptyDelegate DisplayTextIsEmpty)
        {
            this.DisplayTextIsEmpty = DisplayTextIsEmpty;
        }

        public EditableDataObject Data { get; private set; }

        BaseEditSettings IInplaceEditorColumn.EditSettings =>
            this.settings;

        DataTemplateSelector IInplaceEditorColumn.EditorTemplateSelector =>
            null;

        ControlTemplate IInplaceEditorColumn.EditTemplate =>
            null;

        ControlTemplate IInplaceEditorColumn.DisplayTemplate =>
            (this.Data.Value != null) ? (!this.IsBooleanEditSettings() ? ((((this.Data.Value as string) == string.Empty) || this.DisplayTextIsEmpty()) ? this.stringEmptyDisplayTemplate : this.valueTemplate) : this.booleanValueTemplate) : this.emptyValueDisplayTemplate;

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            HorizontalAlignment.Left;

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;
    }
}

