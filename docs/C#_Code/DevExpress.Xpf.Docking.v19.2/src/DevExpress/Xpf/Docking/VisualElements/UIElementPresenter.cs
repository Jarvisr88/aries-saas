namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class UIElementPresenter : psvDecorator
    {
        public static readonly DependencyProperty UIElementProperty;

        static UIElementPresenter()
        {
            new DependencyPropertyRegistrator<UIElementPresenter>().Register<System.Windows.UIElement>("UIElement", ref UIElementProperty, null, (dObj, e) => ((UIElementPresenter) dObj).OnUIElementChanged((System.Windows.UIElement) e.NewValue), null);
        }

        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent) => 
            new UIElementCollection(this, null);

        protected override void OnDispose()
        {
            base.ClearValue(psvDecorator.ChildProperty);
            base.ClearValue(FrameworkElement.DataContextProperty);
            base.OnDispose();
        }

        private void OnUIElementChanged(System.Windows.UIElement element)
        {
            if (element != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(element);
                UIElementPresenter presenter = parent as UIElementPresenter;
                if (presenter != null)
                {
                    presenter.Child = null;
                }
                DocumentSelectorPreview.DetachedElementDecorator decorator = parent as DocumentSelectorPreview.DetachedElementDecorator;
                if (decorator != null)
                {
                    decorator.Child = null;
                }
            }
            base.Child = element;
        }

        public System.Windows.UIElement UIElement
        {
            get => 
                (System.Windows.UIElement) base.GetValue(UIElementProperty);
            set => 
                base.SetValue(UIElementProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UIElementPresenter.<>c <>9 = new UIElementPresenter.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((UIElementPresenter) dObj).OnUIElementChanged((UIElement) e.NewValue);
            }
        }
    }
}

