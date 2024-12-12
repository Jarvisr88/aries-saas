namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class DetailControlPartContainer : Control
    {
        public static readonly DependencyProperty DetailDescriptorProperty;
        public static readonly DependencyProperty ViewProperty;
        public static readonly DependencyProperty DetailPartTemplateProperty;
        private ItemsControl nextLevelItemsControl;

        static DetailControlPartContainer()
        {
            DetailDescriptorProperty = DependencyPropertyManager.Register("DetailDescriptor", typeof(DetailDescriptorBase), typeof(DetailControlPartContainer), new PropertyMetadata(null, (d, e) => ((DetailControlPartContainer) d).OnDetailDescriptorChanged()));
            ViewProperty = DependencyPropertyManager.Register("View", typeof(DataViewBase), typeof(DetailControlPartContainer), new PropertyMetadata(null, (d, e) => ((DetailControlPartContainer) d).OnViewChanged()));
            DetailPartTemplateProperty = DependencyPropertyManager.Register("DetailPartTemplate", typeof(ControlTemplate), typeof(DetailControlPartContainer), new PropertyMetadata(null, (d, e) => ((DetailControlPartContainer) d).UpdateTemplate()));
        }

        protected DetailControlPartContainer()
        {
        }

        private void BindNextLevelItemsControl()
        {
            if ((this.nextLevelItemsControl != null) && (this.View != null))
            {
                this.View.BindDetailContainerNextLevelItemsControl(this.nextLevelItemsControl);
            }
        }

        internal DependencyObject GetTemplateChildInternal(string name) => 
            base.GetTemplateChild(name);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.nextLevelItemsControl = base.GetTemplateChild("PART_NextLevelItemsControl") as ItemsControl;
            this.BindNextLevelItemsControl();
        }

        private void OnDetailDescriptorChanged()
        {
            if (this.DetailDescriptor == null)
            {
                base.ClearValue(ViewProperty);
            }
            else
            {
                Binding binding = new Binding(DataControlDetailDescriptor.DataControlProperty.GetName() + ".View");
                binding.Source = this.DetailDescriptor;
                base.SetBinding(ViewProperty, binding);
            }
        }

        private void OnViewChanged()
        {
            DataControlBase.SetCurrentView(this, this.View);
            this.UpdateTemplate();
            this.BindNextLevelItemsControl();
        }

        private void UpdateTemplate()
        {
            this.Template = (this.View != null) ? this.DetailPartTemplate : null;
        }

        public DetailDescriptorBase DetailDescriptor
        {
            get => 
                (DetailDescriptorBase) base.GetValue(DetailDescriptorProperty);
            set => 
                base.SetValue(DetailDescriptorProperty, value);
        }

        public DataViewBase View
        {
            get => 
                (DataViewBase) base.GetValue(ViewProperty);
            set => 
                base.SetValue(ViewProperty, value);
        }

        public ControlTemplate DetailPartTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(DetailPartTemplateProperty);
            set => 
                base.SetValue(DetailPartTemplateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailControlPartContainer.<>c <>9 = new DetailControlPartContainer.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailControlPartContainer) d).OnDetailDescriptorChanged();
            }

            internal void <.cctor>b__20_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailControlPartContainer) d).OnViewChanged();
            }

            internal void <.cctor>b__20_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailControlPartContainer) d).UpdateTemplate();
            }
        }
    }
}

