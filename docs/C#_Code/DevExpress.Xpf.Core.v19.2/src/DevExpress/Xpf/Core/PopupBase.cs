namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;

    public class PopupBase : Popup
    {
        public static readonly DependencyProperty PopupContentProperty = DependencyPropertyManager.Register("PopupContent", typeof(object), typeof(PopupBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(PopupBase.OnPopupContentPropertyChanged)));

        static PopupBase()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBase), new FrameworkPropertyMetadata(typeof(PopupBase)));
        }

        protected virtual PopupBorderControl CreateBorderControl() => 
            new PopupBorderControl();

        protected virtual void Initialize()
        {
            PopupBorderControl control = this.CreateBorderControl();
            if ((control != null) && BrowserInteropHelper.IsBrowserHosted)
            {
                Thickness thickness = new Thickness();
                control.Margin = thickness;
            }
            control.Popup = this;
            base.Child = control;
        }

        protected void OnPopupContentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (base.Child == null)
            {
                this.Initialize();
            }
            this.PopupContentChangedInternal((UIElement) e.OldValue);
        }

        protected static void OnPopupContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PopupBase) d).OnPopupContentChanged(e);
        }

        protected virtual void PopupContentChangedInternal(UIElement oldValue)
        {
            PopupBorderControl child = base.Child as PopupBorderControl;
            if (child != null)
            {
                child.Content = this.PopupContent;
            }
        }

        public object PopupContent
        {
            get => 
                base.GetValue(PopupContentProperty);
            set => 
                base.SetValue(PopupContentProperty, value);
        }
    }
}

