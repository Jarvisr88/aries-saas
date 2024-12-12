namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class DockHintButton : psvControl
    {
        public static readonly DependencyProperty IsHotProperty;
        public static readonly DependencyProperty IsAvailableProperty;

        static DockHintButton()
        {
            DependencyPropertyRegistrator<DockHintButton> registrator = new DependencyPropertyRegistrator<DockHintButton>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<bool>("IsHot", ref IsHotProperty, false, (dObj, ea) => ((DockHintButton) dObj).OnIsHotChanged((bool) ea.NewValue), null);
            registrator.Register<bool>("IsAvailable", ref IsAvailableProperty, true, (dObj, ea) => ((DockHintButton) dObj).OnIsAvailableChanged((bool) ea.NewValue), null);
        }

        public DockHintButton()
        {
            base.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.DockHintButton_IsEnabledChanged);
        }

        private void DockHintButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateVisualState();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
        }

        protected virtual void OnIsAvailableChanged(bool available)
        {
            this.UpdateVisualState();
        }

        protected virtual void OnIsHotChanged(bool hot)
        {
            this.UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            if (!base.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Normal", false);
                VisualStateManager.GoToState(this, "Disabled", false);
            }
            else if (this.IsAvailable)
            {
                VisualStateManager.GoToState(this, "Available", false);
                VisualStateManager.GoToState(this, this.IsHot ? "MouseOver" : "Normal", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", false);
                VisualStateManager.GoToState(this, "Unavailable", false);
            }
        }

        public bool IsHot
        {
            get => 
                (bool) base.GetValue(IsHotProperty);
            set => 
                base.SetValue(IsHotProperty, value);
        }

        public bool IsAvailable
        {
            get => 
                (bool) base.GetValue(IsAvailableProperty);
            set => 
                base.SetValue(IsAvailableProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockHintButton.<>c <>9 = new DockHintButton.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockHintButton) dObj).OnIsHotChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockHintButton) dObj).OnIsAvailableChanged((bool) ea.NewValue);
            }
        }
    }
}

