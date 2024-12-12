namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class MaximizableHeaderedContentControlBase : HeaderedContentControlBase
    {
        public static readonly DependencyProperty CurrentContentTemplateProperty = DependencyProperty.Register("CurrentContentTemplate", typeof(DataTemplate), typeof(MaximizableHeaderedContentControlBase), null);
        public static readonly DependencyProperty MaximizedContentTemplateProperty;
        private bool _IsMaximizedCore;

        static MaximizableHeaderedContentControlBase()
        {
            MaximizedContentTemplateProperty = DependencyProperty.Register("MaximizedContentTemplate", typeof(DataTemplate), typeof(MaximizableHeaderedContentControlBase), new PropertyMetadata((o, e) => ((MaximizableHeaderedContentControlBase) o).OnMaximizedContentTemplateChanged()));
        }

        public MaximizableHeaderedContentControlBase()
        {
            this.UpdateCurrentContentTemplate();
        }

        protected virtual DependencyProperty GetCurrentContentTemplateProperty() => 
            (!this.IsMaximizedCore || (this.MaximizedContentTemplate == null)) ? ContentControlBase.ContentTemplateProperty : MaximizedContentTemplateProperty;

        protected virtual void OnIsMaximizedCoreChanged()
        {
            this.UpdateCurrentContentTemplate();
        }

        protected virtual void OnMaximizedContentTemplateChanged()
        {
            this.UpdateCurrentContentTemplate();
        }

        protected void UpdateCurrentContentTemplate()
        {
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath(this.GetCurrentContentTemplateProperty());
            base.SetBinding(CurrentContentTemplateProperty, binding);
        }

        public DataTemplate CurrentContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CurrentContentTemplateProperty);
            private set => 
                base.SetValue(CurrentContentTemplateProperty, value);
        }

        public DataTemplate MaximizedContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MaximizedContentTemplateProperty);
            set => 
                base.SetValue(MaximizedContentTemplateProperty, value);
        }

        protected bool IsMaximizedCore
        {
            get => 
                this._IsMaximizedCore;
            set
            {
                if (this.IsMaximizedCore != value)
                {
                    this._IsMaximizedCore = value;
                    this.OnIsMaximizedCoreChanged();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MaximizableHeaderedContentControlBase.<>c <>9 = new MaximizableHeaderedContentControlBase.<>c();

            internal void <.cctor>b__17_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((MaximizableHeaderedContentControlBase) o).OnMaximizedContentTemplateChanged();
            }
        }
    }
}

