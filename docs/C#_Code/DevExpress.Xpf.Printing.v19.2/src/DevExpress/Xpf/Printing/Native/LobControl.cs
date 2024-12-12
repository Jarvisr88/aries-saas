namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class LobControl : ContentControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyPropertyManager.Register("Header", typeof(Label), typeof(LobControl), new UIPropertyMetadata(new PropertyChangedCallback(LobControl.HeaderChanged)));
        public static readonly DependencyProperty HeaderSizeGroupProperty = DependencyPropertyManager.Register("HeaderSizeGroup", typeof(SharedSizeGroup), typeof(LobControl), new UIPropertyMetadata(new PropertyChangedCallback(LobControl.HeaderSizeGroupChanged)));

        static LobControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LobControl), new FrameworkPropertyMetadata(typeof(LobControl)));
        }

        private static void HeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LobControl control = d as LobControl;
            if (control.HeaderSizeGroup != null)
            {
                Label oldValue = e.OldValue as Label;
                Label newValue = e.NewValue as Label;
                if (oldValue != null)
                {
                    control.HeaderSizeGroup.UnregisterElement(oldValue);
                }
                if (newValue != null)
                {
                    control.HeaderSizeGroup.RegisterElement(newValue);
                }
            }
        }

        private static void HeaderSizeGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LobControl control = d as LobControl;
            if (control.Header != null)
            {
                SharedSizeGroup oldValue = e.OldValue as SharedSizeGroup;
                SharedSizeGroup newValue = e.NewValue as SharedSizeGroup;
                if (oldValue != null)
                {
                    oldValue.UnregisterElement(control.Header);
                }
                if (newValue != null)
                {
                    newValue.RegisterElement(control.Header);
                }
            }
        }

        public Label Header
        {
            get => 
                (Label) base.GetValue(HeaderProperty);
            set => 
                base.SetValue(HeaderProperty, value);
        }

        public SharedSizeGroup HeaderSizeGroup
        {
            get => 
                (SharedSizeGroup) base.GetValue(HeaderSizeGroupProperty);
            set => 
                base.SetValue(HeaderSizeGroupProperty, value);
        }
    }
}

