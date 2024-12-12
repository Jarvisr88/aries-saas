namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IRealTimeListChangeProcessor
    {
        void NotifyLastProcessedCommandCreationTime(DateTime sent);

        List<RealTimeProxyForObject> Cache { get; set; }

        ListChangedEventHandler ListChanged { get; }

        RealTimePropertyDescriptorCollection PropertyDescriptorsCollection { get; set; }

        bool IsCatchUp { get; set; }
    }
}

