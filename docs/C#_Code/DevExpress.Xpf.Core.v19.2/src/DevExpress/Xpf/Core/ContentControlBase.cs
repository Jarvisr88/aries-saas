namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Content"), DXToolboxBrowsable(false)]
    public class ContentControlBase : ControlBase
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;

        static ContentControlBase()
        {
            ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ContentControlBase), new PropertyMetadata((o, e) => ((ContentControlBase) o).OnContentChanged(e.OldValue, e.NewValue)));
            ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(ContentControlBase), null);
        }

        protected virtual void OnContentChanged(object oldValue, object newValue)
        {
            if (this.IsContentInLogicalTree)
            {
                base.RemoveLogicalChild(oldValue);
                base.AddLogicalChild(newValue);
            }
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        protected virtual bool IsContentInLogicalTree =>
            true;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (!this.IsContentInLogicalTree || (this.Content == null))
                {
                    return base.LogicalChildren;
                }
                object[] objArray1 = new object[] { this.Content };
                return objArray1.GetEnumerator();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentControlBase.<>c <>9 = new ContentControlBase.<>c();

            internal void <.cctor>b__14_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ContentControlBase) o).OnContentChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

