namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.IO;

    internal class OfficeArtClientAnchorSheet : OfficeArtPartBase
    {
        private XlDrawingObjectBase drawingObject;

        public OfficeArtClientAnchorSheet(XlDrawingObjectBase drawingObject)
        {
            this.drawingObject = drawingObject;
        }

        private short GetOffsetValue(float value, int factor)
        {
            int num = (int) (value * factor);
            num = Math.Min(factor, num);
            return (short) Math.Max(-factor, num);
        }

        protected internal override int GetSize() => 
            0x12;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.DrawingObject.AnchorBehavior == XlAnchorType.Absolute)
            {
                num = (ushort) (num | 1);
            }
            if (this.DrawingObject.AnchorBehavior != XlAnchorType.TwoCell)
            {
                num = (ushort) (num | 2);
            }
            writer.Write(num);
            writer.Write((ushort) this.DrawingObject.TopLeft.Column);
            writer.Write(this.GetOffsetValue(this.DrawingObject.TopLeft.RelativeColumnOffset, 0x400));
            writer.Write((ushort) this.DrawingObject.TopLeft.Row);
            writer.Write(this.GetOffsetValue(this.DrawingObject.TopLeft.RelativeRowOffset, 0x100));
            writer.Write((ushort) this.DrawingObject.BottomRight.Column);
            writer.Write(this.GetOffsetValue(this.DrawingObject.BottomRight.RelativeColumnOffset, 0x400));
            writer.Write((ushort) this.DrawingObject.BottomRight.Row);
            writer.Write(this.GetOffsetValue(this.DrawingObject.BottomRight.RelativeRowOffset, 0x100));
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf010;

        public override int HeaderVersion =>
            0;

        public XlDrawingObjectBase DrawingObject =>
            this.drawingObject;
    }
}

