namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class XETextBrickExporter : TextBrickExporter
    {
        protected override void SetStringFormatTabStops(BrickStyle style, Measurer measurer)
        {
            XETextBrick brick = base.Brick as XETextBrick;
            if ((brick != null) && (brick.FTabInterval != 0f))
            {
                float[] tabStops = new float[] { brick.FTabInterval };
                style.StringFormat.SetTabStops(tabStops);
            }
            else
            {
                float num = measurer.MeasureString("W", style.Font, (float) 0f, style.StringFormat.Value, GraphicsUnit.Document).Width * 1.5f;
                float[] tabStops = new float[] { num };
                style.StringFormat.SetTabStops(tabStops);
            }
        }
    }
}

