namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    internal class CursorLocationHelper
    {
        private int width;
        private int height;

        public CursorLocationHelper(int w, int h)
        {
            this.width = w;
            this.height = h;
        }

        public Point CorrectPosition(Rect targetRect, MoveType type)
        {
            double x = targetRect.X;
            double y = targetRect.Y;
            switch (type)
            {
                case MoveType.Left:
                    x = targetRect.Left - this.width;
                    y = targetRect.Top - ((this.height - targetRect.Height) / 2.0);
                    break;

                case MoveType.Right:
                    x = targetRect.Right;
                    y = targetRect.Top - ((this.height - targetRect.Height) / 2.0);
                    break;

                case MoveType.Top:
                    x = targetRect.Left - ((this.width - targetRect.Width) / 2.0);
                    y = targetRect.Top - this.height;
                    break;

                case MoveType.Bottom:
                    x = targetRect.Left - ((this.width - targetRect.Width) / 2.0);
                    y = targetRect.Bottom;
                    break;

                default:
                    break;
            }
            return new Point(x, y);
        }
    }
}

