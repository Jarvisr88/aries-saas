namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public abstract class OfficeDrawingColorPropertyBase : OfficeDrawingPropertyBase
    {
        private OfficeColorRecord colorRecord = new OfficeColorRecord(DXColor.Empty);

        protected OfficeDrawingColorPropertyBase()
        {
        }

        public override void Read(BinaryReader reader)
        {
            this.ColorRecord = OfficeColorRecord.FromStream(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(OfficePropertiesFactory.GetOpcodeByType(base.GetType()));
            this.ColorRecord.Write(writer);
        }

        public OfficeColorRecord ColorRecord
        {
            get => 
                this.colorRecord;
            set => 
                this.colorRecord = value;
        }
    }
}

