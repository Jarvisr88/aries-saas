namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeArtFCalloutRule : OfficeArtSolverContainerFileBlock
    {
        private int calloutShapeId;
        private const int RecordLength = 8;

        public OfficeArtFCalloutRule()
        {
        }

        public OfficeArtFCalloutRule(int ruleId, int calloutShapeId)
        {
            base.RuleId = ruleId;
            this.calloutShapeId = calloutShapeId;
        }

        protected internal override int GetSize() => 
            8;

        protected internal override void Read(BinaryReader reader)
        {
            base.RuleId = (int) reader.ReadUInt32();
            this.calloutShapeId = (int) reader.ReadUInt32();
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write((uint) base.RuleId);
            writer.Write((uint) this.CalloutShapeId);
        }

        public override int HeaderVersion =>
            0;

        public override int HeaderTypeCode =>
            0xf017;

        public int CalloutShapeId =>
            this.calloutShapeId;
    }
}

