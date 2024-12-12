namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.DateNavigator;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DateNavigatorPanel : Panel
    {
        public DateNavigatorPanel()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (base.Children.Count != 2)
            {
                return base.ArrangeOverride(finalSize);
            }
            base.Children[0].Arrange(new Rect(0.0, 0.0, finalSize.Width, base.Children[0].DesiredSize.Height));
            base.Children[1].Arrange(new Rect(0.0, base.Children[0].DesiredSize.Height, finalSize.Width, base.Children[1].DesiredSize.Height));
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (base.Children.Count != 2)
            {
                return base.MeasureOverride(availableSize);
            }
            base.Children[1].Measure(new Size(availableSize.Width, double.PositiveInfinity));
            if (double.IsInfinity(availableSize.Height))
            {
                base.Children[0].Measure(availableSize);
            }
            else
            {
                base.Children[0].Measure(new Size(availableSize.Width, Math.Max((double) (availableSize.Height - base.Children[1].DesiredSize.Height), (double) 0.0)));
            }
            return new Size { 
                Width = !double.IsInfinity(availableSize.Width) ? availableSize.Width : Math.Max(base.Children[0].DesiredSize.Width, base.Children[1].DesiredSize.Width),
                Height = base.Children[0].DesiredSize.Height + base.Children[1].DesiredSize.Height
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Action<DevExpress.Xpf.Editors.DateNavigator.DateNavigator> action = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Action<DevExpress.Xpf.Editors.DateNavigator.DateNavigator> local1 = <>c.<>9__3_0;
                action = <>c.<>9__3_0 = x => x.UpdateCurrentDateText();
            }
            this.Navigator.Do<DevExpress.Xpf.Editors.DateNavigator.DateNavigator>(action);
        }

        private DevExpress.Xpf.Editors.DateNavigator.DateNavigator Navigator =>
            LayoutHelper.FindAmongParents<DevExpress.Xpf.Editors.DateNavigator.DateNavigator>(this, null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorPanel.<>c <>9 = new DateNavigatorPanel.<>c();
            public static Action<DevExpress.Xpf.Editors.DateNavigator.DateNavigator> <>9__3_0;

            internal void <OnLoaded>b__3_0(DevExpress.Xpf.Editors.DateNavigator.DateNavigator x)
            {
                x.UpdateCurrentDateText();
            }
        }
    }
}

