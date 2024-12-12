namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ColumnHeaderDockPanel : Panel
    {
        public static readonly DependencyProperty ContentMarginProperty;
        public static readonly DependencyProperty ContainerAlignmentProperty;
        private UIElement headerPresenterCore;
        private UIElement headerCustomizationAreaCore;
        private UIElement filterCore;
        private UIElement sortIndicatorCore;
        private UIElement imageCore;
        private DevExpress.Xpf.Editors.CheckEdit checkEditCore;
        private UIElement headerGripperCore;
        private StringAlignment imageAlignment;

        static ColumnHeaderDockPanel()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ColumnHeaderDockPanel), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<ColumnHeaderDockPanel> registrator1 = DependencyPropertyRegistrator<ColumnHeaderDockPanel>.New().Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<ColumnHeaderDockPanel, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ColumnHeaderDockPanel.get_ContentMargin)), parameters), out ContentMarginProperty, new Thickness(0.0), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ColumnHeaderDockPanel), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            registrator1.Register<HorizontalAlignment>(System.Linq.Expressions.Expression.Lambda<Func<ColumnHeaderDockPanel, HorizontalAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ColumnHeaderDockPanel.get_ContainerAlignment)), expressionArray2), out ContainerAlignmentProperty, HorizontalAlignment.Left, 2);
        }

        private double ArrangeCenter(UIElement element, double height, double indent)
        {
            element.Arrange(new Rect(indent, this.ContentMargin.Top, element.DesiredSize.Width, height));
            return (indent + element.DesiredSize.Width);
        }

        private double ArrangeLeft(UIElement element, double left, double height, double top)
        {
            element.Arrange(new Rect(left, top, element.DesiredSize.Width, height));
            return (left + element.DesiredSize.Width);
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            double left = this.ContentMargin.Left;
            double right = this.ContentMargin.Right;
            double height = Math.Max((double) 0.0, (double) (finalSize.Height - (this.ContentMargin.Top + this.ContentMargin.Bottom)));
            foreach (UIElement element in base.Children)
            {
                if (!this.IsDocked(element))
                {
                    element.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
                }
            }
            if ((this.Image != null) && (this.ImageAlignment == StringAlignment.Near))
            {
                left = this.ArrangeLeft(this.Image, left, height, this.ContentMargin.Top);
            }
            if (this.SortIndicator != null)
            {
                right = this.ArrangeRight(this.SortIndicator, right, finalSize.Width, height, this.ContentMargin.Top);
            }
            if (this.HeaderCustomizationArea != null)
            {
                right = this.ArrangeRight(this.HeaderCustomizationArea, right, finalSize.Width, height, this.ContentMargin.Top);
            }
            double num4 = height;
            double top = this.ContentMargin.Top;
            if ((this.HeaderPresenter != null) && (this.FilterPresenter != null))
            {
                double num7 = this.HeaderPresenter.DesiredSize.Height;
                if ((num7 > this.FilterPresenter.DesiredSize.Height) && (num7 < height))
                {
                    num4 = num7;
                    top = this.ContentMargin.Top + ((height - num4) / 2.0);
                }
            }
            if (this.Image != null)
            {
                if (this.ImageAlignment == StringAlignment.Far)
                {
                    right = this.ArrangeRight(this.Image, right, finalSize.Width, height, this.ContentMargin.Top);
                }
                else if (this.ImageAlignment == StringAlignment.Center)
                {
                    double num8 = this.Image.DesiredSize.Width + ((this.FilterPresenter != null) ? this.FilterPresenter.DesiredSize.Width : 0.0);
                    left = this.ArrangeCenter(this.Image, height, Math.Max((double) 0.0, (double) (left + ((((finalSize.Width - left) - right) - num8) / 2.0))));
                    if (this.FilterPresenter != null)
                    {
                        left = this.ArrangeLeft(this.FilterPresenter, left, num4, top);
                    }
                    return finalSize;
                }
            }
            double num6 = (this.CheckEdit != null) ? Math.Round((double) (this.CheckEdit.DesiredSize.Width / 2.0)) : 0.0;
            switch (this.ContainerAlignment)
            {
                case HorizontalAlignment.Left:
                    if (this.HeaderPresenter != null)
                    {
                        left = this.ArrangeLeft(this.HeaderPresenter, left, height, this.ContentMargin.Top) + num6;
                    }
                    if (this.CheckEdit != null)
                    {
                        left = this.ArrangeLeft(this.CheckEdit, left, height, this.ContentMargin.Top);
                    }
                    if (this.FilterPresenter != null)
                    {
                        left = this.ArrangeLeft(this.FilterPresenter, left, num4, top);
                    }
                    break;

                case HorizontalAlignment.Center:
                    if (this.HeaderPresenter == null)
                    {
                        if (this.CheckEdit != null)
                        {
                            double num13 = this.CheckEdit.DesiredSize.Width + ((this.FilterPresenter != null) ? this.FilterPresenter.DesiredSize.Width : 0.0);
                            left = this.ArrangeCenter(this.CheckEdit, height, Math.Max((double) 0.0, (double) (left + ((((finalSize.Width - left) - right) - num13) / 2.0))));
                        }
                    }
                    else
                    {
                        double num10 = (this.HeaderPresenter.DesiredSize.Width + ((this.FilterPresenter != null) ? this.FilterPresenter.DesiredSize.Width : 0.0)) + ((this.CheckEdit != null) ? (this.CheckEdit.DesiredSize.Width + num6) : 0.0);
                        double num12 = left;
                        left = this.ArrangeCenter(this.HeaderPresenter, height, Math.Max((double) 0.0, (double) (left + ((((finalSize.Width - left) - right) - num10) / 2.0)))) + ((this.HeaderPresenter.DesiredSize.Width > 0.0) ? num6 : 0.0);
                        if (this.CheckEdit != null)
                        {
                            left = (this.HeaderPresenter.DesiredSize.Width <= 0.0) ? this.ArrangeCenter(this.CheckEdit, height, Math.Max((double) 0.0, (double) (num12 + ((((finalSize.Width - num12) - right) - this.CheckEdit.DesiredSize.Width) / 2.0)))) : this.ArrangeLeft(this.CheckEdit, left, height, this.ContentMargin.Top);
                        }
                    }
                    if (this.FilterPresenter != null)
                    {
                        left = this.ArrangeLeft(this.FilterPresenter, left, num4, top);
                    }
                    break;

                case HorizontalAlignment.Right:
                    if (this.FilterPresenter != null)
                    {
                        right = this.ArrangeRight(this.FilterPresenter, right, finalSize.Width, num4, top) + num6;
                    }
                    if (this.CheckEdit != null)
                    {
                        right = this.ArrangeRight(this.CheckEdit, right, finalSize.Width, height, this.ContentMargin.Top);
                    }
                    if (this.HeaderPresenter != null)
                    {
                        right = this.ArrangeRight(this.HeaderPresenter, right + num6, finalSize.Width, height, this.ContentMargin.Top);
                    }
                    break;

                case HorizontalAlignment.Stretch:
                    if (this.FilterPresenter != null)
                    {
                        right = this.ArrangeRight(this.FilterPresenter, right, finalSize.Width, num4, top);
                    }
                    if (this.CheckEdit == null)
                    {
                        if (this.HeaderPresenter != null)
                        {
                            left = this.ArrangeStretch(this.HeaderPresenter, left, right, finalSize.Width, height);
                        }
                    }
                    else
                    {
                        if (this.HeaderPresenter != null)
                        {
                            left = this.ArrangeLeft(this.HeaderPresenter, left, height, this.ContentMargin.Top) + num6;
                        }
                        left = this.ArrangeStretch(this.CheckEdit, left, right, finalSize.Width, height);
                    }
                    break;

                default:
                    break;
            }
            return finalSize;
        }

        private double ArrangeRight(UIElement element, double right, double width, double height, double top)
        {
            element.Arrange(new Rect(Math.Max((double) 0.0, (double) ((width - right) - element.DesiredSize.Width)), top, element.DesiredSize.Width, height));
            return (right + element.DesiredSize.Width);
        }

        private double ArrangeStretch(UIElement element, double left, double right, double width, double height)
        {
            element.Arrange(new Rect(left, this.ContentMargin.Top, Math.Max((double) 0.0, (double) (width - (left + right))), height));
            return (width - right);
        }

        [IteratorStateMachine(typeof(<GetDockedElements>d__35))]
        private IEnumerable<UIElement> GetDockedElements()
        {
            if (this.Image != null)
            {
                yield return this.Image;
            }
            if (this.SortIndicator != null)
            {
                yield return this.SortIndicator;
            }
            if (this.HeaderCustomizationArea != null)
            {
                yield return this.HeaderCustomizationArea;
            }
            if (this.FilterPresenter != null)
            {
                yield return this.FilterPresenter;
            }
            if (this.CheckEdit != null)
            {
                yield return this.CheckEdit;
            }
            while (true)
            {
                if (this.HeaderPresenter != null)
                {
                    yield return this.HeaderPresenter;
                }
            }
        }

        private double GetHeight(UIElement element, double currentHeight) => 
            Math.Max(currentHeight, element.DesiredSize.Height);

        private bool IsDocked(UIElement element) => 
            (element != null) && this.GetDockedElements().Contains<UIElement>(element);

        private double Measure(UIElement element, double width, System.Windows.Size constraint)
        {
            element.Measure(new System.Windows.Size(Math.Max((double) 0.0, (double) (constraint.Width - width)), constraint.Height));
            return (width + element.DesiredSize.Width);
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            double width = this.ContentMargin.Left + this.ContentMargin.Right;
            double currentHeight = 0.0;
            foreach (UIElement element in base.Children)
            {
                if (!this.IsDocked(element))
                {
                    element.Measure(new System.Windows.Size(constraint.Width, constraint.Height));
                }
            }
            foreach (UIElement element2 in this.GetDockedElements())
            {
                width = this.Measure(element2, width, constraint);
                currentHeight = this.GetHeight(element2, currentHeight);
            }
            currentHeight += this.ContentMargin.Top + this.ContentMargin.Bottom;
            return new System.Windows.Size(Math.Min(width, constraint.Width), Math.Min(currentHeight, constraint.Height));
        }

        private void ReplaceChild(UIElement oldElement, UIElement newElement)
        {
            oldElement.Do<UIElement>(x => base.Children.Remove(x));
            newElement.Do<UIElement>(delegate (UIElement x) {
                if (!base.Children.Contains(x))
                {
                    base.Children.Add(x);
                }
            });
        }

        public Thickness ContentMargin
        {
            get => 
                (Thickness) base.GetValue(ContentMarginProperty);
            set => 
                base.SetValue(ContentMarginProperty, value);
        }

        public HorizontalAlignment ContainerAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(ContainerAlignmentProperty);
            set => 
                base.SetValue(ContainerAlignmentProperty, value);
        }

        public UIElement HeaderPresenter
        {
            get => 
                this.headerPresenterCore;
            set
            {
                if (!ReferenceEquals(this.headerPresenterCore, value))
                {
                    UIElement headerPresenterCore = this.headerPresenterCore;
                    this.headerPresenterCore = value;
                    this.ReplaceChild(headerPresenterCore, this.headerPresenterCore);
                }
            }
        }

        public UIElement HeaderCustomizationArea
        {
            get => 
                this.headerCustomizationAreaCore;
            set
            {
                if (!ReferenceEquals(this.headerCustomizationAreaCore, value))
                {
                    UIElement headerCustomizationAreaCore = this.headerCustomizationAreaCore;
                    this.headerCustomizationAreaCore = value;
                    this.ReplaceChild(headerCustomizationAreaCore, this.headerCustomizationAreaCore);
                }
            }
        }

        public UIElement FilterPresenter
        {
            get => 
                this.filterCore;
            set
            {
                if (!ReferenceEquals(this.filterCore, value))
                {
                    UIElement filterCore = this.filterCore;
                    this.filterCore = value;
                    this.ReplaceChild(filterCore, this.filterCore);
                }
            }
        }

        public UIElement SortIndicator
        {
            get => 
                this.sortIndicatorCore;
            set
            {
                if (!ReferenceEquals(this.sortIndicatorCore, value))
                {
                    UIElement sortIndicatorCore = this.sortIndicatorCore;
                    this.sortIndicatorCore = value;
                    this.ReplaceChild(sortIndicatorCore, this.sortIndicatorCore);
                }
            }
        }

        public UIElement Image
        {
            get => 
                this.imageCore;
            set
            {
                if (!ReferenceEquals(this.imageCore, value))
                {
                    UIElement imageCore = this.imageCore;
                    this.imageCore = value;
                    this.ReplaceChild(imageCore, this.imageCore);
                }
            }
        }

        public DevExpress.Xpf.Editors.CheckEdit CheckEdit
        {
            get => 
                this.checkEditCore;
            set
            {
                if (!ReferenceEquals(this.checkEditCore, value))
                {
                    DevExpress.Xpf.Editors.CheckEdit checkEditCore = this.checkEditCore;
                    this.checkEditCore = value;
                    this.ReplaceChild(checkEditCore, this.checkEditCore);
                }
            }
        }

        public UIElement HeaderGripper
        {
            get => 
                this.headerGripperCore;
            set
            {
                if (!ReferenceEquals(this.headerGripperCore, value))
                {
                    UIElement headerGripperCore = this.headerGripperCore;
                    this.headerGripperCore = value;
                    this.ReplaceChild(headerGripperCore, this.headerGripperCore);
                }
            }
        }

        internal StringAlignment ImageAlignment
        {
            get => 
                this.imageAlignment;
            set
            {
                if (this.imageAlignment != value)
                {
                    this.imageAlignment = value;
                    base.InvalidateMeasure();
                }
            }
        }

    }
}

