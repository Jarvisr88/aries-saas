namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDVal : XlsContentBase
    {
        private int xLeft;
        private int yTop;
        private int objId = -1;
        private int recordCount;

        public override int GetSize() => 
            0x12;

        public override void Read(XlReader reader, int size)
        {
            ushort num = reader.ReadUInt16();
            this.InputWindowClosed = (num & 1) != 0;
            this.xLeft = reader.ReadInt32();
            this.yTop = reader.ReadInt32();
            this.objId = reader.ReadInt32();
            this.recordCount = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.InputWindowClosed)
            {
                num = (ushort) (num | 1);
            }
            writer.Write(num);
            writer.Write(this.xLeft);
            writer.Write(this.yTop);
            writer.Write(this.objId);
            writer.Write(this.recordCount);
        }

        public bool InputWindowClosed { get; set; }

        public int XLeft
        {
            get => 
                this.xLeft;
            set
            {
                base.CheckValue(value, 0, 0xffff, "XLeft");
                this.xLeft = value;
            }
        }

        public int YTop
        {
            get => 
                this.yTop;
            set
            {
                base.CheckValue(value, 0, 0xffff, "YTop");
                this.yTop = value;
            }
        }

        public int ObjId
        {
            get => 
                this.objId;
            set
            {
                if (value != -1)
                {
                    base.CheckValue(value, 1, 0x7fff, "ObjId");
                }
                this.objId = value;
            }
        }

        public int RecordCount
        {
            get => 
                this.recordCount;
            set
            {
                base.CheckValue(value, 0, 0xfffe, "RecordCount");
                this.recordCount = value;
            }
        }
    }
}

