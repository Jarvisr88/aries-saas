namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class ExpandHelper : DependencyObject
    {
        public static readonly Size DefaultVisibleSize = new Size(double.NaN, double.NaN);
        public static readonly RoutedEvent IsExpandedChangedEvent = EventManager.RegisterRoutedEvent("IsExpandedChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ExpandHelper));
        public static readonly DependencyProperty IsExpandedProperty = DependencyPropertyManager.RegisterAttached("IsExpanded", typeof(bool), typeof(ExpandHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ExpandHelper.OnIsExpandedChanged)));
        public static readonly DependencyProperty VisibleSizeProperty = DependencyPropertyManager.RegisterAttached("VisibleSize", typeof(Size), typeof(ExpandHelper), new FrameworkPropertyMetadata(DefaultVisibleSize));
        public static readonly DependencyProperty ItemsContainerProperty = DependencyPropertyManager.RegisterAttached("ItemsContainer", typeof(FrameworkElement), typeof(ExpandHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty RowContainerProperty = DependencyPropertyManager.RegisterAttached("RowContainer", typeof(FrameworkElement), typeof(ExpandHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ExpandSpeedProperty = DependencyPropertyManager.RegisterAttached("ExpandSpeed", typeof(double), typeof(ExpandHelper), new FrameworkPropertyMetadata(1500.0));
        public static readonly DependencyProperty ExpandStoryboardProperty = DependencyPropertyManager.RegisterAttached("ExpandStoryboard", typeof(Storyboard), typeof(ExpandHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty CollapseStoryboardProperty = DependencyPropertyManager.RegisterAttached("CollapseStoryboard", typeof(Storyboard), typeof(ExpandHelper), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty IsContinuousAnimationProperty = DependencyPropertyManager.RegisterAttached("IsContinuousAnimation", typeof(bool), typeof(ExpandHelper), new FrameworkPropertyMetadata(true));

        public static Storyboard GetCollapseStoryboard(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Storyboard) element.GetValue(CollapseStoryboardProperty);
        }

        public static double GetExpandSpeed(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (double) element.GetValue(ExpandSpeedProperty);
        }

        public static Storyboard GetExpandStoryboard(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Storyboard) element.GetValue(ExpandStoryboardProperty);
        }

        public static bool GetIsContinuousAnimation(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsContinuousAnimationProperty);
        }

        public static bool GetIsExpanded(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsExpandedProperty);
        }

        public static FrameworkElement GetItemsContainer(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (FrameworkElement) element.GetValue(ItemsContainerProperty);
        }

        public static FrameworkElement GetRowContainer(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (FrameworkElement) element.GetValue(RowContainerProperty);
        }

        public static Size GetVisibleSize(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Size) element.GetValue(VisibleSizeProperty);
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UIElement) d).RaiseEvent(new RoutedEventArgs(IsExpandedChangedEvent, e));
        }

        public static void SetCollapseStoryboard(DependencyObject element, Storyboard value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(CollapseStoryboardProperty, value);
        }

        public static void SetExpandSpeed(DependencyObject element, double value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ExpandSpeedProperty, value);
        }

        public static void SetExpandStoryboard(DependencyObject element, Storyboard value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ExpandStoryboardProperty, value);
        }

        public static void SetIsContinuousAnimation(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsContinuousAnimationProperty, value);
        }

        public static void SetIsExpanded(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsExpandedProperty, value);
        }

        public static void SetItemsContainer(DependencyObject element, FrameworkElement value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ItemsContainerProperty, value);
        }

        public static void SetRowContainer(DependencyObject element, FrameworkElement value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(RowContainerProperty, value);
        }

        public static void SetVisibleSize(DependencyObject element, Size value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(VisibleSizeProperty, value);
        }
    }
}

