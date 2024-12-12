namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;

    public class HdcDpiToDocuments : HdcDpiModifier
    {
        public HdcDpiToDocuments(Graphics gr, Size viewPort) : base(gr, viewPort, 300)
        {
        }
    }
}

