namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class RectF : RectFBase
    {
        public static RectangleF Align(RectangleF rect, RectangleF baseRect, BrickAlignment alignment, BrickAlignment lineAlignment) => 
            AlignVert(AlignHorz(rect, baseRect, alignment), baseRect, lineAlignment);

        public static RectangleF AlignHorz(RectangleF rect, RectangleF baseRect, BrickAlignment alignment)
        {
            switch (alignment)
            {
                case BrickAlignment.Near:
                    rect.X = baseRect.X;
                    break;

                case BrickAlignment.Center:
                {
                    RectangleF ef = rect;
                    rect = Center(rect, baseRect);
                    rect.Y = ef.Y;
                    break;
                }
                case BrickAlignment.Far:
                    rect.X = baseRect.Right - rect.Width;
                    break;

                default:
                    break;
            }
            return rect;
        }

        public static RectangleF AlignVert(RectangleF rect, RectangleF baseRect, BrickAlignment alignment)
        {
            switch (alignment)
            {
                case BrickAlignment.Near:
                    rect.Y = baseRect.Y;
                    break;

                case BrickAlignment.Center:
                {
                    RectangleF ef = rect;
                    rect = Center(rect, baseRect);
                    rect.X = ef.X;
                    break;
                }
                case BrickAlignment.Far:
                    rect.Y = baseRect.Bottom - rect.Height;
                    break;

                default:
                    break;
            }
            return rect;
        }

        public static bool Contains(RectangleF baseRect, RectangleF rect) => 
            (baseRect.X <= rect.X) && (((rect.X + rect.Width) <= (baseRect.X + baseRect.Width)) && ((baseRect.Y <= rect.Y) && ((rect.Y + rect.Height) <= (baseRect.Y + baseRect.Height))));

        public static RectangleF DeflateRect(RectangleF rect, MarginsF margins) => 
            RectangleF.FromLTRB(rect.Left + margins.Left, rect.Top + margins.Top, rect.Right - margins.Right, rect.Bottom - margins.Bottom);
    }
}

