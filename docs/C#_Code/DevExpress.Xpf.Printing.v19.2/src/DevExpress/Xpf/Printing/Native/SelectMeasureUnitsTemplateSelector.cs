namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SelectMeasureUnitsTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            ((IPageSetupViewModel) item).EnableUnitsEditor ? this.EnableUnitsEditorTemplate : this.DisableUnitsEditorTemplate;

        public DataTemplate EnableUnitsEditorTemplate { get; set; }

        public DataTemplate DisableUnitsEditorTemplate { get; set; }
    }
}

