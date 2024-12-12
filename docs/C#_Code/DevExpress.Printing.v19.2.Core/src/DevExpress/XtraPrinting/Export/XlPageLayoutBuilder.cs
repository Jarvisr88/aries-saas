namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;

    public class XlPageLayoutBuilder : PageLayoutBuilder
    {
        private PointF offset;

        public XlPageLayoutBuilder(Page page, XlExportContext xlExportContext) : base(page, xlExportContext)
        {
            float num = GraphicsUnitConverter.DocToDip(base.Page.MarginsF.Left);
            float num2 = GraphicsUnitConverter.DocToDip(base.Page.MarginsF.Top);
            this.offset = new PointF(-num, HasMarginBrick(page.InnerBrickList, base.Page.MarginsF.Top) ? 0f : -num2);
        }

        internal override RectangleF GetCorrectClipRect(RectangleF clipRect)
        {
            clipRect.Offset(this.offset);
            return clipRect;
        }

        internal override BrickViewData[] GetData(Brick brick, RectangleF bounds, RectangleF clipRect)
        {
            bounds.Offset(this.offset);
            return base.exportContext.GetData(brick, bounds, clipRect);
        }

        private static bool HasMarginBrick(IList brickList, float topMarginHeight)
        {
            bool flag;
            if (brickList.Count == 0)
            {
                return false;
            }
            using (IEnumerator enumerator = brickList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BrickBase current = (BrickBase) enumerator.Current;
                        RectangleF initialRect = current.InitialRect;
                        if (topMarginHeight <= initialRect.Top)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }
    }
}

