namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public static class SizingActionExtension
    {
        public static Cursor ToCursor(this SizingAction sa)
        {
            switch (sa)
            {
                case SizingAction.West:
                case SizingAction.East:
                    return Cursors.SizeWE;

                case SizingAction.North:
                case SizingAction.South:
                    return Cursors.SizeNS;

                case SizingAction.NorthWest:
                case SizingAction.SouthEast:
                    return Cursors.SizeNWSE;

                case SizingAction.NorthEast:
                case SizingAction.SouthWest:
                    return Cursors.SizeNESW;
            }
            return Cursors.None;
        }

        public static ResizeType ToResizeType(this SizingAction sa, bool isHorizontal)
        {
            ResizeType none = ResizeType.None;
            if (isHorizontal)
            {
                switch (sa)
                {
                    case SizingAction.West:
                    case SizingAction.NorthWest:
                    case SizingAction.SouthWest:
                        none = ResizeType.Left;
                        break;

                    case SizingAction.East:
                    case SizingAction.NorthEast:
                    case SizingAction.SouthEast:
                        none = ResizeType.Right;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (sa)
                {
                    case SizingAction.North:
                    case SizingAction.NorthWest:
                    case SizingAction.NorthEast:
                        none = ResizeType.Top;
                        break;

                    case SizingAction.South:
                    case SizingAction.SouthWest:
                    case SizingAction.SouthEast:
                        none = ResizeType.Bottom;
                        break;

                    default:
                        break;
                }
            }
            return none;
        }
    }
}

