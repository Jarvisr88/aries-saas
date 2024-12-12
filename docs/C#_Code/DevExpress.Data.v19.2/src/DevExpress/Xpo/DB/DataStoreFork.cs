namespace DevExpress.Xpo.DB
{
    using System;
    using System.Threading;

    public class DataStoreFork : DataStoreForkBase
    {
        private readonly IDataStore[] providersList;
        private int free;
        private readonly ManualResetEvent providerFreeEvent;
        private readonly DevExpress.Xpo.DB.AutoCreateOption autoCreateOption;

        public DataStoreFork(params IDataStore[] providers) : this(((providers == null) || (providers.Length == 0)) ? DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists : providers[0].AutoCreateOption, providers)
        {
        }

        public DataStoreFork(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, params IDataStore[] providers)
        {
            this.providerFreeEvent = new ManualResetEvent(true);
            if ((providers == null) || (providers.Length == 0))
            {
                throw new ArgumentNullException("providers");
            }
            this.autoCreateOption = autoCreateOption;
            this.providersList = (IDataStore[]) providers.Clone();
            this.free = this.providersList.Length;
        }

        public override IDataStore AcquireChangeProvider() => 
            this.AcquireReadProvider();

        public override IDataStore AcquireReadProvider()
        {
            IDataStore store;
            while (true)
            {
                object syncRoot = this.SyncRoot;
                lock (syncRoot)
                {
                    if (this.free > 0)
                    {
                        this.free--;
                        store = this.providersList[this.free];
                        break;
                    }
                    this.providerFreeEvent.Reset();
                }
                this.providerFreeEvent.WaitOne();
            }
            return store;
        }

        public override void ReleaseChangeProvider(IDataStore provider)
        {
            this.ReleaseReadProvider(provider);
        }

        public override void ReleaseReadProvider(IDataStore provider)
        {
            object syncRoot = this.SyncRoot;
            lock (syncRoot)
            {
                if (this.free == 0)
                {
                    this.providerFreeEvent.Set();
                }
                this.providersList[this.free] = provider;
                this.free++;
            }
        }

        public object SyncRoot =>
            this;

        public override DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            this.autoCreateOption;
    }
}

