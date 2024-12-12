namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ColumnTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            string name = null;
            foreach (Attribute attribute in ((ColumnGeneratorItemContext) item).PropertyDescriptor.Attributes)
            {
                if (attribute is ColumnGeneratorTemplateNameAttribute)
                {
                    name = ((ColumnGeneratorTemplateNameAttribute) attribute).Name;
                }
            }
            if (name == null)
            {
                return null;
            }
            Control control = container as Control;
            if (control == null)
            {
                throw new Exception();
            }
            return (control.Resources[name] as DataTemplate);
        }
    }
}

