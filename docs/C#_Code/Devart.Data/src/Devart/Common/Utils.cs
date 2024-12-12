namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public sealed class Utils
    {
        public static bool DesignMode;
        private static char[] a = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static bool? b;

        public static void AddRowsToTable(DataTable destTable, ICollection srcRows)
        {
            foreach (DataRow row in srcRows)
            {
                DataRow row2 = destTable.NewRow();
                row2.ItemArray = row.ItemArray;
                destTable.Rows.Add(row2);
            }
        }

        public static bool ByteArrayEquals(byte[] value1, byte[] value2)
        {
            if (value1.Length != value2.Length)
            {
                return false;
            }
            for (int i = 0; i < value1.Length; i++)
            {
                if (value1[i] != value2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte num2 in bytes)
            {
                builder.Append(a[num2 >> 4]);
                builder.Append(a[num2 & 15]);
            }
            return builder.ToString();
        }

        public static void CheckArgumentNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void CheckArgumentNull(object value, string parameterName, string resMessage)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, g.a(resMessage));
            }
        }

        public static void CheckConnectionOpen(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new InvalidOperationException(g.a("ConnectionNotInit"));
            }
            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException(g.a("ConnMustOpen"));
            }
        }

        public static bool Compare(string st1, string st2) => 
            CultureInfo.CurrentCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase) == 0;

        public static bool Compare(string st1, string st2, bool ignoreCase) => 
            !ignoreCase ? (st1 == st2) : (CultureInfo.CurrentCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase) == 0);

        public static bool CompareInvariant(string st1, string st2) => 
            CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase) == 0;

        public static bool CompareInvariant(string st1, string st2, bool ignoreCase) => 
            !ignoreCase ? (CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) == 0) : (CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase) == 0);

        public static bool CompareObjectNameSuffix(string source, string target, bool ignoreCase, string[] excludeStrings)
        {
            if (excludeStrings != null)
            {
                for (int j = 0; j < excludeStrings.Length; j++)
                {
                    if (!IsEmpty(excludeStrings[j]))
                    {
                        source = source.Replace(excludeStrings[j], "");
                        target = target.Replace(excludeStrings[j], "");
                    }
                }
            }
            CompareOptions options = CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType;
            if (ignoreCase)
            {
                options |= CompareOptions.IgnoreCase;
            }
            char[] separator = new char[] { '.' };
            string[] strArray = source.Split(separator);
            char[] chArray2 = new char[] { '.' };
            string[] strArray2 = target.Split(chArray2);
            int index = strArray.Length - 1;
            for (int i = strArray2.Length - 1; (i >= 0) && (index >= 0); i--)
            {
                if (CultureInfo.CurrentCulture.CompareInfo.Compare(strArray[index], strArray2[i], options) != 0)
                {
                    return false;
                }
                index--;
            }
            return true;
        }

        public static bool CompareSuffix(string source, string suffix, bool ignoreCase) => 
            CompareSuffix(source, suffix, ignoreCase, null);

        public static bool CompareSuffix(string source, string suffix, bool ignoreCase, string[] excludeStrings)
        {
            if (excludeStrings != null)
            {
                for (int i = 0; i < excludeStrings.Length; i++)
                {
                    if (!IsEmpty(excludeStrings[i]))
                    {
                        source = source.Replace(excludeStrings[i], "");
                        suffix = suffix.Replace(excludeStrings[i], "");
                    }
                }
            }
            CompareOptions options = CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType;
            if (ignoreCase)
            {
                options |= CompareOptions.IgnoreCase;
            }
            return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(source, suffix, options);
        }

        public static Hashtable CreateHashtable(bool ignoreCase) => 
            !ignoreCase ? new Hashtable(StringComparer.InvariantCulture) : new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public static void FilterTable(ref DataTable table, string filterExpr)
        {
            if (filterExpr != null)
            {
                DataView view = new DataView(table) {
                    RowFilter = filterExpr
                };
                table = table.Clone();
                DataRow[] srcRows = new DataRow[view.Count];
                int index = 0;
                while (true)
                {
                    if (index >= view.Count)
                    {
                        AddRowsToTable(table, srcRows);
                        break;
                    }
                    srcRows[index] = view[index].Row;
                    index++;
                }
            }
        }

        public static Devart.Common.Utils.a GetDllMachineType(string dllPath)
        {
            Devart.Common.Utils.a a;
            using (FileStream stream = new FileStream(dllPath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    stream.Seek((long) 60, SeekOrigin.Begin);
                    stream.Seek((long) reader.ReadInt32(), SeekOrigin.Begin);
                    a = (reader.ReadUInt32() == 0x4550) ? ((Devart.Common.Utils.a) reader.ReadUInt16()) : Devart.Common.Utils.a.a;
                }
            }
            return a;
        }

        public static byte[] GetMaxBytes(Encoding encoding, string s, out int byteCount)
        {
            int length = s.Length;
            byte[] bytes = new byte[encoding.GetMaxByteCount(length) + 1];
            byteCount = encoding.GetBytes(s, 0, length, bytes, 0);
            return bytes;
        }

        public static bool GetWeakIsAlive(WeakReference weakReference)
        {
            if (weakReference == null)
            {
                return false;
            }
            try
            {
                return weakReference.IsAlive;
            }
            catch
            {
                return false;
            }
        }

        public static object GetWeakTarget(WeakReference weakReference)
        {
            try
            {
                return (((weakReference == null) || !weakReference.IsAlive) ? null : weakReference.Target);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsBasicLetter(char c) => 
            ((c < 'a') || (c > 'z')) ? ((c >= 'A') && (c <= 'Z')) : true;

        public static bool IsEmpty(ICollection collection) => 
            (collection == null) || (collection.Count == 0);

        public static bool IsEmpty(string st) => 
            (st == null) || (st.Length == 0);

        public static bool IsIpAddress(string hostname)
        {
            if ((hostname == null) || (hostname == ""))
            {
                return false;
            }
            for (int i = 0; i < hostname.Length; i++)
            {
                char ch = hostname[i];
                if (((ch < '0') || (ch > '9')) && (ch != '.'))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNull(object val) => 
            (val == null) || (val == DBNull.Value);

        public static bool IsNumber(string s)
        {
            double num;
            return (TryParse(s, NumberStyles.Number, null, out num) || TryParse(s, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num));
        }

        public static bool NeedQuote(string name, Hashtable keywords, char[] prefixes, char[] suffixes) => 
            NeedQuote(name, keywords, prefixes, suffixes, false);

        public static bool NeedQuote(string name, Hashtable keywords, char[] prefixes, char[] suffixes, bool checkCase)
        {
            CheckArgumentNull(name, "name");
            if ((prefixes != null) && (suffixes != null))
            {
                char c = '\0';
                string str = name.Trim();
                if (keywords[ToUpperInvariant(str)] != null)
                {
                    return true;
                }
                int length = str.Length;
                if (length > 0)
                {
                    bool flag = false;
                    bool flag2 = false;
                    int index = 0;
                    while (true)
                    {
                        if (index < prefixes.Length)
                        {
                            if (str[0] != prefixes[index])
                            {
                                index++;
                                continue;
                            }
                            flag = true;
                        }
                        int num3 = 0;
                        while (true)
                        {
                            if (num3 < suffixes.Length)
                            {
                                if (str[length - 1] != suffixes[num3])
                                {
                                    num3++;
                                    continue;
                                }
                                flag2 = true;
                            }
                            if (flag2 || flag)
                            {
                                break;
                            }
                            bool flag3 = true;
                            for (int i = 0; i < length; i++)
                            {
                                c = str[i];
                                if (checkCase && char.IsUpper(c))
                                {
                                    return true;
                                }
                                if (flag3 && !char.IsDigit(c))
                                {
                                    flag3 = false;
                                }
                                if (!char.IsLetterOrDigit(c) && ((c != '_') && ((c != '$') || (i <= 0))))
                                {
                                    return true;
                                }
                            }
                            return flag3;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public static string ObjectToString(object obj) => 
            (obj != null) ? obj.ToString() : string.Empty;

        public static object Parse(string s, Type enumType) => 
            Parse(s, enumType, true);

        public static object Parse(string s, Type enumType, bool ignoreCase)
        {
            object obj2;
            if (!TryParse(s, out obj2, enumType, ignoreCase))
            {
                throw new ArgumentException(g.a("RequestedValueNotFound", s));
            }
            return obj2;
        }

        public static int ParseIntWith0(string s)
        {
            int startIndex = 0;
            while ((startIndex < s.Length) && (s[startIndex] == '0'))
            {
                startIndex++;
            }
            if (startIndex > 0)
            {
                s = s.Substring(startIndex);
            }
            return (((s == "") || (s == "0")) ? 0 : int.Parse(s));
        }

        public static void SetWeakTarget(ref WeakReference weakReference, object target)
        {
            if (target == null)
            {
                weakReference = null;
            }
            else
            {
                weakReference = new WeakReference(target);
            }
        }

        public static DataTable SortTable(DataTable table, string columnsName)
        {
            DataView view = new DataView(table) {
                Sort = columnsName
            };
            DataTable destTable = table.Clone();
            DataRow[] srcRows = new DataRow[view.Count];
            for (int i = 0; i < view.Count; i++)
            {
                srcRows[i] = view[i].Row;
            }
            AddRowsToTable(destTable, srcRows);
            return destTable;
        }

        public static string[] SplitItems(string names, char[] quotes)
        {
            CheckArgumentNull(names, "names");
            ArrayList list = new ArrayList();
            int num = 0;
            int length = names.Length;
            int startIndex = 0;
            char ch = '\0';
            char ch2 = ch;
            char ch3 = '\0';
            while (num < length)
            {
                ch3 = names[num];
                if ((ch3 == ';') && (ch == '\0'))
                {
                    list.Add(names.Substring(startIndex, num - startIndex));
                    startIndex = ++num;
                    continue;
                }
                if (ch == '\\')
                {
                    ch = ch2;
                }
                else
                {
                    bool flag = false;
                    if (quotes != null)
                    {
                        for (int i = 0; i < quotes.Length; i++)
                        {
                            if (quotes[i] == ch3)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        if (ch == '\0')
                        {
                            ch = ch3;
                        }
                        else if (ch == ch3)
                        {
                            ch = '\0';
                        }
                    }
                }
                num++;
            }
            list.Add(names.Substring(startIndex, num - startIndex));
            return (string[]) list.ToArray(typeof(string));
        }

        public static char ToLowerInvariant(char value) => 
            char.ToLower(value, CultureInfo.InvariantCulture);

        public static string ToLowerInvariant(string value) => 
            value?.ToLower(CultureInfo.InvariantCulture);

        public static char ToUpperInvariant(char value) => 
            char.ToUpper(value, CultureInfo.InvariantCulture);

        public static string ToUpperInvariant(string value) => 
            value?.ToUpper(CultureInfo.InvariantCulture);

        public static string TruncateVersion(string version, int count)
        {
            if ((count > 4) || (count <= 0))
            {
                throw new ArgumentException("The value 'count' must be greater than 0 and less than or equal to 4", "count");
            }
            if (string.IsNullOrEmpty(version))
            {
                return string.Empty;
            }
            char[] separator = new char[] { '.' };
            string[] strArray = version.Split(separator);
            if (strArray.Length < count)
            {
                throw new InvalidOperationException($"It is impossible to truncate version to {count} parts because version has been truncated already.");
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < (count - 1); i++)
            {
                builder.Append(strArray[i]);
                builder.Append(".");
            }
            builder.Append(strArray[count - 1]);
            return builder.ToString();
        }

        public static bool TryGetValue(Hashtable dictionary, object key, out object val)
        {
            val = dictionary[key];
            return (val != null);
        }

        public static bool TryParse(string s, out bool b) => 
            TryParse(s, out b, true);

        public static bool TryParse(string s, out double d) => 
            TryParse(s, NumberStyles.Number, null, out d);

        public static bool TryParse(string s, out int i)
        {
            double num;
            i = (int) num;
            return TryParse(s, NumberStyles.Integer, null, out num);
        }

        public static bool TryParse(string s, out bool b, bool ignoreCase)
        {
            if (Compare(s, bool.TrueString, ignoreCase))
            {
                b = true;
                return true;
            }
            if (Compare(s, bool.FalseString, ignoreCase))
            {
                b = false;
                return true;
            }
            b = false;
            return false;
        }

        public static bool TryParse(string s, out object value, Type enumType) => 
            TryParse(s, out value, enumType, true);

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result) => 
            double.TryParse(s, style, provider, out result);

        public static bool TryParse(string s, out object value, Type enumType, bool ignoreCase)
        {
            foreach (FieldInfo info in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (string.Compare(s, info.Name, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) == 0)
                {
                    value = info.GetValue(null);
                    return true;
                }
            }
            value = null;
            return false;
        }

        public static int TryParseInt(string s, ref int pos)
        {
            int startIndex = pos;
            int length = s.Length;
            while ((pos < length) && char.IsDigit(s[pos]))
            {
                pos++;
            }
            return ((pos != startIndex) ? ParseIntWith0(s.Substring(startIndex, pos - startIndex)) : 0);
        }

        public static bool? UnmanagedDllIs64Bit(string dllPath)
        {
            Devart.Common.Utils.a dllMachineType;
            try
            {
                dllMachineType = GetDllMachineType(dllPath);
            }
            catch
            {
                return null;
            }
            if (dllMachineType == Devart.Common.Utils.a.f)
            {
                return false;
            }
            return ((dllMachineType == Devart.Common.Utils.a.g) || (dllMachineType == Devart.Common.Utils.a.c));
        }

        public static bool WaitOne(WaitHandle waitHandle, TimeSpan timeout, bool exitContext) => 
            waitHandle.WaitOne(timeout, exitContext);

        public static bool MonoDetected
        {
            get
            {
                if (b == null)
                {
                    b = false;
                    object[] customAttributes = typeof(DbConnection).Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
                    if ((customAttributes != null) && (customAttributes.Length != 0))
                    {
                        b = new bool?(((AssemblyProductAttribute) customAttributes[0]).Product.IndexOf("MONO") >= 0);
                    }
                }
                return b.Value;
            }
        }

        public static bool IsWIntel =>
            true;

        public enum a
        {
            a = 0,
            b = 0x1d3,
            c = 0x8664,
            d = 0x1c0,
            e = 0xebc,
            f = 0x14c,
            g = 0x200,
            h = 0x9041,
            i = 0x266,
            j = 870,
            k = 0x466,
            l = 0x1f0,
            m = 0x1f1,
            n = 0x166,
            o = 0x1a2,
            p = 0x1a3,
            q = 0x1a6,
            r = 0x1a8,
            s = 450,
            t = 0x169
        }
    }
}

