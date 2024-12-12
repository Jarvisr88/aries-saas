namespace Devart.Common
{
    using System;

    internal sealed class DbConnectionPoolOptions
    {
        private readonly int a;
        private readonly bool b;
        private readonly TimeSpan c;
        private readonly int d;
        private readonly int e;
        private readonly bool f;
        private readonly bool g;
        private readonly bool h;
        private bool i;

        public DbConnectionPoolOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity, bool useDeactivateQueue)
        {
            this.f = poolByIdentity;
            this.e = minPoolSize;
            this.d = maxPoolSize;
            this.a = creationTimeout;
            if (loadBalanceTimeout != 0)
            {
                this.c = new TimeSpan(0, 0, loadBalanceTimeout);
                this.h = true;
            }
            this.b = hasTransactionAffinity;
            this.g = useDeactivateQueue;
        }

        public override bool Equals(object obj)
        {
            DbConnectionPoolOptions options = obj as DbConnectionPoolOptions;
            return ((options != null) ? ((this.a == options.a) && ((this.b == options.b) && ((this.c == options.c) && ((this.d == options.d) && ((this.e == options.e) && ((this.f == options.f) && ((this.g == options.g) && ((this.h == options.h) && (this.i == options.i))))))))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public int CreationTimeout =>
            this.a;

        public bool HasTransactionAffinity =>
            this.b;

        public TimeSpan LoadBalanceTimeout =>
            this.c;

        public int MaxPoolSize =>
            this.d;

        public int MinPoolSize =>
            this.e;

        public bool PoolByIdentity =>
            this.f;

        public bool UseDeactivateQueue =>
            this.g;

        public bool UseLoadBalancing =>
            this.h;

        public bool UsePerformanceMonitor
        {
            get => 
                this.i;
            set => 
                this.i = value;
        }
    }
}

