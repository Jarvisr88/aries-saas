namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DateNavigatorContentPanel : Panel
    {
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty RowCountProperty;
        private int virtColumnCount = 1;
        private int virtRowCount = 1;

        static DateNavigatorContentPanel()
        {
            Type ownerType = typeof(DateNavigatorContentPanel);
            ColumnCountProperty = DependencyPropertyManager.Register("ColumnCount", typeof(int), ownerType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure), new ValidateValueCallback(DateNavigatorContentPanel.ValidatePropertyValueColumnCount));
            RowCountProperty = DependencyPropertyManager.Register("RowCount", typeof(int), ownerType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure), new ValidateValueCallback(DateNavigatorContentPanel.ValidatePropertyValueRowCount));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            IDateNavigatorContentPanelOwner panelOwner = DateNavigatorContent.GetPanelOwner(this);
            if ((panelOwner == null) || panelOwner.IsHidden)
            {
                return base.ArrangeOverride(finalSize);
            }
            Size itemSize = panelOwner.GetItemSize();
            double width = itemSize.Width;
            double height = itemSize.Height;
            finalSize.Width = itemSize.Width * this.virtColumnCount;
            finalSize.Height = itemSize.Height * this.virtRowCount;
            int num3 = ((int) (finalSize.Width - (this.virtColumnCount * width))) / 2;
            int num4 = 0;
            while (num4 < this.virtColumnCount)
            {
                int num5 = 0;
                while (true)
                {
                    if (num5 >= this.virtRowCount)
                    {
                        num4++;
                        break;
                    }
                    base.Children[(num5 * this.virtColumnCount) + num4].Arrange(new Rect(num3 + (num4 * width), num5 * height, width, height));
                    num5++;
                }
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            IDateNavigatorContentPanelOwner panelOwner = DateNavigatorContent.GetPanelOwner(this);
            if (panelOwner == null)
            {
                return base.MeasureOverride(availableSize);
            }
            bool flag = false;
            if (base.Children.Count == 0)
            {
                base.Children.Add(panelOwner.CreateItem());
                flag = true;
            }
            base.Children[0].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            if (panelOwner.IsHidden)
            {
                return base.MeasureOverride(availableSize);
            }
            Size itemSize = panelOwner.GetItemSize();
            if (double.IsInfinity(availableSize.Width))
            {
                availableSize.Width = itemSize.Width;
            }
            if (double.IsInfinity(availableSize.Height))
            {
                availableSize.Height = itemSize.Height;
            }
            this.virtColumnCount = (this.ColumnCount != 0) ? this.ColumnCount : Math.Max((int) (availableSize.Width / itemSize.Width), 1);
            this.virtRowCount = (this.RowCount != 0) ? this.RowCount : Math.Max((int) (availableSize.Height / itemSize.Height), 1);
            int index = this.virtColumnCount * this.virtRowCount;
            int num2 = index - base.Children.Count;
            if (num2 > 0)
            {
                for (int i = 1; i <= num2; i++)
                {
                    base.Children.Add(panelOwner.CreateItem());
                }
            }
            else if (num2 < 0)
            {
                for (int i = 1; i <= -num2; i++)
                {
                    panelOwner.UninitializeItem(base.Children[index]);
                    base.Children.RemoveAt(index);
                }
            }
            if (num2 != 0)
            {
                for (int i = 1; i < index; i++)
                {
                    base.Children[i].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                }
            }
            if ((num2 != 0) | flag)
            {
                panelOwner.ItemCountChanged();
            }
            availableSize.Height = itemSize.Height * this.virtRowCount;
            availableSize.Width = itemSize.Width * this.virtColumnCount;
            panelOwner.UpdateItemPositions(this.virtColumnCount, this.virtRowCount);
            return availableSize;
        }

        private static bool ValidatePropertyValueColumnCount(object value) => 
            ((int) value) >= 0;

        private static bool ValidatePropertyValueRowCount(object value) => 
            ((int) value) >= 0;

        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        public int RowCount
        {
            get => 
                (int) base.GetValue(RowCountProperty);
            set => 
                base.SetValue(RowCountProperty, value);
        }
    }
}

