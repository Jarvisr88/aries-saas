namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Markup;

    public class EditValueChangedEventArgsSelectedItemConverter : MarkupExtension, IEventArgsConverter
    {
        public object Convert(object sender, object value)
        {
            EditValueChangedEventArgs args = value as EditValueChangedEventArgs;
            return args?.NewValue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

