namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using DevExpress.Xpo.DB.Exceptions;
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [Serializable]
    public class DBColumn
    {
        [XmlAttribute]
        public DBColumnType ColumnType;
        [XmlAttribute]
        public string Name;
        [XmlAttribute, DefaultValue(0)]
        public int Size;
        [XmlAttribute, DefaultValue(false)]
        public bool IsKey;
        [XmlAttribute, DefaultValue(false)]
        public bool IsIdentity;
        [XmlAttribute, DefaultValue(true)]
        public bool IsNullable;
        [XmlElement, DefaultValue((string) null)]
        public object DefaultValue;
        [XmlAttribute, DefaultValue((string) null)]
        public string DbDefaultValue;
        [XmlAttribute, DefaultValue("")]
        public string DBTypeName;

        public DBColumn()
        {
            this.IsNullable = true;
        }

        public DBColumn(string name, bool isKey, string dBTypeName, int size, DBColumnType type) : this(name, isKey, dBTypeName, size, type, true, null)
        {
        }

        public DBColumn(string name, bool isKey, string dBTypeName, int size, DBColumnType type, bool isNullable, object defaultValue)
        {
            this.IsNullable = true;
            this.IsKey = isKey;
            this.Name = name;
            this.DBTypeName = dBTypeName;
            this.Size = size;
            this.ColumnType = type;
            this.IsNullable = isNullable;
            this.DefaultValue = defaultValue;
        }

        public static DBColumnType GetColumnType(Type type) => 
            GetColumnType(type, false);

        public static DBColumnType GetColumnType(Type type, bool suppressExceptionOnUnknown)
        {
            switch (DXTypeExtensions.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return DBColumnType.Boolean;

                case TypeCode.Char:
                    return DBColumnType.Char;

                case TypeCode.SByte:
                    return DBColumnType.SByte;

                case TypeCode.Byte:
                    return DBColumnType.Byte;

                case TypeCode.Int16:
                    return DBColumnType.Int16;

                case TypeCode.UInt16:
                    return DBColumnType.UInt16;

                case TypeCode.Int32:
                    return DBColumnType.Int32;

                case TypeCode.UInt32:
                    return DBColumnType.UInt32;

                case TypeCode.Int64:
                    return DBColumnType.Int64;

                case TypeCode.UInt64:
                    return DBColumnType.UInt64;

                case TypeCode.Single:
                    return DBColumnType.Single;

                case TypeCode.Double:
                    return DBColumnType.Double;

                case TypeCode.Decimal:
                    return DBColumnType.Decimal;

                case TypeCode.DateTime:
                    return DBColumnType.DateTime;

                case TypeCode.String:
                    return DBColumnType.String;
            }
            if (type == typeof(Guid))
            {
                return DBColumnType.Guid;
            }
            if (ConnectionProviderSql.UseLegacyTimeSpanSupport && (type == typeof(TimeSpan)))
            {
                return DBColumnType.TimeSpan;
            }
            if (type == typeof(byte[]))
            {
                return DBColumnType.ByteArray;
            }
            if (!suppressExceptionOnUnknown)
            {
                throw new PropertyTypeMappingMissingException(type);
            }
            return DBColumnType.Unknown;
        }

        public static Type GetType(DBColumnType type)
        {
            if (ConnectionProviderSql.UseLegacyTimeSpanSupport && (type == DBColumnType.TimeSpan))
            {
                return typeof(TimeSpan);
            }
            switch (type)
            {
                case DBColumnType.Boolean:
                    return typeof(bool);

                case DBColumnType.Byte:
                    return typeof(byte);

                case DBColumnType.SByte:
                    return typeof(sbyte);

                case DBColumnType.Char:
                    return typeof(char);

                case DBColumnType.Decimal:
                    return typeof(decimal);

                case DBColumnType.Double:
                    return typeof(double);

                case DBColumnType.Single:
                    return typeof(float);

                case DBColumnType.Int32:
                    return typeof(int);

                case DBColumnType.UInt32:
                    return typeof(uint);

                case DBColumnType.Int16:
                    return typeof(short);

                case DBColumnType.UInt16:
                    return typeof(ushort);

                case DBColumnType.Int64:
                    return typeof(long);

                case DBColumnType.UInt64:
                    return typeof(ulong);

                case DBColumnType.String:
                    return typeof(string);

                case DBColumnType.DateTime:
                    return typeof(DateTime);

                case DBColumnType.Guid:
                    return typeof(Guid);

                case DBColumnType.ByteArray:
                    return typeof(byte[]);
            }
            return typeof(object);
        }

        public static bool IsStorableType(DBColumnType type) => 
            type != DBColumnType.Unknown;

        public static bool IsStorableType(Type type) => 
            IsStorableType(GetColumnType(type, true));
    }
}

