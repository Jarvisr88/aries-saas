namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    internal class BooleanLine : PropertyLine
    {
        private ComboBoxEdit editor;
        private List<bool> items;
        private Dictionary<string, bool> values;
        private Label label;

        public BooleanLine(PropertyDescriptor property, object obj) : base(property, obj)
        {
            this.editor = new ComboBoxEdit();
            Label label1 = new Label();
            label1.Padding = new Thickness(0.0);
            label1.VerticalAlignment = VerticalAlignment.Center;
            this.label = label1;
            List<bool> list1 = new List<bool>();
            list1.Add(false);
            list1.Add(true);
            this.items = list1;
            this.values = new Dictionary<string, bool>();
            this.values[PrintingLocalizer.GetString(PrintingStringId.True)] = true;
            this.values[PrintingLocalizer.GetString(PrintingStringId.False)] = false;
            this.editor.ItemsSource = this.values;
            this.editor.DisplayMember = "Key";
            this.editor.ValueMember = "Value";
            this.editor.SelectedIndexChanged += new RoutedEventHandler(this.editor_SelectedIndexChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.editor.SelectedIndexChanged -= new RoutedEventHandler(this.editor_SelectedIndexChanged);
            }
        }

        private void editor_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            base.Value = this.editor.EditValue;
        }

        public override void RefreshContent()
        {
            base.RefreshContent();
            this.editor.EditValue = Convert.ToBoolean(base.Value);
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

