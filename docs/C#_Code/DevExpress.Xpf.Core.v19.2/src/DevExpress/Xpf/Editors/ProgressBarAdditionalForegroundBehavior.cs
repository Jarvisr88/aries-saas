namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ProgressBarAdditionalForegroundBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty ClipRectProperty;

        static ProgressBarAdditionalForegroundBehavior()
        {
            Type ownerType = typeof(ProgressBarAdditionalForegroundBehavior);
            ClipRectProperty = DependencyProperty.Register("ClipRect", typeof(Rect), ownerType, new PropertyMetadata(Rect.Empty, (o, args) => ((ProgressBarAdditionalForegroundBehavior) o).ClipRectChanged((Rect) args.NewValue)));
        }

        private void ClipRectChanged(Rect newValue)
        {
            if (base.AssociatedObject != null)
            {
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = newValue;
                base.AssociatedObject.Clip = geometry1;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        public Rect ClipRect
        {
            get => 
                (Rect) base.GetValue(ClipRectProperty);
            set => 
                base.SetValue(ClipRectProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProgressBarAdditionalForegroundBehavior.<>c <>9 = new ProgressBarAdditionalForegroundBehavior.<>c();

            internal void <.cctor>b__1_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ProgressBarAdditionalForegroundBehavior) o).ClipRectChanged((Rect) args.NewValue);
            }
        }
    }
}

