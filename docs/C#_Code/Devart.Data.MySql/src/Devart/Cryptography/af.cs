namespace Devart.Cryptography
{
    using System;

    internal sealed class af : ao
    {
        private const int a = 0x40;
        private const int b = 0x10;
        private const int c = 0x80;
        private uint[] d;
        private uint e;
        private byte[] f;
        private int g;

        public af();
        protected override byte[] a();
        private void a(byte[] A_0, int A_1);
        private void a(byte[] A_0, int A_1, int A_2);
        public override void b();
        protected override void b(byte[] A_0, int A_1, int A_2);
        protected override void Finalize();

        private enum a : uint
        {
            public const Devart.Cryptography.af.a a = Devart.Cryptography.af.a.a;,
            public const Devart.Cryptography.af.a b = Devart.Cryptography.af.a.b;,
            public const Devart.Cryptography.af.a c = Devart.Cryptography.af.a.c;,
            public const Devart.Cryptography.af.a d = Devart.Cryptography.af.a.d;,
            public const Devart.Cryptography.af.a e = Devart.Cryptography.af.a.e;,
            public const Devart.Cryptography.af.a f = Devart.Cryptography.af.a.f;,
            public const Devart.Cryptography.af.a g = Devart.Cryptography.af.a.g;,
            public const Devart.Cryptography.af.a h = Devart.Cryptography.af.a.h;,
            public const Devart.Cryptography.af.a i = Devart.Cryptography.af.a.i;,
            public const Devart.Cryptography.af.a j = Devart.Cryptography.af.a.j;,
            public const Devart.Cryptography.af.a k = Devart.Cryptography.af.a.k;,
            public const Devart.Cryptography.af.a l = Devart.Cryptography.af.a.l;,
            public const Devart.Cryptography.af.a m = Devart.Cryptography.af.a.m;,
            public const Devart.Cryptography.af.a n = Devart.Cryptography.af.a.n;,
            public const Devart.Cryptography.af.a o = Devart.Cryptography.af.a.o;,
            public const Devart.Cryptography.af.a p = Devart.Cryptography.af.a.p;,
            public const Devart.Cryptography.af.a q = Devart.Cryptography.af.a.q;,
            public const Devart.Cryptography.af.a r = Devart.Cryptography.af.a.r;,
            public const Devart.Cryptography.af.a s = Devart.Cryptography.af.a.s;,
            public const Devart.Cryptography.af.a t = Devart.Cryptography.af.a.t;,
            public const Devart.Cryptography.af.a u = Devart.Cryptography.af.a.u;,
            public const Devart.Cryptography.af.a v = Devart.Cryptography.af.a.v;,
            public const Devart.Cryptography.af.a w = Devart.Cryptography.af.a.w;,
            public const Devart.Cryptography.af.a x = Devart.Cryptography.af.a.x;,
            public const Devart.Cryptography.af.a y = Devart.Cryptography.af.a.y;,
            public const Devart.Cryptography.af.a z = Devart.Cryptography.af.a.z;,
            public const Devart.Cryptography.af.a aa = Devart.Cryptography.af.a.aa;,
            public const Devart.Cryptography.af.a ab = Devart.Cryptography.af.a.ab;,
            public const Devart.Cryptography.af.a ac = Devart.Cryptography.af.a.ac;,
            public const Devart.Cryptography.af.a ad = Devart.Cryptography.af.a.ad;,
            public const Devart.Cryptography.af.a ae = Devart.Cryptography.af.a.ae;,
            public const Devart.Cryptography.af.a af = Devart.Cryptography.af.a.af;,
            public const Devart.Cryptography.af.a ag = Devart.Cryptography.af.a.ag;,
            public const Devart.Cryptography.af.a ah = Devart.Cryptography.af.a.ah;,
            public const Devart.Cryptography.af.a ai = Devart.Cryptography.af.a.ai;,
            public const Devart.Cryptography.af.a aj = Devart.Cryptography.af.a.aj;,
            public const Devart.Cryptography.af.a ak = Devart.Cryptography.af.a.ak;,
            public const Devart.Cryptography.af.a al = Devart.Cryptography.af.a.al;,
            public const Devart.Cryptography.af.a am = Devart.Cryptography.af.a.am;,
            public const Devart.Cryptography.af.a an = Devart.Cryptography.af.a.an;,
            public const Devart.Cryptography.af.a ao = Devart.Cryptography.af.a.ao;,
            public const Devart.Cryptography.af.a ap = Devart.Cryptography.af.a.ap;,
            public const Devart.Cryptography.af.a aq = Devart.Cryptography.af.a.aq;,
            public const Devart.Cryptography.af.a ar = Devart.Cryptography.af.a.ar;,
            public const Devart.Cryptography.af.a @as = Devart.Cryptography.af.a.@as;,
            public const Devart.Cryptography.af.a at = Devart.Cryptography.af.a.at;,
            public const Devart.Cryptography.af.a au = Devart.Cryptography.af.a.au;,
            public const Devart.Cryptography.af.a av = Devart.Cryptography.af.a.av;,
            public const Devart.Cryptography.af.a aw = Devart.Cryptography.af.a.aw;,
            public const Devart.Cryptography.af.a ax = Devart.Cryptography.af.a.ax;,
            public const Devart.Cryptography.af.a ay = Devart.Cryptography.af.a.ay;,
            public const Devart.Cryptography.af.a az = Devart.Cryptography.af.a.az;,
            public const Devart.Cryptography.af.a a0 = Devart.Cryptography.af.a.a0;,
            public const Devart.Cryptography.af.a a1 = Devart.Cryptography.af.a.a1;,
            public const Devart.Cryptography.af.a a2 = Devart.Cryptography.af.a.a2;,
            public const Devart.Cryptography.af.a a3 = Devart.Cryptography.af.a.a3;,
            public const Devart.Cryptography.af.a a4 = Devart.Cryptography.af.a.a4;,
            public const Devart.Cryptography.af.a a5 = Devart.Cryptography.af.a.a5;,
            public const Devart.Cryptography.af.a a6 = Devart.Cryptography.af.a.a6;,
            public const Devart.Cryptography.af.a a7 = Devart.Cryptography.af.a.a7;,
            public const Devart.Cryptography.af.a a8 = Devart.Cryptography.af.a.a8;,
            public const Devart.Cryptography.af.a a9 = Devart.Cryptography.af.a.a9;,
            public const Devart.Cryptography.af.a ba = Devart.Cryptography.af.a.ba;,
            public const Devart.Cryptography.af.a bb = Devart.Cryptography.af.a.bb;
        }
    }
}

