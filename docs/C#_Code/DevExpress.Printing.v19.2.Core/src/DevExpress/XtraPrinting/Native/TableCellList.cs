namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class TableCellList : BrickList
    {
        protected override unsafe RectangleF CalcBounds()
        {
            SizeF empty = SizeF.Empty;
            foreach (Brick brick in this)
            {
                RectangleF viewRectangle = brick.GetViewRectangle();
                SizeF size = viewRectangle.Size;
                empty.Height = Math.Max(empty.Height, size.Height);
                SizeF* efPtr1 = &empty;
                efPtr1.Width += size.Width;
            }
            return new RectangleF(PointF.Empty, empty);
        }
    }
}

