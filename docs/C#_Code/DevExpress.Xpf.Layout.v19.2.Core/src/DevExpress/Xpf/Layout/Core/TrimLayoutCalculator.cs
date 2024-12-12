namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public class TrimLayoutCalculator : BaseLayoutCalculator
    {
        private double allElements;
        private double onlyCaptionImageAndControlBox;
        private double onlyControlBox;
        private double onlyCaptions;
        private double captionSpaces;
        private double pinnedLengths;

        protected override ITabHeaderLayoutResult CalcCore(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            bool isHorizontal = options.IsHorizontal;
            Size size = options.Size;
            Rect header = new Rect(0.0, 0.0, size.Width, size.Height);
            double length = TabHeaderHelper.GetLength(isHorizontal, size);
            double available = TabHeaderHelper.GetLength(isHorizontal, size) - this.pinnedLengths;
            if (this.allElements >= available)
            {
                if (this.onlyCaptionImageAndControlBox >= available)
                {
                    return ((this.onlyControlBox >= available) ? this.CalcOnlyVisibleElement(header, isHorizontal, headers, length) : this.CalcOnlyControlBoxes(header, isHorizontal, headers, this.onlyControlBox, available));
                }
                double scaleFactor = ((available - this.onlyCaptionImageAndControlBox) - this.captionSpaces) / this.onlyCaptions;
                return base.CalcScaledCaptions(header, isHorizontal, headers, scaleFactor);
            }
            if (options.IsAutoFill)
            {
                double scaleFactor = base.CalcScaleFactor(headers, options);
                if (scaleFactor > 1.0)
                {
                    return base.CalcScaledCaptions(header, options.IsHorizontal, headers, scaleFactor);
                }
            }
            return this.CalcRow(header, options.IsHorizontal, headers);
        }

        protected unsafe ITabHeaderLayoutResult CalcOnlyControlBoxes(Rect header, bool horz, ITabHeaderInfo[] headers, double onlyControlBox, double available)
        {
            double num = available - onlyControlBox;
            double offset = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < headers.Length; i++)
            {
                ITabHeaderInfo info = headers[i];
                if (!info.IsPinned)
                {
                    info.ShowCaption = false;
                    info.ShowCaptionImage = false;
                }
                Size size = TabHeaderHelper.GetSize(info, horz);
                if (info.IsSelected && !info.IsPinned)
                {
                    info.ShowCaption = true;
                    info.ShowCaptionImage = true;
                    if (horz)
                    {
                        Size* sizePtr1 = &size;
                        sizePtr1.Width += num;
                    }
                    else
                    {
                        Size* sizePtr2 = &size;
                        sizePtr2.Height += num;
                    }
                }
                info.Rect = TabHeaderHelper.Arrange(header, horz, offset, size);
                offset += TabHeaderHelper.GetLength(horz, size);
                num3 = Math.Max(num3, TabHeaderHelper.GetLength(!horz, size));
            }
            Size result = TabHeaderHelper.GetSize(horz, offset, num3);
            return new BaseLayoutCalculator.Result(result, base.JustifyRowHeaders(headers, horz, result));
        }

        protected ITabHeaderLayoutResult CalcOnlyVisibleElement(Rect header, bool horz, ITabHeaderInfo[] headers, double available)
        {
            int num5;
            double offset = 0.0;
            double num2 = 0.0;
            double num3 = available;
            int index = 0;
            while (true)
            {
                if (index >= headers.Length)
                {
                    num5 = 0;
                    break;
                }
                ITabHeaderInfo info = headers[index];
                if (info.IsPinned)
                {
                    Size size = TabHeaderHelper.GetSize(info, horz);
                    if (info.IsPinned && (info.PinLocation == TabHeaderPinLocation.Far))
                    {
                        num3 -= TabHeaderHelper.GetLength(horz, size);
                        info.Rect = TabHeaderHelper.Arrange(header, horz, num3, size);
                    }
                    else
                    {
                        info.Rect = TabHeaderHelper.Arrange(header, horz, offset, size);
                        offset += TabHeaderHelper.GetLength(horz, size);
                    }
                    num2 = Math.Max(num2, TabHeaderHelper.GetLength(!horz, size));
                }
                index++;
            }
            while (true)
            {
                while (true)
                {
                    if (num5 >= headers.Length)
                    {
                        if (base.rightAlignedItems.Count > 0)
                        {
                            offset = Math.Max(offset, CheckInfinity(available));
                        }
                        Size result = TabHeaderHelper.GetSize(horz, offset, num2);
                        return new BaseLayoutCalculator.Result(result, base.JustifyRowHeaders(headers, horz, result));
                    }
                    ITabHeaderInfo info = headers[num5];
                    if ((!info.IsPinned || (info.PinLocation != TabHeaderPinLocation.Far)) && !info.IsPinned)
                    {
                        info.ShowCaption = false;
                        info.ShowCaptionImage = false;
                        Size size = TabHeaderHelper.GetSize(info, horz);
                        if ((offset + TabHeaderHelper.GetLength(horz, size)) > num3)
                        {
                            if ((offset + TabHeaderHelper.GetLength(horz, info.ControlBox)) > num3)
                            {
                                info.IsVisible = false;
                                info.Rect = Rect.Empty;
                                break;
                            }
                            size = info.ControlBox;
                        }
                        info.Rect = TabHeaderHelper.Arrange(header, horz, offset, size);
                        offset += TabHeaderHelper.GetLength(horz, size);
                        num2 = Math.Max(num2, TabHeaderHelper.GetLength(!horz, size));
                    }
                    break;
                }
                num5++;
            }
        }

        protected override void OnPrepareHeader(ITabHeaderInfo info, ITabHeaderLayoutOptions options)
        {
            bool isHorizontal = options.IsHorizontal;
            Size size = options.Size;
            double length = TabHeaderHelper.GetLength(isHorizontal, info.DesiredSize);
            double num2 = TabHeaderHelper.GetLength(isHorizontal, info.CaptionText);
            double num3 = num2 + info.CaptionToControlBoxDistance;
            double num4 = TabHeaderHelper.GetLength(isHorizontal, info.CaptionImage) + info.CaptionImageToCaptionDistance;
            double num5 = TabHeaderHelper.GetLength(isHorizontal, info.ControlBox);
            info.ZIndex = info.IsPinned ? 2 : (info.IsSelected ? 1 : 0);
            if (info.IsPinned)
            {
                this.pinnedLengths += length;
            }
            else
            {
                this.allElements += length;
                this.onlyCaptions += num2;
                this.captionSpaces += info.CaptionToControlBoxDistance;
                this.onlyCaptionImageAndControlBox += length - num3;
                this.onlyControlBox += (length - num3) - num4;
            }
        }
    }
}

