namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ZoomSubItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            !(item is ZoomSeparatorItem) ? (((item is ZoomValueItem) || (item is ZoomFitModeItem)) ? this.ZoomModeItemTemplate : null) : this.SeparatorItemTemplate;

        public DataTemplate ZoomModeItemTemplate { get; set; }

        public DataTemplate SeparatorItemTemplate { get; set; }
    }
}

