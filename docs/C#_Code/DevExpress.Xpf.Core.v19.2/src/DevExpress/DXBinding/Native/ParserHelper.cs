namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal static class ParserHelper
    {
        private static bool CanCastValueType(TypeCode from, TypeCode to)
        {
            if (from == to)
            {
                return true;
            }
            switch (from)
            {
                case TypeCode.SByte:
                    switch (to)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.Byte:
                    switch (to)
                    {
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.Int16:
                    switch (to)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.UInt16:
                    switch (to)
                    {
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.Int32:
                    switch (to)
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.UInt32:
                    switch (to)
                    {
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.Int64:
                    switch (to)
                    {
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.UInt64:
                    switch (to)
                    {
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }
                    break;

                case TypeCode.Single:
                    return ((to == TypeCode.Single) || (to == TypeCode.Double));

                default:
                    break;
            }
            return false;
        }

        public static bool CastNumericTypes(ref Expression left, ref Expression right)
        {
            Type type = left.Type;
            Type type2 = right.Type;
            bool flag = CastNumericTypes(ref type, ref type2);
            if (type != left.Type)
            {
                left = Expression.Convert(left, type);
            }
            if (type2 != right.Type)
            {
                right = Expression.Convert(right, type2);
            }
            return flag;
        }

        public static bool CastNumericTypes(ref Type left, ref Type right)
        {
            Type type = Nullable.GetUnderlyingType(left) ?? left;
            Type type2 = Nullable.GetUnderlyingType(right) ?? right;
            if (type.IsEnum || type2.IsEnum)
            {
                return false;
            }
            TypeCode typeCode = Type.GetTypeCode(type);
            TypeCode arg = Type.GetTypeCode(type2);
            Func<TypeCode, bool> func = <>c.<>9__10_0 ??= x => ((x >= TypeCode.SByte) && (x <= TypeCode.Decimal));
            if (!func(typeCode) || !func(arg))
            {
                return false;
            }
            bool isNullable = (type != left) || (type2 != right);
            Func<Type, Type> func2 = delegate (Type x) {
                if (!isNullable)
                {
                    return x;
                }
                Type[] typeArguments = new Type[] { x };
                return typeof(Nullable<>).MakeGenericType(typeArguments);
            };
            if (typeCode < arg)
            {
                left = func2(type2);
                right = func2(type2);
                return true;
            }
            if (typeCode > arg)
            {
                left = func2(type);
                right = func2(type);
                return true;
            }
            left = func2(type);
            right = func2(type2);
            return true;
        }

        public static NRoot GetSyntaxTree(string input, ParserMode parserMode, IParserErrorHandler errorHandler)
        {
            Parser parser1 = new Parser(new Scanner(StringToStream(input)), errorHandler);
            parser1.Mode = parserMode;
            Parser parser = parser1;
            parser.Parse();
            return (errorHandler.HasError ? null : parser.Root);
        }

        public static object ParseBool(string value) => 
            bool.Parse(value);

        public static object ParseFloat(string value)
        {
            value = value.ToLower();
            return (!value.EndsWith("f") ? (!value.EndsWith("d") ? (!value.EndsWith("m") ? ((object) double.Parse(value, CultureInfo.InvariantCulture)) : ((object) decimal.Parse(RemoveLastChars(value, 1), CultureInfo.InvariantCulture))) : double.Parse(RemoveLastChars(value, 1), CultureInfo.InvariantCulture)) : float.Parse(RemoveLastChars(value, 1), CultureInfo.InvariantCulture));
        }

        public static object ParseInt(string value)
        {
            value = value.ToLower();
            if (value.StartsWith("0x"))
            {
                value = value.Substring(2);
            }
            return ((value.EndsWith("ul") || value.EndsWith("lu")) ? ((object) ulong.Parse(RemoveLastChars(value, 2), CultureInfo.InvariantCulture)) : (!value.EndsWith("l") ? (!value.EndsWith("u") ? ((object) int.Parse(value, CultureInfo.InvariantCulture)) : ((object) uint.Parse(RemoveLastChars(value, 1), CultureInfo.InvariantCulture))) : long.Parse(RemoveLastChars(value, 1), CultureInfo.InvariantCulture)));
        }

        public static object ParseString(string value) => 
            !value.StartsWith("`") ? value : Unescape(value.Substring(1, value.Length - 2));

        public static string RemoveFirstChar(string value) => 
            value.Substring(1, value.Length - 1);

        private static string RemoveLastChars(string value, int count) => 
            value.Substring(0, value.Length - count);

        private static Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0L;
            return stream;
        }

        private static string Unescape(string value)
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add(@"\`", "`");
            dictionary1.Add("\\\"", "\"");
            dictionary1.Add(@"\", @"\");
            dictionary1.Add(@"\0", "\0");
            dictionary1.Add(@"\a", "\a");
            dictionary1.Add(@"\b", "\b");
            dictionary1.Add(@"\f", "\f");
            dictionary1.Add(@"\n", "\n");
            dictionary1.Add(@"\r", "\r");
            dictionary1.Add(@"\t", "\t");
            dictionary1.Add(@"\v", "\v");
            foreach (KeyValuePair<string, string> pair in dictionary1)
            {
                value = value.Replace(pair.Key, pair.Value);
            }
            return value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ParserHelper.<>c <>9 = new ParserHelper.<>c();
            public static Func<TypeCode, bool> <>9__10_0;

            internal bool <CastNumericTypes>b__10_0(TypeCode x) => 
                (x >= TypeCode.SByte) && (x <= TypeCode.Decimal);
        }
    }
}

