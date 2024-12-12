namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtFConnectionRule : OfficeArtSolverContainerFileBlock
    {
        private int startShapeId;
        private int endShapeId;
        private int connectionShapeId;
        private int startShapeConnectionSiteId;
        private int endShapeConnectionSiteId;
        private const int RecordLength = 0x18;

        public OfficeArtFConnectionRule()
        {
        }

        public OfficeArtFConnectionRule(int ruleId, int startShapeId, int endShapeId, int connectionShapeId, int startShapeConnectionSiteId, int endShapeConnectionSiteId)
        {
            base.RuleId = ruleId;
            this.startShapeId = startShapeId;
            this.endShapeId = endShapeId;
            this.connectionShapeId = connectionShapeId;
            this.startShapeConnectionSiteId = startShapeConnectionSiteId;
            this.endShapeConnectionSiteId = endShapeConnectionSiteId;
        }

        protected internal override int GetSize() => 
            0x18;

        protected internal override void Read(BinaryReader reader)
        {
            base.RuleId = (int) reader.ReadUInt32();
            this.startShapeId = (int) reader.ReadUInt32();
            this.endShapeId = (int) reader.ReadUInt32();
            this.connectionShapeId = (int) reader.ReadUInt32();
            this.startShapeConnectionSiteId = (int) reader.ReadUInt32();
            this.endShapeConnectionSiteId = (int) reader.ReadUInt32();
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write((uint) base.RuleId);
            writer.Write((uint) this.StartShapeId);
            writer.Write((uint) this.EndShapeId);
            writer.Write((uint) this.ConnectionShapeId);
            writer.Write((uint) this.StartShapeConnectionSiteId);
            writer.Write((uint) this.EndShapeConnectionSiteId);
        }

        public override int HeaderVersion =>
            1;

        public override int HeaderTypeCode =>
            0xf012;

        public int StartShapeId =>
            this.startShapeId;

        public int EndShapeId =>
            this.endShapeId;

        public int ConnectionShapeId =>
            this.connectionShapeId;

        public int StartShapeConnectionSiteId =>
            this.startShapeConnectionSiteId;

        public int EndShapeConnectionSiteId =>
            this.endShapeConnectionSiteId;
    }
}

