namespace Devart.Security.SSL
{
    using Devart.Security;
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class j : Devart.Security.SSL.g
    {
        protected byte[] a;
        protected byte[] b;

        public j(Devart.Security.SSL.g A_0);
        public j(h A_0, l A_1);
        protected byte[] a(Devart.Security.e A_0);
        protected override au a(ai A_0);
        protected override au a(ar A_0);
        protected au b(ai A_0);
        public override au b(byte[] A_0);
        private ag c(byte[] A_0);
        protected au c(ai A_0);
        protected override byte[] d();
        protected au d(ai A_0);
        private byte[] d(byte[] A_0);
        protected override byte[] e();
        protected au e(ai A_0);
        protected au f(ai A_0);
        protected au g(ai A_0);

        [Serializable, CompilerGenerated]
        private sealed class a
        {
            public static readonly Devart.Security.SSL.j.a a;
            public static Predicate<@as> b;

            static a();
            internal bool a(@as A_0);
        }
    }
}

