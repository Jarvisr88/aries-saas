namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class RealTimeRowEvent : RealTimeEventBase
    {
        internal int _From;
        internal int _To;
        public RealTimeProxyForObject FieldsChangeData;

        public RealTimeRowEvent(int? from, int? to, RealTimeProxyForObject value);
        public override void PostProcess(IRealTimeListChangeProcessor realTimeSourceCore);
        public override void Push(RealTimeEventsQueue queue);
    }
}

