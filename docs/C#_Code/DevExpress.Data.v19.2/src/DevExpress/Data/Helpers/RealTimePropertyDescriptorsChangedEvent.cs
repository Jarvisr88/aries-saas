namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class RealTimePropertyDescriptorsChangedEvent : RealTimeResetEvent
    {
        public RealTimePropertyDescriptorCollection properties;

        public RealTimePropertyDescriptorsChangedEvent(RealTimePropertyDescriptorCollection properties, RealTimeProxyForObject[] allValues);
        public override void PostProcess(IRealTimeListChangeProcessor realTimeSourceCore);
    }
}

