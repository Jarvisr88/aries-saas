namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    [DefaultMember("Item")]
    internal class ac
    {
        private readonly ArrayList a;
        private string b;
        internal readonly bool c;
        internal readonly bool d;
        private string e;
        private string f;
        private bool g;

        protected ac(ac A_0)
        {
            this.b = A_0.b;
            this.c = A_0.c;
            this.d = A_0.d;
            ArrayList a = A_0.a;
            int count = a.Count;
            this.a = new ArrayList(count);
            for (int i = 0; i < count; i++)
            {
                Devart.Common.u u = (Devart.Common.u) a[i];
                this.a.Add(new Devart.Common.u(u));
            }
            this.e = A_0.e;
            this.f = A_0.f;
            this.g = A_0.g;
        }

        public ac(string A_0, Hashtable A_1, bool A_2)
        {
            this.a = new ArrayList();
            char[] trimChars = new char[] { 
                ';', '\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ',
                ' ', ' ', ' ', ' ', ' ', '​', '\u2028', '\u2029', '　', '﻿'
            };
            this.b = A_0.TrimEnd(trimChars);
            this.c = true;
            this.d = A_2;
            this.a(A_1);
            this.e = this.a("initialization command", "");
            this.f = this.a("run once command", "");
            this.g = this.a("use performance monitor", false);
        }

        public override int a() => 
            (this.e.GetHashCode() | this.f.GetHashCode()) | this.g.GetHashCode();

        public virtual string a(bool A_0) => 
            !A_0 ? this.b : new ac(this).c("Password");

        public void a(Hashtable A_0)
        {
            if ((this.b != null) && (this.b != string.Empty))
            {
                string str = this.b.TrimEnd(new char[0]);
                int startIndex = 0;
                while (startIndex < str.Length)
                {
                    string str2 = "";
                    bool flag = false;
                    int index = str.IndexOf('=', startIndex);
                    if (index != -1)
                    {
                        char[] trimChars = new char[] { ' ', ';', '\r', '\n' };
                        str2 = str.Substring(startIndex, index - startIndex).Trim(trimChars);
                        if (str2 == "")
                        {
                            throw new ArgumentException(av.a("ParameterNameMissing"));
                        }
                    }
                    int length = index + 1;
                    while (true)
                    {
                        if ((length >= str.Length) || (str[length] != ' '))
                        {
                            string str3;
                            string str4;
                            int num5;
                            if (length >= str.Length)
                            {
                                str3 = string.Empty;
                            }
                            else if ((str[length] != '"') && (str[length] != '\''))
                            {
                                length = str.IndexOf(';', length);
                                if (length == -1)
                                {
                                    length = str.Length;
                                }
                                str3 = str.Substring(index + 1, (length - index) - 1).TrimEnd(new char[0]);
                            }
                            else
                            {
                                char ch = str[length];
                                length = str.IndexOf(ch, length + 1);
                                if (length == -1)
                                {
                                    throw new ArgumentException(string.Format(av.a("ParameterValueMissing"), str2));
                                }
                                str3 = str.Substring(index + 2, (length - index) - 2);
                                flag = ch == '"';
                                if (++length < str.Length)
                                {
                                    int num6 = length;
                                    while (true)
                                    {
                                        if ((length >= str.Length) || (str[length] != ch))
                                        {
                                            while (true)
                                            {
                                                if ((length >= str.Length) || (str[length] != ' '))
                                                {
                                                    if ((length == str.Length) || (str[length] == ';'))
                                                    {
                                                        break;
                                                    }
                                                    throw new ArgumentException(av.a("InvalidChar"));
                                                }
                                                length++;
                                            }
                                            break;
                                        }
                                        length = str.IndexOf(ch, length + 1);
                                        if (length == -1)
                                        {
                                            throw new ArgumentException(string.Format(av.a("ParameterValueMissing"), str2));
                                        }
                                        length++;
                                        str3 = str3 + str.Substring(num6, (length - num6) - 1);
                                        num6 = length;
                                    }
                                }
                            }
                            int num4 = length;
                            if (length < str.Length)
                            {
                                num4++;
                            }
                            if (A_0 == null)
                            {
                                str4 = str2;
                            }
                            else
                            {
                                str4 = (string) A_0[Utils.ToLowerInvariant(str2)];
                                if (str4 == null)
                                {
                                    throw new InvalidOperationException(av.a("UnknownConnectionStringParameter", str2));
                                }
                            }
                            if (this.a(str4, out num5) >= 0)
                            {
                                throw new ArgumentException(string.Format(av.a("DuplicateStringParameter"), str4), str4);
                            }
                            this.a.Add(new Devart.Common.u(str4, str3, num4 - startIndex, (index - startIndex) + 1, flag));
                            startIndex = length + 1;
                            break;
                        }
                        index = length;
                        length++;
                    }
                }
            }
        }

        public override bool a(object A_0)
        {
            ac ac = A_0 as ac;
            return ((ac != null) ? ((this.e == ac.e) && ((this.f == ac.bi()) && (this.g == ac.bg()))) : false);
        }

        private static string a(string A_0) => 
            !new Regex("^[^\"'=;\\s\\p{Cc}]*$").IsMatch(A_0) ? (((A_0.IndexOf('"') == -1) || (A_0.IndexOf('\'') != -1)) ? ("\"" + A_0.Replace("\"", "\"\"") + "\"") : ("'" + A_0 + "'")) : A_0;

        public bool a(string A_0, bool A_1)
        {
            string str = this.d(A_0);
            return ((str != null) ? a3.c(str) : A_1);
        }

        private int a(string A_0, out int A_1)
        {
            A_1 = 0;
            for (int i = 0; i < this.a.Count; i++)
            {
                Devart.Common.u u = (Devart.Common.u) this.a[i];
                if (Utils.CompareInvariant(u.f(), A_0))
                {
                    return i;
                }
                A_1 += u.b();
            }
            if (this.b != null)
            {
                A_1 = this.b.Length;
            }
            return -1;
        }

        public int a(string A_0, int A_1)
        {
            string str = this.d(A_0);
            return ((str != null) ? a3.b(str) : A_1);
        }

        public string a(string A_0, string A_1)
        {
            string str = this.d(A_0);
            return ((str != null) ? a3.a(str) : A_1);
        }

        public static string a(string A_0, string[] A_1)
        {
            Utils.CheckArgumentNull(A_0, "connectionString");
            Utils.CheckArgumentNull(A_1, "keyNames");
            ac ac = new ac(A_0, null, false);
            for (int i = 0; i < A_1.Length; i++)
            {
                ac.c(A_1[i]);
            }
            return ac.ToString();
        }

        internal static void a(StringBuilder A_0, string A_1, string A_2)
        {
            if ((A_0.Length > 0) && (A_0[A_0.Length - 1] != ';'))
            {
                A_0.Append(";");
            }
            A_0.Append(A_1);
            A_0.Append("=");
            if (A_2 != null)
            {
                A_0.Append(a(A_2));
            }
            A_0.Append(";");
        }

        internal static void a(StringBuilder A_0, int A_1, int A_2, string A_3)
        {
            int num = A_1 + A_2;
            if ((num < A_0.Length) && ((A_0[num] != ';') && (A_0[num] != ' ')))
            {
                A_2++;
            }
            A_0.Remove(A_1, A_2);
            A_0.Insert(A_1, a(A_3));
        }

        public virtual bool b() => 
            false;

        private Devart.Common.u b(string A_0)
        {
            int num2;
            int num = this.a(A_0, out num2);
            return ((num >= 0) ? ((Devart.Common.u) this.a[num]) : null);
        }

        public void b(string A_0, string A_1)
        {
            int num;
            int num2 = this.a(A_0, out num);
            if (num2 >= 0)
            {
                Devart.Common.u u = (Devart.Common.u) this.a[num2];
                StringBuilder builder = new StringBuilder(this.b);
                int length = builder.Length;
                a(builder, num + u.e(), u.c(), A_1);
                this.b = builder.ToString();
                u.a((int) (u.c() + (builder.Length - length)));
                u.a(A_1);
            }
            else
            {
                StringBuilder builder2 = new StringBuilder(this.b);
                a(builder2, A_0, A_1);
                if ((num > 0) && (builder2[num] == ';'))
                {
                    Devart.Common.u u1 = (Devart.Common.u) this.a[this.a.Count - 1];
                    u1.b(u1.b() + 1);
                    num++;
                }
                this.b = builder2.ToString();
                int length = builder2.Length;
                Devart.Common.u u2 = new Devart.Common.u(A_0, A_1, length - num, 0, false);
                this.a.Add(u2);
            }
        }

        public string bf() => 
            this.e;

        public bool bg() => 
            this.g;

        internal bool bh() => 
            !this.c || this.a("persist security info", false);

        public string bi() => 
            this.f;

        public ICollection bj()
        {
            int count = this.a.Count;
            string[] strArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                strArray[i] = ((Devart.Common.u) this.a[i]).f();
            }
            return strArray;
        }

        public bool bk() => 
            this.a.Count == 0;

        public ICollection bl()
        {
            int count = this.a.Count;
            string[] strArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                strArray[i] = ((Devart.Common.u) this.a[i]).d();
            }
            return strArray;
        }

        public int bm() => 
            this.a.Count;

        public override string bn() => 
            this.b;

        public virtual void bo()
        {
            this.a.Clear();
            this.b = "";
        }

        public IDictionary bp()
        {
            int count = this.a.Count;
            Hashtable hashtable = Utils.CreateHashtable(true);
            for (int i = 0; i < count; i++)
            {
                Devart.Common.u u = (Devart.Common.u) this.a[i];
                hashtable.Add(u.f(), u.d());
            }
            return hashtable;
        }

        public virtual bool c() => 
            false;

        internal string c(string A_0)
        {
            int num;
            int index = this.a(A_0, out num);
            if (index >= 0)
            {
                Devart.Common.u u = (Devart.Common.u) this.a[index];
                if (this.b != null)
                {
                    this.b = this.b.Remove(num, u.b());
                }
                this.a.RemoveAt(index);
            }
            return this.b;
        }

        internal string c(string A_0, string A_1)
        {
            int num;
            if (this.a(A_0, out num) >= 0)
            {
                this.c(A_0);
            }
            this.b(A_0, A_1);
            return this.b;
        }

        public virtual bool d() => 
            false;

        public string d(string A_0) => 
            this.b(A_0)?.d();

        public string d(string A_0, string A_1)
        {
            Devart.Common.u u = this.b(A_0);
            string str = null;
            if (u != null)
            {
                str = u.d();
            }
            if (str == null)
            {
                return A_1;
            }
            str = a3.a(u.d());
            if (u.a())
            {
                str = $""{str}"";
            }
            return str;
        }

        protected internal bool e(string A_0)
        {
            int num;
            return (this.a(A_0, out num) >= 0);
        }
    }
}

