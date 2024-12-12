namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtShapeRecord : OfficeArtPartBase
    {
        private int shapeTypeCode;
        private int shapeId;

        public OfficeArtShapeRecord(int shapeTypeCode, int shapeId, int flags)
        {
            this.shapeTypeCode = shapeTypeCode;
            this.shapeId = shapeId;
            this.flags = flags;
        }

        protected internal override int GetSize() => 
            8;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.shapeId);
            writer.Write(this.flags);
        }

        private int flags { get; set; }

        public override int HeaderInstanceInfo =>
            this.shapeTypeCode;

        public override int HeaderTypeCode =>
            0xf00a;

        public override int HeaderVersion =>
            2;
    }
}

