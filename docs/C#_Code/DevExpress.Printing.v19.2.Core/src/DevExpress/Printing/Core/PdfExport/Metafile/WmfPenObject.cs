namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class WmfPenObject
    {
        public void Read(MetaReader reader)
        {
            PenStyle style = (PenStyle) reader.ReadUInt16();
            Color color = reader.ReadColorRGB();
            this.Pen = new System.Drawing.Pen(color, (float) reader.ReadPointXY().X);
            if (style > PenStyle.PS_ENDCAP_SQUARE)
            {
                if ((style != PenStyle.PS_ENDCAP_FLAT) && (style != PenStyle.PS_JOIN_BEVEL))
                {
                    PenStyle style2 = style;
                }
            }
            else
            {
                switch (style)
                {
                    case PenStyle.PS_COSMETIC:
                        this.Pen.DashStyle = DashStyle.Solid;
                        return;

                    case PenStyle.PS_DASH:
                        this.Pen.DashStyle = DashStyle.Dash;
                        return;

                    case PenStyle.PS_DOT:
                        this.Pen.DashStyle = DashStyle.Dot;
                        return;

                    case PenStyle.PS_DASHDOT:
                        this.Pen.DashStyle = DashStyle.DashDot;
                        return;

                    case PenStyle.PS_DASHDOTDOT:
                        this.Pen.DashStyle = DashStyle.DashDotDot;
                        return;

                    case PenStyle.PS_NULL:
                        this.Pen.Color = Color.Transparent;
                        break;

                    case PenStyle.PS_INSIDEFRAME:
                    case PenStyle.PS_USERSTYLE:
                    case PenStyle.PS_ALTERNATE:
                        break;

                    default:
                    {
                        PenStyle style1 = style;
                        return;
                    }
                }
            }
        }

        public System.Drawing.Pen Pen { get; set; }
    }
}

