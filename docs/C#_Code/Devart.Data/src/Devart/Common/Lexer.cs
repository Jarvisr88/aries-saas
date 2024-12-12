namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public sealed class Lexer
    {
        private string a;
        private int b;
        private System.IO.TextReader c;
        private int d;
        private int e;
        private int f;
        private int g;
        private int h;
        private StringBuilder i;
        private StringBuilder j;
        private LexerBehavior k;
        private Hashtable l;
        private Hashtable m;
        private char[] n;
        private char o;
        private char p;
        private char q;
        private char r;
        private string[] s;
        private string t;
        private string u;
        private string v;
        private string w;
        private char x;
        private char y;
        private int z;
        private Token aa;
        private Token ab;
        private Token ac;
        private GetSymbolsHandler ad;
        private bool ae;
        public const int DefaultMaxSymbolLength = 3;
        private CompareInfo af;
        private System.Globalization.CultureInfo ag;
        private const int ah = 0x2000;
        private const char ai = '￿';

        public Lexer(System.IO.TextReader reader, LexerBehavior behavior) : this(reader, CommonLexem.symbols, CommonLexem.keywords, behavior)
        {
        }

        public Lexer(string text, LexerBehavior behavior) : this(text, CommonLexem.symbols, CommonLexem.keywords, behavior)
        {
        }

        private Lexer(Hashtable A_0, Hashtable A_1, LexerBehavior A_2)
        {
            this.n = new char[] { '\'' };
            this.o = '"';
            this.p = '"';
            this.q = '"';
            this.s = new string[] { "--" };
            this.t = "/*";
            this.u = "*/";
            this.w = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            this.x = '$';
            this.y = '#';
            this.z = 3;
            this.af = System.Globalization.CultureInfo.CurrentCulture.CompareInfo;
            this.ag = System.Globalization.CultureInfo.CurrentCulture;
            this.k = A_2;
            this.l = A_0;
            this.m = A_1;
            int num = 0;
            this.h = -1;
            foreach (DictionaryEntry entry in (IEnumerable) A_0)
            {
                int num2 = (int) entry.Value;
                switch (num2)
                {
                    case 0x2711:
                    {
                        this.n = ((string) entry.Key).ToCharArray();
                        continue;
                    }
                    case 0x2712:
                    {
                        this.o = ((string) entry.Key)[0];
                        continue;
                    }
                    case 0x2713:
                    {
                        this.p = ((string) entry.Key)[0];
                        continue;
                    }
                    case 0x2714:
                    {
                        this.q = ((string) entry.Key)[0];
                        continue;
                    }
                    case 0x2715:
                    {
                        if (++num == 1)
                        {
                            this.s = new string[] { entry.Key.ToString() };
                            continue;
                        }
                        string[] array = new string[num];
                        this.s.CopyTo(array, 0);
                        array[num - 1] = entry.Key.ToString();
                        this.s = array;
                        continue;
                    }
                    case 0x2716:
                    {
                        this.t = (string) entry.Key;
                        continue;
                    }
                    case 0x2717:
                    {
                        this.u = (string) entry.Key;
                        continue;
                    }
                    case 0x2718:
                    {
                        this.r = ((string) entry.Key)[0];
                        continue;
                    }
                    case 0x2719:
                    {
                        this.v = (string) entry.Key;
                        continue;
                    }
                }
            }
        }

        public Lexer(System.IO.TextReader reader, Hashtable symbols, Hashtable keywords, LexerBehavior behavior) : this(symbols, keywords, behavior)
        {
            this.c = reader;
            this.Reset();
        }

        public Lexer(string text, Hashtable symbols, Hashtable keywords, LexerBehavior behavior) : this(symbols, keywords, behavior)
        {
            this.a = text;
            this.Reset();
        }

        public Lexer(System.IO.TextReader reader, GetSymbolsHandler getSymbolId, Hashtable symbols, Hashtable keywords, LexerBehavior behavior) : this(symbols, keywords, behavior)
        {
            this.c = reader;
            this.ad = getSymbolId;
            this.Reset();
        }

        private string a() => 
            (this.i != null) ? this.i.ToString(0, this.i.Length) : (((this.k & LexerBehavior.OmitTokenValue) == 0) ? this.a.Substring(this.f, (this.e - this.f) - this.g) : null);

        private Token a(Token A_0)
        {
            bool flag3;
            bool flag4;
            char ch4;
            bool flag9;
            if (this.a == null)
            {
                return Token.Empty;
            }
            int endLineBegin = A_0.EndLineBegin;
            int endLineNumber = A_0.EndLineNumber;
            int num3 = endLineBegin;
            int num4 = endLineNumber;
            bool flag = (this.k & LexerBehavior.BreakBlank) != 0;
            if (this.c == null)
            {
                this.e = this.b = A_0.EndPosition;
            }
            int b = this.b;
            this.f = this.e;
            this.g = 0;
            this.i = null;
            bool flag2 = (this.j == null) && (this.h == this.e);
            char ch = this.e();
            goto TR_00F4;
        TR_0059:
            if (this.i != null)
            {
                this.i.Append(ch);
            }
        TR_006C:
            while (true)
            {
                ch = this.d();
                if (ch == ch4)
                {
                    if (((this.k & LexerBehavior.IdentDoubleQuote) == 0) || (this.e() != ch4))
                    {
                        bool flag8;
                        if (!flag8)
                        {
                            this.g = 1;
                        }
                        else if (this.i != null)
                        {
                            this.i.Append(ch);
                        }
                        break;
                    }
                    flag9 = true;
                    if (this.i != null)
                    {
                        this.i.Append(ch);
                    }
                    ch = this.d();
                }
                if (!this.a(ref ch))
                {
                    if ((ch != 0xffff) || !this.b())
                    {
                        goto TR_0059;
                    }
                }
                else
                {
                    num4++;
                    num3 = this.b;
                    goto TR_0059;
                }
                break;
            }
            string str4 = this.a();
            if (((this.k & LexerBehavior.IdentDoubleQuote) != 0) & flag9)
            {
                str4 = str4.Replace(new string(ch4, 2), ch4.ToString());
            }
            return new Devart.Common.j(Devart.Common.TokenType.Identifier, str4, 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
        TR_00DF:
            if (this.b > b)
            {
                if ((this.k & LexerBehavior.OmitBlank) == 0)
                {
                    return new Devart.Common.j(Devart.Common.TokenType.Blank, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                }
                flag3 = true;
                b = this.b;
            }
            int index = 0;
            while (true)
            {
                if (index < this.s.Length)
                {
                    string str2 = this.s[index];
                    if (!this.b(str2))
                    {
                        index++;
                        continue;
                    }
                    bool flag5 = (this.k & LexerBehavior.OmitComment) != 0;
                    flag3 = true;
                    if (flag5)
                    {
                        this.f = this.e;
                    }
                    else if (this.i != null)
                    {
                        this.i.Append(str2);
                    }
                    ch = this.d();
                    while (true)
                    {
                        if (this.a(ref ch))
                        {
                            num4++;
                            num3 = this.b;
                            if (flag5)
                            {
                                this.f = this.e;
                            }
                            ch = this.e();
                        }
                        else
                        {
                            ch = this.d();
                            if ((ch != 0xffff) || !this.b())
                            {
                                if (flag5)
                                {
                                    this.f = this.e;
                                    continue;
                                }
                                if (this.i == null)
                                {
                                    continue;
                                }
                                this.i.Append(ch);
                                continue;
                            }
                        }
                        if (!flag5)
                        {
                            return new Devart.Common.j(Devart.Common.TokenType.Comment, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                        }
                        b = this.b;
                        this.i = null;
                        break;
                    }
                }
                if ((this.v != null) && this.b(this.v))
                {
                    this.b -= this.v.Length;
                    this.e -= this.v.Length;
                }
                else if (this.b(this.t))
                {
                    bool flag6 = (this.k & LexerBehavior.OmitComment) != 0;
                    flag3 = true;
                    if (flag6)
                    {
                        this.f = this.e;
                    }
                    else if (this.i != null)
                    {
                        this.i.Append(this.t);
                    }
                    while (true)
                    {
                        if (this.b(this.u))
                        {
                            ch = this.e();
                            if (flag6)
                            {
                                this.f = this.e;
                            }
                            else if (this.i != null)
                            {
                                this.i.Append(this.u);
                            }
                            break;
                        }
                        ch = this.d();
                        if (this.a(ref ch))
                        {
                            num4++;
                            num3 = this.b;
                        }
                        else if ((ch == 0xffff) && this.b())
                        {
                            break;
                        }
                        if (flag6)
                        {
                            this.f = this.e;
                        }
                        else if (this.i != null)
                        {
                            this.i.Append(ch);
                        }
                    }
                    if (!flag6)
                    {
                        return new Devart.Common.j(Devart.Common.TokenType.Comment, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                    }
                    b = this.b;
                }
                endLineNumber = num4;
                endLineBegin = num3;
                if (flag3)
                {
                    break;
                }
                int num8 = 0;
                while (true)
                {
                    if (num8 < this.n.Length)
                    {
                        char c = this.n[num8];
                        if (ch == c)
                        {
                            this.ae = (this.k & LexerBehavior.OmitTokenStringValue) != 0;
                            this.c();
                            if ((this.k & LexerBehavior.QuotedString) == 0)
                            {
                                this.f = this.e;
                            }
                            bool flag7 = false;
                            char ch3 = '\0';
                            if (((this.k & LexerBehavior.AlternativeQuotedString) != 0) && ((this.aa.Type == Devart.Common.TokenType.Identifier) && (this.aa.ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture) == "q")))
                            {
                                flag7 = true;
                                ch3 = this.e();
                                if ((ch3 == c) || ((ch3 == ' ') || ((ch3 == '\t') || (ch3 == '\r'))))
                                {
                                    flag7 = false;
                                }
                                else if (ch3 == '(')
                                {
                                    ch3 = ')';
                                }
                                else if (ch3 == '[')
                                {
                                    ch3 = ']';
                                }
                                else if (ch3 == '{')
                                {
                                    ch3 = '}';
                                }
                                else if (ch3 == '<')
                                {
                                    ch3 = '>';
                                }
                            }
                            while (true)
                            {
                                ch = this.d();
                                if (this.a(ref ch))
                                {
                                    num4++;
                                    num3 = this.b;
                                }
                                else if ((ch == '\\') && ((this.k & LexerBehavior.HandleEscaping) != 0))
                                {
                                    if (this.i != null)
                                    {
                                        this.i.Append(ch);
                                    }
                                    ch = this.d();
                                }
                                else if (ch != c)
                                {
                                    if (flag7 && ((ch == ch3) && (this.e() == c)))
                                    {
                                        flag7 = false;
                                    }
                                    else if ((ch == 0xffff) && this.b())
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if (((this.k & LexerBehavior.StringDoubleQuote) == 0) || (this.e() != c))
                                    {
                                        if ((this.k & LexerBehavior.QuotedString) == 0)
                                        {
                                            this.g = 1;
                                        }
                                        else if (this.i != null)
                                        {
                                            this.i.Append(ch);
                                        }
                                        if (!flag7)
                                        {
                                            break;
                                        }
                                        continue;
                                    }
                                    if (this.i != null)
                                    {
                                        this.i.Append(ch);
                                    }
                                    ch = this.d();
                                }
                                if (this.i != null)
                                {
                                    this.i.Append(ch);
                                }
                            }
                            string str3 = null;
                            if (this.ae)
                            {
                                this.ae = false;
                            }
                            else
                            {
                                str3 = this.a();
                                if ((this.k & LexerBehavior.StringDoubleQuote) != 0)
                                {
                                    str3 = str3.Replace(new string(c, 2), c.ToString());
                                }
                            }
                            return new Devart.Common.j(Devart.Common.TokenType.String, str3, 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                        }
                        num8++;
                        continue;
                    }
                    if ((ch == this.o) || (ch == this.p))
                    {
                        ch4 = (ch == this.o) ? this.o : this.q;
                        this.c();
                        if ((this.k & LexerBehavior.QuotedIdent) == 0)
                        {
                            this.f = this.e;
                        }
                        flag9 = false;
                    }
                    else
                    {
                        if (ch == this.r)
                        {
                            this.c();
                            if ((this.k & LexerBehavior.QuotedIdent) == 0)
                            {
                                this.f = this.e;
                            }
                            ch = this.e();
                            if (char.IsLetterOrDigit(ch) || ((ch == '_') || ((ch == this.x) || (ch == this.y))))
                            {
                                do
                                {
                                    this.c();
                                    if (this.i != null)
                                    {
                                        this.i.Append(ch);
                                    }
                                    ch = this.e();
                                }
                                while (char.IsLetterOrDigit(ch) || ((ch == '_') || ((ch == this.x) || (ch == this.y))));
                            }
                            string str5 = this.a();
                            if ((this.k & LexerBehavior.UpperedIdent) != 0)
                            {
                                str5 = str5.ToUpper(this.ag);
                            }
                            else if ((this.k & LexerBehavior.LoweredIdent) != 0)
                            {
                                str5 = str5.ToLower(this.ag);
                            }
                            return new Devart.Common.j(Devart.Common.TokenType.Identifier, str5, 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                        }
                        if ((ch >= '0') && (ch <= '9'))
                        {
                            while (true)
                            {
                                this.c();
                                if (this.i != null)
                                {
                                    this.i.Append(ch);
                                }
                                ch = this.e();
                                if ((ch < '0') || (ch > '9'))
                                {
                                    if (this.a(this.w))
                                    {
                                        int length = this.w.Length;
                                        char ch5 = this.c(length);
                                        if ((ch5 >= '0') && (ch5 <= '9'))
                                        {
                                            if (this.i != null)
                                            {
                                                this.i.Append(this.w);
                                            }
                                            this.a(length);
                                            ch = ch5;
                                            do
                                            {
                                                this.c();
                                                if (this.i != null)
                                                {
                                                    this.i.Append(ch);
                                                }
                                                ch = this.e();
                                            }
                                            while ((ch >= '0') && (ch <= '9'));
                                        }
                                    }
                                    if (!char.IsLetter(ch))
                                    {
                                        object obj2 = this.a();
                                        try
                                        {
                                            if (obj2 != null)
                                            {
                                                obj2 = (this.DecimalSeparator != NumberFormatInfo.InvariantInfo.NumberDecimalSeparator) ? Convert.ToDecimal(obj2, NumberFormatInfo.CurrentInfo) : Convert.ToDecimal(obj2, NumberFormatInfo.InvariantInfo);
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        return new Devart.Common.j(Devart.Common.TokenType.Number, obj2, 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                                    }
                                    while (true)
                                    {
                                        this.c();
                                        if (this.i != null)
                                        {
                                            this.i.Append(ch);
                                        }
                                        ch = this.e();
                                        if ((((ch < '0') || (ch > '9')) && (ch != '-')) && ((ch != '+') && !char.IsLetter(ch)))
                                        {
                                            Devart.Common.TokenType undefined = Devart.Common.TokenType.Undefined;
                                            if ((this.k & LexerBehavior.IdentifierHasFirstDigit) == LexerBehavior.IdentifierHasFirstDigit)
                                            {
                                                undefined = Devart.Common.TokenType.Identifier;
                                            }
                                            return new Devart.Common.j(undefined, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                                        }
                                    }
                                }
                            }
                        }
                        if (!char.IsLetter(ch) && ((ch != '_') && ((ch != this.x) && (ch != this.y))))
                        {
                            string str = this.b(this.z);
                            int length = str.Length;
                            while (length > 0)
                            {
                                object obj4 = (this.ad == null) ? this.l[str] : this.ad(str);
                                if (obj4 != null)
                                {
                                    this.a(length);
                                    return new Devart.Common.j(Devart.Common.TokenType.Symbol, str, (int) obj4, b, this.b, endLineBegin, endLineNumber, num3, num4);
                                }
                                length--;
                                str = str.Substring(0, length);
                            }
                            return new Devart.Common.j(Devart.Common.TokenType.Char, this.d(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                        }
                        LexerBehavior k = this.k;
                        this.k = 0;
                        while (true)
                        {
                            try
                            {
                                while (true)
                                {
                                    object obj3;
                                    this.c();
                                    if (this.i != null)
                                    {
                                        this.i.Append(ch);
                                    }
                                    ch = this.e();
                                    if (char.IsLetterOrDigit(ch) || ((ch == '_') || ((ch == this.x) || ((ch == this.y) || ((ch == '-') && ((k & LexerBehavior.IdentifierHasMinus) == LexerBehavior.IdentifierHasMinus))))))
                                    {
                                        break;
                                    }
                                    string str6 = this.a();
                                    if ((this.k & LexerBehavior.UpperedIdent) != 0)
                                    {
                                        str6 = str6.ToUpper(this.ag);
                                        obj3 = this.m[str6];
                                    }
                                    else
                                    {
                                        obj3 = this.m[str6.ToUpper(this.ag)];
                                        if ((this.k & LexerBehavior.LoweredIdent) != 0)
                                        {
                                            str6 = str6.ToLower(this.ag);
                                        }
                                    }
                                    Devart.Common.TokenType keyword = Devart.Common.TokenType.Keyword;
                                    if (obj3 == null)
                                    {
                                        obj3 = 0;
                                        keyword = Devart.Common.TokenType.Identifier;
                                    }
                                    return new Devart.Common.j(keyword, str6, (int) obj3, b, this.b, endLineBegin, endLineNumber, num3, num4);
                                }
                            }
                            finally
                            {
                                this.k = k;
                            }
                        }
                    }
                    break;
                }
                goto TR_006C;
            }
        TR_00F4:
            while (true)
            {
                flag3 = false;
                if ((ch == 0xffff) && this.b())
                {
                    if (this.c != null)
                    {
                        this.c.Close();
                    }
                    return new Devart.Common.j(Devart.Common.TokenType.End, "", 0, b, b, endLineBegin, endLineNumber, num3, num4);
                }
                if (ch > ' ')
                {
                    goto TR_00DF;
                }
                else
                {
                    flag4 = (this.k & LexerBehavior.OmitBlank) != 0;
                    do
                    {
                        this.c();
                        if (this.a(ref ch))
                        {
                            num4++;
                            num3 = this.b;
                            if (flag && (this.e() <= ' '))
                            {
                                return new Devart.Common.j(Devart.Common.TokenType.Blank, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                            }
                        }
                        if (!flag4)
                        {
                            if (this.i != null)
                            {
                                this.i.Append(ch);
                            }
                        }
                        else
                        {
                            this.f = this.e;
                            if (flag2)
                            {
                                this.h = this.e;
                            }
                        }
                        ch = this.e();
                    }
                    while (((ch != 0xffff) || !this.b()) && (ch <= ' '));
                }
                break;
            }
            if (flag4)
            {
                this.i = null;
            }
            goto TR_00DF;
        }

        private void a(int A_0)
        {
            if (((this.d - this.e) < A_0) && (this.d == 0x2000))
            {
                this.f();
            }
            int num = Math.Min(this.d - this.e, A_0);
            this.b += num;
            this.e += num;
        }

        private bool a(string A_0)
        {
            if ((A_0 == null) || (A_0.Length == 0))
            {
                return false;
            }
            int length = A_0.Length;
            if (((this.d - this.e) < length) && (this.d == 0x2000))
            {
                this.f();
            }
            return (((this.d - this.e) >= length) && ((this.a[this.e] == A_0[0]) && (this.af.Compare(this.a, this.e, length, A_0, 0, length, CompareOptions.None) == 0)));
        }

        private bool a(ref char A_0)
        {
            if (A_0 != '\n')
            {
                if (A_0 != '\r')
                {
                    return false;
                }
                if (this.e() == '\n')
                {
                    this.a(1);
                    A_0 = this.e();
                }
            }
            return true;
        }

        private bool b()
        {
            if (this.e < this.d)
            {
                return false;
            }
            if (this.d != 0x2000)
            {
                return true;
            }
            this.f();
            return (this.e >= this.d);
        }

        private string b(int A_0)
        {
            if ((this.e + A_0) <= this.d)
            {
                return this.a.Substring(this.e, A_0);
            }
            if (this.d == 0x2000)
            {
                this.f();
            }
            int length = this.d - this.e;
            return ((A_0 > length) ? this.a.Substring(this.e, length) : this.a.Substring(this.e, A_0));
        }

        private bool b(string A_0)
        {
            int length = A_0.Length;
            if (((this.d - this.e) < length) && (this.d == 0x2000))
            {
                this.f();
            }
            if (((this.d - this.e) < length) || (this.a[this.e] != A_0[0]))
            {
                return false;
            }
            if (this.af.Compare(this.a, this.e, length, A_0, 0, length, CompareOptions.None) != 0)
            {
                return false;
            }
            this.b += length;
            this.e += length;
            return true;
        }

        public void BeginBlock()
        {
            this.h = this.e - (this.b - this.Current.EndPosition);
            if (this.ac != null)
            {
                this.h = this.e - (this.b - this.ac.StartPosition);
                if (this.h < 0)
                {
                    this.j = new StringBuilder(this.ac.ToString(), 0, -this.h, this.a.Length);
                    this.h = 0;
                }
            }
        }

        private void c()
        {
            if (this.e < this.d)
            {
                this.b++;
                this.e++;
            }
            else if (this.d == 0x2000)
            {
                this.f();
                this.b++;
                this.e++;
            }
        }

        private char c(int A_0)
        {
            int num = this.e + A_0;
            if (num < this.d)
            {
                return this.a[num];
            }
            if (this.d == 0x2000)
            {
                this.f();
                A_0 += this.e;
                if (A_0 < this.d)
                {
                    return this.a[A_0];
                }
            }
            return 0xffff;
        }

        public void CrearBlock()
        {
            if (this.j != null)
            {
                this.j = new StringBuilder(this.a.Length);
                this.h = 0;
            }
        }

        private char d()
        {
            int e;
            if (this.e < this.d)
            {
                this.b++;
                e = this.e;
                this.e = e + 1;
                return this.a[e];
            }
            if (this.d == 0x2000)
            {
                this.f();
                if (this.e < this.d)
                {
                    this.b++;
                    e = this.e;
                    this.e = e + 1;
                    return this.a[e];
                }
            }
            return 0xffff;
        }

        private char e()
        {
            if (this.e < this.d)
            {
                return this.a[this.e];
            }
            if (this.d == 0x2000)
            {
                this.f();
                if (this.e < this.d)
                {
                    return this.a[this.e];
                }
            }
            return 0xffff;
        }

        public string EndBlock(Token to) => 
            this.EndBlock((int) (this.b - to.StartPosition));

        public string EndBlock(int lenEnd)
        {
            string str;
            if (this.j == null)
            {
                str = this.a.Substring(this.h, (this.e - this.h) - lenEnd);
            }
            else
            {
                if (this.e != 0)
                {
                    this.j.Append(this.a.Substring(0, this.e));
                }
                str = this.j.ToString(0, this.j.Length - lenEnd);
                this.j = null;
            }
            this.h = -1;
            return str;
        }

        private void f()
        {
            if (this.c != null)
            {
                if (this.i == null)
                {
                    if ((this.f < this.e) && (((this.k & LexerBehavior.OmitTokenValue) == 0) && !this.ae))
                    {
                        this.i = new StringBuilder(this.a.Substring(this.f, this.e - this.f));
                    }
                    else
                    {
                        this.f = 0;
                    }
                }
                if (this.h >= 0)
                {
                    if (this.j != null)
                    {
                        this.j.Append(this.a, 0, this.e);
                    }
                    else if (this.h < this.e)
                    {
                        this.j = new StringBuilder(this.a.Substring(this.h, this.e - this.h));
                    }
                    else
                    {
                        this.h = 0;
                    }
                }
                char[] destination = new char[0x2000];
                if (this.e >= this.d)
                {
                    this.d = 0;
                }
                else
                {
                    if (this.e == 0)
                    {
                        throw new ArgumentException("'CharBufferCapacity' to small.");
                    }
                    this.d -= this.e;
                    this.a.CopyTo(this.e, destination, 0, this.d);
                }
                this.d += this.c.ReadBlock(destination, this.d, 0x2000 - this.d);
                this.a = new string(destination);
                this.e = 0;
            }
        }

        public Token GetNextToken()
        {
            this.ab = this.aa;
            if (this.ac == null)
            {
                this.aa = this.a(this.aa);
            }
            else
            {
                this.aa = this.ac;
                this.ac = null;
            }
            return this.aa;
        }

        public Token LookForSymbols(params string[] symbols)
        {
            int num6;
            if (this.a == null)
            {
                return Token.Empty;
            }
            this.ab = this.aa;
            this.ac = null;
            int endLineBegin = this.aa.EndLineBegin;
            int endLineNumber = this.aa.EndLineNumber;
            int num3 = endLineBegin;
            int num4 = endLineNumber;
            if (this.c == null)
            {
                this.b = num6 = this.aa.EndPosition;
                this.e = num6;
            }
            int b = this.b;
            this.f = this.e;
            this.g = 0;
            this.i = null;
            if ((this.e() == 0xffff) && this.b())
            {
                if (this.c != null)
                {
                    this.c.Close();
                }
                return new Devart.Common.j(Devart.Common.TokenType.End, "", 0, b, b, endLineBegin, endLineNumber, num3, num4);
            }
            while (true)
            {
                char ch;
                bool flag = false;
                string[] strArray = symbols;
                num6 = 0;
                while (true)
                {
                    if (num6 < strArray.Length)
                    {
                        string str = strArray[num6];
                        if (!this.a(str))
                        {
                            num6++;
                            continue;
                        }
                        flag = true;
                    }
                    if (!flag)
                    {
                        ch = this.d();
                        if (this.a(ref ch))
                        {
                            num4++;
                            num3 = this.b;
                            break;
                        }
                        if ((ch != 0xffff) || !this.b())
                        {
                            break;
                        }
                    }
                    this.aa = new Devart.Common.j(Devart.Common.TokenType.Symbol, this.a(), 0, b, this.b, endLineBegin, endLineNumber, num3, num4);
                    return this.aa;
                }
                if (this.i != null)
                {
                    this.i.Append(ch);
                }
            }
        }

        public Token PeekNextToken()
        {
            this.ac ??= this.a(this.aa);
            return this.ac;
        }

        public Token PeekPreviousToken() => 
            this.ab;

        public void Reset()
        {
            this.ab = this.aa = Token.Begin;
            this.ac = null;
            this.e = 0;
            this.b = 0;
            this.f = 0;
            this.i = null;
            if (this.c == null)
            {
                this.d = (this.a == null) ? 0 : this.a.Length;
            }
            else if (this.a == null)
            {
                this.d = 0;
                this.f();
            }
        }

        public System.Globalization.CultureInfo CultureInfo
        {
            get => 
                this.ag;
            set
            {
                this.ag = value;
                this.af = this.ag.CompareInfo;
            }
        }

        public Token Current =>
            this.aa;

        public bool IsEmpty =>
            ReferenceEquals(this.a, null);

        public string Text
        {
            get => 
                (this.c == null) ? this.a : "";
            set
            {
                this.a = value;
                this.c = null;
                this.Reset();
            }
        }

        public System.IO.TextReader TextReader
        {
            get => 
                this.c;
            set
            {
                this.c = value;
                this.a = null;
                this.Reset();
            }
        }

        public string StringQuote
        {
            get => 
                new string(this.n);
            set => 
                this.n = value.ToCharArray();
        }

        public int MaxSymbolLength
        {
            get => 
                this.z;
            set => 
                this.z = value;
        }

        public Hashtable Keywords =>
            this.m;

        public Hashtable Symbols =>
            this.l;

        public GetSymbolsHandler GetSymbols
        {
            get => 
                this.ad;
            set => 
                this.ad = value;
        }

        public char[] IdentChars
        {
            get
            {
                if ((this.x == '\0') && (this.y == '\0'))
                {
                    return new char[0];
                }
                if (this.y == '\0')
                {
                    return new char[] { this.x };
                }
                return new char[] { this.x, this.y };
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                switch (value.Length)
                {
                    case 0:
                        this.x = '\0';
                        this.y = '\0';
                        return;

                    case 1:
                        this.x = value[0];
                        this.y = '\0';
                        return;

                    case 2:
                        this.x = value[0];
                        this.y = value[1];
                        return;
                }
                throw new ArgumentNullException("Lexer does not support more than two custom identifier chars.");
            }
        }

        public string IdentQuote
        {
            get => 
                this.o.ToString();
            set
            {
                if ((value != null) && (value != string.Empty))
                {
                    this.o = value[0];
                }
                else
                {
                    this.o = 0xffff;
                }
            }
        }

        public string IdentQuoteBegin
        {
            get => 
                this.p.ToString();
            set
            {
                if ((value != null) && (value != string.Empty))
                {
                    this.p = value[0];
                }
                else
                {
                    this.p = 0xffff;
                }
            }
        }

        public string IdentQuoteEnd
        {
            get => 
                this.q.ToString();
            set
            {
                if ((value != null) && (value != string.Empty))
                {
                    this.q = value[0];
                }
                else
                {
                    this.q = 0xffff;
                }
            }
        }

        public string[] InlineComments
        {
            get => 
                this.s;
            set => 
                this.s = value;
        }

        public string CommentBegin
        {
            get => 
                this.t;
            set => 
                this.t = value;
        }

        public string CommentEnd
        {
            get => 
                this.u;
            set => 
                this.u = value;
        }

        public string DecimalSeparator
        {
            get => 
                this.w;
            set => 
                this.w = value;
        }
    }
}

