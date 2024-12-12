namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class RealTimeResetEvent : RealTimeEventBase
    {
        public RealTimeProxyForObject[] AllRowsData;

        public RealTimeResetEvent(RealTimeProxyForObject[] allValues);
        public override void PostProcess(IRealTimeListChangeProcessor realTimeSourceCore);
        public override void Push(RealTimeEventsQueue queue);
    }
}

