namespace Devart.Security.Ssh
{
    using Devart.Cryptography;
    using Devart.Cryptography.PKI;
    using System;
    using System.Security.Cryptography;
    using System.Threading;

    internal class ao
    {
        private aq a;
        private Devart.Security.Ssh.f b;
        private aj c;
        private byte[] d;
        private byte[] e;
        private bool f;
        private ManualResetEvent g;
        private Devart.Security.Ssh.ao.a h;
        private const int i = 0x400;
        private const int j = 0x800;
        private const int k = 0x2000;
        private aj l;
        private aj m;
        private aj n;
        private aj o;
        private aj p;
        private Devart.Cryptography.PKI.i q;
        private byte[] r;
        private byte[] s;
        private SymmetricAlgorithm t;
        private SymmetricAlgorithm u;
        private Devart.Cryptography.e v;
        private Devart.Cryptography.e w;
        private static aj x;
        private static aj y;

        public ao(aq A_0, byte[] A_1);
        private static aj a();
        private byte[] a(byte[] A_0);
        private HashAlgorithm a(ab A_0);
        private void a(ac A_0);
        private aa a(string A_0);
        private bool a(byte[] A_0, byte[] A_1, byte[] A_2);
        private void a(Devart.Security.Ssh.i A_0, byte[] A_1, byte[] A_2);
        private static void a(string A_0, string A_1, string A_2);
        private byte[] a(aj A_0, byte[] A_1, char A_2, int A_3);
        private static aj b();
        private bool b(ab A_0);
        private bool b(ac A_0);
        private Devart.Security.Ssh.g b(string A_0);
        private void b(Devart.Security.Ssh.i A_0, byte[] A_1, byte[] A_2);
        private string c();
        private aj c(ab A_0);
        private bool c(ac A_0);
        private Devart.Cryptography.PKI.p c(string A_0);
        private void c(Devart.Security.Ssh.i A_0, byte[] A_1, byte[] A_2);
        private string d();
        private void d(ac A_0);
        private ab d(string A_0);
        private string e();
        private void e(ac A_0);
        private string f();
        public void f(ac A_0);
        private void g();
        private void h();
        private void i();
        private void j();
        private void k();
        public bool l();
        public void m();

        private enum a
        {
            public const Devart.Security.Ssh.ao.a a = Devart.Security.Ssh.ao.a.a;,
            public const Devart.Security.Ssh.ao.a b = Devart.Security.Ssh.ao.a.b;,
            public const Devart.Security.Ssh.ao.a c = Devart.Security.Ssh.ao.a.c;,
            public const Devart.Security.Ssh.ao.a d = Devart.Security.Ssh.ao.a.d;,
            public const Devart.Security.Ssh.ao.a e = Devart.Security.Ssh.ao.a.e;,
            public const Devart.Security.Ssh.ao.a f = Devart.Security.Ssh.ao.a.f;,
            public const Devart.Security.Ssh.ao.a g = Devart.Security.Ssh.ao.a.g;
        }
    }
}

