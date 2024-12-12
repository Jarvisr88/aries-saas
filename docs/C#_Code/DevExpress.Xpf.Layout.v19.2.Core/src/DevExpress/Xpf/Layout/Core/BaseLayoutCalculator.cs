namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BaseLayoutCalculator : ITabHeaderLayoutCalculator
    {
        protected List<ITabHeaderInfo> noneAlignedItems = new List<ITabHeaderInfo>();
        protected List<ITabHeaderInfo> rightAlignedItems = new List<ITabHeaderInfo>();

        protected BaseLayoutCalculator()
        {
        }

        public ITabHeaderLayoutResult Calc(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            this.PrepareHeaders(headers, options);
            return this.CalcCore(headers, options);
        }

        protected abstract ITabHeaderLayoutResult CalcCore(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options);
        protected virtual ITabHeaderLayoutResult CalcRow(Rect row, bool horz, ITabHeaderInfo[] headers)
        {
            double offset = 0.0;
            double num2 = 0.0;
            double length = TabHeaderHelper.GetLength(horz, row);
            for (int i = headers.Length - 1; i >= 0; i--)
            {
                ITabHeaderInfo info = headers[i];
                if (info.IsRightAligned())
                {
                    Size size = TabHeaderHelper.GetSize(info, horz);
                    length -= TabHeaderHelper.GetLength(horz, size);
                    info.Rect = TabHeaderHelper.Arrange(row, horz, length, size);
                    num2 = Math.Max(num2, TabHeaderHelper.GetLength(!horz, size));
                }
            }
            for (int j = 0; j < headers.Length; j++)
            {
                ITabHeaderInfo info = headers[j];
                if (!info.IsRightAligned())
                {
                    Size size = TabHeaderHelper.GetSize(info, horz);
                    info.Rect = TabHeaderHelper.Arrange(row, horz, offset, size);
                    offset += TabHeaderHelper.GetLength(horz, size);
                    num2 = Math.Max(num2, TabHeaderHelper.GetLength(!horz, size));
                }
            }
            if (offset == 0.0)
            {
                return Result.Empty;
            }
            if (this.rightAlignedItems.Count > 0)
            {
                offset = Math.Max(offset, CheckInfinity(TabHeaderHelper.GetLength(horz, row)));
            }
            Size result = TabHeaderHelper.GetSize(horz, offset, num2);
            return new Result(result, ((headers.Length == 0) || !MathHelper.IsZero(offset)) ? (offset > TabHeaderHelper.GetLength(horz, row)) : true, this.JustifyRowHeaders(headers, horz, result));
        }

        protected ITabHeaderLayoutResult CalcScaledCaptions(Rect header, bool horz, ITabHeaderInfo[] headers, double scaleFactor)
        {
            double summaryRounding = 0.0;
            double offset = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo info = headers[i];
                Size size = TabHeaderHelper.GetScaledSize(info, horz, scaleFactor, ref summaryRounding);
                info.Rect = TabHeaderHelper.Arrange(header, horz, offset, size);
                offset += TabHeaderHelper.GetLength(horz, size);
                num3 = Math.Max(num3, TabHeaderHelper.GetLength(!horz, size));
            }
            Size result = TabHeaderHelper.GetSize(horz, offset, num3);
            return new Result(result, this.JustifyRowHeaders(headers, horz, result));
        }

        protected double CalcScaleFactor(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            bool isHorizontal = options.IsHorizontal;
            double num4 = 0.0;
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo info = headers[i];
                if (info.IsPinned)
                {
                    num4 += TabHeaderHelper.GetLength(isHorizontal, info.DesiredSize);
                }
                else
                {
                    double length = TabHeaderHelper.GetLength(isHorizontal, info.DesiredSize);
                    if (length > 0.0)
                    {
                        double num8 = TabHeaderHelper.GetLength(isHorizontal, info.CaptionText);
                        double num10 = TabHeaderHelper.GetLength(isHorizontal, info.CaptionImage) + info.CaptionImageToCaptionDistance;
                        num2 += num8;
                        num3 += info.CaptionToControlBoxDistance;
                        num += length - (num8 + info.CaptionToControlBoxDistance);
                    }
                }
            }
            double d = (((TabHeaderHelper.GetLength(isHorizontal, options.Size) - num4) - num) - num3) / num2;
            return ((double.IsPositiveInfinity(d) || double.IsNaN(d)) ? 1.0 : d);
        }

        protected static double CheckInfinity(double pos) => 
            double.IsInfinity(pos) ? 0.0 : pos;

        protected Rect[] JustifyRowHeaders(ITabHeaderInfo[] headers, bool horz, Size size)
        {
            Rect[] rectArray = new Rect[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo info = headers[i];
                if (info.IsVisible)
                {
                    Rect rect = new Rect(CheckInfinity(info.Rect.Left), CheckInfinity(info.Rect.Top), horz ? info.Rect.Width : size.Width, horz ? size.Height : info.Rect.Height);
                    info.Rect = rect;
                    rectArray[i] = rect;
                }
            }
            return rectArray;
        }

        protected virtual void OnPrepareHeader(ITabHeaderInfo info, ITabHeaderLayoutOptions options)
        {
        }

        protected void PrepareHeaders(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo item = headers[i];
                item.IsVisible = true;
                item.ShowCaption = true;
                item.ShowCaptionImage = true;
                item.ZIndex = 0;
                item.ScrollIndex = -1;
                item.MultiLineResult = null;
                if (!item.IsPinned)
                {
                    this.noneAlignedItems.Add(item);
                }
                else if (item.IsRightAligned())
                {
                    this.rightAlignedItems.Add(item);
                }
                this.OnPrepareHeader(item, options);
            }
        }

        protected class Result : ITabHeaderLayoutResult
        {
            static Result()
            {
                Empty = new BaseLayoutCalculator.Result(System.Windows.Size.Empty, false, new Rect[0]);
            }

            public Result(ITabHeaderLayoutResult result, IScrollResult scrollResult)
            {
                this.Size = result.Size;
                this.HasScroll = result.HasScroll;
                this.Headers = result.Headers;
                this.ScrollResult = scrollResult;
            }

            public Result(System.Windows.Size result, Rect[] headers) : this(result, false, headers)
            {
            }

            public Result(System.Windows.Size result, bool hasScroll, Rect[] headers)
            {
                this.Size = result;
                this.HasScroll = hasScroll;
                this.Headers = headers;
                this.ScrollResult = null;
            }

            public System.Windows.Size Size { get; private set; }

            public bool HasScroll { get; private set; }

            public Rect[] Headers { get; private set; }

            public IScrollResult ScrollResult { get; private set; }

            public static BaseLayoutCalculator.Result Empty { get; private set; }

            public bool IsEmpty =>
                Equals(this, Empty);
        }

        protected class ScrollResult : IScrollResult
        {
            public ScrollResult(int index, int maxIndex, double offset)
            {
                this.Index = index;
                this.MaxIndex = maxIndex;
                this.CanScrollNext = index < maxIndex;
                this.CanScrollPrev = index > 0;
                this.ScrollOffset = offset;
            }

            public int Index { get; private set; }

            public int MaxIndex { get; private set; }

            public bool CanScrollPrev { get; private set; }

            public bool CanScrollNext { get; private set; }

            public double ScrollOffset { get; private set; }
        }
    }
}

