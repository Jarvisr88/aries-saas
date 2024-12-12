namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    internal class EditorPropertyLineBase : PropertyLine
    {
        private Label label;
        protected BaseEdit editor;

        protected EditorPropertyLineBase(BaseEdit editor, PropertyDescriptor property, object obj) : base(property, obj)
        {
            Label label1 = new Label();
            label1.Padding = new Thickness(0.0);
            label1.VerticalAlignment = VerticalAlignment.Center;
            this.label = label1;
            this.editor = editor;
        }

        public override void RefreshContent()
        {
            base.RefreshContent();
            this.editor.ClearError();
        }

        public override void SetText(string text)
        {
            TextBlock block1 = new TextBlock();
            block1.Text = text;
            block1.TextTrimming = TextTrimming.CharacterEllipsis;
            this.label.Content = block1;
        }

        public override Label Header =>
            this.label;

        public override Control Content =>
            this.editor;
    }
}

