namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FlowLayoutControlWrapper : IItemsControlWrapper<FlowLayoutControl>, ITargetWrapper<FlowLayoutControl>
    {
        public FlowLayoutControl Target { get; set; }

        public object ItemsSource
        {
            get => 
                this.Target.ItemsSource;
            set => 
                this.Target.ItemsSource = (IEnumerable) value;
        }

        public DataTemplate ItemTemplate
        {
            get => 
                this.Target.ItemTemplate;
            set => 
                this.Target.ItemTemplate = value;
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                this.Target.ItemTemplateSelector;
            set => 
                this.Target.ItemTemplateSelector = value;
        }
    }
}

