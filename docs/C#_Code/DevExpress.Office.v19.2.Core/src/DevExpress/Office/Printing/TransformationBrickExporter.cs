namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class TransformationBrickExporter : PanelBrickExporter
    {
        public override void Draw(IGraphics gr, RectangleF rect, RectangleF parentRect)
        {
            GdiGraphics graphics = gr as GdiGraphics;
            if (graphics == null)
            {
                base.Draw(gr, rect, parentRect);
            }
            else
            {
                GdiGraphicsModifier service = ((IServiceProvider) graphics.PrintingSystem).GetService(typeof(GraphicsModifier)) as GdiGraphicsModifier;
                if (service == null)
                {
                    base.Draw(gr, rect, parentRect);
                }
                else
                {
                    service.SwitchToLayoutUnits(graphics.Graphics);
                    try
                    {
                        base.Draw(gr, rect, parentRect);
                    }
                    finally
                    {
                        service.SwitchToDocuments(graphics.Graphics);
                    }
                }
            }
        }
    }
}

