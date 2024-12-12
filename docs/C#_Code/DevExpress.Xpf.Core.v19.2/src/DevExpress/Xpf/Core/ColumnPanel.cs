namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ColumnPanel : Panel, IColumnPanel
    {
        public static readonly DependencyProperty ColumnCountProperty;
        public static readonly DependencyProperty ColumnOffsetsProperty;
        public static readonly DependencyProperty RowOffsetsProperty;
        private Size calculatedPanelSize;
        private Rect[] calculatedChildBoundsArray;
        private bool itemsHostChecked;

        static ColumnPanel()
        {
            ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(ColumnPanel), new PropertyMetadata((d, e) => ((ColumnPanel) d).PropertyChangedColumnCount()));
            ColumnOffsetsProperty = DependencyProperty.Register("ColumnOffsets", typeof(int[]), typeof(ColumnPanel), new PropertyMetadata((d, e) => ((ColumnPanel) d).PropertyChangedColumnOffsets()));
            RowOffsetsProperty = DependencyProperty.Register("RowOffsets", typeof(int[]), typeof(ColumnPanel), new PropertyMetadata((d, e) => ((ColumnPanel) d).PropertyChangedRowOffsets()));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int num = 0;
            foreach (UIElement element in base.Children)
            {
                if (element.Visibility == Visibility.Visible)
                {
                    element.Arrange(this.calculatedChildBoundsArray[num++]);
                }
            }
            return this.calculatedPanelSize;
        }

        protected virtual void CheckItemsHost()
        {
            if (!this.itemsHostChecked)
            {
                IItemsControl control = LayoutHelper.FindParentObject<IItemsControl>(this);
                if (control != null)
                {
                    control.SetItemsHost(this);
                }
                this.itemsHostChecked = true;
            }
        }

        protected virtual IColumnPanelLayoutCalculator CreateLayoutCalculator() => 
            new ColumnPanelLayoutCalculator();

        protected virtual int GetVisibleChildCount()
        {
            int num = 0;
            foreach (UIElement element in base.Children)
            {
                if (element.Visibility == Visibility.Visible)
                {
                    num++;
                }
            }
            return num;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size<int> size;
            Point<int>[] pointArray;
            this.CheckItemsHost();
            foreach (UIElement element in base.Children)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }
            int visibleChildCount = this.GetVisibleChildCount();
            Size<int>[] childSizes = new Size<int>[visibleChildCount];
            int num2 = 0;
            foreach (UIElement element2 in base.Children)
            {
                if (element2.Visibility == Visibility.Visible)
                {
                    childSizes[num2++] = new Size<int>((int) Math.Ceiling(element2.DesiredSize.Width), (int) Math.Ceiling(element2.DesiredSize.Height));
                }
            }
            int[] columnOffsets = (this.ColumnOffsets != null) ? this.ColumnOffsets : new int[1];
            this.CreateLayoutCalculator().CalcLayout(childSizes, this.ColumnCount, columnOffsets, (this.RowOffsets != null) ? this.RowOffsets : new int[1], out size, out pointArray);
            this.calculatedPanelSize = new Size((double) size.Width, (double) size.Height);
            this.calculatedChildBoundsArray = new Rect[visibleChildCount];
            for (int i = 0; i < visibleChildCount; i++)
            {
                Point<int> point = pointArray[i];
                this.calculatedChildBoundsArray[i] = new Rect((double) point.X, (double) point.Y, (double) childSizes[i].Width, (double) childSizes[i].Height);
            }
            return this.calculatedPanelSize;
        }

        protected virtual void PropertyChangedColumnCount()
        {
            base.InvalidateMeasure();
        }

        protected virtual void PropertyChangedColumnOffsets()
        {
            base.InvalidateMeasure();
        }

        protected virtual void PropertyChangedRowOffsets()
        {
            base.InvalidateMeasure();
        }

        public int ColumnCount
        {
            get => 
                (int) base.GetValue(ColumnCountProperty);
            set => 
                base.SetValue(ColumnCountProperty, value);
        }

        [TypeConverter(typeof(ColumnPanelOffsetsPropertyConverter))]
        public int[] ColumnOffsets
        {
            get => 
                (int[]) base.GetValue(ColumnOffsetsProperty);
            set => 
                base.SetValue(ColumnOffsetsProperty, value);
        }

        [TypeConverter(typeof(ColumnPanelOffsetsPropertyConverter))]
        public int[] RowOffsets
        {
            get => 
                (int[]) base.GetValue(RowOffsetsProperty);
            set => 
                base.SetValue(RowOffsetsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnPanel.<>c <>9 = new ColumnPanel.<>c();

            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnPanel) d).PropertyChangedColumnCount();
            }

            internal void <.cctor>b__24_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnPanel) d).PropertyChangedColumnOffsets();
            }

            internal void <.cctor>b__24_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ColumnPanel) d).PropertyChangedRowOffsets();
            }
        }
    }
}

