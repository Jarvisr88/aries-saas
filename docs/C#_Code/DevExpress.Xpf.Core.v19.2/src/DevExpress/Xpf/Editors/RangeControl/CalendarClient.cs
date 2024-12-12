namespace DevExpress.Xpf.Editors.RangeControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl.Internal;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class CalendarClient : Control, IRangeControlClient
    {
        private const double DefaultMinItemWidth = 20.0;
        private const double DefaultMinFontSize = 11.0;
        private const double DefaultMaxItemWidth = 100.0;
        private const double MaxScaleFactor = 1500.0;
        public static readonly DependencyProperty SelectionStartProperty;
        public static readonly DependencyProperty SelectionEndProperty;
        public static readonly DependencyProperty VisibleStartProperty;
        public static readonly DependencyProperty VisibleEndProperty;
        public static readonly DependencyProperty StartProperty;
        public static readonly DependencyProperty EndProperty;
        private static readonly DependencyPropertyKey SelectionStartPropertyKey;
        private static readonly DependencyPropertyKey SelectionEndPropertyKey;
        private static readonly DependencyPropertyKey VisibleStartPropertyKey;
        private static readonly DependencyPropertyKey VisibleEndPropertyKey;
        private static readonly DependencyPropertyKey StartPropertyKey;
        private static readonly DependencyPropertyKey EndPropertyKey;
        public static readonly DependencyProperty IntervalFactoriesProperty;
        public static readonly DependencyProperty ItemIntervalFactoryProperty;
        public static readonly DependencyProperty GroupIntervalFactoryProperty;
        private static readonly DependencyPropertyKey ItemIntervalFactoryPropertyKey;
        private static readonly DependencyPropertyKey GroupIntervalFactoryPropertyKey;
        public static readonly DependencyProperty AllowGroupingProperty;
        public static readonly DependencyProperty GroupingHeightProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty GroupItemStyleProperty;
        public static readonly DependencyProperty ZoomOutSelectionMarkerTemplateProperty;
        public static readonly DependencyProperty CustomItemIntervalFactoryProperty;
        public static readonly DependencyProperty CustomGroupIntervalFactoryProperty;
        private ObservableCollection<IntervalFactory> actualIntervalFactories;
        private IValueNormalizer valueValueNormalizer;
        private CalendarClientPanel calendarPanel;
        private Rect clientBounds;

        public event EventHandler<LayoutChangedEventArgs> LayoutChanged;

        static CalendarClient()
        {
            Type ownerType = typeof(CalendarClient);
            ItemStyleProperty = DependencyProperty.Register("ItemStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata((owner, args) => ((CalendarClient) owner).OnItemStyleChanged()));
            GroupItemStyleProperty = DependencyProperty.Register("GroupItemStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata((owner, args) => ((CalendarClient) owner).OnGroupItemStyleChanged()));
            SelectionStartPropertyKey = DependencyProperty.RegisterReadOnly("SelectionStart", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).SelectionStartChanged(args.NewValue)));
            SelectionEndPropertyKey = DependencyProperty.RegisterReadOnly("SelectionEnd", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).SelectionEndChanged(args.NewValue)));
            VisibleStartPropertyKey = DependencyProperty.RegisterReadOnly("VisibleStart", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).VisibleStartChanged(args.NewValue)));
            VisibleEndPropertyKey = DependencyProperty.RegisterReadOnly("VisibleEnd", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).VisibleEndChanged(args.NewValue)));
            StartPropertyKey = DependencyProperty.RegisterReadOnly("Start", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).StartChanged(args.NewValue)));
            EndPropertyKey = DependencyProperty.RegisterReadOnly("End", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (owner, args) => ((CalendarClient) owner).EndChanged(args.NewValue)));
            StartProperty = StartPropertyKey.DependencyProperty;
            EndProperty = EndPropertyKey.DependencyProperty;
            SelectionStartProperty = SelectionStartPropertyKey.DependencyProperty;
            SelectionEndProperty = SelectionEndPropertyKey.DependencyProperty;
            VisibleStartProperty = VisibleStartPropertyKey.DependencyProperty;
            VisibleEndProperty = VisibleEndPropertyKey.DependencyProperty;
            IntervalFactoriesProperty = DependencyProperty.Register("IntervalFactories", typeof(ObservableCollection<IntervalFactory>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CalendarClient) d).OnIntervalFactoriesChanged((ObservableCollection<IntervalFactory>) e.OldValue)));
            ItemIntervalFactoryPropertyKey = DependencyProperty.RegisterReadOnly("ItemIntervalFactory", typeof(IntervalFactory), ownerType, new FrameworkPropertyMetadata(null));
            GroupIntervalFactoryPropertyKey = DependencyProperty.RegisterReadOnly("GroupIntervalFactory", typeof(IntervalFactory), ownerType, new FrameworkPropertyMetadata(null));
            ItemIntervalFactoryProperty = ItemIntervalFactoryPropertyKey.DependencyProperty;
            GroupIntervalFactoryProperty = GroupIntervalFactoryPropertyKey.DependencyProperty;
            AllowGroupingProperty = DependencyProperty.Register("AllowGrouping", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange));
            GroupingHeightProperty = DependencyProperty.Register("GroupingHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(30.0, FrameworkPropertyMetadataOptions.AffectsArrange));
            ZoomOutSelectionMarkerTemplateProperty = DependencyProperty.Register("ZoomOutSelectionMarkerTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));
            CustomGroupIntervalFactoryProperty = DependencyProperty.Register("CustomGroupIntervalFactory", typeof(IntervalFactory), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CalendarClient) d).OnCustomGroupIntervalFactoryChanged()));
            CustomItemIntervalFactoryProperty = DependencyProperty.Register("CustomItemIntervalFactory", typeof(IntervalFactory), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CalendarClient) d).OnCustomItemIntervalFactoryChanged()));
        }

        public CalendarClient()
        {
            base.DefaultStyleKey = typeof(CalendarClient);
            this.GroupIntervalFactory = new DummyIntervalFactory();
            this.IntervalFactories = new ObservableCollection<IntervalFactory>();
            this.IntervalFactories.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnIntervalFactoriesCollectionChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.SizeChanged += new SizeChangedEventHandler(this.CalendarClientSizeChanged);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.calendarPanel != null)
            {
                if (double.IsInfinity(finalSize.Height) || double.IsInfinity(finalSize.Width))
                {
                    return base.ArrangeOverride(finalSize);
                }
                this.PrepareToRender(this.CalcUpdateBounds(finalSize));
                this.calendarPanel.SetLayoutInfo(this.LayoutInfo, this.RenderBounds);
                this.calendarPanel.InvalidateArrange();
            }
            return base.ArrangeOverride(finalSize);
        }

        private void CalcFactoryDefinedVisibleRange(IntervalFactory factory, ref object visibleStart, ref object visibleEnd, double itemWidth, double viewport)
        {
            double comparableValue = this.ValueNormalizer.GetComparableValue(visibleStart);
            double comparable = this.ValueNormalizer.GetComparableValue(visibleEnd);
            double num6 = this.GetSnappedValue(factory, comparableValue, false) - this.GetSnappedValue(factory, comparableValue, true);
            if (!((this.VisibleStart != null) ? this.ValueNormalizer.GetComparableValue(this.VisibleStart) : 0.0).AreClose(comparableValue))
            {
                comparableValue = comparable - ((viewport / itemWidth) * num6);
                visibleStart = this.ValueNormalizer.GetRealValue(comparableValue);
            }
            else
            {
                comparable = comparableValue + ((viewport / itemWidth) * num6);
                visibleEnd = this.ValueNormalizer.GetRealValue(comparable);
            }
        }

        private bool CalcMaxScaleCorrection(object start, object end, ref object visibleStart, ref object visibleEnd, IntervalFactory factory, double width)
        {
            double comparableValue = this.ValueNormalizer.GetComparableValue(visibleStart);
            double comparable = this.ValueNormalizer.GetComparableValue(visibleEnd);
            double min = this.ValueNormalizer.GetComparableValue(start);
            double max = this.ValueNormalizer.GetComparableValue(end);
            if (Math.Ceiling((double) ((max - min) / (comparable - comparableValue))) < 1500.0)
            {
                return false;
            }
            double num6 = (max - min) / 1500.0;
            double num7 = comparable;
            num7 = (num7 > max) ? max : num7;
            if (((this.VisibleStart != null) ? this.ValueNormalizer.GetComparableValue(this.VisibleStart) : 0.0) != comparableValue)
            {
                visibleEnd = this.ValueNormalizer.GetRealValue(comparable);
                visibleStart = this.ValueNormalizer.GetRealValue(Math.Max(comparable - num6, min).ToRange(min, max));
            }
            else
            {
                visibleStart = this.ValueNormalizer.GetRealValue(comparableValue);
                visibleEnd = this.ValueNormalizer.GetRealValue(Math.Min(comparableValue + num6, max).ToRange(min, max));
            }
            double num10 = (this.ViewPort.Width / ((double) this.MinStepsNumber)) / this.clientBounds.Width;
            double num11 = num10 * this.MinStepsNumber;
            if (num10 > (comparable - comparableValue))
            {
                num7 = comparableValue + num11;
                visibleEnd = this.ValueNormalizer.GetRealValue(num7);
            }
            return true;
        }

        private Rect CalcRenderRect(Size size)
        {
            double num = this.ToNormalized(this.Instance.GetComparableValue(this.VisibleStart));
            return new Rect(new Point(size.Width * num, 0.0), this.ViewPort);
        }

        private Rect CalcUpdateBounds(Size size) => 
            this.ViewPort.IsZero() ? new Rect(new Point(0.0, 0.0), size) : this.CalcRenderRect(size);

        private void CalendarClientSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateClientBounds();
            this.UpdateIntervalFactories(this.clientBounds.Width);
            this.RaiseClientDataChanged(LayoutChangedType.Layout);
        }

        private void Clear()
        {
            if (this.calendarPanel != null)
            {
                this.calendarPanel.Clear();
            }
        }

        private ObservableCollection<IntervalFactory> CoerceIntervalFactories(ObservableCollection<IntervalFactory> value) => 
            ((value == null) || (value.Count <= 0)) ? this.GetDefaultFactories() : value;

        private void ConstrainByType(ref object start, ref object end)
        {
            start = ((start == null) || !this.TryGetValue(ref start)) ? this.Start : start;
            end = ((end == null) || !this.TryGetValue(ref end)) ? this.End : end;
        }

        private bool CorrectVisibleRange(object visibleStart, object visibleEnd, double width)
        {
            bool flag = this.CorrectVisibleRangeByData(ref visibleStart, ref visibleEnd);
            Func<ObservableCollection<IntervalFactory>, bool> evaluator = <>c.<>9__169_0;
            if (<>c.<>9__169_0 == null)
            {
                Func<ObservableCollection<IntervalFactory>, bool> local1 = <>c.<>9__169_0;
                evaluator = <>c.<>9__169_0 = x => x.Count > 0;
            }
            Func<ObservableCollection<IntervalFactory>, IntervalFactory> func2 = <>c.<>9__169_1;
            if (<>c.<>9__169_1 == null)
            {
                Func<ObservableCollection<IntervalFactory>, IntervalFactory> local2 = <>c.<>9__169_1;
                func2 = <>c.<>9__169_1 = x => x.First<IntervalFactory>();
            }
            IntervalFactory factory = this.ActualIntervalFactories.If<ObservableCollection<IntervalFactory>>(evaluator).Return<ObservableCollection<IntervalFactory>, IntervalFactory>(func2, () => this.GetDefaultFactories().First<IntervalFactory>());
            Func<ObservableCollection<IntervalFactory>, bool> func3 = <>c.<>9__169_3;
            if (<>c.<>9__169_3 == null)
            {
                Func<ObservableCollection<IntervalFactory>, bool> local3 = <>c.<>9__169_3;
                func3 = <>c.<>9__169_3 = x => x.Count > 0;
            }
            Func<ObservableCollection<IntervalFactory>, IntervalFactory> func4 = <>c.<>9__169_4;
            if (<>c.<>9__169_4 == null)
            {
                Func<ObservableCollection<IntervalFactory>, IntervalFactory> local4 = <>c.<>9__169_4;
                func4 = <>c.<>9__169_4 = x => x.Last<IntervalFactory>();
            }
            IntervalFactory factory2 = this.ActualIntervalFactories.If<ObservableCollection<IntervalFactory>>(func3).Return<ObservableCollection<IntervalFactory>, IntervalFactory>(func4, () => this.GetDefaultFactories().Last<IntervalFactory>());
            double itemWidth = Math.Min(this.GetRenderStep(factory, visibleStart, visibleEnd, width), width / ((double) this.MinStepsNumber));
            double num2 = Math.Min(this.GetRenderStep(factory2, visibleStart, visibleEnd, width), width / ((double) this.MinStepsNumber));
            if (itemWidth < this.GetMinItemWidth())
            {
                this.CalcFactoryDefinedVisibleRange(factory, ref visibleStart, ref visibleEnd, itemWidth, width);
                flag = true;
            }
            else if (num2 > this.GetMaxItemWidth())
            {
                this.CalcFactoryDefinedVisibleRange(factory2, ref visibleStart, ref visibleEnd, num2, width);
                flag = true;
            }
            if (this.CalcMaxScaleCorrection(this.ActualStart, this.ActualEnd, ref visibleStart, ref visibleEnd, factory2, num2 * this.MinStepsNumber))
            {
                flag = true;
            }
            this.VisibleStart = visibleStart;
            this.VisibleEnd = visibleEnd;
            return flag;
        }

        private bool CorrectVisibleRangeByData(ref object visibleStart, ref object visibleEnd)
        {
            bool flag = false;
            double comparableValue = this.ValueNormalizer.GetComparableValue(visibleStart);
            double num2 = this.ValueNormalizer.GetComparableValue(visibleEnd);
            if (comparableValue > num2)
            {
                num2 = comparableValue;
            }
            double min = this.ValueNormalizer.GetComparableValue(this.ActualStart);
            double max = this.ValueNormalizer.GetComparableValue(this.ActualEnd);
            if (!comparableValue.InRange(min, max))
            {
                visibleStart = this.ValueNormalizer.GetRealValue(comparableValue.ToRange(min, max));
                flag = true;
            }
            if (!num2.InRange(min, max))
            {
                visibleEnd = this.ValueNormalizer.GetRealValue(num2.ToRange(min, max));
                flag = true;
            }
            return flag;
        }

        private LayoutChangedEventArgs CreateChangedEventArgs(LayoutChangedType type) => 
            new LayoutChangedEventArgs(type, null, null);

        protected virtual DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo CreateLayoutInfo()
        {
            DateTime time;
            DateTime time2;
            DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo info1 = new DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo();
            info1.ComparableStart = this.Instance.GetComparableValue(this.ActualStart);
            info1.ComparableEnd = this.Instance.GetComparableValue(this.ActualEnd);
            info1.ComparableVisibleStart = this.Instance.GetComparableValue(this.ActualVisibleStart);
            info1.ComparableVisibleEnd = this.Instance.GetComparableValue(this.ActualVisibleEnd);
            DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo info = info1;
            this.GetVisibleRangeWithOffset(out time, out time2);
            info.ComparableRenderVisibleStart = this.Instance.GetComparableValue(time);
            info.ComparableRenderVisibleEnd = this.Instance.GetComparableValue(time2);
            info.ComparableSelectionStart = this.ToComparableRange(info.ComparableStart, info.ComparableEnd, this.Instance.GetComparableValue(this.SelectionStart));
            info.ComparableSelectionEnd = this.ToComparableRange(info.ComparableStart, info.ComparableEnd, this.Instance.GetComparableValue(this.SelectionEnd));
            return info;
        }

        string IRangeControlClient.FormatText(object value) => 
            this.GetItemIntervalFactory().FormatLabelText(value);

        double IRangeControlClient.GetComparableValue(object realValue) => 
            this.ValueNormalizer.GetComparableValue(realValue);

        object IRangeControlClient.GetRealValue(double comparable) => 
            this.ValueNormalizer.GetRealValue(comparable);

        object IRangeControlClient.GetSnappedValue(object value, bool isLeft) => 
            isLeft ? this.GetItemIntervalFactory().Snap(value) : this.GetItemIntervalFactory().GetNextValue(value);

        RangeControlClientHitTestResult IRangeControlClient.HitTest(Point point)
        {
            if (this.IsGroupInterval(point))
            {
                double num = this.RenderToComparable(point.X, base.ActualWidth);
                return new RangeControlClientHitTestResult(RangeControlClientRegionType.GroupInterval, this.GroupIntervalFactory.Snap(this.ValueNormalizer.GetRealValue(num)), this.GroupIntervalFactory.GetNextValue(this.ValueNormalizer.GetRealValue(num)));
            }
            if (!this.IsItemInterval(point))
            {
                return RangeControlClientHitTestResult.Nothing;
            }
            double comparable = this.RenderToComparable(point.X, base.ActualWidth);
            return new RangeControlClientHitTestResult(RangeControlClientRegionType.ItemInterval, this.ItemIntervalFactory.Snap(this.ValueNormalizer.GetRealValue(comparable)), this.ItemIntervalFactory.GetNextValue(this.ValueNormalizer.GetRealValue(comparable)));
        }

        void IRangeControlClient.Invalidate(Size viewPort)
        {
            this.ViewPort = viewPort;
            base.InvalidateArrange();
        }

        bool IRangeControlClient.SetRange(object start, object end, Size viewportSize)
        {
            this.ConstrainByType(ref start, ref end);
            return this.SetRangeInternal(start, end, viewportSize);
        }

        bool IRangeControlClient.SetSelectionRange(object selectedMinimum, object selectedMaximum, Size viewportSize, bool isSnapped)
        {
            this.ConstrainByType(ref selectedMinimum, ref selectedMaximum);
            this.SelectionStart = selectedMinimum;
            this.SelectionEnd = selectedMaximum;
            this.OnSetSelectionRange();
            return true;
        }

        bool IRangeControlClient.SetVisibleRange(object visibleStart, object visibleEnd, Size viewportSize)
        {
            this.ConstrainByType(ref visibleStart, ref visibleEnd);
            return this.CorrectVisibleRange(visibleStart, visibleEnd, viewportSize.Width);
        }

        protected virtual void EndChanged(object newValue)
        {
        }

        private void FindIntervalFactories(double totalWidth, out IntervalFactory groupFactory, out IntervalFactory itemFactory)
        {
            groupFactory = this.GetFirstFactory();
            itemFactory = groupFactory;
            if ((this.Start != null) && (this.End != null))
            {
                foreach (IntervalFactory factory in this.ActualIntervalFactories)
                {
                    double renderStep = this.GetRenderStep(factory, totalWidth);
                    if (this.IsCompatibleRenderStep(renderStep))
                    {
                        break;
                    }
                    groupFactory = itemFactory;
                    itemFactory = factory;
                }
            }
        }

        private double GetComparableStep(IntervalFactory factory, object value)
        {
            object obj2 = factory.Snap(value);
            object nextValue = factory.GetNextValue(obj2);
            return (this.ValueNormalizer.GetComparableValue(nextValue) - this.ValueNormalizer.GetComparableValue(obj2));
        }

        protected virtual ObservableCollection<IntervalFactory> GetDefaultFactories()
        {
            ObservableCollection<IntervalFactory> collection1 = new ObservableCollection<IntervalFactory>();
            collection1.Add(new YearIntervalFactory());
            collection1.Add(new QuarterIntervalFactory());
            collection1.Add(new MonthIntervalFactory());
            collection1.Add(new DayIntervalFactory());
            collection1.Add(new HourIntervalFactory());
            collection1.Add(new MinuteIntervalFactory());
            collection1.Add(new SecondIntervalFactory());
            return collection1;
        }

        private double GetDefaultMinItemWidth() => 
            Math.Max((double) 20.0, (double) (20.0 * this.FontScaleFactor));

        protected object GetDefaultValue() => 
            DateTime.Today;

        private IntervalFactory GetFirstFactory()
        {
            Func<ObservableCollection<IntervalFactory>, bool> evaluator = <>c.<>9__155_0;
            if (<>c.<>9__155_0 == null)
            {
                Func<ObservableCollection<IntervalFactory>, bool> local1 = <>c.<>9__155_0;
                evaluator = <>c.<>9__155_0 = x => x.Count > 0;
            }
            Func<ObservableCollection<IntervalFactory>, IntervalFactory> func2 = <>c.<>9__155_1;
            if (<>c.<>9__155_1 == null)
            {
                Func<ObservableCollection<IntervalFactory>, IntervalFactory> local2 = <>c.<>9__155_1;
                func2 = <>c.<>9__155_1 = x => x.First<IntervalFactory>();
            }
            return this.ActualIntervalFactories.If<ObservableCollection<IntervalFactory>>(evaluator).Return<ObservableCollection<IntervalFactory>, IntervalFactory>(func2, () => this.GetDefaultFactories().First<IntervalFactory>());
        }

        internal double GetGroupingHeight() => 
            this.AllowGrouping ? this.GetGroupingHeight(base.ActualHeight) : 0.0;

        protected double GetGroupingHeight(double height) => 
            Math.Min(this.GroupingHeight, height);

        protected IntervalFactory GetItemIntervalFactory()
        {
            Func<IntervalFactory, IntervalFactory> evaluator = <>c.<>9__116_0;
            if (<>c.<>9__116_0 == null)
            {
                Func<IntervalFactory, IntervalFactory> local1 = <>c.<>9__116_0;
                evaluator = <>c.<>9__116_0 = x => x;
            }
            return this.ItemIntervalFactory.Return<IntervalFactory, IntervalFactory>(evaluator, () => this.GetDefaultFactories().First<IntervalFactory>());
        }

        private double GetMaxItemWidth() => 
            100.0;

        private double GetMinItemWidth() => 
            this.GetDefaultMinItemWidth();

        private object GetNextValue(object value) => 
            this.GetItemIntervalFactory().GetNextValue(value);

        protected double GetRenderStep(IntervalFactory factory, double totalWidth)
        {
            object obj2 = factory.Snap(this.GetDefaultValue());
            object nextValue = factory.GetNextValue(obj2);
            double num = this.ValueNormalizer.GetComparableValue(nextValue) - this.ValueNormalizer.GetComparableValue(obj2);
            object obj4 = this.Start ?? obj2;
            object realValue = this.End ?? factory.GetNextValue(obj4);
            return ((num / (this.ValueNormalizer.GetComparableValue(realValue) - this.ValueNormalizer.GetComparableValue(obj4))) * totalWidth);
        }

        private double GetRenderStep(IntervalFactory factory, object visibleStart, object visibleEnd, double viewport)
        {
            double num4 = (this.ValueNormalizer.GetComparableValue(visibleEnd) - this.ValueNormalizer.GetComparableValue(visibleStart)) / this.GetComparableStep(factory, visibleStart);
            return (viewport / num4);
        }

        protected double GetSnappedValue(IntervalFactory factory, double comparableValue, bool isLeft)
        {
            object obj2 = factory.Snap(this.Instance.GetRealValue(comparableValue));
            object nextValue = factory.Snap(obj2);
            if (!isLeft)
            {
                nextValue = factory.GetNextValue(nextValue);
            }
            return this.Instance.GetComparableValue(nextValue);
        }

        private void GetVisibleRangeWithOffset(out DateTime start, out DateTime end)
        {
            start = (DateTime) this.ActualVisibleStart;
            end = (DateTime) this.ActualVisibleEnd;
            if (this.ItemIntervalFactory != null)
            {
                int num = 5;
                double num2 = this.Instance.GetComparableValue(this.ActualVisibleStart) - (num * this.GetComparableStep(this.ItemIntervalFactory, this.ActualVisibleStart));
                double num3 = this.Instance.GetComparableValue(this.ActualVisibleEnd) + (num * this.GetComparableStep(this.ItemIntervalFactory, this.ActualVisibleEnd));
                start = (DateTime) this.Instance.GetRealValue(Math.Max(num2, this.Instance.GetComparableValue(this.ActualStart)));
                end = (DateTime) this.Instance.GetRealValue(Math.Min(num3, this.Instance.GetComparableValue(this.ActualEnd)));
            }
        }

        protected void InvalidateRender()
        {
            base.InvalidateArrange();
        }

        protected bool IsCompatibleRenderStep(double renderStep) => 
            renderStep < this.GetMinItemWidth();

        protected bool IsGroupInterval(Point point) => 
            point.X.InRange(0.0, base.ActualWidth) && point.Y.InRange(Math.Max((double) 0.0, (double) (base.ActualHeight - this.GetGroupingHeight())), base.ActualHeight);

        protected bool IsItemInterval(Point point) => 
            point.X.InRange(0.0, base.ActualWidth) && point.Y.InRange(0.0, Math.Max((double) 0.0, (double) (base.ActualHeight - this.GetGroupingHeight())));

        private static bool IsTypeCorrect(Type type) => 
            type == typeof(DateTime);

        protected override Size MeasureOverride(Size availableSize) => 
            base.MeasureOverride(availableSize);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.calendarPanel = LayoutHelper.FindElementByType(this, typeof(CalendarClientPanel)) as CalendarClientPanel;
            if (this.calendarPanel != null)
            {
                this.calendarPanel.Owner = this;
            }
        }

        private void OnCustomGroupIntervalFactoryChanged()
        {
            this.GroupIntervalFactory = this.CustomGroupIntervalFactory;
            base.InvalidateArrange();
        }

        private void OnCustomItemIntervalFactoryChanged()
        {
            this.ItemIntervalFactory = this.CustomItemIntervalFactory;
            base.InvalidateArrange();
        }

        private void OnGroupItemStyleChanged()
        {
            this.Clear();
            base.InvalidateArrange();
        }

        private void OnIntervalFactoriesChanged(ObservableCollection<IntervalFactory> oldValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnIntervalFactoriesCollectionChanged);
            }
            if (this.IntervalFactories != null)
            {
                this.IntervalFactories.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnIntervalFactoriesCollectionChanged);
            }
            this.UpdateActualIntervalFactories();
        }

        private void OnIntervalFactoriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateActualIntervalFactories();
        }

        private void OnItemStyleChanged()
        {
            this.Clear();
            base.InvalidateArrange();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.SetupActualIntervalFactories();
            this.UpdateDefaultRanges();
            this.UpdateIntervalFactories(base.ActualWidth);
            this.RaiseClientDataChanged(LayoutChangedType.Layout);
        }

        protected void OnSetSelectionRange()
        {
            this.InvalidateRender();
        }

        protected internal void PrepareToRender(Rect renderBounds)
        {
            this.RenderBounds = renderBounds;
            this.UpdateLayoutInfo();
        }

        protected void RaiseClientDataChanged(LayoutChangedType type)
        {
            LayoutChangedEventArgs e = this.CreateChangedEventArgs(type);
            if (this.LayoutChanged != null)
            {
                this.LayoutChanged(this, e);
            }
        }

        protected double RenderToComparable(double renderValue, double totalWidth) => 
            ((renderValue / totalWidth) * (this.LayoutInfo.ComparableEnd - this.LayoutInfo.ComparableStart)) + this.LayoutInfo.ComparableStart;

        protected virtual void SelectionEndChanged(object newValue)
        {
        }

        protected virtual void SelectionStartChanged(object newValue)
        {
        }

        private bool SetRangeInternal(object start, object end, Size viewportSize)
        {
            this.Start = start;
            this.End = end;
            this.UpdateVisibleRangeOnSetRange(this.ActualStart, this.ActualEnd, viewportSize);
            this.InvalidateRender();
            return true;
        }

        private void SetupActualIntervalFactories()
        {
            this.ActualIntervalFactories = this.CoerceIntervalFactories(this.ActualIntervalFactories);
        }

        protected virtual void StartChanged(object newValue)
        {
        }

        protected double ToComparableRange(double min, double max, double value)
        {
            double num = Math.Max(min, value);
            return Math.Min(max, num);
        }

        private double ToNormalized(double value)
        {
            double comparableValue = this.Instance.GetComparableValue(this.Start);
            double num2 = this.Instance.GetComparableValue(this.End);
            return ((value - comparableValue) / (num2 - comparableValue));
        }

        private bool TryGetValue(ref object value)
        {
            DateTime time;
            bool flag = DateTime.TryParse(value.ToString(), out time);
            value = time;
            return flag;
        }

        private void UpdateActualIntervalFactories()
        {
            this.ActualIntervalFactories = this.IntervalFactories;
            this.UpdateIntervalFactories(this.clientBounds.Width);
            base.InvalidateArrange();
        }

        private void UpdateClientBounds()
        {
            Rect bounds = new Rect(new Point(0.0, 0.0), new Size(base.ActualWidth, base.ActualHeight));
            this.UpdateClientBoundsInternal(ref bounds);
            this.clientBounds = bounds;
        }

        protected virtual void UpdateClientBoundsInternal(ref Rect bounds)
        {
            RectHelper.Deflate(ref bounds, new Thickness(0.0, 0.0, 0.0, this.AllowGrouping ? this.GroupingHeight : 0.0));
        }

        private void UpdateDefaultRange()
        {
            this.Start ??= ((this.ItemIntervalFactory != null) ? this.ItemIntervalFactory.Snap(this.GetDefaultValue()) : this.ActualIntervalFactories.First<IntervalFactory>().Snap(this.GetDefaultValue()));
            this.End ??= ((this.ItemIntervalFactory != null) ? this.ItemIntervalFactory.GetNextValue(this.GetDefaultValue()) : this.ActualIntervalFactories.First<IntervalFactory>().GetNextValue(this.GetDefaultValue()));
        }

        private void UpdateDefaultRanges()
        {
            this.UpdateDefaultRange();
            this.UpdateDefaultVisibleRange();
        }

        private void UpdateDefaultVisibleRange()
        {
            this.VisibleStart ??= this.Start;
            this.VisibleEnd ??= this.End;
        }

        protected virtual void UpdateIntervalFactories(double totalWidth)
        {
            IntervalFactory factory;
            IntervalFactory factory2;
            this.FindIntervalFactories(totalWidth, out factory, out factory2);
            if (this.CustomGroupIntervalFactory == null)
            {
                this.GroupIntervalFactory = factory;
            }
            if (this.CustomItemIntervalFactory == null)
            {
                this.ItemIntervalFactory = factory2;
            }
        }

        private void UpdateLayoutInfo()
        {
            this.LayoutInfo = this.CreateLayoutInfo();
        }

        private void UpdateVisibleRangeOnSetRange(object start, object end, Size viewportSize)
        {
            double comparableValue = this.ValueNormalizer.GetComparableValue(start);
            double max = this.ValueNormalizer.GetComparableValue(end);
            double num3 = this.ValueNormalizer.GetComparableValue(this.ActualVisibleStart);
            double num4 = this.ValueNormalizer.GetComparableValue(this.ActualVisibleEnd);
            if (!num3.InRange(comparableValue, max) || !num4.InRange(comparableValue, max))
            {
                num3 = num3.ToRange(comparableValue, max);
                num4 = num4.ToRange(num3, max);
                this.Instance.SetVisibleRange(this.ValueNormalizer.GetRealValue(num3), this.ValueNormalizer.GetRealValue(num4), viewportSize);
            }
        }

        protected virtual void VisibleEndChanged(object newValue)
        {
        }

        protected virtual void VisibleStartChanged(object newValue)
        {
        }

        public IntervalFactory CustomGroupIntervalFactory
        {
            get => 
                (IntervalFactory) base.GetValue(CustomGroupIntervalFactoryProperty);
            set => 
                base.SetValue(CustomGroupIntervalFactoryProperty, value);
        }

        public IntervalFactory CustomItemIntervalFactory
        {
            get => 
                (IntervalFactory) base.GetValue(CustomItemIntervalFactoryProperty);
            set => 
                base.SetValue(CustomItemIntervalFactoryProperty, value);
        }

        public DataTemplate ZoomOutSelectionMarkerTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ZoomOutSelectionMarkerTemplateProperty);
            set => 
                base.SetValue(ZoomOutSelectionMarkerTemplateProperty, value);
        }

        public Style ItemStyle
        {
            get => 
                (Style) base.GetValue(ItemStyleProperty);
            set => 
                base.SetValue(ItemStyleProperty, value);
        }

        public Style GroupItemStyle
        {
            get => 
                (Style) base.GetValue(GroupItemStyleProperty);
            set => 
                base.SetValue(GroupItemStyleProperty, value);
        }

        internal DevExpress.Xpf.Editors.RangeControl.Internal.LayoutInfo LayoutInfo { get; private set; }

        public double GroupingHeight
        {
            get => 
                (double) base.GetValue(GroupingHeightProperty);
            set => 
                base.SetValue(GroupingHeightProperty, value);
        }

        public bool AllowGrouping
        {
            get => 
                (bool) base.GetValue(AllowGroupingProperty);
            set => 
                base.SetValue(AllowGroupingProperty, value);
        }

        public object Start
        {
            get => 
                base.GetValue(StartProperty);
            private set => 
                base.SetValue(StartPropertyKey, value);
        }

        public object End
        {
            get => 
                base.GetValue(EndProperty);
            private set => 
                base.SetValue(EndPropertyKey, value);
        }

        public object SelectionStart
        {
            get => 
                base.GetValue(SelectionStartProperty);
            private set => 
                base.SetValue(SelectionStartPropertyKey, value);
        }

        public object SelectionEnd
        {
            get => 
                base.GetValue(SelectionEndProperty);
            private set => 
                base.SetValue(SelectionEndPropertyKey, value);
        }

        public object VisibleStart
        {
            get => 
                base.GetValue(VisibleStartProperty);
            private set => 
                base.SetValue(VisibleStartPropertyKey, value);
        }

        public object VisibleEnd
        {
            get => 
                base.GetValue(VisibleEndProperty);
            private set => 
                base.SetValue(VisibleEndPropertyKey, value);
        }

        public ObservableCollection<IntervalFactory> IntervalFactories
        {
            get => 
                (ObservableCollection<IntervalFactory>) base.GetValue(IntervalFactoriesProperty);
            set => 
                base.SetValue(IntervalFactoriesProperty, value);
        }

        internal ObservableCollection<IntervalFactory> ActualIntervalFactories
        {
            get => 
                this.actualIntervalFactories ?? this.GetDefaultFactories();
            set => 
                this.actualIntervalFactories = this.CoerceIntervalFactories(value);
        }

        public IntervalFactory ItemIntervalFactory
        {
            get => 
                (IntervalFactory) base.GetValue(ItemIntervalFactoryProperty);
            private set => 
                base.SetValue(ItemIntervalFactoryPropertyKey, value);
        }

        public IntervalFactory GroupIntervalFactory
        {
            get => 
                (IntervalFactory) base.GetValue(GroupIntervalFactoryProperty);
            private set => 
                base.SetValue(GroupIntervalFactoryPropertyKey, value);
        }

        protected IValueNormalizer ValueNormalizer
        {
            get
            {
                this.valueValueNormalizer ??= new DateTimeValueNormalizer();
                return this.valueValueNormalizer;
            }
        }

        protected IRangeControlClient Instance =>
            this;

        protected Rect RenderBounds { get; private set; }

        private int MinStepsNumber =>
            3;

        private double FontScaleFactor =>
            base.FontSize / 11.0;

        private Size ViewPort { get; set; }

        protected object ActualStart =>
            this.Start ?? this.GetItemIntervalFactory().Snap(this.GetDefaultValue());

        protected object ActualEnd =>
            this.End ?? this.GetNextValue(this.ActualStart);

        protected object ActualVisibleStart =>
            this.VisibleStart ?? this.ActualStart;

        protected object ActualVisibleEnd =>
            this.VisibleEnd ?? this.ActualEnd;

        Rect IRangeControlClient.ClientBounds =>
            this.clientBounds;

        bool IRangeControlClient.ConvergeThumbsOnZoomingOut =>
            true;

        bool IRangeControlClient.GrayOutNonSelectedRange =>
            false;

        bool IRangeControlClient.AllowThumbs =>
            false;

        bool IRangeControlClient.SnapSelectionToGrid =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CalendarClient.<>c <>9 = new CalendarClient.<>c();
            public static Func<IntervalFactory, IntervalFactory> <>9__116_0;
            public static Func<ObservableCollection<IntervalFactory>, bool> <>9__155_0;
            public static Func<ObservableCollection<IntervalFactory>, IntervalFactory> <>9__155_1;
            public static Func<ObservableCollection<IntervalFactory>, bool> <>9__169_0;
            public static Func<ObservableCollection<IntervalFactory>, IntervalFactory> <>9__169_1;
            public static Func<ObservableCollection<IntervalFactory>, bool> <>9__169_3;
            public static Func<ObservableCollection<IntervalFactory>, IntervalFactory> <>9__169_4;

            internal void <.cctor>b__28_0(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).OnItemStyleChanged();
            }

            internal void <.cctor>b__28_1(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).OnGroupItemStyleChanged();
            }

            internal void <.cctor>b__28_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalendarClient) d).OnCustomItemIntervalFactoryChanged();
            }

            internal void <.cctor>b__28_2(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).SelectionStartChanged(args.NewValue);
            }

            internal void <.cctor>b__28_3(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).SelectionEndChanged(args.NewValue);
            }

            internal void <.cctor>b__28_4(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).VisibleStartChanged(args.NewValue);
            }

            internal void <.cctor>b__28_5(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).VisibleEndChanged(args.NewValue);
            }

            internal void <.cctor>b__28_6(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).StartChanged(args.NewValue);
            }

            internal void <.cctor>b__28_7(DependencyObject owner, DependencyPropertyChangedEventArgs args)
            {
                ((CalendarClient) owner).EndChanged(args.NewValue);
            }

            internal void <.cctor>b__28_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalendarClient) d).OnIntervalFactoriesChanged((ObservableCollection<IntervalFactory>) e.OldValue);
            }

            internal void <.cctor>b__28_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CalendarClient) d).OnCustomGroupIntervalFactoryChanged();
            }

            internal bool <CorrectVisibleRange>b__169_0(ObservableCollection<IntervalFactory> x) => 
                x.Count > 0;

            internal IntervalFactory <CorrectVisibleRange>b__169_1(ObservableCollection<IntervalFactory> x) => 
                x.First<IntervalFactory>();

            internal bool <CorrectVisibleRange>b__169_3(ObservableCollection<IntervalFactory> x) => 
                x.Count > 0;

            internal IntervalFactory <CorrectVisibleRange>b__169_4(ObservableCollection<IntervalFactory> x) => 
                x.Last<IntervalFactory>();

            internal bool <GetFirstFactory>b__155_0(ObservableCollection<IntervalFactory> x) => 
                x.Count > 0;

            internal IntervalFactory <GetFirstFactory>b__155_1(ObservableCollection<IntervalFactory> x) => 
                x.First<IntervalFactory>();

            internal IntervalFactory <GetItemIntervalFactory>b__116_0(IntervalFactory x) => 
                x;
        }
    }
}

