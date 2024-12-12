namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class EmfPlusCustomCapData
    {
        public EmfPlusCustomCapData(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            long position = reader.BaseStream.Position;
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            this.Type = (CustomLineCapDataType) reader.ReadInt32();
            if (this.Type == CustomLineCapDataType.CustomLineCapDataTypeAdjustableArrow)
            {
                this.ArrowData = new EmfPlusCustomLineCapArrowData(reader);
            }
            else if (this.Type == CustomLineCapDataType.CustomLineCapDataTypeDefault)
            {
                this.CustomData = new EmfPlusCustomLineCapData(reader);
            }
        }

        public CustomLineCapDataType Type { get; set; }

        public EmfPlusCustomLineCapArrowData ArrowData { get; set; }

        public EmfPlusCustomLineCapData CustomData { get; set; }
    }
}

