namespace #c
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;

    internal static class #e
    {
        public static string #0;

        private static bool #8e(Assembly #Fvf, Assembly #Gvf);
        public static byte[] #9e(byte[] #vb);
        public static byte[] #af(byte[] #vb);
        private static byte[] #af(byte[] #vb, int #7gb, byte[] #1Nc, byte[] #Evf);
        public static byte[] #bf(byte[] #vb, byte[] #1Nc, byte[] #Evf);
        public static byte[] #cf(byte[] #vb, byte[] #1Nc, byte[] #Evf);
        private static ICryptoTransform #d2h(byte[] #1Nc, byte[] #Evf, bool #lVb);
        private static ICryptoTransform #e2h(byte[] #1Nc, byte[] #Evf, bool #lVb);

        internal class #f
        {
            private const int #1 = 0;
            private const int #2 = 1;
            private const int #3 = 2;
            private const int #4 = 3;
            private const int #5 = 4;
            private const int #6 = 5;
            private const int #7 = 6;
            private const int #8 = 7;
            private const int #9 = 8;
            private const int #ab = 9;
            private const int #bb = 10;
            private const int #cb = 11;
            private const int #db = 12;
            private static readonly int[] #eb;
            private static readonly int[] #fb;
            private static readonly int[] #gb;
            private static readonly int[] #hb;
            private int #ib;
            private int #jb;
            private int #kb;
            private int #lb;
            private int #mb;
            private bool #nb;
            private #e.#h #ob;
            private #c.#e.#i #i;
            private #e.#k #pb;
            private #e.#j #qb;
            private #e.#j #rb;

            private bool #df();
            private bool #ef();
            public int #ff(byte[] #Rc, int #fd, int #Hvf);
            static #f();
            public #f(byte[] bytes);
        }

        internal class #h
        {
            private byte[] #sb;
            private int #tb;
            private int #ub;
            private uint #vb;
            private int #wb;

            public int #gf(int #Rze);
            public void #hf(int #Rze);
            public void #kf();
            public int #mf(byte[] #Ivf, int #fd, int #mc);
            public void #of(byte[] #Rc, int #Jvf, int #Hvf);
            public void Reset();

            public int AvailableBits { get; }

            public int AvailableBytes { get; }

            public bool IsNeedingInput { get; }
        }

        internal class #i
        {
            private const int #xb = 0x8000;
            private const int #yb = 0x7fff;
            private byte[] #sb;
            private int #zb;
            private int #Ab;

            public void #pf(int #Kvf);
            private void #qf(int #Lvf, int #Hvf, int #Mvf);
            public void #rf(int #Hvf, int #Mvf);
            public int #sf(#e.#h #ob, int #Hvf);
            public void #tf(byte[] #Nvf, int #fd, int #Hvf);
            public int #uf();
            public int #vf();
            public int #wf(byte[] #Ivf, int #fd, int #Hvf);
            public #i();
            public void Reset();
        }

        internal class #j
        {
            private const int #Bb = 15;
            private short[] #n;
            public static readonly #e.#j #Cb;
            public static readonly #e.#j #Db;

            private void #xf(byte[] #Ovf);
            public int #yf(#e.#h #ob);
            static #j();
            public #j(byte[] codeLengths);
        }

        internal class #k
        {
            private const int #Eb = 0;
            private const int #Fb = 1;
            private const int #Gb = 2;
            private const int #Hb = 3;
            private const int #Ib = 4;
            private const int #Jb = 5;
            private static readonly int[] #Kb;
            private static readonly int[] #Lb;
            private byte[] #Hb;
            private byte[] #Mb;
            private #e.#j #Nb;
            private int #ib;
            private int #Eb;
            private int #Fb;
            private int #Gb;
            private int #Ob;
            private int #Pb;
            private byte #Qb;
            private int #Rb;
            private static readonly int[] #Sb;

            public #e.#j #Af();
            public bool #ef(#e.#h #ob);
            public #e.#j #zf();
            static #k();
        }

        internal class #l
        {
            private const int #Tb = 4;
            private const int #Ub = 8;
            private const int #Vb = 0x10;
            private const int #Wb = 20;
            private const int #Xb = 0x1c;
            private const int #Yb = 30;
            private int #Zb;
            private long #0b;
            private #e.#p #1b;
            private #e.#o #2b;

            public void #Cf();
            public int #Ef(byte[] #Ivf);
            public void #of(byte[] #vb);
            public #l();

            public long TotalOut { get; }

            public bool IsFinished { get; }

            public bool IsNeedingInput { get; }
        }

        internal class #m
        {
            private const int #3b = 0x4000;
            private const int #4b = 0x11e;
            private const int #5b = 30;
            private const int #6b = 0x13;
            private const int #7b = 0x10;
            private const int #8b = 0x11;
            private const int #9b = 0x12;
            private const int #ac = 0x100;
            private static readonly int[] #Sb;
            private static readonly byte[] #bc;
            private #e.#p #1b;
            private #e.#m.#n #cc;
            private #e.#m.#n #rb;
            private #e.#m.#n #Nb;
            private short[] #dc;
            private byte[] #ec;
            private int #fc;
            private int #gc;
            private static readonly short[] #hc;
            private static readonly byte[] #ic;
            private static readonly short[] #jc;
            private static readonly byte[] #kc;

            public static short #Ff(int #Pvf);
            public void #Gf();
            private int #Hf(int #Hvf);
            private int #If(int #Qvf);
            public void #Jf(int #Rvf);
            public void #Kf();
            public void #Lf(byte[] #Svf, int #Tvf, int #Uvf, bool #Vvf);
            public void #Mf(byte[] #Svf, int #Tvf, int #Uvf, bool #Vvf);
            public bool #Nf();
            public bool #Of(int #Wvf);
            public bool #Pf(int #Mvf, int #Hvf);
            static #m();
            public #m(#e.#p pending);

            public class #n
            {
                public short[] #lc;
                public byte[] #mc;
                public int #nc;
                public int #oc;
                private short[] #pc;
                private int[] #qc;
                private int #rc;
                private #e.#m #sc;

                public void #Qf(int #7G);
                public void #Rf(short[] #Xvf, byte[] #Yvf);
                public void #Sf();
                private void #Tf(int[] #Zvf);
                public int #Uf();
                public void #Vf(#e.#m.#n #Nb);
                public void #Wf(#e.#m.#n #Nb);
                public void #xf();
                public #n(#e.#m dh, int elems, int minCodes, int maxLength);
            }
        }

        internal class #o
        {
            private const int #tc = 0x102;
            private const int #uc = 3;
            private const int #vc = 0x8000;
            private const int #wc = 0x7fff;
            private const int #xc = 0x8000;
            private const int #yc = 0x7fff;
            private const int #zc = 5;
            private const int #Ac = 0x106;
            private const int #Bc = 0x7efa;
            private const int #Cc = 0x1000;
            private int #Dc;
            private short[] #Ec;
            private short[] #Fc;
            private int #Gc;
            private int #Hc;
            private bool #Ic;
            private int #Jc;
            private int #Kc;
            private int #Lc;
            private byte[] #sb;
            private byte[] #Mc;
            private int #Nc;
            private int #Oc;
            private int #Pc;
            private #e.#p #1b;
            private #e.#m #Qc;

            public void #0f();
            private bool #1f(int #0vf);
            private bool #2f(bool #ag, bool #Cf);
            public bool #3f();
            public bool #Ef(bool #ag, bool #Cf);
            public void #of(byte[] #vb);
            private void #Xf();
            private int #Yf();
            private void #Zf();
            public #o(#e.#p pending);
        }

        internal class #p
        {
            protected byte[] #Rc;
            private int #Sc;
            private int #Tc;
            private uint #Uc;
            private int #Vc;

            public void #4f(int #hUb);
            public void #5f(byte[] #1vf, int #fd, int #Hvf);
            public void #7f();
            public void #8f(int #fUb, int #LX);
            public #p();
            public int Flush(byte[] #Ivf, int #fd, int #mc);

            public int BitCount { get; }

            public bool IsFlushed { get; }
        }

        internal class #q : MemoryStream
        {
            public void #4f(int #Ld);
            public void #bg(int #Ld);
            public int #cg();
            public int #dg();
            public #q();
            public #q(byte[] buffer);
        }
    }
}

