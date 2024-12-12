namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;

    public class EmfPlusPenData
    {
        public EmfPlusPenData(MetaReader reader, Pen pen)
        {
            PenDataFlags penDataFlags = (PenDataFlags) reader.ReadUInt32();
            GraphicsUnit unit = (GraphicsUnit) reader.ReadUInt32();
            pen.Width = reader.ReadSingle();
            EmfPlusPenOptionalData data1 = new EmfPlusPenOptionalData(reader, penDataFlags, pen);
        }
    }
}

