namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusStateControlRecord : EmfPlusRecord
    {
        private readonly int id;

        protected EmfPlusStateControlRecord(int id) : base(0)
        {
            this.id = id;
        }

        protected EmfPlusStateControlRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.id = reader.ReadInt32();
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            writer.Write(this.id);
        }

        public int Id =>
            this.id;

        protected override int DataSize =>
            4;
    }
}

