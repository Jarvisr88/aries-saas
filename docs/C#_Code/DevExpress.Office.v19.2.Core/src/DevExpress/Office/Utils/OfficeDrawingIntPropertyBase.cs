namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public abstract class OfficeDrawingIntPropertyBase : OfficeDrawingPropertyBase
    {
        private int value;

        protected OfficeDrawingIntPropertyBase()
        {
        }

        public override void Execute(OfficeArtPropertiesBase owner)
        {
        }

        protected bool GetFlag(int mask) => 
            (this.Value & mask) == mask;

        public override void Read(BinaryReader reader)
        {
            this.value = reader.ReadInt32();
        }

        protected void SetFlag(int mask, bool flag)
        {
            if (flag)
            {
                this.Value |= mask;
            }
            else
            {
                this.Value &= ~mask;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(OfficePropertiesFactory.GetOpcodeByType(base.GetType()));
            writer.Write(this.Value);
        }

        public int Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
    }
}

