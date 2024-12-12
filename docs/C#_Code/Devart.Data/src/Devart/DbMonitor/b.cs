namespace Devart.DbMonitor
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct b
    {
        private string a;
        private string b;
        private string c;
        private string d;
        public b(string A_0, string A_1, string A_2, string A_3)
        {
            this.a = A_0;
            this.b = A_1;
            this.c = A_2;
            this.d = A_3;
        }

        public string d() => 
            this.a;

        public void c(string A_0)
        {
            this.a = A_0;
        }

        public string b() => 
            this.b;

        public void b(string A_0)
        {
            this.b = A_0;
        }

        public string a() => 
            this.c;

        public void a(string A_0)
        {
            this.c = A_0;
        }

        public string c() => 
            this.d;

        public void d(string A_0)
        {
            this.d = A_0;
        }
    }
}

