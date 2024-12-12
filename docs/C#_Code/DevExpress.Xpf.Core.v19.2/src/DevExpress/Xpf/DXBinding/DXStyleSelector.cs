namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class DXStyleSelector : StyleSelector
    {
        private Style style;
        private FrameworkElement element;

        public DXStyleSelector()
        {
            this.Items = new DXStyleTriggerCollection();
        }

        public sealed override Style SelectStyle(object item, DependencyObject container)
        {
            Style tag;
            if (!DesignerProperties.GetIsInDesignMode(this.Items))
            {
                this.Items.Freeze();
            }
            if (this.style == null)
            {
                Func<DXStyleTrigger, object> getValue = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<DXStyleTrigger, object> local1 = <>c.<>9__7_0;
                    getValue = <>c.<>9__7_0 = e => e.Style;
                }
                this.style = DXTriggerHelper.CreateStyle<DXStyleTrigger>(this.Items, FrameworkElement.TagProperty, getValue, null);
                this.element = new FrameworkElement();
                this.element.DataContext = item;
                this.element.Style = this.style;
            }
            this.element.DataContext = item;
            try
            {
                tag = (Style) this.element.Tag;
            }
            finally
            {
                this.element.DataContext = null;
            }
            return tag;
        }

        public DXStyleTriggerCollection Items { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXStyleSelector.<>c <>9 = new DXStyleSelector.<>c();
            public static Func<DXStyleTrigger, object> <>9__7_0;

            internal object <SelectStyle>b__7_0(DXStyleTrigger e) => 
                e.Style;
        }
    }
}

