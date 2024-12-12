namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class FloatDocument : Document
    {
        static FloatDocument()
        {
            new DependencyPropertyRegistrator<FloatDocument>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (base.PartContentPresenter != null)
            {
                base.PartContentPresenter.SizeChanged += new SizeChangedEventHandler(this.OnPartContentPresenterSizeChanged);
            }
        }

        private void OnPartContentPresenterSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.PartContentPresenter.SizeChanged -= new SizeChangedEventHandler(this.OnPartContentPresenterSizeChanged);
            if (base.InactiveLayer == null)
            {
                base.EnsureTemplateChildren();
            }
        }
    }
}

