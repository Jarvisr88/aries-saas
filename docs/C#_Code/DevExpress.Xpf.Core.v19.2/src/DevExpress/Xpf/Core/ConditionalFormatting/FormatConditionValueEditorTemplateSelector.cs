namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class FormatConditionValueEditorTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            ((IConditionalFormattingDialogViewModel) item).With<IConditionalFormattingDialogViewModel, DataTemplate>(x => (DataTemplate) ((FrameworkElement) container).FindResource(x.ConditionValueType + "ValueEditor"));
    }
}

