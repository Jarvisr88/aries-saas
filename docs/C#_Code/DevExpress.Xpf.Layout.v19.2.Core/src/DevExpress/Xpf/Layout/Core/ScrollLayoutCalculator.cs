namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScrollLayoutCalculator : BaseLayoutCalculator
    {
        private double pinnedLengths;

        protected override ITabHeaderLayoutResult CalcCore(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            Rect header = new Rect(0.0, 0.0, options.Size.Width, options.Size.Height);
            if (options.IsAutoFill)
            {
                double scaleFactor = base.CalcScaleFactor(headers, options);
                if (scaleFactor > 1.0)
                {
                    return base.CalcScaledCaptions(header, options.IsHorizontal, headers, scaleFactor);
                }
            }
            ITabHeaderLayoutResult result = this.CalcRow(header, options.IsHorizontal, headers);
            if (result.HasScroll)
            {
                result = new BaseLayoutCalculator.Result(result, this.ScrollRowHeaders(headers, options, result));
            }
            return result;
        }

        protected override ITabHeaderLayoutResult CalcRow(Rect row, bool horz, ITabHeaderInfo[] headers)
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
                Size size = TabHeaderHelper.GetSize(info, horz);
                if (!info.IsRightAligned())
                {
                    info.Rect = TabHeaderHelper.Arrange(row, horz, offset, size);
                }
                offset += TabHeaderHelper.GetLength(horz, size);
                num2 = Math.Max(num2, TabHeaderHelper.GetLength(!horz, size));
            }
            if (offset == 0.0)
            {
                return BaseLayoutCalculator.Result.Empty;
            }
            double num4 = offset - this.pinnedLengths;
            if (base.rightAlignedItems.Count > 0)
            {
                offset = Math.Max(offset, CheckInfinity(TabHeaderHelper.GetLength(horz, row)));
            }
            Size result = TabHeaderHelper.GetSize(horz, offset, num2);
            return new BaseLayoutCalculator.Result(result, ((headers.Length == 0) || (!MathHelper.IsZero(num4) || (base.noneAlignedItems.Count <= 0))) ? (num4 > (TabHeaderHelper.GetLength(horz, row) - this.pinnedLengths)) : true, base.JustifyRowHeaders(headers, horz, result));
        }

        private int CalcScrollIndex(ITabHeaderInfo[] headers, double scrollLength, int currentIndex, int optionsScrollIndex, bool horz, Viewport viewport)
        {
            Func<ITabHeaderInfo, bool> predicate = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<ITabHeaderInfo, bool> local1 = <>c.<>9__4_0;
                predicate = <>c.<>9__4_0 = x => !x.IsPinned;
            }
            List<ITabHeaderInfo> collection = headers.Where<ITabHeaderInfo>(predicate).ToList<ITabHeaderInfo>();
            if (collection.IsValidIndex<ITabHeaderInfo>(optionsScrollIndex))
            {
                optionsScrollIndex = collection[optionsScrollIndex].Index;
            }
            Rect rect = headers[currentIndex].Rect;
            switch (viewport.IntersectionResult(rect))
            {
                case IntersectionResult.NearEdgeIntersect:
                case IntersectionResult.Exclude:
                    return currentIndex;

                case IntersectionResult.FarEdgeIntersect:
                {
                    int num2 = optionsScrollIndex;
                    while (true)
                    {
                        num2++;
                        double num = 0.0;
                        int index = num2;
                        while (true)
                        {
                            if (index > currentIndex)
                            {
                                if (num > scrollLength)
                                {
                                    break;
                                }
                                return num2;
                            }
                            if (!headers[index].IsPinned)
                            {
                                num += TabHeaderHelper.GetLength(headers[index], horz);
                            }
                            index++;
                        }
                    }
                }
                case IntersectionResult.Include:
                    return optionsScrollIndex;
            }
            return -1;
        }

        protected override void OnPrepareHeader(ITabHeaderInfo info, ITabHeaderLayoutOptions options)
        {
            Size size = options.Size;
            double length = TabHeaderHelper.GetLength(options.IsHorizontal, info.DesiredSize);
            info.ZIndex = info.IsPinned ? 2 : (info.IsSelected ? 1 : 0);
            if (info.IsPinned)
            {
                this.pinnedLengths += length;
            }
        }

        protected unsafe IScrollResult ScrollRowHeaders(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options, ITabHeaderLayoutResult result)
        {
            bool isHorizontal = options.IsHorizontal;
            double scrollLength = TabHeaderHelper.GetLength(isHorizontal, options.Size) - this.pinnedLengths;
            double num2 = TabHeaderHelper.GetLength(isHorizontal, result.Size) - this.pinnedLengths;
            int num3 = 0;
            double length = 0.0;
            Func<ITabHeaderInfo, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ITabHeaderInfo, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = x => !x.IsPinned;
            }
            List<ITabHeaderInfo> source = headers.Where<ITabHeaderInfo>(predicate).ToList<ITabHeaderInfo>();
            double[] numArray = new double[source.Count<ITabHeaderInfo>()];
            int index = 0;
            while (true)
            {
                if (index < source.Count<ITabHeaderInfo>())
                {
                    numArray[index] = (index > 0) ? (numArray[index - 1] + length) : 0.0;
                    length = TabHeaderHelper.GetLength(isHorizontal, source[index].Rect);
                    if ((num2 - numArray[index]) > scrollLength)
                    {
                        num3++;
                        index++;
                        continue;
                    }
                }
                int num5 = Math.Max(0, Math.Min(num3, options.ScrollIndex));
                double num6 = (num5 == num3) ? (scrollLength - num2) : -numArray[num5];
                for (int i = 0; i < headers.Length; i++)
                {
                    ITabHeaderInfo info = headers[i];
                    Rect rect = info.Rect;
                    if (isHorizontal)
                    {
                        if (!info.IsPinned)
                        {
                            Rect* rectPtr1 = &(result.Headers[i]);
                            rectPtr1.X += num6;
                            Rect* rectPtr2 = &rect;
                            rectPtr2.X += num6;
                        }
                    }
                    else if (!info.IsPinned)
                    {
                        Rect* rectPtr3 = &(result.Headers[i]);
                        rectPtr3.Y += num6;
                        Rect* rectPtr4 = &rect;
                        rectPtr4.Y += num6;
                    }
                    info.Rect = rect;
                }
                double offset = options.Offset - num6;
                Viewport viewport = new Viewport(headers, scrollLength, isHorizontal);
                for (int j = 0; j < headers.Length; j++)
                {
                    ITabHeaderInfo info2 = headers[j];
                    info2.ScrollIndex = info2.IsPinned ? -1 : this.CalcScrollIndex(headers, scrollLength, j, options.ScrollIndex, options.IsHorizontal, viewport);
                }
                return new BaseLayoutCalculator.ScrollResult(num5, num3, offset);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollLayoutCalculator.<>c <>9 = new ScrollLayoutCalculator.<>c();
            public static Func<ITabHeaderInfo, bool> <>9__3_0;
            public static Func<ITabHeaderInfo, bool> <>9__4_0;

            internal bool <CalcScrollIndex>b__4_0(ITabHeaderInfo x) => 
                !x.IsPinned;

            internal bool <ScrollRowHeaders>b__3_0(ITabHeaderInfo x) => 
                !x.IsPinned;
        }

        private enum IntersectionResult
        {
            NearEdgeIntersect,
            FarEdgeIntersect,
            Include,
            Exclude
        }

        private class Viewport
        {
            private double nearEdge;
            private double farEdge;
            private bool isHorizontal;

            public Viewport(ITabHeaderInfo[] headers, double scrollLength, bool horz)
            {
                this.isHorizontal = horz;
                this.nearEdge = 0.0;
                for (int i = 0; i < headers.Length; i++)
                {
                    if (headers[i].IsPinned && !headers[i].IsRightAligned())
                    {
                        this.nearEdge += TabHeaderHelper.GetLength(headers[i], this.isHorizontal);
                    }
                }
                this.farEdge = this.nearEdge + scrollLength;
            }

            public DevExpress.Xpf.Layout.Core.ScrollLayoutCalculator.IntersectionResult IntersectionResult(Rect rect)
            {
                double item = this.isHorizontal ? rect.Left : rect.Top;
                double num2 = this.isHorizontal ? rect.Right : rect.Bottom;
                return (((item >= this.farEdge) || (num2 <= this.nearEdge)) ? DevExpress.Xpf.Layout.Core.ScrollLayoutCalculator.IntersectionResult.Exclude : (!item.IsBetween<double>(this.nearEdge, this.farEdge) ? DevExpress.Xpf.Layout.Core.ScrollLayoutCalculator.IntersectionResult.NearEdgeIntersect : (!num2.IsBetween<double>(this.nearEdge, this.farEdge) ? DevExpress.Xpf.Layout.Core.ScrollLayoutCalculator.IntersectionResult.FarEdgeIntersect : DevExpress.Xpf.Layout.Core.ScrollLayoutCalculator.IntersectionResult.Include)));
            }
        }
    }
}

