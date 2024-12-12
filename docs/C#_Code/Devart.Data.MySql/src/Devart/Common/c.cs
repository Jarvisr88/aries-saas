namespace Devart.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal abstract class c
    {
        private bool a;
        private List<ad> b;
        private object c;
        public ad d;
        public ad e;
        public ad f;
        public ad g;
        public ad h;
        public ad i;
        public ad j;
        public ad k;
        public ad l;
        public ad m;
        public ad n;
        public ad o;

        public c()
        {
            try
            {
                this.f();
                this.e();
                this.c = new object();
            }
            catch
            {
            }
        }

        public abstract string a();
        private void a(object A_0, EventArgs A_1)
        {
            this.c();
        }

        private void a(object A_0, UnhandledExceptionEventArgs A_1)
        {
            if ((A_1 != null) && A_1.IsTerminating)
            {
                this.c();
            }
        }

        public abstract string b();
        private void c()
        {
            using (List<ad>.Enumerator enumerator = this.b.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.b();
                }
            }
            this.b.Clear();
        }

        private void d()
        {
            if (!PerformanceCounterCategory.Exists(this.b()))
            {
                CounterCreationDataCollection counterData = new CounterCreationDataCollection();
                using (List<ad>.Enumerator enumerator = this.b.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        CounterCreationData data = enumerator.Current.c();
                        counterData.Add(data);
                    }
                }
                PerformanceCounterCategory.Create(this.b(), this.a(), PerformanceCounterCategoryType.MultiInstance, counterData);
            }
        }

        private void e()
        {
            if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                AppDomain.CurrentDomain.DomainUnload += new EventHandler(this.a);
            }
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(this.a);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.a);
        }

        private void f()
        {
            ad ad;
            this.b = new List<ad>(14);
            this.d = ad = new ad(this.b(), "HardConnectsPerSecond", "The number of actual connections per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);
            this.b.Add(ad);
            this.e = ad = new ad(this.b(), "HardDisconnectsPerSecond", "The number of actual disconnects per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);
            this.b.Add(ad);
            this.f = ad = new ad(this.b(), "SoftConnectsPerSecond", "The number of connections we get from the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);
            this.b.Add(ad);
            this.g = ad = new ad(this.b(), "SoftDisconnectsPerSecond", "The number of connections we return to the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);
            this.b.Add(ad);
            this.h = ad = new ad(this.b(), "NumberOfActiveConnections", "The number of connections currently in-use", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.i = ad = new ad(this.b(), "NumberOfFreeConnections", "The number of connections currently available for use", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.j = ad = new ad(this.b(), "NumberOfStasisConnections", "The number of connections currently waiting to be made ready for use", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.k = ad = new ad(this.b(), "NumberOfReclaimedConnections", "The number of connections we reclaim from GC'd external connections", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.l = ad = new ad(this.b(), "NumberOfPooledConnections", "The number of connections that are managed by the connection pooler", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.m = ad = new ad(this.b(), "NumberOfNonPooledConnections", "The number of connections that are not using connection pooling", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.n = ad = new ad(this.b(), "NumberOfActiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
            this.o = ad = new ad(this.b(), "NumberOfInactiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);
            this.b.Add(ad);
        }

        public void g()
        {
            if (!this.a)
            {
                lock (this.c)
                {
                    if (!this.a)
                    {
                        this.d();
                        using (List<ad>.Enumerator enumerator = this.b.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                enumerator.Current.a();
                            }
                        }
                        this.a = true;
                    }
                }
            }
        }

        public bool h() => 
            this.a;
    }
}

