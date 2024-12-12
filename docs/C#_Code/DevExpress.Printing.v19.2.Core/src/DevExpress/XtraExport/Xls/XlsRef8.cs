namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsRef8
    {
        public override bool Equals(object obj)
        {
            XlsRef8 ref2 = obj as XlsRef8;
            return ((ref2 != null) ? ((this.FirstRowIndex == ref2.FirstRowIndex) && ((this.LastRowIndex == ref2.LastRowIndex) && ((this.FirstColumnIndex == ref2.FirstColumnIndex) && (this.LastColumnIndex == ref2.LastColumnIndex)))) : false);
        }

        public static XlsRef8 FromRange(XlCellRange range)
        {
            XlsRef8 ref2 = new XlsRef8 {
                FirstRowIndex = (range.TopLeft.Row == XlCellPosition.InvalidValue.Row) ? 0 : range.TopLeft.Row,
                LastRowIndex = (range.BottomRight.Row == XlCellPosition.InvalidValue.Row) ? 0xffff : range.BottomRight.Row,
                FirstColumnIndex = (range.TopLeft.Column == XlCellPosition.InvalidValue.Column) ? 0 : range.TopLeft.Column,
                LastColumnIndex = (range.BottomRight.Column == XlCellPosition.InvalidValue.Column) ? 0xff : range.BottomRight.Column
            };
            if (ref2.FirstRowIndex >= 0x10000)
            {
                return null;
            }
            if (ref2.FirstColumnIndex >= 0x100)
            {
                return null;
            }
            if (ref2.LastRowIndex >= 0x10000)
            {
                ref2.LastRowIndex = 0xffff;
            }
            if (ref2.LastColumnIndex >= 0x100)
            {
                ref2.LastColumnIndex = 0xff;
            }
            return ref2;
        }

        public static XlsRef8 FromStream(XlReader reader)
        {
            XlsRef8 ref2 = new XlsRef8();
            ref2.Read(reader);
            return ref2;
        }

        public override int GetHashCode() => 
            ((this.FirstRowIndex ^ this.LastRowIndex) ^ this.FirstColumnIndex) ^ this.LastColumnIndex;

        protected void Read(XlReader reader)
        {
            this.FirstRowIndex = reader.ReadUInt16();
            this.LastRowIndex = reader.ReadUInt16();
            this.FirstColumnIndex = Math.Min(reader.ReadUInt16(), 0xff);
            this.LastColumnIndex = Math.Min(reader.ReadUInt16(), 0xff);
        }

        internal void Union(XlsRef8 other)
        {
            if (other != null)
            {
                this.FirstColumnIndex = Math.Min(this.FirstColumnIndex, other.FirstColumnIndex);
                this.FirstRowIndex = Math.Min(this.FirstRowIndex, other.FirstRowIndex);
                this.LastColumnIndex = Math.Max(this.LastColumnIndex, other.LastColumnIndex);
                this.LastRowIndex = Math.Max(this.LastRowIndex, other.LastRowIndex);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.FirstRowIndex);
            writer.Write((ushort) this.LastRowIndex);
            writer.Write((ushort) this.FirstColumnIndex);
            writer.Write((ushort) this.LastColumnIndex);
        }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex { get; set; }

        public int FirstRowIndex { get; set; }

        public int LastRowIndex { get; set; }
    }
}

