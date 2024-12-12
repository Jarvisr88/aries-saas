namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class MinimizedFloatGroupsItemPanel : Panel
    {
        internal const double DBL_EPSILON = 2.2204460492503131E-16;

        private bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }
            double num = ((Math.Abs(value1) + Math.Abs(value2)) + 10.0) * 2.2204460492503131E-16;
            double num2 = value1 - value2;
            return ((-num < num2) && (num > num2));
        }

        private void arrangeLine(double v, double lineV, int start, int end)
        {
            double x = 0.0;
            UIElementCollection internalChildren = base.InternalChildren;
            for (int i = start; i < end; i++)
            {
                UIElement element = internalChildren[i];
                if (element != null)
                {
                    Size size = new Size(element.DesiredSize.Width, element.DesiredSize.Height);
                    double width = size.Width;
                    element.Arrange(new Rect(x, v, width, lineV));
                    x += width;
                }
            }
        }

        protected override unsafe Size ArrangeOverride(Size finalSize)
        {
            int start = 0;
            double num2 = 0.0;
            Size size = new Size();
            Size size2 = new Size(finalSize.Width, finalSize.Height);
            UIElementCollection internalChildren = base.InternalChildren;
            int end = 0;
            int count = internalChildren.Count;
            while (end < count)
            {
                UIElement element = internalChildren[end];
                if (element != null)
                {
                    Size size3 = new Size(element.DesiredSize.Width, element.DesiredSize.Height);
                    if (!this.GreaterThan(size.Width + size3.Width, size2.Width))
                    {
                        Size* sizePtr1 = &size;
                        sizePtr1.Width += size3.Width;
                        size.Height = Math.Max(size3.Height, size.Height);
                    }
                    else
                    {
                        num2 += size.Height;
                        this.arrangeLine(finalSize.Height - num2, size.Height, start, end);
                        size = size3;
                        if (this.GreaterThan(size3.Width, size2.Width))
                        {
                            num2 += size3.Height;
                            int num1 = end;
                            this.arrangeLine(finalSize.Height - num2, size3.Height, num1, ++end);
                            size = new Size();
                        }
                        start = end;
                    }
                }
                end++;
            }
            if (start < internalChildren.Count)
            {
                this.arrangeLine(0.0, size.Height, start, internalChildren.Count);
            }
            return finalSize;
        }

        private bool GreaterThan(double value1, double value2) => 
            (value1 > value2) && !this.AreClose(value1, value2);

        protected override unsafe Size MeasureOverride(Size constraint)
        {
            Size size = new Size();
            Size size2 = new Size();
            Size size3 = new Size(constraint.Width, constraint.Height);
            Size availableSize = new Size(constraint.Width, constraint.Height);
            UIElementCollection internalChildren = base.InternalChildren;
            int num = 0;
            int count = internalChildren.Count;
            while (num < count)
            {
                UIElement element = internalChildren[num];
                if (element != null)
                {
                    element.Measure(availableSize);
                    Size size5 = new Size(element.DesiredSize.Width, element.DesiredSize.Height);
                    if (!this.GreaterThan(size.Width + size5.Width, size3.Width))
                    {
                        Size* sizePtr3 = &size;
                        sizePtr3.Width += size5.Width;
                        size.Height = Math.Max(size5.Height, size.Height);
                    }
                    else
                    {
                        size2.Width = Math.Max(size.Width, size2.Width);
                        Size* sizePtr1 = &size2;
                        sizePtr1.Height += size.Height;
                        size = size5;
                        if (this.GreaterThan(size5.Width, size3.Width))
                        {
                            size2.Width = Math.Max(size5.Width, size2.Width);
                            Size* sizePtr2 = &size2;
                            sizePtr2.Height += size5.Height;
                            size = new Size();
                        }
                    }
                }
                num++;
            }
            size2.Width = Math.Max(size.Width, size2.Width);
            Size* sizePtr4 = &size2;
            sizePtr4.Height += size.Height;
            return new Size(size2.Width, size2.Height);
        }
    }
}

