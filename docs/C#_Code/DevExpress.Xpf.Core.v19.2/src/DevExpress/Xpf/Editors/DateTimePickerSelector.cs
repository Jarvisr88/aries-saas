namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DateTimePickerSelector : DXSelector
    {
        public static readonly DependencyProperty IsAnimatedProperty;
        public static readonly DependencyProperty IsExpandedProperty;
        public static readonly DependencyProperty VisibleItemsCountProperty;
        public static readonly DependencyProperty UseTransitionsProperty;

        static DateTimePickerSelector()
        {
            Type ownerType = typeof(DateTimePickerSelector);
            IsAnimatedProperty = DependencyPropertyManager.Register("IsAnimated", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DateTimePickerSelector) d).OnAnimatedChanged((bool) e.NewValue)));
            IsExpandedProperty = DependencyPropertyManager.Register("IsExpanded", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DateTimePickerSelector) d).OnExpandedChanged((bool) e.NewValue)));
            VisibleItemsCountProperty = DependencyPropertyManager.Register("VisibleItemsCount", typeof(int), ownerType, new PropertyMetadata(0));
            UseTransitionsProperty = DependencyPropertyManager.Register("UseTransitions", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public DateTimePickerSelector()
        {
            base.DefaultStyleKey = typeof(DateTimePickerSelector);
            base.ProcessOutOfRangeItem += new EventHandler<DXItemsControlOutOfRangeItemEventArgs>(this.OnProcessOutOfRangeItem);
        }

        protected override void BringToView()
        {
            base.BringToView();
            if ((base.ScrollViewer != null) && (base.SelectedIndex != -2147483648))
            {
                LoopedPanel panelFromItemsControl = GetPanelFromItemsControl(this);
                double num = panelFromItemsControl.IndexCalculator.IndexToLogicalOffset(base.SelectedIndex);
                if (((panelFromItemsControl.Orientation == Orientation.Vertical) ? base.ScrollViewer.VerticalOffset : base.ScrollViewer.HorizontalOffset).AreClose(num))
                {
                    base.InvalidatePanel();
                }
                else if (panelFromItemsControl.Orientation == Orientation.Vertical)
                {
                    base.ScrollViewer.ScrollToVerticalOffset(num);
                }
                else
                {
                    base.ScrollViewer.ScrollToHorizontalOffset(num);
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new DateTimePickerItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is DateTimePickerItem;

        protected virtual void OnAnimatedChanged(bool newValue)
        {
            base.InvalidatePanel();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.BringToView();
        }

        protected virtual void OnExpandedChanged(bool newValue)
        {
            if (newValue)
            {
                base.Focus();
            }
            base.InvalidatePanel();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.IsExpanded = true;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            this.BringToView();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            LoopedPanel panelFromItemsControl = GetPanelFromItemsControl(this);
            double num2 = panelFromItemsControl.IndexCalculator.LogicalToNormalizedOffset((panelFromItemsControl.Orientation == Orientation.Vertical) ? panelFromItemsControl.ViewportHeight : panelFromItemsControl.ViewportWidth);
            this.VisibleItemsCount = Convert.ToInt32(num2);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (!this.IsExpanded)
            {
                this.UseTransitions = true;
                this.IsExpanded = true;
                e.Handled = true;
                base.Focus();
            }
            else if (base.SelectedItem != null)
            {
                this.IsExpanded = false;
                e.Handled = true;
                base.Focus();
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            this.IsExpanded = true;
        }

        private void OnProcessOutOfRangeItem(object sender, DXItemsControlOutOfRangeItemEventArgs e)
        {
            DateTimePickerData data1 = new DateTimePickerData();
            data1.DateTimePart = DateTimePart.None;
            data1.Text = e.Index.ToString();
            e.Item = data1;
            e.Handled = true;
        }

        protected override void OnViewChanged(object sender, ViewChangedEventArgs e)
        {
            base.OnViewChanged(sender, e);
            if (this.IsAnimated != e.IsIntermediate)
            {
                this.IsAnimated = e.IsIntermediate;
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            double num1;
            if (item == null)
            {
                return;
            }
            base.PrepareContainerForItemOverride(element, item);
            DateTimePickerItem item2 = (DateTimePickerItem) element;
            item2.UseTransitions = this.UseTransitions;
            item2.IsExpanded = this.IsExpanded || this.IsAnimated;
            if (!this.IsAnimated)
            {
                Func<bool> fallback = <>c.<>9__24_1;
                if (<>c.<>9__24_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__24_1;
                    fallback = <>c.<>9__24_1 = () => false;
                }
                if (base.SelectedItem.Return<object, bool>(x => x.Equals(item), fallback))
                {
                    num1 = 0.0;
                    goto TR_0001;
                }
            }
            num1 = 1.0;
        TR_0001:
            item2.Opacity = num1;
            item2.IsFake = ((DateTimePart) (item as DateTimePickerData).Return<DateTimePickerData, DateTimePart>((<>c.<>9__24_2 ??= x => x.DateTimePart), (<>c.<>9__24_3 ??= () => DateTimePart.None))) == DateTimePart.None;
        }

        protected override void SelectedIndexChanged(int newValue)
        {
            this.BringToView();
        }

        public void Spin(int count = 1)
        {
            if ((base.ScrollViewer != null) && (base.SelectedIndex != -2147483648))
            {
                int num = base.SelectedIndex + count;
                double num2 = base.Panel.IndexCalculator.IndexToLogicalOffset(base.IsLooped ? num : Math.Max(Math.Min(base.GetItemsCount() - 1, num), 0));
                if (!base.Panel.VerticalOffset.AreClose(num2))
                {
                    base.ScrollViewer.AnimateScrollToVerticalOffset(num2, delegate {
                        base.SelectedItem = null;
                        this.IsAnimated = true;
                    }, null, () => base.ScrollViewer.IsIntermediate = false, base.IsLooped ? new Func<double, double>(base.ScrollViewer.EnsureVerticalOffset) : null, ScrollDataAnimationEase.BeginAnimation);
                }
            }
        }

        public void SpinToIndex(int index)
        {
            if ((base.ScrollViewer != null) && (base.SelectedIndex != -2147483648))
            {
                double num = base.Panel.IndexCalculator.IndexToLogicalOffset(index);
                if (!base.Panel.VerticalOffset.AreClose(num))
                {
                    this.IsAnimated = true;
                    base.ScrollViewer.ScrollToVerticalOffset(num);
                }
            }
        }

        public bool IsAnimated
        {
            get => 
                (bool) base.GetValue(IsAnimatedProperty);
            set => 
                base.SetValue(IsAnimatedProperty, value);
        }

        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            set => 
                base.SetValue(IsExpandedProperty, value);
        }

        public int VisibleItemsCount
        {
            get => 
                (int) base.GetValue(VisibleItemsCountProperty);
            set => 
                base.SetValue(VisibleItemsCountProperty, value);
        }

        public bool UseTransitions
        {
            get => 
                (bool) base.GetValue(UseTransitionsProperty);
            set => 
                base.SetValue(UseTransitionsProperty, value);
        }

        public DataTemplate SelectedItemTemplate =>
            base.GetItemTemplate(base.SelectedItem);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateTimePickerSelector.<>c <>9 = new DateTimePickerSelector.<>c();
            public static Func<bool> <>9__24_1;
            public static Func<DateTimePickerData, DateTimePart> <>9__24_2;
            public static Func<DateTimePart> <>9__24_3;

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateTimePickerSelector) d).OnAnimatedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__4_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateTimePickerSelector) d).OnExpandedChanged((bool) e.NewValue);
            }

            internal bool <PrepareContainerForItemOverride>b__24_1() => 
                false;

            internal DateTimePart <PrepareContainerForItemOverride>b__24_2(DateTimePickerData x) => 
                x.DateTimePart;

            internal DateTimePart <PrepareContainerForItemOverride>b__24_3() => 
                DateTimePart.None;
        }
    }
}

