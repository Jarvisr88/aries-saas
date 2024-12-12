namespace Devart.Common
{
    using System;

    internal class o : ILocalFailoverManager
    {
        private DbConnectionBase a;
        private int b;
        private int c;
        private RetryMode d = RetryMode.Reexecute;
        private bool e;

        internal o(DbConnectionBase A_0)
        {
            this.a = A_0;
        }

        public ILocalFailoverManager a() => 
            this.a(false);

        public ILocalFailoverManager a(bool A_0)
        {
            this.b++;
            if (this.b <= 1)
            {
                this.e = A_0;
            }
            return this;
        }

        public RetryMode a(object A_0, ConnectionLostCause A_1, RetryMode A_2, Exception A_3)
        {
            if ((this.d == RetryMode.Reexecute) && (A_2 == RetryMode.Raise))
            {
                this.d = RetryMode.Raise;
            }
            return ((this.b > 1) ? RetryMode.Raise : this.a.a(A_0, A_1, A_2, A_3, ref this.c, this.e));
        }

        public void Dispose()
        {
            this.b--;
            if (this.b == 0)
            {
                this.d = RetryMode.Reexecute;
                this.c = 0;
                this.e = false;
            }
        }
    }
}

