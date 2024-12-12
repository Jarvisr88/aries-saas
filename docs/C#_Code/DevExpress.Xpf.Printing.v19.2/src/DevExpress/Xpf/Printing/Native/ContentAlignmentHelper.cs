namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Drawing;
    using System.Windows;

    public class ContentAlignmentHelper
    {
        public static ContentAlignment ContentAlignmentFromAlignments(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            ContentAlignment topLeft;
            if (verticalAlignment == VerticalAlignment.Top)
            {
                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        topLeft = ContentAlignment.TopLeft;
                        break;

                    case HorizontalAlignment.Center:
                        topLeft = ContentAlignment.TopCenter;
                        break;

                    case HorizontalAlignment.Right:
                        topLeft = ContentAlignment.TopRight;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
            else if (verticalAlignment == VerticalAlignment.Center)
            {
                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        topLeft = ContentAlignment.MiddleLeft;
                        break;

                    case HorizontalAlignment.Center:
                        topLeft = ContentAlignment.MiddleCenter;
                        break;

                    case HorizontalAlignment.Right:
                        topLeft = ContentAlignment.MiddleRight;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                if (verticalAlignment != VerticalAlignment.Bottom)
                {
                    throw new InvalidOperationException();
                }
                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        topLeft = ContentAlignment.BottomLeft;
                        break;

                    case HorizontalAlignment.Center:
                        topLeft = ContentAlignment.BottomCenter;
                        break;

                    case HorizontalAlignment.Right:
                        topLeft = ContentAlignment.BottomRight;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
            return topLeft;
        }

        public static HorizontalAlignment HorizontalAlignmentFromContentAlignment(ContentAlignment alignment)
        {
            HorizontalAlignment left;
            int num = (int) alignment;
            if ((num & 0x111) != 0)
            {
                left = HorizontalAlignment.Left;
            }
            else if ((num & 0x222) != 0)
            {
                left = HorizontalAlignment.Center;
            }
            else
            {
                if ((num & 0x444) == 0)
                {
                    throw new InvalidOperationException();
                }
                left = HorizontalAlignment.Right;
            }
            return left;
        }

        public static VerticalAlignment VerticalAlignmentFromContentAlignment(ContentAlignment alignment)
        {
            VerticalAlignment top;
            int num = (int) alignment;
            if ((num & 7) != 0)
            {
                top = VerticalAlignment.Top;
            }
            else if ((num & 0x70) != 0)
            {
                top = VerticalAlignment.Center;
            }
            else
            {
                if ((num & 0x700) == 0)
                {
                    throw new InvalidOperationException();
                }
                top = VerticalAlignment.Bottom;
            }
            return top;
        }
    }
}

