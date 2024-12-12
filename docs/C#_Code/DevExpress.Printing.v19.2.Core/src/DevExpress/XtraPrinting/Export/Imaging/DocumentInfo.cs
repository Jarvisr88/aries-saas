namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class DocumentInfo
    {
        private int brickCount;
        private float leftOfBricks;
        private float rightOfBricks;
        private float bottomOfBricks;
        private float pageWidth;
        private float pagesHeight;
        private float borderWidth;

        public DocumentInfo()
        {
            this.Clear();
        }

        private void Clear()
        {
            this.brickCount = 0;
            this.leftOfBricks = float.MaxValue;
            this.rightOfBricks = 0f;
            this.bottomOfBricks = 0f;
            this.pageWidth = 0f;
            this.pagesHeight = 0f;
        }

        public void Update(List<BrickWithOffset> bricks)
        {
            this.Clear();
            foreach (BrickWithOffset offset in bricks)
            {
                this.UpdateContent(offset);
            }
        }

        public void Update(PageList pages, ImageExportOptions options)
        {
            this.borderWidth = (options.PageBorderWidth != 0) ? Math.Max(1f, (float) GraphicsUnitConverter.Convert(options.PageBorderWidth, (float) options.Resolution, 300f)) : 0f;
            foreach (int num2 in ExportOptionsHelper.GetPageIndices(options, pages.Count))
            {
                this.pageWidth = Math.Max(pages[num2].PageData.PageSize.Width + (2f * this.borderWidth), this.pageWidth);
                SizeF pageSize = pages[num2].PageData.PageSize;
                this.pagesHeight += pageSize.Height + this.borderWidth;
            }
            this.pagesHeight += this.borderWidth;
        }

        private void UpdateContent(BrickWithOffset brickInfo)
        {
            RectangleF rect = brickInfo.Rect;
            if (brickInfo.Brick is VisualBrick)
            {
                BrickStyle style = ((VisualBrick) brickInfo.Brick).Style;
                rect = style.InflateBorderWidth(rect, 300f, true, (BorderSide.Bottom | BorderSide.Right) & style.Sides);
            }
            this.leftOfBricks = Math.Min(this.leftOfBricks, rect.Left);
            this.rightOfBricks = Math.Max(this.rightOfBricks, rect.Right);
            this.bottomOfBricks = Math.Max(this.bottomOfBricks, rect.Bottom);
        }

        public float PageWidth =>
            this.pageWidth;

        public float PagesHeight =>
            this.pagesHeight;

        public int BrickCount =>
            this.brickCount;

        public float LeftOfBricks =>
            (this.leftOfBricks == float.MaxValue) ? 0f : this.leftOfBricks;

        public float RightOfBricks =>
            this.rightOfBricks;

        public float BottomOfBricks =>
            this.bottomOfBricks;

        public float BorderWidth =>
            this.borderWidth;
    }
}

