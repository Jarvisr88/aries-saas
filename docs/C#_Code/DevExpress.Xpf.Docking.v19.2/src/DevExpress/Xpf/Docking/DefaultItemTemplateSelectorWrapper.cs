namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class DefaultItemTemplateSelectorWrapper : DataTemplateSelector
    {
        private DataTemplateSelector selector;
        private DataTemplate template;

        public DefaultItemTemplateSelectorWrapper(DataTemplateSelector selector, DataTemplate template)
        {
            this.selector = selector;
            this.template = template;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            (this.selector != null) ? (((this.template == null) || !(this.selector is DefaultItemTemplateSelector)) ? (this.selector.SelectTemplate(item, container) ?? this.template) : this.template) : this.template;

        internal abstract class DefaultItemTemplateSelector : DataTemplateSelector
        {
            protected DefaultItemTemplateSelector()
            {
            }
        }
    }
}

