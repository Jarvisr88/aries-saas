namespace DevExpress.Emf
{
    using System;

    public abstract class EmfPlusModifyWorldTransform : EmfPlusRecord
    {
        protected EmfPlusModifyWorldTransform() : base(0)
        {
        }

        protected EmfPlusModifyWorldTransform(short flags) : base(flags)
        {
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsPostMultiplied =>
            (base.Flags & 0x2000) != 0;

        public abstract DXTransformationMatrix Matrix { get; }
    }
}

