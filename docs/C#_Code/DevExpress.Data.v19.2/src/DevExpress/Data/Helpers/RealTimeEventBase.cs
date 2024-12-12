namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class RealTimeEventBase
    {
        private readonly DateTime _Created;
        private volatile RealTimeEventBase _Before;
        public RealTimeEventBase After;
        public int Locks;

        protected RealTimeEventBase();
        public virtual void PostProcess(IRealTimeListChangeProcessor realTimeSourceCore);
        public abstract void Push(RealTimeEventsQueue queue);

        public RealTimeEventBase Before { get; set; }
    }
}

