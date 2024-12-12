namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ThemedWindowToolbarItemsControl : ThemedWindowHeaderItemsControlBase
    {
        protected override void SetHeaderItemsProperty()
        {
            if (GetAllowHeaderItems(this))
            {
                base.SetHeaderItemsProperty();
                Binding binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItemContainerStyle", new object[0])
                };
                base.SetBinding(ItemsControl.ItemContainerStyleProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItemContainerStyleSelector", new object[0])
                };
                base.SetBinding(ItemsControl.ItemContainerStyleSelectorProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItemTemplate", new object[0])
                };
                base.SetBinding(ItemsControl.ItemTemplateProperty, binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItemTemplateSelector", new object[0])
                };
                base.SetBinding(ItemsControl.ItemTemplateSelectorProperty, binding2);
                PriorityBinding binding = new PriorityBinding();
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItemsSource", new object[0]),
                    Converter = new PriorityBindingConverter()
                };
                binding.Bindings.Add(binding2);
                binding2 = new Binding {
                    Source = base.Window,
                    Path = new PropertyPath("ToolbarItems", new object[0])
                };
                binding.Bindings.Add(binding2);
                base.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            }
        }
    }
}

