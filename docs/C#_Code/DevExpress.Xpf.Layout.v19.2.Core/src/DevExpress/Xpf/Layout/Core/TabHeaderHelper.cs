namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public static class TabHeaderHelper
    {
        public static Rect Arrange(Rect rect, bool horz, double offset, Size size) => 
            new Rect(horz ? offset : rect.Left, horz ? rect.Top : offset, size.Width, size.Height);

        public static double GetLength(ITabHeaderInfo info, bool horz)
        {
            double length = GetLength(horz, info.DesiredSize);
            if (!info.ShowCaptionImage)
            {
                length -= GetLength(horz, info.CaptionImage) + info.CaptionImageToCaptionDistance;
            }
            if (!info.ShowCaption)
            {
                length -= GetLength(horz, info.CaptionText) + info.CaptionToControlBoxDistance;
            }
            if (length < 0.0)
            {
                length = 0.0;
            }
            return length;
        }

        public static double GetLength(bool horz, Rect rect) => 
            horz ? rect.Width : rect.Height;

        public static double GetLength(bool horz, Size size) => 
            horz ? size.Width : size.Height;

        public static Size GetScaledSize(ITabHeaderInfo info, bool horz, double factor, ref double summaryRounding)
        {
            double length = GetLength(horz, info.DesiredSize);
            if (length == 0.0)
            {
                return new Size();
            }
            double num2 = GetLength(horz, info.CaptionText);
            double num3 = RoundSummary(num2 * factor, ref summaryRounding);
            length = info.IsPinned ? length : Math.Max((double) (length + (num3 - num2)), (double) 0.0);
            return (horz ? new Size(length, info.DesiredSize.Height) : new Size(info.DesiredSize.Width, length));
        }

        public static Size GetSize(ITabHeaderInfo info, bool horz)
        {
            double length = GetLength(info, horz);
            return (horz ? new Size(length, info.DesiredSize.Height) : new Size(info.DesiredSize.Width, length));
        }

        public static Size GetSize(bool horz, double length, double dim) => 
            new Size(horz ? length : dim, horz ? dim : length);

        private static double RoundSummary(double value, ref double summaryRounding)
        {
            double num = value + summaryRounding;
            double num2 = (int) (num + 0.5);
            summaryRounding = num - num2;
            return num2;
        }
    }
}

