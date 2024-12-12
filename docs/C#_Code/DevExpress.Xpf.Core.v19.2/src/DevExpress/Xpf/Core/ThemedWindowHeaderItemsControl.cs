namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ThemedWindowHeaderItemsControl : ThemedWindowHeaderItemsControlBase
    {
        protected override void SetHeaderItemsProperty()
        {
            if (GetAllowHeaderItems(this))
            {
                base.SetHeaderItemsProperty();
                Binding binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItemContainerStyle", new object[0])
                };
                base.SetBinding(ItemsControl.ItemContainerStyleProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItemContainerStyleSelector", new object[0])
                };
                base.SetBinding(ItemsControl.ItemContainerStyleSelectorProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItemTemplate", new object[0])
                };
                base.SetBinding(ItemsControl.ItemTemplateProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItemTemplateSelector", new object[0])
                };
                base.SetBinding(ItemsControl.ItemTemplateSelectorProperty, binding2);
                PriorityBinding binding = new PriorityBinding();
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItemsSource", new object[0]),
                    Converter = new PriorityBindingConverter()
                };
                binding.Bindings.Add(binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("HeaderItems", new object[0])
                };
                binding.Bindings.Add(binding2);
                base.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            }
        }
    }
}

