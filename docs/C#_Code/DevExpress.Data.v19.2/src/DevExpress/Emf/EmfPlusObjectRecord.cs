namespace DevExpress.Emf
{
    using System;

    public class EmfPlusObjectRecord : EmfPlusObjectRecordBase
    {
        private readonly EmfPlusObject value;

        public EmfPlusObjectRecord(byte id, EmfPlusObject obj) : base((short) (id | (((int) obj.Type) << 8)))
        {
            this.value = obj;
        }

        public EmfPlusObjectRecord(short flags, EmfPlusReader reader) : base(flags)
        {
            this.value = CreateObject(base.ObjectType, reader);
        }

        public override void Accept(IEmfMetafileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static EmfPlusObject CreateObject(EmfPlusObjectType type, EmfPlusReader reader)
        {
            switch (type)
            {
                case EmfPlusObjectType.ObjectTypeBrush:
                    return EmfPlusBrush.Create(reader);

                case EmfPlusObjectType.ObjectTypePen:
                    return new EmfPlusPen(reader);

                case EmfPlusObjectType.ObjectTypePath:
                    return new EmfPlusPath(reader);

                case EmfPlusObjectType.ObjectTypeRegion:
                    return new EmfPlusRegion(reader);

                case EmfPlusObjectType.ObjectTypeImage:
                    return new EmfPlusImage(reader);

                case EmfPlusObjectType.ObjectTypeFont:
                    return new EmfPlusFont(reader);

                case EmfPlusObjectType.ObjectTypeStringFormat:
                    return new EmfPlusStringFormat(reader);
            }
            return null;
        }

        public override void Write(EmfContentWriter writer)
        {
            base.Write(writer);
            this.value.Write(writer);
            int padding = GetPadding(this.value.Size);
            if (padding != 0)
            {
                writer.Write(new byte[padding]);
            }
        }

        public EmfPlusObject Value =>
            this.value;

        protected override EmfPlusRecordType Type =>
            EmfPlusRecordType.EmfPlusObject;

        protected override int DataSize
        {
            get
            {
                int size = this.value.Size;
                return (size + GetPadding(size));
            }
        }
    }
}

