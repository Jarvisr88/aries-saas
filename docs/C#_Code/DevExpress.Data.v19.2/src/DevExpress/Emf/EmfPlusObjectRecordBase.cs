namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusObjectRecordBase : EmfPlusRecord
    {
        private const int emfPlusContinuedObjectFlagMask = 0x8000;
        private const short emfPlusObjectTypeMask = 0x7f00;

        protected EmfPlusObjectRecordBase(short flags) : base(flags)
        {
        }

        public static EmfPlusObjectRecordBase Create(short flags, EmfPlusReader reader) => 
            ((flags & 0x8000) == 0) ? ((EmfPlusObjectRecordBase) new EmfPlusObjectRecord(flags, reader)) : ((EmfPlusObjectRecordBase) new EmfPlusContinuedObjectRecord(flags, reader));

        protected EmfPlusObjectType ObjectType =>
            (EmfPlusObjectType) ((base.Flags & 0x7f00) >> 8);
    }
}

