namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DropTypeHelper
    {
        public static BaseDropInfo CalcCenterDropInfo(Rect rect, Point point) => 
            new DropInfo(rect, point);

        public static BaseDropInfo CalcCenterDropInfo(Rect rect, Point point, double centerZone) => 
            new DropInfo(rect, point, centerZone, true);

        public static BaseDropInfo CalcSideDropInfo(Rect rect, Point point, double sideZone) => 
            new DropInfo(rect, point, sideZone, false);

        public static DockType ToDockType(this DropType type)
        {
            DockType none = DockType.None;
            switch (type)
            {
                case DropType.Center:
                    none = DockType.Fill;
                    break;

                case DropType.Top:
                    none = DockType.Top;
                    break;

                case DropType.Bottom:
                    none = DockType.Bottom;
                    break;

                case DropType.Left:
                    none = DockType.Left;
                    break;

                case DropType.Right:
                    none = DockType.Right;
                    break;

                default:
                    break;
            }
            return none;
        }

        public static MoveType ToMoveType(this DropType type)
        {
            switch (type)
            {
                case DropType.Center:
                    return MoveType.InsideGroup;

                case DropType.Top:
                    return MoveType.Top;

                case DropType.Bottom:
                    return MoveType.Bottom;

                case DropType.Left:
                    return MoveType.Left;

                case DropType.Right:
                    return MoveType.Right;
            }
            return MoveType.None;
        }

        private class DropInfo : BaseDropInfo
        {
            private DropType dropTypeCore;
            private bool fHorizontal;
            private bool fVertical;
            private Rect dropRectCore;

            public DropInfo(Rect rect, Point pt) : base(rect, pt)
            {
                if (base.ItemRect.Contains(base.DropPoint))
                {
                    this.CalcWithCenterZone(base.ItemRect, base.DropPoint, 0.0);
                }
                else
                {
                    this.CalcWithCenterZone(base.ItemRect, base.DropPoint);
                }
            }

            public DropInfo(Rect rect, Point pt, double factor, bool useCenterZoneCalculation) : base(rect, pt)
            {
                if (useCenterZoneCalculation)
                {
                    this.CalcWithCenterZone(base.ItemRect, base.DropPoint, factor);
                }
                else
                {
                    this.CalcWithRectangularSideZones(base.ItemRect, base.DropPoint, factor);
                }
            }

            private static DropType CalcDropType(Rect r, Point p)
            {
                DropType none = DropType.None;
                double num = ((r.Width == 0.0) || (r.Height == 0.0)) ? 1.0 : (r.Height / r.Width);
                double num2 = r.Left + (r.Width / 2.0);
                double num3 = r.Top + (r.Height / 2.0);
                double num4 = p.X - num2;
                double num5 = p.Y - num3;
                bool flag2 = -num5 >= (num * Math.Abs(num4));
                bool flag3 = Math.Abs(num5) <= (num * num4);
                bool flag4 = num5 >= (num * Math.Abs(num4));
                if (Math.Abs(num5) <= (-num * num4))
                {
                    none = DropType.Left;
                }
                else if (flag2)
                {
                    none = DropType.Top;
                }
                else if (flag3)
                {
                    none = DropType.Right;
                }
                else if (flag4)
                {
                    none = DropType.Bottom;
                }
                return none;
            }

            private void CalcWithCenterZone(Rect r, Point p)
            {
                this.dropTypeCore = CalcDropType(r, p);
                switch (this.dropTypeCore)
                {
                    case DropType.Top:
                        this.dropRectCore = new Rect(r.Left, r.Top, r.Width, r.Height * 0.5);
                        break;

                    case DropType.Bottom:
                        this.dropRectCore = new Rect(r.Left, r.Top + (r.Height * 0.5), r.Width, r.Height * 0.5);
                        break;

                    case DropType.Left:
                        this.dropRectCore = new Rect(r.Left, r.Top, r.Width * 0.5, r.Height);
                        break;

                    case DropType.Right:
                        this.dropRectCore = new Rect(r.Left + (r.Width * 0.5), r.Top, r.Width * 0.5, r.Height);
                        break;

                    default:
                        break;
                }
                this.fHorizontal = (this.Type == DropType.Left) || (this.Type == DropType.Right);
                this.fVertical = (this.Type == DropType.Top) || (this.Type == DropType.Bottom);
            }

            private void CalcWithCenterZone(Rect r, Point p, double centerFactor)
            {
                this.dropTypeCore = DropType.None;
                if (r.Contains(p))
                {
                    if (centerFactor != 0.0)
                    {
                        Rect rect = new Rect(r.Left + ((r.Width * 0.5) * (1.0 - centerFactor)), r.Top + ((r.Height * 0.5) * (1.0 - centerFactor)), r.Width * centerFactor, r.Height * centerFactor);
                        if (rect.Contains(p))
                        {
                            this.dropTypeCore = DropType.Center;
                            this.dropRectCore = rect;
                            return;
                        }
                    }
                    this.CalcWithCenterZone(r, p);
                }
            }

            private void CalcWithRectangularSideZones(Rect r, Point p, double sideFactor)
            {
                this.dropTypeCore = DropType.None;
                if (r.Contains(p))
                {
                    double width = (r.Width * 0.5) * sideFactor;
                    Rect rect = new Rect(r.Left, r.Top, width, r.Height);
                    Rect rect2 = new Rect(r.Right - width, r.Top, width, r.Height);
                    this.fHorizontal = true;
                    if (rect.Contains(p))
                    {
                        this.dropTypeCore = DropType.Left;
                        this.dropRectCore = new Rect(r.Left, r.Top, r.Width * 0.5, r.Height);
                    }
                    else if (rect2.Contains(p))
                    {
                        this.dropTypeCore = DropType.Right;
                        this.dropRectCore = new Rect(r.Left + (r.Width * 0.5), r.Top, r.Width * 0.5, r.Height);
                    }
                    else
                    {
                        this.fHorizontal = false;
                        this.fVertical = true;
                        if (p.Y < (r.Top + (r.Height * 0.5)))
                        {
                            this.dropTypeCore = DropType.Top;
                            this.dropRectCore = new Rect(r.Left, r.Top, r.Width, r.Height * 0.5);
                        }
                        else
                        {
                            this.dropTypeCore = DropType.Bottom;
                            this.dropRectCore = new Rect(r.Left, r.Top + (r.Height * 0.5), r.Width, r.Height * 0.5);
                        }
                    }
                }
            }

            public override DropType Type =>
                this.dropTypeCore;

            public override bool Horizontal =>
                this.fHorizontal;

            public override bool Vertical =>
                this.fVertical;

            public override Rect DropRect =>
                this.dropRectCore;
        }
    }
}

