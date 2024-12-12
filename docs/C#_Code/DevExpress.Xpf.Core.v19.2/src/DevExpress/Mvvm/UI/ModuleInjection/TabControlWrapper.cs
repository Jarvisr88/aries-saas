namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class TabControlWrapper : SelectorWrapper, ISelectorWrapper<TabControl>, IItemsControlWrapper<TabControl>, ITargetWrapper<TabControl>
    {
        public TabControl Target
        {
            get => 
                (TabControl) base.Target;
            set => 
                base.Target = value;
        }

        public override DataTemplate ItemTemplate
        {
            get => 
                this.Target.ContentTemplate;
            set => 
                this.Target.ContentTemplate = value;
        }

        public override DataTemplateSelector ItemTemplateSelector
        {
            get => 
                this.Target.ContentTemplateSelector;
            set => 
                this.Target.ContentTemplateSelector = value;
        }
    }
}

