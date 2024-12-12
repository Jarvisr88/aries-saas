namespace Devart.Common
{
    using System;

    internal class u
    {
        private string a;
        private string b;
        private bool c;
        private int d;
        private int e;

        public u(Devart.Common.u A_0)
        {
            this.a = A_0.a;
            this.b = A_0.b;
            this.d = A_0.d;
            this.e = A_0.e;
            this.c = A_0.c;
        }

        public u(string A_0, string A_1, int A_2, int A_3, bool A_4)
        {
            this.a = A_0;
            this.b = A_1;
            this.c = A_4;
            this.d = A_2;
            if (A_3 == 0)
            {
                this.e = A_0.Length + 1;
            }
            else
            {
                this.e = A_3;
            }
        }

        public bool a() => 
            this.c;

        public void a(bool A_0)
        {
            this.c = A_0;
        }

        public void a(int A_0)
        {
            this.d = (A_0 + this.e) + 1;
        }

        public void a(string A_0)
        {
            this.b = A_0;
        }

        public int b() => 
            this.d;

        public void b(int A_0)
        {
            this.d = A_0;
        }

        public int c() => 
            (this.d - this.e) - 1;

        public string d() => 
            this.b;

        public int e() => 
            this.e;

        public string f() => 
            this.a;
    }
}

