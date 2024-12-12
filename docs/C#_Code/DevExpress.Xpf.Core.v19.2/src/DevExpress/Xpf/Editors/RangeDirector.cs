namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RangeDirector
    {
        private SparklinePointCollection pointsSortedByArgument;
        private InternalRange userArgumentRange;
        private InternalRange userValueRange;
        private ExtremePointIndexes extremeIndexes;
        private ISparklineRangeContainer container;

        public RangeDirector()
        {
        }

        public RangeDirector(ISparklineRangeContainer container)
        {
            this.container = container;
        }

        private InternalRange CalculateArgumentAutoRange()
        {
            int num = -1;
            int num3 = 0;
            while (true)
            {
                if (num3 < this.pointsSortedByArgument.Count)
                {
                    if ((this.pointsSortedByArgument[num3].Value < this.userValueRange.Min) || (this.pointsSortedByArgument[num3].Value > this.userValueRange.Max))
                    {
                        num3++;
                        continue;
                    }
                    num = num3;
                }
                if (num == -1)
                {
                    return new InternalRange(0.0, 0.0);
                }
                int num2 = -1;
                int num4 = this.pointsSortedByArgument.Count - 1;
                while (true)
                {
                    if (num4 >= 0)
                    {
                        if ((this.pointsSortedByArgument[num4].Value < this.userValueRange.Min) || (this.pointsSortedByArgument[num4].Value > this.userValueRange.Max))
                        {
                            num4--;
                            continue;
                        }
                        num2 = num4;
                    }
                    return new InternalRange(this.pointsSortedByArgument[num].Argument, this.pointsSortedByArgument[num2].Argument);
                }
            }
        }

        private void CalculateRanges(out InternalRange argumentRange, out InternalRange valueRange)
        {
            if (this.extremeIndexes.IsEmpty)
            {
                argumentRange = new InternalRange(0.0, 0.0);
                valueRange = new InternalRange(0.0, 0.0);
                argumentRange.SetScaleTypes(SparklineScaleType.Unknown);
                valueRange.SetScaleTypes(SparklineScaleType.Unknown);
            }
            else if (this.userArgumentRange.Auto && this.userValueRange.Auto)
            {
                argumentRange = new InternalRange(this.pointsSortedByArgument[this.extremeIndexes.Start].Argument, this.pointsSortedByArgument[this.extremeIndexes.End].Argument);
                valueRange = new InternalRange(this.pointsSortedByArgument[this.extremeIndexes.Min].Value, this.pointsSortedByArgument[this.extremeIndexes.Max].Value);
                argumentRange.SetScaleTypes(this.pointsSortedByArgument.ArgumentScaleType);
                valueRange.SetScaleTypes(this.pointsSortedByArgument.ValueScaleType);
            }
            else if (this.userArgumentRange.Auto)
            {
                argumentRange = this.CalculateArgumentAutoRange();
                valueRange = this.userValueRange;
                argumentRange.SetScaleTypes(this.pointsSortedByArgument.ArgumentScaleType);
            }
            else if (!this.userValueRange.Auto)
            {
                argumentRange = this.userArgumentRange;
                valueRange = this.userValueRange;
            }
            else
            {
                argumentRange = this.userArgumentRange;
                valueRange = this.CalculateValueAutoRange();
                valueRange.SetScaleTypes(this.pointsSortedByArgument.ValueScaleType);
            }
            argumentRange.Auto = this.userArgumentRange.Auto;
            valueRange.Auto = this.userValueRange.Auto;
        }

        public void CalculateRanges(SparklinePointCollection pointsSortedByArgument, ExtremePointIndexes extremePointIndexes, InternalRange userArgumentRange, InternalRange userValueRange)
        {
            InternalRange range;
            InternalRange range2;
            this.Initialize(pointsSortedByArgument, extremePointIndexes, userArgumentRange, userValueRange);
            this.CalculateRanges(out range, out range2);
            if (!range.IsEqual(this.ArgumentRange))
            {
                this.RaiseRangeChanged(true, range);
            }
            if (!range2.IsEqual(this.ValueRange))
            {
                this.RaiseRangeChanged(false, range2);
            }
            this.ArgumentRange = range;
            this.ValueRange = range2;
        }

        private InternalRange CalculateValueAutoRange()
        {
            SortedSparklinePointCollection pointsSortedByArgument;
            if (this.pointsSortedByArgument.Count == 0)
            {
                return new InternalRange(0.0, 0.0);
            }
            SparklinePoint point = new SparklinePoint(this.userArgumentRange.Min, 0.0);
            SparklinePoint point2 = new SparklinePoint(this.userArgumentRange.Max, 0.0);
            if (this.pointsSortedByArgument is SortedSparklinePointCollection)
            {
                pointsSortedByArgument = (SortedSparklinePointCollection) this.pointsSortedByArgument;
            }
            else
            {
                pointsSortedByArgument = new SortedSparklinePointCollection(new SparklinePointArgumentComparer(true));
                foreach (SparklinePoint point3 in this.pointsSortedByArgument)
                {
                    pointsSortedByArgument.Add(point3);
                }
            }
            int num = pointsSortedByArgument.BinarySearch(point);
            if (num < 0)
            {
                num = ~num;
            }
            if (num >= this.pointsSortedByArgument.Count)
            {
                return new InternalRange(0.0, 0.0);
            }
            int num2 = pointsSortedByArgument.BinarySearch(point2);
            if (num2 < 0)
            {
                num2 = ~num2;
                if (num2 == this.pointsSortedByArgument.Count)
                {
                    num2--;
                }
                if (num2 == 0)
                {
                    return new InternalRange(0.0, 0.0);
                }
            }
            InternalRange range = new InternalRange(this.pointsSortedByArgument[num].Value, this.pointsSortedByArgument[num2].Value);
            for (int i = num; i <= num2; i++)
            {
                range.Min = Math.Min(range.Min, this.pointsSortedByArgument[i].Value);
                range.Max = Math.Max(range.Max, this.pointsSortedByArgument[i].Value);
            }
            return range;
        }

        private void Initialize(SparklinePointCollection pointsSortedByArgument, ExtremePointIndexes extremePointIndexes, InternalRange userArgumentRange, InternalRange userValueRange)
        {
            this.pointsSortedByArgument = pointsSortedByArgument;
            this.userArgumentRange = userArgumentRange;
            this.userValueRange = userValueRange;
            this.extremeIndexes = extremePointIndexes;
        }

        private void RaiseRangeChanged(bool isArgumentRangeChanged, InternalRange newRange)
        {
            if (this.container != null)
            {
                if (isArgumentRangeChanged)
                {
                    this.container.RaiseArgumentRangeChanged(new SparklineRangeChangedEventArgs(newRange));
                }
                else
                {
                    this.container.RaiseValueRangeChanged(new SparklineRangeChangedEventArgs(newRange));
                }
            }
        }

        public InternalRange ArgumentRange { get; private set; }

        public InternalRange ValueRange { get; private set; }
    }
}

