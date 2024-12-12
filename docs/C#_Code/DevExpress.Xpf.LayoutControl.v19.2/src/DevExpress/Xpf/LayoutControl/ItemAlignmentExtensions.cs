namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ItemAlignmentExtensions
    {
        public static HorizontalAlignment GetHorizontalAlignment(this ItemAlignment alignment)
        {
            switch (alignment)
            {
                case ItemAlignment.Start:
                    return HorizontalAlignment.Left;

                case ItemAlignment.Center:
                    return HorizontalAlignment.Center;

                case ItemAlignment.End:
                    return HorizontalAlignment.Right;

                case ItemAlignment.Stretch:
                    return HorizontalAlignment.Stretch;
            }
            return HorizontalAlignment.Left;
        }

        public static ItemAlignment GetItemAlignment(this HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    return ItemAlignment.Start;

                case HorizontalAlignment.Center:
                    return ItemAlignment.Center;

                case HorizontalAlignment.Right:
                    return ItemAlignment.End;

                case HorizontalAlignment.Stretch:
                    return ItemAlignment.Stretch;
            }
            return ItemAlignment.Start;
        }

        public static ItemAlignment GetItemAlignment(this VerticalAlignment alignment)
        {
            switch (alignment)
            {
                case VerticalAlignment.Top:
                    return ItemAlignment.Start;

                case VerticalAlignment.Center:
                    return ItemAlignment.Center;

                case VerticalAlignment.Bottom:
                    return ItemAlignment.End;

                case VerticalAlignment.Stretch:
                    return ItemAlignment.Stretch;
            }
            return ItemAlignment.Start;
        }

        public static VerticalAlignment GetVerticalAlignment(this ItemAlignment alignment)
        {
            switch (alignment)
            {
                case ItemAlignment.Start:
                    return VerticalAlignment.Top;

                case ItemAlignment.Center:
                    return VerticalAlignment.Center;

                case ItemAlignment.End:
                    return VerticalAlignment.Bottom;

                case ItemAlignment.Stretch:
                    return VerticalAlignment.Stretch;
            }
            return VerticalAlignment.Top;
        }
    }
}

