namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    internal abstract class OfficeArtClientData : OfficeArtPartBase
    {
        protected OfficeArtClientData()
        {
        }

        protected internal override int GetSize() => 
            0;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.Flush();
                writer2.SetNextChunk(new XlsChunk(0x5d));
                this.WriteObjData(writer2);
                writer2.Flush();
                writer2.SetNextChunk(new XlsChunk(0xec));
            }
        }

        protected abstract void WriteObjData(BinaryWriter writer);

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf011;

        public override int HeaderVersion =>
            0;
    }
}

