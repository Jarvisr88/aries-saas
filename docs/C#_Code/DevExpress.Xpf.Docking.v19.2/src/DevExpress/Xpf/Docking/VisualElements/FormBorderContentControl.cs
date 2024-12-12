namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FormBorderContentControl : ContentControl
    {
        public static readonly DependencyProperty IsActiveProperty;

        static FormBorderContentControl()
        {
            DependencyPropertyRegistrator<FormBorderContentControl> registrator = new DependencyPropertyRegistrator<FormBorderContentControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<bool>("IsActive", ref IsActiveProperty, false, (dObj, e) => ((FormBorderContentControl) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue), null);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
        }

        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            this.UpdateVisualState();
        }

        protected virtual void UpdateVisualState()
        {
        }

        public bool IsActive
        {
            get => 
                (bool) base.GetValue(IsActiveProperty);
            set => 
                base.SetValue(IsActiveProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormBorderContentControl.<>c <>9 = new FormBorderContentControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((FormBorderContentControl) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }
    }
}

