namespace Devart.Common
{
    using System;
    using System.Text;

    internal class r
    {
        protected byte[] a;
        protected int b;
        protected int c;
        private char[] d;
        private Encoding e;
        protected int f;

        public r(int A_0, Encoding A_1)
        {
            this.a = new byte[A_0];
            this.b = 0;
            this.e = A_1;
        }

        public int a() => 
            this.b(this.l());

        public void a(int A_0)
        {
            this.f = A_0;
        }

        private char[] a(ref int A_0)
        {
            A_0 = Math.Min(A_0, this.l());
            this.b += A_0;
            return this.e.GetChars(this.a, this.b, A_0);
        }

        public void a(int A_0, int A_1)
        {
            this.c = A_1;
            this.b = A_0;
        }

        public int a(char[] A_0, int A_1, int A_2)
        {
            if (this.d == null)
            {
                throw new IndexOutOfRangeException("CharBuffer not initialize.");
            }
            if (this.f >= this.j())
            {
                throw new IndexOutOfRangeException("End of Data");
            }
            int length = Math.Min(this.c(), A_2);
            Array.Copy(this.d, this.f, A_0, A_1, length);
            this.f += length;
            return length;
        }

        internal static int a(byte[] A_0, int A_1, int A_2, int A_3)
        {
            bool flag = false;
            int num = 0;
            byte num2 = A_0[A_2 - 1];
            if (num2 <= 0x7f)
            {
                num = 0;
            }
            else if ((((num2 >= 0xc2) && (num2 <= 0xdf)) || ((num2 >= 0xe0) && (num2 <= 0xef))) || ((num2 >= 240) && (num2 <= 0xf4)))
            {
                num = 1;
            }
            else if ((num2 & 0xc0) != 0x80)
            {
                flag = true;
            }
            else
            {
                int num3 = 2;
                while (true)
                {
                    if ((A_3 - A_1) < num3)
                    {
                        flag = true;
                    }
                    else
                    {
                        num2 = A_0[A_2 - num3];
                        if ((num2 & 0xc0) == 0x80)
                        {
                            if (num3 < 4)
                            {
                                num3++;
                                continue;
                            }
                            flag = true;
                        }
                        else if ((num2 >= 0xc2) && (num2 <= 0xdf))
                        {
                            if ((num3 == 3) || (num3 == 4))
                            {
                                flag = true;
                            }
                        }
                        else if ((num2 >= 0xe0) && (num2 <= 0xef))
                        {
                            if (num3 == 4)
                            {
                                flag = true;
                            }
                            else if (num3 == 2)
                            {
                                num = 2;
                            }
                        }
                        else if ((num2 < 240) || (num2 > 0xf4))
                        {
                            flag = true;
                        }
                        else if (num3 == 3)
                        {
                            num = 3;
                        }
                        else if (num3 == 2)
                        {
                            num = 2;
                        }
                    }
                    break;
                }
            }
            if (flag)
            {
                throw new InvalidOperationException("Bad byte sequence of UTF8.");
            }
            A_3 -= num;
            return A_3;
        }

        public void b()
        {
            this.c = 0;
            this.b = 0;
            this.d = null;
            this.f = 0;
        }

        public int b(int A_0)
        {
            this.d = this.c(ref A_0);
            this.f = 0;
            return A_0;
        }

        private char[] b(ref int A_0)
        {
            A_0 = Math.Min(A_0, this.l());
            if (A_0 == 0)
            {
                return new char[0];
            }
            int num = this.g() + A_0;
            A_0 = a(this.a, this.b, num, A_0);
            this.b += A_0;
            return this.e.GetChars(this.a, this.b, A_0);
        }

        public int c() => 
            this.j() - this.f;

        public void c(int A_0)
        {
            if ((this.d() - this.g()) <= A_0)
            {
                int num = this.d() - this.g();
                int index = 0;
                while (true)
                {
                    if (index >= num)
                    {
                        this.a(0, num);
                        break;
                    }
                    this.a[index] = this.a[this.g() + index];
                    index++;
                }
            }
            this.d = null;
            this.f = 0;
        }

        private char[] c(ref int A_0) => 
            !ReferenceEquals(this.e, Encoding.UTF8) ? this.a(ref A_0) : this.b(ref A_0);

        public int d() => 
            this.c;

        public int d(int A_0)
        {
            if (this.d == null)
            {
                throw new IndexOutOfRangeException("CharBuffer not initialize.");
            }
            if (this.f >= this.j())
            {
                throw new IndexOutOfRangeException("End of Data");
            }
            int num = Math.Min(this.c(), A_0);
            this.f += num;
            return num;
        }

        public int e() => 
            this.a.Length;

        public byte f()
        {
            if (this.b >= this.c)
            {
                throw new IndexOutOfRangeException("End of Data");
            }
            int b = this.b;
            this.b = b + 1;
            return this.a[b];
        }

        public int g() => 
            this.b;

        public byte[] h() => 
            this.a;

        public int i() => 
            this.f;

        public int j() => 
            (this.d == null) ? 0 : this.d.Length;

        public bool k() => 
            this.b >= this.c;

        public int l() => 
            this.c - this.b;
    }
}

