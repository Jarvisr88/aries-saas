namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CardsPanel : OrderPanelBase
    {
        public const double DefaultFixedSize = double.NaN;
        public const int DefaultMaxCardCountInRow = 0x7fffffff;
        public const Alignment DefaultCardAlignment = Alignment.Near;
        private static readonly DependencyPropertyKey CardsSeparatorsPropertyKey;
        public static readonly DependencyProperty CardsSeparatorsProperty;
        public static readonly DependencyProperty CardRowProperty;
        public static readonly DependencyProperty FixedSizeProperty;
        public static readonly DependencyProperty MaxCardCountInRowProperty;
        public static readonly DependencyProperty CardAlignmentProperty;
        public static readonly DependencyProperty SeparatorThicknessProperty;
        public static readonly DependencyProperty CardMarginProperty;
        public static readonly DependencyProperty PaddingProperty;
        public static readonly DependencyProperty OwnerProperty;
        public const int InvalidCardRowIndex = -2147483648;
        private CardLayoutCalculator layoutCalculator = new CardLayoutCalculator();
        private CardsPanelInfo panelInfo;
        private Dictionary<int, CardsSeparator> cache = new Dictionary<int, CardsSeparator>();

        static CardsPanel()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CardsPanel), new FrameworkPropertyMetadata(typeof(CardsPanel)));
            FixedSizeProperty = DependencyProperty.Register("FixedSize", typeof(double), typeof(CardsPanel), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
            MaxCardCountInRowProperty = DependencyProperty.Register("MaxCardCountInRow", typeof(int), typeof(CardsPanel), new FrameworkPropertyMetadata(0x7fffffff, FrameworkPropertyMetadataOptions.AffectsMeasure, null, (CoerceValueCallback) ((d, baseValue) => ((((int) baseValue) <= 0) ? ((CardsPanel) d).MaxCardCountInRow : ((int) baseValue)))));
            CardAlignmentProperty = DependencyProperty.Register("CardAlignment", typeof(Alignment), typeof(CardsPanel), new FrameworkPropertyMetadata(Alignment.Near, FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
            SeparatorThicknessProperty = DependencyProperty.Register("SeparatorThickness", typeof(double), typeof(CardsPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
            CardsSeparatorsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("CardsSeparators", typeof(IEnumerable<CardsSeparator>), typeof(CardsPanel), new PropertyMetadata(null));
            CardsSeparatorsProperty = CardsSeparatorsPropertyKey.DependencyProperty;
            CardMarginProperty = DependencyProperty.Register("CardMargin", typeof(Thickness), typeof(CardsPanel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
            PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(CardsPanel), new FrameworkPropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0), FrameworkPropertyMetadataOptions.AffectsMeasure, null, null));
            CardRowProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterAttachedProperty<CardsPanel, int>("CardRow", -2147483648, FrameworkPropertyMetadataOptions.Inherits, null, null);
            OwnerProperty = DependencyProperty.Register("Owner", typeof(ICardsPanelOwner), typeof(CardsPanel), new FrameworkPropertyMetadata(null));
        }

        public CardsPanel()
        {
            this.panelInfo = new CardsPanelInfo(this);
        }

        protected override Size ArrangeSortedChildrenOverride(Size finalSize, IList<UIElement> sortedChildren)
        {
            Size size = finalSize;
            size.Height = Math.Max((double) 0.0, (double) ((size.Height - this.Padding.Top) - this.Padding.Bottom));
            size.Width = Math.Max((double) 0.0, (double) ((size.Width - this.Padding.Left) - this.Padding.Right));
            IList<Rect> list = this.layoutCalculator.ArrangeElements(size, sortedChildren);
            for (int i = 0; i < list.Count; i++)
            {
                Rect rect = list[i];
                Point location = rect.Location;
                location.Offset(this.Padding.Left + this.CardMargin.Left, this.Padding.Top + this.CardMargin.Top);
                Size size3 = rect.Size;
                Thickness cardMargin = this.CardMargin;
                Size size2 = new Size(rect.Size.Width - (this.CardMargin.Left + this.CardMargin.Right), size3.Height - (this.CardMargin.Top + cardMargin.Bottom));
                sortedChildren[i].Arrange(new Rect(location, size2));
            }
            this.UpdateCardRows(sortedChildren);
            this.UpdateCardsSeparators();
            return finalSize;
        }

        private void AssignToOwner()
        {
            if ((base.SizeHelper.GetDefineSize(new Size(base.ActualWidth, base.ActualHeight)) != 0.0) && (this.Owner != null))
            {
                this.Owner.ActualizePanels();
                if (!this.Owner.Panels.Contains(this))
                {
                    this.Owner.Panels.Add(this);
                }
            }
        }

        public static int GetCardRow(DependencyObject dependencyObject) => 
            (dependencyObject != null) ? ((int) dependencyObject.GetValue(CardRowProperty)) : -2147483648;

        private CardsSeparator GetSeparator(int index)
        {
            CardsSeparator separator;
            if (!this.cache.TryGetValue(index, out separator))
            {
                separator = new CardsSeparator(index + 1);
                this.cache.Add(index, separator);
            }
            return separator;
        }

        private SizeHelperBase GetSizeHelper() => 
            SizeHelperBase.GetDefineSizeHelper(base.Orientation);

        protected override unsafe Size MeasureSortedChildrenOverride(Size availableSize, IList<UIElement> sortedChildren)
        {
            this.AssignToOwner();
            Size size = availableSize;
            Size* sizePtr1 = &size;
            sizePtr1.Width -= this.Padding.Left + this.Padding.Right;
            Size* sizePtr2 = &size;
            sizePtr2.Height -= this.Padding.Bottom + this.Padding.Top;
            return this.layoutCalculator.MeasureElements(size, this.panelInfo, sortedChildren);
        }

        public static void SetCardRow(DependencyObject dependencyObject, int rowIndex)
        {
            dependencyObject.SetValue(CardRowProperty, rowIndex);
        }

        protected virtual void UpdateCardRows(IList<UIElement> sortedChildren)
        {
            int num = 0;
            if (this.layoutCalculator.Rows != null)
            {
                int rowIndex = 0;
                while (rowIndex < this.layoutCalculator.Rows.Count)
                {
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= this.layoutCalculator.Rows[rowIndex].ElementCount)
                        {
                            rowIndex++;
                            break;
                        }
                        if (sortedChildren[num] != null)
                        {
                            SetCardRow(sortedChildren[num], rowIndex);
                        }
                        num3++;
                        num++;
                    }
                }
            }
        }

        private void UpdateCardsSeparators()
        {
            IList<CardsSeparator> list = new List<CardsSeparator>();
            for (int i = 0; i < this.layoutCalculator.RowSeparators.Count; i++)
            {
                LineInfo info = this.layoutCalculator.RowSeparators[i];
                Size size = this.GetSizeHelper().CreateSize(double.NaN, info.Length);
                Point point = this.GetSizeHelper().CreatePoint(this.GetSizeHelper().GetDefinePoint(info.Location), this.GetSizeHelper().GetSecondaryPoint(info.Location));
                CardsSeparator item = this.GetSeparator(i);
                item.Margin = new Thickness(point.X, point.Y, 0.0, 0.0);
                item.Length = info.Length;
                item.Orientation = base.Orientation;
                list.Add(item);
            }
            this.CardsSeparators = list;
        }

        public double FixedSize
        {
            get => 
                (double) base.GetValue(FixedSizeProperty);
            set => 
                base.SetValue(FixedSizeProperty, value);
        }

        public int MaxCardCountInRow
        {
            get => 
                (int) base.GetValue(MaxCardCountInRowProperty);
            set => 
                base.SetValue(MaxCardCountInRowProperty, value);
        }

        public Alignment CardAlignment
        {
            get => 
                (Alignment) base.GetValue(CardAlignmentProperty);
            set => 
                base.SetValue(CardAlignmentProperty, value);
        }

        public double SeparatorThickness
        {
            get => 
                (double) base.GetValue(SeparatorThicknessProperty);
            set => 
                base.SetValue(SeparatorThicknessProperty, value);
        }

        public Thickness CardMargin
        {
            get => 
                (Thickness) base.GetValue(CardMarginProperty);
            set => 
                base.SetValue(CardMarginProperty, value);
        }

        public Thickness Padding
        {
            get => 
                (Thickness) base.GetValue(PaddingProperty);
            set => 
                base.SetValue(PaddingProperty, value);
        }

        public IEnumerable<CardsSeparator> CardsSeparators
        {
            get => 
                (IEnumerable<CardsSeparator>) base.GetValue(CardsSeparatorsProperty);
            internal set => 
                base.SetValue(CardsSeparatorsPropertyKey, value);
        }

        public ICardsPanelOwner Owner
        {
            get => 
                (ICardsPanelOwner) base.GetValue(OwnerProperty);
            set => 
                base.SetValue(OwnerProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CardsPanel.<>c <>9 = new CardsPanel.<>c();

            internal object <.cctor>b__14_0(DependencyObject d, object baseValue) => 
                (((int) baseValue) <= 0) ? ((CardsPanel) d).MaxCardCountInRow : ((int) baseValue);
        }
    }
}

