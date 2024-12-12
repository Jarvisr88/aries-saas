namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class FocusRectPresenter : ContentControl
    {
        public static readonly DependencyProperty ViewProperty;
        public static readonly DependencyProperty SizeExpansionProperty;
        public static readonly DependencyProperty ChildTemplateProperty;
        public static readonly DependencyProperty IsHorizontalScrollHostProperty;
        public static readonly DependencyProperty IsVerticalScrollHostProperty;
        private FrameworkElement owner;
        private FrameworkElement HorizontalClipParent;
        private FrameworkElement VerticalClipParent;
        private Size size = Size.Empty;
        private Rect oldOwnerRect;
        private double oldHorizontalClipParentWidth;
        private double oldVerticalClipParentHeight;

        static FocusRectPresenter()
        {
            ViewProperty = DependencyProperty.Register("View", typeof(DataViewBase), typeof(FocusRectPresenter), new PropertyMetadata(null, (d, e) => ((FocusRectPresenter) d).OnViewChanged()));
            SizeExpansionProperty = DependencyProperty.Register("SizeExpansion", typeof(int), typeof(FocusRectPresenter), new PropertyMetadata(0));
            ChildTemplateProperty = DependencyProperty.Register("ChildTemplate", typeof(ControlTemplate), typeof(FocusRectPresenter), new PropertyMetadata(null, (d, e) => ((FocusRectPresenter) d).OnChildTemplateChanged()));
            IsHorizontalScrollHostProperty = DependencyProperty.RegisterAttached("IsHorizontalScrollHost", typeof(bool), typeof(FocusRectPresenter), new PropertyMetadata(false));
            IsVerticalScrollHostProperty = DependencyProperty.RegisterAttached("IsVerticalScrollHost", typeof(bool), typeof(FocusRectPresenter), new PropertyMetadata(false));
        }

        public FocusRectPresenter()
        {
            base.Content = new Control();
            base.Visibility = Visibility.Collapsed;
            base.IsHitTestVisible = false;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElement child = VisualTreeHelper.GetChild(this, 0) as UIElement;
            if (child != null)
            {
                child.Arrange(new Rect(this.oldOwnerRect.Size));
            }
            return finalSize;
        }

        internal static FrameworkElement FindScrollHost(DependencyObject element, DependencyProperty property)
        {
            while (element != null)
            {
                if ((bool) element.GetValue(property))
                {
                    return (element as FrameworkElement);
                }
                element = VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        private RectangleGeometry GetClip(Rect ownerRect)
        {
            Point location = new Point();
            Rect rect = (this.VerticalClipParent != null) ? LayoutHelper.GetRelativeElementRect(this.VerticalClipParent, this.GetTemplatedParent() as UIElement) : ownerRect;
            double num = rect.Top + this.GetFixedElementsHeight();
            if ((ownerRect.Top < num) && (ownerRect.Bottom >= num))
            {
                location.Y = num - ownerRect.Top;
            }
            Rect rect2 = (this.HorizontalClipParent != null) ? LayoutHelper.GetRelativeElementRect(this.HorizontalClipParent, this.GetTemplatedParent() as UIElement) : ownerRect;
            if (ownerRect.Left < rect2.Left)
            {
                location.X = rect2.Left - ownerRect.Left;
            }
            double num2 = ownerRect.Size.Width - location.X;
            double num3 = ownerRect.Size.Height - location.Y;
            if (rect.Bottom < ownerRect.Bottom)
            {
                num3 += rect.Bottom - ownerRect.Bottom;
            }
            if (rect2.Right < ownerRect.Right)
            {
                num2 += rect2.Right - ownerRect.Right;
            }
            this.size = new Size(Math.Max(0.0, num2), Math.Max(0.0, num3));
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = new Rect(location, this.size);
            return geometry1;
        }

        private Rect GetCoerceRect()
        {
            IChrome owner = this.Owner as IChrome;
            if ((owner != null) && (owner.Root != null))
            {
                return owner.Root.RenderRect;
            }
            return new Rect(new Point(), this.Owner.RenderSize);
        }

        private double GetFixedElementsHeight()
        {
            double num = 0.0;
            HierarchyPanel content = this.View.DataPresenter.Content as HierarchyPanel;
            if (content != null)
            {
                foreach (FrameworkElement element in content.FixedElements)
                {
                    num += element.ActualHeight;
                }
            }
            return num;
        }

        public static bool GetIsHorizontalScrollHost(DependencyObject element) => 
            (bool) element.GetValue(IsHorizontalScrollHostProperty);

        public static bool GetIsVerticalScrollHost(DependencyObject element) => 
            (bool) element.GetValue(IsVerticalScrollHostProperty);

        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(new Size(this.oldOwnerRect.Width, this.oldOwnerRect.Height));
            return this.size;
        }

        private void OnChildTemplateChanged()
        {
            ((Control) base.Content).Template = this.ChildTemplate;
        }

        private void OnViewChanged()
        {
            if (this.View != null)
            {
                this.View.FocusRectPresenter = this;
            }
        }

        public static void SetIsHorizontalScrollHost(DependencyObject element, bool value)
        {
            element.SetValue(IsHorizontalScrollHostProperty, value);
        }

        public static void SetIsVerticalScrollHost(DependencyObject element, bool value)
        {
            element.SetValue(IsVerticalScrollHostProperty, value);
        }

        internal unsafe void UpdateRendering(double leftIndent, double rightIndent = 0.0)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(this.Owner, this.GetTemplatedParent() as UIElement);
            Rect coerceRect = this.GetCoerceRect();
            Rect* rectPtr1 = &relativeElementRect;
            rectPtr1.X += coerceRect.Left;
            Rect* rectPtr2 = &relativeElementRect;
            rectPtr2.Y += coerceRect.Top;
            Rect* rectPtr3 = &relativeElementRect;
            rectPtr3.Width -= (this.Owner.ActualWidth - coerceRect.Width) + rightIndent;
            Rect* rectPtr4 = &relativeElementRect;
            rectPtr4.Height -= this.Owner.ActualHeight - coerceRect.Height;
            Rect* rectPtr5 = &relativeElementRect;
            rectPtr5.X += leftIndent;
            if (relativeElementRect.Width >= leftIndent)
            {
                Rect* rectPtr6 = &relativeElementRect;
                rectPtr6.Width -= leftIndent;
            }
            Rect* rectPtr7 = &relativeElementRect;
            rectPtr7.X -= this.SizeExpansion;
            Rect* rectPtr8 = &relativeElementRect;
            rectPtr8.Y -= this.SizeExpansion;
            Rect* rectPtr9 = &relativeElementRect;
            rectPtr9.Height += this.SizeExpansion * 2;
            Rect* rectPtr10 = &relativeElementRect;
            rectPtr10.Width += this.SizeExpansion * 2;
            double num = (this.HorizontalClipParent != null) ? this.HorizontalClipParent.ActualWidth : 0.0;
            double num2 = (this.VerticalClipParent != null) ? this.VerticalClipParent.ActualHeight : 0.0;
            if ((relativeElementRect != this.oldOwnerRect) || ((this.oldHorizontalClipParentWidth != num) || (this.oldVerticalClipParentHeight != num2)))
            {
                this.oldHorizontalClipParentWidth = num;
                this.oldVerticalClipParentHeight = num2;
                this.oldOwnerRect = relativeElementRect;
                base.Clip = this.GetClip(relativeElementRect);
                base.Margin = new Thickness(relativeElementRect.Left, relativeElementRect.Top, 0.0, 0.0);
                base.InvalidateMeasure();
            }
        }

        public DataViewBase View
        {
            get => 
                (DataViewBase) base.GetValue(ViewProperty);
            set => 
                base.SetValue(ViewProperty, value);
        }

        public int SizeExpansion
        {
            get => 
                (int) base.GetValue(SizeExpansionProperty);
            set => 
                base.SetValue(SizeExpansionProperty, value);
        }

        public ControlTemplate ChildTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ChildTemplateProperty);
            set => 
                base.SetValue(ChildTemplateProperty, value);
        }

        internal FrameworkElement Owner
        {
            get => 
                this.owner;
            set
            {
                if (!ReferenceEquals(this.owner, value))
                {
                    this.owner = value;
                    if (this.owner != null)
                    {
                        this.HorizontalClipParent = FindScrollHost(this.owner, IsHorizontalScrollHostProperty);
                        this.VerticalClipParent = FindScrollHost(this.owner, IsVerticalScrollHostProperty);
                    }
                    else
                    {
                        this.HorizontalClipParent = null;
                        this.VerticalClipParent = null;
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FocusRectPresenter.<>c <>9 = new FocusRectPresenter.<>c();

            internal void <.cctor>b__38_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FocusRectPresenter) d).OnViewChanged();
            }

            internal void <.cctor>b__38_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FocusRectPresenter) d).OnChildTemplateChanged();
            }
        }
    }
}

