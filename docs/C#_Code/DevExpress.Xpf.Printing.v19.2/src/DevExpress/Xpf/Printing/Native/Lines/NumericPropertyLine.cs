namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;
    using System.Windows;

    internal class NumericPropertyLine : EditorPropertyLine
    {
        public NumericPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) : base(new SpinEdit(), converter, property, obj)
        {
            this.Editor.IsFloatValue = PSNativeMethods.IsFloatType(property.PropertyType);
            this.Editor.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
            this.Editor.Validate += new ValidateEventHandler(this.ValidateEditor);
            this.Editor.LostFocus += new RoutedEventHandler(this.EditorLostFocus);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.Editor.Validate -= new ValidateEventHandler(this.ValidateEditor);
                this.Editor.LostFocus -= new RoutedEventHandler(this.EditorLostFocus);
            }
        }

        private void EditorLostFocus(object sender, RoutedEventArgs e)
        {
            base.UpdateValue(this.Editor.Text);
        }

        protected override void SetEditText(object value)
        {
            this.Editor.EditValue = value;
        }

        private SpinEdit Editor =>
            (SpinEdit) base.editor;
    }
}

