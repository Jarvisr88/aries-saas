namespace DevExpress.Emf
{
    using System;

    public class EmfPlusResetWorldTransformRecord : EmfPlusRecord
    {
        public EmfPlusResetWorldTransformRecord(short flags, EmfPlusReader reader) : base(flags)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

