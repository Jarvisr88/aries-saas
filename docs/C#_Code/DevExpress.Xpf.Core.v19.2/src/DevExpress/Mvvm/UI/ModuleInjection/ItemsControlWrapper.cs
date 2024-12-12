namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ItemsControlWrapper : IItemsControlWrapper<ItemsControl>, ITargetWrapper<ItemsControl>
    {
        public ItemsControl Target { get; set; }

        public object ItemsSource
        {
            get => 
                this.Target.ItemsSource;
            set => 
                this.Target.ItemsSource = (IEnumerable) value;
        }

        public virtual DataTemplate ItemTemplate
        {
            get => 
                this.Target.ItemTemplate;
            set => 
                this.Target.ItemTemplate = value;
        }

        public virtual DataTemplateSelector ItemTemplateSelector
        {
            get => 
                this.Target.ItemTemplateSelector;
            set => 
                this.Target.ItemTemplateSelector = value;
        }
    }
}

