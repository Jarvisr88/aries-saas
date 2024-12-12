namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DateTimePickerItem : ContentControl
    {
        public static readonly DependencyProperty IsExpandedProperty;
        public static readonly DependencyProperty IsFakeProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty UseTransitionsProperty;

        static DateTimePickerItem()
        {
            Type ownerType = typeof(DateTimePickerItem);
            IsFakeProperty = DependencyPropertyManager.Register("IsFake", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsActiveProperty = DependencyPropertyManager.Register("IsActive", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            IsExpandedProperty = DependencyPropertyManager.Register("IsExpanded", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            UseTransitionsProperty = DependencyPropertyManager.Register("UseTransitions", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public DateTimePickerItem()
        {
            base.DefaultStyleKey = typeof(DateTimePickerItem);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateVisualStates();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (!this.IsFake)
            {
                LoopedPanel parent = base.Parent as LoopedPanel;
                DateTimePickerSelector selector = parent.ItemsContainerGenerator as DateTimePickerSelector;
                if (selector.IsExpanded)
                {
                    double y = e.GetPosition(this).Y;
                    if (parent.ScrollOwner == null)
                    {
                        return;
                    }
                    IndexCalculator indexCalculator = parent.IndexCalculator;
                    Point position = e.GetPosition(parent.ScrollOwner);
                    double viewport = (parent.Orientation == Orientation.Vertical) ? parent.ScrollOwner.ActualHeight : parent.ScrollOwner.ActualWidth;
                    double offset = indexCalculator.CalcIndexOffset(parent.Offset, parent.Viewport, viewport, y, position.Y);
                    DXScrollViewer scrollViewer = (DXScrollViewer) parent.ScrollOwner;
                    scrollViewer.AnimateScrollToVerticalOffset(offset, delegate {
                        selector.SelectedItem = null;
                        selector.IsAnimated = true;
                    }, null, () => scrollViewer.IsIntermediate = false, parent.IsLooped ? new Func<double, double>(scrollViewer.EnsureVerticalOffset) : null, ScrollDataAnimationEase.BeginAnimation);
                }
                base.OnMouseLeftButtonUp(e);
            }
        }

        public virtual void UpdateVisualStates()
        {
            if (base.IsLoaded)
            {
                if (!this.IsActive && !this.IsExpanded)
                {
                    VisualStateManager.GoToState(this, "Hidden", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Normal", true);
                }
            }
        }

        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            set => 
                base.SetValue(IsExpandedProperty, value);
        }

        public bool UseTransitions
        {
            get => 
                (bool) base.GetValue(UseTransitionsProperty);
            set => 
                base.SetValue(UseTransitionsProperty, value);
        }

        public bool IsFake
        {
            get => 
                (bool) base.GetValue(IsFakeProperty);
            set => 
                base.SetValue(IsFakeProperty, value);
        }

        public bool IsActive
        {
            get => 
                (bool) base.GetValue(IsActiveProperty);
            set => 
                base.SetValue(IsActiveProperty, value);
        }
    }
}

