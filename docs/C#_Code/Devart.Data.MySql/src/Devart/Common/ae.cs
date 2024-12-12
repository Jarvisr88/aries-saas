namespace Devart.Common
{
    using System;
    using System.Collections;

    internal class ae
    {
        private Queue a;
        private byte[] b;
        private byte[] c;
        private int d;
        private int e;
        private int f;

        public ae(int A_0, int A_1)
        {
            this.a = new Queue(A_0);
            this.c = new byte[A_1];
            this.b = this.c;
            this.a.Enqueue(this.c);
            this.f = A_1;
        }

        public int a() => 
            (this.a.Count == 0) ? 0 : ((((this.a.Count - 1) * this.f) + this.e) - this.d);

        public byte a(int A_0) => 
            ((this.d + A_0) >= this.f) ? ((byte[]) this.a.ToArray().GetValue(1))[A_0 - (this.f - this.d)] : this.b[this.d + A_0];

        public void a(byte[] A_0, int A_1, int A_2)
        {
            while (A_2 > 0)
            {
                int count = (this.a.Count != 1) ? Math.Min(this.f - this.d, A_2) : Math.Min(this.e - this.d, A_2);
                Buffer.BlockCopy(this.b, this.d, A_0, A_1, count);
                this.d += count;
                A_1 += count;
                A_2 -= count;
                if (this.d >= this.f)
                {
                    this.a.Dequeue();
                    this.d = 0;
                    this.b = (byte[]) this.a.Peek();
                    if (this.b == null)
                    {
                        throw new ArgumentException("There is not enough data to read.");
                    }
                }
            }
        }

        public void b(byte[] A_0, int A_1, int A_2)
        {
            while (A_2 > 0)
            {
                int count = Math.Min(this.f - this.e, A_2);
                Buffer.BlockCopy(A_0, A_1, this.c, this.e, count);
                this.e += count;
                if (this.e >= this.f)
                {
                    this.c = new byte[this.f];
                    this.a.Enqueue(this.c);
                    this.e = 0;
                }
                A_1 += count;
                A_2 -= count;
            }
        }
    }
}

