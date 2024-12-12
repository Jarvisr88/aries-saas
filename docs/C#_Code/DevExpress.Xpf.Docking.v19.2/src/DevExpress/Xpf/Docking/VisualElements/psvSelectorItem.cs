namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class psvSelectorItem : psvContentControlBase
    {
        private static readonly DependencyPropertyKey IsSelectedPropertyKey;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        private const double AlignPrecision = 1.0000000001;

        static psvSelectorItem()
        {
            new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<psvSelectorItem>().RegisterReadonly<bool>("IsSelected", ref IsSelectedPropertyKey, ref IsSelectedProperty, false, (dObj, e) => ((psvSelectorItem) dObj).OnIsSelectedChanged((bool) e.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(psvSelectorItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<psvSelectorItem>.New().Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<psvSelectorItem, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(psvSelectorItem.get_ViewStyle)), parameters), out ViewStyleProperty, DockingViewStyle.Default, (d, oldValue, newValue) => d.OnViewStyleChanged(oldValue, newValue), frameworkOptions);
        }

        protected virtual void AfterArrange()
        {
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            this.AfterArrange();
            return size;
        }

        internal void GetItemAlignment<TParent>(bool isHorizontal, out bool isLeftAligned, out bool isRightAligned) where TParent: FrameworkElement
        {
            TParent relativeTo = LayoutTreeHelper.GetVisualParents(this, null).OfType<TParent>().FirstOrDefault<TParent>();
            if ((relativeTo == null) || (this.ViewStyle != DockingViewStyle.Light))
            {
                isLeftAligned = false;
                isRightAligned = false;
            }
            else
            {
                bool isFloatingRootInTabbedGroup;
                Point point2 = new Point();
                Point point = base.TranslatePoint(point2, relativeTo);
                LayoutPanel layoutItem = base.LayoutItem as LayoutPanel;
                if (layoutItem != null)
                {
                    isFloatingRootInTabbedGroup = layoutItem.IsFloatingRootInTabbedGroup;
                }
                else
                {
                    LayoutPanel local1 = layoutItem;
                    isFloatingRootInTabbedGroup = false;
                }
                double precesion = isFloatingRootInTabbedGroup ? 1.0000000001 : 1E-10;
                if (isHorizontal)
                {
                    isLeftAligned = MathHelper.AreDoubleClose(point.X, 0.0, 1.0000000001);
                    Point point3 = base.TranslatePoint(new Point(base.ActualWidth, 0.0), relativeTo);
                    isRightAligned = MathHelper.AreDoubleClose(point3.X, relativeTo.ActualWidth, precesion);
                }
                else
                {
                    isRightAligned = MathHelper.AreDoubleClose(point.Y, 0.0, 1.0000000001);
                    Point point4 = base.TranslatePoint(new Point(0.0, base.ActualHeight), relativeTo);
                    isLeftAligned = MathHelper.AreDoubleClose(point4.Y, relativeTo.ActualHeight, precesion);
                }
            }
        }

        protected virtual void OnIsSelectedChanged(bool selected)
        {
        }

        protected virtual void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            if (item != null)
            {
                item.Forward(this, UIElement.IsEnabledProperty, "IsEnabled", BindingMode.OneWay);
                item.Forward(this, ViewStyleProperty, BaseLayoutItem.DockingViewStyleProperty, BindingMode.OneWay);
            }
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.ClearValue(UIElement.IsEnabledProperty);
            base.ClearValue(ViewStyleProperty);
            base.Unsubscribe(item);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            internal set => 
                base.SetValue(IsSelectedPropertyKey, value);
        }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvSelectorItem.<>c <>9 = new psvSelectorItem.<>c();

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvSelectorItem) dObj).OnIsSelectedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__3_1(psvSelectorItem d, DockingViewStyle oldValue, DockingViewStyle newValue)
            {
                d.OnViewStyleChanged(oldValue, newValue);
            }
        }
    }
}

