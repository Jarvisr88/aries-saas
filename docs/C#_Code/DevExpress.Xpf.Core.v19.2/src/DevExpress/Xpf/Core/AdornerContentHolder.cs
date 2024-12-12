namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class AdornerContentHolder : Decorator
    {
        public AdornerContentHolder(BaseFloatingContainer container)
        {
            this.Container = container;
            base.SetValue(FloatingContainer.IsActiveProperty, true);
            base.SetValue(FloatingContainer.IsMaximizedProperty, false);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (!this.Container.UseActiveStateOnly)
            {
                base.SetValue(FloatingContainer.IsActiveProperty, e.NewValue);
            }
        }

        public BaseFloatingContainer Container { get; private set; }
    }
}

