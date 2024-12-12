namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FormatEditorOwnerTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            !(item is ContainViewModel) ? (!(item is AboveBelowViewModel) ? (!(item is TopBottomViewModel) ? (!(item is UniqueDuplicateViewModel) ? (!(item is FormulaViewModel) ? (!(item is AnimationRuleViewModel) ? null : this.AnimationTemplate) : this.FormulaTemplate) : this.UniqueDuplicateTemplate) : this.TopBottomTemplate) : this.AboveBelowTemplate) : this.ContainTemplate;

        public DataTemplate ContainTemplate { get; set; }

        public DataTemplate TopBottomTemplate { get; set; }

        public DataTemplate AboveBelowTemplate { get; set; }

        public DataTemplate FormulaTemplate { get; set; }

        public DataTemplate UniqueDuplicateTemplate { get; set; }

        public DataTemplate AnimationTemplate { get; set; }
    }
}

