namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;
    using System.Windows;

    internal class ComboBoxPropertyLine : EditorPropertyLine
    {
        private ComboBoxItemWrapper[] items;

        public ComboBoxPropertyLine(IStringConverter converter, object[] values, PropertyDescriptor property, object obj) : base(new ComboBoxEdit(), converter, property, obj)
        {
            if (values == null)
            {
                this.Editor.ShowEditorButtons = false;
                this.Editor.Validate += new ValidateEventHandler(this.ValidateEditor);
                this.Editor.LostFocus += (o, e) => base.UpdateValue(this.Editor.Text);
            }
            else
            {
                this.items = new ComboBoxItemWrapper[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    this.items[i] = new ComboBoxItemWrapper(values[i], converter);
                }
                this.Editor.DisplayMember = "Text";
                this.Editor.ShowEditorButtons = true;
                this.Editor.IsTextEditable = false;
                this.Editor.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
                this.Editor.SelectedIndexChanged += new RoutedEventHandler(this.Editor_SelectedIndexChanged);
                this.FillEditor(this.items);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Editor.SelectedIndexChanged -= new RoutedEventHandler(this.Editor_SelectedIndexChanged);
                this.Editor.Validate -= new ValidateEventHandler(this.ValidateEditor);
                base.editor.LostFocus -= (o, e) => base.UpdateValue(this.Editor.Text);
            }
        }

        private void Editor_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            base.Value = this.items[this.Editor.SelectedIndex].Value;
        }

        private void FillEditor(ComboBoxItemWrapper[] values)
        {
            foreach (ComboBoxItemWrapper wrapper in values)
            {
                this.Editor.Items.Add(wrapper);
                if ((wrapper != null) && wrapper.Value.Equals(base.Value))
                {
                    this.Editor.SelectedItem = wrapper;
                }
            }
        }

        protected override void SetEditText(object value)
        {
            if (this.items == null)
            {
                this.Editor.Text = base.ValueToString(value);
            }
            else
            {
                this.Editor.EditValue = new ComboBoxItemWrapper(value, base.converter);
            }
        }

        internal ComboBoxEdit Editor =>
            (ComboBoxEdit) base.editor;

        public bool IsDropDownMode =>
            this.items != null;
    }
}

