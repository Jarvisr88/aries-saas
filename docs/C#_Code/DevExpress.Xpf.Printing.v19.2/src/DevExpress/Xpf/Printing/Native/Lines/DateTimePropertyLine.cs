namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;

    internal class DateTimePropertyLine : CustomEditorPropertyLine
    {
        public DateTimePropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) : base(CreateEditor(), "EditValue", null, null, property, obj)
        {
            this.Editor.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
            this.Editor.EditValueChanged += new EditValueChangedEventHandler(this.EditValueChanged);
        }

        private static DateEdit CreateEditor()
        {
            DateEdit edit1 = new DateEdit();
            edit1.ShowClearButton = false;
            return edit1;
        }

        private void EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                this.Editor.EditValue = ((DateTime) e.NewValue).Date;
            }
            else
            {
                this.Editor.EditValue = null;
            }
        }

        private DateEdit Editor =>
            (DateEdit) base.editor;
    }
}

