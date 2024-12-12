namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal static class DictionarySerializeExt
    {
        private static readonly CultureInfo invariant = CultureInfo.InvariantCulture;

        private static string After(this string str, string part)
        {
            if (!str.StartsWith(part))
            {
                throw new InvalidOperationException();
            }
            return str.Substring(part.Length);
        }

        public static void Deserialize<K, V>(this IDictionary<K, V> dictionary, string str)
        {
            int pos = 0;
            Deserialize<K, V>(dictionary, str, ref pos);
        }

        private static void Deserialize<K, V>(IDictionary<K, V> dictionary, string str, ref int pos)
        {
            str.Get(ref pos, '{');
            while (str[pos] == '{')
            {
                DeserializePair<K, V>(dictionary, str, ref pos);
            }
            str.Get(ref pos, '}');
        }

        private static object DeserializeKey<V>(string str, ref int pos, int end)
        {
            string str2 = str.Substring(pos, end - pos);
            pos = end;
            return str2.ParseValue<V>();
        }

        private static void DeserializePair<K, V>(IDictionary<K, V> dictionary, string str, ref int pos)
        {
            str.Get(ref pos, '{');
            K key = (K) DeserializeKey<K>(str, ref pos, str.Peek(':', pos));
            str.Get(ref pos, ':');
            str.Get(ref pos, '}');
            dictionary.Add(key, (V) DeserializeValue<V>(str, ref pos, str.Peek('}', pos)));
        }

        private static object DeserializeValue<V>(string str, ref int pos, int end)
        {
            Type type = typeof(V);
            if (!(type == typeof(PSUpdatedObjects.PropertyValues)))
            {
                object obj2 = DeserializeKey<V>(str, ref pos, end);
                return (!(type == typeof(string)) ? obj2 : Encoding.UTF8.GetString(Convert.FromBase64String((string) obj2)));
            }
            PSUpdatedObjects.PropertyValues dictionary = new PSUpdatedObjects.PropertyValues();
            Deserialize<string, string>(dictionary, str, ref pos);
            return dictionary;
        }

        private static void Get(this string str, ref int pos, char chr)
        {
            int num = pos;
            pos = num + 1;
            if (str[num] != chr)
            {
                throw new InvalidOperationException();
            }
        }

        private static TypeCode GetTypeCode(Type type)
        {
            if (type == typeof(string))
            {
                return TypeCode.String;
            }
            if (type == typeof(int))
            {
                return TypeCode.Int32;
            }
            if (type == typeof(long))
            {
                return TypeCode.Int64;
            }
            if (type == typeof(int[]))
            {
                return TypeCode.Int32Array;
            }
            if (type == typeof(RectangleF))
            {
                return TypeCode.RectangleF;
            }
            if (type == typeof(decimal))
            {
                return TypeCode.Decimal;
            }
            if (type == typeof(object))
            {
                return TypeCode.Object;
            }
            if (type == typeof(float))
            {
                return TypeCode.Single;
            }
            if (type == typeof(double))
            {
                return TypeCode.Double;
            }
            if (type == typeof(short))
            {
                return TypeCode.Int16;
            }
            if (type != typeof(DateTime))
            {
                throw new ArgumentException(type.ToString(), "type");
            }
            return TypeCode.DateTime;
        }

        private static string ObjectToString(object o) => 
            !(o is DateTime) ? (!(o is float) ? (!(o is double) ? (!(o is RectangleF) ? o.ToString() : RectangleFToString((RectangleF) o)) : ((double) o).ToString("r", invariant)) : ((float) o).ToString("r", invariant)) : ((DateTime) o).ToString(invariant);

        [IteratorStateMachine(typeof(<ParseIntList>d__16))]
        private static IEnumerable<int> ParseIntList(this string str)
        {
            <ParseIntList>d__16 d__1 = new <ParseIntList>d__16(-2);
            d__1.<>3__str = str;
            return d__1;
        }

        private static object ParseObject(this string str)
        {
            int index = str.IndexOf('|');
            if (index < 0)
            {
                return null;
            }
            TypeCode typeCode = (TypeCode) int.Parse(str.Substring(0, index));
            str = str.Substring(index + 1, (str.Length - index) - 1);
            return str.ParseValue(typeCode);
        }

        private static RectangleF ParseRectangleF(this string str)
        {
            char[] trimChars = new char[] { '{', '}' };
            char[] separator = new char[] { ',' };
            string[] strArray = str.Trim(trimChars).Split(separator);
            return new RectangleF(float.Parse(strArray[0].After("X="), invariant), float.Parse(strArray[1].After("Y="), invariant), float.Parse(strArray[2].After("Width="), invariant), float.Parse(strArray[3].After("Height="), invariant));
        }

        public static object ParseValue<V>(this string str) => 
            str.ParseValue(GetTypeCode(typeof(V)));

        private static object ParseValue(this string str, TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Int16:
                    return short.Parse(str, invariant);

                case TypeCode.Int32:
                    return int.Parse(str, invariant);

                case TypeCode.Int64:
                    return long.Parse(str, invariant);

                case TypeCode.Single:
                    return float.Parse(str, invariant);

                case TypeCode.Double:
                    return double.Parse(str, invariant);

                case TypeCode.Decimal:
                    return decimal.Parse(str, invariant);

                case TypeCode.DateTime:
                    return DateTime.Parse(str, invariant);

                case TypeCode.Object:
                    return str.ParseObject();

                case TypeCode.String:
                    return str;

                case TypeCode.Int32Array:
                    return str.ParseIntList().ToArray<int>();

                case TypeCode.RectangleF:
                    return str.ParseRectangleF();
            }
            throw new ArgumentException(typeCode.ToString(), "typeCode");
        }

        private static int Peek(this string str, char chr, int start) => 
            str.IndexOf(chr, start);

        public static string RectangleFToString(RectangleF r)
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "{X=";
            textArray1[1] = ObjectToString(r.X);
            textArray1[2] = ",Y=";
            textArray1[3] = ObjectToString(r.Y);
            textArray1[4] = ",Width=";
            textArray1[5] = ObjectToString(r.Width);
            textArray1[6] = ",Height=";
            textArray1[7] = ObjectToString(r.Height);
            textArray1[8] = "}";
            return string.Concat(textArray1);
        }

        public static string Serialize(this IDictionary dictionary)
        {
            StringBuilder sb = new StringBuilder();
            Serialize(sb, dictionary);
            return sb.ToString();
        }

        private static void Serialize(StringBuilder sb, IDictionary dictionary)
        {
            IEnumerator enumerator = dictionary.Keys.GetEnumerator();
            IEnumerator enumerator2 = dictionary.Values.GetEnumerator();
            sb.Append('{');
            for (int i = 0; enumerator.MoveNext() && enumerator2.MoveNext(); i++)
            {
                SerializePair(sb, enumerator.Current, enumerator2.Current);
            }
            sb.Append('}');
        }

        public static string SerializeObjectToString(object value) => 
            ((int) GetTypeCode(value.GetType())) + "|" + ObjectToString(value);

        private static void SerializePair(StringBuilder sb, object key, object value)
        {
            sb.Append("{");
            IDictionary dictionary = value as IDictionary;
            if (dictionary == null)
            {
                sb.AppendFormat("{0}:{1}", key, Convert.ToBase64String(Encoding.UTF8.GetBytes(ObjectToString(value))));
            }
            else
            {
                sb.AppendFormat("{0}:", key);
                Serialize(sb, dictionary);
            }
            sb.Append("}");
        }

        [CompilerGenerated]
        private sealed class <ParseIntList>d__16 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            private int <>l__initialThreadId;
            private string str;
            public string <>3__str;
            private string[] <>7__wrap1;
            private int <>7__wrap2;

            [DebuggerHidden]
            public <ParseIntList>d__16(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.<>7__wrap2++;
                }
                else
                {
                    this.<>1__state = -1;
                    char[] separator = new char[] { ',' };
                    string[] strArray = this.str.Split(separator);
                    this.<>7__wrap1 = strArray;
                    this.<>7__wrap2 = 0;
                }
                if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
                {
                    this.<>7__wrap1 = null;
                    return false;
                }
                string s = this.<>7__wrap1[this.<>7__wrap2];
                this.<>2__current = int.Parse(s, DictionarySerializeExt.invariant);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<int> IEnumerable<int>.GetEnumerator()
            {
                DictionarySerializeExt.<ParseIntList>d__16 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DictionarySerializeExt.<ParseIntList>d__16(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.str = this.<>3__str;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            int IEnumerator<int>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private enum TypeCode
        {
            Boolean,
            Char,
            SByte,
            Byte,
            Int16,
            UInt16,
            Int32,
            UInt32,
            Int64,
            UInt64,
            Single,
            Double,
            Decimal,
            DateTime,
            Object,
            String,
            Int32Array,
            RectangleF
        }
    }
}

