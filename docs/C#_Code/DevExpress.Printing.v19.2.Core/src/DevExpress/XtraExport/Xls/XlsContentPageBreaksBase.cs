namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public abstract class XlsContentPageBreaksBase : XlsContentBase
    {
        private readonly IXlPageBreaks breaks;

        protected XlsContentPageBreaksBase(IXlPageBreaks breaks)
        {
            this.breaks = breaks;
        }

        public override int GetSize() => 
            (this.breaks.Count * 6) + 2;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            int count = this.breaks.Count;
            if (count > this.MaxCount)
            {
                count = this.MaxCount;
            }
            writer.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                writer.Write((ushort) this.breaks[i]);
                writer.Write((ushort) 0);
                writer.Write((ushort) this.EndValue);
            }
        }

        protected abstract int MaxCount { get; }

        protected abstract int EndValue { get; }
    }
}

