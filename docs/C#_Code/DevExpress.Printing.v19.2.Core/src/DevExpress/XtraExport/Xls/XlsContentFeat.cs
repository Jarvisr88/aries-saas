namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class XlsContentFeat : XlsContentFeatBase
    {
        private FutureRecordHeader header;
        private readonly List<XlsRef8> refs;

        protected XlsContentFeat()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x868;
            this.header = header1;
            this.refs = new List<XlsRef8>();
        }

        public override int GetSize() => 
            (this.header.GetSize() + 15) + (this.Refs.Count * 8);

        public override void Write(BinaryWriter writer)
        {
            this.header.Write(writer);
            writer.Write((ushort) base.FeatureType);
            writer.Write((byte) 0);
            writer.Write(0);
            int count = this.Refs.Count;
            writer.Write((ushort) count);
            writer.Write((base.FeatureType == XlsFeatureType.IgnoredErrors) ? 4 : 0);
            writer.Write((ushort) 0);
            for (int i = 0; i < count; i++)
            {
                this.Refs[i].Write(writer);
            }
        }

        public List<XlsRef8> Refs =>
            this.refs;

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;
    }
}

