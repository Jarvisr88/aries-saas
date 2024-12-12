namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class BaseTabHint : psvControl
    {
        public static readonly DependencyProperty TabHeaderLocationProperty;

        static BaseTabHint()
        {
            DependencyPropertyRegistrator<BaseTabHint> registrator = new DependencyPropertyRegistrator<BaseTabHint>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Dock>("TabHeaderLocation", ref TabHeaderLocationProperty, Dock.Top, (dObj, e) => ((BaseTabHint) dObj).OnTabHeaderLocationChanged((Dock) e.NewValue), null);
        }

        public BaseTabHint(TabHintType type)
        {
            this.Type = type;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, this.TabHeaderLocation.ToString(), false);
        }

        protected virtual void OnTabHeaderLocationChanged(Dock headerLocation)
        {
            VisualStateManager.GoToState(this, headerLocation.ToString(), false);
        }

        public Dock TabHeaderLocation
        {
            get => 
                (Dock) base.GetValue(TabHeaderLocationProperty);
            set => 
                base.SetValue(TabHeaderLocationProperty, value);
        }

        public TabHintType Type { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseTabHint.<>c <>9 = new BaseTabHint.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((BaseTabHint) dObj).OnTabHeaderLocationChanged((Dock) e.NewValue);
            }
        }
    }
}

