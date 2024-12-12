namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class DocumentPaneItem : TabbedPaneItem
    {
        public static readonly DependencyProperty TabColorProperty;
        public static readonly DependencyProperty HasTabColorProperty;
        private static readonly DependencyPropertyKey HasTabColorPropertyKey;

        static DocumentPaneItem()
        {
            DependencyPropertyRegistrator<DocumentPaneItem> registrator = new DependencyPropertyRegistrator<DocumentPaneItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Color>("TabColor", ref TabColorProperty, Colors.Transparent, (dObj, e) => ((DocumentPaneItem) dObj).OnTabColorChanged((Color) e.OldValue, (Color) e.NewValue), null);
            registrator.RegisterReadonly<bool>("HasTabColor", ref HasTabColorPropertyKey, ref HasTabColorProperty, false, null, null);
        }

        protected override bool IsControlBoxActuallyVisible(BaseLayoutItem item) => 
            base.IsControlBoxActuallyVisible(item) || ((bool) item.GetValue(BaseLayoutItem.IsPinButtonVisibleProperty));

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            if (item != null)
            {
                item.Forward(this, TabColorProperty, "ActualTabBackgroundColor", BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(TabColorProperty);
            }
        }

        protected virtual void OnTabColorChanged(Color oldValue, Color newValue)
        {
            base.SetValue(HasTabColorPropertyKey, newValue != Colors.Transparent);
            base.Dispatcher.BeginInvoke(new Action(this.UpdateVisualState), new object[0]);
        }

        private void UpdateColorState()
        {
            VisualStateManager.GoToState(this, "EmptyColorState", false);
            if (this.HasTabColor)
            {
                if (base.IsSelected)
                {
                    if (base.LayoutItem.IsActive)
                    {
                        VisualStateManager.GoToState(this, "ColorSelected", false);
                    }
                    else
                    {
                        VisualStateManager.GoToState(this, "ColorInactive", false);
                    }
                }
                else
                {
                    VisualStateManager.GoToState(this, base.IsMouseOver ? "ColorMouseOver" : "ColorUnselected", false);
                }
            }
        }

        protected override void UpdateSelectionState()
        {
            VisualStateManager.GoToState(this, "EmptySelectionState", false);
            if (!base.IsSelected)
            {
                VisualStateManager.GoToState(this, "Unselected", false);
            }
            else if (base.LayoutItem.IsActive)
            {
                VisualStateManager.GoToState(this, "Selected", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Inactive", false);
            }
        }

        protected override void UpdateVisualState()
        {
            base.UpdateVisualState();
            this.UpdateColorState();
        }

        public Color TabColor
        {
            get => 
                (Color) base.GetValue(TabColorProperty);
            set => 
                base.SetValue(TabColorProperty, value);
        }

        public bool HasTabColor =>
            (bool) base.GetValue(HasTabColorProperty);

        protected override CaptionLocation DefaultCaptionLocation =>
            CaptionLocation.Top;

        protected override bool TransformBorderThickness =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPaneItem.<>c <>9 = new DocumentPaneItem.<>c();

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentPaneItem) dObj).OnTabColorChanged((Color) e.OldValue, (Color) e.NewValue);
            }
        }
    }
}

