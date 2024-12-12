namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Themes;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DialogServiceUITypeEditorSelector : DataTemplateSelector
    {
        private readonly DataTemplateSelector innerSelector;

        public DialogServiceUITypeEditorSelector(DataTemplateSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (this.innerSelector != null)
            {
                UITypeEditorValue value2 = item as UITypeEditorValue;
                return ((value2 == null) ? this.innerSelector.SelectTemplate(value2, container) : this.innerSelector.SelectTemplate(value2.Content, container));
            }
            PopupBaseEditThemeKeyExtension key = new PopupBaseEditThemeKeyExtension();
            key.ResourceKey = PopupBaseEditThemeKeys.DialogServiceContentTemplate;
            key.ThemeName = ThemeHelper.GetEditorThemeName(container);
            return (DataTemplate) ((FrameworkElement) container).FindResource(key);
        }
    }
}

