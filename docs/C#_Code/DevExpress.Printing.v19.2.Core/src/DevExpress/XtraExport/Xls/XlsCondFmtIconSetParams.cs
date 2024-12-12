namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtIconSetParams
    {
        private const int fixedPartSize = 6;
        private readonly List<XlsCondFmtIconThreshold> thresholds = new List<XlsCondFmtIconThreshold>();

        public int GetSize()
        {
            int num = 6;
            foreach (XlsCondFmtIconThreshold threshold in this.Thresholds)
            {
                num += threshold.GetSize();
            }
            return num;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0);
            writer.Write((byte) 0);
            int count = this.Thresholds.Count;
            writer.Write((byte) count);
            writer.Write(XlsCondFmtHelper.IconSetTypeToCode(this.IconSet));
            byte num2 = 0;
            if (this.IconsOnly)
            {
                num2 = (byte) (num2 | 1);
            }
            if (this.Reverse)
            {
                num2 = (byte) (num2 | 4);
            }
            writer.Write(num2);
            for (int i = 0; i < count; i++)
            {
                this.Thresholds[i].Write(writer);
            }
        }

        public XlCondFmtIconSetType IconSet { get; set; }

        public bool IconsOnly { get; set; }

        public bool Reverse { get; set; }

        public List<XlsCondFmtIconThreshold> Thresholds =>
            this.thresholds;
    }
}

