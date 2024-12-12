namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.IO;

    public class TypedBinaryReaderEx : IDisposable
    {
        private BinaryReader input;
        private ICustomObjectConverter customObjectConverter;

        public TypedBinaryReaderEx(BinaryReader input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.input = input;
        }

        public virtual void Close()
        {
            this.Dispose(true);
        }

        protected internal virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.input.Dispose();
            }
        }

        ~TypedBinaryReaderEx()
        {
            this.Dispose(false);
        }

        protected internal virtual bool ReadBoolean(DXTypeCode typeCode) => 
            typeCode != DXTypeCode.Null;

        protected internal virtual byte[] ReadByteArray()
        {
            int count = (int) this.ReadObject();
            return this.input.ReadBytes(count);
        }

        protected internal virtual DateTime ReadDateTime(DXTypeCode typeCode) => 
            new DateTime(Convert.ToInt64(this.ReadInteger(typeCode)));

        protected internal virtual decimal ReadDecimal(DXTypeCode typeCode) => 
            (typeCode != DXTypeCode.Decimal) ? Convert.ToDecimal(this.ReadInteger(typeCode)) : this.input.ReadDecimal();

        protected internal virtual object ReadEnum() => 
            Enum.Parse(Type.GetType((string) this.ReadObject()), (string) this.ReadObject(), false);

        protected internal virtual Guid ReadGuid() => 
            new Guid(this.input.ReadBytes(0x10));

        protected internal virtual object ReadInteger(DXTypeCode typeCode)
        {
            switch (typeCode)
            {
                case DXTypeCode.SByte:
                    return this.input.ReadSByte();

                case DXTypeCode.Byte:
                    return this.input.ReadByte();

                case DXTypeCode.Int16:
                    return this.input.ReadInt16();

                case DXTypeCode.UInt16:
                    return this.input.ReadUInt16();

                case DXTypeCode.UInt32:
                    return this.input.ReadUInt32();

                case DXTypeCode.Int64:
                    return this.input.ReadInt64();

                case DXTypeCode.UInt64:
                    return this.input.ReadUInt64();

                case DXTypeCode.Int32:
                    return this.input.ReadInt32();
            }
            throw new Exception();
        }

        public virtual object ReadObject()
        {
            byte typeCode = this.input.ReadByte();
            return this.ReadObjectCore(typeCode);
        }

        protected internal virtual object ReadObjectCore()
        {
            string typeName = (string) this.ReadObject();
            string str = this.input.ReadString();
            Type type = Type.GetType(typeName);
            if (!string.IsNullOrEmpty(typeName))
            {
                if (type != null)
                {
                    if (this.HasCustomObjectConverter)
                    {
                        if (type == this.CustomObjectConverter.GetType(typeName))
                        {
                            return this.CustomObjectConverter.FromString(type, str);
                        }
                    }
                    else
                    {
                        ObjectConverterImplementation instance = ObjectConverter.Instance;
                        if (type == instance.ResolveType(typeName))
                        {
                            return instance.ConvertFromString(type, str);
                        }
                    }
                }
                else if (this.HasCustomObjectConverter)
                {
                    type = this.CustomObjectConverter.GetType(typeName);
                    if (type != null)
                    {
                        return this.CustomObjectConverter.FromString(type, str);
                    }
                }
                else
                {
                    ObjectConverterImplementation instance = ObjectConverter.Instance;
                    type = instance.ResolveType(typeName);
                    if (type != null)
                    {
                        return instance.ConvertFromString(type, str);
                    }
                }
            }
            return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(str);
        }

        protected internal virtual object ReadObjectCore(byte typeCode)
        {
            DXTypeCode code = (DXTypeCode) typeCode;
            if (code <= DXTypeCode.Guid)
            {
                if (code <= DXTypeCode.Enum)
                {
                    if (code == DXTypeCode.Null)
                    {
                        return null;
                    }
                    if (code == DXTypeCode.Enum)
                    {
                        return this.ReadEnum();
                    }
                }
                else
                {
                    if (code == DXTypeCode.String)
                    {
                        return this.ReadString();
                    }
                    if (code == DXTypeCode.Guid)
                    {
                        return this.ReadGuid();
                    }
                }
            }
            else if (code <= DXTypeCode.Double)
            {
                if (code == DXTypeCode.ByteArray)
                {
                    return this.ReadByteArray();
                }
                if (code == DXTypeCode.Double)
                {
                    return this.input.ReadDouble();
                }
            }
            else
            {
                if (code == DXTypeCode.Single)
                {
                    return this.input.ReadSingle();
                }
                if (code == DXTypeCode.Char)
                {
                    return this.input.ReadChar();
                }
                if (code == DXTypeCode.DBNull)
                {
                    return DBNull.Value;
                }
            }
            return this.ReadObjectCoreDecodeTypeCode(typeCode);
        }

        protected internal virtual object ReadObjectCoreDecodeTypeCode(byte typeCode)
        {
            DXTypeCode code = (DXTypeCode) (typeCode >> 4);
            DXTypeCode code2 = ((DXTypeCode) typeCode) & DXTypeCode.Int32;
            if (code == DXTypeCode.Boolean)
            {
                return this.ReadBoolean(code2);
            }
            code2 ??= code;
            switch (code)
            {
                case DXTypeCode.SByte:
                    return this.input.ReadSByte();

                case DXTypeCode.Byte:
                    return this.input.ReadByte();

                case DXTypeCode.Int16:
                    return Convert.ToInt16(this.ReadInteger(code2));

                case DXTypeCode.UInt16:
                    return Convert.ToUInt16(this.ReadInteger(code2));

                case DXTypeCode.UInt32:
                    return Convert.ToUInt32(this.ReadInteger(code2));

                case DXTypeCode.Int64:
                    return Convert.ToInt64(this.ReadInteger(code2));

                case DXTypeCode.UInt64:
                    return Convert.ToUInt64(this.ReadInteger(code2));

                case DXTypeCode.Decimal:
                    return this.ReadDecimal(code2);

                case DXTypeCode.DateTime:
                    return this.ReadDateTime(code2);

                case DXTypeCode.Boolean:
                case (DXTypeCode.Decimal | DXTypeCode.UInt16):
                case (DXTypeCode.DateTime | DXTypeCode.UInt16):
                case (DXTypeCode.Boolean | DXTypeCode.UInt16):
                    break;

                case DXTypeCode.TimeSpan:
                    return this.ReadTimeSpan(code2);

                case DXTypeCode.Int32:
                    return Convert.ToInt32(this.ReadInteger(code2));

                default:
                    if (code != DXTypeCode.Object)
                    {
                    }
                    break;
            }
            return this.ReadObjectCore();
        }

        protected internal virtual string ReadString() => 
            this.input.ReadString();

        protected internal virtual TimeSpan ReadTimeSpan(DXTypeCode typeCode) => 
            TimeSpan.FromTicks(Convert.ToInt64(this.ReadInteger(typeCode)));

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        protected internal ICustomObjectConverter CustomObjectConverter
        {
            get => 
                this.customObjectConverter;
            set => 
                this.customObjectConverter = value;
        }

        protected bool HasCustomObjectConverter =>
            this.CustomObjectConverter != null;

        protected internal BinaryReader Input =>
            this.input;
    }
}

