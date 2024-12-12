namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class psvHeaderedContentControl : psvContentControl
    {
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty HeaderTemplateProperty;

        static psvHeaderedContentControl()
        {
            DependencyPropertyRegistrator<psvHeaderedContentControl> registrator = new DependencyPropertyRegistrator<psvHeaderedContentControl>();
            registrator.Register<object>("Header", ref HeaderProperty, null, (d, e) => ((psvHeaderedContentControl) d).OnHeaderChanged(e.OldValue, e.NewValue), null);
            registrator.Register<DataTemplate>("HeaderTemplate", ref HeaderTemplateProperty, null, (d, e) => ((psvHeaderedContentControl) d).OnHeaderTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue), null);
        }

        protected override void OnDispose()
        {
            base.ClearValue(HeaderProperty);
            base.ClearValue(HeaderTemplateProperty);
            base.OnDispose();
        }

        protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
        {
        }

        protected virtual void OnHeaderTemplateChanged(DataTemplate oldHeaderTemplate, DataTemplate newHeaderTemplate)
        {
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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvHeaderedContentControl.<>c <>9 = new psvHeaderedContentControl.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((psvHeaderedContentControl) d).OnHeaderChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((psvHeaderedContentControl) d).OnHeaderTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
            }
        }
    }
}

