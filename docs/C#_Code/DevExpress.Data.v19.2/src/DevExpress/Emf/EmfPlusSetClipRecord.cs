namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusSetClipRecord : EmfPlusRecord
    {
        private const short combineModeMask = 0xf00;

        protected EmfPlusSetClipRecord(EmfPlusCombineMode combineMode) : this(0, combineMode)
        {
        }

        protected EmfPlusSetClipRecord(short flags) : base(flags)
        {
        }

        protected EmfPlusSetClipRecord(byte objectId, EmfPlusCombineMode combineMode) : base((short) ((((int) combineMode) << 8) | objectId))
        {
        }

        public EmfPlusCombineMode CombineMode =>
            (EmfPlusCombineMode) ((base.Flags & 0xf00) >> 8);
    }
}

