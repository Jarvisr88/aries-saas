namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DisplayTextProviderBehavior : Behavior<BaseEdit>
    {
        public static readonly DependencyProperty DisplayTextProviderProperty;

        static DisplayTextProviderBehavior()
        {
            Type ownerType = typeof(DisplayTextProviderBehavior);
            DisplayTextProviderProperty = DependencyProperty.Register("DisplayTextProvider", typeof(IDisplayTextProvider), ownerType, new PropertyMetadata(null, (d, e) => ((DisplayTextProviderBehavior) d).DisplayTextProviderChanged((IDisplayTextProvider) e.OldValue, (IDisplayTextProvider) e.NewValue)));
        }

        protected virtual void DisplayTextProviderChanged(IDisplayTextProvider oldValue, IDisplayTextProvider newValue)
        {
            BaseEditHelper.SetTotalDisplayTextProvider(base.AssociatedObject, newValue);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            BaseEditHelper.SetTotalDisplayTextProvider(base.AssociatedObject, this.DisplayTextProvider);
        }

        protected override void OnDetaching()
        {
            BaseEditHelper.SetTotalDisplayTextProvider(base.AssociatedObject, null);
            base.OnDetaching();
        }

        public IDisplayTextProvider DisplayTextProvider
        {
            get => 
                (IDisplayTextProvider) base.GetValue(DisplayTextProviderProperty);
            set => 
                base.SetValue(DisplayTextProviderProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DisplayTextProviderBehavior.<>c <>9 = new DisplayTextProviderBehavior.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DisplayTextProviderBehavior) d).DisplayTextProviderChanged((IDisplayTextProvider) e.OldValue, (IDisplayTextProvider) e.NewValue);
            }
        }
    }
}

