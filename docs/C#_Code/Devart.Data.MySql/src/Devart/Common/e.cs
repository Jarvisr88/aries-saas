namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Threading;

    internal class e
    {
        private Queue a;
        private byte[] b;
        private byte[] c;
        private int d;
        private int e;
        private int f;
        private int g;
        private ManualResetEvent h;
        private ManualResetEvent i;
        private object j = new object();

        public e(int A_0, int A_1)
        {
            this.a = new Queue(A_0);
            this.h = new ManualResetEvent(false);
            this.i = new ManualResetEvent(false);
            this.c = new byte[A_1];
            this.b = this.c;
            this.a.Enqueue(this.c);
            this.f = A_1;
            this.g = A_0;
        }

        public void a()
        {
            this.h.Set();
            this.i.Set();
        }

        public int a(byte[] A_0, int A_1, int A_2)
        {
            int num;
            if (this.a.Count != 1)
            {
                num = Math.Min(this.f - this.d, A_2);
            }
            else
            {
                lock (this.j)
                {
                    num = (this.a.Count != 1) ? Math.Min(this.f - this.d, A_2) : Math.Min(this.e - this.d, A_2);
                }
            }
            Buffer.BlockCopy(this.b, this.d, A_0, A_1, num);
            this.d += num;
            if (this.d >= this.f)
            {
                if (this.a.Count > 1)
                {
                    this.a.Dequeue();
                    this.b = (byte[]) this.a.Peek();
                    this.d = 0;
                }
                else
                {
                    lock (this.j)
                    {
                        if (this.a.Count > 1)
                        {
                            this.a.Dequeue();
                            this.b = (byte[]) this.a.Peek();
                            this.d = 0;
                        }
                        else if (this.d != this.e)
                        {
                            this.d = 0;
                        }
                    }
                }
                if (this.d() < this.g)
                {
                    this.i.Set();
                }
            }
            return num;
        }

        public int b()
        {
            lock (this.j)
            {
                return ((this.a.Count == 0) ? 0 : ((((this.a.Count - 1) * this.f) + this.e) - this.d));
            }
        }

        public void b(byte[] A_0, int A_1, int A_2)
        {
            while (A_2 > 0)
            {
                int count = Math.Min(this.f - this.e, A_2);
                Buffer.BlockCopy(A_0, A_1, this.c, this.e, count);
                lock (this.j)
                {
                    this.e += count;
                }
                if (this.e >= this.f)
                {
                    this.c = new byte[this.f];
                    object j = this.j;
                    lock (j)
                    {
                        this.a.Enqueue(this.c);
                        this.e = 0;
                    }
                }
                this.h.Set();
                A_1 += count;
                A_2 -= count;
            }
        }

        public ManualResetEvent c()
        {
            if (this.b() > 0)
            {
                return null;
            }
            this.h.Reset();
            return this.h;
        }

        public int d()
        {
            lock (this.j)
            {
                return this.a.Count;
            }
        }

        public ManualResetEvent e()
        {
            if (this.d() < this.g)
            {
                return null;
            }
            this.i.Reset();
            return this.i;
        }
    }
}

