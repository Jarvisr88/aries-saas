namespace DevExpress.Emf
{
    using System;

    public class EmfPlusHeaderRecord : EmfPlusRecord
    {
        public const int GraphicsVersion = -608169982;
        private const int dualModeFlagMask = 1;
        private readonly bool isVideoDisplay;
        private readonly int logicalDpiX;
        private readonly int logicalDpiY;

        public EmfPlusHeaderRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            reader.ReadInt32();
            this.isVideoDisplay = (reader.ReadInt32() & 1) != 0;
            this.logicalDpiX = reader.ReadInt32();
            this.logicalDpiY = reader.ReadInt32();
        }

        public EmfPlusHeaderRecord(int logicalDpiX, int logicalDpiY) : base(0)
        {
            this.logicalDpiX = logicalDpiX;
            this.logicalDpiY = logicalDpiY;
            this.isVideoDisplay = true;
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(-608169982);
            writer.Write(this.isVideoDisplay ? 1 : 0);
            writer.Write(this.logicalDpiX);
            writer.Write(this.logicalDpiY);
        }

        public bool IsDualModeFile =>
            (base.Flags & 1) != 0;

        public bool IsVideoDisplay =>
            this.isVideoDisplay;

        public int LogicalDpiX =>
            this.logicalDpiX;

        public int LogicalDpiY =>
            this.logicalDpiY;

        protected override int DataSize =>
            0x10;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusHeader;
    }
}

