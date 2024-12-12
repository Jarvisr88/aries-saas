namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class DXDataTemplateSelector : DataTemplateSelector
    {
        private Style style;

        public DXDataTemplateSelector()
        {
            this.Items = new DXDataTemplateTriggerCollection();
        }

        public sealed override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate tag;
            if (!DesignerProperties.GetIsInDesignMode(this.Items))
            {
                this.Items.Freeze();
            }
            if (this.style == null)
            {
                Func<DXDataTemplateTrigger, object> getValue = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<DXDataTemplateTrigger, object> local1 = <>c.<>9__6_0;
                    getValue = <>c.<>9__6_0 = e => e.Template;
                }
                this.style = DXTriggerHelper.CreateStyle<DXDataTemplateTrigger>(this.Items, FrameworkElement.TagProperty, getValue, null);
            }
            FrameworkElement element = new FrameworkElement {
                DataContext = item,
                Style = this.style
            };
            try
            {
                tag = (DataTemplate) element.Tag;
            }
            finally
            {
                element.DataContext = null;
            }
            return tag;
        }

        public DXDataTemplateTriggerCollection Items { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXDataTemplateSelector.<>c <>9 = new DXDataTemplateSelector.<>c();
            public static Func<DXDataTemplateTrigger, object> <>9__6_0;

            internal object <SelectTemplate>b__6_0(DXDataTemplateTrigger e) => 
                e.Template;
        }
    }
}

