namespace DevExpress.Emf
{
    using System;

    public class EmfPlusContinuedObjectRecord : EmfPlusObjectRecordBase
    {
        private readonly EmfPlusContinuedObject value;

        public EmfPlusContinuedObjectRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.value = new EmfPlusContinuedObject(base.ObjectType, reader.ReadInt32(), reader.ReadBytes(((int) reader.BaseStream.Length) - 4));
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public EmfPlusContinuedObject Value =>
            this.value;
    }
}

