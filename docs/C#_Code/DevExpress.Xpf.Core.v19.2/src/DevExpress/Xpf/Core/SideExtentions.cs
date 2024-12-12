namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class SideExtentions
    {
        public static HorizontalAlignment GetHorizontalAlignment(this Side side) => 
            (side.GetOrientation() != Orientation.Vertical) ? HorizontalAlignment.Stretch : (side.IsStart() ? HorizontalAlignment.Left : HorizontalAlignment.Right);

        public static Side GetOppositeSide(this Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return Side.LeftRight;

                case Side.Top:
                    return Side.Bottom;

                case Side.LeftRight:
                    return Side.Left;

                case Side.Bottom:
                    return Side.Top;
            }
            throw new Exception();
        }

        public static Orientation GetOrientation(this Side side) => 
            ((side == Side.Left) || (side == Side.LeftRight)) ? Orientation.Vertical : Orientation.Horizontal;

        public static VerticalAlignment GetVerticalAlignment(this Side side) => 
            (side.GetOrientation() != Orientation.Horizontal) ? VerticalAlignment.Stretch : (side.IsStart() ? VerticalAlignment.Top : VerticalAlignment.Bottom);

        public static bool IsEnd(this Side side) => 
            (side == Side.LeftRight) || (side == Side.Bottom);

        public static bool IsStart(this Side side) => 
            (side == Side.Left) || (side == Side.Top);
    }
}

