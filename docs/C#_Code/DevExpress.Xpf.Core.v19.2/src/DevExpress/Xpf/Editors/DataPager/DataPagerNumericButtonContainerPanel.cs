namespace DevExpress.Xpf.Editors.DataPager
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DataPagerNumericButtonContainerPanel : Panel
    {
        public static readonly DependencyProperty ButtonCountProperty;
        public static readonly DependencyProperty HorizontalContentAlignmentProperty;
        private double oldAvaibleWidth;
        private double newAvaibleWidth;
        private int lastAddPageIndex;
        private int oldButtonCount;

        static DataPagerNumericButtonContainerPanel()
        {
            Type ownerType = typeof(DataPagerNumericButtonContainerPanel);
            ButtonCountProperty = DependencyPropertyManager.Register("ButtonCount", typeof(int), ownerType, new PropertyMetadata((d, e) => ((DataPagerNumericButtonContainerPanel) d).OnButtonCountChanged((int) e.OldValue)));
            HorizontalContentAlignmentProperty = DependencyPropertyManager.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), ownerType, new FrameworkPropertyMetadata(HorizontalAlignment.Left, FrameworkPropertyMetadataOptions.AffectsArrange, (d, e) => ((DataPagerNumericButtonContainerPanel) d).PropertyChangedHorizontalContentAlignment((HorizontalAlignment) e.OldValue)));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size desiredSize;
            double x = 0.0;
            double num2 = 0.0;
            foreach (UIElement element in base.Children)
            {
                desiredSize = element.DesiredSize;
                num2 += desiredSize.Width;
            }
            switch (this.HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                case HorizontalAlignment.Stretch:
                    x = 0.0;
                    break;

                case HorizontalAlignment.Center:
                    x = Math.Floor((double) ((finalSize.Width - num2) / 2.0));
                    break;

                case HorizontalAlignment.Right:
                    x = Math.Floor((double) (finalSize.Width - num2));
                    break;

                default:
                    break;
            }
            foreach (UIElement element2 in base.Children)
            {
                element2.Arrange(new Rect(x, 0.0, element2.DesiredSize.Width, element2.DesiredSize.Height));
                desiredSize = element2.DesiredSize;
                x += desiredSize.Width;
            }
            return finalSize;
        }

        protected override unsafe Size MeasureOverride(Size availableSize)
        {
            Size desiredSize;
            if (this.Container.PageCount == 0)
            {
                base.Children.Clear();
            }
            this.newAvaibleWidth = availableSize.Width;
            if (this.oldAvaibleWidth == 0.0)
            {
                this.oldAvaibleWidth = availableSize.Width;
            }
            Size size = new Size();
            foreach (UIElement element in base.Children)
            {
                element.Measure(availableSize);
                Size* sizePtr1 = &size;
                desiredSize = element.DesiredSize;
                sizePtr1.Width += desiredSize.Width;
            }
            if (this.ButtonCount == 0)
            {
                int pageNumber = (base.Children.Count != 0) ? ((DataPagerNumericButton) base.Children[0]).PageNumber : 1;
                double num2 = this.newAvaibleWidth - this.oldAvaibleWidth;
                DevExpress.Xpf.Editors.DataPager.DataPager pager = LayoutHelper.FindParentObject<DevExpress.Xpf.Editors.DataPager.DataPager>(this);
                if ((num2 > 0.0) || (this.oldButtonCount != 0))
                {
                    while (true)
                    {
                        if ((size.Width >= availableSize.Width) || (base.Children.Count >= this.Container.PageCount))
                        {
                            while (size.Width > availableSize.Width)
                            {
                                DataPagerNumericButton button = (base.Children.Count != 0) ? ((DataPagerNumericButton) base.Children[base.Children.Count - 1]) : null;
                                if ((button != null) && button.IsCurrentPage)
                                {
                                    Size* sizePtr8 = &size;
                                    sizePtr8.Width -= base.Children[0].DesiredSize.Width;
                                    base.Children.RemoveAt(0);
                                    if ((((DataPagerNumericButton) base.Children[0]).PageNumber == 1) || !pager.AutoEllipsis)
                                    {
                                        continue;
                                    }
                                    ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                                    continue;
                                }
                                Size* sizePtr9 = &size;
                                sizePtr9.Width -= base.Children[this.lastAddPageIndex].DesiredSize.Width;
                                base.Children.RemoveAt(this.lastAddPageIndex);
                                if ((((DataPagerNumericButton) base.Children[base.Children.Count - 1]).PageNumber != this.Container.PageCount) && pager.AutoEllipsis)
                                {
                                    ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = true;
                                }
                                if ((((DataPagerNumericButton) base.Children[0]).PageNumber != 1) && pager.AutoEllipsis)
                                {
                                    ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                                }
                                if (this.lastAddPageIndex != 0)
                                {
                                    this.lastAddPageIndex--;
                                }
                            }
                            break;
                        }
                        if (((pageNumber + base.Children.Count) - 1) >= this.Container.PageCount)
                        {
                            if (base.Children.Count != 0)
                            {
                                ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = false;
                                Size* sizePtr5 = &size;
                                sizePtr5.Width -= base.Children[0].DesiredSize.Width;
                                base.Children[0].Measure(availableSize);
                                Size* sizePtr6 = &size;
                                sizePtr6.Width += base.Children[0].DesiredSize.Width;
                            }
                            base.Children.Insert(0, this.Container.CreateNumericButton(pageNumber - 1));
                            if ((((DataPagerNumericButton) base.Children[0]).PageNumber != 1) && pager.AutoEllipsis)
                            {
                                ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                            }
                            base.Children[0].Measure(availableSize);
                            Size* sizePtr7 = &size;
                            sizePtr7.Width += base.Children[0].DesiredSize.Width;
                            this.lastAddPageIndex = 0;
                        }
                        else
                        {
                            if (base.Children.Count == 0)
                            {
                                base.Children.Add(this.Container.CreateNumericButton(pageNumber));
                            }
                            else
                            {
                                ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = false;
                                Size* sizePtr2 = &size;
                                sizePtr2.Width -= base.Children[base.Children.Count - 1].DesiredSize.Width;
                                base.Children[base.Children.Count - 1].Measure(availableSize);
                                Size* sizePtr3 = &size;
                                sizePtr3.Width += base.Children[base.Children.Count - 1].DesiredSize.Width;
                                base.Children.Add(this.Container.CreateNumericButton(pageNumber + base.Children.Count));
                            }
                            if ((((DataPagerNumericButton) base.Children[base.Children.Count - 1]).PageNumber != this.Container.PageCount) && pager.AutoEllipsis)
                            {
                                ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = true;
                            }
                            base.Children[base.Children.Count - 1].Measure(availableSize);
                            Size* sizePtr4 = &size;
                            sizePtr4.Width += base.Children[base.Children.Count - 1].DesiredSize.Width;
                            this.lastAddPageIndex = base.Children.Count - 1;
                        }
                        pageNumber = ((DataPagerNumericButton) base.Children[0]).PageNumber;
                    }
                }
                else if (num2 < 0.0)
                {
                    while (size.Width > availableSize.Width)
                    {
                        DataPagerNumericButton button2 = (base.Children.Count != 0) ? ((DataPagerNumericButton) base.Children[base.Children.Count - 1]) : null;
                        if ((button2 != null) && ((button2.PageNumber == this.Container.PageCount) && button2.IsCurrentPage))
                        {
                            Size* sizePtr10 = &size;
                            sizePtr10.Width -= base.Children[0].DesiredSize.Width;
                            base.Children.RemoveAt(0);
                            if ((((DataPagerNumericButton) base.Children[0]).PageNumber != 1) && pager.AutoEllipsis)
                            {
                                ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                            }
                        }
                        else if ((button2 != null) && !button2.IsCurrentPage)
                        {
                            Size* sizePtr11 = &size;
                            sizePtr11.Width -= base.Children[base.Children.Count - 1].DesiredSize.Width;
                            base.Children.RemoveAt(base.Children.Count - 1);
                        }
                        else if ((button2 != null) && (button2.IsCurrentPage && (pager != null)))
                        {
                            pager.MoveToPreviousPage();
                        }
                        if ((((DataPagerNumericButton) base.Children[base.Children.Count - 1]).PageNumber != this.Container.PageCount) && pager.AutoEllipsis)
                        {
                            ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = true;
                        }
                    }
                }
                else if (num2 == 0.0)
                {
                    while (true)
                    {
                        if ((size.Width >= availableSize.Width) || (base.Children.Count >= this.Container.PageCount))
                        {
                            while (size.Width > availableSize.Width)
                            {
                                DataPagerNumericButton button3 = (base.Children.Count != 0) ? ((DataPagerNumericButton) base.Children[base.Children.Count - 1]) : null;
                                if ((button3 != null) && button3.IsCurrentPage)
                                {
                                    Size* sizePtr13 = &size;
                                    sizePtr13.Width -= base.Children[0].DesiredSize.Width;
                                    base.Children.RemoveAt(0);
                                    if ((((DataPagerNumericButton) base.Children[0]).PageNumber == 1) || !pager.AutoEllipsis)
                                    {
                                        continue;
                                    }
                                    ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                                    continue;
                                }
                                Size* sizePtr14 = &size;
                                sizePtr14.Width -= base.Children[base.Children.Count - 1].DesiredSize.Width;
                                base.Children.RemoveAt(base.Children.Count - 1);
                                if ((((DataPagerNumericButton) base.Children[base.Children.Count - 1]).PageNumber != this.Container.PageCount) && pager.AutoEllipsis)
                                {
                                    ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = true;
                                }
                                if ((((DataPagerNumericButton) base.Children[0]).PageNumber != 1) && pager.AutoEllipsis)
                                {
                                    ((DataPagerNumericButton) base.Children[0]).ShowEllipsis = true;
                                }
                                if (this.lastAddPageIndex != 0)
                                {
                                    this.lastAddPageIndex--;
                                }
                            }
                            break;
                        }
                        if (base.Children.Count != 0)
                        {
                            ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = false;
                        }
                        base.Children.Add(this.Container.CreateNumericButton(pageNumber + base.Children.Count));
                        base.Children[base.Children.Count - 1].Measure(availableSize);
                        Size* sizePtr12 = &size;
                        sizePtr12.Width += base.Children[base.Children.Count - 1].DesiredSize.Width;
                        if ((((DataPagerNumericButton) base.Children[base.Children.Count - 1]).PageNumber != this.Container.PageCount) && pager.AutoEllipsis)
                        {
                            ((DataPagerNumericButton) base.Children[base.Children.Count - 1]).ShowEllipsis = true;
                        }
                    }
                }
                this.oldButtonCount = 0;
            }
            this.oldAvaibleWidth = this.newAvaibleWidth;
            foreach (UIElement element2 in base.Children)
            {
                desiredSize = element2.DesiredSize;
                size.Height = Math.Max(size.Height, desiredSize.Height);
            }
            return size;
        }

        protected virtual void OnButtonCountChanged(int oldValue)
        {
            if (this.ButtonCount <= 0)
            {
                if (this.ButtonCount == 0)
                {
                    this.oldButtonCount = oldValue;
                    base.InvalidateMeasure();
                }
            }
            else if (this.ButtonCount > base.Children.Count)
            {
                while (this.ButtonCount > base.Children.Count)
                {
                    base.Children.Add(this.Container.CreateNumericButton(base.Children.Count + 1));
                }
            }
            else if (this.ButtonCount < base.Children.Count)
            {
                while (this.ButtonCount < base.Children.Count)
                {
                    base.Children.RemoveAt(base.Children.Count - 1);
                }
            }
        }

        protected virtual void PropertyChangedHorizontalContentAlignment(HorizontalAlignment oldValue)
        {
        }

        public int ButtonCount
        {
            get => 
                (int) base.GetValue(ButtonCountProperty);
            set => 
                base.SetValue(ButtonCountProperty, value);
        }

        public HorizontalAlignment HorizontalContentAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(HorizontalContentAlignmentProperty);
            set => 
                base.SetValue(HorizontalContentAlignmentProperty, value);
        }

        protected DataPagerNumericButtonContainer Container =>
            DataPagerNumericButtonContainer.GetNumericButtonContainer(this);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataPagerNumericButtonContainerPanel.<>c <>9 = new DataPagerNumericButtonContainerPanel.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainerPanel) d).OnButtonCountChanged((int) e.OldValue);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainerPanel) d).PropertyChangedHorizontalContentAlignment((HorizontalAlignment) e.OldValue);
            }
        }
    }
}

