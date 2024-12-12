namespace DevExpress.Mvvm.UI
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DataTemplateByTypeNameSelector : DataTemplateSelector
    {
        public DataTemplateByTypeNameSelector()
        {
            this.Templates = new DataTemplateDictionary();
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template;
            if (item == null)
            {
                return (this.Templates.TryGetValue("NULL", out template) ? template : null);
            }
            for (Type type = item.GetType(); type != null; type = type.BaseType)
            {
                if (this.Templates.TryGetValue(type.Name, out template))
                {
                    return template;
                }
            }
            return null;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataTemplateDictionary Templates { get; set; }
    }
}

