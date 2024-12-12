namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtFArcRule : OfficeArtSolverContainerFileBlock
    {
        private int arcShapeId;
        private const int RecordLength = 8;

        public OfficeArtFArcRule()
        {
        }

        public OfficeArtFArcRule(int ruleId, int arcShapeId)
        {
            base.RuleId = ruleId;
            this.arcShapeId = arcShapeId;
        }

        protected internal override int GetSize() => 
            8;

        protected internal override void Read(BinaryReader reader)
        {
            base.RuleId = (int) reader.ReadUInt32();
            this.arcShapeId = (int) reader.ReadUInt32();
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write((uint) base.RuleId);
            writer.Write((uint) this.ArcShapeId);
        }

        public override int HeaderVersion =>
            0;

        public override int HeaderTypeCode =>
            0xf014;

        public int ArcShapeId =>
            this.arcShapeId;
    }
}

