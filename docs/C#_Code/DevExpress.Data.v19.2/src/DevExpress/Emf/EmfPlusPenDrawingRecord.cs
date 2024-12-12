namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusPenDrawingRecord : EmfPlusRecord
    {
        protected EmfPlusPenDrawingRecord(short flags) : base(flags)
        {
        }

        public int PenId =>
            base.ObjectId;
    }
}

