namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class EditingFieldTemplateSelector : DataTemplateSelector
    {
        private const string ImageEdit = "BrickImageEdit";
        private readonly FrameworkElement frameworkElement = new FrameworkElement();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            EditingField field = Guard.ArgumentMatchType<EditingField>(item, "item");
            if (field is TextEditingField)
            {
                EditingFieldThemeKeyExtension extension1 = new EditingFieldThemeKeyExtension();
                extension1.ResourceKey = ((TextEditingField) field).EditorName;
                return (DataTemplate) this.frameworkElement.TryFindResource(extension1);
            }
            if (!(field is ImageEditingField))
            {
                return null;
            }
            EditingFieldThemeKeyExtension resourceKey = new EditingFieldThemeKeyExtension();
            resourceKey.ResourceKey = "BrickImageEdit";
            return (DataTemplate) this.frameworkElement.TryFindResource(resourceKey);
        }
    }
}

