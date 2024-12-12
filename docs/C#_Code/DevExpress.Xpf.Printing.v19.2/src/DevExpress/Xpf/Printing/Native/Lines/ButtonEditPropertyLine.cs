namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class ButtonEditPropertyLine : EditorPropertyLine
    {
        private readonly IDXTypeEditor valueEditor;

        public ButtonEditPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) : base(new ButtonEdit(), converter, property, obj)
        {
            this.Editor.IsTextEditable = false;
            this.Editor.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
            this.Editor.DefaultButtonClick += new RoutedEventHandler(this.Editor_DefaultButtonClick);
            this.valueEditor = DXTypeEditorHelper.GetEditor(base.Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Editor.DefaultButtonClick -= new RoutedEventHandler(this.Editor_DefaultButtonClick);
            }
        }

        private void Editor_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            this.OnButtonClicked();
        }

        internal void OnButtonClicked()
        {
            this.valueEditor.Edit(base.Value, this.OwnerWindow);
            this.RefreshContent();
        }

        public override void RefreshContent()
        {
            base.RefreshContent();
            this.Content.IsEnabled = this.Header.IsEnabled = this.valueEditor != null;
        }

        protected override void SetEditText(object value)
        {
            if (base.converter != null)
            {
                this.Editor.Text = base.ValueToString(value);
            }
        }

        internal ButtonEdit Editor =>
            (ButtonEdit) base.editor;

        public Window OwnerWindow { get; set; }
    }
}

