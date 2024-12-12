namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;

    public class TypedBinaryWriterEx : IDisposable
    {
        private BinaryWriter output;
        private ICustomObjectConverter customObjectConverter;
        private Dictionary<TypeCode, DXTypeCode> typeCodeTable = CreateTypeCodeTable();

        public TypedBinaryWriterEx(BinaryWriter output)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }
            this.output = output;
        }

        public virtual void Close()
        {
            this.Dispose(true);
        }

        protected internal virtual byte CreateTypeCodeByte(DXTypeCode sourceTypeCode, DXTypeCode destinationTypeCode) => 
            (sourceTypeCode != destinationTypeCode) ? ((byte) ((((int) sourceTypeCode) << 4) | destinationTypeCode)) : ((byte) (((int) sourceTypeCode) << 4));

        private static Dictionary<TypeCode, DXTypeCode> CreateTypeCodeTable() => 
            new Dictionary<TypeCode, DXTypeCode> { 
                [TypeCode.Empty] = DXTypeCode.Null,
                [TypeCode.Object] = DXTypeCode.Object,
                [TypeCode.DBNull] = DXTypeCode.DBNull,
                [TypeCode.Boolean] = DXTypeCode.Boolean,
                [TypeCode.Char] = DXTypeCode.Char,
                [TypeCode.SByte] = DXTypeCode.SByte,
                [TypeCode.Byte] = DXTypeCode.Byte,
                [TypeCode.Int16] = DXTypeCode.Int16,
                [TypeCode.UInt16] = DXTypeCode.UInt16,
                [TypeCode.Int32] = DXTypeCode.Int32,
                [TypeCode.UInt32] = DXTypeCode.UInt32,
                [TypeCode.Int64] = DXTypeCode.Int64,
                [TypeCode.UInt64] = DXTypeCode.UInt64,
                [TypeCode.Single] = DXTypeCode.Single,
                [TypeCode.Double] = DXTypeCode.Double,
                [TypeCode.Decimal] = DXTypeCode.Decimal,
                [TypeCode.DateTime] = DXTypeCode.DateTime,
                [TypeCode.String] = DXTypeCode.String
            };

        protected internal virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.output.Dispose();
            }
        }

        ~TypedBinaryWriterEx()
        {
            this.Dispose(false);
        }

        public virtual void Flush()
        {
            this.output.Flush();
        }

        protected internal virtual DXTypeCode GetTypeCode(object obj) => 
            (obj != null) ? this.GetTypeCode(obj.GetType()) : DXTypeCode.Null;

        protected internal virtual DXTypeCode GetTypeCode(Type type)
        {
            if (typeof(Enum).IsAssignableFrom(type))
            {
                return DXTypeCode.Enum;
            }
            DXTypeCode guid = this.typeCodeTable[DXTypeExtensions.GetTypeCode(type)];
            if (guid == DXTypeCode.Object)
            {
                if (type == typeof(Guid))
                {
                    guid = DXTypeCode.Guid;
                }
                else if (type == typeof(TimeSpan))
                {
                    guid = DXTypeCode.TimeSpan;
                }
                else if (type == typeof(byte[]))
                {
                    guid = DXTypeCode.ByteArray;
                }
            }
            return guid;
        }

        private static bool IsMscorlib(Assembly assembly) => 
            assembly.GetName().Name == "mscorlib";

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        protected internal virtual void WriteBoolean(bool val)
        {
            if (val)
            {
                this.output.Write(this.CreateTypeCodeByte(DXTypeCode.Boolean, DXTypeCode.SByte));
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(DXTypeCode.Boolean, DXTypeCode.Null));
            }
        }

        protected internal virtual void WriteByte(byte val)
        {
            this.output.Write(this.CreateTypeCodeByte(DXTypeCode.Byte, DXTypeCode.Byte));
            this.output.Write(val);
        }

        protected internal virtual void WriteByteArray(byte[] val)
        {
            this.output.Write((byte) 0x99);
            this.WriteInt32(val.Length);
            this.output.Write(val);
        }

        protected internal virtual void WriteChar(char val)
        {
            this.output.Write((byte) 0xcc);
            this.output.Write(val);
        }

        protected internal virtual void WriteDateTime(DateTime val)
        {
            this.WriteInt64Core(val.Ticks, DXTypeCode.DateTime);
        }

        protected internal virtual void WriteDBNull()
        {
            this.output.Write((byte) 0xdd);
        }

        protected internal virtual void WriteDecimal(decimal val)
        {
            if (decimal.Floor(val) != val)
            {
                this.output.Write(this.CreateTypeCodeByte(DXTypeCode.Decimal, DXTypeCode.Decimal));
                this.output.Write(val);
            }
            else if ((val >= 0M) && (val <= -1M))
            {
                this.WriteUInt64Core(decimal.ToUInt64(val), DXTypeCode.Decimal);
            }
            else if ((val < 0M) && (val >= -9223372036854775808M))
            {
                this.WriteInt64Core(decimal.ToInt64(val), DXTypeCode.Decimal);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(DXTypeCode.Decimal, DXTypeCode.Decimal));
                this.output.Write(val);
            }
        }

        protected internal virtual void WriteDouble(double val)
        {
            this.output.Write((byte) 170);
            this.output.Write(val);
        }

        protected internal virtual void WriteEnum(object val)
        {
            this.output.Write((byte) 0x66);
            this.WriteTypeName(val.GetType());
            this.WriteString(val.ToString());
        }

        protected internal virtual void WriteGuid(Guid val)
        {
            this.output.Write((byte) 0x88);
            this.output.Write(val.ToByteArray());
        }

        protected internal virtual void WriteInt16(short val)
        {
            this.WriteInt16Core(val, DXTypeCode.Int16);
        }

        protected internal virtual void WriteInt16Core(short val, DXTypeCode sourceTypeCode)
        {
            if (val >= 0)
            {
                if (val <= 0xff)
                {
                    this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Byte));
                    this.output.Write((byte) val);
                }
                else
                {
                    this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int16));
                    this.output.Write(val);
                }
            }
            else if (val >= -128)
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.SByte));
                this.output.Write((sbyte) val);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int16));
                this.output.Write(val);
            }
        }

        protected internal virtual void WriteInt32(int val)
        {
            this.WriteInt32Core(val, DXTypeCode.Int32);
        }

        protected internal virtual void WriteInt32Core(int val, DXTypeCode sourceTypeCode)
        {
            if (val >= 0)
            {
                if (val <= 0xffff)
                {
                    this.WriteUInt16Core((ushort) val, sourceTypeCode);
                }
                else
                {
                    this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int32));
                    this.output.Write(val);
                }
            }
            else if (val >= -32768)
            {
                this.WriteInt16Core((short) val, sourceTypeCode);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int32));
                this.output.Write(val);
            }
        }

        protected internal virtual void WriteInt64(long val)
        {
            this.WriteInt64Core(val, DXTypeCode.Int64);
        }

        protected internal virtual void WriteInt64Core(long val, DXTypeCode sourceTypeCode)
        {
            if (val >= 0L)
            {
                if (val <= 0xffffffffUL)
                {
                    this.WriteUInt32Core((uint) val, sourceTypeCode);
                }
                else
                {
                    this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int64));
                    this.output.Write(val);
                }
            }
            else if (val >= -2147483648L)
            {
                this.WriteInt32Core((int) val, sourceTypeCode);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Int64));
                this.output.Write(val);
            }
        }

        protected internal virtual void WriteNull()
        {
            this.output.Write((byte) 0);
        }

        public virtual void WriteObject(object obj)
        {
            DXTypeCode typeCode = this.GetTypeCode(obj);
            this.WriteTypedObject(typeCode, obj);
        }

        public virtual void WriteObject(Type type, object obj)
        {
            DXTypeCode @null = DXTypeCode.Null;
            if (obj != null)
            {
                @null = this.GetTypeCode(type);
            }
            this.WriteTypedObject(@null, obj);
        }

        protected internal virtual void WriteObjectCore(object val)
        {
            string serializedObject = TypeDescriptor.GetConverter(val.GetType()).ConvertToInvariantString(val);
            this.WriteObjectCore(val.GetType(), serializedObject);
        }

        private void WriteObjectCore(Type type, string serializedObject)
        {
            this.WriteObjectCore(type, serializedObject, false);
        }

        protected internal virtual void WriteObjectCore(Type type, string serializedObject, bool forceWriteTypeFullName)
        {
            this.output.Write((byte) 0xee);
            if (forceWriteTypeFullName)
            {
                this.WriteString(type.FullName);
            }
            else
            {
                this.WriteTypeName(type);
            }
            this.output.Write(serializedObject);
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteSByte(sbyte val)
        {
            this.output.Write(this.CreateTypeCodeByte(DXTypeCode.SByte, DXTypeCode.SByte));
            this.output.Write(val);
        }

        protected internal virtual void WriteSingle(float val)
        {
            this.output.Write((byte) 0xbb);
            this.output.Write(val);
        }

        protected internal virtual void WriteString(string val)
        {
            this.output.Write((byte) 0x77);
            this.output.Write(val);
        }

        protected internal virtual void WriteTimeSpan(TimeSpan val)
        {
            this.WriteInt64Core(val.Ticks, DXTypeCode.TimeSpan);
        }

        protected internal virtual void WriteTypedObject(DXTypeCode typeCode, object obj)
        {
            if (typeCode > DXTypeCode.ByteArray)
            {
                if (typeCode <= DXTypeCode.Single)
                {
                    if (typeCode == DXTypeCode.Double)
                    {
                        this.WriteDouble((double) obj);
                        return;
                    }
                    if (typeCode == DXTypeCode.Single)
                    {
                        this.WriteSingle((float) obj);
                        return;
                    }
                }
                else
                {
                    if (typeCode == DXTypeCode.Char)
                    {
                        this.WriteChar((char) obj);
                        return;
                    }
                    if (typeCode == DXTypeCode.DBNull)
                    {
                        this.WriteDBNull();
                        return;
                    }
                    if (typeCode != DXTypeCode.Object)
                    {
                    }
                }
            }
            else if (typeCode > DXTypeCode.Enum)
            {
                if (typeCode == DXTypeCode.String)
                {
                    this.WriteString((string) obj);
                    return;
                }
                if (typeCode == DXTypeCode.Guid)
                {
                    this.WriteGuid((Guid) obj);
                    return;
                }
                if (typeCode == DXTypeCode.ByteArray)
                {
                    this.WriteByteArray((byte[]) obj);
                    return;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case DXTypeCode.Null:
                        this.WriteNull();
                        return;

                    case DXTypeCode.SByte:
                        this.WriteSByte((sbyte) obj);
                        return;

                    case DXTypeCode.Byte:
                        this.WriteByte((byte) obj);
                        return;

                    case DXTypeCode.Int16:
                        this.WriteInt16((short) obj);
                        return;

                    case DXTypeCode.UInt16:
                        this.WriteUInt16((ushort) obj);
                        return;

                    case DXTypeCode.UInt32:
                        this.WriteUInt32((uint) obj);
                        return;

                    case DXTypeCode.Int64:
                        this.WriteInt64((long) obj);
                        return;

                    case DXTypeCode.UInt64:
                        this.WriteUInt64((ulong) obj);
                        return;

                    case DXTypeCode.Decimal:
                        this.WriteDecimal((decimal) obj);
                        return;

                    case DXTypeCode.DateTime:
                        this.WriteDateTime((DateTime) obj);
                        return;

                    case DXTypeCode.Boolean:
                        this.WriteBoolean((bool) obj);
                        return;

                    case DXTypeCode.TimeSpan:
                        this.WriteTimeSpan((TimeSpan) obj);
                        return;

                    case (DXTypeCode.Decimal | DXTypeCode.UInt16):
                    case (DXTypeCode.DateTime | DXTypeCode.UInt16):
                    case (DXTypeCode.Boolean | DXTypeCode.UInt16):
                        break;

                    case DXTypeCode.Int32:
                        this.WriteInt32((int) obj);
                        return;

                    default:
                        if (typeCode != DXTypeCode.Enum)
                        {
                            break;
                        }
                        this.WriteEnum(obj);
                        return;
                }
            }
            this.WriteObjectCore(obj);
        }

        protected internal virtual void WriteTypeName(Type type)
        {
            if ((type.Assembly != base.GetType().Assembly) && !IsMscorlib(type.Assembly))
            {
                this.WriteString(type.AssemblyQualifiedName);
            }
            else
            {
                this.WriteString(type.FullName);
            }
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt16(ushort val)
        {
            this.WriteUInt16Core(val, DXTypeCode.UInt16);
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt16Core(ushort val, DXTypeCode sourceTypeCode)
        {
            if (val <= 0xff)
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.Byte));
                this.output.Write((byte) val);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.UInt16));
                this.output.Write(val);
            }
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt32(uint val)
        {
            this.WriteUInt32Core(val, DXTypeCode.UInt32);
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt32Core(uint val, DXTypeCode sourceTypeCode)
        {
            if (val <= 0xffff)
            {
                this.WriteUInt16Core((ushort) val, sourceTypeCode);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.UInt32));
                this.output.Write(val);
            }
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt64(ulong val)
        {
            this.WriteUInt64Core(val, DXTypeCode.UInt64);
        }

        [CLSCompliant(false)]
        protected internal virtual void WriteUInt64Core(ulong val, DXTypeCode sourceTypeCode)
        {
            if (val <= 0xffffffffUL)
            {
                this.WriteUInt32Core((uint) val, sourceTypeCode);
            }
            else
            {
                this.output.Write(this.CreateTypeCodeByte(sourceTypeCode, DXTypeCode.UInt64));
                this.output.Write(val);
            }
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

        protected internal BinaryWriter Output =>
            this.output;
    }
}

