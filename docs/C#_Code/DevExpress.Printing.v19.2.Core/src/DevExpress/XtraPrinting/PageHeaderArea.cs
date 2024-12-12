namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public class PageHeaderArea : PageArea
    {
        public PageHeaderArea()
        {
        }

        public PageHeaderArea(string[] content, Font font, BrickAlignment lineAlignment) : base(content, font, lineAlignment)
        {
        }
    }
}

