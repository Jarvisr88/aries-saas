namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ZoomComboBoxTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is ZoomSeparatorItem))
            {
                return (((item is ZoomValueItem) || (item is ZoomFitModeItem)) ? this.ZoomModeItemTemplate : null);
            }
            ((ComboBoxEditItem) ((ContentPresenter) container).TemplatedParent).IsEnabled = false;
            return this.SeparatorItemTemplate;
        }

        public DataTemplate ZoomModeItemTemplate { get; set; }

        public DataTemplate SeparatorItemTemplate { get; set; }
    }
}

