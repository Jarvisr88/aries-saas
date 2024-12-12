namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class AdditionalOperandsItemsControl : ItemsControl
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            Binding binding = new Binding("DataContext");
            binding.Source = this;
            BindingOperations.SetBinding(element, FrameworkElement.TagProperty, binding);
        }
    }
}

