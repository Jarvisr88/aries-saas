namespace DevExpress.Xpf.Office.Internal
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class OfficeMouseWheelScrollBehavior<T> : Behavior<T> where T: Control, IMouseWheelScrollClient
    {
        private MouseWheelScrollHelper scrollHelper;
        private bool usePixelScroll;

        public OfficeMouseWheelScrollBehavior(bool usePixelScroll)
        {
            this.usePixelScroll = usePixelScroll;
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            this.VerifyMouseHWheelListening();
        }

        private void AssociatedObjectPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.scrollHelper.OnMouseWheel(e);
            e.Handled = true;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.scrollHelper = new MouseWheelScrollHelper(base.AssociatedObject, this.usePixelScroll);
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.AssociatedObjectLoaded);
            base.AssociatedObject.PreviewMouseWheel += new MouseWheelEventHandler(this.AssociatedObjectPreviewMouseWheel);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObjectLoaded);
            base.AssociatedObject.PreviewMouseWheel -= new MouseWheelEventHandler(this.AssociatedObjectPreviewMouseWheel);
            base.OnDetaching();
        }

        private void VerifyMouseHWheelListening()
        {
            if (base.AssociatedObject.IsLoaded)
            {
                DependencyObject objA = LayoutHelper.FindRoot(base.AssociatedObject, false);
                if (!Equals(objA, base.AssociatedObject))
                {
                    BehaviorCollection source = Interaction.GetBehaviors(objA);
                    Func<Behavior, bool> predicate = <>c<T>.<>9__6_0;
                    if (<>c<T>.<>9__6_0 == null)
                    {
                        Func<Behavior, bool> local1 = <>c<T>.<>9__6_0;
                        predicate = <>c<T>.<>9__6_0 = x => x is HWndHostWMMouseHWheelBehavior;
                    }
                    if (source.FirstOrDefault<Behavior>(predicate) == null)
                    {
                        source.Add(new HWndHostWMMouseHWheelBehavior());
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OfficeMouseWheelScrollBehavior<T>.<>c <>9;
            public static Func<Behavior, bool> <>9__6_0;

            static <>c()
            {
                OfficeMouseWheelScrollBehavior<T>.<>c.<>9 = new OfficeMouseWheelScrollBehavior<T>.<>c();
            }

            internal bool <VerifyMouseHWheelListening>b__6_0(Behavior x) => 
                x is HWndHostWMMouseHWheelBehavior;
        }
    }
}

