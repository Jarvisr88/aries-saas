namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class HeaderedContentControlBase : ContentControlBase
    {
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty HeaderTemplateProperty;

        static HeaderedContentControlBase()
        {
            HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(HeaderedContentControlBase), new PropertyMetadata((o, e) => ((HeaderedContentControlBase) o).OnHeaderChanged(e.OldValue, e.NewValue)));
            HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(HeaderedContentControlBase), null);
        }

        protected virtual void OnHeaderChanged(object oldValue, object newValue)
        {
            if (this.IsHeaderInLogicalTree)
            {
                base.RemoveLogicalChild(oldValue);
                base.AddLogicalChild(newValue);
            }
        }

        public object Header
        {
            get => 
                base.GetValue(HeaderProperty);
            set => 
                base.SetValue(HeaderProperty, value);
        }

        public DataTemplate HeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderTemplateProperty);
            set => 
                base.SetValue(HeaderTemplateProperty, value);
        }

        protected virtual bool IsHeaderInLogicalTree =>
            true;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (!this.IsHeaderInLogicalTree || (this.Header == null))
                {
                    return base.LogicalChildren;
                }
                List<object> list = new List<object>();
                IEnumerator logicalChildren = base.LogicalChildren;
                if (logicalChildren != null)
                {
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                list.Add(this.Header);
                return list.GetEnumerator();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HeaderedContentControlBase.<>c <>9 = new HeaderedContentControlBase.<>c();

            internal void <.cctor>b__14_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((HeaderedContentControlBase) o).OnHeaderChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

