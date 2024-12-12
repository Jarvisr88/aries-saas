namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class OfficeDrawingFixedPointPropertyBase : OfficeDrawingPropertyBase
    {
        private double value;

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }

        public override void Read(BinaryReader reader)
        {
            this.value = FixedPoint.FromStream(reader).Value;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(OfficePropertiesFactory.GetOpcodeByType(base.GetType()));
            new FixedPoint { Value = this.Value }.Write(writer);
        }

        public double Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }

        public override bool Complex =>
            true;
    }
}

