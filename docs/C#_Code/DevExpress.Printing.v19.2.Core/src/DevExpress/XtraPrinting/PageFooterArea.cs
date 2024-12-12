namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public class PageFooterArea : PageArea
    {
        public PageFooterArea()
        {
        }

        public PageFooterArea(string[] content, Font font, BrickAlignment lineAlignment) : base(content, font, lineAlignment)
        {
        }
    }
}

