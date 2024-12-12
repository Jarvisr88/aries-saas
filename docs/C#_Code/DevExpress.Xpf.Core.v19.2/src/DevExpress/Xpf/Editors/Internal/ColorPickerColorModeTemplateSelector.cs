namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ColorPickerColorModeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            ColorBase base2 = item as ColorBase;
            if ((element == null) || (base2 == null))
            {
                return base.SelectTemplate(item, container);
            }
            string str = base2.ColorMode + "DataTemplate";
            ColorPickerThemeKeyExtension resourceKey = new ColorPickerThemeKeyExtension {
                ResourceKey = (ColorPickerThemeKeys) Enum.Parse(typeof(ColorPickerThemeKeys), str, false)
            };
            return (DataTemplate) element.FindResource(resourceKey);
        }
    }
}

