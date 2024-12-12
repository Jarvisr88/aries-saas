namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class DateTimePickerItemTemplateSelector : DataTemplateSelector
    {
        private readonly IDictionary<DateTimePart, DataTemplate> cache = new Dictionary<DateTimePart, DataTemplate>();
        private const string DateTimePickerKey = "ItemTemplate";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template;
            DateTimePickerSelector selector = container as DateTimePickerSelector;
            DateTimePickerData data = item as DateTimePickerData;
            if ((selector == null) || (data == null))
            {
                return base.SelectTemplate(item, container);
            }
            if (!this.cache.TryGetValue(data.DateTimePart, out template))
            {
                string str = data.DateTimePart + "ItemTemplate";
                DateTimePickerThemeKeyExtension resourceKey = new DateTimePickerThemeKeyExtension {
                    ResourceKey = (DateTimePickerThemeKeys) Enum.Parse(typeof(DateTimePickerThemeKeys), str)
                };
                template = (DataTemplate) selector.FindResource(resourceKey);
                this.cache.Add(data.DateTimePart, template);
            }
            return template;
        }
    }
}

