namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class EmfPlusPenOptionalData
    {
        public EmfPlusPenOptionalData(MetaReader reader, PenDataFlags penDataFlags, Pen pen)
        {
            if (penDataFlags.HasFlag(PenDataFlags.PenDataTransform))
            {
                pen.Transform = reader.ReadMatrix();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataStartCap))
            {
                pen.StartCap = (LineCap) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataEndCap))
            {
                pen.EndCap = (LineCap) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataJoin))
            {
                pen.LineJoin = (LineJoin) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataMiterLimit))
            {
                pen.MiterLimit = reader.ReadSingle();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataLineStyle))
            {
                pen.DashStyle = (DashStyle) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataDashedLineCap))
            {
                pen.DashCap = (DashCap) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataDashedLineOffset))
            {
                pen.DashOffset = reader.ReadSingle();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataDashedLine))
            {
                pen.DashPattern = new EmfPlusDashedLineData(reader).DashPattern;
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataNonCenter))
            {
                pen.Alignment = (PenAlignment) reader.ReadInt32();
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataCompoundLine))
            {
                pen.CompoundArray = new EmfPlusCompoundLineData(reader).CompoundLineData;
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataCustomStartCap))
            {
                EmfPlusCustomCapData data = new EmfPlusCustomCapData(reader);
                if (data.ArrowData != null)
                {
                    pen.CustomStartCap = this.CreateCustomArrowCap(data.ArrowData);
                }
                else
                {
                    pen.StartCap = LineCap.Flat;
                }
            }
            if (penDataFlags.HasFlag(PenDataFlags.PenDataCustomEndCap))
            {
                EmfPlusCustomCapData data2 = new EmfPlusCustomCapData(reader);
                if (data2.ArrowData != null)
                {
                    pen.CustomEndCap = this.CreateCustomArrowCap(data2.ArrowData);
                }
                else
                {
                    pen.EndCap = LineCap.Flat;
                }
            }
        }

        private AdjustableArrowCap CreateCustomArrowCap(EmfPlusCustomLineCapArrowData capData) => 
            new AdjustableArrowCap(capData.Width, capData.Height) { 
                WidthScale = capData.WidthScale,
                Filled = capData.FillState
            };
    }
}

