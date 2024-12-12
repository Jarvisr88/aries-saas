namespace Devart.Common
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Text;

    internal class af
    {
        protected Type a;
        protected readonly DbType b;
        protected readonly DbType c;
        protected readonly Encoding d;
        protected readonly Encoding e;
        protected static readonly double[] f;
        protected static readonly double[] g;
        protected static readonly float[] h;
        protected static byte i = 0x2e;

        static af()
        {
            byte[] buffer = new byte[] { 
                0, 0, 0, 0, 0, 0, 240, 0x3f, 0, 0, 0, 0, 0, 0, 0x24, 0x40,
                0, 0, 0, 0, 0, 0, 0x59, 0x40, 0, 0, 0, 0, 0, 0x40, 0x8f, 0x40,
                0, 0, 0, 0, 0, 0x88, 0xc3, 0x40, 0, 0, 0, 0, 0, 0x6a, 0xf8, 0x40,
                0, 0, 0, 0, 0x80, 0x84, 0x2e, 0x41, 0, 0, 0, 0, 0xd0, 0x12, 0x63, 0x41,
                0, 0, 0, 0, 0x84, 0xd7, 0x97, 0x41, 0, 0, 0, 0, 0x65, 0xcd, 0xcd, 0x41,
                0, 0, 0, 0x20, 0x5f, 160, 2, 0x42, 0, 0, 0, 0xe8, 0x76, 0x48, 0x37, 0x42,
                0, 0, 0, 0xa2, 0x94, 0x1a, 0x6d, 0x42, 0, 0, 0x40, 0xe5, 0x9c, 0x30, 0xa2, 0x42,
                0, 0, 0x90, 30, 0xc4, 0xbc, 0xd6, 0x42, 0, 0, 0x34, 0x26, 0xf5, 0x6b, 12, 0x43,
                0, 0x80, 0xe0, 0x37, 0x79, 0xc3, 0x41, 0x43, 0, 160, 0xd8, 0x85, 0x57, 0x34, 0x76, 0x43,
                0, 200, 0x4e, 0x67, 0x6d, 0xc1, 0xab, 0x43, 0, 0x3d, 0x91, 0x60, 0xe4, 0x58, 0xe1, 0x43,
                0x40, 140, 0xb5, 120, 0x1d, 0xaf, 0x15, 0x44, 80, 0xef, 0xe2, 0xd6, 0xe4, 0x1a, 0x4b, 0x44,
                0x92, 0xd5, 0x4d, 6, 0xcf, 240, 0x80, 0x44, 0xf6, 0x4a, 0xe1, 0xc7, 2, 0x2d, 0xb5, 0x44,
                180, 0x9d, 0xd9, 0x79, 0x43, 120, 0xea, 0x44, 0x91, 2, 40, 0x2c, 0x2a, 0x8b, 0x20, 0x45,
                0x35, 3, 50, 0xb7, 0xf4, 0xad, 0x54, 0x45, 2, 0x84, 0xfe, 0xe4, 0x71, 0xd9, 0x89, 0x45,
                0x81, 0x12, 0x1f, 0x2f, 0xe7, 0x27, 0xc0, 0x45, 0x21, 0xd7, 230, 250, 0xe0, 0x31, 0xf4, 0x45,
                0xea, 140, 160, 0x39, 0x59, 0x3e, 0x29, 70, 0x24, 0xb0, 8, 0x88, 0xef, 0x8d, 0x5f, 70
            };
            byte[] buffer2 = new byte[] { 
                0, 0, 0, 0, 0, 0, 240, 0x3f, 0x17, 110, 5, 0xb5, 0xb5, 0xb8, 0x93, 70,
                0xf5, 0xf9, 0x3f, 0xe9, 3, 0x4f, 0x38, 0x4d, 0x63, 0xb3, 0xd8, 0x62, 0x75, 0xf6, 0xdd, 0x53,
                50, 0x1d, 0x30, 0xf9, 0x48, 0x77, 130, 90, 0xc3, 0xfc, 0x6f, 0x25, 0xd4, 0xc2, 0x26, 0x61,
                0xeb, 0x24, 0xa7, 0xf1, 30, 14, 0xcc, 0x67, 0x99, 0x67, 0xfc, 0xdf, 0x52, 0x4a, 0x71, 110,
                60, 0xbf, 0x73, 0x7f, 0xdd, 0x4f, 0x15, 0x75, 70, 0x8d, 0x2b, 0x83, 0xdf, 0x44, 0xba, 0x7b
            };
            byte[] buffer3 = new byte[] { 
                0, 0, 0x80, 0x3f, 0, 0, 0x20, 0x41, 0, 0, 200, 0x42, 0, 0, 0x7a, 0x44,
                0, 0x40, 0x1c, 70, 0, 80, 0xc3, 0x47, 0, 0x24, 0x74, 0x49, 0x80, 150, 0x18, 0x4b,
                0x20, 0xbc, 190, 0x4c, 40, 0x6b, 110, 0x4e, 0xf9, 2, 0x15, 80, 0xb7, 0x43, 0xba, 0x51,
                0xa5, 0xd4, 0x68, 0x53, 0xe7, 0x84, 0x11, 0x55, 0x21, 230, 0xb5, 0x56, 0xa9, 0x5f, 0x63, 0x58,
                0xca, 0x1b, 14, 90, 0xbc, 0xa2, 0xb1, 0x5b, 0x6b, 11, 0x5e, 0x5d, 0x23, 0xc7, 10, 0x5f,
                0xec, 120, 0xad, 0x60, 0x27, 0xd7, 0x58, 0x62, 120, 0x86, 7, 100, 0x16, 0x68, 0xa9, 0x65,
                0x1c, 0xc2, 0x53, 0x67, 0x51, 0x59, 4, 0x69, 0xa6, 0x6f, 0xa5, 0x6a, 0x8f, 0xcb, 0x4e, 0x6c,
                0x39, 0x3f, 1, 110, 8, 0x8f, 0xa1, 0x6f, 0xca, 0xf2, 0x49, 0x71, 0x7c, 0x6f, 0xfc, 0x72,
                0xae, 0xc5, 0x9d, 0x74, 0x19, 0x37, 0x45, 0x76, 0xdf, 0x84, 0xf6, 0x77, 12, 0x13, 0x9a, 0x79,
                0xce, 0x97, 0x40, 0x7b, 0xc2, 0xbd, 240, 0x7c, 0x99, 0x76, 150, 0x7e
            };
            f = new double[0x20];
            g = new double[10];
            h = new float[0x27];
            int num = 0;
            for (int i = 0; i < 0x20; i++)
            {
                f[i] = a0.i(buffer, num);
                num += 8;
            }
            num = 0;
            for (int j = 0; j < 10; j++)
            {
                g[j] = a0.i(buffer2, num);
                num += 8;
            }
            num = 0;
            for (int k = 0; k < 0x27; k++)
            {
                h[k] = a0.e(buffer3, num);
                num += 4;
            }
        }

        protected af(DbType A_0, Encoding A_1) : this(A_0, A_0, A_1)
        {
        }

        protected af(DbType A_0, DbType A_1, Encoding A_2) : this(A_0, A_1, A_2, Encoding.Unicode)
        {
        }

        protected af(DbType A_0, Encoding A_1, Encoding A_2) : this(A_0, A_0, A_1, A_2)
        {
        }

        protected af(DbType A_0, DbType A_1, Encoding A_2, Encoding A_3)
        {
            this.c = A_0;
            this.d = A_2;
            this.e = A_3;
            this.b = A_1;
            switch (A_1)
            {
                case DbType.AnsiString:
                case DbType.String:
                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    this.a = typeof(string);
                    return;

                case DbType.Binary:
                    this.a = typeof(byte[]);
                    return;

                case DbType.Byte:
                    this.a = typeof(byte);
                    return;

                case DbType.Boolean:
                    this.a = typeof(bool);
                    return;

                case DbType.Currency:
                case DbType.Decimal:
                case DbType.UInt64:
                case DbType.VarNumeric:
                    this.a = typeof(decimal);
                    return;

                case DbType.Date:
                case DbType.DateTime:
                    this.a = typeof(DateTime);
                    return;

                case DbType.Double:
                    this.a = typeof(double);
                    return;

                case DbType.Guid:
                    this.a = typeof(Guid);
                    return;

                case DbType.Int16:
                case DbType.SByte:
                    this.a = typeof(short);
                    return;

                case DbType.Int32:
                case DbType.UInt16:
                    this.a = typeof(int);
                    return;

                case DbType.Int64:
                case DbType.UInt32:
                    this.a = typeof(long);
                    return;

                case DbType.Object:
                    this.a = typeof(object);
                    return;

                case DbType.Single:
                    this.a = typeof(float);
                    return;

                case DbType.Time:
                    this.a = typeof(TimeSpan);
                    return;
            }
        }

        private static double a(int A_0) => 
            (A_0 >= 0x20) ? (f[A_0 & 0x1f] * g[A_0 >> 5]) : f[A_0];

        public virtual bool a(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                    return bool.Parse(this.@as(A_0, A_1, A_2));

                case DbType.Binary:
                    return (this.ai(A_0, A_1, A_2)[0] != 0);

                case DbType.Byte:
                    return (this.aa(A_0, A_1, A_2) != 0);

                case DbType.Decimal:
                    return (this.ab(A_0, A_1, A_2) != 0M);

                case DbType.Double:
                    return !(this.y(A_0, A_1, A_2) == 0.0);

                case DbType.Int16:
                    return (this.z(A_0, A_1, A_2) != 0);

                case DbType.Int32:
                    return (this.ag(A_0, A_1, A_2) != 0);

                case DbType.Int64:
                    return (this.am(A_0, A_1, A_2) != 0L);

                case DbType.SByte:
                    return (((sbyte) this.aa(A_0, A_1, A_2)) != 0);

                case DbType.Single:
                    return !(this.ac(A_0, A_1, A_2) == 0f);

                case DbType.String:
                case DbType.AnsiStringFixedLength:
                    return this.ap(A_0, A_1, A_2);

                case DbType.UInt16:
                    return (this.ah(A_0, A_1, A_2) != 0);

                case DbType.UInt32:
                    return (this.au(A_0, A_1, A_2) != 0);

                case DbType.UInt64:
                    return (this.ad(A_0, A_1, A_2) != 0L);

                case DbType.StringFixedLength:
                    return bool.Parse(this.b(A_0, A_1, A_2, this.e));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(bool)));
        }

        public virtual void a(byte[] A_0, int A_1, object A_2)
        {
            DbType c = this.c;
            if (c > DbType.Binary)
            {
                switch (c)
                {
                    case DbType.Double:
                    {
                        double[] src = new double[] { Convert.ToDouble(A_2) };
                        Buffer.BlockCopy(src, 0, A_0, A_1, 8);
                        return;
                    }
                    case DbType.Guid:
                    case DbType.Object:
                    case DbType.SByte:
                        break;

                    case DbType.Int16:
                    {
                        short num = Convert.ToInt16(A_2);
                        A_0[A_1++] = (byte) num;
                        A_0[A_1] = (byte) (num >> 8);
                        return;
                    }
                    case DbType.Int32:
                    {
                        int num2 = Convert.ToInt32(A_2);
                        A_0[A_1++] = (byte) num2;
                        A_0[A_1++] = (byte) (num2 >> 8);
                        A_0[A_1++] = (byte) (num2 >> 0x10);
                        A_0[A_1] = (byte) (num2 >> 0x18);
                        return;
                    }
                    case DbType.Int64:
                    {
                        long[] src = new long[] { Convert.ToInt64(A_2) };
                        Buffer.BlockCopy(src, 0, A_0, A_1, 8);
                        return;
                    }
                    case DbType.Single:
                    {
                        float[] src = new float[] { Convert.ToSingle(A_2) };
                        Buffer.BlockCopy(src, 0, A_0, A_1, 4);
                        return;
                    }
                    case DbType.String:
                        goto TR_0010;

                    default:
                        if (c == DbType.AnsiStringFixedLength)
                        {
                            goto TR_000A;
                        }
                        else if (c == DbType.StringFixedLength)
                        {
                            goto TR_0010;
                        }
                        break;
                }
                goto TR_0000;
            }
            else
            {
                if (c != DbType.AnsiString)
                {
                    if (c == DbType.Binary)
                    {
                        byte[] src = (byte[]) A_2;
                        if (src != null)
                        {
                            Buffer.BlockCopy(src, 0, A_0, A_1, src.Length);
                            return;
                        }
                    }
                    else
                    {
                        goto TR_0000;
                    }
                    return;
                }
                goto TR_000A;
            }
            goto TR_0010;
        TR_0000:
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), typeof(object), this.g()));
        TR_000A:
            if (A_2 != null)
            {
                string s = A_2.ToString();
                A_1 += this.d.GetBytes(s, 0, s.Length, A_0, A_1);
            }
            A_0[A_1] = 0;
            if (ReferenceEquals(this.d, Encoding.Unicode) || ReferenceEquals(this.d, Encoding.BigEndianUnicode))
            {
                A_0[A_1 + 1] = 0;
                return;
            }
            return;
        TR_0010:
            if (A_2 != null)
            {
                string s = A_2.ToString();
                A_1 += this.e.GetBytes(s, 0, s.Length, A_0, A_1);
            }
            A_0[A_1] = 0;
            if (ReferenceEquals(this.e, Encoding.Unicode) || ReferenceEquals(this.e, Encoding.BigEndianUnicode))
            {
                A_0[A_1 + 1] = 0;
            }
        }

        protected decimal a(byte[] A_0, int A_1, int A_2, bool A_3)
        {
            bool flag;
            int num3;
            int index = A_1;
            int num2 = A_2 + index;
            while (true)
            {
                if (A_0[num2 - 1] != 0x20)
                {
                    if (A_0[index] == 0x2d)
                    {
                        flag = true;
                        index++;
                    }
                    else
                    {
                        flag = false;
                        if (A_0[index] == 0x2b)
                        {
                            index++;
                        }
                    }
                    num3 = -1;
                    bool flag2 = false;
                    for (int m = index; m < num2; m++)
                    {
                        if ((A_0[m] > 0x30) && ((A_0[m] < 0x3a) && !flag2))
                        {
                            index = m;
                            flag2 = true;
                        }
                        if ((A_0[m] == Devart.Common.af.i) || (A_0[m] == 0x2c))
                        {
                            num3 = m;
                            if (!flag2)
                            {
                                index = num3 - 1;
                            }
                            break;
                        }
                    }
                    break;
                }
                num2--;
            }
            if (A_3 && (num3 != -1))
            {
                int num15 = num2 - 1;
                while (num15 > index)
                {
                    if ((A_0[num15] > 0x30) && (A_0[num15] < 0x3a))
                    {
                        num2 = num15 + 1;
                    }
                    else
                    {
                        if ((A_0[num15] != Devart.Common.af.i) && (A_0[num15] != 0x2c))
                        {
                            num15--;
                            continue;
                        }
                        num2 = num15;
                        num3 = -1;
                    }
                    break;
                }
            }
            if ((num2 - index) > 0x1b)
            {
                StringBuilder builder = new StringBuilder(num2 - A_1);
                for (int m = A_1; m < num2; m++)
                {
                    builder.Append((char) A_0[m]);
                }
                builder.Replace(',', '.');
                return decimal.Parse(builder.ToString(), CultureInfo.InvariantCulture);
            }
            int num4 = num2 - 0x12;
            if (num4 <= index)
            {
                num4 = index;
            }
            else if (num3 >= num4)
            {
                num4--;
            }
            int num5 = num2 - 9;
            if (num5 <= index)
            {
                num5 = index;
            }
            else if (num3 >= num5)
            {
                num5--;
            }
            uint num6 = 0;
            for (int i = index; i < num4; i++)
            {
                if (i != num3)
                {
                    if ((A_0[i] < 0x30) || (A_0[i] > 0x39))
                    {
                        throw new FormatException("IncorrectFormat");
                    }
                    num6 = (uint) (((num6 * 10) + A_0[i]) - 0x30);
                }
            }
            uint num7 = 0;
            for (int j = num4; j < num5; j++)
            {
                if (j != num3)
                {
                    if ((A_0[j] < 0x30) || (A_0[j] > 0x39))
                    {
                        throw new FormatException("IncorrectFormat");
                    }
                    num7 = (uint) (((num7 * 10) + A_0[j]) - 0x30);
                }
            }
            uint num8 = 0;
            for (int k = num5; k < num2; k++)
            {
                if (k != num3)
                {
                    if ((A_0[k] < 0x30) || (A_0[k] > 0x39))
                    {
                        throw new FormatException("IncorrectFormat");
                    }
                    num8 = (uint) (((num8 * 10) + A_0[k]) - 0x30);
                }
            }
            ulong num9 = (ulong) (num6 * 0x3b9aca00L);
            uint num10 = (uint) num9;
            num9 = (num9 >> 0x20) * ((ulong) 0x3b9aca00L);
            uint num11 = (uint) num9;
            num9 = ((num7 + num10) * ((ulong) 0x3b9aca00L)) + num8;
            int lo = (int) num9;
            num9 = (num9 >> 0x20) + num11;
            int hi = ((int) (num9 >> 0x20)) + ((int) (num9 >> 0x20));
            int mid = (int) num9;
            num3 = (num3 <= 0) ? 0 : ((num2 - num3) - 1);
            return new decimal(lo, mid, hi, flag, (byte) num3);
        }

        internal static string a(byte[] A_0, int A_1, int A_2, Encoding A_3)
        {
            if (A_2 < 0)
            {
                int index = A_1;
                A_2 = A_0.Length;
                if (!ReferenceEquals(A_3, Encoding.Unicode) && !ReferenceEquals(A_3, Encoding.BigEndianUnicode))
                {
                    while ((index < A_2) && (A_0[index] != 0))
                    {
                        index++;
                    }
                }
                else
                {
                    while (((index + 1) < A_2) && ((A_0[index] != 0) || (A_0[index + 1] != 0)))
                    {
                        index += 2;
                    }
                }
                A_2 = index - A_1;
            }
            return new string(A_3.GetChars(A_0, A_1, A_2));
        }

        protected virtual byte aa(byte[] A_0, int A_1, int A_2) => 
            A_0[A_1];

        protected virtual decimal ab(byte[] A_0, int A_1, int A_2) => 
            this.al(A_0, A_1, A_2);

        protected virtual float ac(byte[] A_0, int A_1, int A_2) => 
            a0.e(A_0, A_1);

        protected virtual ulong ad(byte[] A_0, int A_1, int A_2) => 
            a0.a(A_0, A_1);

        protected virtual int ae(byte[] A_0, int A_1, int A_2) => 
            this.ag(A_0, A_1, A_2) & 0xffffff;

        public virtual float af(byte[] A_0, int A_1, int A_2) => 
            float.Parse(this.d.GetString(A_0, A_1, A_2), CultureInfo.InvariantCulture);

        protected virtual int ag(byte[] A_0, int A_1, int A_2) => 
            a0.g(A_0, A_1);

        protected virtual ushort ah(byte[] A_0, int A_1, int A_2) => 
            a0.c(A_0, A_1);

        protected virtual byte[] ai(byte[] A_0, int A_1, int A_2)
        {
            if (A_2 < 0)
            {
                int index = A_1;
                A_2 = A_0.Length;
                if (!ReferenceEquals(this.d, Encoding.Unicode) && !ReferenceEquals(this.d, Encoding.BigEndianUnicode))
                {
                    while ((index < A_2) && (A_0[index] != 0))
                    {
                        index++;
                    }
                }
                else
                {
                    while (((index + 1) < A_2) && ((A_0[index] != 0) || (A_0[index + 1] != 0)))
                    {
                        index += 2;
                    }
                }
                A_2 = index - A_1;
            }
            byte[] dst = new byte[A_2];
            Buffer.BlockCopy(A_0, A_1, dst, 0, A_2);
            return dst;
        }

        public virtual long aj(byte[] A_0, int A_1, int A_2)
        {
            bool flag;
            int num = A_1 + A_2;
            if (A_0[num - 1] == 0x20)
            {
                while (A_0[num - 1] == 0x20)
                {
                    num--;
                }
            }
            byte num2 = A_0[A_1];
            if (num2 == 0x2d)
            {
                flag = true;
                A_1++;
            }
            else
            {
                flag = false;
                if (num2 == 0x2b)
                {
                    A_1++;
                }
            }
            long num3 = 0L;
            int index = A_1;
            while (true)
            {
                if (index < num)
                {
                    num2 = A_0[index];
                    if ((num2 != 0) && (num2 != i))
                    {
                        if ((num2 < 0x30) || (num2 > 0x39))
                        {
                            throw new FormatException("IncorrectFormat");
                        }
                        num3 = (num3 * 10) + (num2 - 0x30);
                        index++;
                        continue;
                    }
                }
                return (flag ? -num3 : num3);
            }
        }

        public virtual float ak(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                    return this.af(A_0, A_1, A_2);

                case DbType.Binary:
                    return a0.e(this.ai(A_0, A_1, A_2), 0);

                case DbType.Byte:
                    return (float) this.aa(A_0, A_1, A_2);

                case DbType.Double:
                    return (float) this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return (float) this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return (float) this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return (float) this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (float) ((sbyte) this.aa(A_0, A_1, A_2));

                case DbType.Single:
                    return this.ac(A_0, A_1, A_2);

                case DbType.String:
                    return float.Parse(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return (float) this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return (float) this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return (float) this.ad(A_0, A_1, A_2);

                case DbType.AnsiStringFixedLength:
                    return this.af(A_0, A_1, A_2);

                case DbType.StringFixedLength:
                    return float.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(float)));
        }

        public virtual decimal al(byte[] A_0, int A_1, int A_2) => 
            this.a(A_0, A_1, A_2, true);

        protected virtual long am(byte[] A_0, int A_1, int A_2) => 
            a0.f(A_0, A_1);

        public virtual double an(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    return this.v(A_0, A_1, A_2);

                case DbType.Binary:
                    return a0.i(this.ai(A_0, A_1, A_2), 0);

                case DbType.Byte:
                    return (double) this.aa(A_0, A_1, A_2);

                case DbType.Decimal:
                    return (double) this.ab(A_0, A_1, A_2);

                case DbType.Double:
                    return this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return (double) this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return (double) this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return (double) this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (double) ((sbyte) this.aa(A_0, A_1, A_2));

                case DbType.Single:
                    return (double) this.ac(A_0, A_1, A_2);

                case DbType.String:
                case DbType.StringFixedLength:
                    return double.Parse(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return (double) this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return (double) this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return (double) this.ad(A_0, A_1, A_2);
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(double)));
        }

        public virtual ulong ao(byte[] A_0, int A_1, int A_2)
        {
            bool flag;
            int num = A_1 + A_2;
            ulong num3 = 0UL;
            if (A_0[num - 1] == 0x20)
            {
                while (A_0[num - 1] == 0x20)
                {
                    num--;
                }
            }
            byte num2 = A_0[A_1];
            if (num2 == 0x2d)
            {
                flag = true;
                A_1++;
            }
            else
            {
                flag = false;
                if (num2 == 0x2b)
                {
                    A_1++;
                }
            }
            int index = A_1;
            while (true)
            {
                if (index < num)
                {
                    num2 = A_0[index];
                    if ((num2 != 0) && (num2 != i))
                    {
                        if ((num2 < 0x30) || (num2 > 0x39))
                        {
                            throw new FormatException("IncorrectFormat");
                        }
                        num3 = (num3 * 10) + ((byte) (num2 - 0x30));
                        index++;
                        continue;
                    }
                }
                if (flag)
                {
                    num3 = -num3;
                }
                return num3;
            }
        }

        protected virtual bool ap(byte[] A_0, int A_1, int A_2) => 
            bool.Parse(this.@as(A_0, A_1, A_2));

        protected virtual char aq(byte[] A_0, int A_1, int A_2) => 
            a0.j(A_0, A_1);

        public virtual int ar(byte[] A_0, int A_1, int A_2) => 
            (int) this.aj(A_0, A_1, A_2);

        protected virtual string @as(byte[] A_0, int A_1, int A_2) => 
            a(A_0, A_1, A_2, this.d);

        public virtual decimal at(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    return this.al(A_0, A_1, A_2);

                case DbType.Binary:
                {
                    byte[] src = this.ai(A_0, A_1, A_2);
                    float num = src.Length / 4;
                    if ((num - ((int) num)) > 0.0)
                    {
                        num++;
                    }
                    int[] dst = new int[(int) num];
                    Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                    return new decimal(dst);
                }
                case DbType.Byte:
                    return this.aa(A_0, A_1, A_2);

                case DbType.Decimal:
                    return this.ab(A_0, A_1, A_2);

                case DbType.Double:
                    return (decimal) this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (sbyte) this.aa(A_0, A_1, A_2);

                case DbType.Single:
                    return (decimal) this.ac(A_0, A_1, A_2);

                case DbType.String:
                    return decimal.Parse(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return this.ad(A_0, A_1, A_2);

                case DbType.StringFixedLength:
                    return decimal.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(decimal)));
        }

        protected virtual uint au(byte[] A_0, int A_1, int A_2) => 
            a0.b(A_0, A_1);

        protected virtual TimeSpan av(byte[] A_0, int A_1, int A_2) => 
            TimeSpan.Parse(this.@as(A_0, A_1, A_2));

        public virtual byte aw(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.String:
                    return byte.Parse(this.@as(A_0, A_1, A_2));

                case DbType.Binary:
                {
                    byte[] buffer = this.ai(A_0, A_1, A_2);
                    return this.aa(buffer, 0, buffer.Length);
                }
                case DbType.Byte:
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.SByte:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    return this.aa(A_0, A_1, A_2);

                case DbType.Double:
                    return (byte) this.y(A_0, A_1, A_2);

                case DbType.Single:
                    return (byte) this.ac(A_0, A_1, A_2);

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return byte.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(byte)));
        }

        protected virtual DateTime ax(byte[] A_0, int A_1, int A_2) => 
            DateTime.Parse(this.@as(A_0, A_1, A_2));

        public virtual Guid ay(byte[] A_0, int A_1, int A_2)
        {
            DbType c = this.c;
            if (c > DbType.Binary)
            {
                if (c != DbType.Guid)
                {
                    if ((c == DbType.AnsiStringFixedLength) || (c == DbType.StringFixedLength))
                    {
                        return new Guid(this.x(A_0, A_1, A_2));
                    }
                    goto TR_0000;
                }
            }
            else if (c != DbType.AnsiString)
            {
                if (c == DbType.Binary)
                {
                    byte[] buffer = this.ai(A_0, A_1, A_2);
                    return ((A_2 >= 0x10) ? new Guid(a0.g(buffer, 0), a0.h(buffer, 4), a0.h(buffer, 6), buffer[8], buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15]) : Guid.Empty);
                }
                goto TR_0000;
            }
            return this.j(A_0, A_1, A_2);
        TR_0000:
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(Guid)));
        }

        public virtual int b(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    return this.ar(A_0, A_1, A_2);

                case DbType.Binary:
                    return a0.g(this.ai(A_0, A_1, A_2), 0);

                case DbType.Byte:
                    return this.aa(A_0, A_1, A_2);

                case DbType.Decimal:
                    return (int) this.ab(A_0, A_1, A_2);

                case DbType.Double:
                    return (int) this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return (int) this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (sbyte) this.aa(A_0, A_1, A_2);

                case DbType.Single:
                    return (int) this.ac(A_0, A_1, A_2);

                case DbType.String:
                    return Utils.ParseIntWith0(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return (int) this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return (int) this.ad(A_0, A_1, A_2);

                case DbType.StringFixedLength:
                    return Utils.ParseIntWith0(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(int)));
        }

        protected virtual string b(byte[] A_0, int A_1, int A_2, Encoding A_3)
        {
            char[] chArray = A_3.GetChars(A_0, A_1, A_2);
            int length = chArray.Length;
            while ((length > 0) && (chArray[length - 1].CompareTo(' ') <= 0))
            {
                length--;
            }
            return new string(chArray, 0, length);
        }

        public Encoding c() => 
            this.d;

        public virtual string c(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.String:
                    return this.@as(A_0, A_1, A_2);

                case DbType.Binary:
                {
                    byte[] bytes = this.ai(A_0, A_1, A_2);
                    return this.d.GetString(bytes, 0, bytes.Length);
                }
                case DbType.Byte:
                    return this.aa(A_0, A_1, A_2).ToString();

                case DbType.Boolean:
                    return this.ap(A_0, A_1, A_2).ToString();

                case DbType.Decimal:
                    return this.ab(A_0, A_1, A_2).ToString();

                case DbType.Double:
                    return this.y(A_0, A_1, A_2).ToString();

                case DbType.Guid:
                    return this.ay(A_0, A_1, A_2).ToString();

                case DbType.Int16:
                    return this.z(A_0, A_1, A_2).ToString();

                case DbType.Int32:
                    return this.ag(A_0, A_1, A_2).ToString();

                case DbType.Int64:
                    return this.am(A_0, A_1, A_2).ToString();

                case DbType.SByte:
                    return ((sbyte) this.aa(A_0, A_1, A_2)).ToString();

                case DbType.Single:
                    return this.ac(A_0, A_1, A_2).ToString();

                case DbType.UInt16:
                    return this.ah(A_0, A_1, A_2).ToString();

                case DbType.UInt32:
                    return this.au(A_0, A_1, A_2).ToString();

                case DbType.UInt64:
                    return this.ad(A_0, A_1, A_2).ToString();

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return this.x(A_0, A_1, A_2);
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(string)));
        }

        public DbType d() => 
            this.b;

        public virtual short d(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    return (short) this.ar(A_0, A_1, A_2);

                case DbType.Binary:
                    return a0.h(this.ai(A_0, A_1, A_2), 0);

                case DbType.Byte:
                    return this.aa(A_0, A_1, A_2);

                case DbType.Decimal:
                    return (short) this.ab(A_0, A_1, A_2);

                case DbType.Double:
                    return (short) this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return (short) this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return (short) this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (sbyte) this.aa(A_0, A_1, A_2);

                case DbType.Single:
                    return (short) this.ac(A_0, A_1, A_2);

                case DbType.String:
                    return short.Parse(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return (short) this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return (short) this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return (short) this.ad(A_0, A_1, A_2);

                case DbType.StringFixedLength:
                    return short.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(short)));
        }

        public virtual string e() => 
            this.b.ToString();

        public virtual byte[] e(byte[] A_0, int A_1, int A_2)
        {
            DbType c = this.c;
            if (c > DbType.Binary)
            {
                if ((c != DbType.String) && ((c != DbType.AnsiStringFixedLength) && (c != DbType.StringFixedLength)))
                {
                    goto TR_0000;
                }
            }
            else if ((c != DbType.AnsiString) && (c != DbType.Binary))
            {
                goto TR_0000;
            }
            return this.ai(A_0, A_1, A_2);
        TR_0000:
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(byte[])));
        }

        public Type f() => 
            this.a;

        public virtual object f(byte[] A_0, int A_1, int A_2)
        {
            switch (this.b)
            {
                case DbType.AnsiString:
                case DbType.String:
                    return this.@as(A_0, A_1, A_2);

                case DbType.Binary:
                    return this.e(A_0, A_1, A_2);

                case DbType.Byte:
                    return this.aw(A_0, A_1, A_2);

                case DbType.Boolean:
                    return this.a(A_0, A_1, A_2);

                case DbType.Date:
                case DbType.DateTime:
                    return this.g(A_0, A_1, A_2);

                case DbType.Decimal:
                case DbType.UInt64:
                    return this.at(A_0, A_1, A_2);

                case DbType.Double:
                    return this.an(A_0, A_1, A_2);

                case DbType.Guid:
                    return this.j(A_0, A_1, A_2);

                case DbType.Int16:
                case DbType.SByte:
                    return this.d(A_0, A_1, A_2);

                case DbType.Int32:
                case DbType.UInt16:
                    return this.b(A_0, A_1, A_2);

                case DbType.Int64:
                case DbType.UInt32:
                    return this.i(A_0, A_1, A_2);

                case DbType.Single:
                    return this.ak(A_0, A_1, A_2);

                case DbType.Time:
                    return this.h(A_0, A_1, A_2);

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return this.x(A_0, A_1, A_2);
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(object)));
        }

        protected virtual Type g() => 
            this.a;

        public virtual DateTime g(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.String:
                    return DateTime.Parse(this.@as(A_0, A_1, A_2));

                case DbType.Byte:
                    return new DateTime((long) this.aa(A_0, A_1, A_2));

                case DbType.Date:
                case DbType.DateTime:
                    return this.ax(A_0, A_1, A_2);

                case DbType.Int16:
                    return new DateTime((long) this.z(A_0, A_1, A_2));

                case DbType.Int32:
                    return new DateTime((long) this.ag(A_0, A_1, A_2));

                case DbType.Int64:
                    return new DateTime(this.am(A_0, A_1, A_2));

                case DbType.SByte:
                    return new DateTime((long) ((sbyte) this.aa(A_0, A_1, A_2)));

                case DbType.Time:
                    return new DateTime(this.av(A_0, A_1, A_2).Ticks);

                case DbType.UInt16:
                    return new DateTime((long) this.ah(A_0, A_1, A_2));

                case DbType.UInt32:
                    return new DateTime((long) this.au(A_0, A_1, A_2));

                case DbType.UInt64:
                    return new DateTime((long) this.ad(A_0, A_1, A_2));

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return DateTime.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(DateTime)));
        }

        public virtual TimeSpan h(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.String:
                    return TimeSpan.Parse(this.@as(A_0, A_1, A_2));

                case DbType.Byte:
                    return new TimeSpan((long) this.aa(A_0, A_1, A_2));

                case DbType.Double:
                    return TimeSpan.FromDays(this.y(A_0, A_1, A_2));

                case DbType.Int16:
                    return new TimeSpan((long) this.z(A_0, A_1, A_2));

                case DbType.Int32:
                    return new TimeSpan((long) this.ag(A_0, A_1, A_2));

                case DbType.Int64:
                    return new TimeSpan(this.am(A_0, A_1, A_2));

                case DbType.SByte:
                    return new TimeSpan((long) ((sbyte) this.aa(A_0, A_1, A_2)));

                case DbType.Single:
                    return TimeSpan.FromDays((double) this.ac(A_0, A_1, A_2));

                case DbType.Time:
                    return this.av(A_0, A_1, A_2);

                case DbType.UInt16:
                    return new TimeSpan((long) this.ah(A_0, A_1, A_2));

                case DbType.UInt32:
                    return new TimeSpan((long) this.au(A_0, A_1, A_2));

                case DbType.UInt64:
                    return new TimeSpan((long) this.ad(A_0, A_1, A_2));

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return TimeSpan.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(TimeSpan)));
        }

        public virtual long i(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                    return this.aj(A_0, A_1, A_2);

                case DbType.Binary:
                    return a0.f(this.ai(A_0, A_1, A_2), 0);

                case DbType.Byte:
                    return (long) this.aa(A_0, A_1, A_2);

                case DbType.Decimal:
                    return (long) this.ab(A_0, A_1, A_2);

                case DbType.Double:
                    return (long) this.y(A_0, A_1, A_2);

                case DbType.Int16:
                    return (long) this.z(A_0, A_1, A_2);

                case DbType.Int32:
                    return (long) this.ag(A_0, A_1, A_2);

                case DbType.Int64:
                    return this.am(A_0, A_1, A_2);

                case DbType.SByte:
                    return (long) ((sbyte) this.aa(A_0, A_1, A_2));

                case DbType.Single:
                    return (long) this.ac(A_0, A_1, A_2);

                case DbType.String:
                    return long.Parse(this.@as(A_0, A_1, A_2));

                case DbType.UInt16:
                    return (long) this.ah(A_0, A_1, A_2);

                case DbType.UInt32:
                    return (long) this.au(A_0, A_1, A_2);

                case DbType.UInt64:
                    return (long) this.ad(A_0, A_1, A_2);

                case DbType.AnsiStringFixedLength:
                    return this.aj(A_0, A_1, A_2);

                case DbType.StringFixedLength:
                    return long.Parse(this.x(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(long)));
        }

        protected virtual Guid j(byte[] A_0, int A_1, int A_2) => 
            new Guid(this.@as(A_0, A_1, A_2));

        protected virtual double v(byte[] A_0, int A_1, int A_2)
        {
            double maxValue;
            string s = this.d.GetString(A_0, A_1, A_2);
            try
            {
                maxValue = double.Parse(s, CultureInfo.InvariantCulture);
            }
            catch (OverflowException exception)
            {
                try
                {
                    if (string.Compare(s, 1.7976931348623157E+308.ToString(CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        maxValue = double.MaxValue;
                    }
                    else
                    {
                        if (string.Compare(s, -1.7976931348623157E+308.ToString(CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase) != 0)
                        {
                            throw exception;
                        }
                        maxValue = double.MinValue;
                    }
                }
                catch
                {
                    throw exception;
                }
            }
            return maxValue;
        }

        public virtual char w(byte[] A_0, int A_1, int A_2)
        {
            switch (this.c)
            {
                case DbType.AnsiString:
                case DbType.Byte:
                case DbType.SByte:
                case DbType.AnsiStringFixedLength:
                    return (char) A_0[A_1];

                case DbType.Binary:
                    return this.aq(A_0, A_1, A_2);

                case DbType.Double:
                    return (char) ((ushort) this.y(A_0, A_1, A_2));

                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.String:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                case DbType.StringFixedLength:
                    return (char) ((ushort) this.z(A_0, A_1, A_2));

                case DbType.Single:
                    return (char) ((ushort) this.ac(A_0, A_1, A_2));
            }
            throw new InvalidOperationException(string.Format(Devart.Common.av.a("ConvertFailed"), this.g(), typeof(char)));
        }

        protected virtual string x(byte[] A_0, int A_1, int A_2)
        {
            int index = A_1;
            A_2 += A_1;
            if (ReferenceEquals(this.d, Encoding.Unicode))
            {
                while (true)
                {
                    if (((index + 1) >= A_2) || ((A_0[index] == 0) && (A_0[index + 1] == 0)))
                    {
                        while (((index - 1) > A_1) && ((A_0[index - 2] <= 0x20) && (A_0[index - 1] == 0)))
                        {
                            index -= 2;
                        }
                        break;
                    }
                    index += 2;
                }
            }
            if (ReferenceEquals(this.d, Encoding.BigEndianUnicode))
            {
                while (true)
                {
                    if (((index + 1) >= A_2) || ((A_0[index] == 0) && (A_0[index + 1] == 0)))
                    {
                        while (((index - 1) > A_1) && ((A_0[index - 2] == 0) && (A_0[index - 1] <= 0x20)))
                        {
                            index -= 2;
                        }
                        break;
                    }
                    index += 2;
                }
            }
            else
            {
                while (true)
                {
                    if ((index >= A_2) || (A_0[index] == 0))
                    {
                        while ((index > A_1) && (A_0[index - 1] <= 0x20))
                        {
                            index--;
                        }
                        break;
                    }
                    index++;
                }
            }
            return new string(this.d.GetChars(A_0, A_1, index - A_1));
        }

        protected virtual double y(byte[] A_0, int A_1, int A_2) => 
            a0.i(A_0, A_1);

        protected virtual short z(byte[] A_0, int A_1, int A_2) => 
            a0.h(A_0, A_1);
    }
}

