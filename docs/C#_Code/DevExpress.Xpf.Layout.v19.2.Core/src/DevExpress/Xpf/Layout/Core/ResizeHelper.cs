namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class ResizeHelper
    {
        private static Cursor[] SizingCursors;
        private static SizingAction[] SizingActions;

        static ResizeHelper()
        {
            Cursor[] cursorArray1 = new Cursor[13];
            cursorArray1[1] = Cursors.SizeWE;
            cursorArray1[2] = Cursors.SizeNS;
            cursorArray1[3] = Cursors.SizeNWSE;
            cursorArray1[4] = Cursors.SizeWE;
            cursorArray1[6] = Cursors.SizeNESW;
            cursorArray1[8] = Cursors.SizeNS;
            cursorArray1[9] = Cursors.SizeNESW;
            cursorArray1[12] = Cursors.SizeNWSE;
            SizingCursors = cursorArray1;
            SizingActions = new SizingAction[] { SizingAction.None, SizingAction.West, SizingAction.North, SizingAction.NorthWest, SizingAction.East, SizingAction.None, SizingAction.NorthEast, SizingAction.None, SizingAction.South, SizingAction.SouthWest, SizingAction.None, SizingAction.None, SizingAction.SouthEast };
        }

        public static BaseResizeInfo CalcResizeInfo(Rect rect, Point point) => 
            new ResizeInfo(rect, point);

        public static BaseResizeInfo CalcResizeInfo(Rect rect, Point point, SizingAction sa) => 
            (sa != SizingAction.None) ? new ResizeInfo(sa) : CalcResizeInfo(rect, point);

        public static Rect CalcResizing(Rect startRect, Point startPoint, Point screenPoint, Size minSize)
        {
            BaseResizeInfo info = CalcResizeInfo(startRect, startPoint);
            double num = info.Horizontal ? (screenPoint.X - startPoint.X) : 0.0;
            double num2 = info.Vertical ? (screenPoint.Y - startPoint.Y) : 0.0;
            bool changeLocationX = info.ChangeLocationX;
            bool changeLocationY = info.ChangeLocationY;
            double width = Math.Max(minSize.Width, startRect.Width + (changeLocationX ? -num : num));
            double height = Math.Max(minSize.Height, startRect.Height + (changeLocationY ? -num2 : num2));
            return new Rect(new Point(info.ChangeLocationX ? Math.Min((double) (startRect.Right - minSize.Width), (double) (startRect.X + num)) : startRect.X, info.ChangeLocationY ? Math.Min((double) (startRect.Bottom - minSize.Height), (double) (startRect.Y + num2)) : startRect.Y), new Size(width, height));
        }

        public static Rect CalcResizing(Rect startRect, Point startPoint, Point screenPoint, Size minSize, Size maxSize) => 
            CalcResizing(startRect, startPoint, screenPoint, minSize, maxSize, SizingAction.None);

        public static Rect CalcResizing(Rect startRect, Point startPoint, Point screenPoint, Size minSize, Size maxSize, SizingAction sa)
        {
            BaseResizeInfo info = CalcResizeInfo(startRect, startPoint, sa);
            double num = info.Horizontal ? (screenPoint.X - startPoint.X) : 0.0;
            double num2 = info.Vertical ? (screenPoint.Y - startPoint.Y) : 0.0;
            bool changeLocationX = info.ChangeLocationX;
            bool changeLocationY = info.ChangeLocationY;
            double num3 = Math.Max(minSize.Width, startRect.Width + (changeLocationX ? -num : num));
            double num4 = Math.Max(minSize.Height, startRect.Height + (changeLocationY ? -num2 : num2));
            if (MathHelper.IsConstraintValid(maxSize.Width))
            {
                num3 = Math.Min(num3, maxSize.Width);
            }
            if (MathHelper.IsConstraintValid(maxSize.Height))
            {
                num4 = Math.Min(num4, maxSize.Height);
            }
            num = -num3 + startRect.Width;
            num2 = -num4 + startRect.Height;
            return new Rect(new Point(info.ChangeLocationX ? Math.Min((double) (startRect.Right - minSize.Width), (double) (startRect.X + num)) : startRect.X, info.ChangeLocationY ? Math.Min((double) (startRect.Bottom - minSize.Height), (double) (startRect.Y + num2)) : startRect.Y), new Size(num3, num4));
        }

        public static Cursor GetResizeCursor(Rect rect, Point point) => 
            GetResizeCursor(rect, point, false);

        public static Cursor GetResizeCursor(Rect rect, Point point, bool rightToLeft)
        {
            BaseResizeInfo info = CalcResizeInfo(rect, point);
            int index = 0;
            if (info.HResizeType != ResizeType.None)
            {
                index |= (info.HResizeType == ResizeType.Left) ? (rightToLeft ? 4 : 1) : (rightToLeft ? 1 : 4);
            }
            if (info.VResizeType != ResizeType.None)
            {
                index |= (info.VResizeType == ResizeType.Top) ? 2 : 8;
            }
            return SizingCursors[index];
        }

        public static SizingAction GetSizingAction(Rect rect, Point point, bool rightToLeft)
        {
            BaseResizeInfo info = CalcResizeInfo(rect, point);
            int index = 0;
            if (info.HResizeType != ResizeType.None)
            {
                index |= (info.HResizeType == ResizeType.Left) ? (rightToLeft ? 4 : 1) : (rightToLeft ? 1 : 4);
            }
            if (info.VResizeType != ResizeType.None)
            {
                index |= (info.VResizeType == ResizeType.Top) ? 2 : 8;
            }
            return SizingActions[index];
        }

        private class ResizeInfo : BaseResizeInfo
        {
            private ResizeType hResize;
            private ResizeType vResize;
            private bool fHorizontal;
            private bool fVertical;
            private bool fChangeLocationX;
            private bool fChangeLocationY;
            private double corner;

            public ResizeInfo(SizingAction sa) : base(rect, point)
            {
                this.corner = 10.0;
                Rect rect = new Rect();
                Point point = new Point();
                this.vResize = sa.ToResizeType(false);
                this.hResize = sa.ToResizeType(true);
                this.fHorizontal = (this.HResizeType == ResizeType.Left) || (this.HResizeType == ResizeType.Right);
                this.fVertical = (this.VResizeType == ResizeType.Top) || (this.VResizeType == ResizeType.Bottom);
                this.fChangeLocationX = this.HResizeType == ResizeType.Left;
                this.fChangeLocationY = this.VResizeType == ResizeType.Top;
            }

            public ResizeInfo(Rect rect, Point pt) : base(rect, pt)
            {
                this.corner = 10.0;
                this.Calc(rect, pt);
            }

            private void Calc(Rect r, Point p)
            {
                this.hResize = ResizeType.None;
                this.vResize = ResizeType.None;
                bool flag = p.Y < (r.Top + (r.Height * 0.5));
                bool flag2 = p.X < (r.Left + (r.Width * 0.5));
                bool flag3 = (p.X < (r.Left + this.corner)) && (p.Y < (r.Top + this.corner));
                bool flag4 = (p.X > (r.Right - this.corner)) && (p.Y < (r.Top + this.corner));
                bool flag5 = (p.X > (r.Right - this.corner)) && (p.Y > (r.Bottom - this.corner));
                bool flag6 = (p.X < (r.Left + this.corner)) && (p.Y > (r.Bottom - this.corner));
                bool flag8 = (p.Y - p.X) < (r.Top - r.Left);
                bool flag9 = (p.Y + p.X) < (r.Top + r.Right);
                bool flag10 = (p.Y - p.X) > (r.Bottom - r.Right);
                bool flag11 = (p.Y + p.X) > (r.Bottom + r.Left);
                if (!(((flag3 | flag4) | flag5) | flag6))
                {
                    if ((flag8 & flag9) & flag)
                    {
                        this.vResize = ResizeType.Top;
                    }
                    if ((flag10 & flag11) && !flag)
                    {
                        this.vResize = ResizeType.Bottom;
                    }
                    if ((!flag8 && !flag11) & flag2)
                    {
                        this.hResize = ResizeType.Left;
                    }
                    if (!flag9 && (!flag10 && !flag2))
                    {
                        this.hResize = ResizeType.Right;
                    }
                }
                else
                {
                    if (flag3)
                    {
                        this.vResize = ResizeType.Top;
                        this.hResize = ResizeType.Left;
                    }
                    if (flag4)
                    {
                        this.vResize = ResizeType.Top;
                        this.hResize = ResizeType.Right;
                    }
                    if (flag5)
                    {
                        this.vResize = ResizeType.Bottom;
                        this.hResize = ResizeType.Right;
                    }
                    if (flag6)
                    {
                        this.vResize = ResizeType.Bottom;
                        this.hResize = ResizeType.Left;
                    }
                }
                this.fHorizontal = (this.HResizeType == ResizeType.Left) || (this.HResizeType == ResizeType.Right);
                this.fVertical = (this.VResizeType == ResizeType.Top) || (this.VResizeType == ResizeType.Bottom);
                this.fChangeLocationX = this.HResizeType == ResizeType.Left;
                this.fChangeLocationY = this.VResizeType == ResizeType.Top;
            }

            public override ResizeType HResizeType =>
                this.hResize;

            public override ResizeType VResizeType =>
                this.vResize;

            public override bool Horizontal =>
                this.fHorizontal;

            public override bool Vertical =>
                this.fVertical;

            public override bool ChangeLocationX =>
                this.fChangeLocationX;

            public override bool ChangeLocationY =>
                this.fChangeLocationY;
        }
    }
}

