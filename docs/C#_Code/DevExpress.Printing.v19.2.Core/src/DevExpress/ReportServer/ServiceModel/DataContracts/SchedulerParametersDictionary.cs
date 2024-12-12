namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [CollectionDataContract(ItemName="Item", KeyName="Name", ValueName="Parameter")]
    public class SchedulerParametersDictionary : Dictionary<string, SchedulerParameter>
    {
    }
}

