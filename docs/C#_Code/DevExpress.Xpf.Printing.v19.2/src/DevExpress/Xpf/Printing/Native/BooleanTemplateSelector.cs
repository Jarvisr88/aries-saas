namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BooleanTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            ((bool) item) ? this.TrueValueTemplate : this.FalseValueTemplate;

        public DataTemplate FalseValueTemplate { get; set; }

        public DataTemplate TrueValueTemplate { get; set; }
    }
}

