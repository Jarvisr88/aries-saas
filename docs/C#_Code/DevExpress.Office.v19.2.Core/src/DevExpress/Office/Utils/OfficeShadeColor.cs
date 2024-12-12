namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;
    using System.IO;

    public class OfficeShadeColor
    {
        public const int Size = 8;
        private OfficeColorRecord colorRecord;
        private double position;

        public OfficeShadeColor()
        {
            this.colorRecord = new OfficeColorRecord();
            this.position = 0.0;
        }

        public OfficeShadeColor(Color color, double position)
        {
            this.colorRecord = new OfficeColorRecord(color);
            this.Position = position;
        }

        public static OfficeShadeColor FromBytes(byte[] data, int offset)
        {
            OfficeShadeColor color = new OfficeShadeColor();
            color.ReadBytes(data, offset);
            return color;
        }

        public static OfficeShadeColor FromStream(BinaryReader reader)
        {
            OfficeShadeColor color = new OfficeShadeColor();
            color.Read(reader);
            return color;
        }

        protected void Read(BinaryReader reader)
        {
            this.colorRecord = OfficeColorRecord.FromStream(reader);
            this.position = FixedPoint.FromStream(reader).Value;
        }

        private void ReadBytes(byte[] data, int offset)
        {
            this.colorRecord = OfficeColorRecord.FromBytes(data, offset);
            this.position = FixedPoint.FromBytes(data, offset + 4).Value;
        }

        public void Write(BinaryWriter writer)
        {
            this.colorRecord.Write(writer);
            new FixedPoint { Value = this.position }.Write(writer);
        }

        public OfficeColorRecord ColorRecord
        {
            get => 
                this.colorRecord;
            set
            {
                value ??= new OfficeColorRecord();
                this.colorRecord = value;
            }
        }

        public double Position
        {
            get => 
                this.position;
            set
            {
                if ((value < 0.0) || (value > 1.0))
                {
                    throw new ArgumentOutOfRangeException("Position out of range 0.0...1.0!");
                }
                this.position = value;
            }
        }
    }
}

