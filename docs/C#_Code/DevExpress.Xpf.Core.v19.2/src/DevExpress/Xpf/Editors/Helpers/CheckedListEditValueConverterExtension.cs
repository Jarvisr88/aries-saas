namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Markup;

    public class CheckedListEditValueConverterExtension : MarkupExtension
    {
        private bool isCheckedEditor = true;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            CheckedListEditValueConverter converter1 = new CheckedListEditValueConverter();
            converter1.IsCheckedEditor = this.IsCheckedEditor;
            return converter1;
        }

        public bool IsCheckedEditor
        {
            get => 
                this.isCheckedEditor;
            set => 
                this.isCheckedEditor = value;
        }
    }
}

