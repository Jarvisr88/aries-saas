namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class CastHelper
    {
        private static readonly TypeCode[] byteContainingTypes = new TypeCode[] { TypeCode.UInt16, TypeCode.Int16, TypeCode.UInt32, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal };
        private static readonly TypeCode[] unsignedTypes = new TypeCode[] { TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 };
        private static readonly TypeCode[] shortContainingTypes;
        private static readonly TypeCode[] intContainingTypes;
        private static readonly TypeCode[] longContainingTypes;
        private static readonly Dictionary<TypeCode, TypeCode[]> containingTypesMap;

        static CastHelper()
        {
            TypeCode[] second = new TypeCode[] { TypeCode.Int16, TypeCode.UInt16 };
            shortContainingTypes = byteContainingTypes.Except<TypeCode>(second).ToArray<TypeCode>();
            TypeCode[] codeArray4 = new TypeCode[] { TypeCode.Int32, TypeCode.UInt32 };
            intContainingTypes = shortContainingTypes.Except<TypeCode>(codeArray4).ToArray<TypeCode>();
            TypeCode[] codeArray5 = new TypeCode[] { TypeCode.Int64, TypeCode.UInt64 };
            longContainingTypes = intContainingTypes.Except<TypeCode>(codeArray5).ToArray<TypeCode>();
            Dictionary<TypeCode, TypeCode[]> dictionary = new Dictionary<TypeCode, TypeCode[]> {
                { 
                    TypeCode.SByte,
                    byteContainingTypes.Except<TypeCode>(unsignedTypes).ToArray<TypeCode>()
                },
                { 
                    TypeCode.Byte,
                    byteContainingTypes
                },
                { 
                    TypeCode.Int16,
                    shortContainingTypes.Except<TypeCode>(unsignedTypes).ToArray<TypeCode>()
                },
                { 
                    TypeCode.UInt16,
                    shortContainingTypes
                },
                { 
                    TypeCode.Int32,
                    intContainingTypes.Except<TypeCode>(unsignedTypes).ToArray<TypeCode>()
                },
                { 
                    TypeCode.UInt32,
                    intContainingTypes
                },
                { 
                    TypeCode.Int64,
                    longContainingTypes.Except<TypeCode>(unsignedTypes).ToArray<TypeCode>()
                },
                { 
                    TypeCode.UInt64,
                    TypeCode.Int64.Yield<TypeCode>().Concat<TypeCode>(longContainingTypes).ToArray<TypeCode>()
                }
            };
            TypeCode[] codeArray6 = new TypeCode[] { TypeCode.Double };
            dictionary.Add(TypeCode.Single, codeArray6);
            TypeCode[] codeArray7 = new TypeCode[] { TypeCode.Double };
            dictionary.Add(TypeCode.Decimal, codeArray7);
            dictionary.Add(TypeCode.Double, new TypeCode[0]);
            containingTypesMap = dictionary;
        }

        public static Type GetCast(Type value, Type opposite)
        {
            if (value == opposite)
            {
                return value;
            }
            Type underlyingType = Nullable.GetUnderlyingType(value);
            Type type2 = Nullable.GetUnderlyingType(opposite);
            value = underlyingType ?? value;
            Type type1 = type2;
            if (type2 == null)
            {
                Type local2 = type2;
                type1 = opposite;
            }
            opposite = type1;
            Type type3 = GetCast(Type.GetTypeCode(value), Type.GetTypeCode(opposite)).ToType();
            if (value.IsEnum && (opposite == value))
            {
                type3 = value;
            }
            if ((underlyingType != null) || (type2 != null))
            {
                if (type3 == typeof(object))
                {
                    type3 = underlyingType ?? type2;
                }
                Type[] typeArguments = new Type[] { type3 };
                type3 = typeof(Nullable<>).MakeGenericType(typeArguments);
            }
            return type3;
        }

        public static TypeCode GetCast(TypeCode value, TypeCode opposite)
        {
            if (value == opposite)
            {
                return value;
            }
            TypeCode[] valueOrDefault = containingTypesMap.GetValueOrDefault<TypeCode, TypeCode[]>(value);
            TypeCode[] oppositeContainingTypes = containingTypesMap.GetValueOrDefault<TypeCode, TypeCode[]>(opposite);
            return (((valueOrDefault == null) || (oppositeContainingTypes == null)) ? TypeCode.Empty : (!valueOrDefault.Contains<TypeCode>(opposite) ? (!oppositeContainingTypes.Contains<TypeCode>(value) ? valueOrDefault.First<TypeCode>(x => oppositeContainingTypes.Contains<TypeCode>(x)) : value) : opposite));
        }

        internal static Type ToType(this TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    return null;

                case TypeCode.Object:
                    return typeof(object);

                case TypeCode.DBNull:
                    return typeof(DBNull);

                case TypeCode.Boolean:
                    return typeof(bool);

                case TypeCode.Char:
                    return typeof(char);

                case TypeCode.SByte:
                    return typeof(sbyte);

                case TypeCode.Byte:
                    return typeof(byte);

                case TypeCode.Int16:
                    return typeof(short);

                case TypeCode.UInt16:
                    return typeof(ushort);

                case TypeCode.Int32:
                    return typeof(int);

                case TypeCode.UInt32:
                    return typeof(uint);

                case TypeCode.Int64:
                    return typeof(long);

                case TypeCode.UInt64:
                    return typeof(ulong);

                case TypeCode.Single:
                    return typeof(float);

                case TypeCode.Double:
                    return typeof(double);

                case TypeCode.Decimal:
                    return typeof(decimal);

                case TypeCode.DateTime:
                    return typeof(DateTime);

                case TypeCode.String:
                    return typeof(string);
            }
            return null;
        }
    }
}

