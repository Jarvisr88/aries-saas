namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ActualTemplateSelectorWrapper : DataTemplateSelector
    {
        private DataTemplateSelector selector;
        private DataTemplate template;

        public ActualTemplateSelectorWrapper(DataTemplateSelector selector, DataTemplate template);
        public static ActualTemplateSelectorWrapper Combine(ActualTemplateSelectorWrapper source, DataTemplateSelector selector, DataTemplate template);
        public override DataTemplate SelectTemplate(object item, DependencyObject container);

        public DataTemplate Template { get; }

        public DataTemplateSelector Selector { get; }
    }
}

