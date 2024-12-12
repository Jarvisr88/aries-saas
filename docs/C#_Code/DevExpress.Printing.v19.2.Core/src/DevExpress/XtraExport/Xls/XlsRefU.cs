namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsRefU
    {
        public override bool Equals(object obj)
        {
            XlsRefU fu = obj as XlsRefU;
            return ((fu != null) ? ((this.FirstRowIndex == fu.FirstRowIndex) && ((this.LastRowIndex == fu.LastRowIndex) && ((this.FirstColumnIndex == fu.FirstColumnIndex) && (this.LastColumnIndex == fu.LastColumnIndex)))) : false);
        }

        public static XlsRefU FromRange(XlCellRange range)
        {
            XlsRefU fu = new XlsRefU {
                FirstRowIndex = (range.TopLeft.Row == XlCellPosition.InvalidValue.Row) ? 0 : range.TopLeft.Row,
                LastRowIndex = (range.BottomRight.Row == XlCellPosition.InvalidValue.Row) ? 0xffff : range.BottomRight.Row,
                FirstColumnIndex = (range.TopLeft.Column == XlCellPosition.InvalidValue.Column) ? 0 : range.TopLeft.Column,
                LastColumnIndex = (range.BottomRight.Column == XlCellPosition.InvalidValue.Column) ? 0xff : range.BottomRight.Column
            };
            if (fu.FirstRowIndex >= 0x10000)
            {
                return null;
            }
            if (fu.FirstColumnIndex >= 0x100)
            {
                return null;
            }
            if (fu.LastRowIndex >= 0x10000)
            {
                fu.LastRowIndex = 0xffff;
            }
            if (fu.LastColumnIndex >= 0x100)
            {
                fu.LastColumnIndex = 0xff;
            }
            return fu;
        }

        public static XlsRefU FromStream(XlReader reader)
        {
            XlsRefU fu = new XlsRefU();
            fu.Read(reader);
            return fu;
        }

        public override int GetHashCode() => 
            ((this.FirstRowIndex ^ this.LastRowIndex) ^ this.FirstColumnIndex) ^ this.LastColumnIndex;

        protected void Read(XlReader reader)
        {
            this.FirstRowIndex = reader.ReadUInt16();
            this.LastRowIndex = reader.ReadUInt16();
            this.FirstColumnIndex = reader.ReadByte();
            this.LastColumnIndex = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.FirstRowIndex);
            writer.Write((ushort) this.LastRowIndex);
            writer.Write((byte) this.FirstColumnIndex);
            writer.Write((byte) this.LastColumnIndex);
        }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex { get; set; }

        public int FirstRowIndex { get; set; }

        public int LastRowIndex { get; set; }

        internal int CellCount =>
            ((this.LastColumnIndex - this.FirstColumnIndex) + 1) * ((this.LastRowIndex - this.FirstRowIndex) + 1);
    }
}

