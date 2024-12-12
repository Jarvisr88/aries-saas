namespace DevExpress.Emf
{
    using System;

    public class EmfPlusClearRecord : EmfPlusRecord
    {
        private readonly ARGBColor color;

        public EmfPlusClearRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.color = reader.ReadArgbColor();
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ARGBColor Color =>
            this.color;
    }
}

