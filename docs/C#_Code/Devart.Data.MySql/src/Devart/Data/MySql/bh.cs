namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class bh : af
    {
        private readonly a1 a;
        private static Hashtable b;
        private const long c = 10L;
        private const long d = 0x2710L;
        private const long e = 0x989680L;
        private const long f = 0x23c34600L;
        private const long g = 0x861c46800L;
        private const int h = 6;

        static bh();
        private bh(a1 A_0, bool A_1, Encoding A_2);
        private bool a();
        internal static DbType a(a1 A_0);
        public static int a(byte[] A_0, int A_1);
        public static bh a(a1 A_0, bool A_1, Encoding A_2);
        public override bool a(byte[] A_0, int A_1, int A_2);
        private static void a(byte[] A_0, int A_1, int A_2, out int A_3, out int A_4, out int A_5, out int A_6, out bool A_7);
        public Type b();
        public override int b(byte[] A_0, int A_1, int A_2);
        public override string c(byte[] A_0, int A_1, int A_2);
        public override short d(byte[] A_0, int A_1, int A_2);
        public override byte[] e(byte[] A_0, int A_1, int A_2);
        public override object f(byte[] A_0, int A_1, int A_2);
        public override DateTime g(byte[] A_0, int A_1, int A_2);
        public override TimeSpan h(byte[] A_0, int A_1, int A_2);
        public override long i(byte[] A_0, int A_1, int A_2);
        protected override Guid j(byte[] A_0, int A_1, int A_2);
        public virtual MySqlDecimal k(byte[] A_0, int A_1, int A_2);
        public sbyte l(byte[] A_0, int A_1, int A_2);
        public MySqlDateTime m(byte[] A_0, int A_1, int A_2);
        public ulong n(byte[] A_0, int A_1, int A_2);
        public uint o(byte[] A_0, int A_1, int A_2);
        public MySqlText p(byte[] A_0, int A_1, int A_2);
        public MySqlBinaryString q(byte[] A_0, int A_1, int A_2);
        public object r(byte[] A_0, int A_1, int A_2);
        public MySqlBlob s(byte[] A_0, int A_1, int A_2);
        public MySqlGuid t(byte[] A_0, int A_1, int A_2);
        public ushort u(byte[] A_0, int A_1, int A_2);
    }
}

